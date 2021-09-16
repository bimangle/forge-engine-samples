//--------------------------------------------------------------------------------------+
//
//    $Source: Bimangle.ForgeEngine.Dgn.Loader.cs $
// 
//    $Copyright: (c) 2019 Bentley Systems, Incorporated. All rights reserved. $
//
//---------------------------------------------------------------------------------------+

using System.Diagnostics;
using System.Text;
using Bentley.DgnPlatform;

namespace Bimangle.ForgeEngine.Dgn
{
    [Bentley.MstnPlatformNET.AddIn(MdlTaskID = "BimangleEngineSamples")]
    internal sealed class AddIn : Bentley.MstnPlatformNET.AddIn
    {
        public static AddIn Instance = null;

        private AddIn(System.IntPtr mdlDesc) : base(mdlDesc)
        {
            Instance = this;
        }

        protected override int Run(string[] commandLine)
        {
            return 0;
        }
    }
}