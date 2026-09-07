using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Georeferncing.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using MetadataXml = Bimangle.ForgeEngine.Georeferncing.Utility.MetadataXml;
using MetadataXmlProj = Bimangle.ForgeEngine.Georeferncing.Utility.MetadataXmlProj;

namespace Bimangle.ForgeEngine.Georeferncing.Models
{
    public class ParameterProjModel : AbstractModel, IDataErrorInfo
    {
        private readonly GeoreferencedSetting _Setting;
        private readonly IGeoreferncingHost _Host;
        private readonly ParameterProj _LocalSetting;

        private readonly string _ProjectFilePath;

        // 本地偏移状态副本，不直接写回 _LocalSetting，只在 UpdateTo 时写回
        private ProjOffsetType _LocalOffsetType;
        private double[] _LocalOffset;
        private double _LocalGeoidConstantOffset;

        // 初始化标志，用于跳过 ProjSource setter 中的联动逻辑
        private bool _isInitializing;

        private OriginType _Origin;
        private ProjSourceItem _ProjSource;
        private string _ProjDefinition;
        private bool _IsProjDefinitionReadOnly;
        private bool _ShouldValidateProjDefinition;
        private string _ProjCoordinateOffset;
        private string _ProjectionHeight;
        private VerticalGeoidGrid _VerticalGeoidGrid;
        private string _GeoidConstantOffset;
        private IList<ProjSourceItem> _ProjSourceItems;
        private IList<VerticalGeoidGrid> _VerticalGeoidGridItems;

        public IList<ProjSourceItem> ProjSourceItems
        {
            get => _ProjSourceItems;
            private set => SetField(ref _ProjSourceItems, value);
        }

        public IList<VerticalGeoidGrid> VerticalGeoidGridItems
        {
            get => _VerticalGeoidGridItems;
            private set => SetField(ref _VerticalGeoidGridItems, value);
        }

        // View 注册的回调，用于弹出子窗口
        public Func<string> RequestCreate { get; set; }
        public Func<string> RequestBrowse { get; set; }
        public Func<ParameterProj, ParameterProj> RequestEdit { get; set; }

        public ParameterProjModel(GeoreferencedSetting setting, IGeoreferncingHost host)
        {
            _Setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _LocalSetting = _Setting.Proj;

            _ProjectFilePath = _Host.GetModelFilePath();

            //兼容历史偏移量参数，结果写入本地状态字段，不修改 _LocalSetting
            CompatibilityFixForLegacyOffset();

            //更新本地字段
            UpdateLocalFields();
        }

        private void CompatibilityFixForLegacyOffset()
        {
            //兼容历史数据:
            //  若原先的偏移参数是3D三参数，则转换为4参数, 且将高程偏移转换为大地水准面高程常数偏移量
            var proj = _LocalSetting;
            _LocalOffsetType = proj?.OffsetType ?? ProjOffsetType.None;
            _LocalOffset = proj?.Offset?.CloneArray();
            _LocalGeoidConstantOffset = proj?.GeoidConstantOffset ?? 0.0;

            if (proj?.Offset != null && proj.Offset.Length == 3)
            {
                var offset = proj.Offset;
                if (Math.Abs(offset[0]) <= 1e-10 &&
                    Math.Abs(offset[1]) <= 1e-10)
                {
                    _LocalOffsetType = ProjOffsetType.None;
                    _LocalOffset = null;
                }
                else
                {
                    _LocalOffsetType = ProjOffsetType._2D_Params4;
                    _LocalOffset = new double[7]
                    {
                        offset[0], offset[1], 0.0,
                        0.0, 0.0, 0.0,
                        0.0
                    };
                }

                if (Math.Abs(offset[2]) >= 1e-10)
                {
                    _LocalGeoidConstantOffset = offset[2];
                }
            }
        }

