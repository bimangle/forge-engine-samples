using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.Helpers;
using Bimangle.ForgeEngine.Revit.Utility;
using Form = System.Windows.Forms.Form;
using InvalidOperationException = Autodesk.Revit.Exceptions.InvalidOperationException;
using RevitUI = Autodesk.Revit.UI;

namespace Bimangle.ForgeEngine.Revit.Toolset.PickPosition
{
    public partial class FormPickPosition : Form
    {
        private XYZ _Point;
        private ExternalEvent _PickPosition;
        private readonly ExternalCommandData _Command;

        public FormPickPosition(ExternalCommandData command) : this()
        {
            _Command = command;
        }

        public FormPickPosition()
        {
            InitializeComponent();
        }

        public void InitExternalEvent()
        {
            if (_PickPosition == null)
            {
                var handler = new PickPosition();
                handler.SetCallback(OnPickPositionCallback);

                _PickPosition = ExternalEvent.Create(handler);
            }
        }

        private void FormPickPosition_Load(object sender, EventArgs e)
        {
            #region 初始化原点下拉框
            {
                var adapter = new GeoreferncingAdapter(_Command.View.Document, null);
                var originTypes = adapter.GetSupportOriginTypes();
                var items = new List<ItemValue<OriginType>>(originTypes.Length);
                foreach (var originType in originTypes)
                {
                    items.Add(new ItemValue<OriginType>(adapter.GetLocalString(originType), originType));
                }
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
            _PickPosition?.Dispose();
            _PickPosition = null;
        }

        private void OnPickPositionCallback(XYZ p)
        {
            if (InvokeRequired)
            {
                Action<XYZ> m = OnPickPositionCallback;
                Invoke(m);
                return;
            }

            _Point = p == null ? null : new XYZ(p.X, p.Y, p.Z);

            UpdateUI();

            btnPick.Enabled = true;
            Show(_Command.GetMainWindowHandle());
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
                var document = _Command.View.Document;
                var point = new XYZ(_Point.X, _Point.Y, _Point.Z);
                var originType = cbOrigin.GetSelectedValue<OriginType>();
                switch (originType)
                {
                    case OriginType.Internal:
                    {
                        //do nothing
                        break;
                    }
                    case OriginType.Project:
                    {
                        //得到转换矩阵: 项目坐标 => 内部坐标
                        var transform = document.GetPrjLocation()?.GetTransform() ?? 
                                        Transform.Identity;
                        point = transform.Inverse.OfPoint(point);
                        break;
                    }
                    case OriginType.Shared:
                    {
                        //得到转换矩阵: 共享坐标 => 内部坐标
                        var transform = document.ActiveProjectLocation?.GetTransform() ?? 
                                        Transform.Identity;
                        point = transform.Inverse.OfPoint(point);
                        break;
                    }
                    case OriginType.Survey:
                    {
                        //得到转换矩阵: 共享坐标 => 内部坐标
                        var transform = document.ActiveProjectLocation?.GetTransform() ??
                                        Transform.Identity;

                        //得到测量点的内部坐标
                        var surveyPoint = document.GetSurveyPointLocation();
                        
                        //得到测量点在共享坐标系中的坐标
                        var surveyPointForShared = transform.Inverse.OfPoint(surveyPoint);

                        //偏移修正
                        var surveyPointOffset = Transform.CreateTranslation(-surveyPointForShared);

                        //最终坐标: 先将内部坐标转换为共享坐标，然后共享坐标减去测量点在共享坐标系中偏移
                        var t = surveyPointOffset * transform.Inverse;
                        point = t.OfPoint(point);

                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var unitType = cbUnit.GetSelectedValue<int>();
                if (unitType == 0)
                {
                    //如果选择米，则执行转换
                    var x = GetMeter(point.X);
                    var y = GetMeter(point.Y);
                    var z = GetMeter(point.Z);
                    point = new XYZ(x, y, z);
                }

                const string FORMAT = @"0.########";
                txtX.Text = point.X.ToString(FORMAT);
                txtY.Text = point.Y.ToString(FORMAT);
                txtZ.Text = point.Z.ToString(FORMAT);

                btnOK.Enabled = true;
            }
        }

        private double GetMeter(double feet)
        {
#if R2014 || R2015 || R2016 || R2017 || R2018 || R2019 || R2020
            return UnitUtils.Convert(feet, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_METERS);
#else
            return UnitUtils.Convert(feet, UnitTypeId.Feet, UnitTypeId.Meters);
#endif
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
            _PickPosition.Raise();
            btnPick.Enabled = false;
            Hide();
        }

        private void cbOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void cbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        public class PickPosition : IExternalEventHandler
        {
            private Action<XYZ> _Callback;

            public PickPosition()
            {
            }

            public void SetCallback(Action<XYZ> callback)
            {
                _Callback = callback;
            }

            #region Implementation of IExternalEventHandler

            public void Execute(UIApplication app)
            {
                try
                {
                    var snapType = (ObjectSnapTypes) 0xffff;
                    var point = app.ActiveUIDocument.Selection.PickPoint(snapType,
                        Strings.ToolsetPickPositionPrompt);
                    _Callback?.Invoke(point);
                }
                catch (OperationCanceledException)
                {
                    _Callback?.Invoke(null);
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    _Callback?.Invoke(null);
                }
                catch (InvalidOperationException ex)
                {
                    Debug.WriteLine(ex.ToString());
                    RevitUI.TaskDialog.Show(@"BimAngle", ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    RevitUI.TaskDialog.Show(@"BimAngle", ex.ToString());
                    _Callback?.Invoke(null);
                }
            }

            public string GetName()
            {
                return @"PickPosition";
            }

            #endregion
        }

    }
}
