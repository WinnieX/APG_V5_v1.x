using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Common;
using AEC.Misc;
using AEC.Effectors;
using AEC.Effectors.X5;
using AEC.Effectors.VKxS;
using AEC.Effectors.VKxDM;
using AEC.Effectors.KX;
using AEC.Effectors.VKxSCM;
using AEC.Effectors.VKxDCM;
using AEC.Effectors.VKxPII;
using AEC.Effectors.VKxPIIDCM;
using AEC.Effectors.VX;
using AEC.Effectors.MX;
using AEC.Comm;
using AEC.Comm.X5Comm;
using AEC.Comm.VKxSComm;
using AEC.Comm.VKxDMComm;
using AEC.Comm.KXComm;
using AEC.Comm.VKxSCMComm;
using AEC.Comm.VKxDCMComm;
using AEC.Comm.VKxPIIComm;
using AEC.Comm.VKxPIIDCMComm;
using AEC.Comm.VXComm;
using AEC.Comm.MXComm;
using AEC.RemoteControl;

namespace AEC
{
    public class MController
    {
        protected static Localization localization = Config.GetInstance().localization;

        public readonly CustomModeDataFileManager customModeDataFileManager;
        //private static DeviceModel DefaultDeviceModel = DeviceModel.M_Audio;
        
        public readonly MAudioEffectorModel mEffectorModel;
        public readonly MEffectorSeralizer mEffectorSeralizer;
        public readonly MEffectorThreshold mEffectorThreshold;
        public readonly MEffectorFeatures mEffectorFeatures;

        public Dictionary<MEffectorFamily, MEffector> mEffectorMap = new Dictionary<MEffectorFamily, MEffector>();

        private readonly EventBus commEvent = new EventBus();
        private readonly EventBus effectorEvent = new EventBus();
        public readonly EventBus controllerEvent = new EventBus();

        private readonly MCommService commSvc;

        public readonly EventBus rcLocalEvent = new EventBus();
        private readonly EventBus rcRemoteEvent = new EventBus();
        public readonly EventBus rcEvent = new EventBus();
        public readonly RCClientEngine rcClientEngine;

        public bool isSuperUserMode = false;   //超级用户模式

        public bool _isConnected = false;       //是否已经联机状态
        public bool isConnected
        {
            get
            {
                return _isConnected;
            }
            private set
            {
                _isConnected = value;
            }
        }

        private CommState _commState = CommState.Disconnected;  //通讯状态
        public CommState commState
        {
            get
            {
                return this._commState;
            }
            private set
            {
                if (this._commState != value)
                {
                    _commState = value;
                    this.controllerEvent.NotifyObeserver(new CommStateChangedArgs(_commState));
                    //Log.OutputLine("通讯状态改变****************1");
                    //try
                    //{
                    //    this.commEvent.NotifyObeserver(new CommStateChangedArgs());
                    //}
                    //catch(Exception ex)
                    //{
                    //    Log.OutputLine("异常:{0}", ex.Message);
                    //}
                    //Log.OutputLine("通讯状态改变****************2");
                }
            }
        }

        public string connectionInfo
        {
            get
            {
                return this.commSvc.connectionInfo;
            }
        }

        private MEffectorFamily _currEffectorFamily;

        public MEffectorFamily currEffectorFamily
        {
            get
            {
                return this._currEffectorFamily;
            }
            private set
            {
                this._currEffectorFamily = value;
                Core.DSP.DSPHelper.SwitchSampleRate(GetEffectorFamilySampleRate(this._currEffectorFamily));
                Core.DSP.DSPHelper.SwitchEQMode(GetEffectorFamilyEQMode(this._currEffectorFamily));
            }
        }

        private MEffectorFamily nextEffectorFamily
        {
            get;
            set;
        }

        public MEffector currEffector
        {
            get
            {
                if(this.mEffectorMap.ContainsKey(this.currEffectorFamily))
                {
                    return this.mEffectorMap[this.currEffectorFamily];
                }
                else
                {
                    this.mEffectorMap[this.currEffectorFamily] = CreateMEffector(this.currEffectorFamily);

                    return this.mEffectorMap[this.currEffectorFamily];
                }
            }
        }

        public EffectorModelBase currEffectorModel
        {
            get
            {
                return currEffector.effectorModel;
            }
        }

