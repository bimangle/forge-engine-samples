using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.License;

namespace Bimangle.ForgeEngine.Revit.UI
{
    partial class FormLicense : Form
    {
        private byte[] _LicenseFile;

        public FormLicense()
        {
            InitializeComponent();
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            Text += $@" - {Command.TITLE}";

            RefreshLicenseInfo();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckPathExists = true;
            dialog.DefaultExt = ".lic";
            dialog.Title = Strings.DialogTitleLoadLicense;
            dialog.Filter = string.Join(@"|", Strings.DialogFilterLicenseFile, Strings.DialogFilterAllFile);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _LicenseFile = File.ReadAllBytes(dialog.FileName);

                var licenseKey = LicenseService.ConvertLicenseFileToKey(dialog.FileName);
                RefreshLicenseInfo(licenseKey);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_LicenseFile != null)
            {
                LicenseSession.DeployLicenseFile(_LicenseFile);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void RefreshLicenseInfo(string licenseKey = null)
        {
            using (var license = new LicenseSession(licenseKey))
            {
                txtProductName.Text = PackageInfo.ProductName;
                txtProductVersion.Text = PackageInfo.ProductVersion.ToString();
                txtReleaseDate.Text = PackageInfo.ReleaseDate.ToLongDateString();

                txtIsActivated.Text = LicenseService.IsActivated.ToString();
                txtLicenseMode.Text = LicenseService.LicenseMode.ToString();
                txtLicenseStatus.Text = LicenseService.LicenseStatus;
                txtHardwareId.Text = LicenseService.HardwareId;
                txtClientName.Text = LicenseService.ClientName;
                txtExpirationDate.Text = LicenseService.LicenseExpiration.ToLongDateString();
                txtSubscription.Text = LicenseService.SubscriptionExpiration.ToLongDateString();

                txtIsActivated.BackColor = LicenseService.IsActivated ? Color.LightGreen : Color.OrangeRed;
                txtLicenseStatus.BackColor = LicenseService.IsActivated ? Color.LightGreen : Color.OrangeRed;
                txtLicenseMode.BackColor = LicenseService.IsActivated ? Color.LightGreen : Color.OrangeRed;
                txtExpirationDate.BackColor = DateTime.Today <= LicenseService.LicenseExpiration ? SystemColors.Control : Color.OrangeRed;
                txtSubscription.BackColor = PackageInfo.ReleaseDate <= LicenseService.SubscriptionExpiration ? SystemColors.Control : Color.OrangeRed;
                txtReleaseDate.BackColor = PackageInfo.ReleaseDate <= LicenseService.SubscriptionExpiration ? SystemColors.Control : Color.OrangeRed;

                btnOK.Enabled = license.IsValid;
                btnLoadLicense.Enabled = !LicenseService.IsHardwareLock;
            }
        }
    }
}
