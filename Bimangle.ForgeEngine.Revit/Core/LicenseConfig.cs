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
using LicenseSessionX = Bimangle.ForgeEngine.Revit.Express.LicenseSession;
#else
using LicenseSessionX = Bimangle.ForgeEngine.Revit.Pro.LicenseSession;
#endif

namespace Bimangle.ForgeEngine.Revit.Core
{
    static class LicenseConfig
    {
        public const string CLIENT_ID = @"BimAngle";

        public const string PRODUCT_NAME = @"BimAngle Engine Samples";

        public const string PLUGIN_FOLDER_NAME = @"Bimangle.ForgeEngine.Samples";

        public static Action<byte[]> DeployLicenseFileAction = DeployLicenseFile;

        static LicenseConfig()
        {
            Init();
        }

        public static string GetLicenseKey()
        {
            return null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Init()
        {
            var config = new Libs.License.Types.LicenseConfig();
            //config.DisableUsbkey = true;
            //config.IssuerKeyId = 123456;
            //config.DisableTrial = true;

            LicenseSessionX.Init(config);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static LicenseSessionX Create()
        {
            LicenseSessionX.Init();
            return new LicenseSessionX(CLIENT_ID, PRODUCT_NAME, GetLicenseKey());
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowDialog(LicenseSessionX session, IWin32Window parent)
        {
            var info = LicenseSessionX.GetLicenseInfo(CLIENT_ID, PRODUCT_NAME, GetLicenseKey());

            LicenseSessionX.ShowLicenseDialog(session.ClientId, session.AppName, parent, DeployLicenseFileAction);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowDialog(IWin32Window parent)
        {
            var info = LicenseSessionX.GetLicenseInfo(CLIENT_ID, PRODUCT_NAME, GetLicenseKey());

            LicenseSessionX.ShowLicenseDialog(CLIENT_ID, PRODUCT_NAME, parent, DeployLicenseFileAction);
        }

        /// <summary>
        /// Deploy license file
        /// </summary>
        /// <param name="buffer"></param>
        public static void DeployLicenseFile(byte[] buffer)
        {
            var versions = new[] { "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021" };
            var programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var revitAddinsPath = Path.Combine(programDataPath, @"Autodesk", @"Revit", @"Addins");
            if (Directory.Exists(revitAddinsPath) == false) return;
            foreach (var version in versions)
            {
                var path = Path.Combine(revitAddinsPath, version, PLUGIN_FOLDER_NAME);
                if (Directory.Exists(path))
                {
                    var licFilePath = Path.Combine(path, LicenseSessionX.LICENSE_FILENAME);
                    File.WriteAllBytes(licFilePath, buffer);
                }
            }
        }


        public static OemInfo GetOemInfo(string homePath)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var oem = new OemInfo();
            oem.Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ??
                            @"Copyright © BimAngle 2017-2020";
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
    }
}
