using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using AEC.Core;
using AEC.Misc;
using AEC.Common;

namespace AEC.UI.Common
{
    public class PopMenuChannelDelayUint
    {
        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private System.Windows.Forms.ContextMenuStrip contextMenuStripChannelDelayUint;
        private System.Windows.Forms.ToolStripMenuItem tsmiChannelDelayUnitIn_Ms;
        private System.Windows.Forms.ToolStripMenuItem tsmiChannelDelayUnitIn_M;

        private EventBus eventBus;

        private Localization localization = Config.GetInstance().localization;

        public PopMenuChannelDelayUint(IContainer container)
        {
            this.contextMenuStripChannelDelayUint = new System.Windows.Forms.ContextMenuStrip(container);
            this.tsmiChannelDelayUnitIn_Ms = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChannelDelayUnitIn_M = new System.Windows.Forms.ToolStripMenuItem();

            this.contextMenuStripChannelDelayUint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChannelDelayUnitIn_Ms,
            this.tsmiChannelDelayUnitIn_M});
            this.contextMenuStripChannelDelayUint.Size = new System.Drawing.Size(184, 48);

            // 
            // tsmiThresholdUnitIn_Ms
            // 
            this.tsmiChannelDelayUnitIn_Ms.Size = new System.Drawing.Size(183, 22);
            this.tsmiChannelDelayUnitIn_Ms.Click += new System.EventHandler(this.tsmiChannelDelayUnit_Click);
            // 
            // tsmiThresholdUnitIn_M
            // 
            this.tsmiChannelDelayUnitIn_M.Size = new System.Drawing.Size(183, 22);
            this.tsmiChannelDelayUnitIn_M.Click += new System.EventHandler(this.tsmiChannelDelayUnit_Click);
        }

        public void BindingEventBus(EventBus eventBus_)
        {
            this.eventBus = eventBus_;
        }

        public void Show()
        {
            switch (config.channelDelayUnit)
            {
                case ChannelDelayUnit.InMilliseconds:
                    this.tsmiChannelDelayUnitIn_Ms.Checked = true;
                    this.tsmiChannelDelayUnitIn_M.Checked = false;
                    break;

                case ChannelDelayUnit.InMeter:
                    this.tsmiChannelDelayUnitIn_Ms.Checked = false;
                    this.tsmiChannelDelayUnitIn_M.Checked = true;
                    break;

                default:
                    return;
            }

            this.tsmiChannelDelayUnitIn_Ms.Text = string.Format(localization[Localization.popMenu_ChannelDelayUnit], ChannelDelayParamLabelProvider.ChannelDelayUnitInMs);
            this.tsmiChannelDelayUnitIn_M.Text = string.Format(localization[Localization.popMenu_ChannelDelayUnit], ChannelDelayParamLabelProvider.ChannelDelayUnitInM);
            
            this.contextMenuStripChannelDelayUint.Show(Control.MousePosition);
        }

        private void tsmiChannelDelayUnit_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.tsmiChannelDelayUnitIn_Ms))
            {
                UpdateChannelDelayUint(Misc.ChannelDelayUnit.InMilliseconds);
            }
            else if (sender.Equals(this.tsmiChannelDelayUnitIn_M))
            {
                UpdateChannelDelayUint(Misc.ChannelDelayUnit.InMeter);
            }
        }

        private void UpdateChannelDelayUint(AEC.Misc.ChannelDelayUnit channelDelayUnit)
        {
            if (config.SetChannelDelayUnit(channelDelayUnit) && this.eventBus != null)
            {
                this.eventBus.NotifyObeserver(new ChannelDelayUnitChangedArgs());
            }
        }
    }
}
