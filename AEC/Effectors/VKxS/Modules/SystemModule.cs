using AEC.Core;
using AEC.Common;
using AEC.Comm;
using AEC.Comm.VKxSComm;


namespace AEC.Effectors.VKxS.Modules
{
    public class SystemModule : SystemModuleBase
    {
        private const short MusicInputGainMax = 1;
        private const short MusicInputPortMax = 3;

        public SystemModule(EventBus effectorEvent_) :
            base(VKxSCommProtocol.DefaultSystemModuleIndex, effectorEvent_, musicInputGainMax_: MusicInputGainMax, musicInputPortMax_: MusicInputPortMax)
        {
            base.musicMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicMute, this.OnParamChanged);
            base.micMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.micMute, this.OnParamChanged);
            base.effectMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.effectMute, this.OnParamChanged);
        }
    }
}
