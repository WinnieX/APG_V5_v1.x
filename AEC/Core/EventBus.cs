using System;
using System.Collections.Generic;
using System.Text;
using AEC.Effectors;
using AEC.Comm;
using AEC.Common;

namespace AEC.Core
{
    public class EventBus
    {
        private event EventHandler<EventBusArgs> _handler;
        private bool _isNotify = true;

        public void RegisterHandler(EventHandler<EventBusArgs> handler)
        {
            this._handler += handler;
        }

        public void RemoveHandler(EventHandler<EventBusArgs> handler)
        {
            this._handler -= handler;
        }

        public void RegisterHandler(IEventHanler<EventBusArgs> eventHandlerInterface)
        {
            this._handler += eventHandlerInterface.EventBusHandler;
        }

        public void RemoveHandler(IEventHanler<EventBusArgs> eventHandlerInterface)
        {
            this._handler -= eventHandlerInterface.EventBusHandler;
        }

        public void RemoveAllHandler()
        {
            if (this._handler != null)
            {
                this._handler = null;
            }
        }

        public void NotifyObeserver(EventBusArgs e)
        {
            if(this._handler != null && _isNotify)
            {
                this._handler(this, e);
            }
        }
    }

    public enum EventType
    {
        LoadEffectorModelCompleted = 0,
        LoadDataCompleted,
        UploadDataToDevice,
        UploadDataCompleted,
        CallDeviceMode,
        CallDeviceModeCompleted,
        SwitchDeviceWorkMode,
        SwitchDeviceWorkModeCompleted,
        ConnectStateChanged,
        ModeNameUpdated,
        DeviceIsNotActivated,
        KeyboardIsLocked,
        UnlockKeyLock,
        UnlockUserPermissions,
        EncryptModeData,
        DataParamChanged,
        DEQParamUpdated,
        DEQParamChanged,
        EQChanged,
        CommStateChanged,
        CommCompleted,
        CommSuccessful,
        CommInterrupted,
        ConnectionIsDisconnected,
        Disconnect,
        EQGraphicNeedToBeUpdated,
        CurrEQChangedFromGraphic,
        CurrEQIndexChangedFromGraphic,
        //HPFChangedFromGraphic,
        //LPFChangedFromGraphic,
        XPFChangedFromGraphic,
        WorkModeChanged,
        WorkSelectChanged,
        MusicMaxVolumeChanged,
        MicMaxVolumeChanged,
        EffectMaxVolumeChanged,
        MusicStartVolumeChanged,
        MicStartVolumeChanged,
        EffectStartVolumeChanged,
        MusicVolumeChanged,
        MicVolumeChanged,
        EffectVolumeChanged,
        OutputLimiterStatusLoaded,
        DeviceStatusLoaded,
        PasswordUpdated,
        PasswordChanged,
        SaveDeviceMode,
        UpdateCustomDeviceInfoEnable,
        UpdateCustomDeviceInfoData,
        UpdateModuleAllData,
        UpdateUIFromData,
        CommProgressUpdated,
        SystemUserChanged,
        StartSaveMode,
        SaveModeCompleted,
        DeviceNameChanged,
        UsageLimitLockChanged,
        UpdateUsageLimitLockCompleted,
        UnlockUsageLimit,
        UsageLimitLockFormShow,
        MicABLinked,
        //ShowSignalLevel,
        OutputMuteChangedFromNavigator,
        OutputMuteChangedFromPanel,
        InputMuteChanged,
        OutputMuteChanged,
        UpdateRTADisplay,
        RTATryOperate,
        RTAOperateCompleted,
        AudioInputLevelUpdated,
        RecvMeasDelayDataProgressChanged,
        MeasDelayCompleted,
        LanguageChanged,
        EQGraphicPanelJump,
        OutputLimiterParamChanged,
        OutputParamChanged,
        LimiterThresholdUnitChanged,
        MixerLevelUnitChanged,
        ChannelDelayUnitChanged,
        RCMessage,
        RCConnectToServer,
        RCHandshakeCompleted,

        RCDisconnectFromServer,     //主动断开连接
        RCDisconnectedFromServer,   //被动断开连接

        RCLcConnectToRcToLcResp,    //服务端到本地客户端响应
        RCLcConnectToRcToRcResp,    //服务端到远程客户端响应

        RCDisconnectFromRc,         //主动断开远程连接
        RCDisconnectedFromRc,       //被动断开远程连接

