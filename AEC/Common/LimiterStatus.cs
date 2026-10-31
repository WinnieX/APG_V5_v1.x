using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Common
{
    [Serializable()]
    public class LimiterStatus
    {
        public const float minRatio = 0.0f;
        public const float maxRatio = 1.0f;
        public const float minLevel = -256;
        public const float maxLevel = 0.0f;

        private const int FixedPoint24Max = 0x00800000;

        private float _level = 0;
        private float _ratio = 0;

        public float level
        {
            get
            {
                return _level;
            }
            set
            {
                this._level = value <= maxLevel ? value : maxLevel;
            }
        }

        public float linearValue
        {
            set
            {
                value = Math.Abs(value);

                if (value > 0)
                {
                    level = 20 * (float)(Math.Log10(value / FixedPoint24Max));
                }
                else
                {
                    level = minLevel;
                }
            }
        }

        public float ratio
        {
            get
            {
                return _ratio;
            }
            set
            {
                this._ratio = Utils.ConstrainValue(value, minRatio, maxRatio);
            }
        }

        public LimiterStatus()
        {

        }
    }
}
