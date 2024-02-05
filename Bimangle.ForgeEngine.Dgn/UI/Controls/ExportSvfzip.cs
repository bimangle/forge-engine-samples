using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Bentley.MstnPlatformNET;
using Bimangle.ForgeEngine.Common.Formats.Svf.Dgn;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Dgn.Config;
using Bimangle.ForgeEngine.Dgn.Core;
using Bimangle.ForgeEngine.Dgn.Helpers;
using Bimangle.ForgeEngine.Dgn.Utility;
using Debug = System.Diagnostics.Debug;
using Ef = Bimangle.ForgeEngine.Common.Utils.ExtendFeatures;

namespace Bimangle.ForgeEngine.Dgn.UI.Controls
{
    [Browsable(false)]
    partial class ExportSvfzip : UserControl, IExportControl
    {
        private Viewport _View;
        private AppConfig _Config;
        private AppConfigSvf _LocalConfig;
        private bool _HasSelectElements;
        private Features<FeatureType> _Features;

        private List<VisualStyleInfo> _VisualStyles;
        private VisualStyleInfo _VisualStyleDefault;

        private List<ComboItemInfo> _LevelOfDetails;
        private ComboItemInfo _LevelOfDetailDefault;

        public TimeSpan ExportDuration { get; private set; }


        public ExportSvfzip()
        {
            InitializeComponent();
        }

        string IExportControl.Title => Titles.SVFZIP;

        string IExportControl.Icon => @"svf";

        void IExportControl.Init(Viewport view, AppConfig config, bool hasSelectElements)
        {
            _View = view;
            _Config = config;
            _LocalConfig = _Config.Svf;
            _HasSelectElements = hasSelectElements;

            _Features = new Features<FeatureType>();

            _VisualStyles = new List<VisualStyleInfo>();
            _VisualStyles.Add(new VisualStyleInfo(@"Wireframe", Strings.VisualStyleWireframe, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, true},
                {FeatureType.Wireframe, true},
                {FeatureType.UseViewOverrideGraphic, false},
                {FeatureType.UseBasicRenderColor, false},
                {FeatureType.Gray, false}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Gray", Strings.VisualStyleGray, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, true},
                {FeatureType.Wireframe, false},
                {FeatureType.UseViewOverrideGraphic, false},
                {FeatureType.UseBasicRenderColor, false},
                {FeatureType.Gray, true}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Colored", Strings.VisualStyleColored, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, true},
                {FeatureType.Wireframe, false},
                {FeatureType.UseViewOverrideGraphic, true},
                {FeatureType.UseBasicRenderColor, false},
                {FeatureType.Gray, false}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Textured", Strings.VisualStyleTextured + $@"({Strings.TextDefault})", new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, false},
                {FeatureType.Wireframe, false},
                {FeatureType.UseViewOverrideGraphic, false},
                {FeatureType.UseBasicRenderColor, true},
                {FeatureType.Gray, false}
            }));
            _VisualStyles.Add(new VisualStyleInfo(@"Realistic", Strings.VisualStyleRealistic, new Dictionary<FeatureType, bool>
            {
                {FeatureType.ExcludeTexture, false},
                {FeatureType.Wireframe, false},
                {FeatureType.UseViewOverrideGraphic, false},
                {FeatureType.UseBasicRenderColor, false},
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

        bool IExportControl.Run(IExportForm form)
        {
            var filePath = txtTargetPath.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                ShowMessageBox(Strings.MessageSelectOutputPathFirst);
                return false;
            }

            if (File.Exists(filePath) && 
                ShowConfirmBox(Strings.OutputFileExistedWarning) == false)
            {
                return false;
            }

            var homePath = VersionInfo.GetHomePath();
            if (InnerApp.CheckHomeFolder(homePath) == false &&
                ShowConfirmBox(Strings.HomeFolderIsInvalid) == false)
            {
                return false;
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

            _Features.Set(FeatureType.RegroupForLink, cbRegroupForLink.Checked);

            _Features.Set(FeatureType.ExcludeProperties, cbExcludeElementProperties.Checked);
            _Features.Set(FeatureType.ExcludeLines, cbExcludeLines.Checked);
            _Features.Set(FeatureType.ExcludePoints, cbExcludeModelPoints.Checked);
            _Features.Set(FeatureType.OnlySelected, cbExcludeUnselectedElements.Checked && _HasSelectElements);

            _Features.Set(FeatureType.ExportElementClassConstruction, cbExportElementClassConstruction.Checked);

            #endregion

            var isCanncelled = false;
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
                config.AutoOpenAppName = string.Empty;
                config.VisualStyle = visualStyle?.Key;
                config.LevelOfDetail = levelOfDetail?.Value ?? -1;
                _Config.Save();

                #endregion

                var sw = Stopwatch.StartNew();
                try
                {
                    var setting = new ExportSetting();
                    setting.LevelOfDetail = config.LevelOfDetail;
                    setting.ExportType = ExportType.Zip;
                    setting.OutputPath = config.LastTargetPath;
                    setting.Features = _Features.GetEnabledFeatures().ToList();
                    //setting.Selected2DViewIds = rb2DViewCustom.Checked ? _ViewIds : null;
                    setting.Oem = InnerApp.GetOemInfo(VersionInfo.GetHomePath());

                    //应用扩展特性
                    ApplyExtendFeatures(setting, form);

                    using (var progress = new ProgressExHelper(this.ParentForm, Strings.MessageExporting))
                    {
                        var cancellationToken = progress.GetCancellationToken();
                        try
                        {
                            StartExport(_View, setting, progress.GetProgressCallback(), cancellationToken);
                        }
                        catch (IOException ex)
                        {
                            ShowMessageBox("文件保存失败: " + ex.ToString());
                        }

                        isCanncelled = cancellationToken.IsCancellationRequested;
                    }

                    sw.Stop();
                    var ts = sw.Elapsed;
                    ExportDuration = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds); //去掉毫秒部分

                    Debug.WriteLine(Strings.MessageOperationSuccessAndElapsedTime, ExportDuration);

                    if (isCanncelled == false)
                    {
                        ShowMessageBox(string.Format(Strings.MessageExportSuccess, ExportDuration));
                    }
                }
                catch (IOException ex)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    ShowMessageBox(string.Format(Strings.MessageFileSaveFailure, ex.ToString()));
                }
                //catch (Autodesk.Revit.Exceptions.ExternalApplicationException)
                //{
                //    sw.Stop();
                //    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                //    ShowMessageBox(Strings.MessageOperationFailureAndTryLater);
                //}
                catch (Exception ex)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    ShowMessageBox(ex.ToString());
                }
            }

