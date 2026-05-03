using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Bimangle.ForgeEngine.Common.Types;
using ItemValue = Bimangle.ForgeEngine.Georeferncing.Utility.ItemValue<Bimangle.ForgeEngine.Georeferncing.ProjBuilder.GeoGCS>;
using WpfTextHelper = Bimangle.ForgeEngine.Georeferncing.Utility.WpfTextHelper;

namespace Bimangle.ForgeEngine.Georeferncing.Models
{
    internal class ProjCreateModel : AbstractModel, IDataErrorInfo
    {
        private readonly IGeoreferncingHost _Host;

        private ProjBuilder.GeoGCS _Gcs;
        private string _CentralMeridian;
        private bool _IsPinned;
        private string _FalseEasting;
        private string _FalseNorthing;
        private string _Definition;
        private bool _ShouldValidateDefinition;
        private bool _IsDefinitionGenerated;

        public ProjCreateModel(IGeoreferncingHost host)
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));

            // 初始化默认值
            Gcs = ProjBuilder.GeoGCS.GCS_China_Geodetic_Coordinate_System_2000;

            var site = _Host.GetModelSiteInfo() ?? SiteInfo.CreateDefault();
            CentralMeridian = WpfTextHelper.ToLatLonString(ProjBuilder.GetCentralMeridian(site.Longitude));
            IsPinned = false;
            FalseEasting = WpfTextHelper.ToMetreString(500000.0);
            FalseNorthing = WpfTextHelper.ToMetreString(0.0);
            _ShouldValidateDefinition = false;
            Definition = string.Empty;
            IsDefinitionGenerated = false;
        }

        #region 属性

        public List<ItemValue> GcsItems { get; } = new List<ItemValue>
        {
            new ItemValue(GeoStrings.GcsCGCS2000, ProjBuilder.GeoGCS.GCS_China_Geodetic_Coordinate_System_2000),
            new ItemValue(GeoStrings.GcsWGS84, ProjBuilder.GeoGCS.GCS_WGS_1984),
            new ItemValue(GeoStrings.GcsXian1980, ProjBuilder.GeoGCS.GCS_Xian_1980),
            new ItemValue(GeoStrings.GcsBeijing1954, ProjBuilder.GeoGCS.GCS_Beijing_1954)
        };

        public ProjBuilder.GeoGCS Gcs
        {
            get => _Gcs;
            set => SetField(ref _Gcs, value);
        }

        public string CentralMeridian
        {
            get => _CentralMeridian;
            set => SetField(ref _CentralMeridian, value);
        }

        public bool IsPinned
        {
            get => _IsPinned;
            set => SetField(ref _IsPinned, value);
        }

        public string FalseEasting
        {
            get => _FalseEasting;
            set => SetField(ref _FalseEasting, value);
        }

        public string FalseNorthing
        {
            get => _FalseNorthing;
            set => SetField(ref _FalseNorthing, value);
        }

        public string Definition
        {
            get => _Definition;
            set
            {
                if (SetField(ref _Definition, value))
                {
                    OnPropertyChanged(nameof(DefinitionForeground));
                }
            }
        }

        public System.Windows.Media.Brush DefinitionForeground
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Definition))
                    return System.Windows.Media.Brushes.Gray;

                if (_Host.CheckProjDefinition(Definition, out _))
                    return System.Windows.Media.Brushes.Green;

                return System.Windows.Media.Brushes.Red;
            }
        }

        public bool IsDefinitionGenerated
        {
            get => _IsDefinitionGenerated;
            set => SetField(ref _IsDefinitionGenerated, value);
        }

        #endregion

        #region 回调函数（由 View 注册）

        public Func<double, double, double, double, bool> RequestCalcByRefPoint { get; set; }

        #endregion

        #region 方法

        public void CalcByRefPoint()
        {
            if (RequestCalcByRefPoint == null) return;

            double temp;
            if (!WpfTextHelper.TryParseLongitude(CentralMeridian, out _, out temp))
            {
                return;
            }
            var cm = temp;

            var centralMeridian = IsPinned ? (double?)cm : null;

            if (RequestCalcByRefPoint(centralMeridian ?? 0, 0, 0, 0))
            {
                // 参数已由回调更新
                OnPropertyChanged(nameof(CentralMeridian));
                OnPropertyChanged(nameof(FalseEasting));
                OnPropertyChanged(nameof(FalseNorthing));
            }
        }

        public void GenerateDefinition()
        {
            double centralMeridian, falseEasting, falseNorthing;
            if (!WpfTextHelper.TryParseLongitude(CentralMeridian, out _, out centralMeridian) ||
                !WpfTextHelper.TryParseNumber(FalseEasting, out _, out falseEasting) ||
                !WpfTextHelper.TryParseNumber(FalseNorthing, out _, out falseNorthing))
            {
                return;
            }

            var proj = new ProjBuilder.ProjDefinition
            {
                GeoGCS = Gcs,
                CentralMeridian = centralMeridian,
                FalseEasting = falseEasting,
                FalseNorthing = falseNorthing
            };

            Definition = proj.ToWKT();
            IsDefinitionGenerated = true;
        }

        public bool ValidateAndGetResult(out string definition, out string firstError)
        {
            definition = null;
            firstError = null;

            EnableDefinitionValidation();

            // 验证定义
            if (string.IsNullOrWhiteSpace(Definition))
            {
                firstError = nameof(Definition);
                return false;
            }

            if (!_Host.CheckProjDefinition(Definition, out _))
            {
                firstError = nameof(Definition);
                return false;
            }

            definition = Definition;
            return true;
        }

        private void EnableDefinitionValidation()
        {
            if (_ShouldValidateDefinition)
            {
                return;
            }

            _ShouldValidateDefinition = true;
            OnPropertyChanged(nameof(Definition));
        }

        #endregion

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                string error;
                double temp;
                switch (columnName)
                {
                    case nameof(CentralMeridian):
                        return WpfTextHelper.TryParseLongitude(CentralMeridian, out error, out temp) ? null : error;
                    case nameof(FalseEasting):
                        return WpfTextHelper.TryParseNumber(FalseEasting, out error, out temp) ? null : error;
                    case nameof(FalseNorthing):
                        return WpfTextHelper.TryParseNumber(FalseNorthing, out error, out temp) ? null : error;
                    case nameof(Definition):
                        if (!_ShouldValidateDefinition) return null;
                        if (string.IsNullOrWhiteSpace(Definition)) return GeoStrings.ProjDefinitionIsRequired;
                        if (!_Host.CheckProjDefinition(Definition, out var msg)) return msg;
                        return null;
                    default:
                        return null;
                }
            }
        }

        public string Error => null;

        #endregion
    }
}
