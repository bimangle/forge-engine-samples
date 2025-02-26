using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Bimangle.ForgeEngine.Common.Georeferenced;

namespace Bimangle.ForgeEngine.Georeferncing.Utility
{
    static class TextHelper
    {
        public static bool TryParseLatitude(this TextBox text, ErrorProvider errorProvider, out double n)
        {
            return text.TryParseLatLon(errorProvider, -90.0, 90.0, GeoStrings.ErrorMessageLatitude, out n);
        }

        public static bool TryParseLongitude(this TextBox text, ErrorProvider errorProvider, out double n)
        {
            return text.TryParseLatLon(errorProvider, -180.0, 180.0, GeoStrings.ErrorMessageLongitude, out n);
        }

        public static bool TryParseHeight(this TextBox text, ErrorProvider errorProvider, out double n)
        {
            return text.TryParse(errorProvider, double.MinValue, double.MaxValue, GeoStrings.ErrorMessageHeight, out n);
        }

        public static bool TryParseRotation(this TextBox text, ErrorProvider errorProvider, out double n)
        {
            return text.TryParse(errorProvider, -360.0, 360.0, GeoStrings.ErrorMessageRotation, out n);
        }

        public static bool TryParseNumber(this TextBox text, ErrorProvider errorProvider, out double n)
        {
            return text.TryParse(errorProvider, double.MinValue, double.MaxValue, GeoStrings.ErrorMessageNumber, out n);
        }

        public static string GetOffsetString(this ParameterProj p)
        {
            p.CheckProjOffset();

            if (p.OffsetType == ProjOffsetType.None || 
                p.Offset == null)
            {
                return ProjOffsetType.None.GetString();
            }

            var offset = p.Offset;
            return p.OffsetType.GetOffsetsString(
                offset[0], offset[1], offset[2],
                offset[3], offset[4], offset[5],
                offset[6]);
        }

        private static string GetOffsetsString(this ProjOffsetType offsetType, double dx, double dy, double dz, double rxRad, double ryRad, double rzRad, double m)
        {
            var rx = rxRad * 648000.0 / Math.PI;
            var ry = ryRad * 648000.0 / Math.PI;
            var rz = rzRad * 648000.0 / Math.PI;
            var k = m * 1e6;

            var sdx = dx.ToMetreString();
            var sdy = dy.ToMetreString();
            var sdz = dz.ToMetreString();

            //6位小数已经可以确保最大误差不超过 0.3mm
            var srx = rx.GetDoubleString(6);
            var sry = ry.GetDoubleString(6);
            var srz = rz.GetDoubleString(6);

            //6位小数已经可以确保最大误差不超过 0.2mm
            var sk = k.GetDoubleString(6);

            var prefix = offsetType.GetString();
            switch (offsetType)
            {
                case ProjOffsetType._2D_Params3:
                    return $"{prefix}: {sdx}m,{sdy}m,{srz}\"";
                case ProjOffsetType._2D_Params4:
                    return $"{prefix}: {sdx}m,{sdy}m,{srz}\",{sk}ppm";
                case ProjOffsetType._3D_Params3:
                    return $"{prefix}: {sdx}m,{sdy}m,{sdz}m";
                case ProjOffsetType._3D_Params4:
                    return $"{prefix}: {sdx}m,{sdy}m,{sdz}m,{srz}\"";
                case ProjOffsetType._3D_Params7:
                    return $"{prefix}: {sdx}m,{sdy}m,{sdz}m,{srx}\",{sry}\",{srz}\",{sk}ppm";
                case ProjOffsetType.Auto:
                case ProjOffsetType.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(offsetType), offsetType, null);
            }
        }

        /// <summary>
        /// 转换为经纬度字符串（保留 8 位小数）
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string ToLatLonString(this double n)
        {
            return n.GetDoubleString(8);
        }

        /// <summary>
        /// 转换为角度字符串（保留 10 位小数）
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string ToDegreeString(this double n)
        {
            return n.GetDoubleString(10);
        }

        /// <summary>
        /// 转换为单位为米的字符串（保留 4 位小数）
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string ToMetreString(this double n)
        {
            return n.GetDoubleString(4);
        }

