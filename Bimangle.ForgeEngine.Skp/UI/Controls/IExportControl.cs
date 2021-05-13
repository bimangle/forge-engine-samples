using Bimangle.ForgeEngine.Skp.Config;

namespace Bimangle.ForgeEngine.Skp.UI.Controls
{
    interface IExportControl
    {
        string Icon { get; }

        string Title { get; }

        void Init(IExportForm form, AppConfig config);

        void Reset();

        void RefreshCommand();
    }
}
