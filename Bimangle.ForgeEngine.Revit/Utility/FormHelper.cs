using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Revit.Utility
{
    static class FormHelper
    {
        public static void ShowMessageBox(this Form form, string message)
        {
            MessageBox.Show(message, form.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
