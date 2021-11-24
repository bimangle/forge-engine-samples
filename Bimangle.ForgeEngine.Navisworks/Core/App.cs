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

        public static IList<string> GetPreExportSeedFeatures(string formatKey, string versionKey = @"EngineNW")
        {
            const string KEY = @"PreExportSeedFeatures";

            try
            {
                var filePath = Path.Combine(VersionInfo.GetHomePath(), @"Config.json");
                if (File.Exists(filePath) == false) return null;

                var fileContent = File.ReadAllText(filePath, Encoding.UTF8);
                var json = JObject.Parse(fileContent);

                if (json[versionKey] == null ||
                    json[versionKey][formatKey] == null ||
                    json[versionKey][formatKey][KEY] == null)
                {
                    return null;
                }

                var token = json[versionKey][formatKey];
                return token.Value<JArray>(KEY).Values<string>().ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return null;
            }
        }
        
        public static void UpdateFromSeedFeatures<TFeatureType>(IList<TFeatureType> features, string formatKey, string dataKey = @"SeedFeatures", string versionKey = @"EngineNW")
            where TFeatureType : struct
        {
            try
            {
                var filePath = Path.Combine(VersionInfo.GetHomePath(), @"Config.json");
                if (File.Exists(filePath) == false) return;

                var fileContent = File.ReadAllText(filePath, Encoding.UTF8);
                var json = JObject.Parse(fileContent);

                if (json[versionKey] == null ||
                    json[versionKey][formatKey] == null ||
                    json[versionKey][formatKey][dataKey] == null)
                {
                    return;
                }

                var token = json[versionKey][formatKey];
                var seedFeatures = token.Value<JArray>(dataKey).Values<string>().ToList();
                foreach (var s in seedFeatures)
                {
                    if (Enum.TryParse(s, true, out TFeatureType r) && features.Contains(r) == false)
                    {
                        features.Add(r);
                    }
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
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
