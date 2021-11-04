using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Windows;
using Bimangle.ForgeEngine.Navisworks.Config;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Georeferncing;
using Bimangle.ForgeEngine.Navisworks.UI;
using Application = Autodesk.Navisworks.Api.Application;
using Orientation = System.Windows.Controls.Orientation;

namespace Bimangle.ForgeEngine.Navisworks
{
    public class Command : CommandHandlerPlugin
    {
        public const string COMMAND_SVFZIP = @"ButtonExportToSvfzip";
        public const string COMMAND_GLTF = @"ButtonExportToGltf";
        public const string COMMAND_3DTILES = @"ButtonExportToCesium3DTiles";

        public const string COMPANY_NAME = @"BimAngle";

#if DEBUG
        public const string PLUGIN_ID = @"Engine_Samples_Debug";
#if EXPRESS
        public const string PRODUCT_NAME = @"BimAngle Engine Express Samples (Debug)";
#else
        public const string PRODUCT_NAME = @"BimAngle Engine Samples (Debug)";
#endif
#else
        public const string PLUGIN_ID = @"Engine_Samples";
#if EXPRESS
        public const string PRODUCT_NAME = @"BimAngle Engine Express Samples";
#else
        public const string PRODUCT_NAME = @"BimAngle Engine Samples";
#endif
#endif

        public const string DEVELOPER_ID = @"Bimangle";

        public const string TITLE_SVF = @"Svfzip";
        public const string TITLE_GLTF = @"glTF/glb";
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
                    case COMMAND_SVFZIP:
                    {
                        var appConfig = AppConfigManager.Load();
                        var dialog = new FormExport(appConfig, TITLE_SVF);
                        dialog.ShowDialog(mainWindow);
                        break;
                    }
#endif
                    case COMMAND_GLTF:
                    {
                        var appConfig = AppConfigManager.Load();
                        var dialog = new FormExport(appConfig, TITLE_GLTF);
                        dialog.ShowDialog(mainWindow);
                        break;
                    }
                    case COMMAND_3DTILES:
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

            AppDomain.CurrentDomain.AssemblyResolve += App.OnAssemblyResolve;
        }

        protected override void OnUnloading()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= App.OnAssemblyResolve;

