using System;
using System.Collections.Generic;
using System.Linq;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
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
        public int LevelOfDetail { get; set; }
        public string LevelOfDetailText { get; set; }
        public List<FeatureType> Features { get; set; }

        /// <summary>
        /// 工作模式
        /// </summary>
        /// <remarks>
        /// 0: 基本模式
        /// 1: 室内室外分别优化
        /// 2: 抽壳模式 - Mesh 级
        /// 3: 抽壳模式 - 构件 级
        /// </remarks>
        public int Mode { get; set; }

        /// <summary>
        /// 地理配准设置
        /// </summary>
        public GeoreferencedSetting GeoreferencedSetting { get; set; }

        /// <summary>
        /// 最近使用投影文件路径列表
        /// </summary>
        public IList<string> RecentlyProjFiles { get; set; }

        public AppConfigCesium3DTiles()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = false;
            AutoOpenAppName = null;
            VisualStyle = null;
            LevelOfDetail = -1;
            LevelOfDetailText = @"Auto";
            Features = new List<FeatureType>
            {
                FeatureType.ExcludeLines,
                FeatureType.ExcludePoints,
                FeatureType.GenerateModelsDb,
                FeatureType.UseGoogleDraco,
                FeatureType.ExcludeTexture,
                FeatureType.EnableTextureWebP,
                FeatureType.EnableEmbedGeoreferencing,
                //FeatureType.EnableUnlitMaterials
            };
            Mode = 0;
            GeoreferencedSetting = null;
            RecentlyProjFiles = new List<string>();
        }

        public AppConfigCesium3DTiles Clone()
        {
            return new AppConfigCesium3DTiles
            {
                LastTargetPath = LastTargetPath,
                AutoOpenAllow = AutoOpenAllow,
                AutoOpenAppName = AutoOpenAppName,
                VisualStyle = VisualStyle,
                LevelOfDetail = LevelOfDetail,
                LevelOfDetailText = LevelOfDetailText,
                Features = Features?.ToList() ?? new List<FeatureType>(),
                Mode = Mode,
                GeoreferencedSetting = GeoreferencedSetting?.Clone(),
                RecentlyProjFiles = RecentlyProjFiles?.ToArray().ToList()
            };
        }
    }
}
