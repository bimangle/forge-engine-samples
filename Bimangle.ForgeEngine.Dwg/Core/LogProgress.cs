using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Dwg.Utility;

namespace Bimangle.ForgeEngine.Dwg.Core
{
    class LogProgress : ILog
    {
        private readonly Action<int> _ProgressCallback;

        public LogProgress(ProgressExHelper progress)
        {
            _ProgressCallback = progress?.GetProgressCallback() ?? throw new ArgumentNullException(nameof(progress));
        }

        #region Implementation of ILog

        public void WriteLine(string s)
        {
            Debug.WriteLine(s);
        }

        public void Write(string s)
        {
            Debug.Write(s);
        }

        public void WriteProgress(int progress)
        {
            if (progress > 100) progress = 100;
            if (progress < 0) progress = 0;
            _ProgressCallback(progress);
        }

        #endregion
    }
}
