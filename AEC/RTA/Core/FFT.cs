using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.FFTW;

namespace AEC.RTA.Core
{
    public abstract class FFTBase
    {
        protected fftw_array fft_input = null;
        protected fftw_complexarray fft_output = null;
        protected fftw_plan fft_plan = null;
        protected Complex[] result = null;

        public abstract Complex[] Execute(double[] input);
    }

    public class FFT : FFTBase
    {
        public FFT(int fftSize)
        {
            this.fft_input = new fftw_array(fftSize);
            this.fft_output = new fftw_complexarray(fftSize);
            this.fft_plan = fftw_plan.dft_r2c_1d(fftSize, fft_input, fft_output, fftw_flags.Estimate);
            this.result = new Complex[fftSize];
        }

        public override Complex[] Execute(double[] input)
        {
            this.fft_input.SetData(input);
            this.fft_plan.Execute();
            this.fft_output.GetComplexs(result);
            return result;
        }
    }

    public abstract class IFFTBase
    {
        protected fftw_complexarray ifft_input = null;    //逆变换输入
        protected fftw_complexarray ifft_output = null;   //逆变换输出
        protected fftw_plan ifft_plan = null;             //逆变换
        protected double[] result = null;

        public abstract double[] Execute(Complex[] input);
    }

    public class IFFT : IFFTBase
    {
        public IFFT(int ifftSize)
        {
            this.ifft_input = new fftw_complexarray(ifftSize);
            this.ifft_output = new fftw_complexarray(ifftSize);
            this.ifft_plan = fftw_plan.dft_1d(ifftSize, ifft_input, ifft_output, fftw_direction.Backward, fftw_flags.Estimate);
            this.result = new double[ifftSize];
        }

        public override double[] Execute(Complex[] input)
        {
            this.ifft_input.SetData(input);
            this.ifft_plan.Execute();
            this.ifft_output.GetData_Real(result);
            return result;
        }
    }
}
