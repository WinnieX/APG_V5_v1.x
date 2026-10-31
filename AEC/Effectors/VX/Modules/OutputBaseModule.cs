using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VXComm;

namespace AEC.Effectors.VX.Modules
{
    public abstract class OutputBaseModule : ModuleBase
    {
        public const short MaxDelay = 220;
        public const float DelayStep = 0.1f;
        public const string DelayUnit = "ms";
        public const short maxCompressorThreshold = 36;
        public const short compressorThresholdBase = -22;

        public readonly OutputLevel musicLevel;
        public readonly OutputLevel micLevel;
        public readonly OutputLevel echoLevel;
        public readonly OutputLevel reverbLevel;
        
        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;

        private readonly ShortParam hpfTypeParam;
        private readonly ShortParam hpfFreqParam;
        private readonly ShortParam hpfQParam;
        private readonly ShortParam lpfTypeParam;
        private readonly ShortParam lpfFreqParam;
        private readonly ShortParam lpfQParam;

        public readonly Limiter limiter;

        protected BoolParam eqIsPassParam;

        public readonly EQGroup _eqGroup = null;

        public readonly MusicModule music;

        public readonly OutputChannelType channel;

        public abstract bool outputIsMute
        {
            get;
            set;
        }

        public OutputBaseModule(OutputChannelType outputChannel, byte index, EventBus effectorEvent_, EQGroup eqGroup_, MusicModule music_) :
            base(index:index, effectorEvent_:effectorEvent_)
        {
            this.channel = outputChannel;

            this.musicLevel = new OutputLevel(
                this.moduleIndex,
                (byte)VXCommProtocol.OutputBaseParamAddr.musicSingLevel,
                this.OnParamChanged);
            this.micLevel = new OutputLevel(
                this.moduleIndex,
                (byte)VXCommProtocol.OutputBaseParamAddr.micSingLevel,
                this.OnParamChanged);
            this.echoLevel = new OutputLevel(
                this.moduleIndex,
                (byte)VXCommProtocol.OutputBaseParamAddr.echoSingLevel,
                this.OnParamChanged);
            this.reverbLevel = new OutputLevel(
                this.moduleIndex,
                (byte)VXCommProtocol.OutputBaseParamAddr.reverbSingLevel,
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateClassicalXoverHighPassFilter();
            this.lowPassFilter = XPFilterFactory.CreateClassicalXoverLowPassFilter();
            this.hpfTypeParam = new ShortParam(this.moduleIndex, (byte)VXCommProtocol.OutputBaseParamAddr.hpfMode, this.highPassFilter.typeMaxIndex, this.OnParamChanged);
            this.hpfFreqParam = new ShortParam(this.moduleIndex, (byte)VXCommProtocol.OutputBaseParamAddr.hpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.hpfQParam = new ShortParam(this.moduleIndex, (byte)VXCommProtocol.OutputBaseParamAddr.hpfQ, this.highPassFilter.qMaxIndex, this.OnParamChanged);
            this.lpfTypeParam = new ShortParam(this.moduleIndex, (byte)VXCommProtocol.OutputBaseParamAddr.lpfMode, this.lowPassFilter.typeMaxIndex, this.OnParamChanged);
            this.lpfFreqParam = new ShortParam(this.moduleIndex, (byte)VXCommProtocol.OutputBaseParamAddr.lpfFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            this.lpfQParam = new ShortParam(this.moduleIndex, (byte)VXCommProtocol.OutputBaseParamAddr.lpfQ, this.lowPassFilter.qMaxIndex, this.OnParamChanged);
            
            this.limiter = new Limiter(this.moduleIndex, (byte)VXCommProtocol.OutputBaseParamAddr.attack, this.OnParamChanged);

            this._eqGroup = eqGroup_;

            this.music = music_;

            this.highPassFilter.BindingParams(this.hpfFreqParam, this.hpfTypeParam, this.hpfQParam);
            this.lowPassFilter.BindingParams(this.lpfFreqParam, this.lpfTypeParam, this.lpfQParam);
        }

        public void CopyFrom(OutputBaseModule other)
        {
            this.musicLevel.CopyFrom(other.musicLevel);
            this.micLevel.CopyFrom(other.micLevel);
            this.echoLevel.CopyFrom(other.echoLevel);
            this.reverbLevel.CopyFrom(other.reverbLevel);
            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);
            this.limiter.CopyFrom(other.limiter);
            this._eqGroup.CopyFrom(other._eqGroup);
            this.dataIsPrivateParam = other.dataIsPrivateParam;
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this._eqGroup.InitFreqResp();
            this.highPassFilter.UpdateFreqResp();
            this.lowPassFilter.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupAddHLPFilterFreqResp(this.freqResp, eqGroup, lpf, hpf);
        }

        public override void UpdateXPFilter(XPFilterType xpFilterType, FilterElement filterElement)
        {
            base.UpdateXPFilter(xpFilterType, filterElement);
            if (xpFilterType == XPFilterType.HighPass && filterElement == FilterElement.Freq)
            {
                if (this.limiter.auto)
                {
                    this.limiter.HpfFreqChangedWhenAutoMode(this.highPassFilter.freq);
                }
            }
        }

        public static string GetDelayLabel(short index)
        {
            return string.Format("{0:0.0} {1}", index * DelayStep, DelayUnit);
        }

        public static short CalcDelayIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, DelayUnit);
            double value;
            if (double.TryParse(str, out value))
            {
                short index = (short)Math.Round(value / DelayStep, 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, OutputBaseModule.MaxDelay);
            }

            return -1;
        }

        public override EQGroup eqGroup
        {
            get
            {
                return this._eqGroup;
            }
        }

        public override HighPassFilter hpf
        {
            get
            {
                return this.highPassFilter;
            }
        }

        public override LowPassFilter lpf
        {
            get
            {
                return this.lowPassFilter;
            }
        }

        private bool isSingMode
        {
            get
            {
                return music.workSelect == WorkSelect.Sing;
            }
        }

        private bool isManualMode
        {
            get
            {
                return music.workMode == WorkMode.Manual;
            }
        }

        public Level musicLevelParam
        {
            get
            {
                return isSingMode ? this.musicLevel.singLevel : this.musicLevel.discoLevel;
            }
        }

        public Level micLevelParam
        {
            get
            {
                return isSingMode ? this.micLevel.singLevel : this.micLevel.discoLevel;
            }
        }

        public Level echoLevelParam
        {
            get
            {
                return isSingMode ? this.echoLevel.singLevel : this.echoLevel.discoLevel;
            }
        }

        public Level reverbLevelParam
        {
            get
            {
                return isSingMode ? this.reverbLevel.singLevel : this.reverbLevel.discoLevel;
            }
        }

        public override void ResetData()
        {
            this.musicLevel.Reset();
            this.micLevel.Reset();
            this.echoLevel.Reset();
            this.reverbLevel.Reset();
            this.highPassFilter.Reset();
            this.lowPassFilter.Reset();
            this.limiter.Reset();

            this._eqGroup.ResetData();
        }
    }
}
