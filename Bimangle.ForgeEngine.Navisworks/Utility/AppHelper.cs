using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NwVector3D = Autodesk.Navisworks.Api.Vector3D;

namespace Bimangle.ForgeEngine.Navisworks.Utility
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

        public static void SetDirectionLetter(this Label lbl, NwVector3D v, string placeHolder)
        {
            var letter = GetDirectionLetter(v);
            lbl.Text = lbl.Text.Replace(placeHolder, letter);
        }

        private static bool IsEqual(this double source, double target, double error = 0.000001)
        {
            return Math.Abs(source - target) < error;
        }

        private static bool IsZero(this double n, double error = 0.000001)
        {
            return IsEqual(n, 0.0, error);
        }

        private static string GetDirectionLetter(this NwVector3D v)
        {
            if (IsZero(v.X) == false &&
                IsZero(v.Y) &&
                IsZero(v.Z))
            {
                return v.X >= 0 ? @"X" : @"-X";
            }

            if (IsZero(v.X) &&
                IsZero(v.Y) == false &&
                IsZero(v.Z))
            {
                return v.Y >= 0 ? @"Y" : @"-Y";
            }

            if (IsZero(v.X) &&
                IsZero(v.Y) &&
                IsZero(v.Z) == false)
            {
                return v.Z >= 0 ? @"Z" : @"-Z";
            }

            return @"*";
        }

    }
}
