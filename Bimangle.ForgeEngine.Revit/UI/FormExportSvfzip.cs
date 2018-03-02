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
using Form = System.Windows.Forms.Form;

namespace Bimangle.ForgeEngine.Revit.UI
{
    partial class FormExportSvfzip : Form
    {
        private readonly UIDocument _UIDoc;
        private readonly View3D _View;
        private readonly AppConfig _Config;
        private readonly Dictionary<int, bool> _ElementIds;
        private readonly List<FeatureInfo> _Features;

        public TimeSpan ExportDuration { get; private set; }

        public FormExportSvfzip()
        {
            InitializeComponent();
        }

        public FormExportSvfzip(UIDocument uidoc, View3D view, AppConfig config, Dictionary<int, bool> elementIds)
            : this()
        {
            _UIDoc = uidoc;
            _View = view;
            _Config = config;
            _ElementIds = elementIds;

            _Features = new List<FeatureInfo>
            {
                new FeatureInfo(FeatureType.ExcludeProperties, Strings.FeatureNameExcludeProperties, Strings.FeatureDescriptionExcludeProperties),
                new FeatureInfo(FeatureType.ExcludeTexture, Strings.FeatureNameExcludeTexture, Strings.FeatureDescriptionExcludeTexture),
                new FeatureInfo(FeatureType.ExcludeLines, Strings.FeatureNameExcludeLines, Strings.FeatureDescriptionExcludeLines),
                new FeatureInfo(FeatureType.ExcludePoints, Strings.FeatureNameExcludePoints, Strings.FeatureDescriptionExcludePoints),
                new FeatureInfo(FeatureType.UseLevelCategory, Strings.FeatureNameUseLevelCategory, Strings.FeatureDescriptionUseLevelCategory),
                new FeatureInfo(FeatureType.UseNwLevelCategory, Strings.FeatureNameUseNwLevelCategory, Strings.FeatureDescriptionUseNwLevelCategory),
                new FeatureInfo(FeatureType.UseBoundLevelCategory, Strings.FeatureNameUseBoundLevelCategory, Strings.FeatureDescriptionUseBoundLevelCategory),
                new FeatureInfo(FeatureType.OnlySelected, Strings.FeatureNameOnlySelected, Strings.FeatureDescriptionOnlySelected),
                new FeatureInfo(FeatureType.GenerateElementData, Strings.FeatureNameGenerateElementData, Strings.FeatureDescriptionGenerateElementData),
                new FeatureInfo(FeatureType.ExportGrids, Strings.FeatureNameExportGrids, Strings.FeatureDescriptionExportGrids),
                new FeatureInfo(FeatureType.ExportRooms, Strings.FeatureNameExportRooms, Strings.FeatureDescriptionExportRooms),
                new FeatureInfo(FeatureType.ConsolidateGroup, Strings.FeatureNameConsolidateGroup, Strings.FeatureDescriptionConsolidateGroup),
                new FeatureInfo(FeatureType.ConsolidateAssembly, Strings.FeatureNameConsolidateAssembly, Strings.FeatureDescriptionConsolidateAssembly),
                new FeatureInfo(FeatureType.Wireframe, Strings.FeatureNameWireframe, Strings.FeatureDescriptionWireframe),
                new FeatureInfo(FeatureType.GenerateModelsDb, Strings.FeatureNameGenerateModelsDb, Strings.FeatureDescriptionGenerateModelsDb),
                new FeatureInfo(FeatureType.GenerateThumbnail, Strings.FeatureNameGenerateThumbnail, Strings.FeatureDescriptionGenerateThumbnail),
                new FeatureInfo(FeatureType.UseCurrentViewport, Strings.FeatureNameUseCurrentViewport, Strings.FeatureDescriptionUseCurrentViewport),
                new FeatureInfo(FeatureType.UseViewOverrideGraphic, Strings.FeatureNameUseViewOverrideGraphic, Strings.FeatureDescriptionUseViewOverrideGraphic),
                new FeatureInfo(FeatureType.Export2DViewOnlySheet, Strings.FeatureNameExport2DViewOnlySheet, Strings.FeatureDescriptionExport2DViewOnlySheet),
                new FeatureInfo(FeatureType.Export2DViewAll, Strings.FeatureNameExport2DViewAll, Strings.FeatureDescriptionExport2DViewAll),

            };
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            Text += $@" - {Command.TITLE}";

            var config = _Config.Local;
            txtTargetPath.Text = config.LastTargetPath;

            InitFeatureList();
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            var filePath = txtTargetPath.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show(Strings.MessageSelectOutputPathFirst, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (ListViewItem item in lvFeatures.Items)
            {
                var feature = item.Tag as FeatureInfo;
                feature?.ChangeSelected(_Features, item.Checked);
            }

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
                _Config.Save();

                #endregion

                var sw = Stopwatch.StartNew();
                try
                {
                    this.Enabled = false;

                    var features = _Features.Where(x => x.Selected && x.Enabled).ToDictionary(x => x.Type, x => true);

                    using (new ProgressHelper(this, Strings.MessageExporting))
                    {
                        StartExport(_UIDoc, _View, config.LastTargetPath, ExportType.Zip, null, features, false);
                    }

                    sw.Stop();
                    var ts = sw.Elapsed;
                    ExportDuration = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds); //去掉毫秒部分

                    Debug.WriteLine(Strings.MessageOperationSuccessAndElapsedTime, ExportDuration);

                    this.ShowMessageBox(string.Format(Strings.MessageExportSuccess, ExportDuration));

                    DialogResult = DialogResult.OK;
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


            Close();
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
        /// <param name="targetPath"></param>
        /// <param name="exportType"></param>
        /// <param name="outputStream"></param>
        /// <param name="features"></param>
        /// <param name="useShareTexture"></param>
        private void StartExport(UIDocument uidoc, View3D view, string targetPath, ExportType exportType, Stream outputStream, Dictionary<FeatureType, bool> features, bool useShareTexture)
        {
            using(var log = new RuntimeLog())
            {
                var config = new ExportConfig();
                config.TargetPath = targetPath;
                config.ExportType = exportType;
                config.UseShareTexture = useShareTexture;
                config.OutputStream = outputStream;
                config.Features = features ?? new Dictionary<FeatureType, bool>();
                config.Trace = log.Log;
                config.ElementIds = (features?.FirstOrDefault(x=>x.Key == FeatureType.OnlySelected).Value ?? false) 
                    ?  _ElementIds 
                    : null;

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

                #region Add Plugin - CreateThumbnail
                {
                    var cliPath = Path.Combine(
                        App.GetHomePath(),
                        @"Tools",
                        @"CreateThumbnail",
                        @"CreateThumbnailCLI.exe");
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

                Exporter.ExportToSvf(uidoc, view, config);
            }
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            App.ShowLicenseDialog(this);
        }

        private void InitFeatureList()
        {
            var config = _Config.Local;
            if (config.Features != null && config.Features.Count > 0)
            {
                foreach (var featureType in config.Features)
                {
                    _Features.FirstOrDefault(x=>x.Type == featureType)?.ChangeSelected(_Features, true);
                }
            }

            lvFeatures.Items.Clear();
            foreach (var feature in _Features)
            {
                var item = lvFeatures.Items.Add(new ListViewItem(new string[] {feature.Title, feature.Description}));
                item.Checked = feature.Selected;
                item.Tag = feature;
            }

        }
    }
}
