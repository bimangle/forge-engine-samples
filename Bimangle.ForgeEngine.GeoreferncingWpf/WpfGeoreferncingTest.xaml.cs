using System;
using System.Windows;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing.Models;

namespace Bimangle.ForgeEngine.Georeferncing
{
    /// <summary>
    /// WpfGeoreferncingTest.xaml 的交互逻辑
    /// </summary>
    public partial class WpfGeoreferncingTest : Window
    {
        private readonly IGeoreferncingHost _Host;
        private readonly ParameterProj _Parameters;
        private readonly int _Id;
        private readonly GeoreferncingTestModel _Model;

        public WpfGeoreferncingTest(IGeoreferncingHost host, ParameterProj parameters, int id)
            : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _Id = id;

            _Model = new GeoreferncingTestModel(_Host, _Parameters);

            DataContext = _Model;
        }

        public WpfGeoreferncingTest()
        {
            InitializeComponent();
        }

        private void WpfGeoreferncingTest_OnLoaded(object sender, RoutedEventArgs e)
        {
            // 设置窗口序号
            Title = $"#{_Id} {Title}";

            InitUI();
        }

        private void InitUI()
        {
            if (_Host == null)
            {
                return;
            }

            // 设置方向字母
            _Host.Adapter.SetDirectionLetters(lblModelE, lblModelN, lblModelH);

            // 设置 GeoidGrid 的 ToolTip（长文本支持）
            if (!string.IsNullOrEmpty(txtGeoidGrid.Text))
            {
                txtGeoidGrid.ToolTip = txtGeoidGrid.Text;
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
    }
}
