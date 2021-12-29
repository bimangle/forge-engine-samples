using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Skp.Config;

namespace Bimangle.ForgeEngine.Skp.Core
{
    class GeoreferncingAdapter : Bimangle.ForgeEngine.Georeferncing.Interface.Adapter
    {
        private readonly AppConfigCesium3DTiles _LocalData;

        public GeoreferncingAdapter(string filePath, AppConfigCesium3DTiles localData)
            : base(filePath)
        {
            _LocalData = localData;
        }


        #region Overrides of Adapter

        public override IProj CreateProj(string homeFolder)
        {
            return ProjValidator.Create(homeFolder);
        }

        public override IDictionary<string, string> GetRecentlyProjFiles()
        {
            var results = new Dictionary<string, string>();

            if (_LocalData?.RecentlyProjFiles != null)
            {
                var count = 0;
                foreach (var projFilePath in _LocalData.RecentlyProjFiles)
                {
                    if (results.ContainsKey(projFilePath)) continue;

                    var projDefinition = Host.GetProjDefinition(projFilePath);
                    if (string.IsNullOrWhiteSpace(projDefinition) == false)
                    {
                        results.Add(projFilePath, projDefinition);

                        if (++count > 10) break;    //最多获取最近 10 个
                    }
                }
            }

            return results;
        }

        public override bool CheckInProjFile(string filePath)
        {
            if (_LocalData == null) return false;
            if (string.IsNullOrWhiteSpace(filePath) || File.Exists(filePath) == false) return false;

            var projDefinition = Host.GetProjDefinition(filePath);
            if (string.IsNullOrWhiteSpace(projDefinition)) return false;

            if (_LocalData.RecentlyProjFiles == null)
            {
                _LocalData.RecentlyProjFiles = new List<string>();
            }

            var i = _LocalData.RecentlyProjFiles.IndexOf(filePath);
            if (i >= 0)
            {
                _LocalData.RecentlyProjFiles.RemoveAt(i);
            }

            _LocalData.RecentlyProjFiles.Insert(0, filePath);
            return true;
        }

        public override bool IsLocal()
        {
            return true;
        }

        public override void SetDirectionLetters(Label lblLocalX, Label lblLocalY)
        {
            //do nothing
        }

        public override bool IsTrueNorth(OriginType originType)
        {
            return true;
        }

        public override string GetLocalString(OriginType originType)
        {
            if (originType == OriginType.Internal)
            {
                return Strings.OriginTypeInternal;
            }
            return base.GetLocalString(originType);
        }

        public override OriginType[] GetSupportOriginTypes()
        {
            return new[] { OriginType.Internal };
        }

        #endregion
    }
}
