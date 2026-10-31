using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.FFTW;

namespace AEC.RTA.Core
{
    public class Smoother
    {
        public enum SmoothPoint
        {
            None,
            Point3,
            Point5,
            Point7,
            Point9
        }
    }

    public class MagSmoother : Smoother
    {
        public int length { get; private set; }
        public SmoothPoint smoothPoint { get; private set; }
        private SignalMag buffer = null;

        public static MagSmoother CreateMagSmoother(int length, SmoothPoint smoothPoint)
        {
            return new MagSmoother(length, smoothPoint);
        }

        public MagSmoother(int dataLength, SmoothPoint smoothPt)
        {
            length = dataLength;
            smoothPoint = smoothPt;
            if (smoothPoint != SmoothPoint.None)
            {
                buffer = new SignalMag(dataLength);
            }
        }

        public void Execute(double[] mags)
        {
            System.Diagnostics.Debug.Assert(mags.Length >= length);

            switch (smoothPoint)
            {
                case SmoothPoint.None:
                    return;

                case SmoothPoint.Point3:
                    LinearSmooth3(mags, buffer.mags, length);
                    break;

                case SmoothPoint.Point5:
                    LinearSmooth5(mags, buffer.mags, length);
                    break;

                case SmoothPoint.Point7:
                    LinearSmooth7(mags, buffer.mags, length);
                    break;

                case SmoothPoint.Point9:
                    LinearSmooth9(mags, buffer.mags, length);
                    break;
            }

            Buffer.BlockCopy(buffer.mags, 0, mags, 0, length * sizeof(double));
        }

        //3点线性平滑
        private static void LinearSmooth3(double[] in_data, double[] out_data, int N)
        {
            out_data[0] = (5.0 * in_data[0] + 2.0 * in_data[1] - in_data[2]) / 6.0;
            for (int i = 1; i <= N - 2; i++)
            {
                out_data[i] = (in_data[i - 1] + in_data[i] + in_data[i + 1]) / 3.0;
            }
            out_data[N - 1] = (5.0 * in_data[N - 1] + 2.0 * in_data[N - 2] - in_data[N - 3]) / 6.0;
        }

        //5点线性平滑
        private static void LinearSmooth5(double[] in_data, double[] out_data, int N)
        {
            out_data[0] = (3.0 * in_data[0] + 2.0 * in_data[1] + in_data[2] - in_data[4]) / 5.0;
            out_data[1] = (4.0 * in_data[0] + 3.0 * in_data[1] + 2 * in_data[2] + in_data[3]) / 10.0;
            for (int i = 2; i <= N - 3; i++)
            {
                out_data[i] = (in_data[i - 2] + in_data[i - 1] + in_data[i] + in_data[i + 1] + in_data[i + 2]) / 5.0;
            }
            out_data[N - 2] = (4.0 * in_data[N - 1] + 3.0 * in_data[N - 2] + 2 * in_data[N - 3] + in_data[N - 4]) / 10.0;
            out_data[N - 1] = (3.0 * in_data[N - 1] + 2.0 * in_data[N - 2] + in_data[N - 3] - in_data[N - 5]) / 5.0;
        }

        //7点线性平滑
        private static void LinearSmooth7(double[] in_data, double[] out_data, int N)
        {
            out_data[0] = (13.0 * in_data[0] + 10.0 * in_data[1] + 7.0 * in_data[2] + 4.0 * in_data[3] +
                  in_data[4] - 2.0 * in_data[5] - 5.0 * in_data[6]) / 28.0;

            out_data[1] = (5.0 * in_data[0] + 4.0 * in_data[1] + 3 * in_data[2] + 2 * in_data[3] +
                        in_data[4] - in_data[6]) / 14.0;

            out_data[2] = (7.0 * in_data[0] + 6.0 * in_data[1] + 5.0 * in_data[2] + 4.0 * in_data[3] +
                        3.0 * in_data[4] + 2.0 * in_data[5] + in_data[6]) / 28.0;

            for (int i = 3; i <= N - 4; i++)
            {
                out_data[i] = (in_data[i - 3] + in_data[i - 2] + in_data[i - 1] + in_data[i] + in_data[i + 1] + in_data[i + 2] + in_data[i + 3]) / 7.0;
            }

            out_data[N - 3] = (7.0 * in_data[N - 1] + 6.0 * in_data[N - 2] + 5.0 * in_data[N - 3] +
                            4.0 * in_data[N - 4] + 3.0 * in_data[N - 5] + 2.0 * in_data[N - 6] + in_data[N - 7]) / 28.0;

            out_data[N - 2] = (5.0 * in_data[N - 1] + 4.0 * in_data[N - 2] + 3.0 * in_data[N - 3] +
                            2.0 * in_data[N - 4] + in_data[N - 5] - in_data[N - 7]) / 14.0;

            out_data[N - 1] = (13.0 * in_data[N - 1] + 10.0 * in_data[N - 2] + 7.0 * in_data[N - 3] +
                            4 * in_data[N - 4] + in_data[N - 5] - 2 * in_data[N - 6] - 5 * in_data[N - 7]) / 28.0;
        }

