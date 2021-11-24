using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.Utility;

namespace Bimangle.ForgeEngine.Revit.Toolset.CheckEngineLogs
{
    [Transaction(TransactionMode.Manual)]
    public class CommandToolsetCheckEngineLogs : IExternalCommand, IExternalCommandAvailability
    {
        #region Implementation of IExternalCommand

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var logsPath = Path.Combine(
                    VersionInfo.GetHomePath(),
                    @"Logs");
                if (Directory.Exists(logsPath) == false)
                {
                    Directory.CreateDirectory(logsPath);
                }
                Process.Start(logsPath);
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
