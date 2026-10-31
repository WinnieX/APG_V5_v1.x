using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Comm;
using AEC.Common;
using AEC.Effectors;
using AEC.Effectors.VKxPII;
using AEC.Misc;
using AEC.Tools;

namespace AEC.Comm.VKxPIIComm
{
    public class VKxPIICommService : AbstractCommService
    {
        private readonly VKxPIIEffectorModel effector;
        private readonly VKxPIIEffectorModel tempEffector = new VKxPIIEffectorModel();

        public VKxPIICommService(VKxPIIEffectorModel effector_, CommServiceBase commSvc_) :
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
            if (VKxPIICommProtocol.eqAddrDict.ContainsKey(moduleIndex))
            {
                return VKxPIICommProtocol.eqAddrDict[moduleIndex];
            }
            else
            {
                throw new Exception("GetModuleEQAddr()...moduleIndex超出索引");
            }
        }

        public override byte GetModuleDEQParamAddr(DeqParamType paramType)
        {
            throw new Exception("VKxPIICommService.GetModuleDEQAddr()...V系列不存在DEQ参数!");
        }

        public override bool LoadNonSystemModuleData(CommProcess commProcess)
        {
            if (commProcess == CommProcess.ProModeConnectLoadData || commProcess == CommProcess.ProModeCallDeviceMode || commProcess == CommProcess.ProModeSwitchDeviceWorkMode)
            {
                foreach (ModuleBase module in this.tempEffector.nonSystemModules)
                {
                    //short moduleIndex = module.moduleIndex;
                    string name = VKxPIICommProtocol.GetModuleName(module.moduleIndex);

                    //Log.OutputLine("Load Module : {0}", name);

                    base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_LoadXModuleData], name));

                    byte[] cmd = this.commProtocol.GetReadModuleCmd(module.moduleIndex);

                    base.commSvc.SyncSendCmdAndRetransmission(cmd, CommServiceBase.LoadNonSysteModuleWaitTime, true);

