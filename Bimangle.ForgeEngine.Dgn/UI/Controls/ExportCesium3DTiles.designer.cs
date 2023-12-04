namespace Bimangle.ForgeEngine.Dgn.UI.Controls
{
    partial class ExportCesium3DTiles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportCesium3DTiles));
            this.gpContainer = new System.Windows.Forms.GroupBox();
            this.gb3DTiles = new System.Windows.Forms.GroupBox();
            this.btnGeoreferncingConfig = new System.Windows.Forms.Button();
            this.txtGeoreferencingInfo = new System.Windows.Forms.TextBox();
            this.lblGeoreferncing = new System.Windows.Forms.Label();
            this.cbContentType = new System.Windows.Forms.ComboBox();
            this.lblContents = new System.Windows.Forms.Label();
            this.gbAdvanced = new System.Windows.Forms.GroupBox();
            this.cbUse3DTilesSpecification11 = new System.Windows.Forms.CheckBox();
            this.cbForEarthSdk = new System.Windows.Forms.CheckBox();
            this.cbGeometryCompressTypes = new System.Windows.Forms.ComboBox();
            this.cbEnableGeometryCompress = new System.Windows.Forms.CheckBox();
            this.cbTextureCompressTypes = new System.Windows.Forms.ComboBox();
            this.cbEnableUnlitMaterials = new System.Windows.Forms.CheckBox();
            this.cbEnableTextureCompress = new System.Windows.Forms.CheckBox();
            this.cbExportSvfzip = new System.Windows.Forms.CheckBox();
            this.cbGeneratePropDbSqlite = new System.Windows.Forms.CheckBox();
            this.gpExclude = new System.Windows.Forms.GroupBox();
            this.cbExcludeUnselectedElements = new System.Windows.Forms.CheckBox();
            this.cbExcludeModelPoints = new System.Windows.Forms.CheckBox();
            this.cbExcludeLines = new System.Windows.Forms.CheckBox();
            this.gpGeneral = new System.Windows.Forms.GroupBox();
            this.cbGenerateOutline = new System.Windows.Forms.CheckBox();
            this.lblGenerate = new System.Windows.Forms.Label();
            this.cbVisualStyle = new System.Windows.Forms.ComboBox();
            this.cbGenerateThumbnail = new System.Windows.Forms.CheckBox();
            this.lblVisualStyle = new System.Windows.Forms.Label();
            this.cbLevelOfDetail = new System.Windows.Forms.ComboBox();
            this.lblLevelOfDetail = new System.Windows.Forms.Label();
            this.lblOptions = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpContainer.SuspendLayout();
            this.gb3DTiles.SuspendLayout();
            this.gbAdvanced.SuspendLayout();
            this.gpExclude.SuspendLayout();
            this.gpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpContainer
            // 
            resources.ApplyResources(this.gpContainer, "gpContainer");
            this.gpContainer.Controls.Add(this.gb3DTiles);
            this.gpContainer.Controls.Add(this.gbAdvanced);
            this.gpContainer.Controls.Add(this.gpExclude);
            this.gpContainer.Controls.Add(this.gpGeneral);
            this.gpContainer.Controls.Add(this.lblOptions);
            this.gpContainer.Controls.Add(this.btnBrowse);
            this.gpContainer.Controls.Add(this.txtTargetPath);
            this.gpContainer.Controls.Add(this.lblOutputPath);
            this.errorProvider1.SetError(this.gpContainer, resources.GetString("gpContainer.Error"));
            this.errorProvider1.SetIconAlignment(this.gpContainer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpContainer.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpContainer, ((int)(resources.GetObject("gpContainer.IconPadding"))));
            this.gpContainer.Name = "gpContainer";
            this.gpContainer.TabStop = false;
            this.toolTip1.SetToolTip(this.gpContainer, resources.GetString("gpContainer.ToolTip"));
            // 
            // gb3DTiles
            // 
            resources.ApplyResources(this.gb3DTiles, "gb3DTiles");
            this.gb3DTiles.Controls.Add(this.btnGeoreferncingConfig);
            this.gb3DTiles.Controls.Add(this.txtGeoreferencingInfo);
            this.gb3DTiles.Controls.Add(this.lblGeoreferncing);
            this.gb3DTiles.Controls.Add(this.cbContentType);
            this.gb3DTiles.Controls.Add(this.lblContents);
            this.errorProvider1.SetError(this.gb3DTiles, resources.GetString("gb3DTiles.Error"));
            this.errorProvider1.SetIconAlignment(this.gb3DTiles, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gb3DTiles.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gb3DTiles, ((int)(resources.GetObject("gb3DTiles.IconPadding"))));
            this.gb3DTiles.Name = "gb3DTiles";
            this.gb3DTiles.TabStop = false;
            this.toolTip1.SetToolTip(this.gb3DTiles, resources.GetString("gb3DTiles.ToolTip"));
            // 
            // btnGeoreferncingConfig
            // 
            resources.ApplyResources(this.btnGeoreferncingConfig, "btnGeoreferncingConfig");
            this.errorProvider1.SetError(this.btnGeoreferncingConfig, resources.GetString("btnGeoreferncingConfig.Error"));
            this.errorProvider1.SetIconAlignment(this.btnGeoreferncingConfig, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnGeoreferncingConfig.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnGeoreferncingConfig, ((int)(resources.GetObject("btnGeoreferncingConfig.IconPadding"))));
            this.btnGeoreferncingConfig.Name = "btnGeoreferncingConfig";
            this.toolTip1.SetToolTip(this.btnGeoreferncingConfig, resources.GetString("btnGeoreferncingConfig.ToolTip"));
            this.btnGeoreferncingConfig.UseVisualStyleBackColor = true;
            this.btnGeoreferncingConfig.Click += new System.EventHandler(this.btnGeoreferncingConfig_Click);
            // 
            // txtGeoreferencingInfo
            // 
            resources.ApplyResources(this.txtGeoreferencingInfo, "txtGeoreferencingInfo");
            this.errorProvider1.SetError(this.txtGeoreferencingInfo, resources.GetString("txtGeoreferencingInfo.Error"));
            this.txtGeoreferencingInfo.HideSelection = false;
            this.errorProvider1.SetIconAlignment(this.txtGeoreferencingInfo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtGeoreferencingInfo.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtGeoreferencingInfo, ((int)(resources.GetObject("txtGeoreferencingInfo.IconPadding"))));
            this.txtGeoreferencingInfo.Name = "txtGeoreferencingInfo";
            this.txtGeoreferencingInfo.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtGeoreferencingInfo, resources.GetString("txtGeoreferencingInfo.ToolTip"));
            // 
            // lblGeoreferncing
            // 
            resources.ApplyResources(this.lblGeoreferncing, "lblGeoreferncing");
            this.errorProvider1.SetError(this.lblGeoreferncing, resources.GetString("lblGeoreferncing.Error"));
            this.errorProvider1.SetIconAlignment(this.lblGeoreferncing, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblGeoreferncing.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblGeoreferncing, ((int)(resources.GetObject("lblGeoreferncing.IconPadding"))));
            this.lblGeoreferncing.Name = "lblGeoreferncing";
            this.toolTip1.SetToolTip(this.lblGeoreferncing, resources.GetString("lblGeoreferncing.ToolTip"));
            // 
            // cbContentType
            // 
            resources.ApplyResources(this.cbContentType, "cbContentType");
            this.cbContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbContentType, resources.GetString("cbContentType.Error"));
            this.cbContentType.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbContentType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbContentType.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbContentType, ((int)(resources.GetObject("cbContentType.IconPadding"))));
            this.cbContentType.Name = "cbContentType";
            this.toolTip1.SetToolTip(this.cbContentType, resources.GetString("cbContentType.ToolTip"));
            // 
            // lblContents
            // 
            resources.ApplyResources(this.lblContents, "lblContents");
            this.errorProvider1.SetError(this.lblContents, resources.GetString("lblContents.Error"));
            this.errorProvider1.SetIconAlignment(this.lblContents, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblContents.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblContents, ((int)(resources.GetObject("lblContents.IconPadding"))));
            this.lblContents.Name = "lblContents";
            this.toolTip1.SetToolTip(this.lblContents, resources.GetString("lblContents.ToolTip"));
            // 
            // gbAdvanced
            // 
            resources.ApplyResources(this.gbAdvanced, "gbAdvanced");
            this.gbAdvanced.Controls.Add(this.cbUse3DTilesSpecification11);
            this.gbAdvanced.Controls.Add(this.cbForEarthSdk);
            this.gbAdvanced.Controls.Add(this.cbGeometryCompressTypes);
            this.gbAdvanced.Controls.Add(this.cbEnableGeometryCompress);
            this.gbAdvanced.Controls.Add(this.cbTextureCompressTypes);
            this.gbAdvanced.Controls.Add(this.cbEnableUnlitMaterials);
            this.gbAdvanced.Controls.Add(this.cbEnableTextureCompress);
            this.gbAdvanced.Controls.Add(this.cbExportSvfzip);
            this.gbAdvanced.Controls.Add(this.cbGeneratePropDbSqlite);
            this.errorProvider1.SetError(this.gbAdvanced, resources.GetString("gbAdvanced.Error"));
            this.errorProvider1.SetIconAlignment(this.gbAdvanced, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gbAdvanced.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gbAdvanced, ((int)(resources.GetObject("gbAdvanced.IconPadding"))));
            this.gbAdvanced.Name = "gbAdvanced";
            this.gbAdvanced.TabStop = false;
            this.toolTip1.SetToolTip(this.gbAdvanced, resources.GetString("gbAdvanced.ToolTip"));
            // 
            // cbUse3DTilesSpecification11
            // 
            resources.ApplyResources(this.cbUse3DTilesSpecification11, "cbUse3DTilesSpecification11");
            this.errorProvider1.SetError(this.cbUse3DTilesSpecification11, resources.GetString("cbUse3DTilesSpecification11.Error"));
            this.errorProvider1.SetIconAlignment(this.cbUse3DTilesSpecification11, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbUse3DTilesSpecification11.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbUse3DTilesSpecification11, ((int)(resources.GetObject("cbUse3DTilesSpecification11.IconPadding"))));
            this.cbUse3DTilesSpecification11.Name = "cbUse3DTilesSpecification11";
            this.toolTip1.SetToolTip(this.cbUse3DTilesSpecification11, resources.GetString("cbUse3DTilesSpecification11.ToolTip"));
            this.cbUse3DTilesSpecification11.UseVisualStyleBackColor = true;
            // 
            // cbForEarthSdk
            // 
            resources.ApplyResources(this.cbForEarthSdk, "cbForEarthSdk");
            this.errorProvider1.SetError(this.cbForEarthSdk, resources.GetString("cbForEarthSdk.Error"));
            this.errorProvider1.SetIconAlignment(this.cbForEarthSdk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbForEarthSdk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbForEarthSdk, ((int)(resources.GetObject("cbForEarthSdk.IconPadding"))));
            this.cbForEarthSdk.Name = "cbForEarthSdk";
            this.toolTip1.SetToolTip(this.cbForEarthSdk, resources.GetString("cbForEarthSdk.ToolTip"));
            this.cbForEarthSdk.UseVisualStyleBackColor = true;
            // 
            // cbGeometryCompressTypes
            // 
            resources.ApplyResources(this.cbGeometryCompressTypes, "cbGeometryCompressTypes");
            this.cbGeometryCompressTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbGeometryCompressTypes, resources.GetString("cbGeometryCompressTypes.Error"));
            this.cbGeometryCompressTypes.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbGeometryCompressTypes, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbGeometryCompressTypes.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbGeometryCompressTypes, ((int)(resources.GetObject("cbGeometryCompressTypes.IconPadding"))));
            this.cbGeometryCompressTypes.Name = "cbGeometryCompressTypes";
            this.toolTip1.SetToolTip(this.cbGeometryCompressTypes, resources.GetString("cbGeometryCompressTypes.ToolTip"));
            // 
            // cbEnableGeometryCompress
            // 
            resources.ApplyResources(this.cbEnableGeometryCompress, "cbEnableGeometryCompress");
            this.errorProvider1.SetError(this.cbEnableGeometryCompress, resources.GetString("cbEnableGeometryCompress.Error"));
            this.errorProvider1.SetIconAlignment(this.cbEnableGeometryCompress, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnableGeometryCompress.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnableGeometryCompress, ((int)(resources.GetObject("cbEnableGeometryCompress.IconPadding"))));
            this.cbEnableGeometryCompress.Name = "cbEnableGeometryCompress";
            this.toolTip1.SetToolTip(this.cbEnableGeometryCompress, resources.GetString("cbEnableGeometryCompress.ToolTip"));
            this.cbEnableGeometryCompress.UseVisualStyleBackColor = true;
            // 
            // cbTextureCompressTypes
            // 
            resources.ApplyResources(this.cbTextureCompressTypes, "cbTextureCompressTypes");
            this.cbTextureCompressTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbTextureCompressTypes, resources.GetString("cbTextureCompressTypes.Error"));
            this.cbTextureCompressTypes.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbTextureCompressTypes, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbTextureCompressTypes.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbTextureCompressTypes, ((int)(resources.GetObject("cbTextureCompressTypes.IconPadding"))));
            this.cbTextureCompressTypes.Name = "cbTextureCompressTypes";
            this.toolTip1.SetToolTip(this.cbTextureCompressTypes, resources.GetString("cbTextureCompressTypes.ToolTip"));
            // 
            // cbEnableUnlitMaterials
            // 
            resources.ApplyResources(this.cbEnableUnlitMaterials, "cbEnableUnlitMaterials");
            this.errorProvider1.SetError(this.cbEnableUnlitMaterials, resources.GetString("cbEnableUnlitMaterials.Error"));
            this.errorProvider1.SetIconAlignment(this.cbEnableUnlitMaterials, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnableUnlitMaterials.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnableUnlitMaterials, ((int)(resources.GetObject("cbEnableUnlitMaterials.IconPadding"))));
            this.cbEnableUnlitMaterials.Name = "cbEnableUnlitMaterials";
            this.toolTip1.SetToolTip(this.cbEnableUnlitMaterials, resources.GetString("cbEnableUnlitMaterials.ToolTip"));
            this.cbEnableUnlitMaterials.UseVisualStyleBackColor = true;
            // 
            // cbEnableTextureCompress
            // 
            resources.ApplyResources(this.cbEnableTextureCompress, "cbEnableTextureCompress");
            this.errorProvider1.SetError(this.cbEnableTextureCompress, resources.GetString("cbEnableTextureCompress.Error"));
            this.errorProvider1.SetIconAlignment(this.cbEnableTextureCompress, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnableTextureCompress.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnableTextureCompress, ((int)(resources.GetObject("cbEnableTextureCompress.IconPadding"))));
            this.cbEnableTextureCompress.Name = "cbEnableTextureCompress";
            this.toolTip1.SetToolTip(this.cbEnableTextureCompress, resources.GetString("cbEnableTextureCompress.ToolTip"));
            this.cbEnableTextureCompress.UseVisualStyleBackColor = true;
            // 
            // cbExportSvfzip
            // 
            resources.ApplyResources(this.cbExportSvfzip, "cbExportSvfzip");
            this.errorProvider1.SetError(this.cbExportSvfzip, resources.GetString("cbExportSvfzip.Error"));
            this.errorProvider1.SetIconAlignment(this.cbExportSvfzip, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbExportSvfzip.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbExportSvfzip, ((int)(resources.GetObject("cbExportSvfzip.IconPadding"))));
            this.cbExportSvfzip.Name = "cbExportSvfzip";
            this.toolTip1.SetToolTip(this.cbExportSvfzip, resources.GetString("cbExportSvfzip.ToolTip"));
            this.cbExportSvfzip.UseVisualStyleBackColor = true;
            // 
            // cbGeneratePropDbSqlite
            // 
            resources.ApplyResources(this.cbGeneratePropDbSqlite, "cbGeneratePropDbSqlite");
            this.errorProvider1.SetError(this.cbGeneratePropDbSqlite, resources.GetString("cbGeneratePropDbSqlite.Error"));
            this.errorProvider1.SetIconAlignment(this.cbGeneratePropDbSqlite, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbGeneratePropDbSqlite.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbGeneratePropDbSqlite, ((int)(resources.GetObject("cbGeneratePropDbSqlite.IconPadding"))));
            this.cbGeneratePropDbSqlite.Name = "cbGeneratePropDbSqlite";
            this.toolTip1.SetToolTip(this.cbGeneratePropDbSqlite, resources.GetString("cbGeneratePropDbSqlite.ToolTip"));
            this.cbGeneratePropDbSqlite.UseVisualStyleBackColor = true;
            // 
            // gpExclude
            // 
            resources.ApplyResources(this.gpExclude, "gpExclude");
            this.gpExclude.Controls.Add(this.cbExcludeUnselectedElements);
            this.gpExclude.Controls.Add(this.cbExcludeModelPoints);
            this.gpExclude.Controls.Add(this.cbExcludeLines);
            this.errorProvider1.SetError(this.gpExclude, resources.GetString("gpExclude.Error"));
            this.errorProvider1.SetIconAlignment(this.gpExclude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpExclude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpExclude, ((int)(resources.GetObject("gpExclude.IconPadding"))));
            this.gpExclude.Name = "gpExclude";
            this.gpExclude.TabStop = false;
            this.toolTip1.SetToolTip(this.gpExclude, resources.GetString("gpExclude.ToolTip"));
            // 
            // cbExcludeUnselectedElements
            // 
            resources.ApplyResources(this.cbExcludeUnselectedElements, "cbExcludeUnselectedElements");
            this.errorProvider1.SetError(this.cbExcludeUnselectedElements, resources.GetString("cbExcludeUnselectedElements.Error"));
            this.errorProvider1.SetIconAlignment(this.cbExcludeUnselectedElements, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbExcludeUnselectedElements.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbExcludeUnselectedElements, ((int)(resources.GetObject("cbExcludeUnselectedElements.IconPadding"))));
            this.cbExcludeUnselectedElements.Name = "cbExcludeUnselectedElements";
            this.toolTip1.SetToolTip(this.cbExcludeUnselectedElements, resources.GetString("cbExcludeUnselectedElements.ToolTip"));
            this.cbExcludeUnselectedElements.UseVisualStyleBackColor = true;
            // 
            // cbExcludeModelPoints
            // 
            resources.ApplyResources(this.cbExcludeModelPoints, "cbExcludeModelPoints");
            this.errorProvider1.SetError(this.cbExcludeModelPoints, resources.GetString("cbExcludeModelPoints.Error"));
            this.errorProvider1.SetIconAlignment(this.cbExcludeModelPoints, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbExcludeModelPoints.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbExcludeModelPoints, ((int)(resources.GetObject("cbExcludeModelPoints.IconPadding"))));
            this.cbExcludeModelPoints.Name = "cbExcludeModelPoints";
            this.toolTip1.SetToolTip(this.cbExcludeModelPoints, resources.GetString("cbExcludeModelPoints.ToolTip"));
            this.cbExcludeModelPoints.UseVisualStyleBackColor = true;
            // 
            // cbExcludeLines
            // 
            resources.ApplyResources(this.cbExcludeLines, "cbExcludeLines");
            this.errorProvider1.SetError(this.cbExcludeLines, resources.GetString("cbExcludeLines.Error"));
            this.errorProvider1.SetIconAlignment(this.cbExcludeLines, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbExcludeLines.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbExcludeLines, ((int)(resources.GetObject("cbExcludeLines.IconPadding"))));
            this.cbExcludeLines.Name = "cbExcludeLines";
            this.toolTip1.SetToolTip(this.cbExcludeLines, resources.GetString("cbExcludeLines.ToolTip"));
            this.cbExcludeLines.UseVisualStyleBackColor = true;
            // 
            // gpGeneral
            // 
            resources.ApplyResources(this.gpGeneral, "gpGeneral");
            this.gpGeneral.Controls.Add(this.cbGenerateOutline);
            this.gpGeneral.Controls.Add(this.lblGenerate);
            this.gpGeneral.Controls.Add(this.cbVisualStyle);
            this.gpGeneral.Controls.Add(this.cbGenerateThumbnail);
            this.gpGeneral.Controls.Add(this.lblVisualStyle);
            this.gpGeneral.Controls.Add(this.cbLevelOfDetail);
            this.gpGeneral.Controls.Add(this.lblLevelOfDetail);
            this.errorProvider1.SetError(this.gpGeneral, resources.GetString("gpGeneral.Error"));
            this.errorProvider1.SetIconAlignment(this.gpGeneral, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpGeneral.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpGeneral, ((int)(resources.GetObject("gpGeneral.IconPadding"))));
            this.gpGeneral.Name = "gpGeneral";
            this.gpGeneral.TabStop = false;
            this.toolTip1.SetToolTip(this.gpGeneral, resources.GetString("gpGeneral.ToolTip"));
            // 
            // cbGenerateOutline
            // 
            resources.ApplyResources(this.cbGenerateOutline, "cbGenerateOutline");
            this.errorProvider1.SetError(this.cbGenerateOutline, resources.GetString("cbGenerateOutline.Error"));
            this.errorProvider1.SetIconAlignment(this.cbGenerateOutline, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbGenerateOutline.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbGenerateOutline, ((int)(resources.GetObject("cbGenerateOutline.IconPadding"))));
            this.cbGenerateOutline.Name = "cbGenerateOutline";
            this.toolTip1.SetToolTip(this.cbGenerateOutline, resources.GetString("cbGenerateOutline.ToolTip"));
            this.cbGenerateOutline.UseVisualStyleBackColor = true;
            // 
            // lblGenerate
            // 
            resources.ApplyResources(this.lblGenerate, "lblGenerate");
            this.errorProvider1.SetError(this.lblGenerate, resources.GetString("lblGenerate.Error"));
            this.errorProvider1.SetIconAlignment(this.lblGenerate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblGenerate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblGenerate, ((int)(resources.GetObject("lblGenerate.IconPadding"))));
            this.lblGenerate.Name = "lblGenerate";
            this.toolTip1.SetToolTip(this.lblGenerate, resources.GetString("lblGenerate.ToolTip"));
            // 
            // cbVisualStyle
            // 
            resources.ApplyResources(this.cbVisualStyle, "cbVisualStyle");
            this.cbVisualStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbVisualStyle, resources.GetString("cbVisualStyle.Error"));
            this.cbVisualStyle.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbVisualStyle, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbVisualStyle.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbVisualStyle, ((int)(resources.GetObject("cbVisualStyle.IconPadding"))));
            this.cbVisualStyle.Name = "cbVisualStyle";
            this.toolTip1.SetToolTip(this.cbVisualStyle, resources.GetString("cbVisualStyle.ToolTip"));
            this.cbVisualStyle.SelectedIndexChanged += new System.EventHandler(this.cbVisualStyle_SelectedIndexChanged);
            // 
            // cbGenerateThumbnail
            // 
            resources.ApplyResources(this.cbGenerateThumbnail, "cbGenerateThumbnail");
            this.errorProvider1.SetError(this.cbGenerateThumbnail, resources.GetString("cbGenerateThumbnail.Error"));
            this.errorProvider1.SetIconAlignment(this.cbGenerateThumbnail, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbGenerateThumbnail.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbGenerateThumbnail, ((int)(resources.GetObject("cbGenerateThumbnail.IconPadding"))));
            this.cbGenerateThumbnail.Name = "cbGenerateThumbnail";
            this.toolTip1.SetToolTip(this.cbGenerateThumbnail, resources.GetString("cbGenerateThumbnail.ToolTip"));
            this.cbGenerateThumbnail.UseVisualStyleBackColor = true;
            // 
            // lblVisualStyle
            // 
            resources.ApplyResources(this.lblVisualStyle, "lblVisualStyle");
            this.errorProvider1.SetError(this.lblVisualStyle, resources.GetString("lblVisualStyle.Error"));
            this.errorProvider1.SetIconAlignment(this.lblVisualStyle, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblVisualStyle.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblVisualStyle, ((int)(resources.GetObject("lblVisualStyle.IconPadding"))));
            this.lblVisualStyle.Name = "lblVisualStyle";
            this.toolTip1.SetToolTip(this.lblVisualStyle, resources.GetString("lblVisualStyle.ToolTip"));
            // 
            // cbLevelOfDetail
            // 
            resources.ApplyResources(this.cbLevelOfDetail, "cbLevelOfDetail");
            this.cbLevelOfDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbLevelOfDetail, resources.GetString("cbLevelOfDetail.Error"));
            this.cbLevelOfDetail.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbLevelOfDetail, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbLevelOfDetail.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbLevelOfDetail, ((int)(resources.GetObject("cbLevelOfDetail.IconPadding"))));
            this.cbLevelOfDetail.Name = "cbLevelOfDetail";
            this.toolTip1.SetToolTip(this.cbLevelOfDetail, resources.GetString("cbLevelOfDetail.ToolTip"));
            // 
            // lblLevelOfDetail
            // 
            resources.ApplyResources(this.lblLevelOfDetail, "lblLevelOfDetail");
            this.errorProvider1.SetError(this.lblLevelOfDetail, resources.GetString("lblLevelOfDetail.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLevelOfDetail, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLevelOfDetail.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLevelOfDetail, ((int)(resources.GetObject("lblLevelOfDetail.IconPadding"))));
            this.lblLevelOfDetail.Name = "lblLevelOfDetail";
            this.toolTip1.SetToolTip(this.lblLevelOfDetail, resources.GetString("lblLevelOfDetail.ToolTip"));
            // 
            // lblOptions
            // 
            resources.ApplyResources(this.lblOptions, "lblOptions");
            this.errorProvider1.SetError(this.lblOptions, resources.GetString("lblOptions.Error"));
            this.errorProvider1.SetIconAlignment(this.lblOptions, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblOptions.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblOptions, ((int)(resources.GetObject("lblOptions.IconPadding"))));
            this.lblOptions.Name = "lblOptions";
            this.toolTip1.SetToolTip(this.lblOptions, resources.GetString("lblOptions.ToolTip"));
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.errorProvider1.SetError(this.btnBrowse, resources.GetString("btnBrowse.Error"));
            this.errorProvider1.SetIconAlignment(this.btnBrowse, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnBrowse.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnBrowse, ((int)(resources.GetObject("btnBrowse.IconPadding"))));
            this.btnBrowse.Name = "btnBrowse";
            this.toolTip1.SetToolTip(this.btnBrowse, resources.GetString("btnBrowse.ToolTip"));
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtTargetPath
            // 
            resources.ApplyResources(this.txtTargetPath, "txtTargetPath");
            this.errorProvider1.SetError(this.txtTargetPath, resources.GetString("txtTargetPath.Error"));
            this.errorProvider1.SetIconAlignment(this.txtTargetPath, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtTargetPath.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtTargetPath, ((int)(resources.GetObject("txtTargetPath.IconPadding"))));
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtTargetPath, resources.GetString("txtTargetPath.ToolTip"));
            // 
            // lblOutputPath
            // 
            resources.ApplyResources(this.lblOutputPath, "lblOutputPath");
            this.errorProvider1.SetError(this.lblOutputPath, resources.GetString("lblOutputPath.Error"));
            this.errorProvider1.SetIconAlignment(this.lblOutputPath, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblOutputPath.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblOutputPath, ((int)(resources.GetObject("lblOutputPath.IconPadding"))));
            this.lblOutputPath.Name = "lblOutputPath";
            this.toolTip1.SetToolTip(this.lblOutputPath, resources.GetString("lblOutputPath.ToolTip"));
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // ExportCesium3DTiles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.gpContainer);
            this.errorProvider1.SetError(this, resources.GetString("$this.Error"));
            this.errorProvider1.SetIconAlignment(this, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("$this.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this, ((int)(resources.GetObject("$this.IconPadding"))));
            this.Name = "ExportCesium3DTiles";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.gpContainer.ResumeLayout(false);
            this.gpContainer.PerformLayout();
            this.gb3DTiles.ResumeLayout(false);
            this.gb3DTiles.PerformLayout();
            this.gbAdvanced.ResumeLayout(false);
            this.gbAdvanced.PerformLayout();
            this.gpExclude.ResumeLayout(false);
            this.gpExclude.PerformLayout();
            this.gpGeneral.ResumeLayout(false);
            this.gpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gpContainer;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.ComboBox cbVisualStyle;
        private System.Windows.Forms.Label lblVisualStyle;
        private System.Windows.Forms.ComboBox cbLevelOfDetail;
        private System.Windows.Forms.Label lblLevelOfDetail;
        private System.Windows.Forms.GroupBox gpGeneral;
        private System.Windows.Forms.GroupBox gpExclude;
        private System.Windows.Forms.CheckBox cbExcludeUnselectedElements;
        private System.Windows.Forms.CheckBox cbExcludeModelPoints;
        private System.Windows.Forms.CheckBox cbExcludeLines;
        private System.Windows.Forms.GroupBox gbAdvanced;
        private System.Windows.Forms.CheckBox cbGeneratePropDbSqlite;
        private System.Windows.Forms.CheckBox cbExportSvfzip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox gb3DTiles;
        private System.Windows.Forms.CheckBox cbEnableTextureCompress;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox cbGenerateThumbnail;
        private System.Windows.Forms.CheckBox cbEnableUnlitMaterials;
        private System.Windows.Forms.CheckBox cbGenerateOutline;
        private System.Windows.Forms.Label lblGenerate;
        private System.Windows.Forms.Button btnGeoreferncingConfig;
        private System.Windows.Forms.TextBox txtGeoreferencingInfo;
        private System.Windows.Forms.Label lblGeoreferncing;
        private System.Windows.Forms.ComboBox cbContentType;
        private System.Windows.Forms.Label lblContents;
        private System.Windows.Forms.ComboBox cbTextureCompressTypes;
        private System.Windows.Forms.CheckBox cbForEarthSdk;
        private System.Windows.Forms.ComboBox cbGeometryCompressTypes;
        private System.Windows.Forms.CheckBox cbEnableGeometryCompress;
        private System.Windows.Forms.CheckBox cbUse3DTilesSpecification11;
    }
}