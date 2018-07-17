using System.Windows.Forms;

namespace Bimangle.ForgeEngine.Dwg.App.Utility
{
    static class FormHelper
    {
        public static void ShowMessageBox(this Form form, string message)
        {
            MessageBox.Show(message, form.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
