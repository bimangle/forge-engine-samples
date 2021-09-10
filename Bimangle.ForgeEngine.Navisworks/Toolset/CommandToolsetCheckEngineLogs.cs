using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Utility;

namespace Bimangle.ForgeEngine.Navisworks.Toolset
{
    class CommandToolsetCheckEngineLogs
    {
        public void Execute(IWin32Window parentForm)
        {
            try
            {
                var logsPath = Path.Combine(
                    App.GetHomePath(),
                    @"Logs");
                if (Directory.Exists(logsPath) == false)
                {
                    Directory.CreateDirectory(logsPath);
                }
                Process.Start(logsPath);
            }
            catch (Exception ex)
            {
                parentForm.ShowMessageBox(ex.ToString());

                Trace.WriteLine(ex.ToString());
            }
        }
    }
}
