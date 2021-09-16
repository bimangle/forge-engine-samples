using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bentley.GeometryNET;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Dgn.Georeferncing;
using Bimangle.ForgeEngine.Dgn.Utility;

#if DEBUG
using FormBase = System.Windows.Forms.Form;
#else
using FormBase = Bentley.MstnPlatformNET.WinForms.Adapter;
#endif

namespace Bimangle.ForgeEngine.Dgn.Toolset.PickPosition
{
    public partial class FormPickPosition : FormBase
    {
        private ToolPluginPickPoint _PickPoint;
        private DPoint3d? _Point;
        private double _Upm = 1.0;

        public FormPickPosition()
        {
            InitializeComponent();
        }

#if DEBUG
        public void AttachAsTopLevelForm(
            Bentley.ResourceManagement.IMatchLifetime addin,
            bool useMicroStationPositioning)
        {
        }
#endif

        private void DisposeTool()
        {
            if (_PickPoint != null)
            {
                _PickPoint.Init(null);
                _PickPoint.Exit();
                _PickPoint = null;
            }
        }

        private void FormPickPosition_Load(object sender, EventArgs e)
        {
            #region 初始化原点下拉框

            {
                var items = new List<ItemValue<OriginType>>
                {
                    new ItemValue<OriginType>(GeoStrings.OriginTypeInternal, OriginType.Internal),
                };
                cbOrigin.Items.Clear();
                cbOrigin.Items.AddRange(items.OfType<object>().ToArray());
                cbOrigin.SelectedIndex = 0;
            }

            #endregion

            #region 初始化计量单位下拉框
            {
                var items = new List<ItemValue<int>>
                {
                    new ItemValue<int>(GeoStrings.UnitMetre, 0),
                    new ItemValue<int>(GeoStrings.UnitFeet, 1),
                };
                cbUnit.Items.Clear();
                cbUnit.Items.AddRange(items.OfType<object>().ToArray());
                cbUnit.SelectedIndex = 0;
            }
            #endregion

            #region 初始化坐标文本框
            {
                txtX.Text = string.Empty;
                txtY.Text = string.Empty;
                txtZ.Text = string.Empty;
            }
            #endregion

            UpdateUI();
        }

        private void FormPickPosition_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FormPickPosition_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeTool();
        }

        private void OnPickPositionCallback(DPoint3d? p, double upm)
        {
            if (InvokeRequired)
            {
                Action<DPoint3d?, double> m = OnPickPositionCallback;
                Invoke(m, p, upm);
                return;
            }

            if (p.HasValue == false)
            {
                DisposeTool();

                btnPick.Enabled = true;
            }
            else
            {
                _Point = new DPoint3d(p.Value.X, p.Value.Y, p.Value.Z);
                _Upm = upm;

                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (InvokeRequired)
            {
                MethodInvoker m = UpdateUI;
                Invoke(m);
                return;
            }

            if (_Point == null)
            {
                btnOK.Enabled = false;
            }
            else
            {
                var originType = cbOrigin.GetSelectedValue<OriginType>();
                switch (originType)
                {
                    case OriginType.Internal:
                    {
                        //do nothing
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var unitType = cbUnit.GetSelectedValue<int>();
                double scaleFactor;
                switch (unitType)
                {
                    case 0: //米
                        scaleFactor = 1.0;
                        break;
                    case 1: //英尺
                        scaleFactor = 0.3048;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var vv = _Point.Value * (scaleFactor / _Upm);

                const string FORMAT = @"0.######";
                txtX.Text = vv.X.ToString(FORMAT);
                txtY.Text = vv.Y.ToString(FORMAT);
                txtZ.Text = vv.Z.ToString(FORMAT);

                btnOK.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //const string FORMAT = @"BimAngle/RevitPosition";
            var data = $@"{txtX.Text},{txtY.Text},{txtZ.Text}";
            //Clipboard.SetData(FORMAT, data);
            Clipboard.SetText(data);
            Close();
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            _PickPoint = ToolPluginPickPoint.InstallNewInstance(OnPickPositionCallback);
            btnPick.Enabled = false;
        }


        private void cbOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void cbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
