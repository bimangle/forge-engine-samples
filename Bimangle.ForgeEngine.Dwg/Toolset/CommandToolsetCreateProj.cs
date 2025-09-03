using System;
using System.Diagnostics;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Georeferncing;
using Bimangle.ForgeEngine.Dwg.Config;
using Bimangle.ForgeEngine.Dwg.Core;
using Bimangle.ForgeEngine.Dwg.Utility;

namespace Bimangle.ForgeEngine.Dwg.Toolset
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
