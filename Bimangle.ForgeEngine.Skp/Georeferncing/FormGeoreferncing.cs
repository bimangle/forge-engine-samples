using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bimangle.ForgeEngine.Common.Georeferenced;
using Bimangle.ForgeEngine.Common.Types;
using Bimangle.ForgeEngine.Common.Utils;
using Bimangle.ForgeEngine.Skp.Utility;
using Form = System.Windows.Forms.Form;

namespace Bimangle.ForgeEngine.Skp.Georeferncing
{
    partial class FormGeoreferncing : Form
    {
        private readonly string _ProjectFilePath = null;
        private readonly SiteInfo _SiteInfo = null;
        private readonly IGeoreferncingHost _Host;
        private readonly string _DefaultTitle;

        public FormGeoreferncing(IGeoreferncingHost host, GeoreferencedSetting setting)
            : this()
        {
            _Host = host ?? throw new ArgumentNullException(nameof(host));
            _SiteInfo = _Host.GetModelSiteInfo();
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
            #region 初始化 "原点位置选择" 下拉框

            {
                var originTypes = _Host.GetSupportOriginTypes();
                if (originTypes == null || originTypes.Length == 0)
                {
                    originTypes = new [] {OriginType.Internal};
                }

                var items = new List<ItemValue<OriginType>>();
                foreach (var originType in originTypes)
                {
                    string text;
                    switch (originType)
                    {
                        case OriginType.Internal:
                            text = GeoStrings.OriginTypeInternal;
                            break;
                        case OriginType.Project:
                            text = GeoStrings.OriginTypeProject;
                            break;
                        case OriginType.Shared:
                            text = GeoStrings.OriginTypeShared;
                            break;
                        case OriginType.Survey:
                            text = GeoStrings.OriginTypeSurvey;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(originType.ToString());
                    }
                    items.Add(new ItemValue<OriginType>(text, originType));
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

            #region 初始化 "投影参考定义" 下拉框
            {
                var items = _Host.GetProjSourceItems();
                cbProjDefinition.Items.AddRange(items.OfType<object>().ToArray());
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
                        var projDefinition = _Host.GetProjDefinition(path);
                        if (string.IsNullOrWhiteSpace(projDefinition) == false)
                        {
                            e.Effect = DragDropEffects.Link;
                        }
                    }
                };
            }
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

            cbEnuUseProjectLocation.Checked = p.UseProjectLocation;
            txtEnuLatitude.Text = GetDoubleString(p.Latitude, 10);
            txtEnuLongitude.Text = GetDoubleString(p.Longitude, 10);
            txtEnuHeight.Text = GetDoubleString(p.Height, 6);
            txtEnuRotation.Text = GetDoubleString(p.Rotation, 10);
        }

        private void UpdateToTabLocal(ParameterLocal p)
        {
            cbLocalOriginLocation.SetSelectedValue(p.Origin);

            cbLocalAlignOriginToSitePlaneCenter.Checked = p.AlignOriginToSitePlaneCenter;

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

            p.Origin = cbEnuOriginLocation.GetSelectedValue<OriginType>();
            p.AlignOriginToSitePlaneCenter = cbEnuAlignOriginToSitePlaneCenter.Checked;
            p.UseProjectLocation = cbEnuUseProjectLocation.Checked;

            TextBox errorTextBox = null;

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

            p.Origin = cbLocalOriginLocation.GetSelectedValue<OriginType>();
            p.AlignOriginToSitePlaneCenter = cbLocalAlignOriginToSitePlaneCenter.Checked;
            p.UseProjectLocation = cbLocalUseProjectLocation.Checked;

            TextBox errorTextBox = null;

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

            var setting = _Host.CreateTargetSetting(d);
            txtAutoSummary.Text = setting.GetDetails(true);
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

        private void cbProjDefinition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProjDefinition.SelectedItem is ProjSourceItem item)
            {
                switch (item.SourceType)
                {
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

        private void txtProjCoordinateOffset_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.SetError(txtProjCoordinateOffset, null);

            var s = txtProjCoordinateOffset.Text;
            if (string.IsNullOrWhiteSpace(s))
            {
                txtProjCoordinateOffset.Text = @"0,0,0";
            }
            else if (TryParseOffsets(s, out var offsets) == false)
            {
                e.Cancel = true;
                errorProvider1.SetError(btnProjCoordinateOffsetSave, GeoStrings.InvalidData);
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

        private void txtProjDefinition_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var s = txtProjDefinition.Text;
            if (_Host.CheckProjDefinition(s, out _) == false)
            {
                txtProjDefinition.ForeColor = Color.Red;
                errorProvider1.SetError(txtProjDefinition, GeoStrings.InvalidProjectDefinition);
            }
            else
            {
                txtProjDefinition.ForeColor = Color.Green;
                errorProvider1.SetError(txtProjDefinition, null);
            }
        }

        private void txtLocalLatitude_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryParseLatitude(txtLocalLatitude, out _);
        }

        private void txtLocalLongitude_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryParseLongitude(txtLocalLongitude, out _);
        }

        private void txtLocalHeight_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryParseHeight(txtLocalHeight, out _);
        }

        private void txtLocalRotation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryParseRotation(txtLocalRotation, out _);
        }

        private void txtEnuLatitude_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryParseLatitude(txtEnuLatitude, out _);
        }

        private void txtEnuLongitude_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryParseLongitude(txtEnuLongitude, out _);
        }

        private void txtEnuHeight_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryParseHeight(txtEnuHeight,  out _);
        }

        private void txtEnuRotation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryParseRotation(txtEnuRotation, out _);
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

        private bool TryParseOffsets(string s, out double[] offsets)
        {
            var values = s.Split(new[] { ',', '/', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var valueItems = new List<double>();
            for (var i = 0; i < 3; i++)
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
            return string.Join(@",", offsets.Select(x => GetDoubleString(x, 6)));
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
