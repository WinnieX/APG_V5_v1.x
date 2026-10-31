using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.Core;

namespace AEC.RTA.Utils
{
    public class FixedPointHelper
    {
        public const int MaxMagnitude16Bit = 0x8000;
        public const int MaxMagnitude24Bit = 0x800000;

        public static double[] FixedPoint2Double(byte[] data, BitsPerSample bitsPerSample)
        {
            switch (bitsPerSample)
            {
                case BitsPerSample.BitsPerSample_16Bit:
                    return FixedPoint16bit2Double(data);

                case BitsPerSample.BitsPerSample_24Bit:
                    return FixedPoint24bit2Double(data);
            }

            return FixedPoint16bit2Double(data);
        }

        public static double[] FixedPoint16bit2Double(byte[] data)
        {
            int length = data.Length / 2;
            double[] floats = new double[length];
            for (int i = 0; i < length; ++i)
            {
                Int32 byte1 = data[i * 2 + 0];
                Int32 byte2 = (sbyte)data[i * 2 + 1] << 8;
                floats[i] = byte1 + byte2;

                floats[i] /= MaxMagnitude16Bit;
            }

            return floats;
        }

        public static double[] FixedPoint24bit2Double(byte[] data)
        {
            int length = data.Length / 3;
            double[] floats = new double[length];
            for (int i = 0; i < length; ++i)
            {
                Int32 byte1 = data[i * 3 + 0];
                Int32 byte2 = data[i * 3 + 1] << 8;
                Int32 byte3 = (sbyte)data[i * 3 + 2] << 16;
                floats[i] = byte1 + byte2 + byte3;

                floats[i] /= MaxMagnitude24Bit;
            }

            return floats;
        }
    }
}
