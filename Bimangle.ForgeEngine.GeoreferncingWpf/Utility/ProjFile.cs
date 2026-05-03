using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Georeferncing.Utility
{
    class ProjFile
    {
        private const string PREFIX_PROJCS = @"PROJCS[";
        private const string PREFIX_PROJCRS = @"PROJCRS[";
        private const string PREFIX_COMPOUNDCRS = @"COMPOUNDCRS[";

        /// <summary>
        /// 检查是否为有效的 OGC WKT/WKT2 定义格式
        /// </summary>
        /// <param name="srs"></param>
        /// <returns></returns>
        public static bool IsValidOgcWkt(string srs)
        {
            if (string.IsNullOrWhiteSpace(srs)) return false;

            var s = srs.Trim();
            if (s.StartsWith(PREFIX_PROJCS, StringComparison.OrdinalIgnoreCase) ||
                s.StartsWith(PREFIX_PROJCRS, StringComparison.OrdinalIgnoreCase) ||
                s.StartsWith(PREFIX_COMPOUNDCRS, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
