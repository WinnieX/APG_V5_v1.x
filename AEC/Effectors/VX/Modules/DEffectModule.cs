using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VXComm;

namespace AEC.Effectors.VX.Modules
{
    public class DEffectModule : ModuleBase
    {
        public const short MaxAttack = 989;
        public const short MaxRelease = 30;
        public const short MaxRatio = 40;
        public const short MaxDGain = 6;

        public const short DefThreshold = 6;
        public const short DefGain = 0;

        public ShortParam attack = null;
        public ShortParam release = null;
        public ShortParam threshold = null;
        public ShortParam ratio = null;
        public ShortParam dGain = null;

        public DEffectModule(EventBus effectorEvent_) :
            base(VXCommProtocol.dEffectModuleIndex, effectorEvent_)
        {
            attack = new ShortParam(moduleIndex, (byte)VXCommProtocol.DEffectParamAddr.attack, MaxAttack);
            release = new ShortParam(moduleIndex, (byte)VXCommProtocol.DEffectParamAddr.release, MaxRelease);
            threshold = new ShortParam(moduleIndex, (byte)VXCommProtocol.DEffectParamAddr.threshold, ThresholdHelper.DefMaxThreshold, this.OnParamChanged);
            ratio = new ShortParam(moduleIndex, (byte)VXCommProtocol.DEffectParamAddr.ratio, MaxRatio);
            dGain = new ShortParam(moduleIndex, (byte)VXCommProtocol.DEffectParamAddr.dGain, MaxDGain, this.OnParamChanged);

            threshold.initV = ThresholdHelper.DefMaxThreshold;
            dGain.initV = DefThreshold;
        }

        public void CopyFrom(DEffectModule other)
        {
            this.attack.initV = other.attack.v;
            this.release.initV = other.release.v;
            this.threshold.initV = other.threshold.v;
            this.ratio.initV = other.ratio.v;
            this.dGain.initV = other.dGain.v;
        }

        public override void InitFreqResp()
        {
        }

        public override void UpdateFreqResp()
        {  
        }

        protected override void OnParamChanged(object sender, DataParamChangedArgs e)
        {
            //
            //this.commSvc.UpdateDeffectModule();
            this.UpdateModuleData();
            base.OnParamChanged(sender, e);
        }

        private void UpdateModuleData()
        {
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(new UpdateModuleAllDataArgs(this.moduleIndex, VXCommProtocol.GetDEffectModuleData(this)));
            }
        }

        public override void ResetData()
        {
            this.threshold.initV = ThresholdHelper.DefMaxThreshold;
            this.dGain.initV = DefThreshold;
        }
    }
}
