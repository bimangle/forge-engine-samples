using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Formats.Svf.Dwg;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Dwg.Core;
using Bimangle.ForgeEngine.Dwg.Config;
using Bimangle.ForgeEngine.Dwg.Utility;
using Ef = Bimangle.ForgeEngine.Common.Utils.ExtendFeatures;

namespace Bimangle.ForgeEngine.Dwg.UI.Controls
{
    [Browsable(false)]
    partial class ExportSvfzip : UserControl, IExportControl
    {
        private IExportForm _Form;
        private bool _IsInit;
        private AppConfig _Config;
        private AppConfigSvf _LocalConfig;
        private Features<FeatureType> _Features;

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
        }

        void IExportControl.Reset()
        {
            rbMode2D.Checked = true;

            cbIncludeInvisibleLayers.Checked = false;
            cbIncludeUnplottableLayers.Checked = true;
            cbIncludeLayouts.Checked = true;

            cbGenerateThumbnail.Checked = true;
            cbGeneratePropDbSqlite.Checked = true;
            cbGenerateLeaflet.Checked = false;

            cbUseDefaultViewport.Checked = false;
            cbOptimizationLineStyle.Checked = true;
            cbForceUseWireframe.Checked = true;
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
                        rbMode2D, rbMode3D, rbModeAll,
                        cbIncludeInvisibleLayers, cbIncludeUnplottableLayers, cbIncludeLayouts,
                        cbGenerateThumbnail, cbGeneratePropDbSqlite, cbGenerateLeaflet,
                        cbUseDefaultViewport, cbOptimizationLineStyle, cbForceUseWireframe)
                    .AddEventListener(RefreshCommand);

                InitUI();

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

        private void InitUI()
        {
            var config = _LocalConfig;
            _Features.Init(config.Features);

            txtTargetPath.Text = config.LastTargetPath;

            #region 模式
            {
                toolTip1.SetToolTip(rbMode2D, Strings.FeatureDescriptionExportMode2D);
                toolTip1.SetToolTip(rbMode3D, Strings.FeatureDescriptionExportMode3D);
                toolTip1.SetToolTip(rbModeAll, Strings.FeatureDescriptionExportModeAll);

                if (_Features.IsEnabled(FeatureType.ExportMode2D) &&
                    _Features.IsEnabled(FeatureType.ExportMode3D))
                {
                    rbModeAll.Checked = true;
                }
                else if (_Features.IsEnabled(FeatureType.ExportMode2D))
                {
                    rbMode2D.Checked = true;
                }
                else if (_Features.IsEnabled(FeatureType.ExportMode3D))
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

                cbIncludeInvisibleLayers.Checked = _Features.IsEnabled(FeatureType.IncludeInvisibleLayers);
                cbIncludeUnplottableLayers.Checked = _Features.IsEnabled(FeatureType.IncludeUnplottableLayers);
                cbIncludeLayouts.Checked = _Features.IsEnabled(FeatureType.IncludeLayouts);
            }
            #endregion

            #region 生成
            {
                toolTip1.SetToolTip(cbGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail);
                toolTip1.SetToolTip(cbGeneratePropDbSqlite, Strings.FeatureDescriptionGenerateModelsDb);
                toolTip1.SetToolTip(cbGenerateLeaflet, Strings.FeatureDescriptionGenerateLeaflet);

                if (_Features.IsEnabled(FeatureType.GenerateThumbnail))
                {
                    cbGenerateThumbnail.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.GenerateModelsDb))
                {
                    cbGeneratePropDbSqlite.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.GenerateLeaflet))
                {
                    cbGenerateLeaflet.Checked = true;
                }
            }
            #endregion

            #region 其它
            {
                toolTip1.SetToolTip(cbUseDefaultViewport, Strings.FeatureDescriptionUseDefaultViewport);
                toolTip1.SetToolTip(cbOptimizationLineStyle, Strings.FeatureDescriptionOptimizationLineStyle);
                toolTip1.SetToolTip(cbForceUseWireframe, Strings.FeatureDescriptionForceRenderModeUseWireframe);

                cbUseDefaultViewport.Checked = _Features.IsEnabled(FeatureType.UseDefaultViewport);
                cbOptimizationLineStyle.Checked = _Features.IsEnabled(FeatureType.OptimizationLineStyle);
                cbForceUseWireframe.Checked = _Features.IsEnabled(FeatureType.ForceRenderModeUseWireframe);
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

            var visualStyle = cbForceUseWireframe.Checked ? @"Wireframe" : @"Auto";

            #region 更新界面选项到 _Features

            if (rbMode2D.Checked)
            {
                _Features.Set(FeatureType.ExportMode2D, true);
                _Features.Set(FeatureType.ExportMode3D, false);
            }
            else if (rbMode3D.Checked)
            {
                _Features.Set(FeatureType.ExportMode2D, false);
                _Features.Set(FeatureType.ExportMode3D, true);
            }
            else
            {
                _Features.Set(FeatureType.ExportMode2D, true);
                _Features.Set(FeatureType.ExportMode3D, true);
            }

            _Features.Set(FeatureType.IncludeInvisibleLayers, cbIncludeInvisibleLayers.Checked);
            _Features.Set(FeatureType.IncludeUnplottableLayers, cbIncludeUnplottableLayers.Checked);
            _Features.Set(FeatureType.IncludeLayouts, cbIncludeLayouts.Checked);

            _Features.Set(FeatureType.GenerateThumbnail, cbGenerateThumbnail.Checked);
            _Features.Set(FeatureType.GenerateModelsDb, cbGeneratePropDbSqlite.Checked);
            _Features.Set(FeatureType.GenerateLeaflet, cbGenerateLeaflet.Checked);

            _Features.Set(FeatureType.UseDefaultViewport, cbUseDefaultViewport.Checked);
            _Features.Set(FeatureType.OptimizationLineStyle, cbOptimizationLineStyle.Checked);
            _Features.Set(FeatureType.ForceRenderModeUseWireframe, cbForceUseWireframe.Checked);

            #endregion

            var r = new Options();
            r.Format = @"svf";
            r.Mode = 0;
            r.VisualStyle = visualStyle;
            r.Features = _Features.GetEnabledFeatures().Select(x => x.ToString()).ToList();
            r.OutputPath = targetPath;

            //应用扩展特性
            ApplyExtendFeatures(r);

            #region 保存设置

            var config = _LocalConfig;
            config.Features = _Features.GetEnabledFeatures().ToList();
            config.LastTargetPath = txtTargetPath.Text;
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
