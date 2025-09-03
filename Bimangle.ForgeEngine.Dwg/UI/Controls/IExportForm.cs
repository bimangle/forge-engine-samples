using Bimangle.ForgeEngine.Dwg.Core;

namespace Bimangle.ForgeEngine.Dwg.UI.Controls
{
    interface IExportForm
    {
        void RefreshCommand(Options options);

        string GetInputFilePath();

        /// <summary>
        /// 是否启用扩展特性
        /// </summary>
        /// <param name="featureName"></param>
        /// <returns></returns>
        bool UsedExtendFeature(string featureName);
    }
}
