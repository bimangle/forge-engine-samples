using System;
using System.Collections.Generic;
using System.Linq;
using FeatureType = Bimangle.ForgeEngine.Common.Formats.Gltf.FeatureType;

namespace Bimangle.ForgeEngine._3DXML.Config
{
    [Serializable]
    class AppConfigGltf
    {
        public string LastTargetPath { get; set; }
        public bool AutoOpenAllow { get; set; }
        public string AutoOpenAppName { get; set; }
        public string VisualStyle { get; set; }
        public int LevelOfDetail { get; set; }
        public List<FeatureType> Features { get; set; }

        public AppConfigGltf()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = false;
            AutoOpenAppName = null;
            VisualStyle = null;
            LevelOfDetail = 6;  //默认为 6
            Features = new List<FeatureType>
            {
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
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
