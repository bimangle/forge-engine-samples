using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Bentley.DgnPlatformNET;
using Bentley.DgnPlatformNET.Elements;
using Debug = System.Diagnostics.Debug;


namespace Bimangle.ForgeEngine.Dgn.Utility
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

        /// <summary>
        /// 获得 CPU 核心数
        /// </summary>
        /// <returns></returns>
        public static int GetProcessorCount()
        {
            return Environment.ProcessorCount;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public static void CreateDirectory(string folderPath)
        {
            if (Directory.Exists(folderPath) == false)
            {
                var di = Directory.CreateDirectory(folderPath);

                //确保文件夹内的文件不被索引服务干扰
                if ((di.Attributes & FileAttributes.NotContentIndexed) != FileAttributes.NotContentIndexed)
                {
                    File.SetAttributes(folderPath, di.Attributes | FileAttributes.NotContentIndexed);
                }
            }
        }

        /// <summary>
        /// 清除文件夹内所有的文件和子文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public static void RemoveDirectory(string folderPath)
        {
            if (Directory.Exists(folderPath) == false) return;

            var filePaths = Directory.GetFiles(folderPath, @"*", SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                try
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
                catch
                {
                    // ignored
                }
            }

            var subfolderPaths = Directory.GetDirectories(folderPath, @"*", SearchOption.TopDirectoryOnly);
            foreach (var subFolderPath in subfolderPaths)
            {
                try
                {
                    Directory.Delete(subFolderPath, true);
                }
                catch
                {
                    // ignored
                }
            }

            try
            {
                Directory.Delete(folderPath);
            }
            catch
            {
                // ignored
            }
        }

        public static string Intern(this string s)
        {
            if (s == null) return null;
            if (s.Length == 0) return string.Empty;
            return string.Intern(s);
        }

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
    }
}

