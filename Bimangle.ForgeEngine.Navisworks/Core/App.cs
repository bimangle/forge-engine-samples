using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Navisworks.Core
{
    static class App
    {
        public const string CLIENT_ID = @"BimAngle";
        public const string PRODUCT_NAME = @"Forge Engine Samples";

        public static LicenseSession CreateLicenseSession(string licenseKey = null)
        {
            return new LicenseSession(CLIENT_ID, PRODUCT_NAME, licenseKey);
        }

        /// <summary>
        /// 获得主路径
        /// </summary>
        /// <returns></returns>
        public static string GetHomePath()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                @"Bimangle",
                @"Bimangle.ForgeEngine.Navisworks");

            Common.Utils.FileSystemUtility.CreateDirectory(path);

            return path;
        }

        public static void ShowLicenseDialog(LicenseSession session, IWin32Window parent)
        {
            LicenseSession.ShowLicenseDialog(session.ClientId, session.AppName, parent);
        }

        public static void ShowLicenseDialog(IWin32Window parent)
        {
            LicenseSession.ShowLicenseDialog(CLIENT_ID, PRODUCT_NAME, parent);
        }
    }
}
