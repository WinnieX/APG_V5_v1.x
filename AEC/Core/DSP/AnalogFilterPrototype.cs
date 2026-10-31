using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core.DSP
{
    //模拟滤波器原型
    public class AnalogFilterPrototype
    {
        public const double DefaultK = 1.0;
        public readonly Complex singlePole;
        public readonly Complex[] polePairs;        //拉普拉斯变换, s平面中系统的极点对, 这两个点是共轭的
        public readonly double k = DefaultK;

        public AnalogFilterPrototype(double poleReal, double k = DefaultK)
        {
            this.singlePole = new Complex(poleReal, 0);
            this.k = k;
        }

        public AnalogFilterPrototype(Complex[] polePairs, double k = DefaultK)
        {
            this.polePairs = polePairs;
            this.k = k;
        }

        public AnalogFilterPrototype(double poleReal, Complex[] polePairs, double k = DefaultK)
        {
            this.singlePole = new Complex(poleReal, 0);
            this.polePairs = polePairs;
            this.k = k;
        }

        public void UpdatePFilterFreqResp(XPFilterType type, double fc, double sr, FreqResp freqResp)
        {
            Debug.Assert(type == XPFilterType.HighPass || type == XPFilterType.LowPass);

            freqResp.Reset();

            if(this.polePairs != null && singlePole == null)
            {

            }
            else if(this.polePairs != null && singlePole != null)
            {

            }
            else if(singlePole != null && this.polePairs == null)
            {

            }
        }

        //private void AddPolePairsFreqResp(PFilterType type, double fc, double sr, FreqResp freqResp)
        //{
        //    Debug.Assert(polePairs != null && freqResp != null);

        //    foreach(PolePair polePair in this.polePairs)
        //    {
        //        //polePair
        //        FilterCoefficient coef = FilterCoefficientFactory.Create2ndOrderFilterCoefficient(polePair, type, fc, sr);

        //        Debug.Assert(coef.order == FilterCoefficientFactory.Order2nd);
        //    }
        //}

        //private void UpdatePolePairsFreqResp(PFilterType type, double fc, double sr, FreqResp freqResp)
        //{
        //    Debug.Assert(singlePole != null && freqResp != null);

        //}
    }
}
