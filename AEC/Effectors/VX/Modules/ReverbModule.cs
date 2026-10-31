using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VXComm;

namespace AEC.Effectors.VX.Modules
{
    public class ReverbModule : ModuleBase
    {
        public const double MaxHPFFreq = 1000;
        public const double MinLPFFreq = 1000;
        public const short MaxPreDelay = 200;
        public const short MaxDelay = 80;
        public const short MaxDamp = 10;

        public static readonly short maxHPFFreqIndex = DataUtils.Freq2FilterIndex(MaxHPFFreq);
        public static readonly short minLPFFreqIndex = DataUtils.Freq2FilterIndex(MinLPFFreq);
        
        public readonly Level reverbLevel;
        public readonly Level directLevel;
        
        //public readonly HighPassFilter highPassFilter;
        //public readonly LowPassFilter lowPassFilter;

        public readonly ReverbParam micHpfFreq;
        public readonly ReverbParam micLpfFreq;

        public readonly ReverbParam delayTime;
        public readonly ReverbParam preDelay;
        public readonly ReverbParam trebleDamp;
        public readonly ReverbParam bassDamp;

        public ReverbModule(EventBus commEvent_) :
            base(VXCommProtocol.reverbModuleIndex, commEvent_)
        {
            this.reverbLevel = new Level(
                this.moduleIndex,
                (byte)VXCommProtocol.ReverbParamAddr.reverbMicLevel,
                (byte)VXCommProtocol.ReverbParamAddr.reverbMicLevelPhase, 
                this.OnParamChanged);
            this.directLevel = new Level(
                this.moduleIndex,
                (byte)VXCommProtocol.ReverbParamAddr.directMicLevel,
                (byte)VXCommProtocol.ReverbParamAddr.directMicLevelPhase, 
                this.OnParamChanged);

            //highPassFilter = new HighPassFilter(maxHPFFreqIndex);
            //lowPassFilter = new LowPassFilter(minLPFFreqIndex);

            this.micHpfFreq = new ReverbParam((byte)VXCommProtocol.ReverbParamAddr.reverbMicHPF, maxHPFFreqIndex, this.OnParamChanged);
            this.micLpfFreq = new ReverbParam((byte)VXCommProtocol.ReverbParamAddr.reverbMicLPF, (short)(DataUtils.maxFilterFreqIndex - minLPFFreqIndex), this.OnParamChanged);

            this.delayTime = new ReverbParam((byte)VXCommProtocol.ReverbParamAddr.reverbTime, MaxDelay, this.OnParamChanged);
            this.preDelay = new ReverbParam((byte)VXCommProtocol.ReverbParamAddr.reverbPreDelay, MaxPreDelay, this.OnParamChanged);
            this.trebleDamp = new ReverbParam((byte)VXCommProtocol.ReverbParamAddr.reverbTrebleDamp, MaxDamp, this.OnParamChanged);
            this.bassDamp = new ReverbParam((byte)VXCommProtocol.ReverbParamAddr.reverbBassDamp, MaxDamp, this.OnParamChanged);
        }

        public void CopyFrom(ReverbModule other)
        {
            this.reverbLevel.CopyFrom(other.reverbLevel);
            this.directLevel.CopyFrom(other.directLevel);

            //this.highPassFilter.CopyFrom(other.highPassFilter);
            //this.lowPassFilter.CopyFrom(other.lowPassFilter);
            this.micHpfFreq.initV = other.micHpfFreq.v;
            this.micLpfFreq.initV = other.micLpfFreq.v;

            this.delayTime.initV = other.delayTime.v;
            this.preDelay.initV = other.preDelay.v;
            this.trebleDamp.initV = other.trebleDamp.v;
            this.bassDamp.initV = other.bassDamp.v;
            this.dataIsPrivateParam = other.dataIsPrivateParam;
        }

        public override void InitFreqResp()
        {
        }

        public override void UpdateFreqResp()
        {
        }

        public override void ResetData()
        {
            this.reverbLevel.Reset();
            this.directLevel.Reset();

            this.micHpfFreq.initV = 0;
            this.micLpfFreq.initV = 0;

            this.delayTime.initV = 0;
            this.preDelay.initV = 0;
            this.trebleDamp.initV = 0;
            this.bassDamp.initV = 0;
        }

        //public short hpfInit
        //{
        //    get
        //    {
        //        return this.highPassFilter.freqIndex;
        //    }
        //    set
        //    {
        //        this.highPassFilter.freqIndex = value;
        //    }
        //}

        //public short lpfInit
        //{
        //    get
        //    {
        //        return this.lowPassFilter.freqIndex;
        //    }
        //    set
        //    {
        //        this.lowPassFilter.freqIndex = (short)(minLPFFreqIndex + value);
        //    }
        //}
    }
}
