namespace Bimangle.ForgeEngine.Dwg.App.UI
{
    partial class FormAppXp
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
            this.cbGeneratePropDbSqlite = new System.Windows.Forms.CheckBox();
            this.cbGenerateThumbnail = new System.Windows.Forms.CheckBox();
            this.lblOptions = new System.Windows.Forms.Label();
            this.btnBrowseOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFontFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgSelectFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgSelectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnResetOptions = new System.Windows.Forms.Button();
            this.btnLicense = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gpContainer.SuspendLayout();
            this.gbInclude.SuspendLayout();
            this.gpMode.SuspendLayout();
            this.gpGenerate.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpContainer
            // 
            this.gpContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.gpContainer.Location = new System.Drawing.Point(12, 28);
            this.gpContainer.Name = "gpContainer";
            this.gpContainer.Size = new System.Drawing.Size(717, 224);
            this.gpContainer.TabIndex = 1;
            this.gpContainer.TabStop = false;
            // 
            // gbInclude
            // 
            this.gbInclude.Controls.Add(this.cbIncludeLayouts);
            this.gbInclude.Controls.Add(this.cbIncludeInvisibleLayers);
            this.gbInclude.Location = new System.Drawing.Point(248, 120);
            this.gbInclude.Name = "gbInclude";
            this.gbInclude.Size = new System.Drawing.Size(134, 95);
            this.gbInclude.TabIndex = 12;
            this.gbInclude.TabStop = false;
            this.gbInclude.Text = "包括";
            // 
            // cbIncludeLayouts
            // 
            this.cbIncludeLayouts.AutoSize = true;
            this.cbIncludeLayouts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbIncludeLayouts.Location = new System.Drawing.Point(16, 45);
            this.cbIncludeLayouts.Name = "cbIncludeLayouts";
            this.cbIncludeLayouts.Size = new System.Drawing.Size(99, 18);
            this.cbIncludeLayouts.TabIndex = 2;
            this.cbIncludeLayouts.Text = "布局(2D图纸)";
            this.cbIncludeLayouts.UseVisualStyleBackColor = true;
            this.cbIncludeLayouts.CheckedChanged += new System.EventHandler(this.cbIncludeLayouts_CheckedChanged);
            // 
            // cbIncludeInvisibleLayers
            // 
            this.cbIncludeInvisibleLayers.AutoSize = true;
            this.cbIncludeInvisibleLayers.Checked = true;
            this.cbIncludeInvisibleLayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeInvisibleLayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbIncludeInvisibleLayers.Location = new System.Drawing.Point(16, 21);
            this.cbIncludeInvisibleLayers.Name = "cbIncludeInvisibleLayers";
            this.cbIncludeInvisibleLayers.Size = new System.Drawing.Size(86, 18);
            this.cbIncludeInvisibleLayers.TabIndex = 1;
            this.cbIncludeInvisibleLayers.Text = "不可见图层";
            this.cbIncludeInvisibleLayers.UseVisualStyleBackColor = true;
            this.cbIncludeInvisibleLayers.CheckedChanged += new System.EventHandler(this.cbIncludeInvisibleLayers_CheckedChanged);
            // 
            // gpMode
            // 
            this.gpMode.Controls.Add(this.rbModeAll);
            this.gpMode.Controls.Add(this.rbMode3D);
            this.gpMode.Controls.Add(this.rbMode2D);
            this.gpMode.Location = new System.Drawing.Point(102, 120);
            this.gpMode.Name = "gpMode";
            this.gpMode.Size = new System.Drawing.Size(140, 95);
            this.gpMode.TabIndex = 11;
            this.gpMode.TabStop = false;
            this.gpMode.Text = "模式";
            // 
            // rbModeAll
            // 
            this.rbModeAll.AutoSize = true;
            this.rbModeAll.Location = new System.Drawing.Point(19, 69);
            this.rbModeAll.Name = "rbModeAll";
            this.rbModeAll.Size = new System.Drawing.Size(115, 18);
            this.rbModeAll.TabIndex = 2;
            this.rbModeAll.Text = "2D图纸和3D模型";
            this.rbModeAll.UseVisualStyleBackColor = true;
            this.rbModeAll.CheckedChanged += new System.EventHandler(this.rbModeAll_CheckedChanged);
            // 
            // rbMode3D
            // 
            this.rbMode3D.AutoSize = true;
            this.rbMode3D.Location = new System.Drawing.Point(19, 45);
            this.rbMode3D.Name = "rbMode3D";
            this.rbMode3D.Size = new System.Drawing.Size(64, 18);
            this.rbMode3D.TabIndex = 1;
            this.rbMode3D.Text = "3D模型";
            this.rbMode3D.UseVisualStyleBackColor = true;
            this.rbMode3D.CheckedChanged += new System.EventHandler(this.rbMode3D_CheckedChanged);
            // 
            // rbMode2D
            // 
            this.rbMode2D.AutoSize = true;
            this.rbMode2D.Checked = true;
            this.rbMode2D.Location = new System.Drawing.Point(19, 21);
            this.rbMode2D.Name = "rbMode2D";
            this.rbMode2D.Size = new System.Drawing.Size(64, 18);
            this.rbMode2D.TabIndex = 0;
            this.rbMode2D.TabStop = true;
            this.rbMode2D.Text = "2D图纸";
            this.rbMode2D.UseVisualStyleBackColor = true;
            this.rbMode2D.CheckedChanged += new System.EventHandler(this.rbMode2D_CheckedChanged);
            // 
            // lblInputFilePrompt
            // 
            this.lblInputFilePrompt.AutoSize = true;
            this.lblInputFilePrompt.Location = new System.Drawing.Point(99, 56);
            this.lblInputFilePrompt.Name = "lblInputFilePrompt";
            this.lblInputFilePrompt.Size = new System.Drawing.Size(95, 14);
            this.lblInputFilePrompt.TabIndex = 3;
            this.lblInputFilePrompt.Text = "请选择输入文件.";
            // 
            // btnBrowseInputFile
            // 
            this.btnBrowseInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseInputFile.Location = new System.Drawing.Point(668, 32);
            this.btnBrowseInputFile.Name = "btnBrowseInputFile";
            this.btnBrowseInputFile.Size = new System.Drawing.Size(31, 21);
            this.btnBrowseInputFile.TabIndex = 2;
            this.btnBrowseInputFile.Text = "...";
            this.btnBrowseInputFile.UseVisualStyleBackColor = true;
            this.btnBrowseInputFile.Click += new System.EventHandler(this.btnBrowseInputFile_Click);
            // 
            // txtInputFile
            // 
            this.txtInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputFile.Location = new System.Drawing.Point(101, 32);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(561, 22);
            this.txtInputFile.TabIndex = 1;
            this.txtInputFile.TextChanged += new System.EventHandler(this.txtInputFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入文件:";
            // 
            // gpGenerate
            // 
            this.gpGenerate.Controls.Add(this.cbGeneratePropDbSqlite);
            this.gpGenerate.Controls.Add(this.cbGenerateThumbnail);
            this.gpGenerate.Location = new System.Drawing.Point(388, 120);
            this.gpGenerate.Name = "gpGenerate";
            this.gpGenerate.Size = new System.Drawing.Size(315, 95);
            this.gpGenerate.TabIndex = 10;
            this.gpGenerate.TabStop = false;
            this.gpGenerate.Text = "生成";
            // 
            // cbGeneratePropDbSqlite
            // 
            this.cbGeneratePropDbSqlite.AutoSize = true;
            this.cbGeneratePropDbSqlite.Checked = true;
            this.cbGeneratePropDbSqlite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGeneratePropDbSqlite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbGeneratePropDbSqlite.Location = new System.Drawing.Point(15, 46);
            this.cbGeneratePropDbSqlite.Name = "cbGeneratePropDbSqlite";
            this.cbGeneratePropDbSqlite.Size = new System.Drawing.Size(120, 18);
            this.cbGeneratePropDbSqlite.TabIndex = 1;
            this.cbGeneratePropDbSqlite.Text = "属性数据(SQLite)";
            this.cbGeneratePropDbSqlite.UseVisualStyleBackColor = true;
            this.cbGeneratePropDbSqlite.CheckedChanged += new System.EventHandler(this.cbGeneratePropDbSqlite_CheckedChanged);
            // 
            // cbGenerateThumbnail
            // 
            this.cbGenerateThumbnail.AutoSize = true;
            this.cbGenerateThumbnail.Checked = true;
            this.cbGenerateThumbnail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGenerateThumbnail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbGenerateThumbnail.Location = new System.Drawing.Point(15, 22);
            this.cbGenerateThumbnail.Name = "cbGenerateThumbnail";
            this.cbGenerateThumbnail.Size = new System.Drawing.Size(62, 18);
            this.cbGenerateThumbnail.TabIndex = 0;
            this.cbGenerateThumbnail.Text = "缩略图";
            this.cbGenerateThumbnail.UseVisualStyleBackColor = true;
            this.cbGenerateThumbnail.CheckedChanged += new System.EventHandler(this.cbGenerateThumbnail_CheckedChanged);
            // 
            // lblOptions
            // 
            this.lblOptions.AutoSize = true;
            this.lblOptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOptions.Location = new System.Drawing.Point(30, 120);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(59, 14);
            this.lblOptions.TabIndex = 7;
            this.lblOptions.Text = "高级选项:";
            this.lblOptions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnBrowseOutputFolder
            // 
            this.btnBrowseOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOutputFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseOutputFolder.Location = new System.Drawing.Point(668, 83);
            this.btnBrowseOutputFolder.Name = "btnBrowseOutputFolder";
            this.btnBrowseOutputFolder.Size = new System.Drawing.Size(33, 22);
            this.btnBrowseOutputFolder.TabIndex = 6;
            this.btnBrowseOutputFolder.Text = "...";
            this.btnBrowseOutputFolder.UseVisualStyleBackColor = true;
            this.btnBrowseOutputFolder.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(101, 83);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(561, 22);
            this.txtOutputFolder.TabIndex = 5;
            this.txtOutputFolder.TextChanged += new System.EventHandler(this.txtOutputFolder_TextChanged);
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOutputPath.Location = new System.Drawing.Point(30, 87);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(59, 14);
            this.lblOutputPath.TabIndex = 4;
            this.lblOutputPath.Text = "输出路径:";
            this.lblOutputPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConfig});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(741, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiConfig
            // 
            this.tsmiConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFontFolder});
            this.tsmiConfig.Name = "tsmiConfig";
            this.tsmiConfig.Size = new System.Drawing.Size(74, 21);
            this.tsmiConfig.Text = "Config(&C)";
            // 
            // tsmiFontFolder
            // 
            this.tsmiFontFolder.Name = "tsmiFontFolder";
            this.tsmiFontFolder.Size = new System.Drawing.Size(180, 22);
            this.tsmiFontFolder.Text = "Fonts Folder";
            this.tsmiFontFolder.Click += new System.EventHandler(this.tsmiFontFolder_Click);
            // 
            // dlgSelectFile
            // 
            this.dlgSelectFile.Filter = "Drawing File|*.dwg|All Files|*.*";
            this.dlgSelectFile.Title = "Select Source File";
            // 
            // dlgSelectFolder
            // 
            this.dlgSelectFolder.Description = "Select Output Folder Path";
            this.dlgSelectFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnResetOptions
            // 
            this.btnResetOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetOptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnResetOptions.Location = new System.Drawing.Point(234, 272);
            this.btnResetOptions.Name = "btnResetOptions";
            this.btnResetOptions.Size = new System.Drawing.Size(114, 32);
            this.btnResetOptions.TabIndex = 6;
            this.btnResetOptions.Text = "Reset Options";
            this.btnResetOptions.UseVisualStyleBackColor = true;
            this.btnResetOptions.Click += new System.EventHandler(this.btnResetOptions_Click);
            // 
            // btnLicense
            // 
            this.btnLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLicense.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLicense.Location = new System.Drawing.Point(114, 272);
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.Size = new System.Drawing.Size(114, 32);
            this.btnLicense.TabIndex = 5;
            this.btnLicense.Text = "License ...";
            this.btnLicense.UseVisualStyleBackColor = true;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(636, 272);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(555, 272);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 32);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormAppXp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(741, 316);
            this.Controls.Add(this.btnResetOptions);
            this.Controls.Add(this.btnLicense);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gpContainer);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAppXp";
            this.Text = "BimAngle Forge Engine Dwg";
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
        private System.Windows.Forms.OpenFileDialog dlgSelectFile;
        private System.Windows.Forms.FolderBrowserDialog dlgSelectFolder;
        private System.Windows.Forms.Label lblInputFilePrompt;
        private System.Windows.Forms.Button btnBrowseInputFile;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem tsmiConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiFontFolder;
        private System.Windows.Forms.GroupBox gpMode;
        private System.Windows.Forms.RadioButton rbModeAll;
        private System.Windows.Forms.RadioButton rbMode3D;
        private System.Windows.Forms.RadioButton rbMode2D;
        private System.Windows.Forms.GroupBox gbInclude;
        private System.Windows.Forms.CheckBox cbIncludeInvisibleLayers;
        private System.Windows.Forms.CheckBox cbIncludeLayouts;
        private System.Windows.Forms.Button btnResetOptions;
        private System.Windows.Forms.Button btnLicense;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}