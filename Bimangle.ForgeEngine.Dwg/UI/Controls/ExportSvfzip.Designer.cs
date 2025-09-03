namespace Bimangle.ForgeEngine.Dwg.UI.Controls
{
    partial class ExportSvfzip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportSvfzip));
            this.lblOptions = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gpMisc = new System.Windows.Forms.GroupBox();
            this.cbForceUseWireframe = new System.Windows.Forms.CheckBox();
            this.cbOptimizationLineStyle = new System.Windows.Forms.CheckBox();
            this.cbUseDefaultViewport = new System.Windows.Forms.CheckBox();
            this.gbInclude = new System.Windows.Forms.GroupBox();
            this.cbIncludeUnplottableLayers = new System.Windows.Forms.CheckBox();
            this.cbIncludeLayouts = new System.Windows.Forms.CheckBox();
            this.cbIncludeInvisibleLayers = new System.Windows.Forms.CheckBox();
            this.gpMode = new System.Windows.Forms.GroupBox();
            this.rbModeAll = new System.Windows.Forms.RadioButton();
            this.rbMode3D = new System.Windows.Forms.RadioButton();
            this.rbMode2D = new System.Windows.Forms.RadioButton();
            this.gpGenerate = new System.Windows.Forms.GroupBox();
            this.cbGenerateLeaflet = new System.Windows.Forms.CheckBox();
            this.cbGeneratePropDbSqlite = new System.Windows.Forms.CheckBox();
            this.cbGenerateThumbnail = new System.Windows.Forms.CheckBox();
            this.dlgSelectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.gpMisc.SuspendLayout();
            this.gbInclude.SuspendLayout();
            this.gpMode.SuspendLayout();
            this.gpGenerate.SuspendLayout();
            this.SuspendLayout();
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
            // gpMisc
            // 
            resources.ApplyResources(this.gpMisc, "gpMisc");
            this.gpMisc.Controls.Add(this.cbForceUseWireframe);
            this.gpMisc.Controls.Add(this.cbOptimizationLineStyle);
            this.gpMisc.Controls.Add(this.cbUseDefaultViewport);
            this.gpMisc.Name = "gpMisc";
            this.gpMisc.TabStop = false;
            this.toolTip1.SetToolTip(this.gpMisc, resources.GetString("gpMisc.ToolTip"));
            // 
            // cbForceUseWireframe
            // 
            resources.ApplyResources(this.cbForceUseWireframe, "cbForceUseWireframe");
            this.cbForceUseWireframe.Checked = true;
            this.cbForceUseWireframe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbForceUseWireframe.Name = "cbForceUseWireframe";
            this.toolTip1.SetToolTip(this.cbForceUseWireframe, resources.GetString("cbForceUseWireframe.ToolTip"));
            this.cbForceUseWireframe.UseVisualStyleBackColor = true;
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
            // cbUseDefaultViewport
            // 
            resources.ApplyResources(this.cbUseDefaultViewport, "cbUseDefaultViewport");
            this.cbUseDefaultViewport.Name = "cbUseDefaultViewport";
            this.toolTip1.SetToolTip(this.cbUseDefaultViewport, resources.GetString("cbUseDefaultViewport.ToolTip"));
            this.cbUseDefaultViewport.UseVisualStyleBackColor = true;
            // 
            // gbInclude
            // 
            resources.ApplyResources(this.gbInclude, "gbInclude");
            this.gbInclude.Controls.Add(this.cbIncludeUnplottableLayers);
            this.gbInclude.Controls.Add(this.cbIncludeLayouts);
            this.gbInclude.Controls.Add(this.cbIncludeInvisibleLayers);
            this.gbInclude.Name = "gbInclude";
            this.gbInclude.TabStop = false;
            this.toolTip1.SetToolTip(this.gbInclude, resources.GetString("gbInclude.ToolTip"));
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
            // cbIncludeLayouts
            // 
            resources.ApplyResources(this.cbIncludeLayouts, "cbIncludeLayouts");
            this.cbIncludeLayouts.Checked = true;
            this.cbIncludeLayouts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeLayouts.Name = "cbIncludeLayouts";
            this.toolTip1.SetToolTip(this.cbIncludeLayouts, resources.GetString("cbIncludeLayouts.ToolTip"));
            this.cbIncludeLayouts.UseVisualStyleBackColor = true;
            // 
            // cbIncludeInvisibleLayers
            // 
            resources.ApplyResources(this.cbIncludeInvisibleLayers, "cbIncludeInvisibleLayers");
            this.cbIncludeInvisibleLayers.Name = "cbIncludeInvisibleLayers";
            this.toolTip1.SetToolTip(this.cbIncludeInvisibleLayers, resources.GetString("cbIncludeInvisibleLayers.ToolTip"));
            this.cbIncludeInvisibleLayers.UseVisualStyleBackColor = true;
            // 
            // gpMode
            // 
            resources.ApplyResources(this.gpMode, "gpMode");
            this.gpMode.Controls.Add(this.rbModeAll);
            this.gpMode.Controls.Add(this.rbMode3D);
            this.gpMode.Controls.Add(this.rbMode2D);
            this.gpMode.Name = "gpMode";
            this.gpMode.TabStop = false;
            this.toolTip1.SetToolTip(this.gpMode, resources.GetString("gpMode.ToolTip"));
            // 
            // rbModeAll
            // 
            resources.ApplyResources(this.rbModeAll, "rbModeAll");
            this.rbModeAll.Name = "rbModeAll";
            this.toolTip1.SetToolTip(this.rbModeAll, resources.GetString("rbModeAll.ToolTip"));
            this.rbModeAll.UseVisualStyleBackColor = true;
            // 
            // rbMode3D
            // 
            resources.ApplyResources(this.rbMode3D, "rbMode3D");
            this.rbMode3D.Name = "rbMode3D";
            this.toolTip1.SetToolTip(this.rbMode3D, resources.GetString("rbMode3D.ToolTip"));
            this.rbMode3D.UseVisualStyleBackColor = true;
            // 
            // rbMode2D
            // 
            resources.ApplyResources(this.rbMode2D, "rbMode2D");
            this.rbMode2D.Checked = true;
            this.rbMode2D.Name = "rbMode2D";
            this.rbMode2D.TabStop = true;
            this.toolTip1.SetToolTip(this.rbMode2D, resources.GetString("rbMode2D.ToolTip"));
            this.rbMode2D.UseVisualStyleBackColor = true;
            // 
            // gpGenerate
            // 
            resources.ApplyResources(this.gpGenerate, "gpGenerate");
            this.gpGenerate.Controls.Add(this.cbGenerateLeaflet);
            this.gpGenerate.Controls.Add(this.cbGeneratePropDbSqlite);
            this.gpGenerate.Controls.Add(this.cbGenerateThumbnail);
            this.gpGenerate.Name = "gpGenerate";
            this.gpGenerate.TabStop = false;
            this.toolTip1.SetToolTip(this.gpGenerate, resources.GetString("gpGenerate.ToolTip"));
            // 
            // cbGenerateLeaflet
            // 
            resources.ApplyResources(this.cbGenerateLeaflet, "cbGenerateLeaflet");
            this.cbGenerateLeaflet.Name = "cbGenerateLeaflet";
            this.toolTip1.SetToolTip(this.cbGenerateLeaflet, resources.GetString("cbGenerateLeaflet.ToolTip"));
            this.cbGenerateLeaflet.UseVisualStyleBackColor = true;
            // 
            // cbGeneratePropDbSqlite
            // 
            resources.ApplyResources(this.cbGeneratePropDbSqlite, "cbGeneratePropDbSqlite");
            this.cbGeneratePropDbSqlite.Checked = true;
            this.cbGeneratePropDbSqlite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGeneratePropDbSqlite.Name = "cbGeneratePropDbSqlite";
            this.toolTip1.SetToolTip(this.cbGeneratePropDbSqlite, resources.GetString("cbGeneratePropDbSqlite.ToolTip"));
            this.cbGeneratePropDbSqlite.UseVisualStyleBackColor = true;
            // 
            // cbGenerateThumbnail
            // 
            resources.ApplyResources(this.cbGenerateThumbnail, "cbGenerateThumbnail");
            this.cbGenerateThumbnail.Checked = true;
            this.cbGenerateThumbnail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGenerateThumbnail.Name = "cbGenerateThumbnail";
            this.toolTip1.SetToolTip(this.cbGenerateThumbnail, resources.GetString("cbGenerateThumbnail.ToolTip"));
            this.cbGenerateThumbnail.UseVisualStyleBackColor = true;
            // 
            // dlgSelectFolder
            // 
            resources.ApplyResources(this.dlgSelectFolder, "dlgSelectFolder");
            this.dlgSelectFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // ExportSvfzip
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.gpMisc);
            this.Controls.Add(this.gbInclude);
            this.Controls.Add(this.gpMode);
            this.Controls.Add(this.gpGenerate);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblOptions);
            this.Name = "ExportSvfzip";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.gpMisc.ResumeLayout(false);
            this.gpMisc.PerformLayout();
            this.gbInclude.ResumeLayout(false);
            this.gbInclude.PerformLayout();
            this.gpMode.ResumeLayout(false);
            this.gpMode.PerformLayout();
            this.gpGenerate.ResumeLayout(false);
            this.gpGenerate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.FolderBrowserDialog dlgSelectFolder;
        private System.Windows.Forms.GroupBox gpMisc;
        private System.Windows.Forms.CheckBox cbForceUseWireframe;
        private System.Windows.Forms.CheckBox cbOptimizationLineStyle;
        private System.Windows.Forms.CheckBox cbUseDefaultViewport;
        private System.Windows.Forms.GroupBox gbInclude;
        private System.Windows.Forms.CheckBox cbIncludeUnplottableLayers;
        private System.Windows.Forms.CheckBox cbIncludeLayouts;
        private System.Windows.Forms.CheckBox cbIncludeInvisibleLayers;
        private System.Windows.Forms.GroupBox gpMode;
        private System.Windows.Forms.RadioButton rbModeAll;
        private System.Windows.Forms.RadioButton rbMode3D;
        private System.Windows.Forms.RadioButton rbMode2D;
        private System.Windows.Forms.GroupBox gpGenerate;
        private System.Windows.Forms.CheckBox cbGenerateLeaflet;
        private System.Windows.Forms.CheckBox cbGeneratePropDbSqlite;
        private System.Windows.Forms.CheckBox cbGenerateThumbnail;
    }
}