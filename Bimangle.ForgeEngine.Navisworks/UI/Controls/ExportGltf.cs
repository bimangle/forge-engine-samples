using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Formats.Gltf.Navisworks;
using Bimangle.ForgeEngine.Common.Formats.Gltf;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Navisworks.Config;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Helpers;
using Bimangle.ForgeEngine.Navisworks.Utility;
using Bimangle.ForgeEngine.Common.Utils;
using Ef = Bimangle.ForgeEngine.Common.Utils.ExtendFeatures;

#if EXPRESS
using ExporterX = Bimangle.ForgeEngine.Navisworks.Express.Gltf.Exporter;
#else
using ExporterX = Bimangle.ForgeEngine.Navisworks.Pro.Gltf.Exporter;
#endif

namespace Bimangle.ForgeEngine.Navisworks.UI.Controls
{
    [Browsable(false)]
    partial class ExportGltf : UserControl, IExportControl
    {
        /// <summary>
        /// Draco
        /// </summary>
        private const int GEOMETRY_COMPRESS_TYPE_DEFAULT = 100;

        private AppConfig _Config;
        private AppConfigGltf _LocalConfig;
        private Features<FeatureType> _Features;

        private List<VisualStyleInfo> _VisualStyles;
        private VisualStyleInfo _VisualStyleDefault;

        private List<ComboItemInfo> _LevelOfDetails;
        private ComboItemInfo _LevelOfDetailDefault;

        public TimeSpan ExportDuration { get; private set; }

        public ExportGltf()
        {
            InitializeComponent();
        }

        string IExportControl.Title => Command.TITLE_GLTF;

        string IExportControl.Icon => @"gltf";

        void IExportControl.Init(AppConfig config)
        {
            _Config = config;
            _LocalConfig = _Config.Gltf;

            _Features = new Features<FeatureType>();

            _VisualStyles = new List<VisualStyleInfo>();
            _VisualStyles.Add(new VisualStyleInfo(@"Colored", Strings.VisualStyleColored + $@"({Strings.TextDefault})", new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, true},
                {FeatureType.Wireframe, false},
                {FeatureType.UseBasicRenderColor, false},
                {FeatureType.Gray, false}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Textured", Strings.VisualStyleTextured, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, false},
                {FeatureType.Wireframe, false},
                {FeatureType.UseBasicRenderColor, true},
                {FeatureType.Gray, false}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Realistic", Strings.VisualStyleRealistic, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, false},
                {FeatureType.Wireframe, false},
                {FeatureType.UseBasicRenderColor, false},
                {FeatureType.Gray, false}
            }));
            _VisualStyleDefault = _VisualStyles.First(x => x.Key == @"Colored");

            _LevelOfDetails = new List<ComboItemInfo>();
            _LevelOfDetails.Add(new ComboItemInfo(-1, Strings.TextAuto));
            for (var i = 0; i <= 8; i++)
            {
                string text;
                switch (i)
                {
                    case 0:
                        text = $@"{i} ({Strings.TextLowest})";
                        break;
                    case 7:
                        text = $@"{i} ({Strings.TextHighest})";
                        break;
                    case 8:
                        text = $@"{i} ({Strings.TextOriginal})";
                        break;
                    default:
                        text = i.ToString();
                        break;
                }

                _LevelOfDetails.Add(new ComboItemInfo(i, text));
            }
            _LevelOfDetailDefault = _LevelOfDetails.Find(x => x.Value == -1);

            cbVisualStyle.Items.Clear();
            cbVisualStyle.Items.AddRange(_VisualStyles.Select(x => (object)x).ToArray());

            cbLevelOfDetail.Items.Clear();
            cbLevelOfDetail.Items.AddRange(_LevelOfDetails.Select(x => (object)x).ToArray());

