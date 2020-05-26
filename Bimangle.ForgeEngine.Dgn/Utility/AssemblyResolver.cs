using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Dgn.Utility
{
    class AssemblyResolver : IDisposable
    {
        public static AssemblyResolver Use()
        {
            return new AssemblyResolver();
        }

        private AssemblyResolver()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
        }

        #region IDisposable

        void IDisposable.Dispose()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= OnCurrentDomainAssemblyResolve;
        }

        #endregion

        private static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
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
