using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Comm;
using AEC.Common;
using AEC.Effectors;
using AEC.Effectors.VX;
using AEC.Misc;
using AEC.Tools;

namespace AEC.Comm.VXComm
{
    public class VXCommService : AbstractCommService
    {
        private readonly VXEffectorModel effector;
        private readonly VXEffectorModel tempEffector = new VXEffectorModel();

        public VXCommService(VXEffectorModel effector_, CommServiceBase commSvc_) :
            base(commSvc_)
        {
            this.effector = effector_;
        }

        public override bool InitCommKey()
        {
            if (AppConfig.isEncryptCommunication)
            {
                return base.commSvc.InitRandKey();
            }
            else
            {
                return true;
            }
        }

        public override void LoadDataCompleted(CommProcess commProcess)
        {
            base.commSvc.commEvent.NotifyObeserver(new LoadDataCompletedArgs(commProcess, tempEffector));
        }

        public override void CallDeviceModeCompleted(CommProcess commProcess)
        {
            base.commSvc.commEvent.NotifyObeserver(new CallDeviceModeCompletedArgs(commProcess, tempEffector));
        }

        public override void UploadDataCompleted()
        {
            base.commSvc.commEvent.NotifyObeserver(new UploadDataCompletedArgs(tempEffector));
        }

        public override void UpdateTempEffecorModelFeatures(IGetProductFeatures iGetFeatures, DeviceModel model)
        {
            this.tempEffector.UpdateEffecorFeatures(iGetFeatures, model);
        }

        public override SystemModuleBase GetSystemModule()
        {
            return this.tempEffector.system;
        }

        public override byte GetModuleEQAddr(byte moduleIndex)
        {
            if (VXCommProtocol.eqAddrDict.ContainsKey(moduleIndex))
            {
                return VXCommProtocol.eqAddrDict[moduleIndex];
            }
            else
            {
                throw new Exception("GetModuleEQAddr()...moduleIndex超出索引");
            }
        }

        public override byte GetModuleDEQParamAddr(DeqParamType paramType)
        {
            throw new Exception("VXCommService.GetModuleDEQAddr()...V系列不存在DEQ参数!");
        }

        public override bool LoadNonSystemModuleData(CommProcess commProcess)
        {
            if (commProcess == CommProcess.ProModeConnectLoadData || commProcess == CommProcess.ProModeCallDeviceMode)
            {
                foreach (ModuleBase module in this.tempEffector.nonSystemModules)
                {
                    //short moduleIndex = module.moduleIndex;
                    string name = VXCommProtocol.GetModuleName(module.moduleIndex);

                    base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_LoadXModuleData], name));

                    byte[] cmd = this.commProtocol.GetReadModuleCmd(module.moduleIndex);

                    base.commSvc.SyncSendCmdAndRetransmission(cmd, CommServiceBase.LoadNonSysteModuleWaitTime, true);

