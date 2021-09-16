using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bentley.MstnPlatformNET;
using Bimangle.ForgeEngine.Dgn.Core;
using Bimangle.ForgeEngine.Dgn.Utility;

namespace Bimangle.ForgeEngine.Dgn.Toolset
{
    class CommandToolsetPickPosition : IExternalCommand
    {
        private readonly AddIn _Addin;

        public CommandToolsetPickPosition(AddIn addin)
        {
            _Addin = addin ?? throw new ArgumentNullException(nameof(addin));
        }

        #region Implementation of IExternalCommand

        public int Execute(string unparsed, ref string message)
        {
            try
            {
                var dialog = new PickPosition.FormPickPosition();
                dialog.Show();
                dialog.AttachAsTopLevelForm(_Addin, true);
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
