using System;
using System.ComponentModel;
using System.Linq;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing.Utility;

namespace Bimangle.ForgeEngine.Georeferncing.Models
{
    public class GeoreferncingTestModel : AbstractModel, IDataErrorInfo
    {
        private readonly IGeoreferncingHost _Host;
        private readonly ParameterProj _Parameters;

        private readonly TestRunState _LastState = TestRunState.LastState;

        private string _ProjDefinition;
        private string _ProjCoordinateOffset;
        private string _ProjectionHeight;
        private string _GeoidGrid;
        private string _GeoidConstantOffset;

        private string _ModelN;
        private string _ModelE;
        private string _ModelH;

        private string _ProjectedN;
        private string _ProjectedE;
        private string _ProjectedH;

        private string _WorldLat;
        private string _WorldLon;
        private string _WorldHeight;
        private string _GeoidHeight;

        public GeoreferncingTestModel(IGeoreferncingHost host, ParameterProj parameters)
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

            InitializeFields();
        }

        private void InitializeFields()
        {
            // 投影参数（只读显示）
            ProjDefinition = _Parameters.Definition ?? string.Empty;
            ProjCoordinateOffset = _Parameters.GetOffsetString();
            ProjectionHeight = _Parameters.ProjectionHeight.ToMetreString();

            // 水准面网格
            var geoidGrids = _Host.GetVerticalGeoidGridItems();
            var geoidGrid = geoidGrids.FirstOrDefault(x => x.FileName == _Parameters.GeoidGrid) ??
                            geoidGrids.FirstOrDefault();
            GeoidGrid = geoidGrid?.ToString() ?? string.Empty;

            GeoidConstantOffset = _Parameters.GeoidConstantOffset.ToMetreString();

            // 测试输入（从上次状态恢复）
            ModelN = _LastState.Y;
            ModelE = _LastState.X;
            ModelH = _LastState.Z;

            // 测试输出（初始化为 NaN）
            var nan = double.NaN.ToString();
            ProjectedN = nan;
            ProjectedE = nan;
            ProjectedH = nan;
            WorldLat = nan;
            WorldLon = nan;
            WorldHeight = nan;
            GeoidHeight = nan;
        }

        #region 属性 - 投影参数（只读）

        public string ProjDefinition
        {
            get => _ProjDefinition;
            set => SetField(ref _ProjDefinition, value);
        }

        public string ProjCoordinateOffset
        {
            get => _ProjCoordinateOffset;
            set => SetField(ref _ProjCoordinateOffset, value);
        }

        public string ProjectionHeight
        {
            get => _ProjectionHeight;
            set => SetField(ref _ProjectionHeight, value);
        }

        public string GeoidGrid
        {
            get => _GeoidGrid;
            set => SetField(ref _GeoidGrid, value);
        }

        public string GeoidConstantOffset
        {
            get => _GeoidConstantOffset;
            set => SetField(ref _GeoidConstantOffset, value);
        }

        #endregion

        #region 属性 - 测试输入

        public string ModelN
        {
            get => _ModelN;
            set => SetField(ref _ModelN, value);
        }

        public string ModelE
        {
            get => _ModelE;
            set => SetField(ref _ModelE, value);
        }

        public string ModelH
        {
            get => _ModelH;
            set => SetField(ref _ModelH, value);
        }

        #endregion

        #region 属性 - 测试输出（只读）

        public string ProjectedN
        {
            get => _ProjectedN;
            set => SetField(ref _ProjectedN, value);
        }

        public string ProjectedE
        {
            get => _ProjectedE;
            set => SetField(ref _ProjectedE, value);
        }

        public string ProjectedH
        {
            get => _ProjectedH;
            set => SetField(ref _ProjectedH, value);
        }

        public string WorldLat
        {
            get => _WorldLat;
            set => SetField(ref _WorldLat, value);
        }

        public string WorldLon
        {
            get => _WorldLon;
            set => SetField(ref _WorldLon, value);
        }

        public string WorldHeight
        {
            get => _WorldHeight;
            set => SetField(ref _WorldHeight, value);
        }

        public string GeoidHeight
        {
            get => _GeoidHeight;
            set => SetField(ref _GeoidHeight, value);
        }

        #endregion

        #region 方法

        public void ExecuteTest()
        {
            // 解析模型坐标
            if (!ModelN.TryParseNumber(out var errorN, out var localY) ||
                !ModelE.TryParseNumber(out var errorE, out var localX) ||
                !ModelH.TryParseNumber(out var errorH, out var localZ))
            {
                return;
            }

            // 保存最后状态
            _LastState.Y = ModelN;
            _LastState.X = ModelE;
            _LastState.Z = ModelH;

            // 试算模型坐标
            var dataModel = new[] { localX, localY, localZ };
            var r = _Host.TestRun(_Parameters, dataModel, out var dataProjected, out var dataWorld);

            var nan = double.NaN.ToString();

            // 显示投影坐标
            if (!r || dataProjected == null)
            {
                ProjectedN = nan;
                ProjectedE = nan;
                ProjectedH = nan;
            }
            else
            {
                ProjectedN = dataProjected[1].ToMetreString();
                ProjectedE = dataProjected[0].ToMetreString();
                ProjectedH = dataProjected[2].ToMetreString();
            }

            // 显示世界坐标
            if (!r || dataWorld == null)
            {
                WorldLat = nan;
                WorldLon = nan;
                WorldHeight = nan;
                GeoidHeight = nan;
            }
            else
            {
                WorldLon = dataWorld[1].ToLatLonString();
                WorldLat = dataWorld[0].ToLatLonString();
                WorldHeight = dataWorld[2].ToMetreString();
                GeoidHeight = dataWorld.Length > 3 ? dataWorld[3].ToMetreString() : nan;
            }
        }

        public void SwapNE()
        {
            var temp = ModelN;
            ModelN = ModelE;
            ModelE = temp;
        }

        #endregion

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(ModelN):
                        return ModelN.TryParseNumber(out var error, out _) ? null : error;
                    case nameof(ModelE):
                        return ModelE.TryParseNumber(out error, out _) ? null : error;
                    case nameof(ModelH):
                        return ModelH.TryParseNumber(out error, out _) ? null : error;
                    default:
                        return null;
                }
            }
        }

        public string Error => null;

        #endregion
    }
}
