namespace Bimangle.ForgeEngine.Skp.Georeferncing
{
    partial class FormProjParamCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProjParamCalc));
            this.lblRefPointLocalCoords = new System.Windows.Forms.Label();
            this.lblRefPointLocalX = new System.Windows.Forms.Label();
            this.lblRefPointLocalY = new System.Windows.Forms.Label();
            this.txtRefPointLocalY = new System.Windows.Forms.TextBox();
            this.txtRefPointLocalX = new System.Windows.Forms.TextBox();
            this.lblRefPointGeoCoords = new System.Windows.Forms.Label();
            this.txtRefPointGeoLon = new System.Windows.Forms.TextBox();
            this.txtRefPointGeoLat = new System.Windows.Forms.TextBox();
            this.lblRefPointGeoLon = new System.Windows.Forms.Label();
            this.lblRefPointGeoLat = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnPickPosition = new System.Windows.Forms.Button();
            this.btnPickPositionOnModel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRefPointLocalCoords
            // 
            resources.ApplyResources(this.lblRefPointLocalCoords, "lblRefPointLocalCoords");
            this.errorProvider1.SetError(this.lblRefPointLocalCoords, resources.GetString("lblRefPointLocalCoords.Error"));
            this.errorProvider1.SetIconAlignment(this.lblRefPointLocalCoords, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblRefPointLocalCoords.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblRefPointLocalCoords, ((int)(resources.GetObject("lblRefPointLocalCoords.IconPadding"))));
            this.lblRefPointLocalCoords.Name = "lblRefPointLocalCoords";
            this.toolTip1.SetToolTip(this.lblRefPointLocalCoords, resources.GetString("lblRefPointLocalCoords.ToolTip"));
            // 
            // lblRefPointLocalX
            // 
            resources.ApplyResources(this.lblRefPointLocalX, "lblRefPointLocalX");
            this.errorProvider1.SetError(this.lblRefPointLocalX, resources.GetString("lblRefPointLocalX.Error"));
            this.errorProvider1.SetIconAlignment(this.lblRefPointLocalX, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblRefPointLocalX.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblRefPointLocalX, ((int)(resources.GetObject("lblRefPointLocalX.IconPadding"))));
            this.lblRefPointLocalX.Name = "lblRefPointLocalX";
            this.toolTip1.SetToolTip(this.lblRefPointLocalX, resources.GetString("lblRefPointLocalX.ToolTip"));
            // 
            // lblRefPointLocalY
            // 
            resources.ApplyResources(this.lblRefPointLocalY, "lblRefPointLocalY");
            this.errorProvider1.SetError(this.lblRefPointLocalY, resources.GetString("lblRefPointLocalY.Error"));
            this.errorProvider1.SetIconAlignment(this.lblRefPointLocalY, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblRefPointLocalY.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblRefPointLocalY, ((int)(resources.GetObject("lblRefPointLocalY.IconPadding"))));
            this.lblRefPointLocalY.Name = "lblRefPointLocalY";
            this.toolTip1.SetToolTip(this.lblRefPointLocalY, resources.GetString("lblRefPointLocalY.ToolTip"));
            // 
            // txtRefPointLocalY
            // 
            resources.ApplyResources(this.txtRefPointLocalY, "txtRefPointLocalY");
            this.errorProvider1.SetError(this.txtRefPointLocalY, resources.GetString("txtRefPointLocalY.Error"));
            this.errorProvider1.SetIconAlignment(this.txtRefPointLocalY, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtRefPointLocalY.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtRefPointLocalY, ((int)(resources.GetObject("txtRefPointLocalY.IconPadding"))));
            this.txtRefPointLocalY.Name = "txtRefPointLocalY";
            this.toolTip1.SetToolTip(this.txtRefPointLocalY, resources.GetString("txtRefPointLocalY.ToolTip"));
            this.txtRefPointLocalY.Validating += new System.ComponentModel.CancelEventHandler(this.txtRefPointLocalY_Validating);
            // 
            // txtRefPointLocalX
            // 
            resources.ApplyResources(this.txtRefPointLocalX, "txtRefPointLocalX");
            this.errorProvider1.SetError(this.txtRefPointLocalX, resources.GetString("txtRefPointLocalX.Error"));
            this.errorProvider1.SetIconAlignment(this.txtRefPointLocalX, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtRefPointLocalX.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtRefPointLocalX, ((int)(resources.GetObject("txtRefPointLocalX.IconPadding"))));
            this.txtRefPointLocalX.Name = "txtRefPointLocalX";
            this.toolTip1.SetToolTip(this.txtRefPointLocalX, resources.GetString("txtRefPointLocalX.ToolTip"));
            this.txtRefPointLocalX.Validating += new System.ComponentModel.CancelEventHandler(this.txtRefPointLocalX_Validating);
            // 
            // lblRefPointGeoCoords
            // 
            resources.ApplyResources(this.lblRefPointGeoCoords, "lblRefPointGeoCoords");
            this.errorProvider1.SetError(this.lblRefPointGeoCoords, resources.GetString("lblRefPointGeoCoords.Error"));
            this.errorProvider1.SetIconAlignment(this.lblRefPointGeoCoords, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblRefPointGeoCoords.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblRefPointGeoCoords, ((int)(resources.GetObject("lblRefPointGeoCoords.IconPadding"))));
            this.lblRefPointGeoCoords.Name = "lblRefPointGeoCoords";
            this.toolTip1.SetToolTip(this.lblRefPointGeoCoords, resources.GetString("lblRefPointGeoCoords.ToolTip"));
            // 
            // txtRefPointGeoLon
            // 
            resources.ApplyResources(this.txtRefPointGeoLon, "txtRefPointGeoLon");
            this.errorProvider1.SetError(this.txtRefPointGeoLon, resources.GetString("txtRefPointGeoLon.Error"));
            this.errorProvider1.SetIconAlignment(this.txtRefPointGeoLon, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtRefPointGeoLon.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtRefPointGeoLon, ((int)(resources.GetObject("txtRefPointGeoLon.IconPadding"))));
            this.txtRefPointGeoLon.Name = "txtRefPointGeoLon";
            this.toolTip1.SetToolTip(this.txtRefPointGeoLon, resources.GetString("txtRefPointGeoLon.ToolTip"));
            this.txtRefPointGeoLon.Validating += new System.ComponentModel.CancelEventHandler(this.txtRefPointGeoLon_Validating);
            // 
            // txtRefPointGeoLat
            // 
            resources.ApplyResources(this.txtRefPointGeoLat, "txtRefPointGeoLat");
            this.errorProvider1.SetError(this.txtRefPointGeoLat, resources.GetString("txtRefPointGeoLat.Error"));
            this.errorProvider1.SetIconAlignment(this.txtRefPointGeoLat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtRefPointGeoLat.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtRefPointGeoLat, ((int)(resources.GetObject("txtRefPointGeoLat.IconPadding"))));
            this.txtRefPointGeoLat.Name = "txtRefPointGeoLat";
            this.toolTip1.SetToolTip(this.txtRefPointGeoLat, resources.GetString("txtRefPointGeoLat.ToolTip"));
            this.txtRefPointGeoLat.Validating += new System.ComponentModel.CancelEventHandler(this.txtRefPointGeoLat_Validating);
            // 
            // lblRefPointGeoLon
            // 
            resources.ApplyResources(this.lblRefPointGeoLon, "lblRefPointGeoLon");
            this.errorProvider1.SetError(this.lblRefPointGeoLon, resources.GetString("lblRefPointGeoLon.Error"));
            this.errorProvider1.SetIconAlignment(this.lblRefPointGeoLon, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblRefPointGeoLon.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblRefPointGeoLon, ((int)(resources.GetObject("lblRefPointGeoLon.IconPadding"))));
            this.lblRefPointGeoLon.Name = "lblRefPointGeoLon";
            this.toolTip1.SetToolTip(this.lblRefPointGeoLon, resources.GetString("lblRefPointGeoLon.ToolTip"));
            // 
            // lblRefPointGeoLat
            // 
            resources.ApplyResources(this.lblRefPointGeoLat, "lblRefPointGeoLat");
            this.errorProvider1.SetError(this.lblRefPointGeoLat, resources.GetString("lblRefPointGeoLat.Error"));
            this.errorProvider1.SetIconAlignment(this.lblRefPointGeoLat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblRefPointGeoLat.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblRefPointGeoLat, ((int)(resources.GetObject("lblRefPointGeoLat.IconPadding"))));
            this.lblRefPointGeoLat.Name = "lblRefPointGeoLat";
            this.toolTip1.SetToolTip(this.lblRefPointGeoLat, resources.GetString("lblRefPointGeoLat.ToolTip"));
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.btnCancel, resources.GetString("btnCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.btnCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnCancel, ((int)(resources.GetObject("btnCancel.IconPadding"))));
            this.btnCancel.Name = "btnCancel";
            this.toolTip1.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.errorProvider1.SetError(this.btnOK, resources.GetString("btnOK.Error"));
            this.errorProvider1.SetIconAlignment(this.btnOK, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnOK.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnOK, ((int)(resources.GetObject("btnOK.IconPadding"))));
            this.btnOK.Name = "btnOK";
            this.toolTip1.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // btnPickPosition
            // 
            resources.ApplyResources(this.btnPickPosition, "btnPickPosition");
            this.errorProvider1.SetError(this.btnPickPosition, resources.GetString("btnPickPosition.Error"));
            this.errorProvider1.SetIconAlignment(this.btnPickPosition, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnPickPosition.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnPickPosition, ((int)(resources.GetObject("btnPickPosition.IconPadding"))));
            this.btnPickPosition.Name = "btnPickPosition";
            this.toolTip1.SetToolTip(this.btnPickPosition, resources.GetString("btnPickPosition.ToolTip"));
            this.btnPickPosition.UseVisualStyleBackColor = true;
            this.btnPickPosition.Click += new System.EventHandler(this.btnPickPosition_Click);
            // 
            // btnPickPositionOnModel
            // 
            resources.ApplyResources(this.btnPickPositionOnModel, "btnPickPositionOnModel");
            this.errorProvider1.SetError(this.btnPickPositionOnModel, resources.GetString("btnPickPositionOnModel.Error"));
            this.errorProvider1.SetIconAlignment(this.btnPickPositionOnModel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnPickPositionOnModel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnPickPositionOnModel, ((int)(resources.GetObject("btnPickPositionOnModel.IconPadding"))));
            this.btnPickPositionOnModel.Name = "btnPickPositionOnModel";
            this.toolTip1.SetToolTip(this.btnPickPositionOnModel, resources.GetString("btnPickPositionOnModel.ToolTip"));
            this.btnPickPositionOnModel.UseVisualStyleBackColor = true;
            this.btnPickPositionOnModel.Click += new System.EventHandler(this.btnPickPositionOnModel_Click);
            // 
            // FormProjParamCalc
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnPickPositionOnModel);
            this.Controls.Add(this.btnPickPosition);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtRefPointGeoLon);
            this.Controls.Add(this.txtRefPointGeoLat);
            this.Controls.Add(this.lblRefPointGeoLon);
            this.Controls.Add(this.lblRefPointGeoLat);
            this.Controls.Add(this.lblRefPointGeoCoords);
            this.Controls.Add(this.txtRefPointLocalX);
            this.Controls.Add(this.txtRefPointLocalY);
            this.Controls.Add(this.lblRefPointLocalY);
            this.Controls.Add(this.lblRefPointLocalX);
            this.Controls.Add(this.lblRefPointLocalCoords);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProjParamCalc";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Activated += new System.EventHandler(this.FormProjParamCalc_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormProjParamCalc_FormClosed);
            this.Load += new System.EventHandler(this.FormProjParamCalc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRefPointLocalCoords;
        private System.Windows.Forms.Label lblRefPointLocalX;
        private System.Windows.Forms.Label lblRefPointLocalY;
        private System.Windows.Forms.TextBox txtRefPointLocalY;
        private System.Windows.Forms.TextBox txtRefPointLocalX;
        private System.Windows.Forms.Label lblRefPointGeoCoords;
        private System.Windows.Forms.TextBox txtRefPointGeoLon;
        private System.Windows.Forms.TextBox txtRefPointGeoLat;
        private System.Windows.Forms.Label lblRefPointGeoLon;
        private System.Windows.Forms.Label lblRefPointGeoLat;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnPickPosition;
        private System.Windows.Forms.Button btnPickPositionOnModel;
    }
}