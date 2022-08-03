using System;
using System.Threading;

namespace Bimangle.ForgeEngine._3DXML.Core
{
    [Serializable]
    class CancellationTokenSourceProxy : MarshalByRefObject
    {
        private CancellationTokenSource _Source;

        public CancellationTokenSourceProxy(CancellationTokenSource source)
        {
            _Source = source;
        }

        public void Cancel()
        {
            _Source?.Cancel();
        }

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
