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
using Color = System.Drawing.Color;
using Form = System.Windows.Forms.Form;

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

            txtHardwareId.Text = LicenseService.HardwareId;

            RefreshUI();
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
                LicenseService.Activate(@"Open Source", @"Revit Exporter", licenseKey);
                RefreshUI();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_LicenseFile != null)
            {
                PublishLicense(_LicenseFile);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void RefreshUI()
        {
            bool isValid;
            string status;
            License.LicenseManager.Check(out isValid, out status);

            txtStatus.Text = status;
            txtStatus.ForeColor = isValid ? Color.Green : Color.Red;

            btnOK.Enabled = isValid;
        }

        /// <summary>
        /// 部署授权文件
        /// </summary>
        /// <param name="buffer"></param>
        private void PublishLicense(byte[] buffer)
        {
            var licFileName = @"Bimangle.ForgeExporter.lic";
            var versions = new [] {"2014", "2015", "2016", "2017", "2018"};

            var programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var revitAddinsPath = Path.Combine(programDataPath, @"Autodesk", @"Revit", @"Addins");
            if (Directory.Exists(revitAddinsPath) == false) return;

            foreach (var version in versions)
            {
                var path = Path.Combine(revitAddinsPath, version, @"Bimangle.ForgeExporter");
                if (Directory.Exists(path))
                {
                    var licFilePath = Path.Combine(path, licFileName);
                    File.WriteAllBytes(licFilePath, buffer);
                }
            }
        }
    }
}
