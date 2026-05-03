using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Georeferncing.Interface
{
    public interface IAdapterHost
    {
        /// <summary>
        /// 从目标文件加载 proj 信息
        /// </summary>
        /// <param name="projFilePath"></param>
        /// <returns></returns>
        string GetProjDefinition(string projFilePath);

        /// <summary>
        /// 检查 proj 定义信息是否有效
        /// </summary>
        /// <param name="projDefinition"></param>
        /// <param name="projWkt"></param>
        /// <returns></returns>
        bool CheckProjDefinition(string projDefinition, out string projWkt);
    }
}
