using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core.DSP;

namespace AEC.Core
{
    #region EQ
    public class EQ : MFilterBase
    {
        public static short defGainIndex = DataUtils.defGainIndex;
        public static short defQIndex = DataUtils.defQIndex;
        public static short defSIndex = DataUtils.defSIndex;

        public readonly short index;
        public EQType type = EQType.Peaking;
        private short _freqIndex = 0;
        private short _gainIndex = defGainIndex;
        private short _qIndex = defQIndex;
        private short _sIndex = defSIndex;
        //public readonly FreqResp freqResp = new FreqResp();

        //public string groupTag = string.Empty;

        protected EQ(short eqIndex)
        {
            this.index = eqIndex;
        }

        public static EQ Create(short eqIndex)
        {
            return new EQ(eqIndex);
        }

        public void ResetEQAndFreqResp(short initFreqIndex)
        {
            this.type = EQType.Peaking;
            this._freqIndex = initFreqIndex;
            this._gainIndex = defGainIndex;
            this._qIndex = defQIndex;
            this._sIndex = defSIndex;

            base.ResetFreqResp();
        }

        public void ResetEQ()
        {
            this.type = EQType.Peaking;
            this._gainIndex = defGainIndex;
            this._qIndex = defQIndex;
            this._sIndex = defSIndex;
        }

        public void CopyFrom(EQ other, bool isCopyFreqResp)
        {
            this.type = other.type;
            this._freqIndex = other._freqIndex;
            this._gainIndex = other._gainIndex;
            this._qIndex = other._qIndex;
            this._sIndex = other._sIndex;

            if (isCopyFreqResp && this.freqResp != null)
            {
                this.CopyFreqResp(other);
            }
        }

        public bool IsEqual(EQ other)
        {
            if(this.typeIndex != other.typeIndex ||
                this.freqIndex != other.freqIndex ||
                this.gainIndex != other.gainIndex ||
                this.qIndex_ != other.qIndex_ ||
                this.sIndex_ != other.sIndex_)
            {
                return false;
            }

            return true;
        }

        public override bool freqRespIsZero
        {
            get
            {
                return this._gainIndex == defGainIndex;
            }
        }

        public virtual bool canBeChanged
        {
            get
            {
                return true;
            }
        }

        public bool gainCanBeIncreased
        {
            get
            {
                return this.gainIndex < DataUtils.maxGainIndex;
            }
        }

        public bool gainCanBeDecreased
        {
            get
            {
                return this.gainIndex > 0;
            }
        }

        public bool freqCanBeIncreased
        {
            get
            {
                return this.freqIndex < DataUtils.maxEQFreqIndex;
            }
        }

        public bool freqCanBeDecreased
        {
            get
            {
                return this.freqIndex > 0;
            }
        }

        public bool qCanBeIncreased
        {
            get
            {
                return this.qIndex < this.maxQIndex;
            }
        }

        public bool qCanBeDecreased
        {
            get
            {
                return this.qIndex > 0;
            }
        }

        public short typeIndex
        {
            get
            {
                return (short)this.type;
            }
            set
            {
                this.type = DataConverter.Short2EQType(value);
            }
        }

        public short freqIndex
        {
            get
            {
                return this._freqIndex;
            }
            set
            {
                this._freqIndex = Utils.ConstrainMax(value, max: DataUtils.maxEQFreqIndex);
            }
        }

        public short gainIndex
        {
            get
            {
                return this._gainIndex;
            }
            set
            {
                this._gainIndex = Utils.ConstrainMax(value, max: DataUtils.maxGainIndex);
            }
        }

        public short qIndex_
        {
            get
            {
                return this._qIndex;
            }
            set
            {
                this._qIndex = Utils.ConstrainMax(value, max: DataUtils.maxQIndex);
            }
        }

        public short sIndex_
        {
            get
            {
                return this._sIndex;
            }
            set
            {
                this._sIndex = Utils.ConstrainMax(value, max: DataUtils.maxSIndex);
            }
        }

        public short qIndex
        {
            get
            {
                return this.type == EQType.Peaking ? this.qIndex_ : this.sIndex_;
            }
            set
            {
                if (type == EQType.Peaking)
                {
                    this.qIndex_ = value;
                }
                else
                {
                    this.sIndex_ = value;
                }
            }
        }

