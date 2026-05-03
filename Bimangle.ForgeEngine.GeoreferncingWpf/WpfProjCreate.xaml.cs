using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Bimangle.ForgeEngine.Georeferncing.Models;
using Bimangle.ForgeEngine.Georeferncing.Utility;

namespace Bimangle.ForgeEngine.Georeferncing
{
    /// <summary>
    /// WpfProjCreate.xaml 的交互逻辑
    /// </summary>
    public partial class WpfProjCreate : Window
    {
        private static State _LastState;

        private readonly IGeoreferncingHost _Host;
        private readonly ProjCreateModel _Model;

        public string Definition { get; private set; }

        public WpfProjCreate(IGeoreferncingHost host)
            : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));

            _Model = new ProjCreateModel(_Host);

            // 从上次状态恢复
            if (_LastState != null)
            {
                _Model.Gcs = _LastState.Gcs;
                _Model.CentralMeridian = _LastState.CentralMeridian;
                _Model.FalseEasting = _LastState.FalseEasting;
                _Model.FalseNorthing = _LastState.FalseNorthing;
                _Model.Definition = _LastState.Definition;
                _Model.IsDefinitionGenerated = !string.IsNullOrWhiteSpace(_LastState.Definition);
            }

            DataContext = _Model;
        }

        public WpfProjCreate()
        {
            InitializeComponent();
        }

        private void WpfProjCreate_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitUI();
        }

        private void InitUI()
        {
            // 注册回调
            _Model.RequestCalcByRefPoint = (centralMeridian, unused1, unused2, unused3) =>
            {
                var dialog = new WpfProjParamCalc(_Host) { Owner = this };
                if (dialog.ShowDialog() == true)
                {
                    var validator = _Host.GetProjValidator();
                    var proj = validator.CreateProj(
                        _Model.Gcs, 
                        _Model.IsPinned ? (double?)centralMeridian : null, 
                        dialog.LocalX, 
                        dialog.LocalY, 
                        dialog.Lon, 
                        dialog.Lat);

                    if (proj != null)
                    {
                        _Model.CentralMeridian = proj.CentralMeridian.ToLatLonString();
                        _Model.FalseEasting = proj.FalseEasting.ToMetreString();
                        _Model.FalseNorthing = proj.FalseNorthing.ToMetreString();
                        return true;
                    }
                }
                return false;
            };

            // 键盘快捷键支持
            this.PreviewKeyDown += OnWindowKeyDown;
        }

        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !e.Handled)
            {
                if (Keyboard.FocusedElement is TextBox textBox && textBox.AcceptsReturn)
                {
                    return;
                }

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

        private void btnCalc_OnClick(object sender, RoutedEventArgs e)
        {
            _Model.CalcByRefPoint();
        }

        private void btnGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            _Model.GenerateDefinition();
        }

        private void txtDefinition_GotFocus(object sender, RoutedEventArgs e)
        {
            // 当定义文本框获得焦点时，清除灰色前景（如果有）
            if (txtDefinition.Foreground == Brushes.Gray)
            {
                txtDefinition.Foreground = SystemColors.WindowTextBrush;
            }
        }

        private void btnOK_OnClick(object sender, RoutedEventArgs e)
        {
            if (_Model.ValidateAndGetResult(out var definition, out var firstError))
            {
                Definition = definition;

                // 保存状态
                _LastState = new State
                {
                    Gcs = _Model.Gcs,
                    CentralMeridian = _Model.CentralMeridian,
                    FalseEasting = _Model.FalseEasting,
                    FalseNorthing = _Model.FalseNorthing,
                    Definition = _Model.Definition
                };

                DialogResult = true;
                Close();
            }
            else
            {
                FocusFirstError(firstError);
            }
        }

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void FocusFirstError(string propertyName)
        {
            // 使用智能查找聚焦到控件（无需前缀，因为控件名直接是 txt + 属性名）
            this.FocusControlByPropertyName(propertyName);
        }

        private class State
        {
            public ProjBuilder.GeoGCS Gcs { get; set; }
            public string CentralMeridian { get; set; }
            public string FalseEasting { get; set; }
            public string FalseNorthing { get; set; }
            public string Definition { get; set; }
        }
    }
}
