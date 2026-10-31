using System;
using System.Collections.Generic;
using System.Text;
using AEC.Common;
using AEC.UI.Controls;

namespace AEC.UI.KX
{
    public class DynamicValueIndicator
    {
        //private LinearValueSmoother valueSmoother = LinearValueSmoother.Create2ndOrderSmoother();
        private PeakValueHolder valueHolder = KXMonitor.CreateDynamicValueHolder(); 

        public void Clear()
        {
            //this.valueSmoother.Clear();
        }

        public bool UpdateStatus(DUILevelMeter valueMeter, double comp)
        {
            double dynamic_status = -AEC.Core.DSP.DSPHelper.LinearToDecibels(comp);

            dynamic_status = (double)Math.Round(dynamic_status, 1, MidpointRounding.AwayFromZero);

            return valueHolder.UpdateCurrValue(dynamic_status);
        }

        public bool DoValueFreeFall(DUILevelMeter valueMeter)
        {
            bool ret = this.valueHolder.FreeFall();

            valueMeter.currLevel = valueHolder.currValue;

            return ret;
        }
    }
}
