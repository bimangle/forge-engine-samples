using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Georeferncing.Utility;

namespace Bimangle.ForgeEngine.Georeferncing
{
    partial class FormProjCreate : Form
    {
        private static State _LastState;

        private readonly IGeoreferncingHost _Host;

        public FormProjCreate(IGeoreferncingHost host) : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
        }

        public FormProjCreate()
        {
            InitializeComponent();
        }

        private void FormProjCreate_Load(object sender, EventArgs e)
        {
            InitUI();
        }

        public string Definition { get; private set; }

        private void InitUI()
        {
            #region 初始化地理坐标系下拉框
            {
                var gcsTypes = new List<ItemValue<ProjBuilder.GeoGCS>>()
                {
                    new ItemValue<ProjBuilder.GeoGCS>(GeoStrings.GcsCGCS2000, ProjBuilder.GeoGCS.GCS_China_Geodetic_Coordinate_System_2000),
                    new ItemValue<ProjBuilder.GeoGCS>(GeoStrings.GcsWGS84, ProjBuilder.GeoGCS.GCS_WGS_1984),
                    new ItemValue<ProjBuilder.GeoGCS>(GeoStrings.GcsXian1980, ProjBuilder.GeoGCS.GCS_Xian_1980),
                    new ItemValue<ProjBuilder.GeoGCS>(GeoStrings.GcsBeijing1954, ProjBuilder.GeoGCS.GCS_Beijing_1954)
                };

                cbGcs.Items.Clear();
                foreach (var gcsType in gcsTypes)
                {
                    cbGcs.Items.Add(gcsType);
                }

                cbGcs.SetSelectedValue(_LastState?.Gcs ?? ProjBuilder.GeoGCS.GCS_China_Geodetic_Coordinate_System_2000);
            }
            #endregion

            #region 其它输入项默认值
            {
                var site = _Host.GetModelSiteInfo() ?? SiteInfo.CreateDefault();
                txtCentralMeridian.Text = _LastState?.CentralMeridian ??
                                          ProjBuilder.GetCentralMeridian(site.Longitude).ToLatLonString();
                txtFalseEasting.Text = _LastState?.FalseEasting ?? 
                                       (500000.0).ToMetreString();
                txtFalseNorthing.Text = _LastState?.FalseNorthing ?? 
                                        (0.0).ToMetreString();
            }
            #endregion

            #region 其它
            {
                txtDefinition.Text = _LastState?.Definition ?? string.Empty;

                if (string.IsNullOrWhiteSpace(txtDefinition.Text))
                {
                    txtDefinition.Enabled = false;
                    btnOK.Enabled = false;
                }
            }
            #endregion
        }

        private void txtCentralMeridian_Validating(object sender, CancelEventArgs e)
        {
            txtCentralMeridian.TryParseLongitude(errorProvider1, out _);
        }

        private void txtFalseEasting_Validating(object sender, CancelEventArgs e)
        {
            txtFalseEasting.TryParseNumber(errorProvider1, out _);
        }

        private void txtFalseNorthing_Validating(object sender, CancelEventArgs e)
        {
            txtFalseNorthing.TryParseNumber(errorProvider1, out _);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_Host.CheckProjDefinition(txtDefinition.Text, out _) == false)
            {
                txtDefinition.Focus();
                return;
            }

            _LastState = new State
            {
                Gcs = cbGcs.GetSelectedValue<ProjBuilder.GeoGCS>(),
                CentralMeridian = txtCentralMeridian.Text,
                FalseEasting = txtFalseEasting.Text,
                FalseNorthing = txtFalseNorthing.Text,
                Definition = txtDefinition.Text
            };

            Definition = txtDefinition.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            var form = new FormProjParamCalc(_Host);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var gcsValue = cbGcs.GetSelectedValue<ProjBuilder.GeoGCS>();
                var validator = _Host.GetProjValidator();

                double? centralMeridian = null;
                if(cbPinned.Checked && 
                   txtCentralMeridian.TryParseLongitude(errorProvider1, out var centralMeridianValue))
                {
                    centralMeridian = centralMeridianValue;
                }

                var proj = validator.CreateProj(gcsValue, centralMeridian, form.LocalX, form.LocalY, form.Lon, form.Lat);
                if (proj != null)
                {
                    txtCentralMeridian.Text = proj.CentralMeridian.ToLatLonString();
                    txtFalseEasting.Text = proj.FalseEasting.ToMetreString();
                    txtFalseNorthing.Text = proj.FalseNorthing.ToMetreString();
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtCentralMeridian.TryParseLongitude(errorProvider1, out var centralMeridian) == false)
            {
                txtCentralMeridian.Focus();
                return;
            }

            if (txtFalseEasting.TryParseNumber(errorProvider1, out var falseEasting) == false)
            {
                txtFalseEasting.Focus();
                return;
            }

            if (txtFalseNorthing.TryParseNumber(errorProvider1, out var falseNorthing) == false)
            {
                txtFalseNorthing.Focus();
                return;
            }

            var proj = new ProjBuilder.ProjDefinition();
            proj.GeoGCS = cbGcs.GetSelectedValue<ProjBuilder.GeoGCS>();
            proj.CentralMeridian = centralMeridian;
            proj.FalseEasting = falseEasting;
            proj.FalseNorthing = falseNorthing;

            txtDefinition.Text = proj.ToWKT();
            txtDefinition.ForeColor = SystemColors.WindowText;

            errorProvider1.SetError(txtDefinition, null);

            btnOK.Enabled = true;
            txtDefinition.Enabled = true;
        }

        private class State
        {
            public ProjBuilder.GeoGCS Gcs { get; set; }
            public string CentralMeridian { get; set; }
            public string FalseEasting { get; set; }
            public string FalseNorthing { get; set; }
            public string Definition { get; set; }
        }

        private void txtDefinition_Enter(object sender, EventArgs e)
        {
            txtDefinition.ForeColor = SystemColors.WindowText;
        }

        private void txtDefinition_Validating(object sender, CancelEventArgs e)
        {
            var s = txtDefinition.Text;
            if (_Host.CheckProjDefinition(s, out _) == false)
            {
                txtDefinition.ForeColor = Color.Red;
                errorProvider1.SetError(txtDefinition, GeoStrings.InvalidProjectDefinition);
            }
            else
            {
                txtDefinition.ForeColor = Color.Green;
                errorProvider1.SetError(txtDefinition, null);
            }
        }

        private void cbPinned_CheckedChanged(object sender, EventArgs e)
        {
            txtCentralMeridian.ReadOnly = cbPinned.Checked;
        }
    }
}
