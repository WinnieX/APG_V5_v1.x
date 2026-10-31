using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxDCMComm;

namespace AEC.Effectors.VKxDCM.Modules
{
    public class MusicParam : ShortParam
    {
        public MusicParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null):
            base(VKxDCMCommProtocol.musicModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    //public class MicParam : ShortParam
    //{
    //    public MicParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
    //        base(VKxDCMCommProtocol.micAModuleIndex, address, max, dataChangedHandler)
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
            base(VKxDCMCommProtocol.echoModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class ReverbParam : ShortParam
    {
        public ReverbParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxDCMCommProtocol.reverbModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class MainOutputParam : ShortParam
    {
        public MainOutputParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxDCMCommProtocol.mainOutputModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class Aux1Param : ShortParam
    {
        public Aux1Param(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxDCMCommProtocol.aux1ModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class Aux2Param : ShortParam
    {
        public Aux2Param(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxDCMCommProtocol.aux2ModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class SurOutputParam : ShortParam
    {
        public SurOutputParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxDCMCommProtocol.surOutputModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class SystemParam : ShortParam
    {
        public SystemParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VKxDCMCommProtocol.DefaultSystemModuleIndex, address, max, dataChangedHandler)
        {

        }
    }
}
