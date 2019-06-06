using System;
using System.Diagnostics;

namespace Bimangle.ForgeEngine.Dwg.CLI.Core.Log
{
    [Serializable]
    class LogTrace : MarshalByRefObject, ILog
    {
        private readonly string _Source;

        public LogTrace(string source)
        {
            _Source = source;
        }

        #region Implementation of ILog

        public void WriteLine(string s)
        {
            if (string.IsNullOrEmpty(_Source))
            {
                Trace.WriteLine(s);
            }
            else
            {
                Trace.WriteLine($@"[{_Source}]{s}");
            }
        }

        public void Write(string s)
        {
            Trace.Write(s);
        }
        public void WriteProgress(int progress)
        {
        }

        #endregion

        #region Overrides of MarshalByRefObject

        public override object InitializeLifetimeService()
        {
            var lease = (System.Runtime.Remoting.Lifetime.ILease)base.InitializeLifetimeService();
            // Normally, the initial lease time would be much longer.
            // It is shortened here for demonstration purposes.
            if (lease?.CurrentState == System.Runtime.Remoting.Lifetime.LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromSeconds(0);//这里改成0，则是无限期
                //lease.SponsorshipTimeout = TimeSpan.FromSeconds(10);
                //lease.RenewOnCallTime = TimeSpan.FromSeconds(2);
            }
            return lease;
        }

        #endregion
    }
}
