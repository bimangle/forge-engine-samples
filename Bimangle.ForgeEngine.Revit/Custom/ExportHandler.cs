using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Revit.Api;

namespace Bimangle.ForgeEngine.Revit.Custom
{
    class ExportHandler : ExportHandlerBase
    {
        private readonly bool _EnableCustomLevelParser;

        public ExportHandler()
        {
            _EnableCustomLevelParser = Properties.Settings.Default.EnableCustomLevelParser;
        }

        public override ILevelParser OnCreateLevelParser()
        {
            return _EnableCustomLevelParser 
                ? new LevelParser() 
                : base.OnCreateLevelParser();
        }

        public override bool OnGetUseLevelCategory()
        {
            return _EnableCustomLevelParser || base.OnGetUseLevelCategory();
        }
    }
}
