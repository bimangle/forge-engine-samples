using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Georeferncing.Utility
{
    static class ControlEventHandlers
    {
        public static void OnValidating(this TextBox textBox, Func<TextBox, bool> callback)
        {
            TextboxValidatingHandler.Bind(textBox, callback);
        }

        public static void OnValidating(this TextBox textBox, Action<TextBox> callback)
        {
            TextboxValidatingHandler.Bind(textBox, callback);
        }

        public static void OnValidated(this TextBox textBox, Action<TextBox> callback)
        {
            TextboxValidatedHandler.Bind(textBox, callback);
        }

        public static void OnCheckedChanged(this CheckBox checkBox, Action<CheckBox> callback)
        {
            CheckBoxCheckedChangedHandler.Bind(checkBox, callback);
        }

        class TextboxValidatingHandler
        {
            public static void Bind(TextBox textBox, Func<TextBox, bool> callback)
            {
                if (textBox == null) throw new ArgumentNullException(nameof(textBox));
                if (callback == null) throw new ArgumentNullException(nameof(callback));

                var handler = new TextboxValidatingHandler(callback, null);
                textBox.Validating += handler.TextBox_Validating;
            }

            public static void Bind(TextBox textBox, Action<TextBox> callback)
            {
                if (textBox == null) throw new ArgumentNullException(nameof(textBox));
                if (callback == null) throw new ArgumentNullException(nameof(callback));

                var handler = new TextboxValidatingHandler(null, callback);
                textBox.Validating += handler.TextBox_Validating;
            }

            private readonly Func<TextBox, bool> _Callback1;
            private readonly Action<TextBox> _Callback2;

            private TextboxValidatingHandler(Func<TextBox, bool> callback1, Action<TextBox> callback2)
            {
                _Callback1 = callback1;
                _Callback2 = callback2;
            }

            private void TextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
            {
                if (_Callback1 != null)
                {
                    if (_Callback1(sender as TextBox) == false)
                    {
                        e.Cancel = true;
                    }
                    return;
                }

                _Callback2?.Invoke(sender as TextBox);
            }
        }

        class TextboxValidatedHandler
        {
            public static void Bind(TextBox textBox, Action<TextBox> callback)
            {
                if (textBox == null) throw new ArgumentNullException(nameof(textBox));
                if (callback == null) throw new ArgumentNullException(nameof(callback));

                var handler = new TextboxValidatedHandler(callback);
                textBox.Validated += handler.TextBox_Validated;

            }

            private readonly Action<TextBox> _Callback;

            private TextboxValidatedHandler(Action<TextBox> callback)
            {
                _Callback = callback;
            }

            private void TextBox_Validated(object sender, EventArgs e)
            {
                _Callback?.Invoke(sender as TextBox);
            }
        }

        class CheckBoxCheckedChangedHandler
        {
            public static void Bind(CheckBox checkBox, Action<CheckBox> callback)
            {
                if (checkBox == null) throw new ArgumentNullException(nameof(checkBox));
                if (callback == null) throw new ArgumentNullException(nameof(callback));

                var handler = new CheckBoxCheckedChangedHandler(callback);
                checkBox.CheckedChanged += handler.TextBox_CheckedChanged;

            }

            private readonly Action<CheckBox> _Callback;

            private CheckBoxCheckedChangedHandler(Action<CheckBox> callback)
            {
                _Callback = callback;
            }

            private void TextBox_CheckedChanged(object sender, EventArgs e)
            {
                _Callback?.Invoke(sender as CheckBox);
            }
        }
    }
}
