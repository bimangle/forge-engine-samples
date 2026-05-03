using System;
using System.Windows;
using System.Windows.Input;
using Bimangle.ForgeEngine.Georeferncing.Models;
using Bimangle.ForgeEngine.Georeferncing.Utility;

namespace Bimangle.ForgeEngine.Georeferncing
{
    /// <summary>
    /// WpfProjParamCalc.xaml 的交互逻辑
    /// </summary>
    public partial class WpfProjParamCalc : Window
    {
        private static State _LastState;

        private readonly IGeoreferncingHost _Host;
        private readonly ProjParamCalcModel _Model;

        public double LocalX { get; private set; }
        public double LocalY { get; private set; }
        public double Lat { get; private set; }
        public double Lon { get; private set; }

        public WpfProjParamCalc(IGeoreferncingHost host)
            : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));

            _Model = new ProjParamCalcModel(_Host);

            // 从上次状态恢复
            if (_LastState != null)
            {
                _Model.RefPointLocalX = _LastState.RefPointLocalX;
                _Model.RefPointLocalY = _LastState.RefPointLocalY;
                _Model.RefPointGeoLat = _LastState.RefPointGeoLat;
                _Model.RefPointGeoLon = _LastState.RefPointGeoLon;
            }

            DataContext = _Model;
        }

        public WpfProjParamCalc()
        {
            InitializeComponent();
        }

        private void WpfProjParamCalc_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitUI();
        }

        private void InitUI()
        {
            // 设置方向字母
            _Host.Adapter.SetDirectionLetters(lblRefPointLocalX, lblRefPointLocalY, null);

            // 注册回调
            _Model.RequestPickPosition = () =>
            {
                // 显示拾取坐标对话框（启动外部浏览器）
                // 坐标数据会通过专有格式的剪贴板在 Activated 事件中处理
                _Host.ShowPickPositionDialog();
            };

            // 键盘快捷键支持
            this.PreviewKeyDown += OnWindowKeyDown;
        }

        private void WpfProjParamCalc_OnActivated(object sender, EventArgs e)
        {
            // 处理专有格式剪贴板数据（地理坐标）
            const string FORMAT = "BimAngle/GeographicPosition";

            var data = Clipboard.GetData(FORMAT);
            if (data != null)
            {
                Clipboard.Clear();  // 清除避免重复

                var s = data.ToString();
                var values = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length >= 2)
                {
                    if (double.TryParse(values[0], out var lat) &&
                        double.TryParse(values[1], out var lon))
                    {
                        _Model.RefPointGeoLat = lat.ToLatLonString();
                        _Model.RefPointGeoLon = lon.ToLatLonString();
                    }
                }
            }

            // 处理纯文本剪贴板数据（模型坐标）
            var text = Clipboard.GetText();
            if (!string.IsNullOrEmpty(text))
            {
                var values = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length >= 2)
                {
                    if (double.TryParse(values[0], out var east) &&
                        double.TryParse(values[1], out var north))
                    {
                        _Model.RefPointLocalX = east.ToMetreString();
                        _Model.RefPointLocalY = north.ToMetreString();
                    }
                }
            }
        }

        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !e.Handled)
            {
                if (btnOK.IsEnabled && btnOK.IsVisible)
                {
                    e.Handled = true;
                    btnOK_OnClick(btnOK, new RoutedEventArgs());
                }
            }
            else if (e.Key == Key.Escape && !e.Handled)
            {
                e.Handled = true;
                btnCancel_OnClick(btnCancel, new RoutedEventArgs());
            }
        }

        private void btnPickPosition_OnClick(object sender, RoutedEventArgs e)
        {
            _Model.PickPosition();
        }

        private void btnOK_OnClick(object sender, RoutedEventArgs e)
        {
            if (_Model.ValidateAndGetResult(out var localX, out var localY, 
                                             out var lat, out var lon, 
                                             out var firstError))
            {
                LocalX = localX;
                LocalY = localY;
                Lat = lat;
                Lon = lon;

                // 保存状态
                _LastState = new State
                {
                    RefPointLocalX = _Model.RefPointLocalX,
                    RefPointLocalY = _Model.RefPointLocalY,
                    RefPointGeoLat = _Model.RefPointGeoLat,
                    RefPointGeoLon = _Model.RefPointGeoLon
                };

                DialogResult = true;
                Close();
            }
            // 设计决策：字段少，验证错误时用户可直接看到，无需聚焦
        }

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private class State
        {
            public string RefPointLocalX { get; set; }
            public string RefPointLocalY { get; set; }
            public string RefPointGeoLat { get; set; }
            public string RefPointGeoLon { get; set; }
        }
    }
}