        /// <summary>
        /// 更新本地字段。source 不为 null 时从 source 读取（用于 Reset），否则从 _LocalSetting 读取。
        /// </summary>
        private void UpdateLocalFields(ParameterProj source = null)
        {
            if (source != null)
            {
                _LocalOffsetType = source.OffsetType;
                _LocalOffset = source.Offset?.CloneArray();
                _LocalGeoidConstantOffset = source.GeoidConstantOffset;
            }

            var p = source ?? _LocalSetting;

            _isInitializing = true;
            try
            {
                _ShouldValidateProjDefinition = !string.IsNullOrWhiteSpace(p.Definition);

                //模型坐标系
                Origin = p.Origin;

                //投影坐标系
                ProjSourceItems = _Host.GetProjSourceItems();
                var matchedSource = ProjSourceItems.FirstOrDefault(x => x.SourceType == p.DefinitionSource && x.FilePath == p.DefinitionFileName);

                if (p.DefinitionSource == ProjSourceType.Embed)
                {
                    // 使用项目内置的最新信息覆盖本地暂存字段（不修改 _LocalSetting，仅在点击 OK 时才写回原始配置）
                    var embedItem = matchedSource ?? ProjSourceItems.FirstOrDefault(x => x.SourceType == ProjSourceType.Embed);
                    if (embedItem?.ProjEmbed != null)
                    {
                        var embed = embedItem.ProjEmbed;
                        matchedSource = embedItem;
                        ProjDefinition = embed.Definition?.ToWindowsFormat() ?? string.Empty;
                        _LocalOffsetType = embed.OffsetType;
                        _LocalOffset = embed.Offset?.CloneArray();
                        _LocalGeoidConstantOffset = embed.GeoidConstantOffset;
                    }
                    else
                    {
                        // 找不到 Embed 项，回退为 Custom
                        matchedSource = ProjSourceItems.FirstOrDefault(x => x.SourceType == ProjSourceType.Custom) ??
                                        ProjSourceItems.FirstOrDefault();
                        ProjDefinition = p.Definition?.ToWindowsFormat() ?? string.Empty;
                    }
                }
                else
                {
                    matchedSource = matchedSource ?? ProjSourceItems.FirstOrDefault();
                    ProjDefinition = p.Definition?.ToWindowsFormat() ?? string.Empty;
                }

                IsProjDefinitionReadOnly = IsReadOnlyForSource(matchedSource);
                ProjSource = matchedSource;

                //平面坐标变换
                ProjCoordinateOffset = GetLocalOffsetString();

                //投影面高程
                ProjectionHeight = p.ProjectionHeight.ToMetreString();

                //大地水准面高校正
                VerticalGeoidGridItems = _Host.GetVerticalGeoidGridItems();
                var matchedGrid = VerticalGeoidGridItems.FirstOrDefault(x => x.FileName == p.GeoidGrid);

                // 若匹配到但未安装，则重置为第一项（通常是 None）
                if (matchedGrid != null && !matchedGrid.IsInstalled)
                {
                    matchedGrid = null;
                }

                VerticalGeoidGrid = matchedGrid ?? VerticalGeoidGridItems.FirstOrDefault();

                //高程常数偏移量
                GeoidConstantOffset = _LocalGeoidConstantOffset.ToMetreString();
            }
            finally
            {
                _isInitializing = false;
            }
        }

        private static bool IsReadOnlyForSource(ProjSourceItem item)
        {
            if (item == null) return false;
            switch (item.SourceType)
            {
                case ProjSourceType.Custom:
                case ProjSourceType.Create:
                case ProjSourceType.Browse:
                    return false;
                default:
                    return true;
            }
        }

        private string GetLocalOffsetString()
        {
            var temp = new ParameterProj
            {
                OffsetType = _LocalOffsetType,
                Offset = _LocalOffset?.CloneArray()
            };
            return temp.GetOffsetString();
        }

        public bool HasProjectFilePath => _ProjectFilePath != null;

        public OriginType Origin
        {
            get => _Origin;
            set => SetField(ref _Origin, value);
        }