            return isCanncelled == false;
        }

        void IExportControl.Reset()
        {
            cbVisualStyle.SelectedItem = _VisualStyleDefault;
            cbLevelOfDetail.SelectedItem = _LevelOfDetailDefault;

            cbGenerateThumbnail.Checked = true;
            cbGeneratePropDbSqlite.Checked = true;
            //cbGeneratePropDbJson.Checked = false;

            cbRegroupForLink.Checked = false;

            cbExcludeElementProperties.Checked = false;
            cbExcludeLines.Checked = false;
            cbExcludeModelPoints.Checked = false;
            cbExcludeUnselectedElements.Checked = false;

            cbExportElementClassConstruction.Checked = true;
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitUI();

                cbExcludeUnselectedElements.Enabled = _HasSelectElements;

                txtTargetPath.EnableFilePathDrop(@"model.svfzip");
            }
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
                dialog.Filter = string.Join(@"|", Strings.DialogFilterSvfzip, Strings.DialogFilterAllFile);

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
        }

        private void StartExport(Viewport view, ExportSetting setting, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            using (var log = new RuntimeLog())
            {
                var exporter = new Bimangle.ForgeEngine.Dgn.Pro.Svf.Exporter(VersionInfo.GetHomePath());
                exporter.Export(view, setting, log, progressCallback, cancellationToken);
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

            #region 构件分组
            {
                toolTip1.SetToolTip(cbRegroupForLink, Strings.FeatureDescriptionRegroupForLink);

                if (_Features.IsEnabled(FeatureType.RegroupForLink))
                {
                    cbRegroupForLink.Checked = true;
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

                if (_Features.IsEnabled(FeatureType.OnlySelected) && _HasSelectElements)
                {
                    cbExcludeUnselectedElements.Checked = true;
                }

                cbExcludeUnselectedElements.Enabled = _HasSelectElements;
            }
            #endregion

            #region 高级
            {
                toolTip1.SetToolTip(cbExportElementClassConstruction, Strings.FeatureDescriptionExportElementClassConstruction);

                if (_Features.IsEnabled(FeatureType.ExportElementClassConstruction))
                {
                    cbExportElementClassConstruction.Checked = true;
                }
            }
            #endregion
        }

        private void cbAutoOpen_CheckedChanged(object sender, EventArgs e)
        {
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

        private void ShowMessageBox(string message)
        {
            ParentForm.ShowMessageBox(message);
        }
        private bool ShowConfirmBox(string message)
        {
            return MessageBox.Show(ParentForm, message, ParentForm.Text,
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
            if (form.UsedExtendFeature(Ef.RenderingPerformancePreferred))
            {
                setting.Features.Add(FeatureType.RenderingPerformancePreferred);
            }

            if (form.UsedExtendFeature(Ef.DisableMeshSimplifier))
            {
                setting.Features.Add(FeatureType.DisableMeshSimplifier);
            }
        }
    }
}
