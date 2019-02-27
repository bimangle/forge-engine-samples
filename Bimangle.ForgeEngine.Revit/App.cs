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
