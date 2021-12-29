using System;
using System.Collections.Generic;
using System.Text;
using Bimangle.ForgeEngine.Common.Georeferenced;

namespace Bimangle.ForgeEngine.Georeferncing
{
    static class ProjBuilder
    {
        private static readonly IDictionary<GeoGCS, string> GeoGCSWKTs = new Dictionary<GeoGCS, string>
        {
            {
                GeoGCS.GCS_China_Geodetic_Coordinate_System_2000,
                @"GEOGCS[""China Geodetic Coordinate System 2000"",DATUM[""D_China_2000"",SPHEROID[""CGCS2000"",6378137,298.257222101]],PRIMEM[""Greenwich"",0],UNIT[""Degree"",0.017453292519943295]]"
            },
            {
                GeoGCS.GCS_WGS_1984,
                @"GEOGCS[""GCS_WGS_1984"",DATUM[""D_WGS_1984"",SPHEROID[""WGS_1984"",6378137,298.257223563]],PRIMEM[""Greenwich"",0],UNIT[""Degree"",0.017453292519943295]]"
            },
            {
                GeoGCS.GCS_Xian_1980,
                @"GEOGCS[""Xian 1980"",DATUM[""D_Xian_1980"",SPHEROID[""IAG_1975"",6378140,298.257]],PRIMEM[""Greenwich"",0],UNIT[""Degree"",0.017453292519943295]]"
            },
            {
                GeoGCS.GCS_New_Beijing,
                @"GEOGCS[""New Beijing"",DATUM[""D_New_Beijing"",SPHEROID[""Krasovsky_1940"",6378245,298.3]],PRIMEM[""Greenwich"",0],UNIT[""Degree"",0.017453292519943295]]"
            },
            {
                GeoGCS.GCS_Beijing_1954,
                @"GEOGCS[""Beijing 1954"",DATUM[""D_Beijing_1954"",SPHEROID[""Krasovsky_1940"",6378245,298.3]],PRIMEM[""Greenwich"",0],UNIT[""Degree"",0.017453292519943295]]"
            },
        };

        public static ProjDefinition CreateProj(this IProj validator, GeoGCS gcs, double e, double n, double lon, double lat)
        {
            //先创建一个默认的投影定义
            var proj = new ProjDefinition();
            proj.GeoGCS = gcs;
            proj.CentralMeridian = GetCentralMeridian(lon); //按3度带自动确定中央经线
            proj.FalseEasting = 500000.0;
            proj.FalseNorthing = 0.0;

            //算出经纬度坐标对应于投影坐标系的坐标值
            var r = validator.Calculate(proj.ToWKT(), lon, lat);
            if (r == null) return null;

            //根据计算出的坐标值修正伪东和伪北
            proj.FalseEasting -= r[0] - e;
            proj.FalseNorthing -= r[1] - n;

            return proj;
        }


        public static string ToWKT(this GeoGCS gcs)
        {
            if (GeoGCSWKTs.TryGetValue(gcs, out var wkt) == false)
            {
                throw new NotSupportedException(gcs.ToString());
            }
            return wkt;
        }

        /// <summary>
        /// 按照 3 度带划分标准获取对应的中央经线
        /// </summary>
        /// <param name="lon"></param>
        /// <returns></returns>
        public static double GetCentralMeridian(double lon)
        {
            var z = (int)(lon / 3);
            var n = lon % 3;
            return n <= 1.5 ? z * 3.0 : (z + 1) * 3.0;
        }


        public class ProjDefinition
        {
            public string Name { get; set; } = @"unknown";
            public GeoGCS GeoGCS { get; set; } = GeoGCS.GCS_China_Geodetic_Coordinate_System_2000;
            public Projection Projection { get; set; } = Projection.Transverse_Mercator;
            public double LatitudeOfOrigin { get; set; } = 0.0;

            /// <summary>
            /// 中央经线
            /// </summary>
            public double CentralMeridian { get; set; } = 0.0;

            public double ScaleFactor { get; set; } = 1.0;

            public double FalseEasting { get; set; } = 500000.0;
            public double FalseNorthing { get; set; } = 0.0;

            public string ToWKT()
            {
                var sb = new StringBuilder(16 * 1024);

                //头部
                sb.AppendLine($@"PROJCS[""{Name}"",");

                //地理参考系
                sb.AppendLine(GeoGCS.ToWKT() + @",");

                //投影方法
                sb.AppendLine($@"PROJECTION[""{Projection.ToString()}""],");

                //其它参数
                sb.AppendLine($@"PARAMETER[""latitude_of_origin"", {LatitudeOfOrigin}],");
                sb.AppendLine($@"PARAMETER[""central_meridian"", {CentralMeridian}],");
                sb.AppendLine($@"PARAMETER[""scale_factor"", {ScaleFactor}],");
                sb.AppendLine($@"PARAMETER[""false_easting"", {FalseEasting}],");
                sb.AppendLine($@"PARAMETER[""false_northing"", {FalseNorthing}],");

                //单位强制为米
                sb.AppendLine(@"UNIT[""Meter"",1]");

                //尾部
                sb.AppendLine(@"]");

                return sb.ToString();
            }
        }

        public enum GeoGCS
        {
            GCS_China_Geodetic_Coordinate_System_2000 = 4490,
            GCS_WGS_1984 = 4326,
            GCS_Xian_1980 = 4610,
            GCS_New_Beijing = 4555,
            GCS_Beijing_1954 = 4214,
        }

        public enum Projection
        {
            Transverse_Mercator
        }
    }
}
