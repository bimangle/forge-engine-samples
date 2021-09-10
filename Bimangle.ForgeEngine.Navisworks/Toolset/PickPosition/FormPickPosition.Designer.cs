namespace Bimangle.ForgeEngine.Navisworks.Toolset.PickPosition
{
    partial class FormPickPosition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPickPosition));
            this.lblOrigin = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.lblZ = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cbOrigin = new System.Windows.Forms.ComboBox();
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnPick = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOrigin
            // 
            resources.ApplyResources(this.lblOrigin, "lblOrigin");
            this.lblOrigin.Name = "lblOrigin";
            // 
            // txtX
            // 
            resources.ApplyResources(this.txtX, "txtX");
            this.txtX.Name = "txtX";
            this.txtX.ReadOnly = true;
            // 
            // txtY
            // 
            resources.ApplyResources(this.txtY, "txtY");
            this.txtY.Name = "txtY";
            this.txtY.ReadOnly = true;
            // 
            // lblY
            // 
            resources.ApplyResources(this.lblY, "lblY");
            this.lblY.Name = "lblY";
            // 
            // lblX
            // 
            resources.ApplyResources(this.lblX, "lblX");
            this.lblX.Name = "lblX";
            // 
            // txtZ
            // 
            resources.ApplyResources(this.txtZ, "txtZ");
            this.txtZ.Name = "txtZ";
            this.txtZ.ReadOnly = true;
            // 
            // lblZ
            // 
            resources.ApplyResources(this.lblZ, "lblZ");
            this.lblZ.Name = "lblZ";
            // 
            // lblUnit
            // 
            resources.ApplyResources(this.lblUnit, "lblUnit");
            this.lblUnit.Name = "lblUnit";
            // 
            // cbOrigin
            // 
            resources.ApplyResources(this.cbOrigin, "cbOrigin");
            this.cbOrigin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrigin.FormattingEnabled = true;
            this.cbOrigin.Name = "cbOrigin";
            this.cbOrigin.SelectedIndexChanged += new System.EventHandler(this.cbOrigin_SelectedIndexChanged);
            // 
            // cbUnit
            // 
            resources.ApplyResources(this.cbUnit, "cbUnit");
            this.cbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.SelectedIndexChanged += new System.EventHandler(this.cbUnit_SelectedIndexChanged);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnPick
            // 
            resources.ApplyResources(this.btnPick, "btnPick");
            this.btnPick.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPick.Name = "btnPick";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // FormPickPosition
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPick);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbUnit);
            this.Controls.Add(this.cbOrigin);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.txtZ);
            this.Controls.Add(this.lblZ);
            this.Controls.Add(this.lblOrigin);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblX);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPickPosition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPickPosition_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPickPosition_FormClosed);
            this.Load += new System.EventHandler(this.FormPickPosition_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.ComboBox cbOrigin;
        private System.Windows.Forms.ComboBox cbUnit;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnPick;
    }
}