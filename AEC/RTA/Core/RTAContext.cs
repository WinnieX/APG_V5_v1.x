using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class RTAContext
    {
        private RTAMode _rtaMode = RTAMode.Spectrum;
        private RTASampleRate _rtaSampleRate = RTASampleRate.sr48k;
        private FFTSize _rtaFftSize = FFTSize.fftSize32k;

        public RTAMode rtaMode
        {
            get
            {
                return this._rtaMode;
            }
            //set
            //{
            //    this._rtaMode = value;
            //}
        }

        public RTASampleRate rtaSampleRate
        {
            get
            {
                return this._rtaSampleRate;
            }
            //set
            //{
            //    this._sampleRate = value;
            //}
        }

        public FFTSize rtaFftSize
        {
            get
            {
                return this._rtaFftSize;
            }
            //set
            //{
            //    this._fftSize = value;
            //}
        }
        
        public int sampleRate
        {
            get
            {
                return AudioRTAParam.GetSampleRate(_rtaSampleRate);
            }
        }

        public int fftSize
        {
            get
            {
                return AudioRTAParam.GetFftSize(_rtaFftSize);
            }
        }

        public RTAContext()
        {

        }

        public void Update(RTAMode mode, RTASampleRate sr, FFTSize size)
        {
            this._rtaMode = mode;
            this._rtaSampleRate = sr;
            this._rtaFftSize = size;
        }

        public void CopyFrom(RTAContext context)
        {
            this._rtaMode = context._rtaMode;
            this._rtaSampleRate = context._rtaSampleRate;
            this._rtaFftSize = context._rtaFftSize;
        }
    }
}