        public ProjSourceItem ProjSource
        {
            get => _ProjSource;
            set
            {
                if (_isInitializing)
                {
                    SetField(ref _ProjSource, value);
                    return;
                }

                if (!SetField(ref _ProjSource, value)) return;
                if (value == null) return;

                switch (value.SourceType)
                {
                    case ProjSourceType.Create:
                    {
                        // 先重置回"自定义"，再弹出创建窗口
                        var customItem = ProjSourceItems.FirstOrDefault(x => x.SourceType == ProjSourceType.Custom);
                        _ProjSource = customItem ?? ProjSourceItems.FirstOrDefault();
                        OnPropertyChanged(nameof(ProjSource));
                        IsProjDefinitionReadOnly = false;

                        var definition = RequestCreate?.Invoke();
                        if (!string.IsNullOrWhiteSpace(definition))
                        {
                            ProjDefinition = definition;
                        }
                        break;
                    }
                    case ProjSourceType.Browse:
                    {
                        // 先重置回"自定义"，再弹出文件选择
                        var customItem = ProjSourceItems.FirstOrDefault(x => x.SourceType == ProjSourceType.Custom);
                        _ProjSource = customItem ?? ProjSourceItems.FirstOrDefault();
                        OnPropertyChanged(nameof(ProjSource));
                        IsProjDefinitionReadOnly = false;

                        var filePath = RequestBrowse?.Invoke();
                        if (!string.IsNullOrWhiteSpace(filePath))
                        {
                            UseProjFile(filePath);
                        }
                        break;
                    }
                    case ProjSourceType.Custom:
                        IsProjDefinitionReadOnly = false;
                        break;
                    case ProjSourceType.MetadataXml:
                    {
                        if (MetadataXml.TryParse(value.FilePath, out var meta) &&
                            meta.TryGetProj(_Host, out var proj))
                        {
                            UseMetadataXmlProj(proj);
                        }
                        break;
                    }
                    case ProjSourceType.Embed:
                    {
                        var projParameter = value.ProjEmbed;
                        ProjDefinition = projParameter.Definition.ToWindowsFormat();

                        // 一并使用最新的 Embed 数据覆盖本地暂存的偏移量与高程常数偏移量
                        _LocalOffsetType = projParameter.OffsetType;
                        _LocalOffset = projParameter.Offset?.CloneArray();
                        _LocalGeoidConstantOffset = projParameter.GeoidConstantOffset;
                        ProjCoordinateOffset = GetLocalOffsetString();

                        GeoidConstantOffset = projParameter.GeoidConstantOffset.ToMetreString();
                        IsProjDefinitionReadOnly = true;
                        break;
                    }
                    case ProjSourceType.Default:
                    case ProjSourceType.ProjectFolder:
                    case ProjSourceType.Recently:
                        ProjDefinition = value.ProjDefinition.ToWindowsFormat();
                        IsProjDefinitionReadOnly = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public bool IsProjDefinitionReadOnly
        {
            get => _IsProjDefinitionReadOnly;
            set
            {
                if (SetField(ref _IsProjDefinitionReadOnly, value))
                {
                    OnPropertyChanged(nameof(IsReadOnly));
                    OnPropertyChanged(nameof(IsEditable));
                }
            }
        }

        /// <summary>
        /// 当前 ProjSource 是否为只读模式（非 Custom 时所有编辑项均为只读）。
        /// </summary>
        public bool IsReadOnly => _IsProjDefinitionReadOnly;

        /// <summary>
        /// IsReadOnly 的反向属性，便于绑定到 IsEnabled / IsHitTestVisible / Focusable。
        /// </summary>
        public bool IsEditable => !_IsProjDefinitionReadOnly;

        public string ProjDefinition
        {
            get => _ProjDefinition;
            set
            {
                if (SetField(ref _ProjDefinition, value))
                {
                    OnPropertyChanged(nameof(ProjDefinitionForeground));
                }
            }
        }

        public System.Windows.Media.Brush ProjDefinitionForeground
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ProjDefinition))
                    return System.Windows.SystemColors.WindowTextBrush;

                if (_Host.CheckProjDefinition(ProjDefinition, out _))
                    return System.Windows.Media.Brushes.Green;

                return System.Windows.Media.Brushes.Red;
            }
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

        public VerticalGeoidGrid VerticalGeoidGrid
        {
            get => _VerticalGeoidGrid;
            set
            {
                // 如果选择的项未安装，则拒绝并重置为第一项（默认为 None）
                if (value != null && !value.IsInstalled)
                {
                    value = VerticalGeoidGridItems?.FirstOrDefault();
                }

                SetField(ref _VerticalGeoidGrid, value);
            }
        }

