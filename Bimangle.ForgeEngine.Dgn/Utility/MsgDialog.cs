using System.Runtime.InteropServices;
using System.Text;

namespace Bimangle.ForgeEngine.Dgn.Utility
{
    static class MsgDialog
    {
        [DllImport(@"ustation.dll")]
        public static extern void mdlDialog_dmsgsPrint(byte[] wMsg);

        public static void Log(string message)
        {
            mdlDialog_dmsgsPrint(Encoding.Unicode.GetBytes(message));
        }
    }
}
