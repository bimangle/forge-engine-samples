using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Navisworks.Config;

namespace Bimangle.ForgeEngine.Navisworks
{
    static class Program
    {
        const string NAVISWORKS_FOLDER = @"C:\Program Files\Autodesk\Navisworks Manage 2018";
        const string PLUGIN_ID = @"EngineBatch_Sample." + VersionInfo.COMPANY_ID;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            if (Directory.Exists(NAVISWORKS_FOLDER) == false)
            {
                throw new Exception(@"Navisworks folder path invalid!");
            }

            var inputFilePath = @"E:\rasp_r2017.rvt";
            if (File.Exists(inputFilePath) == false)
            {
                throw new Exception(@"Source model file path invalid!");
            }

            TestExportSvf(NAVISWORKS_FOLDER, inputFilePath);
            TestExportGltf(NAVISWORKS_FOLDER, inputFilePath);
            TestExport3DTiles(NAVISWORKS_FOLDER, inputFilePath);
        }

        private static void TestExportSvf(string nwFolderPath, string inputFilePath)
        {
            var outputPath = Path.Combine(Application.StartupPath, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            if (Directory.Exists(outputPath) == false) Directory.CreateDirectory(outputPath);

            //reference features parameter from "Engine NW CLI"
            var features = new List<string>
            {
                "GenerateModelsDb",
                "GenerateThumbnail"
            };

            Export(nwFolderPath, inputFilePath, outputPath, @"svf", features);
        }

        private static void TestExportGltf(string nwFolderPath, string inputFilePath)
        {
            var outputPath = Path.Combine(Application.StartupPath, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            if (Directory.Exists(outputPath) == false) Directory.CreateDirectory(outputPath);

            //reference features parameter from "Engine NW CLI"
            var features = new List<string>
            {
                "GenerateModelsDb"
            };

            Export(nwFolderPath, inputFilePath, Path.Combine(outputPath, @"model.gltf"), @"gltf", features);
        }

        private static void TestExport3DTiles(string nwFolderPath, string inputFilePath)
        {
            var outputPath = Path.Combine(Application.StartupPath, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            if (Directory.Exists(outputPath) == false) Directory.CreateDirectory(outputPath);

            //reference features parameter from "Engine NW CLI"
            var features = new List<string>
            {
                "ExcludeTexture",
                "GenerateModelsDb",
                "EnableQuantizedAttributes",
                "EnableTextureWebP"
            };

            Export(nwFolderPath, inputFilePath, outputPath, @"3dtiles", features);
        }


        private static bool Export(string nwFolderPath, string inputFilePath, string outputPath, string format, List<string> features)
        {
            const string TYPE_NAME = @"Autodesk.Navisworks.Api.Automation.NavisworksApplication";

            //create job config
            var job = new JobConfig();
            job.Format = format; //format: "svf","gltf","3dtiles"
            job.OutputPath = outputPath;
            job.OutputOptions = features;  //reference features parameter from "Engine NW CLI"

            //save job config
            var jobFilePath = Path.Combine(Path.GetTempPath(), DateTime.Now.ToString("yyyyMMddHHmmssfff") + @".json");
            job.Save(jobFilePath);

            var result = false;

            try
            {
                Trace.WriteLine("\tLaunch Autodesk Navisworks Automation ...");
                var automationFilePath = Path.Combine(nwFolderPath, @"Autodesk.Navisworks.Automation.dll");
                var assembly = Assembly.LoadFile(automationFilePath);
                dynamic app = assembly.CreateInstance(TYPE_NAME, true);
                Trace.WriteLine("\tLaunch Autodesk Navisworks Automation succeeded!");

                try
                {
                    Trace.WriteLine("\tOpen source file ...");
                    app.Visible = false;
                    app.DisableProgress();
                    app.AppendFile(inputFilePath);
                    Trace.WriteLine("\tOpen source file succeeded!");

                    Trace.WriteLine("\tExecute exporting ...");
                    var ret = (int)app.ExecuteAddInPlugin(PLUGIN_ID, jobFilePath);
                    switch (ret)
                    {
                        case 0:
                            Trace.WriteLine("\tExecute exporting succeeded!");
                            result = true;
                            break;
                        case 101:
                            Trace.WriteLine("\tExecute exporting fail, parameter invalid!");
                            break;
                        case 102:
                            Trace.WriteLine("\tExecute exporting fail, can't load job config!");
                            break;
                        case 110:
                            Trace.WriteLine("\tExecute exporting fail, license Invalid!");
                            break;
                        default:
                            Trace.WriteLine("\tExecute exporting fail, unknown exception!");
                            break;
                    }
                }
                finally
                {
                    app?.Dispose();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return result;
        }
    }
}
