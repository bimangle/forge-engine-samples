using System;
using System.IO;
using System.Reflection;

namespace Bimangle.ForgeEngine.Dwg.CLI.Core
{
    class App
    {
        /// <summary>
        /// 获得主路径
        /// </summary>
        /// <returns></returns>
        public static string GetHomePath()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                @"Bimangle",
                @"Bimangle.ForgeEngine.Dwg");
            return path;
        }

        public static string GetFontFolderPath()
        {
            var folderPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            return Path.Combine(folderPath, @"Fonts");
        }
    }
}
