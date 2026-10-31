using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.FFTW;

namespace AEC.RTA.Core
{
    public class Averager
    {
        public enum AverageMode
        {
            FIFO1 = 0,
            FIFO2,
            FIFO4,
            FIFO8,
            FIFO16,
            FIFO32,
            FIFO64,
            FIFO128,
            INF,
            FAST,
            SLOW
        }

        public enum FIFOMode
        {
            FIFO1 = 0,
            FIFO2,
            FIFO4,
            FIFO8,
            FIFO16,
            FIFO32,
            FIFO64,
            FIFO128,
        }

        public enum EXPMode
        {
            FAST = 0,
            SLOW,
        }

        public int _length { get; protected set; }
    }

    #region RMS平均
    public class RMSAverager : Averager
    {
        protected RMSAverager(int length)
        {
            _length = length;
        }

        public AverageMode mode { get; protected set; }

        public virtual void Execute(double[] data)
        {

        }

        public static RMSAverager CreateRMSAverager(int data_length, Averager.AverageMode mode)
        {
            RMSAverager averager = null;

            switch (mode)
            {
                case Averager.AverageMode.FIFO1:
                case Averager.AverageMode.FIFO2:
                case Averager.AverageMode.FIFO4:
                case Averager.AverageMode.FIFO8:
                case Averager.AverageMode.FIFO16:
                case Averager.AverageMode.FIFO32:
                case Averager.AverageMode.FIFO64:
                case Averager.AverageMode.FIFO128:
                    Averager.FIFOMode fifoMode = (Averager.FIFOMode)mode;
                    averager = new FIFORMSAverager(data_length, fifoMode);
                    break;

                case Averager.AverageMode.INF:
                    averager = new INFRMSAverager(data_length);
                    break;

                case Averager.AverageMode.FAST:
                    averager = new EXPRMSAverager(data_length, Averager.EXPMode.FAST);
                    break;

                case Averager.AverageMode.SLOW:
                    averager = new EXPRMSAverager(data_length, Averager.EXPMode.SLOW);
                    break;
            }

            return averager;
        }
    }

    public class FIFORMSAverager : RMSAverager
    {
        private int _queueMaxCount = 0;

        private SignalMag first = null;
        private SignalMag last = null;
        private SignalMag sum = null;
        private Queue<SignalMag> queue = null;

        public FIFORMSAverager(int length, FIFOMode fifoMode) :
            base(length)
        {
            int queueMaxCount = 1 << (int)fifoMode;
            _queueMaxCount = queueMaxCount;
            _length = length;
            queue = new Queue<SignalMag>();
            sum = new SignalMag(length);

            mode = (AverageMode)fifoMode;
        }

        public override void Execute(double[] mags)
        {
            System.Diagnostics.Debug.Assert(mags != null && mags.Length > _length);

            if (_queueMaxCount == 1)
            {
                return;
            }

            last = new SignalMag(_length);

            if (queue.Count >= _queueMaxCount)
            {
                first = queue.Dequeue();
                queue.Enqueue(last);

                for (int i = 0; i < _length; ++i)
                {
                    last.mags[i] = mags[i] * mags[i];
                    sum.mags[i] -= first.mags[i];
                    sum.mags[i] += last.mags[i];

                    if (mags[i] > 0)
                    {
                        mags[i] = Math.Sqrt(sum.mags[i] / _queueMaxCount);
                    }
                    else
                    {
                        sum.mags[i] = 0.0;
                        mags[i] = 0.0;

                        foreach (SignalMag signalMags in queue)
                        {
                            signalMags.mags[i] = 0.0;
                        }
                    }
                }
            }
            else
            {
                queue.Enqueue(last);
                int n = queue.Count;
                for (int i = 0; i < _length; ++i)
                {
                    last.mags[i] = mags[i] * mags[i];
                    sum.mags[i] += last.mags[i];
                    mags[i] = Math.Sqrt(sum.mags[i] / n);
                }
            }
        }
    }

    public class INFRMSAverager : RMSAverager
    {
        private UInt64 _count = 0;
        private SignalMag sum = null;

        public INFRMSAverager(int length) :
            base(length)
        {
            sum = new SignalMag(length);

            mode = AverageMode.INF;
        }

