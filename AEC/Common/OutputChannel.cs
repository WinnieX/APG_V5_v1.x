using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Common
{
    public class X5OutputChannel
    {
        public readonly ShortParam delay;
        public readonly BoolParam mute;

        public X5OutputChannel(byte moduleIndex, byte delayAddress, short delayMaxIndex, EventHandler<DataParamChangedArgs> dataChangedHandler)
        {
            this.delay = new ShortParam(moduleIndex, delayAddress++, delayMaxIndex, dataChangedHandler);
            this.mute = new BoolParam(moduleIndex, delayAddress++, dataChangedHandler);
        }

        public void CopyFrom(X5OutputChannel other)
        {
            this.delay.initV = other.delay.v;
            this.mute.initV = other.mute.v;
        }

#if COMM_TEST
        public void RandomTest()
        {
            this.delay.RandomTest();
            this.mute.RandomTest();
        }
#endif
    }

    public class OutputChannel
    {
        public readonly ShortParam delay;
        public readonly BoolParam mute;
        public readonly BoolParam phase;

        public OutputChannel(byte moduleIndex, byte delayAddress, short delayMaxIndex, EventHandler<DataParamChangedArgs> dataChangedHandler)
        {
            this.delay = new ShortParam(moduleIndex, delayAddress++, delayMaxIndex, dataChangedHandler);
            this.mute = new BoolParam(moduleIndex, delayAddress++, dataChangedHandler);
            this.phase = new BoolParam(moduleIndex, delayAddress++, dataChangedHandler);

            this.phase.initV = 1;
        }

        public void CopyFrom(OutputChannel other)
        {
            this.delay.initV = other.delay.v;
            this.mute.initV = other.mute.v;
            this.phase.initV = other.phase.v;
        }

        public void CopyFrom(X5OutputChannel other)
        {
            this.delay.initV = (short)Math.Round(other.delay.v * 4.8, 0, MidpointRounding.AwayFromZero);
            this.mute.initV = other.mute.v;
            this.phase.initV = 1;
        }

#if COMM_TEST
        public void RandomTest()
        {
            this.delay.RandomTest();
            this.mute.RandomTest();
            this.phase.RandomTest();
        }
#endif
    }
}
