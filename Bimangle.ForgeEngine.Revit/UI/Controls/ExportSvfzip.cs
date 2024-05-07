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
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Common.Formats.Svf.Revit;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Revit.Config;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.Custom;
using Bimangle.ForgeEngine.Revit.Helpers;
using Bimangle.ForgeEngine.Revit.Utility;
using Ef = Bimangle.ForgeEngine.Common.Utils.ExtendFeatures;

namespace Bimangle.ForgeEngine.Revit.UI.Controls
{
    [Browsable(false)]
    partial class ExportSvfzip : UserControl, IExportControl
    {
        private UIDocument _UIDocument;
        private View3D _View;
        private AppConfig _Config;
        private AppConfigSvf _LocalConfig;
        private Dictionary<long, bool> _ElementIds;
        private bool _HasElementSelected;
        private Features<FeatureType> _Features;

        private List<VisualStyleInfo> _VisualStyles;
        private VisualStyleInfo _VisualStyleDefault;

        private List<ComboItemInfo> _LevelOfDetails;
        private ComboItemInfo _LevelOfDetailDefault;

        private List<long> _ViewIds;

        public TimeSpan ExportDuration { get; private set; }


        public ExportSvfzip()
        {
            InitializeComponent();
        }

        string IExportControl.Title => InnerCommandExportSvfzip.TITLE;

        string IExportControl.Icon => @"svf";

