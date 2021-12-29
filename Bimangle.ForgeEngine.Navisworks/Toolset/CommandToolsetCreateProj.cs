using System;
using System.Diagnostics;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Georeferncing;
using Bimangle.ForgeEngine.Navisworks.Config;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Utility;

namespace Bimangle.ForgeEngine.Navisworks.Toolset
{
    class CommandToolsetCreateProj
    {
        public void Execute(IWin32Window parentForm, string inputFilePath)
        {
            try
            {
                var homePath = VersionInfo.GetHomePath();

                var appConfig = AppConfigManager.Load();
                var localConfig = appConfig.Cesium3DTiles;

                var adapter = new GeoreferncingAdapter(localConfig);
                var owner = parentForm;
                using (var host = GeoreferncingHost.Create(adapter, homePath))
                {
                    GeoreferncingHelper.ShowProjCreateUI(owner, host);
                }
            }
            catch (Exception ex)
            {
                parentForm.ShowMessageBox(ex.ToString());

                Trace.WriteLine(ex.ToString());
            }
        }
    }
}
