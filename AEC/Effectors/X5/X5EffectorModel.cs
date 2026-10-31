using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Effectors.X5.Modules;
using AEC.Common;

namespace AEC.Effectors.X5
{
    public class X5EffectorModel : EffectorModelBase
    {
        public static X5EffectorModel tempModel;
        public static X5EffectorModel GetTempModel()
        {
            if (tempModel == null)
            {
                tempModel = new X5EffectorModel();
            }

            return tempModel;
        }

        public readonly MusicModule music;
        public readonly MicModule mic;
        public readonly EchoModule echo;
        public readonly ReverbModule reverb;
        public readonly MainOutputModule mainOutput;
        public readonly CenterOutputModule centerOutput;
        public readonly SubOutputModule subOutput;
        public readonly SurOutputModule surOutput;

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
                return this.centerOutput.outputIsMute;
            }
            set
            {
                this.centerOutput.outputIsMute = value;
            }
        }

        public override bool aux2OutputIsMute
        {
            get
            {
                return this.subOutput.outputIsMute;
            }
            set
            {
                this.subOutput.outputIsMute = value;
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

        public X5EffectorModel(EventBus effectorEvent_ = null, MAudioEffectorModel effectorModel_ = null)
        {
            this.music = new MusicModule(effectorEvent_);
            this.mic = new MicModule(effectorEvent_);
            this.reverb = new ReverbModule(effectorEvent_);
            this.echo = new EchoModule(effectorEvent_, this.reverb);
            this.mainOutput = new MainOutputModule(effectorEvent_, music.autoWorkMode);
            this.centerOutput = new CenterOutputModule(effectorEvent_, music.autoWorkMode);
            this.subOutput = new SubOutputModule(effectorEvent_, music.autoWorkMode);
            this.surOutput = new SurOutputModule(effectorEvent_, music.autoWorkMode);
            base.system = new SystemModule(effectorEvent_);

            base.nonSystemModules = new ModuleBase[]
            {
                music, mic, echo, reverb, mainOutput, centerOutput, subOutput, surOutput,
            };

            base.outputModules = new List<ModuleBase>()
            {
                this.mainOutput, 
                this.centerOutput, 
                this.subOutput, 
                this.surOutput,
            };

            InitFreqResp();
        }

        public override void CopyFrom(EffectorModelBase otherEffect, bool isCopyAllSystem = false)
        {
            X5EffectorModel other = otherEffect as X5EffectorModel;

            System.Diagnostics.Debug.Assert(other != null);

            //AEC.Tools.LogHelper.WriteInfo("otherEffect deviceMode:{0}, currEffect:{1}", other.deviceMode, this.deviceMode);
            
            music.CopyFrom(other.music);
            mic.CopyFrom(other.mic);
            echo.CopyFrom(other.echo);
            reverb.CopyFrom(other.reverb);
            mainOutput.CopyFrom(other.mainOutput);
            centerOutput.CopyFrom(other.centerOutput);
            subOutput.CopyFrom(other.subOutput);
            surOutput.CopyFrom(other.surOutput);
            system.CopyFrom(other.system, isCopyAllSystem);
            system.dataIsPrivate = other.dataIsPrivate;
        }

        public override byte[] Serialize(string model, bool isContainAllSystemData)
        {
            return X5DataStorage.Effector2Buffer(this, model, isContainAllSystemData);
        }

        protected override void InitFreqResp()
        {
            music.InitFreqResp();
            mic.InitFreqResp();
            echo.InitFreqResp();
            reverb.InitFreqResp();
            mainOutput.InitFreqResp();
            centerOutput.InitFreqResp();
            subOutput.InitFreqResp();
            surOutput.InitFreqResp();
        }

        protected override void InitEQGraphic()
        {
            music.InitEQGraphic();
            mic.InitEQGraphic();
            echo.InitEQGraphic();
            reverb.InitEQGraphic();
            mainOutput.InitEQGraphic();
            centerOutput.InitEQGraphic();
            subOutput.InitEQGraphic();
            surOutput.InitEQGraphic();
        }

        public override void InitTempModel()
        {
            X5EffectorModel effectorModel = GetTempModel();
            effectorModel.CopyFrom(this, false);
        }

        public override void UpdateEffecorFeatures(IGetProductFeatures iGetFeatures, DeviceModel model)
        {
            this.system.musicInputPort.max = iGetFeatures.GetSystemMusicMaxInputPort(model);
            this.system.timbreControl.max = iGetFeatures.GetSystemTimbreMax(model);
        }
    }
}
