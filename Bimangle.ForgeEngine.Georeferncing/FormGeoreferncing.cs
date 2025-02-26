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
        public const string METRE_ZERO = @"0.0";
        public const string DEGREE_ZERO = @"0.0";

        private readonly string _ProjectFilePath = null;
        private readonly SiteInfo _SiteInfo = null;
        private readonly double[] _DefaultModelOrigin;
        private readonly IGeoreferncingHost _Host;
        private readonly string _DefaultTitle;

        private int _NextTestId = 1;

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

            btnTest.Visible = btnTest.Enabled = tabMain.SelectedTab == tabPageProj;
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

                txtEnuModelOriginN.OnValidating(t =>
                {
                    if (cbEnuAlignOriginToSitePlaneCenter.Checked == false) t.TryParseNumber(errorProvider1, out _);
                });
                txtEnuModelOriginE.OnValidating(t =>
                {
                    if (cbEnuAlignOriginToSitePlaneCenter.Checked == false) t.TryParseNumber(errorProvider1, out _);
                });
                txtEnuModelOriginH.OnValidating(t =>
                {
                    if (cbEnuAlignOriginToSitePlaneCenter.Checked == false) t.TryParseNumber(errorProvider1, out _);
                });

                txtEnuLatitude.OnValidating(t => { t.TryParseLatitude(errorProvider1, out _); });
                txtEnuLongitude.OnValidating(t => { t.TryParseLongitude(errorProvider1, out _); });
                txtEnuHeight.OnValidating(t => { t.TryParseHeight(errorProvider1, out _); });
                txtEnuRotation.OnValidating(t => { t.TryParseRotation(errorProvider1, out _); });
            }
            #endregion

            #region 初始化 暂不配准
            {
                _Host.Adapter.SetDirectionLetters(lblLocalModelOriginE, lblLocalModelOriginN, lblLocalModelOriginH);

                txtLocalModelOriginN.OnValidating(t =>
                {
                    if (cbLocalAlignOriginToSitePlaneCenter.Checked == false) t.TryParseNumber(errorProvider1, out _);
                });
                txtLocalModelOriginE.OnValidating(t =>
                {
                    if (cbLocalAlignOriginToSitePlaneCenter.Checked == false) t.TryParseNumber(errorProvider1, out _);
                });
                txtLocalModelOriginH.OnValidating(t =>
                {
                    if (cbLocalAlignOriginToSitePlaneCenter.Checked == false) t.TryParseNumber(errorProvider1, out _);
                });

                txtLocalLatitude.OnValidating(t => { t.TryParseLatitude(errorProvider1, out _); });
                txtLocalLongitude.OnValidating(t => { t.TryParseLongitude(errorProvider1, out _); });
                txtLocalHeight.OnValidating(t => { t.TryParseHeight(errorProvider1, out _); });
                txtLocalRotation.OnValidating(t => { t.TryParseRotation(errorProvider1, out _); });
            }
            #endregion

            #region 初始化 投影坐标

            #region 初始化 "投影参考定义" 下拉框
            {
                var items = _Host.GetProjSourceItems();
                cbProjDefinition.Items.Clear();
                cbProjDefinition.Items.AddRange(items.OfType<object>().ToArray());
            }
            #endregion

            #region 初始化 "水准面网格" 下拉框
            {
                var items = _Host.GetVerticalGeoidGridItems();
                cbGeoidGrid.Items.AddRange(items.OfType<object>().ToArray());
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

            txtGeoidConstantOffset.OnValidating(t => { t.TryParseHeight(errorProvider1, out _); });

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

            txtProjectionHeight.OnValidating(t => { t.TryParseHeight(errorProvider1, out _); });

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
                txtEnuModelOriginN.Text = p.OriginOffset[1].ToMetreString();
                txtEnuModelOriginE.Text = p.OriginOffset[0].ToMetreString();
                txtEnuModelOriginH.Text = p.OriginOffset[2].ToMetreString();
            }

            cbEnuUseProjectLocation.Checked = p.UseProjectLocation;
            txtEnuLatitude.Text = p.Latitude.ToLatLonString();
            txtEnuLongitude.Text = p.Longitude.ToLatLonString();
            txtEnuHeight.Text = p.Height.ToMetreString();
            txtEnuRotation.Text = p.Rotation.ToDegreeString();

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
                txtLocalModelOriginN.Text = p.OriginOffset[1].ToMetreString();
                txtLocalModelOriginE.Text = p.OriginOffset[0].ToMetreString();
                txtLocalModelOriginH.Text = p.OriginOffset[2].ToMetreString();
            }

            cbLocalUseProjectLocation.Checked = p.UseProjectLocation;
            txtLocalLatitude.Text = p.Latitude.ToLatLonString();
            txtLocalLongitude.Text = p.Longitude.ToLatLonString();
            txtLocalHeight.Text = p.Height.ToMetreString();
            txtLocalRotation.Text = p.Rotation.ToDegreeString();
        }

        private void UpdateToTabProj(ParameterProj p)
        {
            cbProjOriginLocation.SetSelectedValue(p.Origin);

            txtProjectionHeight.Text = p.ProjectionHeight.ToMetreString();

            var found = false;
            foreach (var item in cbGeoidGrid.Items)
            {
                if (item is VerticalGeoidGrid itemValue)
                {
                    if (itemValue.FileName == p.GeoidGrid)
                    {
                        cbGeoidGrid.SelectedItem = item;
                        found = true;
                        break;
                    }
                }
            }
            if (found == false) cbGeoidGrid.SelectedIndex = 0;

            txtGeoidConstantOffset.Text = p.GeoidConstantOffset.ToMetreString();

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
            txtProjCoordinateOffset.Text = p.GetOffsetString();
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

                if (txtEnuModelOriginN.TryParseNumber(errorProvider1, out var y) == false)
                {
                    errorTextBox = errorTextBox ?? txtEnuModelOriginN;
                    ret = false;
                }

                if (txtEnuModelOriginE.TryParseNumber(errorProvider1, out var x) == false)
                {
                    errorTextBox = errorTextBox ?? txtEnuModelOriginE;
                    ret = false;
                }

                if (txtEnuModelOriginH.TryParseNumber(errorProvider1, out var z) == false)
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

            if (txtEnuLatitude.TryParseLatitude(errorProvider1, out var latitude))
            {
                p.Latitude = latitude;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtEnuLatitude;
                ret = false;
            }

            if (txtEnuLongitude.TryParseLongitude(errorProvider1, out var longitude))
            {
                p.Longitude = longitude;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtEnuLongitude;
                ret = false;
            }

            if (txtEnuHeight.TryParseHeight(errorProvider1, out var height))
            {
                p.Height = height;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtEnuHeight;
                ret = false;
            }

            if (txtEnuRotation.TryParseRotation(errorProvider1, out var rotation))
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

                if (txtLocalModelOriginN.TryParseNumber(errorProvider1, out var y) == false)
                {
                    errorTextBox = errorTextBox ?? txtLocalModelOriginN;
                    ret = false;
                }

                if (txtLocalModelOriginE.TryParseNumber(errorProvider1, out var x) == false)
                {
                    errorTextBox = errorTextBox ?? txtLocalModelOriginE;
                    ret = false;
                }

                if (txtLocalModelOriginH.TryParseNumber(errorProvider1, out var z) == false)
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

            if (txtLocalLatitude.TryParseLatitude(errorProvider1, out var latitude))
            {
                p.Latitude = latitude;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtLocalLatitude;
                ret = false;
            }

            if (txtLocalLongitude.TryParseLongitude(errorProvider1, out var longitude))
            {
                p.Longitude = longitude;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtLocalLongitude;
                ret = false;
            }

            if (txtLocalHeight.TryParseHeight(errorProvider1, out var height))
            {
                p.Height = height;
            }
            else
            {
                errorTextBox = errorTextBox ?? txtLocalHeight;
                ret = false;
            }

            if (txtLocalRotation.TryParseRotation(errorProvider1, out var rotation))
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

            if (Setting?.Proj == null)
            {
                p.OffsetType = ProjOffsetType.None;
                p.Offset = null;
            }
            else
            {
                p.OffsetType = Setting.Proj.OffsetType;
                p.Offset = Setting.Proj.Offset.CloneArray();
            }


            TextBox errorTextBox = null;

            //获取投影面高程
            {
                if(txtProjectionHeight.TryParseHeight(errorProvider1, out var height))
                {
                    p.ProjectionHeight = height;
                }
                else
                {
                    errorTextBox = errorTextBox ?? txtProjectionHeight;
                    ret = false;
                }
            }

            //获取大地水准面高校正
            {
                if (txtGeoidConstantOffset.TryParseHeight(errorProvider1, out var constantOffset))
                {
                    p.GeoidConstantOffset = constantOffset;
                }
                else
                {
                    errorTextBox = errorTextBox ?? txtGeoidConstantOffset;
                    ret = false;
                }

                if (cbGeoidGrid.SelectedItem is VerticalGeoidGrid item &&
                    item.IsInstalled)
                {
                    p.GeoidGrid = item.FileName;
                }
                else
                {
                    p.GeoidGrid = null;
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

        private void UpdateFromTabProjX(ParameterProj p)
        {
            p.Origin = cbProjOriginLocation.GetSelectedValue<OriginType>();

            //获取大地水准面高校正
            {
                if (txtGeoidConstantOffset.TryParseHeight(errorProvider1, out var constantOffset))
                {
                    p.GeoidConstantOffset = constantOffset;
                }
                else
                {
                    p.GeoidConstantOffset = 0.0;
                }

                if (cbGeoidGrid.SelectedItem is VerticalGeoidGrid item &&
                    item.IsInstalled)
                {
                    p.GeoidGrid = item.FileName;
                }
                else
                {
                    p.GeoidGrid = null;
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
                p.Definition = null;
            }
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
                    ? DEGREE_ZERO
                    : _SiteInfo.Rotation.ToDegreeString();
            }
        }

        private void cbLocalOriginLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocalUseProjectLocation.Checked)
            {
                var originType = cbLocalOriginLocation.GetSelectedValue<OriginType>();
                txtLocalRotation.Text = _Host.IsTrueNorth(originType)
                    ? DEGREE_ZERO
                    : _SiteInfo.Rotation.ToDegreeString();
            }
        }

        private void cbVerticalGeoidGrids_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGeoidGrid.SelectedItem is VerticalGeoidGrid item &&
                item.IsInstalled == false)
            {
                cbGeoidGrid.SelectedIndex = 0;
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
            if (Setting.Proj == null) Setting.Proj = new ParameterProj();
            var p = Setting.Proj;

            if (metaProj?.SrsOrigin != null && metaProj.SrsOrigin.Length >= 3)
            {
                p.OffsetType = ProjOffsetType._3D_Params3;
                p.Offset = new double[7]
                {
                    metaProj.SrsOrigin[0],
                    metaProj.SrsOrigin[1],
                    metaProj.SrsOrigin[2],
                    0.0, 0.0, 0.0, 
                    0.0
                };
            }
            else
            {
                p.OffsetType = ProjOffsetType.None;
                p.Offset = null;
            }

            txtProjCoordinateOffset.Text = p.GetOffsetString();
            txtProjDefinition.Text = metaProj?.Srs;

            cbProjDefinition.SelectedIndex = 0; //ProjSourceType.Custom
            cbGeoidGrid.SelectedIndex = 0;  // None
            txtGeoidConstantOffset.Text = @"0";
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
                txtEnuModelOriginH.Text = METRE_ZERO;

                txtEnuModelOriginN.ReadOnly = true;
                txtEnuModelOriginE.ReadOnly = true;
                txtEnuModelOriginH.ReadOnly = true;
            }
            else
            {
                var modelOrigin = _DefaultModelOrigin ?? new double[] { 0, 0, 0 };
                txtEnuModelOriginN.Text = modelOrigin[1].ToMetreString();
                txtEnuModelOriginE.Text = modelOrigin[0].ToMetreString();
                txtEnuModelOriginH.Text = modelOrigin[2].ToMetreString(); 

                txtEnuModelOriginN.ReadOnly = false;
                txtEnuModelOriginE.ReadOnly = false;
                txtEnuModelOriginH.ReadOnly = false;
            }
        }

        private void cbEnuUseProjectLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnuUseProjectLocation.Checked)
            {
                txtEnuLatitude.Text = _SiteInfo.Latitude.ToLatLonString();
                txtEnuLongitude.Text = _SiteInfo.Longitude.ToLatLonString();
                txtEnuHeight.Text = _SiteInfo.Height.ToMetreString();

                var originType = cbEnuOriginLocation.GetSelectedValue<OriginType>();
                txtEnuRotation.Text = _Host.IsTrueNorth(originType) 
                    ? DEGREE_ZERO
                    : _SiteInfo.Rotation.ToDegreeString();
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
                txtLocalModelOriginH.Text = METRE_ZERO;

                txtLocalModelOriginN.ReadOnly = true;
                txtLocalModelOriginE.ReadOnly = true;
                txtLocalModelOriginH.ReadOnly = true;
            }
            else
            {
                var modelOrigin = _DefaultModelOrigin ?? new double[] { 0, 0, 0 };
                txtLocalModelOriginN.Text = modelOrigin[1].ToMetreString();
                txtLocalModelOriginE.Text = modelOrigin[0].ToMetreString();
                txtLocalModelOriginH.Text = modelOrigin[2].ToMetreString();

                txtLocalModelOriginN.ReadOnly = false;
                txtLocalModelOriginE.ReadOnly = false;
                txtLocalModelOriginH.ReadOnly = false;
            }
        }

        private void cbLocalUseProjectLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLocalUseProjectLocation.Checked)
            {
                txtLocalLatitude.Text = _SiteInfo.Latitude.ToLatLonString();
                txtLocalLongitude.Text = _SiteInfo.Longitude.ToLatLonString();
                txtLocalHeight.Text = _SiteInfo.Height.ToMetreString();

                var originType = cbLocalOriginLocation.GetSelectedValue<OriginType>();
                txtLocalRotation.Text = _Host.IsTrueNorth(originType)
                    ? DEGREE_ZERO
                    : _SiteInfo.Rotation.ToDegreeString();
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

            var filePathInfo = $@" {GeoStrings.FilePath}: {filePath}";
            if (!this.ShowConfirmBox(GeoStrings.ConfirmSaveOffsets + filePathInfo))
            {
                return;
            }

            if (File.Exists(filePath) && !this.ShowConfirmBox(GeoStrings.ConfirmOverwriteFile + filePathInfo))
            {
                return;
            }

            var p = Setting.Proj;
            var projOffsetType = p?.OffsetType ?? ProjOffsetType.None;
            var projOffset = p?.Offset;
            if (_Host.SaveOffsetFile(filePath, projOffsetType, projOffset))
            {
                this.ShowMessageBox(GeoStrings.SaveSuccessfully);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var p = new ParameterProj();
            if (UpdateFromTabProj(p) == false) return;


            var form = new FormGeoreferncingTest(_Host, p, _NextTestId++);
            form.Show(this);
            form.Location = new Point(Right, Top);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var p = Setting.Proj?.Clone() ?? new ParameterProj();
            UpdateFromTabProjX(p);

            var form = new FormTransformP4(_Host, p);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                //更新
                if (Setting.Proj == null) Setting.Proj = new ParameterProj();
                Setting.Proj.Offset = form.Parameter.GetOffsetForCalc();
                Setting.Proj.OffsetType = form.Parameter.OffsetType;
                UpdateToTabProj(Setting.Proj);
            }
        }
    }
}