        public override void Execute(double[] mags)
        {
            ++_count;
            for (int i = 0; i < _length; ++i)
            {
                sum.mags[i] += mags[i] * mags[i];
                mags[i] = Math.Sqrt(sum.mags[i] / _count);
            }
        }
    }

    public class EXPRMSAverager : RMSAverager
    {
        private UInt64 _count = 0;
        private SignalMag last = null;
        private double _halfLife = 1;        //半衰期
        private double beta = 0;

        public EXPRMSAverager(int length, EXPMode expMode) :
            base(length)
        {
            last = new SignalMag(length);

            switch (expMode)
            {
                case EXPMode.FAST:
                    mode = AverageMode.FAST;
                    _halfLife = 1;
                    break;

                case EXPMode.SLOW:
                    mode = AverageMode.SLOW;
                    _halfLife = 1 / 8.0f;
                    break;
            }

            beta = Math.Pow(0.5, _halfLife);
        }

        public override void Execute(double[] mags)
        {
            if (_count++ > 0)
            {
                for (int i = 0; i < _length; ++i)
                {
                    last.mags[i] = Math.Sqrt(beta * last.mags[i] * last.mags[i] + (1.0 - beta) * mags[i] * mags[i]);
                }
                Buffer.BlockCopy(last.mags, 0, mags, 0, _length * sizeof(double));
            }
            else
            {
                Buffer.BlockCopy(mags, 0, last.mags, 0, _length * sizeof(double));
            }
        }
    }
    #endregion

    #region 立体声RMS 平均
    public class StereoRMSAverager
    {
        private RMSAverager leftAverager = null;
        private RMSAverager rightAverager = null;

        public Averager.AverageMode averageMode { get; private set; }
        public int length { get; private set; }

        public StereoRMSAverager(int data_length, Averager.AverageMode mode)
        {
            length = data_length;
            averageMode = mode;

            leftAverager = RMSAverager.CreateRMSAverager(data_length, mode);
            rightAverager = RMSAverager.CreateRMSAverager(data_length, mode);
        }

        public void Excete(double[] left, double[] right)
        {
            leftAverager.Execute(left);
            rightAverager.Execute(right);
        }
    }
    #endregion
    //*****************

    #region Vector 平均
    public class VectorAverager : Averager
    {
        protected VectorAverager(int length)
        {
            _length = length;
        }

        public AverageMode mode { get; protected set; }

        public virtual void Execute(double[] data)
        {

        }

        public static VectorAverager CreateVectorAverager(int data_length, Averager.AverageMode mode)
        {
            VectorAverager averager = null;

            switch (mode)
            {
                case Averager.AverageMode.FIFO1:
                case Averager.AverageMode.FIFO2:
                case Averager.AverageMode.FIFO4:
                case Averager.AverageMode.FIFO8:
                case Averager.AverageMode.FIFO16:
                case Averager.AverageMode.FIFO32:
                case Averager.AverageMode.FIFO64:
                case Averager.AverageMode.FIFO128:
                    Averager.FIFOMode fifoMode = (Averager.FIFOMode)mode;
                    averager = new FIFOVectorAverager(data_length, fifoMode);
                    break;

                case Averager.AverageMode.INF:
                    averager = new INFVectorAverager(data_length);
                    break;

                case Averager.AverageMode.FAST:
                    averager = new EXPVectorAverager(data_length, Averager.EXPMode.FAST);
                    break;

                case Averager.AverageMode.SLOW:
                    averager = new EXPVectorAverager(data_length, Averager.EXPMode.SLOW);
                    break;
            }

            return averager;
        }
    }

    public class FIFOVectorAverager : VectorAverager
    {
        private int _queueMaxCount = 0;

        private SignalMag first = null;
        private SignalMag last = null;
        private SignalMag sum = null;
        private Queue<SignalMag> queue = null;

        public FIFOVectorAverager(int length, FIFOMode fifoMode) :
            base(length)
        {
            int queueMaxCount = 1 << (int)fifoMode;
            _queueMaxCount = queueMaxCount;
            _length = length;
            queue = new Queue<SignalMag>();
            sum = new SignalMag(length);

            mode = (AverageMode)fifoMode;
        }

