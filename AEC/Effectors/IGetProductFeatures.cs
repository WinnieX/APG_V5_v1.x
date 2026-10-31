using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Effectors
{
    public interface IGetProductFeatures
    {
        short GetSystemMusicMaxInputPort(DeviceModel model);
        short GetSystemTimbreMax(DeviceModel model);
        bool IsSupportBT();
        bool IsSupportSPDIF();
        bool IsSupportSwitchWorkMode();
        bool IsSupportSwitchTimbre();
        bool IsSupportControlClarity();
    }
}
