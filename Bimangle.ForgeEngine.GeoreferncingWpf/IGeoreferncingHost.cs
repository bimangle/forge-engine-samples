using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Georeferncing.Interface;

namespace Bimangle.ForgeEngine.Georeferncing
{
    public interface IGeoreferncingHost
    {
        /// <summary>
        /// 获取投影验证器
        /// </summary>
        /// <returns></returns>
        IProj GetProjValidator();

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
        /// 获得大地水准面网格列表
        /// </summary>
        /// <returns></returns>
        IList<VerticalGeoidGrid> GetVerticalGeoidGridItems();

        /// <summary>
        /// 获得模型坐标变换类型列表
        /// </summary>
        /// <returns></returns>
        IList<GenericItem<ProjOffsetType>> GetProjOffsetTypes();

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
        /// 保存模型坐标变换参数
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="offsetType"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        bool SaveOffsetFile(string filePath, ProjOffsetType offsetType, double[] offset);

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

        /// <summary>
        /// 获得默认站心模型坐标
        /// </summary>
        /// <returns></returns>
        double[] GetDefaultModelOrigin();

        /// <summary>
        /// 显示拾取坐标对话框
        /// </summary>
        /// <returns></returns>
        bool ShowPickPositionDialog();

        /// <summary>
        /// 适配器对象
        /// </summary>
        Adapter Adapter { get; }

        /// <summary>
        /// 坐标转换试算
        /// </summary>
        /// <param name="p">地理配准设置(投影坐标)</param>
        /// <param name="dataModel">模型坐标</param>
        /// <param name="dataProjected">投影坐标</param>
        /// <param name="dataWorld">世界坐标</param>
        /// <returns></returns>
        bool TestRun(ParameterProj p, double[] dataModel, out double[] dataProjected, out double[] dataWorld);

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

    public class VerticalGeoidGrid
    {
        public static IList<VerticalGeoidGrid> GetList(string projLibPath)
        {
            var list = new List<VerticalGeoidGrid>
            {
                new VerticalGeoidGrid(GeoStrings.GeoidGridNone, null, true),
                new VerticalGeoidGrid(@"EGM96 15'", @"us_nga_egm96_15.tif", true),
                new VerticalGeoidGrid(@"EGM2008 25'", @"us_nga_egm08_25.tif", true),
                new VerticalGeoidGrid(@"EGM2008 1'", @"us_nga_egm2008_1.tif", true)
            };

            foreach (var item in list)
            {
                if (item.FileName == null) continue;

                var filePath = Path.Combine(projLibPath, item.FileName);
                item.IsInstalled = File.Exists(filePath);
            }

            return list;
        }

        public string Name { get; set; }
        public string FileName { get; set; }
        public bool IsInstalled { get; set; }

        public VerticalGeoidGrid(string name, string fileName, bool isInstalled)
        {
            Name = name;
            FileName = fileName;
            IsInstalled = isInstalled;
        }

        #region Overrides of Object

        public override string ToString()
        {
            if(FileName == null) return Name;

            return IsInstalled
                ? $@"{Name} [{FileName}]"
                : $@"{Name} [*{GeoStrings.GeoidGridNotInstalled}*]";
        }

        #endregion
    }

    public class GenericItem<TValue>
    {
        public TValue Value { get; set; }
        public string Label { get; set; }

        public GenericItem(TValue value, string label)
        {
            Value = value;
            Label = label;
        }

        #region Overrides of Object

        public override string ToString()
        {
            return Label;
        }

        #endregion
    }
}
