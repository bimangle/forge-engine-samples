using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Config;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.UI;

namespace Bimangle.ForgeEngine.Revit
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public const string TITLE = @"Bimangle.ForgeEngine.Sample";

        #region IExternalCommand 成员

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

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

            var appConfig = AppConfigManager.Load();
            var dialog = new FormExportSvfzip(view, appConfig, elementIds);
            dialog.ShowDialog();

            return Result.Succeeded;
        }

        #endregion

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("Newtonsoft"))
            {
                var filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                filename = Path.Combine(filename, "Newtonsoft.Json.dll");

                if (File.Exists(filename))
                {
                    return System.Reflection.Assembly.LoadFrom(filename);
                }
            }
            else if (args.Name.Contains("Ionic"))
            {
                var filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                filename = Path.Combine(filename, "DotNetZip.dll");

                if (File.Exists(filename))
                {
                    return System.Reflection.Assembly.LoadFrom(filename);
                }
            }
            return null;
        }

        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message, TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
