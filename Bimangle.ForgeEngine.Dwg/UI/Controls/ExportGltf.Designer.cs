namespace Bimangle.ForgeEngine.Dwg.UI.Controls
{
    partial class ExportGltf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportGltf));
            this.gbAdvanced = new System.Windows.Forms.GroupBox();
            this.cbGeometryCompressTypes = new System.Windows.Forms.ComboBox();
            this.cbEnableGeometryCompress = new System.Windows.Forms.CheckBox();
            this.cbTextureCompressTypes = new System.Windows.Forms.ComboBox();
            this.cbEnableTextureCompress = new System.Windows.Forms.CheckBox();
            this.cbAllowRegroupNodes = new System.Windows.Forms.CheckBox();
            this.cbEnableAutomaticSplit = new System.Windows.Forms.CheckBox();
            this.cbExportSvfzip = new System.Windows.Forms.CheckBox();
            this.cbGeneratePropDbSqlite = new System.Windows.Forms.CheckBox();
            this.cbUseExtractShell = new System.Windows.Forms.CheckBox();
            this.gpMisc = new System.Windows.Forms.GroupBox();
            this.cbOptimizationLineStyle = new System.Windows.Forms.CheckBox();
            this.cbIncludeUnplottableLayers = new System.Windows.Forms.CheckBox();
            this.cbIncludeInvisibleLayers = new System.Windows.Forms.CheckBox();
            this.gpGeneral = new System.Windows.Forms.GroupBox();
            this.lblGenerate = new System.Windows.Forms.Label();
            this.cbGenerateThumbnail = new System.Windows.Forms.CheckBox();
            this.cbVisualStyle = new System.Windows.Forms.ComboBox();
            this.lblVisualStyle = new System.Windows.Forms.Label();
            this.lblOptions = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbAdvanced.SuspendLayout();
            this.gpMisc.SuspendLayout();
            this.gpGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAdvanced
            // 
            resources.ApplyResources(this.gbAdvanced, "gbAdvanced");
            this.gbAdvanced.Controls.Add(this.cbGeometryCompressTypes);
            this.gbAdvanced.Controls.Add(this.cbEnableGeometryCompress);
            this.gbAdvanced.Controls.Add(this.cbTextureCompressTypes);
            this.gbAdvanced.Controls.Add(this.cbEnableTextureCompress);
            this.gbAdvanced.Controls.Add(this.cbAllowRegroupNodes);
            this.gbAdvanced.Controls.Add(this.cbEnableAutomaticSplit);
            this.gbAdvanced.Controls.Add(this.cbExportSvfzip);
            this.gbAdvanced.Controls.Add(this.cbGeneratePropDbSqlite);
            this.gbAdvanced.Controls.Add(this.cbUseExtractShell);
            this.gbAdvanced.Name = "gbAdvanced";
            this.gbAdvanced.TabStop = false;
            this.toolTip1.SetToolTip(this.gbAdvanced, resources.GetString("gbAdvanced.ToolTip"));
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
            // cbEnableTextureCompress
            // 
            resources.ApplyResources(this.cbEnableTextureCompress, "cbEnableTextureCompress");
            this.cbEnableTextureCompress.Name = "cbEnableTextureCompress";
            this.toolTip1.SetToolTip(this.cbEnableTextureCompress, resources.GetString("cbEnableTextureCompress.ToolTip"));
            this.cbEnableTextureCompress.UseVisualStyleBackColor = true;
            // 
            // cbAllowRegroupNodes
            // 
            resources.ApplyResources(this.cbAllowRegroupNodes, "cbAllowRegroupNodes");
            this.cbAllowRegroupNodes.Name = "cbAllowRegroupNodes";
            this.toolTip1.SetToolTip(this.cbAllowRegroupNodes, resources.GetString("cbAllowRegroupNodes.ToolTip"));
            this.cbAllowRegroupNodes.UseVisualStyleBackColor = true;
            // 
            // cbEnableAutomaticSplit
            // 
            resources.ApplyResources(this.cbEnableAutomaticSplit, "cbEnableAutomaticSplit");
            this.cbEnableAutomaticSplit.Name = "cbEnableAutomaticSplit";
            this.toolTip1.SetToolTip(this.cbEnableAutomaticSplit, resources.GetString("cbEnableAutomaticSplit.ToolTip"));
            this.cbEnableAutomaticSplit.UseVisualStyleBackColor = true;
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
            // cbUseExtractShell
            // 
            resources.ApplyResources(this.cbUseExtractShell, "cbUseExtractShell");
            this.cbUseExtractShell.Name = "cbUseExtractShell";
            this.toolTip1.SetToolTip(this.cbUseExtractShell, resources.GetString("cbUseExtractShell.ToolTip"));
            this.cbUseExtractShell.UseVisualStyleBackColor = true;
            // 
            // gpMisc
            // 
            resources.ApplyResources(this.gpMisc, "gpMisc");
            this.gpMisc.Controls.Add(this.cbOptimizationLineStyle);
            this.gpMisc.Controls.Add(this.cbIncludeUnplottableLayers);
            this.gpMisc.Controls.Add(this.cbIncludeInvisibleLayers);
            this.gpMisc.Name = "gpMisc";
            this.gpMisc.TabStop = false;
            this.toolTip1.SetToolTip(this.gpMisc, resources.GetString("gpMisc.ToolTip"));
            // 
            // cbOptimizationLineStyle
            // 
            resources.ApplyResources(this.cbOptimizationLineStyle, "cbOptimizationLineStyle");
            this.cbOptimizationLineStyle.Checked = true;
            this.cbOptimizationLineStyle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOptimizationLineStyle.Name = "cbOptimizationLineStyle";
            this.toolTip1.SetToolTip(this.cbOptimizationLineStyle, resources.GetString("cbOptimizationLineStyle.ToolTip"));
            this.cbOptimizationLineStyle.UseVisualStyleBackColor = true;
            // 
            // cbIncludeUnplottableLayers
            // 
            resources.ApplyResources(this.cbIncludeUnplottableLayers, "cbIncludeUnplottableLayers");
            this.cbIncludeUnplottableLayers.Checked = true;
            this.cbIncludeUnplottableLayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeUnplottableLayers.Name = "cbIncludeUnplottableLayers";
            this.toolTip1.SetToolTip(this.cbIncludeUnplottableLayers, resources.GetString("cbIncludeUnplottableLayers.ToolTip"));
            this.cbIncludeUnplottableLayers.UseVisualStyleBackColor = true;
            // 
            // cbIncludeInvisibleLayers
            // 
            resources.ApplyResources(this.cbIncludeInvisibleLayers, "cbIncludeInvisibleLayers");
            this.cbIncludeInvisibleLayers.Name = "cbIncludeInvisibleLayers";
            this.toolTip1.SetToolTip(this.cbIncludeInvisibleLayers, resources.GetString("cbIncludeInvisibleLayers.ToolTip"));
            this.cbIncludeInvisibleLayers.UseVisualStyleBackColor = true;
            // 
            // gpGeneral
            // 
            resources.ApplyResources(this.gpGeneral, "gpGeneral");
            this.gpGeneral.Controls.Add(this.lblGenerate);
            this.gpGeneral.Controls.Add(this.cbGenerateThumbnail);
            this.gpGeneral.Controls.Add(this.cbVisualStyle);
            this.gpGeneral.Controls.Add(this.lblVisualStyle);
            this.gpGeneral.Name = "gpGeneral";
            this.gpGeneral.TabStop = false;
            this.toolTip1.SetToolTip(this.gpGeneral, resources.GetString("gpGeneral.ToolTip"));
            // 
            // lblGenerate
            // 
            resources.ApplyResources(this.lblGenerate, "lblGenerate");
            this.lblGenerate.Name = "lblGenerate";
            this.toolTip1.SetToolTip(this.lblGenerate, resources.GetString("lblGenerate.ToolTip"));
            // 
            // cbGenerateThumbnail
            // 
            resources.ApplyResources(this.cbGenerateThumbnail, "cbGenerateThumbnail");
            this.cbGenerateThumbnail.Name = "cbGenerateThumbnail";
            this.toolTip1.SetToolTip(this.cbGenerateThumbnail, resources.GetString("cbGenerateThumbnail.ToolTip"));
            this.cbGenerateThumbnail.UseVisualStyleBackColor = true;
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
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // ExportGltf
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblOptions);
            this.Controls.Add(this.gpGeneral);
            this.Controls.Add(this.gpMisc);
            this.Controls.Add(this.gbAdvanced);
            this.Name = "ExportGltf";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.gbAdvanced.ResumeLayout(false);
            this.gbAdvanced.PerformLayout();
            this.gpMisc.ResumeLayout(false);
            this.gpMisc.PerformLayout();
            this.gpGeneral.ResumeLayout(false);
            this.gpGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.ComboBox cbVisualStyle;
        private System.Windows.Forms.Label lblVisualStyle;
        private System.Windows.Forms.GroupBox gpGeneral;
        private System.Windows.Forms.GroupBox gpMisc;
        private System.Windows.Forms.GroupBox gbAdvanced;
        private System.Windows.Forms.CheckBox cbUseExtractShell;
        private System.Windows.Forms.CheckBox cbGeneratePropDbSqlite;
        private System.Windows.Forms.CheckBox cbExportSvfzip;
        private System.Windows.Forms.CheckBox cbEnableAutomaticSplit;
        private System.Windows.Forms.CheckBox cbAllowRegroupNodes;
        private System.Windows.Forms.ComboBox cbGeometryCompressTypes;
        private System.Windows.Forms.CheckBox cbEnableGeometryCompress;
        private System.Windows.Forms.ComboBox cbTextureCompressTypes;
        private System.Windows.Forms.CheckBox cbEnableTextureCompress;
        private System.Windows.Forms.Label lblGenerate;
        private System.Windows.Forms.CheckBox cbGenerateThumbnail;
        private System.Windows.Forms.CheckBox cbIncludeUnplottableLayers;
        private System.Windows.Forms.CheckBox cbIncludeInvisibleLayers;
        private System.Windows.Forms.CheckBox cbOptimizationLineStyle;
    }
}