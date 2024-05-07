using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Config;
using Bimangle.ForgeEngine.Revit.Core;
using Bimangle.ForgeEngine.Revit.UI.Controls;
using Bimangle.ForgeEngine.Revit.Utility;
using Newtonsoft.Json.Linq;
using Control = System.Windows.Forms.Control;
using Form = System.Windows.Forms.Form;
using Ef = Bimangle.ForgeEngine.Common.Utils.ExtendFeatures;

namespace Bimangle.ForgeEngine.Revit.UI
{
    partial class FormExport : Form, IExportForm
    {
        [DllImport("user32 ", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetAsyncKeyState(int vKey);

#pragma warning disable 414
        private bool _IsClosing;
#pragma warning restore 414

        private IExportControl _Exporter;
        private View3D _View;
        private bool _EnabledSampling = false;

        private Task<LicenseStatus> _LicenseStatus;

        public FormExport()
        {
            InitializeComponent();
        }

        public FormExport(UIDocument uidoc, View3D view, AppConfig config, Dictionary<long, bool> elementIds, string target)
            : this()
        {
            _View = view;

            tabList.TabPages.Clear();

            var exporters = new List<IExportControl>();

            void AddPage(Control control, IExportControl exporter)
            {
                var tab = new TabPage();
                tabList.Controls.Add(tab);
                tab.Text = exporter.Title;
                tab.Name = $@"tabPage{exporter.Title}";
                tab.UseVisualStyleBackColor = true;
                tab.ImageKey = exporter.Icon;
                tab.Controls.Add(control);
                tab.Tag = exporter;
                control.Dock = DockStyle.Fill;

                if (exporter.Title == target)
                {
                    _Exporter = exporter;
                }

                exporters.Add(exporter);
            }

#if !EXPRESS
#region 增加 SvfZip 导出
            {
                var control = new ExportSvfzip();
                var exporter = (IExportControl)control;
                AddPage(control, exporter);
            }
#endregion
#endif

#region 增加 glTF/glb 导出
            {
                var control = new ExportGltf();
                var exporter = (IExportControl)control;
                AddPage(control, exporter);
            }
#endregion

#region 增加 3D Tiles 导出
            {
                var control = new ExportCesium3DTiles();
                var exporter = (IExportControl)control;
                AddPage(control, exporter);
            }
#endregion

            if (_Exporter == null) _Exporter = exporters.First();

            foreach (var exporter in exporters)
            {
                exporter.Init(uidoc, view, config, elementIds);
            }

            foreach (TabPage tab in tabList.TabPages)
            {
                if (tab.Tag == _Exporter)
                {
                    tabList.SelectTab(tab);
                    break;
                }
            }

            //初始化扩展属性
            InitExtendFeatures();
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            Icon =Icon.FromHandle(Properties.Resources.Converter_32px_1061192.GetHicon());
            Text = $@"{PackageInfo.ProductName} - Samples v{PackageInfo.ProductVersion}";

            //如果启动时按下 Shift 键, 则进入缓存模式
            const int VK_SHIFT = 16;
            if ((GetAsyncKeyState(VK_SHIFT) & 0x8000) != 0)
            {
                Text += @" - [Sampling]";
                _EnabledSampling = true;
            }

            //授权状态相关
            _LicenseStatus = LicenseStatus.Get(OnLicenseStatus);

            //变更扩展属性后自动保存扩展属性设置
            FormHelper
                .ToArray(tsmiRenderingPerformancePreferred, tsmiDisableGeometrySimplification)
                .AddEventListenerForCheckedChanged(SaveExtendFeatures);
        }

        private void FormExportSvfzip_Shown(object sender, EventArgs e)
        {
        }

        private void FormExportSvfzip_FormClosing(object sender, FormClosingEventArgs e)
        {
            _IsClosing = true;
        }

        private void btnResetOptions_Click(object sender, EventArgs e)
        {
            _Exporter.Reset();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;

                if (_Exporter.Run(this, _EnabledSampling) == false)
                {
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            finally
            {
                Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            LicenseConfig.ShowDialog(_View.Document.Application, this);

            //更新授权状态
            _LicenseStatus = LicenseStatus.Get(OnLicenseStatus);
        }

        private void tabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabList.SelectedTab.Tag is IExportControl exporter)
            {
                _Exporter = exporter;
            }
        }

        private bool ShowConfirm(string s)
        {
            var r = MessageBox.Show(this, s, Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            return r == DialogResult.OK;
        }

        private void tsmiRenderingPerformancePreferred_Click(object sender, EventArgs e)
        {
            if (tsmiRenderingPerformancePreferred.Checked &&
                ShowConfirm(Strings.RenderingPerformancePreferredConfirm) == false)
            {
                tsmiRenderingPerformancePreferred.Checked = false;
            }
        }

        public bool UsedExtendFeature(string featureName)
        {
            if (string.CompareOrdinal(featureName, Ef.RenderingPerformancePreferred) == 0)
            {
                return tsmiRenderingPerformancePreferred.Checked && tsmiRenderingPerformancePreferred.Enabled;
            }

            if (string.CompareOrdinal(featureName, Ef.DisableMeshSimplifier) == 0)
            {
                return tsmiDisableGeometrySimplification.Checked;
            }

            return false;
        }

        /// <summary>
        /// 初始化扩展属性
        /// </summary>
        private void InitExtendFeatures()
        {
            //从用户设置中初始化扩展属性
            {
                var settings = Properties.Settings.Default;
                tsmiRenderingPerformancePreferred.Checked = settings.ExRenderingPerformancePreferred;
                tsmiDisableGeometrySimplification.Checked = settings.ExDisableGeometrySimplification;
            }
        }

        /// <summary>
        /// 保存扩展特性设置
        /// </summary>
        private void SaveExtendFeatures()
        {
            var settings = Properties.Settings.Default;
            settings.ExRenderingPerformancePreferred = tsmiRenderingPerformancePreferred.Checked;
            settings.ExDisableGeometrySimplification = tsmiDisableGeometrySimplification.Checked;
            settings.Save();
        }

        private void OnLicenseStatus(LicenseStatus status)
        {
            try
            {
                if (IsDisposed || Visible == false)
                {
                    return;
                }

                if (InvokeRequired)
                {
                    Invoke(new Action(() => OnLicenseStatus(status)));
                    return;
                }

                tsmiRenderingPerformancePreferred.Enabled = status.IsValid && status.IsTrial == false;
            }
            catch
            {
                // ignored
            }
        }

        private class LicenseStatus
        {
            public static Task<LicenseStatus> Get(Action<LicenseStatus> action)
            {
                if (action == null) throw new ArgumentNullException(nameof(action));

                var task = Get();
                task.ContinueWith(t => action(t.Result));
                return task;
            }

            private static Task<LicenseStatus> Get()
            {
                return Task.Run(() =>
                {
                    var isValid = false;
                    var isTrial = true;

                    try
                    {
                        using (var session = LicenseConfig.Create())
                        {
                            if (session.IsValid)
                            {
                                isValid = true;
                                isTrial = session.IsTrial;
                            }
                        }
                    }
                    catch
                    {
                        // ignored
                    }

                    return new LicenseStatus(isValid, isTrial);
                });
            }


            public bool IsValid { get; }
            public bool IsTrial { get; }

            public LicenseStatus(bool isValid, bool isTrial)
            {
                IsValid = isValid;
                IsTrial = isTrial;
            }
        }

    }
}
