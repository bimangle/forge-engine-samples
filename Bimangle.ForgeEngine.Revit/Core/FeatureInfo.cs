using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Revit.Core
{
    class FeatureInfo
    {
        public FeatureType Type { get; }
        public string Title { get; }
        public string Description { get; }
        public bool Enabled { get; private set; }
        public bool Selected { get; private set; }

        public FeatureInfo(FeatureType type, string title, string description, bool enabled = true)
        {
            Type = type;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Enabled = enabled;
            Selected = false;
        }

        public void ChangeSelected(List<FeatureInfo> features, bool selected)
        {
            if (Selected == selected) return;
            Selected = selected;
        }
    }
}
