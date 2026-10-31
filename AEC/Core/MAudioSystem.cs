using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core.DSP;

namespace AEC.Core
{
    //带频率响应的系统基类，所有含有频率响应属性的类都应从此继承
    public abstract class MAudioSystem
    {
        public FreqResp freqResp
        {
            get;
            private set;
        }

        public void CheckFreqResp()
        {
            if (this.freqResp == null)
            {
                this.freqResp = new FreqResp();
            }
        }

        protected void ResetFreqResp()
        {
            if (this.freqResp != null)
            {
                this.freqResp.Reset();
            }
        }

        protected void CopyFreqResp(MAudioSystem other)
        {
            if (this.freqResp != null && other.freqResp != null)
            {
                this.freqResp.CopyFrom(other.freqResp);
            }
        }
    }
}
