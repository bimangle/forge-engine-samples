using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Formats.Cesium3DTiles;
using Bimangle.ForgeEngine.Common.Formats.Cesium3DTiles.Navisworks;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing;
using Bimangle.ForgeEngine.Navisworks.Config;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Helpers;
using Bimangle.ForgeEngine.Navisworks.Utility;

#if EXPRESS
using ExporterX = Bimangle.ForgeEngine.Navisworks.Express.Cesium3DTiles.Exporter;
#else
using ExporterX = Bimangle.ForgeEngine.Navisworks.Pro.Cesium3DTiles.Exporter;
#endif

namespace Bimangle.ForgeEngine.Navisworks.UI.Controls
{
    [Browsable(false)]
    partial class ExportCesium3DTiles : UserControl, IExportControl
    {
        private AppConfig _Config;
        private AppConfigCesium3DTiles _LocalConfig;
        private List<FeatureInfo> _Features;

        private List<VisualStyleInfo> _VisualStyles;
        private VisualStyleInfo _VisualStyleDefault;

        private List<ComboItemInfo> _LevelOfDetails;
        private ComboItemInfo _LevelOfDetailDefault;

        private GeoreferncingHost _GeoreferncingHost;

        public TimeSpan ExportDuration { get; private set; }

        public ExportCesium3DTiles()
        {
            InitializeComponent();

            cbGenerateOutline.CheckedChanged += (sender, e) =>
            {
                if (cbGenerateOutline.Checked)
                {
                    cbEnableQuantizedAttributes.Enabled = false;
                    cbUseDraco.Enabled = false;
                }
                else
                {
                    cbEnableQuantizedAttributes.Enabled = true;
                    cbUseDraco.Enabled = true;
                }
            };
        }

        #region Overrides of Control

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            _GeoreferncingHost?.Dispose();
            _GeoreferncingHost = null;
        }

        #endregion

        string IExportControl.Title => Command.TITLE_3DTILES;

        string IExportControl.Icon => @"3dtiles";

