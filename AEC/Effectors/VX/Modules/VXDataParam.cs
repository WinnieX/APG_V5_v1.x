using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VXComm;

namespace AEC.Effectors.VX.Modules
{
    public class MusicParam : ShortParam
    {
        public MusicParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null):
            base(VXCommProtocol.musicModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class MicParam : ShortParam
    {
        public MicParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.micModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class DEffectParam : ShortParam
    {
        public DEffectParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.dEffectModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class EchoParam : ShortParam
    {
        public EchoParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.echoModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    public class ReverbParam : ShortParam
    {
        public ReverbParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.reverbModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class MainOutputParam : ShortParam
    {
        public MainOutputParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.mainOutputModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class Aux1Param : ShortParam
    {
        public Aux1Param(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.aux1ModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class Aux2Param : ShortParam
    {
        public Aux2Param(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.aux2ModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class SurOutputParam : ShortParam
    {
        public SurOutputParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.surOutputModuleIndex, address, max, dataChangedHandler)
        {

        }
    }

    class SystemParam : ShortParam
    {
        public SystemParam(byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null) :
            base(VXCommProtocol.DefaultSystemModuleIndex, address, max, dataChangedHandler)
        {

        }
    }
}
