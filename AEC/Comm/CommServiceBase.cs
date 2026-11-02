using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Common;
using AEC.Comm.CommDevices;
using AEC.Misc;
using AEC.Tools;

namespace AEC.Comm
{
    public enum Retransmission
    {
        None = 0,
        One = 1,
        Two = 2,
    }    

    public abstract class CommServiceBase
    {
        public const int MCommWaitTimeUnit = 2048;
        public const int ConnectDeviceWaitTime = MCommWaitTimeUnit * 1;
        public const int LoadNonSysteModuleWaitTime = MCommWaitTimeUnit * 1;
        public const int UploadNonSysteModuleWaitTime = MCommWaitTimeUnit * 3;
        public const int LoadSystemModuleDataWaitTime = MCommWaitTimeUnit * 1;
        public const int LoadDeviceSystemDataWaitTime = MCommWaitTimeUnit * 1;
        public const int WriteSystemModuleDataWaitTime = MCommWaitTimeUnit * 1;
        public const int CallDevcieModeWaitTime = MCommWaitTimeUnit * 2;
        public const int LoadDeviceActivationCodeWaitTime = MCommWaitTimeUnit * 1;
        public const int MaxWaitTime = MCommWaitTimeUnit * 3;

        private const short CmdSendingInterval = 28;

        private static Config config = Config.GetInstance();

        protected CommDevice commDevice;
        private CommState _commState = CommState.Disconnected;  //通讯状态
        private System.Timers.Timer heartbeatTimer = null;
        private DateTime _lastCommTimestamp = DateTime.Now;
        private readonly CmdQueue cmdQueue = null;

        private readonly CommKeyPair keyPair = new CommKeyPair();

        private DeviceModel _currModel = DeviceModel.M_Audio;

        private readonly CommProtocolDetector commProtocolDetector = new CommProtocolDetector(CommProtocolBase.CheckCommCmdType, CommProtocolBase.CheckReadResponseIsCompleted, CommProtocolBase.CheckWriteResponseIsOk);
        private readonly List<byte> _recvBuffer = new List<byte>();

        private System.ComponentModel.BackgroundWorker bgwComm;     //用于联机,加载，上传，调用模式数据等通讯过程中
        private System.ComponentModel.BackgroundWorker bgwSendCmd;  //用于联机成功后发送命令

        public readonly CommProtocolBase commProtocol = new CommProtocolBase();
        public readonly EventBus commEvent;

        private ManualResetEvent commIdleEvent = new ManualResetEvent(true);

        protected static Localization localization = Config.GetInstance().localization;

        private EventWaitHandle[] events = new EventWaitHandle[]
        {
            new AutoResetEvent(false),
            new AutoResetEvent(false),
        };

        private EventWaitHandle stopEvent
        {
            get
            {
                return events[0];
            }
        }

        private EventWaitHandle sendCmdEvent
        {
            get
            {
                return events[1];
            }
        }

        public bool isConnected
        {
            get
            {
                return this._commState == CommState.Connected;
            }
        }

        public CommState commState
        {
            get
            {
                return this._commState;
            }
            private set
            {
                if(this._commState != value)
                {
                    _commState = value;
                    this.commEvent.NotifyObeserver(new CommStateChangedArgs(_commState));
                }
            }
        }

        public List<byte> recvBuffer
        {
            get
            {
                lock(this._recvBuffer)
                {
                    return new List<byte>(this._recvBuffer);
                }
            }
        }

        public byte[] decryptedReceivedData
        {
            get
            {
                byte[] data = null;

                lock(this._recvBuffer)
                {
                    data = this._recvBuffer.ToArray();
                }

                System.Diagnostics.Debug.Assert(data != null);
                
                CommProtocolBase.DecryptData(data, keyPair);

                return data;
            }
        }

        private Retransmission currentRetransmission
        {
            get
            {
                return Int2Retransmission(config.commRetries);
            }
        }

        private const string offLine = "Offline";

        public string connectionInfo
        {
            get
            {
                if(this.isConnected)
                {
                    if(this.commDevice != null)
                    {

                        return this.commDevice.connectionInfo;
                    }
                }

                return offLine;
            }
        }
        
        public CommServiceBase(EventBus commEvent_)
        {
            //this.commProtocol = commProtocol_;
            this.commEvent = commEvent_;

            this.bgwComm = new System.ComponentModel.BackgroundWorker();
            this.bgwComm.DoWork += bgwComm_DoWork;

            this.bgwSendCmd = new System.ComponentModel.BackgroundWorker();
            this.bgwSendCmd.DoWork += bgwSendCmd_DoWork;

            this.cmdQueue = new CmdQueue(sendCmdEvent);

            this.heartbeatTimer = new System.Timers.Timer(CommProtocolBase.HeartbeatTimerInterval);
            this.heartbeatTimer.Elapsed += heartbeatTimer_Tick;

            this.commEvent.RegisterHandler(CommEventHandler);
        }

        private void ClearKey()
        {
            this.keyPair.Update(0);
        }

        private static short GenRandKey()
        {
            Random rd = new Random();
            short r = (short)rd.Next(1, 0xffff);
            byte low = CommProtocolBase.GetLowByte(r);
            byte high = CommProtocolBase.GetHighByte(r);
            if (low == high)
            {
                return 0x077f;
            }
            return r;
        }

        //专业模式异步联机并加载全部数据
        public void AsyncProModeConnectAndLoadData()
        {
            AsyncComm(new CommArge(CommProcess.ProModeConnectLoadData));
        }

        //专业模式异步上传数据到设备
        public void AsyncProModeUploadData2Device()
        {
            AsyncComm(new CommArge(CommProcess.ProModeUploadData));
        }

        //专业模式异步调用设备模式
        public void AsyncProModeCallDeviceMode(short modeIndex)
        {
            AsyncComm(new CommArge(CommProcess.ProModeCallDeviceMode, modeIndex));
        }

        //专业模式异步调用切换设备模式
        public void AsyncProModeSwitchDeviceWorkMode(SwitchWorkMode switchWorkMod)
        {
            AsyncComm(new CommArge(CommProcess.ProModeSwitchDeviceWorkMode, switchWorkMod));
        }

        //易调模式异步联机并加载系统数据
        public void AsyncEasyModeConnectAndLoadData()
        {
            AsyncComm(new CommArge(CommProcess.EasyModeConnectLoadData));
        }

        //易调模式异步上传多个数据数据
        public void AsyncEasyModeUploadMultiModeData(object state)
        {
            AsyncComm(new CommArge(CommProcess.EasyModeUploadMultiModeData, state));
        }

        //易调模式异步调用设备模式
        public void AsyncEasyModeCallDeviceMode(short modeIndex)
        {
            AsyncComm(new CommArge(CommProcess.EasyModeCallDeviceMode, modeIndex));
        }