        public string GeoidConstantOffset
        {
            get => _GeoidConstantOffset;
            set => SetField(ref _GeoidConstantOffset, value);
        }

        public bool CheckProjFile(string projFilePath)
        {
            if (string.Compare(Path.GetExtension(projFilePath), @".xml", StringComparison.OrdinalIgnoreCase) == 0 &&
                MetadataXml.TryParse(projFilePath, out _))
            {
                return true;
            }

            var projDefinition = _Host.GetProjDefinition(projFilePath);
            if (string.IsNullOrWhiteSpace(projDefinition) == false)
            {
                return true;
            }

            return false;
        }

        public bool UseProjFile(string projFilePath)
        {
            if (string.Compare(Path.GetExtension(projFilePath), @".xml", StringComparison.OrdinalIgnoreCase) == 0 &&
                MetadataXml.TryParse(projFilePath, out var meta) &&
                meta.TryGetProj(_Host, out var proj))
            {
                UseMetadataXmlProj(proj);
                return true;
            }

            var projDefinition = _Host.GetProjDefinition(projFilePath);
            if (string.IsNullOrWhiteSpace(projDefinition) == false)
            {
                var sourceType = ProjSourceType.Recently;
                var label = $@"{sourceType.GetString()}: {projFilePath}";
                var newItem = new ProjSourceItem(label, sourceType, projFilePath, projDefinition);

                ProjSourceItems.Add(newItem);
                ProjSource = newItem; // setter 会处理 Recently 类型：设置 ProjDefinition 和 ReadOnly

                _Host.CheckInProjFile(projFilePath);

                return true;
            }

            return false;
        }

        private void UseMetadataXmlProj(MetadataXmlProj metaProj)
        {
            // 只更新本地状态字段，不写 _LocalSetting
            if (metaProj?.SrsOrigin != null && metaProj.SrsOrigin.Length >= 3)
            {
                _LocalOffsetType = ProjOffsetType._2D_Params4;
                _LocalOffset = new double[7]
                {
                    metaProj.SrsOrigin[0],
                    metaProj.SrsOrigin[1],
                    0.0, //metaProj.SrsOrigin[2],
                    0.0, 0.0, 0.0,
                    0.0
                };

                GeoidConstantOffset = metaProj.SrsOrigin[2].ToMetreString();
            }
            else
            {
                _LocalOffsetType = ProjOffsetType.None;
                _LocalOffset = null;
            }

            ProjCoordinateOffset = GetLocalOffsetString();
            ProjDefinition = metaProj?.Srs;

            _isInitializing = true;
            try
            {
                ProjSource = ProjSourceItems.FirstOrDefault(x => x.SourceType == ProjSourceType.Custom) ??
                             ProjSourceItems.FirstOrDefault();
                VerticalGeoidGrid = VerticalGeoidGridItems.FirstOrDefault();
            }
            finally
            {
                _isInitializing = false;
            }

            IsProjDefinitionReadOnly = false;
            GeoidConstantOffset = @"0";
        }

        /// <summary>
        /// 执行偏移参数编辑，由 View 的 btnEdit Click 调用
        /// </summary>
        public void EditOffset()
        {
            var snapshot = BuildEditParameter();
            var result = RequestEdit?.Invoke(snapshot);
            if (result != null)
            {
                _LocalOffsetType = result.OffsetType;
                _LocalOffset = result.GetOffsetForCalc();
                ProjCoordinateOffset = GetLocalOffsetString();
            }
        }

        /// <summary>
        /// 构建当前状态的 ParameterProj 快照，用于传给偏移编辑子窗口
        /// </summary>
        private ParameterProj BuildEditParameter()
        {
            var p = new ParameterProj();
            p.Origin = Origin;
            p.OffsetType = _LocalOffsetType;
            p.Offset = _LocalOffset?.CloneArray();

            if (GeoidConstantOffset.TryParseHeight(out _, out var gc))
                p.GeoidConstantOffset = gc;

            p.GeoidGrid = VerticalGeoidGrid?.IsInstalled == true ? VerticalGeoidGrid.FileName : null;

            if (_Host.CheckProjDefinition(ProjDefinition, out _))
            {
                p.Definition = ProjDefinition?.Trim();
                if (ProjSource != null)
                {
                    p.DefinitionSource = ProjSource.SourceType;
                    p.DefinitionFileName = ProjSource.FilePath;
                }
            }

            return p;
        }

