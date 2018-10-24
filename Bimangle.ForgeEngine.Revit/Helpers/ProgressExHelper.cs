using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Revit.Helpers.Progress;

namespace Bimangle.ForgeEngine.Revit.Helpers
{
    class ProgressExHelper : IDisposable
    {
        private static ProgressExHelper _Instance;

        public static void Close()
        {
            var obj = _Instance;
            if (obj != null)
            {
                obj.Dispose();
            }
        }

        private FormProgressEx _Form;

        public ProgressExHelper(string title = null, int progressLimit = 100)
        {
            _Form = new FormProgressEx(title, progressLimit);
            _Form.StartPosition = FormStartPosition.CenterScreen;
            _Form.Show();
            _Form.Refresh();

            _Instance = this;
        }

        public ProgressExHelper(Form owner, string title = null, int progressLimit = 100)
        {
            _Form = new FormProgressEx(title, progressLimit);
            _Form.StartPosition = FormStartPosition.CenterParent;
            _Form.Show(owner);
            _Form.Location = new Point(
                (owner.Width - _Form.Width)/2 + owner.Left,
                (owner.Height - _Form.Height)/2 + owner.Top);
            _Form.Refresh();

            _Instance = this;
        }

        public CancellationToken GetCancellationToken()
        {
            return _Form?.GetCancellationToken() ?? CancellationToken.None;
        }

        public Action<int> GetProgressCallback()
        {
            if (_Form == null) return null;

            Action<int> m = _Form.SetProgressValue;
            return m;
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