using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Common;
using AEC.Effectors.VX;
using AEC.Effectors.MX;
using AEC.Effectors.X5;
using AEC.Effectors.KX;

namespace AEC.Effectors
{
    public class MEffectorSeralizer
    {
        public readonly MAudioEffectorModel mEffectorModel;

        public DeviceModel currDeviceModel
        {
            get
            {
                return mEffectorModel.currModel;
            }
        }

        public DeviceModel nextDeviceModel
        {
            get
            {
                return mEffectorModel.nextModel;
            }
        }

        public string currEffectorType
        {
            get
            {
                return MAudioEffectorModel.EffectorModelToType(this.currDeviceModel);
            }
        }

        public string nextEffectorType
        {
            get
            {
                return MAudioEffectorModel.EffectorModelToType(this.nextDeviceModel);
            }
        }

        public MEffectorSeralizer(MAudioEffectorModel mEffectorModel_)
        {
            this.mEffectorModel = mEffectorModel_;
        }

        public byte[] NextEffectorSeralizeToBinary(EffectorModelBase effectorModel, bool isContainAllSystemData)
        {
            return EffectorSeralizeToBinary(effectorModel, isContainAllSystemData, nextDeviceModel);
        }

        public byte[] CurrEffectorSeralizeToBinary(EffectorModelBase effectorModel, bool isContainAllSystemData)
        {
            return EffectorSeralizeToBinary(effectorModel, isContainAllSystemData, currDeviceModel);
        }

        private byte[] EffectorSeralizeToBinary(EffectorModelBase effectorModel, bool isContainAllSystemData, DeviceModel deviceModel)
        {
            Debug.Assert(effectorModel != null);

            switch (deviceModel)
            {
                case DeviceModel.M_Audio:
                case DeviceModel.VHi:
                case DeviceModel.V0:
                case DeviceModel.V5:
                case DeviceModel.E300:
                    return VXDataStorage.Effector2Buffer(effectorModel as VXEffectorModel, currEffectorType, isContainAllSystemData);

                case DeviceModel.M5Pro:
                case DeviceModel.A100:
                    return MXDataStorage.Effector2Buffer(effectorModel as MXEffectorModel, currEffectorType, isContainAllSystemData);

                default:
                    throw new Exception("未知效果器型号!");
            }
        }

        public EffectorModelBase DeseralizeToNextEffectorFromBuffer(byte[] buffer, bool isContainAllSystemData = false)
        {
            return DeseralizeToEffectorFromBuffer(buffer, isContainAllSystemData, nextDeviceModel);
        }

        public EffectorModelBase DeseralizeToCurrEffectorFromBuffer(byte[] buffer, bool isContainAllSystemData = false)
        {
            return DeseralizeToEffectorFromBuffer(buffer, isContainAllSystemData, currDeviceModel);
        }

        public static bool CheckVKxSEffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_VKxS ||
                model == ProductsConfig.ProductFamily_X5)
            {
                return true;
            }