        /// <summary>
        /// 构建当前状态的 ParameterProj 快照，用于传给测试子窗口
        /// </summary>
        public ParameterProj BuildTestParameter()
        {
            return BuildEditParameter();
        }

        /// <summary>
        /// 尝试获取已验证的投影定义 WKT，供 View 保存文件使用
        /// </summary>
        public bool TryGetProjDefinitionWkt(out string wkt)
        {
            EnableProjDefinitionValidation();
            return _Host.CheckProjDefinition(ProjDefinition, out wkt);
        }

        private void EnableProjDefinitionValidation()
        {
            if (_ShouldValidateProjDefinition)
            {
                return;
            }

            _ShouldValidateProjDefinition = true;
            OnPropertyChanged(nameof(ProjDefinition));
        }

        /// <summary>
        /// 获取当前本地偏移类型，供 View 保存偏移文件使用
        /// </summary>
        public ProjOffsetType GetLocalOffsetType() => _LocalOffsetType;

        /// <summary>
        /// 获取当前本地偏移数组的副本，供 View 保存偏移文件使用
        /// </summary>
        public double[] GetLocalOffsetArray() => _LocalOffset?.CloneArray();

        /// <summary>
        /// 将当前 UI 状态验证并写回到目标参数对象（仅在点击 OK 时调用）
        /// </summary>
        public bool UpdateTo(ParameterProj p, out string firstErrorProperty)
        {
            firstErrorProperty = null;
            var ok = true;

            EnableProjDefinitionValidation();

            p.Origin = Origin;
            p.OffsetType = _LocalOffsetType;
            p.Offset = _LocalOffset?.CloneArray();

            p.GeoidGrid = (VerticalGeoidGrid?.IsInstalled == true) ? VerticalGeoidGrid.FileName : null;

            if (GeoidConstantOffset.TryParseHeight(out _, out var geoidOffset))
            {
                p.GeoidConstantOffset = geoidOffset;
            }
            else
            {
                firstErrorProperty = firstErrorProperty ?? nameof(GeoidConstantOffset);
                ok = false;
            }

            if (ProjectionHeight.TryParseHeight(out _, out var projHeight))
            {
                p.ProjectionHeight = projHeight;
            }
            else
            {
                firstErrorProperty = firstErrorProperty ?? nameof(ProjectionHeight);
                ok = false;
            }

            if (_Host.CheckProjDefinition(ProjDefinition, out _))
            {
                p.Definition = ProjDefinition?.Trim();
                if (ProjSource != null)
                {
                    p.DefinitionSource = ProjSource.SourceType;
                    p.DefinitionFileName = ProjSource.FilePath;
                }
                else
                {
                    p.DefinitionSource = ProjSourceType.Custom;
                    p.DefinitionFileName = null;
                }
            }
            else
            {
                firstErrorProperty = firstErrorProperty ?? nameof(ProjDefinition);
                ok = false;
            }

            return ok;
        }

        /// <summary>
        /// 从默认设置重置 UI 状态（用于 btnReset）
        /// </summary>
        public void Reset(ParameterProj defaultParam) => UpdateLocalFields(defaultParam);

        #region Implementation of IDataErrorInfo

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(ProjDefinition):
                    {
                        if (!_ShouldValidateProjDefinition)
                        {
                            break;
                        }

                        if (!_Host.CheckProjDefinition(ProjDefinition, out _))
                        {
                            error = GeoStrings.InvalidProjectDefinition;
                        }
                        break;
                    }
                    case nameof(ProjectionHeight):
                        ProjectionHeight.TryParseHeight(out error, out _);
                        break;
                    case nameof(GeoidConstantOffset):
                        GeoidConstantOffset.TryParseHeight(out error, out _);
                        break;
                }

                return error;
            }
        }

        string IDataErrorInfo.Error => null;

        #endregion
    }
}
