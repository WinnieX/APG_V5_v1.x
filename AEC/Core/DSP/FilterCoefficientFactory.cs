using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core.DSP
{
    public class FilterCoefficientFactory
    {
        public const int Order1st = 1;
        public const int Order2nd = 2;

        public static FilterCoefficient Create1stOrderFilterCoefficient(double singlePoleReal, double k, XPFilterType type, double fc, double sr)
        {
            System.Diagnostics.Debug.Assert(type == XPFilterType.HighPass || type == XPFilterType.LowPass);

            double wn = 2 * fc / sr;

            Complex p = new Complex(singlePoleReal, 0);

            //double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = k * Math.Pow(w0, Order1st);
                p = w0 * p;
            }
            else if (type == XPFilterType.HighPass)
            {
                //Cancel out gain change caused by inversion
                //k = k * real(prod(-z) / prod(-p))
                Complex prod_negativeZ = new Complex(1.0, 0);
                Complex prod_negativaP = -p;
                k = k * (prod_negativeZ / prod_negativaP).Real;
                p = w0 / p;
            }
            else
            {
                throw new Exception("FilterCoefficientFactory.Create1stOrderFilterCoefficient()...filter type Error!");
            }
            //billinear_zpk
            double fs2 = 2 * fs;

            Complex z_z = new Complex();
            Complex p_z = new Complex();
            double k_z = 0.0;

            if (type == XPFilterType.HighPass)
            {
                Complex z = new Complex();

                z_z = (fs2 + z) / (fs2 - z);
                p_z = (fs2 + p) / (fs2 - p);
                k_z = k * ((fs2 - z) / (fs2 - p)).Real;
            }
            else
            {
                z_z.Real = -1;
                p_z = (fs2 + p) / (fs2 - p);
                k_z = k * (1.0 / (fs2 - p)).Real;
            }

            //zpk2tf
            FilterCoefficient coef = new FilterCoefficient(Order1st);
            coef.b[0] = k_z;
            coef.b[1] = -k_z * z_z.Real;

            coef.a[0] = 1.0;
            coef.a[1] = -p_z.Real;

            return coef;
        }

        public static FilterCoefficient Create2ndOrderFilterCoefficient(Complex polePair, double k, XPFilterType type, double fc, double sr)
        {
            System.Diagnostics.Debug.Assert(type == XPFilterType.HighPass || type == XPFilterType.LowPass);

            double wn = 2 * fc / sr;
            
            Complex[] z = null;
            Complex[] p = new Complex[Order2nd]
            {
                new Complex(polePair.Real, polePair.Imaginary),
                new Complex(polePair.Real, -polePair.Imaginary),
            };

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                //# Each shifted pole decreases gain by wo, each shifted zero increases it.
                //# Cancel out the net change to keep overall gain the same
                k = k * Math.Pow(w0, Order2nd);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
            }
            else if (type == XPFilterType.HighPass)
            {
                //Cancel out gain change caused by inversion
                //k = k * real(prod(-z) / prod(-p))
                Complex prod_negativeZ = new Complex(1.0, 0);
                Complex prod_negativaP = p[0] * p[1];
                k = k * (prod_negativeZ / prod_negativaP).Real;
                z = new Complex[Order2nd];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
            }
            else
            {
                throw new Exception("FilterCoefficientFactory.Create2ndOrderFilterCoefficient()...filter type Error!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[Order2nd];
            Complex[] p_z = new Complex[Order2nd];
            double k_z = 0.0;

            if (z != null)
            {
                z_z[0] = (fs2 + z[0]) / (fs2 - z[0]);
                z_z[1] = (fs2 + z[1]) / (fs2 - z[1]);
                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                k_z = k * ((fs2 - z[0]) * (fs2 - z[1]) / ((fs2 - p[0]) * (fs2 - p[1]))).Real;
            }
            else
            {
                z_z[0].Real = -1;
                z_z[1].Real = -1;
                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                k_z = k * (1.0 / ((fs2 - p[0]) * (fs2 - p[1]))).Real;
            }

            //zpk2tf
            FilterCoefficient coef = new FilterCoefficient(Order2nd);
            coef.b[0] = k_z;
            coef.b[1] = -k_z * (z_z[0].Real + z_z[1].Real);
            coef.b[2] = k_z * (z_z[0].Real * z_z[1].Real);
            coef.a[0] = 1.0;
            coef.a[1] = -2.0 * p_z[0].Real;
            coef.a[2] = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary;

            return coef;
        }
    }
}
