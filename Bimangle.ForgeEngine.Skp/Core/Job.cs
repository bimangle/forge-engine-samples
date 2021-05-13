using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Skp.Config;

namespace Bimangle.ForgeEngine.Skp.Core
{
    class Job
    {
        private ILog _Log;
        private double _LastProgressValue = -1.0;
        private CancellationToken _CancellationToken;

        public Job()
        {
        }

        public int Run(Options options, ILog log, CancellationToken cancellationToken)
        {
            _Log = log ?? throw new ArgumentNullException(nameof(log));
            _CancellationToken = cancellationToken;

            try
            {
                var result = InternalRun(options, _CancellationToken);
                if (_CancellationToken.IsCancellationRequested)
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

            //执行转换过程
            bool isSuccess;
            var errorCode = 40; //40 一般性失败, 110: 授权无效
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                _Log.WriteLine("Execute Job:");

                bool exportSuccess;
                string exportMessage;

                {
                    _Log.WriteLine("\tExecute exporting ...");

                    var ret = ExecuteExport(options);
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
                            exportMessage = "Unknown exception!";
                            break;
                    }
                }

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

        private int ExecuteExport(Options config)
        {
            var homePath = App.GetHomePath();

            using (var log = new RuntimeLog(homePath))
            {
                try
                {
                    if (StartExport(config, log, x => OnProgressCallback(x), _CancellationToken))
                    {
                        OnProgressCallback(100);
                        return 0;
                    }
                    return 40;
                }
                catch (Exception ex)
                {
                    _Log.WriteLine(ex.ToString());
                    Trace.WriteLine($@"EngineSKP: {ex}");
                    return 102;
                }
            }
        }

        private bool StartExport(Options config, RuntimeLog log, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            var targetFormat = config.Format?.Trim().ToLowerInvariant();

            switch (targetFormat)
            {
                case @"gltf":
                    ExportToGltf(config, log, progressCallback, cancellationToken);
                    break;
                case @"3dtiles":
                    ExportToCesium3DTiles(config, log, progressCallback, cancellationToken);
                    break;
#if !EXPRESS
                case @"svf":
                    ExportToSvf(config, log, progressCallback, cancellationToken);
                    break;
#endif
                default:
                    log.Log(@"Fail", @"Startup", $@"Unsupported format - {targetFormat}");
                    return false;
            }

            return true;
        }


#if !EXPRESS
        private void ExportToSvf(Options config, RuntimeLog log, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            var features = new Dictionary<Common.Formats.Svf.Skp.FeatureType, bool>();
            var options = config.Features?.ToList();
            if (options != null && options.Count > 0)
            {
                foreach (var option in options)
                {
                    if (Enum.TryParse(option, true, out Common.Formats.Svf.Skp.FeatureType result))
                    {
                        features[result] = true;
                    }
                }
            }

            var setting = new Bimangle.ForgeEngine.Common.Formats.Svf.Skp.ExportSetting();
            setting.ExportType = Common.Formats.Svf.Skp.ExportType.Folder;
            setting.OutputPath = config.OutputFolderPath;
            setting.Features = features.Where(x => x.Value).Select(x => x.Key).ToList();

            var exporter = new Bimangle.ForgeEngine.Skp.Pro.Svf.Exporter(App.GetHomePath());
            exporter.Export(config.InputFilePath, setting, log, progressCallback, CancellationToken.None);
        }
#endif

        private void ExportToGltf(Options config, RuntimeLog log, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            var features = new Dictionary<Common.Formats.Gltf.FeatureType, bool>();
            var options = config.Features?.ToList();
            if (options != null && options.Count > 0)
            {
                foreach (var option in options)
                {
                    if (Enum.TryParse(option, true, out Common.Formats.Gltf.FeatureType result))
                    {
                        features[result] = true;
                    }
                }
            }

            var setting = new Bimangle.ForgeEngine.Common.Formats.Gltf.Skp.ExportSetting();
            setting.OutputPath = config.OutputFolderPath;
            setting.Features = features?.Where(x => x.Value).Select(x => x.Key).ToList();

#if EXPRESS
            var exporter = new Bimangle.ForgeEngine.Skp.Express.Gltf.Exporter(App.GetHomePath());
#else
            var exporter = new Bimangle.ForgeEngine.Skp.Pro.Gltf.Exporter(App.GetHomePath());
#endif
            exporter.Export(config.InputFilePath, setting, log, progressCallback, CancellationToken.None);
        }

        private void ExportToCesium3DTiles(Options config, RuntimeLog log, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            var features = new Dictionary<Common.Formats.Cesium3DTiles.FeatureType, bool>();
            var options = config.Features?.ToList();
            if (options != null && options.Count > 0)
            {
                foreach (var option in options)
                {
                    if (Enum.TryParse(option, true, out Common.Formats.Cesium3DTiles.FeatureType result))
                    {
                        features[result] = true;
                    }
                }

                features[Common.Formats.Cesium3DTiles.FeatureType.EnableEmbedGeoreferencing] = true;
            }
            else
            {
                var defaultConfig = new AppConfigCesium3DTiles();
                foreach (var feature in defaultConfig.Features)
                {
                    features[feature] = true;
                }
            }


            var setting = new Bimangle.ForgeEngine.Common.Formats.Cesium3DTiles.Skp.ExportSetting();
            setting.OutputPath = config.OutputFolderPath;
            setting.Mode = config.Mode;
            setting.Features = features?.Where(x => x.Value).Select(x => x.Key).ToList();
            setting.Site = SiteInfo.CreateDefault();
            setting.Oem = App.GetOemInfo();

#if EXPRESS
            var exporter = new Bimangle.ForgeEngine.Skp.Express.Cesium3DTiles.Exporter(App.GetHomePath());
#else
            var exporter = new Bimangle.ForgeEngine.Skp.Pro.Cesium3DTiles.Exporter(App.GetHomePath());
#endif
            exporter.Export(config.InputFilePath, setting, log, progressCallback, CancellationToken.None);
        }

        private void OnProgressCallback(double progress)
        {
            if (Math.Abs(progress - _LastProgressValue) < 0.01) return;
            _LastProgressValue = progress;

            _Log.WriteProgress((int)progress);
            _Log.WriteLine($"\tProcessing, {progress}%");
        }

        private bool CheckOutputFolder(Options options, ILog log, out string logFilePath)
        {
            try
            {
                var outputFolderPath = options.Format == Options.FORMAT_GLTF
                    ? Path.GetDirectoryName(options.OutputFolderPath)
                    : options.OutputFolderPath;

                log.WriteLine($"Check output folder path \"{options.OutputFolderPath}\" ...");
                if (Directory.Exists(outputFolderPath) == false)
                {
                    Directory.CreateDirectory(outputFolderPath);
                    log.WriteLine($"\tInfo: Create directory \"{outputFolderPath}\"!");
                }

                //设置文件夹不被索引服务干扰
                SetFolderNotIndexed(outputFolderPath);

                logFilePath = Path.Combine(outputFolderPath, @"translate.log");
                File.WriteAllText(logFilePath, string.Empty, Encoding.UTF8);

                var format = options.Format ?? Options.DEFAULT_FORMAT;

                log.WriteLine($"\tInfo: Output folder path is valid.");
                log.WriteLine($"\tInfo: Output Format: {format.ToUpper()}");
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
                log.WriteLine($"Analyzing source file \"{ options.InputFilePath}\" ...");
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
    }
}
