using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Common
{
    //峰值保持器
    public class PeakValueHolder
    {
        private const float DefaultFallAcceleratedSpeed = 48.0f;

        //下限
        private readonly int lowerLimiter;
        //上限
        private readonly int upperLimiter;
        //峰值保持时间，单位ms
        public readonly int peakHoldTimeInMs;
        //掉落加速度
        public readonly float dropAcceleratedSpeed;

        //峰值
        public int peakValue { get; private set; }
        //当前值

        public int _currValue;
        public int currValue 
        { 
            get
            {
                return this._currValue;
            }
            private set
            {
                this._currValue = AEC.Core.Utils.ConstrainValue(value, lowerLimiter, upperLimiter);
                if (this.peakValue < this._currValue)
                {
                    this.peakValue = this._currValue;
                    this.peakTriggerTime = DateTime.Now;
                }
            }
        }

        //峰值触发时间
        public DateTime peakTriggerTime { get; set; }

        public PeakValueHolder(int lowerLimiter, int upperLimiter, int holdTimeInMs, float fallAcceleratedSpeed = DefaultFallAcceleratedSpeed)
        {
            this.lowerLimiter = lowerLimiter;
            this.upperLimiter = upperLimiter;
            this.peakHoldTimeInMs = holdTimeInMs;
            this.dropAcceleratedSpeed = fallAcceleratedSpeed;
            this.peakValue = lowerLimiter;
            this.currValue = lowerLimiter;
            this.peakTriggerTime = DateTime.Now;
        }

        public void Reset()
        {
            this.peakValue = lowerLimiter;
            this.currValue = lowerLimiter;
            this.peakTriggerTime = DateTime.Now;
        }

        //如果返回true，则需要刷新界面
        public bool UpdateCurrValue(double value)
        {
            int roundValue = (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);
            roundValue = AEC.Core.Utils.ConstrainValue(roundValue, lowerLimiter, upperLimiter);
            if (this._currValue <= roundValue)   //当前电平小于此值，则更新当前电平和峰值，并保存触发时间
            {
                this._currValue = roundValue;
                this.peakValue = this._currValue;
                this.peakTriggerTime = DateTime.Now;

                //Log.OutputLine("UpdateCurrValue()...");

                return true;
            }
            
            return false;
        }

        //如果返回true，则需要刷新界面
        public bool FreeFall()
        {
            if(this.currValue <= lowerLimiter)
            {
                return false;
            }

            TimeSpan ts = DateTime.Now - this.peakTriggerTime;

            if(ts.TotalMilliseconds <= peakHoldTimeInMs)
            {
                return false;
            }

            double fallTime = (ts.TotalMilliseconds - peakHoldTimeInMs) / 1000;
            double fallHeight = 0.5 * dropAcceleratedSpeed * fallTime * fallTime;
            //Log.OutputLine("fallHeight = {0}", fallHeight);
            int curr = (int)Math.Round(this.peakValue - fallHeight, 0, MidpointRounding.AwayFromZero);

            this.currValue = AEC.Core.Utils.ConstrainValue(curr, lowerLimiter, upperLimiter);

            return true;
        }
    }
}
