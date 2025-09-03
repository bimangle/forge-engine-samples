using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Types;
using Newtonsoft.Json.Linq;

#if EXPRESS
using LicenseSessionX = Bimangle.ForgeEngine.Dwg.Express.LicenseSession;
#else
using LicenseSessionX = Bimangle.ForgeEngine.Dwg.Pro.LicenseSession;
#endif

namespace Bimangle.ForgeEngine.Dwg.Core
{
    static class LicenseConfig
    {
        public const string CLIENT_ID = @"BimAngle";

        public const string PRODUCT_NAME = @"BimAngle Engine Samples";

        public static Action<byte[]> DeployLicenseFileAction = null;

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
    }
}