        RCOperationStatusChanged,          //打开远程模式或者关闭远程模式
        RCConnectionStatusChanged,         //连接远程客户端状态发生变化

        RCProModeConnectAndLoadData,
        RCEasyModeConnectAndLoadData,
        RCProModeCallDeviceMode,
        RCEasyModeCallDeviceMode,
        RCProModeUpdateToBeUploadedData,
        RCProModeUploadData,
        RCEasyUploadMultiPresetData,
        RCForcedDisconnect,
        RCReadSystemSatatus,
        

        LCNotConnectedToRC, //本地客户端未连接到远程客户端

        FirmwareUpgradeOutput,
        FirmwareUpgradeCompleted,
    }
        
    public class EventBusArgs : EventArgs
    {
        private EventType _type;
        public EventBusArgs(EventType type)
        {
            this._type = type;
        }

        public EventType type
        {
            get
            {
                return _type;
            }
        }
    }

    public class LoadEffectorModelCompletedArgs : EventBusArgs
    {
        public readonly DeviceModel model;

        public LoadEffectorModelCompletedArgs(DeviceModel model_)
            : base(EventType.LoadEffectorModelCompleted)
        {
            this.model = model_;
        }
    }

    public class LoadDataCompletedArgs : EventBusArgs
    {
        public readonly Comm.CommProcess commProcess;
        public readonly EffectorModelBase effectorModel;

        public LoadDataCompletedArgs(Comm.CommProcess commProcess_, EffectorModelBase effectorModel_)
            : base(EventType.LoadDataCompleted)
        {
            this.commProcess = commProcess_;
            this.effectorModel = effectorModel_;
        }
    }

    public class UploadDataToDeviceArgs : EventBusArgs
    {
        public readonly EffectorModelBase effectorModel;

        public UploadDataToDeviceArgs(EffectorModelBase effectorModel_)
            : base(EventType.UploadDataToDevice)
        {
            this.effectorModel = effectorModel_;
        }
    }

    public class UploadDataCompletedArgs : EventBusArgs
    {
        public readonly EffectorModelBase effectorModel;

        public UploadDataCompletedArgs(EffectorModelBase effectorModel_)
            : base(EventType.UploadDataCompleted)
        {
            this.effectorModel = effectorModel_;
        }
    }

    public class CallDeviceModeArgs : EventBusArgs
    {
        public readonly short modeIndex;

        public CallDeviceModeArgs(short index)
            : base(EventType.CallDeviceMode)
        {
            modeIndex = index;
        }
    }

    public class CallDeviceModeCompletedArgs : EventBusArgs
    {
        public readonly Comm.CommProcess commProcess;
        public readonly EffectorModelBase effectorModel;

        public CallDeviceModeCompletedArgs(Comm.CommProcess commProcess_, EffectorModelBase effectorModel_)
            : base(EventType.CallDeviceModeCompleted)
        {
            this.commProcess = commProcess_;
            this.effectorModel = effectorModel_;
        }
    }

    public class SwitchDeviceWorkModeArgs : EventBusArgs
    {
        public readonly SwitchWorkMode switchWorkMode;

        public SwitchDeviceWorkModeArgs(SwitchWorkMode switchWorkMode_)
            : base(EventType.SwitchDeviceWorkMode)
        {
            switchWorkMode = switchWorkMode_;
        }
    }

    public class SwitchDeviceWorkModeCompletedArgs : EventBusArgs
    {
        //public readonly Comm.CommProcess commProcess;
        //public readonly EffectorModelBase effectorModel;

        public SwitchDeviceWorkModeCompletedArgs()
            : base(EventType.SwitchDeviceWorkModeCompleted)
        {
            //this.commProcess = commProcess_;
            //this.effectorModel = effectorModel_;
        }
    }

    public class ConnectStateChangedArgs : EventBusArgs
    {
        public ConnectStateChangedArgs()
            : base(EventType.ConnectStateChanged)
        {
        }
    }

    public class ModeNameUpdatedArgs : EventBusArgs
    {
        public ModeNameUpdatedArgs()
            : base(EventType.ModeNameUpdated)
        {
        }
    }

    public class DeviceIsNotActivatedArgs : EventBusArgs
    {
        public DeviceIsNotActivatedArgs()
            : base(EventType.DeviceIsNotActivated)
        {
        }
    }

    public class KeyboardIsLockedArgs : EventBusArgs
    {
        public KeyboardIsLockedArgs()
            : base(EventType.KeyboardIsLocked)
        {
        }
    }

