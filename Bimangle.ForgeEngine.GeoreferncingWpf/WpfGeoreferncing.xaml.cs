using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing.Models;
using Bimangle.ForgeEngine.Georeferncing.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Bimangle.ForgeEngine.Georeferncing
{
    /// <summary>
    /// WpfGeoreferncing.xaml 的交互逻辑
    /// </summary>
    public partial class WpfGeoreferncing : Window
    {
        private readonly IGeoreferncingHost _Host;
        private readonly GeoreferencedSetting _Setting;
        private readonly GeoreferencedSettingModel _Model;

        private int _NextTestId = 1;
        private bool _DialogResultSet;

        /// <summary>
        /// 对话框成功关闭后，调用方从此属性读取结果
        /// </summary>
        public GeoreferencedSetting Setting { get; private set; }

        public WpfGeoreferncing(IGeoreferncingHost host, GeoreferencedSetting setting)
            : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _Setting = _Host.CreateSuitedSetting(setting);

            _Model = new GeoreferencedSettingModel(_Setting, _Host);
            _Model.InitSelectedTabItem(Title, tabPageAuto, tabPageEnu, tabPageLocal, tabPageProj, GeoreferencedMode.Auto);

            DataContext = _Model;
        }

        public WpfGeoreferncing()
        {
            InitializeComponent();
        }

        private void WpfGeoreferncing_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitUI();
        }

        private void TabMain_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnTest.Visibility = Equals(tabMain.SelectedItem, tabPageProj)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void InitUI()
        {
            #region 初始化 窗口

            //切换标签时自动更新窗口标题
            SetBinding(TitleProperty, new Binding(nameof(_Model.Title))
            {
                Source = _Model,
                Mode = BindingMode.OneWay
            });

            #endregion

            #region 初始化 站心坐标
            {
                _Host.Adapter.SetDirectionLetters(lblEnuModelOriginE, lblEnuModelOriginN, lblEnuModelOriginH);
            }
            #endregion

            #region 初始化 暂不配准
            {
                _Host.Adapter.SetDirectionLetters(lblLocalModelOriginE, lblLocalModelOriginN, lblLocalModelOriginH);
            }
            #endregion

            #region 初始化 投影坐标

            #region 为投影定义文本框增加拖入文件的功能
            {
                var text = txtProjDefinition;
                text.AllowDrop = true;
                text.Drop += (sender, e) =>
                {
                    if (e.Data.TryParsePath(out var path))
                    {
                        _Model.Proj.UseProjFile(path);
                        e.Handled = true;
                    }
                };

                text.PreviewDragOver += (sender, e) =>
                {
                    if (e.Data.TryParsePath(out var path) && File.Exists(path))
                    {
                        if (_Model.Proj.CheckProjFile(path))
                        {
                            e.Effects = DragDropEffects.Link;
                            e.Handled = true;
                        }
                    }
                };
            }
            #endregion

            #region 注册 ProjSource 联动的 View 回调
            {
                _Model.Proj.RequestCreate = () =>
                {
                    var dialog = new WpfProjCreate(_Host) { Owner = this };
                    return dialog.ShowDialog() == true ? dialog.Definition : null;
                };

                _Model.Proj.RequestBrowse = () =>
                {
                    var dlg = new Microsoft.Win32.OpenFileDialog();
                    return dlg.ShowDialog(this) == true ? dlg.FileName : null;
                };

                _Model.Proj.RequestEdit = snapshot =>
                {
                    var dialog = new WpfTransformP4(_Host, snapshot) { Owner = this };
                    return dialog.ShowDialog() == true ? dialog.Parameter : null;
                };
            }
            #endregion

            #endregion

            // 键盘快捷键支持
            this.PreviewKeyDown += OnWindowKeyDown;
        }

        #region 键盘快捷键

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

        #endregion

        #region 按钮事件处理

        private void btnOK_OnClick(object sender, RoutedEventArgs e)
        {
            if (_Model.UpdateTo(_Setting, out var firstError))
            {
                Setting = _Setting;
                _DialogResultSet = true;
                DialogResult = true;
            }
            else
            {
                FocusFirstError(firstError);
            }
        }

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            //if (this.ShowConfirmBox(GeoStrings.ConfirmCancel))
            {
                _DialogResultSet = true;
                DialogResult = false;
            }
        }

        private void btnReset_OnClick(object sender, RoutedEventArgs e)
        {
            _Model.Reset();
        }

        private void btnTest_OnClick(object sender, RoutedEventArgs e)
        {
            // 先验证 Proj Tab 的参数，验证通过才打开测试窗口
            var tempProj = new ParameterProj();
            if (_Model.Proj.UpdateTo(tempProj, out var firstError) == false)
            {
                FocusFirstError(firstError);
                return;
            }

            var dialog = new WpfGeoreferncingTest(_Host, tempProj, _NextTestId++);
            dialog.Left = Left + ActualWidth;
            dialog.Top = Top;
            dialog.Show();
        }

        private void btnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            _Model.Proj.EditOffset();
        }

        private void btnProjDefinitionSave_OnClick(object sender, RoutedEventArgs e)
        {
            var filePath = _Host.GetDefaultProjFilePath();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                btnProjDefinitionSave.IsEnabled = false;
                return;
            }

            if (_Model.Proj.TryGetProjDefinitionWkt(out var wkt) == false)
            {
                txtProjDefinition.Focus();
                return;
            }

            var filePathInfo = $@" {GeoStrings.FilePath}: {filePath}";
            if (!this.ShowConfirmBox(GeoStrings.ConfirmSaveProjDefinition + filePathInfo)) return;
            if (File.Exists(filePath) && !this.ShowConfirmBox(GeoStrings.ConfirmOverwriteFile + filePathInfo)) return;

            if (_Host.SaveProjFile(filePath, wkt))
            {
                this.ShowMessageBox(GeoStrings.SaveSuccessfully);
            }
        }

        private void btnProjCoordinateOffsetSave_OnClick(object sender, RoutedEventArgs e)
        {
            var filePath = _Host.GetDefaultOffsetFilePath();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                btnProjCoordinateOffsetSave.IsEnabled = false;
                return;
            }

            var filePathInfo = $@" {GeoStrings.FilePath}: {filePath}";
            if (!this.ShowConfirmBox(GeoStrings.ConfirmSaveOffsets + filePathInfo)) return;
            if (File.Exists(filePath) && !this.ShowConfirmBox(GeoStrings.ConfirmOverwriteFile + filePathInfo)) return;

            var offsetType = _Model.Proj.GetLocalOffsetType();
            var offset = _Model.Proj.GetLocalOffsetArray();
            if (_Host.SaveOffsetFile(filePath, offsetType, offset))
            {
                this.ShowMessageBox(GeoStrings.SaveSuccessfully);
            }
        }

        #endregion

        #region 文本框事件处理

        private void txtProjDefinition_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_Model.Proj.IsProjDefinitionReadOnly)
            {
                // 双击只读文本框时，切换到"自定义"模式
                var customItem = _Model.Proj.ProjSourceItems.FirstOrDefault(x => x.SourceType == ProjSourceType.Custom);
                if (customItem != null)
                {
                    _Model.Proj.ProjSource = customItem;
                }
            }
        }

        #endregion

        #region ComboBox 事件处理

        private void cbVerticalGeoidGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbVerticalGeoidGrid.SelectedItem != _Model.Proj.VerticalGeoidGrid)
            {
                cbVerticalGeoidGrid.SelectedItem = _Model.Proj.VerticalGeoidGrid;
            }
        }

        #endregion

        #region Window Closing

        private void WpfGeoreferncing_OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_DialogResultSet) return;

            //// 用户通过 X 按钮关闭时，等同于取消
            //if (!this.ShowConfirmBox(GeoStrings.ConfirmCancel))
            //{
            //    e.Cancel = true;
            //}
        }

        #endregion

        #region 辅助方法

        private void FocusFirstError(string propertyName)
        {
            if (propertyName == null) return;

            // 根据当前标签页确定控件名称前缀
            string prefix = null;
            if (Equals(tabMain.SelectedItem, tabPageEnu))
                prefix = "Enu";
            else if (Equals(tabMain.SelectedItem, tabPageLocal))
                prefix = "Local";
            else if (Equals(tabMain.SelectedItem, tabPageProj))
                prefix = "Proj";

            // 使用智能查找聚焦到控件
            this.FocusControlByPropertyName(propertyName, prefix);
        }

        #endregion
    }
}

