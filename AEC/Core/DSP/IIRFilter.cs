using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core.DSP
{
    class IIRFilter
    {
        private delegate AnalogFilterPrototype GetAnalogFilterPrototyperHandler();
        private delegate void CalcXOrderCascadedStagesFilterFreqRespHandler(XPFilterBase filter, AnalogFilterPrototype analogFilterPrototype);

        private const double PI = 3.1415926535897932384626433832795;

        public static DSPHelper.EQMode eqMode =  DSPHelper.EQMode.Classical;

        private const double InfinitesimalAmpIndB = -320;  //无穷小幅度dB值
        private static readonly double ZeroMagnitudeThreshold = Math.Pow(10, InfinitesimalAmpIndB / 20.0);

        public readonly double samplingRate;

        private double[] omegaList = null;
        private double[] zeroAmps = null;

        private readonly static Dictionary<FilterType, GetAnalogFilterPrototyperHandler> pFilterMapToGetAnalogFilterPrototyperHandler = new Dictionary<FilterType, GetAnalogFilterPrototyperHandler>()
        {
            {FilterType.FirstOrder,         AnalogFilterPrototyperFactory.Create1stOrderButterworthFilter},
            {FilterType.Bessel12dB,         AnalogFilterPrototyperFactory.Create2ndOrderBesselFilter},
            {FilterType.Butterworth12dB,    AnalogFilterPrototyperFactory.Create2ndOrderButterworthFilter},
            {FilterType.LinkwitzRiley12dB,  AnalogFilterPrototyperFactory.Create2ndOrderLinkwitzRileyFilter},
            {FilterType.Bessel18dB,         AnalogFilterPrototyperFactory.Create3rdOrderBesselFilter},
            {FilterType.Butterworth18dB,    AnalogFilterPrototyperFactory.Create3rdOrderButterworthFilter},
            {FilterType.Bessel24dB,         AnalogFilterPrototyperFactory.Create4thOrderBesselFilter},
            {FilterType.Butterworth24dB,    AnalogFilterPrototyperFactory.Create4thOrderButterworthFilter},
            {FilterType.LinkwitzRiley24dB,  AnalogFilterPrototyperFactory.Create4thOrderLinkwitzRileyFilter},
            {FilterType.Bessel36dB,         AnalogFilterPrototyperFactory.Create6thOrderBesselFilter},
            {FilterType.Butterworth36dB,    AnalogFilterPrototyperFactory.Create6thOrderButterworthFilter},
            {FilterType.LinkwitzRiley36dB,  AnalogFilterPrototyperFactory.Create6thOrderLinkwitzRileyFilter},
            {FilterType.Bessel48dB,         AnalogFilterPrototyperFactory.Create8thOrderBesselFilter},
            {FilterType.Butterworth48dB,    AnalogFilterPrototyperFactory.Create8thOrderButterworthFilter},
            {FilterType.LinkwitzRiley48dB,  AnalogFilterPrototyperFactory.Create8thOrderLinkwitzRileyFilter},
            {FilterType.X5_Bessel24dB,          AnalogFilterPrototyperFactory.Create4thOrderBesselFilter},
            {FilterType.X5_Butterworth24dB,     AnalogFilterPrototyperFactory.Create4thOrderButterworthFilter},
            {FilterType.X5_LinkwitzRiley24dB,   AnalogFilterPrototyperFactory.Create4thOrderLinkwitzRileyFilter},
        };

        private readonly Dictionary<FilterType, CalcXOrderCascadedStagesFilterFreqRespHandler> pFilterMapToCalcXOrderCascadedStagesFilterFreqRespHandler;

        public IIRFilter(double sr)
        {
            this.samplingRate = sr;

            this.InitFreqList();

            this.pFilterMapToCalcXOrderCascadedStagesFilterFreqRespHandler = new Dictionary<FilterType, CalcXOrderCascadedStagesFilterFreqRespHandler>()
            {
                {FilterType.FirstOrder,         Calc1stOrderFilterFreqResp},
                {FilterType.Bessel12dB,         Calc2ndOrderFilterFreqResp},
                {FilterType.Butterworth12dB,    Calc2ndOrderFilterFreqResp},
                {FilterType.LinkwitzRiley12dB,  Calc2ndOrderFilterFreqResp},
                {FilterType.Bessel18dB,         Calc3rdOrderFilterFreqResp},
                {FilterType.Butterworth18dB,    Calc3rdOrderFilterFreqResp},
                {FilterType.Bessel24dB,         CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.Butterworth24dB,    CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.LinkwitzRiley24dB,  CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.Bessel36dB,         CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.Butterworth36dB,    CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.LinkwitzRiley36dB,  CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.Bessel48dB,         CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.Butterworth48dB,    CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.LinkwitzRiley48dB,  CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.X5_Bessel24dB,          CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.X5_Butterworth24dB,     CalcHigherThan3thOrderFilterFreqResp},
                {FilterType.X5_LinkwitzRiley24dB,   CalcHigherThan3thOrderFilterFreqResp},
            };
        }

        private void InitFreqList()
        {
            omegaList = new double[FreqResp.keyFreqListLength];
            zeroAmps = new double[FreqResp.keyFreqListLength];
            for (int i = 0; i < FreqResp.keyFreqListLength; ++i)
            {
                omegaList[i] = 2 * PI * FreqResp.keyFreqList[i] / this.samplingRate;
            }
        }

        public void UpdateEQFreqResp(EQ eq)
        {
            Debug.Assert(eq != null);
            if (eq.freqRespIsZero)
            {
                eq.freqResp.Reset();
                return;
            }

            BiQuadFilter iirFilter = null;

            switch (eq.type)
            {
                case EQType.Peaking:
                    if (eqMode == DSPHelper.EQMode.Professional)
                    {
                        iirFilter = BiQuadFilter.AnalogPeakingEQ(this.samplingRate, eq.freq, eq.gain, eq.q);
                    }
                    else
                    {
                        iirFilter = BiQuadFilter.PeakingEQ(this.samplingRate, eq.freq, eq.gain, eq.q);
                    }
                    break;

                case EQType.LowShelf:
                    iirFilter = BiQuadFilter.LowShelf(this.samplingRate, eq.freq, eq.gain, eq.q);
                    break;

                case EQType.HighShelf:
                    iirFilter = BiQuadFilter.HighShelf(this.samplingRate, eq.freq, eq.gain, eq.q);
                    break;
            }
            if (iirFilter != null)
            {
                UpdateBiQuadFilterFreqResp(iirFilter, eq.freqResp);
            }
        }

        //更新音色控制滤波器频率响应
        public void UpdateToneControlFilterFreqResp(ToneControlFilter filter)
        {
            Debug.Assert(filter != null);
            if (filter.freqRespIsZero)
            {
                filter.freqResp.Reset();
                return;
            }

            BiQuadFilter iirFilter = BiQuadFilter.ToneControl(this.samplingRate, filter.freqBassCutoff, filter.boostBass, filter.freqTrebleCutoff, filter.boostTreble);

            UpdateBiQuadFilterFreqResp(iirFilter, filter.freqResp);
        }

        //计算高/低通滤波器频率响应
        public void UpdateXPFilterFreqResp(XPFilterBase filter)
        {
            Debug.Assert(filter != null && filter.freqResp != null);

            filter.freqResp.Reset();

            if (filter.freqRespIsZero)
            {
                filter.freqResp.Reset();
                return;
            }

            if (filter.isClassicalFilter)
            {
                BiQuadFilter iirFilter = null;

                switch (filter.pFilterType)
                {
                    case XPFilterType.LowPass:
                        iirFilter = BiQuadFilter.LowPass(this.samplingRate, filter.freq, filter.q);
                        break;

                    case XPFilterType.HighPass:
                        iirFilter = BiQuadFilter.HighPass(this.samplingRate, filter.freq, filter.q);
                        break;
                }

                if (iirFilter != null)
                {
                    UpdateBiQuadFilterFreqResp(iirFilter, filter.freqResp);
                    if (filter.type != FilterType.X5_User12dB)
                    {
                        filter.freqResp.Scale(2.0);
                    }
                }
            }
            else
            {
                AnalogFilterPrototype analogFilterPrototype = null;

                if (pFilterMapToGetAnalogFilterPrototyperHandler.ContainsKey(filter.type))
                {
                    analogFilterPrototype = pFilterMapToGetAnalogFilterPrototyperHandler[filter.type]();
                }
                else
                {
                    throw (new Exception("UpdateFilterFreqResp()...filter type 错误!"));
                }

                if (analogFilterPrototype == null)
                {
                    return;
                }

                if (this.pFilterMapToCalcXOrderCascadedStagesFilterFreqRespHandler.ContainsKey(filter.type))
                {
                    this.pFilterMapToCalcXOrderCascadedStagesFilterFreqRespHandler[filter.type](filter, analogFilterPrototype);
                }

                //FilterCoefficient coef = null;
                //switch (filter.type)
                //{
                //    case FilterType.Bessel24dB:
                //        coef = FilterCoefficient.FourthOrderBessel(filter.pFilterType, filter.freq, this.samplingRate);
                //        break;

                //    case FilterType.Butterworth24dB:
                //        coef = FilterCoefficient.FourthOrderButterworth(filter.pFilterType, filter.freq, this.samplingRate);
                //        break;

                //    case FilterType.LinkwitzRiley24dB:
                //        coef = FilterCoefficient.FourthOrderLinkwitz_Riley(filter.pFilterType, filter.freq, this.samplingRate);
                //        break;

                //    default:
                //        throw (new Exception("UpdateFilterFreqResp()...filter type 错误!"));
                //}

                //if (coef != null)
                //{
                //    UpdateFourthOrderFilterFreqResp(coef, filter.freqResp);
                //}
            }
        }

        private void UpdateBiQuadFilterFreqResp(BiQuadFilter filter, FreqResp freqResp)
        {
            for (int i = 0; i < omegaList.Length; ++i)
            {
                //if (i < 411 || i > 415)
                //{
                //    continue;
                //}

                double w = omegaList[i];
                Complex z = new Complex(Math.Cos(w), -Math.Sin(w));
                Complex z2 = z * z;

                Complex numerator = filter.b0 + filter.b1 * z + filter.b2 * z2;
                Complex denominator = filter.a0 + filter.a1 * z + filter.a2 * z2;

                //Complex numerator = filter.b0 + (filter.b1 + filter.b2 * z) * z;
                //Complex denominator = filter.a0 + (filter.a1 + filter.a2 * z) * z;

                Complex h = numerator / denominator;
                double mag = h.Magnitude;

                if (mag > ZeroMagnitudeThreshold)
                {
                    freqResp.amps[i] = 20 * Math.Log10(mag);
                }
                else
                {
                    freqResp.amps[i] = InfinitesimalAmpIndB;
                }
            }
        }

        private void Calc1stOrderFilterFreqResp(XPFilterBase filter, AnalogFilterPrototype cascadedStagesFilter)
        {
            Debug.Assert(filter != null && cascadedStagesFilter != null && cascadedStagesFilter.singlePole != null);

            FilterCoefficient coef = FilterCoefficientFactory.Create1stOrderFilterCoefficient(cascadedStagesFilter.singlePole.Real, cascadedStagesFilter.k, filter.pFilterType, filter.freq, this.samplingRate);

            Debug.Assert(coef.order == FilterCoefficientFactory.Order1st);

            UpdateFirstOrderFilterFreqResp(coef, filter.freqResp);
        }

        private void Calc2ndOrderFilterFreqResp(XPFilterBase filter, AnalogFilterPrototype cascadedStagesFilter)
        {
            Debug.Assert(filter != null && cascadedStagesFilter != null && cascadedStagesFilter.polePairs != null && cascadedStagesFilter.polePairs.Length == 1);

            FilterCoefficient coef = FilterCoefficientFactory.Create2ndOrderFilterCoefficient(cascadedStagesFilter.polePairs[0], cascadedStagesFilter.k, filter.pFilterType, filter.freq, this.samplingRate);

            Debug.Assert(coef.order == FilterCoefficientFactory.Order2nd);

            UpdateSecondOrderFilterFreqResp(coef, filter.freqResp);

        }

        private void Calc3rdOrderFilterFreqResp(XPFilterBase filter, AnalogFilterPrototype cascadedStagesFilter)
        {
            Debug.Assert(filter != null && cascadedStagesFilter != null &&
                cascadedStagesFilter.singlePole != null &&
                cascadedStagesFilter.polePairs != null && cascadedStagesFilter.polePairs.Length == 1);

            FilterCoefficient coef1st = FilterCoefficientFactory.Create1stOrderFilterCoefficient(cascadedStagesFilter.singlePole.Real, Math.Sqrt(cascadedStagesFilter.k), filter.pFilterType, filter.freq, this.samplingRate);
            Debug.Assert(coef1st.order == FilterCoefficientFactory.Order1st);
            UpdateFirstOrderFilterFreqResp(coef1st, filter.freqResp);

            FreqResp freqResp2nd = new FreqResp();
            FilterCoefficient coef2nd = FilterCoefficientFactory.Create2ndOrderFilterCoefficient(cascadedStagesFilter.polePairs[0], Math.Sqrt(cascadedStagesFilter.k), filter.pFilterType, filter.freq, this.samplingRate);
            Debug.Assert(coef2nd.order == FilterCoefficientFactory.Order2nd);
            UpdateSecondOrderFilterFreqResp(coef2nd, freqResp2nd);

            filter.freqResp.Add(freqResp2nd);
        }

        //计算超过3阶（即 4 阶或更高阶）滤波器频率响应，要求是偶数阶
        private void CalcHigherThan3thOrderFilterFreqResp(XPFilterBase filter, AnalogFilterPrototype cascadedStagesFilter)
        {
            Debug.Assert(filter != null && cascadedStagesFilter != null &&
                cascadedStagesFilter.polePairs != null && cascadedStagesFilter.polePairs.Length >= 2);

            for (int i = 0; i < cascadedStagesFilter.polePairs.Length; ++i)
            {
                FreqResp freqResp2nd = new FreqResp();
                FilterCoefficient coef2nd = FilterCoefficientFactory.Create2ndOrderFilterCoefficient(cascadedStagesFilter.polePairs[i], Math.Pow(cascadedStagesFilter.k, 1.0 / cascadedStagesFilter.polePairs.Length), filter.pFilterType, filter.freq, this.samplingRate);
                Debug.Assert(coef2nd.order == FilterCoefficientFactory.Order2nd);
                UpdateSecondOrderFilterFreqResp(coef2nd, freqResp2nd);

                filter.freqResp.Add(freqResp2nd);
            }
        }

        private void UpdateFirstOrderFilterFreqResp(FilterCoefficient coef, FreqResp freqResp)
        {
            System.Diagnostics.Debug.Assert(coef != null && freqResp != null && coef.order == 1);

            for (int i = 0; i < omegaList.Length; ++i)
            {
                double w = omegaList[i];
                Complex z = new Complex(Math.Cos(w), -Math.Sin(w));

                Complex numerator = coef.b[0] + coef.b[1] * z;
                Complex denominator = coef.a[0] + coef.a[1] * z;

                Complex h = numerator / denominator;
                freqResp.amps[i] = 20 * Math.Log10(h.Magnitude);
            }
        }

        private void UpdateSecondOrderFilterFreqResp(FilterCoefficient coef, FreqResp freqResp)
        {
            System.Diagnostics.Debug.Assert(coef != null && freqResp != null && coef.order == 2);

            for (int i = 0; i < omegaList.Length; ++i)
            {
                double w = omegaList[i];
                Complex z = new Complex(Math.Cos(w), -Math.Sin(w));
                Complex z2 = z * z;

                Complex numerator = coef.b[0] + coef.b[1] * z + coef.b[2] * z2;
                Complex denominator = coef.a[0] + coef.a[1] * z + coef.a[2] * z2;

                Complex h = numerator / denominator;
                freqResp.amps[i] = 20 * Math.Log10(h.Magnitude);
            }
        }
    }
}
