using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;

namespace Bimangle.ForgeEngine.Navisworks.Toolset.PickPosition
{
#if DEBUG
    [Plugin("Engine_PickPoint",         //Plugin name
        Command.DEVELOPER_ID,              //4 character Developer ID or GUID
        ToolTip = "",                        //The tooltip for the item in the ribbon
        DisplayName = "Engine PickPoint")]   //Display name for the Plugin in the Ribbon
#endif
    public class ToolPluginPickPoint : ToolPlugin, IPickPoint
    {
        private Action<Point3D, Units> _Callback;

        public void Init(Action<Point3D, Units> callback)
        {
            _Callback = callback;
        }

        #region Overrides of ToolPlugin

        public override bool MouseDown(View view, KeyModifiers modifiers, ushort button, int x, int y, double timeOffset)
        {
            if (modifiers == KeyModifiers.None && button == 1)
            {
                var result = view.PickItemFromPoint(x, y);
                if (result?.Point != null && result?.ModelItem != null && _Callback != null)
                {
                    _Callback(result.Point, GetUnits(result.ModelItem));
                    return true;
                }
            }
            return base.MouseDown(view, modifiers, button, x, y, timeOffset);
        }

        public override bool KeyDown(View view, KeyModifiers modifier, ushort key, double timeOffset)
        {
            if (modifier == KeyModifiers.None && key == 27)
            {
                Dismiss();
                return true;
            }

            return base.KeyDown(view, modifier, key, timeOffset);
        }

        #endregion

        private void Dismiss()
        {
            _Callback?.Invoke(null, Units.Meters);
        }

        private Units GetUnits(ModelItem modelItem)
        {
            if (modelItem.Model != null) return modelItem.Model.Units;
            if (modelItem.Parent != null) return GetUnits(modelItem.Parent);
            return Units.Miles;
        }
    }

    public interface IPickPoint
    {
        void Init(Action<Point3D, Units> callback);
    }

}
