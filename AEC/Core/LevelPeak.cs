using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class LevelPeak
    {
        public const int peak_holdon_time = 1000;
        private const float fallStep = 0.5f;
        public readonly float min = 0;
        public readonly float max = 0;
        public float _peak = 0;
        public DateTime peakTime = DateTime.Now;
        private int fallCount = 0;

        public float peak
        {
            get
            {
                return this._peak;
            }
        }
        
        public LevelPeak(float min_, float max_)
        {
            System.Diagnostics.Debug.Assert(min_ < max_);

            this.min = min_;
            this.max = max_;
            this._peak = this.min;
        }

        public bool Update(float level)
        {
            if(level > this.max)
            {
                level = this.max;
            }

            if (this._peak <= level)
            {
                this._peak = level;
                this.fallCount = 0;
                this.peakTime = DateTime.Now;

                return true;
            }

            return false;
        }

        public bool CheckPeakFall()
        {
            if (this._peak > this.min && (DateTime.Now - this.peakTime).TotalMilliseconds > peak_holdon_time)
            {
                this._peak -= fallStep * fallCount++;

                if (this._peak < this.min)
                {
                    this._peak = this.min;
                }

                return true;
            }

            return false;
        }

        public void Reset()
        {
            this._peak = this.min;
            this.fallCount = 0;
        }
    }
}