        private void CommEventHandler(object sender, EventBusArgs e)
        {
            switch(e.type)
            {
                case EventType.DataParamChanged:
                    this.UpdateParam((e as DataParamChangedArgs).param);
                    break;

                case EventType.EQChanged:
                    {
                        EQChangedArgs args = e as EQChangedArgs;
                        this.UpdateEQ(args.moduleIndex, GetModuleEQAddr(args.moduleIndex), args.eq);
                    }
                    break;

                case EventType.DEQParamChanged:
                    {
                        DEQParamChangedArgs args = e as DEQParamChangedArgs;
                        this.UpdateDEQ(args.moduleIndex, args.deq, args.paramType);
                    }
                    break;

                case EventType.PasswordChanged:
                    {
                        PasswordChangedArgs args = e as PasswordChangedArgs;
                        this.UpdatePassowrd(args.addr, args.passowrd);
                    }
                    break;

                case EventType.SaveDeviceMode:
                    {
                        SaveDeviceModeArgs args = e as SaveDeviceModeArgs;
                        this.SaveDeviceMode(args.modeIndex, args.modeName);
                    }
                    break;

                case EventType.UpdateModuleAllData:
                    {
                        UpdateModuleAllDataArgs args = e as UpdateModuleAllDataArgs;
                        this.UpdateNonSysteModuleData(args.moduleIndex, args.dataList);
                    }
                    break;

                case EventType.UpdateCustomDeviceInfoEnable:
                    {
                        UpdateCustomDeviceInfoEnableArgs args = e as UpdateCustomDeviceInfoEnableArgs;
                        this.UpdateCustomDeviceInfoEnable(args.enable);
                    }
                    break;

                case EventType.UpdateCustomDeviceInfoData:
                    {
                        UpdateCustomDeviceInfoDataArgs args = e as UpdateCustomDeviceInfoDataArgs;
                        this.UpdateCustomDeviceInfoData(args.efd, args.data);
                    }
                    break;

                case EventType.DeviceNameChanged:
                    {
                        DeviceNameChangedArgs args = e as DeviceNameChangedArgs;
                        this.UpdateDeviceName(args.deviceName);
                    }
                    break;

                case EventType.UsageLimitLockChanged:
                    {
                        UsageLimitLockUpdatedArgs args = e as UsageLimitLockUpdatedArgs;
                        this.UpdateUsageLimitLock(args.usageLimitLock);
                    }
                    break;

                default:
                    break;
            }
        }

        //更新参数
        private void UpdateParam(ShortParam param)
        {
            //Log.OutputLine("更新模块 {0} 参数地址:{1}({2}) 值:{3}", param.moduleIndex, param.addr, Core.Utils.Uint32ToShortHexStr(param.addr), param.v);

            if(isConnected)
            {
                CommCmd commCmd = this.commProtocol.GetWriteModuleParamCmd(param);
                //Log.OutputLine("更新模块参数:{0}", Utils.ByteToHexStr(commCmd.cmd));
                this.cmdQueue.Push(commCmd);
            }
        }

        //更新EQ
        private void UpdateEQ(byte moduleIndex, byte addr, EQ eq)
        {
            //Log.OutputLine("更新模块 {0} EQ 参数地址:{1}({2}) 值:{3}", moduleIndex, addr, Core.Utils.Uint32ToShortHexStr(addr), eq.ToString());

            if (isConnected)
            {
                CommCmd commCmd = this.commProtocol.GetWriteEQParamCmd(moduleIndex, addr, eq);
                //Log.OutputLine("更新模块EQ命令id:{0}", commCmd.identity);
                this.cmdQueue.Push(commCmd);
                //Log.OutputLine("cmd queue 长度:{0}", this.cmdQueue.count);
            }
        }

        private void UpdateDEQ(byte moduleIndex, DEQ deq, DeqParamType type)
        {
            if (isConnected)
            {
                byte addr = GetModuleDEQParamAddr(type);
                ShortParam param = deq.GetParam(type);
                CommCmd commCmd = this.commProtocol.GetWriteDEQParamCmd(moduleIndex, addr, deq.index, param);
                this.cmdQueue.Push(commCmd);
            }
        }

        private void UpdatePassowrd(byte addr, string passowrd)
        {
            //AEC.Log.OutputLine("更新密码:{0}", passowrd);
            if (isConnected)
            {
                CommCmd commCmd = this.commProtocol.GetWritePasswordCmd(addr, passowrd);
                this.cmdQueue.Push(commCmd);
            }
        }

        private void SaveDeviceMode(short modeIndex, string modeName)
        {
            //AEC.Log.OutputLine("更新设备模式{0}:{1}", moduleIndex, modeName);
            if (isConnected)
            {
                CommCmd commCmd = this.commProtocol.GetSaveDeviceModeCmd(modeIndex, modeName, StartSaveDeviceMode, SaveDeviceModeComplete);
                this.cmdQueue.Push(commCmd);
            }
        }

        private void StartSaveDeviceMode(object obj)
        {
            //AEC.Log.OutputLine("开始保存设备模式");
            this.commEvent.NotifyObeserver(new StartSaveModeArgs());
        }

        private void SaveDeviceModeComplete(object obj)
        {
            //AEC.Log.OutputLine("保存设备模式完成");
            this.commEvent.NotifyObeserver(new SaveModeCompletedArgs());
        }

        private void UpdateNonSysteModuleData(byte moduleIndex, List<short> dataList)
        {
            //AEC.Log.OutputLine("UpdateModuleAllData");
            if (isConnected)
            {
                CommCmd commCmd = this.commProtocol.UpdateNonSysteModuleDataCmd(moduleIndex, dataList);
                this.cmdQueue.Push(commCmd);
            }
        }

        private void UpdateCustomDeviceInfoEnable(bool enable)
        {
            //AEC.Log.OutputLine("UpdateExtendedFunctionState");
            if (isConnected)
            {
                CommCmd commCmd = this.commProtocol.UpdateCustomDeviceInfoEnableCmd(enable);
                this.cmdQueue.Push(commCmd);
            }
        }

        private void UpdateCustomDeviceInfoData(SystemCommProtocol.SystemCustomDeviceInfoData efd, Object data)
        {
            //AEC.Log.OutputLine("UpdateExtendedFunctionData");
            if (isConnected)
            {
                CommCmd commCmd = this.commProtocol.UpdateCustomDeviceInfoData(efd, data);
                this.cmdQueue.Push(commCmd);
            }
        }

        private void UpdateDeviceName(string name)
        {
            if (isConnected)
            {
                CommCmd commCmd = this.commProtocol.UpdateDeviceName(name);
                this.cmdQueue.Push(commCmd);
            }
        }

        private void UpdateUsageLimitLock(UsageLimitLock usageLimitLock)
        {
            if (isConnected)
            {
                CommCmd commCmd = this.commProtocol.UpdateUsageLimitLockCmd(usageLimitLock, UpdateUsageLimitLockCompleted);
                this.cmdQueue.Push(commCmd);
            }
        }

        private void UpdateUsageLimitLockCompleted(object obj)
        {
            //AEC.Log.OutputLine("开始保存设备模式");
            UsageLimitLock usageLimitLock = obj as UsageLimitLock;
            Debug.Assert(usageLimitLock != null);
            this.commEvent.NotifyObeserver(new UpdateUsageLimitLockCompletedArgs(usageLimitLock));
        }

        //发送心跳包
        private void SendHeartbeatMsg()
        {
            //Log.OutputLine("SendHeartbeatMsg()1");
            if(isConnected)
            {
                if(!this.commIdleEvent.WaitOne(0))
                {
                    return;
                }

                if(this.cmdQueue.count > 0)
                {
                    return;
                }

                //Log.OutputLine("SendHeartbeatMsg()2...{0}ms, {1}, {2}", (DateTime.Now - _lastCommTimestamp).Milliseconds, _lastCommTimestamp, DateTime.Now);
                TimeSpan sp = DateTime.Now - _lastCommTimestamp;
                //Log.OutputLine("SendHeartbeatMsg() {0}", sp.TotalMilliseconds);
                if ((DateTime.Now - _lastCommTimestamp).TotalMilliseconds > CommProtocolBase.HeartbeatInterval)
                {
#if UNENCRYPTED_COMM
                    CommCmd commCmd = this.commProtocol.GetHeartbeatCmd(keyPair);
#else
                    //CommCmd commCmd = ((_heartbeatCount++ % 2) != 0) ?
                    //    this.commProtocol.GetHeartbeatCmd(keyPair) :
                    //    this.commProtocol.GetUpdateRandomKeyCmd(GenRandKey(), keyPair);
                    CommCmd commCmd = this.commProtocol.GetHeartbeatCmd();
#endif
                    this.cmdQueue.Push(commCmd);
                    //Log.OutputLine("发送心跳包到消息队列");
                }
            }
        }

        public void SyncSendCmd(byte[] cmd, int timeout, bool encrypt = true)
        {
            if(this.commDevice != null)
            {
                byte[] cmdCopy = CreateByteArrayCopy(cmd);

                if (encrypt)
                {
                    CommProtocolBase.EncryptCmd(cmdCopy, keyPair);
                }

                this.commDevice.SyncSend(cmdCopy, timeout);
            }
        }

