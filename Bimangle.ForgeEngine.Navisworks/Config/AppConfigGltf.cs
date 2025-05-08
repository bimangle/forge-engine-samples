using System;
using System.Collections.Generic;
using System.Linq;
using FeatureType = Bimangle.ForgeEngine.Common.Formats.Gltf.FeatureType;

namespace Bimangle.ForgeEngine.Navisworks.Config
{
    [Serializable]
    class AppConfigGltf
    {
        public string LastTargetPath { get; set; }
        public bool AutoOpenAllow { get; set; }
        public string AutoOpenAppName { get; set; }
        public string VisualStyle { get; set; }
        public int LevelOfDetail { get; set; }
        public bool LevelOfDetailAssigned { get; set; }
        public List<FeatureType> Features { get; set; }

        public AppConfigGltf()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = true;
            AutoOpenAppName = null;
            VisualStyle = @"Auto";
            LevelOfDetail = -1;
            LevelOfDetailAssigned = true;
            Features = new List<FeatureType>
            {
                FeatureType.VisualStyleAuto,
                FeatureType.GenerateModelsDb,
                FeatureType.AllowRegroupNodes
            };
        }

        public AppConfigGltf Clone()
        {
            return new AppConfigGltf
            {
                LastTargetPath = LastTargetPath,
                AutoOpenAllow = AutoOpenAllow,
                AutoOpenAppName = AutoOpenAppName,
                VisualStyle = VisualStyle,
                LevelOfDetail = LevelOfDetail,
                LevelOfDetailAssigned = LevelOfDetailAssigned,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
