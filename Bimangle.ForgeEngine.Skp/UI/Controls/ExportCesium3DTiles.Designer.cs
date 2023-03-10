namespace Bimangle.ForgeEngine.Skp.UI.Controls
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
            this.gb3DTiles = new System.Windows.Forms.GroupBox();
            this.btnGeoreferncingConfig = new System.Windows.Forms.Button();
            this.txtGeoreferencingInfo = new System.Windows.Forms.TextBox();
            this.lblGeoreferncing = new System.Windows.Forms.Label();
            this.cbContentType = new System.Windows.Forms.ComboBox();
            this.lblContents = new System.Windows.Forms.Label();
            this.gbAdvanced = new System.Windows.Forms.GroupBox();
            this.cbForEarthSdk = new System.Windows.Forms.CheckBox();
            this.cbGeometryCompressTypes = new System.Windows.Forms.ComboBox();
            this.cbEnableGeometryCompress = new System.Windows.Forms.CheckBox();
            this.cbTextureCompressTypes = new System.Windows.Forms.ComboBox();
            this.cbEnableUnlitMaterials = new System.Windows.Forms.CheckBox();
            this.cbEnableTextureCompress = new System.Windows.Forms.CheckBox();
            this.cbExportSvfzip = new System.Windows.Forms.CheckBox();
            this.cbGeneratePropDbSqlite = new System.Windows.Forms.CheckBox();
            this.cbGenerateThumbnail = new System.Windows.Forms.CheckBox();
            this.gpExclude = new System.Windows.Forms.GroupBox();
            this.cbExcludeUnselectedElements = new System.Windows.Forms.CheckBox();
            this.cbExcludeModelPoints = new System.Windows.Forms.CheckBox();
            this.cbExcludeLines = new System.Windows.Forms.CheckBox();
            this.gpGeneral = new System.Windows.Forms.GroupBox();
            this.cbGenerateOutline = new System.Windows.Forms.CheckBox();
            this.lblGenerate = new System.Windows.Forms.Label();
            this.cbVisualStyle = new System.Windows.Forms.ComboBox();
            this.lblVisualStyle = new System.Windows.Forms.Label();
            this.cbLevelOfDetail = new System.Windows.Forms.ComboBox();
            this.lblLevelOfDetail = new System.Windows.Forms.Label();
            this.lblOptions = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.gb3DTiles.SuspendLayout();
            this.gbAdvanced.SuspendLayout();
            this.gpExclude.SuspendLayout();
            this.gpGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb3DTiles
            // 
            resources.ApplyResources(this.gb3DTiles, "gb3DTiles");
            this.gb3DTiles.Controls.Add(this.btnGeoreferncingConfig);
            this.gb3DTiles.Controls.Add(this.txtGeoreferencingInfo);
            this.gb3DTiles.Controls.Add(this.lblGeoreferncing);
            this.gb3DTiles.Controls.Add(this.cbContentType);
            this.gb3DTiles.Controls.Add(this.lblContents);
            this.gb3DTiles.Name = "gb3DTiles";
            this.gb3DTiles.TabStop = false;
            this.toolTip1.SetToolTip(this.gb3DTiles, resources.GetString("gb3DTiles.ToolTip"));
            // 
            // btnGeoreferncingConfig
            // 
            resources.ApplyResources(this.btnGeoreferncingConfig, "btnGeoreferncingConfig");
            this.btnGeoreferncingConfig.Name = "btnGeoreferncingConfig";
            this.toolTip1.SetToolTip(this.btnGeoreferncingConfig, resources.GetString("btnGeoreferncingConfig.ToolTip"));
            this.btnGeoreferncingConfig.UseVisualStyleBackColor = true;
            this.btnGeoreferncingConfig.Click += new System.EventHandler(this.btnGeoreferncingConfig_Click);
            // 
            // txtGeoreferencingInfo
            // 
            resources.ApplyResources(this.txtGeoreferencingInfo, "txtGeoreferencingInfo");
            this.txtGeoreferencingInfo.HideSelection = false;
            this.txtGeoreferencingInfo.Name = "txtGeoreferencingInfo";
            this.txtGeoreferencingInfo.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtGeoreferencingInfo, resources.GetString("txtGeoreferencingInfo.ToolTip"));
            // 
            // lblGeoreferncing
            // 
            resources.ApplyResources(this.lblGeoreferncing, "lblGeoreferncing");
            this.lblGeoreferncing.Name = "lblGeoreferncing";
            this.toolTip1.SetToolTip(this.lblGeoreferncing, resources.GetString("lblGeoreferncing.ToolTip"));
            // 
            // cbContentType
            // 
            resources.ApplyResources(this.cbContentType, "cbContentType");
            this.cbContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContentType.FormattingEnabled = true;
            this.cbContentType.Name = "cbContentType";
            this.toolTip1.SetToolTip(this.cbContentType, resources.GetString("cbContentType.ToolTip"));
            // 
            // lblContents
            // 
            resources.ApplyResources(this.lblContents, "lblContents");
            this.lblContents.Name = "lblContents";
            this.toolTip1.SetToolTip(this.lblContents, resources.GetString("lblContents.ToolTip"));
            // 
            // gbAdvanced
            // 
            resources.ApplyResources(this.gbAdvanced, "gbAdvanced");
            this.gbAdvanced.Controls.Add(this.cbForEarthSdk);
            this.gbAdvanced.Controls.Add(this.cbGeometryCompressTypes);
            this.gbAdvanced.Controls.Add(this.cbEnableGeometryCompress);
            this.gbAdvanced.Controls.Add(this.cbTextureCompressTypes);
            this.gbAdvanced.Controls.Add(this.cbEnableUnlitMaterials);
            this.gbAdvanced.Controls.Add(this.cbEnableTextureCompress);
            this.gbAdvanced.Controls.Add(this.cbExportSvfzip);
            this.gbAdvanced.Controls.Add(this.cbGeneratePropDbSqlite);
            this.gbAdvanced.Name = "gbAdvanced";
            this.gbAdvanced.TabStop = false;
            this.toolTip1.SetToolTip(this.gbAdvanced, resources.GetString("gbAdvanced.ToolTip"));
            // 
            // cbForEarthSdk
            // 
            resources.ApplyResources(this.cbForEarthSdk, "cbForEarthSdk");
            this.cbForEarthSdk.Name = "cbForEarthSdk";
            this.toolTip1.SetToolTip(this.cbForEarthSdk, resources.GetString("cbForEarthSdk.ToolTip"));
            this.cbForEarthSdk.UseVisualStyleBackColor = true;
            // 
            // cbGeometryCompressTypes
            // 
            resources.ApplyResources(this.cbGeometryCompressTypes, "cbGeometryCompressTypes");
            this.cbGeometryCompressTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGeometryCompressTypes.FormattingEnabled = true;
            this.cbGeometryCompressTypes.Name = "cbGeometryCompressTypes";
            this.toolTip1.SetToolTip(this.cbGeometryCompressTypes, resources.GetString("cbGeometryCompressTypes.ToolTip"));
            // 
            // cbEnableGeometryCompress
            // 
            resources.ApplyResources(this.cbEnableGeometryCompress, "cbEnableGeometryCompress");
            this.cbEnableGeometryCompress.Name = "cbEnableGeometryCompress";
            this.toolTip1.SetToolTip(this.cbEnableGeometryCompress, resources.GetString("cbEnableGeometryCompress.ToolTip"));
            this.cbEnableGeometryCompress.UseVisualStyleBackColor = true;
            // 
            // cbTextureCompressTypes
            // 
            resources.ApplyResources(this.cbTextureCompressTypes, "cbTextureCompressTypes");
            this.cbTextureCompressTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextureCompressTypes.FormattingEnabled = true;
            this.cbTextureCompressTypes.Name = "cbTextureCompressTypes";
            this.toolTip1.SetToolTip(this.cbTextureCompressTypes, resources.GetString("cbTextureCompressTypes.ToolTip"));
            // 
            // cbEnableUnlitMaterials
            // 
            resources.ApplyResources(this.cbEnableUnlitMaterials, "cbEnableUnlitMaterials");
            this.cbEnableUnlitMaterials.Name = "cbEnableUnlitMaterials";
            this.toolTip1.SetToolTip(this.cbEnableUnlitMaterials, resources.GetString("cbEnableUnlitMaterials.ToolTip"));
            this.cbEnableUnlitMaterials.UseVisualStyleBackColor = true;
            // 
            // cbEnableTextureCompress
            // 
            resources.ApplyResources(this.cbEnableTextureCompress, "cbEnableTextureCompress");
            this.cbEnableTextureCompress.Name = "cbEnableTextureCompress";
            this.toolTip1.SetToolTip(this.cbEnableTextureCompress, resources.GetString("cbEnableTextureCompress.ToolTip"));
            this.cbEnableTextureCompress.UseVisualStyleBackColor = true;
            // 
            // cbExportSvfzip
            // 
            resources.ApplyResources(this.cbExportSvfzip, "cbExportSvfzip");
            this.cbExportSvfzip.Name = "cbExportSvfzip";
            this.toolTip1.SetToolTip(this.cbExportSvfzip, resources.GetString("cbExportSvfzip.ToolTip"));
            this.cbExportSvfzip.UseVisualStyleBackColor = true;
            // 
            // cbGeneratePropDbSqlite
            // 
            resources.ApplyResources(this.cbGeneratePropDbSqlite, "cbGeneratePropDbSqlite");
            this.cbGeneratePropDbSqlite.Name = "cbGeneratePropDbSqlite";
            this.toolTip1.SetToolTip(this.cbGeneratePropDbSqlite, resources.GetString("cbGeneratePropDbSqlite.ToolTip"));
            this.cbGeneratePropDbSqlite.UseVisualStyleBackColor = true;
            // 
            // cbGenerateThumbnail
            // 
            resources.ApplyResources(this.cbGenerateThumbnail, "cbGenerateThumbnail");
            this.cbGenerateThumbnail.Name = "cbGenerateThumbnail";
            this.toolTip1.SetToolTip(this.cbGenerateThumbnail, resources.GetString("cbGenerateThumbnail.ToolTip"));
            this.cbGenerateThumbnail.UseVisualStyleBackColor = true;
            // 
            // gpExclude
            // 
            resources.ApplyResources(this.gpExclude, "gpExclude");
            this.gpExclude.Controls.Add(this.cbExcludeUnselectedElements);
            this.gpExclude.Controls.Add(this.cbExcludeModelPoints);
            this.gpExclude.Controls.Add(this.cbExcludeLines);
            this.gpExclude.Name = "gpExclude";
            this.gpExclude.TabStop = false;
            this.toolTip1.SetToolTip(this.gpExclude, resources.GetString("gpExclude.ToolTip"));
            // 
            // cbExcludeUnselectedElements
            // 
            resources.ApplyResources(this.cbExcludeUnselectedElements, "cbExcludeUnselectedElements");
            this.cbExcludeUnselectedElements.Name = "cbExcludeUnselectedElements";
            this.toolTip1.SetToolTip(this.cbExcludeUnselectedElements, resources.GetString("cbExcludeUnselectedElements.ToolTip"));
            this.cbExcludeUnselectedElements.UseVisualStyleBackColor = true;
            // 
            // cbExcludeModelPoints
            // 
            resources.ApplyResources(this.cbExcludeModelPoints, "cbExcludeModelPoints");
            this.cbExcludeModelPoints.Name = "cbExcludeModelPoints";
            this.toolTip1.SetToolTip(this.cbExcludeModelPoints, resources.GetString("cbExcludeModelPoints.ToolTip"));
            this.cbExcludeModelPoints.UseVisualStyleBackColor = true;
            // 
            // cbExcludeLines
            // 
            resources.ApplyResources(this.cbExcludeLines, "cbExcludeLines");
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
            this.gpGeneral.Name = "gpGeneral";
            this.gpGeneral.TabStop = false;
            this.toolTip1.SetToolTip(this.gpGeneral, resources.GetString("gpGeneral.ToolTip"));
            // 
            // cbGenerateOutline
            // 
            resources.ApplyResources(this.cbGenerateOutline, "cbGenerateOutline");
            this.cbGenerateOutline.Name = "cbGenerateOutline";
            this.toolTip1.SetToolTip(this.cbGenerateOutline, resources.GetString("cbGenerateOutline.ToolTip"));
            this.cbGenerateOutline.UseVisualStyleBackColor = true;
            // 
            // lblGenerate
            // 
            resources.ApplyResources(this.lblGenerate, "lblGenerate");
            this.lblGenerate.Name = "lblGenerate";
            this.toolTip1.SetToolTip(this.lblGenerate, resources.GetString("lblGenerate.ToolTip"));
            // 
            // cbVisualStyle
            // 
            resources.ApplyResources(this.cbVisualStyle, "cbVisualStyle");
            this.cbVisualStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVisualStyle.FormattingEnabled = true;
            this.cbVisualStyle.Name = "cbVisualStyle";
            this.toolTip1.SetToolTip(this.cbVisualStyle, resources.GetString("cbVisualStyle.ToolTip"));
            this.cbVisualStyle.SelectedIndexChanged += new System.EventHandler(this.cbVisualStyle_SelectedIndexChanged);
            // 
            // lblVisualStyle
            // 
            resources.ApplyResources(this.lblVisualStyle, "lblVisualStyle");
            this.lblVisualStyle.Name = "lblVisualStyle";
            this.toolTip1.SetToolTip(this.lblVisualStyle, resources.GetString("lblVisualStyle.ToolTip"));
            // 
            // cbLevelOfDetail
            // 
            resources.ApplyResources(this.cbLevelOfDetail, "cbLevelOfDetail");
            this.cbLevelOfDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevelOfDetail.FormattingEnabled = true;
            this.cbLevelOfDetail.Name = "cbLevelOfDetail";
            this.toolTip1.SetToolTip(this.cbLevelOfDetail, resources.GetString("cbLevelOfDetail.ToolTip"));
            // 
            // lblLevelOfDetail
            // 
            resources.ApplyResources(this.lblLevelOfDetail, "lblLevelOfDetail");
            this.lblLevelOfDetail.Name = "lblLevelOfDetail";
            this.toolTip1.SetToolTip(this.lblLevelOfDetail, resources.GetString("lblLevelOfDetail.ToolTip"));
            // 
            // lblOptions
            // 
            resources.ApplyResources(this.lblOptions, "lblOptions");
            this.lblOptions.Name = "lblOptions";
            this.toolTip1.SetToolTip(this.lblOptions, resources.GetString("lblOptions.ToolTip"));
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.toolTip1.SetToolTip(this.btnBrowse, resources.GetString("btnBrowse.ToolTip"));
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtTargetPath
            // 
            resources.ApplyResources(this.txtTargetPath, "txtTargetPath");
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtTargetPath, resources.GetString("txtTargetPath.ToolTip"));
            // 
            // lblOutputPath
            // 
            resources.ApplyResources(this.lblOutputPath, "lblOutputPath");
            this.lblOutputPath.Name = "lblOutputPath";
            this.toolTip1.SetToolTip(this.lblOutputPath, resources.GetString("lblOutputPath.ToolTip"));
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // ExportCesium3DTiles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblOptions);
            this.Controls.Add(this.gpGeneral);
            this.Controls.Add(this.gpExclude);
            this.Controls.Add(this.gbAdvanced);
            this.Controls.Add(this.gb3DTiles);
            this.Name = "ExportCesium3DTiles";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.gb3DTiles.ResumeLayout(false);
            this.gb3DTiles.PerformLayout();
            this.gbAdvanced.ResumeLayout(false);
            this.gbAdvanced.PerformLayout();
            this.gpExclude.ResumeLayout(false);
            this.gpExclude.PerformLayout();
            this.gpGeneral.ResumeLayout(false);
            this.gpGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.CheckBox cbGenerateThumbnail;
        private System.Windows.Forms.CheckBox cbEnableUnlitMaterials;
        private System.Windows.Forms.Label lblGenerate;
        private System.Windows.Forms.CheckBox cbGenerateOutline;
        private System.Windows.Forms.Button btnGeoreferncingConfig;
        private System.Windows.Forms.TextBox txtGeoreferencingInfo;
        private System.Windows.Forms.Label lblGeoreferncing;
        private System.Windows.Forms.ComboBox cbContentType;
        private System.Windows.Forms.Label lblContents;
        private System.Windows.Forms.ComboBox cbTextureCompressTypes;
        private System.Windows.Forms.CheckBox cbForEarthSdk;
        private System.Windows.Forms.ComboBox cbGeometryCompressTypes;
        private System.Windows.Forms.CheckBox cbEnableGeometryCompress;
    }
}