                    if (!VXCommProtocol.ParseNonSystemModuleData(module.moduleIndex, base.commSvc.decryptedReceivedData, tempEffector))
                    {
                        Tools.LogHelper.WriteInfo("VXCommService.ParseNonSystemModuleData()... Module index: {0} Failed!", module.moduleIndex);
                        base.commSvc.UpdateCommFailedProgress();
                        return false;
                    }
                    else
                    {
                        base.commSvc.UpdateCommSuccessProgress(GetLoadNonSysteModuleDataProgressIncrease(commProcess));
                    }
                }
            }
            else if (commProcess == CommProcess.EasyModeConnectLoadData || commProcess == CommProcess.EasyModeCallDeviceMode)
            {
                ModuleBase module = this.tempEffector.music;
                string name = VXCommProtocol.GetModuleName(module.moduleIndex);

                base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_LoadXModuleData], name));

                byte[] cmd = this.commProtocol.GetReadModuleCmd(module.moduleIndex);

                base.commSvc.SyncSendCmdAndRetransmission(cmd, CommServiceBase.LoadNonSysteModuleWaitTime, true);

                if (!VXCommProtocol.ParseNonSystemModuleData(module.moduleIndex, base.commSvc.decryptedReceivedData, tempEffector))
                {
                    Tools.LogHelper.WriteInfo("ParseNonSystemModuleData()... Module index: {0} Failed!", module.moduleIndex);
                    base.commSvc.UpdateCommFailedProgress();
                    return false;
                }
                else
                {
                    base.commSvc.UpdateCommSuccessProgress(GetLoadNonSysteModuleDataProgressIncrease(commProcess));
                }
            }

            return true;
        }

        public override bool UploadNonSystemModuleData(CommProcess commProcess, float progressRatio = 1.0f)
        {
            LogHelper.WriteDebug("VXCommService.UploadNonSystemModuleData()...");
            foreach (ModuleBase module in this.tempEffector.nonSystemModules)
            {
                string name = VXCommProtocol.GetModuleName(module.moduleIndex);

                LogHelper.WriteDebug("VXCommService.UploadNonSystemModuleData()...Update \" {0} \" Module Data", name);

                base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_UploadXModuleData], name));

                List<short> dataList = VXCommProtocol.GetNonSystemModuleData(module.moduleIndex, this.tempEffector);
                byte[] cmd = this.commProtocol.GetWriteNonSysteModuleCmd(module.moduleIndex, dataList);

                base.commSvc.SyncSendCmdAndRetransmission(cmd, CommServiceBase.UploadNonSysteModuleWaitTime, true);

                if (!CommProtocolBase.CheckWriteResponseIsOk(base.commSvc.recvBuffer))
                {
                    //Log.OutputLine("CheckWriteResponseIsOk...失败!");
                    base.commSvc.UpdateCommFailedProgress();
                    return false;
                }
                else
                {
                    base.commSvc.UpdateCommSuccessProgress(GetUploadNonSysteModuleDataProgressIncrease(commProcess) * progressRatio);
                    //Log.OutputLine("CheckWriteResponseIsOk...成功!");
                }
            }

            //return false;
            return true;
        }

        //上传预设模式数据
        public override bool UploadMultiPresetData(object state)
        {
            List<MDeviceMode> deviceModeList = state as List<MDeviceMode>;
            if (deviceModeList == null || deviceModeList.Count == 0)
            {
                return false;
            }

            float progressRatio = 1.0f / deviceModeList.Count;
            //float progressRatio = 0.5f;
            for (int i = deviceModeList.Count - 1; i >= 0; --i)
            {
                MDeviceMode mode = deviceModeList[i];
                this.tempEffector.CopyFrom(mode.effector);

                base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_UpdateXModeData], mode.modeName), isEnd: true);

                //AEC.Log.OutputLine("UploadNonSystemModuleData()...");
                if (!UploadNonSystemModuleData(CommProcess.EasyModeUploadMultiModeData, progressRatio))
                {
                    //AEC.Log.OutputLine("UploadNonSystemModuleData()...失败!");
                    base.commSvc.UpdateUploadDataFailedProgress();
                    base.commSvc.OnCommFailed();
                    return false;
                }

                //AEC.Log.OutputLine("UploadSysteModuleData()...");
                if (!base.commSvc.UploadSysteModuleData(CommProcess.EasyModeUploadMultiModeData, progressRatio))
                {
                    base.commSvc.UpdateUploadDataFailedProgress();
                    base.commSvc.OnCommFailed();
                    return false;
                }
                //AEC.Log.OutputLine("SaveDeviceMode()...");

                base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_SaveXDataToYMode], mode.modeName, i + 1));

                byte[] cmd = this.commProtocol.GetSaveDeviceModeCmd((short)i, mode.modeName);

                base.commSvc.SyncSendCmdAndRetransmission(cmd, CommProtocolBase.WriteDeviceModeWaitTime, true);

                if (!CommProtocolBase.CheckWriteResponseIsOk(base.commSvc.recvBuffer))
                {
                    base.commSvc.UpdateUploadDataFailedProgress();
                    base.commSvc.OnCommFailed();
                    return false;
                }
                else
                {
                    base.commSvc.UpdateCommSuccessProgress(0.25f * progressRatio);
                }
            }

            if (!base.commSvc.UpdateDeviceModeCall(CommProcess.EasyModeUploadMultiModeData, 0))
            {
                base.commSvc.OnCommFailed();
                base.commSvc.UpdateCallDeviceModeFailedProgress();
                return false;
            }

            this.effector.system.CopyFrom(tempEffector.system, false);
            this.effector.system.dataIsPrivate = deviceModeList[0].effector.system.dataIsPrivate;
            this.effector.system.deviceModeCallIndex.v = 0;
            this.effector.system.deviceModeSaveIndex.v = 0;

            return true;
        }

        public override void UpdateToBeUploadedData(EffectorModelBase effectorModel)
        {
            this.tempEffector.CopyFrom(effectorModel, false);
        }

        public override SystemParamComment[] GetSystemParamsForConnectAndLoad()
        {
            List<SystemParamComment> list = new List<SystemParamComment>(base.GetSystemParamsForConnectAndLoad());
            int insertIndex = list.Count > 9 ? 9 : 0;   //注:
            list.InsertRange(insertIndex, new SystemParamComment[]
                {
                    new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicMute, localization[Localization.comm_MusicMute]),
                    new SystemParamComment(SystemCommProtocol.SystemParamAddr.micMute, localization[Localization.comm_MicMute]),
                    new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectMute, localization[Localization.comm_EffectMute]),
                }
            );

            return list.ToArray();
        }

        public override bool ParseSystemStatusData(byte[] data)
        {
            QuadChannelLimiterStatus qcls = VXCommProtocol.ParseOutputLimiterStatusData(data);

            if (qcls != null)
            {
                base.commSvc.commEvent.NotifyObeserver(new OutputLimiterStatusLoadedArgs(qcls));
            }

            return qcls != null;
        }

        protected float GetLoadNonSysteModuleDataProgressIncrease(CommProcess commProcess)
        {
            switch (commProcess)
            {
                case CommProcess.ProModeConnectLoadData:
                    return 0.06f;

                case CommProcess.ProModeCallDeviceMode:
                    return 0.07f;

                case CommProcess.EasyModeConnectLoadData:
                    return 0.132f;

                case CommProcess.EasyModeCallDeviceMode:
                    return 0.16f;
            }

            return 0.0f;
        }

        protected float GetUploadNonSysteModuleDataProgressIncrease(CommProcess commProcess)
        {
            switch (commProcess)
            {
                case CommProcess.ProModeUploadData:
                    return 0.1f;

                case CommProcess.EasyModeUploadMultiModeData:
                    return 0.07f;
            }

            return 0.0f;
        }
    }
}