        public void SyncSendCmdAndRetransmission(byte[] cmd, int timeout, bool encrypt = true)
        {
            int retransmissionTimes = (int)currentRetransmission;
            int times = 0;

            do
            {
                if (times > 0)
                {
                    AEC.Tools.LogHelper.WriteDebug("CommServiceBase.SyncSendCmdAndRetransmission()...Retransmission: {0}", times);
                }

                this.SyncSendCmd(cmd, timeout, encrypt);

                CommCmdType commCmdType = CommProtocolBase.CheckCommCmdType(cmd);

                if(commCmdType == CommCmdType.Read)
                {
                    if (CommProtocolBase.CheckReadResponseIsCompleted(this.recvBuffer))
                    {
                        return;
                    }
                }
                else if(commCmdType == CommCmdType.Write)
                {
                    if (CommProtocolBase.CheckWriteResponseIsOk(this.recvBuffer))
                    {
                        return;
                    }
                }
                else
                {
                    AEC.Tools.LogHelper.WriteDebug("CommServiceBase.SyncSendCmdAndRetransmission()...CommCmdType Error.");
                }
            }
            while (++times <= retransmissionTimes);
        }

        public void ReadSystemSatatus()
        {
            if (isConnected)
            {
                if (this.cmdQueue.isEmpty)
                {
                    this.cmdQueue.Push(this.commProtocol.GetReadSystemStatusCmd());    //读取输出压限状态
                }
            }
        }

        public void StopService()
        {
            this.heartbeatTimer.Stop();
            //Log.OutputLine("StopService()...1");
            this.stopEvent.Set();
            //Log.OutputLine("StopService()...2");
            this.Disconnect();
            //Log.OutputLine("StopService()...3");
            this.commIdleEvent.WaitOne(MaxWaitTime);
            //Log.OutputLine("StopService()...4");
            //Log.OutputLine("StopService()");
        }

