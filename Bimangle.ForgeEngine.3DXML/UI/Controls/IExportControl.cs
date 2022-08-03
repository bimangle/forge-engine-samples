using Bimangle.ForgeEngine._3DXML.Config;

namespace Bimangle.ForgeEngine._3DXML.UI.Controls
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
