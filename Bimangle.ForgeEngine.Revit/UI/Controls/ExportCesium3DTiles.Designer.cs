namespace Bimangle.ForgeEngine.Revit.UI.Controls
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
            this.cbEnableUnlitMaterials = new System.Windows.Forms.CheckBox();
            this.cbEnableTextureWebP = new System.Windows.Forms.CheckBox();
            this.cbEnableQuantizedAttributes = new System.Windows.Forms.CheckBox();
            this.cbExportSvfzip = new System.Windows.Forms.CheckBox();
            this.cbGeneratePropDbSqlite = new System.Windows.Forms.CheckBox();
            this.cbUseDraco = new System.Windows.Forms.CheckBox();
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
            this.gpContainer.Name = "gpContainer";
            this.gpContainer.TabStop = false;
            // 
            // gb3DTiles
            // 
            this.gb3DTiles.Controls.Add(this.btnGeoreferncingConfig);
            this.gb3DTiles.Controls.Add(this.txtGeoreferencingInfo);
            this.gb3DTiles.Controls.Add(this.lblGeoreferncing);
            this.gb3DTiles.Controls.Add(this.cbContentType);
            this.gb3DTiles.Controls.Add(this.lblContents);
            resources.ApplyResources(this.gb3DTiles, "gb3DTiles");
            this.gb3DTiles.Name = "gb3DTiles";
            this.gb3DTiles.TabStop = false;
            // 
            // btnGeoreferncingConfig
            // 
            resources.ApplyResources(this.btnGeoreferncingConfig, "btnGeoreferncingConfig");
            this.btnGeoreferncingConfig.Name = "btnGeoreferncingConfig";
            this.btnGeoreferncingConfig.UseVisualStyleBackColor = true;
            this.btnGeoreferncingConfig.Click += new System.EventHandler(this.btnGeoreferncingConfig_Click);
            // 
            // txtGeoreferencingInfo
            // 
            resources.ApplyResources(this.txtGeoreferencingInfo, "txtGeoreferencingInfo");
            this.txtGeoreferencingInfo.HideSelection = false;
            this.txtGeoreferencingInfo.Name = "txtGeoreferencingInfo";
            this.txtGeoreferencingInfo.ReadOnly = true;
            // 
            // lblGeoreferncing
            // 
            this.errorProvider1.SetIconAlignment(this.lblGeoreferncing, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblGeoreferncing.IconAlignment"))));
            resources.ApplyResources(this.lblGeoreferncing, "lblGeoreferncing");
            this.lblGeoreferncing.Name = "lblGeoreferncing";
            // 
            // cbContentType
            // 
            resources.ApplyResources(this.cbContentType, "cbContentType");
            this.cbContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContentType.FormattingEnabled = true;
            this.cbContentType.Name = "cbContentType";
            // 
            // lblContents
            // 
            this.errorProvider1.SetIconAlignment(this.lblContents, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblContents.IconAlignment"))));
            resources.ApplyResources(this.lblContents, "lblContents");
            this.lblContents.Name = "lblContents";
            // 
            // gbAdvanced
            // 
            this.gbAdvanced.Controls.Add(this.cbEnableUnlitMaterials);
            this.gbAdvanced.Controls.Add(this.cbEnableTextureWebP);
            this.gbAdvanced.Controls.Add(this.cbEnableQuantizedAttributes);
            this.gbAdvanced.Controls.Add(this.cbExportSvfzip);
            this.gbAdvanced.Controls.Add(this.cbGeneratePropDbSqlite);
            this.gbAdvanced.Controls.Add(this.cbUseDraco);
            resources.ApplyResources(this.gbAdvanced, "gbAdvanced");
            this.gbAdvanced.Name = "gbAdvanced";
            this.gbAdvanced.TabStop = false;
            // 
            // cbEnableUnlitMaterials
            // 
            resources.ApplyResources(this.cbEnableUnlitMaterials, "cbEnableUnlitMaterials");
            this.cbEnableUnlitMaterials.Name = "cbEnableUnlitMaterials";
            this.cbEnableUnlitMaterials.UseVisualStyleBackColor = true;
            // 
            // cbEnableTextureWebP
            // 
            resources.ApplyResources(this.cbEnableTextureWebP, "cbEnableTextureWebP");
            this.cbEnableTextureWebP.Name = "cbEnableTextureWebP";
            this.cbEnableTextureWebP.UseVisualStyleBackColor = true;
            // 
            // cbEnableQuantizedAttributes
            // 
            resources.ApplyResources(this.cbEnableQuantizedAttributes, "cbEnableQuantizedAttributes");
            this.cbEnableQuantizedAttributes.Name = "cbEnableQuantizedAttributes";
            this.cbEnableQuantizedAttributes.UseVisualStyleBackColor = true;
            // 
            // cbExportSvfzip
            // 
            resources.ApplyResources(this.cbExportSvfzip, "cbExportSvfzip");
            this.cbExportSvfzip.Name = "cbExportSvfzip";
            this.cbExportSvfzip.UseVisualStyleBackColor = true;
            // 
            // cbGeneratePropDbSqlite
            // 
            resources.ApplyResources(this.cbGeneratePropDbSqlite, "cbGeneratePropDbSqlite");
            this.cbGeneratePropDbSqlite.Name = "cbGeneratePropDbSqlite";
            this.cbGeneratePropDbSqlite.UseVisualStyleBackColor = true;
            // 
            // cbUseDraco
            // 
            resources.ApplyResources(this.cbUseDraco, "cbUseDraco");
            this.cbUseDraco.Name = "cbUseDraco";
            this.cbUseDraco.UseVisualStyleBackColor = true;
            // 
            // gpExclude
            // 
            this.gpExclude.Controls.Add(this.cbExcludeUnselectedElements);
            this.gpExclude.Controls.Add(this.cbExcludeModelPoints);
            this.gpExclude.Controls.Add(this.cbExcludeLines);
            resources.ApplyResources(this.gpExclude, "gpExclude");
            this.gpExclude.Name = "gpExclude";
            this.gpExclude.TabStop = false;
            // 
            // cbExcludeUnselectedElements
            // 
            resources.ApplyResources(this.cbExcludeUnselectedElements, "cbExcludeUnselectedElements");
            this.cbExcludeUnselectedElements.Name = "cbExcludeUnselectedElements";
            this.cbExcludeUnselectedElements.UseVisualStyleBackColor = true;
            // 
            // cbExcludeModelPoints
            // 
            resources.ApplyResources(this.cbExcludeModelPoints, "cbExcludeModelPoints");
            this.cbExcludeModelPoints.Name = "cbExcludeModelPoints";
            this.cbExcludeModelPoints.UseVisualStyleBackColor = true;
            // 
            // cbExcludeLines
            // 
            resources.ApplyResources(this.cbExcludeLines, "cbExcludeLines");
            this.cbExcludeLines.Name = "cbExcludeLines";
            this.cbExcludeLines.UseVisualStyleBackColor = true;
            // 
            // gpGeneral
            // 
            this.gpGeneral.Controls.Add(this.cbGenerateOutline);
            this.gpGeneral.Controls.Add(this.lblGenerate);
            this.gpGeneral.Controls.Add(this.cbVisualStyle);
            this.gpGeneral.Controls.Add(this.cbGenerateThumbnail);
            this.gpGeneral.Controls.Add(this.lblVisualStyle);
            this.gpGeneral.Controls.Add(this.cbLevelOfDetail);
            this.gpGeneral.Controls.Add(this.lblLevelOfDetail);
            resources.ApplyResources(this.gpGeneral, "gpGeneral");
            this.gpGeneral.Name = "gpGeneral";
            this.gpGeneral.TabStop = false;
            // 
            // cbGenerateOutline
            // 
            resources.ApplyResources(this.cbGenerateOutline, "cbGenerateOutline");
            this.errorProvider1.SetIconAlignment(this.cbGenerateOutline, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbGenerateOutline.IconAlignment"))));
            this.cbGenerateOutline.Name = "cbGenerateOutline";
            this.cbGenerateOutline.UseVisualStyleBackColor = true;
            // 
            // lblGenerate
            // 
            resources.ApplyResources(this.lblGenerate, "lblGenerate");
            this.errorProvider1.SetIconAlignment(this.lblGenerate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblGenerate.IconAlignment"))));
            this.lblGenerate.Name = "lblGenerate";
            // 
            // cbVisualStyle
            // 
            this.cbVisualStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVisualStyle.FormattingEnabled = true;
            resources.ApplyResources(this.cbVisualStyle, "cbVisualStyle");
            this.cbVisualStyle.Name = "cbVisualStyle";
            this.cbVisualStyle.SelectedIndexChanged += new System.EventHandler(this.cbVisualStyle_SelectedIndexChanged);
            // 
            // cbGenerateThumbnail
            // 
            resources.ApplyResources(this.cbGenerateThumbnail, "cbGenerateThumbnail");
            this.cbGenerateThumbnail.Name = "cbGenerateThumbnail";
            this.cbGenerateThumbnail.UseVisualStyleBackColor = true;
            // 
            // lblVisualStyle
            // 
            resources.ApplyResources(this.lblVisualStyle, "lblVisualStyle");
            this.lblVisualStyle.Name = "lblVisualStyle";
            // 
            // cbLevelOfDetail
            // 
            this.cbLevelOfDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevelOfDetail.FormattingEnabled = true;
            resources.ApplyResources(this.cbLevelOfDetail, "cbLevelOfDetail");
            this.cbLevelOfDetail.Name = "cbLevelOfDetail";
            // 
            // lblLevelOfDetail
            // 
            resources.ApplyResources(this.lblLevelOfDetail, "lblLevelOfDetail");
            this.lblLevelOfDetail.Name = "lblLevelOfDetail";
            // 
            // lblOptions
            // 
            resources.ApplyResources(this.lblOptions, "lblOptions");
            this.lblOptions.Name = "lblOptions";
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtTargetPath
            // 
            resources.ApplyResources(this.txtTargetPath, "txtTargetPath");
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.ReadOnly = true;
            // 
            // lblOutputPath
            // 
            resources.ApplyResources(this.lblOutputPath, "lblOutputPath");
            this.lblOutputPath.Name = "lblOutputPath";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ExportCesium3DTiles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.gpContainer);
            this.Name = "ExportCesium3DTiles";
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
        private System.Windows.Forms.CheckBox cbUseDraco;
        private System.Windows.Forms.CheckBox cbGeneratePropDbSqlite;
        private System.Windows.Forms.CheckBox cbExportSvfzip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox gb3DTiles;
        private System.Windows.Forms.CheckBox cbEnableQuantizedAttributes;
        private System.Windows.Forms.CheckBox cbEnableTextureWebP;
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
    }
}