        //检测连接是否能被断开
        private bool CheckCanBeDisconnected()
        {
            if(this.isConnected)
            {
                if(this.cmdQueue.IsContainNormalCmd())
                {
                    return false;
                }
                else
                {
                    if(!commIdleEvent.WaitOne(512))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void ForcedDisconnect()
        {
            this.CheckCanBeDisconnected();
            this.Disconnect();
        }

        //断开连接
        public void Disconnect()
        {
            this.commConsecutiveFailedCount = 0;
            this.heartbeatTimer.Stop();
            this.DisconnectAndCloseDevice();     //断开连接并且关闭设备
            this.ClearRecvBuffer(); //清空缓冲区
            this.ClearCmdQueue();   //清空命令队列
            this.commState = CommState.Disconnected;
            this.commEvent.NotifyObeserver(new ConnectionIsDisconnectedArgs());
        }

        private void AsyncComm(CommArge arge)
        {
            if (this.bgwComm.IsBusy)
            {
                return;
            }

            this.bgwComm.RunWorkerAsync(arge);
        }

        //联机加载
        private void bgwComm_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //Log.OutputLine("bgwComm_DoWork, thread id = {0}", Thread.CurrentThread.ManagedThreadId);
            CommArge arge = e.Argument as CommArge;
            bool result = false;
            switch (arge.process)
            {
                case CommProcess.ProModeConnectLoadData:
                    result = ConnectAndLoadData(CommProcess.ProModeConnectLoadData);
                    break;

                case CommProcess.ProModeUploadData:
                    result = ProModeUploadAllModuleData();
                    break;

                case CommProcess.ProModeCallDeviceMode:
                    result = CallDeviceMode(CommProcess.ProModeCallDeviceMode, (short)(arge.state));
                    break;

                case CommProcess.ProModeSwitchDeviceWorkMode:
                    result = SwitchDeviceWorkMode(CommProcess.ProModeSwitchDeviceWorkMode, (SwitchWorkMode)(arge.state));
                    break;

                case CommProcess.EasyModeConnectLoadData:
                    result = ConnectAndLoadData(CommProcess.EasyModeConnectLoadData);
                    break;

                case CommProcess.EasyModeUploadMultiModeData:
                    result = EasyModeUploadMultiData(arge.state);
                    break;

                case CommProcess.EasyModeCallDeviceMode:
                    result = CallDeviceMode(CommProcess.ProModeCallDeviceMode, (short)(arge.state));
                    break;
            }

            //Log.OutputLine("加载数据完成****************");
            commEvent.NotifyObeserver(new CommCompletedArgs(new CommResult(arge.process, result)));
            //Log.OutputLine("通讯结束!");
        }


        //private int bgwSendCmd_count = 0;
        //发送数据
        private void bgwSendCmd_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool exitLoop = false;
            //Console.WriteLine("进入循环!");
            while (true)
            {
                //Console.WriteLine("wait event-0!");
                int result = WaitHandle.WaitAny(events);
                //Console.WriteLine("wait event-1!");
                switch (result)
                {
                    case 0:
                        exitLoop = true;
                        break;

                    case 1:
                        try
                        {
                            SendCmdFromQueue();
                        }
                        catch(Exception ex)
                        {
                            Tools.LogHelper.WriteDebug("SendCmdFromQueue.... Exception: ", ex);
                        }
                        break;

                    default:
                        exitLoop = true;
                        break;
                }

                if (exitLoop)
                {
                    break;
                }

                //AEC.Log.OutputLine("bgwSendCmd_DoWork 循环:{0}", ++bgwSendCmd_count);
            }
        }

        //连接设备，并加载数据
        public bool ConnectAndLoadData(CommProcess commProcess)
        {
            commState = CommState.Connecting;

            //清除密钥，此操作是必要的，不能删除
            this.ClearKey();

            //打开端口尝试连接
            //Log.OutputLine("打开端口尝试连接");
            if (!OpenDeviceAndTryConnect())
            {
                UpdateConnectDeviceFailedProgress();
                OnCommFailed();
                return false;
            }

            //加载设备型号
            if (!LoadEffectorModel(commProcess))
            {
                UpdateConnectDeviceFailedProgress();
                OnCommFailed();
                return false;
            }

            //初始化密钥
            if (!InitCommKey())
            {
                UpdateConnectDeviceFailedProgress();
                OnCommFailed();
                return false;
            }

            //加载效果器型号完成
            this.LoadEffectorModelCompleted();

            //加载所有数据
            //Log.OutputLine("加载所有数据");
            if (!LoadAllModuleData(commProcess))
            {
                UpdateConnectDeviceFailedProgress();
                OnCommFailed();
                return false;
            }

            this.ClearRecvBuffer();
            
            _lastCommTimestamp = DateTime.Now;

            this.StartHeartbeat();

            //Log.OutputLine("加载数据完成****************1");

            commState = CommState.Connected;

            //Log.OutputLine("加载数据完成****************2");

            if (!this.bgwSendCmd.IsBusy)
            {
                this.bgwSendCmd.RunWorkerAsync();
            }

            this.LoadDataCompleted(commProcess);

            return true;
        }

        //上传数据
        private bool ProModeUploadAllModuleData()
        {
            commState = CommState.Connecting;
            StopHeartbeat();
            //AEC.Log.OutputLine("StopHeartbeat()...");

            //如果通讯队列中有数据，等待队列为空
            while (cmdQueue.isNotEmpty)
            {
                Thread.Sleep(10);
            }
            //等待事件
            bool isIdle = commIdleEvent.WaitOne(MaxWaitTime);
            if(!isIdle || this._commState == CommState.Disconnected)
            {
                Tools.LogHelper.WriteInfo("Connection has been interrupted!");
                UpdateUploadDataFailedProgress();
                return false;
            }

            //AEC.Log.OutputLine("UploadAllModuleData()...");

            if (!UploadNonSystemModuleData(CommProcess.ProModeUploadData))
            {
                Tools.LogHelper.WriteInfo("UploadNonSystemModuleData()...Failed!");
                UpdateUploadDataFailedProgress();
                OnCommFailed();
                return false;
            }

            //AEC.Tools.LogHelper.WriteDebug("UploadSysteModuleData()...0");

            if (!UploadSysteModuleData(CommProcess.ProModeUploadData))
            {
                //AEC.Tools.LogHelper.WriteDebug("UploadSysteModuleData()...1");

                UpdateUploadDataFailedProgress();
                OnCommFailed();
                return false;
            }

            //AEC.Tools.LogHelper.WriteDebug("UploadSysteModuleData()...2");

            //AEC.Log.OutputLine("加载数据完成!");

            this.ClearRecvBuffer();
            
            _lastCommTimestamp = DateTime.Now;

            this.StartHeartbeat();

            commState = CommState.Connected;

            this.UploadDataCompleted();

            LogHelper.WriteInfo("CommServiceBase.ProModeUploadAllModuleData()...Completed.");

            return true;
        }

        //调用设备模式
        private bool CallDeviceMode(CommProcess commProcess, short index)
        {
            Debug.Assert(commProcess == CommProcess.ProModeCallDeviceMode || commProcess == CommProcess.EasyModeCallDeviceMode);

            //AEC.Log.OutputLine("调用设备模式:{0}", index);
            commState = CommState.Connecting;
            StopHeartbeat();

            //如果通讯队列中有数据，等待队列为空
            while (cmdQueue.isNotEmpty)
            {
                Thread.Sleep(10);
            }
            //等待事件
            bool isIdle = commIdleEvent.WaitOne(MaxWaitTime);
            if (!isIdle || this._commState == CommState.Disconnected)
            {
                Tools.LogHelper.WriteInfo("Connection has been interrupted!");
                UpdateCallDeviceModeFailedProgress();
                return false;
            }

            //AEC.Log.OutputLine("CallDeviceMode()...");

            //AEC.Tools.LogHelper.WriteDebug("CallDeviceMode()1...{0}", index);

            if (!UpdateDeviceModeCall(commProcess, index))
            {
                OnCommFailed();
                UpdateCallDeviceModeFailedProgress();
                return false;
            }

            //AEC.Tools.LogHelper.WriteDebug("CallDeviceMode()2...{0}", index);

            if (!LoadAllNonDeviceModeData(commProcess))
            {
                OnCommFailed();
                UpdateCallDeviceModeFailedProgress();
                return false;
            }

            this.ClearRecvBuffer();
            
            _lastCommTimestamp = DateTime.Now;

            this.StartHeartbeat();

            commState = CommState.Connected;

            this.CallDeviceModeCompleted(CommProcess.ProModeCallDeviceMode);

            return true;
        }

        //切换设备工作模式
        private bool SwitchDeviceWorkMode(CommProcess commProcess, SwitchWorkMode switchWorkMode)
        {
            Debug.Assert(commProcess == CommProcess.ProModeSwitchDeviceWorkMode);

            //AEC.Log.OutputLine("调用设备模式:{0}", index);
            commState = CommState.Connecting;
            StopHeartbeat();

            //如果通讯队列中有数据，等待队列为空
            while (cmdQueue.isNotEmpty)
            {
                Thread.Sleep(10);
            }
            //等待事件
            bool isIdle = commIdleEvent.WaitOne(MaxWaitTime);
            if (!isIdle || this._commState == CommState.Disconnected)
            {
                Tools.LogHelper.WriteInfo("Connection has been interrupted!");
                UpdateCallDeviceModeFailedProgress();
                return false;
            }

            AEC.Log.OutputLine("UpdateDeviceWorkMode()...");

            //AEC.Tools.LogHelper.WriteDebug("CallDeviceMode()1...{0}", index);
            if (!UpdateDeviceWorkMode(commProcess, switchWorkMode))
            {
                OnCommFailed();
                UpdateCallDeviceModeFailedProgress();
                return false;
            }

            AEC.Log.OutputLine("LoadEffectorModel()...");

            //加载设备型号
            if (!LoadEffectorModel(commProcess))
            {
                UpdateConnectDeviceFailedProgress();
                OnCommFailed();
                return false;
            }

            AEC.Log.OutputLine("LoadEffectorModelCompleted()...");

            //加载效果器型号完成
            this.LoadEffectorModelCompleted();

            ////加载所有数据
            AEC.Log.OutputLine("LoadAllModuleData()...");
            if (!LoadAllModuleData(commProcess))
            {
                UpdateConnectDeviceFailedProgress();
                OnCommFailed();
                return false;
            }

            AEC.Log.OutputLine("LoadAllModuleData()...完成");

            this.ClearRecvBuffer();

            _lastCommTimestamp = DateTime.Now;

            this.StartHeartbeat();

            commState = CommState.Connected;

            this.LoadDataCompleted(commProcess);

            //this.CallDeviceModeCompleted(CommProcess.ProModeCallDeviceMode);
            LogHelper.WriteInfo("CommServiceBase.SwitchDeviceWorkMode()...Completed.");

            return true;
        }

        private bool EasyModeUploadMultiData(object state)
        {
            commState = CommState.Connecting;
            StopHeartbeat();
            //AEC.Log.OutputLine("StopHeartbeat()...");

            //如果通讯队列中有数据，等待队列为空
            while (cmdQueue.isNotEmpty)
            {
                Thread.Sleep(10);
            }
            //等待事件
            bool isIdle = commIdleEvent.WaitOne(MaxWaitTime);
            if (!isIdle || this._commState == CommState.Disconnected)
            {
                Tools.LogHelper.WriteInfo("Connection has been interrupted!");
                UpdateUploadDataFailedProgress();
                return false;
            }

            //AEC.Log.OutputLine("UploadEasyModeMultiData()...");

            if (!UploadMultiPresetData(state))
            {
                UpdateUploadDataFailedProgress();
                OnCommFailed();
                return false;
            }

            this.ClearRecvBuffer();
            
            _lastCommTimestamp = DateTime.Now;

            this.StartHeartbeat();

            commState = CommState.Connected;

            return true;
        }

        private int commConsecutiveFailedCount = 0;
        private const int MaxCommConsecutiveFailedCount = 3;

        private void SendCmdFromQueue()
        {
            //Console.WriteLine("队列中命令数量:{0}", cmdQueue.count);
            CommCmd commCmd = cmdQueue.Pop();
            if (commCmd == null)
            {
                return;
            }

            bool isFailed = false;

            //Console.WriteLine("[{0}]发送命令:{1}, 等待:{2}ms", DateTime.Now.ToLongTimeString(), lmUtility.lmUtility.ByteToHexStr(commCmd.cmd), commCmd.commWaitTime);
            //Log.OutputLine("commIdleEvent.Reset()...");
            commIdleEvent.Reset();
            //_isWaiting = true;

            DateTime start = DateTime.Now;

            //Log.OutputLine("StartSendCmd...");
            if(commCmd.StartSendCmd != null)
            {
                commCmd.StartSendCmd(commCmd.context);
            }
            
            if (commCmd.type == CmdType.Normal)
            {
                this.SyncSendCmdAndRetransmission(commCmd.cmd, commCmd.commWaitTime, true);
            }
            else
            {
                this.SyncSendCmd(commCmd.cmd, commCmd.commWaitTime, true);
            }

            if (commCmd.SendCmdComplete != null)
            {
                commCmd.SendCmdComplete(commCmd.context);
            }
            
            if (commCmd.type == CmdType.Normal)
            {
                //Log.OutputLine("commCmd.type == CmdType.Normal || commCmd.type == CmdType.Heartbeat...");
                if (!CommProtocolBase.CheckWriteResponseIsOk(this.recvBuffer))
                {
                    isFailed = true;
                    OnConnectionInterrupted();
                    //Log.OutputLine("通讯成功");                    
                }
                else
                {
                    this._lastCommTimestamp = DateTime.Now;

                    DateTime end = _lastCommTimestamp;
                    TimeSpan timeSpan = end - start;

                    if (timeSpan.Milliseconds < CmdSendingInterval)
                    {
                        int sleep = CmdSendingInterval - timeSpan.Milliseconds;
                        System.Threading.Thread.Sleep(sleep);

                        Tools.LogHelper.WriteDebug("CommServiceBase.SendCmdFromQueue()...Sleep:{0}", sleep);
                    }
                }
            }
            else if (commCmd.type == CmdType.Heartbeat)
            {
                //Log.OutputLine("commCmd.type == CmdType.Normal || commCmd.type == CmdType.Heartbeat...");
                if (!CommProtocolBase.CheckWriteResponseIsOk(this.recvBuffer))
                {
                    if (++this.commConsecutiveFailedCount >= MaxCommConsecutiveFailedCount)
                    {
                        Tools.LogHelper.WriteDebug("CommServiceBase.SendCmdFromQueue()...Heartbeat detection failed!");
                        this.commConsecutiveFailedCount = 0;
                        isFailed = true;
                        OnConnectionInterrupted();
                    }
                    
                    //Log.OutputLine("通讯成功");                    
                }
                else
                {
                    this.commConsecutiveFailedCount = 0;
                    this._lastCommTimestamp = DateTime.Now;
                    //Log.OutputLine("通讯失败");
                }
            }
            else if (commCmd.type == CmdType.ReadSystemStatus)
            {
                if (CommProtocolBase.CheckReadResponseIsCompleted(this.recvBuffer))
                {
                    this.commConsecutiveFailedCount = 0;
                    this._lastCommTimestamp = DateTime.Now;

                    bool ret = this.ParseSystemStatusData(this.decryptedReceivedData);
                    if(!ret)
                    {
                        isFailed = true;
                        this.OnConnectionInterrupted();
                    }

                    //QuadChannelLimiterStatus qcls = this.commProtocol.ParseOutputLimiterStatusData(this.decryptedReceivedData);

                    //if (qcls != null)
                    //{
                        
                    //    this.commEvent.NotifyObeserver(new OutputLimiterStatusLoadedArgs(qcls));
                    //}
                    //else
                    //{
                    //    isFailed = true;
                    //    this.OnConnectionInterrupted();
                    //}
                }
                else
                {
                    if (++this.commConsecutiveFailedCount >= MaxCommConsecutiveFailedCount)
                    {
                        Tools.LogHelper.WriteDebug("CommServiceBase.SendCmdFromQueue()...Read system status failed!");
                        isFailed = true;
                        this.commConsecutiveFailedCount = 0;
                        this.OnConnectionInterrupted();
                    }
                }
            }

            this.ClearRecvBuffer();

            //Log.OutputLine("commIdleEvent.Set()...");
            commIdleEvent.Set();

            if (!isFailed && cmdQueue.isNotEmpty)
            {
                sendCmdEvent.Set();
            }
            //Log.OutputLine("SendCmdFromQueue()...End");
        }

        private bool LoadEffectorModel(CommProcess commProcess)
        {
            SystemModuleBase system = this.GetSystemModule();

            SystemParamComment systemParam = new SystemParamComment(SystemCommProtocol.SystemParamAddr.deviceModel, localization[Localization.comm_DeviceModel]);
            byte[] cmd = this.commProtocol.GetReadSystemDataCmd(systemParam.param);

            UpdateCommProgress(string.Format(localization[Localization.comm_LoadSystemXData], systemParam.comment));

            this.SyncSendCmdAndRetransmission(cmd, LoadSystemModuleDataWaitTime, true);

            string msg = string.Empty;

            if (!this.commProtocol.ParseEffectorModelData(this.decryptedReceivedData, ref this._currModel, ref msg))
            {
                UpdateCommFailedProgress(false);
                UpdateCommProgress(msg, isEnd:true, isFailed: true);
                return false;
            }
            else
            {
                UpdateCommProgress(msg);
                UpdateCommSuccessProgress(GetLoadSystemModuleDataProgressIncrease(commProcess));
                //Log.OutputLine("ParseSystemData()...成功!");
            }

            return true;
        }

        public bool InitRandKey()
        {
            short randKey = GenRandKey();
            byte[] cmd = this.commProtocol.GetWriteSystemDataCmd(SystemCommProtocol.SystemParamAddr.randKey, randKey);
            UpdateCommProgress(localization[Localization.comm_LoadDeviceInfo]);
            this.SyncSendCmdAndRetransmission(cmd, WriteSystemModuleDataWaitTime, false);
            //this.commDevice.SyncSend(cmd, 0);
            if (!CommProtocolBase.CheckWriteResponseIsOk(this.recvBuffer))
            {
                Tools.LogHelper.WriteInfo("Update random key failed!");
                UpdateCommProgress(localization[Localization.comm_CheckResponseFailed], true, isFailed:true);
                return false;
            }

            UpdateCommProgress(localization[Localization.comm_CheckResponseSuccessful], isEnd: true, progressIncrement: 0.00f);
            
            this.keyPair.Update(randKey);
            
            return true;
        }

        private void StartHeartbeat()
        {
            this.heartbeatTimer.Start();
            //Log.OutputLine("开启心跳包");
        }

        private void StopHeartbeat()
        {
            this.heartbeatTimer.Stop();
            //Log.OutputLine("停止心跳包");
        }

        private void ClearRecvBuffer()
        {
            lock (this._recvBuffer)
            {
                this._recvBuffer.Clear();
            }
        }

        private void ClearCmdQueue()
        {
            this.cmdQueue.Clear();
        }

        private void DisconnectAndCloseDevice()
        {
            if (this.commDevice != null)
            {
                if (this._commState != CommState.Disconnected)
                {
                    this.SyncSendCmd(this.commProtocol.OfflineCmd, 0, true);
                }
                this.commDevice.Close();
                this.commDevice = null;
            }
        }

        public void OnCommFailed()
        {
            DisconnectAndCloseDevice();
            ClearRecvBuffer();
            commState = CommState.Disconnected;
        }

        private void OnConnectionInterrupted()
        {
            //AEC.Log.OutputLine("OnConnectionInterrupted()");
            Tools.LogHelper.WriteInfo("OnConnectionInterrupted()...");
            this.commConsecutiveFailedCount = 0;
            if(isConnected)
            {
                this.StopHeartbeat();
                this.DisconnectAndCloseDevice();
                this.ClearRecvBuffer();
                this.ClearCmdQueue();
                this.commState = CommState.Disconnected;
                this.commEvent.NotifyObeserver(new CommInterruptedArgs());
            }
        }

        private bool LoadAllModuleData(CommProcess commProcess)
        {
            //Log.OutputLine("LoadAllModuleData() 1!");
            if (!LoadNonSystemModuleData(commProcess))
            {
                //Log.OutputLine("LoadAllModuleData()失败!");
                return false;
            }

            //Log.OutputLine("LoadAllModuleData() 2!");
            if (!LoadSystemModuleData(commProcess))
            {
                //Log.OutputLine("LoadAllModuleData()失败!");
                return false;
            }
            //Log.OutputLine("LoadAllModuleData()成功!");
            //return false;
            return true;
        }

        private bool LoadAllNonDeviceModeData(CommProcess commProcess)
        {
            if (!LoadNonSystemModuleData(commProcess))
            {
                return false;
            }

            if (!LoadSystemModuleData(commProcess))
            {
                return false;
            }

            return true;
        }

        private void LoadEffectorModelCompleted()
        {
            this.commEvent.NotifyObeserver(new LoadEffectorModelCompletedArgs(this._currModel));
        }

        protected abstract bool InitCommKey();
        protected abstract void LoadDataCompleted(CommProcess commProcess);

        protected abstract void CallDeviceModeCompleted(CommProcess commProcess);

        protected abstract void UploadDataCompleted();

        protected abstract SystemModuleBase GetSystemModule();
        protected abstract bool ParseSystemData(byte[] buffer, SystemModuleBase system);
        protected abstract byte GetModuleEQAddr(byte moduleIndex);
        protected abstract byte GetModuleDEQParamAddr(DeqParamType paramType);
        protected abstract bool LoadNonSystemModuleData(CommProcess commProcess);
        protected abstract bool UploadNonSystemModuleData(CommProcess commProcess, float progressRatio = 1.0f);
        protected abstract bool UploadMultiPresetData(object state);

        protected abstract bool ParseSystemStatusData(byte[] data);

        protected abstract bool LoadCustomDeviceInfo(SystemModuleBase system);

        protected abstract SystemParamComment[] GetSystemParamsForConnectAndLoad();
        protected abstract SystemParamComment[] GetSystemParamsForRecall();
        protected abstract SystemParamCommentValue[] GetSystemParamsForUpload(SystemModuleBase system);
        
        //加载系统模块数据
        private bool LoadSystemModuleData(CommProcess commProcess)
        {
            bool isLoadSystemAllData = commProcess == CommProcess.ProModeConnectLoadData ||
                                    commProcess == CommProcess.EasyModeConnectLoadData ||
                                    commProcess == CommProcess.ProModeSwitchDeviceWorkMode;

            SystemModuleBase system = this.GetSystemModule();

            SystemParamComment[] systemParams = GetSystemParamsForLoad(commProcess);

            foreach (SystemParamComment systemParam in systemParams)
            {
                byte[] cmd = this.commProtocol.GetReadSystemDataCmd(systemParam.param);

                //AEC.Log.OutputLine(string.Format("加载系统 {0} 数据 原始命令...{1}", systemParam.comment, AEC.Core.Utils.ByteArrayToHexStr(cmd)));

                UpdateCommProgress(string.Format(localization[Localization.comm_LoadSystemXData], systemParam.comment));

                this.SyncSendCmdAndRetransmission(cmd, LoadSystemModuleDataWaitTime, true);

                //AEC.Log.OutputLine(string.Format("加载系统 {0} 数据 加密命令...{1}", systemParam.comment, AEC.Core.Utils.ByteArrayToHexStr(cmd)));

                //AEC.Log.OutputLine(string.Format("加载系统 {0} 数据 原始响应...{1}", systemParam.comment, AEC.Core.Utils.ByteArrayToHexStr(this.recvBuffer.ToArray())));
                //AEC.Log.OutputLine(string.Format("加载系统 {0} 数据 解密响应...{1}", systemParam.comment, AEC.Core.Utils.ByteArrayToHexStr(this.decryptedReceivedData)));
                //111
                //if (!this.commProtocol.ParseSystemData(this.decryptedReceivedData, system))
                if (!ParseSystemData(this.decryptedReceivedData, system))
                {
                    //Log.OutputLine("ParseSystemData()...失败!");
                    UpdateCommFailedProgress();
                    return false;
                }
                else
                {
                    UpdateCommSuccessProgress(GetLoadSystemModuleDataProgressIncrease(commProcess));
                    //Log.OutputLine("ParseSystemData()...成功!");
                }
            }

            if(system.deviceHardwareState != 0)
            {
                UpdateCommProgress(string.Format("设备状态异常：{0}", Utils.Uint32ToHexString(system.deviceHardwareState)), isEnd: true, isFailed: true);
                return false;
            }

            system.ConstraintParam();

            if (!isLoadSystemAllData)
            {
                return true;
            }

            if (!LoadDeviceAllModeName(commProcess, system))
            {
                return false;
            }

            ////加载设备是激活码
            if (!LoadDeviceActivationCode(commProcess, system))
            {
                return false;
            }

            if (!LoadCustomDeviceInfo(system))
            {
                return false;
            }

            //return false;
            return true;
        }

        //加载系统模块数据
        //protected bool LoadSystemModuleAllData(CommProcess commProcess, SystemModuleBase system, bool isLoadModeData)
        //{
        //    SystemParamComment[] systemParams = GetSystemParamsForLoad(isLoadModeData: isLoadModeData);
        //    foreach (SystemParamComment systemParam in systemParams)
        //    {
        //        byte[] cmd = this.commProtocol.GetReadSystemDataCmd(systemParam.param);

        //        UpdateCommProgress(string.Format("加载系统 {0} 数据...", systemParam.comment));

        //        this.commDevice.SyncSend(cmd, LoadSystemModuleDataWaitTime);
        //        //if (!CommProtocolBase.CheckReadResponseIsOk(this.recvBuffer))
        //        //{
        //        //    return false;
        //        //}

        //        //AEC.Log.OutputLine(string.Format("加载系统 {0} 数据...", systemParam.comment));

        //        if (!this.commProtocol.ParseSystemData(this.recvBuffer.ToArray(), system, keyPair))
        //        {
        //            //Log.OutputLine("ParseSystemData()...失败!");
        //            UpdateCommFailedProgress();
        //            return false;
        //        }
        //        else
        //        {
        //            UpdateCommSuccessProgress(GetLoadSystemModuleAllDataProgressIncrease(commProcess));
        //            //Log.OutputLine("ParseSystemData()...成功!");
        //        }
        //    }

        //    if (!LoadDeviceAllModeName(commProcess, system))
        //    {
        //        return false;
        //    }

        //    //加载设备是激活码
        //    if (!LoadDeviceActivationCode(commProcess, system))
        //    {
        //        return false;
        //    }

        //    system.ConstraintParam();

        //    return true;
        //}

        //private bool LoadDeviceAllUsageLimitPassword(CommProcess commProcess, SystemModuleBase system)
        //{
        //    if (!system.systemSupportUsageLimitLock)//如果用户不支持此功能，直接发回false
        //    {
        //        return true;
        //    }

        //    for (short i = 0; i < system.usageLimitLocksCount; ++i)
        //    {
        //        byte[] cmd = this.commProtocol.GetReadUsageLimitLockDataCmd(i);

        //        UpdateCommProgress(string.Format(localization[Localization.comm_UsageLimitLockData], (i + 1)));

        //        this.SyncSendCmdAndRetransmission(cmd, LoadDeviceSystemDataWaitTime, true);

        //        if (!this.commProtocol.ParseSystemUsageLimitLockData(i, this.decryptedReceivedData, system))
        //        {
        //            Tools.LogHelper.WriteInfo("ParsSystemModeNameData()...Failed!");
        //            UpdateCommFailedProgress();
        //            return false;
        //        }
        //        else
        //        {
        //            UpdateCommSuccessProgress(GetLoadUsageLimitLockProgressIncrease(commProcess));
        //            //UpdateCommProgress(string.Format("成功"), isEnd: true, progressIncrement: 0.01f);
        //            //Log.OutputLine("ParsSystemModeNameData()...成功!");
        //        }
        //    }

        //    return true;
        //}

        //加载设备模式名称
        private bool LoadDeviceAllModeName(CommProcess commProcess, SystemModuleBase system)
        {
            for (short i = 0; i < system.deviceModeCount; ++i)
            {
                byte[] cmd = this.commProtocol.GetReadDeviceModeName(i);

                UpdateCommProgress(string.Format(localization[Localization.comm_LoadSystemModeXNameData], (i + 1)));

                this.SyncSendCmdAndRetransmission(cmd, LoadDeviceSystemDataWaitTime, true);
                //if (!CommProtocolBase.CheckReadResponseIsOk(this.recvBuffer))
                //{
                //    return false;
                //}
                if (!this.commProtocol.ParseSystemModeNameData(i, this.decryptedReceivedData, system))
                {
                    Tools.LogHelper.WriteInfo("ParsSystemModeNameData()...Failed!");
                    UpdateCommFailedProgress();
                    return false;
                }
                else
                {
                    UpdateCommSuccessProgress(GetLoadDeviceModeNameProgressIncrease(commProcess));
                    //UpdateCommProgress(string.Format("成功"), isEnd: true, progressIncrement: 0.01f);
                    //Log.OutputLine("ParsSystemModeNameData()...成功!");
                }
            }
            
            return true;
        }

        private bool LoadDeviceActivationCode(CommProcess commProcess, SystemModuleBase system)
        {
            byte[] cmd = this.commProtocol.GetReadDeviceActivationCode();
            UpdateCommProgress(localization[Localization.comm_LoadSystemData]);
            this.SyncSendCmdAndRetransmission(cmd, LoadDeviceActivationCodeWaitTime, true);
            if (!this.commProtocol.ParseDeviceActivationCodeData(this.decryptedReceivedData, system))
            {
                UpdateCommFailedProgress();
                return false;
            }
            else
            {
                UpdateCommSuccessProgress(GetLoadDeviceActivationCodeProgressIncrease(commProcess));
            }
            //return false;
            return true;
        }

        //上传系统模块非模式数据
        public bool UploadSysteModuleData(CommProcess commProcess, float progressRatio = 1.0f)
        {
            LogHelper.WriteInfo("CommServiceBase.UploadSysteModuleData()...{0}", commProcess);

            SystemModuleBase system = GetSystemModule();
            SystemParamCommentValue[] systemParams = this.GetSystemParamsForUpload(system);
            foreach (SystemParamCommentValue systemParam in systemParams)
            {
                UpdateCommProgress(string.Format(localization[Localization.comm_UploadSystemXData], systemParam.comment));

                byte[] cmd = this.commProtocol.GetWriteSystemDataCmd(systemParam.param, systemParam.value);

                this.SyncSendCmdAndRetransmission(cmd, WriteSystemModuleDataWaitTime, true);

                if (!CommProtocolBase.CheckWriteResponseIsOk(this.recvBuffer))
                {
                    UpdateCommFailedProgress();
                    return false;
                }
                else
                {
                    UpdateCommSuccessProgress(GetUploadSysteModuleDataProgressIncrease(commProcess) * progressRatio);
                }
            }

            return true;
        }

        //更新模式调用
        public bool UpdateDeviceModeCall(CommProcess commProcess, short modeIndex)
        {
            UpdateCommProgress(localization[Localization.comm_UpdateDeviceModeIndexData]);

            byte[] cmd = this.commProtocol.GetWriteSystemDataCmd(SystemCommProtocol.SystemParamAddr.deviceModeCall, modeIndex);

            //AEC.Tools.LogHelper.WriteDebug("UpdateDeviceModeCall()...{0}", AEC.Core.Utils.ByteArrayToHexStr(cmd));
            this.SyncSendCmdAndRetransmission(cmd, CallDevcieModeWaitTime, true);
            //AEC.Tools.LogHelper.WriteDebug("UpdateDeviceModeCall()...{0}", AEC.Core.Utils.ByteArrayToHexStr(this._recvBuffer.ToArray()));

            if (!CommProtocolBase.CheckWriteResponseIsOk(this.recvBuffer))
            {
                //AEC.Tools.LogHelper.WriteDebug("UpdateDeviceModeCall()...失败！");
                UpdateCommFailedProgress();
                return false;
            }

            UpdateCommSuccessProgress(GetUpdateDeviceModeCallProgressIncrease(commProcess));

            SystemModuleBase system = GetSystemModule();
            system.deviceModeCallIndex.initV = modeIndex;

            this.ClearRecvBuffer();
            //AEC.Tools.LogHelper.WriteDebug("UpdateDeviceModeCall()...成功！");

            return true;
        }

        //更新模式调用
        public bool UpdateDeviceWorkMode(CommProcess commProcess, SwitchWorkMode switchWorkMod)
        {
            UpdateCommProgress(localization[Localization.comm_UpdateDeviceModeIndexData]);

            byte[] cmd = this.commProtocol.GetWriteSystemWrorkDataCmd(SystemCommProtocol.SystemParamAddr.deviceWorkMode, switchWorkMod);

            //AEC.Tools.LogHelper.WriteDebug("UpdateDeviceModeCall()...{0}", AEC.Core.Utils.ByteArrayToHexStr(cmd));
            this.SyncSendCmdAndRetransmission(cmd, WriteSystemModuleDataWaitTime, true);
            //AEC.Tools.LogHelper.WriteDebug("UpdateDeviceModeCall()...{0}", AEC.Core.Utils.ByteArrayToHexStr(this._recvBuffer.ToArray()));

            if (!CommProtocolBase.CheckWriteResponseIsOk(this.recvBuffer))
            {
                //AEC.Tools.LogHelper.WriteDebug("UpdateDeviceModeCall()...失败！");
                UpdateCommFailedProgress();
                return false;
            }

            UpdateCommSuccessProgress(GetUpdateDeviceModeCallProgressIncrease(commProcess));

            SystemModuleBase system = GetSystemModule();
            system.deviceWorkMode.initV = switchWorkMod.workModeIndex;

            this.ClearRecvBuffer();
            //AEC.Tools.LogHelper.WriteDebug("UpdateDeviceModeCall()...成功！");

            return true;
        }

        protected float GetLoadSystemModuleDataProgressIncrease(CommProcess commProcess)
        {
            switch (commProcess)
            {
                case CommProcess.ProModeConnectLoadData:
                case CommProcess.ProModeSwitchDeviceWorkMode:
                    return 0.01f;

                case CommProcess.ProModeCallDeviceMode:
                    return 0.02f;

                case CommProcess.EasyModeConnectLoadData:
                    return 0.033f;

                case CommProcess.EasyModeCallDeviceMode:
                    return 0.04f;
            }

            return 0.0f;
        }

        protected float GetUpdateDeviceModeCallProgressIncrease(CommProcess commProcess)
        {
            switch(commProcess)
            {
                case CommProcess.ProModeCallDeviceMode:
                    return 0.19f;

                case CommProcess.EasyModeCallDeviceMode:
                    return 0.47f;


                case CommProcess.EasyModeUploadMultiModeData:
                    return 0.025f;
            }

            return 0.0f;
        }

        protected float GetUploadSysteModuleDataProgressIncrease(CommProcess commProcess)
        {
            return 0.01f;
        }

        protected float GetLoadDeviceActivationCodeProgressIncrease(CommProcess commProcess)
        {
            switch (commProcess)
            {
                case CommProcess.ProModeConnectLoadData:
                case CommProcess.ProModeSwitchDeviceWorkMode:
                    return 0.04f;

                case CommProcess.EasyModeConnectLoadData:
                    return 0.033f;
            }

            return 0.0f;
        }

        protected float GetLoadUsageLimitLockProgressIncrease(CommProcess commProcess)
        {
            switch (commProcess)
            {
                case CommProcess.ProModeConnectLoadData:
                    return 0.01f;

                case CommProcess.EasyModeConnectLoadData:
                    return 0.033f;
            }

            return 0.0f;
        }

        protected float GetLoadDeviceModeNameProgressIncrease(CommProcess commProcess)
        {
            switch (commProcess)
            {
                case CommProcess.ProModeConnectLoadData:
                    return 0.02f;

                case CommProcess.EasyModeConnectLoadData:
                    return 0.033f;
            }

            return 0.0f;
        }

        private String lastValidPort = String.Empty;
        private bool OpenDeviceAndTryConnect()
        {
            if (config.connectionMethod == ConnectionMethod.None)
            {
                config.ResetConnectionMethod();
            }

            if(config.connectionViaUSB)
            {
                if(TryConnectByUSB_HID())
                {
                    return true;
                }
            }

            if(config.connectionViaRS232)
            {
                if(TryConnectBySerialPort())
                {
                    return true;
                }
            }

            if(config.connectionViaUDP)
            {
                if(TryConnectByUdp())
                {
                    return true;
                }
            }

            if(config.connectionViaTCP)
            {
                if(TryConnectViaTcp())
                {
                    return true;
                }
            }

            return false;
        }

        private bool TryConnectBySerialPort()
        {
            String[] ports = SerialPort.GetValidPorts();
            if (ports.Length == 0)
            {
                UpdateCommProgress(string.Format(localization[Localization.comm_NoPortAvailable]), isEnd: true);
                return false;
            }

            List<string> portList = new List<string>(ports);
            //将默认端口移动到第一个
            if(portList.Contains(config.default_SerialPort))
            {
                if(config.isSpManualMode)
                {
                    portList.Clear();
                    portList.Add(config.default_SerialPort);
                }
                else
                {
                    if (portList[0] != config.default_SerialPort)
                    {
                        portList.Remove(config.default_SerialPort);
                        portList.Insert(0, config.default_SerialPort);
                    }
                }
            }

            //将上次成功通讯成功端口移动到第一个
            if (string.IsNullOrEmpty(lastValidPort) && portList.Contains(lastValidPort))
            {
                if (portList[0] != lastValidPort)
                {
                    portList.Remove(lastValidPort);
                    portList.Insert(0, lastValidPort);
                }
            }

            if (this.commDevice != null)
            {
                this.commDevice.Close();
                this.commDevice = null;
            }

            SerialPort sp = new SerialPort(commProtocolDetector, this._recvBuffer);
            this.commDevice = sp;

            foreach (String port in portList)
            {
                sp.portName = port;

                UpdateCommProgress(string.Format(localization[Localization.comm_OpenPort], port));

                if (sp.Open())
                {
                    UpdateCommSuccessProgress(0);

                    if (ConnectDevice())
                    {
                        lastValidPort = port;

                        return true;
                    }

                    sp.Close();
                }
                else
                {
                    UpdateCommFailedProgress();
                }
            }

            this.commDevice = null;

            return false;
        }

        private bool TryConnectByUdp()
        {
            this.CloseCommDevice();
            
            UDPDevice udpDevice = new UDPDevice(commProtocolDetector, this._recvBuffer);  
            udpDevice.UpdateRemoteEP(config.udp_ip, config.udp_port);

            this.commDevice = udpDevice;

            //UpdateCommProgress(string.Format("Connect to TCPServer（IP:{0}, Port:{1}）...", config.tcp_ip, config.tcp_port));
            UpdateCommProgress(string.Format(string.Format(localization[Localization.comm_InitUdpClient], config.udp_ip, config.udp_port)));
            
            if (udpDevice.Open())
            {
                UpdateCommSuccessProgress(0);

                if (ConnectDevice())
                {
                    return true;
                }

                udpDevice.Close();
            }
            else
            {
                UpdateCommFailedProgress();
            }

            this.commDevice = null;

            return false;
        }

        private bool TryConnectViaTcp()
        {
            this.CloseCommDevice();

            TCPDevice tcpDevice = new TCPDevice(commProtocolDetector, this._recvBuffer);
            tcpDevice.UpdateRemoteEP(config.tcp_ip, config.tcp_port);

            this.commDevice = tcpDevice;

            //UpdateCommProgress(string.Format("Connect to TCPServer（IP:{0}, Port:{1}）...", config.tcp_ip, config.tcp_port));
            UpdateCommProgress(string.Format(string.Format(localization[Localization.comm_InitTcpClient], config.tcp_ip, config.tcp_port)));

            if (tcpDevice.Open())
            {
                UpdateCommSuccessProgress(0);

                if (ConnectDevice())
                {
                    return true;
                }

                tcpDevice.Close();
            }
            else
            {
                UpdateCommFailedProgress();
            }

            this.commDevice = null;

            return false;
        }

        private bool TryConnectByUSB_HID()
        {
            AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] hidInterfaces = AEC.Comm.CommDevices.HIDDevice.GetValidDevices();

            if (hidInterfaces.Length == 0)
            {
                UpdateCommProgress(string.Format(localization[Localization.comm_NoUSBHIDAvailable]), isEnd: true);
                return false;
            }

            if (this.commDevice != null)
            {
                this.commDevice.Close();
                this.commDevice = null;
            }

            HIDDevice hid = new HIDDevice(commProtocolDetector, this._recvBuffer);
            this.commDevice = hid;

            foreach (var hidDevice in hidInterfaces)
            {
                hid.devicePath = hidDevice.devicePath;
                hid.serialNumber = hidDevice.serialNumber;

                UpdateCommProgress(string.Format(localization[Localization.comm_OpenPort], hid.connectionInfo));

                if (hid.Open())
                {
                    UpdateCommSuccessProgress(0);

                    if (ConnectDevice())
                    {
                        return true;
                    }

                    hid.Close();
                }
                else
                {
                    UpdateCommFailedProgress();
                }
            }

            this.commDevice = null;

            return false;
        }