        public static string GetDoubleString(this double n, int digits)
        {
            //小数位数取值规则:
            // 1. 高程保留 4 位小数（精度 0.1mm）
            // 2. 经纬度保留 8 位小数（精度 0.3mm）
            // 3. 其它值保留 10 位小数

            return Math.Round(n, digits).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 尝试解析经纬度
        /// </summary>
        /// <param name="text"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <remarks>
        /// 支持数字格式（-180.0 ~ 180.0）和度分秒格式（0°0′0″ ~ 180°0′0″）
        /// </remarks>
        public static bool TryParseLatLon(this string text, out double n)
        {
            var splitSigns = new char[]
            {
                ' ', ',', ';', ':', '/', '\\',
                '度', '分', '秒',       // 中文
                '°', '′', '″',       // 全角
                '\u00b0', '\'', '\"'    // 半角
            };

            var s = text?.Trim().TrimEnd(splitSigns) ?? string.Empty;

            double? sign = null;

            // 尝试解析后置符号
            {
                var endSigns = new Dictionary<string, double>()
                {
                    {@"N", 1.0},
                    {@"S", -1.0},
                    {@"E", 1.0},
                    {@"W", -1.0}
                };
                foreach (var p in endSigns)
                {
                    if (s.EndsWith(p.Key))
                    {
                        sign = p.Value;
                        s = s.Substring(0, s.Length - p.Key.Length)
                            .Trim()
                            .TrimEnd(splitSigns);
                        break;
                    }
                }
            }

            // 尝试解析前置符号
            if (sign == null)
            {
                var startSigns = new Dictionary<string, double>()
                {
                    {@"北纬", 1.0},
                    {@"南纬", -1.0},
                    {@"东经", 1.0},
                    {@"西经", -1.0}
                };
                foreach (var p in startSigns)
                {
                    if (s.StartsWith(p.Key))
                    {
                        sign = p.Value;
                        s = s.Substring(p.Key.Length)
                            .Trim()
                            .TrimEnd(splitSigns);
                        break;
                    }
                }
            }

            var items = s.Split(splitSigns, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length >= 1 && items.Length <= 3)
            {
                var nm = 0.0;
                var ns = 0.0;

                if (double.TryParse(items[0], out n) &&
                    (items.Length < 2 || double.TryParse(items[1], out nm)) &&
                    (items.Length < 3 || double.TryParse(items[2], out ns)))
                {
                    if (nm >= 0.0 && nm <= 60.0 &&
                        ns >= 0.0 && ns <= 60.0)
                    {
                        n += nm / 60.0 + ns / 3600.0;

                        if (n >= 0.0 && sign.HasValue) n *= sign.Value;
                        return true;
                    }
                }
            }

            n = 0.0;
            return false;
        }

        /// <summary>
        /// 尝试解析经纬度
        /// </summary>
        /// <param name="text"></param>
        /// <param name="errorProvider"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="errorInfo"></param>
        /// <param name="n"></param>
        /// <remarks>
        /// 支持数字格式（-180.0 ~ 180.0）和度分秒格式（0°0′0″ ~ 180°0′0″）
        /// </remarks>
        /// <returns></returns>
        private static bool TryParseLatLon(this TextBox text, ErrorProvider errorProvider, double min, double max, string errorInfo, out double n)
        {
            errorProvider.SetError(text, null);

            var s = text.Text.Trim();
            if (string.IsNullOrWhiteSpace(s) == false && s.TryParseLatLon(out n))
            {
                if (n >= min && n <= max) return true;
            }

            errorProvider.SetError(text, errorInfo);
            n = 0.0;
            return false;
        }

        /// <summary>
        /// 尝试解析数字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="errorProvider"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="errorInfo"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static bool TryParse(this TextBox text, ErrorProvider errorProvider, double min, double max, string errorInfo, out double n)
        {
            errorProvider.SetError(text, null);
            n = 0.0;

            if (double.TryParse(text.Text, out n) &&
                n >= min && n <= max)
            {
                return true;
            }

            errorProvider.SetError(text, errorInfo);
            return false;
        }
    }
}
