using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Georeferncing.Utility;
using Form = System.Windows.Forms.Form;

namespace Bimangle.ForgeEngine.Georeferncing
{
    partial class FormGeoreferncing : Form
    {
        private readonly string _ProjectFilePath = null;
        private readonly SiteInfo _SiteInfo = null;
        private readonly double[] _DefaultModelOrigin;
        private readonly IGeoreferncingHost _Host;
        private readonly string _DefaultTitle;

        public FormGeoreferncing(IGeoreferncingHost host, GeoreferencedSetting setting)
            : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _SiteInfo = _Host.GetModelSiteInfo();
            _DefaultModelOrigin = _Host.GetDefaultModelOrigin();
            _ProjectFilePath = _Host.GetModelFilePath();
            _DefaultTitle = Text;

            Setting = _Host.CreateSuitedSetting(setting);
        }

        public FormGeoreferncing()
        {
            InitializeComponent();
        }

        public GeoreferencedSetting Setting { get; private set; }

        private void FormGeoreferncing_Load(object sender, System.EventArgs e)
        {
            InitUI();

            UpdateToTabs(Setting);
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == null)
            {
                Text =_DefaultTitle;
            }
            else
            {
                Text = $@"{_DefaultTitle} - {tabMain.SelectedTab.Text}";
            }
        }

        private void InitUI()
        {
            #region 初始化 "模型坐标系" 下拉框

            {
                var originTypes = _Host.GetSupportOriginTypes();
                var items = new List<ItemValue<OriginType>>();
                foreach (var originType in originTypes)
                {
                    items.Add(new ItemValue<OriginType>(_Host.Adapter.GetLocalString(originType), originType));
                }

                var controls = new[]
                {
                    cbAutoOriginLocation,
                    cbEnuOriginLocation,
                    cbLocalOriginLocation,
                    cbProjOriginLocation
                };

                foreach (var control in controls)
                {
                    control.Items.Clear();

                    foreach (var item in items)
                    {
                        control.Items.Add(item);
                    }

                    if (control.Items.Count == 1)
                    {
                        control.Enabled = false;
                    }
                }

                //如果 "自动" 模式下支持多种原点选择, 则再增加一个 "自动" 选项
                if (cbAutoOriginLocation.Enabled)
                {
                    var item = new ItemValue<OriginType>(GeoStrings.OriginTypeAuto, OriginType.Auto);
                    cbAutoOriginLocation.Items.Insert(0, item);
                }
            }
            #endregion

            #region 初始化 站心坐标
            {
                _Host.Adapter.SetDirectionLetters(lblEnuModelOriginE, lblEnuModelOriginN, lblEnuModelOriginH);

                txtEnuModelOriginN.OnValidating(t => { TryParseNumber(t, out _); });
                txtEnuModelOriginE.OnValidating(t => { TryParseNumber(t, out _); });
                txtEnuModelOriginH.OnValidating(t => { TryParseNumber(t, out _); });

                txtEnuLatitude.OnValidating(t => { TryParseLatitude(t, out _); });
                txtEnuLongitude.OnValidating(t => { TryParseLongitude(t, out _); });
                txtEnuHeight.OnValidating(t => { TryParseHeight(t, out _); });
                txtEnuRotation.OnValidating(t => { TryParseRotation(t, out _); });
            }
            #endregion

            #region 初始化 暂不配准
            {
                _Host.Adapter.SetDirectionLetters(lblLocalModelOriginE, lblLocalModelOriginN, lblLocalModelOriginH);

                txtLocalModelOriginN.OnValidating(t => { TryParseNumber(t, out _); });
                txtLocalModelOriginE.OnValidating(t => { TryParseNumber(t, out _); });
                txtLocalModelOriginH.OnValidating(t => { TryParseNumber(t, out _); });

                txtLocalLatitude.OnValidating(t => { TryParseLatitude(t, out _); });
                txtLocalLongitude.OnValidating(t => { TryParseLongitude(t, out _); });
                txtLocalHeight.OnValidating(t => { TryParseHeight(t, out _); });
                txtLocalRotation.OnValidating(t => { TryParseRotation(t, out _); });
            }
            #endregion

            #region 初始化 投影坐标

            #region 初始化 "投影参考定义" 下拉框
            {
                var items = _Host.GetProjSourceItems();
                cbProjDefinition.Items.AddRange(items.OfType<object>().ToArray());
            }
            #endregion

            #region 初始化 "投影高程定义" 下拉框
            {
                var items = _Host.GetProjElevationItems();
                cbProjElevation.Items.AddRange(items.OfType<object>().ToArray());
            }
            #endregion

            #region 为投影定义文本框增加拖入文件的功能
            {
                var text = txtProjDefinition;
                text.AllowDrop = true;
                text.DragDrop += (sender, e) =>
                {
                    if (e.Data.TryParsePath(out var path))
                    {
                        UseProjFile(path);
                    }
                };

                text.DragEnter += (sender, e) =>
                {
                    e.Effect = DragDropEffects.None;
                    if (e.Data.TryParsePath(out var path) && File.Exists(path))
                    {
                        if (CheckProjFile(path))
                        {
                            e.Effect = DragDropEffects.Link;
                        }
                    }
                };
            }
            #endregion

            txtProjCoordinateOffset.OnValidating(t =>
            {
                errorProvider1.SetError(t, null);

                var ret = true;
                var s = t.Text;
                if (string.IsNullOrWhiteSpace(s))
                {
                    t.Text = @"0,0,0";
                }
                else if (TryParseOffsets(s, out _) == false)
                {
                    ret = false; //e.Cancel = true;
                    errorProvider1.SetError(btnProjCoordinateOffsetSave, GeoStrings.InvalidData);
                }
                return ret;
            });

            txtProjElevation.OnValidating(t => { TryParseHeight(t, out _); });

            txtProjDefinition.OnValidating(t =>
            {
                var s = t.Text;
                if (_Host.CheckProjDefinition(s, out _) == false)
                {
                    t.ForeColor = Color.Red;
                    errorProvider1.SetError(t, GeoStrings.InvalidProjectDefinition);
                }
                else
                {
                    t.ForeColor = Color.Green;
                    errorProvider1.SetError(t, null);
                }
            });

            #endregion


            if (_SiteInfo == null)
            {
                cbEnuUseProjectLocation.Visible = false;
                cbLocalOriginLocation.Visible = false;
            }

            if (_ProjectFilePath == null)
            {
                btnProjDefinitionSave.Enabled = false;
                btnProjCoordinateOffsetSave.Enabled = false;
            }
        }

        #region UpdateTo UI

        private void UpdateToTabs(GeoreferencedSetting setting)
        {
            UpdateToTabAuto(setting.Auto);
            UpdateToTabEnu(setting.Enu);
            UpdateToTabLocal(setting.Local);
            UpdateToTabProj(setting.Proj);

            tabMain.SelectedIndex = -1;
            switch (setting.Mode)
            {
                case GeoreferencedMode.Enu:
                    tabMain.SelectedTab = tabPageEnu;
                    break;
                case GeoreferencedMode.Local:
                    tabMain.SelectedTab = tabPageLocal;
                    break;
                case GeoreferencedMode.Proj:
                    tabMain.SelectedTab = tabPageProj;
                    break;
                case GeoreferencedMode.Auto:
                default:
                    tabMain.SelectedTab = tabPageAuto;
                    break;
            }
        }

        private void UpdateToTabAuto(ParameterAuto p)
        {
            cbAutoOriginLocation.SetSelectedValue(p.Origin);
        }

        private void UpdateToTabEnu(ParameterEnu p)
        {
            cbEnuOriginLocation.SetSelectedValue(p.Origin);

            cbEnuAlignOriginToSitePlaneCenter.Checked = p.AlignOriginToSitePlaneCenter;

            if (p.AlignOriginToSitePlaneCenter == false && 
                p.OriginOffset != null && 
                p.OriginOffset.Length >= 3)
            {
                txtEnuModelOriginN.Text = GetDoubleString(p.OriginOffset[1], 10);
                txtEnuModelOriginE.Text = GetDoubleString(p.OriginOffset[0], 10);
                txtEnuModelOriginH.Text = GetDoubleString(p.OriginOffset[2], 10);
            }

            cbEnuUseProjectLocation.Checked = p.UseProjectLocation;
            txtEnuLatitude.Text = GetDoubleString(p.Latitude, 10);
            txtEnuLongitude.Text = GetDoubleString(p.Longitude, 10);
            txtEnuHeight.Text = GetDoubleString(p.Height, 6);
            txtEnuRotation.Text = GetDoubleString(p.Rotation, 10);

            cbEnuAutoAlignToGround.Checked = p.UseAutoAlignToGround;
        }

        private void UpdateToTabLocal(ParameterLocal p)
        {
            cbLocalOriginLocation.SetSelectedValue(p.Origin);

            cbLocalAlignOriginToSitePlaneCenter.Checked = p.AlignOriginToSitePlaneCenter;

            if (p.AlignOriginToSitePlaneCenter == false &&
                p.OriginOffset != null &&
                p.OriginOffset.Length >= 3)
            {
                txtLocalModelOriginN.Text = GetDoubleString(p.OriginOffset[1], 10);
                txtLocalModelOriginE.Text = GetDoubleString(p.OriginOffset[0], 10);
                txtLocalModelOriginH.Text = GetDoubleString(p.OriginOffset[2], 10);
            }

            cbLocalUseProjectLocation.Checked = p.UseProjectLocation;
            txtLocalLatitude.Text = GetDoubleString(p.Latitude, 10);
            txtLocalLongitude.Text = GetDoubleString(p.Longitude, 10);
            txtLocalHeight.Text = GetDoubleString(p.Height, 6);
            txtLocalRotation.Text = GetDoubleString(p.Rotation, 10);
        }

        private void UpdateToTabProj(ParameterProj p)
        {
            cbProjOriginLocation.SetSelectedValue(p.Origin);

            var found = false;
            foreach (var item in cbProjElevation.Items)
            {
                if (item is ProjElevationItem itemValue)
                {
                    if (itemValue.ElevationType == p.ElevationType)
                    {
                        cbProjElevation.SelectedItem = item;
                        found = true;
                        break;
                    }
                }
            }
            if (found == false) cbProjElevation.SelectedIndex = 0;

            txtProjElevation.Text = p.ElevationValue.ToString(@"G");

            found = false;
            foreach (var item in cbProjDefinition.Items)
            {
                if (item is ProjSourceItem itemValue)
                {
                    if (itemValue.SourceType == p.DefinitionSource &&
                        itemValue.FilePath == p.DefinitionFileName)
                    {
                        cbProjDefinition.SelectedItem = item;
                        found = true;
                        break;
                    }
                }
            }
            if (found == false) cbProjDefinition.SelectedIndex = 0;

            txtProjDefinition.Text = p.Definition?.ToWindowsFormat() ?? string.Empty;
            txtProjCoordinateOffset.Text = GetOffsetsString(p.Offset);
        }

        #endregion

        #region UpdateFrom UI

        private bool UpdateFromTabs(GeoreferencedSetting setting)
        {
            var retAuto = UpdateFromTabAuto(setting.Auto);
            var retEnu = UpdateFromTabEnu(setting.Enu);
            var retLocal = UpdateFromTabLocal(setting.Local);
            var retProj = UpdateFromTabProj(setting.Proj);

            if (tabMain.SelectedTab == tabPageAuto)
            {
                setting.Mode = GeoreferencedMode.Auto;
                return retAuto;
            }

            if (tabMain.SelectedTab == tabPageEnu)
            {
                setting.Mode = GeoreferencedMode.Enu;
                return retEnu;
            }

            if (tabMain.SelectedTab == tabPageLocal)
            {
                setting.Mode = GeoreferencedMode.Local;
                return retLocal;
            }

            if (tabMain.SelectedTab == tabPageProj)
            {
                setting.Mode = GeoreferencedMode.Proj;
                return retProj;
            }

            return false;
        }

        private bool UpdateFromTabAuto(ParameterAuto p)
        {
            p.Origin = cbAutoOriginLocation.GetSelectedValue<OriginType>();
            return true;
        }

        private bool UpdateFromTabEnu(ParameterEnu p)
        {
            var isActive = tabMain.SelectedTab == tabPageEnu;
            var ret = true;

            TextBox errorTextBox = null;

            p.Origin = cbEnuOriginLocation.GetSelectedValue<OriginType>();

            if (cbEnuAlignOriginToSitePlaneCenter.Checked)
            {
                p.AlignOriginToSitePlaneCenter = true;
                p.OriginOffset = null;
            }
            else
            {
                p.AlignOriginToSitePlaneCenter = false;

                if (TryParseNumber(txtEnuModelOriginN, out var y) == false)
                {
                    errorTextBox = errorTextBox ?? txtEnuModelOriginN;
                    ret = false;
                }

                if (TryParseNumber(txtEnuModelOriginE, out var x) == false)
                {
                    errorTextBox = errorTextBox ?? txtEnuModelOriginE;
                    ret = false;
                }

                if (TryParseNumber(txtEnuModelOriginH, out var z) == false)
                {
                    errorTextBox = errorTextBox ?? txtEnuModelOriginH;
                    ret = false;
                }

                if (ret)
                {
                    p.OriginOffset = new[] { x, y, z };
                }
            }

            p.UseProjectLocation = cbEnuUseProjectLocation.Checked;
            p.UseAutoAlignToGround = cbEnuAutoAlignToGround.Checked;

            if (TryParseLatitude(txtEnuLatitude, out var latitude))
            {
                p.Latitude = latitude;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtEnuLatitude;
                ret = false;
            }

            if (TryParseLongitude(txtEnuLongitude, out var longitude))
            {
                p.Longitude = longitude;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtEnuLongitude;
                ret = false;
            }

            if (TryParseHeight(txtEnuHeight, out var height))
            {
                p.Height = height;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtEnuHeight;
                ret = false;
            }

            if (TryParseRotation(txtEnuRotation, out var rotation))
            {
                p.Rotation = rotation;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtEnuRotation;
                ret = false;
            }

            if (isActive && errorTextBox != null)
            {
                errorTextBox.Focus();
            }

            return ret;
        }

        private bool UpdateFromTabLocal(ParameterLocal p)
        {
            var isActive = tabMain.SelectedTab == tabPageLocal;
            var ret = true;

            TextBox errorTextBox = null;

            p.Origin = cbLocalOriginLocation.GetSelectedValue<OriginType>();

            if (cbLocalAlignOriginToSitePlaneCenter.Checked)
            {
                p.AlignOriginToSitePlaneCenter = true;
                p.OriginOffset = null;
            }
            else
            {
                p.AlignOriginToSitePlaneCenter = false;

                if (TryParseNumber(txtLocalModelOriginN, out var y) == false)
                {
                    errorTextBox = errorTextBox ?? txtLocalModelOriginN;
                    ret = false;
                }

                if (TryParseNumber(txtLocalModelOriginE, out var x) == false)
                {
                    errorTextBox = errorTextBox ?? txtLocalModelOriginE;
                    ret = false;
                }

                if (TryParseNumber(txtLocalModelOriginH, out var z) == false)
                {
                    errorTextBox = errorTextBox ?? txtLocalModelOriginH;
                    ret = false;
                }

                if (ret)
                {
                    p.OriginOffset = new[] { x, y, z };
                }
            }

            p.UseProjectLocation = cbLocalUseProjectLocation.Checked;

            if (TryParseLatitude(txtLocalLatitude, out var latitude))
            {
                p.Latitude = latitude;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtLocalLatitude;
                ret = false;
            }

            if (TryParseLongitude(txtLocalLongitude, out var longitude))
            {
                p.Longitude = longitude;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtLocalLongitude;
                ret = false;
            }

            if (TryParseHeight(txtLocalHeight, out var height))
            {
                p.Height = height;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtLocalHeight;
                ret = false;
            }

            if (TryParseRotation(txtLocalRotation, out var rotation))
            {
                p.Rotation = rotation;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtLocalRotation;
                ret = false;
            }

            if (isActive && errorTextBox != null)
            {
                errorTextBox.Focus();
            }

            return ret;
        }

        private bool UpdateFromTabProj(ParameterProj p)
        {
            var isActive = tabMain.SelectedTab == tabPageProj;
            var ret = true;

            p.Origin = cbProjOriginLocation.GetSelectedValue<OriginType>();

            TextBox errorTextBox = null;

            if (TryParseOffsets(txtProjCoordinateOffset.Text, out var offsets))
            {
                p.Offset = offsets;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtProjCoordinateOffset;
                ret = false;
            }

            //获取投影高程
            {
                if (TryParseHeight(txtProjElevation, out var height))
                {
                    p.ElevationValue = height;
                }
                else
                {
                    errorTextBox = errorTextBox ?? txtProjElevation;
                    ret = false;
                }

                if (cbProjElevation.SelectedItem is ProjElevationItem item)
                {
                    p.ElevationType = item.ElevationType;
                }
                else
                {
                    p.ElevationType = ProjElevationType.Custom;
                }
            }

            if (_Host.CheckProjDefinition(txtProjDefinition.Text, out _))
            {
                p.Definition = txtProjDefinition.Text.Trim();

                if (cbProjDefinition.SelectedItem is ProjSourceItem sourceItem)
                {
                    p.DefinitionSource = sourceItem.SourceType;
                    p.DefinitionFileName = sourceItem.FilePath;
                }
                else
                {
                    p.DefinitionSource = ProjSourceType.Custom;
                    p.DefinitionFileName = null;
                }
            }
            else
            {
                errorTextBox = errorTextBox ?? txtProjDefinition;
                ret = false;
            }

            if (isActive && errorTextBox != null)
            {
                errorTextBox.Focus();
            }

            return ret;
        }

        #endregion

        private void btnReset_Click(object sender, EventArgs e)
        {
            var setting = _Host.CreateDefaultSetting();
            UpdateToTabs(setting);
        }

        private void cbAutoOriginLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var originType = cbAutoOriginLocation.GetSelectedValue<OriginType>();

            var d = _Host.CreateDefaultSetting();
            d.Mode = GeoreferencedMode.Auto;
            d.Auto.Origin = originType;

            txtAutoSummary.Text = d.GetDetails(_Host);
        }

        private void cbEnuOriginLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEnuUseProjectLocation.Checked)
            {
                var originType = cbEnuOriginLocation.GetSelectedValue<OriginType>();
                txtEnuRotation.Text = _Host.IsTrueNorth(originType)
                    ? GetDoubleString(0, 10)
                    : GetDoubleString(_SiteInfo.Rotation, 10);
            }
        }

        private void cbLocalOriginLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocalUseProjectLocation.Checked)
            {
                var originType = cbLocalOriginLocation.GetSelectedValue<OriginType>();
                txtLocalRotation.Text = _Host.IsTrueNorth(originType)
                    ? GetDoubleString(0, 10)
                    : GetDoubleString(_SiteInfo.Rotation, 10);
            }
        }

        private void cbProjElevation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProjElevation.SelectedItem is ProjElevationItem item)
            {
                txtProjElevation.Text = item.Value.ToString(@"G");

                if (item.ElevationType == ProjElevationType.Custom)
                {
                    txtProjElevation.ReadOnly = false;
                    txtProjElevation.Focus();
                }
                else
                {
                    txtProjElevation.ReadOnly = true;
                }
            }
        }

        private void cbProjDefinition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProjDefinition.SelectedItem is ProjSourceItem item)
            {
                switch (item.SourceType)
                {
                    case ProjSourceType.Create:
                    {
                        cbProjDefinition.SelectedIndex = 0;

                        var form = new FormProjCreate(_Host);
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            var definition = form.Definition;
                            if (string.IsNullOrWhiteSpace(definition) == false)
                            {
                                txtProjDefinition.Text = definition;
                            }
                        }
                        break;
                    }
                    case ProjSourceType.Browse:
                    {
                        var found = openFileDialog1.ShowDialog(this) == DialogResult.OK &&
                                     UseProjFile(openFileDialog1.FileName);
                        if (found == false) cbProjDefinition.SelectedIndex = 0;
                        break;
                    }
                    case ProjSourceType.Custom:
                        txtProjDefinition.ReadOnly = false;
                        break;
                    case ProjSourceType.Embed:
                        txtProjDefinition.Text = item.ProjDefinition.ToWindowsFormat();
                        txtProjDefinition.ReadOnly = true;
                        break;
                    case ProjSourceType.Default:
                        txtProjDefinition.Text = item.ProjDefinition.ToWindowsFormat();
                        txtProjDefinition.ReadOnly = true;
                        break;
                    case ProjSourceType.MetadataXml:
                    {
                        if (MetadataXml.TryParse(item.FilePath, out var meta) &&
                            meta.TryGetProj(_Host, out var proj))
                        {
                            UseMetadataXmlProj(proj);
                        }
                        break;
                    }
                    case ProjSourceType.ProjectFolder:
                        txtProjDefinition.Text = item.ProjDefinition.ToWindowsFormat();
                        txtProjDefinition.ReadOnly = true;
                        break;
                    case ProjSourceType.Recently:
                        txtProjDefinition.Text = item.ProjDefinition.ToWindowsFormat();
                        txtProjDefinition.ReadOnly = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void txtProjDefinition_Enter(object sender, EventArgs e)
        {
            txtProjDefinition.ForeColor = SystemColors.WindowText;
        }

        private void txtProjDefinition_DoubleClick(object sender, EventArgs e)
        {
            //双击投影定义输入框, 如果当前内容只读则自动切换到“自定义”状态
            if (txtProjDefinition.ReadOnly)
            {
                cbProjDefinition.SelectedIndex = 0;
            }
        }

        private bool TryParseLatitude(TextBox text, out double n)
        {
            return TryParse(text, -90.0, 90.0, GeoStrings.ErrorMessageLatitude, out n);
        }

        private bool TryParseLongitude(TextBox text, out double n)
        {
            return TryParse(text, -180.0, 180.0, GeoStrings.ErrorMessageLongitude, out n);
        }

        private bool TryParseHeight(TextBox text, out double n)
        {
            return TryParse(text, double.MinValue, double.MaxValue, GeoStrings.ErrorMessageHeight, out n);
        }

        private bool TryParseRotation(TextBox text, out double n)
        {
            return TryParse(text, -360.0, 360.0, GeoStrings.ErrorMessageRotation, out n);
        }

        private bool TryParseNumber(TextBox text, out double n)
        {
            return TryParse(text, double.MinValue, double.MaxValue, GeoStrings.ErrorMessageNumber, out n);
        }

        private bool TryParse(TextBox text, double min, double max, string errorInfo, out double n)
        {
            errorProvider1.SetError(text, null);
            n = 0.0;


            if (double.TryParse(text.Text, out n) &&
                n>= min && n <= max)
            {
                return true;
            }

            errorProvider1.SetError(text, errorInfo);
            return false; 
        }

        private bool UseProjFile(string projFilePath)
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
                cbProjDefinition.Items.Add(newItem);
                cbProjDefinition.SelectedItem = newItem;

                _Host.CheckInProjFile(projFilePath);

                return true;
            }

            return false;
        }

        private void UseMetadataXmlProj(MetadataXmlProj metaProj)
        {
            txtProjCoordinateOffset.Text = metaProj.SrsOrigin == null
                ? @"0,0,0"
                : string.Join(@",", metaProj.SrsOrigin);
            txtProjDefinition.Text = metaProj.Srs;

            cbProjDefinition.SelectedIndex = 0; //ProjSourceType.Custom
            cbProjElevation.SelectedIndex = 0;  //ProjElevationType.Default
        }

        private bool CheckProjFile(string projFilePath)
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

        private bool TryParseOffsets(string s, out double[] offsets)
        {
            var values = s.Split(new[] { ',', '/', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var parameterCount = values.Length >= 7 ? 7 : 3;
            var valueItems = new List<double>();
            for (var i = 0; i < parameterCount; i++)
            {
                if (values.Length > i && double.TryParse(values[i], out var n))
                {
                    valueItems.Add(n);
                }
                else
                {
                    valueItems.Add(0.0);
                }
            }

            offsets = valueItems.ToArray();
            return true;
        }

        private static string GetOffsetsString(double[] offsets)
        {
            if (offsets == null) return @"0,0,0";
            return string.Join(@",", offsets.Select(x => GetDoubleString(x, 10)));
        }

        private static string GetDoubleString(double n, int digits)
        {
            return Math.Round(n, digits).ToString(CultureInfo.InvariantCulture);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (UpdateFromTabs(Setting))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbEnuAlignOriginToSitePlaneCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnuAlignOriginToSitePlaneCenter.Checked)
            {
                txtEnuModelOriginN.Text = GeoStrings.Auto;
                txtEnuModelOriginE.Text = GeoStrings.Auto;
                txtEnuModelOriginH.Text = GetDoubleString(0, 10);

                txtEnuModelOriginN.ReadOnly = true;
                txtEnuModelOriginE.ReadOnly = true;
                txtEnuModelOriginH.ReadOnly = true;
            }
            else
            {
                var modelOrigin = _DefaultModelOrigin ?? new double[] { 0, 0, 0 };
                txtEnuModelOriginN.Text = GetDoubleString(modelOrigin[1], 10);
                txtEnuModelOriginE.Text = GetDoubleString(modelOrigin[0], 10);
                txtEnuModelOriginH.Text = GetDoubleString(modelOrigin[2], 10); 

                txtEnuModelOriginN.ReadOnly = false;
                txtEnuModelOriginE.ReadOnly = false;
                txtEnuModelOriginH.ReadOnly = false;
            }
        }

        private void cbEnuUseProjectLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnuUseProjectLocation.Checked)
            {
                txtEnuLatitude.Text = GetDoubleString(_SiteInfo.Latitude, 10);
                txtEnuLongitude.Text = GetDoubleString(_SiteInfo.Longitude, 10);
                txtEnuHeight.Text = GetDoubleString(_SiteInfo.Height, 6);

                var originType = cbEnuOriginLocation.GetSelectedValue<OriginType>();
                txtEnuRotation.Text = _Host.IsTrueNorth(originType) 
                    ? GetDoubleString(0, 10) 
                    : GetDoubleString(_SiteInfo.Rotation, 10);
            }

            txtEnuLatitude.ReadOnly = cbEnuUseProjectLocation.Checked;
            txtEnuLongitude.ReadOnly = cbEnuUseProjectLocation.Checked;
            txtEnuHeight.ReadOnly = cbEnuUseProjectLocation.Checked;
            txtEnuRotation.ReadOnly = cbEnuUseProjectLocation.Checked;
        }

        private void cbLocalAlignOriginToSitePlaneCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLocalAlignOriginToSitePlaneCenter.Checked)
            {
                txtLocalModelOriginN.Text = GeoStrings.Auto;
                txtLocalModelOriginE.Text = GeoStrings.Auto;
                txtLocalModelOriginH.Text = GetDoubleString(0, 10);

                txtLocalModelOriginN.ReadOnly = true;
                txtLocalModelOriginE.ReadOnly = true;
                txtLocalModelOriginH.ReadOnly = true;
            }
            else
            {
                var modelOrigin = _DefaultModelOrigin ?? new double[] { 0, 0, 0 };
                txtLocalModelOriginN.Text = GetDoubleString(modelOrigin[1], 10);
                txtLocalModelOriginE.Text = GetDoubleString(modelOrigin[0], 10);
                txtLocalModelOriginH.Text = GetDoubleString(modelOrigin[2], 10);

                txtLocalModelOriginN.ReadOnly = false;
                txtLocalModelOriginE.ReadOnly = false;
                txtLocalModelOriginH.ReadOnly = false;
            }
        }

        private void cbLocalUseProjectLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLocalUseProjectLocation.Checked)
            {
                txtLocalLatitude.Text = GetDoubleString(_SiteInfo.Latitude, 10);
                txtLocalLongitude.Text = GetDoubleString(_SiteInfo.Longitude, 10);
                txtLocalHeight.Text = GetDoubleString(_SiteInfo.Height, 6);

                var originType = cbLocalOriginLocation.GetSelectedValue<OriginType>();
                txtLocalRotation.Text = _Host.IsTrueNorth(originType)
                    ? GetDoubleString(0, 10)
                    : GetDoubleString(_SiteInfo.Rotation, 10);
            }

            txtLocalLatitude.ReadOnly = cbLocalUseProjectLocation.Checked;
            txtLocalLongitude.ReadOnly = cbLocalUseProjectLocation.Checked;
            txtLocalHeight.ReadOnly = cbLocalUseProjectLocation.Checked;
            txtLocalRotation.ReadOnly = cbLocalUseProjectLocation.Checked;
        }

        private void btnProjDefinitionSave_Click(object sender, EventArgs e)
        {
            var filePath = _Host.GetDefaultProjFilePath();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                btnProjDefinitionSave.Enabled = false;
                return;
            }

            if (_Host.CheckProjDefinition(txtProjDefinition.Text, out var wkt) == false)
            {
                txtProjDefinition.Focus();
                return;
            }

            var filePathInfo = $@" {GeoStrings.FilePath}: {filePath}";
            if (!this.ShowConfirmBox(GeoStrings.ConfirmSaveProjDefinition + filePathInfo))
            {
                return;
            }

            if (File.Exists(filePath) && !this.ShowConfirmBox(GeoStrings.ConfirmOverwriteFile + filePathInfo))
            {
                return;
            }

            if (_Host.SaveProjFile(filePath, wkt))
            {
                this.ShowMessageBox(GeoStrings.SaveSuccessfully);
            }
        }

        private void btnProjCoordinateOffsetSave_Click(object sender, EventArgs e)
        {
            var filePath = _Host.GetDefaultOffsetFilePath();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                btnProjCoordinateOffsetSave.Enabled = false;
                return;
            }

            if (TryParseOffsets(txtProjCoordinateOffset.Text, out var offset) == false)
            {
                txtProjCoordinateOffset.Focus();
                return;
            }

            var filePathInfo = $@" {GeoStrings.FilePath}: {filePath}";
            if (!this.ShowConfirmBox(GeoStrings.ConfirmSaveOffsets + filePathInfo))
            {
                return;
            }

            if (File.Exists(filePath) && !this.ShowConfirmBox(GeoStrings.ConfirmOverwriteFile + filePathInfo))
            {
                return;
            }

            if (_Host.SaveOffsetFile(filePath, offset))
            {
                this.ShowMessageBox(GeoStrings.SaveSuccessfully);
            }
        }
    }
}
