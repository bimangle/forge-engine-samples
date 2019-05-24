using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Types;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Dwg.CLI.Core
{
    static class LicenseConfig
    {
        public const string LICENSE_KEY = null;

        public const string CLIENT_ID = @"BimAngle";

        public const string PRODUCT_NAME = @"BimAngle Engine Samples";

        public static Action<byte[]> DeployLicenseFileAction = null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static LicenseSession Create()
        {
            LicenseSession.Init();
            return new LicenseSession(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowDialog(LicenseSession session, IWin32Window parent)
        {
            var info = LicenseSession.GetLicenseInfo(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);

            LicenseSession.ShowLicenseDialog(session.ClientId, session.AppName, parent, DeployLicenseFileAction);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowDialog(IWin32Window parent)
        {
            var info = LicenseSession.GetLicenseInfo(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);

            LicenseSession.ShowLicenseDialog(CLIENT_ID, PRODUCT_NAME, parent, DeployLicenseFileAction);
        }

        public static OemInfo GetOemInfo(string homePath)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var oem = new OemInfo();
            oem.Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ??
                            @"Copyright © BimAngle 2017-2019";
            oem.Generator = $@"{PRODUCT_NAME} v{PackageInfo.ProductVersion}";
            oem.Title = @"BimAngle.com";

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
