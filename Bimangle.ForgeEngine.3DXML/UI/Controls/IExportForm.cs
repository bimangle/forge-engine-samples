using Bimangle.ForgeEngine._3DXML.Core;

namespace Bimangle.ForgeEngine._3DXML.UI.Controls
{
    interface IExportForm
    {
        void RefreshCommand(Options options);

        string GetInputFilePath();
    }
}
