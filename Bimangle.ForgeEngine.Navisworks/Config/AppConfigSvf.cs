using System;
using System.Collections.Generic;
using System.Linq;
using Bimangle.ForgeEngine.Common.Formats.Svf.Navisworks;

namespace Bimangle.ForgeEngine.Navisworks.Config
{
    [Serializable]
    class AppConfigSvf
    {
        public string LastTargetPath { get; set; }
        public bool AutoOpenAllow { get; set; }
        public string AutoOpenAppName { get; set; }
        public string VisualStyle { get; set; }
        public int LevelOfDetail { get; set; }
        public string LevelOfDetailText { get; set; }
        public List<FeatureType> Features { get; set; }

        public AppConfigSvf()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = true;
            AutoOpenAppName = null;
            VisualStyle = null;
            LevelOfDetail = -1;
            LevelOfDetailText = @"Auto";
            Features = new List<FeatureType>
            {
                FeatureType.GenerateThumbnail,
                FeatureType.GenerateModelsDb
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
                LevelOfDetailText = LevelOfDetailText,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
