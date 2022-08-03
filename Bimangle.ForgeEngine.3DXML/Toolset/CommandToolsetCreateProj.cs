using System;
using System.Diagnostics;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Georeferncing;
using Bimangle.ForgeEngine._3DXML.Config;
using Bimangle.ForgeEngine._3DXML.Core;
using Bimangle.ForgeEngine._3DXML.Utility;

namespace Bimangle.ForgeEngine._3DXML.Toolset
{
    class CommandToolsetCreateProj
    {
        public void Execute(Form parentForm, string inputFilePath)
        {
            try
            {
                var homePath = App.GetHomePath();

                var appConfig = AppConfigManager.Load();
                var localConfig = appConfig.Cesium3DTiles;

                var adapter = new GeoreferncingAdapter(inputFilePath, localConfig);
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
