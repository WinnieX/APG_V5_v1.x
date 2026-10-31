using System;
using System.Collections.Generic;
using System.Text;
using AEC.AudioProducts;

namespace AEC.Common
{
    public class MPresetDeviceMode
    {
        public readonly IGetAudioSystem getAudioSystem;
        public readonly List<MDeviceMode> deviceModeList;

        public MPresetDeviceMode(IGetAudioSystem getAudioSystem_, List<MDeviceMode> deviceModeList_)
        {
            getAudioSystem = getAudioSystem_;
            deviceModeList = deviceModeList_;
        }
    }
}
