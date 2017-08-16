using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Revit.Config;

namespace Bimangle.ForgeEngine.Revit.License
{
    static class LicenseManager
    {
        public static bool IsValid { get; private set; }
        public static string Message { get; private set; }

        public static bool Start()
        {
            End();

            bool isValid;
            string status;
            Check(out isValid, out status);

            IsValid = isValid;
            Message = IsValid ? null : Strings.MessageLicenseInvalid;

            return IsValid;
        }

        public static void End()
        {
            LicenseService.Deactivate();
        }

        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="status"></param>
        public static void Check(out bool isValid, out string status)
        {
            CheckLicense(out isValid, out status);
        }

        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="status"></param>
        private static void CheckLicense(out bool isValid, out string status)
        {
            LicenseService.Activate(@"Open Source", @"Revit Exporter");

            isValid = LicenseService.IsActivated;

            var sb = new StringBuilder();
            sb.AppendLine($@"IsActivated:       {LicenseService.IsActivated}");
            sb.AppendLine($@"LicenseStatus:     {LicenseService.LicenseStatus}");
            sb.AppendLine($@"LicenseExpiration: {LicenseService.LicenseExpiration:yyyy-MM-dd}");
            sb.AppendLine($@"ClientName:   {LicenseService.ClientName}");
            sb.AppendLine($@"ReleaseDate:  {PackageInfo.ReleaseDate:yyyy-MM-dd}");
            sb.AppendLine($@"Subscription: {LicenseService.SubscriptionExpiration:yyyy-MM-dd}");
            status = sb.ToString();
        }
    }
}
