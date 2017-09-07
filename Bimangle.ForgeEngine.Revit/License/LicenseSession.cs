using System;

using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Revit.UI;
using Bimangle.ForgeEngine.Revit.Utility;

namespace Bimangle.ForgeEngine.Revit.License
{
    /// <summary>
    /// 授权会话
    /// </summary>
    class LicenseSession : IDisposable
    {
        /// <summary>
        /// 打开授权信息窗口
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static bool ShowLicenseDialog(IWin32Window owner = null)
        {
            var form = new FormLicense();
            return form.ShowDialog(owner) == DialogResult.OK;
        }

        /// <summary>
        /// 部署授权文件
        /// </summary>
        /// <param name="buffer"></param>
        public static void DeployLicenseFile(byte[] buffer)
        {
            const string LIC_FILE_NAME = @"Bimangle.ForgeEngine.lic";
            var licFilePath = AppHelper.GetPath(LIC_FILE_NAME);
            File.WriteAllBytes(licFilePath, buffer);
        }

        public bool IsValid { get;}

        public LicenseSession(string licenseKey = null)
        {
            LicenseService.Activate(@"BimAngle", @"ForgeEngine Plugin", licenseKey);
            IsValid = LicenseService.IsActivated;
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            LicenseService.Deactivate();
        }

        #endregion
    }
}
