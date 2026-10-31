using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core.DSP;

namespace AEC.Core
{
    //滤波器基类, 只有一个频率响应数据
    public abstract class MFilterBase : MAudioSystem
    {
        //public DSPHelper dspHelper
        //{
        //    get;
        //    private set;
        //}

        public bool bypass
        {
            get;
            set;
        }

        public short bypassIndex
        {
            get
            {
                return DataConverter.Bool2Short(this.bypass);
            }
            set
            {
                this.bypass = DataConverter.Short2Bool(value);
            }
        }

        public abstract bool freqRespIsZero
        {
            get;
        }

        public bool visible
        {
            get;
            set;
        }

        //public abstract bool ampFreqRespIsZero
        //{
        //    get;
        //}

        //public abstract bool phaseFreqRespIsZero
        //{
        //    get;
        //}

        public MFilterBase()
        {
            this.visible = true;
        }

        //public void BindingDSPHelper(DSPHelper dspHelper_)
        //{
        //    this.dspHelper = dspHelper_;
        //}
    }
}