    public class UnlockKeyLockArgs : EventBusArgs
    {
        public UnlockKeyLockArgs()
            : base(EventType.UnlockKeyLock)
        {
        }
    }

    public class UnlockUserPermissionsArgs : EventBusArgs
    {
        public UnlockUserPermissionsArgs()
            : base(EventType.UnlockUserPermissions)
        {
        }
    }

    public class EncryptModeDataArgs : EventBusArgs
    {
        public EncryptModeDataArgs()
            : base(EventType.EncryptModeData)
        {
        }
    }


    public class DataParamChangedArgs : EventBusArgs
    {
        public readonly ShortParam param;
        public DataParamChangedArgs(ShortParam param_)
            : base(EventType.DataParamChanged)
        {
            this.param = param_;
        }
    }
    
    public class DEQParamUpdatedArgs : EventBusArgs
    {
        public readonly DEQ deq;
        public readonly DeqParamType paramType;

        public DEQParamUpdatedArgs(DEQ deq_, DeqParamType paramType_)
        //public DEQParamUpdatedArgs(DeqParamType paramType_)
            : base(EventType.DEQParamUpdated)
        {
            this.deq = deq_;
            this.paramType = paramType_;
        }
    }

    public class DEQParamChangedArgs : EventBusArgs
    {
        public readonly byte moduleIndex;
        public readonly DEQ deq;
        public readonly DeqParamType paramType;
        public DEQParamChangedArgs(byte moduleIndex_, DEQ deq_, DeqParamType paramType_)
            : base(EventType.DEQParamChanged)
        {
            this.moduleIndex = moduleIndex_;
            this.deq = deq_;
            this.paramType = paramType_;
        }
    }

    public class EQChangedArgs : EventBusArgs
    {
        public readonly byte moduleIndex;
        public readonly EQ eq;
        public EQChangedArgs(byte moduleIndex_, EQ eq_)
            : base(EventType.EQChanged)
        {
            moduleIndex = moduleIndex_;
            eq = eq_;
        }
    }

    public class CommStateChangedArgs : EventBusArgs
    {
        public readonly CommState commState;
        public CommStateChangedArgs(CommState commState_)
            : base(EventType.CommStateChanged)
        {
            this.commState = commState_;
        }
    }

    public class CommCompletedArgs : EventBusArgs
    {
        public readonly CommResult commResult;
        public CommCompletedArgs(CommResult commResult_)
            : base(EventType.CommCompleted)
        {
            commResult = commResult_;
        }
    }

    //public class CommCompletedArgs : EventBusArgs
    //{
    //    public readonly CommResult commResult;
    //    public CommCompletedArgs(CommResult commResult_)
    //        : base(EventType.CommSuccessful)
    //    {
    //        commResult = commResult_;
    //    }
    //}

    public class CommInterruptedArgs : EventBusArgs
    {
        public CommInterruptedArgs()
            : base(EventType.CommInterrupted)
        {
        }
    }

    public class ConnectionIsDisconnectedArgs : EventBusArgs
    {
        public ConnectionIsDisconnectedArgs()
            : base(EventType.ConnectionIsDisconnected)
        {
        }
    }

    public class DisconnectArgs : EventBusArgs
    {
        public DisconnectArgs()
            : base(EventType.Disconnect)
        {
        }
    }

    //public class ParamChangedArgs : EventBusArgs
    //{
    //    public readonly short value;
    //    public ParamChangedArgs(short value_)
    //        : base(EventType.ParamChanged)
    //    {
    //        this.value = value_;
    //    }
    //}

    public class EQGraphicNeedToBeUpdatedArgs : EventBusArgs
    {
        public EQGraphicNeedToBeUpdatedArgs()
            : base(EventType.EQGraphicNeedToBeUpdated)
        {
        }
    }

    public class CurrEQChangedFromGraphicArgs : EventBusArgs
    {
        public CurrEQChangedFromGraphicArgs()
            : base(EventType.CurrEQChangedFromGraphic)
        {
        }
    }

    public class CurrEQIndexChangedFromGraphicArgs : EventBusArgs
    {
        public CurrEQIndexChangedFromGraphicArgs()
            : base(EventType.CurrEQIndexChangedFromGraphic)
        {
        }
    }

    //public class HPFChangedFromGraphicArgs : EventBusArgs
    //{
    //    public readonly FilterElement filterElement;
    //    public HPFChangedFromGraphicArgs(FilterElement filterElement_)
    //        : base(EventType.HPFChangedFromGraphic)
    //    {
    //        this.filterElement = filterElement_;
    //    }
    //}

