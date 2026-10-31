using AEC.Core;
using AEC.Common;
using AEC.Comm;
using AEC.Comm.VKxDCMComm;


namespace AEC.Effectors.VKxDCM.Modules
{
    public class SystemModule : SystemModuleBase
    {
        private const short MusicInputGainMax = 1;

        public SystemModule(EventBus effectorEvent_) :
            base(VKxDCMCommProtocol.DefaultSystemModuleIndex, effectorEvent_, musicInputGainMax_: MusicInputGainMax)
        {
            base.musicMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicMute, this.OnParamChanged);
            base.micMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.micMute, this.OnParamChanged);
            base.effectMute = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.effectMute, this.OnParamChanged);
        }

#if COMM_TEST
        public override void RandomTest()
        {
            base.RandomTest();
            clarity.RandomTest();
        }
#endif
    }
}
