using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Form = System.Windows.Forms.Form;
using View = Autodesk.Revit.DB.View;

namespace Bimangle.ForgeEngine.Revit.UI
{
    public partial class FormViews : Form
    {
        public List<int> SelectedViewIds { get; private set; }

        public FormViews()
        {
            InitializeComponent();
        }

        public FormViews(Document document, List<int> selectedViewIds) : this()
        {
            var viewItems = GetViewItems(document);

            lvViews.Items.Clear();

            var items = viewItems.OrderBy(x => x.ViewType).ThenBy(x => x.ViewName).ToList();
            foreach (var item in items)
            {
                var viewItem = new ListViewItem(new[] {item.ViewType, item.ViewName}, 0)
                {
                    Tag = item,
                    Checked = selectedViewIds?.Contains(item.ViewId) ?? false
                };
                lvViews.Items.Add(viewItem);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var selectedViewIds = new List<int>();
            foreach (ListViewItem viewItem in lvViews.Items)
            {
                if (viewItem.Checked && viewItem.Tag is ViewItem item)
                {
                    selectedViewIds.Add(item.ViewId);
                }
            }
            SelectedViewIds = selectedViewIds;

            DialogResult = DialogResult.OK;
            Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private List<ViewItem> GetViewItems(Document document)
        {
            var results = new List<ViewItem>();

            using (var collector = new FilteredElementCollector(document))
            {
                var views = collector.OfClass(typeof(View)).OfType<View>();

                foreach (var view in views)
                {
                    //跳过不能打印输出的视图
                    if (view.CanBePrinted == false) continue;

                    //跳过 3D 视图
                    if (view is View3D) continue;

                    //跳过模板视图
                    if (view.IsTemplate) continue;

                    var viewId = view.Id.IntegerValue;
                    var viewType = view.ViewType.ToString();
#if R2014
                    var viewName = view.ViewName;
#else
                    var viewName = view.Title;
#endif

                    results.Add(new ViewItem(viewId, viewType, viewName));
                }
            }

            return results;
        }

        private class ViewItem
        {
            public int ViewId { get; set; }
            public string ViewType { get; set; }
            public string ViewName { get; set; }

            public ViewItem(int viewId, string viewType, string viewName)
            {
                ViewId = viewId;
                ViewType = viewType;
                ViewName = viewName;
            }
        }

        private void FormViews_Load(object sender, EventArgs e)
        {
            //Icon = Properties.Resources.app;
        }
    }
}