        //9点线性平滑
        private static void LinearSmooth9(double[] in_data, double[] out_data, int N)
        {
            out_data[0] = (17.0 * in_data[0] + 14.0 * in_data[1] + 11.0 * in_data[2] + 8.0 * in_data[3] + 5.0 * in_data[4] +
                2.0 * in_data[5] - in_data[6] - 4.0 * in_data[7] - 7.0 * in_data[8]) / 45.0;

            out_data[1] = (56.0 * in_data[0] + 47.0 * in_data[1] + 38.0 * in_data[2] + 29.0 * in_data[3] + 20.0 * in_data[4] +
                11.0 * in_data[5] + 2.0 * in_data[6] - 7.0 * in_data[7] - 16 * in_data[8]) / 180.0;

            out_data[2] = (22.0 * in_data[0] + 19.0 * in_data[1] + 16.0 * in_data[2] + 13.0 * in_data[3] + 10.0 * in_data[4] +
                        7.0 * in_data[5] + 4.0 * in_data[6] + in_data[7] - 2.0 * in_data[8]) / 90.0;

            out_data[3] = (32.0 * in_data[0] + 29.0 * in_data[1] + 26.0 * in_data[2] + 23.0 * in_data[3] + 20.0 * in_data[4] +
                       17.0 * in_data[5] + 14.0 * in_data[6] + 11.0 * in_data[7] + 8.0 * in_data[8]) / 180.0;


            for (int i = 4; i <= N - 5; i++)
            {
                out_data[i] = (in_data[i - 4] + in_data[i - 3] + in_data[i - 2] + in_data[i - 1] + in_data[i] + in_data[i + 1] + in_data[i + 2] + in_data[i + 3] + in_data[i + 4]) / 9.0;
            }

            out_data[N - 4] = (8.0 * in_data[N - 1] + 11.0 * in_data[N - 2] + 14.0 * in_data[N - 3] + 17.0 * in_data[N - 4] + 20.0 * in_data[N - 5] +
                       23.0 * in_data[N - 6] + 26.0 * in_data[N - 7] + 29.0 * in_data[N - 8] + 32.0 * in_data[N - 9]) / 180.0;

            out_data[N - 3] = (-2.0 * in_data[N - 1] + in_data[N - 2] + 4.0 * in_data[N - 3] + 7.0 * in_data[N - 4] + 10.0 * in_data[N - 5] +
                       13.0 * in_data[N - 6] + 16.0 * in_data[N - 7] + 19.0 * in_data[N - 8] + 22.0 * in_data[N - 9]) / 90.0;

            out_data[N - 2] = (-16.0 * in_data[N - 1] - 7.0 * in_data[N - 2] + 2.0 * in_data[N - 3] + 11.0 * in_data[N - 4] + 20.0 * in_data[N - 5] +
                       29.0 * in_data[N - 6] + 38.0 * in_data[N - 7] + 47.0 * in_data[N - 8] + 56.0 * in_data[N - 9]) / 180.0;

            out_data[N - 1] = (-7.0 * in_data[N - 1] - 4.0 * in_data[N - 2] - 1.0 * in_data[N - 3] + 2.0 * in_data[N - 4] + 5.0 * in_data[N - 5] +
                       8.0 * in_data[N - 6] + 11.0 * in_data[N - 7] + 14.0 * in_data[N - 8] + 17.0 * in_data[N - 9]) / 45.0;
        }
    }

