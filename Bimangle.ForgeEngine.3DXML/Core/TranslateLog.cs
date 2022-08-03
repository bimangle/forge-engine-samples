using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine._3DXML.Core
{
    class TranslateLog : IDisposable
    {
        public static TranslateLog Init(string filePath)
        {
            var stream = File.OpenWrite(filePath);
            return new TranslateLog(stream);
        }

        private FileStream _Stream;
        private CustomTraceListener _TraceListener;

        private TranslateLog(FileStream stream)
        {
            _Stream = stream;
            _TraceListener = new CustomTraceListener(Log);
        }

        public void Debug(string s)
        {
            Log(s);
        }

        public void Log(string s)
        {
            var log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + @" " + s + "\r\n";

            if (_Stream != null)
            {
                var buffer = Encoding.UTF8.GetBytes(log);
                _Stream.Write(buffer, 0, buffer.Length);
                _Stream.Flush(true);
            }
        }

        public TraceListener GetTraceListener()
        {
            return _TraceListener;
        }

        #region IDisposable

        public  void Dispose()
        {
            if (_Stream != null)
            {
                _Stream.Dispose();
                _Stream = null;
            }

            if (_TraceListener != null)
            {
                _TraceListener.Dispose();
                _TraceListener = null;
            }
        }

        #endregion
    }
}
