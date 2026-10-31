using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.Utils;
using AEC.RTA.FFTW;

namespace AEC.RTA.Core
{
    public class MeasDelay : ThreadBase
    {
        private MeasDelayAudioDataBuffer dataBuffer = null;

        private int fftSize = AudioRTAParam.FFTSize32K;
        private int maxDelayOffset = 0;
        #region fft
        private StereoFFTHelper stereoFFTHelper = null;
        private IFFT ifft = null;
        #endregion
        Complex[] products = null;

        private EventHandler MeasDelayCompletedHandler = null;

        public MeasDelay()
        {
            //Init();
        }

        public void RegisterMeasDelayCompletedHandler(EventHandler handler)
        {
            MeasDelayCompletedHandler += handler;
        }

        public void RemoveMeasDelayCompletedHandler(EventHandler handler)
        {
            MeasDelayCompletedHandler -= handler;
        }

        private void Init()
        {
            maxDelayOffset = fftSize - 1;
            ifft = new IFFT(fftSize);
            stereoFFTHelper = new StereoFFTHelper(fftSize);
            products = new Complex[fftSize];
        }

        protected override void Entrance()
        {
            Init();
        }

        protected override void Run()
        {
            if (dataBuffer == null)
            {
                return;
            }

            Prepare();

            int offset = CalcDelay();
            short delayOffset = offset < maxDelayOffset ? (short)offset : (short)maxDelayOffset;
            
            if (MeasDelayCompletedHandler != null)
            {
                MeasDelayCompletedHandler(delayOffset, EventArgs.Empty);
            }
        }

        private void Prepare()
        {
            if (dataBuffer.fftSize != fftSize)
            {
                fftSize = dataBuffer.fftSize;
                this.Init();
            }
        }

        private int CalcDelay()
        {
            int minIndex = fftSize - 1;
            for (int i = 0; i < dataBuffer.repeatTimes; ++i)
            {
                double[] leftCh = dataBuffer.GetMonoData(i, AudioChannel.LeftCh);
                double[] rightCh = dataBuffer.GetMonoData(i, AudioChannel.RightCh);

                double[] xcorr = XCorr(leftCh, rightCh);

                int index = GetAbsMaxIndex(xcorr);

                if (index < minIndex)
                {
                    minIndex = index;
                }
            }

            return minIndex;
        }

        private int GetAbsMaxIndex(double[] data)
        {
            int index = 0;
            double max = Math.Abs(data[index]);
            for (int i = index + 1; i < data.Length; ++i)
            {
                double abs = Math.Abs(data[i]);
                if (abs > max)
                {
                    index = i;
                    max = abs;
                }
            }

            //int index = 0;
            //double max = data[index];
            //for (int i = index + 1; i < data.Length; ++i)
            //{
            //    double abs = data[i];
            //    if (abs > max)
            //    {
            //        index = i;
            //        max = abs;
            //    }
            //}

            return index;
        }

        private int GetMaxIndex(double[] data)
        {
            int index = 0;
            double max = data[index];
            for (int i = index + 1; i < data.Length; ++i)
            {
                double abs = data[i];
                if (abs > max)
                {
                    index = i;
                    max = abs;
                }
            }

            return index;
        }

        private int GetMinIndex(double[] data)
        {
            int index = 0;
            double min = data[index];
            for (int i = index + 1; i < data.Length; ++i)
            {
                double abs = data[i];
                if (abs < min)
                {
                    index = i;
                    min = abs;
                }
            }

            return index;
        }

        //广义互相关-相位变换方法（Generalized Cross Correlation PHAse Transformation，GCC-PHAT）方法本身具有一定的抗噪声和抗混响能力，但是在信噪比降低和混响增强时，该算法性能急剧下降。
        private double[] XCorr(double[] x, double[] y)
        {
            //计算双通道FFT
            stereoFFTHelper.DoFFTNoWindow(x, y);
            //计算点积(共轭)
            CalcProducts();
            //相位变换加权函数,实质上是一个白化滤波器，使得信号间的互功率谱更加平滑，从而锐化广义互相关函数。
            for (int i = 0; i < products.Length; ++i)
            {
                double mag = products[i].Magnitude;
                products[i] = products[i] / (mag + 0.1);
            }

            //计算傅里叶逆变换
            double[] ifft_out = ifft.Execute(products);
            double[] xcorr = new double[ifft_out.Length];

            Array.Copy(ifft_out, xcorr, ifft_out.Length);

            return xcorr;
        }

        private void CalcProducts()
        {
            for (int i = 0; i < fftSize; ++i)
            {
                double a = stereoFFTHelper.stereoResult.left[i].Real;
                double b = stereoFFTHelper.stereoResult.left[i].Imaginary;
                double c = stereoFFTHelper.stereoResult.right[i].Real;
                double d = stereoFFTHelper.stereoResult.right[i].Imaginary;
                products[i].Real = a * c + b * d;
                products[i].Imaginary = b * c - a * d;
            }
        }

        private void InputData(MeasDelayAudioDataBuffer delayBuffer)
        {
            dataBuffer = delayBuffer;
            ExecuteOnce();
        }

        public void MeasDelayHandler(object sender, EventArgs e)
        {
            AudioBuffer buffer = sender as AudioBuffer;
            InputData(buffer.measDelayAudioDataBuffer);
        }
    }
}