    //public class LPFChangedFromGraphicArgs : EventBusArgs
    //{
    //    public readonly FilterElement filterElement;
    //    public LPFChangedFromGraphicArgs(FilterElement filterElement_)
    //        : base(EventType.LPFChangedFromGraphic)
    //    {
    //        this.filterElement = filterElement_;
    //    }
    //}

    public class XPFChangedFromGraphicArgs : EventBusArgs
    {
        public readonly XPFilterType filterType;
        public readonly FilterElement filterElement;

        public XPFChangedFromGraphicArgs(XPFilterType xpFilterType_, FilterElement filterElement_)
            : base(EventType.XPFChangedFromGraphic)
        {
            this.filterType = xpFilterType_;
            this.filterElement = filterElement_;
        }
    }

    public class WorkModeChangedArgs : EventBusArgs
    {
        public WorkModeChangedArgs()
            : base(EventType.WorkModeChanged)
        {
        }
    }

    public class WorkSelectChangedArgs : EventBusArgs
    {
        public WorkSelectChangedArgs()
            : base(EventType.WorkSelectChanged)
        {
        }
    }

    //public class WorkSelectChangedArgs : EventBusArgs
    //{
    //    public WorkSelectChangedArgs()
    //        : base(EventType.WorkSelectChanged)
    //    {
    //    }
    //}

    public class VolumeChangedArgs : EventBusArgs
    {
        public readonly object sender;
        public VolumeChangedArgs(EventType type_, object sender_)
            : base(type_)
        {
            this.sender = sender_;
        }
    }

    public class MuiscMaxVolumeChangedArgs : VolumeChangedArgs
    {
        public MuiscMaxVolumeChangedArgs(object sender_)
            : base(EventType.MusicMaxVolumeChanged, sender_)
        {
        }
    }

    public class MicMaxVolumeChangedArgs : VolumeChangedArgs
    {
        public MicMaxVolumeChangedArgs(object sender_)
            : base(EventType.MicMaxVolumeChanged, sender_)
        {
        }
    }

    public class EffectMaxVolumeChangedArgs : VolumeChangedArgs
    {
        public EffectMaxVolumeChangedArgs(object sender_)
            : base(EventType.EffectMaxVolumeChanged, sender_)
        {
        }
    }

    public class MusciStartVolumeChangedArgs : VolumeChangedArgs
    {
        public MusciStartVolumeChangedArgs(object sender_)
            : base(EventType.MusicStartVolumeChanged, sender_)
        {
        }
    }

    public class MicStartVolumeChangedArgs : VolumeChangedArgs
    {
        public MicStartVolumeChangedArgs(object sender_)
            : base(EventType.MicStartVolumeChanged, sender_)
        {
        }
    }

    public class EffectStartVolumeChangedArgs : VolumeChangedArgs
    {
        public EffectStartVolumeChangedArgs(object sender_)
            : base(EventType.EffectStartVolumeChanged, sender_)
        {
        }
    }


    public class MusicVolumeChangedArgs : EventBusArgs
    {
        public MusicVolumeChangedArgs()
            : base(EventType.MusicVolumeChanged)
        {
        }
    }

    public class MicVolumeChangedArgs : EventBusArgs
    {
        public MicVolumeChangedArgs()
            : base(EventType.MicVolumeChanged)
        {
        }
    }

    public class EffectVolumeChangedArgs : EventBusArgs
    {
        public EffectVolumeChangedArgs()
            : base(EventType.EffectVolumeChanged)
        {
        }
    }

    //public class SystemFrontLevelLoadedArgs : EventBusArgs
    //{
    //    public readonly SystemFrontLevel systemFrontLevel;
    //    public SystemFrontLevelLoadedArgs(SystemFrontLevel systemFrontLevel_)
    //        : base(EventType.SystemFrontLevelLoaded)
    //    {
    //        systemFrontLevel = systemFrontLevel_;
    //    }
    //}

    //public class SystemEndLevelLoadedArgs : EventBusArgs
    //{
    //    public readonly SystemEndLevel systemEndLevel;
    //    public SystemEndLevelLoadedArgs(SystemEndLevel systemEndLevel_)
    //        : base(EventType.SystemEndLevelLoaded)
    //    {
    //        systemEndLevel = systemEndLevel_;
    //    }
    //}

    public class OutputLimiterStatusLoadedArgs : EventBusArgs
    {
        public readonly QuadChannelLimiterStatus limiterStatus;

