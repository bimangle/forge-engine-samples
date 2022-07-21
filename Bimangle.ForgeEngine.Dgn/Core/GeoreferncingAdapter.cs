using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Bentley.GeoCoordinatesNET;
using Bentley.MstnPlatformNET;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Dgn.Config;

namespace Bimangle.ForgeEngine.Dgn.Core
{
    class GeoreferncingAdapter : Bimangle.ForgeEngine.Georeferncing.Interface.Adapter
    {
        private readonly AppConfigCesium3DTiles _LocalData;

        public GeoreferncingAdapter(AppConfigCesium3DTiles localData)
            : base(Session.Instance.GetActiveFileName())
        {
            _LocalData = localData;
        }

        #region Overrides of Adapter

        public override IProj CreateProj(string homeFolder)
        {
            return ProjValidator.Create(homeFolder);
        }

        public override IDictionary<string, string> GetRecentlyProjFiles()
        {
            var results = new Dictionary<string, string>();

            if (_LocalData?.RecentlyProjFiles != null)
            {
                var count = 0;
                foreach (var projFilePath in _LocalData.RecentlyProjFiles)
                {
                    if (results.ContainsKey(projFilePath)) continue;

                    var projDefinition = Host.GetProjDefinition(projFilePath);
                    if (string.IsNullOrWhiteSpace(projDefinition) == false)
                    {
                        results.Add(projFilePath, projDefinition);

                        if (++count > 10) break;    //最多获取最近 10 个
                    }
                }
            }

            return results;
        }

        public override bool CheckInProjFile(string filePath)
        {
            if (_LocalData == null) return false;
            if (string.IsNullOrWhiteSpace(filePath) || File.Exists(filePath) == false) return false;

            var projDefinition = Host.GetProjDefinition(filePath);
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

        public override bool IsLocal()
        {
            return true;
        }

        public override void SetDirectionLetters(Label lblLocalX, Label lblLocalY)
        {
            //do nothing
        }

        public override bool IsTrueNorth(OriginType originType)
        {
            return true;
        }

        public override string GetLocalString(OriginType originType)
        {
            if (originType == OriginType.Internal)
            {
                return Strings.OriginTypeInternal;
            }
            return base.GetLocalString(originType);
        }

        public override OriginType[] GetSupportOriginTypes()
        {
            return new[] { OriginType.Internal };
        }

        public override string GetEmbedProjDefinition()
        {
            var gcs = DgnGCS.FromModel(Session.Instance.GetActiveDgnModel(), true);
            if (gcs == null) return null;

            var ret0 = GetEPSG(gcs, out var epsgCode);
            if (ret0) return epsgCode;

            //var ret2 = gcs.GetWellKnownText(out var ogc, BaseGCS.WellKnownTextFlavor.wktFlavorOGC);
            //if (ret2 == 0) return ogc;

            //var ret1 = gcs.GetWellKnownText(out var epsg, BaseGCS.WellKnownTextFlavor.wktFlavorEPSG);
            //if (ret1 == 0) return epsg;

            if (TryGetWellKnownText(gcs, out var ogc, BaseGCS.WellKnownTextFlavor.wktFlavorOGC))
            {
                return ogc;
            }

            if (TryGetWellKnownText(gcs, out var epsg, BaseGCS.WellKnownTextFlavor.wktFlavorEPSG))
            {
                return epsg;
            }

            return null;
        }

        /// <summary>
        /// 尽量返回 EPSG:xxxx 形式的投影定义
        /// </summary>
        /// <param name="gcs"></param>
        /// <param name="epsgCode"></param>
        /// <returns></returns>
        private bool GetEPSG(DgnGCS gcs, out string epsgCode)
        {
            try
            {
                var code = gcs.EPSGCode;
                if (code > 0)
                {
                    epsgCode = $@"EPSG:{code}";
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            try
            {
                //疑似 Update14 新增加的方法
                var code = gcs.GetEPSGCode(false);
                if (code > 0)
                {
                    epsgCode = $@"EPSG:{code}";
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            epsgCode = null;
            return false;
        }

        /// <summary>
        /// 为了兼容 Update 17 的接口变更, 用反射方法来调用 GetWellKnownText
        /// </summary>
        /// <param name="gcs"></param>
        /// <param name="wkt"></param>
        /// <param name="flavor"></param>
        /// <param name="originalIfPresent"></param>
        /// <returns></returns>
        private bool TryGetWellKnownText(DgnGCS gcs, out string wkt, BaseGCS.WellKnownTextFlavor flavor, bool originalIfPresent = false)
        {
            wkt = null;

            var methodInfo = gcs.GetType().GetMethod(@"GetWellKnownText");
            if (methodInfo == null) return false;

            var parametersInfo = methodInfo.GetParameters();

            var args = parametersInfo.Length == 2
                ? new object[] { null, flavor }                         //对应 Update 16 或更低版本
                : new object[] { null, flavor, originalIfPresent };     //对应 Update 17 或更高版本
            var ret = (int)methodInfo.Invoke(gcs, args);
            if (ret != 0) return false;

            wkt = args[0] as string;
            return wkt != null;
        }

        #endregion
    }
}