        private void CloseCommDevice()
        {
            if (this.commDevice != null)
            {
                this.commDevice.Close();
                this.commDevice = null;
            }
        }

        private bool ConnectDevice()
        {
            Debug.Assert(this.commDevice != null);
            UpdateCommProgress(localization[Localization.comm_SendConnectionRequest]);

            this.SyncSendCmdAndRetransmission(this.commProtocol.handshakeCmd, ConnectDeviceWaitTime, false);            
            
            if (CommProtocolBase.CheckWriteResponseIsOk(this.recvBuffer))
            {
                UpdateCommProgress(localization[Localization.comm_CheckResponseSuccessful], isEnd: true, progressIncrement: 0.01f);
                return true;
            }

            UpdateCommProgress(localization[Localization.comm_CheckResponseFailed], true);
            return false;
        }

        private void heartbeatTimer_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("发送心跳消息！");
            SendHeartbeatMsg();
        }

        public void UpdateCommProgress(string msg, bool isEnd = false, float progressIncrement = 0.0f, bool isFailed = false)
        {
            this.commEvent.NotifyObeserver(new CommProgressUpdatedArgs(msg, isEnd, progressIncrement, isFailed));
        }

        public void UpdateCommFailedProgress(bool isEnd = true)
        {
            this.commEvent.NotifyObeserver(new CommProgressUpdatedArgs(localization[Localization.comm_Failed], isEnd_: isEnd, isFailed_: true));
        }

