using System;
using System.IO;

namespace Bimangle.ForgeEngine._3DXML.Utility
{
    static class AppHelper
    {
        /// <summary>
        /// 确保文件夹路径存在
        /// </summary>
        /// <param name="folderPath"></param>
        public static void MakeSureFolderPathExists(string folderPath)
        {
            var targetFolderPath = Path.GetPathRoot(folderPath);    //获得文件夹的根路径
            if (string.IsNullOrEmpty(targetFolderPath) == false)
            {
                folderPath = folderPath.Substring(targetFolderPath.Length); //获得文件夹路径中除了根路径剩余的部分
            }

            var segs = folderPath.Split(new[] { @"\", @"/" }, StringSplitOptions.None);
            for (var i = 0; i < segs.Length; i++)
            {
                var seg = segs[i];
                targetFolderPath = Path.Combine(targetFolderPath, seg);
                if (Directory.Exists(targetFolderPath) == false)
                {
                    Directory.CreateDirectory(targetFolderPath);
                }
            }
        }

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
