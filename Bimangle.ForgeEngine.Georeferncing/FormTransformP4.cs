using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing.Utility;

namespace Bimangle.ForgeEngine.Georeferncing
{
    partial class FormTransformP4 : Form
    {
        private readonly string _NaN = double.NaN.ToString(CultureInfo.InvariantCulture);

        private readonly TestRunState _LastState = TestRunState.LastState;

        private readonly IGeoreferncingHost _Host;
        private readonly ParameterProj _ParameterOriginal;
        private ParameterProj _Parameter;

        public FormTransformP4(IGeoreferncingHost host, ParameterProj parameter) : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _ParameterOriginal = parameter ?? throw new ArgumentNullException(nameof(parameter));
            _ParameterOriginal.CheckProjOffset();
            _Parameter = _ParameterOriginal.Clone();
        }

        public FormTransformP4()
        {
            InitializeComponent();
        }

        public ParameterProj Parameter => _Parameter;

        private void FormGeoreferncingTest_Load(object sender, EventArgs e)
        {
            InitUI();
            UpdateUI();
        }

        private void InitUI()
        {
            #region 初始化转换参数
            {
                cbUsed.OnCheckedChanged(t =>
                {
                    var used = t.Checked;

                    lblTranslation.Visible = used;
                    lblDx.Visible = txtDx.Visible = used;
                    lblDy.Visible = txtDy.Visible = used;

                    lblRotation.Visible = used;
                    lblWz.Visible = txtWz.Visible = used;

                    lblScaling.Visible = used;
                    lblK.Visible = txtK.Visible = used;
                    lblScalingFactor.Visible = txtScalingFactor.Visible = used;

                    if (used)
                    {
                        _Parameter.OffsetType = ProjOffsetType._2D_Params4;

                        if(_Parameter.Offset == null) _Parameter.Offset = new double[7];
                    }
                    else
                    {
                        _Parameter.OffsetType = ProjOffsetType.None;
                    }

                    RefreshOffsetText();
                });

                txtDx.OnValidating(t => t.TryParseNumber(errorProvider1, out _));
                txtDy.OnValidating(t => t.TryParseNumber(errorProvider1, out _));
                txtWz.OnValidating(t => t.TryParseNumber(errorProvider1, out _));
                txtK.OnValidating(t => t.TryParseNumber(errorProvider1, out _));

                txtDx.OnValidated(t =>
                {
                    _Parameter.Offset[0] = double.Parse(t.Text);
                    RefreshOffsetText();
                });
                txtDy.OnValidated(t =>
                {
                    _Parameter.Offset[1] = double.Parse(t.Text);
                    RefreshOffsetText();
                });

                txtWz.OnValidated(t =>
                {
                    _Parameter.Offset[5] = double.Parse(t.Text) * Math.PI / 648000.0;
                    RefreshOffsetText();
                });

                txtK.OnValidated(t =>
                {
                    var k = double.Parse(t.Text);
                    var m = k * 1e-6;
                    _Parameter.Offset[6] = m;

                    txtScalingFactor.Text = (1.0 + m).GetDoubleString(12);

                    RefreshOffsetText();
                });
            }
            #endregion

            #region 初始化模型坐标
            {
                txtModelN.Text = _LastState.Y;
                txtModelE.Text = _LastState.X;
                txtModelH.Text = _LastState.Z;

                _Host.Adapter.SetDirectionLetters(lblModelE, lblModelN, lblModelH);

                txtModelN.OnValidating(t => t.TryParseNumber(errorProvider1, out _));
                txtModelE.OnValidating(t => t.TryParseNumber(errorProvider1, out _));
                txtModelH.OnValidating(t => t.TryParseNumber(errorProvider1, out _));
            }
            #endregion

            #region 初始化投影坐标
            {
                txtProjectedN.Text = _NaN;
                txtProjectedE.Text = _NaN;
                txtProjectedH.Text = _NaN;
            }
            #endregion

            #region 初始化世界坐标
            {
                txtWorldLat.Text = _NaN;
                txtWorldLon.Text = _NaN;
                txtWorldHeight.Text = _NaN;
            }
            #endregion
        }

