using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;

namespace Bimangle.ForgeEngine.Georeferncing.Interface
{
    public abstract class Adapter
    {
        protected string FilePath { get; private set; }
        protected IAdapterHost Host { get; private set; }

        protected Adapter(string filePath)
        {
            FilePath = filePath;
        }

        internal void Init(IAdapterHost host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
        }

        public virtual string GetFilePath()
        {
            return FilePath;
        }

        public virtual bool SetFilePath(string filePath)
        {
            if (FilePath == filePath)
            {
                return false;
            }

            FilePath = filePath;
            return true;
        }

        /// <summary>
        /// 获得 PROJ 库路径
        /// </summary>
        /// <param name="homeFolder"></param>
        /// <returns></returns>
        public virtual string GetProjLibPath(string homeFolder)
        {
            return Path.Combine(homeFolder, @"Common", @"Proj"); ;
        }

        /// <summary>
        /// 创建投影工具类
        /// </summary>
        /// <param name="homeFolder"></param>
        /// <returns></returns>
        public abstract IProj CreateProj(string homeFolder);

        /// <summary>
        /// 获取最近使用的地理参考定义文件路径
        /// </summary>
        /// <returns></returns>
        public abstract IDictionary<string, string> GetRecentlyProjFiles();

        /// <summary>
        /// 记录使用到的地理参考定义文件路径
        /// </summary>
        /// <param name="projFilePath"></param>
        /// <returns></returns>
        public abstract bool CheckInProjFile(string projFilePath);

        /// <summary>
        /// 获得模型的场地信息
        /// </summary>
        /// <returns></returns>
        public virtual SiteInfo GetSiteInfo()
        {
            //默认模型无场地信息
            return null;
        }

        /// <summary>
        /// 获取内嵌的投影坐标定义
        /// </summary>
        /// <returns>投影坐标系定义, 若无效则返回 null</returns>
        public virtual string GetEmbedProjDefinition()
        {
            //默认无内嵌投影坐标定义
            return null;
        }

        /// <summary>
        /// 当前平台是否是 Revit
        /// </summary>
        /// <returns></returns>
        public virtual bool IsRevit()
        {
            return false;
        }

        /// <summary>
        /// 当前模型是否为局部模型
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLocal()
        {
            if (IsRevit())
            {
                var filePath = GetFilePath();
                if (string.IsNullOrWhiteSpace(filePath) == false)
                {
                    var ext = Path.GetExtension(filePath)?.ToLower();
                    if (ext == @".rfa") return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 修正本地坐标方向标签
        /// </summary>
        /// <param name="lblLocalE"></param>
        /// <param name="lblLocalN"></param>
        /// <param name="lblLocalH"></param>
        public virtual void SetDirectionLetters(Label lblLocalE, Label lblLocalN, Label lblLocalH)
        {
            //do nothing
        }

        /// <summary>
        /// 判断对应原点是否为正北方向
        /// </summary>
        /// <param name="originType"></param>
        /// <returns></returns>
        public virtual bool IsTrueNorth(OriginType originType)
        {
            switch (originType)
            {
                case OriginType.Internal:
                case OriginType.Project:
                    return false;
                case OriginType.Shared:
                case OriginType.Survey:
                    return true;
                case OriginType.Auto:
                default:
                    throw new ArgumentOutOfRangeException(nameof(originType), originType, null);
            }
        }

        /// <summary>
        /// 获取原点类型的本地字符串
        /// </summary>
        /// <param name="originType"></param>
        /// <returns></returns>
        public virtual string GetLocalString(OriginType originType)
        {
            switch (originType)
            {
                case OriginType.Auto:
                    return GeoStrings.OriginTypeAuto;
                case OriginType.Internal:
                    return IsRevit() ? GeoStrings.OriginTypeInternal : GeoStrings.OriginTypeDefault;
                case OriginType.Project:
                    return GeoStrings.OriginTypeProject;
                case OriginType.Shared:
                    return GeoStrings.OriginTypeShared;
                case OriginType.Survey:
                    return GeoStrings.OriginTypeSurvey;
                default:
                    throw new ArgumentOutOfRangeException(nameof(originType), originType, null);
            }
        }

        /// <summary>
        /// 获取支持的原点类型
        /// </summary>
        /// <returns></returns>
        public virtual OriginType[] GetSupportOriginTypes()
        {
            return IsLocal()
                ? new[] { OriginType.Internal }
                : new[] { OriginType.Internal, OriginType.Project, OriginType.Shared, OriginType.Survey };
        }

        /// <summary>
        /// 坐标转换试算
        /// </summary>
        /// <param name="projLibPath"></param>
        /// <param name="p">地理配准设置(投影坐标)</param>
        /// <param name="dataModel">模型坐标</param>
        /// <param name="dataProjected">投影坐标</param>
        /// <param name="dataWorld">世界坐标</param>
        /// <returns></returns>
        public abstract bool TestRun(string projLibPath, ParameterProj p, double[] dataModel, out double[] dataProjected, out double[] dataWorld);
    }
}
