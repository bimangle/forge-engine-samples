using System;

namespace Bimangle.ForgeEngine.Dwg.App.Config
{
    [Serializable]
    class AppConfig
    {
        public AppLocalConfig Local { get; set; }

        public AppConfig()
        {
            Local = new AppLocalConfig();
        }

        public AppConfig Clone()
        {
            return new AppConfig
            {
                Local = Local == null ? new AppLocalConfig() : Local.Clone(),
            };
        }
    }
}
