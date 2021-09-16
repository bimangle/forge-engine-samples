namespace Bimangle.ForgeEngine.Dgn.Georeferncing
{
    partial class FormProjCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProjCreate));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtCentralMeridian = new System.Windows.Forms.TextBox();
            this.txtFalseEasting = new System.Windows.Forms.TextBox();
            this.txtFalseNorthing = new System.Windows.Forms.TextBox();
            this.gpParameter = new System.Windows.Forms.GroupBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblGcs = new System.Windows.Forms.Label();
            this.cbGcs = new System.Windows.Forms.ComboBox();
            this.lblCentralMeridian = new System.Windows.Forms.Label();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblFalseEasting = new System.Windows.Forms.Label();
            this.lblFalseNorthing = new System.Windows.Forms.Label();
            this.gpDefinition = new System.Windows.Forms.GroupBox();
            this.txtDefinition = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.gpParameter.SuspendLayout();
            this.gpDefinition.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtCentralMeridian
            // 
            resources.ApplyResources(this.txtCentralMeridian, "txtCentralMeridian");
            this.txtCentralMeridian.Name = "txtCentralMeridian";
            this.toolTip1.SetToolTip(this.txtCentralMeridian, resources.GetString("txtCentralMeridian.ToolTip"));
            this.txtCentralMeridian.Validating += new System.ComponentModel.CancelEventHandler(this.txtCentralMeridian_Validating);
            // 
            // txtFalseEasting
            // 
            resources.ApplyResources(this.txtFalseEasting, "txtFalseEasting");
            this.txtFalseEasting.Name = "txtFalseEasting";
            this.toolTip1.SetToolTip(this.txtFalseEasting, resources.GetString("txtFalseEasting.ToolTip"));
            this.txtFalseEasting.Validating += new System.ComponentModel.CancelEventHandler(this.txtFalseEasting_Validating);
            // 
            // txtFalseNorthing
            // 
            resources.ApplyResources(this.txtFalseNorthing, "txtFalseNorthing");
            this.txtFalseNorthing.Name = "txtFalseNorthing";
            this.toolTip1.SetToolTip(this.txtFalseNorthing, resources.GetString("txtFalseNorthing.ToolTip"));
            this.txtFalseNorthing.Validating += new System.ComponentModel.CancelEventHandler(this.txtFalseNorthing_Validating);
            // 
            // gpParameter
            // 
            resources.ApplyResources(this.gpParameter, "gpParameter");
            this.gpParameter.Controls.Add(this.btnGenerate);
            this.gpParameter.Controls.Add(this.lblGcs);
            this.gpParameter.Controls.Add(this.cbGcs);
            this.gpParameter.Controls.Add(this.lblCentralMeridian);
            this.gpParameter.Controls.Add(this.btnCalc);
            this.gpParameter.Controls.Add(this.txtCentralMeridian);
            this.gpParameter.Controls.Add(this.txtFalseNorthing);
            this.gpParameter.Controls.Add(this.lblFalseEasting);
            this.gpParameter.Controls.Add(this.txtFalseEasting);
            this.gpParameter.Controls.Add(this.lblFalseNorthing);
            this.gpParameter.Name = "gpParameter";
            this.gpParameter.TabStop = false;
            // 
            // btnGenerate
            // 
            resources.ApplyResources(this.btnGenerate, "btnGenerate");
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblGcs
            // 
            resources.ApplyResources(this.lblGcs, "lblGcs");
            this.lblGcs.Name = "lblGcs";
            // 
            // cbGcs
            // 
            resources.ApplyResources(this.cbGcs, "cbGcs");
            this.cbGcs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGcs.FormattingEnabled = true;
            this.cbGcs.Name = "cbGcs";
            // 
            // lblCentralMeridian
            // 
            resources.ApplyResources(this.lblCentralMeridian, "lblCentralMeridian");
            this.lblCentralMeridian.Name = "lblCentralMeridian";
            // 
            // btnCalc
            // 
            resources.ApplyResources(this.btnCalc, "btnCalc");
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // lblFalseEasting
            // 
            resources.ApplyResources(this.lblFalseEasting, "lblFalseEasting");
            this.lblFalseEasting.Name = "lblFalseEasting";
            // 
            // lblFalseNorthing
            // 
            resources.ApplyResources(this.lblFalseNorthing, "lblFalseNorthing");
            this.lblFalseNorthing.Name = "lblFalseNorthing";
            // 
            // gpDefinition
            // 
            resources.ApplyResources(this.gpDefinition, "gpDefinition");
            this.gpDefinition.Controls.Add(this.txtDefinition);
            this.gpDefinition.Name = "gpDefinition";
            this.gpDefinition.TabStop = false;
            // 
            // txtDefinition
            // 
            resources.ApplyResources(this.txtDefinition, "txtDefinition");
            this.txtDefinition.Name = "txtDefinition";
            this.txtDefinition.Enter += new System.EventHandler(this.txtDefinition_Enter);
            this.txtDefinition.Validating += new System.ComponentModel.CancelEventHandler(this.txtDefinition_Validating);
            // 
            // FormProjCreate
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.gpDefinition);
            this.Controls.Add(this.gpParameter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProjCreate";
            this.Load += new System.EventHandler(this.FormProjCreate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.gpParameter.ResumeLayout(false);
            this.gpParameter.PerformLayout();
            this.gpDefinition.ResumeLayout(false);
            this.gpDefinition.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gpDefinition;
        private System.Windows.Forms.GroupBox gpParameter;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblGcs;
        private System.Windows.Forms.ComboBox cbGcs;
        private System.Windows.Forms.Label lblCentralMeridian;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.TextBox txtCentralMeridian;
        private System.Windows.Forms.TextBox txtFalseNorthing;
        private System.Windows.Forms.Label lblFalseEasting;
        private System.Windows.Forms.TextBox txtFalseEasting;
        private System.Windows.Forms.Label lblFalseNorthing;
        private System.Windows.Forms.TextBox txtDefinition;
    }
}