        void IExportControl.Init(UIDocument uidoc, View3D view, AppConfig config, Dictionary<long, bool> elementIds)
        {
            _UIDocument = uidoc;
            _View = view;
            _Config = config;
            _LocalConfig = _Config.Svf;
            _ElementIds = elementIds;
            _HasElementSelected = _ElementIds != null && _ElementIds.Count > 0;
            _ViewIds = null;

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

        bool IExportControl.Run(IExportForm form, bool enabledSampling)
        {
            var filePath = txtTargetPath.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                ShowMessageBox(Strings.MessageSelectOutputPathFirst);
                return false;
            }

#if !R2014
            if (CustomExporter.IsRenderingSupported() == false &&
                ShowConfirmBox(Strings.ExportWillFailBecauseMaterialLib) == false)
            {
                return false;
            }
#endif

            if (File.Exists(filePath) && 
                ShowConfirmBox(Strings.OutputFileExistedWarning) == false)
            {
                return false;
            }

            var homePath = VersionInfo.GetHomePath();
            if (GlobalConfig.CheckHomeFolder(homePath) == false &&
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

            _Features.Set(FeatureType.Export2DViewAll, rb2DViewsAll.Checked);
            _Features.Set(FeatureType.Export2DViewOnlySheet, rb2DViewsOnlySheet.Checked);

            _Features.Set(FeatureType.GenerateThumbnail, cbGenerateThumbnail.Checked);
            //_Features.Set(FeatureType.GenerateElementData, cbGeneratePropDbJson.Checked);
            _Features.Set(FeatureType.GenerateModelsDb, cbGeneratePropDbSqlite.Checked);
            _Features.Set(FeatureType.GenerateLeaflet, cbGenerateLeaflet.Checked);
            _Features.Set(FeatureType.GenerateDwgDrawing, cbGenerateDwg.Checked);

            _Features.Set(FeatureType.RegroupForLink, cbRegroupForLink.Checked);
            _Features.Set(FeatureType.RegroupForLinkFolderHierarchy, cbRegroupForLinkFolderHierarchy.Checked);
            _Features.Set(FeatureType.RegroupForWorkSet, cbRegroupForWorkset.Checked);

            _Features.Set(FeatureType.Force2DViewUseWireframe, cbForce2DViewUseWireframe.Checked);

            _Features.Set(FeatureType.ExportGrids, cbIncludeGrids.Checked);
            _Features.Set(FeatureType.ExportRooms, cbIncludeRooms.Checked);
            _Features.Set(FeatureType.ExportOpenings, cbIncludeOpenings.Checked);

            _Features.Set(FeatureType.ExcludeProperties, cbExcludeElementProperties.Checked);
            _Features.Set(FeatureType.ExcludeLines, cbExcludeLines.Checked);
            _Features.Set(FeatureType.ExcludePoints, cbExcludeModelPoints.Checked);
            _Features.Set(FeatureType.OnlySelected, cbExcludeUnselectedElements.Checked && _HasElementSelected);

            _Features.Set(FeatureType.ConsolidateGroup, cbConsolidateArrayGroup.Checked);
            _Features.Set(FeatureType.ConsolidateAssembly, cbConsolidateAssembly.Checked);
            _Features.Set(FeatureType.ConsolidateCompositeElement, cbConsolidateCompositeElement.Checked);
            _Features.Set(FeatureType.ConsolidateLinkInstance, cbConsolidateLinkInstance.Checked);

            _Features.Set(FeatureType.UseLevelCategory, rbGroupByLevelDefault.Checked);
            _Features.Set(FeatureType.UseNwLevelCategory, rbGroupByLevelNavisworks.Checked);
            _Features.Set(FeatureType.UseBoundLevelCategory, rbGroupByLevelBoundingBox.Checked);

            _Features.Set(FeatureType.UseCurrentViewport, cbUseCurrentViewport.Checked);

            //根据当前 "细线" 的状态确定是否增加特性 AreThinLinesEnabled
            _Features.Set(FeatureType.AreThinLinesEnabled, _View.Document.Application.AreThinLinesEnabled());

            #endregion

            var isCancelled = false;
            using (var session = LicenseConfig.Create())
            {
                if (session.IsValid == false)
                {
                    LicenseConfig.ShowDialog(session, ParentForm);
                    return false;
                }


                var features = _Features.GetEnabledFeatures().ToList();

                #region 保存设置

                var config = _LocalConfig;
                config.Features = features.ToList();
                config.LastTargetPath = txtTargetPath.Text;
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
                    setting.Features = features.ToList();
                    setting.SelectedElementIds = _ElementIds?.Where(x => x.Value).Select(x => x.Key).ToList();
                    setting.Selected2DViewIds = rb2DViewCustom.Checked ? _ViewIds : null;
                    setting.Oem = GlobalConfig.GetOemInfo(homePath);

                    //应用扩展特性
                    ApplyExtendFeatures(setting, form);

                    var hasSuccess = false;

                    using (var progress = new ProgressExHelper(this.ParentForm, Strings.MessageExporting))
                    {
                        var cancellationToken = progress.GetCancellationToken();

#if !DEBUG
                        //在有些 Revit 会遇到时不时无法转换的问题，循环多次重试, 应该可以成功
                        for (var i = 0; i < 5; i++)
                        {
                            try
                            {
                                StartExport(_UIDocument, _View, setting, enabledSampling, progress.GetProgressCallback(), cancellationToken);
                                hasSuccess = true;
                                break;
                            }
                            catch (Autodesk.Revit.Exceptions.ExternalApplicationException)
                            {
                                Application.DoEvents();
                            }
                            catch (IOException ex)
                            {
                                ShowMessageBox("文件保存失败: " + ex.ToString());
                                hasSuccess = true;
                                break;
                            }
                        }
#endif

                        //如果之前多次重试仍然没有成功, 这里再试一次，如果再失败就会给出稍后重试的提示
                        if (hasSuccess == false)
                        {
                            StartExport(_UIDocument, _View, setting, enabledSampling, progress.GetProgressCallback(), cancellationToken);
                        }

                        isCancelled = cancellationToken.IsCancellationRequested;
                    }

                    sw.Stop();
                    var ts = sw.Elapsed;
                    ExportDuration = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds); //去掉毫秒部分

                    Debug.WriteLine(Strings.MessageOperationSuccessAndElapsedTime, ExportDuration);

                    if (isCancelled == false)
                    {
                        {
                            ShowMessageBox(string.Format(Strings.MessageExportSuccess, ExportDuration));
                        }
                    }
                }
                catch (IOException ex)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    ShowMessageBox(string.Format(Strings.MessageFileSaveFailure, ex.ToString()));
                }
                catch (Autodesk.Revit.Exceptions.ExternalApplicationException)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    ShowMessageBox(Strings.MessageOperationFailureAndTryLater);
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

            rb2DViewsOnlySheet.Checked = true;

            cbGenerateThumbnail.Checked = true;
            cbGeneratePropDbSqlite.Checked = true;
            //cbGeneratePropDbJson.Checked = false;
            cbGenerateLeaflet.Checked = false;
            cbGenerateDwg.Checked = false;

