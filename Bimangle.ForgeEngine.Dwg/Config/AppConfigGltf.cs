using System;
using System.Collections.Generic;
using System.Linq;
using FeatureType = Bimangle.ForgeEngine.Common.Formats.Gltf.FeatureType;
using PreFeatureType = Bimangle.ForgeEngine.Common.Formats.Svf.Dwg.FeatureType;

namespace Bimangle.ForgeEngine.Dwg.Config
{
    [Serializable]
    class AppConfigGltf
    {
        public string LastTargetPath { get; set; }
        public string VisualStyle { get; set; }
        //public int LevelOfDetail { get; set; }
        public List<FeatureType> Features { get; set; }
        public List<PreFeatureType> PreFeatures { get; set; }

        public AppConfigGltf()
        {
            LastTargetPath = string.Empty;
            VisualStyle = @"Wireframe";
            //LevelOfDetail = 6;  //默认为 6
            Features = new List<FeatureType>
            {
                FeatureType.Wireframe,
                FeatureType.GenerateModelsDb,
                FeatureType.AllowRegroupNodes
            };
            PreFeatures = new List<PreFeatureType>()
            {
                PreFeatureType.IncludeUnplottableLayers,    //包括 不可打印图层
                PreFeatureType.OptimizationLineStyle        //自动优化线型
            };
        }

        public AppConfigGltf Clone()
        {
            return new AppConfigGltf
            {
                LastTargetPath = LastTargetPath,
                VisualStyle = VisualStyle,
                //LevelOfDetail = LevelOfDetail,
                Features = Features?.ToList() ?? new List<FeatureType>(),
                PreFeatures = PreFeatures?.ToList() ?? new List<PreFeatureType>()
            };
        }

        public IList<string> GetFeatureStrings()
        {
            var featureStrings = new HashSet<string>();

            if (Features != null && Features.Any())
            {
                foreach (var feature in Features)
                {
                    featureStrings.Add(feature.ToString());
                }
            }

            if (PreFeatures != null && PreFeatures.Any())
            {
                foreach (var feature in PreFeatures)
                {
                    featureStrings.Add(feature.ToString());
                }
            }

            return featureStrings.ToList();
        }
    }
}
