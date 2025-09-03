using Bimangle.ForgeEngine.Common.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bimangle.ForgeEngine.Dwg.Core
{
    class App
    {
        /// <summary>
        /// 获得主路径
        /// </summary>
        /// <returns></returns>
        public static string GetHomePath()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                @"Bimangle",
                @"Bimangle.ForgeEngine.Dwg");
            
            Common.Utils.FileSystemUtility.CreateDirectory(path);

            return path;
        }

        public static bool CheckHomeFolder(string homePath)
        {
            return Directory.Exists(homePath) &&
                   Directory.Exists(Path.Combine(homePath, @"Tools"));
        }

        public static string GetFontFolderPath()
        {
            var folderPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            return Path.Combine(folderPath, @"Fonts");
        }


        public static OemInfo GetOemInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var oem = new OemInfo();
            oem.Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ??
                            @"Copyright © BimAngle 2017-2025";
            oem.Generator = PackageInfo.ProductVersion.ToString();
            oem.Title = @"BimAngle.com";

            var oemFilePath = Path.Combine(GetHomePath(), @"Oem.json");
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
                        oem.Generator = string.Format(generator, $@"(For Dwg) {PackageInfo.ProductVersion}");
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
