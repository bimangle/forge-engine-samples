using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Autodesk.Revit.DB;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Revit.Config;

namespace Bimangle.ForgeEngine.Revit.Core
{
    class GeoreferncingAdapter : Bimangle.ForgeEngine.Georeferncing.Interface.Adapter
    {
        private readonly Document _Document;
        private readonly AppConfigCesium3DTiles _LocalData;

        public GeoreferncingAdapter(Document document, AppConfigCesium3DTiles localData) : base(document?.PathName)
        {
            _Document = document;
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

        public override SiteInfo GetSiteInfo()
        {
            return _Document != null
                ? ExporterHelper.GetSiteInfo(_Document)
                : base.GetSiteInfo();
        }

        public override string GetEmbedProjDefinition()
        {
#if !R2014 && !R2015 && !R2016 && !R2017
            //获取项目内嵌的投影坐标定义, 仅 Revit 2018 或更高版本有效
            var gcsDefinition = _Document?.SiteLocation?.GeoCoordinateSystemDefinition;
            if (string.IsNullOrWhiteSpace(gcsDefinition) == false &&
                TryParseGcsDefinition(gcsDefinition, out var epsgString))
            {
                if (Host.CheckProjDefinition(epsgString, out _))
                {
                    return epsgString;
                }
            }
#endif
            return base.GetEmbedProjDefinition();
        }

        public override bool IsLocal()
        {
            return _Document?.IsFamilyDocument ?? false;
        }

        #endregion

        /// <summary>
        /// 尝试解析 doc.SiteLocation.GeoCoordinateSystemDefinition 获得 EPSG 定义
        /// </summary>
        /// <param name="gcsDefinition"></param>
        /// <param name="epsgString"></param>
        /// <returns></returns>
        private bool TryParseGcsDefinition(string gcsDefinition, out string epsgString)
        {
            var xml = new XmlDocument();
            xml.LoadXml(gcsDefinition);

            var xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace(@"i", @"http://www.osgeo.org/mapguide/coordinatesystem");

            var xpath = @"/i:Dictionary/i:Alias[@type='CoordinateSystem'][i:Namespace='EPSG Code']/@id";
            var node = xml.SelectSingleNode(xpath, xmlns);

            var code = node?.Value;
            if (string.IsNullOrWhiteSpace(code) == false)
            {
                //Debug.WriteLine($@"EPSG:{code}");
                epsgString = $@"EPSG:{code}";
                return true;
            }

            epsgString = null;
            return false;
        }
    }
}
