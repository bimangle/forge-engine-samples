﻿using System;
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
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Revit.Core
{
    class InnerApp : IExternalApplication
    {
        private const string PANEL_NAME = "Forge Engine Samples";

        public InnerApp()
        {
        }

#region IExternalApplication 成员


        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            application.DialogBoxShowing += UIControlledApplication_DialogBoxShowing;

            #region 增加 UI

            var dllPath = Assembly.GetExecutingAssembly().Location;
            var panel = application.CreateRibbonPanel(PANEL_NAME);

#if !EXPRESS
            {
                var button = new PushButtonData(@"BimAngle_Command_Export_Svfzip", Strings.ToolTextExportToSvfzip, dllPath, typeof(CommandExportSvfzip).FullName);
                button.Image = GetImageSource(Properties.Resources.Converter_16px_1061192);
                button.LargeImage = GetImageSource(Properties.Resources.Converter_32px_1061192);
                button.ToolTip = Strings.ToolTipExportToSvfzip;
                panel.AddItem(button);
            }
#endif

            {
                var button = new PushButtonData(@"BimAngle_Command_Export_Gltf", Strings.ToolTextExportToGltf, dllPath, typeof(CommandExportGltf).FullName);
                button.Image = GetImageSource(Properties.Resources.gltf_16px);
                button.LargeImage = GetImageSource(Properties.Resources.gltf_32px);
                button.ToolTip = Strings.ToolTipExportToGltf;
                panel.AddItem(button);
            }

            {
                var button = new PushButtonData(@"BimAngle_Command_Export_Cesium3DTiles", Strings.ToolTextExportToCesium3DTiles, dllPath, typeof(CommandExportCesium3DTiles).FullName);
                button.Image = GetImageSource(Properties.Resources.Cesium3DTiles_16px);
                button.LargeImage = GetImageSource(Properties.Resources.Cesium3DTiles_32px);
                button.ToolTip = Strings.ToolTipExportToCesium3DTiles;
                panel.AddItem(button);
            }

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

        private ImageSource GetImageSource(Bitmap img)
        {
            var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var imageSourceConverter = new ImageSourceConverter();
            return (ImageSource)imageSourceConverter.ConvertFrom(stream);
        }

        /// <summary>
        /// 获得主路径
        /// </summary>
        /// <returns></returns>
        public static string GetHomePath()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                @"Bimangle",
                @"Bimangle.ForgeEngine.Revit");

            Common.Utils.FileSystemUtility.CreateDirectory(path);

            return path;
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
                var filePath = Path.Combine(GetHomePath(), @"Config.json");
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
                var filePath = Path.Combine(GetHomePath(), @"Config.json");
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

    }
}
