using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Comm.CommTest
{
    public class DeviceModelLoadedEventArgs : MEventBusArgs
    {
        public const string DeviceModelLoaded = "DeviceModelLoaded";
        public readonly string model;
        public readonly string modelCode;
        public DeviceModelLoadedEventArgs(string model_, string modelCode_)
            : base(DeviceModelLoaded)
        {
            this.model = model_;
            this.modelCode = modelCode_;
        }        
    }
}