            cbGeometryCompressTypes.Items.Clear();
            cbGeometryCompressTypes.Items.Add(new ItemValue<int>(@"Draco", 100));
            cbGeometryCompressTypes.Items.Add(new ItemValue<int>(@"Mesh Optimizer", 200));
            cbGeometryCompressTypes.Items.Add(new ItemValue<int>(@"Mesh Quantization", 300));
            cbGeometryCompressTypes.Items.Add(new ItemValue<int>(@"Web3D Quantized", 400));
            cbGeometryCompressTypes.Left = cbEnableGeometryCompress.Left + cbEnableGeometryCompress.Width;
            cbGeometryCompressTypes.Enabled = cbEnableGeometryCompress.Checked & cbEnableGeometryCompress.Enabled;

            cbTextureCompressTypes.Items.Clear();
            cbTextureCompressTypes.Items.Add(new ItemValue<int>(@"KTX2", 0));
            cbTextureCompressTypes.Items.Add(new ItemValue<int>(@"WebP", 1));
            cbTextureCompressTypes.Left = cbEnableTextureCompress.Left + cbEnableTextureCompress.Width;
            cbTextureCompressTypes.Enabled = cbEnableTextureCompress.Checked & cbEnableTextureCompress.Enabled;

            cbEnableTextureCompress.CheckedChanged += (sender, e) =>
            {
                cbTextureCompressTypes.Enabled = cbEnableTextureCompress.Checked & cbEnableTextureCompress.Enabled;
            };
            cbEnableTextureCompress.EnabledChanged += (sender, e) =>
            {
                cbTextureCompressTypes.Enabled = cbEnableTextureCompress.Checked & cbEnableTextureCompress.Enabled;
            };

