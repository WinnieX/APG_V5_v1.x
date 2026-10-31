using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Common
{
    public class DeviceStatus
    {
        public const int DeviceStatusOutputLimiterChannels = 5;
        public const int DeviceStatusOutputChannels = 8;
        public const int DeviceStatuMusicInputChannels = 2;
        public const int DeviceStatuMicInputChannels = 2;
        public const int DeviceStatusReservedDataLength = 4;

        public double[] ouputLimiter = new double[DeviceStatusOutputLimiterChannels];
        public double micCompressor;
        public double[] outputLevel = new double[DeviceStatusOutputChannels];
        public double[] musicInputLevel = new double[DeviceStatuMusicInputChannels];
        public double[] micInputLevel = new double[DeviceStatuMicInputChannels];
        public double[] reserved = new double[DeviceStatusReservedDataLength];

        public double musicInputLeft
        {
            get
            {
                return this.musicInputLevel[0];
            }
        }

        public double musicInputRight
        {
            get
            {
                return this.musicInputLevel[1];
            }
        }

        public double micInputA
        {
            get
            {
                return this.micInputLevel[0];
            }
        }

        public double micInputB
        {
            get
            {
                return this.micInputLevel[1];
            }
        }

        public double mainOutputLeft
        {
            get
            {
                return outputLevel[0];
            }
        }

        public double mainOutputRight
        {
            get
            {
                return outputLevel[1];
            }
        }

        public double mainOutputLimiter
        {
            get
            {
                return ouputLimiter[0];
            }
        }

        public double aux1Output
        {
            get
            {
                return outputLevel[2];
            }
        }

        public double aux1OutputLimiter
        {
            get
            {
                return ouputLimiter[1];
            }
        }

        public double aux2Output
        {
            get
            {
                return outputLevel[3];
            }
        }

        public double aux2OutputLimiter
        {
            get
            {
                return ouputLimiter[2];
            }
        }

        public double surOutputLeft
        {
            get
            {
                return outputLevel[4];
            }
        }

        public double surOutputRight
        {
            get
            {
                return outputLevel[5];
            }
        }

        public double surOutputLimiter
        {
            get
            {
                return ouputLimiter[3];
            }
        }
    }
}
