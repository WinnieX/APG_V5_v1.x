using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Controls;
using AEC.Common;

namespace AEC.UI.KX
{
    public partial class KXMonitorMusicPanel : AEC.UI.KX.KXMonitorModulePanelBase
    {
        private const int LayoutSpacingLevelMeter = 2;
        
        private const int LayoutTicksPaddingX = 4;

        private DUILevelMeter leftChannelLevelMeter;
        private DUILevelMeter rightChannelLevelMeter;
        private DUIPicture levelMeterLeftTicks;
        private DUIPicture levelMeterRightTicks;

        private DualLinearLevelIndicator dualLinearLevelIndicator = new DualLinearLevelIndicator();

        public override bool monitorIsON
        {
            set
            {
                if (this.leftChannelLevelMeter != null)
                {
                    this.leftChannelLevelMeter.isOn = value;
                }
                if (this.rightChannelLevelMeter != null)
                {
                    this.rightChannelLevelMeter.isOn = value;
                }

                if(!value)
                {
                    this.dualLinearLevelIndicator.Clear();
                }

                this.Invalidate();
            }
        }

        public KXMonitorMusicPanel()
        {
            InitializeComponent();
        }

        protected override void OnInitDuiControls()
        {
            base.OnInitDuiControls();

            if(base.effectorThreshold == null)
            {
                return;
            }

            if (this.leftChannelLevelMeter == null)
            {
                this.leftChannelLevelMeter = new DUIMaxLevelMeter();
                this.leftChannelLevelMeter.ledLightImageModel = KXLevelMeterImageProvider.levelMeterLEDLightImageModel;
                this.leftChannelLevelMeter.levelBackground = KXLevelMeterImageProvider.GetLevelMeterBackground();
                this.leftChannelLevelMeter.levelForeground = KXLevelMeterImageProvider.GetLevelMeterForeground();
                this.leftChannelLevelMeter.Size = this.leftChannelLevelMeter.levelBackground.Size;
                this.leftChannelLevelMeter.Location = new Point(this.Width / 2 - this.leftChannelLevelMeter.Size.Width - LayoutSpacingLevelMeter, LayoutLevelTicksY + KXLevelMeterImageProvider.YOffsetFromTickToLed);
                this.leftChannelLevelMeter.Visible = true;
            }

            if (this.rightChannelLevelMeter == null)
            {
                this.rightChannelLevelMeter = new DUIMaxLevelMeter();
                this.rightChannelLevelMeter.ledLightImageModel = KXLevelMeterImageProvider.levelMeterLEDLightImageModel;
                this.rightChannelLevelMeter.levelBackground = KXLevelMeterImageProvider.GetLevelMeterBackground();
                this.rightChannelLevelMeter.levelForeground = KXLevelMeterImageProvider.GetLevelMeterForeground();
                this.rightChannelLevelMeter.Size = this.rightChannelLevelMeter.levelBackground.Size;
                this.rightChannelLevelMeter.Location = new Point(this.Width / 2 + LayoutSpacingLevelMeter, LayoutLevelTicksY + KXLevelMeterImageProvider.YOffsetFromTickToLed);
                this.rightChannelLevelMeter.Visible = true;
            }

            if (this.levelMeterLeftTicks == null)
            {
                this.levelMeterLeftTicks = new DUIPicture();
                this.levelMeterLeftTicks.background = KXLevelMeterImageProvider.GetLevelMeterLeftTicks(base.effectorThreshold.maxLevel);
                this.levelMeterLeftTicks.Size = this.levelMeterLeftTicks.background.Size;
                this.levelMeterLeftTicks.Location = new Point(LayoutTicksPaddingX, LayoutLevelTicksY);
                this.levelMeterLeftTicks.Visible = true;
            }

            if (this.levelMeterRightTicks == null)
            {
                this.levelMeterRightTicks = new DUIPicture();
                this.levelMeterRightTicks.background = KXLevelMeterImageProvider.GetLevelMeterRightTicks(base.effectorThreshold.maxLevel);
                this.levelMeterRightTicks.Size = this.levelMeterRightTicks.background.Size;
                this.levelMeterRightTicks.Location = new Point(this.Width - this.levelMeterRightTicks.Size.Width - LayoutTicksPaddingX, LayoutLevelTicksY);
                this.levelMeterRightTicks.Visible = true;
            }

            base.AddDuiControl(this.leftChannelLevelMeter);
            base.AddDuiControl(this.rightChannelLevelMeter);
            base.AddDuiControl(this.levelMeterLeftTicks);
            base.AddDuiControl(this.levelMeterRightTicks);
        }

        public void UpdateStatus(double musicInputLeftLevel, double musicInputRightLevel)
        {
            Rectangle invalidRect = this.dualLinearLevelIndicator.UpdateStatus(
                this.leftChannelLevelMeter, musicInputLeftLevel, 
                this.rightChannelLevelMeter, musicInputRightLevel);

            if (!invalidRect.IsEmpty)
            {
                this.Invalidate(invalidRect);
                this.Update();
            }
        }

        public override void DoLevelFreeFall()
        {
            if (this.leftChannelLevelMeter == null || this.rightChannelLevelMeter == null)
            {
                return;
            }

            Rectangle invalidRect = this.dualLinearLevelIndicator.DoLevelFreeFall(
                this.leftChannelLevelMeter, this.rightChannelLevelMeter);

            if (!invalidRect.IsEmpty)
            {
                this.Invalidate(invalidRect);
                this.Update();
            }
        }

        public override void UpdateEffectorModel()
        {
            if (base.effectorThreshold == null)
            {
                return;
            }

            if (this.levelMeterLeftTicks != null)
            {
                this.levelMeterLeftTicks.background = KXLevelMeterImageProvider.GetLevelMeterLeftTicks(base.effectorThreshold.maxLevel);
            }

            if (this.levelMeterRightTicks != null)
            {
                this.levelMeterRightTicks.background = KXLevelMeterImageProvider.GetLevelMeterRightTicks(base.effectorThreshold.maxLevel);
            }
        }
    }
}
