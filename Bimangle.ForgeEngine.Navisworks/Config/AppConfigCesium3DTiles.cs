using System;
using System.Collections.Generic;
using System.Linq;
using FeatureType = Bimangle.ForgeEngine.Common.Formats.Cesium3DTiles.FeatureType;

namespace Bimangle.ForgeEngine.Navisworks.Config
{
    [Serializable]
    class AppConfigCesium3DTiles
    {
        public string LastTargetPath { get; set; }
        public bool AutoOpenAllow { get; set; }
        public string AutoOpenAppName { get; set; }
        public string VisualStyle { get; set; }
        public List<FeatureType> Features { get; set; }
        public int Mode { get; set; }

        public AppConfigCesium3DTiles()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = false;
            AutoOpenAppName = null;
            VisualStyle = null;
            Features = new List<FeatureType>
            {
                FeatureType.ExcludeLines,
                FeatureType.ExcludePoints,
                FeatureType.GenerateModelsDb,
                FeatureType.EnableQuantizedAttributes
            };
            Mode = 0;
        }

        public AppConfigCesium3DTiles Clone()
        {
            return new AppConfigCesium3DTiles
            {
                LastTargetPath = LastTargetPath,
                AutoOpenAllow = AutoOpenAllow,
                AutoOpenAppName = AutoOpenAppName,
                VisualStyle = VisualStyle,
                Features = Features?.ToList() ?? new List<FeatureType>(),
                Mode = Mode
            };
        }
    }
}
