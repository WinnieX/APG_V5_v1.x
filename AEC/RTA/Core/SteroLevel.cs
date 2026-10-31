using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class SteroLevel
    {
        public double leftCh = SignalLevel.MinLevel_16Bit;
        public double rightCh = SignalLevel.MinLevel_16Bit;

        private int leftPeakCount = 0;
        private int rightPeakCount = 0;

        public static int PeakValueHoldOn = 6;

        public SteroLevel()
        {

        }

        public void Reset()
        {
            leftCh = SignalLevel.MinLevel_16Bit;
            rightCh = SignalLevel.MinLevel_16Bit;
        }

        public void UpdateLevel(double[] left, double[] right)
        {
            double left_ch = SignalLevel.CalcdBFS16Bit(left);
            double right_ch = SignalLevel.CalcdBFS16Bit(right);

            if (leftCh <= left_ch)
            {
                leftCh = left_ch;
                leftPeakCount = 0;
            }
            else
            {
                if (leftPeakCount++ > PeakValueHoldOn)
                {
                    leftCh = left_ch;
                    leftPeakCount = 0;
                }
            }

            if (rightCh <= right_ch)
            {
                rightCh = right_ch;
                rightPeakCount = 0;
            }
            else
            {
                if (rightPeakCount++ > PeakValueHoldOn)
                {
                    rightCh = right_ch;
                    rightPeakCount = 0;
                }
            }
        }
    }
}