                    if (!VKxPIICommProtocol.ParseNonSystemModuleData(module.moduleIndex, base.commSvc.decryptedReceivedData, tempEffector))
                    {
                        Tools.LogHelper.WriteInfo("KXCommService.ParseNonSystemModuleData()... Module index: {0} Failed!", module.moduleIndex);
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
                string name = VKxPIICommProtocol.GetModuleName(module.moduleIndex);

                base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_LoadXModuleData], name));

                byte[] cmd = this.commProtocol.GetReadModuleCmd(module.moduleIndex);

                base.commSvc.SyncSendCmdAndRetransmission(cmd, CommServiceBase.LoadNonSysteModuleWaitTime, true);

                if (!VKxPIICommProtocol.ParseNonSystemModuleData(module.moduleIndex, base.commSvc.decryptedReceivedData, tempEffector))
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
            LogHelper.WriteDebug("KXCommService.UploadNonSystemModuleData()...");
            foreach (ModuleBase module in this.tempEffector.nonSystemModules)
            {
                string name = VKxPIICommProtocol.GetModuleName(module.moduleIndex);

                LogHelper.WriteDebug("KXCommService.UploadNonSystemModuleData()...Update \" {0} \" Module Data", name);

                base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_UploadXModuleData], name));

                List<short> dataList = VKxPIICommProtocol.GetNonSystemModuleData(module.moduleIndex, this.tempEffector);
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

        public override bool ParseSystemStatusData(byte[] data)
        {
            DeviceStatus deviceStatus = VKxPIICommProtocol.ParseSystemStatusData(data);

            if (deviceStatus != null)
            {
                base.commSvc.commEvent.NotifyObeserver(new DeviceStatusLoadedArgs(deviceStatus));
            }

            return deviceStatus != null;
        }

        public bool ReadCustomDeviceInfoEnable(SystemModuleBase system)
        {
            byte[] cmd = this.commProtocol.GetReadCustomDeviceInfoEnable();

            base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_LoadSystemXData], localization[Localization.comm_IsEnableCustomDeviceInfo]));

            base.commSvc.SyncSendCmdAndRetransmission(cmd, CommServiceBase.LoadSystemModuleDataWaitTime, true);

            if (!this.commProtocol.ParseCustomDeviceInfoEnable(base.commSvc.decryptedReceivedData, system))
            {
                Tools.LogHelper.WriteInfo("LoadSystemExtendedFunctionData()...ReadDeviceExtendedFunctionState...Failed!");
                base.commSvc.UpdateCommFailedProgress();
                return false;
            }

            base.commSvc.UpdateCommSuccessProgress(0.01f);

            return true;
        }

        public override bool LoadCustomDeviceInfo(SystemModuleBase system)
        {
            if(!AppConfig.isLoadCustomDeviceInfo)
            {
                return true;
            }

            if(!ReadCustomDeviceInfoEnable(system))
            {
                return false;
            }
            
            //加载扩展功能数据
            List<short> data_list = new List<short>()
            {
                (short)SystemCommProtocol.SystemCustomDeviceInfoData.custom_manufacturer_data,
                (short)SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_model_data,
                (short)SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_version_data,
                (short)SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_description_data,
            };

            base.commSvc.UpdateCommProgress(string.Format(localization[Localization.comm_LoadSystemXData], localization[Localization.comm_CustomDeviceInfoData]));

            foreach (short data_index in data_list)
            {
                byte[] cmd = this.commProtocol.GetReadDeviceExtendedFunctionData(data_index);

                base.commSvc.SyncSendCmdAndRetransmission(cmd, CommServiceBase.LoadSystemModuleDataWaitTime, true);

                if (!this.commProtocol.ParseSystemExtendedFunctionData(data_index, base.commSvc.decryptedReceivedData, system))
                {
                    Tools.LogHelper.WriteInfo("LoadSystemExtendedFunctionData()...ReadDeviceExtendedFunctionState...Failed!");
                    base.commSvc.UpdateCommFailedProgress();
                    return false;
                }
            }

            base.commSvc.UpdateCommSuccessProgress(0.01f);

            return true;
        }

        public override SystemParamComment[] GetSystemParamsForConnectAndLoad()
        {
            List<SystemParamComment> list = new List<SystemParamComment>(base.GetSystemParamsForConnectAndLoad());
            int insertIndex = list.Count > 9 ? 9 : 0;   //注:
            list.InsertRange(insertIndex, new SystemParamComment[]
                {
                    new SystemParamComment(SystemCommProtocol.SystemParamAddr.deviceWorkMode, localization[Localization.comm_DeviceWorkMode]),
                    new SystemParamComment(SystemCommProtocol.SystemParamAddr.musicMute, localization[Localization.comm_MusicMute]),
                    new SystemParamComment(SystemCommProtocol.SystemParamAddr.micMute, localization[Localization.comm_MicMute]),
                    new SystemParamComment(SystemCommProtocol.SystemParamAddr.effectMute, localization[Localization.comm_EffectMute]),
                }
            );

            if (AppConfig.isLoadCustomDeviceInfo)
            {
                list.Add(new SystemParamComment(SystemCommProtocol.SystemParamAddr.prototypeModel, localization[Localization.comm_PrototypeModel]));
                list.Add(new SystemParamComment(SystemCommProtocol.SystemParamAddr.firmwareVersion, localization[Localization.comm_FirmwareVersion]));
                list.Add(new SystemParamComment(SystemCommProtocol.SystemParamAddr.firmwareBuildNumbers, localization[Localization.comm_FirmwareBuildNumbers]));
            }
           
            return list.ToArray();
        }

        public override SystemParamComment[] GetSystemParamsForRecall()
        {
            List<SystemParamComment> list = new List<SystemParamComment>(base.GetSystemParamsForRecall());
            int insertIndex = list.Count > 9 ? 9 : 0;   //注:
            list.InsertRange(insertIndex, new SystemParamComment[]
                {
                    new SystemParamComment(SystemCommProtocol.SystemParamAddr.timbre, localization[Localization.comm_Timbre]),
                }
            );

            return list.ToArray();
        }

        public override SystemParamCommentValue[] GetSystemParamsForUpload(SystemModuleBase system)
        {
            List<SystemParamCommentValue> list = new List<SystemParamCommentValue>(base.GetSystemParamsForUpload(system));
            int insertIndex = list.Count > 9 ? 9 : 0;   //注:
            list.InsertRange(insertIndex, new SystemParamCommentValue[]
                {
                    new SystemParamCommentValue(SystemCommProtocol.SystemParamAddr.timbre, localization[Localization.comm_Timbre], system.timbreControl.v),
                }
            );

            return list.ToArray();
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
