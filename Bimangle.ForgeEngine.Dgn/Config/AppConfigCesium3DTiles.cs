using System;
using System.Collections.Generic;
using System.Linq;
using Bimangle.ForgeEngine.Common.Georeferenced;
using FeatureType = Bimangle.ForgeEngine.Common.Formats.Cesium3DTiles.FeatureType;

namespace Bimangle.ForgeEngine.Dgn.Config
{
    [Serializable]
    class AppConfigCesium3DTiles
    {
        public string LastTargetPath { get; set; }
        public bool AutoOpenAllow { get; set; }
        public string AutoOpenAppName { get; set; }
        public string VisualStyle { get; set; }
        public int LevelOfDetail { get; set; }
        public List<FeatureType> Features { get; set; }

        /// <summary>
        /// 工作模式
        /// </summary>
        /// <remarks>
        /// 0: 完整模型
        /// 1: (已废弃)区分室内和室外分别优化
        /// 2: 仅模型外壳 - 按三角面筛选
        /// 3: 仅模型外壳 - 按构件筛选
        /// 10: 完整模型 - LOD
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
            AutoOpenAllow = true;
            AutoOpenAppName = null;
            VisualStyle = @"Auto";
            LevelOfDetail = 6;  //默认为 6
            Features = new List<FeatureType>
            {
                FeatureType.VisualStyleAuto,
                FeatureType.GenerateModelsDb,
                FeatureType.ForEarthSdk
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
                Features = Features?.ToList() ?? new List<FeatureType>(),
                Mode = Mode,
                GeoreferencedSetting = GeoreferencedSetting?.Clone(),
                RecentlyProjFiles = RecentlyProjFiles?.ToArray().ToList()
            };
        }
    }
}
