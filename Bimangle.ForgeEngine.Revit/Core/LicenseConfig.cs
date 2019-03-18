using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#if EXPRESS
using LicenseSessionX = Bimangle.ForgeEngine.Revit.Express.LicenseSession;
#else
using LicenseSessionX = Bimangle.ForgeEngine.Revit.Pro.LicenseSession;
#endif

namespace Bimangle.ForgeEngine.Revit.Core
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
            var versions = new[] { "2014", "2015", "2016", "2017", "2018", "2019" };
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
    }
}
