using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace Bimangle.ForgeEngine.Dwg.Core
{
    [Serializable]
    public class Options
    {
        public const string FORMAT_SVF = @"svf";
        public const string FORMAT_GLTF = @"gltf";
        public const string FORMAT_3DTILES = @"3dtiles";


        public const string DEFAULT_FORMAT = FORMAT_GLTF;

        private const string FEATURE_HELP_TEXT = @"Translate features(GenerateModelsDb, GenerateThumbnail)";

        [Option('w', @"winform", HelpText = @"WinForm Mode", Default = false)]
        public bool WinFormMode { get; set; }

        [Option(@"format", HelpText = @"Target data format", Default = @"svf")]
        public string Format { get; set; }

        [Option(@"mode", HelpText = @"Target data format convert mode", Default = 0)]
        public int Mode { get; set; }

        [Option('v', @"visualstyle", HelpText = @"Visual Style", Default = @"Wireframe")]
        public string VisualStyle { get; set; }

        [Option('f', @"features", HelpText = FEATURE_HELP_TEXT, Separator = ',')]
        public IEnumerable<string> Features { get; set; }

        [Option('g', @"georeferenced", HelpText = @"Georeferenced Setting (Base64)")]
        public string GeoreferencedBase64 { get; set; }

        [Option('i', @"input", HelpText = @"Source file path")]
        public string InputFilePath { get; set; }

        [Option('o', @"output", HelpText = @"Output path")]
        public string OutputPath { get; set; }

        public string OutputFolderPath
        {
            get => OutputPath;
            set => OutputPath = value;
        }

        [Usage(ApplicationAlias = "ForgeEngineCLI.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Default", new Options { InputFilePath = @"d:\input.dwg", OutputPath = @"d:\output\" });
            }
        }

        /// <summary>
        /// 获得输出文件夹路径
        /// </summary>
        /// <returns></returns>
        public string GetOutputFolderPath()
        {
            if (string.IsNullOrEmpty(OutputPath)) return null;

            var outputFolderPath = Format == FORMAT_GLTF
                ? Path.GetDirectoryName(OutputPath)
                : OutputPath;
            return outputFolderPath;
        }

        /// <summary>
        /// 解析获取特性列表
        /// </summary>
        /// <typeparam name="TFeatureType"></typeparam>
        /// <param name="defaultFeatures"></param>
        /// <returns></returns>
        public IList<TFeatureType> GetFeatures<TFeatureType>(IList<TFeatureType> defaultFeatures)
            where TFeatureType : struct
        {
            var features = new HashSet<TFeatureType>();

            var featureStrings = Features?.ToList();
            if (featureStrings != null && featureStrings.Count > 0)
            {
                foreach (var featureString in featureStrings)
                {
                    if (Enum.TryParse(featureString, true, out TFeatureType feature))
                    {
                        features.Add(feature);
                    }
                }
            }

            if (features.Any() == false && defaultFeatures != null)
            {
                foreach (var feature in defaultFeatures)
                {
                    features.Add(feature);
                }
            }

            return features.ToList();
        }
    }
}
