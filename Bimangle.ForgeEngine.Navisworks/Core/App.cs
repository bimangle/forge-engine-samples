using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Navisworks.Core
{
    class App
    {
        public const string VERSION = @"2017.9.1";
        public const string TITLE = "BimAngle Forge Engine For Navisworks " + VERSION;

#if R2014
        public const string NW_VERSION = "2014";
#elif R2015
        public const string NW_VERSION = "2015";
#elif R2016
        public const string NW_VERSION = "2016";
#elif R2017
        public const string NW_VERSION = "2017";
#elif R2018
        public const string NW_VERSION = "2018";
#else
        public const string NW_VERSION = "UNKNOW";
#endif
    }
}
