using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.DgnPlatformNET;
using Bentley.GeometryNET;
using Bentley.MstnPlatformNET;

namespace Bimangle.ForgeEngine.Dgn.Toolset.PickPosition
{
    public class ToolPluginPickPoint : DgnPrimitiveTool, IPickPoint
    {
        public static ToolPluginPickPoint InstallNewInstance(Action<DPoint3d?, double> callback)
        {
            var tool = new ToolPluginPickPoint();
            tool.Init(callback);
            tool.InstallTool();
            return tool;
        }

        private Action<DPoint3d?, double> _Callback;

        public ToolPluginPickPoint() : base(0, 0)
        {
        }

        private void Dismiss()
        {
            if (_Callback != null)
            {
                _Callback(null, 0.0);
                _Callback = null;
            }
        }

        #region Overrides of DgnTool

        protected override string GetToolName()
        {
            return Strings.ToolsetPickPosition;
        }

        protected override bool OnInstall()
        {
            if (_Callback == null) return false;

            return base.OnInstall();
        }

        protected override void OnPostInstall()
        {
            AccuSnap.SnapEnabled = true;

            MessageCenter.Instance.StatusCommand = Strings.ToolTextToolset;
            MessageCenter.Instance.StatusPrompt = Strings.ToolsetPickPositionPrompt;

            base.OnPostInstall();
        }

        protected override bool OnResetButton(DgnButtonEvent ev)
        {
            Dismiss();

            ExitTool();
            return true;
        }

        protected override bool OnDataButton(DgnButtonEvent ev)
        {
            if (ev.KeyModifiers == 0)
            {
                var upm = ev.Viewport?.GetRootModel()?.GetModelInfo()?.UorPerMeter ?? 1.0;
                _Callback?.Invoke(ev.Point, upm);
            }

            return true;
        }

        #endregion

        #region Overrides of DgnPrimitiveTool

        protected override void OnRestartTool()
        {
            InstallNewInstance(_Callback);
        }

        #endregion

        #region Implementation of IPickPoint

        public void Init(Action<DPoint3d?, double> callback)
        {
            _Callback = callback;
        }

        public void Exit()
        {
            _Callback = null;
            ExitTool();
        }

        #endregion
    }

    public interface IPickPoint
    {
        void Init(Action<DPoint3d?, double> callback);

        void Exit();
    }

}
