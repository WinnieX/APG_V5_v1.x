using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Controls;
using AEC.Core;
using AEC.Common;

namespace AEC.UI.KX
{
    public partial class KXMonitorMicPanel : AEC.UI.KX.KXMonitorModulePanelBase
    {
        private const int LayoutSpacingLevelMeter = 2;
        private const int LayoutTicksPaddingX = 4;

        private DUILevelMeter channelALevelMeter;
        private DUILevelMeter channelBLevelMeter;
        private DUILevelMeter compressorMeter;
        private DUIPicture levelMeterATicks;
        private DUIPicture levelMeterBTicks;
        private DUIPicture compressorTicks;

        private DualLinearLevelIndicator dualLinearLevelIndicator = new DualLinearLevelIndicator();
        private DynamicValueIndicator compIndicator = new DynamicValueIndicator();

        public override bool monitorIsON
        {
            set
            {
                if (this.channelALevelMeter != null)
                {
                    this.channelALevelMeter.isOn = value;
                }
                if (this.channelBLevelMeter != null)
                {
                    this.channelBLevelMeter.isOn = value;
                }
                if (this.compressorMeter != null)
                {
                    this.compressorMeter.isOn = value;
                }

                if (!value)
                {
                    this.dualLinearLevelIndicator.Clear();
                    this.compIndicator.Clear();
                }

                this.Invalidate();
            }
        }

        public KXMonitorMicPanel()
        {
            InitializeComponent();
        }

        protected override void OnInitDuiControls()
        {
            base.OnInitDuiControls();

            if (this.channelALevelMeter == null)
            {
                this.channelALevelMeter = new DUIMaxLevelMeter();
                this.channelALevelMeter.ledLightImageModel = KXLevelMeterImageProvider.levelMeterLEDLightImageModel;
                this.channelALevelMeter.levelBackground = KXLevelMeterImageProvider.GetLevelMeterBackground();
                this.channelALevelMeter.levelForeground = KXLevelMeterImageProvider.GetLevelMeterForeground();
                this.channelALevelMeter.Size = this.channelALevelMeter.levelBackground.Size;
                this.channelALevelMeter.Location = new Point(this.Width / 2 - this.channelALevelMeter.Size.Width - LayoutSpacingLevelMeter, LayoutLevelTicksY + KXLevelMeterImageProvider.YOffsetFromTickToLed);
                this.channelALevelMeter.Visible = true;
            }

            if (this.channelBLevelMeter == null)
            {
                this.channelBLevelMeter = new DUIMaxLevelMeter();
                this.channelBLevelMeter.ledLightImageModel = KXLevelMeterImageProvider.levelMeterLEDLightImageModel;
                this.channelBLevelMeter.levelBackground = KXLevelMeterImageProvider.GetLevelMeterBackground();
                this.channelBLevelMeter.levelForeground = KXLevelMeterImageProvider.GetLevelMeterForeground();
                this.channelBLevelMeter.Size = this.channelBLevelMeter.levelBackground.Size;
                this.channelBLevelMeter.Location = new Point(this.Width / 2 + LayoutSpacingLevelMeter, LayoutLevelTicksY + KXLevelMeterImageProvider.YOffsetFromTickToLed);
                this.channelBLevelMeter.Visible = true;
            }

            if (this.compressorMeter == null)
            {
                this.compressorMeter = new DUILevelMeter0_12dB();
                this.compressorMeter.ledLightImageModel = KXLevelMeterImageProvider.limiterLEDLightImageModel;
                this.compressorMeter.levelBackground = KXLevelMeterImageProvider.GetLimiterMeterBackground();
                this.compressorMeter.levelForeground = KXLevelMeterImageProvider.GetLimiterMeterForeground();
                this.compressorMeter.Size = this.compressorMeter.levelBackground.Size;
                this.compressorMeter.Location = new Point(this.Width / 2 + LayoutSpacingLevelMeter, KXLevelMeterImageProvider.YOffsetFromTickToLed + LayoutLimiterTicksY);
                this.compressorMeter.Visible = true;
            }

            if (this.levelMeterATicks == null)
            {
                this.levelMeterATicks = new DUIPicture();
                this.levelMeterATicks.background = KXLevelMeterImageProvider.GetLevelMeterLeftTicks(base.effectorThreshold.maxLevel);
                this.levelMeterATicks.Size = this.levelMeterATicks.background.Size;
                this.levelMeterATicks.Location = new Point(LayoutTicksPaddingX, LayoutLevelTicksY);
                this.levelMeterATicks.Visible = true;
            }

            if (this.levelMeterBTicks == null)
            {
                this.levelMeterBTicks = new DUIPicture();
                this.levelMeterBTicks.background = KXLevelMeterImageProvider.GetLevelMeterRightTicks(base.effectorThreshold.maxLevel);
                this.levelMeterBTicks.Size = this.levelMeterBTicks.background.Size;
                this.levelMeterBTicks.Location = new Point(this.Width - this.levelMeterBTicks.Size.Width - LayoutTicksPaddingX, LayoutLevelTicksY);
                this.levelMeterBTicks.Visible = true;
            }

            if (this.compressorTicks == null)
            {
                this.compressorTicks = new DUIPicture();
                this.compressorTicks.background = KXLevelMeterImageProvider.GetLimiterMeterTicks();
                this.compressorTicks.Size = this.compressorTicks.background.Size;
                this.compressorTicks.Location = new Point(this.Width - this.levelMeterBTicks.Size.Width - LayoutTicksPaddingX, LayoutLimiterTicksY);
                this.compressorTicks.Visible = true;
            }

            base.AddDuiControl(this.channelALevelMeter);
            base.AddDuiControl(this.channelBLevelMeter);
            base.AddDuiControl(this.compressorMeter);
            base.AddDuiControl(this.levelMeterATicks);
            base.AddDuiControl(this.levelMeterBTicks);
            base.AddDuiControl(this.compressorTicks);
        }

