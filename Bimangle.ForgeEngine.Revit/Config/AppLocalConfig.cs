using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Revit.Core;

namespace Bimangle.ForgeEngine.Revit.Config
{
    [Serializable]
    class AppLocalConfig
    {
        public string LastTargetPath { get; set; }

        public List<FeatureType> Features { get; set; }

        public AppLocalConfig()
        {
            LastTargetPath = string.Empty;
            Features = new List<FeatureType>();
        }

        public AppLocalConfig Clone()
        {
            return new AppLocalConfig
            {
                LastTargetPath = LastTargetPath,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
