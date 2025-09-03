using System;
using System.Collections.Generic;
using System.Linq;
using Bimangle.ForgeEngine.Common.Formats.Svf.Dwg;


namespace Bimangle.ForgeEngine.Dwg.Config
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
            Features = new List<FeatureType>
            {
                FeatureType.ExportMode2D,
                FeatureType.IncludeUnplottableLayers,
                FeatureType.IncludeLayouts,
                FeatureType.GenerateThumbnail,
                FeatureType.GenerateModelsDb,
                FeatureType.OptimizationLineStyle,
                FeatureType.ForceRenderModeUseWireframe
            };
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

            return featureStrings.ToList();
        }
    }
}
