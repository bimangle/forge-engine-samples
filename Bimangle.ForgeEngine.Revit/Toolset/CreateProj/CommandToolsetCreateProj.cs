using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Revit.Config;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.Georeferncing;
using Bimangle.ForgeEngine.Revit.Utility;

namespace Bimangle.ForgeEngine.Revit.Toolset.CreateProj
{
    [Transaction(TransactionMode.Manual)]
    public class CommandToolsetCreateProj : IExternalCommand, IExternalCommandAvailability
    {
        #region Implementation of IExternalCommand

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var document = commandData.View.Document;
                var homePath = VersionInfo.GetHomePath();

                var appConfig = AppConfigManager.Load();
                var localConfig = appConfig.Cesium3DTiles;

                using (var host = GeoreferncingHost.Create(document, homePath, localConfig))
                {
                    var form = new FormProjCreate(host);
                    if (form.ShowDialog(commandData.GetMainWindowHandle()) == DialogResult.OK &&
                        string.IsNullOrWhiteSpace(form.Definition) == false)
                    {
                        var dialog = new SaveFileDialog();
                        dialog.OverwritePrompt = true;
                        dialog.Filter = @"Projected Definition|*.prj|All Files|*.*";
                        dialog.AddExtension = true;
                        if (dialog.ShowDialog(commandData.GetMainWindowHandle()) == DialogResult.OK)
                        {
                            form.Definition.SaveToTextFile(dialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show(@"Error", ex.ToString());

                Trace.WriteLine(ex.ToString());
            }

            return Result.Succeeded;
        }

        #endregion

        #region Implementation of IExternalCommandAvailability

        bool IExternalCommandAvailability.IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }

        #endregion

    }
}
