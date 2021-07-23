using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Bentley.GeoCoordinatesNET;
using Bentley.MstnPlatformNET;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Dgn.Config;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Bimangle.ForgeEngine.Dgn.Georeferncing
{
    class GeoreferncingHost : IGeoreferncingHost, IDisposable
    {
        private string _InputFilePath;
        private ProjValidator _ProjValidator;
        private AppConfigCesium3DTiles _LocalData;

        public static GeoreferncingHost Create(string homeFolder, AppConfigCesium3DTiles localData)
        {
            var inputFilePath = Session.Instance.GetActiveFileName();
            return Create(inputFilePath, homeFolder, localData);
        }

        public static GeoreferncingHost Create(string inputFilePath, string homeFolder, AppConfigCesium3DTiles localData)
        {
            ProjValidator projValidator = null;
            try
            {
                projValidator = ProjValidator.Create(homeFolder);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return new GeoreferncingHost(inputFilePath, projValidator, localData);
        }

        private GeoreferncingHost(string inputFilePath, ProjValidator projValidator, AppConfigCesium3DTiles localData)
        {
            _InputFilePath = inputFilePath;
            _ProjValidator = projValidator;
            _LocalData = localData;
        }

        /// <summary>
        /// 预加载
        /// </summary>
        public void Preload()
        {
            if (_ProjValidator == null) return;

            //用单独的线程预加载初始化 ProjValidator
            ThreadPool.QueueUserWorkItem(_ => _ProjValidator?.Init());
        }

        #region IDisposable

        public void Dispose()
        {
            _ProjValidator?.Dispose();
            _ProjValidator = null;

            _InputFilePath = null;
        }

        #endregion

        #region Implementation of IGeoreferncingHost

        public bool CheckProjDefinition(string projDefinition, out string projWkt)
        {
            if (_ProjValidator != null && 
                _ProjValidator.Check(projDefinition, out projWkt))
            {
                return true;
            }

            projWkt = null;
            return false;
        }

        /// <summary>
        /// 从目标文件加载 proj 信息
        /// </summary>
        /// <param name="projFilePath"></param>
        /// <returns></returns>
        public string GetProjDefinition(string projFilePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(projFilePath)) return null;
                if (File.Exists(projFilePath) == false) return null;

                var projContent = File.ReadAllText(projFilePath, Encoding.UTF8);
                if (CheckProjDefinition(projContent, out _))
                {
                    return projContent;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return null;
        }

        public IList<ProjSourceItem> GetProjSourceItems()
        {
            var items = new List<ProjSourceItem>();

            //加入自定义选项
            {
                var label = ProjSourceType.Custom.GetString();
                items.Add(new ProjSourceItem(label, ProjSourceType.Custom, null, null));
            }

            //加入内置坐标系定义选项
            {
                var projDefinition = GetEmbedProjDefinition();
                if (string.IsNullOrWhiteSpace(projDefinition) == false)
                {
                    var label = ProjSourceType.Embed.GetString();
                    items.Add(new ProjSourceItem(label, ProjSourceType.Embed, null, projDefinition));
                }
            }


            //加入项目默认定义
            {
                var projDefinition = GetDefaultProjDefinition();
                if (string.IsNullOrWhiteSpace(projDefinition) == false)
                {
                    var label = ProjSourceType.Default.GetString();
                    items.Add(new ProjSourceItem(label, ProjSourceType.Default, null, projDefinition));
                }
            }

            //加入项目文件夹下 *.proj 文件选项
            {
                var sourceType = ProjSourceType.ProjectFolder;
                var files = GetProjectProjFiles();
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        var filePath = file.Key;
                        var projDefinition = file.Value;
                        var fileName = Path.GetFileName(filePath);
                        var label = $@"{sourceType.GetString()}: {fileName}";
                        items.Add(new ProjSourceItem(label, sourceType, filePath, projDefinition));
                    }
                }
            }

            //加入最近使用的 *.prj 文件选项
            {
                var sourceType = ProjSourceType.Recently;
                var files = GetRecentlyProjFiles();
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        var filePath = file.Key;
                        var projDefinition = file.Value;
                        var label = $@"{sourceType.GetString()}: {filePath}";
                        items.Add(new ProjSourceItem(label, sourceType, filePath, projDefinition));
                    }
                }
            }

            //加入从文件中加载
            {
                var label = ProjSourceType.Browse.GetString();
                items.Add(new ProjSourceItem(label, ProjSourceType.Browse, null, null));
            }

            return items;
        }

        public string GetModelFilePath()
        {
            var filePath = _InputFilePath;
            if (string.IsNullOrWhiteSpace(filePath) || 
                File.Exists(filePath) == false)
            {
                return null;
            }
            return filePath;
        }

        public string GetDefaultProjFilePath()
        {
            var modelFilePath = GetModelFilePath();
            if (modelFilePath == null) return null;

            return Path.ChangeExtension(modelFilePath, @".prj");
        }

        public string GetDefaultOffsetFilePath()
        {
            var modelFilePath = GetModelFilePath();
            if (modelFilePath == null) return null;

            return Path.ChangeExtension(modelFilePath, @".prjoffset");
        }

        public bool SaveProjFile(string filePath, string projDefinition)
        {
            if (CheckProjDefinition(projDefinition, out var projWkt))
            {
                projWkt.ToUnixFormat().SaveToTextFile(filePath);
                return true;
            }

            return false;
        }

        public bool SaveOffsetFile(string filePath, double[] offsets)
        {
            var json = new JObject();
            json[@"Offset"] = new JArray(offsets);

            json
                .ToString(Formatting.Indented)
                .ToUnixFormat()
                .SaveToTextFile(filePath);
            return true;
        }

        public OriginType[] GetSupportOriginTypes()
        {
            return IsInternalOnly()
                ? new [] {OriginType.Internal}
                : new [] {OriginType.Internal, OriginType.Project, OriginType.Shared, OriginType.Survey};
        }

        public bool IsTrueNorth(OriginType originType)
        {
            return true;
        }

        public GeoreferencedSetting CreateSuitedSetting(GeoreferencedSetting setting)
        {
            var s = setting?.Clone() ?? CreateDefaultSetting();

            if (IsInternalOnly())
            {
                if (s.Auto != null) s.Auto.Origin = OriginType.Internal;
                if (s.Enu != null) s.Enu.Origin = OriginType.Internal;
                if (s.Local != null) s.Local.Origin = OriginType.Internal;
                if (s.Proj != null) s.Proj.Origin = OriginType.Internal;
            }

            var siteInfo = GetModelSiteInfo();
            if (s.Enu != null && s.Enu.UseProjectLocation)
            {
                s.Enu.Latitude = siteInfo.Latitude;
                s.Enu.Longitude = siteInfo.Longitude;
                s.Enu.Height = siteInfo.Height;
                s.Enu.Rotation = IsTrueNorth(s.Enu.Origin) ? 0.0 : siteInfo.Rotation;
            }
            if (s.Local!= null && s.Local.UseProjectLocation)
            {
                s.Local.Latitude = siteInfo.Latitude;
                s.Local.Longitude = siteInfo.Longitude;
                s.Local.Height = siteInfo.Height;
                s.Local.Rotation = IsTrueNorth(s.Local.Origin) ? 0.0 : siteInfo.Rotation;
            }

            if (s.Proj != null)
            {
                var p = s.Proj;
                switch (p.DefinitionSource)
                {
                    case ProjSourceType.Custom: //人工指定
                        break;
                    case ProjSourceType.Embed:  //项目内置
                    {
                        var projDefinition = GetEmbedProjDefinition();
                        if (string.IsNullOrWhiteSpace(projDefinition))
                        {
                            //如果没有项目内置投影信息，则变更为人工指定
                            p.DefinitionSource = 0;
                            p.DefinitionFileName = null;
                        }
                        else
                        {
                            p.Definition = projDefinition;
                        }
                        break;
                    }
                    case ProjSourceType.Default: //项目默认
                    {
                        var projDefinition = GetDefaultProjDefinition();
                        if (string.IsNullOrWhiteSpace(projDefinition))
                        {
                            //如果没有项目内置投影信息，则变更为人工指定
                            p.DefinitionSource = 0;
                            p.DefinitionFileName = null;
                        }
                        else
                        {
                            p.Definition = projDefinition;

                            var projOffsets = GetDefaultOffset();
                            if (projOffsets != null)
                            {
                                p.Offset = projOffsets;
                            }
                        }
                        break;
                    }
                }
            }

            return s;
        }

        public GeoreferencedSetting CreateDefaultSetting()
        {
            var isInternalOnly = IsInternalOnly();
            var site = GetModelSiteInfo();

            var setting = new GeoreferencedSetting();
            setting.Mode = GeoreferencedMode.Auto;
            setting.Auto = new ParameterAuto
            {
                Origin = isInternalOnly ? OriginType.Internal : OriginType.Auto
            };
            setting.Enu = new ParameterEnu
            {
                Origin = isInternalOnly ? OriginType.Internal : OriginType.Project,
                AlignOriginToSitePlaneCenter = false,
                Latitude = site.Latitude,
                Longitude = site.Longitude,
                Height = site.Height,
                Rotation = site.Rotation,
                UseProjectLocation =  !isInternalOnly
            };
            setting.Local = new ParameterLocal
            {
                Origin = isInternalOnly ? OriginType.Internal : OriginType.Project,
                AlignOriginToSitePlaneCenter = false,
                Latitude = site.Latitude,
                Longitude = site.Longitude,
                Height = site.Height,
                Rotation = site.Rotation,
                UseProjectLocation = !isInternalOnly
            };
            setting.Proj = CreateParameterProj(isInternalOnly);

            return setting;
        }

        public GeoreferencedSetting CreateTargetSetting(GeoreferencedSetting setting)
        {
            var s = setting?.Clone() ?? CreateDefaultSetting();
            if (s.Mode == GeoreferencedMode.Auto)
            {
                //获取目标原点类型
                var targetOriginType = GetSupportOriginTypes().Contains(s.Auto.Origin)
                    ? s.Auto.Origin
                    : OriginType.Auto;

                //自动模式下获取默认设置
                s = CreateDefaultSetting();

                //如果存在有效的内置或默认 proj 定义, 则按照 proj 模式返回
                if (s.Proj != null)
                {
                    if (s.Proj.DefinitionSource == ProjSourceType.Embed ||
                        s.Proj.DefinitionSource == ProjSourceType.Default)
                    {
                        var result = new GeoreferencedSetting();
                        result.Mode = GeoreferencedMode.Proj;
                        result.Proj = s.Proj.Clone();

                        if (targetOriginType != OriginType.Auto)
                        {
                            result.Proj.Origin = s.Auto.Origin;
                        }

                        return CreateSuitedSetting(result);
                    }
                }

                //按照 Local 模式返回
                {
                    var result = new GeoreferencedSetting();
                    result.Mode = GeoreferencedMode.Local;
                    result.Local = s.Local.Clone();

                    if (targetOriginType != OriginType.Auto)
                    {
                        result.Local.Origin = s.Auto.Origin;
                    }

                    return CreateSuitedSetting(result);
                }
            }

            return CreateSuitedSetting(s);
        }

        public bool CheckInProjFile(string filePath)
        {
            if (_LocalData == null) return false;
            if (string.IsNullOrWhiteSpace(filePath) || File.Exists(filePath) == false) return false;

            var projDefinition = GetProjDefinition(filePath);
            if (string.IsNullOrWhiteSpace(projDefinition)) return false;

            if (_LocalData.RecentlyProjFiles == null)
            {
                _LocalData.RecentlyProjFiles = new List<string>();
            }

            var i = _LocalData.RecentlyProjFiles.IndexOf(filePath);
            if (i >= 0)
            {
                _LocalData.RecentlyProjFiles.RemoveAt(i);
            }

            _LocalData.RecentlyProjFiles.Insert(0, filePath);
            return true;
        }

        public SiteInfo GetModelSiteInfo()
        {
            return GetDefaultSiteInfo();
        }

        #endregion

        private bool IsInternalOnly()
        {
            return true;
        }

        /// <summary>
        /// 获得默认场地信息
        /// </summary>
        /// <returns></returns>
        private SiteInfo GetDefaultSiteInfo()
        {
            return SiteInfo.CreateDefault();    //默认返回北京坐标
        }

        /// <summary>
        /// 获取内嵌的投影坐标定义
        /// </summary>
        /// <returns>投影坐标系定义, 若无效则返回 null</returns>
        private string GetEmbedProjDefinition()
        {
            var gcs = DgnGCS.FromModel(Session.Instance.GetActiveDgnModel(), true);
            if (gcs == null) return null;

            var ret2 = gcs.GetWellKnownText(out var ogc, BaseGCS.WellKnownTextFlavor.wktFlavorOGC);
            if (ret2 == 0) return ogc;

            var ret1 = gcs.GetWellKnownText(out var epsg, BaseGCS.WellKnownTextFlavor.wktFlavorEPSG);
            if (ret1 == 0) return epsg;

            return null;
        }

        /// <summary>
        /// 获取默认的投影坐标定义
        /// </summary>
        /// <returns></returns>
        private string GetDefaultProjDefinition()
        {
            var filePath = GetDefaultProjFilePath();
            return GetProjDefinition(filePath);
        }

        /// <summary>
        /// 获取默认的投影坐标偏移
        /// </summary>
        /// <returns></returns>
        private double[] GetDefaultOffset()
        {
            var filePath = GetDefaultOffsetFilePath();
            return GetOffset(filePath);
        }

        /// <summary>
        /// 获取最近使用的地理参考定义文件路径
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetRecentlyProjFiles()
        {
            var results = new Dictionary<string, string>();

            if (_LocalData?.RecentlyProjFiles != null)
            {
                var count = 0;
                foreach (var projFilePath in _LocalData.RecentlyProjFiles)
                {
                    if (results.ContainsKey(projFilePath)) continue;

                    var projDefinition = GetProjDefinition(projFilePath);
                    if (string.IsNullOrWhiteSpace(projDefinition) == false)
                    {
                        results.Add(projFilePath, projDefinition);

                        if (++count > 10) break;    //最多获取最近 10 个
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 获取项目文件夹内的地理参考定义文件路径
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetProjectProjFiles()
        {
            var results = new Dictionary<string, string>();

            var projectFilePath = GetModelFilePath();
            if (projectFilePath != null)
            {
                var projectFolderPath = Path.GetDirectoryName(projectFilePath);
                if (string.IsNullOrWhiteSpace(projectFolderPath) == false)
                {
                    var filePaths = Directory.GetFiles(projectFolderPath, @"*.prj", SearchOption.TopDirectoryOnly);
                    foreach (var filePath in filePaths)
                    {
                        var ext = Path.GetExtension(filePath).ToLower();
                        if (ext != @".prj") continue;

                        var fileContent = File.ReadAllText(filePath);
                        if (CheckProjDefinition(fileContent, out _))
                        {
                            var fileName = Path.GetFileName(filePath);
                            results.Add(fileName, fileContent);
                        }
                    }
                }
            }

            return results;
        }

        private double[] GetOffset(string projOffsetFilePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(projOffsetFilePath)) return null;
                if (File.Exists(projOffsetFilePath) == false) return null;

                var content = File.ReadAllText(projOffsetFilePath, Encoding.UTF8);
                var json = JObject.Parse(content);
                var offsetValues = json.Value<JArray>(@"Offset")?.Values<double>()?.ToList();

                var resultValues = new [] {0.0, 0.0, 0.0};
                if (offsetValues != null && offsetValues.Count > 0)
                {
                    for (var i = 0; i < 3; i++)
                    {
                        resultValues[i] = offsetValues[i];
                    }
                }
                return resultValues;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return null;
        }

        private ParameterProj CreateParameterProj(bool isFamily)
        {
            var proj = new ParameterProj
            {
                Origin = isFamily ? OriginType.Internal : OriginType.Shared,
                DefinitionSource = ProjSourceType.Custom,
                DefinitionFileName = null,
                Definition = null,
                Offset = GetDefaultOffset() ?? new[] { 0.0, 0.0, 0.0 } //默认从投影偏移参数文件中加载数据
            };

            #region 先尝试加载默认投影定义
            {
                var projDefinition = GetDefaultProjDefinition();
                if (projDefinition != null)
                {
                    proj.DefinitionSource = ProjSourceType.Default;
                    proj.DefinitionFileName = null;
                    proj.Definition = projDefinition;
                    return proj;
                }
            }
            #endregion

            #region 再尝试加载项目内置的 proj 定义
            {
                var projDefinition = GetEmbedProjDefinition();
                if (projDefinition != null)
                {
                    proj.DefinitionSource = ProjSourceType.Embed;
                    proj.DefinitionFileName = null;
                    proj.Definition = projDefinition;
                    return proj;
                }
            }
            #endregion

            #region 最后尝试加载项目文件夹内的唯一 *.prj 文件
            {
                var projDefinitions = GetProjectProjFiles();
                if (projDefinitions != null && projDefinitions.Count == 1)
                {
                    proj.DefinitionSource = ProjSourceType.ProjectFolder;
                    proj.DefinitionFileName = projDefinitions.First().Key;
                    proj.Definition = projDefinitions.First().Value;
                }
            }
            #endregion

            return proj;
        }
    }
}
