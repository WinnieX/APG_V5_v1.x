using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using AEC.Core;
using AEC.Misc;

namespace AEC.UI.Common
{
    public class PopMenuMixerLevelUint
    {
        private const string MixerLevelUnitIndB = "dB";
        private const string MixerLevelUnitInPercentage = "%";

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private System.Windows.Forms.ContextMenuStrip contextMenuStripMixerLevelUint;
        private System.Windows.Forms.ToolStripMenuItem tsmiMixerLevelUnitIn_dB;
        private System.Windows.Forms.ToolStripMenuItem tsmiMixerLevelUnitIn_Percentage;

        private EventBus eventBus;

        private Localization localization = Config.GetInstance().localization;

        public PopMenuMixerLevelUint(IContainer container)
        {
            this.contextMenuStripMixerLevelUint = new System.Windows.Forms.ContextMenuStrip(container);
            this.tsmiMixerLevelUnitIn_dB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMixerLevelUnitIn_Percentage = new System.Windows.Forms.ToolStripMenuItem();

            this.contextMenuStripMixerLevelUint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMixerLevelUnitIn_dB,
            this.tsmiMixerLevelUnitIn_Percentage});
            this.contextMenuStripMixerLevelUint.Size = new System.Drawing.Size(184, 48);

            // 
            // tsmiThresholdUnitIn_dB
            // 
            this.tsmiMixerLevelUnitIn_dB.Size = new System.Drawing.Size(183, 22);
            this.tsmiMixerLevelUnitIn_dB.Click += new System.EventHandler(this.tsmiMixerLevelUnit_Click);
            // 
            // tsmiThresholdUnitIn_Percentage
            // 
            this.tsmiMixerLevelUnitIn_Percentage.Size = new System.Drawing.Size(183, 22);
            this.tsmiMixerLevelUnitIn_Percentage.Click += new System.EventHandler(this.tsmiMixerLevelUnit_Click);
        }

        public void BindingEventBus(EventBus eventBus_)
        {
            this.eventBus = eventBus_;
        }

        public void Show()
        {
            switch (config.mixerLevelUnit)
            {
                case MixerLevelUnit.IndB:
                    this.tsmiMixerLevelUnitIn_dB.Checked = true;
                    this.tsmiMixerLevelUnitIn_Percentage.Checked = false;
                    break;

                case MixerLevelUnit.InPercentage:
                    this.tsmiMixerLevelUnitIn_dB.Checked = false;
                    this.tsmiMixerLevelUnitIn_Percentage.Checked = true;
                    break;

                default:
                    return;
            }

            this.tsmiMixerLevelUnitIn_dB.Text = string.Format(localization[Localization.popMenu_MixerLevelUnit], MixerLevelUnitIndB);
            this.tsmiMixerLevelUnitIn_Percentage.Text = string.Format(localization[Localization.popMenu_MixerLevelUnit], MixerLevelUnitInPercentage);
            
            this.contextMenuStripMixerLevelUint.Show(Control.MousePosition);
        }

        private void tsmiMixerLevelUnit_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.tsmiMixerLevelUnitIn_dB))
            {
                UpdateMixerLevelUint(Misc.MixerLevelUnit.IndB);
            }
            else if (sender.Equals(this.tsmiMixerLevelUnitIn_Percentage))
            {
                UpdateMixerLevelUint(Misc.MixerLevelUnit.InPercentage);
            }
        }

        private void UpdateMixerLevelUint(AEC.Misc.MixerLevelUnit mixerLevelUnit)
        {
            if (config.SetMixerLevelUnit(mixerLevelUnit) && this.eventBus != null)
            {
                this.eventBus.NotifyObeserver(new MixerLevelUnitChangedArgs());
            }
        }
    }
}
