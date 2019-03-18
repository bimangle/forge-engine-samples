using System;
using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Dwg.CLI.Utility
{
    static class FormHelper
    {
        public static void ShowMessageBox(this Form form, string message)
        {
            MessageBox.Show(message, form.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static Control[] ToArray(params Control[] controls)
        {
            return controls;
        }

        /// <summary>
        /// 批量增加控件事件处理
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="handler"></param>
        public static void AddEventListener(this Control[] controls, Action handler)
        {
            void OnEvent(object sender, EventArgs e)
            {
                handler();
            }

            foreach (var control in controls)
            {
                switch (control)
                {
                    case TextBox textBox:
                        textBox.TextChanged += OnEvent;
                        break;
                    case ComboBox comboBox:
                        comboBox.SelectedIndexChanged += OnEvent;
                        break;
                    case RadioButton radioButton:
                        radioButton.CheckedChanged += OnEvent;
                        break;
                    case CheckBox checkBox:
                        checkBox.CheckedChanged += OnEvent;
                        break;
                    default:
                        throw new NotSupportedException(control.GetType().FullName);
                }
            }
        }
    }
}
