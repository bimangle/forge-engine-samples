using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Types;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Navisworks.Core
{
    static class App
    {

        public static bool CheckHomeFolder(string homePath)
        {
            return Directory.Exists(homePath) &&
                   Directory.Exists(Path.Combine(homePath, @"Tools"));
        }
        
        public static System.Reflection.Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var mapping = new Dictionary<string, string>
            {
                {"Newtonsoft.Json", "Newtonsoft.Json.dll"},
                {"DotNetZip", "DotNetZip.dll"}
            };

            try
            {
                foreach (var key in mapping.Keys)
                {
                    if (args.Name.Contains(key))
                    {
                        var folderPath =
                            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        if (folderPath == null) continue;

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

        public static OemInfo GetOemInfo(string homePath)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var oem = new OemInfo();
            oem.Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ??
                            VersionInfo.COPYRIGHT;
            oem.Generator = $@"{VersionInfo.PRODUCT_NAME} v{PackageInfo.VERSION_STRING}";
            oem.Title = VersionInfo.HTML_TITLE;

            var oemFilePath = Path.Combine(homePath, @"Oem.json");
            if (File.Exists(oemFilePath))
            {
                try
                {
                    var s = File.ReadAllText(oemFilePath, Encoding.UTF8);
                    var json = JObject.Parse(s);

                    var copyright = json.Value<string>(@"copyright");
                    if (string.IsNullOrWhiteSpace(copyright) == false)
                    {
                        oem.Copyright = copyright;
                    }

                    var generator = json.Value<string>(@"generator");
                    if (string.IsNullOrWhiteSpace(copyright) == false)
                    {
                        oem.Generator = string.Format(generator, $@"(For Navisworks) {PackageInfo.VERSION_STRING}");
                    }

                    var title = json.Value<string>(@"title");
                    if (string.IsNullOrWhiteSpace(title) == false)
                    {
                        oem.Title = title;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }

            return oem;
        }
    }
}