        public X5EffectorModel x5EffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.X5))
                {
                    return (X5EffectorModel)this.mEffectorMap[MEffectorFamily.X5].effectorModel;
                }

                return null;
            }
        }

        public VKxSEffectorModel vkxsEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.VKxS))
                {
                    return (VKxSEffectorModel)this.mEffectorMap[MEffectorFamily.VKxS].effectorModel;
                }

                return null;
            }
        }

        public KXEffectorModel kxEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.KX))
                {
                    return (KXEffectorModel)this.mEffectorMap[MEffectorFamily.KX].effectorModel;
                }

                return null;
            }
        }

        public VKxSCMEffectorModel vkxscmEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.VKxSCM))
                {
                    return (VKxSCMEffectorModel)this.mEffectorMap[MEffectorFamily.VKxSCM].effectorModel;
                }

                return null;
            }
        }

        public VKxDCMEffectorModel vkxdcmEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.VKxDCM))
                {
                    return (VKxDCMEffectorModel)this.mEffectorMap[MEffectorFamily.VKxDCM].effectorModel;
                }

                return null;
            }
        }

        public VKxPIIEffectorModel vkxPIIEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.VKxPII))
                {
                    return (VKxPIIEffectorModel)this.mEffectorMap[MEffectorFamily.VKxPII].effectorModel;
                }

                return null;
            }
        }

        public VKxPIIDCMEffectorModel vkxPIIdcmEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.VKxPIIDCM))
                {
                    return (VKxPIIDCMEffectorModel)this.mEffectorMap[MEffectorFamily.VKxPIIDCM].effectorModel;
                }

                return null;
            }
        }

        public VKxDMEffectorModel vkxsdmEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.VKxDM))
                {
                    return (VKxDMEffectorModel)this.mEffectorMap[MEffectorFamily.VKxDM].effectorModel;
                }

                return null;
            }
        }

        public VXEffectorModel vxEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.VX))
                {
                    return (VXEffectorModel)this.mEffectorMap[MEffectorFamily.VX].effectorModel;
                }

                return null;
            }
        }

        public MXEffectorModel mxEffectorModel
        {
            get
            {
                if (this.mEffectorMap.ContainsKey(MEffectorFamily.MX))
                {
                    return (MXEffectorModel)this.mEffectorMap[MEffectorFamily.MX].effectorModel;
                }

                return null;
            }
        }

        public bool isSupportLoadDeviceFirmwareVersion
        {
            get
            {
                return _currEffectorFamily == MEffectorFamily.VKxS || _currEffectorFamily == MEffectorFamily.X5;
            }
        }

        public MController(string startupPath)
        {
            this.customModeDataFileManager = new CustomModeDataFileManager(startupPath);

            DeviceModel defaultDeviceModel = ProductsConfig.GetDeviceModelFromModelLabel(Config.GetInstance().defaultDevcieModel);
            //初始化效果器型号
            this.mEffectorModel = new MAudioEffectorModel(defaultDeviceModel);
            this.mEffectorSeralizer = new MEffectorSeralizer(this.mEffectorModel);
            this.mEffectorThreshold = new MEffectorThreshold(this.mEffectorModel);
            this.mEffectorFeatures = new MEffectorFeatures(this.mEffectorModel);

            //默认的效果器家族
            this.currEffectorFamily = ProductsConfig.EffectorModelToFamily(this.mEffectorModel.currModel);
            this.nextEffectorFamily = this.currEffectorFamily;

            //创建通讯服务模块
            this.commSvc = new MCommService(commEvent);

            //创建默认效果器家族
            this.mEffectorMap[this.currEffectorFamily] = CreateMEffector(this.currEffectorFamily);
            this.mEffectorMap[this.currEffectorFamily].effectorModel.UpdateEffecorFeatures(this.mEffectorFeatures, this.mEffectorModel.currModel);

            //注册通讯服务
            this.commSvc.RegisterCommService(this.mEffectorMap[this.currEffectorFamily].commService);

            //创建远程控制引擎
            this.rcClientEngine = new RCClientEngine(rcLocalEvent, rcRemoteEvent, this.mEffectorSeralizer);

            this.commEvent.RegisterHandler(CommEventHandler);
            this.effectorEvent.RegisterHandler(EffectorEventHandler);            
            this.rcLocalEvent.RegisterHandler(RCLocalEventEventHandler);
            this.rcRemoteEvent.RegisterHandler(RCRemoteEventEventHandler);
        }

        //切换当前设备型号
        public bool SwitchCurrDeviceModel(DeviceModel deviceModel)
        {
            if(this.mEffectorModel.currModel == deviceModel)
            {
                return false;
            }

            this.mEffectorModel.currModel = deviceModel;
            this.mEffectorModel.nextModel = deviceModel;

            this.nextEffectorFamily = ProductsConfig.EffectorModelToFamily(deviceModel);

            if (this.currEffectorFamily != this.nextEffectorFamily)
            {
                this.currEffectorFamily = this.nextEffectorFamily;    //数据加载完成后,同步数据

                if (!this.mEffectorMap.ContainsKey(this.nextEffectorFamily))
                {
                    this.mEffectorMap[this.nextEffectorFamily] = CreateMEffector(this.nextEffectorFamily);
                }

                this.commSvc.RegisterCommService(this.mEffectorMap[this.nextEffectorFamily].commService);
            }

            this.currEffectorModel.UpdateEffecorFeatures(this.mEffectorFeatures, deviceModel);

            return true;
        }

        private MEffector CreateMEffector(MEffectorFamily effectorFamily_)
        {
            switch(effectorFamily_)
            {
                case MEffectorFamily.X5:
                    X5EffectorModel x5EffectorModel = new X5EffectorModel(this.effectorEvent, this.mEffectorModel);
                    X5CommService x5CommService = new X5CommService(x5EffectorModel, this.commSvc);
                    return new MEffector(x5EffectorModel, x5CommService);

                case MEffectorFamily.VKxS:
                    VKxSEffectorModel vkxsEffectorModel = new VKxSEffectorModel(this.effectorEvent, this.mEffectorModel);
                    VKxSCommService vkxsCommService = new VKxSCommService(vkxsEffectorModel, this.commSvc);
                    return new MEffector(vkxsEffectorModel, vkxsCommService);

                case MEffectorFamily.VKxDM:
                    VKxDMEffectorModel vkxsdmEffectorModel = new VKxDMEffectorModel(this.effectorEvent, this.mEffectorModel);
                    VKxDMCommService vkxsdmCommService = new VKxDMCommService(vkxsdmEffectorModel, this.commSvc);
                    return new MEffector(vkxsdmEffectorModel, vkxsdmCommService);

                case MEffectorFamily.KX:
                    KXEffectorModel kxEffectorModel = new KXEffectorModel(this.effectorEvent, this.mEffectorModel);
                    KXCommService kxCommService = new KXCommService(kxEffectorModel, this.commSvc);
                    return new MEffector(kxEffectorModel, kxCommService);

                case MEffectorFamily.VKxSCM:
                    VKxSCMEffectorModel vkxscmEffectorModel = new VKxSCMEffectorModel(this.effectorEvent, this.mEffectorModel);
                    VKxSCMCommService vkxscmCommService = new VKxSCMCommService(vkxscmEffectorModel, this.commSvc);
                    return new MEffector(vkxscmEffectorModel, vkxscmCommService);

                case MEffectorFamily.VKxDCM:
                    VKxDCMEffectorModel vkxdcmEffectorModel = new VKxDCMEffectorModel(this.effectorEvent, this.mEffectorModel);
                    VKxDCMCommService vkxdcmCommService = new VKxDCMCommService(vkxdcmEffectorModel, this.commSvc);
                    return new MEffector(vkxdcmEffectorModel, vkxdcmCommService);

                case MEffectorFamily.VKxPII:
                    VKxPIIEffectorModel vkxPIIEffectorModel = new VKxPIIEffectorModel(this.effectorEvent, this.mEffectorModel);
                    VKxPIICommService vkxPIICommService = new VKxPIICommService(vkxPIIEffectorModel, this.commSvc);
                    return new MEffector(vkxPIIEffectorModel, vkxPIICommService);

                case MEffectorFamily.VKxPIIDCM:
                    VKxPIIDCMEffectorModel vkxPIIdcmEffectorModel = new VKxPIIDCMEffectorModel(this.effectorEvent, this.mEffectorModel);
                    VKxPIIDCMCommService vkxPIIdcmCommService = new VKxPIIDCMCommService(vkxPIIdcmEffectorModel, this.commSvc);
                    return new MEffector(vkxPIIdcmEffectorModel, vkxPIIdcmCommService);

                case MEffectorFamily.VX:
                    VXEffectorModel vxEffectorModel = new VXEffectorModel(this.effectorEvent, this.mEffectorModel);
                    VXCommService vxCommService = new VXCommService(vxEffectorModel, this.commSvc);
                    return new MEffector(vxEffectorModel, vxCommService);

                case MEffectorFamily.MX:
                    MXEffectorModel mxEffectorModel = new MXEffectorModel(this.effectorEvent, this.mEffectorModel);
                    MXCommService mxCommService = new MXCommService(mxEffectorModel, this.commSvc);
                    return new MEffector(mxEffectorModel, mxCommService);

                default:
                    throw(new Exception("MController.CreateMEffector 参数错误!"));
            }
        }

        private void EffectorEventHandler(object sender, EventBusArgs e)
        {
            if (this.rcClientEngine.localRCModeIsOn) //如果是远程模式
            {
                AEC.Tools.LogHelper.WriteInfo("MController.EffectorEventHandler()...type: {0} ", e.type);
                this.rcClientEngine.localClient.SendEffectorEvent(e);
            }
            else
            {
                this.commEvent.NotifyObeserver(e);
            }
        }

        private void CommEventHandler(object sender, EventBusArgs e)
        {
            if(rcClientEngine.remoteRCModeIsOn) //如果是远程模式
            {
                //AEC.Log.OutputLine("MController.CommEventHandler()...type:{0}", e.type);
                if(e.type == EventType.LoadEffectorModelCompleted)
                {
                    var args = e as LoadEffectorModelCompletedArgs;
                    this.LoadEffectorModelCompletedHandler(args.model);
                }
                else if (e.type == EventType.LoadDataCompleted)
                {
                    var args = e as LoadDataCompletedArgs;
                    this.LoadDataCompletedHandler(args.commProcess, args.effectorModel);
                }

                switch(e.type)
                {
                    case EventType.CommInterrupted:                 //通讯中断
                    case EventType.CommStateChanged:                //通讯状态发生变化
                    case EventType.CommProgressUpdated:             //通讯进度
                    case EventType.StartSaveMode:                   //开始保存模式
                    case EventType.SaveModeCompleted:               //保存模式完成
                    case EventType.ConnectionIsDisconnected:        //断开连接
                    case EventType.CommCompleted:                   //通讯完成
                    case EventType.OutputLimiterStatusLoaded:       //输出压限状态已经加载得到
                    case EventType.LoadEffectorModelCompleted:      //效果器型号加载完成
                    case EventType.LoadDataCompleted:               //加载数据完成
                    case EventType.CallDeviceModeCompleted:         //调用模式数据完成
                    case EventType.UploadDataCompleted:             //上传模式数据完成
                        rcClientEngine.remoteClient.SendCommEvent(e);
                        break;
                    default:
                        return;
                }
            }
            else
            {
                if (e.type == EventType.CommStateChanged)
                {
                    UpdateCommStatus(e as CommStateChangedArgs);
                }
                else if(e.type == EventType.LoadEffectorModelCompleted)
                {
                    var args = e as LoadEffectorModelCompletedArgs;
                    this.LoadEffectorModelCompletedHandler(args.model);
                }
                else if(e.type == EventType.LoadDataCompleted)
                {
                    var args = e as LoadDataCompletedArgs;
                    this.LoadDataCompletedHandler(args.commProcess, args.effectorModel);
                }
                else if(e.type == EventType.CallDeviceModeCompleted)
                {
                    var args = e as CallDeviceModeCompletedArgs;
                    this.CallDeviceModeCompletedHandler(args.commProcess, args.effectorModel);
                }
                else if(e.type == EventType.UploadDataCompleted)
                {
                    var args = e as UploadDataCompletedArgs;
                    this.UploadDataCompletedHandler(args.effectorModel);
                }
                else
                {
                    this.controllerEvent.NotifyObeserver(e);
                }
            }
        }

        private void UpdateCommStatus(CommStateChangedArgs args)
        {
            this._isConnected = args.commState == CommState.Connected;
            this.commState = args.commState;
        }

        //加载效果器型号数据
        private void LoadEffectorModelCompletedHandler(DeviceModel model)
        {
            AEC.Tools.LogHelper.WriteInfo("LoadEffectorModelCompletedHandler()...Device Model: {0}", model);

            this.mEffectorModel.nextModel = model;
            this.nextEffectorFamily = ProductsConfig.EffectorModelToFamily(model);
            if (!this.mEffectorMap.ContainsKey(this.nextEffectorFamily))
            {
                this.mEffectorMap[this.nextEffectorFamily] = CreateMEffector(this.nextEffectorFamily);
            }

            this.mEffectorMap[this.nextEffectorFamily].commService.UpdateTempEffecorModelFeatures(this.mEffectorFeatures, model);

            this.commSvc.RegisterCommService(this.mEffectorMap[this.nextEffectorFamily].commService);
        }

        private void LoadDataCompletedHandler(CommProcess commProcess, EffectorModelBase effectorModel)
        {
            this.mEffectorModel.currModel = this.mEffectorModel.nextModel;

            if(Config.GetInstance().useLastConnectedModelWhenStarts)
            {
                Config.GetInstance().defaultDevcieModel = MAudioEffectorModel.GetModelLabel(this.mEffectorModel.currModel);
            }

            if (this.currEffectorFamily != this.nextEffectorFamily)
            {
                this.currEffectorFamily = this.nextEffectorFamily;    //数据加载完成后,同步数据

                this.commEvent.NotifyObeserver(new CommProgressUpdatedArgs(localization[Localization.comm_InitializingUI], isEnd_: true));
            }

            MEffector nextEffector = this.mEffectorMap[this.nextEffectorFamily];

            Debug.Assert(nextEffector != null);

            EffectorModelBase nextEffectorModel = nextEffector.effectorModel;

            nextEffector.effectorModel.UpdateEffecorFeatures(this.mEffectorFeatures, this.mEffectorModel.currModel);

            nextEffectorModel.CopyFrom(effectorModel, true);

            this.OnDataLoadedCompletedHandler();

            if (commProcess == CommProcess.ProModeConnectLoadData || commProcess == CommProcess.ProModeSwitchDeviceWorkMode)
            {
                nextEffectorModel.InitFreqRespAndEQGraphic();
            }
            else if (commProcess == CommProcess.EasyModeConnectLoadData)
            {
                //this.currEffector.deviceKeyIsLocked = effector.system.isLockKeyboard.value;
                //this.currEffector.isLockUserOperation = effector.system.currUser == User.Normal;
            }
        }

        private void CallDeviceModeCompletedHandler(CommProcess commProcess, EffectorModelBase effectorModel)
        {
            EffectorModelBase currEffectorModel = this.currEffector.effectorModel;

            currEffectorModel.CopyFrom(effectorModel, false);

            currEffectorModel.system.deviceModeCallIndex.initV = effectorModel.system.deviceModeCallIndex.v;

            this.OnDataLoadedCompletedHandler();

            if (commProcess == CommProcess.ProModeCallDeviceMode)
            {
                if (!currEffectorModel.isSupportDataEncryption || !currEffectorModel.dataIsPrivate)
                {
                    currEffectorModel.InitFreqRespAndEQGraphic();
                }
            }
        }

        private void UploadDataCompletedHandler(EffectorModelBase effectorModel)
        {
            EffectorModelBase currEffectorModel = this.currEffector.effectorModel;

            currEffectorModel.CopyFrom(effectorModel, false);

            if (!currEffectorModel.isSupportDataEncryption || !currEffectorModel.dataIsPrivate)
            {
                currEffectorModel.InitFreqRespAndEQGraphic();
            }
        }

        private void RCLocalEventEventHandler(object sender, EventBusArgs e)
        {
            if (rcClientEngine.localRCModeIsOn) //如果是远程模式
            {
                AEC.Tools.LogHelper.WriteInfo("MController.RCLocalEventEventHandler()...type: {0} ", e.type);

                if (this._isConnected)
                {
                    if (e.type == EventType.RCDisconnectFromServer ||
                    e.type == EventType.RCDisconnectedFromServer ||
                    e.type == EventType.RCDisconnectedFromRc ||
                    e.type == EventType.RCDisconnectFromRc)
                    {
                        this._isConnected = false;

                        return;
                    }
                }

                if (e.type == EventType.CommStateChanged)       //通讯状态发生变化
                {
                    UpdateCommStatus(e as CommStateChangedArgs);
                }
                else if (e.type == EventType.LoadEffectorModelCompleted)     //加载得到效果器型号
                {
                    var args = e as LoadEffectorModelCompletedArgs;
                    this.LoadEffectorModelCompletedHandler(args.model);
                }
                else if(e.type == EventType.LoadDataCompleted)          //加载数据完成
                {
                    var args = e as LoadDataCompletedArgs;
                    this.LoadDataCompletedHandler(args.commProcess, args.effectorModel);
                }
                else if(e.type == EventType.CallDeviceModeCompleted)
                {
                    var args = e as CallDeviceModeCompletedArgs;
                    this.CallDeviceModeCompletedHandler(args.commProcess, args.effectorModel);
                }
                else if(e.type == EventType.UploadDataCompleted)
                {
                    var args = e as UploadDataCompletedArgs;
                    this.UploadDataCompletedHandler(args.effectorModel);
                }
                else
                {
                    this.controllerEvent.NotifyObeserver(e);
                }
            }
        }

        private void RCRemoteEventEventHandler(object sender, EventBusArgs e)
        {
            if (!rcClientEngine.remoteRCModeIsOn) //如果是远程模式
            {
                return;
            }

            //AEC.Log.OutputLine("RCRemoteEventEventHandler()...type:{0}", e.type);

            if (this.commSvc.isConnected)
            {
                if (e.type == EventType.RCDisconnectFromServer ||
                    e.type == EventType.RCDisconnectedFromServer ||
                    e.type == EventType.RCDisconnectedFromRc ||
                    e.type == EventType.RCDisconnectFromRc)
                {
                    this.commSvc.ForcedDisconnect();
                    return;
                }
            }

            switch(e.type)
            {
                case EventType.RCProModeConnectAndLoadData:
                    this.commSvc.AsyncProModeConnectAndLoadData();
                    break;

                case EventType.RCEasyModeConnectAndLoadData:
                    this.commSvc.AsyncEasyModeConnectAndLoadData();
                    break;

                case EventType.RCProModeCallDeviceMode:
                    this.commSvc.AsyncProModeCallDeviceMode((e as RCProModeCallDeviceModeArgs).modeIndex);
                    break;

                case EventType.RCEasyModeCallDeviceMode:
                    this.commSvc.AsyncEasyModeCallDeviceMode((e as RCEasyModeCallDeviceModeArgs).modeIndex);
                    break;

                case EventType.RCProModeUpdateToBeUploadedData:
                    {
                        RCProModeUpdateToBeUploadedDataArgs args = e as RCProModeUpdateToBeUploadedDataArgs;
                        this.commSvc.UpdateToBeUploadedData(args.effectorModel);
                    }
                    break;

                case EventType.RCProModeUploadData:
                    this.commSvc.AsyncProModeUploadData2Device();
                    break;

                case EventType.RCEasyUploadMultiPresetData:
                    {
                        RCEasyUploadMultiPresetDataArgs args = e as RCEasyUploadMultiPresetDataArgs;
                        this.commSvc.AsyncEasyModeUploadMultiModeData(args.modeList);
                    }
                    break;

                case EventType.RCForcedDisconnect:
                    this.commSvc.ForcedDisconnect();
                    break;

                case EventType.RCReadSystemSatatus:
                    this.commSvc.ReadSystemSatatus();
                    break;

                case EventType.DataParamChanged:
                case EventType.EQChanged:
                case EventType.PasswordChanged:
                case EventType.SaveDeviceMode:
                case EventType.UpdateModuleAllData:
                    this.commEvent.NotifyObeserver(e);
                    break;
            }
        }

        public void StopService()
        {
            this.commSvc.StopService();
        }

        //能否连接设备，如果使远程模式下，未连接到远程主机，则不能连接设备
        public bool CanConnectToDevice()
        {
            if (this.rcClientEngine.localRCModeIsOn && 
                !this.rcClientEngine.localClient.rcConnected)
            {
                this.rcEvent.NotifyObeserver(new LCNotConnectedToRCArgs());
                return false;
            }

            return true;
        }

        public void AsyncProModeConnectAndLoadData()
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                AEC.Tools.LogHelper.WriteInfo("MController.AsyncProModeConnectAndLoadData()...");
                this.rcClientEngine.localClient.ProModeConnectAndLoadData();
            }
            else
            {
                this.commSvc.AsyncProModeConnectAndLoadData();
            }
        }

        public void AsyncEasyModeConnectAndLoadData()
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                AEC.Tools.LogHelper.WriteInfo("MController.AsyncEasyModeConnectAndLoadData()...");
                rcClientEngine.localClient.EasyModeConnectAndLoadData();
            }
            else
            {
                this.commSvc.AsyncEasyModeConnectAndLoadData();
            }
        }

        public void AsyncProModeCallDeviceMode(short modeIndex)
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                this.rcClientEngine.localClient.ProModeCallDeviceMode(modeIndex);
            }
            else
            {
                this.commSvc.AsyncProModeCallDeviceMode(modeIndex);
            }
        }

        public void AsyncProModeSwitchDeviceWorkMode(SwitchWorkMode switchWorkMode)
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                //this.rcClientEngine.localClient.ProModeCallDeviceMode(deviceWorkModeIndex);
            }
            else
            {
                this.commSvc.AsyncProModeSwitchDeviceWorkMode(switchWorkMode);
            }
        }

        public void AsyncEasyModeCallDeviceMode(short modeIndex)
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                rcClientEngine.localClient.EasyModeCallDeviceMode(modeIndex);
            }
            else
            {
                this.commSvc.AsyncEasyModeCallDeviceMode(modeIndex);
            }
        }

        public void UpdateToBeUploadedData(EffectorModelBase effectorModel)
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                this.rcClientEngine.localClient.UpdateToBeUploadedData(effectorModel);
            }
            else
            {
                this.commSvc.UpdateToBeUploadedData(effectorModel);
            }
        }

        public void AsyncProModeUploadData2Device()
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                rcClientEngine.localClient.ProModeProModeUploadData();
            }
            else
            {
                this.commSvc.AsyncProModeUploadData2Device();
            }
        }

        public void AsyncEasyModeUploadMultiPresetData(List<MDeviceMode> deviceModeList)
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                rcClientEngine.localClient.EasyModeUploadMultiPresetData(deviceModeList);
            }
            else
            {
                this.commSvc.AsyncEasyModeUploadMultiModeData(deviceModeList);
            }
        }

        public void ForcedDisconnect()
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                rcClientEngine.localClient.ForcedDisconnect();
            }
            else
            {
                this.commSvc.ForcedDisconnect();
            }
        }

        public void ReadSystemSatatus()
        {
            if (rcClientEngine.localRCModeIsOn)
            {
                rcClientEngine.localClient.ReadSystemSatatus();
            }
            else
            {
                this.commSvc.ReadSystemSatatus();
            }
        }

        //public void SeralizeCurrEffectorModelToFile(string path)
        //{
        //    if(this.currEffectorFamily == MEffectorFamily.X5)
        //    {
        //        X5DataStorage.SaveToFile(this.x5EffectorModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
        //    }
        //    else if (this.currEffectorFamily == MEffectorFamily.VKxS)
        //    {
        //        VKxSDataStorage.SaveToFile(this.vkxsEffectorModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
        //    }
        //    else if(this.currEffectorFamily == MEffectorFamily.VX)
        //    {
        //        VXDataStorage.SaveToFile(this.vxEffectorModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
        //    }
        //    else if(this.currEffectorFamily == MEffectorFamily.MX)
        //    {
        //        MXDataStorage.SaveToFile(this.mxEffectorModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
        //    }
        //}

        public bool SeralizeEffectorModelToFile(EffectorModelBase effectorModel, string path)
        {
            if (this.currEffectorFamily == MEffectorFamily.X5)
            {
                X5EffectorModel x5Model = effectorModel as X5EffectorModel;
                if(x5Model != null)
                {
                    X5DataStorage.SaveToFile(x5Model, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
                    return true;
                }
            }
            else if (this.currEffectorFamily == MEffectorFamily.VKxS)
            {
                VKxSEffectorModel vkxSModel = effectorModel as VKxSEffectorModel;
                if (vkxSModel != null)
                {
                    VKxSDataStorage.SaveToFile(vkxSModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
                    return true;
                }
            }
            else if (this.currEffectorFamily == MEffectorFamily.VKxDM)
            {
                VKxDMEffectorModel vkxsdmModel = effectorModel as VKxDMEffectorModel;
                if (vkxsdmModel != null)
                {
                    VKxDMDataStorage.SaveToFile(vkxsdmModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
                    return true;
                }
            }
            else if (this.currEffectorFamily == MEffectorFamily.KX)
            {
                KXEffectorModel kxdmModel = effectorModel as KXEffectorModel;
                if (kxdmModel != null)
                {
                    KXDataStorage.SaveToFile(kxdmModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
                    return true;
                }
            }
            else if (this.currEffectorFamily == MEffectorFamily.VKxSCM)
            {
                VKxSCMEffectorModel vkxscmModel = effectorModel as VKxSCMEffectorModel;
                if (vkxscmModel != null)
                {
                    VKxSCMDataStorage.SaveToFile(vkxscmModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
                }
            }
            else if (this.currEffectorFamily == MEffectorFamily.VKxDCM)
            {
                VKxDCMEffectorModel vkxdcmModel = effectorModel as VKxDCMEffectorModel;
                if (vkxdcmModel != null)
                {
                    VKxDCMDataStorage.SaveToFile(vkxdcmModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
                }
            }
            else if (this.currEffectorFamily == MEffectorFamily.VKxPII)
            {
                VKxPIIEffectorModel vkxpiiModel = effectorModel as VKxPIIEffectorModel;
                if (vkxpiiModel != null)
                {
                    VKxPIIDataStorage.SaveToFile(vkxpiiModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
                }
            }
            else if(this.currEffectorFamily == MEffectorFamily.VKxPIIDCM)
            {
                VKxPIIDCMEffectorModel vkxpiidcmModel = effectorModel as VKxPIIDCMEffectorModel;
                if(vkxpiidcmModel != null)
                {
                    VKxPIIDCMDataStorage.SaveToFile(vkxpiidcmModel, this.mEffectorModel.currModelSeralizeType, path, this.isSuperUserMode);
                }
            }

            return false;
        }

        public string RmoteControlMessage
        {
            get
            {
                if (this.rcClientEngine.localRCModeIsOn)
                {
                    if (this.rcClientEngine.localClient.rcConnected)
                    {
                        return string.Format("| 远程控制  已经连接到 ID:{0}, IP:{1} |", rcClientEngine.localClient.targetIDCode, rcClientEngine.localClient.targetIP);
                    }
                    else
                    {
                        return "| 远程控制  未连接远程主机 |";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private void OnDataLoadedCompletedHandler()
        {
            if (this.currEffectorFamily == MEffectorFamily.MX && this.mxEffectorModel != null)
            {
                this.mxEffectorModel.ResetMicModuleNoiseGate();
            }
        }

        public static Core.DSP.DSPHelper.sampleRate GetEffectorFamilySampleRate(MEffectorFamily effectorFamily_)
        {
            switch(effectorFamily_)
            {
                case MEffectorFamily.VX:
                    return Core.DSP.DSPHelper.sampleRate.sr96K;

                //case MEffectorFamily.X5:
                //case MEffectorFamily.VKxS:
                //case MEffectorFamily.VKxDM:
                //case MEffectorFamily.KX:
                //case MEffectorFamily.VKxPIIDCM:
                //case MEffectorFamily.MX:
                //    return Core.DSP.DSPHelper.sampleRate.sr48K;

                default:
                    return Core.DSP.DSPHelper.sampleRate.sr48K;
            }
        }

        public static Core.DSP.DSPHelper.EQMode GetEffectorFamilyEQMode(MEffectorFamily effectorFamily_)
        {
            switch (effectorFamily_)
            {
                //case MEffectorFamily.X5:
                //case MEffectorFamily.VKxS:
                //case MEffectorFamily.VKxDM:
                //case MEffectorFamily.KX:
                //case MEffectorFamily.VKxPIIDCM:
                //    return Core.DSP.DSPHelper.EQMode.Professional;

                case MEffectorFamily.VX:
                case MEffectorFamily.MX:
                    return Core.DSP.DSPHelper.EQMode.Classical;

                default:
                    return Core.DSP.DSPHelper.EQMode.Professional;
            }
        }
    }
}
