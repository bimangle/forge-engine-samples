using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace Bimangle.ForgeEngine.Dwg.CLI.Core
{
    [Serializable]
    public class Options
    {
        private const string FEATURE_HELP_TEXT = @"Translate features(GenerateModelsDb, GenerateThumbnail)";

        [Option('w', @"winform", HelpText = @"WinForm Mode")]
        public bool WinFormMode { get; set; }

        [Option('f', @"features", HelpText = FEATURE_HELP_TEXT, Separator = ',')]
        public IEnumerable<string> Features { get; set; }

        [Option('i', @"input", HelpText = @"Source file path")]
        public string InputFilePath { get; set; }

        [Option('o', @"output", HelpText = @"Output folder path")]
        public string OutputFolderPath { get; set; }

        [Usage(ApplicationAlias = "ForgeEngineDwgCLI.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Default", new Options { InputFilePath = @"d:\input.dwg", OutputFolderPath = @"d:\output\" });
            }
        }
    }
}
