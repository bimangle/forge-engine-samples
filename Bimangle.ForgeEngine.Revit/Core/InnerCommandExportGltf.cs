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
using Bimangle.ForgeEngine.Revit.Utility;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Revit.Core
{
    [Transaction(TransactionMode.Manual)]
    class InnerCommandExportGltf : IExternalCommand
    {
        public const string TITLE = @"glTF/glb";

        #region IExternalCommand 成员

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            //配置默认字符编码
            GlobalConfig.ConfigDefaultEncoding();

            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            var view = doc.ActiveView as View3D;
            if (null == view)
            {
                ShowMessageBox(Strings.MessageOpen3DViewFirst);
                return Result.Succeeded;
            }

            Dictionary<long, bool> elementIds;

            var elementSelected = uidoc.Selection.GetElementIds();
            if (elementSelected != null && elementSelected.Count > 0)
            {
                elementIds = new Dictionary<long, bool>(elementSelected.Count);
                foreach (var elementId in elementSelected)
                {
                    if (elementId == ElementId.InvalidElementId) continue;
                    elementIds.Add(elementId.Value(), true);
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
                dialog.ShowDialog(commandData.GetMainWindowHandle());
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
            MessageBox.Show(message, VersionInfo.PRODUCT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
