using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Navisworks.Core.Batch
{
    /// <summary>
    /// 消息路由器
    /// </summary>
    /// <remarks>
    /// 用于自动转换时，Navisworks 插件向外部控制程序报告转换进度
    /// 注: 这里删除了具体实现代码，如果有需要可以自行实现（基于 TCP/IP, 也可以基于内存映射等等）
    /// </remarks>
    class Router
    {
        public static void SetProgressPhase(string name, double weightFactor)
        {
        }

        public static void SetProgressValue(string name, int progressValue)
        {
        }
    }
}
