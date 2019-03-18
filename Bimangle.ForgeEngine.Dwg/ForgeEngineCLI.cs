using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Bimangle.ForgeEngine.Dwg.CLI.Core;
using Bimangle.ForgeEngine.Dwg.CLI.Core.Log;
using Bimangle.ForgeEngine.Dwg;

namespace Bimangle.ForgeEngine.Dwg.CLI
{
    [Serializable]
    public static class ForgeEngineCLI
    {
        private static int _NextJobId;

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="options"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static int RunEx(Options options, ILog log)
        {
            if (options == null) new ArgumentNullException(nameof(options));

            try
            {
                var job = new Job();
                return job.Run(options, log);
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.ToString());
                return 3;
            }
        }

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="options"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static int Run(Options options, ILog log)
        {
            if (options == null) new ArgumentNullException(nameof(options));

            //使用 AppDomain 来隔离每个执行的任务，确保不发生相互干扰
            AppDomain ad = null;
            try
            {
                var assemblyName = Assembly.GetExecutingAssembly().Location;
                var typeName = typeof(Job).FullName ?? string.Empty;

                ad = AppDomain.CreateDomain($@"ForgeEngineDwgJob_{++_NextJobId}");
                ad.Load(Assembly.GetExecutingAssembly().GetName());
                var job = ad.CreateInstanceFromAndUnwrap(assemblyName, typeName) as Job;
                return job?.Run(options, log) ?? 2;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                log.WriteLine(ex.ToString());
                return 3;
            }
            finally
            {
                try
                {
                    if (ad != null) AppDomain.Unload(ad);
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// 软件版本
        /// </summary>
        public static string Version => PackageInfo.ProductVersion.ToString();
    }
}
