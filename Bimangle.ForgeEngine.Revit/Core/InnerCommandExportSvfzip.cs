using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Config;
using Bimangle.ForgeEngine.Revit.UI;
using Bimangle.ForgeEngine.Revit.Core;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Revit.Core
{
    [Transaction(TransactionMode.Manual)]
    class InnerCommandExportSvfzip : IExternalCommand
    {
        public const string TITLE = @"Svfzip";

        #region IExternalCommand 成员

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            var view = doc.ActiveView as View3D;
            if (null == view)
            {
                ShowMessageBox(Strings.MessageOpen3DViewFirst);
                return Result.Succeeded;
            }

            Dictionary<int, bool> elementIds;

            var elementSelected = uidoc.Selection.GetElementIds();
            if (elementSelected != null && elementSelected.Count > 0)
            {
                elementIds = new Dictionary<int, bool>(elementSelected.Count);
                foreach (var elementId in elementSelected)
                {
                    if (elementId == ElementId.InvalidElementId) continue;
                    elementIds.Add(elementId.IntegerValue, true);
                }
            }
            else
            {
                elementIds = null;
            }

            try
            {
#if R2014
                uidoc.Selection.Elements?.Clear();
#else
                uidoc.Selection.SetElementIds(new List<ElementId>());
#endif
                Application.DoEvents();

                var appConfig = AppConfigManager.Load();
                var dialog = new FormExport(uidoc, view, appConfig, elementIds, TITLE);
                dialog.ShowDialog();
            }
            finally
            {
#if R2014
                if (elementSelected != null && elementSelected.Count > 0)
                {
                    foreach (var elementId in elementSelected)
                    {
                        var element = doc.GetElement(elementId);
                        uidoc.Selection.Elements?.Add(element);
                    }
                }
#else
                uidoc.Selection.SetElementIds(elementSelected);
#endif
            }

            return Result.Succeeded;
        }

#endregion

        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message, LicenseConfig.PRODUCT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