        private void UpdateUI()
        {
            #region 初始化转换类型
            {
                cbUsed.Checked = _Parameter.OffsetType == ProjOffsetType._2D_Params4;
            }
            #endregion

            #region 初始化转换参数
            {
                var offset = _Parameter.Offset ?? new double[7];
                var rz = offset[5] * 648000.0 / Math.PI;
                var k = offset[6] * 1e6;

                var sdx = offset[0].ToMetreString();
                var sdy = offset[1].ToMetreString();

                //6位小数已经可以确保最大误差不超过 0.3mm
                var srz = rz.GetDoubleString(6);

                //6位小数已经可以确保最大误差不超过 0.2mm
                var sk = k.GetDoubleString(6);

                txtDx.Text = sdx;
                txtDy.Text = sdy;

                txtWz.Text = srz;

                txtK.Text = sk;
                txtScalingFactor.Text = (1.0 + offset[6]).GetDoubleString(12);
            }
            #endregion

            #region 初始化投影坐标参数
            {
                txtProjCoordinateOffset.Text = _Parameter.GetOffsetString();
            }
            #endregion
        }

        private void btnTestRun_Click(object sender, EventArgs e)
        {
            //解析模型坐标

            if (txtModelN.TryParseNumber(errorProvider1, out var localY) == false)
            {
                txtModelN.Focus();
                return;
            }

            if (txtModelE.TryParseNumber(errorProvider1, out var localX) == false)
            {
                txtModelN.Focus();
                return;
            }

            if (txtModelH.TryParseNumber(errorProvider1, out var localZ) == false)
            {
                txtModelH.Focus();
                return;
            }

            //保存最后状态
            _LastState.Y = txtModelN.Text;
            _LastState.X = txtModelE.Text;
            _LastState.Z = txtModelH.Text;

            //更新转换参数
            if (cbUsed.Checked &&
                _Parameter.Offset != null &&
                _Parameter.Offset.Any(x => Math.Abs(x) > 1e-10))
            {
                _Parameter.OffsetType = ProjOffsetType._2D_Params4;
            }

            //试算模型坐标
            var dataModel = new double[] { localX, localY, localZ };
            var r = _Host.TestRun(_Parameter, dataModel, out var dataProjected, out var dataWorld);

            //显示投影坐标
            if (r == false || dataProjected == null)
            {
                txtProjectedN.Text = _NaN;
                txtProjectedE.Text = _NaN;
                txtProjectedH.Text = _NaN;
            }
            else
            {
                txtProjectedN.Text = dataProjected[1].ToMetreString();
                txtProjectedE.Text = dataProjected[0].ToMetreString();
                txtProjectedH.Text = dataProjected[2].ToMetreString();
            }

            //显示世界坐标
            if (r == false || dataWorld == null)
            {
                txtWorldLat.Text = _NaN;
                txtWorldLon.Text = _NaN;
                txtWorldHeight.Text = _NaN;
                // txtGeoidHeight.Text = _NaN;
            }
            else
            {
                txtWorldLon.Text = dataWorld[1].ToLatLonString();
                txtWorldLat.Text = dataWorld[0].ToLatLonString();
                txtWorldHeight.Text = dataWorld[2].ToMetreString();
                // txtGeoidHeight.Text = dataWorld[3].ToMetreString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //更新转换参数
            if (cbUsed.Checked &&
                _Parameter.Offset != null &&
                _Parameter.Offset.Any(x => Math.Abs(x) > 1e-10))
            {
                _Parameter.OffsetType = ProjOffsetType._2D_Params4;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void RefreshOffsetText()
        {
            txtProjCoordinateOffset.Text = _Parameter.GetOffsetString();
        }

        private void btnSwapNE_Click(object sender, EventArgs e)
        {
            var s = txtModelN.Text;
            txtModelN.Text = txtModelE.Text;
            txtModelE.Text = s;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            _Parameter = _ParameterOriginal.Clone();
            UpdateUI();
        }
    }
}
