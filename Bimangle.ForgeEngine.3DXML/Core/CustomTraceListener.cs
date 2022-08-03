using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine._3DXML.Core
{
    class CustomTraceListener : TraceListener
    {
        private Action<string> _Callback;

        public CustomTraceListener(Action<string> callback)
        {
            _Callback = callback;
        }

        #region Overrides of TraceListener

        public override void Write(string message)
        {
            _Callback?.Invoke(message);
        }

        public override void WriteLine(string message)
        {
            _Callback?.Invoke(message);
        }

        #endregion
    }
}
