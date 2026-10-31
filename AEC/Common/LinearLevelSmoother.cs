using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Common
{
    public class LinearValueSmoother
    {
        private static readonly double[] Weights2ndOrder = new double[]{1.0 / 4, 3.0 / 4};
        private static readonly double[] Weights3thOrder = new double[]{1.0 / 12, 3.0 / 12, 8.0 / 12};
        private static readonly double[] Weights4thOrder = new double[]{1.0 / 24, 2.0 / 24, 6.0 / 24, 15.0 / 24};

        private readonly double[] weights;
        private readonly List<double> levelBuffer;

        public static LinearValueSmoother Create2ndOrderSmoother()
        {
            return new LinearValueSmoother(Weights2ndOrder);
        }

        public static LinearValueSmoother Create3thOrderSmoother()
        {
            return new LinearValueSmoother(Weights3thOrder);
        }

        public static LinearValueSmoother Create4thOrderSmoother()
        {
            return new LinearValueSmoother(Weights4thOrder);
        }

        private LinearValueSmoother(double[] weights_)
        {
            Debug.Assert(weights_ != null && weights_.Length > 1);

            this.weights = weights_;
            this.levelBuffer = new List<double>();
            for(int i = 0; i < this.weights.Length; ++i)
            {
                this.levelBuffer.Add(0);
            }
        }

        public double GetSmoothValue(double level)
        {
            this.levelBuffer.RemoveAt(0);
            this.levelBuffer.Add(level);

            level = 0;
            for (int i = 0; i < weights.Length; ++i )
            {
                level += levelBuffer[i] * weights[i];
            }

            return level;
        }

        public void Clear()
        {
            for(int i = 0; i < levelBuffer.Count; ++i)
            {
                this.levelBuffer[i] = 0.0;
            }
        }
    }
}