        public void UpdateStatus(double micInputALevel, double micInputBLevel, double micCompressor)
        {
            Rectangle invalidRect = this.dualLinearLevelIndicator.UpdateStatus(
                this.channelALevelMeter, micInputALevel,
                this.channelBLevelMeter, micInputBLevel);

            if (this.compIndicator.UpdateStatus(this.compressorMeter, micCompressor))
            {
                if (invalidRect.IsEmpty)
                {
                    invalidRect = this.compressorMeter.ClientRectangle;
                }
                else
                {
                    invalidRect = Rectangle.Union(invalidRect, this.compressorMeter.ClientRectangle);
                }
            }

            if (!invalidRect.IsEmpty)
            {
                this.Invalidate(invalidRect);
                this.Update();
            }
        }

        public override void DoLevelFreeFall()
        {
            if (this.channelALevelMeter == null || this.channelBLevelMeter == null)
            {
                return;
            }

            Rectangle invalidRect = this.dualLinearLevelIndicator.DoLevelFreeFall(
                this.channelALevelMeter, this.channelBLevelMeter);

            if (compIndicator.DoValueFreeFall(this.compressorMeter))
            {
                if (invalidRect.IsEmpty)
                {
                    invalidRect = this.compressorMeter.ClientRectangle; ;
                }
                else
                {
                    invalidRect = Rectangle.Union(invalidRect, this.compressorMeter.ClientRectangle);
                }
            }

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

            if (this.levelMeterATicks != null)
            {
                this.levelMeterATicks.background = KXLevelMeterImageProvider.GetLevelMeterLeftTicks(base.effectorThreshold.maxLevel);
            }

            if (this.levelMeterBTicks != null)
            {
                this.levelMeterBTicks.background = KXLevelMeterImageProvider.GetLevelMeterRightTicks(base.effectorThreshold.maxLevel);
            }
        }
    }
}
