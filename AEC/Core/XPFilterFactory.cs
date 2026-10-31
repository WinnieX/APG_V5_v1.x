using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class XPFilterFactory
    {
        #region 创建高通滤波器
        public static HighPassFilter CreateImmutableTypeHighPassFilter(double freqUpperLimit, FilterType immutableType = FilterType.Butterworth12dB)
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(freqUpperLimit),
                XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(immutableType));
        }

        public static HighPassFilter CreateImmutableTypeHighPassFilter(double freqUpperLimit, double freqInit, FilterType immutableType = FilterType.Butterworth12dB)
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(freqUpperLimit, freqInit),
                XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(immutableType));
        }

        public static HighPassFilter Create2ndOrderHighPassFilter(double freqUpperLimit)
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(freqUpperLimit),
                XPFilterTypeConverterFactory.Create2ndOrderFilterTypeConverter());
        }

        public static HighPassFilter Create4thOrderHighPassFilter(double freqUpperLimit)
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(freqUpperLimit),
                XPFilterTypeConverterFactory.Create4thOrderFilterTypeConverter());
        }

        public static HighPassFilter CreateClassicalXoverHighPassFilter()
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(),
                XPFilterTypeConverterFactory.CreateClassicalFilterTypeConverter(),
                XPFilterQConverterFactory.CreateDefaultFilterQConverter());
        }

        public static HighPassFilter CreateVKxSOutputHighPassFilter()
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(),
                XPFilterTypeConverterFactory.CreateVKxSFilterTypeConverter(),
                XPFilterQConverterFactory.CreateDefaultFilterQConverter());
        }

        public static HighPassFilter CreateX5XoverHighPassFilter(double freqUpperLimit)
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(freqUpperLimit),
                XPFilterTypeConverterFactory.CreateClassicalFilterTypeConverter(),
                XPFilterQConverterFactory.CreateDefaultFilterQConverter());
        }

        public static HighPassFilter CreateX5HighPassFilter(double freqUpperLimit)
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(freqUpperLimit),
                XPFilterTypeConverterFactory.CreateX5FilterTypeConverter());
        }

        public static HighPassFilter CreateX5ImmutableTypeHighPassFilter(double freqLowerLimit, double freqUpperLimit, FilterType immutableType = FilterType.Butterworth12dB)
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(freqLowerLimit, freqUpperLimit, freqLowerLimit),
                XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(immutableType));
        }

        public static HighPassFilter CreateProfessinalXoverHighPassFilter()
        {
            return new HighPassFilter(
                XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(),
                XPFilterTypeConverterFactory.CreateProfessionalFilterTypeConverter());
        }
        #endregion

        #region 创建低通滤波器
        public static LowPassFilter CreateImmutableTypeLowPassFilter(double freqLowerLimit, FilterType immutableType)
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(freqLowerLimit),
                XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(immutableType));
        }

        public static LowPassFilter CreateImmutableTypeLowPassFilter(double freqLowerLimit, double freqInit, FilterType immutableType = FilterType.Butterworth12dB)
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(freqLowerLimit, freqInit),
                XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(immutableType));
        }

        public static LowPassFilter Create2ndOrderLowPassFilter(double freqLowerLimit, double freqInit)
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(freqLowerLimit, freqInit),
                XPFilterTypeConverterFactory.Create2ndOrderFilterTypeConverter());
        }

        public static LowPassFilter Create4thOrderLowPassFilter(double freqLowerLimit)
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(freqLowerLimit, DataUtils.maxFilterFreq),
                XPFilterTypeConverterFactory.Create4thOrderFilterTypeConverter());
        }

        public static LowPassFilter Create8thOrderLowPassFilter(double freqLowerLimit)
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(freqLowerLimit, DataUtils.maxFilterFreq),
                XPFilterTypeConverterFactory.CreateProfessionalFilterTypeConverter());
        }

        public static LowPassFilter CreateClassicalXoverLowPassFilter()
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(),
                XPFilterTypeConverterFactory.CreateClassicalFilterTypeConverter(),
                XPFilterQConverterFactory.CreateDefaultFilterQConverter());
        }

        public static LowPassFilter CreateVKxSOutputLowPassFilter()
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(),
                XPFilterTypeConverterFactory.CreateVKxSFilterTypeConverter(),
                XPFilterQConverterFactory.CreateDefaultFilterQConverter());
        }

        public static LowPassFilter CreateX5ImmutableTypeLowPassFilter(double freqLowerLimit, double freqUpperLimit, FilterType immutableType)
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateX5PassFilterFreqConverter(freqLowerLimit, freqUpperLimit),
                XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(immutableType));
        }

        public static LowPassFilter CreateX5XoverLowPassFilter(double freqUpperLimit)
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateX5LowPassFilterFreqConverter(freqUpperLimit),
                XPFilterTypeConverterFactory.CreateClassicalFilterTypeConverter(),
                XPFilterQConverterFactory.CreateDefaultFilterQConverter());
        }

        public static LowPassFilter CreateProfessinalXoverLowPassFilter()
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(),
                XPFilterTypeConverterFactory.CreateProfessionalFilterTypeConverter());
        }

        public static LowPassFilter CreateAdjustableQLowPassFilter(double freqLowerLimit, double qLowerLimit, double qUpperLimit)
        {
            return new LowPassFilter(
                XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(freqLowerLimit),
                XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(FilterType.X5_User12dB),
                XPFilterQConverterFactory.CreateFilterQConverter(qLowerLimit, qUpperLimit, qUpperLimit));
        }
        #endregion
    }
}
