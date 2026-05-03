using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Georeferncing.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Georeferncing.Models
{
    public class ParameterEnuModel : AbstractModel, IDataErrorInfo
    {
        private readonly GeoreferencedSetting _Setting;
        private readonly IGeoreferncingHost _Host;
        private readonly ParameterEnu _LocalSetting;

        private readonly SiteInfo _SiteInfo = null;
        private readonly double[] _DefaultModelOrigin;

        //本地字段 - 用于数据绑定:

        private OriginType _Origin;

        private bool _AlignOriginToSitePlaneCenter;
        private string _ModelOriginN;
        private string _ModelOriginE;
        private string _ModelOriginH;

        private bool _UseProjectLocation;
        private string _Latitude;
        private string _Longitude;
        private string _Height;
        private string _Rotation;

        private bool _UseAutoAlignToGround;

        public ParameterEnuModel(GeoreferencedSetting setting, IGeoreferncingHost host)
        {
            _Setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _LocalSetting = _Setting.Enu;

            _SiteInfo = _Host.GetModelSiteInfo();
            _DefaultModelOrigin = _Host.GetDefaultModelOrigin();

            //规范化 OriginOffset 字段
            CompatibilityFixForLegacyOffset();

            //更新本地字段
            UpdateLocalFields();
        }

        private void CompatibilityFixForLegacyOffset()
        {
            //规范化 OriginOffset 字段
            if (_LocalSetting.OriginOffset == null)
            {
                _LocalSetting.OriginOffset = new[] { 0.0, 0.0, 0.0 };
            }
            else if (_LocalSetting.OriginOffset.Length < 3)
            {
                var originOffset = _LocalSetting.OriginOffset;
                _LocalSetting.OriginOffset = new[] { 0.0, 0.0, 0.0 };
                if (originOffset.Length > 0) _LocalSetting.OriginOffset[0] = originOffset[0];
                if (originOffset.Length > 1) _LocalSetting.OriginOffset[1] = originOffset[1];
                if (originOffset.Length > 2) _LocalSetting.OriginOffset[2] = originOffset[2];
            }
        }

        /// <summary>
        /// 更新本地字段
        /// </summary>
        private void UpdateLocalFields()
        {
            var p = _LocalSetting;

            //模型坐标系
            Origin = p.Origin;

            //站心模型坐标
            AlignOriginToSitePlaneCenter = p.AlignOriginToSitePlaneCenter;
            // setter 会调用 UpdateModelOriginStrings() 使用 _DefaultModelOrigin，
            // 若设置了 OriginOffset 则用保存的值覆盖
            if (!p.AlignOriginToSitePlaneCenter && p.OriginOffset?.Length >= 3)
            {
                ModelOriginN = p.OriginOffset[1].ToMetreString();
                ModelOriginE = p.OriginOffset[0].ToMetreString();
                ModelOriginH = p.OriginOffset[2].ToMetreString();
            }

            //站心地理坐标
            UseProjectLocation = p.UseProjectLocation && HasSiteInfo;
            Latitude = p.Latitude.ToLatLonString();
            Longitude = p.Longitude.ToLatLonString();
            Height = p.Height.ToMetreString();
            Rotation = p.Rotation.ToDegreeString();

            //高级
            UseAutoAlignToGround = p.UseAutoAlignToGround;
        }

        /// <summary>
        /// 更新站心模型坐标文本
        /// </summary>
        private void UpdateModelOriginStrings()
        {
            if (AlignOriginToSitePlaneCenter)
            {
                ModelOriginN = GeoStrings.Auto;
                ModelOriginE = GeoStrings.Auto;
                ModelOriginH = METRE_ZERO;
            }
            else
            {
                var modelOrigin = _DefaultModelOrigin ?? new double[] { 0, 0, 0 };
                ModelOriginN = modelOrigin[1].ToMetreString();
                ModelOriginE = modelOrigin[0].ToMetreString();
                ModelOriginH = modelOrigin[2].ToMetreString();
            }
        }

        public bool HasSiteInfo => _SiteInfo != null;

        public OriginType Origin
        {
            get => _Origin;
            set
            {
                if (SetField(ref _Origin, value))
                {
                    if (UseProjectLocation)
                    {
                        Rotation = _Host.IsTrueNorth(Origin)
                            ? DEGREE_ZERO
                            : _SiteInfo.Rotation.ToDegreeString();
                    }
                }
            }
        }

        public bool AlignOriginToSitePlaneCenter
        {
            get => _AlignOriginToSitePlaneCenter;
            set
            {
                if (SetField(ref _AlignOriginToSitePlaneCenter, value))
                {
                    UpdateModelOriginStrings();
                }
            }
        }

        public string ModelOriginN
        {
            get => _ModelOriginN;
            set => SetField(ref _ModelOriginN, value);
        }

        public string ModelOriginE
        {
            get => _ModelOriginE;
            set => SetField(ref _ModelOriginE, value);
        }

        public string ModelOriginH
        {
            get => _ModelOriginH;
            set => SetField(ref _ModelOriginH, value);
        }

        public bool UseProjectLocation
        {
            get => _UseProjectLocation;
            set
            {
                if (SetField(ref _UseProjectLocation, value && HasSiteInfo))
                {
                    if (UseProjectLocation)
                    {
                        Latitude = _SiteInfo.Latitude.ToLatLonString();
                        Longitude = _SiteInfo.Longitude.ToLatLonString();
                        Height = _SiteInfo.Height.ToMetreString();

                        Rotation = _Host.IsTrueNorth(Origin)
                            ? DEGREE_ZERO
                            : _SiteInfo.Rotation.ToDegreeString();
                    }
                }
            }
        }

        public string Latitude
        {
            get => _Latitude;
            set => SetField(ref _Latitude, value);
        }

        public string Longitude
        {
            get => _Longitude;
            set => SetField(ref _Longitude, value);
        }

        public string Height
        {
            get => _Height;
            set => SetField(ref _Height, value);
        }

        public string Rotation
        {
            get => _Rotation;
            set => SetField(ref _Rotation, value);
        }

        public bool UseAutoAlignToGround
        {
            get => _UseAutoAlignToGround;
            set => SetField(ref _UseAutoAlignToGround, value);
        }

        #region Implementation of IDataErrorInfo

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(ModelOriginN):
                    {
                        if (!AlignOriginToSitePlaneCenter)
                        {
                            ModelOriginN.TryParseNumber(out error, out _);
                        }
                        break;
                    }
                    case nameof(ModelOriginE):
                    {
                        if (!AlignOriginToSitePlaneCenter)
                        {
                            ModelOriginE.TryParseNumber(out error, out _);
                        }
                        break;
                    }
                    case nameof(ModelOriginH):
                    {
                        if (!AlignOriginToSitePlaneCenter)
                        {
                            ModelOriginH.TryParseNumber(out error, out _);
                        }
                        break;
                    }
                    case nameof(Latitude):
                        Latitude.TryParseLatitude(out error, out _);
                        break;
                    case nameof(Longitude):
                        Longitude.TryParseLongitude(out error, out _);
                        break;
                    case nameof(Height):
                        Height.TryParseHeight(out error, out _);
                        break;
                    case nameof(Rotation):
                        Rotation.TryParseRotation(out error, out _);
                        break;
                }
                return error;
            }
        }

        string IDataErrorInfo.Error => null;

        #endregion

        /// <summary>
        /// 将当前 UI 状态验证并写回到目标参数对象（仅在点击 OK 时调用）
        /// </summary>
        public bool UpdateTo(ParameterEnu p, out string firstErrorProperty)
        {
            firstErrorProperty = null;
            var ok = true;

            p.Origin = Origin;

            if (AlignOriginToSitePlaneCenter)
            {
                p.AlignOriginToSitePlaneCenter = true;
                p.OriginOffset = null;
            }
            else
            {
                p.AlignOriginToSitePlaneCenter = false;

                var yOk = ModelOriginN.TryParseNumber(out _, out var y);
                var xOk = ModelOriginE.TryParseNumber(out _, out var x);
                var zOk = ModelOriginH.TryParseNumber(out _, out var z);

                if (!yOk) { firstErrorProperty = firstErrorProperty ?? nameof(ModelOriginN); ok = false; }
                if (!xOk) { firstErrorProperty = firstErrorProperty ?? nameof(ModelOriginE); ok = false; }
                if (!zOk) { firstErrorProperty = firstErrorProperty ?? nameof(ModelOriginH); ok = false; }

                if (ok) p.OriginOffset = new[] { x, y, z };
            }

            p.UseProjectLocation = UseProjectLocation;

            if (Latitude.TryParseLatitude(out _, out var lat))
                p.Latitude = lat;
            else { firstErrorProperty = firstErrorProperty ?? nameof(Latitude); ok = false; }

            if (Longitude.TryParseLongitude(out _, out var lon))
                p.Longitude = lon;
            else { firstErrorProperty = firstErrorProperty ?? nameof(Longitude); ok = false; }

            if (Height.TryParseHeight(out _, out var h))
                p.Height = h;
            else { firstErrorProperty = firstErrorProperty ?? nameof(Height); ok = false; }

            if (Rotation.TryParseRotation(out _, out var r))
                p.Rotation = r;
            else { firstErrorProperty = firstErrorProperty ?? nameof(Rotation); ok = false; }

            p.UseAutoAlignToGround = UseAutoAlignToGround;

            return ok;
        }

        /// <summary>
        /// 从默认设置重置 UI 状态（用于 btnReset）
        /// </summary>
        public void Reset(ParameterEnu defaultParam)
        {
            var p = defaultParam;
            Origin = p.Origin;

            AlignOriginToSitePlaneCenter = p.AlignOriginToSitePlaneCenter;
            if (!p.AlignOriginToSitePlaneCenter && p.OriginOffset?.Length >= 3)
            {
                ModelOriginN = p.OriginOffset[1].ToMetreString();
                ModelOriginE = p.OriginOffset[0].ToMetreString();
                ModelOriginH = p.OriginOffset[2].ToMetreString();
            }

            UseProjectLocation = p.UseProjectLocation && HasSiteInfo;
            Latitude = p.Latitude.ToLatLonString();
            Longitude = p.Longitude.ToLatLonString();
            Height = p.Height.ToMetreString();
            Rotation = p.Rotation.ToDegreeString();
            UseAutoAlignToGround = p.UseAutoAlignToGround;
        }
    }
}
