using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core.DSP
{
    public class AnalogFilterPrototyperFactory
    {
        #region 一阶滤波器
        public static AnalogFilterPrototype Create1stOrderButterworthFilter()
        {
            double singlePoleReal = -1;

            return new AnalogFilterPrototype(singlePoleReal);
        }
        #endregion

        #region 二阶滤波器
        public static AnalogFilterPrototype Create2ndOrderBesselFilter()
        {
            //norm : {'phase', 'delay', 'mag'}, optional
            //Critical frequency normalization:

            //``phase``
            //    The filter is normalized such that the phase response reaches its
            //    midpoint at angular (e.g. rad/s) frequency `Wn`.  This happens for
            //    both low-pass and high-pass filters, so this is the
            //    "phase-matched" case.

            //    The magnitude response asymptotes are the same as a Butterworth
            //    filter of the same order with a cutoff of `Wn`.

            //    This is the default, and matches MATLAB's implementation.

            //``delay``
            //    The filter is normalized such that the group delay in the passband
            //    is 1/`Wn` (e.g. seconds).  This is the "natural" type obtained by
            //    solving Bessel polynomials.

            //``mag``
            //    The filter is normalized such that the gain magnitude is -3 dB at
            //    angular frequency `Wn`.



            //norm is phase
            //PolePair[] polePairs = new PolePair[]
            //{
            //    new PolePair(-Math.Sqrt(3) / 2.0, 0.5),
            //};

            //return new CascadedStagesFilter(polePairs);

            //norm is mag
            Complex[] polePairs = new Complex[]
            {
                new Complex(-1.1016013305921617, 0.6360098247570346),
            };

            return new AnalogFilterPrototype(polePairs, 1.618033988749895);
        }

        public static AnalogFilterPrototype Create2ndOrderButterworthFilter()
        {
            Complex[] polePairs = new Complex[]
            {
                new Complex(-Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0),
            };

            return new AnalogFilterPrototype(polePairs);
        }

        public static AnalogFilterPrototype Create2ndOrderLinkwitzRileyFilter()
        {
            Complex[] polePairs = new Complex[]
            {
                new Complex(-1, 0),
            };

            return new AnalogFilterPrototype(polePairs);
        }
        #endregion

        #region 三阶滤波器
        public static AnalogFilterPrototype Create3rdOrderBesselFilter()
        {
            //H(s) = 15 / (s^3, 6 * s^2, 15 * s + 15);

            //double a = 1;
            //double b = 6;
            ////double c = 15;
            ////double d = 15;
            //double b_3a = b / (3 * a);
            //double f_p = 3;     // (3ac-b^2)/(3a^2) 
            //double f_q = 1;     // (27a^2*d - 9abc + 2*b^3) / (27a^3) 

            //double delta = (f_q / 2) * (f_q / 2) + (f_p / 3) * (f_p / 3) * (f_p / 3);
            //Complex w = new Complex(-0.5, Math.Sqrt(3) / 2);
            //Complex w2 = new Complex(-0.5, -Math.Sqrt(3) / 2);
            //double t1 = -f_q / 2 + Math.Sqrt(delta);
            //double t2 = -f_q / 2 - Math.Sqrt(delta);

            //Complex y1 = w * Math.Pow(t1, 1.0 / 3) - w2 * Math.Pow(-t2, 1.0 / 3);
            //double y2 = Math.Pow(t1, 1.0 / 3) - Math.Pow(-t2, 1.0 / 3);
            //Complex y3 = w2 * Math.Pow(t1, 1.0 / 3) - w * Math.Pow(-t2, 1.0 / 3);

            //Complex x1 = y1 - b_3a;
            //double x2 = y2 - b_3a;
            //Complex x3 = y3 - b_3a;

            //double ratio = Math.Pow(15, 1.0 / 3.0);

            //x1 = x1 / ratio;
            //x2 = x2 / ratio;
            //x3 = x3 / ratio;

            ////Complex[] p = new Complex[order]
            ////{
            ////    x1,     //new Complex(-0.7456403858480766441810907, 0.7113666249728352680992154),
            ////    new Complex(x2, 0.0),   //new Complex(-0.9416000265332067855971980, 0.0),
            ////    x3,     //new Complex(-0.7456403858480766441810907, -0.7113666249728352680992154),
            ////};

            //double singlePoleReal = x2;

            //PolePair[] polePairs = new PolePair[]
            //{
            //    new PolePair(x1.Real, x1.Imaginary),
            //};

            //return new CascadedStagesFilter(singlePoleReal, polePairs);

            Complex[] polePairs = new Complex[]
            {
                new Complex(-1.0474091610089349, 0.9992644362806367),
            };

            return new AnalogFilterPrototype(-1.3226757999104441, polePairs, 2.7717932746063267);
        }

        public static AnalogFilterPrototype Create3rdOrderButterworthFilter()
        {
            double singlePoleReal = -1;

            Complex[] polePairs = new Complex[]
            {
                new Complex(-0.5, Math.Sqrt(3.0) / 2.0),
            };

            return new AnalogFilterPrototype(singlePoleReal, polePairs);
        }
        #endregion

        #region 四阶滤波器
        private const int Order4th = 4;

        public static AnalogFilterPrototype Create4thOrderBesselFilter()
        {
            //PolePair[] polePairs = new PolePair[]
            //{
            //    new PolePair(-0.6572111716718829545787788, 0.8301614350048733772399715),
            //    new PolePair(-0.9047587967882449459642624, 0.2709187330038746636700926),
            //};

            //return new CascadedStagesFilter(polePairs);

            Complex[] polePairs = new Complex[]
            {
                new Complex(-0.995208764350273, 1.2571057394546654),
                new Complex(-1.370067830551444, 0.41024971749375194),
            };

            return new AnalogFilterPrototype(polePairs, 5.258199010244166);
        }

        public static AnalogFilterPrototype Create4thOrderButterworthFilter()
        {
            Complex[] polePairs = new Complex[]
            {
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * Order4th)), Math.Sin(Math.PI * 3.0 / (2 * Order4th))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * Order4th)), Math.Sin(Math.PI * 1.0 / (2 * Order4th))),
            };

            return new AnalogFilterPrototype(polePairs);
        }

        public static AnalogFilterPrototype Create4thOrderLinkwitzRileyFilter()
        {
            Complex[] polePairs = new Complex[]
            {
                new Complex(-Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0),
                new Complex(-Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0),
            };

            return new AnalogFilterPrototype(polePairs);
        }
        #endregion
        
        #region 六阶滤波器
        private const int Order6th = 6;

        public static AnalogFilterPrototype Create6thOrderBesselFilter()
        {
            //PolePair[] polePairs = new PolePair[]
            //{
            //    new PolePair(-0.5385526816693109, 0.9616876881954276),
            //    new PolePair(-0.799654185832829, 0.5621717346937318),
            //    new PolePair(-0.909390683047227, 0.18569643967930463),
            //};

            //return new CascadedStagesFilter(polePairs);

            Complex[] polePairs = new Complex[]
            {
                new Complex(-0.9306565229468584, 1.6618632689425903),
                new Complex(-1.3818580975965635, 0.9714718907115708),
                new Complex(-1.5714904036160302, 0.3208963742226236),
            };

            return new AnalogFilterPrototype(polePairs, 26.62976888914561);
        }

        public static AnalogFilterPrototype Create6thOrderButterworthFilter()
        {
            Complex[] polePairs = new Complex[]
            {
                new Complex(-Math.Cos(Math.PI * 5.0 / (2 * Order6th)), Math.Sin(Math.PI * 5.0 / (2 * Order6th))),
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * Order6th)), Math.Sin(Math.PI * 3.0 / (2 * Order6th))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * Order6th)), Math.Sin(Math.PI * 1.0 / (2 * Order6th))),
            };

            return new AnalogFilterPrototype(polePairs);
        }

        public static AnalogFilterPrototype Create6thOrderLinkwitzRileyFilter()
        {
            Complex[] polePairs = new Complex[]
            {
                new Complex(-0.5, Math.Sqrt(3.0) / 2.0),
                new Complex(-1, 0),
                new Complex(-0.5, Math.Sqrt(3.0) / 2.0),
            };

            return new AnalogFilterPrototype(polePairs);
        }
        #endregion

        #region 八阶滤波器
        private const int Order8th = 8;

        public static AnalogFilterPrototype Create8thOrderBesselFilter()
        {
            //PolePair[] polePairs = new PolePair[]
            //{
            //    new PolePair(-0.4621740412532122027072175, 1.034388681126901058116589),
            //    new PolePair(-0.7111381808485399250796172, 0.7186517314108401705762571),
            //    new PolePair(-0.8473250802359334320103023, 0.4259017538272934994996429),
            //    new PolePair(-0.9096831546652910216327629, 0.1412437976671422927888150),
            //};

            //return new CascadedStagesFilter(polePairs);

            Complex[] polePairs = new Complex[]
            {
                new Complex(-0.8928697188471373, 1.9983258436413063),
                new Complex(-1.3738412176373767, 1.3883565758775627),
                new Complex(-1.6369394181268884, 0.8227956251396996),
                new Complex(-1.757408400401653, 0.27286757510223264),
            };

            return new AnalogFilterPrototype(polePairs, 194.02619330333525);
        }

        public static AnalogFilterPrototype Create8thOrderButterworthFilter()
        {
            Complex[] polePairs = new Complex[]
            {
                new Complex(-Math.Cos(Math.PI * 7.0 / (2 * Order8th)), Math.Sin(Math.PI * 7.0 / (2 * Order8th))),
                new Complex(-Math.Cos(Math.PI * 5.0 / (2 * Order8th)), Math.Sin(Math.PI * 5.0 / (2 * Order8th))),
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * Order8th)), Math.Sin(Math.PI * 3.0 / (2 * Order8th))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * Order8th)), Math.Sin(Math.PI * 1.0 / (2 * Order8th))),
            };

            return new AnalogFilterPrototype(polePairs);
        }

        public static AnalogFilterPrototype Create8thOrderLinkwitzRileyFilter()
        {
            Complex[] polePairs = new Complex[]
            {
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * Order4th)), Math.Sin(Math.PI * 3.0 / (2 * Order4th))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * Order4th)), Math.Sin(Math.PI * 1.0 / (2 * Order4th))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * Order4th)), Math.Sin(Math.PI * 1.0 / (2 * Order4th))),
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * Order4th)), Math.Sin(Math.PI * 3.0 / (2 * Order4th))),
            };

            return new AnalogFilterPrototype(polePairs);
        }
        #endregion
    }
}
