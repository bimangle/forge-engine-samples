namespace Bimangle.ForgeEngine.Georeferncing
{
    partial class FormGeoreferncingTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGeoreferncingTest));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblGeoidHeight = new System.Windows.Forms.Label();
            this.txtGeoidHeight = new System.Windows.Forms.TextBox();
            this.txtGeoidConstantOffset = new System.Windows.Forms.TextBox();
            this.txtProjCoordinateOffset = new System.Windows.Forms.TextBox();
            this.txtGeoidGrid = new System.Windows.Forms.TextBox();
            this.gpProjParameters = new System.Windows.Forms.GroupBox();
            this.txtProjectionHeight = new System.Windows.Forms.TextBox();
            this.lblProjectionHeight = new System.Windows.Forms.Label();
            this.lblGeoidConstantOffset = new System.Windows.Forms.Label();
            this.lblGeoidHeightCorrection = new System.Windows.Forms.Label();
            this.txtProjDefinition = new System.Windows.Forms.TextBox();
            this.lblProjCoordinateOffset = new System.Windows.Forms.Label();
            this.lblProjDefinition = new System.Windows.Forms.Label();
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.gpProjParameters.SuspendLayout();
            this.gpTestRun.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // lblGeoidHeight
            // 
            resources.ApplyResources(this.lblGeoidHeight, "lblGeoidHeight");
            this.errorProvider1.SetError(this.lblGeoidHeight, resources.GetString("lblGeoidHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.lblGeoidHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblGeoidHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblGeoidHeight, ((int)(resources.GetObject("lblGeoidHeight.IconPadding"))));
            this.lblGeoidHeight.Name = "lblGeoidHeight";
            this.toolTip1.SetToolTip(this.lblGeoidHeight, resources.GetString("lblGeoidHeight.ToolTip"));
            // 
            // txtGeoidHeight
            // 
            resources.ApplyResources(this.txtGeoidHeight, "txtGeoidHeight");
            this.errorProvider1.SetError(this.txtGeoidHeight, resources.GetString("txtGeoidHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.txtGeoidHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtGeoidHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtGeoidHeight, ((int)(resources.GetObject("txtGeoidHeight.IconPadding"))));
            this.txtGeoidHeight.Name = "txtGeoidHeight";
            this.txtGeoidHeight.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtGeoidHeight, resources.GetString("txtGeoidHeight.ToolTip"));
            // 
            // txtGeoidConstantOffset
            // 
            resources.ApplyResources(this.txtGeoidConstantOffset, "txtGeoidConstantOffset");
            this.errorProvider1.SetError(this.txtGeoidConstantOffset, resources.GetString("txtGeoidConstantOffset.Error"));
            this.errorProvider1.SetIconAlignment(this.txtGeoidConstantOffset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtGeoidConstantOffset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtGeoidConstantOffset, ((int)(resources.GetObject("txtGeoidConstantOffset.IconPadding"))));
            this.txtGeoidConstantOffset.Name = "txtGeoidConstantOffset";
            this.txtGeoidConstantOffset.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtGeoidConstantOffset, resources.GetString("txtGeoidConstantOffset.ToolTip"));
            // 
            // txtProjCoordinateOffset
            // 
            resources.ApplyResources(this.txtProjCoordinateOffset, "txtProjCoordinateOffset");
            this.errorProvider1.SetError(this.txtProjCoordinateOffset, resources.GetString("txtProjCoordinateOffset.Error"));
            this.errorProvider1.SetIconAlignment(this.txtProjCoordinateOffset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtProjCoordinateOffset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtProjCoordinateOffset, ((int)(resources.GetObject("txtProjCoordinateOffset.IconPadding"))));
            this.txtProjCoordinateOffset.Name = "txtProjCoordinateOffset";
            this.txtProjCoordinateOffset.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtProjCoordinateOffset, resources.GetString("txtProjCoordinateOffset.ToolTip"));
            // 
            // txtGeoidGrid
            // 
            resources.ApplyResources(this.txtGeoidGrid, "txtGeoidGrid");
            this.errorProvider1.SetError(this.txtGeoidGrid, resources.GetString("txtGeoidGrid.Error"));
            this.errorProvider1.SetIconAlignment(this.txtGeoidGrid, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtGeoidGrid.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtGeoidGrid, ((int)(resources.GetObject("txtGeoidGrid.IconPadding"))));
            this.txtGeoidGrid.Name = "txtGeoidGrid";
            this.txtGeoidGrid.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtGeoidGrid, resources.GetString("txtGeoidGrid.ToolTip"));
            // 
            // gpProjParameters
            // 
            resources.ApplyResources(this.gpProjParameters, "gpProjParameters");
            this.gpProjParameters.Controls.Add(this.txtProjectionHeight);
            this.gpProjParameters.Controls.Add(this.lblProjectionHeight);
            this.gpProjParameters.Controls.Add(this.txtGeoidGrid);
            this.gpProjParameters.Controls.Add(this.lblGeoidConstantOffset);
            this.gpProjParameters.Controls.Add(this.txtGeoidConstantOffset);
            this.gpProjParameters.Controls.Add(this.lblGeoidHeightCorrection);
            this.gpProjParameters.Controls.Add(this.txtProjDefinition);
            this.gpProjParameters.Controls.Add(this.txtProjCoordinateOffset);
            this.gpProjParameters.Controls.Add(this.lblProjCoordinateOffset);
            this.gpProjParameters.Controls.Add(this.lblProjDefinition);
            this.errorProvider1.SetError(this.gpProjParameters, resources.GetString("gpProjParameters.Error"));
            this.errorProvider1.SetIconAlignment(this.gpProjParameters, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpProjParameters.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpProjParameters, ((int)(resources.GetObject("gpProjParameters.IconPadding"))));
            this.gpProjParameters.Name = "gpProjParameters";
            this.gpProjParameters.TabStop = false;
            this.toolTip1.SetToolTip(this.gpProjParameters, resources.GetString("gpProjParameters.ToolTip"));
            // 
            // txtProjectionHeight
            // 
            resources.ApplyResources(this.txtProjectionHeight, "txtProjectionHeight");
            this.errorProvider1.SetError(this.txtProjectionHeight, resources.GetString("txtProjectionHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.txtProjectionHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtProjectionHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtProjectionHeight, ((int)(resources.GetObject("txtProjectionHeight.IconPadding"))));
            this.txtProjectionHeight.Name = "txtProjectionHeight";
            this.txtProjectionHeight.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtProjectionHeight, resources.GetString("txtProjectionHeight.ToolTip"));
            // 
            // lblProjectionHeight
            // 
            resources.ApplyResources(this.lblProjectionHeight, "lblProjectionHeight");
            this.errorProvider1.SetError(this.lblProjectionHeight, resources.GetString("lblProjectionHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.lblProjectionHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjectionHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblProjectionHeight, ((int)(resources.GetObject("lblProjectionHeight.IconPadding"))));
            this.lblProjectionHeight.Name = "lblProjectionHeight";
            this.toolTip1.SetToolTip(this.lblProjectionHeight, resources.GetString("lblProjectionHeight.ToolTip"));
            // 
            // lblGeoidConstantOffset
            // 
            resources.ApplyResources(this.lblGeoidConstantOffset, "lblGeoidConstantOffset");
            this.errorProvider1.SetError(this.lblGeoidConstantOffset, resources.GetString("lblGeoidConstantOffset.Error"));
            this.errorProvider1.SetIconAlignment(this.lblGeoidConstantOffset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblGeoidConstantOffset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblGeoidConstantOffset, ((int)(resources.GetObject("lblGeoidConstantOffset.IconPadding"))));
            this.lblGeoidConstantOffset.Name = "lblGeoidConstantOffset";
            this.toolTip1.SetToolTip(this.lblGeoidConstantOffset, resources.GetString("lblGeoidConstantOffset.ToolTip"));
            // 
            // lblGeoidHeightCorrection
            // 
            resources.ApplyResources(this.lblGeoidHeightCorrection, "lblGeoidHeightCorrection");
            this.errorProvider1.SetError(this.lblGeoidHeightCorrection, resources.GetString("lblGeoidHeightCorrection.Error"));
            this.errorProvider1.SetIconAlignment(this.lblGeoidHeightCorrection, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblGeoidHeightCorrection.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblGeoidHeightCorrection, ((int)(resources.GetObject("lblGeoidHeightCorrection.IconPadding"))));
            this.lblGeoidHeightCorrection.Name = "lblGeoidHeightCorrection";
            this.toolTip1.SetToolTip(this.lblGeoidHeightCorrection, resources.GetString("lblGeoidHeightCorrection.ToolTip"));
            // 
            // txtProjDefinition
            // 
            resources.ApplyResources(this.txtProjDefinition, "txtProjDefinition");
            this.errorProvider1.SetError(this.txtProjDefinition, resources.GetString("txtProjDefinition.Error"));
            this.errorProvider1.SetIconAlignment(this.txtProjDefinition, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtProjDefinition.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtProjDefinition, ((int)(resources.GetObject("txtProjDefinition.IconPadding"))));
            this.txtProjDefinition.Name = "txtProjDefinition";
            this.txtProjDefinition.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtProjDefinition, resources.GetString("txtProjDefinition.ToolTip"));
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
            // gpTestRun
            // 
            resources.ApplyResources(this.gpTestRun, "gpTestRun");
            this.gpTestRun.Controls.Add(this.lblGeoidHeight);
            this.gpTestRun.Controls.Add(this.btnSwapNE);
            this.gpTestRun.Controls.Add(this.txtWorldHeight);
            this.gpTestRun.Controls.Add(this.txtGeoidHeight);
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
            this.errorProvider1.SetError(this.gpTestRun, resources.GetString("gpTestRun.Error"));
            this.errorProvider1.SetIconAlignment(this.gpTestRun, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gpTestRun.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gpTestRun, ((int)(resources.GetObject("gpTestRun.IconPadding"))));
            this.gpTestRun.Name = "gpTestRun";
            this.gpTestRun.TabStop = false;
            this.toolTip1.SetToolTip(this.gpTestRun, resources.GetString("gpTestRun.ToolTip"));
            // 
            // btnSwapNE
            // 
            resources.ApplyResources(this.btnSwapNE, "btnSwapNE");
            this.errorProvider1.SetError(this.btnSwapNE, resources.GetString("btnSwapNE.Error"));
            this.errorProvider1.SetIconAlignment(this.btnSwapNE, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnSwapNE.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnSwapNE, ((int)(resources.GetObject("btnSwapNE.IconPadding"))));
            this.btnSwapNE.Name = "btnSwapNE";
            this.toolTip1.SetToolTip(this.btnSwapNE, resources.GetString("btnSwapNE.ToolTip"));
            this.btnSwapNE.UseVisualStyleBackColor = true;
            this.btnSwapNE.Click += new System.EventHandler(this.btnSwapNE_Click);
            // 
            // txtWorldHeight
            // 
            resources.ApplyResources(this.txtWorldHeight, "txtWorldHeight");
            this.errorProvider1.SetError(this.txtWorldHeight, resources.GetString("txtWorldHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.txtWorldHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtWorldHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtWorldHeight, ((int)(resources.GetObject("txtWorldHeight.IconPadding"))));
            this.txtWorldHeight.Name = "txtWorldHeight";
            this.txtWorldHeight.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtWorldHeight, resources.GetString("txtWorldHeight.ToolTip"));
            // 
            // lblWorldHeight
            // 
            resources.ApplyResources(this.lblWorldHeight, "lblWorldHeight");
            this.errorProvider1.SetError(this.lblWorldHeight, resources.GetString("lblWorldHeight.Error"));
            this.errorProvider1.SetIconAlignment(this.lblWorldHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblWorldHeight.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblWorldHeight, ((int)(resources.GetObject("lblWorldHeight.IconPadding"))));
            this.lblWorldHeight.Name = "lblWorldHeight";
            this.toolTip1.SetToolTip(this.lblWorldHeight, resources.GetString("lblWorldHeight.ToolTip"));
            // 
            // txtWorldLat
            // 
            resources.ApplyResources(this.txtWorldLat, "txtWorldLat");
            this.errorProvider1.SetError(this.txtWorldLat, resources.GetString("txtWorldLat.Error"));
            this.errorProvider1.SetIconAlignment(this.txtWorldLat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtWorldLat.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtWorldLat, ((int)(resources.GetObject("txtWorldLat.IconPadding"))));
            this.txtWorldLat.Name = "txtWorldLat";
            this.txtWorldLat.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtWorldLat, resources.GetString("txtWorldLat.ToolTip"));
            // 
            // lblWorldLat
            // 
            resources.ApplyResources(this.lblWorldLat, "lblWorldLat");
            this.errorProvider1.SetError(this.lblWorldLat, resources.GetString("lblWorldLat.Error"));
            this.errorProvider1.SetIconAlignment(this.lblWorldLat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblWorldLat.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblWorldLat, ((int)(resources.GetObject("lblWorldLat.IconPadding"))));
            this.lblWorldLat.Name = "lblWorldLat";
            this.toolTip1.SetToolTip(this.lblWorldLat, resources.GetString("lblWorldLat.ToolTip"));
            // 
            // txtWorldLon
            // 
            resources.ApplyResources(this.txtWorldLon, "txtWorldLon");
            this.errorProvider1.SetError(this.txtWorldLon, resources.GetString("txtWorldLon.Error"));
            this.errorProvider1.SetIconAlignment(this.txtWorldLon, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtWorldLon.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtWorldLon, ((int)(resources.GetObject("txtWorldLon.IconPadding"))));
            this.txtWorldLon.Name = "txtWorldLon";
            this.txtWorldLon.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtWorldLon, resources.GetString("txtWorldLon.ToolTip"));
            // 
            // lblWorldLon
            // 
            resources.ApplyResources(this.lblWorldLon, "lblWorldLon");
            this.errorProvider1.SetError(this.lblWorldLon, resources.GetString("lblWorldLon.Error"));
            this.errorProvider1.SetIconAlignment(this.lblWorldLon, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblWorldLon.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblWorldLon, ((int)(resources.GetObject("lblWorldLon.IconPadding"))));
            this.lblWorldLon.Name = "lblWorldLon";
            this.toolTip1.SetToolTip(this.lblWorldLon, resources.GetString("lblWorldLon.ToolTip"));
            // 
            // lblWorld
            // 
            resources.ApplyResources(this.lblWorld, "lblWorld");
            this.errorProvider1.SetError(this.lblWorld, resources.GetString("lblWorld.Error"));
            this.errorProvider1.SetIconAlignment(this.lblWorld, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblWorld.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblWorld, ((int)(resources.GetObject("lblWorld.IconPadding"))));
            this.lblWorld.Name = "lblWorld";
            this.toolTip1.SetToolTip(this.lblWorld, resources.GetString("lblWorld.ToolTip"));
            // 
            // txtProjectedH
            // 
            resources.ApplyResources(this.txtProjectedH, "txtProjectedH");
            this.errorProvider1.SetError(this.txtProjectedH, resources.GetString("txtProjectedH.Error"));
            this.errorProvider1.SetIconAlignment(this.txtProjectedH, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtProjectedH.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtProjectedH, ((int)(resources.GetObject("txtProjectedH.IconPadding"))));
            this.txtProjectedH.Name = "txtProjectedH";
            this.txtProjectedH.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtProjectedH, resources.GetString("txtProjectedH.ToolTip"));
            // 
            // lblProjectedH
            // 
            resources.ApplyResources(this.lblProjectedH, "lblProjectedH");
            this.errorProvider1.SetError(this.lblProjectedH, resources.GetString("lblProjectedH.Error"));
            this.errorProvider1.SetIconAlignment(this.lblProjectedH, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjectedH.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblProjectedH, ((int)(resources.GetObject("lblProjectedH.IconPadding"))));
            this.lblProjectedH.Name = "lblProjectedH";
            this.toolTip1.SetToolTip(this.lblProjectedH, resources.GetString("lblProjectedH.ToolTip"));
            // 
            // txtProjectedN
            // 
            resources.ApplyResources(this.txtProjectedN, "txtProjectedN");
            this.errorProvider1.SetError(this.txtProjectedN, resources.GetString("txtProjectedN.Error"));
            this.errorProvider1.SetIconAlignment(this.txtProjectedN, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtProjectedN.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtProjectedN, ((int)(resources.GetObject("txtProjectedN.IconPadding"))));
            this.txtProjectedN.Name = "txtProjectedN";
            this.txtProjectedN.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtProjectedN, resources.GetString("txtProjectedN.ToolTip"));
            // 
            // btnTestRun
            // 
            resources.ApplyResources(this.btnTestRun, "btnTestRun");
            this.errorProvider1.SetError(this.btnTestRun, resources.GetString("btnTestRun.Error"));
            this.errorProvider1.SetIconAlignment(this.btnTestRun, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnTestRun.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnTestRun, ((int)(resources.GetObject("btnTestRun.IconPadding"))));
            this.btnTestRun.Name = "btnTestRun";
            this.toolTip1.SetToolTip(this.btnTestRun, resources.GetString("btnTestRun.ToolTip"));
            this.btnTestRun.UseVisualStyleBackColor = true;
            this.btnTestRun.Click += new System.EventHandler(this.btnTestRun_Click);
            // 
            // lblProjectedN
            // 
            resources.ApplyResources(this.lblProjectedN, "lblProjectedN");
            this.errorProvider1.SetError(this.lblProjectedN, resources.GetString("lblProjectedN.Error"));
            this.errorProvider1.SetIconAlignment(this.lblProjectedN, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjectedN.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblProjectedN, ((int)(resources.GetObject("lblProjectedN.IconPadding"))));
            this.lblProjectedN.Name = "lblProjectedN";
            this.toolTip1.SetToolTip(this.lblProjectedN, resources.GetString("lblProjectedN.ToolTip"));
            // 
            // txtProjectedE
            // 
            resources.ApplyResources(this.txtProjectedE, "txtProjectedE");
            this.errorProvider1.SetError(this.txtProjectedE, resources.GetString("txtProjectedE.Error"));
            this.errorProvider1.SetIconAlignment(this.txtProjectedE, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtProjectedE.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtProjectedE, ((int)(resources.GetObject("txtProjectedE.IconPadding"))));
            this.txtProjectedE.Name = "txtProjectedE";
            this.txtProjectedE.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtProjectedE, resources.GetString("txtProjectedE.ToolTip"));
            // 
            // lblProjectedE
            // 
            resources.ApplyResources(this.lblProjectedE, "lblProjectedE");
            this.errorProvider1.SetError(this.lblProjectedE, resources.GetString("lblProjectedE.Error"));
            this.errorProvider1.SetIconAlignment(this.lblProjectedE, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjectedE.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblProjectedE, ((int)(resources.GetObject("lblProjectedE.IconPadding"))));
            this.lblProjectedE.Name = "lblProjectedE";
            this.toolTip1.SetToolTip(this.lblProjectedE, resources.GetString("lblProjectedE.ToolTip"));
            // 
            // lblProjected
            // 
            resources.ApplyResources(this.lblProjected, "lblProjected");
            this.errorProvider1.SetError(this.lblProjected, resources.GetString("lblProjected.Error"));
            this.errorProvider1.SetIconAlignment(this.lblProjected, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblProjected.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblProjected, ((int)(resources.GetObject("lblProjected.IconPadding"))));
            this.lblProjected.Name = "lblProjected";
            this.toolTip1.SetToolTip(this.lblProjected, resources.GetString("lblProjected.ToolTip"));
            // 
            // txtModelH
            // 
            resources.ApplyResources(this.txtModelH, "txtModelH");
            this.errorProvider1.SetError(this.txtModelH, resources.GetString("txtModelH.Error"));
            this.errorProvider1.SetIconAlignment(this.txtModelH, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtModelH.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtModelH, ((int)(resources.GetObject("txtModelH.IconPadding"))));
            this.txtModelH.Name = "txtModelH";
            this.toolTip1.SetToolTip(this.txtModelH, resources.GetString("txtModelH.ToolTip"));
            // 
            // lblModelH
            // 
            resources.ApplyResources(this.lblModelH, "lblModelH");
            this.errorProvider1.SetError(this.lblModelH, resources.GetString("lblModelH.Error"));
            this.errorProvider1.SetIconAlignment(this.lblModelH, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblModelH.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblModelH, ((int)(resources.GetObject("lblModelH.IconPadding"))));
            this.lblModelH.Name = "lblModelH";
            this.toolTip1.SetToolTip(this.lblModelH, resources.GetString("lblModelH.ToolTip"));
            // 
            // txtModelN
            // 
            resources.ApplyResources(this.txtModelN, "txtModelN");
            this.errorProvider1.SetError(this.txtModelN, resources.GetString("txtModelN.Error"));
            this.errorProvider1.SetIconAlignment(this.txtModelN, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtModelN.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtModelN, ((int)(resources.GetObject("txtModelN.IconPadding"))));
            this.txtModelN.Name = "txtModelN";
            this.toolTip1.SetToolTip(this.txtModelN, resources.GetString("txtModelN.ToolTip"));
            // 
            // lblModelN
            // 
            resources.ApplyResources(this.lblModelN, "lblModelN");
            this.errorProvider1.SetError(this.lblModelN, resources.GetString("lblModelN.Error"));
            this.errorProvider1.SetIconAlignment(this.lblModelN, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblModelN.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblModelN, ((int)(resources.GetObject("lblModelN.IconPadding"))));
            this.lblModelN.Name = "lblModelN";
            this.toolTip1.SetToolTip(this.lblModelN, resources.GetString("lblModelN.ToolTip"));
            // 
            // txtModelE
            // 
            resources.ApplyResources(this.txtModelE, "txtModelE");
            this.errorProvider1.SetError(this.txtModelE, resources.GetString("txtModelE.Error"));
            this.errorProvider1.SetIconAlignment(this.txtModelE, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtModelE.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtModelE, ((int)(resources.GetObject("txtModelE.IconPadding"))));
            this.txtModelE.Name = "txtModelE";
            this.toolTip1.SetToolTip(this.txtModelE, resources.GetString("txtModelE.ToolTip"));
            // 
            // lblModelE
            // 
            resources.ApplyResources(this.lblModelE, "lblModelE");
            this.errorProvider1.SetError(this.lblModelE, resources.GetString("lblModelE.Error"));
            this.errorProvider1.SetIconAlignment(this.lblModelE, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblModelE.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblModelE, ((int)(resources.GetObject("lblModelE.IconPadding"))));
            this.lblModelE.Name = "lblModelE";
            this.toolTip1.SetToolTip(this.lblModelE, resources.GetString("lblModelE.ToolTip"));
            // 
            // lblModel
            // 
            resources.ApplyResources(this.lblModel, "lblModel");
            this.errorProvider1.SetError(this.lblModel, resources.GetString("lblModel.Error"));
            this.errorProvider1.SetIconAlignment(this.lblModel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblModel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblModel, ((int)(resources.GetObject("lblModel.IconPadding"))));
            this.lblModel.Name = "lblModel";
            this.toolTip1.SetToolTip(this.lblModel, resources.GetString("lblModel.ToolTip"));
            // 
            // FormGeoreferncingTest
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpTestRun);
            this.Controls.Add(this.gpProjParameters);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGeoreferncingTest";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FormGeoreferncingTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.gpProjParameters.ResumeLayout(false);
            this.gpProjParameters.PerformLayout();
            this.gpTestRun.ResumeLayout(false);
            this.gpTestRun.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gpProjParameters;
        private System.Windows.Forms.TextBox txtGeoidGrid;
        private System.Windows.Forms.Label lblGeoidConstantOffset;
        private System.Windows.Forms.TextBox txtGeoidConstantOffset;
        private System.Windows.Forms.Label lblGeoidHeightCorrection;
        private System.Windows.Forms.TextBox txtProjDefinition;
        private System.Windows.Forms.TextBox txtProjCoordinateOffset;
        private System.Windows.Forms.Label lblProjCoordinateOffset;
        private System.Windows.Forms.Label lblProjDefinition;
        private System.Windows.Forms.Label lblGeoidHeight;
        private System.Windows.Forms.TextBox txtGeoidHeight;
        private System.Windows.Forms.GroupBox gpTestRun;
        private System.Windows.Forms.Button btnSwapNE;
        private System.Windows.Forms.TextBox txtWorldHeight;
        private System.Windows.Forms.Label lblWorldHeight;
        private System.Windows.Forms.TextBox txtWorldLat;
        private System.Windows.Forms.Label lblWorldLat;
        private System.Windows.Forms.TextBox txtWorldLon;
        private System.Windows.Forms.Label lblWorldLon;
        private System.Windows.Forms.Label lblWorld;
        private System.Windows.Forms.TextBox txtProjectedH;
        private System.Windows.Forms.Label lblProjectedH;
        private System.Windows.Forms.TextBox txtProjectedN;
        private System.Windows.Forms.Button btnTestRun;
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
        private System.Windows.Forms.TextBox txtProjectionHeight;
        private System.Windows.Forms.Label lblProjectionHeight;
    }
}