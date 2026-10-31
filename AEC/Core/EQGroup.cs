using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core.DSP;

namespace AEC.Core
{
    #region EQGroup
    public class EQGroup : MAudioSystem
    {
        public readonly double[] freqList;
        protected readonly short maxEQIndex = 0;
        protected short _currEQIndex = 0;
        //public bool isPass = false;
        //public readonly EQ[] eqList;
        //public readonly FreqResp freqResp = new FreqResp();

        //public string tag = "";

        private BoolParam bypassParam
        {
            get;
            set;
        }

        public bool isBypassParam
        {
            get
            {
                return this.isBypass;
            }
            set
            {
                this.isBypass = value;
                this.UpdateBypassParam();
            }
        }

        public bool eqBypassInit
        {
            get
            {
                return isBypass;
            }
            set
            {
                this.isBypass = value;
                InitBypassParam();
            }
        }

        public virtual bool isBypass
        {
            get;
            set;
        }

        public EQ[] eqList
        {
            get;
            protected set;
        }

        public EQGroup(double[] freqs, bool initEQList = true)
        {
            Debug.Assert(freqs != null && freqs.Length > 0);
            this.freqList = freqs;
            this.maxEQIndex = (short)(freqs.Length - 1);
            this.isBypass = false;

            if (initEQList)
            {
                this.eqList = InitEQList(freqs);
            }
            //this.tag = tag_;
        }

        public void BindingBypassParam(BoolParam bypassParam_)
        {
            Debug.Assert(bypassParam_ != null);

            this.bypassParam = bypassParam_;

            this.InitBypassParam();
        }

        public void InitBypassParam()
        {
            if (this.bypassParam != null)
            {
                this.bypassParam.initV = DataConverter.Bool2Short(this.isBypass);
            }
        }

        private void UpdateBypassParam()
        {
            if (this.bypassParam != null)
            {
                this.bypassParam.v = DataConverter.Bool2Short(this.isBypass);
            }
        }
        //public EQGroup(double[] freqs)
        //{
        //    Debug.Assert(freqs != null && 
        //                freqs.Length > 0);

        //    this.freqList = freqs;
        //    this.maxEQIndex = (short)(freqs.Length - 1);
        //}

        public void CopyFrom(EQGroup other, bool isCopyFreqResp = false)
        {
            //Debug.Assert(this.freqList.Length == other.freqList.Length);
            Debug.Assert(other != null);

            this._currEQIndex = AEC.Core.Utils.ConstrainMax(other._currEQIndex, (short)this.eqList.Length);
            this.isBypass = other.isBypass;

            //如果目标EQ段数大于源EQ段数，则需要先复位目标EQ组
            if(this.eqList.Length > other.eqList.Length)
            {
                EQListReset(this.eqList, this.freqList);
            }

            for (int i = 0; i < this.eqList.Length && i < other.eqList.Length; ++i)
            {
                eqList[i].CopyFrom(other.eqList[i], isCopyFreqResp);
            }

            this.InitBypassParam();

            if (isCopyFreqResp)
            {
                this.freqResp.CopyFrom(other.freqResp);
            }
        }

        public bool IsEqual(EQGroup other)
        {
            if (this.isBypass != other.isBypass)
            {
                return false;
            }

            if (this.eqList.Length != other.eqList.Length)
            {
                return false;
            }

            for (int i = 0; i < this.eqList.Length && i < other.eqList.Length; ++i)
            {
                if (!eqList[i].IsEqual(other.eqList[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public virtual void Reset()
        {
            EQListReset(this.eqList, this.freqList);

            this.ResetFreqResp();
        }

        public virtual void ResetData()
        {
            this.Reset();
            this.isBypass = false;
            this._currEQIndex = 0;
            this.InitBypassParam();
        }

        public void InitFreqResp()
        {
            base.CheckFreqResp();

            Debug.Assert(this.freqResp != null);

            this.ResetFreqResp();

            for (int i = 0; i < this.eqList.Length; ++i)
            {
                DSPHelper.UpdateEQFreqResp(this.eqList[i]);
                if (!this.eqList[i].freqRespIsZero)
                {
                    AddSingleFreqResp(this.eqList[i]);
                }
            }
        }

        public void UpdateEQFreqResp(EQ eq)
        {
            SubtractSingleFreqResp(eq);
            DSPHelper.UpdateEQFreqResp(eq);
            if (!eq.freqRespIsZero)
            {
                AddSingleFreqResp(eq);
            }
        }

        private void SubtractSingleFreqResp(EQ eq)
        {
            freqResp.Subtract(eq.freqResp);
            //for (int i = 0; i < FreqResp.keyFreqListLength; ++i)
            //{
            //    freqResp.amps[i] -= eq.freqResp.amps[i];
            //}
        }

        private void AddSingleFreqResp(EQ eq)
        {
            freqResp.Add(eq.freqResp);
            //for (int i = 0; i < FreqResp.keyFreqListLength; ++i)
            //{
            //    freqResp.amps[i] += eq.freqResp.amps[i];
            //}
        }

        //public FreqResp freqResp
        //{
        //    get
        //    {
        //        if (this.isPass)
        //        {
        //            return FreqResp.zeroFreqResp;
        //        }
        //        else
        //        {
        //            return _freqResp;
        //        }
        //    }
        //}

        public EQ currEQ
        {
            get
            {
                return this.eqList[this._currEQIndex];
            }
        }

        public short currEQIndex
        {
            get
            {
                return this._currEQIndex;
            }
            set
            {
                this._currEQIndex = Utils.ConstrainMax(value, (short)(this.eqList.Length - 1));
            }
        }

        public short eqPassIndex
        {
            get
            {
                return DataConverter.Bool2Short(this.isBypass);
            }
            set
            {
                this.isBypass = DataConverter.Short2Bool(value);
            }
        }

        public int eqBandCount
        {
            get
            {
                return this.eqList.Length;
            }
        }

        private static EQ[] InitEQList(double[] freqList)
        {
            Debug.Assert(freqList != null && freqList.Length > 0);
            EQ[] eqList = new EQ[freqList.Length];
            for (short i = 0; i < freqList.Length; ++i)
            {
                eqList[i] = EQ.Create(i);
                //eqList[i].groupTag = groupTag;
                eqList[i].freqIndex = (short)DataUtils.Freq2EQIndex(freqList[i]);
            }
            return eqList;
        }

        private static void EQListReset(EQ[] eqList, double[] freqList)
        {
            Debug.Assert(freqList != null && freqList.Length > 0 && eqList.Length == freqList.Length);
            for (int i = 0; i < eqList.Length; ++i)
            {
                eqList[i].ResetEQAndFreqResp((short)DataUtils.Freq2EQIndex(freqList[i]));
            }
        }

#if COMM_TEST
        public void RandomTest()
        {
            foreach (EQ eq in this.eqList)
            {
                eq.RandomTest();
            }

            this._currEQIndex = (short)EQ.random.Next(maxEQIndex);
        }
#endif
    }
    #endregion
}
