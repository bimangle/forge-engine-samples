using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Revit.Helpers.Progress
{
    partial class FormProgressEx : Form
    {
        private readonly CancellationTokenSource _CancellationTokenSource;

        private string _Title;
        private readonly DateTime _StartTime = DateTime.Now;

        public FormProgressEx()
        {
            this.InitializeComponent();
        }

        public FormProgressEx(string title, int progressLimit = 100) : this()
        {
            _CancellationTokenSource = new CancellationTokenSource();
            _Title = title;

            if (_Title != null)
            {
                label.Text = _Title;
                Text = _Title;
            }

            progressBar.Maximum = progressLimit;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this, 
                    Strings.SureToCancelTask, 
                    Text, 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _CancellationTokenSource.Cancel();

                btnCancel.Enabled = false;
                timer1.Enabled = false;
            }
        }

        public CancellationToken GetCancellationToken()
        {
            return _CancellationTokenSource.Token;
        }

        public void SetProgressValue(int value)
        {
            try
            {
                if (IsDisposed) return;
                if (IsHandleCreated == false) return;

                if (InvokeRequired)
                {
                    Action<int> m = SetProgressValue;
                    Invoke(m, value);
                    return;
                }

                if (value > progressBar.Maximum) value = progressBar.Maximum;
                if (value < progressBar.Minimum) value = progressBar.Minimum;
                progressBar.Value = value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            //finally
            //{
            //    Application.DoEvents();
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsDisposed) return;
                if (IsHandleCreated == false) return;

                if (progressBar.Value <= 0) return;

                //已使用的秒数
                var usedSeconds = (DateTime.Now - _StartTime).TotalSeconds;

                //剩余的秒数
                //  预计总耗时= 已用秒数/已完成比例
                //  剩余的秒数= 预计总耗时 - 已使用的秒数
                var restSeconds = usedSeconds / (progressBar.Value * 1.0 / progressBar.Maximum) - usedSeconds;

                if (double.IsInfinity(restSeconds) == false && double.IsNaN(restSeconds) == false)
                {
                    var usedTime = TimeSpan.FromSeconds(Math.Round(usedSeconds));
                    var restTime = TimeSpan.FromSeconds(Math.Round(restSeconds));
                    lblTime.Text = $@"[{usedTime} / ~ {restTime}]";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void FormProgressEx_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
