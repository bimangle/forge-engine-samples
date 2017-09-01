using System;
using System.Drawing;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Navisworks.Helpers.Progress;

namespace Bimangle.ForgeEngine.Navisworks.Helpers
{
    class ProgressHelper : IDisposable
    {
        private static ProgressHelper _Instance;

        public static void Close()
        {
            var obj = _Instance;
            if (obj != null)
            {
                obj.Dispose();
            }
        }

        private FormProgress _Form;

        public ProgressHelper(string title = null)
        {
            _Form = new FormProgress(title);
            _Form.StartPosition = FormStartPosition.CenterScreen;
            _Form.Show();
            _Form.Refresh();

            _Instance = this;
        }

        public ProgressHelper(Form owner, string title = null)
        {
            _Form = new FormProgress(title);
            _Form.StartPosition = FormStartPosition.CenterParent;
            _Form.Show(owner);
            _Form.Location = new Point(
                (owner.Width - _Form.Width)/2 + owner.Left,
                (owner.Height - _Form.Height)/2 + owner.Top);
            _Form.Refresh();

            _Instance = this;
        }

        public void Dispose()
        {
            if (_Form != null)
            {
                _Form.Close();
                _Form = null;

                _Instance = null;
            }
        }
    }
}