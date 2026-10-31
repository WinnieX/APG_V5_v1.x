using AEC.Core;
using AEC.Common;
using AEC.Comm;
using AEC.Comm.VKxSCMComm;


namespace AEC.Effectors.VKxSCM.Modules
{
    public class SystemModule : SystemModuleBase
    {
        private const short MusicInputGainMax = 1;

        public SystemModule(EventBus effectorEvent_) :
            base(VKxSCMCommProtocol.DefaultSystemModuleIndex, effectorEvent_, musicInputGainMax_: MusicInputGainMax)
        {
            base.musicMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicMute, this.OnParamChanged);
            base.micMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.micMute, this.OnParamChanged);
            base.effectMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.effectMute, this.OnParamChanged);
        }
    }
}
