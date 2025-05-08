using System;
using System.Collections.Generic;
using System.Linq;
using Bimangle.ForgeEngine.Common.Formats.Svf.Dgn;

namespace Bimangle.ForgeEngine.Dgn.Config
{
    [Serializable]
    class AppConfigSvf
    {
        public string LastTargetPath { get; set; }
        public bool AutoOpenAllow { get; set; }
        public string AutoOpenAppName { get; set; }
        public string VisualStyle { get; set; }
        public int LevelOfDetail { get; set; }
        public List<FeatureType> Features { get; set; }

        public AppConfigSvf()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = true;
            AutoOpenAppName = null;
            VisualStyle = @"Auto";
            LevelOfDetail = -1;
            Features = new List<FeatureType>
            {
                FeatureType.VisualStyleAuto,
                FeatureType.GenerateThumbnail,
                FeatureType.GenerateModelsDb,
                FeatureType.ExportElementClassConstruction
            };
        }

        public AppConfigSvf Clone()
        {
            return new AppConfigSvf
            {
                LastTargetPath = LastTargetPath,
                AutoOpenAllow = AutoOpenAllow,
                AutoOpenAppName = AutoOpenAppName,
                VisualStyle = VisualStyle,
                LevelOfDetail = LevelOfDetail,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
