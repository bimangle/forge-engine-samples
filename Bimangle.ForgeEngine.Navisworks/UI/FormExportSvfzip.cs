using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Navisworks.Config;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Helpers;
using Bimangle.ForgeEngine.Navisworks.License;
using Bimangle.ForgeEngine.Navisworks.Utility;
using Newtonsoft.Json.Linq;
using Form = System.Windows.Forms.Form;

namespace Bimangle.ForgeEngine.Navisworks.UI
{
    partial class FormExportSvfzip : Form
    {
        private readonly AppConfig _Config;
        private readonly List<FeatureInfo> _Features;

        public TimeSpan ExportDuration { get; private set; }

        public FormExportSvfzip()
        {
            InitializeComponent();
        }

        public FormExportSvfzip(AppConfig config)
            : this()
        {
            _Config = config;

            _Features = new List<FeatureInfo>
            {
                new FeatureInfo(FeatureType.ExcludeProperties, Strings.FeatureNameExcludeProperties, Strings.FeatureDescriptionExcludeProperties),
                new FeatureInfo(FeatureType.ExcludeLines, Strings.FeatureNameExcludeLines, Strings.FeatureDescriptionExcludeLines),
                new FeatureInfo(FeatureType.ExcludePoints, Strings.FeatureNameExcludePoints, Strings.FeatureDescriptionExcludePoints),
                new FeatureInfo(FeatureType.OnlySelected, Strings.FeatureNameOnlySelected, Strings.FeatureDescriptionOnlySelected),
                new FeatureInfo(FeatureType.GenerateElementData, Strings.FeatureNameGenerateElementData, Strings.FeatureDescriptionGenerateElementData),
                //new FeatureInfo(FeatureType.Wireframe, Strings.FeatureNameWireframe, Strings.FeatureDescriptionWireframe),
            };
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            Icon =Icon.FromHandle(Properties.Resources.Converter_32px_1061192.GetHicon());
            Text += $@" - {Command.TITLE}";

            var config = _Config.Local;
            txtTargetPath.Text = config.LastTargetPath;

            InitFeatureList();
        }

        private void FormExportSvfzip_Shown(object sender, EventArgs e)
        {
        }

        private void FormExportSvfzip_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var filePath = txtTargetPath.Text;

            {
                var dialog = new SaveFileDialog();
                dialog.OverwritePrompt = true;
                dialog.AddExtension = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = @".svfzip";
                dialog.Title = Strings.DialogTitleSelectTarget;
                dialog.Filter = string.Join(@"|", Strings.DialogFilterSvfzip, Strings.DialogFilterSvfzip);

                if (string.IsNullOrEmpty(filePath) == false)
                {
                    dialog.FileName = filePath;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtTargetPath.Text = dialog.FileName;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var filePath = txtTargetPath.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show(Strings.MessageSelectOutputPathFirst, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (ListViewItem item in lvFeatures.Items)
            {
                var feature = item.Tag as FeatureInfo;
                feature?.ChangeSelected(_Features, item.Checked);
            }

            using (var license = new LicenseSession())
            {
                if (license.IsValid == false)
                {
                    LicenseSession.ShowLicenseDialog(this);
                    return;
                }

                #region 保存设置

                var config = _Config.Local;
                config.Features = _Features.Where(x => x.Selected).Select(x => x.Type).ToList();
                config.LastTargetPath = txtTargetPath.Text;
                _Config.Save();

                #endregion

                var sw = Stopwatch.StartNew();
                try
                {
                    this.Enabled = false;

                    var features = _Features.Where(x => x.Selected && x.Enabled).ToDictionary(x => x.Type, x => true);
                    using (new ProgressHelper(this, Strings.MessageExporting))
                    {
                        StartExport(config.LastTargetPath, ExportType.Zip, null, features);
                    }

                    sw.Stop();
                    var ts = sw.Elapsed;
                    ExportDuration = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds); //去掉毫秒部分

                    Debug.WriteLine(Strings.MessageOperationSuccessAndElapsedTime, ExportDuration);

                    if (config.AutoOpenAllow && config.AutoOpenAppName != null)
                    {
                        Process.Start(config.AutoOpenAppName, config.LastTargetPath);
                    }
                    else
                    {
                        this.ShowMessageBox(string.Format(Strings.MessageExportSuccess, ExportDuration));
                    }

                    DialogResult = DialogResult.OK;
                }
                catch (IOException ex)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    this.ShowMessageBox(string.Format(Strings.MessageFileSaveFailure, ex.Message));

                    DialogResult = DialogResult.Cancel;
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    this.ShowMessageBox(ex.ToString());

                    DialogResult = DialogResult.Cancel;
                }
                finally
                {
                    Enabled = true;
                }
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 开始导出
        /// </summary>
        /// <param name="view"></param>
        /// <param name="targetPath"></param>
        /// <param name="exportType"></param>
        /// <param name="outputStream"></param>
        /// <param name="features"></param>
        private void StartExport(string targetPath, ExportType exportType, Stream outputStream, Dictionary<FeatureType, bool> features)
        {
            using(var log = new RuntimeLog())
            {
                var config = new ExportConfig();
                config.TargetPath = targetPath;
                config.ExportType = exportType;
                config.OutputStream = outputStream;
                config.Features = features?.Keys.ToList() ?? new List<FeatureType>();
                config.Trace = log.Log;

                Exporter.ExportToSvf(config);
            }
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            LicenseSession.ShowLicenseDialog(this);
        }

        private void InitFeatureList()
        {
            var config = _Config.Local;
            if (config.Features != null && config.Features.Count > 0)
            {
                foreach (var featureType in config.Features)
                {
                    _Features.FirstOrDefault(x=>x.Type == featureType)?.ChangeSelected(_Features, true);
                }
            }

            lvFeatures.Items.Clear();
            foreach (var feature in _Features)
            {
                var item = lvFeatures.Items.Add(new ListViewItem(new [] {feature.Title, feature.Description}));
                item.Checked = feature.Selected;
                item.Tag = feature;
            }

        }
    }
}
