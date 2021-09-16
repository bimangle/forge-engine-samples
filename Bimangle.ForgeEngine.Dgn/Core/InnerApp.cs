using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Dgn.Core.Batch;
using Bimangle.Libs.License.Types;
using Newtonsoft.Json.Linq;
#if EXPRESS
using LicenseSessionX = Bimangle.ForgeEngine.Dgn.Express.LicenseSession;
#else
using LicenseSessionX = Bimangle.ForgeEngine.Dgn.Pro.LicenseSession;
#endif

namespace Bimangle.ForgeEngine.Dgn.Core
{
    class InnerApp
    {
        public InnerApp()
        {
        }

        private void Log(MessageObj msg)
        {
            Router.Instance.SendMessage(msg);

            Debug.WriteLine(msg.Key + ": " + string.Join(",", msg.Items));
        }

        /// <summary>
        /// 获得主路径
        /// </summary>
        /// <returns></returns>
        public static string GetHomePath()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                @"Bimangle",
                @"Bimangle.ForgeEngine.Dgn");

            // Common.Utils.FileSystemUtility.CreateDirectory(path);

            return path;
        }

        public static bool CheckHomeFolder(string homePath)
        {
            return Directory.Exists(homePath) &&
                   Directory.Exists(Path.Combine(homePath, @"Tools"));
        }

        public static IList<string> GetPreExportSeedFeatures(string formatKey, string versionKey = @"EngineDGN")
        {
            const string KEY = @"PreExportSeedFeatures";

            try
            {
                var filePath = Path.Combine(GetHomePath(), @"Config.json");
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

        public static void UpdateFromSeedFeatures<TFeatureType>(IList<TFeatureType> features, string formatKey, string dataKey = @"SeedFeatures", string versionKey = @"EngineDGN")
            where TFeatureType : struct
        {
            try
            {
                var filePath = Path.Combine(GetHomePath(), @"Config.json");
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

    }
}
