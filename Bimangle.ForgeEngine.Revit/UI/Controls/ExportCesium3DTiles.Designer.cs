namespace Bimangle.ForgeEngine.Revit.UI.Controls
{
    partial class ExportCesium3DTiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportCesium3DTiles));
            this.gpContainer = new System.Windows.Forms.GroupBox();
            this.gpSiteInfo = new System.Windows.Forms.GroupBox();
            this.rbGeoreferencingCustom = new System.Windows.Forms.RadioButton();
            this.rbGeoreferencingSiteLocation = new System.Windows.Forms.RadioButton();
            this.rbGeoreferencingNone = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRotation = new System.Windows.Forms.TextBox();
            this.lblRotation = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.gb3DTiles = new System.Windows.Forms.GroupBox();
            this.rbModeShellElement = new System.Windows.Forms.RadioButton();
            this.rbModeShellMesh = new System.Windows.Forms.RadioButton();
            this.rbModeBasic = new System.Windows.Forms.RadioButton();
            this.gbAdvanced = new System.Windows.Forms.GroupBox();
            this.cbAlignOriginToSiteCenter = new System.Windows.Forms.CheckBox();
            this.cbEnableUnlitMaterials = new System.Windows.Forms.CheckBox();
            this.cbEnableTextureWebP = new System.Windows.Forms.CheckBox();
            this.cbEnableQuantizedAttributes = new System.Windows.Forms.CheckBox();
            this.cbExportSvfzip = new System.Windows.Forms.CheckBox();
            this.cbGeneratePropDbSqlite = new System.Windows.Forms.CheckBox();
            this.cbUseDraco = new System.Windows.Forms.CheckBox();
            this.gpExclude = new System.Windows.Forms.GroupBox();
            this.cbExcludeUnselectedElements = new System.Windows.Forms.CheckBox();
            this.cbExcludeModelPoints = new System.Windows.Forms.CheckBox();
            this.cbExcludeLines = new System.Windows.Forms.CheckBox();
            this.gpGeneral = new System.Windows.Forms.GroupBox();
            this.cbGenerateOutline = new System.Windows.Forms.CheckBox();
            this.lblGenerate = new System.Windows.Forms.Label();
            this.cbVisualStyle = new System.Windows.Forms.ComboBox();
            this.cbGenerateThumbnail = new System.Windows.Forms.CheckBox();
            this.lblVisualStyle = new System.Windows.Forms.Label();
            this.cbLevelOfDetail = new System.Windows.Forms.ComboBox();
            this.lblLevelOfDetail = new System.Windows.Forms.Label();
            this.lblOptions = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpContainer.SuspendLayout();
            this.gpSiteInfo.SuspendLayout();
            this.gb3DTiles.SuspendLayout();
            this.gbAdvanced.SuspendLayout();
            this.gpExclude.SuspendLayout();
            this.gpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpContainer
            // 
            resources.ApplyResources(this.gpContainer, "gpContainer");
            this.gpContainer.Controls.Add(this.gpSiteInfo);
            this.gpContainer.Controls.Add(this.gb3DTiles);
            this.gpContainer.Controls.Add(this.gbAdvanced);
            this.gpContainer.Controls.Add(this.gpExclude);
            this.gpContainer.Controls.Add(this.gpGeneral);
            this.gpContainer.Controls.Add(this.lblOptions);
            this.gpContainer.Controls.Add(this.btnBrowse);
            this.gpContainer.Controls.Add(this.txtTargetPath);
            this.gpContainer.Controls.Add(this.lblOutputPath);
            this.errorProvider1.SetError(this.gpContainer, resources.GetString("gpContainer.Error"));
            this.errorProvider1.SetIconAlignment(this.gpContainer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpContainer.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpContainer, ((int)(resources.GetObject("gpContainer.IconPadding"))));
            this.gpContainer.Name = "gpContainer";
            this.gpContainer.TabStop = false;
            this.toolTip1.SetToolTip(this.gpContainer, resources.GetString("gpContainer.ToolTip"));
            // 
            // gpSiteInfo
            // 
            resources.ApplyResources(this.gpSiteInfo, "gpSiteInfo");
            this.gpSiteInfo.Controls.Add(this.rbGeoreferencingCustom);
            this.gpSiteInfo.Controls.Add(this.rbGeoreferencingSiteLocation);
            this.gpSiteInfo.Controls.Add(this.rbGeoreferencingNone);
            this.gpSiteInfo.Controls.Add(this.label1);
            this.gpSiteInfo.Controls.Add(this.txtRotation);
            this.gpSiteInfo.Controls.Add(this.lblRotation);
            this.gpSiteInfo.Controls.Add(this.txtHeight);
            this.gpSiteInfo.Controls.Add(this.lblHeight);
            this.gpSiteInfo.Controls.Add(this.txtLatitude);
            this.gpSiteInfo.Controls.Add(this.lblLatitude);
            this.gpSiteInfo.Controls.Add(this.lblLongitude);
            this.gpSiteInfo.Controls.Add(this.txtLongitude);
            this.errorProvider1.SetError(this.gpSiteInfo, resources.GetString("gpSiteInfo.Error"));
            this.errorProvider1.SetIconAlignment(this.gpSiteInfo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpSiteInfo.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpSiteInfo, ((int)(resources.GetObject("gpSiteInfo.IconPadding"))));
            this.gpSiteInfo.Name = "gpSiteInfo";
            this.gpSiteInfo.TabStop = false;
            this.toolTip1.SetToolTip(this.gpSiteInfo, resources.GetString("gpSiteInfo.ToolTip"));
            // 
            // rbGeoreferencingCustom
            // 
            resources.ApplyResources(this.rbGeoreferencingCustom, "rbGeoreferencingCustom");
            this.errorProvider1.SetError(this.rbGeoreferencingCustom, resources.GetString("rbGeoreferencingCustom.Error"));
            this.errorProvider1.SetIconAlignment(this.rbGeoreferencingCustom, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbGeoreferencingCustom.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.rbGeoreferencingCustom, ((int)(resources.GetObject("rbGeoreferencingCustom.IconPadding"))));
            this.rbGeoreferencingCustom.Name = "rbGeoreferencingCustom";
            this.toolTip1.SetToolTip(this.rbGeoreferencingCustom, resources.GetString("rbGeoreferencingCustom.ToolTip"));
            this.rbGeoreferencingCustom.UseVisualStyleBackColor = true;
            // 
            // rbGeoreferencingSiteLocation
            // 
            resources.ApplyResources(this.rbGeoreferencingSiteLocation, "rbGeoreferencingSiteLocation");
            this.errorProvider1.SetError(this.rbGeoreferencingSiteLocation, resources.GetString("rbGeoreferencingSiteLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.rbGeoreferencingSiteLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbGeoreferencingSiteLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.rbGeoreferencingSiteLocation, ((int)(resources.GetObject("rbGeoreferencingSiteLocation.IconPadding"))));
            this.rbGeoreferencingSiteLocation.Name = "rbGeoreferencingSiteLocation";
            this.toolTip1.SetToolTip(this.rbGeoreferencingSiteLocation, resources.GetString("rbGeoreferencingSiteLocation.ToolTip"));
            this.rbGeoreferencingSiteLocation.UseVisualStyleBackColor = true;
            // 
            // rbGeoreferencingNone
            // 
            resources.ApplyResources(this.rbGeoreferencingNone, "rbGeoreferencingNone");
            this.errorProvider1.SetError(this.rbGeoreferencingNone, resources.GetString("rbGeoreferencingNone.Error"));
            this.errorProvider1.SetIconAlignment(this.rbGeoreferencingNone, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbGeoreferencingNone.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.rbGeoreferencingNone, ((int)(resources.GetObject("rbGeoreferencingNone.IconPadding"))));
            this.rbGeoreferencingNone.Name = "rbGeoreferencingNone";
            this.toolTip1.SetToolTip(this.rbGeoreferencingNone, resources.GetString("rbGeoreferencingNone.ToolTip"));
            this.rbGeoreferencingNone.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // txtRotation
            // 
            resources.ApplyResources(this.txtRotation, "txtRotation");
            this.errorProvider1.SetError(this.txtRotation, resources.GetString("txtRotation.Error"));
            this.errorProvider1.SetIconAlignment(this.txtRotation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtRotation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtRotation, ((int)(resources.GetObject("txtRotation.IconPadding"))));
            this.txtRotation.Name = "txtRotation";
            this.toolTip1.SetToolTip(this.txtRotation, resources.GetString("txtRotation.ToolTip"));
            // 
            // lblRotation
            // 
            resources.ApplyResources(this.lblRotation, "lblRotation");
            this.errorProvider1.SetError(this.lblRotation, resources.GetString("lblRotation.Error"));
            this.errorProvider1.SetIconAlignment(this.lblRotation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblRotation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblRotation, ((int)(resources.GetObject("lblRotation.IconPadding"))));
            this.lblRotation.Name = "lblRotation";
            this.toolTip1.SetToolTip(this.lblRotation, resources.GetString("lblRotation.ToolTip"));
            // 
            // txtHeight
            // 
            resources.ApplyResources(this.txtHeight, "txtHeight");
            this.errorProvider1.SetError(this.txtHeight, resources.GetString("txtHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.txtHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtHeight, ((int)(resources.GetObject("txtHeight.IconPadding"))));
            this.txtHeight.Name = "txtHeight";
            this.toolTip1.SetToolTip(this.txtHeight, resources.GetString("txtHeight.ToolTip"));
            // 
            // lblHeight
            // 
            resources.ApplyResources(this.lblHeight, "lblHeight");
            this.errorProvider1.SetError(this.lblHeight, resources.GetString("lblHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.lblHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblHeight, ((int)(resources.GetObject("lblHeight.IconPadding"))));
            this.lblHeight.Name = "lblHeight";
            this.toolTip1.SetToolTip(this.lblHeight, resources.GetString("lblHeight.ToolTip"));
            // 
            // txtLatitude
            // 
            resources.ApplyResources(this.txtLatitude, "txtLatitude");
            this.errorProvider1.SetError(this.txtLatitude, resources.GetString("txtLatitude.Error"));
            this.errorProvider1.SetIconAlignment(this.txtLatitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLatitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtLatitude, ((int)(resources.GetObject("txtLatitude.IconPadding"))));
            this.txtLatitude.Name = "txtLatitude";
            this.toolTip1.SetToolTip(this.txtLatitude, resources.GetString("txtLatitude.ToolTip"));
            // 
            // lblLatitude
            // 
            resources.ApplyResources(this.lblLatitude, "lblLatitude");
            this.errorProvider1.SetError(this.lblLatitude, resources.GetString("lblLatitude.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLatitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLatitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLatitude, ((int)(resources.GetObject("lblLatitude.IconPadding"))));
            this.lblLatitude.Name = "lblLatitude";
            this.toolTip1.SetToolTip(this.lblLatitude, resources.GetString("lblLatitude.ToolTip"));
            // 
            // lblLongitude
            // 
            resources.ApplyResources(this.lblLongitude, "lblLongitude");
            this.errorProvider1.SetError(this.lblLongitude, resources.GetString("lblLongitude.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLongitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLongitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLongitude, ((int)(resources.GetObject("lblLongitude.IconPadding"))));
            this.lblLongitude.Name = "lblLongitude";
            this.toolTip1.SetToolTip(this.lblLongitude, resources.GetString("lblLongitude.ToolTip"));
            // 
            // txtLongitude
            // 
            resources.ApplyResources(this.txtLongitude, "txtLongitude");
            this.errorProvider1.SetError(this.txtLongitude, resources.GetString("txtLongitude.Error"));
            this.errorProvider1.SetIconAlignment(this.txtLongitude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLongitude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtLongitude, ((int)(resources.GetObject("txtLongitude.IconPadding"))));
            this.txtLongitude.Name = "txtLongitude";
            this.toolTip1.SetToolTip(this.txtLongitude, resources.GetString("txtLongitude.ToolTip"));
            // 
            // gb3DTiles
            // 
            resources.ApplyResources(this.gb3DTiles, "gb3DTiles");
            this.gb3DTiles.Controls.Add(this.rbModeShellElement);
            this.gb3DTiles.Controls.Add(this.rbModeShellMesh);
            this.gb3DTiles.Controls.Add(this.rbModeBasic);
            this.errorProvider1.SetError(this.gb3DTiles, resources.GetString("gb3DTiles.Error"));
            this.errorProvider1.SetIconAlignment(this.gb3DTiles, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gb3DTiles.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gb3DTiles, ((int)(resources.GetObject("gb3DTiles.IconPadding"))));
            this.gb3DTiles.Name = "gb3DTiles";
            this.gb3DTiles.TabStop = false;
            this.toolTip1.SetToolTip(this.gb3DTiles, resources.GetString("gb3DTiles.ToolTip"));
            // 
            // rbModeShellElement
            // 
            resources.ApplyResources(this.rbModeShellElement, "rbModeShellElement");
            this.errorProvider1.SetError(this.rbModeShellElement, resources.GetString("rbModeShellElement.Error"));
            this.errorProvider1.SetIconAlignment(this.rbModeShellElement, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbModeShellElement.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.rbModeShellElement, ((int)(resources.GetObject("rbModeShellElement.IconPadding"))));
            this.rbModeShellElement.Name = "rbModeShellElement";
            this.toolTip1.SetToolTip(this.rbModeShellElement, resources.GetString("rbModeShellElement.ToolTip"));
            this.rbModeShellElement.UseVisualStyleBackColor = true;
            // 
            // rbModeShellMesh
            // 
            resources.ApplyResources(this.rbModeShellMesh, "rbModeShellMesh");
            this.errorProvider1.SetError(this.rbModeShellMesh, resources.GetString("rbModeShellMesh.Error"));
            this.errorProvider1.SetIconAlignment(this.rbModeShellMesh, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbModeShellMesh.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.rbModeShellMesh, ((int)(resources.GetObject("rbModeShellMesh.IconPadding"))));
            this.rbModeShellMesh.Name = "rbModeShellMesh";
            this.toolTip1.SetToolTip(this.rbModeShellMesh, resources.GetString("rbModeShellMesh.ToolTip"));
            this.rbModeShellMesh.UseVisualStyleBackColor = true;
            // 
            // rbModeBasic
            // 
            resources.ApplyResources(this.rbModeBasic, "rbModeBasic");
            this.rbModeBasic.Checked = true;
            this.errorProvider1.SetError(this.rbModeBasic, resources.GetString("rbModeBasic.Error"));
            this.errorProvider1.SetIconAlignment(this.rbModeBasic, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbModeBasic.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.rbModeBasic, ((int)(resources.GetObject("rbModeBasic.IconPadding"))));
            this.rbModeBasic.Name = "rbModeBasic";
            this.rbModeBasic.TabStop = true;
            this.toolTip1.SetToolTip(this.rbModeBasic, resources.GetString("rbModeBasic.ToolTip"));
            this.rbModeBasic.UseVisualStyleBackColor = true;
            // 
            // gbAdvanced
            // 
            resources.ApplyResources(this.gbAdvanced, "gbAdvanced");
            this.gbAdvanced.Controls.Add(this.cbAlignOriginToSiteCenter);
            this.gbAdvanced.Controls.Add(this.cbEnableUnlitMaterials);
            this.gbAdvanced.Controls.Add(this.cbEnableTextureWebP);
            this.gbAdvanced.Controls.Add(this.cbEnableQuantizedAttributes);
            this.gbAdvanced.Controls.Add(this.cbExportSvfzip);
            this.gbAdvanced.Controls.Add(this.cbGeneratePropDbSqlite);
            this.gbAdvanced.Controls.Add(this.cbUseDraco);
            this.errorProvider1.SetError(this.gbAdvanced, resources.GetString("gbAdvanced.Error"));
            this.errorProvider1.SetIconAlignment(this.gbAdvanced, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gbAdvanced.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gbAdvanced, ((int)(resources.GetObject("gbAdvanced.IconPadding"))));
            this.gbAdvanced.Name = "gbAdvanced";
            this.gbAdvanced.TabStop = false;
            this.toolTip1.SetToolTip(this.gbAdvanced, resources.GetString("gbAdvanced.ToolTip"));
            // 
            // cbAlignOriginToSiteCenter
            // 
            resources.ApplyResources(this.cbAlignOriginToSiteCenter, "cbAlignOriginToSiteCenter");
            this.errorProvider1.SetError(this.cbAlignOriginToSiteCenter, resources.GetString("cbAlignOriginToSiteCenter.Error"));
            this.errorProvider1.SetIconAlignment(this.cbAlignOriginToSiteCenter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbAlignOriginToSiteCenter.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbAlignOriginToSiteCenter, ((int)(resources.GetObject("cbAlignOriginToSiteCenter.IconPadding"))));
            this.cbAlignOriginToSiteCenter.Name = "cbAlignOriginToSiteCenter";
            this.toolTip1.SetToolTip(this.cbAlignOriginToSiteCenter, resources.GetString("cbAlignOriginToSiteCenter.ToolTip"));
            this.cbAlignOriginToSiteCenter.UseVisualStyleBackColor = true;
            // 
            // cbEnableUnlitMaterials
            // 
            resources.ApplyResources(this.cbEnableUnlitMaterials, "cbEnableUnlitMaterials");
            this.errorProvider1.SetError(this.cbEnableUnlitMaterials, resources.GetString("cbEnableUnlitMaterials.Error"));
            this.errorProvider1.SetIconAlignment(this.cbEnableUnlitMaterials, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnableUnlitMaterials.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnableUnlitMaterials, ((int)(resources.GetObject("cbEnableUnlitMaterials.IconPadding"))));
            this.cbEnableUnlitMaterials.Name = "cbEnableUnlitMaterials";
            this.toolTip1.SetToolTip(this.cbEnableUnlitMaterials, resources.GetString("cbEnableUnlitMaterials.ToolTip"));
            this.cbEnableUnlitMaterials.UseVisualStyleBackColor = true;
            // 
            // cbEnableTextureWebP
            // 
            resources.ApplyResources(this.cbEnableTextureWebP, "cbEnableTextureWebP");
            this.errorProvider1.SetError(this.cbEnableTextureWebP, resources.GetString("cbEnableTextureWebP.Error"));
            this.errorProvider1.SetIconAlignment(this.cbEnableTextureWebP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnableTextureWebP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnableTextureWebP, ((int)(resources.GetObject("cbEnableTextureWebP.IconPadding"))));
            this.cbEnableTextureWebP.Name = "cbEnableTextureWebP";
            this.toolTip1.SetToolTip(this.cbEnableTextureWebP, resources.GetString("cbEnableTextureWebP.ToolTip"));
            this.cbEnableTextureWebP.UseVisualStyleBackColor = true;
            // 
            // cbEnableQuantizedAttributes
            // 
            resources.ApplyResources(this.cbEnableQuantizedAttributes, "cbEnableQuantizedAttributes");
            this.errorProvider1.SetError(this.cbEnableQuantizedAttributes, resources.GetString("cbEnableQuantizedAttributes.Error"));
            this.errorProvider1.SetIconAlignment(this.cbEnableQuantizedAttributes, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEnableQuantizedAttributes.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbEnableQuantizedAttributes, ((int)(resources.GetObject("cbEnableQuantizedAttributes.IconPadding"))));
            this.cbEnableQuantizedAttributes.Name = "cbEnableQuantizedAttributes";
            this.toolTip1.SetToolTip(this.cbEnableQuantizedAttributes, resources.GetString("cbEnableQuantizedAttributes.ToolTip"));
            this.cbEnableQuantizedAttributes.UseVisualStyleBackColor = true;
            // 
            // cbExportSvfzip
            // 
            resources.ApplyResources(this.cbExportSvfzip, "cbExportSvfzip");
            this.errorProvider1.SetError(this.cbExportSvfzip, resources.GetString("cbExportSvfzip.Error"));
            this.errorProvider1.SetIconAlignment(this.cbExportSvfzip, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbExportSvfzip.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbExportSvfzip, ((int)(resources.GetObject("cbExportSvfzip.IconPadding"))));
            this.cbExportSvfzip.Name = "cbExportSvfzip";
            this.toolTip1.SetToolTip(this.cbExportSvfzip, resources.GetString("cbExportSvfzip.ToolTip"));
            this.cbExportSvfzip.UseVisualStyleBackColor = true;
            // 
            // cbGeneratePropDbSqlite
            // 
            resources.ApplyResources(this.cbGeneratePropDbSqlite, "cbGeneratePropDbSqlite");
            this.errorProvider1.SetError(this.cbGeneratePropDbSqlite, resources.GetString("cbGeneratePropDbSqlite.Error"));
            this.errorProvider1.SetIconAlignment(this.cbGeneratePropDbSqlite, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbGeneratePropDbSqlite.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbGeneratePropDbSqlite, ((int)(resources.GetObject("cbGeneratePropDbSqlite.IconPadding"))));
            this.cbGeneratePropDbSqlite.Name = "cbGeneratePropDbSqlite";
            this.toolTip1.SetToolTip(this.cbGeneratePropDbSqlite, resources.GetString("cbGeneratePropDbSqlite.ToolTip"));
            this.cbGeneratePropDbSqlite.UseVisualStyleBackColor = true;
            // 
            // cbUseDraco
            // 
            resources.ApplyResources(this.cbUseDraco, "cbUseDraco");
            this.errorProvider1.SetError(this.cbUseDraco, resources.GetString("cbUseDraco.Error"));
            this.errorProvider1.SetIconAlignment(this.cbUseDraco, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbUseDraco.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbUseDraco, ((int)(resources.GetObject("cbUseDraco.IconPadding"))));
            this.cbUseDraco.Name = "cbUseDraco";
            this.toolTip1.SetToolTip(this.cbUseDraco, resources.GetString("cbUseDraco.ToolTip"));
            this.cbUseDraco.UseVisualStyleBackColor = true;
            // 
            // gpExclude
            // 
            resources.ApplyResources(this.gpExclude, "gpExclude");
            this.gpExclude.Controls.Add(this.cbExcludeUnselectedElements);
            this.gpExclude.Controls.Add(this.cbExcludeModelPoints);
            this.gpExclude.Controls.Add(this.cbExcludeLines);
            this.errorProvider1.SetError(this.gpExclude, resources.GetString("gpExclude.Error"));
            this.errorProvider1.SetIconAlignment(this.gpExclude, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpExclude.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpExclude, ((int)(resources.GetObject("gpExclude.IconPadding"))));
            this.gpExclude.Name = "gpExclude";
            this.gpExclude.TabStop = false;
            this.toolTip1.SetToolTip(this.gpExclude, resources.GetString("gpExclude.ToolTip"));
            // 
            // cbExcludeUnselectedElements
            // 
            resources.ApplyResources(this.cbExcludeUnselectedElements, "cbExcludeUnselectedElements");
            this.errorProvider1.SetError(this.cbExcludeUnselectedElements, resources.GetString("cbExcludeUnselectedElements.Error"));
            this.errorProvider1.SetIconAlignment(this.cbExcludeUnselectedElements, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbExcludeUnselectedElements.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbExcludeUnselectedElements, ((int)(resources.GetObject("cbExcludeUnselectedElements.IconPadding"))));
            this.cbExcludeUnselectedElements.Name = "cbExcludeUnselectedElements";
            this.toolTip1.SetToolTip(this.cbExcludeUnselectedElements, resources.GetString("cbExcludeUnselectedElements.ToolTip"));
            this.cbExcludeUnselectedElements.UseVisualStyleBackColor = true;
            // 
            // cbExcludeModelPoints
            // 
            resources.ApplyResources(this.cbExcludeModelPoints, "cbExcludeModelPoints");
            this.errorProvider1.SetError(this.cbExcludeModelPoints, resources.GetString("cbExcludeModelPoints.Error"));
            this.errorProvider1.SetIconAlignment(this.cbExcludeModelPoints, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbExcludeModelPoints.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbExcludeModelPoints, ((int)(resources.GetObject("cbExcludeModelPoints.IconPadding"))));
            this.cbExcludeModelPoints.Name = "cbExcludeModelPoints";
            this.toolTip1.SetToolTip(this.cbExcludeModelPoints, resources.GetString("cbExcludeModelPoints.ToolTip"));
            this.cbExcludeModelPoints.UseVisualStyleBackColor = true;
            // 
            // cbExcludeLines
            // 
            resources.ApplyResources(this.cbExcludeLines, "cbExcludeLines");
            this.errorProvider1.SetError(this.cbExcludeLines, resources.GetString("cbExcludeLines.Error"));
            this.errorProvider1.SetIconAlignment(this.cbExcludeLines, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbExcludeLines.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbExcludeLines, ((int)(resources.GetObject("cbExcludeLines.IconPadding"))));
            this.cbExcludeLines.Name = "cbExcludeLines";
            this.toolTip1.SetToolTip(this.cbExcludeLines, resources.GetString("cbExcludeLines.ToolTip"));
            this.cbExcludeLines.UseVisualStyleBackColor = true;
            // 
            // gpGeneral
            // 
            resources.ApplyResources(this.gpGeneral, "gpGeneral");
            this.gpGeneral.Controls.Add(this.cbGenerateOutline);
            this.gpGeneral.Controls.Add(this.lblGenerate);
            this.gpGeneral.Controls.Add(this.cbVisualStyle);
            this.gpGeneral.Controls.Add(this.cbGenerateThumbnail);
            this.gpGeneral.Controls.Add(this.lblVisualStyle);
            this.gpGeneral.Controls.Add(this.cbLevelOfDetail);
            this.gpGeneral.Controls.Add(this.lblLevelOfDetail);
            this.errorProvider1.SetError(this.gpGeneral, resources.GetString("gpGeneral.Error"));
            this.errorProvider1.SetIconAlignment(this.gpGeneral, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpGeneral.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpGeneral, ((int)(resources.GetObject("gpGeneral.IconPadding"))));
            this.gpGeneral.Name = "gpGeneral";
            this.gpGeneral.TabStop = false;
            this.toolTip1.SetToolTip(this.gpGeneral, resources.GetString("gpGeneral.ToolTip"));
            // 
            // cbGenerateOutline
            // 
            resources.ApplyResources(this.cbGenerateOutline, "cbGenerateOutline");
            this.errorProvider1.SetError(this.cbGenerateOutline, resources.GetString("cbGenerateOutline.Error"));
            this.errorProvider1.SetIconAlignment(this.cbGenerateOutline, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbGenerateOutline.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbGenerateOutline, ((int)(resources.GetObject("cbGenerateOutline.IconPadding"))));
            this.cbGenerateOutline.Name = "cbGenerateOutline";
            this.toolTip1.SetToolTip(this.cbGenerateOutline, resources.GetString("cbGenerateOutline.ToolTip"));
            this.cbGenerateOutline.UseVisualStyleBackColor = true;
            // 
            // lblGenerate
            // 
            resources.ApplyResources(this.lblGenerate, "lblGenerate");
            this.errorProvider1.SetError(this.lblGenerate, resources.GetString("lblGenerate.Error"));
            this.errorProvider1.SetIconAlignment(this.lblGenerate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblGenerate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblGenerate, ((int)(resources.GetObject("lblGenerate.IconPadding"))));
            this.lblGenerate.Name = "lblGenerate";
            this.toolTip1.SetToolTip(this.lblGenerate, resources.GetString("lblGenerate.ToolTip"));
            // 
            // cbVisualStyle
            // 
            resources.ApplyResources(this.cbVisualStyle, "cbVisualStyle");
            this.cbVisualStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbVisualStyle, resources.GetString("cbVisualStyle.Error"));
            this.cbVisualStyle.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbVisualStyle, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbVisualStyle.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbVisualStyle, ((int)(resources.GetObject("cbVisualStyle.IconPadding"))));
            this.cbVisualStyle.Name = "cbVisualStyle";
            this.toolTip1.SetToolTip(this.cbVisualStyle, resources.GetString("cbVisualStyle.ToolTip"));
            this.cbVisualStyle.SelectedIndexChanged += new System.EventHandler(this.cbVisualStyle_SelectedIndexChanged);
            // 
            // cbGenerateThumbnail
            // 
            resources.ApplyResources(this.cbGenerateThumbnail, "cbGenerateThumbnail");
            this.errorProvider1.SetError(this.cbGenerateThumbnail, resources.GetString("cbGenerateThumbnail.Error"));
            this.errorProvider1.SetIconAlignment(this.cbGenerateThumbnail, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbGenerateThumbnail.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbGenerateThumbnail, ((int)(resources.GetObject("cbGenerateThumbnail.IconPadding"))));
            this.cbGenerateThumbnail.Name = "cbGenerateThumbnail";
            this.toolTip1.SetToolTip(this.cbGenerateThumbnail, resources.GetString("cbGenerateThumbnail.ToolTip"));
            this.cbGenerateThumbnail.UseVisualStyleBackColor = true;
            // 
            // lblVisualStyle
            // 
            resources.ApplyResources(this.lblVisualStyle, "lblVisualStyle");
            this.errorProvider1.SetError(this.lblVisualStyle, resources.GetString("lblVisualStyle.Error"));
            this.errorProvider1.SetIconAlignment(this.lblVisualStyle, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblVisualStyle.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblVisualStyle, ((int)(resources.GetObject("lblVisualStyle.IconPadding"))));
            this.lblVisualStyle.Name = "lblVisualStyle";
            this.toolTip1.SetToolTip(this.lblVisualStyle, resources.GetString("lblVisualStyle.ToolTip"));
            // 
            // cbLevelOfDetail
            // 
            resources.ApplyResources(this.cbLevelOfDetail, "cbLevelOfDetail");
            this.cbLevelOfDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cbLevelOfDetail, resources.GetString("cbLevelOfDetail.Error"));
            this.cbLevelOfDetail.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cbLevelOfDetail, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbLevelOfDetail.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cbLevelOfDetail, ((int)(resources.GetObject("cbLevelOfDetail.IconPadding"))));
            this.cbLevelOfDetail.Name = "cbLevelOfDetail";
            this.toolTip1.SetToolTip(this.cbLevelOfDetail, resources.GetString("cbLevelOfDetail.ToolTip"));
            // 
            // lblLevelOfDetail
            // 
            resources.ApplyResources(this.lblLevelOfDetail, "lblLevelOfDetail");
            this.errorProvider1.SetError(this.lblLevelOfDetail, resources.GetString("lblLevelOfDetail.Error"));
            this.errorProvider1.SetIconAlignment(this.lblLevelOfDetail, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLevelOfDetail.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblLevelOfDetail, ((int)(resources.GetObject("lblLevelOfDetail.IconPadding"))));
            this.lblLevelOfDetail.Name = "lblLevelOfDetail";
            this.toolTip1.SetToolTip(this.lblLevelOfDetail, resources.GetString("lblLevelOfDetail.ToolTip"));
            // 
            // lblOptions
            // 
            resources.ApplyResources(this.lblOptions, "lblOptions");
            this.errorProvider1.SetError(this.lblOptions, resources.GetString("lblOptions.Error"));
            this.errorProvider1.SetIconAlignment(this.lblOptions, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblOptions.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblOptions, ((int)(resources.GetObject("lblOptions.IconPadding"))));
            this.lblOptions.Name = "lblOptions";
            this.toolTip1.SetToolTip(this.lblOptions, resources.GetString("lblOptions.ToolTip"));
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.errorProvider1.SetError(this.btnBrowse, resources.GetString("btnBrowse.Error"));
            this.errorProvider1.SetIconAlignment(this.btnBrowse, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnBrowse.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnBrowse, ((int)(resources.GetObject("btnBrowse.IconPadding"))));
            this.btnBrowse.Name = "btnBrowse";
            this.toolTip1.SetToolTip(this.btnBrowse, resources.GetString("btnBrowse.ToolTip"));
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtTargetPath
            // 
            resources.ApplyResources(this.txtTargetPath, "txtTargetPath");
            this.errorProvider1.SetError(this.txtTargetPath, resources.GetString("txtTargetPath.Error"));
            this.errorProvider1.SetIconAlignment(this.txtTargetPath, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtTargetPath.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtTargetPath, ((int)(resources.GetObject("txtTargetPath.IconPadding"))));
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtTargetPath, resources.GetString("txtTargetPath.ToolTip"));
            // 
            // lblOutputPath
            // 
            resources.ApplyResources(this.lblOutputPath, "lblOutputPath");
            this.errorProvider1.SetError(this.lblOutputPath, resources.GetString("lblOutputPath.Error"));
            this.errorProvider1.SetIconAlignment(this.lblOutputPath, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblOutputPath.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblOutputPath, ((int)(resources.GetObject("lblOutputPath.IconPadding"))));
            this.lblOutputPath.Name = "lblOutputPath";
            this.toolTip1.SetToolTip(this.lblOutputPath, resources.GetString("lblOutputPath.ToolTip"));
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // ExportCesium3DTiles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.gpContainer);
            this.errorProvider1.SetError(this, resources.GetString("$this.Error"));
            this.errorProvider1.SetIconAlignment(this, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("$this.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this, ((int)(resources.GetObject("$this.IconPadding"))));
            this.Name = "ExportCesium3DTiles";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.gpContainer.ResumeLayout(false);
            this.gpContainer.PerformLayout();
            this.gpSiteInfo.ResumeLayout(false);
            this.gpSiteInfo.PerformLayout();
            this.gb3DTiles.ResumeLayout(false);
            this.gb3DTiles.PerformLayout();
            this.gbAdvanced.ResumeLayout(false);
            this.gbAdvanced.PerformLayout();
            this.gpExclude.ResumeLayout(false);
            this.gpExclude.PerformLayout();
            this.gpGeneral.ResumeLayout(false);
            this.gpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gpContainer;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.ComboBox cbVisualStyle;
        private System.Windows.Forms.Label lblVisualStyle;
        private System.Windows.Forms.ComboBox cbLevelOfDetail;
        private System.Windows.Forms.Label lblLevelOfDetail;
        private System.Windows.Forms.GroupBox gpGeneral;
        private System.Windows.Forms.GroupBox gpExclude;
        private System.Windows.Forms.CheckBox cbExcludeUnselectedElements;
        private System.Windows.Forms.CheckBox cbExcludeModelPoints;
        private System.Windows.Forms.CheckBox cbExcludeLines;
        private System.Windows.Forms.GroupBox gbAdvanced;
        private System.Windows.Forms.CheckBox cbUseDraco;
        private System.Windows.Forms.CheckBox cbGeneratePropDbSqlite;
        private System.Windows.Forms.CheckBox cbExportSvfzip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox gb3DTiles;
        private System.Windows.Forms.RadioButton rbModeBasic;
        private System.Windows.Forms.CheckBox cbEnableQuantizedAttributes;
        private System.Windows.Forms.CheckBox cbEnableTextureWebP;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox gpSiteInfo;
        private System.Windows.Forms.TextBox txtRotation;
        private System.Windows.Forms.Label lblRotation;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.RadioButton rbModeShellElement;
        private System.Windows.Forms.RadioButton rbModeShellMesh;
        private System.Windows.Forms.CheckBox cbGenerateThumbnail;
        private System.Windows.Forms.CheckBox cbEnableUnlitMaterials;
        private System.Windows.Forms.RadioButton rbGeoreferencingCustom;
        private System.Windows.Forms.RadioButton rbGeoreferencingSiteLocation;
        private System.Windows.Forms.RadioButton rbGeoreferencingNone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbAlignOriginToSiteCenter;
        private System.Windows.Forms.CheckBox cbGenerateOutline;
        private System.Windows.Forms.Label lblGenerate;
    }
}