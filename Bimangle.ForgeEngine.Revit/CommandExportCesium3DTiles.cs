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
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.UI;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Revit
{
    [Transaction(TransactionMode.Manual)]
    public class CommandExportCesium3DTiles : IExternalCommand, IExternalCommandAvailability
    {
        private readonly IExternalCommand _Command;

        public CommandExportCesium3DTiles()
        {
            _Command = new InnerCommandExportCesium3DTiles();
        }

        #region IExternalCommand 成员

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += App.OnAssemblyResolve;

                return _Command.Execute(commandData, ref message, elements);
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= App.OnAssemblyResolve;
            }
        }

        #endregion

        #region IExternalCommandAvailability 成员

        bool IExternalCommandAvailability.IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }

        #endregion

    }
}