        void IExportControl.Init(AppConfig config)
        {
            _Config = config;
            _LocalConfig = _Config.Cesium3DTiles;

            _GeoreferncingHost = GeoreferncingHost.Create(new GeoreferncingAdapter(_LocalConfig), VersionInfo.GetHomePath());
            _GeoreferncingHost.Preload();

            _Features = new List<FeatureInfo>
            {
                new FeatureInfo(FeatureType.ExcludeTexture, Strings.FeatureNameExcludeTexture, Strings.FeatureDescriptionExcludeTexture, true, false),
                new FeatureInfo(FeatureType.ExcludeLines, Strings.FeatureNameExcludeLines, Strings.FeatureDescriptionExcludeLines),
                new FeatureInfo(FeatureType.ExcludePoints, Strings.FeatureNameExcludePoints, Strings.FeatureDescriptionExcludePoints, true, false),
                new FeatureInfo(FeatureType.OnlySelected, Strings.FeatureNameOnlySelected, Strings.FeatureDescriptionOnlySelected),
                new FeatureInfo(FeatureType.GenerateModelsDb, Strings.FeatureNameGenerateModelsDb, Strings.FeatureDescriptionGenerateModelsDb),
                new FeatureInfo(FeatureType.GenerateThumbnail, Strings.FeatureNameGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail),
                new FeatureInfo(FeatureType.UseGoogleDraco, Strings.FeatureNameUseGoogleDraco, Strings.FeatureDescriptionUseGoogleDraco, true, false),
                new FeatureInfo(FeatureType.ExtractShell, Strings.FeatureNameExtractShell, Strings.FeatureDescriptionExtractShell, true, false),
                new FeatureInfo(FeatureType.ExportSvfzip, Strings.FeatureNameExportSvfzip, Strings.FeatureDescriptionExportSvfzip, true, false),
                new FeatureInfo(FeatureType.EnableQuantizedAttributes, Strings.FeatureNameEnableQuantizedAttributes, Strings.FeatureDescriptionEnableQuantizedAttributes, true, false),
                new FeatureInfo(FeatureType.EnableTextureWebP, Strings.FeatureNameEnableTextureWebP, Strings.FeatureDescriptionEnableTextureWebP, true, false),
                new FeatureInfo(FeatureType.EnableEmbedGeoreferencing, Strings.FeatureNameEnableEmbedGeoreferencing, Strings.FeatureDescriptionEnableEmbedGeoreferencing, true, false),
                new FeatureInfo(FeatureType.EnableUnlitMaterials, Strings.FeatureNameEnableUnlitMaterials, Strings.FeatureDescriptionEnableUnlitMaterials, true, false),
                new FeatureInfo(FeatureType.AutoAlignOriginToSiteCenter, Strings.FeatureNameAutoAlignOriginToSiteCenter, Strings.FeatureDescriptionAutoAlignOriginToSiteCenter, true, false),
                new FeatureInfo(FeatureType.EnableCesiumPrimitiveOutline, Strings.FeatureNameEnableCesiumPrimitiveOutline, Strings.FeatureDescriptionEnableCesiumPrimitiveOutline, true, false),
            };

            _VisualStyles = new List<VisualStyleInfo>();
            _VisualStyles.Add(new VisualStyleInfo(@"Colored", Strings.VisualStyleColored + $@"({Strings.TextDefault})", new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, true}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Textured", Strings.VisualStyleTextured, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, false}
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

            cbContentType.Items.Clear();
            cbContentType.Items.Add(new ItemValue<int>(Strings.ContentTypeBasic, 0));
            cbContentType.Items.Add(new ItemValue<int>(Strings.ContentTypeShellOnlyByElement, 3));
            cbContentType.Items.Add(new ItemValue<int>(Strings.ContentTypeShellOnlyByMesh, 2));
        }

        bool IExportControl.Run()
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

            var visualStyle = cbVisualStyle.SelectedItem as VisualStyleInfo;
            if (visualStyle != null)
            {
                foreach (var p in visualStyle.Features)
                {
                    _Features.FirstOrDefault(x => x.Type == p.Key)?.ChangeSelected(_Features, p.Value);
                }
            }

            var levelOfDetail = (cbLevelOfDetail.SelectedItem as ComboItemInfo) ?? _LevelOfDetailDefault;

            #region 更新界面选项到 _Features

            void SetFeature(FeatureType featureType, bool selected)
            {
                _Features.FirstOrDefault(x => x.Type == featureType)?.ChangeSelected(_Features, selected);
            }

            //SetFeature(FeatureType.ExportGrids, cbIncludeGrids.Checked);

            SetFeature(FeatureType.ExcludeLines, cbExcludeLines.Checked);
            SetFeature(FeatureType.ExcludePoints, cbExcludeModelPoints.Checked);
            SetFeature(FeatureType.OnlySelected, cbExcludeUnselectedElements.Checked);

            SetFeature(FeatureType.UseGoogleDraco, cbUseDraco.Checked);
            //SetFeature(FeatureType.ExtractShell, cbUseExtractShell.Checked);
            SetFeature(FeatureType.GenerateModelsDb, cbGeneratePropDbSqlite.Checked);
            SetFeature(FeatureType.ExportSvfzip, cbExportSvfzip.Checked);
            SetFeature(FeatureType.EnableQuantizedAttributes, cbEnableQuantizedAttributes.Checked);
            SetFeature(FeatureType.EnableTextureWebP, cbEnableTextureWebP.Checked);
            SetFeature(FeatureType.GenerateThumbnail, cbGenerateThumbnail.Checked);
            SetFeature(FeatureType.EnableCesiumPrimitiveOutline, cbGenerateOutline.Checked);
            SetFeature(FeatureType.EnableUnlitMaterials, cbEnableUnlitMaterials.Checked);

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
                config.Features = _Features.Where(x => x.Selected).Select(x => x.Type).ToList();
                config.LastTargetPath = txtTargetPath.Text;
                config.VisualStyle = visualStyle?.Key;
                config.LevelOfDetail = levelOfDetail?.Value ?? -1;
                config.LevelOfDetailText = levelOfDetail?.ToString();
                config.Mode = cbContentType.GetSelectedValue<int>();

                _Config.Save();

                #endregion

                var sw = Stopwatch.StartNew();
                try
                {
                    const string FORMAT_KEY = @"3DTiles";

                    var setting = new ExportSetting();
                    setting.LevelOfDetail = config.LevelOfDetail;
                    setting.OutputPath = config.LastTargetPath;
                    setting.Mode = config.Mode;
                    setting.Features = _Features.Where(x => x.Selected && x.Enabled).Select(x => x.Type).ToList();
                    setting.Oem = App.GetOemInfo(homePath);
                    setting.PreExportSeedFeatures = App.GetPreExportSeedFeatures(FORMAT_KEY);
                    setting.GeoreferencedSetting = _GeoreferncingHost.CreateTargetSetting(config.GeoreferencedSetting);

                    //追加种子特性
                    App.UpdateFromSeedFeatures(setting.Features, FORMAT_KEY);

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

            cbExcludeLines.Checked = true;
            cbExcludeModelPoints.Checked = true;
            cbExcludeUnselectedElements.Checked = false;

            cbUseDraco.Checked = false;
            //cbUseExtractShell.Checked = false;
            cbGeneratePropDbSqlite.Checked = true;
            cbExportSvfzip.Checked = false;
            cbEnableQuantizedAttributes.Checked = true;
            cbEnableTextureWebP.Checked = true;
            //cbEmbedGeoreferencing.Checked = true;
            cbGenerateThumbnail.Checked = false;
            cbGenerateOutline.Checked = false;
            cbEnableUnlitMaterials.Checked = false;

            {
                _LocalConfig.GeoreferencedSetting = _GeoreferncingHost.CreateDefaultSetting();

                txtGeoreferencingInfo.Text = _LocalConfig.GeoreferencedSetting.GetDetails(_GeoreferncingHost);
            }

            cbContentType.SetSelectedValue(0);
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitUI();

                txtTargetPath.EnableFolderPathDrop();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var filePath = txtTargetPath.Text;

            {
                var dialog = this.folderBrowserDialog1;

                if (string.IsNullOrEmpty(filePath) == false)
                {
                    dialog.SelectedPath = filePath;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtTargetPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void cbVisualStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var visualStyle = cbVisualStyle.SelectedItem as VisualStyleInfo;
            if (visualStyle == null) return;

            foreach (var p in visualStyle.Features)
            {
                _Features.FirstOrDefault(x => x.Type == p.Key)?.ChangeSelected(_Features, p.Value);
            }

            var excludeTexture = _Features.FirstOrDefault(x => x.Type == FeatureType.ExcludeTexture)?.Selected ?? false;
            cbEnableTextureWebP.Enabled = !excludeTexture;
        }

        /// <summary>
        /// 开始导出
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="progressCallback"></param>
        /// <param name="cancellationToken"></param>
        private void StartExport(ExportSetting setting, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            using(var log = new RuntimeLog())
            {
                var exporter = new ExporterX(VersionInfo.GetHomePath());
                exporter.Export(setting, log, progressCallback, cancellationToken);

                //定制化输出成果
                VersionInfo.CustomOutputFor3DTiles(setting.OutputPath);
            }
        }

        private void InitUI()
        {
            var config = _LocalConfig;
            if (config.Features != null && config.Features.Count > 0)
            {
                foreach (var featureType in config.Features)
                {
                    _Features.FirstOrDefault(x=>x.Type == featureType)?.ChangeSelected(_Features, true);
                }
            }

            txtTargetPath.Text = config.LastTargetPath;

            bool IsAllowFeature(FeatureType feature)
            {
                return _Features.Any(x => x.Type == feature && x.Selected);
            }

            #region 基本
            {
                //视觉样式
                var visualStyle = _VisualStyles.FirstOrDefault(x => x.Key == config.VisualStyle) ??
                                  _VisualStyleDefault;
                foreach (var p in visualStyle.Features)
                {
                    _Features.FirstOrDefault(x => x.Type == p.Key)?.ChangeSelected(_Features, p.Value);
                }
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

                if (IsAllowFeature(FeatureType.ExcludeLines))
                {
                    cbExcludeLines.Checked = true;
                }

                if (IsAllowFeature(FeatureType.ExcludePoints))
                {
                    cbExcludeModelPoints.Checked = true;
                }

                if (IsAllowFeature(FeatureType.OnlySelected))
                {
                    cbExcludeUnselectedElements.Checked = true;
                }
            }
            #endregion

            #region 高级
            {
                toolTip1.SetToolTip(cbUseDraco, Strings.FeatureDescriptionUseGoogleDraco);
                //toolTip1.SetToolTip(cbUseExtractShell, Strings.FeatureDescriptionExtractShell);
                toolTip1.SetToolTip(cbGeneratePropDbSqlite, Strings.FeatureDescriptionGenerateModelsDb);
                toolTip1.SetToolTip(cbExportSvfzip, Strings.FeatureDescriptionExportSvfzip);
                toolTip1.SetToolTip(cbEnableQuantizedAttributes, Strings.FeatureDescriptionEnableQuantizedAttributes);
                toolTip1.SetToolTip(cbEnableTextureWebP, Strings.FeatureDescriptionEnableTextureWebP);
                toolTip1.SetToolTip(cbGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail);
                toolTip1.SetToolTip(cbGenerateOutline, Strings.FeatureDescriptionEnableCesiumPrimitiveOutline);
                toolTip1.SetToolTip(cbEnableUnlitMaterials, Strings.FeatureDescriptionEnableUnlitMaterials);

                if (IsAllowFeature(FeatureType.UseGoogleDraco))
                {
                    cbUseDraco.Checked = true;
                }

                //if (IsAllowFeature(FeatureType.ExtractShell))
                //{
                //    cbUseExtractShell.Checked = true;
                //}

                if (IsAllowFeature(FeatureType.GenerateModelsDb))
                {
                    cbGeneratePropDbSqlite.Checked = true;
                }

                if (IsAllowFeature(FeatureType.ExportSvfzip))
                {
                    cbExportSvfzip.Checked = true;
                }

                if (IsAllowFeature(FeatureType.EnableQuantizedAttributes))
                {
                    cbEnableQuantizedAttributes.Checked = true;
                }

                if (IsAllowFeature(FeatureType.EnableTextureWebP))
                {
                    cbEnableTextureWebP.Checked = true;
                }

                if (IsAllowFeature(FeatureType.GenerateThumbnail))
                {
                    cbGenerateThumbnail.Checked = true;
                }

                if (IsAllowFeature(FeatureType.EnableCesiumPrimitiveOutline))
                {
                    cbGenerateOutline.Checked = true;
                }

                if (IsAllowFeature(FeatureType.EnableUnlitMaterials))
                {
                    cbEnableUnlitMaterials.Checked = true;
                }
            }
            #endregion

            #region 3D Tiles

            cbContentType.SetSelectedValue(config.Mode);

            //toolTip1.SetToolTip(cbEmbedGeoreferencing, Strings.FeatureDescriptionEnableEmbedGeoreferencing);

            //cbEmbedGeoreferencing.Checked = IsAllowFeature(FeatureType.EnableEmbedGeoreferencing);

            #endregion

            #region 初始化地理配准信息
            {
                if (config.GeoreferencedSetting == null)
                {
                    config.GeoreferencedSetting = _GeoreferncingHost.CreateDefaultSetting();
                }

                txtGeoreferencingInfo.Text = config.GeoreferencedSetting.GetDetails(_GeoreferncingHost);
            }
            #endregion

#if EXPRESS
            cbExportSvfzip.Enabled = false;
			cbExportSvfzip.Checked = false;
#else
            cbExportSvfzip.Enabled = true;
#endif

        }

        private bool TryGetDoubleFromTextBox(TextBox tb, out double value)
        {
            if (double.TryParse(tb.Text, out value))
            {
                errorProvider1.SetError(tb, null);
                return true;
            }

            errorProvider1.SetError(tb, Strings.InvalidFormat);
            tb.Focus();
            return false;
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
        
        private void btnGeoreferncingConfig_Click(object sender, EventArgs e)
        {
            var owner = this.ParentForm;
            var host = _GeoreferncingHost;
            var input = _LocalConfig.GeoreferencedSetting;
            GeoreferncingHelper.ShowGeoreferncingUI(owner, host, input, setting =>
            {
                _LocalConfig.GeoreferencedSetting = setting;

                txtGeoreferencingInfo.Text = _LocalConfig.GeoreferencedSetting.GetDetails(host);
            });
        }
    }
}