        public OutputLimiterStatusLoadedArgs(QuadChannelLimiterStatus limiterStatus_)
            : base(EventType.OutputLimiterStatusLoaded)
        {
            limiterStatus = limiterStatus_;
        }
    }

    public class DeviceStatusLoadedArgs : EventBusArgs
    {
        public readonly DeviceStatus deviceStatus;

        public DeviceStatusLoadedArgs(DeviceStatus deviceStatus_)
            : base(EventType.DeviceStatusLoaded)
        {
            deviceStatus = deviceStatus_;
        }
    }   
    //public class SystemLevelLoadedArgs : EventBusArgs
    //{
    //    public readonly SystemLevel systemLevel;
    //    public SystemLevelLoadedArgs(SystemLevel systemLevel_)
    //        : base(EventType.SystemLevelLoaded)
    //    {
    //        systemLevel = systemLevel_;
    //    }
    //}

    public class PasswordUpdatedArgs : EventBusArgs
    {
        public readonly string passowrd;
        public PasswordUpdatedArgs(string passowrd_)
            : base(EventType.PasswordUpdated)
        {
            passowrd = passowrd_;
        }
    }

    public class PasswordChangedArgs : EventBusArgs
    {
        public readonly byte addr;
        public readonly string passowrd;
        public PasswordChangedArgs(byte addr_, string passowrd_)
            : base(EventType.PasswordChanged)
        {
            this.addr = addr_;
            this.passowrd = passowrd_;
        }
    }

    public class SaveDeviceModeArgs : EventBusArgs
    {
        public readonly short modeIndex;
        public readonly string modeName;

        public SaveDeviceModeArgs(short modeIndex_, string modeName_)
            : base(EventType.SaveDeviceMode)
        {
            this.modeIndex = modeIndex_;
            this.modeName = modeName_;
        }
    }

    public class UpdateCustomDeviceInfoEnableArgs : EventBusArgs
    {
        public readonly bool enable;

        public UpdateCustomDeviceInfoEnableArgs(bool enable_)
            : base(EventType.UpdateCustomDeviceInfoEnable)
        {
            this.enable = enable_;
        }
    }

    public class UpdateCustomDeviceInfoDataArgs : EventBusArgs
    {
        public readonly SystemCommProtocol.SystemCustomDeviceInfoData efd;
        public readonly Object data;

        public UpdateCustomDeviceInfoDataArgs(SystemCommProtocol.SystemCustomDeviceInfoData efd_, Object data_)
            : base(EventType.UpdateCustomDeviceInfoData)
        {
            this.efd = efd_;
            this.data = data_;
        }
    }

    public class UpdateModuleAllDataArgs : EventBusArgs
    {
        public readonly byte moduleIndex;
        public readonly List<short> dataList;

        public UpdateModuleAllDataArgs(byte moduleIndex_, List<short> dataList_)
            : base(EventType.UpdateModuleAllData)
        {
            this.moduleIndex = moduleIndex_;
            this.dataList = dataList_;
        }
    }

    public class UpdateUIFromDataArgs : EventBusArgs
    {
        public UpdateUIFromDataArgs()
            : base(EventType.UpdateUIFromData)
        {
           
        }
    }

    public class SystemUserChangedArgs : EventBusArgs
    {
        public SystemUserChangedArgs()
            : base(EventType.SystemUserChanged)
        {
        }
    }

    public class CommProgressUpdatedArgs : EventBusArgs
    {
        public readonly string msg;
        public readonly bool isEnd;
        public readonly float progressIncrement;
        public readonly bool isFailed;

        public CommProgressUpdatedArgs(string msg_, bool isEnd_ = false, float progressIncrement_ = 0.0f, bool isFailed_ = false)
            : base(EventType.CommProgressUpdated)
        {
            this.msg = msg_;
            this.isEnd = isEnd_;
            this.progressIncrement = progressIncrement_;
            this.isFailed = isFailed_;
        }
    }

    public class StartSaveModeArgs : EventBusArgs
    {
        public StartSaveModeArgs()
            : base(EventType.StartSaveMode)
        {
        }
    }

    public class SaveModeCompletedArgs : EventBusArgs
    {
        public SaveModeCompletedArgs()
            : base(EventType.SaveModeCompleted)
        {
        }
    }

    public class DeviceNameChangedArgs : EventBusArgs
    {
        public readonly string deviceName;

        public DeviceNameChangedArgs(string name_)
            : base(EventType.DeviceNameChanged)
        {
            this.deviceName = name_;
        }
    }

