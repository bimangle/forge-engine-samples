using Bimangle.ForgeEngine._3DXML.Core;

namespace Bimangle.ForgeEngine._3DXML.UI.Controls
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
