using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Architecture;
using Bimangle.ForgeEngine.Revit.Api;

namespace Bimangle.ForgeEngine.Revit.Custom
{
    class LevelParser : ILevelParser
    {
        /// <summary>
        /// 标高原始信息列表
        /// </summary>
        private List<LevelInfo> _LevelBaseInfos;

        /// <summary>
        /// 标高信息列表
        /// </summary>
        private List<LevelInfo> _LevelInfos;


        public LevelParser()
        {
        }

        public void Init(Document document)
        {
            using (var collector = new FilteredElementCollector(document).OfClass(typeof(Level)))
            {
                //先尝试获取建筑楼层, 如果建筑楼层不存在则获取所有标高作为楼层

                _LevelBaseInfos = new List<LevelInfo>();
                foreach (var level in collector.OfType<Level>())
                {
                    var p = level.get_Parameter(BuiltInParameter.LEVEL_IS_BUILDING_STORY);
                    if (p?.AsInteger() == 1)
                    {
                        _LevelBaseInfos.Add(new LevelInfo(level, level.Elevation - 0.000001));  //加上一个容差量，避免浮点精度问题
                    }
                }

                if (_LevelBaseInfos.Count == 0)
                {
                    foreach (var level in collector.OfType<Level>())
                    {
                        _LevelBaseInfos.Add(new LevelInfo(level, level.Elevation - 0.000001));  //加上一个容差量，避免浮点精度问题
                    }
                }
            }

            RefreshLevelInfo();
        }

        public Level Parse(Element element)
        {
            return GetLevel(element.Document, element);
        }

        /// <summary>
        /// 确定构件所属标高
        /// </summary>
        /// <param name="document"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private Level GetLevel(Document document, Element element)
        {
            switch (element)
            {
                case FamilyInstance v:
                    {
                        var host = SafeGetHost(v);
                        if (host != null) return GetLevel(document, host);
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                case FlexDuct v:
                    {
                        //因为 FlexDuct 获取 LevelOffset 时会抛异常, 所以这里单独处理
                        if (v.ReferenceLevel != null) return v.ReferenceLevel;
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                case MEPCurve v:
                    {
                        if (v.ReferenceLevel != null) return v.ReferenceLevel;
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                case Level v:
                    {
                        return v;
                    }
                case Opening v:
                    {
                        var host = SafeGetHost(v);
                        if (host != null) return GetLevel(document, host);
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                case DividedSurface v:
                    {
                        var host = SafeGetHost(v);
                        if (host != null) return GetLevel(document, host);
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                case Railing v:
                    {
                        if (v.HostId != ElementId.InvalidElementId)
                        {
                            var host = document.GetElement(v.HostId);
                            if (host != null) return GetLevel(document, host);
                        }
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                case BuildingPad v:
                    {
                        if (v.HostId != ElementId.InvalidElementId)
                        {
                            var host = document.GetElement(v.HostId);
                            if (host != null) return GetLevel(document, host);
                        }
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                case FabricSheet v:
                    {
#if !R2014
                        if (v.HostId != ElementId.InvalidElementId)
                        {
                            var host = document.GetElement(v.HostId);
                            if (host != null) return GetLevel(document, host);
                        }
#endif
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                case FabricArea v:
                    {
                        if (v.HostId != ElementId.InvalidElementId)
                        {
                            var host = document.GetElement(v.HostId);
                            if (host != null) return GetLevel(document, host);
                        }
                        if (CheckElementLevelId(v)) return document.GetElement(v.LevelId) as Level;
                        break;
                    }
                default:
                    {
                        if (CheckElementLevelId(element)) return document.GetElement(element.LevelId) as Level;
                        break;
                    }
            }


            var refLevelId = CheckElementRefLevelId(element);
            if (refLevelId != ElementId.InvalidElementId) return document.GetElement(refLevelId) as Level;

            #region 根据包围盒计算标高

            {
                Level level = null;
                var boundingBox = element.get_BoundingBox(null);
                if (boundingBox != null && boundingBox.get_MinEnabled(2))
                {
                    if (_LevelInfos == null)
                    {
                        RefreshLevelInfo();
                    }

                    //根据包围盒计算标高
                    var z = boundingBox.Min.Z;
                    var levelInfo = _LevelInfos.FirstOrDefault(x => z >= x.Elevation && z < x.ElevationMax);
                    if (levelInfo != null)
                    {
                        level = levelInfo.Element;
                    }
                }
                return level;
            }

            #endregion
        }


        private bool CheckElementLevelId(Element element)
        {
            if (element.LevelId == ElementId.InvalidElementId) return false;
            return true;
        }

        private ElementId CheckElementRefLevelId(Element element)
        {
            var levelParameter = element.get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM);
            if (levelParameter == null || levelParameter.HasValue == false) return ElementId.InvalidElementId;

            return levelParameter.AsElementId();
        }

        private void RefreshLevelInfo()
        {
            _LevelInfos = _LevelBaseInfos.OrderBy(x => x.Elevation).ToList();
            _LevelInfos.Insert(0, new LevelInfo(null, double.MinValue));

            for (var i = 0; i < _LevelInfos.Count; i++)
            {
                _LevelInfos[i].ElevationMax = i == _LevelInfos.Count - 1
                    ? double.MaxValue
                    : _LevelInfos[i + 1].Elevation;
            }

            _LevelInfos = _LevelInfos.OrderByDescending(x => x.Elevation).ToList();
        }


        protected Element SafeGetHost(FamilyInstance element)
        {
            var host = element.Host;
            if (host != null && host.Id == element.Id)
            {
                //某些情况下, 构件的 Host 就是它自己, 会导致无限循环递归调用, 直至堆栈溢出
                host = null;
            }
            return host;
        }

        protected Element SafeGetHost(DividedSurface element)
        {
            var host = element.Host;
            if (host != null && host.Id == element.Id)
            {
                //某些情况下, 构件的 Host 就是它自己, 会导致无限循环递归调用, 直至堆栈溢出
                host = null;
            }
            return host;
        }

        protected Element SafeGetHost(Opening element)
        {
            var host = element.Host;
            if (host != null && host.Id == element.Id)
            {
                //某些情况下, 构件的 Host 就是它自己, 会导致无限循环递归调用, 直至堆栈溢出
                host = null;
            }
            return host;
        }

        private class LevelInfo
        {
            public Level Element { get; }

            public double Elevation { get; }

            public double ElevationMax { get; set; }

            public LevelInfo(Level element, double elevation)
            {
                Element = element;
                Elevation = elevation;
            }

            public override string ToString()
            {
                return $@"{Element?.Name ?? @"<null>"}({Elevation} - {ElevationMax})";
            }
        }

    }
}