        public void UpdateCommSuccessProgress(float progressIncrement)
        {
            this.commEvent.NotifyObeserver(new CommProgressUpdatedArgs(localization[Localization.comm_Successful], isEnd_: true, progressIncrement_: progressIncrement, isFailed_: false));
        }

        protected void UpdateConnectDeviceFailedProgress()
        {
            this.commEvent.NotifyObeserver(new CommProgressUpdatedArgs(localization[Localization.comm_FailedToLoadData], isEnd_: true, isFailed_: true));
        }

        public void UpdateUploadDataFailedProgress()
        {
            this.commEvent.NotifyObeserver(new CommProgressUpdatedArgs(localization[Localization.comm_FailedToUploadData], isEnd_: true, isFailed_: true));
        }

        public void UpdateCallDeviceModeFailedProgress()
        {
            this.commEvent.NotifyObeserver(new CommProgressUpdatedArgs(localization[Localization.comm_FailedToCallModeData], isEnd_: true, isFailed_: true));
        }

        private SystemParamComment[] GetSystemParamsForLoad(CommProcess commProcess)
        {
            if (commProcess == CommProcess.ProModeConnectLoadData || commProcess == CommProcess.ProModeSwitchDeviceWorkMode)
            {
                return GetSystemParamsForConnectAndLoad();                
            }
            else if (commProcess == CommProcess.ProModeCallDeviceMode )
            {
                return GetSystemParamsForRecall();
            }
            else if (commProcess == CommProcess.EasyModeConnectLoadData)
            {
                return GetSystemParamsForConnectAndLoad();  
            }
            else
            {
                throw (new Exception("GetSystemParamsForLoad()...数据错误!"));
            }
        }

        private static Retransmission Int2Retransmission(int value)
        {
            switch (value)
            {
                case 0:
                    return Retransmission.None;
                case 1:
                    return Retransmission.One;
                case 2:
                    return Retransmission.Two;
            }

            return Retransmission.One;
        }

        public static byte[] CreateByteArrayCopy(byte[] data)
        {
            Debug.Assert(data != null && data.Length > 0);

            byte[] copy = new byte[data.Length];

            Array.Copy(data, copy, data.Length);

            return copy;
        }
    }
}
