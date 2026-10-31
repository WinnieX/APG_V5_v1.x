using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public enum OutputSignalType
    {
        PinkNoise = 0,
        PinkSweep,
        Sine,
        DualSine,
    }

    public abstract class SignalGenerator
    {
        protected const int MAX_16BITS = 0x8000;

        public abstract OutputSignalType type
        {
            get;
        }

        //获取立体声信号
        public virtual byte[] GetStereoSignal(int length)
        {
            System.Diagnostics.Debug.Assert(length > 0);
            int monoSignalLength = (length / 2) / 2;    //2个声道，位深度16位2个字节
            short[] monoData = GetMonoSignal(monoSignalLength);
            short[] stereoData = new short[monoSignalLength * 2];
            for (int i = 0; i < monoSignalLength; ++i)
            {
                stereoData[2 * i] = monoData[i];
                stereoData[2 * i + 1] = monoData[i];
            }

            byte[] stereoBytes = new byte[stereoData.Length * 2];
            Buffer.BlockCopy(stereoData, 0, stereoBytes, 0, stereoBytes.Length);

            return stereoBytes;
        }

        public abstract short[] GetMonoSignal(int length);

        public static double dB2Linear(short level_in_dB)
        {
            return Math.Pow(10, level_in_dB / 20.0);
        }

        //最大公约数
        public static int gcd(int v1, int v2)
        {
            int max = v1 > v2 ? v1 : v2;
            int min = v1 < v2 ? v1 : v2;
            int remainder;
            while (min != 0)
            {
                remainder = max % min;
                max = min;
                min = remainder;
            }

            return max;
        }

        //最小公倍数
        public static int mcm(int v1, int v2)
        {
            return v1 * v2 / gcd(v1, v2);
        }

        public static ulong gcd(ulong v1, ulong v2)
        {
            ulong max = v1 > v2 ? v1 : v2;
            ulong min = v1 < v2 ? v1 : v2;
            ulong remainder;
            while (min != 0)
            {
                remainder = max % min;
                max = min;
                min = remainder;
            }

            return max;
        }

        //最小公倍数
        public static ulong mcm(ulong v1, ulong v2)
        {
            return v1 * v2 / gcd(v1, v2);
        }
    }


    public class PinkNoiseGenerator : SignalGenerator
    {
        public const int PinkSignalLength = 1024 * 512;

        private static float b0 = 0;
        private static float b1 = 0;
        private static float b2 = 0;
        private static float runningsum = 0;
        
        private double linearRatio = 1.0f;
        private float[] pinkNoise = null;

        private int index = 0;

        public short levelIndB
        {
            set
            {
                if(value > 0)
                {
                    value = 0;
                }
                linearRatio = dB2Linear(value);
            }
        }

        public PinkNoiseGenerator(short level)
        {
            linearRatio = dB2Linear(level < 0 ? level : (short)0);
            pinkNoise = GenPinkNoise(PinkSignalLength);
        }

        public override OutputSignalType type
        {
            get
            {
                return OutputSignalType.PinkNoise;
            }
        }

        public override short[] GetMonoSignal(int length)
        {
            CircularBuffer<float> circularBuffer = new CircularBuffer<float>(length);
            int remainder = length;
            while(remainder > 0)
            {
                int writeLength = Math.Min(remainder, pinkNoise.Length - index);
                circularBuffer.Write(pinkNoise, index, writeLength);
                index += writeLength;
                if (index >= pinkNoise.Length)
                {
                    index = 0;
                }
                remainder -= writeLength;
            }

            System.Diagnostics.Debug.Assert(circularBuffer.Count == length);

            float[] buffer = new float[length];
            circularBuffer.Read(buffer, 0, buffer.Length);

            short[] data = new short[length];

            for (int i = 0; i < data.Length; ++i )
            {
                double amp = buffer[i] * linearRatio * MAX_16BITS;
                if (amp > MAX_16BITS - 1)
                {
                    amp = MAX_16BITS - 1;
                }
                else if (amp < -MAX_16BITS)
                {
                    amp = -MAX_16BITS;
                }

                data[i] = (short)Math.Round(amp, 0, MidpointRounding.AwayFromZero);
            }

            return data;
        }

        private static float[] GenPinkNoise(int size)     //size是逻辑上的大小
        {
            Random random = new Random();
            float[] pink_data = new float[size];
            for (int i = 0; i < size; ++i)
            {
                /*实现1  Simple filter algorithm*/
                b0 = 0.99765f * b0 + (((float)random.NextDouble()) * 2 - 1) * 0.0990460f;
                b1 = 0.96300f * b1 + (((float)random.NextDouble()) * 2 - 1) * 0.2965164f;
                b2 = 0.57000f * b2 + (((float)random.NextDouble()) * 2 - 1) * 1.0526913f;
                runningsum = b0 + b1 + b2 + (((float)random.NextDouble()) * 2 - 1) * 0.1848f;
                runningsum *= 0.5f;

                float output = runningsum * 0.3f;
                if (output >= 0.999999)
                {
                    output = 0.999999f;
                }
                else if (output < -0.999999)
                {
                    output = -0.999999f;
                }

                pink_data[i] = output;
            }

            return pink_data;
        }
    }

    public class PinkSweepGenerator : SignalGenerator
    {
        private const short baseLevel = -6;

        private double linearRatio = 1.0f;

        private short[] pcm_16Bit = null;
        private int index = 0;

        public short levelIndB
        {
            set
            {
                if (value > 0)
                {
                    value = 0;
                }

                linearRatio = dB2Linear(value) / dB2Linear(baseLevel);
            }
        }

        //short 

        public PinkSweepGenerator(FFTSize fftSize, short level)
        {
            byte[] data = null;

            switch (fftSize)
            {
                case FFTSize.fftSize8k:
                    data = global::AEC.Properties.Resources.pink_sweep_8k;
                    break;

                case FFTSize.fftSize16k:
                    data = global::AEC.Properties.Resources.pink_sweep_16k;
                    break;

                case FFTSize.fftSize32k:
                case FFTSize.FPPO:
                    data = global::AEC.Properties.Resources.pink_sweep_32k;
                    break;

                default:
                    throw (new Exception("参数错误"));
            }

            pcm_16Bit = BinaryData2ShortData(data);

            linearRatio = dB2Linear(level < 0 ? level : (short)0) / dB2Linear(baseLevel);
        }

        public override OutputSignalType type
        {
            get
            {
                return OutputSignalType.PinkSweep;
            }
        }

        public override short[] GetMonoSignal(int length)
        {
            CircularBuffer<short> circularBuffer = new CircularBuffer<short>(length);
            int remainder = length;
            //int startIndex = index;
            while(remainder > 0)
            {
                int writeLength = Math.Min(remainder, pcm_16Bit.Length - index);
                circularBuffer.Write(pcm_16Bit, index, writeLength);
                index += writeLength;
                if (index >= pcm_16Bit.Length)
                {
                    index = 0;
                }
                remainder -= writeLength;
            }

            //int endIndex = index;
            //Console.WriteLine("PinkSweepGenerator.GetMonoSignal()...start index:{0}, end index:{1}", startIndex, endIndex);

            System.Diagnostics.Debug.Assert(circularBuffer.Count == length);

            short[] buffer = new short[length];
            circularBuffer.Read(buffer, 0, buffer.Length);

            short[] data = new short[length];

            for (int i = 0; i < data.Length; ++i )
            {
                double amp = buffer[i] * linearRatio;

                if (amp > MAX_16BITS - 1)
                {
                    amp = MAX_16BITS - 1;
                }
                else if (amp <= -MAX_16BITS)
                {
                    amp = -MAX_16BITS;
                }

                data[i] = (short)Math.Round(amp, 0, MidpointRounding.AwayFromZero);
            }

            return data;
        }

        private static short[] BinaryData2ShortData(byte[] data)
        {
            short[] buffer = new short[data.Length / 2];
            Buffer.BlockCopy(data, 0, buffer, 0, buffer.Length * sizeof(short));
            return buffer;
        }
    }

    public class SineGenerator : SignalGenerator
    {
        private readonly int sr = 48000;
        private uint index = 0;
        private double linearRatio = 1.0f;
        private int _freq = 1000;

        public short levelIndB
        {
            set
            {
                if (value > 0)
                {
                    value = 0;
                }
                linearRatio = dB2Linear(value);
            }
        }

        public int freq
        {
            set
            {
                _freq = value;
                index = 0;
            }
        }

        public SineGenerator(int sr_, short level, int freq_)
        {
            this.sr = sr_;
            linearRatio = dB2Linear(level < 0 ? level : (short)0);
            _freq = freq_;
        }

        public override OutputSignalType type
        {
            get
            {
                return OutputSignalType.Sine;
            }
        }

        public override short[] GetMonoSignal(int length)
        {
            short[] data = new short[length];
            int cirtialIndex = mcm(_freq, sr) / gcd(_freq, sr);
            for (int i = 0; i < length; ++i)
            {
                double amp = MAX_16BITS * linearRatio * Math.Sin(2 * Math.PI * _freq * index / sr);

                if (amp > MAX_16BITS - 1)
                {
                    amp = MAX_16BITS - 1;
                }
                else if (amp < -MAX_16BITS)
                {
                    amp = -MAX_16BITS;
                }

                data[i] = (short)Math.Round(amp, 0, MidpointRounding.AwayFromZero);

                ++index;
                if (index >= cirtialIndex)
                {
                    index = 0;
                }
            }

            return data;
        }
    }

    public class DualSineGenerator : SignalGenerator
    {
        private readonly int sr = 48000;
        private ulong index = 0;
        private double sine1LinearRatio = 1.0f;
        private double sine2LinearRatio = 1.0f;
        private int _sine1freq = 1000;
        private int _sine2freq = 1000;

        public short sine1LevelIndB
        {
            set
            {
                if (value > 0)
                {
                    value = 0;
                }
                sine1LinearRatio = dB2Linear(value);
            }
        }

        public short sine2LevelIndB
        {
            set
            {
                if (value > 0)
                {
                    value = 0;
                }
                sine2LinearRatio = dB2Linear(value);
            }
        }

        public int sine1freq
        {
            set
            {
                _sine1freq = value;
                index = 0;
            }
        }

        public int sine2freq
        {
            set
            {
                _sine2freq = value;
                index = 0;
            }
        }

        public DualSineGenerator(int sr_, short level1, int freq1, short level2, int freq2)
        {
            this.sr = sr_;
            sine1LinearRatio = dB2Linear(level1 < 0 ? level1 : (short)0);
            _sine1freq = freq1;
            sine2LinearRatio = dB2Linear(level2 < 0 ? level2 : (short)0);
            _sine2freq = freq2;
        }

        public override OutputSignalType type
        {
            get
            {
                return OutputSignalType.DualSine;
            }
        }

        public override short[] GetMonoSignal(int length)
        {
            short[] data = new short[length];
            ulong cirtialIndex = mcm(mcm((ulong)_sine1freq, (ulong)_sine2freq), (ulong)sr) / gcd(gcd((ulong)_sine1freq, (ulong)_sine2freq), (ulong)sr);
            for (int i = 0; i < length; ++i)
            {
                double amp = sine1LinearRatio * Math.Sin(2 * Math.PI * _sine1freq * index / sr) + sine2LinearRatio * Math.Sin(2 * Math.PI * _sine2freq * index / sr);
                amp *= MAX_16BITS;

                if (amp > MAX_16BITS - 1)
                {
                    amp = MAX_16BITS - 1;
                }
                else if (amp < -MAX_16BITS)
                {
                    amp = -MAX_16BITS;
                }
                data[i] = (short)Math.Round(amp, 0, MidpointRounding.AwayFromZero);

                ++index;
                if (index >= cirtialIndex)
                {
                    index = 0;
                }
            }

            return data;
        }
    }
}
