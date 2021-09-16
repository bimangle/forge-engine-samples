using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Skp.Core;
using Bimangle.ForgeEngine.Skp.Utility;

namespace Bimangle.ForgeEngine.Skp.Toolset
{
    class CommandToolsetQuickPreview
    {
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
                Process.Start(previewAppPath);
            }
            catch (Exception ex)
            {
                form.ShowMessageBox(ex.ToString());
                Trace.WriteLine(ex.ToString());
            }
        }
    }
}
