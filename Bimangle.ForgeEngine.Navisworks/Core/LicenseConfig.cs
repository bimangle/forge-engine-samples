using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Types;
using Newtonsoft.Json.Linq;
#if EXPRESS
using LicenseSessionX = Bimangle.ForgeEngine.Navisworks.Express.LicenseSession;
#else
using LicenseSessionX = Bimangle.ForgeEngine.Navisworks.Pro.LicenseSession;
#endif

namespace Bimangle.ForgeEngine.Navisworks.Core
{
    static class LicenseConfig
    {
        public const string LICENSE_KEY = null;

        public const string CLIENT_ID = @"BimAngle";

        public const string PRODUCT_NAME = @"BimAngle Engine Samples";

        public const string PLUGIN_FOLDER_NAME = @"Bimangle.ForgeEngine.Samples";

        public static Action<byte[]> DeployLicenseFileAction = DeployLicenseFile;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static LicenseSessionX Create()
        {
            LicenseSessionX.Init();
            return new LicenseSessionX(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowDialog(LicenseSessionX session, IWin32Window parent)
        {
            var info = LicenseSessionX.GetLicenseInfo(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);

            LicenseSessionX.ShowLicenseDialog(session.ClientId, session.AppName, parent, DeployLicenseFileAction);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowDialog(IWin32Window parent)
        {
            var info = LicenseSessionX.GetLicenseInfo(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);

            LicenseSessionX.ShowLicenseDialog(CLIENT_ID, PRODUCT_NAME, parent, DeployLicenseFileAction);
        }

        /// <summary>
        /// Deploy license file
        /// </summary>
        /// <param name="buffer"></param>
        public static void DeployLicenseFile(byte[] buffer)
        {
            var versions = new[] { @"2014", @"2015", @"2016", @"2017", @"2018", @"2019", @"2020" };
            var programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            //var programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            foreach (var version in versions)
            {
                var addinsPath = Path.Combine(programDataPath, $@"Autodesk Navisworks Manage {version}", @"Plugins", PLUGIN_FOLDER_NAME);
                if (Directory.Exists(addinsPath) == false) continue;

                var licFilePath = Path.Combine(addinsPath, LicenseSessionX.LICENSE_FILENAME);
                File.WriteAllBytes(licFilePath, buffer);
            }
        }

        public static OemInfo GetOemInfo(string homePath)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var oem = new OemInfo();
            oem.Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ??
                            @"Copyright © BimAngle 2017-2019";
            oem.Generator = $@"{PRODUCT_NAME} v{PackageInfo.VERSION_STRING}";
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
