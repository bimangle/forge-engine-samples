using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Skp.Core;
using Bimangle.ForgeEngine.Skp.Utility;

namespace Bimangle.ForgeEngine.Skp.Toolset
{
    class CommandToolsetPickPositionFromMap
    {
        #region Implementation of IExternalCommand

        public void Execute(Form form)
        {
            var previewAppPath = Path.Combine(
                App.GetHomePath(),
                @"Tools",
                @"Browser",
                @"Bimangle.ForgeBrowser.exe"
            );

            try
            {
                Process.Start(previewAppPath, @"--PickPosition");
            }
            catch (Exception ex)
            {
                form.ShowMessageBox(ex.ToString());

                Trace.WriteLine(ex.ToString());
            }
        }

        #endregion

    }
}
