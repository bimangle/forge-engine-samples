using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Dwg.Core;
using Bimangle.ForgeEngine.Dwg.Utility;

namespace Bimangle.ForgeEngine.Dwg.Toolset
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