        public short maxQIndex
        {
            get
            {
                return this.type == EQType.Peaking ? DataUtils.maxQIndex : DataUtils.maxSIndex;
            }
        }

        public short ConstrainQ(short q_index_)
        {
            if (this.type == EQType.Peaking)
            {
                return Utils.ConstrainMax(q_index_, max: DataUtils.maxQIndex);
            }
            else
            {
                return Utils.ConstrainMax(q_index_, max: DataUtils.maxSIndex);
            }
        }

        public double freq
        {
            get
            {
                return DataUtils.eqFreqList[this._freqIndex];
            }
            set
            {
                this._freqIndex = DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.eqFreqList);
            }
        }

        public double gain
        {
            get
            {
                return DataUtils.eqGainList[this._gainIndex];
            }
            set
            {
                this._gainIndex = DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.eqGainList);
            }
        }

        public double q
        {
            get
            {
                return type == EQType.Peaking ? DataUtils.eqQList[this._qIndex] : DataUtils.eqSList[this._sIndex];
            }
            set
            {
                if (type == EQType.Peaking)
                {
                    this._qIndex = DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.eqQList);
                }
                else
                {
                    this._sIndex = DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.eqSList);
                }
            }
        }

        public string label
        {
            get
            {
                return String.Format("{0}{1}{2}{3}Q:{4}",
                    DataUtils.EQFreq2String(this._freqIndex),
                    System.Environment.NewLine,
                    DataUtils.EQGain2String(this._gainIndex),
                    System.Environment.NewLine,
                    DataUtils.Q2String(this.q));
            }
        }

        public String QIndex2String(short q_index)
        {
            if (this.type == EQType.Peaking)
            {
                return DataUtils.Q2String(DataUtils.eqQList[q_index]);
            }
            else
            {
                return DataUtils.Q2String(DataUtils.eqSList[q_index]);
            }
        }

        public void GainIncrease()
        {
            if(this.gainIndex < DataUtils.maxGainIndex)
            {
                this.gainIndex++;
            }
        }

        public void GainDecrease()
        {
            if (this.gainIndex > 0)
            {
                this.gainIndex--;
            }
        }

        public void FreqIncrease()
        {
            if (this.freqIndex < DataUtils.maxEQFreqIndex)
            {
                this.freqIndex++;
            }
        }

        public void FreqDecrease()
        {
            if (this.freqIndex > 0)
            {
                this.freqIndex--;
            }
        }

        public void QIncrease()
        {
            if (this.qIndex < this.maxQIndex)
            {
                this.qIndex++;
            }
        }

        public void QDecrease()
        {
            if (this.qIndex > 0)
            {
                this.qIndex--;
            }
        }

        public void Change2NextMode()
        {
            switch (this.type)
            {
                case EQType.Peaking:
                    this.type = EQType.LowShelf;
                    break;

                case EQType.LowShelf:
                    this.type = EQType.HighShelf;
                    break;

                case EQType.HighShelf:
                    this.type = EQType.Peaking;
                    break;
            }
        }

        public void Change2PrevMode()
        {
            switch (this.type)
            {
                case EQType.Peaking:
                    this.type = EQType.HighShelf;
                    break;

                case EQType.LowShelf:
                    this.type = EQType.Peaking;
                    break;

                case EQType.HighShelf:
                    this.type = EQType.LowShelf;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("index = {0}, freqIndex = {1}, freq = {2}, gainIndex = {3}, gain = {4}, qIndex = {5}, q = {6}", this.index, this.freqIndex, this.freq, this.gainIndex, this.gain, this.qIndex,  this.q);
        }

#if COMM_TEST
        public readonly static Random random = new Random();
        public void RandomTest()
        {
            this.typeIndex = (short)random.Next((int)(EQType.HighShelf + 1));
            this.freqIndex = (short)random.Next((int)(DataUtils.maxEQFreqIndex + 1));
            this.gainIndex = (short)random.Next((int)(DataUtils.maxGainIndex + 1));
            this.qIndex_ = (short)random.Next((int)(DataUtils.maxQIndex + 1));
            this.sIndex_ = (short)random.Next((int)(DataUtils.maxSIndex + 1));
        }
#endif
    }
    #endregion EQ
}
