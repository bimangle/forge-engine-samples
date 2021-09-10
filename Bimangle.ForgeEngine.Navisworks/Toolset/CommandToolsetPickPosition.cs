using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Navisworks.Utility;

namespace Bimangle.ForgeEngine.Navisworks.Toolset
{
    class CommandToolsetPickPosition
    {
        #region Implementation of IExternalCommand

        public void Execute(IWin32Window form)
        {
            try
            {
                if (Autodesk.Navisworks.Api.Application.ActiveDocument.Models.Count == 0)
                {
                    form.ShowMessageBox(Strings.SceneNoData);
                    return;
                }

                var dialog = new PickPosition.FormPickPosition();
                dialog.Show(form);
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
