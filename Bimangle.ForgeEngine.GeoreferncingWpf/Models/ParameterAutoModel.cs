using Bimangle.ForgeEngine.Common.Georeferenced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Georeferncing.Models
{
    public class ParameterAutoModel : AbstractModel
    {
        private readonly GeoreferencedSetting _Setting;
        private readonly IGeoreferncingHost _Host;
        private readonly ParameterAuto _LocalSetting;

        private string _Details;

        public ParameterAutoModel(GeoreferencedSetting setting, IGeoreferncingHost host)
        {
            _Setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _LocalSetting = _Setting.Auto ?? throw new ArgumentOutOfRangeException(nameof(setting));

            UpdateDetails();
        }

        private void UpdateDetails()
        {
            var d = _Host.CreateDefaultSetting();
            d.Mode = GeoreferencedMode.Auto;
            d.Auto.Origin = _LocalSetting.Origin;

            Details = d.GetDetails(_Host);
        }

        public OriginType Origin
        {
            get => _LocalSetting.Origin;
            set
            {
                if (_LocalSetting.Origin != value)
                {
                    _LocalSetting.Origin = value;
                    OnPropertyChanged();

                    UpdateDetails();
                }
            }
        }

        public string Details
        {
            get => _Details;
            set => SetField(ref _Details, value);
        }

        /// <summary>
        /// 将当前 UI 状态写回到目标参数对象（仅在点击 OK 时调用）
        /// </summary>
        public bool UpdateTo(ParameterAuto p, out string firstErrorProperty)
        {
            firstErrorProperty = null;
            p.Origin = Origin;
            return true;
        }

        /// <summary>
        /// 从默认设置重置 UI 状态（用于 btnReset）
        /// </summary>
        public void Reset(ParameterAuto defaultParam)
        {
            Origin = defaultParam.Origin;
        }
    }
}
