﻿using System;
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
        public string LevelOfDetailText { get; set; }
        public List<FeatureType> Features { get; set; }

        public AppConfigGltf()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = false;
            AutoOpenAppName = null;
            VisualStyle = null;
            LevelOfDetail = -1;
            LevelOfDetailText = @"Auto";
            Features = new List<FeatureType>
            {
                FeatureType.GenerateModelsDb
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
                LevelOfDetailText = LevelOfDetailText,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
