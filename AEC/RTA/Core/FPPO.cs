using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.FFTW;

namespace AEC.RTA.Core
{
    public class FPPO
    {
        private const int FftSize128 = AudioRTAParam.FFTSize1K >> 3;
        private const int FftSize512 = AudioRTAParam.FFTSize1K >> 1;
        private const int FftSize2k = AudioRTAParam.FFTSize1K << 1;
        private const int FftSize8k = AudioRTAParam.FFTSize1K << 3;
        private const int FftSize32K = AudioRTAParam.FFTSize1K << 5;

        private enum FFT_LENGTH
        {
            FFT_32K = FftSize32K,
            FFT_8K = FftSize8k,
            FFT_2K = FftSize2k,
            FFT_512 = FftSize512,
            FFT_128 = FftSize128,
        }

        private const int FFT_COUNT = 5;
        //private StereoFFTHelper stereoFFT128 = null;
        //private StereoFFTHelper stereoFFT512 = null;
        //private StereoFFTHelper stereoFFT2k = null;
        //private StereoFFTHelper stereoFFT8K = null;
        //private StereoFFTHelper stereoFFT32K = null;

        private StereoFFTHelper[] stereoFFT = null;
        //private StereoBuffer[] stereoBuffer = null;

        private static OctaveEntity Octave24 = new Octave24();
        //private FPFR fpfrSR48K = null;
        //private FPFR fpfrSR96K = null;

        private FPFR currFPFR = null;

        private RTASampleRate sf = RTASampleRate.sr48k;
        private int sr = AudioRTAParam.SampleRate48k;

        private WindowFunction _winFunction = WindowFunction.HannWindow;

        private StereoBuffer amp32K = null;
        private StereoBuffer amp8K = null;
        private StereoBuffer amp2K = null;
        private StereoBuffer amp512 = null;
        private StereoBuffer amp128 = null;

        public FPPO(RTASampleRate samplingFreq, WindowFunction window)
        {
            sf = samplingFreq;
            switch (samplingFreq)
            {
                case RTASampleRate.sr48k:
                    sr = AudioRTAParam.SampleRate48k;
                    break;

                case RTASampleRate.sr96k:
                    sr = AudioRTAParam.SampleRate96k;
                    break;

                default:
                    throw new Exception("Sampleing Rate Error");
            }

            currFPFR = this.InitFPFR(sr);

            stereoFFT = new StereoFFTHelper[]
                {
                    new StereoFFTHelper(FftSize32K),
                    new StereoFFTHelper(FftSize8k),
                    new StereoFFTHelper(FftSize2k),
                    new StereoFFTHelper(FftSize512),
                    new StereoFFTHelper(FftSize128),
                };

            winFunction = window;

            amp32K = new StereoBuffer(FftSize32K);
            amp8K = new StereoBuffer(FftSize8k);
            amp2K = new StereoBuffer(FftSize2k);
            amp512 = new StereoBuffer(FftSize512);
            amp128 = new StereoBuffer(FftSize128);
        }

        public RTASampleRate sampleRate
        {
            get
            {
                return sf;
            }
        }

        public WindowFunction winFunction
        {
            get
            {
                return _winFunction;
            }
            set
            {
                _winFunction = value;
                foreach (StereoFFTHelper fft in stereoFFT)
                {
                    fft.winFunction = value;
                }
            }
        }
        
        public StereoFFTHelper stereoFFT32k
        {
            get
            {
                return stereoFFT[0];
            }
        }
        private StereoFFTHelper stereoFFT8k
        {
            get
            {
                return stereoFFT[1];
            }
        }
        private StereoFFTHelper stereoFFT2k
        {
            get
            {
                return stereoFFT[2];
            }
        }
        private StereoFFTHelper stereoFFT512
        {
            get
            {
                return stereoFFT[3];
            }
        }
        private StereoFFTHelper stereoFFT128
        {
            get
            {
                return stereoFFT[4];
            }
        }

