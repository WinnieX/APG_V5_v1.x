using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public class DeqModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 80.3, 1100.0, 15100.0 };

        public readonly DEQGroup _deqGroup;

        public DEQGroup deqGroup
        {
            get
            {
                return _deqGroup;
            }
        }

        public DeqModule(byte index, EventBus effectorEvent_):base(index, effectorEvent_)
        {
            this._deqGroup = new DEQGroup(initFreqList);
        }

        public void CopyFrom(DeqModule other)
        {
            this._deqGroup.CopyFrom(other._deqGroup);
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this._deqGroup.InitFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            this.freqResp.CopyFrom(eqGroup.freqResp);
        }

        public override EQGroup eqGroup
        {
            get
            {
                return this._deqGroup;
            }
        }

        public override void ResetData()
        {
            //000
            this._deqGroup.ResetData();
        }
    }

    public class MusicDeqModule : DeqModule
    {
        public MusicDeqModule(EventBus effectorEvent_):base(MXCommProtocol.musicDeqModuleIndex, effectorEvent_)
        {
            _themeColor = Color.FromArgb(211, 14, 14);
        }
    }

    public class MicDeqModule : DeqModule
    {
        public MicDeqModule(EventBus effectorEvent_)
            : base(MXCommProtocol.micDeqModuleIndex, effectorEvent_)
        {
            _themeColor = Color.FromArgb(230, 81, 0);
        }
    }
}
