using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace Bimangle.ForgeEngine.Skp.Core
{
    [Serializable]
    public class Options
    {
        public const string FORMAT_SVF = @"svf";
        public const string FORMAT_GLTF = @"gltf";
        public const string FORMAT_3DTILES = @"3dtiles";


        public const string DEFAULT_FORMAT = FORMAT_GLTF;

        private const string FEATURE_HELP_TEXT = @"Translate features(...)";

        [Option('w', @"winform", HelpText = @"WinForm Mode", Default = false)]
        public bool WinFormMode { get; set; }

        [Option(@"format", HelpText = @"Target data format", Default = @"svf")]
        public string Format { get; set; }

        [Option(@"mode", HelpText = @"Target data format convert mode", Default = 0)]
        public int Mode { get; set; }

        [Option('v', @"visualstyle", HelpText = @"Visual Style", Default = @"Textured")]
        public string VisualStyle { get; set; }

        [Option('l', @"levelofdetail", HelpText = @"Level Of Details", Default = -1)]
        public int LevelOfDetail { get; set; }

        [Option('f', @"features", HelpText = FEATURE_HELP_TEXT, Separator = ',')]
        public IEnumerable<string> Features { get; set; }

        [Option('g', @"georeferenced", HelpText = @"Georeferenced Setting (Base64)")]
        public string GeoreferencedBase64 { get; set; }

        [Option('i', @"input", HelpText = @"Source file path")]
        public string InputFilePath { get; set; }

        [Option('o', @"output", HelpText = @"Output folder path")]
        public string OutputFolderPath { get; set; }

        [Usage(ApplicationAlias = "ForgeEngineCLI.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Default", new Options { InputFilePath = @"d:\input.skp", OutputFolderPath = @"d:\output\" });
                yield return new Example("Exclude Properties", new Options { InputFilePath = @"d:\input.skp", OutputFolderPath = @"d:\output\", Features = new []{ @"ExcludeProperties" } });
            }
        }
    }
}
