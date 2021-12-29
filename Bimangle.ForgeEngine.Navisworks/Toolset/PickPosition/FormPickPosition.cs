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
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing;
using Bimangle.ForgeEngine.Navisworks.Utility;
using Application = Autodesk.Navisworks.Api.Application;
using NwVector3D = Autodesk.Navisworks.Api.Vector3D;
using BaGeometry = Bimangle.ForgeEngine.Types.Geometry;
using BaVector3D = Bimangle.ForgeEngine.Types.Geometry.Vector3D;


namespace Bimangle.ForgeEngine.Navisworks.Toolset.PickPosition
{
    public partial class FormPickPosition : Form
    {
        private BaGeometry.Transform _Transform;
        private IPickPoint _PickPoint;
        private Point3D _Point;
        private Units _Units;

        public FormPickPosition()
        {
            InitializeComponent();
        }

        private void FormPickPosition_Load(object sender, EventArgs e)
        {
            var doc = Application.ActiveDocument;

            #region 初始化原点下拉框
            {
                var items = new List<ItemValue<OriginType>>
                {
                    new ItemValue<OriginType>(Strings.OriginTypeInternal, OriginType.Internal),
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
                    new ItemValue<int>(GeoreferncingHelper.GetUnitString(0), 0),
                    new ItemValue<int>(GeoreferncingHelper.GetUnitString(1), 1),
                };
                cbUnit.Items.Clear();
                cbUnit.Items.AddRange(items.OfType<object>().ToArray());
                cbUnit.SelectedIndex = 0;
            }
            #endregion

            #region 初始化坐标文本框
            {
                lblX.SetDirectionLetter(doc.RightVector, @"X");
                lblY.SetDirectionLetter(doc.FrontVector, @"Y");
                lblZ.SetDirectionLetter(doc.UpVector, @"Z");

                txtX.Text = string.Empty;
                txtY.Text = string.Empty;
                txtZ.Text = string.Empty;
            }
            #endregion

            #region 初始化坐标转换矩阵
            {
                //上方向旋转为 +Z
                var rotationUp = new BaGeometry.QuaternionD().SetFromUnitVectors(ConvertTo(doc.UpVector), new BaVector3D(0, 0, 1));

                //前方向旋转为 +Y
                var vectorFront = ConvertTo(doc.FrontVector).ApplyQuaternion(rotationUp);
                var rotationFront = new BaGeometry.QuaternionD().SetFromUnitVectors(vectorFront, new BaVector3D(0, 1, 0));

                var rotation = new BaGeometry.Matrix4D();
                rotation.MakeRotationFromQuaternion(rotationFront.Multiply(rotationUp));

                _Transform = BaGeometry.Transform.GetAffineMatrix(rotation, new BaVector3D());
            }
            #endregion

            UpdateUI();
        }

        private void FormPickPosition_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FormPickPosition_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.MainDocument.Tool.Changed -= Tool_Changed;

            if (_PickPoint != null)
            {
                _PickPoint?.Init(null);
                _PickPoint = null;

                Application.MainDocument.Tool.Value = Tool.Select;
            }
        }

        private void OnPickPositionCallback(Point3D p, Units units)
        {
            if (InvokeRequired)
            {
                Action<Point3D, Units> m = OnPickPositionCallback;
                Invoke(m, p, units);
                return;
            }

            if (p == null)
            {
                _PickPoint?.Init(null);
                _PickPoint = null;

                Application.MainDocument.Tool.Changed -= Tool_Changed;
                Application.MainDocument.Tool.Value = Tool.Select;

                btnPick.Enabled = true;
            }
            else
            {
                _Point = new Point3D(p.X, p.Y, p.Z);
                _Units = units;

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
                        scaleFactor = UnitConversion.ScaleFactor(_Units, Units.Meters);
                        break;
                    case 1: //英尺
                        scaleFactor = UnitConversion.ScaleFactor(_Units, Units.Feet);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var v = _Point.ToVector3D() * scaleFactor;
                var vv = _Transform.OfPoint(ConvertTo(v));

                const string FORMAT = @"0.########";
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
            var toolPluginRecord = Application.Plugins.FindPlugin($@"Engine_PickPoint.{Command.DEVELOPER_ID}") as ToolPluginRecord;
            if (toolPluginRecord == null) return;

            var toolPlugin = toolPluginRecord.IsLoaded
                ? toolPluginRecord.LoadedPlugin
                : toolPluginRecord.LoadPlugin();
            if (toolPlugin is IPickPoint pickPoint)
            {
                _PickPoint = pickPoint;
                _PickPoint.Init(OnPickPositionCallback);

                Application.MainDocument.Tool.SetCustomToolPlugin(toolPlugin);
                Application.MainDocument.Tool.Changed += Tool_Changed;

                btnPick.Enabled = false;
            }
        }

        private void Tool_Changed(object sender, EventArgs e)
        {
            Application.MainDocument.Tool.Changed -= Tool_Changed;
            OnPickPositionCallback(null, Units.Meters);
        }

        private void cbOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void cbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private BaVector3D ConvertTo(NwVector3D src)
        {
            return new BaVector3D(src.X, src.Y, src.Z);
        }

    }
}
