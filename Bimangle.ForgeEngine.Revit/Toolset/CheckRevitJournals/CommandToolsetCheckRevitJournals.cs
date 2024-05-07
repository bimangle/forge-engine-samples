using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.Utility;
using RevitUI = Autodesk.Revit.UI;

namespace Bimangle.ForgeEngine.Revit.Toolset.CheckRevitJournals
{
    [Transaction(TransactionMode.Manual)]
    public class CommandToolsetCheckRevitJournals : IExternalCommand, IExternalCommandAvailability
    {
        #region Implementation of IExternalCommand

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var journalsPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Autodesk",
                    @"Revit",
                    $@"Autodesk Revit {PackageInfo.REVIT_VERSION}",
                    @"Journals");
                Process.Start(journalsPath);
            }
            catch (Exception ex)
            {
                RevitUI.TaskDialog.Show(@"Error", ex.ToString());

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
