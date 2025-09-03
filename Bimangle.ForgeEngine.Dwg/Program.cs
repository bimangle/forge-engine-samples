using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Dwg.Core;
using Bimangle.ForgeEngine.Dwg.Config;

namespace Bimangle.ForgeEngine.Dwg
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var appConfig = AppConfigManager.Load();
            var format = Options.DEFAULT_FORMAT;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI.FormExport(appConfig, format));
        }
    }
}
