using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing.Utility;

namespace Bimangle.ForgeEngine.Georeferncing.Models
{
    public class TransformP4Model : AbstractModel, IDataErrorInfo
    {
        private readonly IGeoreferncingHost _Host;
        private readonly ParameterProj _ParameterOriginal;
        private ParameterProj _Parameter;

        private readonly TestRunState _LastState = TestRunState.LastState;

        private bool _IsUsed;
        private string _Dx;
        private string _Dy;
        private string _Wz;
        private string _K;
        private string _ScalingFactor;

        private string _ModelN;
        private string _ModelE;
        private string _ModelH;

        private string _ProjectedN;
        private string _ProjectedE;
        private string _ProjectedH;

        private string _WorldLat;
        private string _WorldLon;
        private string _WorldHeight;

        private string _ProjCoordinateOffset;

        public TransformP4Model(IGeoreferncingHost host, ParameterProj parameter)
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _ParameterOriginal = parameter ?? throw new ArgumentNullException(nameof(parameter));
            _ParameterOriginal.CheckProjOffset();
            _Parameter = _ParameterOriginal.Clone();

            InitializeFields();
        }

        private void InitializeFields()
        {
            var offset = _Parameter.Offset ?? new double[7];

            // 转换参数
            SetField(ref _IsUsed, _Parameter.OffsetType == ProjOffsetType._2D_Params4, nameof(IsUsed));
            SetField(ref _Dx, offset[0].ToMetreString(), nameof(Dx));
            SetField(ref _Dy, offset[1].ToMetreString(), nameof(Dy));

            var rz = offset[5] * 648000.0 / Math.PI;
            SetField(ref _Wz, rz.GetDoubleString(6), nameof(Wz));

            var k = offset[6] * 1e6;
            SetField(ref _K, k.GetDoubleString(6), nameof(K));
            SetField(ref _ScalingFactor, (1.0 + offset[6]).GetDoubleString(12), nameof(ScalingFactor));

            // 测试输入（从上次状态恢复）
            ModelN = _LastState.Y;
            ModelE = _LastState.X;
            ModelH = _LastState.Z;

            // 测试输出（初始化为 NaN）
            var nan = double.NaN.ToString(CultureInfo.InvariantCulture);
            ProjectedN = nan;
            ProjectedE = nan;
            ProjectedH = nan;
            WorldLat = nan;
            WorldLon = nan;
            WorldHeight = nan;

            // 偏移量摘要
            ProjCoordinateOffset = _Parameter.GetOffsetString();
        }

        #region 属性 - 转换参数

        public bool IsUsed
        {
            get => _IsUsed;
            set
            {
                if (SetField(ref _IsUsed, value))
                {
                    if (value)
                    {
                        _Parameter.OffsetType = ProjOffsetType._2D_Params4;
                        if (_Parameter.Offset == null) _Parameter.Offset = new double[7];
                    }
                    else
                    {
                        _Parameter.OffsetType = ProjOffsetType.None;
                    }
                    RefreshOffsetText();
                }
            }
        }

        public string Dx
        {
            get => _Dx;
            set
            {
                if (SetField(ref _Dx, value))
                {
                    if (value.TryParseNumber(out _, out var v))
                    {
                        _Parameter.Offset[0] = v;
                        RefreshOffsetText();
                    }
                }
            }
        }

        public string Dy
        {
            get => _Dy;
            set
            {
                if (SetField(ref _Dy, value))
                {
                    if (value.TryParseNumber(out _, out var v))
                    {
                        _Parameter.Offset[1] = v;
                        RefreshOffsetText();
                    }
                }
            }
        }

        public string Wz
        {
            get => _Wz;
            set
            {
                if (SetField(ref _Wz, value))
                {
                    if (value.TryParseNumber(out _, out var v))
                    {
                        _Parameter.Offset[5] = v * Math.PI / 648000.0;
                        RefreshOffsetText();
                    }
                }
            }
        }

        public string K
        {
            get => _K;
            set
            {
                if (SetField(ref _K, value))
                {
                    if (value.TryParseNumber(out _, out var k))
                    {
                        var m = k * 1e-6;
                        _Parameter.Offset[6] = m;
                        ScalingFactor = (1.0 + m).GetDoubleString(12);
                        RefreshOffsetText();
                    }
                }
            }
        }

        public string ScalingFactor
        {
            get => _ScalingFactor;
            set => SetField(ref _ScalingFactor, value);
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

        #endregion

        #region 属性 - 其他

        public string ProjCoordinateOffset
        {
            get => _ProjCoordinateOffset;
            set => SetField(ref _ProjCoordinateOffset, value);
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

            // 更新转换参数
            if (IsUsed &&
                _Parameter.Offset != null &&
                _Parameter.Offset.Any(x => Math.Abs(x) > 1e-10))
            {
                _Parameter.OffsetType = ProjOffsetType._2D_Params4;
            }

            // 试算模型坐标
            var dataModel = new[] { localX, localY, localZ };
            var r = _Host.TestRun(_Parameter, dataModel, out var dataProjected, out var dataWorld);

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
            }
            else
            {
                WorldLon = dataWorld[1].ToLatLonString();
                WorldLat = dataWorld[0].ToLatLonString();
                WorldHeight = dataWorld[2].ToMetreString();
            }
        }

        public void SwapNE()
        {
            var temp = ModelN;
            ModelN = ModelE;
            ModelE = temp;
        }

        public void Restore()
        {
            _Parameter = _ParameterOriginal.Clone();
            InitializeFields();
        }

        public bool UpdateTo(ParameterProj target, out string firstError)
        {
            firstError = null;

            // 验证所有输入（不使用 ValueTuple，兼容 .NET 4.5）
            if (!string.IsNullOrEmpty(this[nameof(Dx)]))
            {
                firstError = nameof(Dx);
                return false;
            }
            if (!string.IsNullOrEmpty(this[nameof(Dy)]))
            {
                firstError = nameof(Dy);
                return false;
            }
            if (!string.IsNullOrEmpty(this[nameof(Wz)]))
            {
                firstError = nameof(Wz);
                return false;
            }
            if (!string.IsNullOrEmpty(this[nameof(K)]))
            {
                firstError = nameof(K);
                return false;
            }

            // 更新转换参数
            if (IsUsed &&
                _Parameter.Offset != null &&
                _Parameter.Offset.Any(x => Math.Abs(x) > 1e-10))
            {
                target.OffsetType = ProjOffsetType._2D_Params4;
                target.Offset = (double[])_Parameter.Offset.Clone();
            }
            else
            {
                target.OffsetType = ProjOffsetType.None;
            }

            return true;
        }

        private void RefreshOffsetText()
        {
            ProjCoordinateOffset = _Parameter.GetOffsetString();
        }

        #endregion

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Dx):
                        return Dx.TryParseNumber(out var error, out _) ? null : error;
                    case nameof(Dy):
                        return Dy.TryParseNumber(out error, out _) ? null : error;
                    case nameof(Wz):
                        return Wz.TryParseNumber(out error, out _) ? null : error;
                    case nameof(K):
                        return K.TryParseNumber(out error, out _) ? null : error;
                    case nameof(ModelN):
                        return ModelN.TryParseNumber(out error, out _) ? null : error;
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
