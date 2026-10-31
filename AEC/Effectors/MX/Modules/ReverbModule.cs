using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public class ReverbModule : ModuleBase
    {
        public const double MaxHPFFreq = 1000;
        public const double MinLPFFreq = 5990;
        public const short MaxPreDelay = 100;
        public const short MaxDelay = 80;
        public const short MaxDamp = 10;

        public static readonly short maxHPFFreqIndex = DataUtils.Freq2FilterIndex(MaxHPFFreq);
        public static readonly short minLPFFreqIndex = DataUtils.Freq2FilterIndex(MinLPFFreq);
        
        public readonly Level reverbLevel;
        public readonly Level directLevel;
        
        public readonly ReverbParam micHpfFreq;
        public readonly ReverbParam micLpfFreq;
        public readonly ReverbParam musicHpfFreq;
        public readonly ReverbParam musicLpfFreq;

        public readonly Level reverbMusicLevel;

        public readonly ReverbParam delayTime;
        public readonly ReverbParam preDelay;
        public readonly ReverbParam trebleDamp;
        public readonly ReverbParam bassDamp;

        public ReverbModule(EventBus effectorEvent_) :
            base(MXCommProtocol.reverbModuleIndex, effectorEvent_)
        {
            reverbLevel = new Level(
                this.moduleIndex,
                (byte)MXCommProtocol.ReverbParamAddr.reverbMicLevel,
                (byte)MXCommProtocol.ReverbParamAddr.reverbMicLevelPhase, 
                this.OnParamChanged);
            directLevel = new Level(
                this.moduleIndex,
                (byte)MXCommProtocol.ReverbParamAddr.directMicLevel,
                (byte)MXCommProtocol.ReverbParamAddr.directMicLevelPhase, 
                this.OnParamChanged);

            micHpfFreq = new ReverbParam((byte)MXCommProtocol.ReverbParamAddr.reverbMicHPF, maxHPFFreqIndex, this.OnParamChanged);
            micLpfFreq = new ReverbParam((byte)MXCommProtocol.ReverbParamAddr.reverbMicLPF, (short)(DataUtils.maxFilterFreqIndex - minLPFFreqIndex), this.OnParamChanged);
            musicHpfFreq = new ReverbParam((byte)MXCommProtocol.ReverbParamAddr.reverbMusicHPF, maxHPFFreqIndex, this.OnParamChanged);
            musicLpfFreq = new ReverbParam((byte)MXCommProtocol.ReverbParamAddr.reverbMusicLPF, (short)(DataUtils.maxFilterFreqIndex - minLPFFreqIndex), this.OnParamChanged);

            reverbMusicLevel = new Level(
                this.moduleIndex,
                (byte)MXCommProtocol.ReverbParamAddr.reverbMusicLevel,
                (byte)MXCommProtocol.ReverbParamAddr.reverbMusicLevelPhase,
                this.OnParamChanged);

            delayTime = new ReverbParam((byte)MXCommProtocol.ReverbParamAddr.reverbTime, MaxDelay, this.OnParamChanged);
            preDelay = new ReverbParam((byte)MXCommProtocol.ReverbParamAddr.reverbPreDelay, MaxPreDelay, this.OnParamChanged);
            trebleDamp = new ReverbParam((byte)MXCommProtocol.ReverbParamAddr.reverbTrebleDamp, MaxDamp, this.OnParamChanged);
            bassDamp = new ReverbParam((byte)MXCommProtocol.ReverbParamAddr.reverbBassDamp, MaxDamp, this.OnParamChanged);
        }

        public void CopyFrom(ReverbModule other)
        {
            this.reverbLevel.CopyFrom(other.reverbLevel);
            this.directLevel.CopyFrom(other.directLevel);

            this.micHpfFreq.initV = other.micHpfFreq.v;
            this.micLpfFreq.initV = other.micLpfFreq.v;

            this.musicHpfFreq.initV = other.musicHpfFreq.v;
            this.musicLpfFreq.initV = other.musicLpfFreq.v;

            this.reverbMusicLevel.CopyFrom(other.reverbMusicLevel);

            this.delayTime.initV = other.delayTime.v;
            this.preDelay.initV = other.preDelay.v;
            this.trebleDamp.initV = other.trebleDamp.v;
            this.bassDamp.initV = other.bassDamp.v;
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

            this.musicHpfFreq.initV = 0;
            this.musicLpfFreq.initV = 0;

            this.reverbMusicLevel.Reset();

            this.delayTime.initV = 0;
            this.preDelay.initV = 0;
            this.trebleDamp.initV = 0;
            this.bassDamp.initV = 0;
        }
    }
}
