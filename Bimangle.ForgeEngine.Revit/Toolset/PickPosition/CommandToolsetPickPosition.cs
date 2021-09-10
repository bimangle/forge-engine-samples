using System;
using System.Diagnostics;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Utility;

namespace Bimangle.ForgeEngine.Revit.Toolset.PickPosition
{
    [Transaction(TransactionMode.Manual)]
    public class CommandToolsetPickPosition : IExternalCommand, IExternalCommandAvailability
    {
        #region Implementation of IExternalCommand

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var form = new FormPickPosition(commandData);
            form.InitExternalEvent();
            form.Show(commandData.GetMainWindowHandle());

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
