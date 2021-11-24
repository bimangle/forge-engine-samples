using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Dgn.Core;
using Bimangle.ForgeEngine.Dgn.Utility;

namespace Bimangle.ForgeEngine.Dgn.Toolset
{
    public class CommandToolsetCheckEngineLogs : IExternalCommand
    {
        #region Implementation of IExternalCommand

        public int Execute(string unparsed, ref string message)
        {
            try
            {
                var logsPath = Path.Combine(
                    VersionInfo.GetHomePath(),
                    @"Logs");
                if (Directory.Exists(logsPath) == false)
                {
                    Directory.CreateDirectory(logsPath);
                }
                Process.Start(logsPath);
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
