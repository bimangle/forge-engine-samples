using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Formats.Svf.Dwg;
using Bimangle.ForgeEngine.Dwg.CLI.Config;
using Bimangle.ForgeEngine.Dwg.CLI.Core;
using Bimangle.ForgeEngine.Dwg.CLI.Utility;
using CommandLine;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Dwg.CLI.UI
{
    partial class FormExport : Form
    {
        private readonly AppConfig _Config;
        private readonly AppConfigSvf _LocalConfig;

        private readonly Options _Options;
        private readonly List<FeatureInfo> _Features;
        private bool _IsInit;
#pragma warning disable 414
        private bool _IsClosing;
#pragma warning restore 414

        public FormExport(AppConfig config, Options options)
            : this()
        {
            _Options = options ?? new Options();
            _Options.WinFormMode = false;

            _Config = config;
            _LocalConfig = _Config.Svf;

            if (_Options.Features == null || !_Options.Features.Any())
            {
                _Options.Features = new[]
                {
                    FeatureType.ExportMode2D.ToString(),
                    FeatureType.ExportMode3D.ToString(),
                    FeatureType.IncludeLayouts.ToString(),
                    FeatureType.GenerateThumbnail.ToString(),
                    FeatureType.GenerateModelsDb.ToString()
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
                new FeatureInfo(FeatureType.GenerateLeaflet, Strings.FeatureNameGenerateLeaflet, Strings.FeatureDescriptionGenerateLeaflet),
                new FeatureInfo(FeatureType.UseDefaultViewport, Strings.FeatureNameUseDefaultViewport, Strings.FeatureDescriptionUseDefaultViewport),
                new FeatureInfo(FeatureType.IncludeUnplottableLayers, Strings.FeatureNameIncludeUnplottableLayers, Strings.FeatureDescriptionIncludeUnplottableLayers)
            };
        }

        public FormExport()
        {
            InitializeComponent();
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Text = $@"{PackageInfo.ProductName} - Samples v{PackageInfo.ProductVersion}";


                FormHelper
                    .ToArray(txtInputFile, txtOutputFolder,
                        rbMode2D, rbMode3D, rbModeAll,
                        cbIncludeInvisibleLayers, cbIncludeUnplottableLayers, cbIncludeLayouts,
                        cbGenerateThumbnail, cbGeneratePropDbSqlite, cbGenerateLeaflet,
                        cbUseDefaultViewport)
                    .AddEventListener(RefreshCommand);

                txtInputFile.Text = _Options.InputFilePath ?? string.Empty;
                txtOutputFolder.Text = _Options.OutputFolderPath ?? string.Empty;

                InitUI();

                _IsInit = true;

                RefreshOutputCommand();
            }
        }

        private void FormAppXp_Shown(object sender, EventArgs e)
        {
        }

        private void FormExportSvfzip_FormClosing(object sender, FormClosingEventArgs e)
        {
            _IsClosing = true;
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

            var config = _Options;
            if (config.Features != null && config.Features.Any())
            {
                foreach (var featureTypeString in config.Features)
                {
                    if (Enum.TryParse<FeatureType>(featureTypeString, true, out var featureType))
                    {
                        _Features.FirstOrDefault(x => x.Type == featureType)?.ChangeSelected(_Features, true);
                    }
                }
            }
            else
            {
                SetAllowFeature(FeatureType.ExportMode2D);
                SetAllowFeature(FeatureType.ExportMode3D);
                SetAllowFeature(FeatureType.IncludeLayouts);
                SetAllowFeature(FeatureType.GenerateModelsDb);
                SetAllowFeature(FeatureType.GenerateThumbnail);
                SetAllowFeature(FeatureType.UseDefaultViewport);
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
                toolTip1.SetToolTip(cbIncludeUnplottableLayers, Strings.FeatureDescriptionIncludeUnplottableLayers);
                toolTip1.SetToolTip(cbIncludeLayouts, Strings.FeatureDescriptionIncludeLayouts);

                cbIncludeInvisibleLayers.Checked = IsAllowFeature(FeatureType.IncludeInvisibleLayers);
                cbIncludeUnplottableLayers.Checked = IsAllowFeature(FeatureType.IncludeUnplottableLayers);
                cbIncludeLayouts.Checked = IsAllowFeature(FeatureType.IncludeLayouts);
            }
            #endregion

            #region 生成
            {
                toolTip1.SetToolTip(cbGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail);
                toolTip1.SetToolTip(cbGeneratePropDbSqlite, Strings.FeatureDescriptionGenerateModelsDb);
                toolTip1.SetToolTip(cbGenerateLeaflet, Strings.FeatureDescriptionGenerateLeaflet);

                if (IsAllowFeature(FeatureType.GenerateThumbnail))
                {
                    cbGenerateThumbnail.Checked = true;
                }

                if (IsAllowFeature(FeatureType.GenerateModelsDb))
                {
                    cbGeneratePropDbSqlite.Checked = true;
                }

                if (IsAllowFeature(FeatureType.GenerateLeaflet))
                {
                    cbGenerateLeaflet.Checked = true;
                }
            }
            #endregion

            #region 其它
            {
                toolTip1.SetToolTip(cbUseDefaultViewport, Strings.FeatureDescriptionUseDefaultViewport);

                cbUseDefaultViewport.Checked = IsAllowFeature(FeatureType.UseDefaultViewport);
            }
            #endregion
        }

        private void ResetOptions()
        {
            txtInputFile.Text = string.Empty;
            txtOutputFolder.Text = string.Empty;

            rbModeAll.Checked = true;

            cbIncludeInvisibleLayers.Checked = false;
            cbIncludeUnplottableLayers.Checked = false;
            cbIncludeLayouts.Checked = true;

            cbGenerateThumbnail.Checked = true;
            cbGeneratePropDbSqlite.Checked = true;
            cbGenerateLeaflet.Checked = false;

            cbUseDefaultViewport.Checked = true;
        }

        private void btnBrowseInputFile_Click(object sender, EventArgs e)
        {
            dlgSelectFile.FileName = txtInputFile.Text;

            if (dlgSelectFile.ShowDialog(this) == DialogResult.OK)
            {
                txtInputFile.Text = dlgSelectFile.FileName;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var arguments = GeneralCommandArguments();
            if (arguments == null) return;

            using (var session = LicenseConfig.Create())
            {
                if (session.IsValid == false)
                {
                    LicenseConfig.ShowDialog(session, this);
                    return;
                }
            }

            var homePath = App.GetHomePath();
            if (App.CheckHomeFolder(homePath) == false &&
                ShowConfirm(Strings.HomeFolderIsInvalid) == false)
            {
                return;
            }

            try
            {
                var executePath = Assembly.GetExecutingAssembly().Location;
                Process.Start(executePath, arguments);
            }
            catch (Win32Exception)
            {
                var message = StringsUI.CommandCallIsBlocked;
                MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RefreshCommand()
        {
            if (!_IsInit) return;

            RefreshOutputCommand();
        }

        private void RefreshOutputCommand()
        {
            var arguments = GeneralCommandArguments();
            if (arguments == null)
            {
                txtOutput.Text = string.Empty;
                btnRun.Enabled = false;
            }
            else
            {
                var executePath = Assembly.GetExecutingAssembly().Location;
                if (executePath.Contains(' ')) executePath = '"' + executePath + '"';

                txtOutput.Text = executePath + @" " + arguments;
                btnRun.Enabled = true;
            }
        }

        private string GeneralCommandArguments()
        {
            if (!_IsInit) return null;

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
            SetFeature(FeatureType.IncludeUnplottableLayers, cbIncludeUnplottableLayers.Checked);
            SetFeature(FeatureType.IncludeLayouts, cbIncludeLayouts.Checked);

            SetFeature(FeatureType.GenerateThumbnail, cbGenerateThumbnail.Checked);
            SetFeature(FeatureType.GenerateModelsDb, cbGeneratePropDbSqlite.Checked);
            SetFeature(FeatureType.GenerateLeaflet, cbGenerateLeaflet.Checked);

            SetFeature(FeatureType.UseDefaultViewport, cbUseDefaultViewport.Checked);

            #endregion

            _Options.InputFilePath = txtInputFile.Text.Trim();
            _Options.OutputFolderPath = txtOutputFolder.Text.Trim();
            _Options.Features = _Features.Where(x => x.Selected).Select(x => x.Type.ToString()).ToList();

            if (string.IsNullOrWhiteSpace(_Options.InputFilePath) ||
                string.IsNullOrWhiteSpace(_Options.OutputFolderPath))
            {
                return null;
            }

            #region 保存设置

            var config = _LocalConfig;
            config.Features = _Features.Where(x => x.Selected).Select(x => x.Type).ToList();
            config.InputFilePath = _Options.InputFilePath;
            config.LastTargetPath = _Options.OutputFolderPath;
            _Config.Save();

            #endregion

            return Parser.Default.FormatCommandLine(_Options); ;
        }

        private void ShowMessage(string s)
        {
            MessageBox.Show(this, s, Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private bool ShowConfirm(string s)
        {
            var r = MessageBox.Show(this, s, Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            return r == DialogResult.OK;
        }

        private void txtInputFile_TextChanged(object sender, EventArgs e)
        {
            var s = txtInputFile.Text.Trim();
            if (string.IsNullOrWhiteSpace(s))
            {
                lblInputFilePrompt.ForeColor = SystemColors.ControlText;
                lblInputFilePrompt.Text = StringsUI.SelectInputFileFirst;
            }
            else
            {
                if (File.Exists(s) == false)
                {
                    lblInputFilePrompt.ForeColor = Color.Red;
                    lblInputFilePrompt.Text = StringsUI.InputFileNotExist;
                }
                else
                {
                    lblInputFilePrompt.ForeColor = Color.Green;
                    lblInputFilePrompt.Text = StringsUI.InputFileValid;
                }
            }
        }

        private void tsmiLicense_Click(object sender, EventArgs e)
        {
            LicenseConfig.ShowDialog(this);
        }

        private void tsmiFontFolder_Click(object sender, EventArgs e)
        {
            try
            {
                var fontFolderPath = Core.App.GetFontFolderPath();
                Process.Start(fontFolderPath);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void tsmiResetOptions_Click(object sender, EventArgs e)
        {
            ResetOptions();
        }
    }
}
