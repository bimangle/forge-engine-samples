using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Dwg.Config;
using Bimangle.ForgeEngine.Georeferncing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Bimangle.ForgeEngine.Dwg.Core
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
            if (CheckOutputFolder(options, _Log, out var georeferencedSetting, out var logFilePath) == false)
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

                try
                {
                    _Log.WriteLine("\tExecute exporting ...");

                    var ret = ExecuteExport(options, georeferencedSetting);
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
                catch (TypeInitializationException ex)
                {
                    //偶尔会遇到这种未知原因，暂时无法处理的异常，目前唯一能做的就是重试
                    //“<Module>”的类型初始值设定项引发异常。 ---> System.AppDomainUnloadedException: 尝试访问已卸载的 AppDomain。

                    _Log.WriteLine($"\t{ex}");
                
                    errorCode = 120;
                    exportSuccess = false;
                    exportMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    _Log.WriteLine("\tJob completed, Result: Fail");
                    _Log.WriteLine($"\t{ex}");

                    exportSuccess = false;
                    exportMessage = ex.Message;
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

        private int ExecuteExport(Options options, GeoreferencedSetting georeferencedSetting)
        {
            var homePath = App.GetHomePath();

            using (var log = new RuntimeLog(homePath))
            {
                try
                {
                    if (StartExport(options, georeferencedSetting, log, x => OnProgressCallback(x), CancellationToken.None))
                    {
                        OnProgressCallback(100);
                        return 0;
                    }
                    return 40;
                }
                catch (Exception ex)
                {
                    _Log.WriteLine(ex.ToString());
                    Trace.WriteLine($@"EngineDWG: {ex}");
                    return 102;
                }
            }
        }

        private bool StartExport(Options options, GeoreferencedSetting georeferencedSetting, RuntimeLog log, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            var targetFormat = options.Format?.Trim().ToLowerInvariant();

            switch (targetFormat)
            {
                case @"gltf":
                    ExportToGltf(options, log, progressCallback, cancellationToken);
                    break;
                case @"3dtiles":
                    ExportToCesium3DTiles(options, georeferencedSetting, log, progressCallback, cancellationToken);
                    break;
#if !EXPRESS
                case @"svf":
                    ExportToSvf(options, log, progressCallback, cancellationToken);
                    break;
#endif
                default:
                    log.Log(@"Fail", @"Startup", $@"Unsupported format - {targetFormat}");
                    return false;
            }

            return true;
        }


#if !EXPRESS
        private void ExportToSvf(Options options, RuntimeLog log, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            var features = new HashSet<Common.Formats.Svf.Dwg.FeatureType>();
            if (options.Features != null && options.Features.Any())
            {
                foreach (var feature in options.Features)
                {
                    if (Enum.TryParse(feature, true, out Common.Formats.Svf.Dwg.FeatureType result))
                    {
                        features.Add(result);
                    }
                }
            }

            var setting = new Bimangle.ForgeEngine.Common.Formats.Svf.Dwg.ExportSetting();
            setting.OutputPath = options.OutputPath;
            setting.Features = features.ToList();
            setting.DefaultFontName = Properties.Settings.Default.DefaultFontName;
            setting.FontPath = new List<string>
            {
                App.GetFontFolderPath()
            };
            setting.Oem = App.GetOemInfo();

            var exporter = new Bimangle.ForgeEngine.Dwg.Pro.Svf.Exporter(App.GetHomePath());
            exporter.Export(options.InputFilePath, setting, log, progressCallback, cancellationToken);
        }
#endif

        private void ExportToGltf(Options options, RuntimeLog log, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            var features = new HashSet<Common.Formats.Gltf.FeatureType>();
            if (options.Features != null && options.Features.Any())
            {
                foreach (var feature in options.Features)
                {
                    if (Enum.TryParse(feature, true, out Common.Formats.Gltf.FeatureType result))
                    {
                        features.Add(result);
                    }
                }
            }

            var setting = new Bimangle.ForgeEngine.Common.Formats.Gltf.Dwg.ExportSetting();
            setting.OutputPath = options.OutputPath;
            setting.Features = features.ToList();
            setting.PreExportSeedFeatures = options.Features?.ToList();
            setting.DefaultFontName = Properties.Settings.Default.DefaultFontName;
            setting.FontPath = new List<string>
            {
                App.GetFontFolderPath()
            };
            setting.Oem = App.GetOemInfo();

#if EXPRESS
            var exporter = new Bimangle.ForgeEngine.Dwg.Express.Gltf.Exporter(App.GetHomePath());
#else
            var exporter = new Bimangle.ForgeEngine.Dwg.Pro.Gltf.Exporter(App.GetHomePath());
#endif
            exporter.Export(options.InputFilePath, setting, log, progressCallback, cancellationToken);
        }

        private void ExportToCesium3DTiles(Options options, GeoreferencedSetting georeferencedSetting, RuntimeLog log, Action<int> progressCallback, CancellationToken cancellationToken)
        {
            var features = new HashSet<Common.Formats.Cesium3DTiles.FeatureType>();
            if (options.Features != null && options.Features.Any())
            {
                foreach (var feature in options.Features)
                {
                    if (Enum.TryParse(feature, true, out Common.Formats.Cesium3DTiles.FeatureType result))
                    {
                        features.Add(result);
                    }
                }

                features.Add(Common.Formats.Cesium3DTiles.FeatureType.EnableEmbedGeoreferencing);
            }
            else
            {
                var defaultConfig = new AppConfigCesium3DTiles();
                foreach (var feature in defaultConfig.Features)
                {
                    features.Add(feature);
                }
            }

            var setting = new Bimangle.ForgeEngine.Common.Formats.Cesium3DTiles.Dwg.ExportSetting();
            setting.OutputPath = options.OutputPath;
            setting.Mode = options.Mode;
            setting.Features = features.ToList();
            setting.PreExportSeedFeatures = options.Features?.ToList();
            setting.GeoreferencedSetting = GetGeoreferencedSetting(options.InputFilePath, georeferencedSetting, setting.Features);
            setting.DefaultFontName = Properties.Settings.Default.DefaultFontName;
            setting.FontPath = new List<string>
            {
                App.GetFontFolderPath()
            };
            setting.Oem = App.GetOemInfo();

#if EXPRESS
            var exporter = new Bimangle.ForgeEngine.Dwg.Express.Cesium3DTiles.Exporter(App.GetHomePath());
#else
            var exporter = new Bimangle.ForgeEngine.Dwg.Pro.Cesium3DTiles.Exporter(App.GetHomePath());
#endif
            exporter.Export(options.InputFilePath, setting, log, progressCallback, cancellationToken);
        }

        private GeoreferencedSetting GetGeoreferencedSetting(string filePath, GeoreferencedSetting setting, IList<Common.Formats.Cesium3DTiles.FeatureType> features)
        {
            var adapter = new GeoreferncingAdapter(filePath, null);
            using (var gh = GeoreferncingHost.Create(adapter, App.GetHomePath()))
            {
                var d = setting?.Clone() ?? gh.CreateDefaultSetting();
                var result = gh.CreateTargetSetting(d);
                result?.Fit(features);
                return result;
            }
        }

        private void OnProgressCallback(double progress)
        {
            if (Math.Abs(progress - _LastProgressValue) < 0.01) return;
            _LastProgressValue = progress;

            _Log.WriteProgress((int)progress);
            _Log.WriteLine($"\tProcessing, {progress}%");
        }

        private bool CheckOutputFolder(Options options, ILog log, out GeoreferencedSetting georeferenced, out string logFilePath)
        {
            georeferenced = null;

            try
            {
                var outputFolderPath = options.GetOutputFolderPath();

                log.WriteLine($"Check output folder path \"{options.OutputPath}\" ...");
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

                if (format == Options.FORMAT_3DTILES)
                {
                    log.WriteLine("Check Georeferenced Setting ...");

                    if (string.IsNullOrWhiteSpace(options.GeoreferencedBase64) == false)
                    {
                        try
                        {
                            georeferenced = GeoreferncingHelper.CreateFromBase64(options.GeoreferencedBase64);
                            log.WriteLine("\tInfo: Georeferenced Setting is valid.");
                        }
                        catch (Exception ex)
                        {
                            log.WriteLine(ex.ToString());
                            log.WriteLine("\tWarn: Parse Georeferenced Setting Fail! Default Settings will be used.");
                        }
                    }

                    var adapter = new GeoreferncingAdapter(options.InputFilePath, null);
                    using (var host = GeoreferncingHost.Create(adapter, App.GetHomePath()))
                    {
                        if (georeferenced == null)
                        {
                            var d = host.CreateSuitedSetting(null);
                            georeferenced = host.CreateTargetSetting(d);
                        }

                        georeferenced?.Fit(options.Features);

                        var infos = georeferenced.GetBrief(adapter);
                        foreach (var info in infos)
                        {
                            log.WriteLine($"\t{info}");
                        }
                    }
                }

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
