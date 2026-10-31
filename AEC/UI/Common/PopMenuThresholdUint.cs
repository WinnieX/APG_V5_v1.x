using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using AEC.Core;
using AEC.Misc;

namespace AEC.UI.Common
{
    public class PopMenuThresholdUint
    {
        private const string ThresholdUnitIndBu = "dBu";
        private const string ThresholdUnitInV = "V";

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private System.Windows.Forms.ContextMenuStrip contextMenuStripThresholdUint;
        private System.Windows.Forms.ToolStripMenuItem tsmiThresholdUnitIn_dBu;
        private System.Windows.Forms.ToolStripMenuItem tsmiThresholdUnitIn_V;

        private EventBus eventBus;

        private Localization localization = Config.GetInstance().localization;

        public PopMenuThresholdUint(IContainer container)
        {
            this.contextMenuStripThresholdUint = new System.Windows.Forms.ContextMenuStrip(container);
            this.tsmiThresholdUnitIn_dBu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThresholdUnitIn_V = new System.Windows.Forms.ToolStripMenuItem();

            this.contextMenuStripThresholdUint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiThresholdUnitIn_dBu,
            this.tsmiThresholdUnitIn_V});
            this.contextMenuStripThresholdUint.Size = new System.Drawing.Size(184, 48);

            // 
            // tsmiThresholdUnitIn_dBu
            // 
            this.tsmiThresholdUnitIn_dBu.Size = new System.Drawing.Size(183, 22);
            this.tsmiThresholdUnitIn_dBu.Click += new System.EventHandler(this.tsmiThresholdUnit_Click);
            // 
            // tsmiThresholdUnitIn_V
            // 
            this.tsmiThresholdUnitIn_V.Size = new System.Drawing.Size(183, 22);
            this.tsmiThresholdUnitIn_V.Click += new System.EventHandler(this.tsmiThresholdUnit_Click);
        }

        public void BindingEventBus(EventBus eventBus_)
        {
            this.eventBus = eventBus_;
        }

        public void Show()
        {
            switch(config.thresholdUnit)
            {
                case Misc.ThresholdUnit.IndB:
                    this.tsmiThresholdUnitIn_dBu.Checked = true;
                    this.tsmiThresholdUnitIn_V.Checked = false;
                    break;

                case Misc.ThresholdUnit.InV:
                    this.tsmiThresholdUnitIn_dBu.Checked = false;
                    this.tsmiThresholdUnitIn_V.Checked = true;
                    break;

                default:
                    return;
            }

            this.tsmiThresholdUnitIn_dBu.Text = string.Format(localization[Localization.popMenu_ThresholdUnit], ThresholdUnitIndBu);
            this.tsmiThresholdUnitIn_V.Text = string.Format(localization[Localization.popMenu_ThresholdUnit], ThresholdUnitInV);
            
            this.contextMenuStripThresholdUint.Show(Control.MousePosition);
        }

        private void tsmiThresholdUnit_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.tsmiThresholdUnitIn_dBu))
            {
                UpdateThresholdUint(Misc.ThresholdUnit.IndB);
            }
            else if (sender.Equals(this.tsmiThresholdUnitIn_V))
            {
                UpdateThresholdUint(Misc.ThresholdUnit.InV);
            }
        }

        private void UpdateThresholdUint(AEC.Misc.ThresholdUnit thresholdUint)
        {
            if (config.SetThresholdUnit(thresholdUint) && this.eventBus != null)
            {
                this.eventBus.NotifyObeserver(new LimiterThresholdUnitChangedArgs());
            }
        }
    }
}
