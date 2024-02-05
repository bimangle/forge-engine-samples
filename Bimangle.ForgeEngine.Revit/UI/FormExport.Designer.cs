namespace Bimangle.ForgeEngine.Revit.UI
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnLicense = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnResetOptions = new System.Windows.Forms.Button();
            this.tabList = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiExtends = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPayingSubscribersOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRenderingPerformancePreferred = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDisableGeometrySimplification = new System.Windows.Forms.ToolStripMenuItem();
            this.tabList.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLicense
            // 
            resources.ApplyResources(this.btnLicense, "btnLicense");
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.UseVisualStyleBackColor = true;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // btnResetOptions
            // 
            resources.ApplyResources(this.btnResetOptions, "btnResetOptions");
            this.btnResetOptions.Name = "btnResetOptions";
            this.btnResetOptions.UseVisualStyleBackColor = true;
            this.btnResetOptions.Click += new System.EventHandler(this.btnResetOptions_Click);
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
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.tsmiExtends});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // tsmiExtends
            // 
            this.tsmiExtends.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPayingSubscribersOnly,
            this.toolStripMenuItem3,
            this.tsmiDisableGeometrySimplification});
            this.tsmiExtends.Name = "tsmiExtends";
            resources.ApplyResources(this.tsmiExtends, "tsmiExtends");
            // 
            // tsmiPayingSubscribersOnly
            // 
            this.tsmiPayingSubscribersOnly.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRenderingPerformancePreferred});
            this.tsmiPayingSubscribersOnly.Name = "tsmiPayingSubscribersOnly";
            resources.ApplyResources(this.tsmiPayingSubscribersOnly, "tsmiPayingSubscribersOnly");
            // 
            // tsmiRenderingPerformancePreferred
            // 
            this.tsmiRenderingPerformancePreferred.CheckOnClick = true;
            this.tsmiRenderingPerformancePreferred.Name = "tsmiRenderingPerformancePreferred";
            resources.ApplyResources(this.tsmiRenderingPerformancePreferred, "tsmiRenderingPerformancePreferred");
            this.tsmiRenderingPerformancePreferred.Click += new System.EventHandler(this.tsmiRenderingPerformancePreferred_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // tsmiDisableGeometrySimplification
            // 
            this.tsmiDisableGeometrySimplification.CheckOnClick = true;
            this.tsmiDisableGeometrySimplification.Name = "tsmiDisableGeometrySimplification";
            resources.ApplyResources(this.tsmiDisableGeometrySimplification, "tsmiDisableGeometrySimplification");
            // 
            // FormExport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabList);
            this.Controls.Add(this.btnResetOptions);
            this.Controls.Add(this.btnLicense);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExportSvfzip_FormClosing);
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.Shown += new System.EventHandler(this.FormExportSvfzip_Shown);
            this.tabList.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnLicense;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnResetOptions;
        private System.Windows.Forms.TabControl tabList;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExtends;
        private System.Windows.Forms.ToolStripMenuItem tsmiPayingSubscribersOnly;
        private System.Windows.Forms.ToolStripMenuItem tsmiRenderingPerformancePreferred;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisableGeometrySimplification;
    }
}