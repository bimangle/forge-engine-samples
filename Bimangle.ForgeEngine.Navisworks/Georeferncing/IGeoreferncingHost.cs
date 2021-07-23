using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;

namespace Bimangle.ForgeEngine.Navisworks.Georeferncing
{
    public interface IGeoreferncingHost
    {
        /// <summary>
        /// 检查投影定义是否有效
        /// </summary>
        /// <param name="projDefinition"></param>
        /// <param name="projWkt"></param>
        /// <returns></returns>
        bool CheckProjDefinition(string projDefinition, out string projWkt);

        /// <summary>
        /// 从目标文件加载 proj 信息
        /// </summary>
        /// <param name="projFilePath"></param>
        /// <returns></returns>
        string GetProjDefinition(string projFilePath);

        /// <summary>
        /// 获得 Proj 来源项列表
        /// </summary>
        /// <returns></returns>
        IList<ProjSourceItem> GetProjSourceItems();

        /// <summary>
        /// 获得模型文件路径
        /// </summary>
        /// <returns></returns>
        string GetModelFilePath();

        /// <summary>
        /// 获得默认投影文件路径
        /// </summary>
        /// <returns></returns>
        string GetDefaultProjFilePath();

        /// <summary>
        /// 获得默认偏移文件路径
        /// </summary>
        /// <returns></returns>
        string GetDefaultOffsetFilePath();

        /// <summary>
        /// 保存投影文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="projDefinition"></param>
        /// <returns></returns>
        bool SaveProjFile(string filePath, string projDefinition);

        /// <summary>
        /// 保存偏移参数
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="offsets"></param>
        /// <returns></returns>
        bool SaveOffsetFile(string filePath, double[] offsets);

        /// <summary>
        /// 获取支持的原点类型
        /// </summary>
        /// <returns></returns>
        OriginType[] GetSupportOriginTypes();

        /// <summary>
        /// 判断原点类型是否为正北
        /// </summary>
        /// <param name="originType"></param>
        /// <returns></returns>
        bool IsTrueNorth(OriginType originType);

        /// <summary>
        /// 创建适配配置
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        GeoreferencedSetting CreateSuitedSetting(GeoreferencedSetting setting);

        /// <summary>
        /// 创建默认配置
        /// </summary>
        /// <returns></returns>
        GeoreferencedSetting CreateDefaultSetting();

        /// <summary>
        /// 创建目标配置
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        GeoreferencedSetting CreateTargetSetting(GeoreferencedSetting setting);

        /// <summary>
        /// 增加最近使用的地理参考定义文件路径
        /// </summary>
        /// <param name="filePath"></param>
        bool CheckInProjFile(string filePath);

        /// <summary>
        /// 获取模型场地信息
        /// </summary>
        /// <returns></returns>
        SiteInfo GetModelSiteInfo();
    }

    public class ProjSourceItem
    {
        public string Label { get; }
        public string FilePath { get; }
        public string ProjDefinition { get; }
        public ProjSourceType SourceType { get; }

        public ProjSourceItem(string label, ProjSourceType sourceType, string filePath, string projDefinition)
        {
            Label = label;
            SourceType = sourceType;
            FilePath = filePath;
            ProjDefinition = projDefinition;
        }

        #region Overrides of Object

        public override string ToString()
        {
            return Label;
        }

        #endregion
    }
}
