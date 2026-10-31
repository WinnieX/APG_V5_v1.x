using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Effectors.MX.Modules;

namespace AEC.Effectors.MX
{
    public class MXEffectorModel : EffectorModelBase
    {
        public static MXEffectorModel tempModel;
        public static MXEffectorModel GetTempModel()
        {
            if (tempModel == null)
            {
                tempModel = new MXEffectorModel();
            }

            return tempModel;
        }

        public readonly MusicModule music;
        public readonly MicModule mic;
        public readonly DeqModule musicDeq;
        public readonly DeqModule micDeq;
        public readonly EchoModule echo;
        public readonly ReverbModule reverb;
        public readonly OutputModule mainOutput;
        public readonly AuxOutputModule aux1Output;
        public readonly AuxOutputModule aux2Output;
        public readonly OutputModule surOutput;

        public override bool mainOutputIsMute
        {
            get
            {
                return this.mainOutput.outputIsMute;
            }
            set
            {
                this.mainOutput.outputIsMute = value;
            }
        }

        public override bool aux1OutputIsMute
        {
            get
            {
                return this.aux1Output.outputIsMute;
            }
            set
            {
                this.aux1Output.outputIsMute = value;
            }
        }

        public override bool aux2OutputIsMute
        {
            get
            {
                return this.aux2Output.outputIsMute;
            }
            set
            {
                this.aux2Output.outputIsMute = value;
            }
        }

        public override bool surOutputIsMute
        {
            get
            {
                return this.surOutput.outputIsMute;
            }
            set
            {
                this.surOutput.outputIsMute = value;
            }
        }

        public MXEffectorModel(EventBus effectorEvent_ = null, MAudioEffectorModel effectorModel_ = null)
        {
            this.music = new MusicModule(effectorEvent_);
            this.mic = new MicModule(effectorEvent_);
            this.musicDeq = new MusicDeqModule(effectorEvent_);
            this.micDeq = new MicDeqModule(effectorEvent_);
            this.echo = new EchoModule(effectorEvent_);
            this.reverb = new ReverbModule(effectorEvent_);
            this.mainOutput = new MainOutputModule(effectorEvent_, music);
            this.aux1Output = new Aux1OutputModule(effectorEvent_, music);
            this.aux2Output = new Aux2OutputModule(effectorEvent_, music);
            this.surOutput = new SurOutputModule(effectorEvent_, music);
            base.system = new SystemModule(effectorEvent_);

            base.nonSystemModules = new ModuleBase[]
            {
                music, mic, musicDeq, micDeq, echo, reverb, mainOutput, aux1Output, aux2Output, surOutput,
            };

            base.outputModules = new List<ModuleBase>()
            {
                this.mainOutput, 
                this.aux1Output, 
                this.aux2Output, 
                this.surOutput,
            };

            InitFreqResp();
        }

        public override void CopyFrom(EffectorModelBase otherEffect, bool isCopyAllSystem = false)
        {
            MXEffectorModel other = otherEffect as MXEffectorModel;

            System.Diagnostics.Debug.Assert(other != null);

            music.CopyFrom(other.music);
            mic.CopyFrom(other.mic);
            musicDeq.CopyFrom(other.musicDeq);
            micDeq.CopyFrom(other.micDeq);
            echo.CopyFrom(other.echo);
            reverb.CopyFrom(other.reverb);
            mainOutput.CopyFrom(other.mainOutput);
            aux1Output.CopyFrom(other.aux1Output);
            aux2Output.CopyFrom(other.aux2Output);
            surOutput.CopyFrom(other.surOutput);
            system.CopyFrom(other.system, isCopyAllSystem);
        }

        public override byte[] Serialize(string model, bool isContainAllSystemData)
        {
            return MXDataStorage.Effector2Buffer(this, model, isContainAllSystemData);
        }

        protected override void InitFreqResp()
        {
            music.InitFreqResp();
            mic.InitFreqResp();
            musicDeq.InitFreqResp();
            micDeq.InitFreqResp();
            echo.InitFreqResp();
            mainOutput.InitFreqResp();
            aux1Output.InitFreqResp();
            aux2Output.InitFreqResp();
            surOutput.InitFreqResp();
        }

        protected override void InitEQGraphic()
        {
            music.InitEQGraphic();
            mic.InitEQGraphic();
            music.InitEQGraphic();
            mic.InitEQGraphic();
            echo.InitEQGraphic();
            mainOutput.InitEQGraphic();
            aux1Output.InitEQGraphic();
            aux2Output.InitEQGraphic();
            surOutput.InitEQGraphic();
        }

        public void ResetMicModuleNoiseGate()
        {
            this.mic.noiseGate.v = 0;
        }

        public override void InitTempModel()
        {
            MXEffectorModel effectorModel = GetTempModel();
            effectorModel.CopyFrom(this, false);
        }
    }
}
