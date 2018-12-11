using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Formats.Svf.Dwg;
using Bimangle.ForgeEngine.Dwg.App.Config;
using Bimangle.ForgeEngine.Dwg.App.Core;
using Bimangle.ForgeEngine.Dwg.App.Helpers;
using Bimangle.ForgeEngine.Dwg.App.Utility;

namespace Bimangle.ForgeEngine.Dwg.App.UI
{
    partial class FormAppXp : Form
    {
        private readonly AppConfig _Config;

        private readonly List<FeatureInfo> _Features;
        private bool _IsInit;

        public TimeSpan ExportDuration { get; private set; }

        public FormAppXp(AppConfig appConfig)
            : this()
        {
            _Config = appConfig ?? new AppConfig();

            if (_Config.Local == null)
            {
                _Config.Local = new AppLocalConfig();
            }

            if (_Config.Local.Features == null || _Config.Local.Features.Count == 0)
            {
                _Config.Local.Features = new List<FeatureType>
                {
                    FeatureType.ExportMode2D,
                    FeatureType.ExportMode3D,
                    FeatureType.IncludeInvisibleLayers,
                    FeatureType.IncludeLayouts,
                    FeatureType.GenerateThumbnail,
                    FeatureType.GenerateModelsDb
                };
            }

            _Features = new List<FeatureInfo>
            {
                new FeatureInfo(FeatureType.ExportMode2D, Strings.FeatureNameExportMode2D, Strings.FeatureDescriptionExportMode2D),
                new FeatureInfo(FeatureType.ExportMode3D, Strings.FeatureNameExportMode3D, Strings.FeatureDescriptionExportMode3D),
                new FeatureInfo(FeatureType.IncludeInvisibleLayers, Strings.FeatureNameIncludeInvisibleLayers, Strings.FeatureDescriptionIncludeInvisibleLayers),
                new FeatureInfo(FeatureType.IncludeLayouts, Strings.FeatureNameIncludeLayouts, Strings.FeatureDescriptionIncludeLayouts),
                new FeatureInfo(FeatureType.GenerateModelsDb, Strings.FeatureNameGenerateModelsDb, Strings.FeatureDescriptionGenerateModelsDb),
                new FeatureInfo(FeatureType.GenerateThumbnail, Strings.FeatureNameGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail),
            };
        }

        public FormAppXp()
        {
            InitializeComponent();
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            InitUI();
            _IsInit = true;

            RefreshOuputCommand();
        }

        private void FormAppXp_Shown(object sender, EventArgs e)
        {
        }

        private void FormExportSvfzip_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgSelectFolder.SelectedPath = txtOutputFolder.Text;
            if (dlgSelectFolder.ShowDialog(this) == DialogResult.OK)
            {
                txtOutputFolder.Text = dlgSelectFolder.SelectedPath;
            }
        }

