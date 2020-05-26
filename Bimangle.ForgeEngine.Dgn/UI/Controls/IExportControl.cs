using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.DgnPlatformNET;
using Bimangle.ForgeEngine.Dgn.Config;

namespace Bimangle.ForgeEngine.Dgn.UI.Controls
{
    interface IExportControl
    {
        string Icon { get; }

        string Title { get; }

        void Init(Viewport view, AppConfig config, bool hasSelectElements);

        bool Run();

        void Reset();
    }
}
