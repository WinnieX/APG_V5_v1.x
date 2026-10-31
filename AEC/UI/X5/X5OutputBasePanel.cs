using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors.X5;
using AEC.Effectors.X5.Modules;
using AEC.Common;
using AEC.UI.Common;

namespace AEC.UI.X5
{
    public partial class X5OutputBasePanel : AEC.UI.PanelBase
    {
        private PopMenuChannelDelayUint popMenuChannelDelayUint;

        protected static readonly ChannelDelayParamLabelProvider channelDelayParamLabelProvider = new ChannelDelayParamLabelProvider(OutputBaseModule.DelayStep, OutputBaseModule.MaxDelayInms);

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        public X5OutputBasePanel()
        {
            InitializeComponent();

            this.components = new System.ComponentModel.Container();
            this.popMenuChannelDelayUint = new PopMenuChannelDelayUint(this.components);
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            if (this._eventBus != null)
            {
                this.popMenuChannelDelayUint.BindingEventBus(this._eventBus);
                this._eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
                this._eventBus.RegisterHandler(ChannelDelayUnitChangedHandler);
                this._eventBus.RegisterHandler(OnOutputMuteChangedFromNavigatorHandler);
            }
        }

        protected virtual void OnOutputMuteChangedFromNavigatorHandler(OutputChannelType channel)
        {

        }

        private void OnOutputMuteChangedFromNavigatorHandler(object sender, EventBusArgs e)
        {
            if(e.type == EventType.OutputMuteChangedFromNavigator)
            {
                OutputMuteChangedFromNavigatorArgs omc = e as OutputMuteChangedFromNavigatorArgs;
                if (omc == null)
                {
                    return;
                }

                this.OnOutputMuteChangedFromNavigatorHandler(omc.channel);
            }
        }

        protected virtual void OnChannelDelayUintChangedHandler()
        {

        }

        private void ChannelDelayUnitChangedHandler(object sender, EventBusArgs e)
        {
            if(e.type == EventType.ChannelDelayUnitChanged)
            {
                this.OnChannelDelayUintChangedHandler();
            }            
        }

        protected void SwitchChannelDelayUnit()
        {
            config.SwitchChannelDelayUnit();
            this._eventBus.NotifyObeserver(new ChannelDelayUnitChangedArgs());
        }

        protected void MouseUpHandler(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenuChannelDelayUint.Show();
            }
        }
    }
}
