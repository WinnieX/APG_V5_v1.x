using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Common
{
    public abstract class MusicModuleBase : ModuleBase
    {
        public MusicModuleBase(byte index, EventBus effectorEvent_)
            : base(index, effectorEvent_)
        {

        }

        public abstract WorkMode workMode
        {
            get;
            set;
        }

        public abstract WorkSelect workSelect
        {
            get;
            set;
        }
    }
}
