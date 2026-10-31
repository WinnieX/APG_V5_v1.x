using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Common
{
    public class LimiterStatusUIValue
    {
        public const short refLevel = 51;
        public const short maxLevel = 4;
        public const short minLevel = -36;
        public const short minCompressed = 0;
        public const short maxCompressed = 10;

        public readonly Limiter limiter;

        private float _level = minLevel;
        private float _levelPeakHold = minLevel;
        private float _peakLevel = minLevel;

        private short _compressed = minCompressed;

        private DateTime _peakTime = DateTime.Now;
        private const int peakHoldTime = 200;

        public LimiterStatusUIValue(Limiter limiter_)
        {
            this.limiter = limiter_;
            this.levelPeakHold = minLevel;
        }

        public void Update(LimiterStatus limiterStatus)
        {
            float rawLevel = (short)Math.Round(limiterStatus.level + refLevel - limiter.threshold.v, 0, MidpointRounding.AwayFromZero);

            //Log.OutputLine("v:{0} delta:{1} --- {2}", limiterStatus.level, limiterStatus.level + refLevel - limiter.threshold.v, value);

            //Log.OutputLine("rawLevel = {0} dB", rawLevel);

            this._level = Utils.ConstrainValue(rawLevel, minLevel, maxLevel);

            if (this._level >= this._levelPeakHold)
            {
                this._levelPeakHold = this._level;
                this._peakLevel = this._level;
                this._peakTime = DateTime.Now;

                this.levelPeakHold = (short)Math.Round(this._levelPeakHold, 0, MidpointRounding.AwayFromZero);
                //this._peakHoldCount = 0;
            }
            else if (this._levelPeakHold > minLevel)
            {
                TimeSpan ts = DateTime.Now - this._peakTime;
                //Log.OutputLine("ts = {0} ms", ts.TotalMilliseconds);
                if (ts.TotalMilliseconds >= peakHoldTime)
                {
                    float fallTime = ((float)(ts.TotalMilliseconds - peakHoldTime) / 1000.0f);
                    float fallHeight = 25.0f * fallTime * fallTime;
                    float peak = this._peakLevel - fallHeight;

                    if (peak < rawLevel)
                    {
                        peak = rawLevel;
                    }

                    //Log.OutputLine("fallTime = {0} s, fallHeight = {1}, peak: {2} dB", fallTime, fallHeight, peak);

                    this._levelPeakHold = Utils.ConstrainValue(peak, minLevel, maxLevel);

                    this.levelPeakHold = (short)Math.Round(this._levelPeakHold, 0, MidpointRounding.AwayFromZero);
                }
            }

            //Log.OutputLine("level = {0} dB, levelPeakHold: {1} dB, minLevel = {2}", this._level, this._levelPeakHold, minLevel);

            if (limiterStatus.ratio > 0)
            {
                double decibels = AEC.Core.DSP.DSPHelper.LinearToDecibels(limiterStatus.ratio);
                short dB = (short)Math.Round(-decibels, 0, MidpointRounding.AwayFromZero);
                this._compressed = Utils.ConstrainValue(dB, minCompressed, maxCompressed);
            }
            else
            {
                this._compressed = maxCompressed;
            }
        }

        public float levelValue
        {
            get
            {
                return _level;
            }
        }

        public short levelPeakHold
        {
            get;
            private set;
        }

        public short compressed
        {
            get
            {
                return _compressed;
            }
        }
    }
}
