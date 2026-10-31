using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core.DSP
{
    #region 双二阶IIR滤波器
    class BiQuadFilter
    {
        //public static const int kCoeffLength = 3;
        public double a0
        {
            get;
            private set;
        }
        public double a1
        {
            get;
            private set;
        }
        public double a2
        {
            get;
            private set;
        }
        public double b0
        {
            get;
            private set;
        }
        public double b1
        {
            get;
            private set;
        }
        public double b2
        {
            get;
            private set;
        }

        //public double a0 = 0.0;
        //public double a1 = 0.0;
        //public double a2 = 0.0;
        //public double b0 = 0.0;
        //public double b1 = 0.0;
        //public double b2 = 0.0;

        private const double PI = 3.1415926535897932384626433832795;

        private BiQuadFilter(double a_0, double a_1, double a_2, double b_0, double b_1, double b_2)
        {
            this.b0 = b_0 / a_0;
            this.b1 = b_1 / a_0;
            this.b2 = b_2 / a_0;
            this.a1 = a_1 / a_0;
            this.a2 = a_2 / a_0;
            this.a0 = 1.0;
        }

        public static BiQuadFilter PeakingEQ(double samplingRate, double freq, double gain, double q)
        {
            double A = Math.Pow(10, gain / 40);
            double w0 = 2 * PI * freq / samplingRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / (2 * q);

            double b_0 = 1 + alpha * A;
            double b_1 = -2 * cosw0;
            double b_2 = 1 - alpha * A;
            double a_0 = 1 + alpha / A;
            double a_1 = -2 * cosw0;
            double a_2 = 1 - alpha / A;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }

        public static BiQuadFilter AnalogPeakingEQ(double samplingRate, double freq, double gain, double q)
        {
            double A = Math.Pow(10, gain / 40);
            double w0 = 2 * PI * freq / samplingRate;
            double cosw0 = Math.Cos(w0);
            double B = gain <= 0.0 ? A : 1 / A;
            double alpha = Math.Sin(w0) / (2 * B * q);        //XTA处理器算法使用此值

            double b_0 = 1 + alpha * A;
            double b_1 = -2 * cosw0;
            double b_2 = 1 - alpha * A;
            double a_0 = 1 + alpha / A;
            double a_1 = -2 * cosw0;
            double a_2 = 1 - alpha / A;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }

        public static BiQuadFilter LowShelf(double samplingRate, double freq, double gain, double s)
        {
            double A = Math.Pow(10, gain / 40);
            double w0 = 2 * PI * freq / samplingRate;
            double cosw0 = Math.Cos(w0);
            double B = gain <= 0.0 ? A : 1 / A;
            double alpha = Math.Sin(w0) / 2 * Math.Sqrt((A + 1 / A) * (1 / s - 1) + 2);

            double temp = 2 * Math.Sqrt(A) * alpha;

            double b_0 = A * ((A + 1) - (A - 1) * cosw0 + temp);
            double b_1 = 2 * A * ((A - 1) - (A + 1) * cosw0);
            double b_2 = A * ((A + 1) - (A - 1) * cosw0 - temp);
            double a_0 = (A + 1) + (A - 1) * cosw0 + temp;
            double a_1 = -2 * ((A - 1) + (A + 1) * cosw0);
            double a_2 = (A + 1) + (A - 1) * cosw0 - temp;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }

        public static BiQuadFilter AnalogLowShelf(double samplingRate, double freq, double gain, double s)
        {
            if (s >= 0.4)
            {
                s = 2 * s - 0.4;
            }
            else
            {
                s = 1.5 * s - 0.2;
            }
            double A = Math.Pow(10, gain / 40);
            double w0 = 2 * PI * freq / samplingRate;
            double cosw0 = Math.Cos(w0);
            double B = gain <= 0.0 ? A : 1 / A;
            double alpha = Math.Sin(w0) / 2 * Math.Sqrt((A + 1 / A) * (1 / s - 1) + 2);

            double temp = 2 * Math.Sqrt(A) * alpha;

            double b_0 = A * ((A + 1) - (A - 1) * cosw0 + temp);
            double b_1 = 2 * A * ((A - 1) - (A + 1) * cosw0);
            double b_2 = A * ((A + 1) - (A - 1) * cosw0 - temp);
            double a_0 = (A + 1) + (A - 1) * cosw0 + temp;
            double a_1 = -2 * ((A - 1) + (A + 1) * cosw0);
            double a_2 = (A + 1) + (A - 1) * cosw0 - temp;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }

        public static BiQuadFilter HighShelf(double samplingRate, double freq, double gain, double s)
        {
            double A = Math.Pow(10, gain / 40);
            double w0 = 2 * PI * freq / samplingRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / 2 * Math.Sqrt((A + 1 / A) * (1 / s - 1) + 2);

            double temp = 2 * Math.Sqrt(A) * alpha;

            double b_0 = A * ((A + 1) + (A - 1) * cosw0 + temp);
            double b_1 = -2 * A * ((A - 1) + (A + 1) * cosw0);
            double b_2 = A * ((A + 1) + (A - 1) * cosw0 - temp);
            double a_0 = (A + 1) - (A - 1) * cosw0 + temp;
            double a_1 = 2 * ((A - 1) - (A + 1) * cosw0);
            double a_2 = (A + 1) - (A - 1) * cosw0 - temp;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }

        public static BiQuadFilter AnalogHighShelf(double samplingRate, double freq, double gain, double s)
        {

            if (s >= 0.4)
            {
                s = 2 * s - 0.4;
            }
            else
            {
                s = 1.5 * s - 0.2;
            }

            double A = Math.Pow(10, gain / 40);
            double w0 = 2 * PI * freq / samplingRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / 2 * Math.Sqrt((A + 1 / A) * (1 / s - 1) + 2);

            double temp = 2 * Math.Sqrt(A) * alpha;

            double b_0 = A * ((A + 1) + (A - 1) * cosw0 + temp);
            double b_1 = -2 * A * ((A - 1) + (A + 1) * cosw0);
            double b_2 = A * ((A + 1) + (A - 1) * cosw0 - temp);
            double a_0 = (A + 1) - (A - 1) * cosw0 + temp;
            double a_1 = 2 * ((A - 1) - (A + 1) * cosw0);
            double a_2 = (A + 1) - (A - 1) * cosw0 - temp;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }

        //public static BiQuadFilter BandPass(double samplingRate, double freq, double q)
        //{
        //    double w0 = 2 * PI * freq / samplingRate;
        //    double cosw0 = Math.Cos(w0);
        //    double alpha = Math.Sin(w0) / (2 * q);

        //    double b_0 = alpha;
        //    double b_1 = 0;
        //    double b_2 = -alpha;
        //    double a_0 = 1 + alpha;
        //    double a_1 = -2 * cosw0;
        //    double a_2 = 1 - alpha;

        //    return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        //}

        //public static BiQuadFilter Notch(double samplingRate, double freq, double q)
        //{
        //    double w0 = 2 * PI * freq / samplingRate;
        //    double cosw0 = Math.Cos(w0);
        //    double alpha = Math.Sin(w0) / (2 * q);

        //    double b_0 = 1;
        //    double b_1 = -2 * cosw0;
        //    double b_2 = 1;
        //    double a_0 = 1 + alpha;
        //    double a_1 = -2 * cosw0;
        //    double a_2 = 1 - alpha;

        //    return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        //}

        //public static BiQuadFilter AllPass(double samplingRate, double freq, double q)
        //{
        //    double w0 = 2 * PI * freq / samplingRate;
        //    double cosw0 = Math.Cos(w0);
        //    double alpha = Math.Sin(w0) / (2 * q);

        //    double b_0 = 1 - alpha;
        //    double b_1 = -2 * cosw0;
        //    double b_2 = 1 + alpha;
        //    double a_0 = 1 + alpha;
        //    double a_1 = -2 * cosw0;
        //    double a_2 = 1 - alpha;

        //    return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        //}

        public static BiQuadFilter LowPass(double samplingRate, double freq, double q)
        {
            double w0 = 2 * PI * freq / samplingRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / (2 * q);

            double b_0 = (1 - cosw0) / 2;
            double b_1 = 1 - cosw0;
            double b_2 = (1 - cosw0) / 2;
            double a_0 = 1 + alpha;
            double a_1 = -2 * cosw0;
            double a_2 = 1 - alpha;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }

        public static BiQuadFilter HighPass(double samplingRate, double freq, double q)
        {
            double w0 = 2 * PI * freq / samplingRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / (2 * q);

            double b_0 = (1 + cosw0) / 2;
            double b_1 = -(1 + cosw0);
            double b_2 = (1 + cosw0) / 2;
            double a_0 = 1 + alpha;
            double a_1 = -2 * cosw0;
            double a_2 = 1 - alpha;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }

        public static BiQuadFilter ToneControl(double samplingRate, double freqBassCutoff, double boostBass, double freqTrebleCutoff, double boostTreble)
        {
            double boostT = Math.Pow(10, boostTreble / 20);
            double boostB = Math.Pow(10, boostBass / 20);
            double tanT = Math.Tan(Math.PI * freqTrebleCutoff / samplingRate);
            double tanB = Math.Tan(Math.PI * freqBassCutoff / samplingRate);
            double KnumT = 2 / (1 + (1 / boostT));
            double KdenT = 2 / (1 + boostT);
            double KnumB = 2 / (1 + (1 / boostB));
            double KdenB = 2 / (1 + boostB);
            double a10 = tanT + KdenT;
            double b10 = tanT + KnumT;
            double a11 = tanT - KdenT;
            double b11 = tanT - KnumT;

            double a20 = (tanB * KdenB) + 1;
            double b20 = (tanB * KnumB) - 1;
            double a21 = (tanB * KdenB) - 1;
            double b21 = (tanB * KnumB) + 1;

            double b_0 = b10 * b20;
            double b_1 = ((b10 * b21) + (b11 * b20));
            double b_2 = (b11 * b21);
            double a_0 = a10 * a20;
            double a_1 = ((a10 * a21) + (a11 * a20));
            double a_2 = a11 * a21;

            return new BiQuadFilter(a_0, a_1, a_2, b_0, b_1, b_2);
        }
    }
    #endregion
}
