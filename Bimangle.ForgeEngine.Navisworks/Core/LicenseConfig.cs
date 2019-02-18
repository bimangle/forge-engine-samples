using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Navisworks.Core
{
    static class LicenseConfig
    {

#if DEBUG
        public const string LICENSE_KEY = null;
#else
        public const string LICENSE_KEY = null;
#endif

        public const string CLIENT_ID = @"BimAngle";

        public const string PRODUCT_NAME = @"Forge Engine Samples";

        public const string PLUGIN_FOLDER_NAME = @"Bimangle.ForgeEngine.Samples";

        public static Action<byte[]> DeployLicenseFileAction = DeployLicenseFile;

        public static LicenseSession Create()
        {
            LicenseSession.Init();
            return new LicenseSession(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);
        }

        public static void ShowDialog(LicenseSession session, IWin32Window parent)
        {
            //var info = LicenseSession.GetLicenseInfo(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);

            LicenseSession.ShowLicenseDialog(session.ClientId, session.AppName, parent, DeployLicenseFileAction);
        }

        public static void ShowDialog(IWin32Window parent)
        {
            //var info = LicenseSession.GetLicenseInfo(CLIENT_ID, PRODUCT_NAME, LICENSE_KEY);

            LicenseSession.ShowLicenseDialog(CLIENT_ID, PRODUCT_NAME, parent, DeployLicenseFileAction);
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

                var licFilePath = Path.Combine(addinsPath, LicenseSession.LICENSE_FILENAME);
                File.WriteAllBytes(licFilePath, buffer);
            }
        }
    }
}