        private void InitUI()
        {
            bool IsAllowFeature(FeatureType feature)
            {
                return _Features.Any(x => x.Type == feature && x.Selected);
            }

            void SetAllowFeature(FeatureType feature)
            {
                var item = _Features.FirstOrDefault(x => x.Type == feature);
                item?.ChangeSelected(_Features, true);
            }

            var config = _Config?.Local ?? new AppLocalConfig();

            txtInputFile.Text = config.InputFilePath ?? string.Empty;
            txtOutputFolder.Text = config.LastTargetPath ?? string.Empty;

            if (config.Features != null && config.Features.Any())
            {
                foreach (var feature in config.Features)
                {
                    _Features.FirstOrDefault(x => x.Type == feature)?.ChangeSelected(_Features, true);
                }
            }
            else
            {
                SetAllowFeature(FeatureType.ExportMode2D);
                SetAllowFeature(FeatureType.IncludeInvisibleLayers);
                SetAllowFeature(FeatureType.GenerateModelsDb);
                SetAllowFeature(FeatureType.GenerateThumbnail);
            }

            #region 模式
            {
                toolTip1.SetToolTip(rbMode2D, Strings.FeatureDescriptionExportMode2D);
                toolTip1.SetToolTip(rbMode3D, Strings.FeatureDescriptionExportMode3D);
                toolTip1.SetToolTip(rbModeAll, Strings.FeatureDescriptionExportModeAll);

                if (IsAllowFeature(FeatureType.ExportMode2D) && 
                    IsAllowFeature(FeatureType.ExportMode3D))
                {
                    rbModeAll.Checked = true;
                }
                else if (IsAllowFeature(FeatureType.ExportMode2D))
                {
                    rbMode2D.Checked = true;
                }
                else if (IsAllowFeature(FeatureType.ExportMode3D))
                {
                    rbMode3D.Checked = true;
                }
                else
                {
                    rbMode2D.Checked = true;
                }
            }
            #endregion

            #region 包括
            {
                toolTip1.SetToolTip(cbIncludeInvisibleLayers, Strings.FeatureDescriptionIncludeInvisibleLayers);
                toolTip1.SetToolTip(cbIncludeLayouts, Strings.FeatureDescriptionIncludeLayouts);

                cbIncludeInvisibleLayers.Checked = IsAllowFeature(FeatureType.IncludeInvisibleLayers);
                cbIncludeLayouts.Checked = IsAllowFeature(FeatureType.IncludeLayouts);
            }
            #endregion

            #region 生成
            {
                toolTip1.SetToolTip(cbGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail);
                toolTip1.SetToolTip(cbGeneratePropDbSqlite, Strings.FeatureDescriptionGenerateModelsDb);

                if (IsAllowFeature(FeatureType.GenerateThumbnail))
                {
                    cbGenerateThumbnail.Checked = true;
                }

                if (IsAllowFeature(FeatureType.GenerateModelsDb))
                {
                    cbGeneratePropDbSqlite.Checked = true;
                }
            }
            #endregion

        }

        private void btnBrowseInputFile_Click(object sender, EventArgs e)
        {
            dlgSelectFile.FileName = txtInputFile.Text;

            if (dlgSelectFile.ShowDialog(this) == DialogResult.OK)
            {
                txtInputFile.Text = dlgSelectFile.FileName;
            }
        }

        private void RefreshOuputCommand()
        {
            btnOK.Enabled = GeneralCommandArguments();
        }

        private bool GeneralCommandArguments()
        {
            if (!_IsInit) return false;

            #region 更新界面选项到 _Features

            void SetFeature(FeatureType featureType, bool selected)
            {
                _Features.FirstOrDefault(x => x.Type == featureType)?.ChangeSelected(_Features, selected);
            }

            if (rbMode2D.Checked)
            {
                SetFeature(FeatureType.ExportMode2D, true);
                SetFeature(FeatureType.ExportMode3D, false);
            }
            else if (rbMode3D.Checked)
            {
                SetFeature(FeatureType.ExportMode2D, false);
                SetFeature(FeatureType.ExportMode3D, true);
            }
            else
            {
                SetFeature(FeatureType.ExportMode2D, true);
                SetFeature(FeatureType.ExportMode3D, true);
            }

            SetFeature(FeatureType.IncludeInvisibleLayers, cbIncludeInvisibleLayers.Checked);
            SetFeature(FeatureType.IncludeLayouts, cbIncludeLayouts.Checked);

            SetFeature(FeatureType.GenerateThumbnail, cbGenerateThumbnail.Checked);
            SetFeature(FeatureType.GenerateModelsDb, cbGeneratePropDbSqlite.Checked);

            #endregion

            _Config.Local.InputFilePath = txtInputFile.Text.Trim();
            _Config.Local.LastTargetPath = txtOutputFolder.Text.Trim();
            _Config.Local.Features = _Features.Where(x => x.Selected && x.Enabled).Select(x => x.Type).ToList();

            if (string.IsNullOrWhiteSpace(_Config.Local.InputFilePath) ||
                string.IsNullOrWhiteSpace(_Config.Local.LastTargetPath))
            {
                return false;
            }

            return true;
        }

