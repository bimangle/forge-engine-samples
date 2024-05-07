using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitXYZ = Autodesk.Revit.DB.XYZ;

namespace Bimangle.ForgeEngine.Revit.Utility
{
    static class AppHelper
    {
        /// <summary>
        /// 细线选项是否处于启用状态
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static bool AreThinLinesEnabled(this Autodesk.Revit.ApplicationServices.Application app)
        {
#if R2014
            //Revit 2014 不不提供静态类 ThinLinesOptions
            return false;
#elif R2015
            //从 Revit 2015 R2 开始, 才开始提供静态类 ThinLinesOptions
            //参考:
            //  https://thebuildingcoder.typepad.com/blog/2015/03/thin-lines-add-in-using-ui-automation.html
            //  https://knowledge.autodesk.com/support/revit/downloads/caas/downloads/content/autodesk-revit-2015-product-updates.html

            //Revit 2015 从 R2 开始提供静态类 ThinLinesOptions

            //2022-03-03 因为开发环境安装的 Revit 2015 是 R2 之前的老版本, 以下代码无法通过编译, 暂时屏蔽
            //const string R2015_R2_VERSION_BUILD = @"20140905_0730(x64)";
            //if (string.CompareOrdinal(app.VersionBuild, R2015_R2_VERSION_BUILD) >= 0)
            //{
            //    try
            //    {
            //        return ThinLinesOptions.AreThinLinesEnabled;
            //    }
            //    catch (Exception ex)
            //    {
            //        Trace.WriteLine(ex.ToString());
            //    }
            //}
            return false;
#else
            return ThinLinesOptions.AreThinLinesEnabled;
#endif
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

        /// <summary>
        /// 获得项目基点信息
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ProjectLocation GetPrjLocation(this Document doc)
        {
            //参考 https://thebuildingcoder.typepad.com/blog/2017/05/finding-the-right-project-location.html

            if (doc.ActiveProjectLocation == null) return null;

            var docProjectLocations = doc.ProjectLocations.OfType<ProjectLocation>().ToList();

            using (var collector = new FilteredElementCollector(doc).OfClass(typeof(ProjectLocation)))
            {
                var plList = new List<ProjectLocation>();
                foreach (var element in collector)
                {
                    var pl = element as ProjectLocation;
                    if (pl == null) continue;

                    plList.Add(pl);
                }

                // 判断条件1: 对应项目基点的 ProjectLocation 并不在 doc.ProjectLocations 列表中（据说也不一定）
                if (plList.Count == docProjectLocations.Count + 1)
                {
                    var pl = plList.FirstOrDefault(x => docProjectLocations.Any(y => y.Id == x.Id) == false);
                    if (pl != null) return pl;
                }

                // 判断条件2: 所有的命名位置对应的 ProjectLocation 共享相同的 SiteLocation
                {
                    //按照 SiteLocation 分组，并按成员数对分组排序
                    var groups = plList
                        .GroupBy(x => x.GetSiteLocation()?.Id.Value() ?? -1)
                        .OrderBy(x => x.Count())
                        .ToList();
                    if (groups.Count == 2 && groups[0].Count() == 1 && groups[1].Count() > 1)
                    {
                        return groups[0].First();
                    }
                }

                // 判断条件3: 最后的办法, 用名字来匹配，虽然不准确但也没别的办法了
                {
                    var pl = plList.FirstOrDefault(x => x.Name == @"Project" || x.Name == @"项目" || x.Name == @"項目");
                    if (pl != null) return pl;
                }
            }

            return null;
        }

        public static RevitXYZ GetPosition(this BasePoint rp)
        {
#if R2014 || R2015 || R2016 || R2017 || R2018 || R2018 || R2019
            var bb = rp?.get_BoundingBox(null);
            return bb?.Min;
#else
            return rp?.Position;            
#endif
        }

        /// <summary>
        /// 获得测量点的内部坐标
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static RevitXYZ GetSurveyPointLocation(this Document doc)
        {
#if R2014 || R2015 || R2016 || R2017 || R2018 || R2018 || R2019 || R2020
            var points = new FilteredElementCollector(doc)
                .OfClass(typeof(BasePoint))
                .Cast<BasePoint>();
            foreach (var bp in points)
            {
                if (bp.IsShared)
                {
                    var xyz = bp.GetPosition();
                    if (xyz != null) return xyz;
                }
            }
#else
            var point = Autodesk.Revit.DB.BasePoint.GetSurveyPoint(doc);
            if(point != null)
            {
                var xyz = point.Position;
                if (xyz != null) return xyz;
            }
#endif

            return RevitXYZ.Zero;
        }


#if R2014 || R2015 || R2016 || R2017
        public static SiteLocation GetSiteLocation(this ProjectLocation pl)
        {
            return pl?.SiteLocation;
        }
#endif

        public static IWin32Window GetMainWindowHandle(this ExternalCommandData commandData)
        {
            IntPtr h;

#if R2014 || R2015 || R2016 || R2017 || R2018
            h = System.Diagnostics.Process.GetCurrentProcess()?.MainWindowHandle ?? IntPtr.Zero;
#else
            h = commandData?.Application?.MainWindowHandle ?? IntPtr.Zero;
#endif
            return IntPtr.Zero == h 
                ? null 
                : new WindowHandleFomIntPtr(h);
        }

        private class WindowHandleFomIntPtr : IWin32Window
        {
            public IntPtr Handle { get; }

            public WindowHandleFomIntPtr(IntPtr handle)
            {
                Handle = handle;
            }
        }
    }
}