    public class AngleSmoother : Smoother
    {
        public int length { get; private set; }
        public SmoothPoint smoothPoint { get; private set; }
        private SignalMag buffer = null;

        public static AngleSmoother CreateAngleSmoother(int length, SmoothPoint smoothPoint)
        {
            return new AngleSmoother(length, smoothPoint);
        }

        public AngleSmoother(int dataLength, SmoothPoint smoothPt)
        {
            length = dataLength;
            smoothPoint = smoothPt;
            if (smoothPoint != SmoothPoint.None)
            {
                buffer = new SignalMag(dataLength);
            }
        }

        public void Execute(double[] angles)
        {
            System.Diagnostics.Debug.Assert(angles.Length >= length);

            switch (smoothPoint)
            {
                case SmoothPoint.None:
                    return;

                case SmoothPoint.Point3:
                    LinearSmooth3(angles, buffer.mags, length);
                    break;

                case SmoothPoint.Point5:
                    LinearSmooth5(angles, buffer.mags, length);
                    break;

                case SmoothPoint.Point7:
                    LinearSmooth7(angles, buffer.mags, length);
                    break;

                case SmoothPoint.Point9:
                    LinearSmooth9(angles, buffer.mags, length);
                    break;
            }

            Buffer.BlockCopy(buffer.mags, 0, angles, 0, length * sizeof(double));
        }

        //3点线性平滑
        private static void LinearSmooth3(double[] in_data, double[] out_data, int N)
        {
            out_data[0] = in_data[0];
            for (int i = 1; i <= N - 2; i++)
            {
                out_data[i] = AverageAngle(in_data, i - 1, i + 1);
            }
            out_data[N - 1] = in_data[N - 1];
        }

        //5点线性平滑
        private static void LinearSmooth5(double[] in_data, double[] out_data, int N)
        {
            out_data[0] = in_data[0];
            out_data[1] = in_data[1];
            for (int i = 2; i <= N - 3; i++)
            {
                out_data[i] = AverageAngle(in_data, i - 2, i + 2);
            }
            out_data[N - 2] = in_data[N - 2];
            out_data[N - 1] = in_data[N - 1];
        }

        //7点线性平滑
        private static void LinearSmooth7(double[] in_data, double[] out_data, int N)
        {
            out_data[0] = in_data[0];
            out_data[1] = in_data[1];
            out_data[2] = in_data[2];
            for (int i = 3; i <= N - 4; i++)
            {
                out_data[i] = AverageAngle(in_data, i - 3, i + 3);
            }
            out_data[N - 3] = in_data[N - 3];
            out_data[N - 2] = in_data[N - 2];
            out_data[N - 1] = in_data[N - 1];
        }

        //9点线性平滑
        private static void LinearSmooth9(double[] in_data, double[] out_data, int N)
        {
            out_data[0] = in_data[0];
            out_data[1] = in_data[1];
            out_data[2] = in_data[2];
            out_data[3] = in_data[3];
            for (int i = 4; i <= N - 5; i++)
            {
                out_data[i] = AverageAngle(in_data, i - 4, i + 4);
            }
            out_data[N - 4] = in_data[N - 4];
            out_data[N - 3] = in_data[N - 3];
            out_data[N - 2] = in_data[N - 2];
            out_data[N - 1] = in_data[N - 1];
        }

        public static double AverageAngle(double[] data, int startIndex, int endIndex)
        {
            System.Diagnostics.Debug.Assert(data != null && startIndex >= 0 && endIndex > startIndex && endIndex < data.Length);
            double diff = 0;
            double last = data[startIndex];
            double sum = data[startIndex];

            for (int i = startIndex + 1; i <= endIndex; ++i)
            {
                diff = data[i] - data[i - 1] + 180;

                if (diff < 0)
                {
                    diff += 360 - 180;
                }
                else if (diff > 360)
                {
                    diff += -360 - 180;
                }
                else
                {
                    diff += -180;
                }

                last += diff;
                sum += last;
            }

            int count = endIndex - startIndex + 1;

            double avg = sum / count;
            if (avg >= 180)
            {
                avg = avg - Math.Ceiling((avg - 180) / 360.0) * 360;
            }
            else if (avg < -180)
            {
                avg = avg - Math.Floor((avg + 180) / 360.0) * 360;
            }
            return avg;
        }
    }

