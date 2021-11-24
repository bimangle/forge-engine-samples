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

#if EXPRESS
using LicenseSessionX = Bimangle.ForgeEngine.Navisworks.Express.LicenseSession;
#else
using LicenseSessionX = Bimangle.ForgeEngine.Navisworks.Pro.LicenseSession;
#endif

namespace Bimangle.ForgeEngine.Navisworks.Core
{
    class LicenseConfig
    {
        public const string CLIENT_ID = VersionInfo.COMPANY_ID;

        public const string APPLICATION_NAME = VersionInfo.TITLE;

        static LicenseConfig()
        {
            Init();
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
        public static LicenseSessionX Create(string licenseKey = null)
        {
            //从自定义授权文件加载授权
            licenseKey = GetLicenseKey(licenseKey);

            return new LicenseSessionX(CLIENT_ID, APPLICATION_NAME, licenseKey);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowDialog(LicenseSessionX session, IWin32Window parent)
        {
            LicenseSessionX.ShowLicenseDialog(session.ClientId, session.AppName, parent, DeployLicenseFile);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowDialog(IWin32Window parent)
        {
            //从自定义授权文件加载授权
            var licenseKey = GetLicenseKey(null);
            LicenseSessionX.GetLicenseInfo(CLIENT_ID, APPLICATION_NAME, licenseKey);

            LicenseSessionX.ShowLicenseDialog(CLIENT_ID, APPLICATION_NAME, parent, DeployLicenseFile);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool ShowTrialDialog(LicenseSessionX session, IWin32Window parent)
        {
            return LicenseSessionX.ShowTrialDialog(session, parent);
        }

        /// <summary>
        /// 部署授权文件
        /// </summary>
        /// <param name="buffer"></param>
        private static void DeployLicenseFile(byte[] buffer)
        {
            var licFileName = GetLicenseFileName();

            //首先尝试写入 HomeFolder
            var homeFolder = VersionInfo.GetHomePath();
            if (Directory.Exists(homeFolder))
            {
                var licFilePath = Path.Combine(homeFolder, licFileName);

                try
                {
                    File.WriteAllBytes(licFilePath, buffer);
                    return;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }

            //如果无法写入 HomeFolder, 则写入当前文件夹
            {
                var dllFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var licFilePath = Path.Combine(dllFolder, licFileName);
                File.WriteAllBytes(licFilePath, buffer);
            }
        }

        //加载授权文件
        private static string GetLicenseKey(string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
            {
                var licFileName = GetLicenseFileName();

                //首先尝试从 Home Folder 读取
                {
                    var homeFolder = VersionInfo.GetHomePath();
                    var licFilePath = Path.Combine(homeFolder, licFileName);
                    if (File.Exists(licFilePath))
                    {
                        licenseKey = LicenseSessionX.LoadLicenseKeyFromFile(licFilePath);
                        return licenseKey;
                    }
                }

                //尝试从当前文件夹读取
                {
                    var dllFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var licFilePath = Path.Combine(dllFolder, licFileName);
                    if (File.Exists(licFilePath))
                    {
                        licenseKey = LicenseSessionX.LoadLicenseKeyFromFile(licFilePath);
                        return licenseKey;
                    }
                }
            }

            return licenseKey;
        }

        /// <summary>
        /// 获得授权文件名
        /// </summary>
        /// <returns></returns>
        private static string GetLicenseFileName()
        {
            return $@"{VersionInfo.PRODUCT_ID}_{LicenseSessionX.GetHardwareId()}.lic";
        }
    }
}
