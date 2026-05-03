using System;
using System.ComponentModel;
using Bimangle.ForgeEngine.Georeferncing.Utility;

namespace Bimangle.ForgeEngine.Georeferncing.Models
{
    public class ProjParamCalcModel : AbstractModel, IDataErrorInfo
    {
        private readonly IGeoreferncingHost _Host;

        private string _RefPointLocalX;
        private string _RefPointLocalY;
        private string _RefPointGeoLat;
        private string _RefPointGeoLon;
        private bool _ShouldValidateGeoCoordinate;

        public ProjParamCalcModel(IGeoreferncingHost host)
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));

            // 初始化默认值
            RefPointLocalX = "0";
            RefPointLocalY = "0";
            _ShouldValidateGeoCoordinate = false;
            RefPointGeoLat = string.Empty;
            RefPointGeoLon = string.Empty;
        }

        #region 属性

        public string RefPointLocalX
        {
            get => _RefPointLocalX;
            set => SetField(ref _RefPointLocalX, value);
        }

        public string RefPointLocalY
        {
            get => _RefPointLocalY;
            set => SetField(ref _RefPointLocalY, value);
        }

        public string RefPointGeoLat
        {
            get => _RefPointGeoLat;
            set => SetField(ref _RefPointGeoLat, value);
        }

        public string RefPointGeoLon
        {
            get => _RefPointGeoLon;
            set => SetField(ref _RefPointGeoLon, value);
        }

        #endregion

        #region 回调函数（由 View 注册）

        public Action RequestPickPosition { get; set; }

        #endregion

        #region 方法

        public void PickPosition()
        {
            RequestPickPosition?.Invoke();
        }

        public bool ValidateAndGetResult(out double localX, out double localY, 
                                          out double lat, out double lon, 
                                          out string firstError)
        {
            localX = localY = lat = lon = 0;
            firstError = null;

            EnableGeoCoordinateValidation();

            // 验证 LocalY
            if (!RefPointLocalY.TryParseNumber(out var error, out localY))
            {
                firstError = nameof(RefPointLocalY);
                return false;
            }

            // 验证 LocalX
            if (!RefPointLocalX.TryParseNumber(out error, out localX))
            {
                firstError = nameof(RefPointLocalX);
                return false;
            }

            // 验证 Latitude
            if (!RefPointGeoLat.TryParseLatitude(out error, out lat))
            {
                firstError = nameof(RefPointGeoLat);
                return false;
            }

            // 验证 Longitude
            if (!RefPointGeoLon.TryParseLongitude(out error, out lon))
            {
                firstError = nameof(RefPointGeoLon);
                return false;
            }

            return true;
        }

        private void EnableGeoCoordinateValidation()
        {
            if (_ShouldValidateGeoCoordinate)
            {
                return;
            }

            _ShouldValidateGeoCoordinate = true;
            OnPropertyChanged(nameof(RefPointGeoLat));
            OnPropertyChanged(nameof(RefPointGeoLon));
        }

        #endregion

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(RefPointLocalX):
                        return RefPointLocalX.TryParseNumber(out var error, out _) ? null : error;
                    case nameof(RefPointLocalY):
                        return RefPointLocalY.TryParseNumber(out error, out _) ? null : error;
                    case nameof(RefPointGeoLat):
                        if (!_ShouldValidateGeoCoordinate) return null;
                        return RefPointGeoLat.TryParseLatitude(out error, out _) ? null : error;
                    case nameof(RefPointGeoLon):
                        if (!_ShouldValidateGeoCoordinate) return null;
                        return RefPointGeoLon.TryParseLongitude(out error, out _) ? null : error;
                    default:
                        return null;
                }
            }
        }

        public string Error => null;

        #endregion
    }
}
