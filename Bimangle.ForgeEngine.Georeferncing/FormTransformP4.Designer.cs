namespace Bimangle.ForgeEngine.Georeferncing
{
    partial class FormTransformP4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTransformP4));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtProjCoordinateOffset = new System.Windows.Forms.TextBox();
            this.gpTestRun = new System.Windows.Forms.GroupBox();
            this.btnSwapNE = new System.Windows.Forms.Button();
            this.txtWorldHeight = new System.Windows.Forms.TextBox();
            this.lblWorldHeight = new System.Windows.Forms.Label();
            this.txtWorldLat = new System.Windows.Forms.TextBox();
            this.lblWorldLat = new System.Windows.Forms.Label();
            this.txtWorldLon = new System.Windows.Forms.TextBox();
            this.lblWorldLon = new System.Windows.Forms.Label();
            this.lblWorld = new System.Windows.Forms.Label();
            this.txtProjectedH = new System.Windows.Forms.TextBox();
            this.lblProjectedH = new System.Windows.Forms.Label();
            this.txtProjectedN = new System.Windows.Forms.TextBox();
            this.btnTestRun = new System.Windows.Forms.Button();
            this.lblProjectedN = new System.Windows.Forms.Label();
            this.txtProjectedE = new System.Windows.Forms.TextBox();
            this.lblProjectedE = new System.Windows.Forms.Label();
            this.lblProjected = new System.Windows.Forms.Label();
            this.txtModelH = new System.Windows.Forms.TextBox();
            this.lblModelH = new System.Windows.Forms.Label();
            this.txtModelN = new System.Windows.Forms.TextBox();
            this.lblModelN = new System.Windows.Forms.Label();
            this.txtModelE = new System.Windows.Forms.TextBox();
            this.lblModelE = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblProjCoordinateOffset = new System.Windows.Forms.Label();
            this.gpDescription = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.gpParameters = new System.Windows.Forms.GroupBox();
            this.cbUsed = new System.Windows.Forms.CheckBox();
            this.txtScalingFactor = new System.Windows.Forms.TextBox();
            this.lblScalingFactor = new System.Windows.Forms.Label();
            this.txtK = new System.Windows.Forms.TextBox();
            this.lblK = new System.Windows.Forms.Label();
            this.lblScaling = new System.Windows.Forms.Label();
            this.txtWz = new System.Windows.Forms.TextBox();
            this.lblWz = new System.Windows.Forms.Label();
            this.lblRotation = new System.Windows.Forms.Label();
            this.txtDy = new System.Windows.Forms.TextBox();
            this.txtDx = new System.Windows.Forms.TextBox();
            this.lblDy = new System.Windows.Forms.Label();
            this.lblDx = new System.Windows.Forms.Label();
            this.lblTranslation = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.gpTestRun.SuspendLayout();
            this.gpDescription.SuspendLayout();
            this.gpParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtProjCoordinateOffset
            // 
            resources.ApplyResources(this.txtProjCoordinateOffset, "txtProjCoordinateOffset");
            this.txtProjCoordinateOffset.Name = "txtProjCoordinateOffset";
            this.txtProjCoordinateOffset.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtProjCoordinateOffset, resources.GetString("txtProjCoordinateOffset.ToolTip"));
            // 
            // gpTestRun
            // 
            resources.ApplyResources(this.gpTestRun, "gpTestRun");
            this.gpTestRun.Controls.Add(this.btnSwapNE);
            this.gpTestRun.Controls.Add(this.txtWorldHeight);
            this.gpTestRun.Controls.Add(this.lblWorldHeight);
            this.gpTestRun.Controls.Add(this.txtWorldLat);
            this.gpTestRun.Controls.Add(this.lblWorldLat);
            this.gpTestRun.Controls.Add(this.txtWorldLon);
            this.gpTestRun.Controls.Add(this.lblWorldLon);
            this.gpTestRun.Controls.Add(this.lblWorld);
            this.gpTestRun.Controls.Add(this.txtProjectedH);
            this.gpTestRun.Controls.Add(this.lblProjectedH);
            this.gpTestRun.Controls.Add(this.txtProjectedN);
            this.gpTestRun.Controls.Add(this.btnTestRun);
            this.gpTestRun.Controls.Add(this.lblProjectedN);
            this.gpTestRun.Controls.Add(this.txtProjectedE);
            this.gpTestRun.Controls.Add(this.lblProjectedE);
            this.gpTestRun.Controls.Add(this.lblProjected);
            this.gpTestRun.Controls.Add(this.txtModelH);
            this.gpTestRun.Controls.Add(this.lblModelH);
            this.gpTestRun.Controls.Add(this.txtModelN);
            this.gpTestRun.Controls.Add(this.lblModelN);
            this.gpTestRun.Controls.Add(this.txtModelE);
            this.gpTestRun.Controls.Add(this.lblModelE);
            this.gpTestRun.Controls.Add(this.lblModel);
            this.gpTestRun.Controls.Add(this.txtProjCoordinateOffset);
            this.gpTestRun.Controls.Add(this.lblProjCoordinateOffset);
            this.gpTestRun.Name = "gpTestRun";
            this.gpTestRun.TabStop = false;
            // 
            // btnSwapNE
            // 
            this.btnSwapNE.CausesValidation = false;
            resources.ApplyResources(this.btnSwapNE, "btnSwapNE");
            this.btnSwapNE.Name = "btnSwapNE";
            this.btnSwapNE.UseVisualStyleBackColor = true;
            this.btnSwapNE.Click += new System.EventHandler(this.btnSwapNE_Click);
            // 
            // txtWorldHeight
            // 
            resources.ApplyResources(this.txtWorldHeight, "txtWorldHeight");
            this.txtWorldHeight.ForeColor = System.Drawing.Color.Gray;
            this.txtWorldHeight.Name = "txtWorldHeight";
            this.txtWorldHeight.ReadOnly = true;
            // 
            // lblWorldHeight
            // 
            resources.ApplyResources(this.lblWorldHeight, "lblWorldHeight");
            this.lblWorldHeight.ForeColor = System.Drawing.Color.Gray;
            this.lblWorldHeight.Name = "lblWorldHeight";
            // 
            // txtWorldLat
            // 
            resources.ApplyResources(this.txtWorldLat, "txtWorldLat");
            this.txtWorldLat.ForeColor = System.Drawing.Color.Gray;
            this.txtWorldLat.Name = "txtWorldLat";
            this.txtWorldLat.ReadOnly = true;
            // 
            // lblWorldLat
            // 
            resources.ApplyResources(this.lblWorldLat, "lblWorldLat");
            this.lblWorldLat.ForeColor = System.Drawing.Color.Gray;
            this.lblWorldLat.Name = "lblWorldLat";
            // 
            // txtWorldLon
            // 
            resources.ApplyResources(this.txtWorldLon, "txtWorldLon");
            this.txtWorldLon.ForeColor = System.Drawing.Color.Gray;
            this.txtWorldLon.Name = "txtWorldLon";
            this.txtWorldLon.ReadOnly = true;
            // 
            // lblWorldLon
            // 
            resources.ApplyResources(this.lblWorldLon, "lblWorldLon");
            this.lblWorldLon.ForeColor = System.Drawing.Color.Gray;
            this.lblWorldLon.Name = "lblWorldLon";
            // 
            // lblWorld
            // 
            resources.ApplyResources(this.lblWorld, "lblWorld");
            this.lblWorld.ForeColor = System.Drawing.Color.Gray;
            this.lblWorld.Name = "lblWorld";
            // 
            // txtProjectedH
            // 
            resources.ApplyResources(this.txtProjectedH, "txtProjectedH");
            this.txtProjectedH.Name = "txtProjectedH";
            this.txtProjectedH.ReadOnly = true;
            // 
            // lblProjectedH
            // 
            resources.ApplyResources(this.lblProjectedH, "lblProjectedH");
            this.lblProjectedH.Name = "lblProjectedH";
            // 
            // txtProjectedN
            // 
            resources.ApplyResources(this.txtProjectedN, "txtProjectedN");
            this.txtProjectedN.Name = "txtProjectedN";
            this.txtProjectedN.ReadOnly = true;
            // 
            // btnTestRun
            // 
            resources.ApplyResources(this.btnTestRun, "btnTestRun");
            this.btnTestRun.Name = "btnTestRun";
            this.btnTestRun.UseVisualStyleBackColor = true;
            this.btnTestRun.Click += new System.EventHandler(this.btnTestRun_Click);
            // 
            // lblProjectedN
            // 
            resources.ApplyResources(this.lblProjectedN, "lblProjectedN");
            this.lblProjectedN.Name = "lblProjectedN";
            // 
            // txtProjectedE
            // 
            resources.ApplyResources(this.txtProjectedE, "txtProjectedE");
            this.txtProjectedE.Name = "txtProjectedE";
            this.txtProjectedE.ReadOnly = true;
            // 
            // lblProjectedE
            // 
            resources.ApplyResources(this.lblProjectedE, "lblProjectedE");
            this.lblProjectedE.Name = "lblProjectedE";
            // 
            // lblProjected
            // 
            resources.ApplyResources(this.lblProjected, "lblProjected");
            this.lblProjected.Name = "lblProjected";
            // 
            // txtModelH
            // 
            resources.ApplyResources(this.txtModelH, "txtModelH");
            this.txtModelH.Name = "txtModelH";
            // 
            // lblModelH
            // 
            resources.ApplyResources(this.lblModelH, "lblModelH");
            this.lblModelH.Name = "lblModelH";
            // 
            // txtModelN
            // 
            resources.ApplyResources(this.txtModelN, "txtModelN");
            this.txtModelN.Name = "txtModelN";
            // 
            // lblModelN
            // 
            resources.ApplyResources(this.lblModelN, "lblModelN");
            this.lblModelN.Name = "lblModelN";
            // 
            // txtModelE
            // 
            resources.ApplyResources(this.txtModelE, "txtModelE");
            this.txtModelE.Name = "txtModelE";
            // 
            // lblModelE
            // 
            resources.ApplyResources(this.lblModelE, "lblModelE");
            this.lblModelE.Name = "lblModelE";
            // 
            // lblModel
            // 
            resources.ApplyResources(this.lblModel, "lblModel");
            this.lblModel.Name = "lblModel";
            // 
            // lblProjCoordinateOffset
            // 
            resources.ApplyResources(this.lblProjCoordinateOffset, "lblProjCoordinateOffset");
            this.lblProjCoordinateOffset.Name = "lblProjCoordinateOffset";
            // 
            // gpDescription
            // 
            resources.ApplyResources(this.gpDescription, "gpDescription");
            this.gpDescription.Controls.Add(this.txtDescription);
            this.gpDescription.Name = "gpDescription";
            this.gpDescription.TabStop = false;
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            // 
            // gpParameters
            // 
            resources.ApplyResources(this.gpParameters, "gpParameters");
            this.gpParameters.Controls.Add(this.cbUsed);
            this.gpParameters.Controls.Add(this.txtScalingFactor);
            this.gpParameters.Controls.Add(this.lblScalingFactor);
            this.gpParameters.Controls.Add(this.txtK);
            this.gpParameters.Controls.Add(this.lblK);
            this.gpParameters.Controls.Add(this.lblScaling);
            this.gpParameters.Controls.Add(this.txtWz);
            this.gpParameters.Controls.Add(this.lblWz);
            this.gpParameters.Controls.Add(this.lblRotation);
            this.gpParameters.Controls.Add(this.txtDy);
            this.gpParameters.Controls.Add(this.txtDx);
            this.gpParameters.Controls.Add(this.lblDy);
            this.gpParameters.Controls.Add(this.lblDx);
            this.gpParameters.Controls.Add(this.lblTranslation);
            this.gpParameters.Name = "gpParameters";
            this.gpParameters.TabStop = false;
            // 
            // cbUsed
            // 
            resources.ApplyResources(this.cbUsed, "cbUsed");
            this.cbUsed.Checked = true;
            this.cbUsed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUsed.Name = "cbUsed";
            this.cbUsed.UseVisualStyleBackColor = true;
            // 
            // txtScalingFactor
            // 
            resources.ApplyResources(this.txtScalingFactor, "txtScalingFactor");
            this.txtScalingFactor.ForeColor = System.Drawing.Color.Gray;
            this.txtScalingFactor.Name = "txtScalingFactor";
            this.txtScalingFactor.ReadOnly = true;
            // 
            // lblScalingFactor
            // 
            resources.ApplyResources(this.lblScalingFactor, "lblScalingFactor");
            this.lblScalingFactor.ForeColor = System.Drawing.Color.Gray;
            this.lblScalingFactor.Name = "lblScalingFactor";
            // 
            // txtK
            // 
            resources.ApplyResources(this.txtK, "txtK");
            this.txtK.Name = "txtK";
            // 
            // lblK
            // 
            resources.ApplyResources(this.lblK, "lblK");
            this.lblK.Name = "lblK";
            // 
            // lblScaling
            // 
            resources.ApplyResources(this.lblScaling, "lblScaling");
            this.lblScaling.Name = "lblScaling";
            // 
            // txtWz
            // 
            resources.ApplyResources(this.txtWz, "txtWz");
            this.txtWz.Name = "txtWz";
            // 
            // lblWz
            // 
            resources.ApplyResources(this.lblWz, "lblWz");
            this.lblWz.Name = "lblWz";
            // 
            // lblRotation
            // 
            resources.ApplyResources(this.lblRotation, "lblRotation");
            this.lblRotation.Name = "lblRotation";
            // 
            // txtDy
            // 
            resources.ApplyResources(this.txtDy, "txtDy");
            this.txtDy.Name = "txtDy";
            // 
            // txtDx
            // 
            resources.ApplyResources(this.txtDx, "txtDx");
            this.txtDx.Name = "txtDx";
            // 
            // lblDy
            // 
            resources.ApplyResources(this.lblDy, "lblDy");
            this.lblDy.Name = "lblDy";
            // 
            // lblDx
            // 
            resources.ApplyResources(this.lblDx, "lblDx");
            this.lblDx.Name = "lblDx";
            // 
            // lblTranslation
            // 
            resources.ApplyResources(this.lblTranslation, "lblTranslation");
            this.lblTranslation.Name = "lblTranslation";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnRestore
            // 
            resources.ApplyResources(this.btnRestore, "btnRestore");
            this.btnRestore.CausesValidation = false;
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // FormTransformP4
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gpParameters);
            this.Controls.Add(this.gpDescription);
            this.Controls.Add(this.gpTestRun);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTransformP4";
            this.Load += new System.EventHandler(this.FormGeoreferncingTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.gpTestRun.ResumeLayout(false);
            this.gpTestRun.PerformLayout();
            this.gpDescription.ResumeLayout(false);
            this.gpDescription.PerformLayout();
            this.gpParameters.ResumeLayout(false);
            this.gpParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gpTestRun;
        private System.Windows.Forms.TextBox txtProjCoordinateOffset;
        private System.Windows.Forms.Label lblProjCoordinateOffset;
        private System.Windows.Forms.Button btnTestRun;
        private System.Windows.Forms.GroupBox gpDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox gpParameters;
        private System.Windows.Forms.Label lblTranslation;
        private System.Windows.Forms.TextBox txtDy;
        private System.Windows.Forms.TextBox txtDx;
        private System.Windows.Forms.Label lblDy;
        private System.Windows.Forms.Label lblDx;
        private System.Windows.Forms.Label lblScaling;
        private System.Windows.Forms.TextBox txtWz;
        private System.Windows.Forms.Label lblWz;
        private System.Windows.Forms.Label lblRotation;
        private System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Label lblK;
        private System.Windows.Forms.TextBox txtProjectedH;
        private System.Windows.Forms.Label lblProjectedH;
        private System.Windows.Forms.TextBox txtProjectedN;
        private System.Windows.Forms.Label lblProjectedN;
        private System.Windows.Forms.TextBox txtProjectedE;
        private System.Windows.Forms.Label lblProjectedE;
        private System.Windows.Forms.Label lblProjected;
        private System.Windows.Forms.TextBox txtModelH;
        private System.Windows.Forms.Label lblModelH;
        private System.Windows.Forms.TextBox txtModelN;
        private System.Windows.Forms.Label lblModelN;
        private System.Windows.Forms.TextBox txtModelE;
        private System.Windows.Forms.Label lblModelE;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtWorldHeight;
        private System.Windows.Forms.Label lblWorldHeight;
        private System.Windows.Forms.TextBox txtWorldLat;
        private System.Windows.Forms.Label lblWorldLat;
        private System.Windows.Forms.TextBox txtWorldLon;
        private System.Windows.Forms.Label lblWorldLon;
        private System.Windows.Forms.Label lblWorld;
        private System.Windows.Forms.TextBox txtScalingFactor;
        private System.Windows.Forms.Label lblScalingFactor;
        private System.Windows.Forms.Button btnSwapNE;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox cbUsed;
    }
}