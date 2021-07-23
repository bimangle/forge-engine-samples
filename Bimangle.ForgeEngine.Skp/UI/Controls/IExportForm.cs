using Bimangle.ForgeEngine.Skp.Core;

namespace Bimangle.ForgeEngine.Skp.UI.Controls
{
    interface IExportForm
    {
        void RefreshCommand(Options options);

        string GetInputFilePath();
    }
}
