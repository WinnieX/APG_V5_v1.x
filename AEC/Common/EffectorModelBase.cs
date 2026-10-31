using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;

namespace AEC.Common
{
    //效果器模型
    public abstract class EffectorModelBase
    {
        public SystemModuleBase system
        {
            get;
            protected set;
        }

        public ModuleBase[] nonSystemModules
        {
            get;
            protected set;
        }

        protected List<ModuleBase> outputModules
        {
            get;
            set;
        }

        public virtual string modelLabel
        {
            get
            {
                return "M-Audio";
            }
        }

        public abstract bool mainOutputIsMute
        {
            get;
            set;
        }

        public abstract bool aux1OutputIsMute
        {
            get;
            set;
        }

        public abstract bool aux2OutputIsMute
        {
            get;
            set;
        }

        public abstract bool surOutputIsMute
        {
            get;
            set;
        }

        public bool isSupportDataEncryption
        {
            get
            {
                return this.system.isSupportDataEncryption;
            }
        }

        public virtual bool dataIsPrivate
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public bool deviceIsActivated
        {
            get
            {
                return this.system.deviceIsActivated;
            }
        }

        public bool deviceKeyIsLocked
        {
            get
            {
                return system.isLockKeyboard.value;
            }
        }

        
        public bool isUsageLimit
        {
            get
            {
                return system.isUsageLimitLock;
            }
        }

        public bool isLockUserOperation
        {
            get
            {
                return system.isLockUserOperation;
            }
        }

        public EffectorModelBase()
        {
        }

        public abstract void CopyFrom(EffectorModelBase other, bool isCopyAllSystem = false);

        public abstract byte[] Serialize(string model, bool isContainAllSystemData = false);

        public void ResetNonSystemModuleData()
        {
            Debug.Assert(this.nonSystemModules != null);

            foreach (ModuleBase module in nonSystemModules)
            {
                module.ResetData();
            }
        }

        protected virtual void InitFreqResp()
        {

        }

        protected virtual void InitEQGraphic()
        {

        }

        public virtual void UpdateEffecorFeatures(IGetProductFeatures iGetFeatures, DeviceModel model)
        {

        }

        public void InitFreqRespAndEQGraphic()
        {
            InitFreqResp();
            InitEQGraphic();
        }

        public abstract void InitTempModel();

        public ModuleBase[] GetOtherModulesInTheGroup(ModuleBase module)
        {
            if (module == null || this.outputModules == null)
            {
                return null;
            }
            else if(this.outputModules.Contains(module))
            {
                List<ModuleBase> modules = new List<ModuleBase>(this.outputModules);
                modules.Remove(module);

                return modules.ToArray();
            }

            return null;
        }

#if COMM_TEST
        public virtual void RandomTest()
        {
            foreach (ModuleBase module in this.nonSystemModules)
            {
                module.RandomTest();
            }
            this.system.RandomTest();
        }
#endif
    }
}
