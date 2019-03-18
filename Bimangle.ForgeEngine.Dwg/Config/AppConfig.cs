using System;

namespace Bimangle.ForgeEngine.Dwg.CLI.Config
{
    [Serializable]
    class AppConfig
    {
        public AppConfigSvf Svf { get; set; }

        public AppConfig()
        {
            Svf = new AppConfigSvf();
        }

        public AppConfig Clone()
        {
            return new AppConfig
            {
                Svf = Svf == null ? new AppConfigSvf() : Svf.Clone(),
            };
        }
    }
}
