using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Core;

namespace Bimangle.ForgeEngine.Revit
{
    public class App : IExternalApplication
    {
        private readonly IExternalApplication _App;

        public App()
        {
            _App = new InnerApp();
        }

        #region IExternalApplication 成员

        Result IExternalApplication.OnShutdown(UIControlledApplication application)
        {
            return _App.OnShutdown(application);
        }

        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            return _App.OnStartup(application);
        }

        #endregion

        internal static System.Reflection.Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var mapping = new Dictionary<string, string>
            {
                {"Newtonsoft.Json", "Newtonsoft.Json.dll"},
                {"Ionic.Zip", "Ionic.Zip.dll"},
                {"DotNetZip", "DotNetZip.dll"}
            };

#if RENAME_SDK_DLL
            {
                var fields = args.Name.Split(',');
                var name = fields[0];
                var culture = fields[2];
                if (name.EndsWith(".resources") && !culture.EndsWith("neutral"))
                {
                    return null;
                }

                mapping.Add("Bimangle.ForgeEngine.Common", "Bimangle.Engine.Common.dll");
                mapping.Add("Bimangle.ForgeEngine.Dwf.Base", "Bimangle.Engine.Dwf.Base.dll");
                mapping.Add("Bimangle.ForgeEngine.Dwf.Support", "Bimangle.Engine.Dwf.Support.dll");
                mapping.Add("Bimangle.ForgeEngine.Georeferncing", "Bimangle.Engine.Georeferncing.dll");

                for (var v = 2014; v <= 2024; v++)
                {
                    mapping.Add($@"Bimangle.ForgeEngine.Revit{v}", $@"Bimangle.Engine.Revit{v}.dll");
                }
            }
#endif

            try
            {
                var folderPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (folderPath == null) return null;

                foreach (var key in mapping.Keys)
                {
                    if (args.Name.Contains(key))
                    {
                        var filePath = Path.Combine(folderPath, mapping[key]);
                        if (File.Exists(filePath) == false) continue;

                        return System.Reflection.Assembly.LoadFrom(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
