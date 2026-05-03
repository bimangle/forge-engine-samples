using System;
using System.Windows;
using System.Windows.Input;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing.Models;
using Bimangle.ForgeEngine.Georeferncing.Utility;

namespace Bimangle.ForgeEngine.Georeferncing
{
    /// <summary>
    /// WpfTransformP4.xaml 的交互逻辑
    /// </summary>
    public partial class WpfTransformP4 : Window
    {
        private readonly IGeoreferncingHost _Host;
        private readonly ParameterProj _Parameter;
        private readonly TransformP4Model _Model;

        //private bool _DialogResultSet;

        /// <summary>
        /// 对话框成功关闭后，调用方从此属性读取结果
        /// </summary>
        public ParameterProj Parameter => _Parameter;

        public WpfTransformP4(IGeoreferncingHost host, ParameterProj parameter)
            : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));

            _Model = new TransformP4Model(_Host, _Parameter);

            DataContext = _Model;
        }

        public WpfTransformP4()
        {
            InitializeComponent();
        }

        private void WpfTransformP4_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitUI();
        }

        private void InitUI()
        {
            // 设置方向字母
            _Host.Adapter.SetDirectionLetters(lblModelE, lblModelN, lblModelH);

            // 键盘快捷键支持
            this.PreviewKeyDown += OnWindowKeyDown;
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

        private void btnTestRun_OnClick(object sender, RoutedEventArgs e)
        {
            _Model.ExecuteTest();
        }

        private void btnSwapNE_OnClick(object sender, RoutedEventArgs e)
        {
            _Model.SwapNE();
        }

        private void btnRestore_OnClick(object sender, RoutedEventArgs e)
        {
            _Model.Restore();
        }

        private void btnOK_OnClick(object sender, RoutedEventArgs e)
        {
            if (_Model.UpdateTo(_Parameter, out var firstError))
            {
                //_DialogResultSet = true;
                DialogResult = true; // 自动关闭窗口
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
    }
}
