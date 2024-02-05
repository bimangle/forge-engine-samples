using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Formats.Svf._3DXML;
using Bimangle.ForgeEngine._3DXML.Config;
using Bimangle.ForgeEngine._3DXML.Core;
using Bimangle.ForgeEngine._3DXML.Utility;
using Bimangle.ForgeEngine.Common.Utils;
using Ef = Bimangle.ForgeEngine.Common.Utils.ExtendFeatures;

namespace Bimangle.ForgeEngine._3DXML.UI.Controls
{
    [Browsable(false)]
    partial class ExportSvfzip : UserControl, IExportControl
    {
        private IExportForm _Form;
        private bool _IsInit;
        private AppConfig _Config;
        private AppConfigSvf _LocalConfig;
        private Features<FeatureType> _Features;

        private List<VisualStyleInfo> _VisualStyles;
        private VisualStyleInfo _VisualStyleDefault;

        private List<ComboItemInfo> _LevelOfDetails;
        private ComboItemInfo _LevelOfDetailDefault;

        public ExportSvfzip()
        {
            InitializeComponent();
        }

        string IExportControl.Title => @"Svf";

        string IExportControl.Icon => @"svf";

        void IExportControl.Init(IExportForm form, AppConfig config)
        {
            _Form = form;
            _Config = config;
            _LocalConfig = _Config.Svf;

            _Features = new Features<FeatureType>();

            _VisualStyles = new List<VisualStyleInfo>();
            _VisualStyles.Add(new VisualStyleInfo(@"Wireframe", Strings.VisualStyleWireframe, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, true},
                {FeatureType.Wireframe, true},
                {FeatureType.Gray, false}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Gray", Strings.VisualStyleGray, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, true},
                {FeatureType.Wireframe, false},
                {FeatureType.Gray, true}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Colored", Strings.VisualStyleColored, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, true},
                {FeatureType.Wireframe, false},
                {FeatureType.Gray, false}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Textured", Strings.VisualStyleTextured + $@"({Strings.TextDefault})", new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, false},
                {FeatureType.Wireframe, false},
                {FeatureType.Gray, false}
            }));
            _VisualStyleDefault = _VisualStyles.First(x => x.Key == @"Textured");

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
                    case 8:
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
            _LevelOfDetailDefault = _LevelOfDetails.Find(x => x.Value == -1);

            cbVisualStyle.Items.Clear();
            cbVisualStyle.Items.AddRange(_VisualStyles.Select(x => (object)x).ToArray());

            cbLevelOfDetail.Items.Clear();
            cbLevelOfDetail.Items.AddRange(_LevelOfDetails.Select(x => (object)x).ToArray());
        }

        void IExportControl.Reset()
        {
            cbVisualStyle.SelectedItem = _VisualStyleDefault;
            cbLevelOfDetail.SelectedItem = _LevelOfDetailDefault;

            cbGenerateThumbnail.Checked = true;
            cbGeneratePropDbSqlite.Checked = true;

            cbExcludeElementProperties.Checked = false;
            cbExcludeLines.Checked = false;
            cbExcludeModelPoints.Checked = false;
            cbExcludeUnselectedElements.Checked = false;
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
                        cbVisualStyle, cbLevelOfDetail, cbGenerateThumbnail, cbGeneratePropDbSqlite, 
                        cbExcludeElementProperties, cbExcludeLines, cbExcludeModelPoints, cbExcludeUnselectedElements)
                    .AddEventListener(RefreshCommand);

                InitUI();

                cbExcludeUnselectedElements.Checked = false;
                cbExcludeUnselectedElements.Enabled = false;

                txtTargetPath.EnableFolderPathDrop();
            }

            _IsInit = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgSelectFolder.SelectedPath = txtTargetPath.Text;
            if (dlgSelectFolder.ShowDialog(this) == DialogResult.OK)
            {
                txtTargetPath.Text = dlgSelectFolder.SelectedPath;
            }
        }

        private void cbVisualStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var visualStyle = cbVisualStyle.SelectedItem as VisualStyleInfo;
            if (visualStyle == null) return;

            _Features.Apply(visualStyle.Features);
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

            #region 生成
            {
                toolTip1.SetToolTip(cbGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail);
                //toolTip1.SetToolTip(cbGeneratePropDbJson, Strings.FeatureDescriptionGenerateElementData);
                toolTip1.SetToolTip(cbGeneratePropDbSqlite, Strings.FeatureDescriptionGenerateModelsDb);

                if (_Features.IsEnabled(FeatureType.GenerateThumbnail))
                {
                    cbGenerateThumbnail.Checked = true;
                }

                //if (_Features.IsEnabled(FeatureType.GenerateElementData))
                //{
                //    cbGeneratePropDbJson.Checked = true;
                //}

                if (_Features.IsEnabled(FeatureType.GenerateModelsDb))
                {
                    cbGeneratePropDbSqlite.Checked = true;
                }

            }
            #endregion

            #region 排除
            {
                toolTip1.SetToolTip(cbExcludeElementProperties, Strings.FeatureDescriptionExcludeProperties);
                toolTip1.SetToolTip(cbExcludeLines, Strings.FeatureDescriptionExcludeLines);
                toolTip1.SetToolTip(cbExcludeModelPoints, Strings.FeatureDescriptionExcludePoints);
                toolTip1.SetToolTip(cbExcludeUnselectedElements, Strings.FeatureDescriptionOnlySelected);

                if (_Features.IsEnabled(FeatureType.ExcludeProperties))
                {
                    cbExcludeElementProperties.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ExcludeLines))
                {
                    cbExcludeLines.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ExcludePoints))
                {
                    cbExcludeModelPoints.Checked = true;
                }

                //if (_Features.IsEnabled(FeatureType.OnlySelected))
                //{
                //    cbExcludeUnselectedElements.Checked = true;
                //}
            }
            #endregion
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

            _Features.Set(FeatureType.GenerateThumbnail, cbGenerateThumbnail.Checked);
            //_Features.Set(FeatureType.GenerateElementData, cbGeneratePropDbJson.Checked);
            _Features.Set(FeatureType.GenerateModelsDb, cbGeneratePropDbSqlite.Checked);

            _Features.Set(FeatureType.ExcludeProperties, cbExcludeElementProperties.Checked);
            _Features.Set(FeatureType.ExcludeLines, cbExcludeLines.Checked);
            _Features.Set(FeatureType.ExcludePoints, cbExcludeModelPoints.Checked);
            //_Features.Set(FeatureType.OnlySelected, cbExcludeUnselectedElements.Checked);

            #endregion

            var r = new Options();
            r.Format = @"svf";
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
