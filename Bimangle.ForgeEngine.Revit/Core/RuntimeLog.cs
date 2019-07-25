using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Common.Formats;

namespace Bimangle.ForgeEngine.Revit.Core
{
    class RuntimeLog : IRuntimeLog, IDisposable
    {
        private Stream _Stream;
        private bool _IsInit;

        public RuntimeLog()
        {
            _IsInit = false;
            //Log(@"Info", @"(Internal)", @"Start...");
        }

        private void Init()
        {
            if (_Stream == null && _IsInit == false)
            {
                _IsInit = true;
                _Stream = OpenLogStream();
            }
        }

        private Stream OpenLogStream()
        {
            try
            {
                var logFolder = Path.Combine(InnerApp.GetHomePath(), @"Logs");
                Common.Utils.FileSystemUtility.CreateDirectory(logFolder);

                var logFilePath = Path.Combine(logFolder, $@"{DateTime.Now:yyyy-MM-dd_HHmmss_fff}.log");

                return File.Open(logFilePath, FileMode.Create);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Log(string type, string function, string message)
        {
            Init();

            var s = $"[{DateTime.Now:yyyy-MM-dd HH:mm.ss.fff}] {type} {function}\r\n{message}\r\n\r\n";
            if (_Stream == null)
            {
                Trace.WriteLine(s);
            }
            else
            {
                var bytes = Encoding.UTF8.GetBytes(s);
                _Stream.Write(bytes, 0, bytes.Length);
            }
        }

        void IDisposable.Dispose()
        {
            //Log(@"Info", @"(Internal)", @"End!");

            _Stream?.Dispose();
            _Stream = null;
        }
    }
}
