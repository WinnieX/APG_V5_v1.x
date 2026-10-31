using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class DataConverter
    {
        //public static bool Int2Bool(int value)
        //{
        //    return value != 0;
        //}

        //public static int Bool2Int(bool value)
        //{
        //    return value ? (short)1 : (short)0;
        //}

        public static bool Short2Bool(short value)
        {
            return value != 0;
        }

        public static short Bool2Short(bool value)
        {
            return value ? (short)1 : (short)0;
        }

        public static EQType Short2EQType(short value)
        {
            switch (value)
            {
                case 0:
                    return EQType.Peaking;
                case 1:
                    return EQType.LowShelf;
                case 2:
                    return EQType.HighShelf;
            }

            return EQType.Peaking;
        }

        public static DEQControlMode Short2DEQControlMode(short value)
        {
            switch (value)
            {
                case 0:
                    return DEQControlMode.Off;
                case 1:
                    return DEQControlMode.Below;
                case 2:
                    return DEQControlMode.Above;
            }

            return DEQControlMode.Off;
        }

        public static CompressorMode Short2CompressorMode(short value)
        {
            switch (value)
            {
                case 0:
                    return CompressorMode.Fast;
                case 1:
                    return CompressorMode.Medium;
                case 2:
                    return CompressorMode.Slow;
            }

            return CompressorMode.Fast;
        }

        public static EQControlMode Short2EQControlMode(short value)
        {
            switch (value)
            {
                case 0:
                    return EQControlMode.Linkage;
                case 1:
                    return EQControlMode.Left;
                case 2:
                    return EQControlMode.Right;
            }

            return EQControlMode.Linkage;
        }

        public static WorkMode Short2WorkMode(short workMode)
        {
            return workMode == 0 ? WorkMode.Auto : WorkMode.Manual;
        }

        public static WorkSelect Short2WorkSelect(short workSelect)
        {
            return workSelect == 0 ? WorkSelect.Sing : WorkSelect.Disco;
        }

        public static FilterType Short2FilterType(short filterMode)
        {
            switch (filterMode)
            {
                case 0:
                    return FilterType.FirstOrder;

                case 1:
                    return FilterType.Bessel12dB;

                case 2:
                    return FilterType.Butterworth12dB;

                case 3:
                    return FilterType.LinkwitzRiley12dB;

                case 4:
                    return FilterType.Bessel18dB;

                case 5:
                    return FilterType.Butterworth18dB;

                case 6:
                    return FilterType.Bessel24dB;

                case 7:
                    return FilterType.Butterworth24dB;

                case 8:
                    return FilterType.LinkwitzRiley24dB;

                case 9:
                    return FilterType.Bessel36dB;

                case 10:
                    return FilterType.Butterworth36dB;

                case 11:
                    return FilterType.LinkwitzRiley36dB;

                case 12:
                    return FilterType.Bessel48dB;

                case 13:
                    return FilterType.Butterworth48dB;

                case 14:
                    return FilterType.LinkwitzRiley48dB;

                case 15:
                    return FilterType.X5_User12dB;

                case 16:
                    return FilterType.X5_User24dB;
            }

            return FilterType.Butterworth12dB;
        }
    }
}
