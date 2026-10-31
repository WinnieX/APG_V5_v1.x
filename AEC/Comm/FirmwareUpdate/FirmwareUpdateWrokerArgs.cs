using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class FirmwareUpdateWrokerArgs
    {
        public readonly FirmwareUpdateProcess firmwareUpgradeProcess;
        public readonly object state;

        public FirmwareUpdateWrokerArgs(FirmwareUpdateProcess firmwareUpgradeProcess_, object state_)
        {
            this.firmwareUpgradeProcess = firmwareUpgradeProcess_;
            this.state = state_;
        }
    }
}
