using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Tools;

namespace AEC.Comm
{
    public class MCommService : CommServiceBase
    {
        private AbstractCommService commSvc
        {
            get;
            set;
        }

        public MCommService(EventBus commEvent_) :
            base(commEvent_)
        {
        }

        public void RegisterCommService(AbstractCommService commSvc_)
        {
            this.commSvc = commSvc_;
        }

        protected override bool InitCommKey()
        {
            if (this.commSvc != null)
            {
                return this.commSvc.InitCommKey();
            }

            throw new Exception("MCommService.InitCommKey()...请先初始化 commSvc !");
        }

        protected override void LoadDataCompleted(CommProcess commProcess)
        {
            if (this.commSvc != null)
            {
                this.commSvc.LoadDataCompleted(commProcess);
                return;
            }

            throw new Exception("MCommService.LoadDataCompleted()...请先初始化 commSvc !");
        }

        protected override void CallDeviceModeCompleted(CommProcess commProcess)
        {
            if (this.commSvc != null)
            {
                this.commSvc.CallDeviceModeCompleted(commProcess);
                return;
            }

            throw new Exception("MCommService.CallDeviceModeCompleted()...请先初始化 commSvc !");
        }

        protected override void UploadDataCompleted()
        {
            if (this.commSvc != null)
            {
                this.commSvc.UploadDataCompleted();

                return;
            }
            
            throw new Exception("MCommService.UploadDataCompleted()...请先初始化 commSvc !");
        }

        protected override SystemModuleBase GetSystemModule()
        {
            if (this.commSvc != null)
            {
                return this.commSvc.GetSystemModule();
            }

            throw new Exception("MCommService.GetSystemModule()...请先初始化 commSvc !");
        }

        protected override bool ParseSystemData(byte[] buffer, SystemModuleBase system)
        {
            if (this.commSvc != null)
            {
                return this.commSvc.commProtocol.ParseSystemData(buffer, system);
            }

            throw new Exception("MCommService.ParseSystemData()...请先初始化 commSvc !");
        }

        protected override byte GetModuleEQAddr(byte moduleIndex)
        {
            if (this.commSvc != null)
            {
                return this.commSvc.GetModuleEQAddr(moduleIndex);
            }

            throw new Exception("MCommService.GetModuleEQAddr()...请先初始化 commSvc !");
        }

        protected override byte GetModuleDEQParamAddr(DeqParamType paramType)
        {
            if (this.commSvc != null)
            {
                return this.commSvc.GetModuleDEQParamAddr(paramType);
            }

            throw new Exception("MCommService.GetModuleDEQAddr()...请先初始化 commSvc !");
        }

        protected override bool LoadNonSystemModuleData(CommProcess commProcess)
        {
            if(this.commSvc != null)
            {
                return this.commSvc.LoadNonSystemModuleData(commProcess);
            }

            return false;
        }

        protected override bool UploadNonSystemModuleData(CommProcess commProcess, float progressRatio = 1.0f)
        {
            if (this.commSvc != null)
            {
                return this.commSvc.UploadNonSystemModuleData(commProcess, progressRatio);
            }

            return false;
        }

        //上传预设模式数据
        protected override bool UploadMultiPresetData(object state)
        {
            if (this.commSvc != null)
            {
                return this.commSvc.UploadMultiPresetData(state);
            }

            return false;
        }

        protected override SystemParamComment[] GetSystemParamsForConnectAndLoad()
        {
            if (this.commSvc != null)
            {
                return this.commSvc.GetSystemParamsForConnectAndLoad();
            }

            throw new Exception("MCommService.GetSystemParamsForConnectAndLoad()...请先初始化 commSvc !");
        }

        protected override SystemParamComment[] GetSystemParamsForRecall()
        {
            if (this.commSvc != null)
            {
                return this.commSvc.GetSystemParamsForRecall();
            }

            throw new Exception("MCommService.GetSystemParamsForRecall()...请先初始化 commSvc !");
        }


        protected override SystemParamCommentValue[] GetSystemParamsForUpload(SystemModuleBase system)
        {
            if(this.commSvc != null)
            {
                return this.commSvc.GetSystemParamsForUpload(system);
            }

            throw new Exception("MCommService.GetSystemParamsForUpload()...请先初始化 commSvc !");
        }

        protected override bool ParseSystemStatusData(byte[] data)
        {
            if(this.commSvc != null)
            {
                return this.commSvc.ParseSystemStatusData(data);
            }

            throw new Exception("MCommService.ParseSystemStatusData()...请先初始化 commSvc !");
        }

        protected override bool LoadCustomDeviceInfo(SystemModuleBase system)
        {
            if (this.commSvc != null)
            {
                return this.commSvc.LoadCustomDeviceInfo(system);
            }

            throw new Exception("MCommService.LoadSystemExtendedFunctionData()...请先初始化 commSvc !");
        }

        public void UpdateToBeUploadedData(EffectorModelBase effectorModel_)
        {
            if(this.commSvc != null)
            {
                LogHelper.WriteDebug("MCommService.UpdateToBeUploadedData()...");
                this.commSvc.UpdateToBeUploadedData(effectorModel_);
                return;
            }

            throw new Exception("MCommService.UpdateToBeUploadedData()...请先初始化 commSvc !");
        }
    }
}
