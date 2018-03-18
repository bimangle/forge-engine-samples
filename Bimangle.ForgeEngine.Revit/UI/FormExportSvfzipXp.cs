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
using Bimangle.ForgeEngine.Revit.Config;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.Helpers;
using Bimangle.ForgeEngine.Revit.Utility;
using Newtonsoft.Json.Linq;
using Form = System.Windows.Forms.Form;

namespace Bimangle.ForgeEngine.Revit.UI
{
    partial class FormExportSvfzipXp : Form
    {
        private readonly UIDocument _UIDocument;
        private readonly View3D _View;
        private readonly AppConfig _Config;
        private readonly Dictionary<int, bool> _ElementIds;
        private readonly List<FeatureInfo> _Features;

        private readonly List<VisualStyleInfo> _VisualStyles;
        private readonly VisualStyleInfo _VisualStyleDefault;

        private readonly List<ComboItemInfo> _LevelOfDetails;
        private readonly ComboItemInfo _LevelOfDetailDefault;


        public TimeSpan ExportDuration { get; private set; }

        public FormExportSvfzipXp()
        {
            InitializeComponent();
        }

        public FormExportSvfzipXp(UIDocument uidoc, View3D view, AppConfig config, Dictionary<int, bool> elementIds)
            : this()
        {
            _UIDocument = uidoc;
            _View = view;
            _Config = config;
            _ElementIds = elementIds;

            _Features = new List<FeatureInfo>
            {
                new FeatureInfo(FeatureType.ExcludeProperties, Strings.FeatureNameExcludeProperties, Strings.FeatureDescriptionExcludeProperties),
                new FeatureInfo(FeatureType.ExcludeTexture, Strings.FeatureNameExcludeTexture, Strings.FeatureDescriptionExcludeTexture, true, false),
                new FeatureInfo(FeatureType.ExcludeLines, Strings.FeatureNameExcludeLines, Strings.FeatureDescriptionExcludeLines),
                new FeatureInfo(FeatureType.ExcludePoints, Strings.FeatureNameExcludePoints, Strings.FeatureDescriptionExcludePoints, true, false),
                new FeatureInfo(FeatureType.UseLevelCategory, Strings.FeatureNameUseLevelCategory, Strings.FeatureDescriptionUseLevelCategory),
                new FeatureInfo(FeatureType.UseNwLevelCategory, Strings.FeatureNameUseNwLevelCategory, Strings.FeatureDescriptionUseNwLevelCategory),
                new FeatureInfo(FeatureType.UseBoundLevelCategory, Strings.FeatureNameUseBoundLevelCategory, Strings.FeatureDescriptionUseBoundLevelCategory),
                new FeatureInfo(FeatureType.OnlySelected, Strings.FeatureNameOnlySelected, Strings.FeatureDescriptionOnlySelected),
                new FeatureInfo(FeatureType.GenerateElementData, Strings.FeatureNameGenerateElementData, Strings.FeatureDescriptionGenerateElementData),
                new FeatureInfo(FeatureType.ExportGrids, Strings.FeatureNameExportGrids, Strings.FeatureDescriptionExportGrids),
                new FeatureInfo(FeatureType.ExportRooms, Strings.FeatureNameExportRooms, Strings.FeatureDescriptionExportRooms),
                new FeatureInfo(FeatureType.ConsolidateGroup, Strings.FeatureNameConsolidateGroup, Strings.FeatureDescriptionConsolidateGroup),
                new FeatureInfo(FeatureType.ConsolidateAssembly, Strings.FeatureNameConsolidateAssembly, Strings.FeatureDescriptionConsolidateAssembly),
                new FeatureInfo(FeatureType.Wireframe, Strings.FeatureNameWireframe, Strings.FeatureDescriptionWireframe, true, false),
                new FeatureInfo(FeatureType.Gray, Strings.FeatureNameGray, Strings.FeatureDescriptionGray, true, false),
                new FeatureInfo(FeatureType.GenerateModelsDb, Strings.FeatureNameGenerateModelsDb, Strings.FeatureDescriptionGenerateModelsDb),
                new FeatureInfo(FeatureType.GenerateThumbnail, Strings.FeatureNameGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail),
                new FeatureInfo(FeatureType.UseCurrentViewport, Strings.FeatureNameUseCurrentViewport, Strings.FeatureDescriptionUseCurrentViewport),
                new FeatureInfo(FeatureType.UseViewOverrideGraphic, Strings.FeatureNameUseViewOverrideGraphic, Strings.FeatureDescriptionUseViewOverrideGraphic, true, false),
                new FeatureInfo(FeatureType.UseBasicRenderColor, string.Empty, string.Empty, true, false),
                new FeatureInfo(FeatureType.Export2DViewOnlySheet, Strings.FeatureNameExport2DViewOnlySheet, Strings.FeatureDescriptionExport2DViewOnlySheet),
                new FeatureInfo(FeatureType.Export2DViewAll, Strings.FeatureNameExport2DViewAll, Strings.FeatureDescriptionExport2DViewAll),
            };

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
            cbVisualStyle.Items.AddRange(_VisualStyles.Select(x=>(object)x).ToArray());

            cbLevelOfDetail.Items.Clear();
            cbLevelOfDetail.Items.AddRange(_LevelOfDetails.Select(x => (object)x).ToArray());
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            Text += $@" - {Command.TITLE}";

            InitUI();
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

        private void cbVisualStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var visualStyle = cbVisualStyle.SelectedItem as VisualStyleInfo;
            if (visualStyle == null) return;

            foreach (var p in visualStyle.Features)
            {
                _Features.FirstOrDefault(x => x.Type == p.Key)?.ChangeSelected(_Features, p.Value);
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

#if !R2014
            if (CustomExporter.IsRenderingSupported() == false)
            {
                var message = @"检测到当前 Revit 实例对数据导出的支持存在问题, 原因可能是材质库未正确安装。 本次操作可能无法成功执行, 确定要继续吗?";
                if (MessageBox.Show(this, message, Text,
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }
#endif

            if (File.Exists(filePath))
            {
                var message = @"输出路径目标文件已存在, 导出操作会覆盖原有的文件, 确定要继续吗?";
                if (MessageBox.Show(this, message, Text,
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
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

            SetFeature(FeatureType.Export2DViewAll, rb2DViewsAll.Checked);
            SetFeature(FeatureType.Export2DViewOnlySheet, rb2DViewsOnlySheet.Checked);

            SetFeature(FeatureType.GenerateThumbnail, cbGenerateThumbnail.Checked);
            SetFeature(FeatureType.GenerateElementData, cbGeneratePropDbJson.Checked);
            SetFeature(FeatureType.GenerateModelsDb, cbGeneratePropDbSqlite.Checked);

            SetFeature(FeatureType.ExportGrids, cbIncludeGrids.Checked);
            SetFeature(FeatureType.ExportRooms, cbIncludeRooms.Checked);

            SetFeature(FeatureType.ExcludeProperties, cbExcludeElementProperties.Checked);
            SetFeature(FeatureType.ExcludeLines, cbExcludeLines.Checked);
            SetFeature(FeatureType.ExcludePoints, cbExcludeModelPoints.Checked);
            SetFeature(FeatureType.OnlySelected, cbExcludeUnselectedElements.Checked);

            SetFeature(FeatureType.ConsolidateGroup, cbConsolidateArrayGroup.Checked);
            SetFeature(FeatureType.ConsolidateAssembly, cbConsolidateAssembly.Checked);

            SetFeature(FeatureType.UseLevelCategory, rbGroupByLevelDefault.Checked);
            SetFeature(FeatureType.UseNwLevelCategory, rbGroupByLevelNavisworks.Checked);
            SetFeature(FeatureType.UseBoundLevelCategory, rbGroupByLevelBoundingBox.Checked);

            SetFeature(FeatureType.UseCurrentViewport, cbUseCurrentViewport.Checked);

            #endregion

            var isCanncelled = false;
            using (var session = App.CreateSession())
            {
                if (session.IsValid == false)
                {
                    App.ShowLicenseDialog(this);
                    return;
                }

                #region 保存设置

                var config = _Config.Local;
                config.Features = _Features.Where(x => x.Selected).Select(x => x.Type).ToList();
                config.LastTargetPath = txtTargetPath.Text;
                config.VisualStyle = visualStyle?.Key;
                config.LevelOfDetail = levelOfDetail?.Value ?? -1;
                _Config.Save();

                #endregion

                var sw = Stopwatch.StartNew();
                try
                {
                    this.Enabled = false;

                    var features = _Features.Where(x => x.Selected && x.Enabled).ToDictionary(x => x.Type, x => true);

                    using (var progress = new ProgressHelper(this, Strings.MessageExporting))
                    {
                        var cancellationToken = progress.GetCancellationToken();
                        StartExport(_UIDocument, _View, config, ExportType.Zip, null, features, false, progress.GetProgressCallback(), cancellationToken);
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
                catch (Autodesk.Revit.Exceptions.ExternalApplicationException)
                {
                    sw.Stop();
                    Debug.WriteLine(Strings.MessageOperationFailureAndElapsedTime, sw.Elapsed);

                    this.ShowMessageBox(Strings.MessageOperationFailureAndTryLater);

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

            if (isCanncelled == false)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 开始导出
        /// </summary>
        /// <param name="uidoc"></param>
        /// <param name="view"></param>
        /// <param name="localConfig"></param>
        /// <param name="exportType"></param>
        /// <param name="outputStream"></param>
        /// <param name="features"></param>
        /// <param name="useShareTexture"></param>
        /// <param name="progressCallback"></param>
        /// <param name="cancellationToken"></param>
        private void StartExport(UIDocument uidoc, View3D view, AppLocalConfig localConfig, ExportType exportType, Stream outputStream, Dictionary<FeatureType, bool> features, bool useShareTexture, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            using(var log = new RuntimeLog())
            {
                var config = new ExportConfig();
                config.TargetPath = localConfig.LastTargetPath;
                config.ExportType = exportType;
                config.UseShareTexture = useShareTexture;
                config.OutputStream = outputStream;
                config.Features = features ?? new Dictionary<FeatureType, bool>();
                config.Trace = log.Log;
                config.ElementIds = (features?.FirstOrDefault(x=>x.Key == FeatureType.OnlySelected).Value ?? false) 
                    ?  _ElementIds 
                    : null;
                config.LevelOfDetail = localConfig.LevelOfDetail;

                #region Add Plugin - CreatePropDb
                {
#if DEBUG
                    var cliPath = @"D:\work-forge-engine\src\Bimangle.ForgeEngine.Tools\CreatePropDbCLI\bin\Debug\CreatePropDbCLI.exe";
#else
                    var cliPath = Path.Combine(
                        App.GetHomePath(),
                        @"Tools",
                        @"CreatePropDb",
                        @"CreatePropDbCLI.exe");
#endif
                    if (File.Exists(cliPath))
                    {
                        config.Addins.Add(new ExportPlugin(
                            FeatureType.GenerateModelsDb,
                            cliPath,
                            new[] {@"-i", config.TargetPath}
                        ));
                    }
                }
                #endregion

                #region Add Plugin - CreateThumbnail
                {
#if DEBUG
                    var cliPath = @"D:\work-forge-engine\src\Bimangle.ForgeEngine.Tools\CreateThumbnailCLI\bin\Debug\CreateThumbnailCLI.exe";
#else
                    var cliPath = Path.Combine(
                        App.GetHomePath(),
                        @"Tools",
                        @"CreateThumbnail",
                        @"CreateThumbnailCLI.exe");
#endif
                    if (File.Exists(cliPath))
                    {
                        config.Addins.Add(new ExportPlugin(
                            FeatureType.GenerateThumbnail,
                            cliPath,
                            new[] { @"-i", config.TargetPath }
                        ));
                    }
                }
                #endregion

                Exporter.ExportToSvf(uidoc, view, config, x => progressCallback?.Invoke((int)x), cancellationToken);
            }
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            App.ShowLicenseDialog(this);
        }


        private void InitUI()
        {
            var config = _Config.Local;
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

            toolTip1.SetToolTip(cbUseCurrentViewport, Strings.FeatureDescriptionUseCurrentViewport);
            cbUseCurrentViewport.Checked = IsAllowFeature(FeatureType.UseCurrentViewport);

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
                var levelOfDetail = _LevelOfDetails.FirstOrDefault(x => x.Value == config.LevelOfDetail) ??
                                    _LevelOfDetailDefault;
                cbLevelOfDetail.SelectedItem = levelOfDetail;
            }
            #endregion

            #region 二维视图
            {
                toolTip1.SetToolTip(rb2DViewsAll, Strings.FeatureDescriptionExport2DViewAll);
                toolTip1.SetToolTip(rb2DViewsOnlySheet, Strings.FeatureDescriptionExport2DViewOnlySheet);

                if (IsAllowFeature(FeatureType.Export2DViewAll))
                {
                    rb2DViewsAll.Checked = true;
                }
                else if (IsAllowFeature(FeatureType.Export2DViewOnlySheet))
                {
                    rb2DViewsOnlySheet.Checked = true;
                }
                else
                {
                    rb2DViewsBypass.Checked = true;
                }
            }
            #endregion

            #region 生成
            {
                toolTip1.SetToolTip(cbGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail);
                toolTip1.SetToolTip(cbGeneratePropDbJson, Strings.FeatureDescriptionGenerateElementData);
                toolTip1.SetToolTip(cbGeneratePropDbSqlite, Strings.FeatureDescriptionGenerateModelsDb);

                if (IsAllowFeature(FeatureType.GenerateThumbnail))
                {
                    cbGenerateThumbnail.Checked = true;
                }

                if (IsAllowFeature(FeatureType.GenerateElementData))
                {
                    cbGeneratePropDbJson.Checked = true;
                }

                if (IsAllowFeature(FeatureType.GenerateModelsDb))
                {
                    cbGeneratePropDbSqlite.Checked = true;
                }
            }
            #endregion

            #region 包含
            {
                toolTip1.SetToolTip(cbIncludeGrids, Strings.FeatureDescriptionExportGrids);
                toolTip1.SetToolTip(cbIncludeRooms, Strings.FeatureDescriptionExportRooms);

                if (IsAllowFeature(FeatureType.ExportGrids))
                {
                    cbIncludeGrids.Checked = true;
                }

                if (IsAllowFeature(FeatureType.ExportRooms))
                {
                    cbIncludeRooms.Checked = true;
                }
            }
            #endregion

            #region 排除
            {
                toolTip1.SetToolTip(cbExcludeElementProperties, Strings.FeatureDescriptionExcludeProperties);
                toolTip1.SetToolTip(cbExcludeLines, Strings.FeatureDescriptionExcludeLines);
                toolTip1.SetToolTip(cbExcludeModelPoints, Strings.FeatureDescriptionExcludePoints);
                toolTip1.SetToolTip(cbExcludeUnselectedElements, Strings.FeatureDescriptionOnlySelected);

                if (IsAllowFeature(FeatureType.ExcludeProperties))
                {
                    cbExcludeElementProperties.Checked = true;
                }

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

            #region 融合
            {
                toolTip1.SetToolTip(cbConsolidateArrayGroup, Strings.FeatureDescriptionConsolidateGroup);
                toolTip1.SetToolTip(cbConsolidateAssembly, Strings.FeatureDescriptionConsolidateAssembly);

                if (IsAllowFeature(FeatureType.ConsolidateGroup))
                {
                    cbConsolidateArrayGroup.Checked = true;
                }

                if (IsAllowFeature(FeatureType.ConsolidateAssembly))
                {
                    cbConsolidateAssembly.Checked = true;
                }
            }
            #endregion

            #region 按楼层分组
            {
                toolTip1.SetToolTip(rbGroupByLevelDefault, Strings.FeatureDescriptionUseLevelCategory);
                toolTip1.SetToolTip(rbGroupByLevelNavisworks, Strings.FeatureDescriptionUseNwLevelCategory);
                toolTip1.SetToolTip(rbGroupByLevelBoundingBox, Strings.FeatureDescriptionUseBoundLevelCategory);

                if (IsAllowFeature(FeatureType.UseLevelCategory))
                {
                    rbGroupByLevelDefault.Checked = true;
                }
                else if (IsAllowFeature(FeatureType.UseNwLevelCategory))
                {
                    rbGroupByLevelNavisworks.Checked = true;
                }
                else if (IsAllowFeature(FeatureType.UseBoundLevelCategory))
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

        private void btnResetOptions_Click(object sender, EventArgs e)
        {
            cbVisualStyle.SelectedItem = _VisualStyleDefault;
            cbLevelOfDetail.SelectedItem = _LevelOfDetailDefault;

            rb2DViewsOnlySheet.Checked = true;

            cbGenerateThumbnail.Checked = true;
            cbGeneratePropDbSqlite.Checked = true;
            cbGeneratePropDbJson.Checked = false;

            cbIncludeGrids.Checked = false;
            cbIncludeRooms.Checked = false;

            cbExcludeElementProperties.Checked = false;
            cbExcludeLines.Checked = false;
            cbExcludeModelPoints.Checked = false;
            cbExcludeUnselectedElements.Checked = false;

            cbConsolidateArrayGroup.Checked = false;
            cbConsolidateAssembly.Checked = false;

            rbGroupByLevelDisable.Checked = true;

            cbUseCurrentViewport.Checked = false;
        }
    }
}
