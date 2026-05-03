using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Common.Utils;

namespace Bimangle.ForgeEngine.Georeferncing.Utility
{
    class MetadataXml
    {
        public const string FILE_NAME = @"metadata.xml";

        private const string PREFIX_ENU = @"ENU:";
        private const string PREFIX_EPSG = @"EPSG:";

        public static bool TryParse(string filePath, out MetadataXml meta)
        {
            meta = null;

            if (string.IsNullOrWhiteSpace(filePath)) return false;
            if (File.Exists(filePath) == false) return false;

            try
            {
                var doc = new XmlDocument();
                doc.Load(filePath);

                //解析空间参考系统定义
                var srsXml = doc.SelectSingleNode(@"/ModelMetadata/SRS");
                var srs = srsXml?.InnerText?.Trim().ToWindowsFormat();
                if (srs == null || IsValidSrs(srs) == false) return false;

                //解析站心模型原点坐标
                var srsOriginXml = doc.SelectSingleNode(@"/ModelMetadata/SRSOrigin") ??
                                   doc.SelectSingleNode(@"/ModelMetadata/SRsorigin");
                var srsOriginString = srsOriginXml?.InnerText?.Trim();
                if (TryParseSrsOrigin(srsOriginString, out var srsOrigin) == false)
                {
                    return false;
                }

                MetadataXmlEnu enu;
                MetadataXmlProj proj;

                if (srs.StartsWith(PREFIX_ENU, StringComparison.OrdinalIgnoreCase))
                {
                    if (TryParseEnuOrigin(srs, out var enuOrigin) == false)
                    {
                        //如果解析 ENU 站心地理坐标失败
                        return false;
                    }

                    enu = new MetadataXmlEnu(enuOrigin, srsOrigin);
                    proj = null;
                }
                else
                {
                    enu = null;
                    proj = new MetadataXmlProj(srs, srsOrigin);
                }

                meta = new MetadataXml(filePath, enu, proj);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        private static bool IsValidSrs(string srs)
        {
            if (string.IsNullOrWhiteSpace(srs)) return false;

            var s = srs.Trim();
            if (s.StartsWith(PREFIX_ENU, StringComparison.OrdinalIgnoreCase) ||
                s.StartsWith(PREFIX_EPSG, StringComparison.OrdinalIgnoreCase) ||
                ProjFile.IsValidOgcWkt(s))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 解析 ENU 坐标
        /// </summary>
        /// <param name="srs"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool TryParseEnuOrigin(string srs, out double[] result)
        {
            result = null;

            var s = srs.Substring(PREFIX_ENU.Length);
            var items = s.Split(new[] { ',' }, StringSplitOptions.None);
            if (items.Length < 2) return false;

            var values = new[] { 0.0, 0.0, 0.0 };
            for (var i = 0; i < items.Length; i++)
            {
                if (double.TryParse(items[i], out var n))
                {
                    values[i] = n;
                }
                else
                {
                    return false;
                }
            }

            result = values;
            return true;
        }

        /// <summary>
        /// 解析站心模型坐标
        /// </summary>
        /// <param name="srsOrigin"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool TryParseSrsOrigin(string srsOrigin, out double[] result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(srsOrigin)) return false;

            var items = srsOrigin.Split(new[] { ',' }, StringSplitOptions.None);
            if (items.Length < 2) return false;

            var values = new[] { 0.0, 0.0, 0.0 };
            for (var i = 0; i < items.Length; i++)
            {
                if (double.TryParse(items[i], out var n))
                {
                    values[i] = n;
                }
                else
                {
                    return false;
                }
            }

            result = values;
            return true;
        }

        public string FilePath { get; }
        public MetadataXmlEnu Enu { get; }
        public MetadataXmlProj Proj { get; private set; }

        public MetadataXml(string filePath, MetadataXmlEnu enu, MetadataXmlProj proj)
        {
            FilePath = filePath;
            Enu = enu;
            Proj = proj;
        }

        public bool TryGetProj(IGeoreferncingHost host, out MetadataXmlProj proj)
        {
            if (Proj == null)
            {
                //将 ENU 转换得到 PROJCS
                var validator = host.GetProjValidator();
                var gcs = ProjBuilder.GeoGCS.GCS_China_Geodetic_Coordinate_System_2000;
                var projDefinition = validator.CreateProj(gcs, null, Enu.SrsOrigin[0], Enu.SrsOrigin[1], Enu.EnuOrigin[1], Enu.EnuOrigin[0]);
                if (projDefinition != null)
                {
                    var srs = projDefinition.ToWKT();
                    var srsOrigin = new[] { 0.0, 0.0, Enu.SrsOrigin[2] - Enu.EnuOrigin[2] };
                    Proj = new MetadataXmlProj(srs, srsOrigin);
                }
            }

            proj = Proj;
            return proj != null;
        }

        public SiteInfo GetSiteInfo()
        {
            if (Enu == null) return null;

            return new SiteInfo
            {
                Latitude = Enu.EnuOrigin[0],
                Longitude = Enu.EnuOrigin[1],
                Height = Enu.EnuOrigin[2],
                Rotation = 0.0
            };
        }
    }

    class MetadataXmlEnu
    {
        /// <summary>
        /// 站心地理坐标 (lat, long, [height])
        /// </summary>
        public double[] EnuOrigin { get; }
        
        /// <summary>
        /// 站心模型坐标（x,y,z)
        /// </summary>
        public double[] SrsOrigin { get; }

        public MetadataXmlEnu(double[] enuOrigin, double[] srsOrigin)
        {
            EnuOrigin = enuOrigin;
            SrsOrigin = srsOrigin;
        }
    }

    class MetadataXmlProj
    {
        /// <summary>
        /// 投影坐标参考系定义
        /// </summary>
        public string Srs { get; }

        /// <summary>
        /// 站心模型坐标（x,y,z)
        /// </summary>
        public double[] SrsOrigin { get; }

        public MetadataXmlProj(string srs, double[] srsOrigin)
        {
            Srs = srs;
            SrsOrigin = srsOrigin;
        }
    }
}
