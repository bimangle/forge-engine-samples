using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Dgn.Core;
using Bimangle.ForgeEngine.Dgn.Utility;

namespace Bimangle.ForgeEngine.Dgn.Toolset
{
    public class CommandToolsetQuickPreview : IExternalCommand
    {

        #region Implementation of IExternalCommand

        public int Execute(string unparsed, ref string message)
        {
            var previewAppPath = Path.Combine(
                InnerApp.GetHomePath(),
                @"Tools",
                @"Browser",
                @"Bimangle.ForgeBrowser.exe"
            );

            try
            {
                Process.Start(previewAppPath);
            }
            catch (Exception ex)
            {
                FormHelper.ShowMessageBox(ex.ToString());
                Trace.WriteLine(ex.ToString());
            }

            return 0;
        }

        #endregion
    }
}
