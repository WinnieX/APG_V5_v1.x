using AEC.Core;
using AEC.Common;
using AEC.Comm;
using AEC.Comm.X5Comm;


namespace AEC.Effectors.X5.Modules
{
    public class SystemModule : SystemModuleBase
    {
        public const int X5PresetModeCount = 0x10;
        private const short MusicInputGainMax = 2;
        private const short MusicInputPortMax = 3;

        public SystemModule(EventBus effectorEvent_) :
            base(X5CommProtocol.DefaultSystemModuleIndex, effectorEvent_, modeCount: X5PresetModeCount, musicInputGainMax_: MusicInputGainMax, musicInputPortMax_: MusicInputPortMax)
        {
            base.musicMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicMute, this.OnParamChanged);
            base.micMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.micMute, this.OnParamChanged);
            base.effectMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.effectMute, this.OnParamChanged);

            base.deviceWorkMode.initV = 1;
        }
    }
}
