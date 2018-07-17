using System;
using System.Collections.Generic;
using System.Linq;
using Bimangle.ForgeEngine.Dwg.Core;

namespace Bimangle.ForgeEngine.Dwg.App.Config
{
    [Serializable]
    class AppLocalConfig
    {
        public string InputFilePath { get; set; }
        public string LastTargetPath { get; set; }

        public List<FeatureType> Features { get; set; }

        public AppLocalConfig()
        {
            InputFilePath = string.Empty;
            LastTargetPath = string.Empty;
            Features = new List<FeatureType>();
        }

        public AppLocalConfig Clone()
        {
            return new AppLocalConfig
            {
                InputFilePath = InputFilePath,
                LastTargetPath = LastTargetPath,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
