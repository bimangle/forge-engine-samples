using System;
using System.Diagnostics;
using System.Windows.Forms;
using Bentley.MstnPlatformNET;
using Bimangle.ForgeEngine.Dgn.Config;
using Bimangle.ForgeEngine.Dgn.Core;
using Bimangle.ForgeEngine.Dgn.Utility;
using Bimangle.ForgeEngine.Georeferncing;

namespace Bimangle.ForgeEngine.Dgn.Toolset
{
    public class CommandToolsetCreateProj : IExternalCommand
    {

        #region Implementation of IExternalCommand

        public int Execute(string unparsed, ref string message)
        {
            try
            {
                var homePath = VersionInfo.GetHomePath();

                var appConfig = AppConfigManager.Load();
                var localConfig = appConfig.Cesium3DTiles;

                var adapter = new GeoreferncingAdapter(localConfig);
                Form owner = null;
                using (var host = GeoreferncingHost.Create(adapter, homePath))
                {
                    GeoreferncingHelper.ShowProjCreateUI(owner, host);
                }
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
