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
    partial class FormGeoreferncingTest : Form
    {
        private readonly string _NaN = double.NaN.ToString(CultureInfo.InvariantCulture);

        private readonly TestRunState _LastState = TestRunState.LastState;

        private readonly IGeoreferncingHost _Host;
        private readonly ParameterProj _Parameters;
        private readonly int _Id;

        public FormGeoreferncingTest(IGeoreferncingHost host, ParameterProj parameters, int id) : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _Id = id;
        }

        public FormGeoreferncingTest()
        {
            InitializeComponent();
        }

        private void FormGeoreferncingTest_Load(object sender, EventArgs e)
        {
            Text = $@"#{_Id} {Text}";

            InitUI();
        }

        private void InitUI()
        {
            #region 初始化投影坐标参数
            {
                txtProjDefinition.Text = _Parameters.Definition ?? string.Empty;
                txtProjectionHeight.Text = _Parameters.ProjectionHeight.ToMetreString();
                txtProjCoordinateOffset.Text = _Parameters.GetOffsetString();

                var geoidGrids = _Host.GetVerticalGeoidGridItems();
                var geoidGrid = geoidGrids.FirstOrDefault(x=>x.FileName == _Parameters.GeoidGrid) ?? 
                                geoidGrids.FirstOrDefault();
                txtGeoidGrid.Text = geoidGrid?.ToString() ?? string.Empty;
                txtGeoidConstantOffset.Text = _Parameters.GeoidConstantOffset.ToMetreString();

                //解决大地水准面网格过长无法显示完整的问题
                this.toolTip1.SetToolTip(txtGeoidGrid, txtGeoidGrid.Text);
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
                txtGeoidHeight.Text = _NaN;
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

            //试算模型坐标
            var dataModel = new double[] { localX, localY, localZ };
            var r = _Host.TestRun(_Parameters, dataModel, out var dataProjected, out var dataWorld);

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
                txtGeoidHeight.Text = _NaN;
            }
            else
            {
                txtWorldLon.Text = dataWorld[1].ToLatLonString();
                txtWorldLat.Text = dataWorld[0].ToLatLonString();
                txtWorldHeight.Text = dataWorld[2].ToMetreString();
                txtGeoidHeight.Text = dataWorld[3].ToMetreString();
            }
        }

        private void btnSwapNE_Click(object sender, EventArgs e)
        {
            var s = txtModelE.Text;
            txtModelE.Text = txtModelN.Text;
            txtModelN.Text = s;
        }
    }
}
