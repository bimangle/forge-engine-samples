namespace Bimangle.ForgeEngine.Skp.UI
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabList = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.exportSvfzip1 = new Bimangle.ForgeEngine.Skp.UI.Controls.ExportSvfzip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiResetOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.gpInput = new System.Windows.Forms.GroupBox();
            this.lblInputFile = new System.Windows.Forms.Label();
            this.lblInputFilePrompt = new System.Windows.Forms.Label();
            this.btnBrowseInputFile = new System.Windows.Forms.Button();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.dlgSelectFile = new System.Windows.Forms.OpenFileDialog();
            this.tabList.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gpInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabList
            // 
            resources.ApplyResources(this.tabList, "tabList");
            this.tabList.Controls.Add(this.tabPage1);
            this.tabList.ImageList = this.imageList1;
            this.tabList.Name = "tabList";
            this.tabList.SelectedIndex = 0;
            this.tabList.SelectedIndexChanged += new System.EventHandler(this.tabList_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.exportSvfzip1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // exportSvfzip1
            // 
            resources.ApplyResources(this.exportSvfzip1, "exportSvfzip1");
            this.exportSvfzip1.Name = "exportSvfzip1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "gltf");
            this.imageList1.Images.SetKeyName(1, "svf");
            this.imageList1.Images.SetKeyName(2, "3dtiles");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEdit,
            this.tsmiToolset,
            this.tsmiHelp});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResetOptions});
            this.tsmiEdit.Name = "tsmiEdit";
            resources.ApplyResources(this.tsmiEdit, "tsmiEdit");
            // 
            // tsmiResetOptions
            // 
            this.tsmiResetOptions.Name = "tsmiResetOptions";
            resources.ApplyResources(this.tsmiResetOptions, "tsmiResetOptions");
            this.tsmiResetOptions.Click += new System.EventHandler(this.tsmiResetOptions_Click);
            // 
            // tsmiToolset
            // 
            this.tsmiToolset.Name = "tsmiToolset";
            resources.ApplyResources(this.tsmiToolset, "tsmiToolset");
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLicense});
            this.tsmiHelp.Name = "tsmiHelp";
            resources.ApplyResources(this.tsmiHelp, "tsmiHelp");
            // 
            // tsmiLicense
            // 
            this.tsmiLicense.Name = "tsmiLicense";
            resources.ApplyResources(this.tsmiLicense, "tsmiLicense");
            this.tsmiLicense.Click += new System.EventHandler(this.tsmiLicense_Click);
            // 
            // gpInput
            // 
            resources.ApplyResources(this.gpInput, "gpInput");
            this.gpInput.Controls.Add(this.lblInputFile);
            this.gpInput.Controls.Add(this.lblInputFilePrompt);
            this.gpInput.Controls.Add(this.btnBrowseInputFile);
            this.gpInput.Controls.Add(this.txtInputFile);
            this.gpInput.Name = "gpInput";
            this.gpInput.TabStop = false;
            // 
            // lblInputFile
            // 
            resources.ApplyResources(this.lblInputFile, "lblInputFile");
            this.lblInputFile.Name = "lblInputFile";
            // 
            // lblInputFilePrompt
            // 
            resources.ApplyResources(this.lblInputFilePrompt, "lblInputFilePrompt");
            this.lblInputFilePrompt.Name = "lblInputFilePrompt";
            // 
            // btnBrowseInputFile
            // 
            resources.ApplyResources(this.btnBrowseInputFile, "btnBrowseInputFile");
            this.btnBrowseInputFile.Name = "btnBrowseInputFile";
            this.btnBrowseInputFile.UseVisualStyleBackColor = true;
            this.btnBrowseInputFile.Click += new System.EventHandler(this.btnBrowseInputFile_Click);
            // 
            // txtInputFile
            // 
            resources.ApplyResources(this.txtInputFile, "txtInputFile");
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.TextChanged += new System.EventHandler(this.txtInputFile_TextChanged);
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // dlgSelectFile
            // 
            resources.ApplyResources(this.dlgSelectFile, "dlgSelectFile");
            // 
            // FormExport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.gpInput);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExportSvfzip_FormClosing);
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.Shown += new System.EventHandler(this.FormExportSvfzip_Shown);
            this.tabList.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gpInput.ResumeLayout(false);
            this.gpInput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabList;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.GroupBox gpInput;
        private System.Windows.Forms.Label lblInputFilePrompt;
        private System.Windows.Forms.Button btnBrowseInputFile;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetOptions;
        private Controls.ExportSvfzip exportSvfzip1;
        private System.Windows.Forms.Label lblInputFile;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.OpenFileDialog dlgSelectFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolset;
    }
}