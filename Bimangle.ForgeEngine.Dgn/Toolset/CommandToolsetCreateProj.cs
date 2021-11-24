using System;
using System.Diagnostics;
using System.Windows.Forms;
using Bentley.MstnPlatformNET;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Dgn.Config;
using Bimangle.ForgeEngine.Dgn.Core;
using Bimangle.ForgeEngine.Dgn.Georeferncing;
using Bimangle.ForgeEngine.Dgn.Utility;

namespace Bimangle.ForgeEngine.Dgn.Toolset
{
    public class CommandToolsetCreateProj : IExternalCommand
    {

        #region Implementation of IExternalCommand

        public int Execute(string unparsed, ref string message)
        {
            try
            {
                Form parentForm = null;
                var homePath = VersionInfo.GetHomePath();

                var appConfig = AppConfigManager.Load();
                var localConfig = appConfig.Cesium3DTiles;
                var inputFilePath = Session.Instance.GetActiveFileName();

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
                FormHelper.ShowMessageBox(ex.ToString());

                Trace.WriteLine(ex.ToString());
            }

            return 0;
        }

        #endregion
    }
}
