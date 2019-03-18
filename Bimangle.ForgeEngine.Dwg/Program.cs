using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Dwg.CLI.Core;
using Bimangle.ForgeEngine.Dwg.CLI.Core.Log;
using Bimangle.ForgeEngine.Dwg;
using Bimangle.ForgeEngine.Dwg.CLI.Config;
using CommandLine;

namespace Bimangle.ForgeEngine.Dwg.CLI
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern Boolean FreeConsole();

        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                return Run(null);
            }

            return Parser.Default.ParseArguments<Options>(args).MapResult(Run, _ => 1);
        }

        private static int Run(Options options)
        {
            if (options == null || options.WinFormMode)
            {
                FreeConsole();

                var appConfig = AppConfigManager.Load();

                if (options != null)
                {
                    ApplyOptionsToConfig(options, appConfig);
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new UI.FormExport(appConfig, options));

                return 0;
            }

            //打印版本信息
            {
                var assembly = Assembly.GetExecutingAssembly();
                var version = PackageInfo.ProductVersion.ToString();
                var title = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
                var copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
                Console.WriteLine($@"{title} {version}");
                Console.WriteLine(copyright);
                Console.WriteLine();
            }

            //如果参数不足则报错并给出提示
            if (options.InputFilePath == null || options.OutputFolderPath == null)
            {
                Console.WriteLine(@"ERROR(S):");
                if (options.InputFilePath == null)
                {
                    Console.WriteLine(@"  Required option 'i, input' is missing.");
                }
                if (options.OutputFolderPath == null)
                {
                    Console.WriteLine(@"  Required option 'o, output' is missing.");
                }

                Console.WriteLine();
                Console.WriteLine(@"SHOW HELP:");
                Console.WriteLine($@"  {Path.GetFileName(Assembly.GetExecutingAssembly().Location)} --help");
                Console.WriteLine();

                return 1;
            }

            var ret = ForgeEngineCLI.Run(options, new LogConsole(string.Empty));
            return ret;
        }

        private static void ApplyOptionsToConfig(Options options, AppConfig appConfig)
        {
            var settings = Properties.Settings.Default;
            settings.OptionsInputFilePath = options.InputFilePath ?? settings.OptionsInputFilePath;
            settings.Save();

            ApplyOptionsToConfigSvf(options, appConfig.Svf);
        }

        private static void ApplyOptionsToConfigSvf(Options options, AppConfigSvf config)
        {
            config.LastTargetPath = options.OutputFolderPath ?? config.LastTargetPath;

            if (options.Features != null && options.Features.Any())
            {
                config.Features = new List<Common.Formats.Svf.Dwg.FeatureType>();

                foreach (var feature in options.Features)
                {
                    if (Enum.TryParse(feature, true,
                        out Common.Formats.Svf.Dwg.FeatureType value))
                    {
                        config.Features.Add(value);
                    }
                }
            }
        }
    }
}
