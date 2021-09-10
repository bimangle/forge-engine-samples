using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Utility;

namespace Bimangle.ForgeEngine.Navisworks.Toolset
{
    class CommandToolsetPickPositionFromMap
    {
        #region Implementation of IExternalCommand

        public void Execute(IWin32Window form)
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
