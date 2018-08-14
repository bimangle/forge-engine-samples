namespace Bimangle.ForgeEngine.Revit.UI
{
    partial class FormViews
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormViews));
            this.lvViews = new System.Windows.Forms.ListView();
            this.chViewType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chViewName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvViews
            // 
            resources.ApplyResources(this.lvViews, "lvViews");
            this.lvViews.CheckBoxes = true;
            this.lvViews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chViewType,
            this.chViewName});
            this.lvViews.FullRowSelect = true;
            this.lvViews.GridLines = true;
            this.lvViews.HideSelection = false;
            this.lvViews.Name = "lvViews";
            this.lvViews.SmallImageList = this.imageList1;
            this.lvViews.UseCompatibleStateImageBehavior = false;
            this.lvViews.View = System.Windows.Forms.View.Details;
            // 
            // chViewType
            // 
            resources.ApplyResources(this.chViewType, "chViewType");
            // 
            // chViewName
            // 
            resources.ApplyResources(this.chViewName, "chViewName");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "drawing");
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
            // FormViews
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvViews);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormViews";
            this.Load += new System.EventHandler(this.FormViews_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvViews;
        private System.Windows.Forms.ColumnHeader chViewType;
        private System.Windows.Forms.ColumnHeader chViewName;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}