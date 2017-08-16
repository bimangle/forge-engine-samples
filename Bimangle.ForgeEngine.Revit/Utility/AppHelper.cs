using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Autodesk.Revit.DB;


namespace Bimangle.ForgeEngine.Revit.Utility
{
    static class AppHelper
    {
        private static readonly Dictionary<BuiltInParameterGroup, string> _BuiltInParameterGroupLabels = new Dictionary<BuiltInParameterGroup, string>();

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
