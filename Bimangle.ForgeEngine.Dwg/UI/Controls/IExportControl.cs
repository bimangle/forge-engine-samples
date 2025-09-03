using Bimangle.ForgeEngine.Dwg.Config;

namespace Bimangle.ForgeEngine.Dwg.UI.Controls
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