        private void ShowMessage(string s)
        {
            MessageBox.Show(this, s, Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void txtInputFile_TextChanged(object sender, EventArgs e)
        {
            var s = txtInputFile.Text.Trim();
            if (string.IsNullOrWhiteSpace(s))
            {
                lblInputFilePrompt.ForeColor = SystemColors.ControlText;
                lblInputFilePrompt.Text = @"请选择或输入源模型文件.";
            }
            else
            {
                if (File.Exists(s) == false)
                {
                    lblInputFilePrompt.ForeColor = Color.Red;
                    lblInputFilePrompt.Text = @"文件不存在!";
                }
                else
                {
                    lblInputFilePrompt.ForeColor = Color.Green;
                    lblInputFilePrompt.Text = @"文件位置有效!";
                }
            }

            RefreshOuputCommand();
        }

        private void txtOutputFolder_TextChanged(object sender, EventArgs e)
        {
            RefreshOuputCommand();
        }

        private void rbMode2D_CheckedChanged(object sender, EventArgs e)
        {
            RefreshOuputCommand();
        }

        private void rbMode3D_CheckedChanged(object sender, EventArgs e)
        {
            RefreshOuputCommand();
        }

        private void rbModeAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshOuputCommand();
        }

        private void cbIncludeInvisibleLayers_CheckedChanged(object sender, EventArgs e)
        {
            RefreshOuputCommand();
        }

        private void cbIncludeLayouts_CheckedChanged(object sender, EventArgs e)
        {
            RefreshOuputCommand();
        }

        private void cbGenerateThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            RefreshOuputCommand();
        }

        private void cbGeneratePropDbSqlite_CheckedChanged(object sender, EventArgs e)
        {
            RefreshOuputCommand();
        }


        private void tsmiFontFolder_Click(object sender, EventArgs e)
        {
            try
            {
                var fontFolderPath = App.GetFontFolderPath();
                Process.Start(fontFolderPath);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            App.ShowLicenseDialog(this);
        }

        private void btnResetOptions_Click(object sender, EventArgs e)
        {
            _Config.Local.Features = new List<FeatureType>
            {
                FeatureType.ExportMode2D,
                FeatureType.ExportMode3D,
                FeatureType.IncludeInvisibleLayers,
                FeatureType.IncludeLayouts,
                FeatureType.GenerateThumbnail,
                FeatureType.GenerateModelsDb
            };

            _IsInit = false;

            InitUI();

            _IsInit = true;

            RefreshOuputCommand();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!GeneralCommandArguments()) return;

            using (var session = App.CreateLicenseSession())
            {
                if (session.IsValid == false)
                {
                    App.ShowLicenseDialog(session, this);
                    return;
                }

                #region 保存设置

                _Config.Save();

                #endregion

                var sw = Stopwatch.StartNew();
                try
                {
                    this.Enabled = false;

                    bool isCanncelled;
                    using (var progress = new ProgressHelper(this, Strings.MessageExporting))
                    {
                        var cancellationToken = progress.GetCancellationToken();
                        StartExport(_Config.Local, progress.GetProgressCallback(), cancellationToken);
                        isCanncelled = cancellationToken.IsCancellationRequested;
                    }

                    sw.Stop();
                    var ts = sw.Elapsed;
                    ExportDuration = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds); //去掉毫秒部分

                    Debug.WriteLine(Strings.MessageOperationSuccessAndElapsedTime, ExportDuration);

                    if (isCanncelled == false)
                    {
                        this.ShowMessageBox(string.Format(Strings.MessageExportSuccess, ExportDuration));
                    }
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
        }

        /// <summary>
        /// 开始导出
        /// </summary>
        /// <param name="localConfig"></param>
        /// <param name="progressCallback"></param>
        /// <param name="cancellationToken"></param>
        private void StartExport(AppLocalConfig localConfig, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            using (var log = new RuntimeLog())
            {
                var config = new ExportConfig();
                config.InputFilePath = localConfig.InputFilePath;
                config.TargetPath = localConfig.LastTargetPath;
                config.DefaultFontName = Properties.Settings.Default.DefaultFontName;
                config.Features = localConfig.Features.ToDictionary(x => x, x => true);
                config.Trace = log.Log;
                config.FontPath = new List<string>
                {
                    App.GetFontFolderPath()
                };

                #region Add Plugin - CreatePropDb
                {
                    var cliPath = Path.Combine(
                        App.GetHomePath(),
                        @"Tools",
                        @"CreatePropDb",
                        @"CreatePropDbCLI.exe");
                    if (File.Exists(cliPath))
                    {
                        config.Addins.Add(new ExportPlugin(
                            FeatureType.GenerateModelsDb,
                            cliPath,
                            new[] { @"-i", config.TargetPath }
                        ));
                    }
                }
                #endregion

                Exporter.ExportToSvf(config, x => progressCallback?.Invoke((int)x), cancellationToken);
            }
        }
    }
}
