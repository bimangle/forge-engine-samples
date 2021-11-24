using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Revit.Georeferncing;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Revit.Core
{
    class InnerApp : IExternalApplication
    {
        public InnerApp()
        {
        }

#region IExternalApplication 成员


        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            application.DialogBoxShowing += UIControlledApplication_DialogBoxShowing;

            #region 增加 UI

            CreateUI(application);

            #endregion

            return Result.Succeeded;
        }

        Result IExternalApplication.OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

#endregion

        void UIControlledApplication_DialogBoxShowing(object sender, Autodesk.Revit.UI.Events.DialogBoxShowingEventArgs e)
        {
            if (e is TaskDialogShowingEventArgs args)
            {
                //对于对话框 "打印 - 着色视图的设置已修改", 即使不在批处理模式下也不允许其显示, 避免影响手工转换输出
                if (args.DialogId == @"TaskDialog_Printing_Setting_Changed_For_Shaded_Views")
                {
                    args.OverrideResult(8); //关闭
                }
            }
        }

        private void CreateUI(UIControlledApplication application)
        {
            var dllPath = Assembly.GetExecutingAssembly().Location;
            var panel = application.CreateRibbonPanel(VersionInfo.PANEL_NAME);

#if !EXPRESS
            {
                var button = new PushButtonData($@"{VersionInfo.COMPANY_ID}_Command_Export_Svfzip", Strings.ToolTextExportToSvfzip, dllPath, typeof(CommandExportSvfzip).FullName);
                button.Image = GetImageSource(Properties.Resources.Converter_16px_1061192);
                button.LargeImage = GetImageSource(Properties.Resources.Converter_32px_1061192);
                button.ToolTip = Strings.ToolTipExportToSvfzip;
                panel.AddItem(button);
            }
#endif

            {
                var button = new PushButtonData($@"{VersionInfo.COMPANY_ID}_Command_Export_Gltf", Strings.ToolTextExportToGltf, dllPath, typeof(CommandExportGltf).FullName);
                button.Image = GetImageSource(Properties.Resources.gltf_16px);
                button.LargeImage = GetImageSource(Properties.Resources.gltf_32px);
                button.ToolTip = Strings.ToolTipExportToGltf;
                panel.AddItem(button);
            }

            {
                var button = new PushButtonData($@"{VersionInfo.COMPANY_ID}_Command_Export_Cesium3DTiles", Strings.ToolTextExportToCesium3DTiles, dllPath, typeof(CommandExportCesium3DTiles).FullName);
                button.Image = GetImageSource(Properties.Resources.Cesium3DTiles_16px);
                button.LargeImage = GetImageSource(Properties.Resources.Cesium3DTiles_32px);
                button.ToolTip = Strings.ToolTipExportToCesium3DTiles;
                panel.AddItem(button);
            }

            {
                var data = new PulldownButtonData(@"Toolset", Strings.ToolTextToolset);
                var toolset = panel.AddItem(data) as PulldownButton;
                toolset.Image = GetImageSource(Properties.Resources.Toolset_16px);
                toolset.LargeImage = GetImageSource(Properties.Resources.Toolset_32px);

                toolset.AddPushButton(new PushButtonData(@"QuickPreview", Strings.PreviewAppName, dllPath, typeof(Toolset.QuickPreview.CommandToolsetQuickPreview).FullName));
                toolset.AddSeparator();

                toolset.AddPushButton(new PushButtonData(@"PickPosition", GeoStrings.ToolsetPickPosition, dllPath, typeof(Toolset.PickPosition.CommandToolsetPickPosition).FullName));
                toolset.AddPushButton(new PushButtonData(@"PickPositionFromMap", GeoStrings.ToolsetPickPositionFromMap, dllPath, typeof(Toolset.PickPositionFromMap.CommandToolsetPickPositionFromMap).FullName));
                toolset.AddPushButton(new PushButtonData(@"CreateProj", GeoStrings.ToolsetCreateProj, dllPath, typeof(Toolset.CreateProj.CommandToolsetCreateProj).FullName));
                toolset.AddSeparator();

                toolset.AddPushButton(new PushButtonData(@"CheckRevitJournals", Strings.ToolsetCheckRevitJournals, dllPath, typeof(Toolset.CheckRevitJournals.CommandToolsetCheckRevitJournals).FullName));
                toolset.AddPushButton(new PushButtonData(@"CheckEngineLogs", Strings.ToolsetCheckEngineLogs, dllPath, typeof(Toolset.CheckEngineLogs.CommandToolsetCheckEngineLogs).FullName));
            }

        }

        private ImageSource GetImageSource(Bitmap img)
        {
            var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var imageSourceConverter = new ImageSourceConverter();
            return (ImageSource)imageSourceConverter.ConvertFrom(stream);
        }


        public static bool CheckHomeFolder(string homePath)
        {
            return Directory.Exists(homePath) &&
                   Directory.Exists(Path.Combine(homePath, @"Tools"));
        }

        public static IList<string> GetPreExportSeedFeatures(string formatKey, string versionKey = @"EngineRVT")
        {
            const string KEY = @"PreExportSeedFeatures";

            try
            {
                var filePath = Path.Combine(VersionInfo.GetHomePath(), @"Config.json");
                if (File.Exists(filePath) == false) return null;

                var fileContent = File.ReadAllText(filePath, Encoding.UTF8);
                var json = JObject.Parse(fileContent);

                if (json[versionKey] == null ||
                    json[versionKey][formatKey] == null ||
                    json[versionKey][formatKey][KEY] == null)
                {
                    return null;
                }

                var token = json[versionKey][formatKey];
                return token.Value<JArray>(KEY).Values<string>().ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return null;
            }
        }

        public static void UpdateFromSeedFeatures<TFeatureType>(IList<TFeatureType> features, string formatKey, string dataKey = @"SeedFeatures", string versionKey = @"EngineRVT")
            where TFeatureType : struct
        {
            try
            {
                var filePath = Path.Combine(VersionInfo.GetHomePath(), @"Config.json");
                if (File.Exists(filePath) == false) return;

                var fileContent = File.ReadAllText(filePath, Encoding.UTF8);
                var json = JObject.Parse(fileContent);

                if (json[versionKey] == null ||
                    json[versionKey][formatKey] == null ||
                    json[versionKey][formatKey][dataKey] == null)
                {
                    return;
                }

                var token = json[versionKey][formatKey];
                var seedFeatures = token.Value<JArray>(dataKey).Values<string>().ToList();
                foreach (var s in seedFeatures)
                {
                    if (Enum.TryParse(s, true, out TFeatureType r) && features.Contains(r) == false)
                    {
                        features.Add(r);
                    }
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }


        public static OemInfo GetOemInfo(string homePath)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var oem = new OemInfo();
            oem.Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ??
                            VersionInfo.COPYRIGHT;
            oem.Generator = $@"{VersionInfo.PRODUCT_NAME} v{PackageInfo.VERSION_STRING}";
            oem.Title = VersionInfo.HTML_TITLE;

            var oemFilePath = Path.Combine(homePath, @"Oem.json");
            if (File.Exists(oemFilePath))
            {
                try
                {
                    var s = File.ReadAllText(oemFilePath, Encoding.UTF8);
                    var json = JObject.Parse(s);

                    var copyright = json.Value<string>(@"copyright");
                    if (string.IsNullOrWhiteSpace(copyright) == false)
                    {
                        oem.Copyright = copyright;
                    }

                    var generator = json.Value<string>(@"generator");
                    if (string.IsNullOrWhiteSpace(copyright) == false)
                    {
                        oem.Generator = string.Format(generator, $@"(For Revit) {PackageInfo.VERSION_STRING}");
                    }

                    var title = json.Value<string>(@"title");
                    if (string.IsNullOrWhiteSpace(title) == false)
                    {
                        oem.Title = title;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }

            return oem;
        }
    }
}
