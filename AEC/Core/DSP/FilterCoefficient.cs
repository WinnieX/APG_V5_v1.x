using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;

namespace AEC.Core.DSP
{
    public class FilterCoefficient
    {
        public readonly int order;
        public readonly double[] a;
        public readonly double[] b;

        public FilterCoefficient(int order_)
        {
            Debug.Assert(order_ >= 1);
            this.order = order_;
            this.a = new double[order + 1];
            this.b = new double[order + 1];
        }

        public void Print()
        {
            Console.WriteLine("a = [ {0} ]", Array2String(a));
            Console.WriteLine("b = [ {0} ]", Array2String(b));
        }

        private static String Array2String(double[] arr)
        {
            String str = String.Empty;
            for (int i = 0; i < arr.Length; ++i)
            {
                if (i != arr.Length - 1)
                {
                    str += arr[i].ToString("F20") + ", ";
                }
                else
                {
                    str += arr[i].ToString("F20");
                }
            }

            return str;
        }

        public static FilterCoefficient FirstOrderButterworth(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 1;
            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                new Complex(-1, 0),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
            }
            else
            {
                throw new Exception("FilterCoefficient.FirstOrderButterworth()...filter type 错误!");
            }
            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
            double k_z = 0.0;

            if (z != null)
            {
                z_z[0] = (fs2 + z[0]) / (fs2 - z[0]);
                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                k_z = k * ((fs2 - z[0]) / (fs2 - p[0])).Real;
            }
            else
            {
                z_z[0].Real = -1;
                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                k_z = k * (1.0 / (fs2 - p[0])).Real;
            }

            //zpk2tf
            FilterCoefficient coef = new FilterCoefficient(order);
            coef.b[0] = k_z;
            coef.b[1] = -k_z * z_z[0].Real;

            coef.a[0] = 1.0;
            coef.a[1] = -p_z[0].Real;

            return coef;
        }

