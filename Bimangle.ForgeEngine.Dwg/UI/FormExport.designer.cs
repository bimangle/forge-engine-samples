namespace Bimangle.ForgeEngine.Dwg.CLI.UI
{
    partial class FormExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExport));
            this.gpContainer = new System.Windows.Forms.GroupBox();
            this.gbInclude = new System.Windows.Forms.GroupBox();
            this.cbIncludeLayouts = new System.Windows.Forms.CheckBox();
            this.cbIncludeInvisibleLayers = new System.Windows.Forms.CheckBox();
            this.gpMode = new System.Windows.Forms.GroupBox();
            this.rbModeAll = new System.Windows.Forms.RadioButton();
            this.rbMode3D = new System.Windows.Forms.RadioButton();
            this.rbMode2D = new System.Windows.Forms.RadioButton();
            this.lblInputFilePrompt = new System.Windows.Forms.Label();
            this.btnBrowseInputFile = new System.Windows.Forms.Button();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpGenerate = new System.Windows.Forms.GroupBox();
            this.cbGenerateLeaflet = new System.Windows.Forms.CheckBox();
            this.cbGeneratePropDbSqlite = new System.Windows.Forms.CheckBox();
            this.cbGenerateThumbnail = new System.Windows.Forms.CheckBox();
            this.lblOptions = new System.Windows.Forms.Label();
            this.btnBrowseOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiResetOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFontFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.dlgSelectFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgSelectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.gpContainer.SuspendLayout();
            this.gbInclude.SuspendLayout();
            this.gpMode.SuspendLayout();
            this.gpGenerate.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpContainer
            // 
            resources.ApplyResources(this.gpContainer, "gpContainer");
            this.gpContainer.Controls.Add(this.gbInclude);
            this.gpContainer.Controls.Add(this.gpMode);
            this.gpContainer.Controls.Add(this.lblInputFilePrompt);
            this.gpContainer.Controls.Add(this.btnBrowseInputFile);
            this.gpContainer.Controls.Add(this.txtInputFile);
            this.gpContainer.Controls.Add(this.label1);
            this.gpContainer.Controls.Add(this.gpGenerate);
            this.gpContainer.Controls.Add(this.lblOptions);
            this.gpContainer.Controls.Add(this.btnBrowseOutputFolder);
            this.gpContainer.Controls.Add(this.txtOutputFolder);
            this.gpContainer.Controls.Add(this.lblOutputPath);
            this.gpContainer.Name = "gpContainer";
            this.gpContainer.TabStop = false;
            this.toolTip1.SetToolTip(this.gpContainer, resources.GetString("gpContainer.ToolTip"));
            // 
            // gbInclude
            // 
            resources.ApplyResources(this.gbInclude, "gbInclude");
            this.gbInclude.Controls.Add(this.cbIncludeLayouts);
            this.gbInclude.Controls.Add(this.cbIncludeInvisibleLayers);
            this.gbInclude.Name = "gbInclude";
            this.gbInclude.TabStop = false;
            this.toolTip1.SetToolTip(this.gbInclude, resources.GetString("gbInclude.ToolTip"));
            // 
            // cbIncludeLayouts
            // 
            resources.ApplyResources(this.cbIncludeLayouts, "cbIncludeLayouts");
            this.cbIncludeLayouts.Name = "cbIncludeLayouts";
            this.toolTip1.SetToolTip(this.cbIncludeLayouts, resources.GetString("cbIncludeLayouts.ToolTip"));
            this.cbIncludeLayouts.UseVisualStyleBackColor = true;
            // 
            // cbIncludeInvisibleLayers
            // 
            resources.ApplyResources(this.cbIncludeInvisibleLayers, "cbIncludeInvisibleLayers");
            this.cbIncludeInvisibleLayers.Checked = true;
            this.cbIncludeInvisibleLayers.CheckState = System.Windows.Forms.CheckState.Checked;
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
            // lblInputFilePrompt
            // 
            resources.ApplyResources(this.lblInputFilePrompt, "lblInputFilePrompt");
            this.lblInputFilePrompt.Name = "lblInputFilePrompt";
            this.toolTip1.SetToolTip(this.lblInputFilePrompt, resources.GetString("lblInputFilePrompt.ToolTip"));
            // 
            // btnBrowseInputFile
            // 
            resources.ApplyResources(this.btnBrowseInputFile, "btnBrowseInputFile");
            this.btnBrowseInputFile.Name = "btnBrowseInputFile";
            this.toolTip1.SetToolTip(this.btnBrowseInputFile, resources.GetString("btnBrowseInputFile.ToolTip"));
            this.btnBrowseInputFile.UseVisualStyleBackColor = true;
            this.btnBrowseInputFile.Click += new System.EventHandler(this.btnBrowseInputFile_Click);
            // 
            // txtInputFile
            // 
            resources.ApplyResources(this.txtInputFile, "txtInputFile");
            this.txtInputFile.Name = "txtInputFile";
            this.toolTip1.SetToolTip(this.txtInputFile, resources.GetString("txtInputFile.ToolTip"));
            this.txtInputFile.TextChanged += new System.EventHandler(this.txtInputFile_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
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
            // lblOptions
            // 
            resources.ApplyResources(this.lblOptions, "lblOptions");
            this.lblOptions.Name = "lblOptions";
            this.toolTip1.SetToolTip(this.lblOptions, resources.GetString("lblOptions.ToolTip"));
            // 
            // btnBrowseOutputFolder
            // 
            resources.ApplyResources(this.btnBrowseOutputFolder, "btnBrowseOutputFolder");
            this.btnBrowseOutputFolder.Name = "btnBrowseOutputFolder";
            this.toolTip1.SetToolTip(this.btnBrowseOutputFolder, resources.GetString("btnBrowseOutputFolder.ToolTip"));
            this.btnBrowseOutputFolder.UseVisualStyleBackColor = true;
            this.btnBrowseOutputFolder.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtOutputFolder
            // 
            resources.ApplyResources(this.txtOutputFolder, "txtOutputFolder");
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.toolTip1.SetToolTip(this.txtOutputFolder, resources.GetString("txtOutputFolder.ToolTip"));
            // 
            // lblOutputPath
            // 
            resources.ApplyResources(this.lblOutputPath, "lblOutputPath");
            this.lblOutputPath.Name = "lblOutputPath";
            this.toolTip1.SetToolTip(this.lblOutputPath, resources.GetString("lblOutputPath.ToolTip"));
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConfig,
            this.tsmiHelp});
            this.menuStrip1.Name = "menuStrip1";
            this.toolTip1.SetToolTip(this.menuStrip1, resources.GetString("menuStrip1.ToolTip"));
            // 
            // tsmiConfig
            // 
            resources.ApplyResources(this.tsmiConfig, "tsmiConfig");
            this.tsmiConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResetOptions,
            this.toolStripMenuItem1,
            this.tsmiFontFolder});
            this.tsmiConfig.Name = "tsmiConfig";
            // 
            // tsmiResetOptions
            // 
            resources.ApplyResources(this.tsmiResetOptions, "tsmiResetOptions");
            this.tsmiResetOptions.Name = "tsmiResetOptions";
            this.tsmiResetOptions.Click += new System.EventHandler(this.tsmiResetOptions_Click);
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // tsmiFontFolder
            // 
            resources.ApplyResources(this.tsmiFontFolder, "tsmiFontFolder");
            this.tsmiFontFolder.Name = "tsmiFontFolder";
            this.tsmiFontFolder.Click += new System.EventHandler(this.tsmiFontFolder_Click);
            // 
            // tsmiHelp
            // 
            resources.ApplyResources(this.tsmiHelp, "tsmiHelp");
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLicense});
            this.tsmiHelp.Name = "tsmiHelp";
            // 
            // tsmiLicense
            // 
            resources.ApplyResources(this.tsmiLicense, "tsmiLicense");
            this.tsmiLicense.Name = "tsmiLicense";
            this.tsmiLicense.Click += new System.EventHandler(this.tsmiLicense_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.btnRun);
            this.groupBox2.Controls.Add(this.txtOutput);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.toolTip1.SetToolTip(this.btnRun, resources.GetString("btnRun.ToolTip"));
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtOutput
            // 
            resources.ApplyResources(this.txtOutput, "txtOutput");
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtOutput, resources.GetString("txtOutput.ToolTip"));
            // 
            // dlgSelectFile
            // 
            resources.ApplyResources(this.dlgSelectFile, "dlgSelectFile");
            // 
            // dlgSelectFolder
            // 
            resources.ApplyResources(this.dlgSelectFolder, "dlgSelectFolder");
            this.dlgSelectFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // FormExport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gpContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExport";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExportSvfzip_FormClosing);
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.Shown += new System.EventHandler(this.FormAppXp_Shown);
            this.gpContainer.ResumeLayout(false);
            this.gpContainer.PerformLayout();
            this.gbInclude.ResumeLayout(false);
            this.gbInclude.PerformLayout();
            this.gpMode.ResumeLayout(false);
            this.gpMode.PerformLayout();
            this.gpGenerate.ResumeLayout(false);
            this.gpGenerate.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gpContainer;
        private System.Windows.Forms.Button btnBrowseOutputFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.GroupBox gpGenerate;
        private System.Windows.Forms.CheckBox cbGeneratePropDbSqlite;
        private System.Windows.Forms.CheckBox cbGenerateThumbnail;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.OpenFileDialog dlgSelectFile;
        private System.Windows.Forms.FolderBrowserDialog dlgSelectFolder;
        private System.Windows.Forms.Label lblInputFilePrompt;
        private System.Windows.Forms.Button btnBrowseInputFile;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.ToolStripMenuItem tsmiLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiFontFolder;
        private System.Windows.Forms.GroupBox gpMode;
        private System.Windows.Forms.RadioButton rbModeAll;
        private System.Windows.Forms.RadioButton rbMode3D;
        private System.Windows.Forms.RadioButton rbMode2D;
        private System.Windows.Forms.GroupBox gbInclude;
        private System.Windows.Forms.CheckBox cbIncludeInvisibleLayers;
        private System.Windows.Forms.CheckBox cbIncludeLayouts;
        private System.Windows.Forms.CheckBox cbGenerateLeaflet;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}