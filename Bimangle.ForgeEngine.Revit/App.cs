using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Core;

namespace Bimangle.ForgeEngine.Revit
{
    public class App : IExternalApplication
    {
        private readonly IExternalApplication _App;

        public App()
        {
            _App = new InnerApp();
        }

        #region IExternalApplication 成员

        Result IExternalApplication.OnShutdown(UIControlledApplication application)
        {
            return _App.OnShutdown(application);
        }

        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            return _App.OnStartup(application);
        }

        #endregion
    }
}
