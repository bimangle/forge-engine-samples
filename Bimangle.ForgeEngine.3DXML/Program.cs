using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Bimangle.ForgeEngine._3DXML.Config;
using Bimangle.ForgeEngine._3DXML.Core;

namespace Bimangle.ForgeEngine._3DXML
{
    class Program
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
