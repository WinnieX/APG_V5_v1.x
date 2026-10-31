using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.FFTW;

namespace AEC.RTA.Core
{
    public class StereoFFTHelper
    {
        private int fft_length = 0;
        private FFT fft = null;
        private double[] buffer = null;
        private SignalWindow signalWindow = null;
        public StereoFFTResult stereoResult = null;

        public StereoFFTHelper(int length)
        {
            fft_length = length;
            fft = new FFT(length);
            buffer = new double[length];
            signalWindow = SignalWindowFactory.CreateWindowFunction(length);
            stereoResult = new StereoFFTResult(length);
        }

        public WindowFunction winFunction
        {
            get
            {
                return signalWindow.wf;
            }
            set
            {
                signalWindow.wf = value;
            }
        }

        private Complex[] fft_output = null;
        public void DoFFT(double[] left, double[] right, int offset = 0)
        {
            CopyBuffer(left, offset, buffer);
            signalWindow.SignalWindowing(buffer);
            fft_output = fft.Execute(buffer);
            Array.Copy(fft_output, stereoResult.left, fft_length / 2);

            CopyBuffer(right, offset, buffer);
            signalWindow.SignalWindowing(buffer);
            fft_output = fft.Execute(buffer);
            Array.Copy(fft_output, stereoResult.right, fft_length / 2);
        }

        public void DoFFT(StereoBuffer stereo, int offset = 0)
        {
            DoFFT(stereo.left, stereo.right, offset);
        }

        public void DoFFTNoWindow(double[] left, double[] right)
        {
            CopyBuffer(left, 0, buffer);
            fft_output = fft.Execute(buffer);
            Array.Copy(fft_output, stereoResult.left, fft_length);
            CopyBuffer(right, 0, buffer);
            fft_output = fft.Execute(buffer);
            Array.Copy(fft_output, stereoResult.right, fft_length);
        }

        private void CopyBuffer(double[] src, int offset, double[] dst)
        {
            Buffer.BlockCopy(src, sizeof(double) * offset, dst, 0, sizeof(double) * fft_length);
        }

        //public void DoFFT(StereoBuffer buffer)
        //{
        //    signalWindow.SignalWindowing(buffer.left);
        //    fft_output = fft.Execute(buffer.left);
        //    Array.Copy(fft_output, stereoResult.left, fft_length / 2);

        //    signalWindow.SignalWindowing(buffer.right);
        //    fft_output = fft.Execute(buffer.right);
        //    Array.Copy(fft_output, stereoResult.right, fft_length / 2);
        //}
    }
}
