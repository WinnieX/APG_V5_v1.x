using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class SwitchWorkMode
    {
        public readonly short workModeIndex;
        public readonly bool retainsData;
        public readonly bool outputMute;

        public SwitchWorkMode(short workModeIndex_, bool retainsData_, bool outputMute_)
        {
            this.workModeIndex = workModeIndex_;
            this.retainsData = retainsData_;
            this.outputMute = outputMute_;
        }
    }
}
