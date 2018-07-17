using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Dwg.App.Config;
using Bimangle.ForgeEngine.Dwg.App.UI;
using Bimangle.ForgeEngine.Dwg.Core;

namespace Bimangle.ForgeEngine.Dwg.App
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var appConfig = AppConfigManager.Load();
            Application.Run(new FormAppXp(appConfig));
        }
    }
}
