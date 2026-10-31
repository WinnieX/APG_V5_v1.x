using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class Delay
    {
        public const short maxOffset = AudioRTAParam.FFTSize32K / 2;
        private short _delayOffset = 0;
        private int _sr = AudioRTAParam.SampleRate48k;
        private const float temperature = 20.0f;      //单位摄氏度
        private const float Kilo = 1000;

        private const string TimeUnit = "ms";
        private const string DistanceUnit = "m";


        public Delay()
        {

        }

        public Delay(int sr)
        {
            _sr = sr;
        }

        public void UpdateSr(int sr)
        {
            if (sr != AudioRTAParam.SampleRate48k &&
                sr != AudioRTAParam.SampleRate96k)
            {
                throw (new ArgumentException("UpdateSr...参数异常"));
            }

            if (sr != _sr)
            {
                _delayOffset = (short)(_delayOffset * sr / _sr);
                _sr = sr;
            }
        }

        /// <summary>
        /// 初始化延时
        /// </summary>
        /// <param name="sr">采样频率</param>
        /// <param name="delay">延时事件, 单位ms</param>
        public void Init(int sr, float delayTime)
        {
            if (sr != AudioRTAParam.SampleRate48k &&
                sr != AudioRTAParam.SampleRate96k)
            {
                return;
            }

            if (delayTime < 0)
            {
                delayTime = 0;
            }

            _sr = sr;
            _delayOffset = (short)(Math.Round(Math.Round(delayTime * sr / 1000.0, 1, MidpointRounding.AwayFromZero)));
        }

        public short delayOffset
        {
            get
            {
                return this._delayOffset;
            }
            set
            {
                this._delayOffset = Math.Min(Math.Max(value, (short)0), maxOffset);
            }
        }

        public int sr
        {
            get
            {
                return _sr;
            }
        }

        //单位ms
        public float delayTime
        {
            get
            {
                return 1f / (float)_sr * Kilo * _delayOffset;
            }
            set
            {
                short offset = (short)(Math.Round(Math.Round(value * sr / Kilo, 1, MidpointRounding.AwayFromZero)));
                _delayOffset = Math.Min(Math.Max(offset, (short)0), maxOffset);
            }
        }

        //单位m/s
        public static float speedOfSound
        {
            get
            {
                //公式1
                return 331.45f + 0.61f * temperature;
                //公式2
                //return 331.3f * (float)Math.Sqrt(1 + temperature / 273.15f);
            }
        }

        //单位m
        public float distance
        {
            get
            {
                return speedOfSound * delayTime / Kilo;
            }
        }

        public override string ToString()
        {
            return string.Format("{0:0.00}ms   ({1:0.00}m)", this.delayTime, this.distance);
        }

        public static string GetLabel(short index, int sr)
        {
            index = Math.Min(Math.Max(index, (short)0), maxOffset);
            float delay_time = Kilo / (float)sr * index;
            //Console.WriteLine("delay:{0}", delay_time);
            float distance = speedOfSound * delay_time / Kilo;
            return string.Format("{0:0.00}{1}   ({2:0.00}{3})", delay_time, TimeUnit, distance, DistanceUnit);
        }

        public string GetDelayLabel(short index)
        {
            return GetLabel(index, sr);
            //index = Math.Min(Math.Max(index, (short)0), maxOffset);
            //float delay_time = Kilo / (float)_sr * index;
            ////Console.WriteLine("delay:{0}", delay_time);
            //float distance = speedOfSound * delay_time / Kilo;
            //return string.Format("{0:0.00}{1}   ({2:0.00}{3})", delay_time, TimeUnit, distance, DistanceUnit);
        }

        public short CalcDelayIndex(string text)
        {
            text = text.Trim();
            
            if(text.Length == 0)
            {
                return -1;
            }
            
            string[] texts = text.Split(' ');
            if (texts.Length == 0)
            {
                return -1;
            }
            text = texts[0];

            if (text.EndsWith(TimeUnit))
            {
                string str = AEC.Core.Utils.RemoveStringSuffix(text, TimeUnit);
                float v = 0;
                if(float.TryParse(str, out v) && v >= 0f)
                {
                    int index = (int)Math.Round(v * _sr / Kilo, MidpointRounding.AwayFromZero);
                    return (short)(Math.Min(Math.Max(index, 0), (int)maxOffset));
                }
            }
            else if (text.EndsWith(DistanceUnit))
            {
                string str = AEC.Core.Utils.RemoveStringSuffix(text, DistanceUnit);
                float v = 0;
                if (float.TryParse(str, out v) && v >= 0f)
                {
                    short index = (short)Math.Round(v / speedOfSound * _sr, MidpointRounding.AwayFromZero);
                    return Math.Min(Math.Max(index, (short)0), maxOffset);
                }
            }
            else
            {
                float v = 0;
                if (float.TryParse(text, out v) && v >= 0f)
                {
                    short index = (short)Math.Round(v * _sr / Kilo, MidpointRounding.AwayFromZero);
                    return Math.Min(Math.Max(index, (short)0), maxOffset);
                }
            }

            return -1;
        }
    }
}