    public class UsageLimitLockUpdatedArgs : EventBusArgs
    {
        public readonly UsageLimitLock usageLimitLock;

        public UsageLimitLockUpdatedArgs(UsageLimitLock usageLimitLock_)
            : base(EventType.UsageLimitLockChanged)
        {
            this.usageLimitLock = usageLimitLock_;
        }
    }

    public class UpdateUsageLimitLockCompletedArgs : EventBusArgs
    {
        public readonly UsageLimitLock usageLimitLock;

        public UpdateUsageLimitLockCompletedArgs(UsageLimitLock usageLimitLock_)
            : base(EventType.UpdateUsageLimitLockCompleted)
        {
            this.usageLimitLock = usageLimitLock_;
        }
    }

    public class UnlockUsageLimitArgs : EventBusArgs
    {
        public UnlockUsageLimitArgs()
            : base(EventType.UnlockUsageLimit)
        {
        }
    }

    public class UsageLimitLockFormShowArgs : EventBusArgs
    {
        public readonly bool isShow = false;
        public UsageLimitLockFormShowArgs(bool isShow_)
            : base(EventType.UsageLimitLockFormShow)
        {
            this.isShow = isShow_;
        }
    }

    public class MicABLinkedArgs : EventBusArgs
    {
        public readonly bool isLink;
        public readonly MicChannel channel;
        public MicABLinkedArgs(bool isLink_, MicChannel channel_)
            : base(EventType.MicABLinked)
        {
            this.isLink = isLink_;
            this.channel = channel_;
        }
    }

    //public class ShowSignalLevelArgs : EventBusArgs
    //{
    //    public readonly bool isShow;
    //    public ShowSignalLevelArgs(bool isShow_)
    //        : base(EventType.ShowSignalLevel)
    //    {
    //        this.isShow = isShow_;
    //    }
    //}

    

    public enum OutputParamChangedSource
    {
        OutputPanel,
        MonitorPanel,
        HpfFreqChanged,
    }

    public enum LimiterParam
    {
        All,
        Threshold,
        Attack,
        Release,
        ClipThreshold,
        ClipRelease,
        Auto,
    }

    public enum OutputParam
    {
        LeftChDelay,
        RightChDelay,
        Delay,
        LeftChMute,
        RightChMute,
        Mute,
        LeftChPhase,
        RightChPhase,
        Phase,
    }

    public class OutputParamChangedArgs : EventBusArgs
    {
        public readonly OutputChannelType channel;
        public readonly OutputParamChangedSource source;
        public readonly OutputParam param;

        public OutputParamChangedArgs(
            OutputChannelType channel_,
            OutputParamChangedSource source_,
            OutputParam param_)
            : base(EventType.OutputParamChanged)
        {
            this.channel = channel_;
            this.source = source_;
            this.param = param_;
        }
    }

    public class OutputMuteChangedFromNavigatorArgs : EventBusArgs
    {
        public readonly OutputChannelType channel;
        public OutputMuteChangedFromNavigatorArgs(OutputChannelType channel_)
            : base(EventType.OutputMuteChangedFromNavigator)
        {
            channel = channel_;
        }
    }

    public class OutputMuteChangedFromPanelArgs : EventBusArgs
    {
        public readonly OutputChannelType channel;
        public OutputMuteChangedFromPanelArgs(OutputChannelType channel_)
            : base(EventType.OutputMuteChangedFromPanel)
        {
            channel = channel_;
        }
    }

    public class InputMuteChangedArgs : EventBusArgs
    {
        public readonly InputMuteOperateSource operateSource;
        public readonly MuteSource muteSource;

        public InputMuteChangedArgs(InputMuteOperateSource operateSource_, MuteSource muteSource_)
            : base(EventType.InputMuteChanged)
        {
            this.operateSource = operateSource_;
            this.muteSource = muteSource_;
        }
    }

    public class OutputMuteChangedArgs : EventBusArgs
    {
        public readonly MuteSource source;
        public readonly bool mute;
        public OutputMuteChangedArgs(MuteSource source_, bool mute_)
            : base(EventType.OutputMuteChanged)
        {
            this.source = source_;
            mute = mute_;
        }
    }

    public class LimiterThresholdUnitChangedArgs : EventBusArgs
    {
        public LimiterThresholdUnitChangedArgs()
            : base(EventType.LimiterThresholdUnitChanged)
        {
        }
    }

