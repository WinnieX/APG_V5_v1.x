using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxPIIDCMComm;

namespace AEC.Effectors.VKxPIIDCM.Modules
{
    public class MusicParam : ShortParam
    {
        public MusicParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null):
            base(VKxPIIDCMCommProtocol.musicModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    //public class MicParam : ShortParam
    //{
    //    public MicParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
    //        base(VKxPIIDCMCommProtocol.micBModuleIndex, address, max, dataChangedHandler)
    //    {

    //    }
    //}

    //public class MicExciterParam : ShortParam
    //{
    //    public MicExciterParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
    //        base(VKxPIICommProtocol.micModuleIndex, address, max, dataChangedHandler)
    //    {

    //    }
    //}

    public class EchoParam : ShortParam
    {
        public EchoParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxPIIDCMCommProtocol.echoModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class ReverbParam : ShortParam
    {
        public ReverbParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxPIIDCMCommProtocol.reverbModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class MainOutputParam : ShortParam
    {
        public MainOutputParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxPIIDCMCommProtocol.mainOutputModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class Aux1Param : ShortParam
    {
        public Aux1Param(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxPIIDCMCommProtocol.aux1ModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class Aux2Param : ShortParam
    {
        public Aux2Param(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxPIIDCMCommProtocol.aux2ModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class SurOutputParam : ShortParam
    {
        public SurOutputParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxPIIDCMCommProtocol.surOutputModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class SystemParam : ShortParam
    {
        public SystemParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxPIIDCMCommProtocol.DefaultSystemModuleIndex, address, max, dataChangedHandler)
        {

        }
    }
}
