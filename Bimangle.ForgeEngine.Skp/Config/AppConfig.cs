﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Bimangle.ForgeEngine.Skp.Config
{
    [Serializable]
    class AppConfig
    {
        public AppConfigSvf Svf { get; set; }
        public AppConfigGltf Gltf { get; set; }
        public AppConfigCesium3DTiles Cesium3DTiles { get; set; }

        public AppConfig()
        {
            Svf = new AppConfigSvf();
            Gltf = new AppConfigGltf();
            Cesium3DTiles = new AppConfigCesium3DTiles();
        }

        public AppConfig Clone()
        {
            return new AppConfig
            {
                Svf = Svf == null ? new AppConfigSvf() : Svf.Clone(),
                Gltf = Gltf == null ? new AppConfigGltf() : Gltf.Clone(),
                Cesium3DTiles = Cesium3DTiles == null ? new AppConfigCesium3DTiles() : Cesium3DTiles.Clone()
            };
        }
    }
}
