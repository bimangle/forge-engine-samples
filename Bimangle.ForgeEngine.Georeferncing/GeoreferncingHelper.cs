using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Georeferncing.Interface;
using Bimangle.ForgeEngine.Georeferncing.Utility;
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
                    return GeoStrings.GeoreferencedModeAuto;
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
                case ProjSourceType.MetadataXml:
                    return MetadataXml.FILE_NAME;
                default:
                    throw new ArgumentOutOfRangeException(nameof(t), t, null);
            }
        }

        public static string GetString(this ProjOffsetType t)
        {
            switch (t)
            {
                case ProjOffsetType.Auto:
                    return GeoStrings.ProjOffsetTypeAuto;
                case ProjOffsetType.None:
                    return GeoStrings.ProjOffsetTypeNone;
                case ProjOffsetType._2D_Params3:
                    return GeoStrings.ProjOffsetType2dP3;
                case ProjOffsetType._2D_Params4:
                    return GeoStrings.ProjOffsetType2dP4;
                case ProjOffsetType._3D_Params3:
                    return GeoStrings.ProjOffsetType3dP3;
                case ProjOffsetType._3D_Params4:
                    return GeoStrings.ProjOffsetType3dP4;
                case ProjOffsetType._3D_Params7:
                    return GeoStrings.ProjOffsetType3dP7;
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

                    if (adapter.IsRevit() || p.Origin != OriginType.Default)
                    {
                        sb.Add($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                    }

                    if (p.AlignOriginToSitePlaneCenter)
                    {
                        sb.Add($@"{GeoStrings.OriginModelCoordinate}: ({GeoStrings.AlignOriginToSitePlaneCenter})");
                    }
                    else if (p.OriginOffset != null && p.OriginOffset.Length >= 3)
                    {
                        var n = p.OriginOffset;
                        sb.Add($@"{GeoStrings.OriginModelCoordinate}: (N:{n[1].ToMetreString()}, E:{n[0].ToMetreString()}, H:{n[2].ToMetreString()})");
                    }

                    var siteString = $@"{p.Rotation.ToDegreeString()}@{p.Latitude.ToLatLonString()},{p.Longitude.ToLatLonString()},{p.Height.ToMetreString()}z";
                    if (p.UseProjectLocation)
                    {
                        sb.Add($@"{GeoStrings.OriginDefaultCoordinate}: ({GeoStrings.UseProjectLocation}: {siteString})");
                    }
                    else
                    {
                        sb.Add($@"{GeoStrings.OriginDefaultCoordinate}: {siteString}");
                    }
                    break;
                }
                case GeoreferencedMode.Enu:
                {
                    var p = setting.Enu;
                    if (adapter.IsRevit() || p.Origin != OriginType.Default)
                    {
                        sb.Add($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                    }

                    if (p.AlignOriginToSitePlaneCenter)
                    {
                        sb.Add($@"{GeoStrings.OriginModelCoordinate}: ({GeoStrings.AlignOriginToSitePlaneCenter})");
                    }
                    else if (p.OriginOffset != null && p.OriginOffset.Length >= 3)
                    {
                        var n = p.OriginOffset;
                        sb.Add($@"{GeoStrings.OriginModelCoordinate}: (N:{n[1].ToMetreString()}, E:{n[0].ToMetreString()}, H:{n[2].ToMetreString()})");
                    }

                    var siteString = $@"{p.Rotation.ToDegreeString()}@{p.Latitude.ToLatLonString()},{p.Longitude.ToLatLonString()},{p.Height.ToMetreString()}z";
                    if (p.UseProjectLocation)
                    {
                        sb.Add($@"{GeoStrings.OriginCoordinate}: ({GeoStrings.UseProjectLocation}: {siteString})");
                    }
                    else
                    {
                        sb.Add($@"{GeoStrings.OriginCoordinate}: {siteString}");
                    }

                    if (p.UseAutoAlignToGround)
                    {
                        sb.Add($@"{GeoStrings.AutoAlignToGround}: {p.UseAutoAlignToGround}");
                    }

                    break;
                }
                case GeoreferencedMode.Proj:
                {
                    var p = setting.Proj;
                    if (adapter.IsRevit() || p.Origin != OriginType.Default)
                    {
                        sb.Add($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                    }

                    sb.Add($@"{GeoStrings.DefinitionSource}:");
                    sb.Add($@"  {p.DefinitionSource.GetString()}");

                    if (string.IsNullOrWhiteSpace(p.DefinitionFileName) == false)
                    {
                        sb.Add($@"  {p.DefinitionFileName}");
                    }

                    sb.Add($@"{GeoStrings.ProjectedDefinition}:");
                    sb.Add($@"  {p.Definition.ToWindowsFormat()}");

                    if (p.OffsetType != ProjOffsetType.None)
                    {
                        sb.Add($@"{GeoStrings.CoordinateOffset}: {p.GetOffsetString()}");
                    }

                    if(p.HasProjectionHeight())
                    {
                        sb.Add($@"{GeoStrings.ProjectionHeight}: {p.ProjectionHeight.ToMetreString()}");
                    }

                    if (p.HasGeoidHeightCorrection())
                    {
                        sb.Add($@"{GeoStrings.GeoidHeightCorrection}:");

                        if (p.HasGeoidGrid())
                        {
                            sb.Add($@"  {GeoStrings.GeoidGrid}: {p.GeoidGrid}");
                        }

                        if (p.HasGeoidConstantOffset())
                        {
                            sb.Add($@"  {GeoStrings.GeoidConstantOffset}: {p.GeoidConstantOffset.ToMetreString()}");
                        }
                    }
                    break;
                }
                case GeoreferencedMode.Auto:
                {
                    var p = setting.Auto;
                    if (adapter.IsRevit() || p.Origin != OriginType.Default)
                    {
                        sb.Add($@"{GeoStrings.OriginLocation}: {adapter.GetLocalString(p.Origin)}");
                    }
                    break;
                }
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

            var brief = setting.GetBrief(host.Adapter);
            foreach (var s in brief)
            {
                sb.AppendLine(s);
            }

            return sb.ToString();
        }

        public static string ToBase64(this GeoreferencedSetting setting)
        {
            if (setting == null) return null;

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            var json = JsonConvert.SerializeObject(setting, Formatting.None, options);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }

        public static GeoreferencedSetting CreateFromBase64(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var json = new string(Encoding.UTF8.GetChars(bytes));
            return JsonConvert.DeserializeObject<GeoreferencedSetting>(json);
        }
    }
}

