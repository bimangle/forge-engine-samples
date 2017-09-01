using System;
using System.Collections.Generic;
using System.Linq;
using Bimangle.ForgeEngine.Navisworks.Core;

namespace Bimangle.ForgeEngine.Navisworks.Config
{
    [Serializable]
    class AppLocalConfig
    {
       public string LastTargetPath { get; set; }
        public bool AutoOpenAllow { get; set; }
        public string AutoOpenAppName { get; set; }

        public List<FeatureType> Features { get; set; }

        public AppLocalConfig()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = true;
            AutoOpenAppName = null;
            Features = new List<FeatureType>();
        }

        public AppLocalConfig Clone()
        {
            return new AppLocalConfig
            {
                LastTargetPath = LastTargetPath,
                AutoOpenAllow = AutoOpenAllow,
                AutoOpenAppName = AutoOpenAppName,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