            cbRegroupForLink.Checked = false;
            cbRegroupForLinkFolderHierarchy.Checked = false;
            cbRegroupForWorkset.Checked = false;

            cbForce2DViewUseWireframe.Checked = true;

            cbIncludeGrids.Checked = false;
            cbIncludeRooms.Checked = false;
            cbIncludeOpenings.Checked = false;

            cbExcludeElementProperties.Checked = false;
            cbExcludeLines.Checked = false;
            cbExcludeModelPoints.Checked = false;
            cbExcludeUnselectedElements.Checked = false;

            cbConsolidateArrayGroup.Checked = false;
            cbConsolidateAssembly.Checked = false;
			cbConsolidateCompositeElement.Checked = false;
            cbConsolidateLinkInstance.Checked = false;

            rbGroupByLevelDisable.Checked = true;

            cbUseCurrentViewport.Checked = false;
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitUI();

                FormHelper
                    .ToArray(rb2DViewsBypass, rb2DViewsOnlySheet, rb2DViewsAll, rb2DViewCustom)
                    .AddEventListener(Refresh2DDerivedDataStatus);


                cbExcludeUnselectedElements.Enabled = _ElementIds != null && _ElementIds.Count > 0;

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

        /// <summary>
        /// 开始导出
        /// </summary>
        /// <param name="uidoc"></param>
        /// <param name="view"></param>
        /// <param name="setting"></param>
        /// <param name="progressCallback"></param>
        /// <param name="cancellationToken"></param>
        private void StartExport(UIDocument uidoc, View3D view, ExportSetting setting, bool enabledSampling, Action<int> progressCallback, CancellationToken cancellationToken)
        {
#if EXPRESS
            throw new NotImplementedException();
#else

            using(var log = new RuntimeLog())
            {
                var exporter = new Bimangle.ForgeEngine.Revit.Pro.Svf.Exporter(VersionInfo.GetHomePath());
                exporter.Handler = new ExportHandler();
				exporter.EnabledSampling = enabledSampling;

                if (uidoc != null && uidoc.ActiveView.Id == view.Id)
                {
                    exporter.Export(view, uidoc, setting, log, progressCallback, cancellationToken);
                }
                else
                {
                    exporter.Export(view, setting, log, progressCallback, cancellationToken);
                }
            }
#endif
        }

        private void InitUI()
        {
            var config = _LocalConfig;
            _Features.Init(config.Features);

            txtTargetPath.Text = config.LastTargetPath;


            toolTip1.SetToolTip(cbUseCurrentViewport, Strings.FeatureDescriptionUseCurrentViewport);
            cbUseCurrentViewport.Checked = _Features.IsEnabled(FeatureType.UseCurrentViewport);

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

            #region 二维视图
            {
                toolTip1.SetToolTip(rb2DViewsAll, Strings.FeatureDescriptionExport2DViewAll);
                toolTip1.SetToolTip(rb2DViewsOnlySheet, Strings.FeatureDescriptionExport2DViewOnlySheet);
                toolTip1.SetToolTip(cbGenerateLeaflet, Strings.FeatureDescriptionGenerateLeaflet);
                toolTip1.SetToolTip(cbGenerateDwg, Strings.FeatureDescriptionGenerateDwgDrawing);
                toolTip1.SetToolTip(cbForce2DViewUseWireframe, Strings.FeatureDescriptionForce2DViewUseWireframe);

                if (_Features.IsEnabled(FeatureType.Export2DViewAll))
                {
                    rb2DViewsAll.Checked = true;
                }
                else if (_Features.IsEnabled(FeatureType.Export2DViewOnlySheet))
                {
                    rb2DViewsOnlySheet.Checked = true;
                }
                else
                {
                    rb2DViewsBypass.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.GenerateLeaflet))
                {
                    cbGenerateLeaflet.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.GenerateDwgDrawing))
                {
                    cbGenerateDwg.Checked = true;
                }

                cbForce2DViewUseWireframe.Checked = _Features.IsEnabled(FeatureType.Force2DViewUseWireframe);
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
                toolTip1.SetToolTip(cbRegroupForLinkFolderHierarchy, Strings.FeatureDescriptionRegroupForLinkFolderHierarchy);
                toolTip1.SetToolTip(cbRegroupForWorkset, Strings.FeatureDescriptionRegroupForWorkSet);

