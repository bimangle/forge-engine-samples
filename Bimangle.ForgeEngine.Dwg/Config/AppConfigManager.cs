using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Bimangle.ForgeEngine.Dwg.CLI.Utility;
using Newtonsoft.Json;

namespace Bimangle.ForgeEngine.Dwg.CLI.Config
{
    static class AppConfigManager
    {

        private const string FILE_NAME = "Bimangle.ForgeEngine.Sample.cfg";

        public static AppConfig Load()
        {
            try
            {
                var filePath = AppHelper.GetPath(FILE_NAME);
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var json = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<AppConfig>(json);
                    if (result.Svf == null) result.Svf = new AppConfigSvf();
                    return result;
                }
            }
            catch (Exception)
            {
                return new AppConfig();
            }
        }

        public static bool Save(this AppConfig config)
        {
            try
            {

                var filePath = AppHelper.GetPath(FILE_NAME);
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        var json = JsonConvert.SerializeObject(config);
                        writer.Write(json);
                        writer.Flush();
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
