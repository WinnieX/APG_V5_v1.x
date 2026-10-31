using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class PeakHolder
    {
        public const int peak_holdon_time = 1024;       //峰值保持时间
        private const float fallAccelerated = 0.4f;     //掉落加速度
        public float min = 0;
        public float max = 0;
        public float _peak = 0;
        public DateTime peakTime = DateTime.Now;
        private int fallCount = 0;

        public float peak
        {
            get
            {
                //Console.WriteLine("get peak:{0}", this._peak);
                return this._peak;
            }
        }

        public PeakHolder(float min_, float max_)
        {
            System.Diagnostics.Debug.Assert(min_ < max_);

            this.min = min_;
            this.max = max_;
            this._peak = this.min;
            this.peakTime = DateTime.Now;
            this.fallCount = 0;
        }

        public void UpdateLimit(float min_, float max_)
        {
            System.Diagnostics.Debug.Assert(min_ < max_);
            this.min = min_;
            this.max = max_;
            this._peak = this.min;
            this.peakTime = DateTime.Now;
            this.fallCount = 0;
        }

        //public bool UpdatePeak(float level)
        //{
        //    return Update(level) || CheckPeakFall();
        //}

        public bool UpdatePeak(float level)
        {
            if(level > this.max)
            {
                level = this.max;
            }

            //Console.WriteLine("this._peak:{0}", this._peak);

            if (this._peak <= level)
            {
                this._peak = level;
                this.fallCount = 0;
                this.peakTime = DateTime.Now;

                return true;
            }

            return false;
        }

        //public bool TryMakePeakFall()
        //{
        //    if (this._peak > this.min && (DateTime.Now - this.peakTime).TotalMilliseconds > peak_holdon_time)
        //    {
        //        this._peak -= (++fallCount - 0.5f) * fallAccelerated;

        //        if (this._peak < this.min)
        //        {
        //            this._peak = this.min;
        //        }

        //        return true;
        //    }

        //    return false;
        //}

        public bool CheckPeakFall()
        {
            if (this._peak > this.min && (DateTime.Now - this.peakTime).TotalMilliseconds > peak_holdon_time)
            {
                this._peak -= (++fallCount - 0.5f) * fallAccelerated;

                if (this._peak < this.min)
                {
                    this._peak = this.min;
                }

                return true;
            }

            return false;
        }

        //public void Reset()
        //{
        //    this._peak = this.min;
        //    this.fallCount = 0;
        //}
    }
}