                if (_Features.IsEnabled(FeatureType.RegroupForLink))
                {
                    cbRegroupForLink.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.RegroupForLinkFolderHierarchy))
                {
                    cbRegroupForLinkFolderHierarchy.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.RegroupForWorkSet))
                {
                    cbRegroupForWorkset.Checked = true;
                }
            }
            #endregion

            #region 包含
            {
                toolTip1.SetToolTip(cbIncludeGrids, Strings.FeatureDescriptionExportGrids);
                toolTip1.SetToolTip(cbIncludeRooms, Strings.FeatureDescriptionExportRooms);
                toolTip1.SetToolTip(cbIncludeOpenings, Strings.FeatureDescriptionExportOpenings);

                if (_Features.IsEnabled(FeatureType.ExportGrids))
                {
                    cbIncludeGrids.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ExportRooms))
                {
                    cbIncludeRooms.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ExportOpenings))
                {
                    cbIncludeOpenings.Checked = true;
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

                if (_Features.IsEnabled(FeatureType.OnlySelected))
                {
                    cbExcludeUnselectedElements.Checked = true;
                }

                cbExcludeUnselectedElements.Enabled = _HasElementSelected;
            }
            #endregion

            #region 融合
            {
                toolTip1.SetToolTip(cbConsolidateArrayGroup, Strings.FeatureDescriptionConsolidateGroup);
                toolTip1.SetToolTip(cbConsolidateAssembly, Strings.FeatureDescriptionConsolidateAssembly);
                toolTip1.SetToolTip(cbConsolidateCompositeElement, Strings.FeatureDescriptionConsolidateCompositeElement);
                toolTip1.SetToolTip(cbConsolidateLinkInstance, Strings.FeatureDescriptionConsolidateLinkInstance);

                if (_Features.IsEnabled(FeatureType.ConsolidateGroup))
                {
                    cbConsolidateArrayGroup.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ConsolidateAssembly))
                {
                    cbConsolidateAssembly.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ConsolidateCompositeElement))
                {
                    cbConsolidateCompositeElement.Checked = true;
                }

                if (_Features.IsEnabled(FeatureType.ConsolidateLinkInstance))
                {
                    cbConsolidateLinkInstance.Checked = true;
                }
            }
            #endregion

            #region 按楼层分组
            {
                toolTip1.SetToolTip(rbGroupByLevelDefault, Strings.FeatureDescriptionUseLevelCategory);
                toolTip1.SetToolTip(rbGroupByLevelNavisworks, Strings.FeatureDescriptionUseNwLevelCategory);
                toolTip1.SetToolTip(rbGroupByLevelBoundingBox, Strings.FeatureDescriptionUseBoundLevelCategory);

                if (_Features.IsEnabled(FeatureType.UseLevelCategory))
                {
                    rbGroupByLevelDefault.Checked = true;
                }
                else if (_Features.IsEnabled(FeatureType.UseNwLevelCategory))
                {
                    rbGroupByLevelNavisworks.Checked = true;
                }
                else if (_Features.IsEnabled(FeatureType.UseBoundLevelCategory))
                {
                    rbGroupByLevelBoundingBox.Checked = true;
                }
                else
                {
                    rbGroupByLevelDisable.Checked = true;
                }
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

        private void btnSelectViews_Click(object sender, EventArgs e)
        {
            rb2DViewCustom.Checked = true;

            var form = new FormViews(_UIDocument.Document, _ViewIds);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _ViewIds = form.SelectedViewIds;

                btnSelectViews.Text = string.Format(Strings.SelectedViews, _ViewIds.Count);
            }

            Refresh2DDerivedDataStatus();
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

        private void Refresh2DDerivedDataStatus()
        {
            var allow = true;
            if (rb2DViewsBypass.Checked)
            {
                allow = false;
            }
            else if (rb2DViewCustom.Checked)
            {
                if (_ViewIds == null || _ViewIds.Count == 0)
                {
                    allow = false;
                }
            }

            cbGenerateLeaflet.Enabled = allow;
            cbGenerateDwg.Enabled = allow;
            cbForce2DViewUseWireframe.Enabled = allow;
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