        public override void Execute(double[] mags)
        {
            System.Diagnostics.Debug.Assert(mags != null && mags.Length > _length);

            if (_queueMaxCount == 1)
            {
                return;
            }

            last = new SignalMag(_length);

            if (queue.Count >= _queueMaxCount)
            {
                first = queue.Dequeue();
                queue.Enqueue(last);

                for (int i = 0; i < _length; ++i)
                {
                    last.mags[i] = mags[i];
                    sum.mags[i] -= first.mags[i];
                    sum.mags[i] += last.mags[i];
                    mags[i] = sum.mags[i] / _queueMaxCount;
                }
            }
            else
            {
                queue.Enqueue(last);
                int n = queue.Count;
                for (int i = 0; i < _length; ++i)
                {
                    last.mags[i] = mags[i];
                    sum.mags[i] += last.mags[i];
                    mags[i] = sum.mags[i] / n;
                }
            }
        }
    }

    public class INFVectorAverager : VectorAverager
    {
        private UInt64 _count = 0;
        private SignalMag sum = null;

        public INFVectorAverager(int length) :
            base(length)
        {
            sum = new SignalMag(length);

            mode = AverageMode.INF;
        }

        public override void Execute(double[] mags)
        {
            ++_count;
            for (int i = 0; i < _length; ++i)
            {
                sum.mags[i] += mags[i];
                mags[i] = sum.mags[i] / _count;
            }
        }
    }

    public class EXPVectorAverager : VectorAverager
    {
        private UInt64 _count = 0;
        private SignalMag last = null;
        private double _halfLife = 1;        //半衰期
        private double beta = 0;

        public EXPVectorAverager(int length, EXPMode expMode) :
            base(length)
        {
            last = new SignalMag(length);

            switch (expMode)
            {
                case EXPMode.FAST:
                    mode = AverageMode.FAST;
                    _halfLife = 1;
                    break;

                case EXPMode.SLOW:
                    mode = AverageMode.SLOW;
                    _halfLife = 1 / 8.0f;
                    break;
            }

            beta = Math.Pow(0.5, _halfLife);
        }

        public override void Execute(double[] mags)
        {
            if (_count++ > 0)
            {
                for (int i = 0; i < _length; ++i)
                {
                    last.mags[i] = beta * last.mags[i] + (1.0 - beta) * mags[i];
                }
                Buffer.BlockCopy(last.mags, 0, mags, 0, _length * sizeof(double));
            }
            else
            {
                Buffer.BlockCopy(mags, 0, last.mags, 0, _length * sizeof(double));
            }
        }
    }
    #endregion
    //*****************


    //*****************
    #region 复数平均
    public class ComplexAverager : Averager
    {
        protected ComplexAverager(int length)
        {
            _length = length;
        }

        public AverageMode mode { get; protected set; }

        public virtual void Execute(Complex[] signals)
        {

        }

        public static ComplexAverager CreateComplexAverager(int data_length, Averager.AverageMode mode)
        {
            ComplexAverager averager = null;

            switch (mode)
            {
                case Averager.AverageMode.FIFO1:
                case Averager.AverageMode.FIFO2:
                case Averager.AverageMode.FIFO4:
                case Averager.AverageMode.FIFO8:
                case Averager.AverageMode.FIFO16:
                case Averager.AverageMode.FIFO32:
                case Averager.AverageMode.FIFO64:
                case Averager.AverageMode.FIFO128:
                    Averager.FIFOMode fifoMode = (Averager.FIFOMode)mode;
                    averager = new FIFOComplexAverager(data_length, fifoMode);
                    break;

                case Averager.AverageMode.INF:
                    averager = new INFComplexAverager(data_length);
                    break;

                case Averager.AverageMode.FAST:
                    averager = new EXPComplexAverager(data_length, Averager.EXPMode.FAST);
                    break;

                case Averager.AverageMode.SLOW:
                    averager = new EXPComplexAverager(data_length, Averager.EXPMode.SLOW);
                    break;
            }

            return averager;
        }
    }

    public class FIFOComplexAverager : ComplexAverager
    {
        private int _queueMaxCount = 0;

