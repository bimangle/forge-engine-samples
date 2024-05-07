using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bimangle.ForgeEngine.Revit.Config;

namespace Bimangle.ForgeEngine.Revit.UI.Controls
{
    interface IExportControl
    {
        string Icon { get; }

        string Title { get; }

        void Init(UIDocument uidoc, View3D view, AppConfig config, Dictionary<long, bool> elementIds);

        bool Run(IExportForm form, bool enabledSampling);

        void Reset();
    }

    interface IExportForm
    {
        bool UsedExtendFeature(string featureName);
    }
}
