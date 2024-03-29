﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Navisworks.Api.Plugins;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Georeferncing;
using Bimangle.ForgeEngine.Navisworks.Config;
using Bimangle.ForgeEngine.Navisworks.Core;
using Bimangle.ForgeEngine.Navisworks.Core.Batch;

namespace Bimangle.ForgeEngine.Navisworks
{

    [Plugin("EngineBatch_Sample", VersionInfo.COMPANY_ID)]
    [AddInPlugin(AddInLocation.None)]
    public class CommandBatch : AddInPlugin
    {

        #region Overrides of AddInPlugin

        public override int Execute(params string[] args)
        {
            using (var log = new RuntimeLog())
            using (var session = LicenseConfig.Create())
            {
                try
                {
                    if (args == null || args.Length != 1)
                    {
                        log.Log(@"Fail", @"Startup", $@"传入参数数量: {args?.Length ?? -1}");
                        return 101;
                    }

                    var jobConfigFilePath = args[0];
                    var jobConfig = JobConfig.Load(jobConfigFilePath);
                    if (jobConfig == null)
                    {
                        log.Log(@"Fail", @"Startup", @"任务设置加载失败");
                        return 102;
                    }

                    if (session.IsValid == false)
                    {
                        log.Log(@"Fail", @"Startup", @"授权无效!");

                        #region 保存授权无效信息文件
                        try
                        {
                            var filePath = Path.Combine(jobConfig.OutputPath, @"License Invalid.txt");
                            File.WriteAllText(filePath, @"未检测到有效的授权, 请检查授权期限是否已过期, 如使用 USBKEY 请确认 USBKEY 是否已正确插入 USB 接口!", Encoding.UTF8);
                        }
                        catch
                        {
                            // ignored
                        }
                        #endregion

                        return 110;
                    }

                    const string SOURCE_NAME = @"Nw";   //这个数据源名称不可修改，是和 CLI 里的设置对应的
                    Router.SetProgressPhase(SOURCE_NAME, 1.0);

                    var progress = 0;
                    var ret = StartExport(jobConfig, log, x =>
                    {
                        var newProgress = x;
                        if (newProgress > progress)
                        {
                            progress = newProgress;
                            Router.SetProgressValue(SOURCE_NAME, newProgress);
                        }
                    });

                    if (!ret)
                    {
                        return 103;
                    }

                    Router.SetProgressValue(SOURCE_NAME, 100);
                }
                catch (Exception ex)
                {
                    log.Log(@"Exception", @"Startup", ex.ToString());
                    return 100;
                }
            }

            return 0;
        }

        #endregion

        #region Overrides of Plugin

        protected override void OnLoaded()
        {
            base.OnLoaded();

            AppDomain.CurrentDomain.AssemblyResolve += App.OnAssemblyResolve;
        }

        protected override void OnUnloading()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= App.OnAssemblyResolve;

            base.OnUnloading();
        }

        #endregion

        private bool StartExport(JobConfig jobConfig, RuntimeLog log, Action<int> progressCallback)
        {
            var targetFormat = jobConfig.Format?.Trim().ToLowerInvariant();

            switch (targetFormat)
            {
                case @"gltf":
                    ExportToGltf(jobConfig, log, progressCallback);
                    break;
                case @"3dtiles":
                    ExportToCesium3DTiles(jobConfig, log, progressCallback);
                    break;
#if !EXPRESS
                case @"svf":
                    ExportToSvf(jobConfig, log, progressCallback);
                    break;
#endif
                default:
                    log.Log(@"Fail", @"Startup", $@"Unsupported format - {targetFormat}");
                    return false;
            }

            return true;
        }

#if !EXPRESS
        private void ExportToSvf(JobConfig config, RuntimeLog log, Action<int> progressCallback)
        {
            var features = new Dictionary<Common.Formats.Svf.Navisworks.FeatureType, bool>();
            var options = config.OutputOptions;
            if (options != null && options.Count > 0)
            {
                foreach (var option in options)
                {
                    if (Enum.TryParse(option, true, out Common.Formats.Svf.Navisworks.FeatureType result))
                    {
                        features[result] = true;
                    }
                }
            }

            var setting = new Bimangle.ForgeEngine.Common.Formats.Svf.Navisworks.ExportSetting();
            setting.ExportType = Common.Formats.Svf.Navisworks.ExportType.Folder;
            setting.OutputPath = config.OutputPath;
            setting.LevelOfDetail = config.GetLevelOfDetail();
            setting.Features = features.Where(x => x.Value).Select(x => x.Key).ToList();

            var exporter = new Bimangle.ForgeEngine.Navisworks.Pro.Svf.Exporter(VersionInfo.GetHomePath());
            exporter.Export(setting, log, progressCallback, CancellationToken.None);
        }
#endif

        private void ExportToGltf(JobConfig config, RuntimeLog log, Action<int> progressCallback)
        {
            var features = new Dictionary<Common.Formats.Gltf.FeatureType, bool>();
            var options = config.OutputOptions;
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

            var setting = new Bimangle.ForgeEngine.Common.Formats.Gltf.Navisworks.ExportSetting();
            setting.OutputPath = config.OutputPath;
            setting.LevelOfDetail = config.GetLevelOfDetail();
            setting.Features = features?.Where(x => x.Value).Select(x => x.Key).ToList();

#if EXPRESS
            var exporter = new Bimangle.ForgeEngine.Navisworks.Express.Gltf.Exporter(VersionInfo.GetHomePath());
#else
            var exporter = new Bimangle.ForgeEngine.Navisworks.Pro.Gltf.Exporter(VersionInfo.GetHomePath());
#endif
            exporter.Export(setting, log, progressCallback, CancellationToken.None);
        }

        private void ExportToCesium3DTiles(JobConfig config, RuntimeLog log, Action<int> progressCallback)
        {
            var features = new Dictionary<Common.Formats.Cesium3DTiles.FeatureType, bool>();
            var options = config.OutputOptions;
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


            var setting = new Bimangle.ForgeEngine.Common.Formats.Cesium3DTiles.Navisworks.ExportSetting();
            setting.OutputPath = config.OutputPath;
            setting.Mode = config.Mode;
            setting.LevelOfDetail = config.GetLevelOfDetail();
            setting.Features = features?.Where(x => x.Value).Select(x => x.Key).ToList();
            setting.GeoreferencedSetting = GetGeoreferencedSetting(config.GeoreferencedSetting, setting.Features);
            setting.Oem = App.GetOemInfo(VersionInfo.GetHomePath());

#if EXPRESS
            var exporter = new Bimangle.ForgeEngine.Navisworks.Express.Cesium3DTiles.Exporter(VersionInfo.GetHomePath());
#else
            var exporter = new Bimangle.ForgeEngine.Navisworks.Pro.Cesium3DTiles.Exporter(VersionInfo.GetHomePath());
#endif
            exporter.Export(setting, log, progressCallback, CancellationToken.None);

            //定制化输出成果
            VersionInfo.CustomOutputFor3DTiles(setting.OutputPath);
        }

        private GeoreferencedSetting GetGeoreferencedSetting(GeoreferencedSetting setting, IList<Common.Formats.Cesium3DTiles.FeatureType> features)
        {
            var adapter = new GeoreferncingAdapter(null);
            using (var gh = GeoreferncingHost.Create(adapter, VersionInfo.GetHomePath()))
            {
                var d = setting?.Clone() ?? gh.CreateDefaultSetting();
                var result = gh.CreateTargetSetting(d);
                result?.Fit(features);
                return result;
            }
        }
    }
}
