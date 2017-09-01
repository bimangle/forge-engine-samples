using System;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Navisworks.UI;

namespace Bimangle.ForgeEngine.Navisworks.License
{
    class LicenseScope : IDisposable
    {
        public bool Silent { get; private set; }

        public bool IsValid { get; private set; }

        public LicenseScope(bool silent = false)
        {
            Silent = silent;

            if (Start())
            {
                IsValid = true;
            }
            else
            {
                if (Silent)
                {
                    IsValid = false;
                }
                else
                {
                    if (ShowForm() && Start())
                    {
                        IsValid = true;
                    }
                    else
                    {
                        IsValid = false;
                    }
                }
            }
        }

        private bool Start()
        {
            return LicenseManager.Start();
        }

        private void End()
        {
            LicenseManager.End();
        }

        public bool ShowForm()
        {
            var form = new FormLicense();
            return form.ShowDialog() == DialogResult.OK;
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            End();
        }

        #endregion
    }
}