        //private FreqDomainSignal first = null;
        private FreqDomainSignal last = null;
        private FreqDomainSignal sum = null;
        private Queue<FreqDomainSignal> queue = null;

        public FIFOComplexAverager(int length, FIFOMode fifoMode) :
            base(length)
        {
            int queueMaxCount = 1 << (int)fifoMode;
            _queueMaxCount = queueMaxCount;
            _length = length;
            queue = new Queue<FreqDomainSignal>();
            sum = new FreqDomainSignal(length);

            mode = (AverageMode)fifoMode;
        }

        public override void Execute(Complex[] signals)
        {
            System.Diagnostics.Debug.Assert(signals != null && signals.Length >= _length);

            if (_queueMaxCount == 1)
            {
                return;
            }

            last = new FreqDomainSignal(_length);
            Array.Copy(signals, last.signals, _length);
            if (queue.Count >= _queueMaxCount)
            {
                queue.Dequeue();
            }
            queue.Enqueue(last);
            int queueCount = queue.Count;
            for (int i = 0; i < _length; ++i)
            {
                Complex sum = 0;
                foreach (FreqDomainSignal fds in queue)
                {
                    sum += fds.signals[i];
                }
                signals[i].Real = sum.Real / queueCount;
                signals[i].Imaginary = sum.Imaginary / queueCount;
            }

            //last = new FreqDomainSignal(_length);

            //if (queue.Count >= _queneMaxCount)
            //{
            //    first = queue.Dequeue();
            //    queue.Enqueue(last);

            //    Array.Copy(signals, last.signals, _length);

            //    for (int i = 0; i < _length; ++i)
            //    {
            //        //last.signals[i] = signals[i];
            //        sum.signals[i] -= first.signals[i];
            //        sum.signals[i] += last.signals[i];

            //        if (signals[i].Magnitude > 0)
            //        {
            //            signals[i] = sum.signals[i] / _queneMaxCount;
            //        }
            //        else
            //        {
            //            sum.signals[i] = 0.0;
            //            signals[i] = 0.0;
            //        }
            //    }
            //}
            //else
            //{
            //    queue.Enqueue(last);
            //    int n = queue.Count;

            //    Array.Copy(signals, last.signals, _length);

            //    for (int i = 0; i < _length; ++i)
            //    {
            //        //last.signals[i] = signals[i];
            //        sum.signals[i] += last.signals[i];
            //        signals[i] = sum.signals[i] / n;
            //    }
            //}
        }
    }

    public class INFComplexAverager : ComplexAverager
    {
        private UInt64 _count = 0;
        private FreqDomainSignal sum = null;

        public INFComplexAverager(int length) :
            base(length)
        {
            sum = new FreqDomainSignal(length);

            mode = AverageMode.INF;
        }

        public override void Execute(Complex[] signals)
        {
            ++_count;
            for (int i = 0; i < _length; ++i)
            {
                sum.signals[i] += signals[i];
                signals[i] = sum.signals[i] / _count;
            }
        }
    }

    public class EXPComplexAverager : ComplexAverager
    {
        private UInt64 _count = 0;
        private FreqDomainSignal last = null;
        private double _halfLife = 1;        //半衰期
        private double beta = 0;

        public EXPComplexAverager(int length, EXPMode expMode) :
            base(length)
        {
            last = new FreqDomainSignal(length);

            switch (expMode)
            {
                case EXPMode.FAST:
                    mode = AverageMode.FAST;
                    _halfLife = 1;
                    break;

                case EXPMode.SLOW:
                    mode = AverageMode.SLOW;
                    _halfLife = 1 / 8.0f;
                    break;
            }

            beta = Math.Pow(0.5, _halfLife);
        }

        public override void Execute(Complex[] signals)
        {
            if (_count++ > 0)
            {
                for (int i = 0; i < _length; ++i)
                {
                    last.signals[i] = beta * last.signals[i] + (1.0 - beta) * signals[i];
                }
                Array.Copy(last.signals, signals, _length);
            }
            else
            {
                Array.Copy(signals, last.signals, _length);
            }
        }
    }
    #endregion
    //*****************

}
