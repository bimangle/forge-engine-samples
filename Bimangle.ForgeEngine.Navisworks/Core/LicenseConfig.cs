using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            var versions = new[] { @"2014", @"2015", @"2016", @"2017", @"2018", @"2019" };
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
    }
}
