using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Formats.Gltf;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine._3DXML.Core;
using Bimangle.ForgeEngine._3DXML.Config;
using Bimangle.ForgeEngine._3DXML.Utility;
using Ef = Bimangle.ForgeEngine.Common.Utils.ExtendFeatures;

namespace Bimangle.ForgeEngine._3DXML.UI.Controls
{
    [Browsable(false)]
    partial class ExportGltf : UserControl, IExportControl
    {
        /// <summary>
        /// Draco
        /// </summary>
        private const int GEOMETRY_COMPRESS_TYPE_DEFAULT = 100;

        private IExportForm _Form;
        private bool _IsInit;
        private AppConfig _Config;
        private AppConfigGltf _LocalConfig;
        private Features<FeatureType> _Features;

        private List<VisualStyleInfo> _VisualStyles;
        private VisualStyleInfo _VisualStyleDefault;

        private List<ComboItemInfo> _LevelOfDetails;
        private ComboItemInfo _LevelOfDetailDefault;

        public ExportGltf()
        {
            InitializeComponent();
        }

        string IExportControl.Title => @"glTF/glb";

        string IExportControl.Icon => @"gltf";

        void IExportControl.Init(IExportForm form, AppConfig config)
        {
            _Form = form;
            _Config = config;
            _LocalConfig = _Config.Gltf;

            _Features = new Features<FeatureType>();

            _VisualStyles = new List<VisualStyleInfo>();
            _VisualStyles.Add(new VisualStyleInfo(@"Wireframe", Strings.VisualStyleWireframe, FeatureType.ExcludeTexture, FeatureType.Wireframe));
            _VisualStyles.Add(new VisualStyleInfo(@"Gray", Strings.VisualStyleGray, FeatureType.ExcludeTexture, FeatureType.Gray));
            _VisualStyles.Add(new VisualStyleInfo(@"Colored", Strings.VisualStyleColored, FeatureType.ExcludeTexture));
            _VisualStyles.Add(new VisualStyleInfo(@"Textured", Strings.VisualStyleTextured + $@"({Strings.TextDefault})"));
            _VisualStyleDefault = _VisualStyles.First(x => x.Key == @"Textured");

            const int DEFAULT_LEVEL_OF_DETAILS = 6;
            _LevelOfDetails = new List<ComboItemInfo>();
            _LevelOfDetails.Add(new ComboItemInfo(-1, Strings.TextAuto));
            for (var i = 0; i <= 15; i++)
            {
                string text;
                switch (i)
                {
                    case 0:
                        text = $@"{i} ({Strings.TextLowest})";
                        break;
                    case DEFAULT_LEVEL_OF_DETAILS:
                        text = $@"{i} ({Strings.TextNormal})";
                        break;
                    case 15:
                        text = $@"{i} ({Strings.TextHighest})";
                        break;
                    default:
                        text = i.ToString();
                        break;
                }

                _LevelOfDetails.Add(new ComboItemInfo(i, text));
            }
            _LevelOfDetailDefault = _LevelOfDetails.Find(x => x.Value == DEFAULT_LEVEL_OF_DETAILS);

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

            cbEnableGeometryCompress.Checked = false;
            cbGeometryCompressTypes.SetSelectedValue(GEOMETRY_COMPRESS_TYPE_DEFAULT);    //Default: Draco
            cbEnableTextureCompress.Checked = false;
            cbTextureCompressTypes.SetSelectedValue(0);

            cbGenerateThumbnail.Checked = false;
            cbEnableAutomaticSplit.Checked = false;
            cbAllowRegroupNodes.Checked = true;
        }

        void IExportControl.RefreshCommand()
        {
            RefreshCommand();
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                FormHelper
                    .ToArray(txtTargetPath,
                        cbVisualStyle, cbLevelOfDetail,
                        cbExcludeLines, cbExcludeModelPoints, cbExcludeUnselectedElements,
                        cbEnableGeometryCompress, cbGeometryCompressTypes, cbEnableTextureCompress, cbTextureCompressTypes,
                        cbUseExtractShell, cbGeneratePropDbSqlite, cbExportSvfzip, cbGenerateThumbnail, cbEnableAutomaticSplit, cbAllowRegroupNodes)
                    .AddEventListener(RefreshCommand);

                InitUI();

                cbExcludeUnselectedElements.Checked = false;
                cbExcludeUnselectedElements.Enabled = false;

                txtTargetPath.EnableFilePathDrop(@"model.gltf");
            }

            _IsInit = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var filePath = txtTargetPath.Text;

            {
                var dialog = this.saveFileDialog1;

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

#if DEBUG
            cbExportSvfzip.Enabled = true;
#endif
        }

        private class VisualStyleInfo
        {
            public string Key { get; }

            private string Text { get; }

            public Dictionary<FeatureType, bool> Features { get; }

            public VisualStyleInfo(string key, string text, params FeatureType[] features)
            {
                Key = key;
                Text = text;
                Features = new Dictionary<FeatureType, bool>
                {
                    {FeatureType.ExcludeTexture, false},
                    {FeatureType.Wireframe, false},
                    {FeatureType.Gray, false}
                };
                foreach (var feature in features)
                {
                    Features[feature] = true;
                }
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
            public int Value { get;  }

            private string Text { get;  }

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

        private void RefreshCommand()
        {
            if (!_IsInit) return;

            var options = BuildOptions();
            _Form.RefreshCommand(options);
        }

        private Options BuildOptions()
        {
            var targetPath = txtTargetPath.Text;
            if (string.IsNullOrEmpty(targetPath))
            {
                return null;
            }

            //重置 Features 所有特性为 false
            _Features.Clear();

            var visualStyle = cbVisualStyle.SelectedItem as VisualStyleInfo;
            _Features.Apply(visualStyle?.Features);

            var levelOfDetail = (cbLevelOfDetail.SelectedItem as ComboItemInfo) ?? _LevelOfDetailDefault;
            
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

            var r = new Options();
            r.Format = @"gltf";
            r.Mode = 0;
            r.VisualStyle = visualStyle?.Key;
            r.LevelOfDetail = levelOfDetail?.Value ?? -1;
            r.Features = _Features.GetEnabledFeatures().Select(x=>x.ToString()).ToList();
            r.OutputFolderPath = targetPath;

            //应用扩展特性
            ApplyExtendFeatures(r);

            #region 保存设置

            var config = _LocalConfig;
            config.Features = _Features.GetEnabledFeatures().ToList();
            config.LastTargetPath = txtTargetPath.Text;
            config.VisualStyle = visualStyle?.Key;
            config.LevelOfDetail = levelOfDetail?.Value ?? -1;
            _Config.Save();

            #endregion

            return r;
        }

        /// <summary>
        /// 应用扩展属性
        /// </summary>
        /// <param name="options"></param>
        private void ApplyExtendFeatures(Options options)
        {
            var features = options.Features.ToList();

            if (_Form.UsedExtendFeature(Ef.RenderingPerformancePreferred))
            {
                features.Add(Ef.RenderingPerformancePreferred);
            }

            if (_Form.UsedExtendFeature(Ef.DisableMeshSimplifier))
            {
                features.Add(Ef.DisableMeshSimplifier);
            }

            options.Features = features;
        }
    }
}
