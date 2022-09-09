namespace Bimangle.ForgeEngine.Georeferncing
{
    partial class FormGeoreferncing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGeoreferncing));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageAuto = new System.Windows.Forms.TabPage();
            this.txtAutoSummary = new System.Windows.Forms.TextBox();
            this.cbAutoOriginLocation = new System.Windows.Forms.ComboBox();
            this.lblAutoOriginLocation = new System.Windows.Forms.Label();
            this.tabPageEnu = new System.Windows.Forms.TabPage();
            this.cbEnuAutoAlignToGround = new System.Windows.Forms.CheckBox();
            this.lblEnuAdvanced = new System.Windows.Forms.Label();
            this.cbEnuAlignOriginToSitePlaneCenter = new System.Windows.Forms.CheckBox();
            this.cbEnuUseProjectLocation = new System.Windows.Forms.CheckBox();
            this.txtEnuRotation = new System.Windows.Forms.TextBox();
            this.lblEnuRotation = new System.Windows.Forms.Label();
            this.txtEnuHeight = new System.Windows.Forms.TextBox();
            this.lblEnuHeight = new System.Windows.Forms.Label();
            this.txtEnuLongitude = new System.Windows.Forms.TextBox();
            this.lblEnuLongitude = new System.Windows.Forms.Label();
            this.txtEnuLatitude = new System.Windows.Forms.TextBox();
            this.lblEnuLatitude = new System.Windows.Forms.Label();
            this.lblEnuOriginCoordinate = new System.Windows.Forms.Label();
            this.cbEnuOriginLocation = new System.Windows.Forms.ComboBox();
            this.lblEnuOriginLocation = new System.Windows.Forms.Label();
            this.tabPageLocal = new System.Windows.Forms.TabPage();
            this.cbLocalAlignOriginToSitePlaneCenter = new System.Windows.Forms.CheckBox();
            this.cbLocalUseProjectLocation = new System.Windows.Forms.CheckBox();
            this.txtLocalRotation = new System.Windows.Forms.TextBox();
            this.lblLocalRotation = new System.Windows.Forms.Label();
            this.txtLocalHeight = new System.Windows.Forms.TextBox();
            this.lblLocalHeight = new System.Windows.Forms.Label();
            this.txtLocalLongitude = new System.Windows.Forms.TextBox();
            this.lblLocalLongitude = new System.Windows.Forms.Label();
            this.txtLocalLatitude = new System.Windows.Forms.TextBox();
            this.lblLocalLatitude = new System.Windows.Forms.Label();
            this.lblLocalOriginCoordinate = new System.Windows.Forms.Label();
            this.cbLocalOriginLocation = new System.Windows.Forms.ComboBox();
            this.lblLocalOriginLocation = new System.Windows.Forms.Label();
            this.tabPageProj = new System.Windows.Forms.TabPage();
            this.btnProjDefinitionSave = new System.Windows.Forms.Button();
            this.btnProjCoordinateOffsetSave = new System.Windows.Forms.Button();
            this.txtProjDefinition = new System.Windows.Forms.TextBox();
            this.cbProjDefinition = new System.Windows.Forms.ComboBox();
            this.txtProjCoordinateOffset = new System.Windows.Forms.TextBox();
            this.lblProjCoordinateOffset = new System.Windows.Forms.Label();
            this.lblProjDefinition = new System.Windows.Forms.Label();
            this.cbProjOriginLocation = new System.Windows.Forms.ComboBox();
            this.lblProjOriginLocation = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabMain.SuspendLayout();
            this.tabPageAuto.SuspendLayout();
            this.tabPageEnu.SuspendLayout();
            this.tabPageLocal.SuspendLayout();
            this.tabPageProj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            resources.ApplyResources(this.tabMain, "tabMain");
            this.tabMain.Controls.Add(this.tabPageAuto);
            this.tabMain.Controls.Add(this.tabPageEnu);
            this.tabMain.Controls.Add(this.tabPageLocal);
            this.tabMain.Controls.Add(this.tabPageProj);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPageAuto
            // 
            this.tabPageAuto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageAuto.Controls.Add(this.txtAutoSummary);
            this.tabPageAuto.Controls.Add(this.cbAutoOriginLocation);
            this.tabPageAuto.Controls.Add(this.lblAutoOriginLocation);
            resources.ApplyResources(this.tabPageAuto, "tabPageAuto");
            this.tabPageAuto.Name = "tabPageAuto";
            this.tabPageAuto.UseVisualStyleBackColor = true;
            // 
            // txtAutoSummary
            // 
            resources.ApplyResources(this.txtAutoSummary, "txtAutoSummary");
            this.txtAutoSummary.Name = "txtAutoSummary";
            this.txtAutoSummary.ReadOnly = true;
            // 
            // cbAutoOriginLocation
            // 
            resources.ApplyResources(this.cbAutoOriginLocation, "cbAutoOriginLocation");
            this.cbAutoOriginLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoOriginLocation.DropDownWidth = 400;
            this.cbAutoOriginLocation.FormattingEnabled = true;
            this.cbAutoOriginLocation.Name = "cbAutoOriginLocation";
            this.cbAutoOriginLocation.SelectedIndexChanged += new System.EventHandler(this.cbAutoOriginLocation_SelectedIndexChanged);
            // 
            // lblAutoOriginLocation
            // 
            resources.ApplyResources(this.lblAutoOriginLocation, "lblAutoOriginLocation");
            this.lblAutoOriginLocation.Name = "lblAutoOriginLocation";
            // 
            // tabPageEnu
            // 
            this.tabPageEnu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageEnu.Controls.Add(this.cbEnuAutoAlignToGround);
            this.tabPageEnu.Controls.Add(this.lblEnuAdvanced);
            this.tabPageEnu.Controls.Add(this.cbEnuAlignOriginToSitePlaneCenter);
            this.tabPageEnu.Controls.Add(this.cbEnuUseProjectLocation);
            this.tabPageEnu.Controls.Add(this.txtEnuRotation);
            this.tabPageEnu.Controls.Add(this.lblEnuRotation);
            this.tabPageEnu.Controls.Add(this.txtEnuHeight);
            this.tabPageEnu.Controls.Add(this.lblEnuHeight);
            this.tabPageEnu.Controls.Add(this.txtEnuLongitude);
            this.tabPageEnu.Controls.Add(this.lblEnuLongitude);
            this.tabPageEnu.Controls.Add(this.txtEnuLatitude);
            this.tabPageEnu.Controls.Add(this.lblEnuLatitude);
            this.tabPageEnu.Controls.Add(this.lblEnuOriginCoordinate);
            this.tabPageEnu.Controls.Add(this.cbEnuOriginLocation);
            this.tabPageEnu.Controls.Add(this.lblEnuOriginLocation);
            resources.ApplyResources(this.tabPageEnu, "tabPageEnu");
            this.tabPageEnu.Name = "tabPageEnu";
            this.tabPageEnu.UseVisualStyleBackColor = true;
            // 
            // cbEnuAutoAlignToGround
            // 
            resources.ApplyResources(this.cbEnuAutoAlignToGround, "cbEnuAutoAlignToGround");
            this.cbEnuAutoAlignToGround.Name = "cbEnuAutoAlignToGround";
            this.cbEnuAutoAlignToGround.UseVisualStyleBackColor = true;
            // 
            // lblEnuAdvanced
            // 
            resources.ApplyResources(this.lblEnuAdvanced, "lblEnuAdvanced");
            this.lblEnuAdvanced.Name = "lblEnuAdvanced";
            // 
            // cbEnuAlignOriginToSitePlaneCenter
            // 
            resources.ApplyResources(this.cbEnuAlignOriginToSitePlaneCenter, "cbEnuAlignOriginToSitePlaneCenter");
            this.cbEnuAlignOriginToSitePlaneCenter.Name = "cbEnuAlignOriginToSitePlaneCenter";
            this.cbEnuAlignOriginToSitePlaneCenter.UseVisualStyleBackColor = true;
            // 
            // cbEnuUseProjectLocation
            // 
            resources.ApplyResources(this.cbEnuUseProjectLocation, "cbEnuUseProjectLocation");
            this.cbEnuUseProjectLocation.Name = "cbEnuUseProjectLocation";
            this.cbEnuUseProjectLocation.UseVisualStyleBackColor = true;
            this.cbEnuUseProjectLocation.CheckedChanged += new System.EventHandler(this.cbEnuUseProjectLocation_CheckedChanged);
            // 
            // txtEnuRotation
            // 
            resources.ApplyResources(this.txtEnuRotation, "txtEnuRotation");
            this.txtEnuRotation.Name = "txtEnuRotation";
            this.toolTip1.SetToolTip(this.txtEnuRotation, resources.GetString("txtEnuRotation.ToolTip"));
            this.txtEnuRotation.Validating += new System.ComponentModel.CancelEventHandler(this.txtEnuRotation_Validating);
            // 
            // lblEnuRotation
            // 
            resources.ApplyResources(this.lblEnuRotation, "lblEnuRotation");
            this.lblEnuRotation.Name = "lblEnuRotation";
            // 
            // txtEnuHeight
            // 
            resources.ApplyResources(this.txtEnuHeight, "txtEnuHeight");
            this.txtEnuHeight.Name = "txtEnuHeight";
            this.toolTip1.SetToolTip(this.txtEnuHeight, resources.GetString("txtEnuHeight.ToolTip"));
            this.txtEnuHeight.Validating += new System.ComponentModel.CancelEventHandler(this.txtEnuHeight_Validating);
            // 
            // lblEnuHeight
            // 
            resources.ApplyResources(this.lblEnuHeight, "lblEnuHeight");
            this.lblEnuHeight.Name = "lblEnuHeight";
            // 
            // txtEnuLongitude
            // 
            resources.ApplyResources(this.txtEnuLongitude, "txtEnuLongitude");
            this.txtEnuLongitude.Name = "txtEnuLongitude";
            this.toolTip1.SetToolTip(this.txtEnuLongitude, resources.GetString("txtEnuLongitude.ToolTip"));
            this.txtEnuLongitude.Validating += new System.ComponentModel.CancelEventHandler(this.txtEnuLongitude_Validating);
            // 
            // lblEnuLongitude
            // 
            resources.ApplyResources(this.lblEnuLongitude, "lblEnuLongitude");
            this.lblEnuLongitude.Name = "lblEnuLongitude";
            // 
            // txtEnuLatitude
            // 
            resources.ApplyResources(this.txtEnuLatitude, "txtEnuLatitude");
            this.txtEnuLatitude.Name = "txtEnuLatitude";
            this.toolTip1.SetToolTip(this.txtEnuLatitude, resources.GetString("txtEnuLatitude.ToolTip"));
            this.txtEnuLatitude.Validating += new System.ComponentModel.CancelEventHandler(this.txtEnuLatitude_Validating);
            // 
            // lblEnuLatitude
            // 
            resources.ApplyResources(this.lblEnuLatitude, "lblEnuLatitude");
            this.lblEnuLatitude.Name = "lblEnuLatitude";
            // 
            // lblEnuOriginCoordinate
            // 
            resources.ApplyResources(this.lblEnuOriginCoordinate, "lblEnuOriginCoordinate");
            this.lblEnuOriginCoordinate.Name = "lblEnuOriginCoordinate";
            // 
            // cbEnuOriginLocation
            // 
            resources.ApplyResources(this.cbEnuOriginLocation, "cbEnuOriginLocation");
            this.cbEnuOriginLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEnuOriginLocation.FormattingEnabled = true;
            this.cbEnuOriginLocation.Name = "cbEnuOriginLocation";
            this.cbEnuOriginLocation.SelectedIndexChanged += new System.EventHandler(this.cbEnuOriginLocation_SelectedIndexChanged);
            // 
            // lblEnuOriginLocation
            // 
            resources.ApplyResources(this.lblEnuOriginLocation, "lblEnuOriginLocation");
            this.lblEnuOriginLocation.Name = "lblEnuOriginLocation";
            // 
            // tabPageLocal
            // 
            this.tabPageLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageLocal.Controls.Add(this.cbLocalAlignOriginToSitePlaneCenter);
            this.tabPageLocal.Controls.Add(this.cbLocalUseProjectLocation);
            this.tabPageLocal.Controls.Add(this.txtLocalRotation);
            this.tabPageLocal.Controls.Add(this.lblLocalRotation);
            this.tabPageLocal.Controls.Add(this.txtLocalHeight);
            this.tabPageLocal.Controls.Add(this.lblLocalHeight);
            this.tabPageLocal.Controls.Add(this.txtLocalLongitude);
            this.tabPageLocal.Controls.Add(this.lblLocalLongitude);
            this.tabPageLocal.Controls.Add(this.txtLocalLatitude);
            this.tabPageLocal.Controls.Add(this.lblLocalLatitude);
            this.tabPageLocal.Controls.Add(this.lblLocalOriginCoordinate);
            this.tabPageLocal.Controls.Add(this.cbLocalOriginLocation);
            this.tabPageLocal.Controls.Add(this.lblLocalOriginLocation);
            resources.ApplyResources(this.tabPageLocal, "tabPageLocal");
            this.tabPageLocal.Name = "tabPageLocal";
            this.tabPageLocal.UseVisualStyleBackColor = true;
            // 
            // cbLocalAlignOriginToSitePlaneCenter
            // 
            resources.ApplyResources(this.cbLocalAlignOriginToSitePlaneCenter, "cbLocalAlignOriginToSitePlaneCenter");
            this.cbLocalAlignOriginToSitePlaneCenter.Name = "cbLocalAlignOriginToSitePlaneCenter";
            this.cbLocalAlignOriginToSitePlaneCenter.UseVisualStyleBackColor = true;
            // 
            // cbLocalUseProjectLocation
            // 
            resources.ApplyResources(this.cbLocalUseProjectLocation, "cbLocalUseProjectLocation");
            this.cbLocalUseProjectLocation.Name = "cbLocalUseProjectLocation";
            this.cbLocalUseProjectLocation.UseVisualStyleBackColor = true;
            this.cbLocalUseProjectLocation.CheckedChanged += new System.EventHandler(this.cbLocalUseProjectLocation_CheckedChanged);
            // 
            // txtLocalRotation
            // 
            resources.ApplyResources(this.txtLocalRotation, "txtLocalRotation");
            this.txtLocalRotation.Name = "txtLocalRotation";
            this.toolTip1.SetToolTip(this.txtLocalRotation, resources.GetString("txtLocalRotation.ToolTip"));
            this.txtLocalRotation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocalRotation_Validating);
            // 
            // lblLocalRotation
            // 
            resources.ApplyResources(this.lblLocalRotation, "lblLocalRotation");
            this.lblLocalRotation.Name = "lblLocalRotation";
            // 
            // txtLocalHeight
            // 
            resources.ApplyResources(this.txtLocalHeight, "txtLocalHeight");
            this.txtLocalHeight.Name = "txtLocalHeight";
            this.toolTip1.SetToolTip(this.txtLocalHeight, resources.GetString("txtLocalHeight.ToolTip"));
            this.txtLocalHeight.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocalHeight_Validating);
            // 
            // lblLocalHeight
            // 
            resources.ApplyResources(this.lblLocalHeight, "lblLocalHeight");
            this.lblLocalHeight.Name = "lblLocalHeight";
            // 
            // txtLocalLongitude
            // 
            resources.ApplyResources(this.txtLocalLongitude, "txtLocalLongitude");
            this.txtLocalLongitude.Name = "txtLocalLongitude";
            this.toolTip1.SetToolTip(this.txtLocalLongitude, resources.GetString("txtLocalLongitude.ToolTip"));
            this.txtLocalLongitude.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocalLongitude_Validating);
            // 
            // lblLocalLongitude
            // 
            resources.ApplyResources(this.lblLocalLongitude, "lblLocalLongitude");
            this.lblLocalLongitude.Name = "lblLocalLongitude";
            // 
            // txtLocalLatitude
            // 
            resources.ApplyResources(this.txtLocalLatitude, "txtLocalLatitude");
            this.txtLocalLatitude.Name = "txtLocalLatitude";
            this.toolTip1.SetToolTip(this.txtLocalLatitude, resources.GetString("txtLocalLatitude.ToolTip"));
            this.txtLocalLatitude.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocalLatitude_Validating);
            // 
            // lblLocalLatitude
            // 
            resources.ApplyResources(this.lblLocalLatitude, "lblLocalLatitude");
            this.lblLocalLatitude.Name = "lblLocalLatitude";
            // 
            // lblLocalOriginCoordinate
            // 
            resources.ApplyResources(this.lblLocalOriginCoordinate, "lblLocalOriginCoordinate");
            this.lblLocalOriginCoordinate.Name = "lblLocalOriginCoordinate";
            // 
            // cbLocalOriginLocation
            // 
            resources.ApplyResources(this.cbLocalOriginLocation, "cbLocalOriginLocation");
            this.cbLocalOriginLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocalOriginLocation.FormattingEnabled = true;
            this.cbLocalOriginLocation.Name = "cbLocalOriginLocation";
            this.cbLocalOriginLocation.SelectedIndexChanged += new System.EventHandler(this.cbLocalOriginLocation_SelectedIndexChanged);
            // 
            // lblLocalOriginLocation
            // 
            resources.ApplyResources(this.lblLocalOriginLocation, "lblLocalOriginLocation");
            this.lblLocalOriginLocation.Name = "lblLocalOriginLocation";
            // 
            // tabPageProj
            // 
            this.tabPageProj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageProj.Controls.Add(this.btnProjDefinitionSave);
            this.tabPageProj.Controls.Add(this.btnProjCoordinateOffsetSave);
            this.tabPageProj.Controls.Add(this.txtProjDefinition);
            this.tabPageProj.Controls.Add(this.cbProjDefinition);
            this.tabPageProj.Controls.Add(this.txtProjCoordinateOffset);
            this.tabPageProj.Controls.Add(this.lblProjCoordinateOffset);
            this.tabPageProj.Controls.Add(this.lblProjDefinition);
            this.tabPageProj.Controls.Add(this.cbProjOriginLocation);
            this.tabPageProj.Controls.Add(this.lblProjOriginLocation);
            resources.ApplyResources(this.tabPageProj, "tabPageProj");
            this.tabPageProj.Name = "tabPageProj";
            this.tabPageProj.UseVisualStyleBackColor = true;
            // 
            // btnProjDefinitionSave
            // 
            resources.ApplyResources(this.btnProjDefinitionSave, "btnProjDefinitionSave");
            this.btnProjDefinitionSave.Name = "btnProjDefinitionSave";
            this.toolTip1.SetToolTip(this.btnProjDefinitionSave, resources.GetString("btnProjDefinitionSave.ToolTip"));
            this.btnProjDefinitionSave.UseVisualStyleBackColor = true;
            this.btnProjDefinitionSave.Click += new System.EventHandler(this.btnProjDefinitionSave_Click);
            // 
            // btnProjCoordinateOffsetSave
            // 
            resources.ApplyResources(this.btnProjCoordinateOffsetSave, "btnProjCoordinateOffsetSave");
            this.btnProjCoordinateOffsetSave.Name = "btnProjCoordinateOffsetSave";
            this.toolTip1.SetToolTip(this.btnProjCoordinateOffsetSave, resources.GetString("btnProjCoordinateOffsetSave.ToolTip"));
            this.btnProjCoordinateOffsetSave.UseVisualStyleBackColor = true;
            this.btnProjCoordinateOffsetSave.Click += new System.EventHandler(this.btnProjCoordinateOffsetSave_Click);
            // 
            // txtProjDefinition
            // 
            resources.ApplyResources(this.txtProjDefinition, "txtProjDefinition");
            this.txtProjDefinition.Name = "txtProjDefinition";
            this.txtProjDefinition.DoubleClick += new System.EventHandler(this.txtProjDefinition_DoubleClick);
            this.txtProjDefinition.Enter += new System.EventHandler(this.txtProjDefinition_Enter);
            this.txtProjDefinition.Validating += new System.ComponentModel.CancelEventHandler(this.txtProjDefinition_Validating);
            // 
            // cbProjDefinition
            // 
            resources.ApplyResources(this.cbProjDefinition, "cbProjDefinition");
            this.cbProjDefinition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProjDefinition.FormattingEnabled = true;
            this.cbProjDefinition.Name = "cbProjDefinition";
            this.cbProjDefinition.SelectedIndexChanged += new System.EventHandler(this.cbProjDefinition_SelectedIndexChanged);
            // 
            // txtProjCoordinateOffset
            // 
            resources.ApplyResources(this.txtProjCoordinateOffset, "txtProjCoordinateOffset");
            this.txtProjCoordinateOffset.Name = "txtProjCoordinateOffset";
            this.toolTip1.SetToolTip(this.txtProjCoordinateOffset, resources.GetString("txtProjCoordinateOffset.ToolTip"));
            this.txtProjCoordinateOffset.Validating += new System.ComponentModel.CancelEventHandler(this.txtProjCoordinateOffset_Validating);
            // 
            // lblProjCoordinateOffset
            // 
            resources.ApplyResources(this.lblProjCoordinateOffset, "lblProjCoordinateOffset");
            this.lblProjCoordinateOffset.Name = "lblProjCoordinateOffset";
            // 
            // lblProjDefinition
            // 
            resources.ApplyResources(this.lblProjDefinition, "lblProjDefinition");
            this.lblProjDefinition.Name = "lblProjDefinition";
            // 
            // cbProjOriginLocation
            // 
            resources.ApplyResources(this.cbProjOriginLocation, "cbProjOriginLocation");
            this.cbProjOriginLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProjOriginLocation.FormattingEnabled = true;
            this.cbProjOriginLocation.Name = "cbProjOriginLocation";
            // 
            // lblProjOriginLocation
            // 
            resources.ApplyResources(this.lblProjOriginLocation, "lblProjOriginLocation");
            this.lblProjOriginLocation.Name = "lblProjOriginLocation";
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
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
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.prj";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // FormGeoreferncing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tabMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGeoreferncing";
            this.Load += new System.EventHandler(this.FormGeoreferncing_Load);
            this.tabMain.ResumeLayout(false);
            this.tabPageAuto.ResumeLayout(false);
            this.tabPageAuto.PerformLayout();
            this.tabPageEnu.ResumeLayout(false);
            this.tabPageEnu.PerformLayout();
            this.tabPageLocal.ResumeLayout(false);
            this.tabPageLocal.PerformLayout();
            this.tabPageProj.ResumeLayout(false);
            this.tabPageProj.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageAuto;
        private System.Windows.Forms.TabPage tabPageEnu;
        private System.Windows.Forms.TabPage tabPageLocal;
        private System.Windows.Forms.TabPage tabPageProj;
        private System.Windows.Forms.Label lblEnuOriginCoordinate;
        private System.Windows.Forms.ComboBox cbEnuOriginLocation;
        private System.Windows.Forms.Label lblEnuOriginLocation;
        private System.Windows.Forms.Label lblEnuLatitude;
        private System.Windows.Forms.TextBox txtEnuRotation;
        private System.Windows.Forms.Label lblEnuRotation;
        private System.Windows.Forms.TextBox txtEnuHeight;
        private System.Windows.Forms.Label lblEnuHeight;
        private System.Windows.Forms.TextBox txtEnuLongitude;
        private System.Windows.Forms.Label lblEnuLongitude;
        private System.Windows.Forms.TextBox txtEnuLatitude;
        private System.Windows.Forms.ComboBox cbAutoOriginLocation;
        private System.Windows.Forms.Label lblAutoOriginLocation;
        private System.Windows.Forms.TextBox txtLocalRotation;
        private System.Windows.Forms.Label lblLocalRotation;
        private System.Windows.Forms.TextBox txtLocalHeight;
        private System.Windows.Forms.Label lblLocalHeight;
        private System.Windows.Forms.TextBox txtLocalLongitude;
        private System.Windows.Forms.Label lblLocalLongitude;
        private System.Windows.Forms.TextBox txtLocalLatitude;
        private System.Windows.Forms.Label lblLocalLatitude;
        private System.Windows.Forms.Label lblLocalOriginCoordinate;
        private System.Windows.Forms.ComboBox cbLocalOriginLocation;
        private System.Windows.Forms.Label lblLocalOriginLocation;
        private System.Windows.Forms.ComboBox cbProjOriginLocation;
        private System.Windows.Forms.Label lblProjOriginLocation;
        private System.Windows.Forms.Label lblProjDefinition;
        private System.Windows.Forms.TextBox txtProjCoordinateOffset;
        private System.Windows.Forms.Label lblProjCoordinateOffset;
        private System.Windows.Forms.TextBox txtAutoSummary;
        private System.Windows.Forms.TextBox txtProjDefinition;
        private System.Windows.Forms.ComboBox cbProjDefinition;
        private System.Windows.Forms.Button btnProjDefinitionSave;
        private System.Windows.Forms.Button btnProjCoordinateOffsetSave;
        private System.Windows.Forms.CheckBox cbEnuUseProjectLocation;
        private System.Windows.Forms.CheckBox cbLocalUseProjectLocation;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox cbEnuAlignOriginToSitePlaneCenter;
        private System.Windows.Forms.CheckBox cbLocalAlignOriginToSitePlaneCenter;
        private System.Windows.Forms.CheckBox cbEnuAutoAlignToGround;
        private System.Windows.Forms.Label lblEnuAdvanced;
    }
}