using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bimangle.ForgeEngine.Navisworks
{
    static class VersionInfo
    {
        public const string COMPANY_NAME = COMPANY_ID;
        public const string COMPANY_ID = @"BimAngleSamples";
        public const string COMPANY_FOLDER = @"Bimangle";

        public const string PRODUCT_ID = @"EngineNW";
        public const string PRODUCT_FOLDER = @"Bimangle.ForgeEngine.Navisworks";

        public const string HTML_TITLE = @"BimAngle.com";

#if EXPRESS
        public const string PRODUCT_NAME = @"BimAngle Engine Express (For Navisworks)";
#else
        public const string PRODUCT_NAME = @"BimAngle Engine (For Navisworks)";
#endif

#if CLI
        public const string VERSION = @"0.1";
#else
        public const string VERSION = PackageInfo.VERSION_STRING;
#endif

        public const string TITLE = PRODUCT_NAME + @" " + VERSION;
        public const string COPYRIGHT = @"Copyright © " + COMPANY_NAME + @" 2017-2023";

        public const string TAB_NAME = COMPANY_ID;

#if EXPRESS
        public const string PANEL_NAME = "BimAngle Engine Express Samples";
#else
        public const string PANEL_NAME = "BimAngle Engine Samples";
#endif

        /// <summary>
        /// 获得 Home Folder 路径
        /// </summary>
        /// <returns></returns>
        public static string GetHomePath()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                COMPANY_FOLDER,
                PRODUCT_FOLDER);

            // Common.Utils.FileSystemUtility.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// 定制化 3D Tiles 文件输出
        /// </summary>
        /// <param name="folderPath"></param>
        public static void CustomOutputFor3DTiles(string folderPath)
        {
#region 删除不需要的文件
            {
                //var filePathList = new[]
                //{
                //    Path.Combine(folderPath, @"bimangle-latest.js"),
                //    Path.Combine(folderPath, @"index.html"),
                //    Path.Combine(folderPath, @"index-online.html")
                //};
                //foreach (var filePath in filePathList)
                //{
                //    try
                //    {
                //        if (File.Exists(filePath)) File.Delete(filePath);
                //    }
                //    catch (Exception ex)
                //    {
                //        Trace.WriteLine(ex.ToString());
                //    }
                //}
            }
#endregion

#region 删除 tileset.json 中不需要的部分
            {
                /*
                try
                {
                    var filePath = Path.Combine(folderPath, @"tileset.json");
                    if (File.Exists(filePath))
                    {
                        var content = File.ReadAllText(filePath, Encoding.UTF8);
                        var json = JObject.Parse(content);

                        //移除 extras
                        {
                            var root = json;
                            root.Remove(@"extras");
                        }

                        //移除 asset/extras
                        {
                            var asset = json[@"asset"] as JObject;
                            asset?.Remove(@"extras");
                        }

                        content = json.ToString(Formatting.None);
                        var buffer = Encoding.UTF8.GetBytes(content);
                        File.WriteAllBytes(filePath, buffer);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
                */
            }
#endregion
        }

    }
}
