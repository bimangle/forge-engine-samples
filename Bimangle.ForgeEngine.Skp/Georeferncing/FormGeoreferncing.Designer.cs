namespace Bimangle.ForgeEngine.Skp.Georeferncing
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
            this.errorProvider1.SetError(this.tabMain, resources.GetString("tabMain.Error"));
            this.errorProvider1.SetIconAlignment(this.tabMain, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabMain.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabMain, ((int)(resources.GetObject("tabMain.IconPadding"))));
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabMain, resources.GetString("tabMain.ToolTip"));
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPageAuto
            // 
            resources.ApplyResources(this.tabPageAuto, "tabPageAuto");
            this.tabPageAuto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageAuto.Controls.Add(this.txtAutoSummary);
            this.tabPageAuto.Controls.Add(this.cbAutoOriginLocation);
            this.tabPageAuto.Controls.Add(this.lblAutoOriginLocation);
            this.errorProvider1.SetError(this.tabPageAuto, resources.GetString("tabPageAuto.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageAuto, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageAuto.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageAuto, ((int)(resources.GetObject("tabPageAuto.IconPadding"))));
            this.tabPageAuto.Name = "tabPageAuto";
            this.toolTip1.SetToolTip(this.tabPageAuto, resources.GetString("tabPageAuto.ToolTip"));
            this.tabPageAuto.UseVisualStyleBackColor = true;
            // 
            // txtAutoSummary
            // 
            resources.ApplyResources(this.txtAutoSummary, "txtAutoSummary");
            this.errorProvider1.SetError(this.txtAutoSummary, resources.GetString("txtAutoSummary.Error"));
            this.errorProvider1.SetIconAlignment(this.txtAutoSummary, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtAutoSummary.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtAutoSummary, ((int)(resources.GetObject("txtAutoSummary.IconPadding"))));
            this.txtAutoSummary.Name = "txtAutoSummary";
            this.txtAutoSummary.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtAutoSummary, resources.GetString("txtAutoSummary.ToolTip"));
            // 
            // cbAutoOriginLocation
            // 
            resources.ApplyResources(this.cbAutoOriginLocation, "cbAutoOriginLocation");
            this.cbAutoOriginLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoOriginLocation.DropDownWidth = 400;
            this.errorProvider1.SetError(this.cbAutoOriginLocation, resources.GetString("cbAutoOriginLocation.Error"));
            this.cbAutoOriginLocation.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbAutoOriginLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbAutoOriginLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbAutoOriginLocation, ((int)(resources.GetObject("cbAutoOriginLocation.IconPadding"))));
            this.cbAutoOriginLocation.Name = "cbAutoOriginLocation";
            this.toolTip1.SetToolTip(this.cbAutoOriginLocation, resources.GetString("cbAutoOriginLocation.ToolTip"));
            this.cbAutoOriginLocation.SelectedIndexChanged += new System.EventHandler(this.cbAutoOriginLocation_SelectedIndexChanged);
            // 
            // lblAutoOriginLocation
            // 
            resources.ApplyResources(this.lblAutoOriginLocation, "lblAutoOriginLocation");
            this.errorProvider1.SetError(this.lblAutoOriginLocation, resources.GetString("lblAutoOriginLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.lblAutoOriginLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblAutoOriginLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblAutoOriginLocation, ((int)(resources.GetObject("lblAutoOriginLocation.IconPadding"))));
            this.lblAutoOriginLocation.Name = "lblAutoOriginLocation";
            this.toolTip1.SetToolTip(this.lblAutoOriginLocation, resources.GetString("lblAutoOriginLocation.ToolTip"));
            // 
            // tabPageEnu
            // 
            resources.ApplyResources(this.tabPageEnu, "tabPageEnu");
            this.tabPageEnu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.errorProvider1.SetError(this.tabPageEnu, resources.GetString("tabPageEnu.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageEnu, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageEnu.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageEnu, ((int)(resources.GetObject("tabPageEnu.IconPadding"))));
            this.tabPageEnu.Name = "tabPageEnu";
            this.toolTip1.SetToolTip(this.tabPageEnu, resources.GetString("tabPageEnu.ToolTip"));
            this.tabPageEnu.UseVisualStyleBackColor = true;
            // 
            // cbEnuAlignOriginToSitePlaneCenter
            // 
            resources.ApplyResources(this.cbEnuAlignOriginToSitePlaneCenter, "cbEnuAlignOriginToSitePlaneCenter");
            this.errorProvider1.SetError(this.cbEnuAlignOriginToSitePlaneCenter, resources.GetString("cbEnuAlignOriginToSitePlaneCenter.Error"));
            this.errorProvider1.SetIconAlignment(this.cbEnuAlignOriginToSitePlaneCenter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnuAlignOriginToSitePlaneCenter.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnuAlignOriginToSitePlaneCenter, ((int)(resources.GetObject("cbEnuAlignOriginToSitePlaneCenter.IconPadding"))));
            this.cbEnuAlignOriginToSitePlaneCenter.Name = "cbEnuAlignOriginToSitePlaneCenter";
            this.toolTip1.SetToolTip(this.cbEnuAlignOriginToSitePlaneCenter, resources.GetString("cbEnuAlignOriginToSitePlaneCenter.ToolTip"));
            this.cbEnuAlignOriginToSitePlaneCenter.UseVisualStyleBackColor = true;
            // 
            // cbEnuUseProjectLocation
            // 
            resources.ApplyResources(this.cbEnuUseProjectLocation, "cbEnuUseProjectLocation");
            this.errorProvider1.SetError(this.cbEnuUseProjectLocation, resources.GetString("cbEnuUseProjectLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.cbEnuUseProjectLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnuUseProjectLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnuUseProjectLocation, ((int)(resources.GetObject("cbEnuUseProjectLocation.IconPadding"))));
            this.cbEnuUseProjectLocation.Name = "cbEnuUseProjectLocation";
            this.toolTip1.SetToolTip(this.cbEnuUseProjectLocation, resources.GetString("cbEnuUseProjectLocation.ToolTip"));
            this.cbEnuUseProjectLocation.UseVisualStyleBackColor = true;
            this.cbEnuUseProjectLocation.CheckedChanged += new System.EventHandler(this.cbEnuUseProjectLocation_CheckedChanged);
            // 
            // txtEnuRotation
            // 
            resources.ApplyResources(this.txtEnuRotation, "txtEnuRotation");
            this.errorProvider1.SetError(this.txtEnuRotation, resources.GetString("txtEnuRotation.Error"));
            this.errorProvider1.SetIconAlignment(this.txtEnuRotation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtEnuRotation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtEnuRotation, ((int)(resources.GetObject("txtEnuRotation.IconPadding"))));
            this.txtEnuRotation.Name = "txtEnuRotation";
            this.toolTip1.SetToolTip(this.txtEnuRotation, resources.GetString("txtEnuRotation.ToolTip"));
            this.txtEnuRotation.Validating += new System.ComponentModel.CancelEventHandler(this.txtEnuRotation_Validating);
            // 
            // lblEnuRotation
            // 
            resources.ApplyResources(this.lblEnuRotation, "lblEnuRotation");
            this.errorProvider1.SetError(this.lblEnuRotation, resources.GetString("lblEnuRotation.Error"));
            this.errorProvider1.SetIconAlignment(this.lblEnuRotation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEnuRotation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblEnuRotation, ((int)(resources.GetObject("lblEnuRotation.IconPadding"))));
            this.lblEnuRotation.Name = "lblEnuRotation";
            this.toolTip1.SetToolTip(this.lblEnuRotation, resources.GetString("lblEnuRotation.ToolTip"));
            // 
            // txtEnuHeight
            // 
            resources.ApplyResources(this.txtEnuHeight, "txtEnuHeight");
            this.errorProvider1.SetError(this.txtEnuHeight, resources.GetString("txtEnuHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.txtEnuHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtEnuHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtEnuHeight, ((int)(resources.GetObject("txtEnuHeight.IconPadding"))));
            this.txtEnuHeight.Name = "txtEnuHeight";
            this.toolTip1.SetToolTip(this.txtEnuHeight, resources.GetString("txtEnuHeight.ToolTip"));
            this.txtEnuHeight.Validating += new System.ComponentModel.CancelEventHandler(this.txtEnuHeight_Validating);
            // 
            // lblEnuHeight
            // 
            resources.ApplyResources(this.lblEnuHeight, "lblEnuHeight");
            this.errorProvider1.SetError(this.lblEnuHeight, resources.GetString("lblEnuHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.lblEnuHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEnuHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblEnuHeight, ((int)(resources.GetObject("lblEnuHeight.IconPadding"))));
            this.lblEnuHeight.Name = "lblEnuHeight";
            this.toolTip1.SetToolTip(this.lblEnuHeight, resources.GetString("lblEnuHeight.ToolTip"));
            // 
            // txtEnuLongitude
            // 
            resources.ApplyResources(this.txtEnuLongitude, "txtEnuLongitude");
            this.errorProvider1.SetError(this.txtEnuLongitude, resources.GetString("txtEnuLongitude.Error"));
            this.errorProvider1.SetIconAlignment(this.txtEnuLongitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtEnuLongitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtEnuLongitude, ((int)(resources.GetObject("txtEnuLongitude.IconPadding"))));
            this.txtEnuLongitude.Name = "txtEnuLongitude";
            this.toolTip1.SetToolTip(this.txtEnuLongitude, resources.GetString("txtEnuLongitude.ToolTip"));
            this.txtEnuLongitude.Validating += new System.ComponentModel.CancelEventHandler(this.txtEnuLongitude_Validating);
            // 
            // lblEnuLongitude
            // 
            resources.ApplyResources(this.lblEnuLongitude, "lblEnuLongitude");
            this.errorProvider1.SetError(this.lblEnuLongitude, resources.GetString("lblEnuLongitude.Error"));
            this.errorProvider1.SetIconAlignment(this.lblEnuLongitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEnuLongitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblEnuLongitude, ((int)(resources.GetObject("lblEnuLongitude.IconPadding"))));
            this.lblEnuLongitude.Name = "lblEnuLongitude";
            this.toolTip1.SetToolTip(this.lblEnuLongitude, resources.GetString("lblEnuLongitude.ToolTip"));
            // 
            // txtEnuLatitude
            // 
            resources.ApplyResources(this.txtEnuLatitude, "txtEnuLatitude");
            this.errorProvider1.SetError(this.txtEnuLatitude, resources.GetString("txtEnuLatitude.Error"));
            this.errorProvider1.SetIconAlignment(this.txtEnuLatitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtEnuLatitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtEnuLatitude, ((int)(resources.GetObject("txtEnuLatitude.IconPadding"))));
            this.txtEnuLatitude.Name = "txtEnuLatitude";
            this.toolTip1.SetToolTip(this.txtEnuLatitude, resources.GetString("txtEnuLatitude.ToolTip"));
            this.txtEnuLatitude.Validating += new System.ComponentModel.CancelEventHandler(this.txtEnuLatitude_Validating);
            // 
            // lblEnuLatitude
            // 
            resources.ApplyResources(this.lblEnuLatitude, "lblEnuLatitude");
            this.errorProvider1.SetError(this.lblEnuLatitude, resources.GetString("lblEnuLatitude.Error"));
            this.errorProvider1.SetIconAlignment(this.lblEnuLatitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEnuLatitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblEnuLatitude, ((int)(resources.GetObject("lblEnuLatitude.IconPadding"))));
            this.lblEnuLatitude.Name = "lblEnuLatitude";
            this.toolTip1.SetToolTip(this.lblEnuLatitude, resources.GetString("lblEnuLatitude.ToolTip"));
            // 
            // lblEnuOriginCoordinate
            // 
            resources.ApplyResources(this.lblEnuOriginCoordinate, "lblEnuOriginCoordinate");
            this.errorProvider1.SetError(this.lblEnuOriginCoordinate, resources.GetString("lblEnuOriginCoordinate.Error"));
            this.errorProvider1.SetIconAlignment(this.lblEnuOriginCoordinate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEnuOriginCoordinate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblEnuOriginCoordinate, ((int)(resources.GetObject("lblEnuOriginCoordinate.IconPadding"))));
            this.lblEnuOriginCoordinate.Name = "lblEnuOriginCoordinate";
            this.toolTip1.SetToolTip(this.lblEnuOriginCoordinate, resources.GetString("lblEnuOriginCoordinate.ToolTip"));
            // 
            // cbEnuOriginLocation
            // 
            resources.ApplyResources(this.cbEnuOriginLocation, "cbEnuOriginLocation");
            this.cbEnuOriginLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbEnuOriginLocation, resources.GetString("cbEnuOriginLocation.Error"));
            this.cbEnuOriginLocation.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbEnuOriginLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnuOriginLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnuOriginLocation, ((int)(resources.GetObject("cbEnuOriginLocation.IconPadding"))));
            this.cbEnuOriginLocation.Name = "cbEnuOriginLocation";
            this.toolTip1.SetToolTip(this.cbEnuOriginLocation, resources.GetString("cbEnuOriginLocation.ToolTip"));
            this.cbEnuOriginLocation.SelectedIndexChanged += new System.EventHandler(this.cbEnuOriginLocation_SelectedIndexChanged);
            // 
            // lblEnuOriginLocation
            // 
            resources.ApplyResources(this.lblEnuOriginLocation, "lblEnuOriginLocation");
            this.errorProvider1.SetError(this.lblEnuOriginLocation, resources.GetString("lblEnuOriginLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.lblEnuOriginLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEnuOriginLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblEnuOriginLocation, ((int)(resources.GetObject("lblEnuOriginLocation.IconPadding"))));
            this.lblEnuOriginLocation.Name = "lblEnuOriginLocation";
            this.toolTip1.SetToolTip(this.lblEnuOriginLocation, resources.GetString("lblEnuOriginLocation.ToolTip"));
            // 
            // tabPageLocal
            // 
            resources.ApplyResources(this.tabPageLocal, "tabPageLocal");
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
            this.errorProvider1.SetError(this.tabPageLocal, resources.GetString("tabPageLocal.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageLocal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageLocal.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageLocal, ((int)(resources.GetObject("tabPageLocal.IconPadding"))));
            this.tabPageLocal.Name = "tabPageLocal";
            this.toolTip1.SetToolTip(this.tabPageLocal, resources.GetString("tabPageLocal.ToolTip"));
            this.tabPageLocal.UseVisualStyleBackColor = true;
            // 
            // cbLocalAlignOriginToSitePlaneCenter
            // 
            resources.ApplyResources(this.cbLocalAlignOriginToSitePlaneCenter, "cbLocalAlignOriginToSitePlaneCenter");
            this.errorProvider1.SetError(this.cbLocalAlignOriginToSitePlaneCenter, resources.GetString("cbLocalAlignOriginToSitePlaneCenter.Error"));
            this.errorProvider1.SetIconAlignment(this.cbLocalAlignOriginToSitePlaneCenter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbLocalAlignOriginToSitePlaneCenter.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbLocalAlignOriginToSitePlaneCenter, ((int)(resources.GetObject("cbLocalAlignOriginToSitePlaneCenter.IconPadding"))));
            this.cbLocalAlignOriginToSitePlaneCenter.Name = "cbLocalAlignOriginToSitePlaneCenter";
            this.toolTip1.SetToolTip(this.cbLocalAlignOriginToSitePlaneCenter, resources.GetString("cbLocalAlignOriginToSitePlaneCenter.ToolTip"));
            this.cbLocalAlignOriginToSitePlaneCenter.UseVisualStyleBackColor = true;
            // 
            // cbLocalUseProjectLocation
            // 
            resources.ApplyResources(this.cbLocalUseProjectLocation, "cbLocalUseProjectLocation");
            this.errorProvider1.SetError(this.cbLocalUseProjectLocation, resources.GetString("cbLocalUseProjectLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.cbLocalUseProjectLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbLocalUseProjectLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbLocalUseProjectLocation, ((int)(resources.GetObject("cbLocalUseProjectLocation.IconPadding"))));
            this.cbLocalUseProjectLocation.Name = "cbLocalUseProjectLocation";
            this.toolTip1.SetToolTip(this.cbLocalUseProjectLocation, resources.GetString("cbLocalUseProjectLocation.ToolTip"));
            this.cbLocalUseProjectLocation.UseVisualStyleBackColor = true;
            this.cbLocalUseProjectLocation.CheckedChanged += new System.EventHandler(this.cbLocalUseProjectLocation_CheckedChanged);
            // 
            // txtLocalRotation
            // 
            resources.ApplyResources(this.txtLocalRotation, "txtLocalRotation");
            this.errorProvider1.SetError(this.txtLocalRotation, resources.GetString("txtLocalRotation.Error"));
            this.errorProvider1.SetIconAlignment(this.txtLocalRotation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLocalRotation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtLocalRotation, ((int)(resources.GetObject("txtLocalRotation.IconPadding"))));
            this.txtLocalRotation.Name = "txtLocalRotation";
            this.toolTip1.SetToolTip(this.txtLocalRotation, resources.GetString("txtLocalRotation.ToolTip"));
            this.txtLocalRotation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocalRotation_Validating);
            // 
            // lblLocalRotation
            // 
            resources.ApplyResources(this.lblLocalRotation, "lblLocalRotation");
            this.errorProvider1.SetError(this.lblLocalRotation, resources.GetString("lblLocalRotation.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLocalRotation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLocalRotation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLocalRotation, ((int)(resources.GetObject("lblLocalRotation.IconPadding"))));
            this.lblLocalRotation.Name = "lblLocalRotation";
            this.toolTip1.SetToolTip(this.lblLocalRotation, resources.GetString("lblLocalRotation.ToolTip"));
            // 
            // txtLocalHeight
            // 
            resources.ApplyResources(this.txtLocalHeight, "txtLocalHeight");
            this.errorProvider1.SetError(this.txtLocalHeight, resources.GetString("txtLocalHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.txtLocalHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLocalHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtLocalHeight, ((int)(resources.GetObject("txtLocalHeight.IconPadding"))));
            this.txtLocalHeight.Name = "txtLocalHeight";
            this.toolTip1.SetToolTip(this.txtLocalHeight, resources.GetString("txtLocalHeight.ToolTip"));
            this.txtLocalHeight.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocalHeight_Validating);
            // 
            // lblLocalHeight
            // 
            resources.ApplyResources(this.lblLocalHeight, "lblLocalHeight");
            this.errorProvider1.SetError(this.lblLocalHeight, resources.GetString("lblLocalHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLocalHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLocalHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLocalHeight, ((int)(resources.GetObject("lblLocalHeight.IconPadding"))));
            this.lblLocalHeight.Name = "lblLocalHeight";
            this.toolTip1.SetToolTip(this.lblLocalHeight, resources.GetString("lblLocalHeight.ToolTip"));
            // 
            // txtLocalLongitude
            // 
            resources.ApplyResources(this.txtLocalLongitude, "txtLocalLongitude");
            this.errorProvider1.SetError(this.txtLocalLongitude, resources.GetString("txtLocalLongitude.Error"));
            this.errorProvider1.SetIconAlignment(this.txtLocalLongitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLocalLongitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtLocalLongitude, ((int)(resources.GetObject("txtLocalLongitude.IconPadding"))));
            this.txtLocalLongitude.Name = "txtLocalLongitude";
            this.toolTip1.SetToolTip(this.txtLocalLongitude, resources.GetString("txtLocalLongitude.ToolTip"));
            this.txtLocalLongitude.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocalLongitude_Validating);
            // 
            // lblLocalLongitude
            // 
            resources.ApplyResources(this.lblLocalLongitude, "lblLocalLongitude");
            this.errorProvider1.SetError(this.lblLocalLongitude, resources.GetString("lblLocalLongitude.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLocalLongitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLocalLongitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLocalLongitude, ((int)(resources.GetObject("lblLocalLongitude.IconPadding"))));
            this.lblLocalLongitude.Name = "lblLocalLongitude";
            this.toolTip1.SetToolTip(this.lblLocalLongitude, resources.GetString("lblLocalLongitude.ToolTip"));
            // 
            // txtLocalLatitude
            // 
            resources.ApplyResources(this.txtLocalLatitude, "txtLocalLatitude");
            this.errorProvider1.SetError(this.txtLocalLatitude, resources.GetString("txtLocalLatitude.Error"));
            this.errorProvider1.SetIconAlignment(this.txtLocalLatitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLocalLatitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtLocalLatitude, ((int)(resources.GetObject("txtLocalLatitude.IconPadding"))));
            this.txtLocalLatitude.Name = "txtLocalLatitude";
            this.toolTip1.SetToolTip(this.txtLocalLatitude, resources.GetString("txtLocalLatitude.ToolTip"));
            this.txtLocalLatitude.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocalLatitude_Validating);
            // 
            // lblLocalLatitude
            // 
            resources.ApplyResources(this.lblLocalLatitude, "lblLocalLatitude");
            this.errorProvider1.SetError(this.lblLocalLatitude, resources.GetString("lblLocalLatitude.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLocalLatitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLocalLatitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLocalLatitude, ((int)(resources.GetObject("lblLocalLatitude.IconPadding"))));
            this.lblLocalLatitude.Name = "lblLocalLatitude";
            this.toolTip1.SetToolTip(this.lblLocalLatitude, resources.GetString("lblLocalLatitude.ToolTip"));
            // 
            // lblLocalOriginCoordinate
            // 
            resources.ApplyResources(this.lblLocalOriginCoordinate, "lblLocalOriginCoordinate");
            this.errorProvider1.SetError(this.lblLocalOriginCoordinate, resources.GetString("lblLocalOriginCoordinate.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLocalOriginCoordinate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLocalOriginCoordinate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLocalOriginCoordinate, ((int)(resources.GetObject("lblLocalOriginCoordinate.IconPadding"))));
            this.lblLocalOriginCoordinate.Name = "lblLocalOriginCoordinate";
            this.toolTip1.SetToolTip(this.lblLocalOriginCoordinate, resources.GetString("lblLocalOriginCoordinate.ToolTip"));
            // 
            // cbLocalOriginLocation
            // 
            resources.ApplyResources(this.cbLocalOriginLocation, "cbLocalOriginLocation");
            this.cbLocalOriginLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbLocalOriginLocation, resources.GetString("cbLocalOriginLocation.Error"));
            this.cbLocalOriginLocation.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbLocalOriginLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbLocalOriginLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbLocalOriginLocation, ((int)(resources.GetObject("cbLocalOriginLocation.IconPadding"))));
            this.cbLocalOriginLocation.Name = "cbLocalOriginLocation";
            this.toolTip1.SetToolTip(this.cbLocalOriginLocation, resources.GetString("cbLocalOriginLocation.ToolTip"));
            this.cbLocalOriginLocation.SelectedIndexChanged += new System.EventHandler(this.cbLocalOriginLocation_SelectedIndexChanged);
            // 
            // lblLocalOriginLocation
            // 
            resources.ApplyResources(this.lblLocalOriginLocation, "lblLocalOriginLocation");
            this.errorProvider1.SetError(this.lblLocalOriginLocation, resources.GetString("lblLocalOriginLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLocalOriginLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLocalOriginLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLocalOriginLocation, ((int)(resources.GetObject("lblLocalOriginLocation.IconPadding"))));
            this.lblLocalOriginLocation.Name = "lblLocalOriginLocation";
            this.toolTip1.SetToolTip(this.lblLocalOriginLocation, resources.GetString("lblLocalOriginLocation.ToolTip"));
            // 
            // tabPageProj
            // 
            resources.ApplyResources(this.tabPageProj, "tabPageProj");
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
            this.errorProvider1.SetError(this.tabPageProj, resources.GetString("tabPageProj.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageProj, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageProj.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageProj, ((int)(resources.GetObject("tabPageProj.IconPadding"))));
            this.tabPageProj.Name = "tabPageProj";
            this.toolTip1.SetToolTip(this.tabPageProj, resources.GetString("tabPageProj.ToolTip"));
            this.tabPageProj.UseVisualStyleBackColor = true;
            // 
            // btnProjDefinitionSave
            // 
            resources.ApplyResources(this.btnProjDefinitionSave, "btnProjDefinitionSave");
            this.errorProvider1.SetError(this.btnProjDefinitionSave, resources.GetString("btnProjDefinitionSave.Error"));
            this.errorProvider1.SetIconAlignment(this.btnProjDefinitionSave, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnProjDefinitionSave.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnProjDefinitionSave, ((int)(resources.GetObject("btnProjDefinitionSave.IconPadding"))));
            this.btnProjDefinitionSave.Name = "btnProjDefinitionSave";
            this.toolTip1.SetToolTip(this.btnProjDefinitionSave, resources.GetString("btnProjDefinitionSave.ToolTip"));
            this.btnProjDefinitionSave.UseVisualStyleBackColor = true;
            this.btnProjDefinitionSave.Click += new System.EventHandler(this.btnProjDefinitionSave_Click);
            // 
            // btnProjCoordinateOffsetSave
            // 
            resources.ApplyResources(this.btnProjCoordinateOffsetSave, "btnProjCoordinateOffsetSave");
            this.errorProvider1.SetError(this.btnProjCoordinateOffsetSave, resources.GetString("btnProjCoordinateOffsetSave.Error"));
            this.errorProvider1.SetIconAlignment(this.btnProjCoordinateOffsetSave, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnProjCoordinateOffsetSave.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnProjCoordinateOffsetSave, ((int)(resources.GetObject("btnProjCoordinateOffsetSave.IconPadding"))));
            this.btnProjCoordinateOffsetSave.Name = "btnProjCoordinateOffsetSave";
            this.toolTip1.SetToolTip(this.btnProjCoordinateOffsetSave, resources.GetString("btnProjCoordinateOffsetSave.ToolTip"));
            this.btnProjCoordinateOffsetSave.UseVisualStyleBackColor = true;
            this.btnProjCoordinateOffsetSave.Click += new System.EventHandler(this.btnProjCoordinateOffsetSave_Click);
            // 
            // txtProjDefinition
            // 
            resources.ApplyResources(this.txtProjDefinition, "txtProjDefinition");
            this.errorProvider1.SetError(this.txtProjDefinition, resources.GetString("txtProjDefinition.Error"));
            this.errorProvider1.SetIconAlignment(this.txtProjDefinition, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtProjDefinition.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtProjDefinition, ((int)(resources.GetObject("txtProjDefinition.IconPadding"))));
            this.txtProjDefinition.Name = "txtProjDefinition";
            this.toolTip1.SetToolTip(this.txtProjDefinition, resources.GetString("txtProjDefinition.ToolTip"));
            this.txtProjDefinition.DoubleClick += new System.EventHandler(this.txtProjDefinition_DoubleClick);
            this.txtProjDefinition.Enter += new System.EventHandler(this.txtProjDefinition_Enter);
            this.txtProjDefinition.Validating += new System.ComponentModel.CancelEventHandler(this.txtProjDefinition_Validating);
            // 
            // cbProjDefinition
            // 
            resources.ApplyResources(this.cbProjDefinition, "cbProjDefinition");
            this.cbProjDefinition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbProjDefinition, resources.GetString("cbProjDefinition.Error"));
            this.cbProjDefinition.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbProjDefinition, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbProjDefinition.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbProjDefinition, ((int)(resources.GetObject("cbProjDefinition.IconPadding"))));
            this.cbProjDefinition.Name = "cbProjDefinition";
            this.toolTip1.SetToolTip(this.cbProjDefinition, resources.GetString("cbProjDefinition.ToolTip"));
            this.cbProjDefinition.SelectedIndexChanged += new System.EventHandler(this.cbProjDefinition_SelectedIndexChanged);
            // 
            // txtProjCoordinateOffset
            // 
            resources.ApplyResources(this.txtProjCoordinateOffset, "txtProjCoordinateOffset");
            this.errorProvider1.SetError(this.txtProjCoordinateOffset, resources.GetString("txtProjCoordinateOffset.Error"));
            this.errorProvider1.SetIconAlignment(this.txtProjCoordinateOffset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtProjCoordinateOffset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtProjCoordinateOffset, ((int)(resources.GetObject("txtProjCoordinateOffset.IconPadding"))));
            this.txtProjCoordinateOffset.Name = "txtProjCoordinateOffset";
            this.toolTip1.SetToolTip(this.txtProjCoordinateOffset, resources.GetString("txtProjCoordinateOffset.ToolTip"));
            this.txtProjCoordinateOffset.Validating += new System.ComponentModel.CancelEventHandler(this.txtProjCoordinateOffset_Validating);
            // 
            // lblProjCoordinateOffset
            // 
            resources.ApplyResources(this.lblProjCoordinateOffset, "lblProjCoordinateOffset");
            this.errorProvider1.SetError(this.lblProjCoordinateOffset, resources.GetString("lblProjCoordinateOffset.Error"));
            this.errorProvider1.SetIconAlignment(this.lblProjCoordinateOffset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjCoordinateOffset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblProjCoordinateOffset, ((int)(resources.GetObject("lblProjCoordinateOffset.IconPadding"))));
            this.lblProjCoordinateOffset.Name = "lblProjCoordinateOffset";
            this.toolTip1.SetToolTip(this.lblProjCoordinateOffset, resources.GetString("lblProjCoordinateOffset.ToolTip"));
            // 
            // lblProjDefinition
            // 
            resources.ApplyResources(this.lblProjDefinition, "lblProjDefinition");
            this.errorProvider1.SetError(this.lblProjDefinition, resources.GetString("lblProjDefinition.Error"));
            this.errorProvider1.SetIconAlignment(this.lblProjDefinition, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjDefinition.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblProjDefinition, ((int)(resources.GetObject("lblProjDefinition.IconPadding"))));
            this.lblProjDefinition.Name = "lblProjDefinition";
            this.toolTip1.SetToolTip(this.lblProjDefinition, resources.GetString("lblProjDefinition.ToolTip"));
            // 
            // cbProjOriginLocation
            // 
            resources.ApplyResources(this.cbProjOriginLocation, "cbProjOriginLocation");
            this.cbProjOriginLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbProjOriginLocation, resources.GetString("cbProjOriginLocation.Error"));
            this.cbProjOriginLocation.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbProjOriginLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbProjOriginLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbProjOriginLocation, ((int)(resources.GetObject("cbProjOriginLocation.IconPadding"))));
            this.cbProjOriginLocation.Name = "cbProjOriginLocation";
            this.toolTip1.SetToolTip(this.cbProjOriginLocation, resources.GetString("cbProjOriginLocation.ToolTip"));
            // 
            // lblProjOriginLocation
            // 
            resources.ApplyResources(this.lblProjOriginLocation, "lblProjOriginLocation");
            this.errorProvider1.SetError(this.lblProjOriginLocation, resources.GetString("lblProjOriginLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.lblProjOriginLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjOriginLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblProjOriginLocation, ((int)(resources.GetObject("lblProjOriginLocation.IconPadding"))));
            this.lblProjOriginLocation.Name = "lblProjOriginLocation";
            this.toolTip1.SetToolTip(this.lblProjOriginLocation, resources.GetString("lblProjOriginLocation.ToolTip"));
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.errorProvider1.SetError(this.btnReset, resources.GetString("btnReset.Error"));
            this.errorProvider1.SetIconAlignment(this.btnReset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnReset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnReset, ((int)(resources.GetObject("btnReset.IconPadding"))));
            this.btnReset.Name = "btnReset";
            this.toolTip1.SetToolTip(this.btnReset, resources.GetString("btnReset.ToolTip"));
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
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
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
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
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
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
    }
}