        private FPFR InitFPFR(int sr)
        {
            FPFR fpfr = new FPFR(sr);
            int lower = 0;
            int upper = 0;
            float start_freq = 0;
            List<float> freq_resp = new List<float>();
            List<float> temp = null;

            temp = GetFixedPoint(sr, FftSize32K, start_freq);
            lower = freq_resp.Count;
            freq_resp.AddRange(temp);
            upper = freq_resp.Count;
            fpfr.FFT32KInterval = new FreqInterval(lower, upper);
            start_freq = temp[temp.Count - 1];

            temp = GetFixedPoint(sr, FftSize8k, start_freq);
            lower = freq_resp.Count;
            freq_resp.AddRange(temp);
            upper = freq_resp.Count;
            fpfr.FFT8KInterval = new FreqInterval(lower, upper);
            start_freq = temp[temp.Count - 1];

            temp = GetFixedPoint(sr, FftSize2k, start_freq);
            lower = freq_resp.Count;
            freq_resp.AddRange(temp);
            upper = freq_resp.Count;
            fpfr.FFT2KInterval = new FreqInterval(lower, upper);
            start_freq = temp[temp.Count - 1];

            temp = GetFixedPoint(sr, FftSize512, start_freq);
            if(temp.Count > 0)
            {
                lower = freq_resp.Count;
                freq_resp.AddRange(temp);
                upper = freq_resp.Count - 1;
                fpfr.FFT512Interval = new FreqInterval(lower, upper);
                start_freq = temp[temp.Count - 1];
            }
            else
            {
                fpfr.FFT2KInterval.upper = freq_resp.Count - 1;
            }

            temp = GetFixedPoint(sr, FftSize128, start_freq);
            if(temp.Count > 0)
            {
                lower = freq_resp.Count;
                freq_resp.AddRange(temp);
                upper = freq_resp.Count - 1;
                fpfr.FFT128Interval = new FreqInterval(lower, upper);
                start_freq = temp[temp.Count - 1];
            }
            else
            {
                fpfr.FFT512Interval.upper = freq_resp.Count - 1;
            }

            fpfr.freqResp = new FreqResp(freq_resp.ToArray());

            return fpfr;
            //Console.WriteLine("FFT32KInterval lower = {0}, upper = {1}", fpfr.FFT32KInterval.lower, fpfr.FFT128Interval.upper);
        }

        private static void PrintFreqResp(FPFR fpfr)
        {
            //Utility.OutputLine("****** SR = {0} ******", fpfr.sr);
            PrintFreqRespInterval(fpfr.freqResp, fpfr.FFT32KInterval);
            PrintFreqRespInterval(fpfr.freqResp, fpfr.FFT8KInterval);
            PrintFreqRespInterval(fpfr.freqResp, fpfr.FFT2KInterval);
            PrintFreqRespInterval(fpfr.freqResp, fpfr.FFT512Interval);
            if (fpfr.FFT128Interval != null)
            {
                PrintFreqRespInterval(fpfr.freqResp, fpfr.FFT128Interval);
            }
        }

