using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Revit.Config;
using Bimangle.ForgeEngine.Revit.UI;

namespace Bimangle.ForgeEngine.Revit.License
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
