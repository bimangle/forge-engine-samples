using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Types.Misc;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Revit.Core
{
    static class GlobalConfig
    {
        public static bool CheckHomeFolder(string homePath)
        {
            return Directory.Exists(homePath) &&
                   Directory.Exists(Path.Combine(homePath, @"Tools"));
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
                        oem.Generator = string.Format(generator, $@"(For Revit) {PackageInfo.VERSION_STRING}");
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

        /// <summary>
        /// 配置默认编码器
        /// </summary>
        public static void ConfigDefaultEncoding()
        {
#if NET8_0_OR_GREATER

            //注册代码页
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                var code = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage;
                var encoding = Encoding.GetEncoding(code);
                EncodingConfig.Set(encoding);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
#endif
        }
    }
}
