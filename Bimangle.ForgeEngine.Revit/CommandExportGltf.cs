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
    public class CommandExportGltf : IExternalCommand, IExternalCommandAvailability
    {
        private readonly IExternalCommand _Command;

        public CommandExportGltf()
        {
            _Command = new InnerCommandExportGltf();
        }

        #region IExternalCommand 成员

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                return _Command.Execute(commandData, ref message, elements);
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            }
        }

        #endregion

        #region IExternalCommandAvailability 成员

        bool IExternalCommandAvailability.IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
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

                filename = Path.Combine(filename, "Ionic.Zip.dll");

                if (File.Exists(filename))
                {
                    return System.Reflection.Assembly.LoadFrom(filename);
                }
            }
            else if (args.Name.Contains("DotNetZip"))
            {
                var filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                filename = Path.Combine(filename, "DotNetZip.dll");

                if (File.Exists(filename))
                {
                    return System.Reflection.Assembly.LoadFrom(filename);
                }
            }
            else
            {
                //Debug.WriteLine(args.Name);
            }
            return null;
        }
    }
}
