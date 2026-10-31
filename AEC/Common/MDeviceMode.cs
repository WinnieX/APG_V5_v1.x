using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Common
{
    public class MDeviceMode
    {
        public readonly EffectorModelBase effector;
        public readonly string modeName;

        public MDeviceMode(EffectorModelBase effector_, string modeName_)
        {
            this.effector = effector_;
            this.modeName = modeName_;
        }
    }
}
