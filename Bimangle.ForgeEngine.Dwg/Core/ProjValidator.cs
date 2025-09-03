using System;
using System.Diagnostics;
using System.IO;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.Libs.Proj;

namespace Bimangle.ForgeEngine.Dwg.Core
{
    class ProjValidator : IProj
    {
        /// <summary>
        /// 获取 Proj 的搜索路径
        /// </summary>
        /// <param name="homeFolder"></param>
        /// <returns></returns>
        public static string GetSearchPath(string homeFolder)
        {
            var searchPath = Path.Combine(homeFolder, @"Common", @"Proj");
            return searchPath;
        }

        public static ProjValidator Create(string homeFolder)
        {
            try
            {
                var searchPath = GetSearchPath(homeFolder);
                var projDbFilePath = Path.Combine(searchPath, @"proj.db");
                if (File.Exists(projDbFilePath) == false)
                {
                    Trace.WriteLine($@"Missing file {projDbFilePath}");
                    return null;
                }

                return new ProjValidator(searchPath);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return null;
            }
        }

        private readonly string _SearchPath;
        private ProjContext _Context;
        private bool _Init;

        public ProjContext Context
        {
            get
            {
                Init();
                return _Context;
            }
        }

        public ProjValidator(string searchPath)
        {
            _SearchPath = searchPath;
            _Context = null;
            _Init = false;
        }

        public bool Check(string definition, out string wkt)
        {
            wkt = null;
            if (string.IsNullOrWhiteSpace(definition)) return false;
            if (Context == null) return false;

            try
            {
                var proj = Context.Create(definition);
                if (proj == null) return false;

                using (proj)
                {
                    var type = proj.GetProjType();
                    if (type == ProjType.PROJECTED_CRS || 
                        type == ProjType.COMPOUND_CRS)
                    {
                        wkt = Context.AsWKT(proj);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return false;
        }

        public void Init()
        {
            if (_Init) return;

            lock (this)
            {
                if (_Init == false)
                {
                    try
                    {
                        _Context = new ProjContext(_SearchPath);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                    }

                    _Init = true;
                }
            }
        }

        public double[] Calculate(string projDef, double lon, double lat)
        {
            if (Context == null) return null;

            using (var wgs84 = Context.Create(@"EPSG:4326"))
            using (var projcs = Context.Create(projDef))
            using (var conv = Context.Create(wgs84, projcs, true))
            {
                var r = conv.Trans(new[] { lon, lat, 0.0 });
                return r;
            }
        }

        #region IDisposable

        public void Dispose()
        {
            _Context?.Dispose();
            _Context = null;
        }

        #endregion
    }
}
