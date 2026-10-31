using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Effectors
{
    public class MEffectorFeatures : IGetProductFeatures
    {
        private readonly MAudioEffectorModel mEffectorModel;
        public MEffectorFeatures(MAudioEffectorModel mEffectorModel_)
        {
            this.mEffectorModel = mEffectorModel_;
        }

        public short GetSystemMusicMaxInputPort(DeviceModel model)
        {
            switch (model)
            {
                case DeviceModel.VK3W:
                case DeviceModel.X5W:
                case DeviceModel.X5OPBT:
                case DeviceModel.VK3WDM:
                case DeviceModel.VK6WS:
                case DeviceModel.Q2:
                case DeviceModel.Q3:
                    return 3;   //VK3W机型，带蓝牙和SPDIF，有4种选择 模拟输入1 + 模拟输入2 + 蓝牙 + SPDIF

                case DeviceModel.VK3W_WithoutBT:
                case DeviceModel.VK6GS:
                case DeviceModel.VK3BT:
                    return 2;   //VK3W机型，带蓝牙和SPDIF，有4种选择 模拟输入1 + 模拟输入2 + SPDIF

                default:
                    return 1;   //默认情况下，只有模拟输入0和模拟输入1
            }
        }

        public short GetSystemTimbreMax(DeviceModel model)
        {
            switch (model)
            {
                case DeviceModel.K320:
                case DeviceModel.A300:
                case DeviceModel.K220:
                    return 100;   //VK3 VK5 A300 音色调节最大为100

                default:
                    return 1;   //默认情况下，只有模拟输入0和模拟输入1
            }
        }

        public bool IsSupportBT()
        {
            switch (mEffectorModel.currModel)
            {
                case DeviceModel.VK3W:
                case DeviceModel.X5W:
                case DeviceModel.X5OPBT:
                case DeviceModel.VK3WDM:
                case DeviceModel.VK6WS:
                case DeviceModel.Q2:
                case DeviceModel.Q3:
                case DeviceModel.VK3BT:
                    return true;   //VK3W机型，带蓝牙和SPDIF，有4种选择 模拟输入0 + 模拟输入1 + 蓝牙 + SPDIF

                default:
                    return false;   //默认情况下，只有模拟输入0和模拟输入1
            }
        }

        public bool IsSupportSPDIF()
        {
            switch (mEffectorModel.currModel)
            {
                case DeviceModel.VK3W:
                case DeviceModel.VK3W_WithoutBT:
                case DeviceModel.X5W:
                case DeviceModel.X5OPBT:
                case DeviceModel.VK3WDM:
                case DeviceModel.VK6WS:
                case DeviceModel.VK6GS:
                case DeviceModel.Q2:
                case DeviceModel.Q3:
                    return true;   //VK3W机型，带蓝牙和SPDIF，有4种选择 模拟输入1 + 模拟输入2 + 蓝牙 + SPDIF

                default:
                    return false;   //默认情况下，只有模拟输入0和模拟输入1
            }
        }

        public bool IsSupportSwitchWorkMode()
        {
            switch (mEffectorModel.currModel)
            {
                case DeviceModel.VK5S:
                case DeviceModel.VK3S:
                case DeviceModel.X5:
                case DeviceModel.X5W:
                case DeviceModel.VK3I:
                case DeviceModel.VK3W:
                case DeviceModel.VK3W_WithoutBT:
                case DeviceModel.VK8Pro:
                case DeviceModel.VK6Pro:
                case DeviceModel.VK8SPro:
                case DeviceModel.VK6SPro:
                    return true;   

                default:
                    return false;   
            }
        }

        public bool IsSupportSwitchTimbre()
        {
            switch (mEffectorModel.currModel)
            {
                case DeviceModel.VK5S:
                case DeviceModel.VK3S:
                case DeviceModel.VK3I:
                case DeviceModel.VK3W:
                case DeviceModel.VK3WDM:
                    return true;   //VK3W机型，带蓝牙和SPDIF，有4种选择 模拟输入0 + 模拟输入0 + 蓝牙 + SPDIF

                default:
                    return false;   //默认情况下，只有模拟输入0和模拟输入1
            }
        }

        public bool IsSupportControlClarity()
        {
            switch (mEffectorModel.currModel)
            {
                case DeviceModel.VK6S:
                case DeviceModel.VK6GS:
                case DeviceModel.Q2:
                case DeviceModel.Q3:
                case DeviceModel.VK3D:
                case DeviceModel.VK3BT:
                case DeviceModel.VK5D:
                    return true;

                default:
                    return false;
            }
        }
    }
}