        private static void PrintFreqRespInterval(FreqResp freq_resp, FreqInterval freq_interval)
        {
            //Utility.OutputLine("******Print freq_resp  Start******");
            //Utility.OutputLine("lower = {0}, upper = {1}", freq_interval.lower, freq_interval.upper);
            //for(int i = freq_interval.lower; i < freq_interval.upper; ++i)
            //{
            //    Utility.OutputLine("freq = {0}", freq_resp.freqs[i]);
            //}
            //Utility.OutputLine("******Print freq_resp  End******");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="fft_size"></param>
        /// <returns></returns>
        private List<float> GetFixedPoint(int sr, int fft_size, float start_freq)
        {
            float curr_fr = (float)sr / (float)fft_size;
            float critical_fr = curr_fr * 4;

            List<float> fixedPoint = new List<float>();

            if (fft_size == FftSize32K)
            {
                critical_fr = curr_fr * 2;

                for (int i = 0; i < Octave24.bands.Length - 1; ++i)
                {
                    OctaveBand currBand = Octave24.bands[i];
                    OctaveBand nextBand = Octave24.bands[i + 1];

                    if(nextBand.middle - currBand.middle > critical_fr)
                    {
                        int endIndex = (int)(currBand.middle / curr_fr);
                        for(int j = 1; j <= endIndex; ++j)
                        {
                            fixedPoint.Add(j * curr_fr);
                        }

                        return fixedPoint;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Octave24.bands.Length - 1; ++i)
                {
                    OctaveBand currBand = Octave24.bands[i];
                    OctaveBand nextBand = Octave24.bands[i + 1];
                    if(currBand.middle > start_freq)
                    {
                        if(nextBand.middle - currBand.middle > critical_fr)
                        {
                            return fixedPoint;
                        }

                        fixedPoint.Add(currBand.middle);
                    }
                }

                OctaveBand lastBand = Octave24.bands[Octave24.bandCount - 1];
                if (lastBand.middle > start_freq)
                {
                    fixedPoint.Add(lastBand.middle);
                }
            }

            return fixedPoint;
        }

        public FreqResp Execute_Test(StereoBuffer stereo)
        {
            if (stereo.length != FftSize32K)
            {
                return null;
            }
            double[] left = stereo.left;
            double[] right = stereo.right;
            //StopWatch sw = new StopWatch("FPPO");
            //Utility.OutputLine("************");
            //sw.Start();
            DoFFT32K(left, right);
            DoFFT_Test(left, right, FFT_LENGTH.FFT_8K);
            DoFFT_Test(left, right, FFT_LENGTH.FFT_2K);
            DoFFT_Test(left, right, FFT_LENGTH.FFT_512);
            DoFFT_Test(left, right, FFT_LENGTH.FFT_128);

            //for (int i = currFPFR.FFT128Interval.lower; i < currFPFR.FFT128Interval.upper; ++i)
            //{
            //    Utility.OutputLine("freq = {0}Hz, mag = {1}dB", currFPFR.freqResp.freqs[i], currFPFR.freqResp.magnitudes[i]);
            //}

            return currFPFR.freqResp;
            //sw.Stop();
            //Utility.OutputLine("************");
        }

        private StereoRMSAverager averager32K = null;
        private StereoRMSAverager averager8K = null;
        private StereoRMSAverager averager2K = null;
        private StereoRMSAverager averager512 = null;
        private StereoRMSAverager averager128 = null;

        private void UpdateAverager(Averager.AverageMode averageMode)
        {
            if(averager32K == null || averager32K.averageMode != averageMode)
            {
                averager32K = new StereoRMSAverager(FftSize32K / 2, averageMode);
            }
            if (averager8K == null || averager8K.averageMode != averageMode)
            {
                averager8K = new StereoRMSAverager(FftSize8k / 2, averageMode);
            }
            if (averager2K == null || averager2K.averageMode != averageMode)
            {
                averager2K = new StereoRMSAverager(FftSize2k / 2, averageMode);
            }
            if (averager512 == null || averager512.averageMode != averageMode)
            {
                averager512 = new StereoRMSAverager(FftSize512 / 2, averageMode);
            }
            if (averager128 == null || averager128.averageMode != averageMode)
            {
                averager128 = new StereoRMSAverager(FftSize128 / 2, averageMode);
            }
        }

        public FreqResp Execute(StereoBuffer stereo, Averager.AverageMode averageMode)
        {
            if (stereo.length != FftSize32K)
            {
                return null;
            }
            double[] left = stereo.left;
            double[] right = stereo.right;

            UpdateAverager(averageMode);

            DoFFT32K(left, right);
            DoFFT(left, right, FFT_LENGTH.FFT_8K);
            DoFFT(left, right, FFT_LENGTH.FFT_2K);
            DoFFT(left, right, FFT_LENGTH.FFT_512);
            DoFFT(left, right, FFT_LENGTH.FFT_128);
            return currFPFR.freqResp;
        }

        private void DoFFT32K(double[] left, double[] right)
        {
            //bufferFFT32k.CopyFromData(left, right);
            stereoFFT32k.DoFFT(left, right);
            StereoBuffer originalAmp = new StereoBuffer(FftSize32K);

            for (int i = 1; i <= currFPFR.FFT32KInterval.upper; ++i)
            {
                originalAmp.left[i] = stereoFFT32k.stereoResult.left[i].Magnitude;
                originalAmp.right[i] = stereoFFT32k.stereoResult.right[i].Magnitude;
            }

            Array.Copy(originalAmp.left, amp32K.left, currFPFR.FFT32KInterval.upper + 1);
            Array.Copy(originalAmp.right, amp32K.right, currFPFR.FFT32KInterval.upper + 1);

            averager32K.Excete(amp32K.left, amp32K.right);

            //currFPFR.freqResps[]
            int end = currFPFR.FFT32KInterval.upper - currFPFR.FFT32KInterval.lower;
            //Complex[] division = new Complex[n];
            for (int i = 1; i <= end; ++i)
            {
                int fft32Index = i - 1;

                if (amp32K.left[i] > 0 && amp32K.right[i] > 0)
                {
                    currFPFR.freqResp.magnitudes[fft32Index] = 20 * Math.Log10(amp32K.left[i] / amp32K.right[i]);
                }
                else
                {
                    currFPFR.freqResp.magnitudes[fft32Index] = 0;
                }

                if (originalAmp.left[i] > 0 &&
                   originalAmp.right[i] > 0)
                {
                    currFPFR.freqResp.signals[fft32Index] = stereoFFT32k.stereoResult.left[i] / stereoFFT32k.stereoResult.right[i];
                }
                else
                {
                    currFPFR.freqResp.signals[fft32Index] = Complex.Zero;
                }

                //if (stereoFFT32k.stereoResult.right[fft32Index].Magnitude == 0)
                //{
                //    //currFPFR.freqResp.magnitudes[fft32Index] = 0;
                //    currFPFR.freqResp.signals[fft32Index] = Complex.Zero;
                //    continue;
                //}

                //currFPFR.freqResp.signals[fft32Index] = stereoFFT32k.stereoResult.left[fft32Index] / stereoFFT32k.stereoResult.right[fft32Index];
                //Complex division = stereoFFT32k.stereoResult.left[fft32Index] / stereoFFT32k.stereoResult.right[fft32Index];
                //currFPFR.freqResp.magnitudes[fft32Index] = 20 * Math.Log10(division.Magnitude);
                //currFPFR.freqResp.phases[fft32Index] = division.Phase * 180 / Math.PI;
                //Utility.OutputLine("freq = {0}Hz, mag = {1}dB", currFPFR.freqResps[i].freq, currFPFR.freqResps[i].magnitude + 6.0);
            }
        }

        private void DoFFT(double[] left, double[] right, FFT_LENGTH fft_length)
        {
            int length = 0;
            int start = currFPFR.FFT8KInterval.lower;
            int end = currFPFR.FFT8KInterval.upper;
            StereoFFTHelper stereoFFT = null;            
            StereoBuffer amp = null;
            StereoRMSAverager averager = null;
            switch (fft_length)
            {
                case FFT_LENGTH.FFT_8K:
                    length = FftSize8k;
                    start = currFPFR.FFT8KInterval.lower;
                    end = currFPFR.FFT8KInterval.upper;
                    stereoFFT = stereoFFT8k;
                    amp = amp8K;
                    averager = averager8K;                    
                    break;

                case FFT_LENGTH.FFT_2K:
                    length = FftSize2k;
                    start = currFPFR.FFT2KInterval.lower;
                    end = currFPFR.FFT2KInterval.upper;
                    stereoFFT = stereoFFT2k;
                    amp = amp2K;
                    averager = averager2K;                    
                    break;

                case FFT_LENGTH.FFT_512:
                    length = FftSize512;
                    if (currFPFR.FFT512Interval != null)
                    {
                        start = currFPFR.FFT512Interval.lower;
                        end = currFPFR.FFT512Interval.upper;
                        stereoFFT = stereoFFT512;
                        amp = amp512;
                        averager = averager512;                        
                    }
                    else
                    {
                        return;
                    }
                    break;

                case FFT_LENGTH.FFT_128:
                    length = FftSize128;
                    if (currFPFR.FFT128Interval != null)
                    {
                        start = currFPFR.FFT128Interval.lower;
                        end = currFPFR.FFT128Interval.upper;
                        stereoFFT = stereoFFT128;
                        amp = amp128;
                        averager = averager128;                        
                    }
                    else
                    {
                        return;
                    }
                    break;

                default:
                    return;
            }

            float fr = (float)sr / length;
            int n = FftSize32K / length;
            //int n = 1;

            int freqDomainStartIndex = (int)Math.Floor(currFPFR.freqResp.freqs[start] / fr);
            int freqDomainEndIndex = (int)Math.Ceiling(currFPFR.freqResp.freqs[end] / fr);

            Complex[] average = new Complex[freqDomainEndIndex - freqDomainStartIndex + 1];
            double[] average_sum = new double[freqDomainEndIndex - freqDomainStartIndex + 1];

            stereoFFT.DoFFT(left, right);
            for (int j = 1; j < length / 2; ++j)
            {
                amp.left[j] = stereoFFT.stereoResult.left[j].Magnitude;
                amp.right[j] = stereoFFT.stereoResult.right[j].Magnitude;
            }

            averager.Excete(amp.left, amp.right);

            //Utility.OutputLine("******************");
            for (int j = freqDomainStartIndex; j <= freqDomainEndIndex; ++j)
            {
                if (amp.left[j] > 0 && amp.right[j] > 0)
                {
                    average_sum[j - freqDomainStartIndex] = 20 * Math.Log10(amp.left[j] / amp.right[j]);
                }
                else
                {
                    average_sum[j - freqDomainStartIndex] = 0;
                }

                if (stereoFFT.stereoResult.right[j].Magnitude == 0)
                {
                    continue;
                }

                Complex division = stereoFFT.stereoResult.left[j] / stereoFFT.stereoResult.right[j];
                average[j - freqDomainStartIndex] += division / n;

                //double mag = 20 * Math.Log10(division.Magnitude);
                //average_sum[j - freqDomainStartIndex] += mag / n;
                //Utility.OutputLine("freq = {0}Hz, mag = {1}dB", j * fr, mag);
                //sum[j - freqDomainStartIndex] += division;
            }
            
            Complex t = new Complex();
            double curr_mag = 0;
            for (int i = start; i <= end; ++i)
            {
                float currFreq = currFPFR.freqResp.freqs[i];
                int index = (int)(currFreq / fr);
                float remain = currFreq - index * fr;
                float prevFreq = index * fr;
                float nextFreq = prevFreq + fr;
                if (FloatIsEqual(remain, 0.0f))
                {
                    t = average[index - freqDomainStartIndex];
                    //curr_mag = average_sum[index - freqDomainStartIndex];
                    curr_mag = average_sum[index - freqDomainStartIndex];
                }
                else if (FloatIsEqual(remain, fr))
                {
                    t = average[index - freqDomainStartIndex + 1];
                    curr_mag = average_sum[index - freqDomainStartIndex + 1];
                }
                else
                {
                    Complex prev = average[index - freqDomainStartIndex];
                    Complex next = average[index - freqDomainStartIndex + 1];
                    t.Real = (currFreq - prevFreq) / fr * (next.Real - prev.Real) + prev.Real;
                    t.Imaginary = (currFreq - prevFreq) / fr * (next.Imaginary - prev.Imaginary) + prev.Imaginary;

                    double prev_mag = average_sum[index - freqDomainStartIndex];
                    double next_mag = average_sum[index - freqDomainStartIndex + 1];
                    curr_mag = (currFreq - prevFreq) / fr * (next_mag - prev_mag) + prev_mag;
                }

                //if (curr_mag == 0)
                //{

                //}

                //if(t.Magnitude == 0)
                //{
                //    currFPFR.freqResp.signals[i] = Complex.Zero;
                //    //currFPFR.freqResp.magnitudes[i] = AudioParams.ZERO_MAG_DB;
                //    //currFPFR.freqResp.phases[i] = 0;
                //    continue;
                //}



                currFPFR.freqResp.signals[i] = t;
                currFPFR.freqResp.magnitudes[i] = curr_mag;

                //currFPFR.freqResp.magnitudes[i] = 20 * Math.Log10(t.Magnitude);
                //currFPFR.freqResp.phases[i] = t.Phase * 180 / Math.PI;
                //Utility.OutputLine("freq = {0}Hz, mag = {1} dB", currFreq, currFPFR.freqResps[i].magnitude + 6.0);
            }
        }

        private void DoFFT_Test(double[] left, double[] right, FFT_LENGTH fft_length)
        {
            int length = 0;
            int start = currFPFR.FFT8KInterval.lower;
            int end = currFPFR.FFT8KInterval.upper;
            StereoFFTHelper stereoFFT = null;
            switch (fft_length)
            {
                case FFT_LENGTH.FFT_8K:
                    length = FftSize8k;
                    start = currFPFR.FFT8KInterval.lower;
                    end = currFPFR.FFT8KInterval.upper;
                    stereoFFT = stereoFFT8k;
                    break;

                case FFT_LENGTH.FFT_2K:
                    length = FftSize2k;
                    start = currFPFR.FFT2KInterval.lower;
                    end = currFPFR.FFT2KInterval.upper;
                    stereoFFT = stereoFFT2k;
                    break;

                case FFT_LENGTH.FFT_512:
                    length = FftSize512;
                    if(currFPFR.FFT512Interval != null)
                    {
                        start = currFPFR.FFT512Interval.lower;
                        end = currFPFR.FFT512Interval.upper;
                        stereoFFT = stereoFFT512;
                    }
                    else
                    {
                        return;
                    }
                    break;

                case FFT_LENGTH.FFT_128:
                    length = FftSize128;
                    if (currFPFR.FFT128Interval != null)
                    {
                        start = currFPFR.FFT128Interval.lower;
                        end = currFPFR.FFT128Interval.upper;
                        stereoFFT = stereoFFT128;
                    }
                    else
                    {
                        return;
                    }
                    break;

                default:
                    return;
            }

            float fr = (float)sr / length;
            int n = FftSize32K / length;
            //int n = 1;

            int freqDomainStartIndex = (int)Math.Floor(currFPFR.freqResp.freqs[start] / fr);
            int freqDomainEndIndex = (int)Math.Ceiling(currFPFR.freqResp.freqs[end] / fr);

            Complex[] average = new Complex[freqDomainEndIndex - freqDomainStartIndex + 1];
            double[] average_sum = new double[freqDomainEndIndex - freqDomainStartIndex + 1];

            for (int i = 0; i < n; ++i)
            {
                stereoFFT.DoFFT(left, right, i * length);

                //Utility.OutputLine("******************");
                for (int j = freqDomainStartIndex; j <= freqDomainEndIndex; ++j)
                {
                    if(stereoFFT.stereoResult.right[j].Magnitude == 0)
                    {
                        continue;
                    }

                    Complex division = stereoFFT.stereoResult.left[j] / stereoFFT.stereoResult.right[j];
                    average[j - freqDomainStartIndex] += division / n;

                    double mag = 20 * Math.Log10(division.Magnitude);
                    average_sum[j - freqDomainStartIndex] += mag / n;
                    //Utility.OutputLine("freq = {0}Hz, mag = {1}dB", j * fr, mag);
                    //sum[j - freqDomainStartIndex] += division;
                }
            }

            //for(int j = 0; j <= freqDomainEndIndex - freqDomainStartIndex; ++j)
            //{
            //    //sum[j] = sum[j] / n;
            //    ///double mag = 20 * Math.Log10(average[j].Magnitude);
            //    //Utility.OutputLine("freq = {0}Hz, mag = {1}dB", (j + freqDomainStartIndex) * fr, mag);
            //}

            Complex t = new Complex();
            double curr_mag = 0;
            for(int i = start; i <= end; ++i)
            {
                float currFreq = currFPFR.freqResp.freqs[i];
                int index = (int)(currFreq / fr);
                float remain = currFreq - index * fr;
                float prevFreq = index * fr;
                float nextFreq = prevFreq + fr;
                if(FloatIsEqual(remain, 0.0f))
                {
                    t = average[index - freqDomainStartIndex];
                    curr_mag = average_sum[index - freqDomainStartIndex];
                }
                else if(FloatIsEqual(remain, fr))
                {
                    t = average[index - freqDomainStartIndex + 1];
                    curr_mag = average_sum[index - freqDomainStartIndex + 1];
                }
                else
                {
                    Complex prev = average[index - freqDomainStartIndex];
                    Complex next = average[index - freqDomainStartIndex + 1];
                    t.Real = (currFreq - prevFreq) / fr * (next.Real - prev.Real) + prev.Real;
                    t.Imaginary = (currFreq - prevFreq) / fr * (next.Imaginary - prev.Imaginary) + prev.Imaginary;

                    double prev_mag = average_sum[index - freqDomainStartIndex];
                    double next_mag = average_sum[index - freqDomainStartIndex + 1];
                    curr_mag = (currFreq - prevFreq) / fr * (next_mag - prev_mag) + prev_mag;
                }

                //if (curr_mag == 0)
                //{

                //}

                //if(t.Magnitude == 0)
                //{
                //    currFPFR.freqResp.signals[i] = Complex.Zero;
                //    //currFPFR.freqResp.magnitudes[i] = AudioParams.ZERO_MAG_DB;
                //    //currFPFR.freqResp.phases[i] = 0;
                //    continue;
                //}

                

                currFPFR.freqResp.signals[i] = t;
                currFPFR.freqResp.magnitudes[i] = curr_mag;

                //currFPFR.freqResp.magnitudes[i] = 20 * Math.Log10(t.Magnitude);
                //currFPFR.freqResp.phases[i] = t.Phase * 180 / Math.PI;
                //Utility.OutputLine("freq = {0}Hz, mag = {1} dB", currFreq, currFPFR.freqResps[i].magnitude + 6.0);
            }
        }

        //private void DoFFT8K(double[] left, double[] right)
        //{
        //    //bufferFFT8k.CopyFromData(left, right);
        //    float fr = (float)sr / FFT_SIZE_8K;
        //    int n = FFT_SIZE_32K / FFT_SIZE_8K;
        //    int start = currFPFR.FFT8KInterval.lower;
        //    int end = currFPFR.FFT8KInterval.upper;

        //    int freqDomainStartIndex = (int)Math.Floor(currFPFR.freqResps[start].freq / fr);
        //    int freqDomainEndIndex = (int)Math.Round(currFPFR.freqResps[end].freq / fr);

        //    Complex[] average = new Complex[freqDomainEndIndex - freqDomainStartIndex + 1];

        //    for (int i = 0; i < n; ++i)
        //    {
        //        stereoFFT8k.DoFFT(left, right, i * FFT_SIZE_8K);

        //        //Utility.OutputLine("******************");
        //        for (int j = freqDomainStartIndex; j <= freqDomainEndIndex; ++j)
        //        {
        //            Complex division = stereoFFT8k.stereoResult.left[j] / stereoFFT8k.stereoResult.right[j];
        //            //double mag = 20 * Math.Log10(division.Magnitude) + 6.0;
        //            average[j - freqDomainStartIndex] += division / n;
        //            //Utility.OutputLine("freq = {0}Hz, mag = {1}dB", j * fr, mag);
        //            //sum[j - freqDomainStartIndex] += division;
        //        }
        //    }

        //    //for(int j = 0; j <= freqDomainEndIndex - freqDomainStartIndex; ++j)
        //    //{
        //    //    //sum[j] = sum[j] / n;
        //    //    ///double mag = 20 * Math.Log10(average[j].Magnitude);
        //    //    //Utility.OutputLine("freq = {0}Hz, mag = {1}dB", (j + freqDomainStartIndex) * fr, mag);
        //    //}

        //    Complex t = new Complex();
        //    for (int i = start; i < end; ++i)
        //    {
        //        float currFreq = currFPFR.freqResps[i].freq;
        //        int index = (int)(currFreq / fr);
        //        float remain = currFreq - index * fr;
        //        float prevFreq = index * fr;
        //        float nextFreq = prevFreq + fr;
        //        if (FloatIsEqual(remain, 0.0f))
        //        {
        //            t = average[index - freqDomainStartIndex];
        //        }
        //        else if (FloatIsEqual(remain, fr))
        //        {
        //            t = average[index - freqDomainStartIndex + 1];
        //        }
        //        else
        //        {
        //            Complex prev = average[index - freqDomainStartIndex];
        //            Complex next = average[index - freqDomainStartIndex + 1];
        //            t.Real = (currFreq - prevFreq) / fr * (next.Real - prev.Real) + prev.Real;
        //            t.Imaginary = (currFreq - prevFreq) / fr * (next.Imaginary - prev.Imaginary) + prev.Imaginary;
        //        }

        //        currFPFR.freqResps[i].magnitude = 20 * Math.Log10(t.Magnitude);
        //        //Utility.OutputLine("freq = {0}Hz, mag = {1} dB", currFreq, currFPFR.freqResps[i].magnitude + 6.0);
        //    }
        //}

        private const float EPSILON = 0.000001f;
        public static bool FloatIsEqual(float v1, float v2)
        {
            return Math.Abs(v1 - v2) <= EPSILON;
        }

        [Serializable]
        public class FreqResp
        {
            public int length { get; private set; }
            public float[] freqs = null;
            public double[] magnitudes = null;
            public double[] phases =null ;
            [NonSerialized]
            public Complex[] signals = null;
            [NonSerialized]
            public int startIndex = 0;

            public FreqResp(float[] freqList)
            {
                System.Diagnostics.Debug.Assert(freqList != null && freqList.Length != 0);

                length = freqList.Length;

                freqs = freqList;
                magnitudes = new double[freqList.Length];
                phases = new double[freqList.Length];
                signals = new Complex[freqList.Length];
            }

            public float minFreq
            {
                get
                {
                    if(freqs != null && startIndex >= 0 && startIndex < freqs.Length)
                    {
                        return freqs[startIndex];
                    }
                    else
                    {
                        return 0.0f;
                    }
                }
            }

            public float maxFreq
            {
                get
                {
                    if (freqs != null  && freqs.Length > 0)
                    {
                        return freqs[freqs.Length - 1];
                    }
                    else
                    {
                        return 0.0f;
                    }
                }
            }
        }

        public class FreqInterval
        {
            public int lower;
            public int upper;

            public FreqInterval(int low, int up)
            {
                this.lower = low;
                this.upper = up;
            }
        }

        private class FPFR
        {
            public int sr = 0;
            public FreqResp freqResp = null;

            public FreqInterval FFT32KInterval = null;
            public FreqInterval FFT8KInterval = null;
            public FreqInterval FFT2KInterval = null;
            public FreqInterval FFT512Interval = null;
            public FreqInterval FFT128Interval = null;

            public FPFR(int samplingRate)
            {
                sr = samplingRate;
            }
        }
        
    }
}
