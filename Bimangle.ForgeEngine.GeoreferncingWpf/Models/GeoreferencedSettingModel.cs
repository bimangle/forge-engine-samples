using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Georeferncing.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bimangle.ForgeEngine.Georeferncing.Models
{
    public class GeoreferencedSettingModel : AbstractModel
    {
        private readonly GeoreferencedSetting _Setting;
        private readonly IGeoreferncingHost _Host;
        private string _DefaultTitle;
        private string _Title;

        private readonly IDictionary<TabItem, GeoreferencedModeInfo> _TabItemModeLinks = new Dictionary<TabItem, GeoreferencedModeInfo>();
        private TabItem _SelectedTabItem;

        public IList<ItemValue<OriginType>> SupportOriginTypes { get; }
        public IList<ItemValue<OriginType>> SupportOriginTypesForAuto { get; }
        public bool IsSupportMultiOriginType { get; }

        public TabItem SelectedTabItem
        {
            get => _SelectedTabItem;
            set
            {
                if (_SelectedTabItem != value)
                {
                    _SelectedTabItem = value;

                    var info = _TabItemModeLinks[_SelectedTabItem];
                    _Setting.Mode = info.Mode;
                    OnPropertyChanged();

                    Title = SelectedTabItem == null
                        ? _DefaultTitle
                        : $"{_DefaultTitle} - {info.Title}";
                }
            }
        }

        public string Title
        {
            get => _Title;
            set => SetField(ref _Title, value);
        }

        public ParameterAutoModel Auto { get; }
        public ParameterEnuModel Enu { get; }
        public ParameterLocalModel Local { get; }
        public ParameterProjModel Proj { get; }

        public GeoreferencedSettingModel(GeoreferencedSetting setting, IGeoreferncingHost host)
        {
            _Setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _Host = host ?? throw new ArgumentNullException(nameof(host));

            #region 初始化 模型坐标系列表
            {
                SupportOriginTypes = new List<ItemValue<OriginType>>();
                SupportOriginTypesForAuto = new List<ItemValue<OriginType>>();

                //构造可选的原点选项列表
                var originTypes = _Host.GetSupportOriginTypes();
                foreach (var originType in originTypes)
                {
                    var item = new ItemValue<OriginType>(_Host.Adapter.GetLocalString(originType), originType);
                    SupportOriginTypes.Add(item);
                    SupportOriginTypesForAuto.Add(item);
                }
                IsSupportMultiOriginType = originTypes.Length > 1;

                //如果支持多种原点选择, 则为 "自动" 模式下再增加一个 "自动" 选项
                if (IsSupportMultiOriginType)
                {
                    var item = new ItemValue<OriginType>(GeoStrings.OriginTypeAuto, OriginType.Auto);
                    SupportOriginTypesForAuto.Insert(0, item);
                }
            }
            #endregion

            Auto = new ParameterAutoModel(_Setting, _Host);
            Enu = new ParameterEnuModel(_Setting, _Host);
            Local = new ParameterLocalModel(_Setting, _Host);
            Proj = new ParameterProjModel(_Setting, _Host);
        }

        public void InitSelectedTabItem(
            string defaultTitle,
            TabItem tabItemAuto,
            TabItem tabItemEnu,
            TabItem tabItemLocal,
            TabItem tabItemProj,
            GeoreferencedMode defaultMode = GeoreferencedMode.Auto)
        {
            _Title = _DefaultTitle = defaultTitle;

            var tabItems = new Dictionary<GeoreferencedMode, TabItem>();

            void AddTabItem(TabItem tabItem, GeoreferencedMode mode, string title)
            {
                if (tabItem == null || tabItem.Visibility != Visibility.Visible)
                {
                    return;
                }
                tabItems.Add(mode, tabItem);
                _TabItemModeLinks[tabItem] = new GeoreferencedModeInfo(title, mode);
            }

            AddTabItem(tabItemAuto, GeoreferencedMode.Auto, WpfGeoreferncingRes.tabPageAuto_Text);
            AddTabItem(tabItemEnu, GeoreferencedMode.Enu, WpfGeoreferncingRes.tabPageEnu_Text);
            AddTabItem(tabItemLocal, GeoreferencedMode.Local, WpfGeoreferncingRes.tabPageLocal_Text);
            AddTabItem(tabItemProj, GeoreferencedMode.Proj, WpfGeoreferncingRes.tabPageProj_Text);

            //获取当前模式对应的 TabItem, 如果当前模式无效，则将当前模式修改为默认模式的值
            if (tabItems.TryGetValue(_Setting.Mode, out var item) == false)
            {
                //获取默认模式对应的 TabItem, 如果默认模式无效，则将默认模式重设为第一个有效值
                if (tabItems.TryGetValue(defaultMode, out item) == false)
                {
                    defaultMode = tabItems.First().Key;
                }

                _Setting.Mode = defaultMode;
                item = tabItems[defaultMode];
            }

            SelectedTabItem = item;
        }

        private class GeoreferencedModeInfo
        {
            public string Title { get; }
            public GeoreferencedMode Mode { get; }

            public GeoreferencedModeInfo(string title, GeoreferencedMode mode)
            {
                Title = title;
                Mode = mode;
            }
        }

        /// <summary>
        /// 将当前活动标签页的 UI 状态验证并写回到目标设置对象（仅在点击 OK 时调用）
        /// </summary>
        public bool UpdateTo(GeoreferencedSetting setting, out string firstErrorProperty)
        {
            firstErrorProperty = null;
            switch (setting.Mode)
            {
                case GeoreferencedMode.Auto:
                    return Auto.UpdateTo(setting.Auto, out firstErrorProperty);
                case GeoreferencedMode.Enu:
                    return Enu.UpdateTo(setting.Enu, out firstErrorProperty);
                case GeoreferencedMode.Local:
                    return Local.UpdateTo(setting.Local, out firstErrorProperty);
                case GeoreferencedMode.Proj:
                    return Proj.UpdateTo(setting.Proj, out firstErrorProperty);
                default:
                    throw new InvalidOperationException($"Unsupported georeferenced mode: {setting.Mode}");
            }
        }

        /// <summary>
        /// 从默认设置重置所有标签页的 UI 状态（用于 btnReset）
        /// </summary>
        public void Reset()
        {
            var defaultSetting = _Host.CreateDefaultSetting();
            Auto.Reset(defaultSetting.Auto);
            Enu.Reset(defaultSetting.Enu);
            Local.Reset(defaultSetting.Local);
            Proj.Reset(defaultSetting.Proj);
        }
    }
}
