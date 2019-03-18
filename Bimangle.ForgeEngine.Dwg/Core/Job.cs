using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Formats.Svf.Dwg;
using Bimangle.ForgeEngine.Dwg.CLI.Core.Log;

namespace Bimangle.ForgeEngine.Dwg.CLI.Core
{
    class Job : MarshalByRefObject
    {
        #region AssemblyResolve

        static Job()
        {
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith(@"ForgeEngineDwgCLI,", StringComparison.OrdinalIgnoreCase))
            {
                return Assembly.GetCallingAssembly();
            }

            return null;
        }

        #endregion

        private ILog _Log;

        public CancellationTokenSource Cancellation { get; }

        public Job()
        {
            Cancellation = new CancellationTokenSource();

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public int Run(Options options, ILog log)
        {
            _Log = log ?? new LogDummy();

            using (var mutex = new Mutex(false, @"Bimangle.ForgeEngine.Translator", out var createdNew))
            {
                //程序多开时等待
                while (mutex.WaitOne(1000) == false)
                {
                    if (Cancellation.IsCancellationRequested)
                    {
                        return 11;  //被取消
                    }

                    _Log.WriteLine(@"Waiting for other instance to complete job ...");
                }
                
                try
                {
                    try
                    {
                        var result = InternalRun(options, Cancellation.Token);
                        if (Cancellation.IsCancellationRequested)
                        {
                            return 11; //被取消
                        }

                        return result;
                    }
                    catch (TypeInitializationException ex)
                    {
                        Trace.WriteLine(ex.ToString());
                        return 120; //由于未知的原因，导致的随机异常，暂时没有办法解决
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                        return 12;  //失败
                    }
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        private int InternalRun(Options options, CancellationToken cancellationToken)
        {
            //检查输出文件夹
            if (CheckOutputFolder(options, _Log, out var logFilePath) == false)
            {
                return 10;
            }

            //分析源文件
            if (CheckInputFile(options, _Log) == false)
            {
                return 20;
            }

            //构造任务配置
            var config = new ExportConfig();
            config.InputFilePath = options.InputFilePath;
            config.TargetPath = options.OutputFolderPath;
            config.DefaultFontName = Properties.Settings.Default.DefaultFontName;

            config.FontPath = new List<string>
            {
                App.GetFontFolderPath()
            };

            config.Features = new Dictionary<FeatureType, bool>();
            if (options.Features != null)
            {
                foreach (var item in options.Features)
                {
                    if (Enum.TryParse(item, true, out FeatureType featureType))
                    {
                        config.Features[featureType] = true;
                    }
                }
            }

            #region Add Plugin - CreatePropDb
            {
                var cliPath = Path.Combine(
                    App.GetHomePath(),
                    @"Tools",
                    @"CreatePropDb",
                    @"CreatePropDbCLI.exe");

                if (File.Exists(cliPath))
                {
                    config.Addins.Add(new ExportPlugin(
                        FeatureType.GenerateModelsDb,
                        cliPath,
                        new[] { @"-i", config.TargetPath }
                    ));
                }
            }
            #endregion

            #region Add Plugin - CreateLeaflet

            {
                var cliPath = Path.Combine(
                    App.GetHomePath(),
                    @"Tools",
                    @"CreateLeaflet",
                    @"CreateLeafletCLI.exe");
                if (File.Exists(cliPath))
                {
                    config.Addins.Add(new ExportPlugin(
                            FeatureType.GenerateLeaflet,
                            cliPath,
                            new[] { @"-i", config.TargetPath }
                        )
                        { OnlyFor2D = true });
                }
            }
            #endregion

            //执行转换过程
            bool isSuccess;
            var errorCode = 40; //40 一般性失败, 110: 授权无效
            try
            {
                #region 清除目标文件夹的授权失效标记文件
                {
                    try
                    {
                        const string LICENSE_INVALID_FLAG_FILE_NAME = @"License Invalid.txt";
                        var licenseInvalidFlagFilePath = Path.Combine(config.TargetPath, LICENSE_INVALID_FLAG_FILE_NAME);
                        if (File.Exists(licenseInvalidFlagFilePath))
                        {
                            File.Delete(licenseInvalidFlagFilePath);
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
                #endregion

                cancellationToken.ThrowIfCancellationRequested();

                _Log.WriteLine("Execute Job:");

                bool exportSuccess;
                string exportMessage;

                try
                {

                    _Log.WriteLine("\tExecute exporting ...");

                    var ret = ExecuteExport(config);
                    switch (ret)
                    {
                        case 0:
                            _Log.WriteLine($"\tProcessing, 100%");
                            _Log.WriteLine("\tExecute exporting succeeded!");
                            _Log.WriteProgress(90);
                            cancellationToken.ThrowIfCancellationRequested();

                            exportSuccess = true;
                            exportMessage = string.Empty;
                            break;
                        case 110:
                            _Log.WriteLine("\tExecute exporting fail!");

                            errorCode = 110;
                            exportSuccess = false;
                            exportMessage = "License Invalid!";
                            break;
                        default:
                            _Log.WriteLine("\tExecute exporting fail!");

                            exportSuccess = false;
                            exportMessage = "Unknow exception!";
                            break;
                    }
                }
                finally
                {}

                if (exportSuccess)
                {
                    _Log.WriteProgress(100);
                    isSuccess = true;
                    _Log.WriteLine("\tJob completed, Result: Success");
                }
                else
                {
                    _Log.WriteProgress(0);
                    isSuccess = false;
                    _Log.WriteLine("\tJob completed, Result: Fail");
                    _Log.WriteLine($"\t{exportMessage}");
                }
            }
            catch (Exception ex)
            {
                _Log.WriteLine("\tJob completed, Result: Fail");
                _Log.WriteLine($"\t{ex}");

                isSuccess = false;
            }

            //如果转换成功, 就把转换过程日志文件删掉
            if (isSuccess)
            {
                try
                {
                    if (File.Exists(logFilePath)) File.Delete(logFilePath);
                }
                catch
                {
                    // ignored
                }
            }

            return isSuccess ? 0 : errorCode;
        }

        private int ExecuteExport(ExportConfig config)
        {
            using (var session = LicenseConfig.Create())
            {
                if (session.IsValid == false)
                {
                    _Log.WriteLine("\tLicense Invalid!");

                    #region 保存授权无效信息文件
                    try
                    {
                        var filePath = Path.Combine(config.TargetPath, @"License Invalid.txt");
                        File.WriteAllText(filePath, @"未检测到有效的授权, 请检查授权期限是否已过期, 如使用 USBKEY 请确认 USBKEY 是否已正确插入 USB 接口!", Encoding.UTF8);
                    }
                    catch
                    {
                        // ignored
                    }
                    #endregion

                    return 110;
                }

                try
                {
                    Exporter.ExportToSvf(config, OnProgressCallback, Cancellation.Token);
                    OnProgressCallback(100);
                    return 0;
                }
                catch (Exception ex)
                {
                    _Log.WriteLine(ex.ToString());
                    Trace.WriteLine($@"EngineDWG: {ex}");
                    return 102;
                }
            }
        }

        private void OnProgressCallback(double progress)
        {
            _Log.WriteProgress((int)progress);
            _Log.WriteLine($"\tProcessing, {progress}%");
        }

        private bool CheckOutputFolder(Options options, ILog log, out string logFilePath)
        {
            try
            {
                log.WriteLine($"Check output folder path \"{options.OutputFolderPath}\" ...");
                if (Directory.Exists(options.OutputFolderPath) == false)
                {
                    Directory.CreateDirectory(options.OutputFolderPath);
                    log.WriteLine($"\tInfo: Create directory \"{options.OutputFolderPath}\"!");
                }

                //设置文件夹不被索引服务干扰
                SetFolderNotIndexed(options.OutputFolderPath);

                logFilePath = Path.Combine(options.OutputFolderPath, @"translate.log");
                File.WriteAllText(logFilePath, string.Empty, Encoding.UTF8);

                log.WriteLine($"\tInfo: Output folder path is valid.");
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLine($@"\tError: {ex}");
                logFilePath = null;
                return false;
            }
        }

        private bool CheckInputFile(Options options, ILog log)
        {
            try
            {
                log.WriteLine($"Analyzing soruce file \"{ options.InputFilePath}\" ...");
                if (File.Exists(options.InputFilePath) == false)
                {
                    log.WriteLine($"\tError: Source file \"{options.InputFilePath}\" not exists!");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                log.WriteLine($@"\tError: {ex}");
                return false;
            }
        }

        /// <summary>
        /// 设置文件夹不被索引服务干扰
        /// </summary>
        /// <param name="folderPath"></param>
        private void SetFolderNotIndexed(string folderPath)
        {
            try
            {
                var di = new DirectoryInfo(folderPath);
                if ((di.Attributes & FileAttributes.NotContentIndexed) != FileAttributes.NotContentIndexed)
                {
                    File.SetAttributes(folderPath, di.Attributes | FileAttributes.NotContentIndexed);
                }
            }
            catch
            {
                // ignored
            }
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