    public class ComplexSmoother : Smoother
    {
        public int length { get; private set; }
        public SmoothPoint smoothPoint { get; private set; }
        private FreqDomainSignal buffer = null;

        public static ComplexSmoother CreateComplexSmoother(int length, SmoothPoint smoothPoint)
        {
            return new ComplexSmoother(length, smoothPoint);
        }

        public ComplexSmoother(int dataLength, SmoothPoint smoothPt)
        {
            length = dataLength;
            smoothPoint = smoothPt;
            if (smoothPoint != SmoothPoint.None)
            {
                buffer = new FreqDomainSignal(dataLength);
            }
        }

        public void Execute(Complex[] signals)
        {
            System.Diagnostics.Debug.Assert(signals.Length == length);

            switch (smoothPoint)
            {
                case SmoothPoint.None:
                    return;

                case SmoothPoint.Point3:
                    LinearSmooth3(signals, buffer.signals, length);
                    break;

                case SmoothPoint.Point5:
                    LinearSmooth5(signals, buffer.signals, length);
                    break;

                case SmoothPoint.Point7:
                    LinearSmooth7(signals, buffer.signals, length);
                    break;

                case SmoothPoint.Point9:
                    LinearSmooth9(signals, buffer.signals, length);
                    break;
            }

            Array.Copy(buffer.signals, signals, length);
            //Buffer.BlockCopy(buffer.signals, 0, signals, 0, length * sizeof(Complex));
        }

        //3点线性平滑
        private static void LinearSmooth3(Complex[] in_data, Complex[] out_data, int N)
        {
            out_data[0] = (5.0 * in_data[0] + 2.0 * in_data[1] - in_data[2]) / 6.0;
            for (int i = 1; i <= N - 2; i++)
            {
                out_data[i] = (in_data[i - 1] + in_data[i] + in_data[i + 1]) / 3.0;
            }
            out_data[N - 1] = (5.0 * in_data[N - 1] + 2.0 * in_data[N - 2] - in_data[N - 3]) / 6.0;
        }

        //5点线性平滑
        private static void LinearSmooth5(Complex[] in_data, Complex[] out_data, int N)
        {
            out_data[0] = (3.0 * in_data[0] + 2.0 * in_data[1] + in_data[2] - in_data[4]) / 5.0;
            out_data[1] = (4.0 * in_data[0] + 3.0 * in_data[1] + 2 * in_data[2] + in_data[3]) / 10.0;
            for (int i = 2; i <= N - 3; i++)
            {
                out_data[i] = (in_data[i - 2] + in_data[i - 1] + in_data[i] + in_data[i + 1] + in_data[i + 2]) / 5.0;
            }
            out_data[N - 2] = (4.0 * in_data[N - 1] + 3.0 * in_data[N - 2] + 2 * in_data[N - 3] + in_data[N - 4]) / 10.0;
            out_data[N - 1] = (3.0 * in_data[N - 1] + 2.0 * in_data[N - 2] + in_data[N - 3] - in_data[N - 5]) / 5.0;
        }

        //7点线性平滑
        private static void LinearSmooth7(Complex[] in_data, Complex[] out_data, int N)
        {
            out_data[0] = (13.0 * in_data[0] + 10.0 * in_data[1] + 7.0 * in_data[2] + 4.0 * in_data[3] +
                  in_data[4] - 2.0 * in_data[5] - 5.0 * in_data[6]) / 28.0;

            out_data[1] = (5.0 * in_data[0] + 4.0 * in_data[1] + 3 * in_data[2] + 2 * in_data[3] +
                        in_data[4] - in_data[6]) / 14.0;

            out_data[2] = (7.0 * in_data[0] + 6.0 * in_data[1] + 5.0 * in_data[2] + 4.0 * in_data[3] +
                        3.0 * in_data[4] + 2.0 * in_data[5] + in_data[6]) / 28.0;

            for (int i = 3; i <= N - 4; i++)
            {
                out_data[i] = (in_data[i - 3] + in_data[i - 2] + in_data[i - 1] + in_data[i] + in_data[i + 1] + in_data[i + 2] + in_data[i + 3]) / 7.0;
            }

            out_data[N - 3] = (7.0 * in_data[N - 1] + 6.0 * in_data[N - 2] + 5.0 * in_data[N - 3] +
                            4.0 * in_data[N - 4] + 3.0 * in_data[N - 5] + 2.0 * in_data[N - 6] + in_data[N - 7]) / 28.0;

            out_data[N - 2] = (5.0 * in_data[N - 1] + 4.0 * in_data[N - 2] + 3.0 * in_data[N - 3] +
                            2.0 * in_data[N - 4] + in_data[N - 5] - in_data[N - 7]) / 14.0;

            out_data[N - 1] = (13.0 * in_data[N - 1] + 10.0 * in_data[N - 2] + 7.0 * in_data[N - 3] +
                            4 * in_data[N - 4] + in_data[N - 5] - 2 * in_data[N - 6] - 5 * in_data[N - 7]) / 28.0;
        }

