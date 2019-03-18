using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Navisworks.Api.Plugins;
using Bimangle.ForgeEngine.Navisworks.Config;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.UI;

namespace Bimangle.ForgeEngine.Navisworks
{
    [Plugin(@"Bimangle.ForgeEngine.Sample", @"Bimangle",
        DisplayName = @"Export To Svfzip, glTF/glb, 3D Tiles(For Cesium)",
        ExtendedToolTip = @"Export To Svfzip, glTF/glb, 3D Tiles(For Cesium)",
        Options = PluginOptions.SupportsControls,
        ToolTip = @"Export To Svfzip, glTF/glb, 3D Tiles(For Cesium)"
    )]
    [RibbonLayout(@"Command.xaml")]
    [RibbonTab(@"ForgeEngineRibbonTab")]
#if !EXPRESS
    [Command(@"ButtonExportToSvfzip")]
#endif
    [Command(@"ButtonExportToGltf")]
	[Command(@"ButtonExportToCesium3DTiles")]
    [AddInPlugin(AddInLocation.Export)]
    public class Command : CommandHandlerPlugin
    {
        public const string TITLE_GLTF = @"glTF/glb";
        public const string TITLE_SVF = @"Svfzip";
        public const string TITLE_3DTILES = @"3D Tiles";

        #region Overrides of CommandHandlerPlugin

        public override int ExecuteCommand(string name, params string[] parameters)
        {
            var mainWindow = Autodesk.Navisworks.Api.Application.Gui.MainWindow;
            try
            {
                switch (name)
                {
#if !EXPRESS
                    case @"ButtonExportToSvfzip":
                    {
                        var appConfig = AppConfigManager.Load();
                        var dialog = new FormExport(appConfig, TITLE_SVF);
                        dialog.ShowDialog(mainWindow);
                        break;
                    }
#endif
                    case @"ButtonExportToGltf":
                    {
                        var appConfig = AppConfigManager.Load();
                        var dialog = new FormExport(appConfig, TITLE_GLTF);
                        dialog.ShowDialog(mainWindow);
                        break;
                    }
                    case @"ButtonExportToCesium3DTiles":
                    {
                        var appConfig = AppConfigManager.Load();
                        var dialog = new FormExport(appConfig, TITLE_3DTILES);
                        dialog.ShowDialog(mainWindow);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(mainWindow, ex.ToString(), @"Exception", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            return 0;
        }

        #endregion

        #region Overrides of Plugin

        protected override void OnLoaded()
        {
            base.OnLoaded();

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        protected override void OnUnloading()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;

            base.OnUnloading();
        }

        #endregion

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var mapping = new Dictionary<string, string>
            {
                {"Newtonsoft.Json", "Newtonsoft.Json.dll"},
                {"DotNetZip", "DotNetZip.dll"}
            };

            try
            {
                foreach (var key in mapping.Keys)
                {
                    if (args.Name.Contains(key))
                    {
                        var folderPath =
                            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        if (folderPath == null) continue;

                        var filePath = Path.Combine(folderPath, mapping[key]);
                        if (File.Exists(filePath) == false) continue;

                        return System.Reflection.Assembly.LoadFrom(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
