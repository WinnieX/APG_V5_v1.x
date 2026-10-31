using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Common;
using AEC.UI.Controls;

namespace AEC.UI.KX
{
    public class DualLinearLevelIndicator
    {
        public LinearLevelIndicator[] levelIndicators = new LinearLevelIndicator[]
        {
            new LinearLevelIndicator(),
            new LinearLevelIndicator(),
        };

        public Rectangle UpdateStatus(
            DUILevelMeter levelMeter1, double level1, 
            DUILevelMeter levelMeter2, double level2)
        {
            Rectangle invalidRect = Rectangle.Empty;

            //Log.OutputLine("******DualLinearLevelIndicator.UpdateStatus()...0");

            if (this.levelIndicators[0].UpdateStatus(levelMeter1, level1))
            {
                invalidRect = levelMeter1.ClientRectangle;
            }

            //Log.OutputLine("******DualLinearLevelIndicator.UpdateStatus()...1");

            if (this.levelIndicators[1].UpdateStatus(levelMeter2, level2))
            {
                if (invalidRect.IsEmpty)
                {
                    invalidRect = levelMeter2.ClientRectangle;
                }
                else
                {
                    invalidRect = Rectangle.Union(invalidRect, levelMeter2.ClientRectangle);
                }
            }

            //if (levelMeter1.peakLevel != levelMeter2.peakLevel)
            //{
            //    Log.OutputLine("!!!!!!DualLinearLevelIndicator.level A = {0}dB, level B = {1}dB", levelMeter1.peakLevel, levelMeter2.peakLevel);
            //}

            //Log.OutputLine("******DualLinearLevelIndicator.UpdateStatus()...2");

            return invalidRect;
        }

        public Rectangle DoLevelFreeFall(DUILevelMeter levelMeter1, DUILevelMeter levelMeter2)
        {
            Rectangle invalidRect = Rectangle.Empty;

            if (this.levelIndicators[0].DoLevelFreeFall(levelMeter1))
            {
                invalidRect = levelMeter1.ClientRectangle;
            }

            if (this.levelIndicators[1].DoLevelFreeFall(levelMeter2))
            {
                if (invalidRect.IsEmpty)
                {
                    invalidRect = levelMeter2.ClientRectangle;
                }
                else
                {
                    invalidRect = Rectangle.Union(invalidRect, levelMeter2.ClientRectangle);
                }
            }

            return invalidRect;
        }

        public void Clear()
        {
            this.levelIndicators[0].Clear();
            this.levelIndicators[1].Clear();
        }
    }
}