            return false;
        }

        public static bool CheckVKxSMDEffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_VKxDM ||
                model == ProductsConfig.ProductFamily_X5)
            {
                return true;
            }

            return false;
        }

        public static bool CheckVKxSDWEffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_VKxDM ||
                model == ProductsConfig.ProductFamily_X5)
            {
                return true;
            }

            return false;
        }

        public static bool CheckX5EffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_X5)
            {
                return true;
            }

            return false;
        }

        public static bool CheckVKxEffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_VKx ||
                model == ProductsConfig.Model_VK5_Presentation ||
                model == ProductsConfig.Model_VK3_Presentation ||
                model == ProductsConfig.Model_A300_Presentation)
            {
                return true;
            }

            return false;
        }

        public static bool CheckVKxSCMEffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_VKxSCM)
            {
                return true;
            }

            return false;
        }

        public static bool CheckVKxDCMEffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_VKxDCM)
            {
                return true;
            }

            return false;
        }

        public static bool CheckVKxPIIEffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_VKxPII)
            {
                return true;
            }

            return false;
        }

        public static bool CheckVKxPIIDCMEffectorPresetModel(string model)
        {
            if (model == ProductsConfig.ProductFamily_VKxPIIDCM)
            {
                return true;
            }

            return false;
        }

        private EffectorModelBase DeseralizeToEffectorFromBuffer(byte[] buffer, bool isContainAllSystemData, DeviceModel deviceModel)
        {
            Debug.Assert(buffer != null);

            switch (deviceModel)
            {
                case DeviceModel.X5:
                case DeviceModel.X5W:
                case DeviceModel.X5Pro:
                case DeviceModel.X5OPBT:
                    return X5.X5DataStorage.Buffer2Effector(buffer, CheckX5EffectorPresetModel, isContainAllSystemData);

                case DeviceModel.M_Audio:
                case DeviceModel.VK3S:
                case DeviceModel.VK5S:
                case DeviceModel.VK3I:
                case DeviceModel.VK3W:
                case DeviceModel.VK3W_WithoutBT:
                    return VKxS.VKxSDataStorage.Buffer2Effector(buffer, CheckVKxSEffectorPresetModel, isContainAllSystemData);

                case DeviceModel.VK3WDM:
                    return VKxDM.VKxDMDataStorage.Buffer2Effector(buffer, CheckVKxSDWEffectorPresetModel, isContainAllSystemData);

                case DeviceModel.K320:
                case DeviceModel.A300:
                case DeviceModel.K220:
                    return KX.KXDataStorage.Buffer2Effector(buffer, CheckVKxEffectorPresetModel, isContainAllSystemData);

                case DeviceModel.VK3D:
                case DeviceModel.VK3BT:
                case DeviceModel.VK5D:
                case DeviceModel.VK8:
                    return VKxSCM.VKxSCMDataStorage.Buffer2Effector(buffer, CheckVKxSCMEffectorPresetModel, isContainAllSystemData);

                case DeviceModel.VK6S:
                case DeviceModel.VK6WS:
                case DeviceModel.VK6GS:
                case DeviceModel.VK8S:
                case DeviceModel.Q2:
                case DeviceModel.Q3:
                    return VKxDCM.VKxDCMDataStorage.Buffer2Effector(buffer, CheckVKxDCMEffectorPresetModel, isContainAllSystemData);

                case DeviceModel.VK6Pro:
                case DeviceModel.VK8Pro:
                    return VKxPII.VKxPIIDataStorage.Buffer2Effector(buffer, CheckVKxPIIEffectorPresetModel, isContainAllSystemData);

                case DeviceModel.VK6SPro:
                case DeviceModel.VK8SPro:
                    return VKxPIIDCM.VKxPIIDCMDataStorage.Buffer2Effector(buffer, CheckVKxPIIDCMEffectorPresetModel, isContainAllSystemData);

                case DeviceModel.VHi:
                case DeviceModel.V0:
                case DeviceModel.V5:
                case DeviceModel.E300:
                    return VX.VXDataStorage.Buffer2Effector(buffer, currEffectorType, isContainAllSystemData);

                case DeviceModel.M5Pro:
                case DeviceModel.A100:
                    return MX.MXDataStorage.Buffer2Effector(buffer, currEffectorType, isContainAllSystemData);

                default:
                    throw new Exception("未知效果器型号!");
            }
        }

        public EffectorModelBase DeseralizeToCurrEffectorFromFile(string filePath, bool isContainAllSystemData = false)
        {
            MEffectorFamily effectorFamily = ProductsConfig.EffectorModelToFamily(this.currDeviceModel);

            switch (effectorFamily)
            {
                case MEffectorFamily.X5:
                    return X5.X5DataStorage.LoadFromFile(filePath, CheckX5EffectorPresetModel);

                case MEffectorFamily.VKxS:
                    return VKxS.VKxSDataStorage.LoadFromFile(filePath, CheckVKxSEffectorPresetModel);

                case MEffectorFamily.VKxDM:
                    return VKxDM.VKxDMDataStorage.LoadFromFile(filePath, CheckVKxSMDEffectorPresetModel);

                case MEffectorFamily.KX:
                    return KX.KXDataStorage.LoadFromFile(filePath, CheckVKxEffectorPresetModel);

                case MEffectorFamily.VKxSCM:
                    return VKxSCM.VKxSCMDataStorage.LoadFromFile(filePath, CheckVKxSCMEffectorPresetModel);

                case MEffectorFamily.VKxDCM:
                    return VKxDCM.VKxDCMDataStorage.LoadFromFile(filePath, CheckVKxDCMEffectorPresetModel);

                case MEffectorFamily.VKxPII:
                    return VKxPII.VKxPIIDataStorage.LoadFromFile(filePath, CheckVKxPIIEffectorPresetModel);

                case MEffectorFamily.VKxPIIDCM:
                    return VKxPIIDCM.VKxPIIDCMDataStorage.LoadFromFile(filePath, CheckVKxPIIDCMEffectorPresetModel);

                case MEffectorFamily.VX:
                    return VX.VXDataStorage.LoadFromFile(filePath, currEffectorType);

                case MEffectorFamily.MX:
                    return MX.MXDataStorage.LoadFromFile(filePath, currEffectorType);

                default:
                    throw new Exception("未知效果器型号!");
            }
        }
    }
}
