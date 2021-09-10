using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Utils;

namespace Bimangle.ForgeEngine.Navisworks.Georeferncing
{
    static class GeoreferncingHelper
    {
        public static string GetString(this OriginType originType)
        {
            switch (originType)
            {
                case OriginType.Auto:
                    return GeoStrings.OriginTypeAuto;
                case OriginType.Internal:
                    return GeoStrings.OriginTypeInternal;
                case OriginType.Project:
                    return GeoStrings.OriginTypeProject;
                case OriginType.Shared:
                    return GeoStrings.OriginTypeShared;
                case OriginType.Survey:
                    return GeoStrings.OriginTypeSurvey;
                default:
                    throw new ArgumentOutOfRangeException(nameof(originType), originType, null);
            }
        }

        public static string GetString(this GeoreferencedMode mode)
        {
            switch (mode)
            {
                case GeoreferencedMode.Local:
                    return GeoStrings.GeoreferencedModeLocal;
                case GeoreferencedMode.Enu:
                    return GeoStrings.GeoreferencedModeEnu;
                case GeoreferencedMode.Proj:
                    return GeoStrings.GeoreferencedModeProj;
                case GeoreferencedMode.Auto:
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static string GetString(this ProjSourceType t)
        {
            switch (t)
            {
                case ProjSourceType.Create:
                    return GeoStrings.DefinitionSourceCreate;
                case ProjSourceType.Browse:
                    return GeoStrings.DefinitionSourceBrowse;
                case ProjSourceType.Custom:
                    return GeoStrings.DefinitionSourceCustom;
                case ProjSourceType.Embed:
                    return GeoStrings.DefinitionSourceEmbed;
                case ProjSourceType.Default:
                    return GeoStrings.DefinitionSourceProjectDefault;
                case ProjSourceType.ProjectFolder:
                    return GeoStrings.DefinitionSourceProjectFolder;
                case ProjSourceType.Recently:
                    return GeoStrings.DefinitionSourceRecently;
                default:
                    throw new ArgumentOutOfRangeException(nameof(t), t, null);
            }
        }

        public static string GetDetails(this GeoreferencedSetting setting, bool sourceIsAutoMode)
        {
            var sb = new StringBuilder();

            if (sourceIsAutoMode)
            {
                sb.AppendLine($@"{GeoStrings.AutoMode} =>");
            }

            sb.AppendLine($@"{GeoStrings.GeoreferencedMode}: {setting.Mode.GetString()}");
            switch (setting.Mode)
            {
                case GeoreferencedMode.Local:
                {
                    var p = setting.Local;
                    sb.AppendLine($@"{GeoStrings.OriginLocation}: {p.Origin.GetString()}");
                    sb.AppendLine($@"{GeoStrings.AlignOriginToSitePlaneCenter}: {p.AlignOriginToSitePlaneCenter}");
                    sb.AppendLine($@"{GeoStrings.OriginDefaultCoordinate}: {GetDoubleString(p.Rotation, 10)}@{GetDoubleString(p.Latitude, 10)},{GetDoubleString(p.Longitude, 10)},{GetDoubleString(p.Height, 6)}z");
                    break;
                }
                case GeoreferencedMode.Enu:
                {
                    var p = setting.Enu;
                    sb.AppendLine($@"{GeoStrings.OriginLocation}: {p.Origin.GetString()}");
                    sb.AppendLine($@"{GeoStrings.AlignOriginToSitePlaneCenter}: {p.AlignOriginToSitePlaneCenter}");
                    sb.AppendLine($@"{GeoStrings.OriginCoordinate}: {GetDoubleString(p.Rotation, 10)}@{GetDoubleString(p.Latitude, 10)},{GetDoubleString(p.Longitude, 10)},{GetDoubleString(p.Height, 6)}z");
                    break;
                }
                case GeoreferencedMode.Proj:
                {
                    var p = setting.Proj;
                    sb.AppendLine($@"{GeoStrings.OriginLocation}: {p.Origin.GetString()}");
                    sb.AppendLine($@"{GeoStrings.CoordinateOffset}: {GetOffsetsString(p.Offset)}");
                    sb.AppendLine($@"{GeoStrings.DefinitionSource}:");
                    sb.AppendLine($@"  {p.DefinitionSource.GetString()}");

                    if (string.IsNullOrWhiteSpace(p.DefinitionFileName) == false)
                    {
                        sb.AppendLine($@"  {p.DefinitionFileName}");
                    }

                    sb.AppendLine($@"{GeoStrings.ProjectedDefinition}:");
                    sb.AppendLine($@"  {p.Definition.ToWindowsFormat()}");
                    break;
                }
                case GeoreferencedMode.Auto:
                default:
                    throw new NotSupportedException($@"Mode: {setting.Mode}");
            }

            return sb.ToString();

        }

        private static string GetDoubleString(double n, int digits)
        {
            return Math.Round(n, digits).ToString(CultureInfo.InvariantCulture);
        }

        private static string GetOffsetsString(double[] offsets)
        {
            if (offsets == null) return @"0,0,0";
            return string.Join(@",", offsets.Select(x => GetDoubleString(x, 6)));
        }
    }
}