    public class MixerLevelUnitChangedArgs : EventBusArgs
    {
        public MixerLevelUnitChangedArgs()
            : base(EventType.MixerLevelUnitChanged)
        {
        }
    }

    public class ChannelDelayUnitChangedArgs : EventBusArgs
    {
        public ChannelDelayUnitChangedArgs()
            : base(EventType.ChannelDelayUnitChanged)
        {
        }
    }

    public class UpdateRTADisplayArgs : EventBusArgs
    {
        public UpdateRTADisplayArgs()
            : base(EventType.UpdateRTADisplay)
        {
        }
    }

    public enum RTAOperate
    {
        TurnOnRTA,
        TurnOffRTA,
        TurnOnOutput,
        TurnOffOutput,
    }

    public class RTATryOperateArgs : EventBusArgs
    {
        public readonly RTAOperate rtaOperate;

        public RTATryOperateArgs(RTAOperate _rtaOperate)
            : base(EventType.RTATryOperate)
        {
            rtaOperate = _rtaOperate;
        }
    }

    public class OperateResult
    {
        public readonly bool success;
        public readonly string error;

        public OperateResult(bool _success = false, string _error = "")
        {
            this.success = _success;
            this.error = _error;
        }
    }

    public class RTAOperateCompletedArgs : EventBusArgs
    {
        public readonly RTAOperate operate;
        public readonly OperateResult result;

        public RTAOperateCompletedArgs(RTAOperate _operate, OperateResult _result)
            : base(EventType.RTAOperateCompleted)
        {
            this.operate = _operate;
            this.result = _result;
        }
    }

    public class AudioInputLevelUpdatedArgs : EventBusArgs
    {
        public readonly double left;
        public readonly double right;

        public AudioInputLevelUpdatedArgs(double left_, double right_)
            : base(EventType.AudioInputLevelUpdated)
        {
            this.left = left_;
            this.right = right_;
        }
    }

    public class RecvMeasDelayDataProgressChangedArgs : EventBusArgs
    {
        public readonly float progress;

        public RecvMeasDelayDataProgressChangedArgs(float progress_)
            : base(EventType.RecvMeasDelayDataProgressChanged)
        {
            this.progress = progress_;
        }
    }

    public class MeasDelayCompletedArgs : EventBusArgs
    {
        public readonly short delayOffset;

        public MeasDelayCompletedArgs(short delayOffset_)
            : base(EventType.MeasDelayCompleted)
        {
            this.delayOffset = delayOffset_;
        }
    }

    public class LanguageChangedArgs : EventBusArgs
    {
        public LanguageChangedArgs()
            : base(EventType.LanguageChanged)
        {
        }
    }

    public class EQGraphicPanelJumpArgs : EventBusArgs
    {
        public readonly ModuleBase module;
        public EQGraphicPanelJumpArgs(ModuleBase module_)
            : base(EventType.EQGraphicPanelJump)
        {
            this.module = module_;
        }
    }

    public class OutputLimiterParamChangedArgs : EventBusArgs
    {
        public readonly OutputChannelType channel;
        public readonly OutputParamChangedSource source;
        public readonly LimiterParam limiterParam;

        public OutputLimiterParamChangedArgs(OutputChannelType channel_, OutputParamChangedSource source_, LimiterParam limiterParam_)
            : base(EventType.OutputLimiterParamChanged)
        {
            this.channel = channel_;
            this.source = source_;
            this.limiterParam = limiterParam_;
        }
    }

    public class RCMessageArgs : EventBusArgs
    {
        public readonly string message;
        public RCMessageArgs(string message_)
            : base(EventType.RCMessage)
        {
            this.message = message_;
        }
    }

    public class RCConnectToServerArgs : EventBusArgs
    {
        public RCConnectToServerArgs()
            : base(EventType.RCConnectToServer)
        {
        }
    }

    public class RCHandshakeCompletedArgs : EventBusArgs
    {
        public readonly bool success;
        public readonly string message;
        public RCHandshakeCompletedArgs(bool success_, string message_ = "")
            : base(EventType.RCHandshakeCompleted)
        {
            this.success = success_;
            this.message = message_;
        }
    }

    public class RCDisconnectFromServerArgs : EventBusArgs
    {
        public RCDisconnectFromServerArgs()
            : base(EventType.RCDisconnectFromServer)
        {
            
        }
    }

