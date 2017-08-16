using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Bimangle.ForgeEngine.Revit.Utility;
using Newtonsoft.Json;

namespace Bimangle.ForgeEngine.Revit.Config
{
    [Serializable]
    class AppConfig
    {
        public AppLocalConfig Local { get; set; }

        public AppConfig()
        {
            Local = new AppLocalConfig();
        }

        public AppConfig Clone()
        {
            return new AppConfig
            {
                Local = Local == null ? new AppLocalConfig() : Local.Clone(),
            };
        }
    }
}
