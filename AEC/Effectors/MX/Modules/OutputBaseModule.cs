using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public abstract class OutputBaseModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 80.3, 191.0, 463.0, 1100.0, 2620.0, 6350.0, 15100.0 };

        public const short MaxLevel = 100;        
        public const short MaxDelay = 500;
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

        public readonly MXCompressor compressor;

        protected BoolParam eqBypassParam;

        public readonly MusicModule music;

        public readonly OutputChannelType channel;

        public abstract bool outputIsMute
        {
            get;
            set;
        }

        public OutputBaseModule(OutputChannelType outputChannel, byte index, EventBus effectorEvent_, MusicModule music_) :
            base(index:index, effectorEvent_:effectorEvent_)
        {
            this.channel = outputChannel;

            this.musicLevel = new OutputLevel(
                this.moduleIndex,
                (byte)MXCommProtocol.OutputBaseParamAddr.musicSingLevel,
                this.OnParamChanged,
                MaxLevel);
            this.micLevel = new OutputLevel(
                this.moduleIndex,
                (byte)MXCommProtocol.OutputBaseParamAddr.micSingLevel,
                this.OnParamChanged,
                MaxLevel);
            this.echoLevel = new OutputLevel(
                this.moduleIndex,
                (byte)MXCommProtocol.OutputBaseParamAddr.echoSingLevel,
                this.OnParamChanged,
                MaxLevel);
            this.reverbLevel = new OutputLevel(
                this.moduleIndex,
                (byte)MXCommProtocol.OutputBaseParamAddr.reverbSingLevel,
                this.OnParamChanged,
                MaxLevel);

            this.highPassFilter = XPFilterFactory.CreateClassicalXoverHighPassFilter();
            this.lowPassFilter = XPFilterFactory.CreateClassicalXoverLowPassFilter();
            this.hpfTypeParam = new ShortParam(this.moduleIndex, (byte)MXCommProtocol.OutputBaseParamAddr.hpfMode, this.highPassFilter.typeMaxIndex, this.OnParamChanged);
            this.hpfFreqParam = new ShortParam(this.moduleIndex, (byte)MXCommProtocol.OutputBaseParamAddr.hpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.hpfQParam = new ShortParam(this.moduleIndex, (byte)MXCommProtocol.OutputBaseParamAddr.hpfQ, this.highPassFilter.qMaxIndex, this.OnParamChanged);
            this.lpfTypeParam = new ShortParam(this.moduleIndex, (byte)MXCommProtocol.OutputBaseParamAddr.lpfMode, this.lowPassFilter.typeMaxIndex, this.OnParamChanged);
            this.lpfFreqParam = new ShortParam(this.moduleIndex, (byte)MXCommProtocol.OutputBaseParamAddr.lpfFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            this.lpfQParam = new ShortParam(this.moduleIndex, (byte)MXCommProtocol.OutputBaseParamAddr.lpfQ, this.lowPassFilter.qMaxIndex, this.OnParamChanged);

            this.compressor = new MXCompressor(this.moduleIndex, (byte)MXCommProtocol.OutputBaseParamAddr.attack, this.OnParamChanged);

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
            this.compressor.CopyFrom(other.compressor);
        }

        protected virtual void InitOutputEQGroupFreqResp()
        {
            this.eqGroup.InitFreqResp();
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this.InitOutputEQGroupFreqResp();

            this.highPassFilter.UpdateFreqResp();
            this.lowPassFilter.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupAddHLPFilterFreqResp(this.freqResp, eqGroup, lpf, hpf);
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

            this.compressor.Reset();
        }
    }
}
