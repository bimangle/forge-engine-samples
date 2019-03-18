using System.IO;

namespace Bimangle.ForgeEngine.Dwg.CLI.Utility
{
    static class AppHelper
    {
        /// <summary>
        /// 获得相对路径
        /// </summary>
        /// <param name="relatePath"></param>
        /// <returns></returns>
        public static string GetPath(string relatePath)
        {
            var dllFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(dllFolder, relatePath);
        }
    }
}
