using System;
using System.Diagnostics;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Navisworks.Config;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Georeferncing;
using Bimangle.ForgeEngine.Navisworks.Utility;

namespace Bimangle.ForgeEngine.Navisworks.Toolset
{
    class CommandToolsetCreateProj
    {
        public void Execute(IWin32Window parentForm, string inputFilePath)
        {
            try
            {
                var homePath = VersionInfo.GetHomePath();

                var appConfig = AppConfigManager.Load();
                var localConfig = appConfig.Cesium3DTiles;

                using (var host = GeoreferncingHost.Create(inputFilePath, homePath, localConfig))
                {
                    var form = new FormProjCreate(host);
                    if (form.ShowDialog(parentForm) == DialogResult.OK &&
                        string.IsNullOrWhiteSpace(form.Definition) == false)
                    {
                        var dialog = new SaveFileDialog();
                        dialog.OverwritePrompt = true;
                        dialog.Filter = @"Projected Definition|*.prj|All Files|*.*";
                        dialog.AddExtension = true;
                        if (dialog.ShowDialog(parentForm) == DialogResult.OK)
                        {
                            form.Definition.SaveToTextFile(dialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                parentForm.ShowMessageBox(ex.ToString());

                Trace.WriteLine(ex.ToString());
            }
        }
    }
}
