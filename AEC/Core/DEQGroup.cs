using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core
{
    #region DEQGroup
    public class DEQGroup : EQGroup
    {
        public readonly DEQ[] deqList;

        public override bool isBypass
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        public DEQGroup(double[] freqs)
            : base(freqs, false)
        {
            this.deqList = InitDEQList(freqs);
            this.eqList = this.deqList;
        }

        public void CopyFrom(DEQGroup other)
        {
            Debug.Assert(base.freqList.Length == other.freqList.Length);
            base._currEQIndex = other._currEQIndex;
            for (int i = 0; i < this.deqList.Length; ++i)
            {
                deqList[i].CopyFrom(other.deqList[i]);
            }
        }

        public override void Reset()
        {
            DEQListReset(this.deqList, this.freqList);

            if (this.freqResp != null)
            {
                freqResp.Reset();
            }
        }

        public DEQ currDEQ
        {
            get
            {
                return this.deqList[this._currEQIndex];
            }
        }

        private static DEQ[] InitDEQList(double[] freqList)
        {
            Debug.Assert(freqList != null && freqList.Length > 0);
            DEQ[] deqList = new DEQ[freqList.Length];
            for (short i = 0; i < freqList.Length; ++i)
            {
                deqList[i] = new DEQ(i);
                deqList[i].freqIndex = (short)DataUtils.Freq2EQIndex(freqList[i]);
            }
            return deqList;
        }

        private static void DEQListReset(DEQ[] deqList, double[] freqList)
        {
            Debug.Assert(freqList != null && freqList.Length > 0 && deqList.Length == freqList.Length);
            for (int i = 0; i < deqList.Length; ++i)
            {
                deqList[i].ResetDEQ((short)DataUtils.Freq2EQIndex(freqList[i]));
            }
        }

        public override void ResetData()
        {
            this.Reset();
            this._currEQIndex = 0;
            foreach (DEQ deq in this.deqList)
            {
                deq.isPass = false;
                deq.deqCM.initV = 0;
                deq.threshold.initV = 0;
                deq.attack.initV = 0;
                deq.release.initV = 0;
                deq.ratio.initV = 0;
                deq.deqDM.initV = 0;
            }

        }
    }
    #endregion
}