        //9点线性平滑
        private static void LinearSmooth9(Complex[] in_data, Complex[] out_data, int N)
        {
            out_data[0] = (17.0 * in_data[0] + 14.0 * in_data[1] + 11.0 * in_data[2] + 8.0 * in_data[3] + 5.0 * in_data[4] +
                2.0 * in_data[5] - in_data[6] - 4.0 * in_data[7] - 7.0 * in_data[8]) / 45.0;

            out_data[1] = (56.0 * in_data[0] + 47.0 * in_data[1] + 38.0 * in_data[2] + 29.0 * in_data[3] + 20.0 * in_data[4] +
                11.0 * in_data[5] + 2.0 * in_data[6] - 7.0 * in_data[7] - 16 * in_data[8]) / 180.0;

            out_data[2] = (22.0 * in_data[0] + 19.0 * in_data[1] + 16.0 * in_data[2] + 13.0 * in_data[3] + 10.0 * in_data[4] +
                        7.0 * in_data[5] + 4.0 * in_data[6] + in_data[7] - 2.0 * in_data[8]) / 90.0;

            out_data[3] = (32.0 * in_data[0] + 29.0 * in_data[1] + 26.0 * in_data[2] + 23.0 * in_data[3] + 20.0 * in_data[4] +
                       17.0 * in_data[5] + 14.0 * in_data[6] + 11.0 * in_data[7] + 8.0 * in_data[8]) / 180.0;


            for (int i = 4; i <= N - 5; i++)
            {
                out_data[i] = (in_data[i - 4] + in_data[i - 3] + in_data[i - 2] + in_data[i - 1] + in_data[i] + in_data[i + 1] + in_data[i + 2] + in_data[i + 3] + in_data[i + 4]) / 9.0;
            }

            out_data[N - 4] = (8.0 * in_data[N - 1] + 11.0 * in_data[N - 2] + 14.0 * in_data[N - 3] + 17.0 * in_data[N - 4] + 20.0 * in_data[N - 5] +
                       23.0 * in_data[N - 6] + 26.0 * in_data[N - 7] + 29.0 * in_data[N - 8] + 32.0 * in_data[N - 9]) / 180.0;

            out_data[N - 3] = (-2.0 * in_data[N - 1] + in_data[N - 2] + 4.0 * in_data[N - 3] + 7.0 * in_data[N - 4] + 10.0 * in_data[N - 5] +
                       13.0 * in_data[N - 6] + 16.0 * in_data[N - 7] + 19.0 * in_data[N - 8] + 22.0 * in_data[N - 9]) / 90.0;

            out_data[N - 2] = (-16.0 * in_data[N - 1] - 7.0 * in_data[N - 2] + 2.0 * in_data[N - 3] + 11.0 * in_data[N - 4] + 20.0 * in_data[N - 5] +
                       29.0 * in_data[N - 6] + 38.0 * in_data[N - 7] + 47.0 * in_data[N - 8] + 56.0 * in_data[N - 9]) / 180.0;

            out_data[N - 1] = (-7.0 * in_data[N - 1] - 4.0 * in_data[N - 2] - 1.0 * in_data[N - 3] + 2.0 * in_data[N - 4] + 5.0 * in_data[N - 5] +
                       8.0 * in_data[N - 6] + 11.0 * in_data[N - 7] + 14.0 * in_data[N - 8] + 17.0 * in_data[N - 9]) / 45.0;
        }
    }
}