            cbEnableGeometryCompress.CheckedChanged += (sender, e) =>
            {
                cbGeometryCompressTypes.Enabled = cbEnableGeometryCompress.Checked & cbEnableGeometryCompress.Enabled;

                if (cbGeometryCompressTypes.Enabled &&
                    cbGeometryCompressTypes.SelectedItem == null)
                {
                    cbGeometryCompressTypes.SetSelectedValue(GEOMETRY_COMPRESS_TYPE_DEFAULT);
                }
            };
            cbEnableGeometryCompress.EnabledChanged += (sender, e) =>
            {
                cbGeometryCompressTypes.Enabled = cbEnableGeometryCompress.Checked & cbEnableGeometryCompress.Enabled;
            };
        }

        bool IExportControl.Run(IExportForm form)
        {
            var filePath = txtTargetPath.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                ShowMessageBox(Strings.MessageSelectOutputPathFirst);
                return false;
            }

            if (Autodesk.Navisworks.Api.Application.ActiveDocument.Models.Count == 0)
            {
                ShowMessageBox(Strings.SceneIsEmpty);
                return false;
            }
            if (File.Exists(filePath) && ShowConfirmBox(Strings.OutputFileExistedWarning) == false)
            {
                return false;
            }

            var homePath = VersionInfo.GetHomePath();
            if (App.CheckHomeFolder(homePath) == false &&
                ShowConfirmBox(Strings.HomeFolderIsInvalid) == false)
            {
                return false;
            }

            //重置 Features 所有特性为 false
            _Features.Clear();

            var visualStyle = cbVisualStyle.SelectedItem as VisualStyleInfo;
            _Features.Apply(visualStyle?.Features);

            var levelOfDetail = (cbLevelOfDetail.SelectedItem as ComboItemInfo) ?? _LevelOfDetailDefault;

            // var autoOpenAppItem = cbAppList.SelectedItem as IconComboBoxItem;

            #region 更新界面选项到 _Features

            _Features.Set(FeatureType.ExcludeLines, cbExcludeLines.Checked);
            _Features.Set(FeatureType.ExcludePoints, cbExcludeModelPoints.Checked);
            _Features.Set(FeatureType.OnlySelected, cbExcludeUnselectedElements.Checked);

            _Features.Set(FeatureType.UseGoogleDraco, false);
            if (cbEnableGeometryCompress.Checked)
            {
                var geometryGeometryType = cbGeometryCompressTypes.GetSelectedValue<int>();
                switch (geometryGeometryType)
                {
                    case 100:
                        _Features.Set(FeatureType.UseGoogleDraco, true);
                        break;
                    case 200:
                        _Features.Set(FeatureType.EnableMeshOptCompression, true);
                        break;
                    case 300:
                        _Features.Set(FeatureType.EnableMeshQuantized, true);
                        break;
                    case 400:
                        _Features.Set(FeatureType.EnableQuantizedAttributes, true);
                        break;
                    default:
                        throw new NotSupportedException($@"GeometryCompressType: {geometryGeometryType}");
                }
            }

            _Features.Set(FeatureType.ExtractShell, cbUseExtractShell.Checked);
            _Features.Set(FeatureType.GenerateModelsDb, cbGeneratePropDbSqlite.Checked);
            _Features.Set(FeatureType.ExportSvfzip, cbExportSvfzip.Checked);
            _Features.Set(FeatureType.GenerateThumbnail, cbGenerateThumbnail.Checked);
            _Features.Set(FeatureType.EnableAutomaticSplit, cbEnableAutomaticSplit.Checked);
            _Features.Set(FeatureType.AllowRegroupNodes, cbAllowRegroupNodes.Checked);

            _Features.Set(FeatureType.EnableTextureWebP, false);
            _Features.Set(FeatureType.EnableTextureKtx2, false);
            if (cbEnableTextureCompress.Checked)
            {
                var textureCompressType = cbTextureCompressTypes.GetSelectedValue<int>() == 1
                    ? FeatureType.EnableTextureWebP
                    : FeatureType.EnableTextureKtx2;
                _Features.Set(textureCompressType, true);
            }

            #endregion

            var isCancelled = false;
            using (var session = LicenseConfig.Create())
            {
                if (session.IsValid == false)
                {
                    LicenseConfig.ShowDialog(session, ParentForm);
                    return false;
                }

                #region 保存设置

                var config = _LocalConfig;
                config.Features = _Features.GetEnabledFeatures().ToList();
                config.LastTargetPath = txtTargetPath.Text;
                config.VisualStyle = visualStyle?.Key;
                config.LevelOfDetail = levelOfDetail?.Value ?? -1;
                config.LevelOfDetailText = levelOfDetail?.ToString();
                _Config.Save();

                #endregion

                var sw = Stopwatch.StartNew();
                try
                {
                    var setting = new ExportSetting();
                    setting.LevelOfDetail = config.LevelOfDetail;
                    setting.OutputPath = config.LastTargetPath;
                    setting.Features = _Features.GetEnabledFeatures().ToList();
                    setting.Site = SiteInfo.CreateDefault();
                    setting.Oem = App.GetOemInfo(homePath);

                    //应用扩展特性
                    ApplyExtendFeatures(setting, form);

                    using (var progress = new ProgressExHelper(ParentForm, Strings.MessageExporting))
                    {
                        var cancellationToken = progress.GetCancellationToken();
                        StartExport(setting, progress.GetProgressCallback(), cancellationToken);
                        isCancelled = cancellationToken.IsCancellationRequested;
                    }

                    sw.Stop();
                    var ts = sw.Elapsed;
                    ExportDuration = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds); //去掉毫秒部分

                    Debug.WriteLine(Strings.MessageOperationSuccessAndElapsedTime, ExportDuration);

                    if (isCancelled == false)
                    {
                        if (config.AutoOpenAllow && config.AutoOpenAppName != null)
                        {
                            Process.Start(config.AutoOpenAppName, config.LastTargetPath);
                        }
                        else
                        {
                            ShowMessageBox(string.Format(Strings.MessageExportSuccess, ExportDuration));
                        }
                    }
                }
                catch (IOException ex)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    ShowMessageBox(string.Format(Strings.MessageFileSaveFailure, ex.Message));
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    ShowMessageBox(ex.ToString());
                }
            }

            return isCancelled == false;
        }

        void IExportControl.Reset()
        {
            cbVisualStyle.SelectedItem = _VisualStyleDefault;
            cbLevelOfDetail.SelectedItem = _LevelOfDetailDefault;

            cbExcludeLines.Checked = false;
            cbExcludeModelPoints.Checked = false;
            cbExcludeUnselectedElements.Checked = false;

            cbUseExtractShell.Checked = false;
            cbGeneratePropDbSqlite.Checked = true;
            cbExportSvfzip.Checked = false;

            cbEnableGeometryCompress.Checked = true;
            cbGeometryCompressTypes.SetSelectedValue(GEOMETRY_COMPRESS_TYPE_DEFAULT);    //Default: Draco
            cbEnableTextureCompress.Checked = true;
            cbTextureCompressTypes.SetSelectedValue(0);

            cbGenerateThumbnail.Checked = false;
            cbEnableAutomaticSplit.Checked = false;
            cbAllowRegroupNodes.Checked = true;
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitUI();
                txtTargetPath.EnableFilePathDrop(@"model.gltf");
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var filePath = txtTargetPath.Text;

            {
                var dialog = saveFileDialog1;

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

        private void cbVisualStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var visualStyle = cbVisualStyle.SelectedItem as VisualStyleInfo;
            if (visualStyle == null) return;

            _Features.Apply(visualStyle.Features);

            var excludeTexture = _Features.IsEnabled(FeatureType.ExcludeTexture);
            cbEnableTextureCompress.Enabled = !excludeTexture;
        }

        /// <summary>
        /// 开始导出
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="progressCallback"></param>
        /// <param name="cancellationToken"></param>
        private void StartExport(ExportSetting setting,  Action<int> progressCallback, CancellationToken cancellationToken)
        {
            using(var log = new RuntimeLog())
            {
                var exporter = new ExporterX(VersionInfo.GetHomePath());
                exporter.Export(setting, log, progressCallback, cancellationToken);
            }
        }

        private void InitUI()
        {
            var config = _LocalConfig;
            _Features.Init(config.Features);

            txtTargetPath.Text = config.LastTargetPath;

            #region 基本
            {
                //视觉样式
                var visualStyle = _VisualStyles.FirstOrDefault(x => x.Key == config.VisualStyle) ??
                                  _VisualStyleDefault;
                _Features.Apply(visualStyle.Features);
                cbVisualStyle.SelectedItem = visualStyle;

                //详细程度
                if (string.IsNullOrEmpty(config.LevelOfDetailText)) config.LevelOfDetail = -1;
                var levelOfDetail = _LevelOfDetails.FirstOrDefault(x => x.Value == config.LevelOfDetail) ??
                                    _LevelOfDetailDefault;
                cbLevelOfDetail.SelectedItem = levelOfDetail;
            }
            #endregion

            #region 排除
            {
                toolTip1.SetToolTip(cbExcludeLines, Strings.FeatureDescriptionExcludeLines);
                toolTip1.SetToolTip(cbExcludeModelPoints, Strings.FeatureDescriptionExcludePoints);
                toolTip1.SetToolTip(cbExcludeUnselectedElements, Strings.FeatureDescriptionOnlySelected);

                if (_Features.IsEnabled(FeatureType.ExcludeLines))
                {
                    cbExcludeLines.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ExcludePoints))
                {
                    cbExcludeModelPoints.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.OnlySelected))
                {
                    cbExcludeUnselectedElements.Checked = true;
                }
            }
            #endregion

            #region 高级
            {
                toolTip1.SetToolTip(cbUseExtractShell, Strings.FeatureDescriptionExtractShell);
                toolTip1.SetToolTip(cbGeneratePropDbSqlite, Strings.FeatureDescriptionGenerateModelsDb);
                toolTip1.SetToolTip(cbExportSvfzip, Strings.FeatureDescriptionExportSvfzip);
                toolTip1.SetToolTip(cbGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail);
                toolTip1.SetToolTip(cbEnableAutomaticSplit, Strings.FeatureDescriptionEnableAutomaticSplit);
                toolTip1.SetToolTip(cbAllowRegroupNodes, Strings.FeatureDescriptionAllowRegroupNodes);

                if (_Features.IsEnabled(FeatureType.UseGoogleDraco))
                {
                    cbGeometryCompressTypes.SetSelectedValue(100);
                    cbEnableGeometryCompress.Checked = true;
                }
                else if (_Features.IsEnabled(FeatureType.EnableMeshOptCompression))
                {
                    cbGeometryCompressTypes.SetSelectedValue(200);
                    cbEnableGeometryCompress.Checked = true;
                }
                else if (_Features.IsEnabled(FeatureType.EnableMeshQuantized))
                {
                    cbGeometryCompressTypes.SetSelectedValue(300);
                    cbEnableGeometryCompress.Checked = true;
                }
                else if (_Features.IsEnabled(FeatureType.EnableQuantizedAttributes))
                {
                    cbGeometryCompressTypes.SetSelectedValue(400);
                    cbEnableGeometryCompress.Checked = true;
                }
                else
                {
                    cbGeometryCompressTypes.SetSelectedValue(GEOMETRY_COMPRESS_TYPE_DEFAULT);
                    cbEnableGeometryCompress.Checked = false;
                }

                if (_Features.IsEnabled(FeatureType.EnableTextureWebP))
                {
                    cbEnableTextureCompress.Checked = true;
                    cbTextureCompressTypes.SetSelectedValue(1);
                }
                else if (_Features.IsEnabled(FeatureType.EnableTextureKtx2))
                {
                    cbEnableTextureCompress.Checked = true;
                    cbTextureCompressTypes.SetSelectedValue(0);
                }
                else
                {
                    cbEnableTextureCompress.Checked = false;
                    cbTextureCompressTypes.SetSelectedValue(0);
                }

                if (_Features.IsEnabled(FeatureType.ExtractShell))
                {
                    cbUseExtractShell.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.GenerateModelsDb))
                {
                    cbGeneratePropDbSqlite.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ExportSvfzip))
                {
                    cbExportSvfzip.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.GenerateThumbnail))
                {
                    cbGenerateThumbnail.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.EnableAutomaticSplit))
                {
                    cbEnableAutomaticSplit.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.AllowRegroupNodes))
                {
                    cbAllowRegroupNodes.Checked = true;
                }
            }
            #endregion

