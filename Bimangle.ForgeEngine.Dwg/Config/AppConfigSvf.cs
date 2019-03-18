using System;
using System.Collections.Generic;
using System.Linq;
using Bimangle.ForgeEngine.Common.Formats.Svf.Dwg;


namespace Bimangle.ForgeEngine.Dwg.CLI.Config
{
    [Serializable]
    class AppConfigSvf
    {
        public string InputFilePath { get; set; }
        public string LastTargetPath { get; set; }

        public List<FeatureType> Features { get; set; }

        public AppConfigSvf()
        {
            InputFilePath = string.Empty;
            LastTargetPath = string.Empty;
            Features = new List<FeatureType>();
        }

        public AppConfigSvf Clone()
        {
            return new AppConfigSvf
            {
                InputFilePath = InputFilePath,
                LastTargetPath = LastTargetPath,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
