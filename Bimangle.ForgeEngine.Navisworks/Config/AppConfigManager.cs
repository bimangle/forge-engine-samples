using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Bimangle.ForgeEngine.Navisworks.Utility;
using Newtonsoft.Json;

namespace Bimangle.ForgeEngine.Navisworks.Config
{
    static class AppConfigManager
    {
        private const string FILE_NAME = "Bimangle.ForgeEngine.Sample.cfg";

        public static AppConfig Load()
        {
            try
            {
                var filePath = AppHelper.GetPath(FILE_NAME);
                if (File.Exists(filePath) == false)
                {
                    return new AppConfig();
                }

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        var json = reader.ReadToEnd();
                        var result = JsonConvert.DeserializeObject<AppConfig>(json);
                        if (result.Local == null) result.Local = new AppLocalConfig();
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
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
                Debug.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
