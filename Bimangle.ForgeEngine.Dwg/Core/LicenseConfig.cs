using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Dwg.App.Core
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
    }
}
