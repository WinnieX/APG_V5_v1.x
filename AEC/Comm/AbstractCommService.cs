using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Effectors;
using AEC.Common;
using AEC.Misc;


namespace AEC.Comm
{
    public abstract class AbstractCommService
    {
        protected CommServiceBase commSvc;

        protected static Localization localization = Config.GetInstance().localization;

        public virtual CommProtocolBase commProtocol
        {
            get
            {
                return this.commSvc.commProtocol;
            }
        }

        //static AbstractCommService()
        //{
        //    localization = Config.GetInstance().localization;
        
        //    if(localization != null)
        //    {
        //        Console.WriteLine("localization 不为空");
        //    }
        //    else
        //    {
        //        Console.WriteLine("localization 为空");
        //    }
        //}

        public AbstractCommService(CommServiceBase commSvc_)
        {
            this.commSvc = commSvc_;
        }

        public virtual bool InitCommKey()
        {
            //AEC.Log.OutputLine("AbstractCommService.InitCommKey()...");
            return true;
        }

        public abstract void LoadDataCompleted(CommProcess commProcess);

        public abstract void CallDeviceModeCompleted(CommProcess commProcess);

        public abstract void UploadDataCompleted();

        public abstract void UpdateTempEffecorModelFeatures(IGetProductFeatures iGetFeatures, DeviceModel model);
        public abstract SystemModuleBase GetSystemModule();
        public abstract byte GetModuleEQAddr(byte moduleIndex);
        public abstract byte GetModuleDEQParamAddr(DeqParamType paramType);
        public abstract bool LoadNonSystemModuleData(CommProcess commProcess);
        public abstract bool UploadNonSystemModuleData(CommProcess commProcess, float progressRatio = 1.0f);
        public abstract bool UploadMultiPresetData(object state);
        public abstract void UpdateToBeUploadedData(EffectorModelBase effectorModel_);

        public virtual bool ParseSystemStatusData(byte[] data)
        {
            return true;
        }

        public virtual bool LoadCustomDeviceInfo(SystemModuleBase system)
        {
            return true;
        }

        public virtual SystemParamComment[] GetSystemParamsForConnectAndLoad()
        {
            return new SystemParamComment[]
            {
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicStartVolume, localization[Localization.comm_MusicStartVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicMaxVolume, localization[Localization.comm_MusicMaxVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.micStartVolume, localization[Localization.comm_MicStartVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.micMaxVolume, localization[Localization.comm_MicMaxVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectStartVolume, localization[Localization.comm_EffectStartVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectMaxVolume, localization[Localization.comm_EffectMaxVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicVolume, localization[Localization.comm_MusicVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.micVolume, localization[Localization.comm_MicVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectVolume, localization[Localization.comm_EffectVolume]),
                //new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicMute, localization[Localization.comm_MusicMute]),
                //new SystemParamComment(SystemCommProtocol.SystemParamAddr.micMute, localization[Localization.comm_MicMute]),
                //new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectMute, localization[Localization.comm_EffectMute]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicInputGain, localization[Localization.comm_MusicInputGain]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicInputPort, localization[Localization.comm_MusicInputPort]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.systemPassword, localization[Localization.comm_SystemPassword]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.keyboardLockPassword, localization[Localization.comm_KeyboardLockPassword]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.user, localization[Localization.comm_User]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.isLockKeyboard, localization[Localization.comm_IsLockKeyboard]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.isEnableRomoteControl, localization[Localization.comm_IsEnableRomoteControl]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.deviceModeSave, localization[Localization.comm_DeviceModeSave]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.deviceModeCall, localization[Localization.comm_DeviceModeCall]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.deviceId, localization[Localization.comm_DeviceId]),
                //new SystemParamComment(SystemCommProtocol.SystemParamAddr.deviceHardwareState, localization[Localization.comm_DeviceHardwareState]),
                //new SystemParamComment(SystemCommProtocol.SystemParamAddr.deviceName, localization[Localization.comm_DeviceName]),
                //new SystemParamComment(SystemCommProtocol.SystemParamAddr.usageLimitLock, localization[Localization.comm_UsageLimitLockData]),
            };
        }

        public virtual SystemParamComment[] GetSystemParamsForRecall()
        {
            return new SystemParamComment[]
            {
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicStartVolume, localization[Localization.comm_MusicStartVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicMaxVolume, localization[Localization.comm_MusicMaxVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.micStartVolume, localization[Localization.comm_MicStartVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.micMaxVolume, localization[Localization.comm_MicMaxVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectStartVolume, localization[Localization.comm_EffectStartVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectMaxVolume, localization[Localization.comm_EffectMaxVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicVolume, localization[Localization.comm_MusicVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.micVolume, localization[Localization.comm_MicVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectVolume, localization[Localization.comm_EffectVolume]),
                new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicInputPort, localization[Localization.comm_MusicInputPort]),
            };
        }        

        public virtual SystemParamCommentValue[] GetSystemParamsForUpload(SystemModuleBase system)
        {
            return new SystemParamCommentValue[]
            {
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.musicMaxVolume, localization[Localization.comm_MusicMaxVolume], system.musicMaxVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.musicStartVolume, localization[Localization.comm_MusicStartVolume], system.musicStartVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.micMaxVolume, localization[Localization.comm_MicMaxVolume], system.micMaxVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.micStartVolume, localization[Localization.comm_MicStartVolume], system.micStartVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.effectMaxVolume, localization[Localization.comm_EffectMaxVolume], system.effectMaxVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.effectStartVolume, localization[Localization.comm_EffectStartVolume], system.effectStartVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.musicVolume, localization[Localization.comm_MusicVolume], system.musicVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.micVolume, localization[Localization.comm_MicVolume], system.micVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.effectVolume, localization[Localization.comm_EffectVolume], system.effectVolume.v),
                new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.musicInputPort, localization[Localization.comm_MusicInputPort], system.musicInputPort.v),
            };
        }
    }
}