        public static FilterCoefficient SecondOrderBessel(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 2;
            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                new Complex(-Math.Sqrt(3) / 2.0, -0.5),
                new Complex(-Math.Sqrt(3) / 2.0, 0.5),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
            }
            else
            {
                throw new Exception("FilterCoefficient.SecondOrderBessel()...filter type 错误!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
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
            FilterCoefficient coef = new FilterCoefficient(order);
            coef.b[0] = k_z;
            coef.b[1] = -k_z * (z_z[0].Real + z_z[1].Real);
            coef.b[2] = k_z * (z_z[0].Real * z_z[1].Real);
            coef.a[0] = 1.0;
            coef.a[1] = -2.0 * p_z[0].Real;
            coef.a[2] = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary;

            return coef;
        }

        public static FilterCoefficient SecondOrderButterworth(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 2;
            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                new Complex(-Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0),
                new Complex(-Math.Sqrt(2) / 2.0, -Math.Sqrt(2) / 2.0),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
            }
            else
            {
                throw new Exception("FilterCoefficient.SecondOrderButterworth()...filter type 错误!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
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
            FilterCoefficient coef = new FilterCoefficient(order);
            coef.b[0] = k_z;
            coef.b[1] = -k_z * (z_z[0].Real + z_z[1].Real);
            coef.b[2] = k_z * (z_z[0].Real * z_z[1].Real);
            coef.a[0] = 1.0;
            coef.a[1] = -2.0 * p_z[0].Real;
            coef.a[2] = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary;

            return coef;
        }

        public static FilterCoefficient SecondOrderLinkwitzRiley(XPFilterType type, double fc, double sr)
        {
            FilterCoefficient butter1st = FirstOrderButterworth(type, fc, sr);
            const int order = 2;
            FilterCoefficient coef = new FilterCoefficient(order);
            coef.b[0] = butter1st.b[0] * butter1st.b[0];
            coef.b[1] = 2 * butter1st.b[0] * butter1st.b[1];
            coef.b[2] = butter1st.b[1] * butter1st.b[1];
            coef.a[0] = butter1st.a[0] * butter1st.a[0];
            coef.a[1] = 2 * butter1st.a[0] * butter1st.a[1];
            coef.a[2] = butter1st.a[1] * butter1st.a[1];
            return coef;
        }

        public static FilterCoefficient ThirdOrderBessel(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 3;

            //H(s) = 15 / (s^3, 6 * s^2, 15 * s + 15);

            double a = 1;
            double b = 6;
            //double c = 15;
            //double d = 15;
            double b_3a = b / (3 * a);
            double f_p = 3;     // (3ac-b^2)/(3a^2) 
            double f_q = 1;     // (27a^2*d - 9abc + 2*b^3) / (27a^3) 

            double delta = (f_q / 2) * (f_q / 2) + (f_p / 3) * (f_p / 3) * (f_p / 3);
            Complex w = new Complex(-0.5, Math.Sqrt(3) / 2);
            Complex w2 = new Complex(-0.5, -Math.Sqrt(3) / 2);
            double t1 = -f_q / 2 + Math.Sqrt(delta);
            double t2 = -f_q / 2 - Math.Sqrt(delta);

            Complex y1 = w * Math.Pow(t1, 1.0 / 3) - w2 * Math.Pow(-t2, 1.0 / 3);
            double y2 = Math.Pow(t1, 1.0 / 3) - Math.Pow(-t2, 1.0 / 3);
            Complex y3 = w2 * Math.Pow(t1, 1.0 / 3) - w * Math.Pow(-t2, 1.0 / 3);

            Complex x1 = y1 - b_3a;
            double x2 = y2 - b_3a;
            Complex x3 = y3 - b_3a;

            double ratio = Math.Pow(15, 1.0 / 3.0);

            x1 = x1 / ratio;
            x2 = x2 / ratio;
            x3 = x3 / ratio;

            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                x1,     //new Complex(-0.7456403858480766441810907, 0.7113666249728352680992154),
                new Complex(x2, 0.0),   //new Complex(-0.9416000265332067855971980, 0.0),
                x3,     //new Complex(-0.7456403858480766441810907, -0.7113666249728352680992154),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
                p[2] = w0 * p[2];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
                p[2] = w0 / p[2];
            }
            else
            {
                throw new Exception("FilterCoefficient.ThirdOrderBessel()...filter type 错误!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
            double k_z = 0.0;

            if (z != null)
            {
                z_z[0] = (fs2 + z[0]) / (fs2 - z[0]);
                z_z[1] = (fs2 + z[1]) / (fs2 - z[1]);
                z_z[2] = (fs2 + z[2]) / (fs2 - z[2]);

                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);

                k_z = k * ((fs2 - z[0]) * (fs2 - z[1]) * (fs2 - z[2]) / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]))).Real;
            }
            else
            {
                z_z[0].Real = -1;
                z_z[1].Real = -1;
                z_z[2].Real = -1;
                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                k_z = k * (1.0 / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]))).Real;
            }

            //zpk2tf
            FilterCoefficient temp = new FilterCoefficient(order);
            temp.b[0] = k_z;
            temp.b[1] = -k_z * (z_z[0].Real * order);
            temp.b[2] = k_z * (order * z_z[0].Real * z_z[0].Real);
            temp.b[3] = -k_z * z_z[0].Real * z_z[0].Real * z_z[0].Real;
            temp.a[0] = 1.0;
            temp.a[1] = -(2.0 * p_z[0].Real + p_z[1].Real);
            temp.a[2] = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary + 2 * p_z[0].Real * p_z[1].Real;
            temp.a[3] = -(p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary) * p_z[1].Real;

            return temp;
        }

        public static FilterCoefficient ThirdOrderButterworth(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 3;
            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                new Complex(-0.5, Math.Sqrt(3.0) / 2),
                new Complex(-1, 0.0),
                new Complex(-0.5, - Math.Sqrt(3.0) / 2),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
                p[2] = w0 * p[2];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
                p[2] = w0 / p[2];
            }
            else
            {
                throw new Exception("FilterCoefficient.ThirdOrderButterworth()...filter type 错误!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
            double k_z = 0.0;

            if (z != null)
            {
                z_z[0] = (fs2 + z[0]) / (fs2 - z[0]);
                z_z[1] = (fs2 + z[1]) / (fs2 - z[1]);
                z_z[2] = (fs2 + z[2]) / (fs2 - z[2]);

                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);

                k_z = k * ((fs2 - z[0]) * (fs2 - z[1]) * (fs2 - z[2]) / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]))).Real;
            }
            else
            {
                z_z[0].Real = -1;
                z_z[1].Real = -1;
                z_z[2].Real = -1;
                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                k_z = k * (1.0 / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]))).Real;
            }

            //zpk2tf
            FilterCoefficient temp = new FilterCoefficient(order);
            temp.b[0] = k_z;
            temp.b[1] = -k_z * (z_z[0].Real * order);
            temp.b[2] = k_z * (order * z_z[0].Real * z_z[0].Real);
            temp.b[3] = -k_z * z_z[0].Real * z_z[0].Real * z_z[0].Real;
            temp.a[0] = 1.0;
            temp.a[1] = -(2.0 * p_z[0].Real + p_z[1].Real);
            temp.a[2] = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary + 2 * p_z[0].Real * p_z[1].Real;
            temp.a[3] = -(p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary) * p_z[1].Real;

            return temp;
        }

        public static FilterCoefficient FourthOrderBessel(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 4;
            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                new Complex(-0.6572111716718829545787788, 0.8301614350048733772399715),
                new Complex(-0.9047587967882449459642624, 0.2709187330038746636700926),
                new Complex(-0.9047587967882449459642637, -0.2709187330038746636700923),
                new Complex(-0.6572111716718829545787788, -0.8301614350048733772399715),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
                p[2] = w0 * p[2];
                p[3] = w0 * p[3];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
                p[2] = w0 / p[2];
                p[3] = w0 / p[3];
            }
            else
            {
                throw new Exception("FilterCoefficient.FourthOrderBessel()...filter type 错误!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
            double k_z = 0.0;

            if (z != null)
            {
                z_z[0] = (fs2 + z[0]) / (fs2 - z[0]);
                z_z[1] = (fs2 + z[1]) / (fs2 - z[1]);
                z_z[2] = (fs2 + z[2]) / (fs2 - z[2]);
                z_z[3] = (fs2 + z[3]) / (fs2 - z[3]);

                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                p_z[3] = (fs2 + p[3]) / (fs2 - p[3]);

                k_z = k * ((fs2 - z[0]) * (fs2 - z[1]) * (fs2 - z[2]) * (fs2 - z[3]) / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]) * (fs2 - p[3]))).Real;
            }
            else
            {
                z_z[0].Real = -1;
                z_z[1].Real = -1;
                z_z[2].Real = -1;
                z_z[3].Real = -1;
                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                p_z[3] = (fs2 + p[3]) / (fs2 - p[3]);
                k_z = k * (1.0 / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]) * (fs2 - p[3]))).Real;
            }

            //zpk2tf
            FilterCoefficient temp = new FilterCoefficient(order);
            temp.b[0] = k_z;
            temp.b[1] = -k_z * (z_z[0].Real * 4);
            temp.b[2] = k_z * (6 * z_z[0].Real * z_z[0].Real);
            temp.b[3] = -k_z * (z_z[0].Real * z_z[0].Real * z_z[0].Real * 4);
            temp.b[4] = k_z * (z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);

            temp.a[0] = 1.0;
            temp.a[1] = -2.0 * (p_z[0].Real + p_z[1].Real);
            temp.a[2] = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary + p_z[1].Real * p_z[1].Real + p_z[1].Imaginary * p_z[1].Imaginary + 4 * p_z[0].Real * p_z[1].Real;
            temp.a[3] = -2.0 * (p_z[0].Real * (p_z[1].Real * p_z[1].Real + p_z[1].Imaginary * p_z[1].Imaginary) + p_z[1].Real * (p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary));
            temp.a[4] = (p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary) * (p_z[1].Real * p_z[1].Real + p_z[1].Imaginary * p_z[1].Imaginary);

            return temp;
        }

        public static FilterCoefficient FourthOrderButterworth(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 4;
            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * order)), Math.Sin(Math.PI * 3.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * order)), Math.Sin(Math.PI * 1.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * order)), -Math.Sin(Math.PI * 1.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * order)), -Math.Sin(Math.PI * 3.0 / (2 * order))),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
                p[2] = w0 * p[2];
                p[3] = w0 * p[3];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
                p[2] = w0 / p[2];
                p[3] = w0 / p[3];
            }
            else
            {
                throw new Exception("FilterCoefficient.FourthOrderButterworth()...filter type 错误!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
            double k_z = 0.0;

            if (z != null)
            {
                z_z[0] = (fs2 + z[0]) / (fs2 - z[0]);
                z_z[1] = (fs2 + z[1]) / (fs2 - z[1]);
                z_z[2] = (fs2 + z[2]) / (fs2 - z[2]);
                z_z[3] = (fs2 + z[3]) / (fs2 - z[3]);

                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                p_z[3] = (fs2 + p[3]) / (fs2 - p[3]);

                k_z = k * ((fs2 - z[0]) * (fs2 - z[1]) * (fs2 - z[2]) * (fs2 - z[3]) / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]) * (fs2 - p[3]))).Real;
            }
            else
            {
                z_z[0].Real = -1;
                z_z[1].Real = -1;
                z_z[2].Real = -1;
                z_z[3].Real = -1;
                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                p_z[3] = (fs2 + p[3]) / (fs2 - p[3]);
                k_z = k * (1.0 / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]) * (fs2 - p[3]))).Real;
            }

            //zpk2tf
            FilterCoefficient temp = new FilterCoefficient(order);
            temp.b[0] = k_z;
            temp.b[1] = -k_z * (z_z[0].Real * 4);
            temp.b[2] = k_z * (6 * z_z[0].Real * z_z[0].Real);
            temp.b[3] = -k_z * (z_z[0].Real * z_z[0].Real * z_z[0].Real * 4);
            temp.b[4] = k_z * (z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);

            temp.a[0] = 1.0;
            temp.a[1] = -2.0 * (p_z[0].Real + p_z[1].Real);
            temp.a[2] = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary + p_z[1].Real * p_z[1].Real + p_z[1].Imaginary * p_z[1].Imaginary + 4 * p_z[0].Real * p_z[1].Real;
            temp.a[3] = -2.0 * (p_z[0].Real * (p_z[1].Real * p_z[1].Real + p_z[1].Imaginary * p_z[1].Imaginary) + p_z[1].Real * (p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary));
            temp.a[4] = (p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary) * (p_z[1].Real * p_z[1].Real + p_z[1].Imaginary * p_z[1].Imaginary);

            return temp;
        }

        public static FilterCoefficient FourthOrderLinkwitzRiley(XPFilterType type, double fc, double sr)
        {
            FilterCoefficient butter2nd = SecondOrderButterworth(type, fc, sr);
            const int order = 4;
            FilterCoefficient coef = new FilterCoefficient(order);
            coef.b[0] = butter2nd.b[0] * butter2nd.b[0];
            coef.b[1] = 2 * butter2nd.b[0] * butter2nd.b[1];
            coef.b[2] = 2 * butter2nd.b[0] * butter2nd.b[2] + butter2nd.b[1] * butter2nd.b[1];
            coef.b[3] = 2 * butter2nd.b[1] * butter2nd.b[2];
            coef.b[4] = butter2nd.b[2] * butter2nd.b[2];

            coef.a[0] = butter2nd.a[0] * butter2nd.a[0];
            coef.a[1] = 2 * butter2nd.a[0] * butter2nd.a[1];
            coef.a[2] = 2 * butter2nd.a[0] * butter2nd.a[2] + butter2nd.a[1] * butter2nd.a[1];
            coef.a[3] = 2 * butter2nd.a[1] * butter2nd.a[2];
            coef.a[4] = butter2nd.a[2] * butter2nd.a[2];
            return coef;
        }

        public static FilterCoefficient EighthOrderBessel(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 8;
            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                new Complex(-0.4621740412532122027072175, 1.034388681126901058116589),
                new Complex(-0.7111381808485399250796172, 0.7186517314108401705762571),
                new Complex(-0.8473250802359334320103023, 0.4259017538272934994996429),
                new Complex(-0.9096831546652910216327629, 0.1412437976671422927888150),
                new Complex(-0.9096831546652910216327629, -0.1412437976671422927888150),
                new Complex(-0.8473250802359334320103023, -0.4259017538272934994996429),
                new Complex(-0.7111381808485399250796172, -0.7186517314108401705762571),
                new Complex(-0.4621740412532122027072175, -1.034388681126901058116589),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
                p[2] = w0 * p[2];
                p[3] = w0 * p[3];
                p[4] = w0 * p[4];
                p[5] = w0 * p[5];
                p[6] = w0 * p[6];
                p[7] = w0 * p[7];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
                p[2] = w0 / p[2];
                p[3] = w0 / p[3];
                p[4] = w0 / p[4];
                p[5] = w0 / p[5];
                p[6] = w0 / p[6];
                p[7] = w0 / p[7];
            }
            else
            {
                throw new Exception("FilterCoefficient.EighthOrderBessel()...filter type 错误!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
            double k_z = 0.0;

            if (z != null)
            {
                z_z[0] = (fs2 + z[0]) / (fs2 - z[0]);
                z_z[1] = (fs2 + z[1]) / (fs2 - z[1]);
                z_z[2] = (fs2 + z[2]) / (fs2 - z[2]);
                z_z[3] = (fs2 + z[3]) / (fs2 - z[3]);
                z_z[4] = (fs2 + z[4]) / (fs2 - z[4]);
                z_z[5] = (fs2 + z[5]) / (fs2 - z[5]);
                z_z[6] = (fs2 + z[6]) / (fs2 - z[6]);
                z_z[7] = (fs2 + z[7]) / (fs2 - z[7]);

                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                p_z[3] = (fs2 + p[3]) / (fs2 - p[3]);
                p_z[4] = (fs2 + p[4]) / (fs2 - p[4]);
                p_z[5] = (fs2 + p[5]) / (fs2 - p[5]);
                p_z[6] = (fs2 + p[6]) / (fs2 - p[6]);
                p_z[7] = (fs2 + p[7]) / (fs2 - p[7]);

                k_z = k * ((fs2 - z[0]) * (fs2 - z[1]) * (fs2 - z[2]) * (fs2 - z[3]) * (fs2 - z[4]) * (fs2 - z[5]) * (fs2 - z[6]) * (fs2 - z[7]) / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]) * (fs2 - p[3]) * (fs2 - p[4]) * (fs2 - p[5]) * (fs2 - p[6]) * (fs2 - p[7]))).Real;
            }
            else
            {
                z_z[0].Real = -1;
                z_z[1].Real = -1;
                z_z[2].Real = -1;
                z_z[3].Real = -1;
                z_z[4].Real = -1;
                z_z[5].Real = -1;
                z_z[6].Real = -1;
                z_z[7].Real = -1;

                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                p_z[3] = (fs2 + p[3]) / (fs2 - p[3]);
                p_z[4] = (fs2 + p[4]) / (fs2 - p[4]);
                p_z[5] = (fs2 + p[5]) / (fs2 - p[5]);
                p_z[6] = (fs2 + p[6]) / (fs2 - p[6]);
                p_z[7] = (fs2 + p[7]) / (fs2 - p[7]);

                k_z = k * (1.0 / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]) * (fs2 - p[3]) * (fs2 - p[4]) * (fs2 - p[5]) * (fs2 - p[6]) * (fs2 - p[7]))).Real;
            }

            //zpk2tf
            FilterCoefficient temp = new FilterCoefficient(order);
            temp.b[0] = k_z;
            temp.b[1] = -k_z * ( 8 * z_z[0].Real );
            temp.b[2] = k_z * (28 * z_z[0].Real * z_z[0].Real);
            temp.b[3] = -k_z * (56 * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[4] = k_z * (70 * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[5] = -k_z * (56 * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[6] = k_z * (28 * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[7] = -k_z * (8 * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[8] = k_z * (z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);

            double m1 = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary;
            double m2 = p_z[1].Real * p_z[1].Real + p_z[1].Imaginary * p_z[1].Imaginary;

            double c0 = m1 * m2;
            double c1 = -2 * (p_z[0].Real * m2 + p_z[1].Real * m1);
            double c2 = 4 * p_z[0].Real * p_z[1].Real + m1 + m2;
            double c3 = -2 * (p_z[0].Real + p_z[1].Real);

            double n1 = p_z[2].Real * p_z[2].Real + p_z[2].Imaginary * p_z[2].Imaginary;
            double n2 = p_z[3].Real * p_z[3].Real + p_z[3].Imaginary * p_z[3].Imaginary;

            double d0 = n1 * n2;
            double d1 = -2 * (p_z[2].Real * n2 + p_z[3].Real * n1);
            double d2 = 4 * p_z[2].Real * p_z[3].Real + n1 + n2;
            double d3 = -2 * (p_z[2].Real + p_z[3].Real);

            temp.a[0] = 1.0;
            temp.a[1] = d3 + c3;
            temp.a[2] = d2 + c3 * d3 + c2;
            temp.a[3] = d1 + d2 * c3 + d3 * c2 + c1;
            temp.a[4] = d0 + d1 * c3 + d2 * c2 + d3 * c1 + c0;
            temp.a[5] = d0 * c3 + d1 * c2 + d2 * c1 + d3 * c0;
            temp.a[6] = d0 * c2 + d1 * c1 + d2 * c0;
            temp.a[7] = d0 * c1 + d1 * c0;
            temp.a[8] = c0 * d0;
            
            return temp;
        }

        public static FilterCoefficient EighthOrderButterworth(XPFilterType type, double fc, double sr)
        {
            double wn = 2 * fc / sr;
            const int order = 8;
            Complex[] z = null;
            Complex[] p = new Complex[order]
            {
                new Complex(-Math.Cos(Math.PI * 7.0 / (2 * order)), Math.Sin(Math.PI * 7.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 5.0 / (2 * order)), Math.Sin(Math.PI * 5.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * order)), Math.Sin(Math.PI * 3.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * order)), Math.Sin(Math.PI * 1.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 1.0 / (2 * order)), -Math.Sin(Math.PI * 1.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 3.0 / (2 * order)), -Math.Sin(Math.PI * 3.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 5.0 / (2 * order)), -Math.Sin(Math.PI * 5.0 / (2 * order))),
                new Complex(-Math.Cos(Math.PI * 7.0 / (2 * order)), -Math.Sin(Math.PI * 7.0 / (2 * order))),
            };
            double k = 1.0;

            double fs = 2.0;
            double w0 = 2 * fs * Math.Tan(Math.PI * wn / fs);

            if (type == XPFilterType.LowPass)
            {
                //lp2lp_zpk
                k = Math.Pow(w0, order);
                p[0] = w0 * p[0];
                p[1] = w0 * p[1];
                p[2] = w0 * p[2];
                p[3] = w0 * p[3];
                p[4] = w0 * p[4];
                p[5] = w0 * p[5];
                p[6] = w0 * p[6];
                p[7] = w0 * p[7];
            }
            else if (type == XPFilterType.HighPass)
            {
                //k = k * 1.0 / (-p[0] * -p[1] * -p[2]).Real;
                k = 1.0;
                z = new Complex[order];
                p[0] = w0 / p[0];
                p[1] = w0 / p[1];
                p[2] = w0 / p[2];
                p[3] = w0 / p[3];
                p[4] = w0 / p[4];
                p[5] = w0 / p[5];
                p[6] = w0 / p[6];
                p[7] = w0 / p[7];
            }
            else
            {
                throw new Exception("FilterCoefficient.EighthOrderButterworth()...filter type 错误!");
            }

            //billinear_zpk
            double fs2 = 2 * fs;

            Complex[] z_z = new Complex[order];
            Complex[] p_z = new Complex[order];
            double k_z = 0.0;

            if (z != null)
            {
                z_z[0] = (fs2 + z[0]) / (fs2 - z[0]);
                z_z[1] = (fs2 + z[1]) / (fs2 - z[1]);
                z_z[2] = (fs2 + z[2]) / (fs2 - z[2]);
                z_z[3] = (fs2 + z[3]) / (fs2 - z[3]);
                z_z[4] = (fs2 + z[4]) / (fs2 - z[4]);
                z_z[5] = (fs2 + z[5]) / (fs2 - z[5]);
                z_z[6] = (fs2 + z[6]) / (fs2 - z[6]);
                z_z[7] = (fs2 + z[7]) / (fs2 - z[7]);

                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                p_z[3] = (fs2 + p[3]) / (fs2 - p[3]);
                p_z[4] = (fs2 + p[4]) / (fs2 - p[4]);
                p_z[5] = (fs2 + p[5]) / (fs2 - p[5]);
                p_z[6] = (fs2 + p[6]) / (fs2 - p[6]);
                p_z[7] = (fs2 + p[7]) / (fs2 - p[7]);

                k_z = k * ((fs2 - z[0]) * (fs2 - z[1]) * (fs2 - z[2]) * (fs2 - z[3]) * (fs2 - z[4]) * (fs2 - z[5]) * (fs2 - z[6]) * (fs2 - z[7]) / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]) * (fs2 - p[3]) * (fs2 - p[4]) * (fs2 - p[5]) * (fs2 - p[6]) * (fs2 - p[7]))).Real;
            }
            else
            {
                z_z[0].Real = -1;
                z_z[1].Real = -1;
                z_z[2].Real = -1;
                z_z[3].Real = -1;
                z_z[4].Real = -1;
                z_z[5].Real = -1;
                z_z[6].Real = -1;
                z_z[7].Real = -1;

                p_z[0] = (fs2 + p[0]) / (fs2 - p[0]);
                p_z[1] = (fs2 + p[1]) / (fs2 - p[1]);
                p_z[2] = (fs2 + p[2]) / (fs2 - p[2]);
                p_z[3] = (fs2 + p[3]) / (fs2 - p[3]);
                p_z[4] = (fs2 + p[4]) / (fs2 - p[4]);
                p_z[5] = (fs2 + p[5]) / (fs2 - p[5]);
                p_z[6] = (fs2 + p[6]) / (fs2 - p[6]);
                p_z[7] = (fs2 + p[7]) / (fs2 - p[7]);

                k_z = k * (1.0 / ((fs2 - p[0]) * (fs2 - p[1]) * (fs2 - p[2]) * (fs2 - p[3]) * (fs2 - p[4]) * (fs2 - p[5]) * (fs2 - p[6]) * (fs2 - p[7]))).Real;
            }

            //zpk2tf
            FilterCoefficient temp = new FilterCoefficient(order);
            temp.b[0] = k_z;
            temp.b[1] = -k_z * (8 * z_z[0].Real);
            temp.b[2] = k_z * (28 * z_z[0].Real * z_z[0].Real);
            temp.b[3] = -k_z * (56 * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[4] = k_z * (70 * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[5] = -k_z * (56 * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[6] = k_z * (28 * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[7] = -k_z * (8 * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);
            temp.b[8] = k_z * (z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real * z_z[0].Real);

            double m1 = p_z[0].Real * p_z[0].Real + p_z[0].Imaginary * p_z[0].Imaginary;
            double m2 = p_z[1].Real * p_z[1].Real + p_z[1].Imaginary * p_z[1].Imaginary;

            double c0 = m1 * m2;
            double c1 = -2 * (p_z[0].Real * m2 + p_z[1].Real * m1);
            double c2 = 4 * p_z[0].Real * p_z[1].Real + m1 + m2;
            double c3 = -2 * (p_z[0].Real + p_z[1].Real);

            double n1 = p_z[2].Real * p_z[2].Real + p_z[2].Imaginary * p_z[2].Imaginary;
            double n2 = p_z[3].Real * p_z[3].Real + p_z[3].Imaginary * p_z[3].Imaginary;

            double d0 = n1 * n2;
            double d1 = -2 * (p_z[2].Real * n2 + p_z[3].Real * n1);
            double d2 = 4 * p_z[2].Real * p_z[3].Real + n1 + n2;
            double d3 = -2 * (p_z[2].Real + p_z[3].Real);

            temp.a[0] = 1.0;
            temp.a[1] = d3 + c3;
            temp.a[2] = d2 + c3 * d3 + c2;
            temp.a[3] = d1 + d2 * c3 + d3 * c2 + c1;
            temp.a[4] = d0 + d1 * c3 + d2 * c2 + d3 * c1 + c0;
            temp.a[5] = d0 * c3 + d1 * c2 + d2 * c1 + d3 * c0;
            temp.a[6] = d0 * c2 + d1 * c1 + d2 * c0;
            temp.a[7] = d0 * c1 + d1 * c0;
            temp.a[8] = c0 * d0;

            return temp;
        }

        public static FilterCoefficient EighthOrderLinkwitzRiley(XPFilterType type, double fc, double sr)
        {
            FilterCoefficient butter4nd = FourthOrderButterworth(type, fc, sr);
            const int order = 8;
            FilterCoefficient coef = new FilterCoefficient(order);
            coef.b[0] = butter4nd.b[0] * butter4nd.b[0];
            coef.b[1] = 2 * butter4nd.b[0] * butter4nd.b[1];
            coef.b[2] = 2 * butter4nd.b[0] * butter4nd.b[2] + butter4nd.b[1] * butter4nd.b[1];
            coef.b[3] = 2 * (butter4nd.b[0] * butter4nd.b[3] + butter4nd.b[1] * butter4nd.b[2]);
            coef.b[4] = 2 * (butter4nd.b[0] * butter4nd.b[4] + butter4nd.b[1] * butter4nd.b[3]) + butter4nd.b[2] * butter4nd.b[2];
            coef.b[5] = 2 * (butter4nd.b[1] * butter4nd.b[4] + butter4nd.b[2] * butter4nd.b[3]);
            coef.b[6] = 2 * butter4nd.b[2] * butter4nd.b[4] + butter4nd.b[3] * butter4nd.b[3];
            coef.b[7] = 2 * butter4nd.b[3] * butter4nd.b[4];
            coef.b[8] = butter4nd.b[4] * butter4nd.b[4];

            coef.a[0] = butter4nd.a[0] * butter4nd.a[0];
            coef.a[1] = 2 * butter4nd.a[0] * butter4nd.a[1];
            coef.a[2] = 2 * butter4nd.a[0] * butter4nd.a[2] + butter4nd.a[1] * butter4nd.a[1];
            coef.a[3] = 2 * (butter4nd.a[0] * butter4nd.a[3] + butter4nd.a[1] * butter4nd.a[2]);
            coef.a[4] = 2 * (butter4nd.a[0] * butter4nd.a[4] + butter4nd.a[1] * butter4nd.a[3]) + butter4nd.a[2] * butter4nd.a[2];
            coef.a[5] = 2 * (butter4nd.a[1] * butter4nd.a[4] + butter4nd.a[2] * butter4nd.a[3]);
            coef.a[6] = 2 * butter4nd.a[2] * butter4nd.a[4] + butter4nd.a[3] * butter4nd.a[3];
            coef.a[7] = 2 * butter4nd.a[3] * butter4nd.a[4];
            coef.a[8] = butter4nd.a[4] * butter4nd.a[4];

            return coef;
        }
    }
}
