using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Navisworks.Utility
{
    static class FormHelper
    {
        public static void ShowMessageBox(this Form form, string message)
        {
            MessageBox.Show(message, form.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 允许文本框接收拖入的文件路径
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultFileName"></param>
        public static void EnableFilePathDrop(this TextBox text, string defaultFileName)
        {
            if (text == null || text.AllowDrop) return;

            text.AllowDrop = true;
            text.DragDrop += (sender, e) =>
            {
                if (e.Data.TryParsePath(out var path))
                {
                    if (File.Exists(path))
                    {
                        text.Text = path;
                    }
                    else if (Directory.Exists(path))
                    {
                        var fileName = defaultFileName;
                        if (string.IsNullOrWhiteSpace(text.Text) == false)
                        {
                            var s = Path.GetFileName(text.Text);
                            if (string.IsNullOrWhiteSpace(s) == false)
                            {
                                fileName = s;
                            }
                        }
                        text.Text = Path.Combine(path, fileName);
                    }
                }
            };

            text.DragEnter += (sender, e) =>
            {
                if (e.Data.TryParsePath(out var path) && (File.Exists(path) || Directory.Exists(path)))
                {
                    e.Effect = DragDropEffects.Link;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            };
        }

        /// <summary>
        /// 允许文本框接收拖入的文件夹路径
        /// </summary>
        /// <param name="text"></param>
        public static void EnableFolderPathDrop(this TextBox text)
        {
            if (text == null || text.AllowDrop) return;

            text.AllowDrop = true;
            text.DragDrop += (sender, e) =>
            {
                if (e.Data.TryParsePath(out var path) && Directory.Exists(path))
                {
                    text.Text = path;
                }
            };

            text.DragEnter += (sender, e) =>
            {
                if (e.Data.TryParsePath(out var path) && Directory.Exists(path))
                {
                    e.Effect = DragDropEffects.Link;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            };
        }

        private static bool TryParsePath(this IDataObject data, out string path)
        {
            path = null;

            try
            {
                if (data == null || data.GetDataPresent(DataFormats.FileDrop) == false)
                {
                    return false;
                }

                path = ((System.Array)data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