#if EXPRESS
            cbExportSvfzip.Enabled = false;
            cbExportSvfzip.Checked = false;
#else
            cbExportSvfzip.Enabled = true;
#endif

        }

        private class VisualStyleInfo
        {
            public string Key { get; }

            private string Text { get; }

            public Dictionary<FeatureType, bool> Features { get; }

            public VisualStyleInfo(string key, string text, Dictionary<FeatureType, bool> features)
            {
                Key = key;
                Text = text;
                Features = features;
            }

            #region Overrides of Object

            public override string ToString()
            {
                return Text;
            }

            #endregion
        }


        private class ComboItemInfo
        {
            public int Value { get; }

            private string Text { get; }

            public ComboItemInfo(int value, string text)
            {
                Value = value;
                Text = text;
            }

            #region Overrides of Object

            public override string ToString()
            {
                return Text;
            }

            #endregion
        }

        private void ShowMessageBox(string message)
        {
            ParentForm.ShowMessageBox(message);
        }

        private bool ShowConfirmBox(string message)
        {
            return MessageBox.Show(ParentForm, message, ParentForm?.Text,
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button2) == DialogResult.OK;
        }

        /// <summary>
        /// 应用扩展属性
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="form"></param>
        private void ApplyExtendFeatures(ExportSetting setting, IExportForm form)
        {
            if (setting.PreExportSeedFeatures == null) setting.PreExportSeedFeatures = new List<string>();

            if (form.UsedExtendFeature(Ef.RenderingPerformancePreferred))
            {
                setting.PreExportSeedFeatures.Add(Ef.RenderingPerformancePreferred);
            }

            if (form.UsedExtendFeature(Ef.DisableMeshSimplifier))
            {
                setting.PreExportSeedFeatures.Add(Ef.DisableMeshSimplifier);
            }
        }
    }
}
