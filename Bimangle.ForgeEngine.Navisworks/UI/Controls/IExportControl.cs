using System.Collections.Generic;
using System.Drawing;
using Bimangle.ForgeEngine.Navisworks.Config;

namespace Bimangle.ForgeEngine.Navisworks.UI.Controls
{
    interface IExportControl
    {
        string Icon { get; }

        string Title { get; }

        void Init(AppConfig config);

        bool Run(IExportForm form);

        void Reset();
    }

    interface IExportForm
    {
        bool UsedExtendFeature(string featureName);
    }
}
