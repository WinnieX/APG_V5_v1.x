using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Effectors.VKxDM.Modules;
using AEC.Common;

namespace AEC.Effectors.VKxDM
{
    public class VKxDMEffectorModel : EffectorModelBase
    {
        public static VKxDMEffectorModel tempModel;
        public static VKxDMEffectorModel GetTempModel()
        {
            if (tempModel == null)
            {
                tempModel = new VKxDMEffectorModel();
            }

            return tempModel;
        }

        public readonly MusicModule music;
        public readonly MicModule micA;
        public readonly MicModule micB;
        public readonly EchoModule echo;
        public readonly ReverbModule reverb;
        public readonly StereoOutputModule mainOutput;
        public readonly MonoOutputModule aux1Output;
        public readonly MonoOutputModule aux2Output;
        public readonly StereoOutputModule surOutput;

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

        public VKxDMEffectorModel(EventBus effectorEvent_ = null, MAudioEffectorModel effectorModel_ = null)
        {
            this.music = new MusicModule(effectorEvent_);
            this.micA = new MicAModule(effectorEvent_);
            this.micB = new MicBModule(effectorEvent_);
            this.reverb = new ReverbModule(effectorEvent_);
            this.echo = new EchoModule(effectorEvent_, this.reverb);
            this.mainOutput = new MainOutputModule(effectorEvent_, this.music.autoWorkMode);
            this.aux1Output = new Aux1OutputModule(effectorEvent_, this.music.autoWorkMode);
            this.aux2Output = new Aux2OutputModule(effectorEvent_, this.music.autoWorkMode);
            this.surOutput = new SurOutputModule(effectorEvent_, this.music.autoWorkMode);
            base.system = new SystemModule(effectorEvent_);

            base.nonSystemModules = new ModuleBase[]
            {
                music, micA, micB, echo, reverb, mainOutput, aux1Output, aux2Output, surOutput,
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
            VKxDMEffectorModel other = otherEffect as VKxDMEffectorModel;

            System.Diagnostics.Debug.Assert(other != null);

            //AEC.Tools.LogHelper.WriteInfo("otherEffect deviceMode:{0}, currEffect:{1}", other.deviceMode, this.deviceMode);
            music.CopyFrom(other.music);
            micA.CopyFrom(other.micA);
            micB.CopyFrom(other.micB);
            echo.CopyFrom(other.echo);
            reverb.CopyFrom(other.reverb);
            mainOutput.CopyFrom(other.mainOutput);
            aux1Output.CopyFrom(other.aux1Output);
            aux2Output.CopyFrom(other.aux2Output);
            surOutput.CopyFrom(other.surOutput);
            system.CopyFrom(other.system, isCopyAllSystem);
            system.dataIsPrivate = other.dataIsPrivate;
        }

        public void CopyFrom(X5.X5EffectorModel x5Effector, bool isCopyAllSystem = false)
        {
            music.CopyFrom(x5Effector.music);
            micA.CopyFrom(x5Effector.mic);
            micB.CopyFrom(x5Effector.mic);
            echo.CopyFrom(x5Effector.echo);
            reverb.CopyFrom(x5Effector.reverb);
            mainOutput.CopyFrom(x5Effector.mainOutput);
            aux1Output.CopyFrom(x5Effector.centerOutput);
            aux2Output.CopyFrom(x5Effector.subOutput);
            surOutput.CopyFrom(x5Effector.surOutput);

            system.CopyFromWithoutWorkMode(x5Effector.system, isCopyAllSystem);

            system.musicStartVolume.initV = (short)(system.musicStartVolume.v + x5Effector.system.musicInputGain.v * 6);
            system.musicMaxVolume.initV = (short)(system.musicMaxVolume.v + x5Effector.system.musicInputGain.v * 6);
        }

        public override byte[] Serialize(string model, bool isContainAllSystemData)
        {
            return VKxDMDataStorage.Effector2Buffer(this, model, isContainAllSystemData);
        }

        protected override void InitFreqResp()
        {
            music.InitFreqResp();
            micA.InitFreqResp();
            micB.InitFreqResp();
            echo.InitFreqResp();
            reverb.InitFreqResp();
            mainOutput.InitFreqResp();
            aux1Output.InitFreqResp();
            aux2Output.InitFreqResp();
            surOutput.InitFreqResp();
        }

        protected override void InitEQGraphic()
        {
            music.InitEQGraphic();
            micA.InitEQGraphic();
            micB.InitEQGraphic();
            echo.InitEQGraphic();
            reverb.InitEQGraphic();
            mainOutput.InitEQGraphic();
            aux1Output.InitEQGraphic();
            aux2Output.InitEQGraphic();
            surOutput.InitEQGraphic();
        }

        public override bool dataIsPrivate
        {
            get
            {
                return music.dataIsPrivate |
                    reverb.dataIsPrivate |
                    mainOutput.dataIsPrivate |
                    aux1Output.dataIsPrivate |
                    aux2Output.dataIsPrivate |
                    surOutput.dataIsPrivate |
                    system.dataIsPrivate;
            }
            set
            {
                short v = value ? (short)1 : (short)0;
                music.dataIsPrivateParam = v;
                reverb.dataIsPrivateParam = v;
                mainOutput.dataIsPrivateParam = v;
                aux1Output.dataIsPrivateParam = v;
                aux2Output.dataIsPrivateParam = v;
                surOutput.dataIsPrivateParam = v;
                system.dataIsPrivate = v != 0;
            }
        }

        public override void InitTempModel()
        {
            VKxDMEffectorModel effectorModel = GetTempModel();
            effectorModel.CopyFrom(this, false);
        }

        public override void UpdateEffecorFeatures(IGetProductFeatures iGetFeatures, DeviceModel model)
        {
            this.system.musicInputPort.max = iGetFeatures.GetSystemMusicMaxInputPort(model);
            this.system.timbreControl.max = iGetFeatures.GetSystemTimbreMax(model);
        }
    }
}
