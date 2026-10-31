using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public class MusicParam : ShortParam
    {
        public MusicParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null):
            base(MXCommProtocol.musicModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class MicParam : ShortParam
    {
        public MicParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(MXCommProtocol.micModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class EchoParam : ShortParam
    {
        public EchoParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(MXCommProtocol.echoModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class ReverbParam : ShortParam
    {
        public ReverbParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(MXCommProtocol.reverbModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class MainOutputParam : ShortParam
    {
        public MainOutputParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(MXCommProtocol.mainOutputModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class Aux1Param : ShortParam
    {
        public Aux1Param(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(MXCommProtocol.aux1ModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class Aux2Param : ShortParam
    {
        public Aux2Param(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(MXCommProtocol.aux2ModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class SurOutputParam : ShortParam
    {
        public SurOutputParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(MXCommProtocol.surOutputModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class SystemParam : ShortParam
    {
        public SystemParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(MXCommProtocol.systemModuleIndex, address, max, dataChangedHandler)
        {

        }
    }
}