    public class RCDisconnectedFromServerArgs : EventBusArgs
    {
        public readonly bool rcConnected;
        public readonly string message;
        public RCDisconnectedFromServerArgs(bool rcConnected_, string message_ = "")
            : base(EventType.RCDisconnectedFromServer)
        {
            this.rcConnected = rcConnected_;
            this.message = message_;
        }
    }

    public class RCDisconnectFromRcArgs : EventBusArgs
    {
        public RCDisconnectFromRcArgs()
            : base(EventType.RCDisconnectFromRc)
        {

        }
    }

    public class RCDisconnectedFromRcArgs : EventBusArgs
    {
        public readonly string message;
        public RCDisconnectedFromRcArgs(string message_ = "")
            : base(EventType.RCDisconnectedFromRc)
        {
            this.message = message_;
        }
    }

    public class RCOperationStatusChangedArgs : EventBusArgs
    {
        public readonly bool isOn;
        public RCOperationStatusChangedArgs(bool isOn_)
            : base(EventType.RCOperationStatusChanged)
        {
            this.isOn = isOn_;
        }
    }

    public class RCConnectionStatusChangedArgs : EventBusArgs
    {
        public readonly bool isConnected;
        public RCConnectionStatusChangedArgs(bool isConnected_)
            : base(EventType.RCConnectionStatusChanged)
        {
            this.isConnected = isConnected_;
        }
    }

    public class RCLcConnectToRcToLcRespArgs : EventBusArgs
    {
        public readonly bool success;
        public readonly string message;
        public RCLcConnectToRcToLcRespArgs(bool success_, string message_ = "")
            : base(EventType.RCLcConnectToRcToLcResp)
        {
            this.success = success_;
            this.message = message_;
        }
    }

    public class RCLcConnectToRcToRcRespArgs : EventBusArgs
    {
        //public readonly string message;
        public RCLcConnectToRcToRcRespArgs()
            : base(EventType.RCLcConnectToRcToRcResp)
        {
            //this.message = message_;
        }
    }

    public class RCProModeConnectAndLoadDataArgs : EventBusArgs
    {
        public RCProModeConnectAndLoadDataArgs()
            : base(EventType.RCProModeConnectAndLoadData)
        {
        }
    }

    public class RCEasyModeConnectAndLoadDataArgs : EventBusArgs
    {
        public RCEasyModeConnectAndLoadDataArgs()
            : base(EventType.RCEasyModeConnectAndLoadData)
        {
        }
    }

    public class RCProModeCallDeviceModeArgs : EventBusArgs
    {
        public readonly short modeIndex;
        public RCProModeCallDeviceModeArgs(short modeIndex_)
            : base(EventType.RCProModeCallDeviceMode)
        {
            this.modeIndex = modeIndex_;
        }
    }

    public class RCEasyModeCallDeviceModeArgs : EventBusArgs
    {
        public readonly short modeIndex;
        public RCEasyModeCallDeviceModeArgs(short modeIndex_)
            : base(EventType.RCEasyModeCallDeviceMode)
        {
            this.modeIndex = modeIndex_;
        }
    }

    public class RCProModeUpdateToBeUploadedDataArgs : EventBusArgs
    {
        public readonly EffectorModelBase effectorModel;
        public RCProModeUpdateToBeUploadedDataArgs(EffectorModelBase effectorModel_)
            : base(EventType.RCProModeUpdateToBeUploadedData)
        {
            this.effectorModel = effectorModel_;
        }
    }

    public class RCProModeUploadDataArgs : EventBusArgs
    {
        public RCProModeUploadDataArgs()
            : base(EventType.RCProModeUploadData)
        {
        }
    }

    public class RCEasyUploadMultiPresetDataArgs : EventBusArgs
    {
        public readonly object modeList;
        public RCEasyUploadMultiPresetDataArgs(object modeList_)
            : base(EventType.RCEasyUploadMultiPresetData)
        {
            this.modeList = modeList_;
        }
    }

    public class RCForcedDisconnectArgs : EventBusArgs
    {
        public RCForcedDisconnectArgs()
            : base(EventType.RCForcedDisconnect)
        {
        }
    }

    public class RCReadSystemSatatusArgs : EventBusArgs
    {
        public RCReadSystemSatatusArgs()
            : base(EventType.RCReadSystemSatatus)
        {
        }
    }

    public class LCNotConnectedToRCArgs : EventBusArgs
    {
        public LCNotConnectedToRCArgs()
            : base(EventType.LCNotConnectedToRC)
        {
        }
    }

    public interface IEventHanler<TEventArgs>
    {
        void EventBusHandler(object sender, TEventArgs e);
    }
}