            base.OnUnloading();
        }

        #endregion
    }
	
	
    [Plugin("Engine_InitUI",            //Plugin name
        Command.DEVELOPER_ID,              //4 character Developer ID or GUID
        ToolTip = "",                        //The tooltip for the item in the ribbon
        DisplayName = "Engine InitUI")]      //Display name for the Plugin in the Ribbon
    public class InitUI : EventWatcherPlugin
    {
        private bool _Init;

#region Overrides of EventWatcherPlugin

        public override void OnLoaded()
        {
            Application.Idle += Application_Idle;
        }

        public override void OnUnloading()
        {
            Application.Idle -= Application_Idle;
        }

#endregion

        private void Application_Idle(object sender, EventArgs e)
        {
            if (_Init) return;

            if (new CreateUI().Execute())
            {
                _Init = true;
                Application.Idle -= Application_Idle;
            }
        }
    }

    public class CreateUI
    {
        public bool Execute()
        {
            var commandHandler = new Command();

            var ribbon = Autodesk.Windows.ComponentManager.Ribbon;
            if (ribbon == null) return false;

            var tabId = $@"{Command.DEVELOPER_ID}.Tab";
            var tab = ribbon.Tabs.FirstOrDefault(x => x.Id == tabId);
            if (tab == null)
            {
                tab = new RibbonTab();
                tab.Id = tab.UID = tabId;
                tab.Title = Command.COMPANY_NAME;
                tab.KeyTip = @"B1";

                //尽量把 Tab 放在 "输出" 右边
                var outputTab = ribbon.Tabs.FirstOrDefault(x => x.Id == @"ID_TabOutput");
                if (outputTab == null)
                {
                    ribbon.Tabs.Add(tab);
                }
                else
                {
                    var index = ribbon.Tabs.IndexOf(outputTab) + 1;
                    ribbon.Tabs.Insert(index, tab);
                }
            }

            var panelId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.Panel";
            var panel = tab.FindPanel(panelId);
            if (panel == null)
            {
                var panelSource = new RibbonPanelSource();
                panelSource.Id = panelSource.UID = panelId + "_Source";
                panelSource.Title = Command.PRODUCT_NAME;
                panelSource.KeyTip = @"E";

                panel = new RibbonPanel();
#if R2014 || R2015 || R2016
                panel.UID = panelId;
#else
                panel.Id = panel.UID = panelId;
#endif
                panel.Source = panelSource;

                tab.Panels.Add(panel);
            }

            #region 添加按钮 ButtonExportToSvfzip
            {
#if !EXPRESS
                var buttonName = Command.COMMAND_SVFZIP;
                var buttonId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.{buttonName}";
                var button = new RibbonButton();
                button.Id = button.UID = buttonId;
                button.Text = Strings.ToolTextExportToSvfzip;
                button.ToolTip = Strings.ToolTipExportToSvfzip;
                button.Image = GetImage(@"Images/ExportToSvfzip_16px.ico");
                button.LargeImage = GetImage(@"Images/ExportToSvfzip_32px.ico");
                button.KeyTip = @"SVF";
                button.Size = RibbonItemSize.Large;
                button.ShowText = true;
                button.ShowImage = true;
                button.Orientation = Orientation.Vertical;
                button.CommandHandler = new DefaultHandler(commandHandler, buttonName);

                panel.Source.Items.Add(button);
#endif
            }
            #endregion

            #region 添加按钮 ButtonExportToGltf
            {
                var buttonName = Command.COMMAND_GLTF;
                var buttonId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.{buttonName}";
                var button = new RibbonButton();
                button.Id = button.UID = buttonId;
                button.Text = Strings.ToolTextExportToGltf;
                button.ToolTip = Strings.ToolTipExportToGltf;
                button.Image = GetImage(@"Images/ExportToGltf_16px.ico");
                button.LargeImage = GetImage(@"Images/ExportToGltf_32px.ico");
                button.KeyTip = @"GLTF";
                button.Size = RibbonItemSize.Large;
                button.ShowText = true;
                button.ShowImage = true;
                button.Orientation = Orientation.Vertical;
                button.CommandHandler = new DefaultHandler(commandHandler, buttonName);

                panel.Source.Items.Add(button);
            }
            #endregion

            #region 添加按钮 ButtonExportToCesium3DTiles
            {
                var buttonName = Command.COMMAND_3DTILES;
                var buttonId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.{buttonName}";
                var button = new RibbonButton();
                button.Id = button.UID = buttonId;
                button.Text = Strings.ToolTextExportToCesium3DTiles;
                button.ToolTip = Strings.ToolTipExportToCesium3DTiles;
                button.Image = GetImage(@"Images/ExportToCesium3DTiles_16px.ico");
                button.LargeImage = GetImage(@"Images/ExportToCesium3DTiles_32px.ico");
                button.KeyTip = @"TILES";
                button.Size = RibbonItemSize.Large;
                button.ShowText = true;
                button.ShowImage = true;
                button.Orientation = Orientation.Vertical;
                button.CommandHandler = new DefaultHandler(commandHandler, buttonName);

                panel.Source.Items.Add(button);
            }
            #endregion

            #region 添加按钮 ButtonToolset
            {
                var buttonToolsetId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.Toolset";
                var buttonToolset = new RibbonMenuButton();
                buttonToolset.Id = buttonToolset.UID = buttonToolsetId;
                buttonToolset.Text = Strings.ToolTextToolset;
                buttonToolset.Image = GetImage(@"Images/Toolset_16px.ico");
                buttonToolset.LargeImage = GetImage(@"Images/Toolset_32px.ico");
                buttonToolset.KeyTip = @"TS";
                buttonToolset.Size = RibbonItemSize.Large;
                buttonToolset.ShowText = true;
                buttonToolset.ShowImage = true;
                buttonToolset.Orientation = Orientation.Vertical;

                panel.Source.Items.Add(buttonToolset);

                var mainWindow = Autodesk.Navisworks.Api.Application.Gui.MainWindow;

                {
                    var menuId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.Toolset.QuickPreview";
                    var menuItem = new RibbonMenuItem();
                    menuItem.Id = menuItem.UID = menuId;
                    menuItem.Text = Strings.PreviewAppName;
                    menuItem.ShowText = true;
                    menuItem.ShowImage = false;
                    menuItem.CommandHandler = new CallbackHandler(state =>
                    {
                        var tool = new Toolset.CommandToolsetQuickPreview();
                        tool.Execute(mainWindow);
                    });

                    buttonToolset.Items.Add(menuItem);
                }

                //增加分隔条
                buttonToolset.Items.Add(new RibbonSeparator());

                {
                    var menuId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.Toolset.PickPosition";
                    var menuItem = new RibbonMenuItem();
                    menuItem.Id = menuItem.UID = menuId;
                    menuItem.Text = GeoStrings.ToolsetPickPosition;
                    menuItem.ShowText = true;
                    menuItem.ShowImage = false;
                    menuItem.CommandHandler = new CallbackHandler(state =>
                    {
                        var tool = new Toolset.CommandToolsetPickPosition();
                        tool.Execute(mainWindow);
                    });

                    buttonToolset.Items.Add(menuItem);
                }

                {
                    var menuId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.Toolset.PickPositionFromMap";
                    var menuItem = new RibbonMenuItem();
                    menuItem.Id = menuItem.UID = menuId;
                    menuItem.Text = GeoStrings.ToolsetPickPositionFromMap;
                    menuItem.ShowText = true;
                    menuItem.ShowImage = false;
                    menuItem.CommandHandler = new CallbackHandler(state =>
                    {
                        var tool = new Toolset.CommandToolsetPickPositionFromMap();
                        tool.Execute(mainWindow);
                    });

                    buttonToolset.Items.Add(menuItem);
                }

                {
                    var menuId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.Toolset.CreateProj";
                    var menuItem = new RibbonMenuItem();
                    menuItem.Id = menuItem.UID = menuId;
                    menuItem.Text = GeoStrings.ToolsetCreateProj;
                    menuItem.ShowText = true;
                    menuItem.ShowImage = false;
                    menuItem.CommandHandler = new CallbackHandler(state =>
                    {
                        var tool = new Toolset.CommandToolsetCreateProj();
                        tool.Execute(mainWindow, Application.ActiveDocument?.FileName);
                    });

                    buttonToolset.Items.Add(menuItem);
                }

                //增加分隔条
                buttonToolset.Items.Add(new RibbonSeparator());

                {
                    var menuId = $@"{Command.PLUGIN_ID}.{Command.DEVELOPER_ID}.Toolset.CheckEngineLogs";
                    var menuItem = new RibbonMenuItem();
                    menuItem.Id = menuItem.UID = menuId;
                    menuItem.Text = Strings.ToolsetCheckEngineLogs;
                    menuItem.ShowText = true;
                    menuItem.ShowImage = false;
                    menuItem.CommandHandler = new CallbackHandler(state =>
                    {
                        var tool = new Toolset.CommandToolsetCheckEngineLogs();
                        tool.Execute(mainWindow);
                    });

                    buttonToolset.Items.Add(menuItem);
                }
            }
            #endregion

            return true;
        }

        private ImageSource GetImage(string imageFilePath)
        {
            var baseUri = new Uri(Assembly.GetCallingAssembly().Location);
            var result = new Uri(baseUri, imageFilePath);
            return File.Exists(result.OriginalString)
                ? new BitmapImage(new Uri(result.OriginalString, UriKind.RelativeOrAbsolute))
                : null;
        }

        private class DefaultHandler : ICommand
        {
            private EventHandler _CanExecuteChanged;
            private readonly CommandHandlerPlugin _CommandHandler;
            private readonly string _Command;

            public DefaultHandler(CommandHandlerPlugin commandHandler, string command)
            {
                _CommandHandler = commandHandler;
                _Command = command;
            }

            #region Implementation of ICommand

            bool ICommand.CanExecute(object parameter)
            {
                return true;
            }

            void ICommand.Execute(object parameter)
            {
                try
                {
                    AppDomain.CurrentDomain.AssemblyResolve += App.OnAssemblyResolve;

                    _CommandHandler.ExecuteCommand(_Command, parameter?.ToString());
                }
                finally
                {
                    AppDomain.CurrentDomain.AssemblyResolve -= App.OnAssemblyResolve;
                }
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add => _CanExecuteChanged += value;
                remove => _CanExecuteChanged -= value;
            }

            #endregion
        }

        private class CallbackHandler : ICommand
        {
            private EventHandler _CanExecuteChanged;
            private readonly Action<object> _Callback;

            public CallbackHandler(Action<object> callback)
            {
                _Callback = callback;
            }

            #region Implementation of ICommand

            bool ICommand.CanExecute(object parameter)
            {
                return true;
            }

            void ICommand.Execute(object parameter)
            {
                try
                {
                    AppDomain.CurrentDomain.AssemblyResolve += App.OnAssemblyResolve;

                    _Callback?.Invoke(parameter);
                }
                finally
                {
                    AppDomain.CurrentDomain.AssemblyResolve -= App.OnAssemblyResolve;
                }
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add => _CanExecuteChanged += value;
                remove => _CanExecuteChanged -= value;
            }

            #endregion
        }

    }
}
