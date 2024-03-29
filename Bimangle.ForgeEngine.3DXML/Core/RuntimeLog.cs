﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Bimangle.ForgeEngine.Common.Formats;

namespace Bimangle.ForgeEngine._3DXML.Core
{
    class RuntimeLog : IRuntimeLog, IDisposable
    {
        private Stream _Stream;
        private bool _IsInit;
        private readonly string _HomePath;

        private CustomTraceListener _TraceListener;

        public RuntimeLog(string homePath)
        {
            _IsInit = false;
            _HomePath = homePath;
            //Log(@"Info", @"(Internal)", @"Start...");

            _TraceListener = new CustomTraceListener(s =>
            {
                Log(@"Info", @"Trace", s);
            });
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
                var logFolder = Path.Combine(_HomePath, @"Logs");
                Common.Utils.FileSystemUtility.CreateDirectory(logFolder);

                var logFilePath = Path.Combine(logFolder, $@"{DateTime.Now:yyyy-MM-dd_HHmmss_fff}.log");

                return File.Open(logFilePath, FileMode.Create);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TraceListener GetTraceListener()
        {
            return _TraceListener;
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
                _Stream.Flush();
            }
        }

        void IDisposable.Dispose()
        {
            //Log(@"Info", @"(Internal)", @"End!");

            _Stream?.Flush();
            _Stream?.Dispose();
            _Stream = null;

            _TraceListener?.Dispose();
            _TraceListener = null;
        }
    }

}
