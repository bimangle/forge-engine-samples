using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Georeferncing.Interface;
using Newtonsoft.Json;

namespace Bimangle.ForgeEngine.Georeferncing
{
    public static class GeoreferncingHelper
    {
        public static void ShowGeoreferncingUI(IWin32Window owner, GeoreferncingHost host, GeoreferencedSetting input, Action<GeoreferencedSetting> callback)
        {
            var dialog = new FormGeoreferncing(host, input);
            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                callback?.Invoke(dialog.Setting);
            }
        }

        public static void ShowProjCreateUI(IWin32Window owner, GeoreferncingHost host)
        {
            var form = new FormProjCreate(host);
            if (form.ShowDialog(owner) == DialogResult.OK &&
                string.IsNullOrWhiteSpace(form.Definition) == false)
            {
                var dialog = new SaveFileDialog();
                dialog.OverwritePrompt = true;
                dialog.Filter = @"Projected Definition|*.prj|All Files|*.*";
                dialog.AddExtension = true;
                if (dialog.ShowDialog(owner) == DialogResult.OK)
                {
                    form.Definition.SaveToTextFile(dialog.FileName);
                }
            }
        }

        public static string GetUnitString(int unitType)
        {
            switch (unitType)
            {
                case 0:
                    return GeoStrings.UnitMetre;
                case 1:
                    return GeoStrings.UnitFeet;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitType), unitType, null);
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

        public static IList<string> GetBrief(this GeoreferencedSetting setting, Adapter adapter)
        {
            var sb = new List<string>();

            sb.Add($@"{GeoStrings.GeoreferencedMode}: {setting.Mode.GetString()}");
            switch (setting.Mode)
            {
                case GeoreferencedMode.Local:
                    {
                        var p = setting.Local;
                        sb.Add($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                        sb.Add($@"{GeoStrings.AlignOriginToSitePlaneCenter}: {p.AlignOriginToSitePlaneCenter}");
                        sb.Add($@"{GeoStrings.OriginDefaultCoordinate}: {GetDoubleString(p.Rotation, 10)}@{GetDoubleString(p.Latitude, 10)},{GetDoubleString(p.Longitude, 10)},{GetDoubleString(p.Height, 6)}z");
                        break;
                    }
                case GeoreferencedMode.Enu:
                    {
                        var p = setting.Enu;
                        sb.Add($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                        sb.Add($@"{GeoStrings.AlignOriginToSitePlaneCenter}: {p.AlignOriginToSitePlaneCenter}");
                        sb.Add($@"{GeoStrings.OriginCoordinate}: {GetDoubleString(p.Rotation, 10)}@{GetDoubleString(p.Latitude, 10)},{GetDoubleString(p.Longitude, 10)},{GetDoubleString(p.Height, 6)}z");
                        sb.Add($@"{GeoStrings.AutoAlignToGround}: {p.UseAutoAlignToGround}");
                        break;
                    }
                case GeoreferencedMode.Proj:
                    {
                        var p = setting.Proj;
                        sb.Add($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                        sb.Add($@"{GeoStrings.CoordinateOffset}: {GetOffsetsString(p.Offset)}");
                        sb.Add($@"{GeoStrings.DefinitionSource}:");
                        sb.Add($@"  {p.DefinitionSource.GetString()}");

                        if (string.IsNullOrWhiteSpace(p.DefinitionFileName) == false)
                        {
                            sb.Add($@"  {p.DefinitionFileName}");
                        }

                        sb.Add($@"{GeoStrings.ProjectedDefinition}:");
                        sb.Add($@"  {p.Definition.ToWindowsFormat()}");
                        break;
                    }
                case GeoreferencedMode.Auto:
                default:
                    throw new NotSupportedException($@"Mode: {setting.Mode}");
            }

            return sb;
        }

        public static string GetDetails(this GeoreferencedSetting setting, IGeoreferncingHost host)
        {
            var isAutoMode = setting.Mode == GeoreferencedMode.Auto;
            var d = host.CreateTargetSetting(setting);
            return d.GetDetails(isAutoMode, host);
        }

        private static string GetDetails(this GeoreferencedSetting setting, bool sourceIsAutoMode, IGeoreferncingHost host)
        {
            var sb = new StringBuilder();

            if (sourceIsAutoMode)
            {
                sb.AppendLine($@"{GeoStrings.AutoMode} =>");
            }

            var adapter = host.Adapter;

            sb.AppendLine($@"{GeoStrings.GeoreferencedMode}: {setting.Mode.GetString()}");
            switch (setting.Mode)
            {
                case GeoreferencedMode.Local:
                {
                    var p = setting.Local;
                    sb.AppendLine($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                    sb.AppendLine($@"{GeoStrings.AlignOriginToSitePlaneCenter}: {p.AlignOriginToSitePlaneCenter}");
                    sb.AppendLine($@"{GeoStrings.OriginDefaultCoordinate}: {GetDoubleString(p.Rotation, 10)}@{GetDoubleString(p.Latitude, 10)},{GetDoubleString(p.Longitude, 10)},{GetDoubleString(p.Height, 6)}z");
                    break;
                }
                case GeoreferencedMode.Enu:
                {
                    var p = setting.Enu;
                    sb.AppendLine($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                    sb.AppendLine($@"{GeoStrings.AlignOriginToSitePlaneCenter}: {p.AlignOriginToSitePlaneCenter}");
                    sb.AppendLine($@"{GeoStrings.OriginCoordinate}: {GetDoubleString(p.Rotation, 10)}@{GetDoubleString(p.Latitude, 10)},{GetDoubleString(p.Longitude, 10)},{GetDoubleString(p.Height, 6)}z");
                    sb.AppendLine($@"{GeoStrings.AutoAlignToGround}: {p.UseAutoAlignToGround}");
                    break;
                }
                case GeoreferencedMode.Proj:
                {
                    var p = setting.Proj;
                    sb.AppendLine($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
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

        public static string ToBase64(this GeoreferencedSetting setting)
        {
            if (setting == null) return null;

            var json = JsonConvert.SerializeObject(setting, Formatting.None);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }

        public static GeoreferencedSetting CreateFromBase64(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var json = new string(Encoding.UTF8.GetChars(bytes));
            return JsonConvert.DeserializeObject<GeoreferencedSetting>(json);
        }

        private static string GetDoubleString(double n, int digits)
        {
            return Math.Round(n, digits).ToString(CultureInfo.InvariantCulture);
        }

        private static string GetOffsetsString(double[] offsets)
        {
            if (offsets == null) return @"0,0,0";
            return string.Join(@",", offsets.Select(x => GetDoubleString(x, 10)));
        }
    }
}

