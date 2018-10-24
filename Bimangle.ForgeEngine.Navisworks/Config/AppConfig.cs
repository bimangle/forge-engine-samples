using System;

namespace Bimangle.ForgeEngine.Navisworks.Config
{
    [Serializable]
    class AppConfig
    {
        public AppConfigSvf Svf { get; set; }
        public AppConfigGltf Gltf { get; set; }

        public AppConfig()
        {
            Svf = new AppConfigSvf();
            Gltf = new AppConfigGltf();
        }

        public AppConfig Clone()
        {
            return new AppConfig
            {
                Svf = Svf == null ? new AppConfigSvf() : Svf.Clone(),
                Gltf = Gltf == null ? new AppConfigGltf() : Gltf.Clone(),
            };
        }
    }
}
