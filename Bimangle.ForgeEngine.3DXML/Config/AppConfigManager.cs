﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine._3DXML.Utility;
using Newtonsoft.Json;

namespace Bimangle.ForgeEngine._3DXML.Config
{
    static class AppConfigManager
    {
        private const string FILE_NAME = "Bimangle.ForgeEngine.Samples.cfg";

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
                using (var reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var settings = new JsonSerializerSettings();
                    settings.ObjectCreationHandling = ObjectCreationHandling.Replace;

                    var json = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<AppConfig>(json, settings);
                    if (result.Svf == null) result.Svf = new AppConfigSvf();
                    return result;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return new AppConfig();
            }
        }

        public static bool Save(this AppConfig config)
        {
            try
            {
                var filePath = AppHelper.GetPath(FILE_NAME);
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                using (var writer = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    var json = JsonConvert.SerializeObject(config);
                    writer.Write(json);
                    writer.Flush();

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
