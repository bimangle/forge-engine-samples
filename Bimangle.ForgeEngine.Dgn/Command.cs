
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Bentley.DgnPlatformNET;
using Bentley.MstnPlatformNET;
using Bimangle.ForgeEngine.Dgn.Config;
using Bimangle.ForgeEngine.Dgn.Core;
using Bimangle.ForgeEngine.Dgn.UI;
using Bimangle.ForgeEngine.Dgn.Utility;

//using Bimangle.ForgeEngine.Dgn.Support;

namespace Bimangle.ForgeEngine.Dgn
{
    internal static class Command
    {
        public static void ExportSvfzip(string unparsed)
        {
#if EXPRESS
            Log(@"Unsupported function: 'Export Svfzip'");
#else
            using (AssemblyResolver.Use())
            {
                try
                {
                    var view = Session.GetActiveViewport();
                    var hasSelectElements = SelectionSetManager.NumSelected() > 0;
                    var appConfig = AppConfigManager.Load();
                    var dialog = new FormExport(view, appConfig, hasSelectElements, Titles.SVFZIP);
                    dialog.ShowDialog();
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }
#endif
        }

        public static void ExportGltf(string unparsed)
        {
            using (AssemblyResolver.Use())
            {
                try
                {
                    var view = Session.GetActiveViewport();
                    var hasSelectElements = SelectionSetManager.NumSelected() > 0;
                    var appConfig = AppConfigManager.Load();
                    var dialog = new FormExport(view, appConfig, hasSelectElements, Titles.GLTF);
                    dialog.ShowDialog();
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }
        }

        public static void ExportCesium3DTiles(string unparsed)
        {
            using (AssemblyResolver.Use())
            {
                try
                {
                    var view = Session.GetActiveViewport();
                    var hasSelectElements = SelectionSetManager.NumSelected() > 0;
                    var appConfig = AppConfigManager.Load();
                    var dialog = new FormExport(view, appConfig, hasSelectElements, Titles.CESIUM_3D_TILES);
                    dialog.ShowDialog();
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }
        }

        public static void Log(string s)
        {
            MsgDialog.Log($@"{DateTime.Now: HH:mm:ss.fff} BimAngle Engine: {s}");
        }
    }
}