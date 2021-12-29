using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Georeferncing.Interface;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Bimangle.ForgeEngine.Georeferncing
{
    public class GeoreferncingHost : IGeoreferncingHost, IAdapterHost, IDisposable
    {
        private readonly string _HomeFolder;
        private Adapter _Adapter;
        private IProj _ProjValidator;

        public static GeoreferncingHost Create(Adapter adapter, string homeFolder)
        {
            IProj projValidator = null;
            try
            {
                projValidator = adapter.CreateProj(homeFolder);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return new GeoreferncingHost(homeFolder, adapter, projValidator);
        }

        private GeoreferncingHost(string homeFolder, Adapter adapter, IProj projValidator)
        {
            _HomeFolder = homeFolder;
            _Adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
            _ProjValidator = projValidator;

            _Adapter.Init(this);
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

            _Adapter = null;
        }

        #endregion

        #region Implementation of IGeoreferncingHost

        IProj IGeoreferncingHost.GetProjValidator()
        {
            return _ProjValidator;
        }

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
                var projDefinition = _Adapter.GetEmbedProjDefinition();
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
                var files = _Adapter.GetRecentlyProjFiles();
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

            //加入创建投影定义
            {
                var label = ProjSourceType.Create.GetString();
                items.Add(new ProjSourceItem(label, ProjSourceType.Create, null, null));
            }

            return items;
        }

        public string GetModelFilePath()
        {
            var filePath = _Adapter.GetFilePath();
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
            var originTypes = _Adapter.GetSupportOriginTypes();
            if (originTypes == null || originTypes.Length == 0)
            {
                originTypes = new[] { OriginType.Internal };
            }
            return originTypes;
        }

        public bool IsTrueNorth(OriginType originType)
        {
            return _Adapter.IsTrueNorth(originType);
        }

        public GeoreferencedSetting CreateSuitedSetting(GeoreferencedSetting setting)
        {
            var s = setting?.Clone() ?? CreateDefaultSetting();

            if (_Adapter.IsLocal())
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
                        var projDefinition = _Adapter.GetEmbedProjDefinition();
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
            var internalOnly = _Adapter.IsLocal();
            var site = GetModelSiteInfo();

            var setting = new GeoreferencedSetting();
            setting.Mode = GeoreferencedMode.Auto;
            setting.Auto = new ParameterAuto
            {
                Origin = internalOnly ? OriginType.Internal : OriginType.Auto
            };
            setting.Enu = new ParameterEnu
            {
                Origin = internalOnly ? OriginType.Internal : OriginType.Project,
                AlignOriginToSitePlaneCenter = false,
                Latitude = site.Latitude,
                Longitude = site.Longitude,
                Height = site.Height,
                Rotation = site.Rotation,
                UseProjectLocation =  !internalOnly
            };
            setting.Local = new ParameterLocal
            {
                Origin = internalOnly ? OriginType.Internal : OriginType.Project,
                AlignOriginToSitePlaneCenter = false,
                Latitude = site.Latitude,
                Longitude = site.Longitude,
                Height = site.Height,
                Rotation = site.Rotation,
                UseProjectLocation = !internalOnly
            };
            setting.Proj = CreateParameterProj(internalOnly);

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
                            result.Proj.Origin = targetOriginType;
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
                        result.Local.Origin = targetOriginType;
                    }

                    return CreateSuitedSetting(result);
                }
            }

            return CreateSuitedSetting(s);
        }

        public bool CheckInProjFile(string filePath)
        {
            return _Adapter.CheckInProjFile(filePath);
        }

        public SiteInfo GetModelSiteInfo()
        {
            return _Adapter.GetSiteInfo() ?? GetDefaultSiteInfo();
        }

        public bool ShowPickPositionDialog()
        {
            var previewAppPath = Path.Combine(
                _HomeFolder,
                @"Tools",
                @"Browser",
                @"Bimangle.ForgeBrowser.exe"
            );
            if (File.Exists(previewAppPath) == false) return false;

            try
            {

                Process.Start(previewAppPath, @"--PickPosition");
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return false;
        }

        public Adapter Adapter => _Adapter;

        #endregion

        /// <summary>
        /// 获得默认场地信息
        /// </summary>
        /// <returns></returns>
        private SiteInfo GetDefaultSiteInfo()
        {
            return SiteInfo.CreateDefault();    //默认返回北京坐标
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
                var projDefinition = _Adapter.GetEmbedProjDefinition();
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
