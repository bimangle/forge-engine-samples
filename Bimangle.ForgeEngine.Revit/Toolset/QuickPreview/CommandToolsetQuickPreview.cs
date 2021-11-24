using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.Utility;

namespace Bimangle.ForgeEngine.Revit.Toolset.QuickPreview
{
    [Transaction(TransactionMode.Manual)]
    public class CommandToolsetQuickPreview : IExternalCommand, IExternalCommandAvailability
    {
        #region Implementation of IExternalCommand

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var previewAppPath = Path.Combine(
                VersionInfo.GetHomePath(),
                @"Tools",
                @"Browser",
                @"Bimangle.ForgeBrowser.exe"
            );

            try
            {
                Process.Start(previewAppPath);
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
