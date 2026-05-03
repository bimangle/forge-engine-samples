using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Bimangle.ForgeEngine.Georeferncing.Utility
{
    public static class WpfHelper
    {
        public static bool TryParsePath(this IDataObject data, out string path)
        {
            path = null;

            try
            {
                if (data == null || data.GetDataPresent(DataFormats.FileDrop) == false)
                {
                    return false;
                }

                var files = data.GetData(DataFormats.FileDrop) as string[];
                if (files == null || files.Length == 0)
                {
                    return false;
                }

                path = files[0];
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        public static bool ShowDialog(this Window window, System.Windows.Forms.IWin32Window owner)
        {
            if (owner != null)
            {
                var helper = new System.Windows.Interop.WindowInteropHelper(window);
                helper.Owner = owner.Handle;

                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            return window.ShowDialog() == true;
        }

        /// <summary>
        /// 显示消息对话框
        /// </summary>
        public static void ShowMessageBox(this Window window, string message)
        {
            MessageBox.Show(
                window,
                message,
                window?.Title ?? string.Empty,
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        /// <summary>
        /// 显示确认对话框
        /// </summary>
        public static bool ShowConfirmBox(this Window window, string message)
        {
            return MessageBox.Show(
                window,
                message,
                window?.Title ?? string.Empty,
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question,
                MessageBoxResult.Cancel) == MessageBoxResult.OK;
        }

        /// <summary>
        /// 允许文本框接收拖入的文件路径
        /// </summary>
        public static void EnableFilePathDrop(this System.Windows.Controls.TextBox textBox, string defaultFileName)
        {
            if (textBox == null || textBox.AllowDrop) return;

            textBox.AllowDrop = true;
            textBox.Drop += (sender, e) =>
            {
                if (e.Data.TryParsePath(out var path))
                {
                    if (File.Exists(path))
                    {
                        textBox.Text = path;
                    }
                    else if (Directory.Exists(path))
                    {
                        var fileName = defaultFileName;
                        if (string.IsNullOrWhiteSpace(textBox.Text) == false)
                        {
                            var s = Path.GetFileName(textBox.Text);
                            if (string.IsNullOrWhiteSpace(s) == false)
                            {
                                fileName = s;
                            }
                        }
                        textBox.Text = Path.Combine(path, fileName);
                    }
                }
            };

            textBox.PreviewDragOver += (sender, e) =>
            {
                if (e.Data.TryParsePath(out var path) && (File.Exists(path) || Directory.Exists(path)))
                {
                    e.Effects = DragDropEffects.Link;
                    e.Handled = true;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                    e.Handled = true;
                }
            };
        }

        /// <summary>
        /// 智能查找并聚焦到指定属性对应的控件（基于命名约定）
        /// </summary>
        /// <param name="window">窗口对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="prefix">控件名称前缀（如 "Enu", "Local", "Proj"）</param>
        /// <returns>是否成功找到并聚焦控件</returns>
        public static bool FocusControlByPropertyName(this Window window, string propertyName, string prefix = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return false;

            // 尝试多种命名模式
            var controlNames = new List<string>();

            // 模式1: txt + 前缀 + 属性名（如 txtEnuLatitude）
            if (!string.IsNullOrEmpty(prefix))
            {
                controlNames.Add($"txt{prefix}{propertyName}");
            }

            // 模式2: txt + 属性名（如 txtLatitude, txtProjDefinition）
            controlNames.Add($"txt{propertyName}");

            // 尝试查找控件
            foreach (var controlName in controlNames)
            {
                var control = window.FindName(controlName) as FrameworkElement;
                if (control != null)
                {
                    control.Focus();
                    Keyboard.Focus(control);

                    // 如果是 TextBox，选中全部文本
                    if (control is System.Windows.Controls.TextBox textBox)
                    {
                        textBox.SelectAll();
                    }

                    return true;
                }
            }

            return false;
        }
    }

    public class ItemValue<T>
    {
        public string Text { get; }
        public T Value { get; }

        public ItemValue(string text, T value)
        {
            Text = text;
            Value = value;
        }

        #region Overrides of Object

        public override string ToString()
        {
            return Text;
        }

        #endregion
    }
}
