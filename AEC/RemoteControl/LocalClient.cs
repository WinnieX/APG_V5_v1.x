using System;
using System.Collections.Generic;
using System.Text;
using AEC.RemoteControl.Message;
using AEC.RemoteControl.Utility;
using AEC.RemoteControl.Protocol;
using AEC.Core;
using AEC.Common;
using AEC.Effectors;
using AEC.Tools;
using AEC.Misc;

namespace AEC.RemoteControl
{
    public class LocalClient : RCClient
    {
        private static readonly Localization localization = Config.GetInstance().localization;

        private readonly Dictionary<string, DeserializeToEventBusArgsCallback> deserializeToEventBusArgsCallbackMap;

        private readonly Dictionary<string, Action<byte[]>> MessageHandlerMap;
        private readonly EventBus localEventBus;
        
        public uint targetID
        {
            get;
            private set;
        }

        public string targetIDCode
        {
            get
            {
                return ClientIdCode2String(targetID);
            }
        }

        public string targetIP
        {
            get;
            private set;
        }

        static LocalClient()
        {
            
        }

        public LocalClient(EventBus localEventBus_, MEffectorSeralizer effectorSeralizer_)
            : base(clientType_: HandshakeRequest.Types.ClientType.LocalClient, effectorSeralizer_:effectorSeralizer_)
        {
            clientTag = "Local Client";

            this.localEventBus = localEventBus_;

            this.deserializeToEventBusArgsCallbackMap = new Dictionary<string, DeserializeToEventBusArgsCallback>()
            {
                {RCCommEventName.CommInterrupted, delegate(byte[] buffer){return new CommInterruptedArgs();}},
                {RCCommEventName.CommStateChanged, ParseDataToCommStateChangedArgs},
                {RCCommEventName.CommProgressUpdated, ParseDataToCommProgressUpdatedArgs},
                {RCCommEventName.CommStartSaveMode, delegate(byte[] buffer){return new StartSaveModeArgs();}},
                {RCCommEventName.CommSaveModeCompleted, delegate(byte[] buffer){return new SaveModeCompletedArgs();}},
                {RCCommEventName.CommConnectionIsDisconnected, delegate(byte[] buffer){return new ConnectionIsDisconnectedArgs();}},
                {RCCommEventName.CommCompleted, ParseDataToCommCompletedArgs},
                {RCCommEventName.CommOutputLimiterStatusLoaded, ParseDataToCommOutputLimiterStatusLoadedArgs},
                {RCCommEventName.CommLoadEffectorModelCompleted, ParseDataToCommLoadEffectorModelCompletedArgs},
                {RCCommEventName.CommLoadDataCompleted, ParseDataToCommLoadDataCompletedArgs},
                {RCCommEventName.CommCallDeviceModeCompleted, ParseDataToCommCallDeviceModeCompletedArgs},
                {RCCommEventName.CommUploadDataCompleted, ParseDataToCommUploadDataCompletedArgs},
            };

            this.MessageHandlerMap = new Dictionary<string, Action<byte[]>>()
            {
                {MessageType.LcConnectToRcToLcResponse, LcConnectToRcToLcResponseHandler},
                {MessageType.RemoteControlResponse, RemoteControlResponseHandler},
                {MessageType.RCSwapData, RCSwapDataResponseHandler},
                {MessageType.RCCommEventResponse, RCCommEventResponseHandler},
            };
        }

        //protected virtual HandshakeRequest.Types.ClientType clientType
        //{
        //    get
        //    {
        //        return HandshakeRequest.Types.ClientType.LocalClient;
        //    }
        //}

        public void ConnectToRemoteClient(uint targetId, uint VCode)
        {
            if (!tcpCliented || rcConnected)
            {
                return;
            }

            this.targetID = targetId;

            LcConnectToRcRequest.Builder builder = new LcConnectToRcRequest.Builder()
                .SetSourceClientId(clientId)
                .SetTargetClientId(targetId)
                .SetVerificationCode(VCode);

            CommMessage message = new CommMessage(MessageType.LcConnectToRcRequest, builder.Build().ToByteArray());
            byte[] request = FrameEncoder.Encode(message);
            //Log.OutputLine(clientTag + ":远程联机请求: {0}", AEC.Core.Utils.ByteArrayToHexStr(request));
            this.tcpClient.Send(request);
        }

        private void LcConnectToRcToLcResponseHandler(byte[] buffer)
        {
            LcConnectToRcToLcResponse response = LcConnectToRcToLcResponse.ParseFrom(buffer);
            //Console.WriteLine(response.ToString());

            if (response.Status == LcConnectToRcToLcResponse.Types.StatusCode.Accept)
            {
                this.targetIP = response.TargetIp;
                this.rcConnected = true;
                this.eventBus.NotifyObeserver(new RCLcConnectToRcToLcRespArgs(true));
            }
            else
            {
                string message = string.Empty;
                switch(response.Status)
                {
                    case LcConnectToRcToLcResponse.Types.StatusCode.TargetNotOnline:
                        message = localization[Localization.remoteControl_ConnectResponse_TargetNotOnline];
                        break;

                    case LcConnectToRcToLcResponse.Types.StatusCode.VerificationCodeError:
                        message = localization[Localization.remoteControl_ConnectResponse_VerificationCodeError];
                        break;

                    case LcConnectToRcToLcResponse.Types.StatusCode.NotAllowedToBeControlled:
                        message = localization[Localization.remoteControl_ConnectResponse_NotAllowedToBeControlled];
                        break;

                    case LcConnectToRcToLcResponse.Types.StatusCode.RemoteClientBusy:
                        message = localization[Localization.remoteControl_ConnectResponse_RemoteClientBusy];
                        break;

                    default:
                        message = localization[Localization.remoteControl_ConnectResponse_Unknown];
                        break;
                }
                this.eventBus.NotifyObeserver(new RCLcConnectToRcToLcRespArgs(false, message));
            }
        }

        protected override void RCMessageHandler(CommMessage message)
        {
            //message.Print();
            if (this.MessageHandlerMap.ContainsKey(message.name))
            {
                try
                {
                    MessageHandlerMap[message.name](message.data);
                }
                catch (Exception e)
                {
                    AEC.Tools.LogHelper.WriteDebug("LocalClient.RCMessageHandler.... Exception: ", e);
                    this.CommExceptionHandler("LocalClient.MessageHandler.... Exception: " + e.Message);
                }
            }
            else
            {
                this.CommExceptionHandler("LocalClient.MessageHandler()...Unprocessable Message: " + message.name);
            }
        }

        private void RemoteControlResponseHandler(byte[] buffer)
        {
            RCResponse response = RCResponse.ParseFrom(buffer);

            if (MessageHandlerMap.ContainsKey(response.CmdName))
            {
                try
                {
                    byte[] data = null;
                    if(response.HasData)
                    {
                        data = response.Data.ToByteArray();
                    }

                    MessageHandlerMap[response.CmdName](data);
                }
                catch (Exception e)
                {
                    AEC.Tools.LogHelper.WriteDebug("RemoteClient.RemoteControlResponseHandler.... Exception: ", e.Message);
                    this.CommExceptionHandler("RemoteClient.RemoteControlResponseHandler.... Exception: " + e.Message);
                }
            }
            else
            {
                CommExceptionHandler("RemoteClient.RemoteControlResponseHandler...Exception, Disconnected From Server.");
            }
        }

        private void RemoteControlRequest(string cmdName, byte[] bytes)
        {
            if (rcConnected)
            {
                RCRequest.Builder builder = new RCRequest.Builder()
                    .SetCmdName(cmdName);

                if(bytes != null)
                {
                    builder.SetData(Google.ProtocolBuffers.ByteString.CopyFrom(bytes));
                }

                CommMessage message = new CommMessage(MessageType.RemoteControlRequest, builder.Build().ToByteArray());
                //byte[] request = ;
                //Log.OutputLine(clientTag + ":远程联机请求: {0}", AEC.Core.Utils.ByteArrayToHexStr(request));
                this.tcpClient.Send(FrameEncoder.Encode(message));
            }
        }

        public void SwapDataWithRemoteClient()
        {
            AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

            Configuration.Builder builder = new Configuration.Builder()
                .SetLang(config.localization.langCode)
                .SetCommConnectionMethod(config.connectionMethodIndex)
                .SetCommDefaultSerialPort(config.default_SerialPort)
                .SetCommSpManualMode(config.isSpManualMode)
                .SetCommTcpIp(config.udp_ip)
                .SetCommTcpPort(config.udp_port);
            //Log.OutputLine(builder.Build().ToString());

            RemoteControlRequest(MessageType.RCSwapData, builder.Build().ToByteArray());
        }

        private void RCSwapDataResponseHandler(byte[] buffer)
        {
            AudioDevice audioDevice = AudioDevice.ParseFrom(buffer);
            List<string> inputDevices = new List<string>(audioDevice.InputDevicesList);
            AEC.Tools.LogHelper.WriteDebug("Input Devices: ");
            foreach(string device in inputDevices)
            {
                AEC.Tools.LogHelper.WriteDebug(device);
            }
            AEC.Tools.LogHelper.WriteDebug("Output Devices: ");
            List<string> outputDevices = new List<string>(audioDevice.OutputDevicesList);
            foreach (string device in outputDevices)
            {
                AEC.Tools.LogHelper.WriteDebug(device);
            }
        }

        public void ProModeConnectAndLoadData()
        {
            AEC.Tools.LogHelper.WriteInfo("LocalClient.ProModeConnectAndLoadData()...");
            RemoteControlRequest(MessageType.RCProModeConnectAndLoadData, null);
        }

        public void EasyModeConnectAndLoadData()
        {
            AEC.Tools.LogHelper.WriteInfo("LocalClient.EasyModeConnectAndLoadData()...");
            RemoteControlRequest(MessageType.RCEasyModeConnectAndLoadData, null);
        }

        public void ProModeCallDeviceMode(short modeIndex)
        {
            RCDeviceModeIndexArgs.Builder builder = new RCDeviceModeIndexArgs.Builder();
            builder.SetIndex(modeIndex);
            RemoteControlRequest(MessageType.RCProModeCallDeviceMode, builder.Build().ToByteArray());
        }

        public void EasyModeCallDeviceMode(short modeIndex)
        {
            RCDeviceModeIndexArgs.Builder builder = new RCDeviceModeIndexArgs.Builder();
            builder.SetIndex(modeIndex);
            RemoteControlRequest(MessageType.RCEasyModeCallDeviceMode, builder.Build().ToByteArray());
        }

        public void UpdateToBeUploadedData(EffectorModelBase effectorModel)
        {
            try
            {
                byte[] buffer = base.effectorSeralizer.CurrEffectorSeralizeToBinary(effectorModel, false);

                byte[] compressed = CompressHelper.Compress(buffer);

                RCDeviceModeDataArgs.Builder builder = new RCDeviceModeDataArgs.Builder()
                    .SetData(Google.ProtocolBuffers.ByteString.CopyFrom(compressed))
                    .SetEncrypted(effectorModel.dataIsPrivate);

                RemoteControlRequest(MessageType.RCProModeUpdateToBeUploadedData, builder.Build().ToByteArray());
            }
            catch(Exception ex)
            {
                CommExceptionHandler("RemoteClient.UpdateToBeUploadedData()...出现异常,关闭连接:" + ex.Message);
            }
            
        }

        public void ProModeProModeUploadData()
        {
            RemoteControlRequest(MessageType.RCProModeUploadData, null);
        }

        public void EasyModeUploadMultiPresetData(List<MDeviceMode> deviceModeList)
        {
            RCMultiDeviceModesArgs.Builder builder = new RCMultiDeviceModesArgs.Builder();
            foreach(MDeviceMode deviceMode in deviceModeList)
            {
                byte[] buffer = base.effectorSeralizer.CurrEffectorSeralizeToBinary(deviceMode.effector, false);
                byte[] compressed = CompressHelper.Compress(buffer);

                Message.RCDeviceModeArgs.Builder modeBuilder = new Message.RCDeviceModeArgs.Builder()
                    .SetName(deviceMode.modeName)
                    .SetData(Google.ProtocolBuffers.ByteString.CopyFrom(compressed))
                    .SetEncrypted(deviceMode.effector.isSupportDataEncryption && deviceMode.effector.dataIsPrivate);
            }
            
            RemoteControlRequest(MessageType.RCEasyModeUploadMultiPresetData, builder.Build().ToByteArray());
        }

        public void ForcedDisconnect()
        {
            RemoteControlRequest(MessageType.RCForcedDisconnect, null);
        }

        public void ReadSystemSatatus()
        {
            RemoteControlRequest(MessageType.RCReadSystemSatatus, null);
        }

        private void RCCommEventResponseHandler(byte[] buffer)
        {
            RCCommEvent rcCommEvent = RCCommEvent.ParseFrom(buffer);
            
            if (deserializeToEventBusArgsCallbackMap.ContainsKey(rcCommEvent.EventName))
            {
                EventBusArgs args = deserializeToEventBusArgsCallbackMap[rcCommEvent.EventName](rcCommEvent.Args.ToByteArray());
                if(args != null)
                {
                    localEventBus.NotifyObeserver(args);
                }
                else
                {
                    AEC.Tools.LogHelper.WriteInfo("LocalClient.DeserializeToEventBusArgs()...Deserialize Failed!", rcCommEvent.EventName);
                }
            }
            else
            {
                AEC.Tools.LogHelper.WriteInfo("LocalClient.DeserializeToEventBusArgs()...{0} does not support Deserialize!", rcCommEvent.EventName);
            }
        }

        private static EventBusArgs ParseDataToCommStateChangedArgs(byte[] buffer)
        {
            try
            {
                RCCommStateChangedArgs commArgs = RCCommStateChangedArgs.ParseFrom(buffer);
                Comm.CommState state = (Comm.CommState)commArgs.CommStatus;
                CommStateChangedArgs args = new CommStateChangedArgs(state);
                return args;
            }
            catch(Exception)
            {
                return null;
            }
        }

        private static EventBusArgs ParseDataToCommProgressUpdatedArgs(byte[] buffer)
        {
            try
            {
                RCCommProgressUpdatedArgs commArgs = RCCommProgressUpdatedArgs.ParseFrom(buffer);
                CommProgressUpdatedArgs args = new CommProgressUpdatedArgs(commArgs.Msg, commArgs.End, commArgs.ProgressIncrement, commArgs.Failed);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static EventBusArgs ParseDataToCommLoadEffectorModelCompletedArgs(byte[] buffer)
        {
            try
            {
                RCCommLoadEffectorModelCompletedArgs commArgs = RCCommLoadEffectorModelCompletedArgs.ParseFrom(buffer);
                DeviceModel model = (DeviceModel)commArgs.EffectorModel;
                LoadEffectorModelCompletedArgs args = new LoadEffectorModelCompletedArgs(model);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private EventBusArgs ParseDataToCommCompletedArgs(byte[] buffer)
        {
            try
            {
                RCCommCompletedArgs commArgs = RCCommCompletedArgs.ParseFrom(buffer);
                AEC.Comm.CommResult commResult = new Comm.CommResult((Comm.CommProcess)commArgs.Process, commArgs.Success);
                CommCompletedArgs args = new CommCompletedArgs(commResult);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private EventBusArgs ParseDataToCommOutputLimiterStatusLoadedArgs(byte[] buffer)
        {
            try
            {
                RCCommOutputLimiterStatusLoadedArgs commArgs = RCCommOutputLimiterStatusLoadedArgs.ParseFrom(buffer);
                if(commArgs.StatusCount != QuadChannelLimiterStatus.Quad)
                {
                    return null;
                }

                QuadChannelLimiterStatus limiterStatus = new QuadChannelLimiterStatus();

                for(int i = 0; i < QuadChannelLimiterStatus.Quad; ++i)
                {
                    limiterStatus[i].level = commArgs.StatusList[i].Level;
                    limiterStatus[i].ratio = commArgs.StatusList[i].Ratio;
                }

                OutputLimiterStatusLoadedArgs args = new OutputLimiterStatusLoadedArgs(limiterStatus);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private EventBusArgs ParseDataToCommLoadDataCompletedArgs(byte[] buffer)
        {
            try
            {
                RCLoadDataCompletedArgs commArgs = RCLoadDataCompletedArgs.ParseFrom(buffer);
                Comm.CommProcess commProcess = (Comm.CommProcess)commArgs.CommProcess;
                byte[] decompressed = CompressHelper.Decompress(commArgs.Data.ToByteArray());

                EffectorModelBase effectorModel = base.effectorSeralizer.DeseralizeToNextEffectorFromBuffer(decompressed, true);

                LoadDataCompletedArgs args = new LoadDataCompletedArgs(commProcess, effectorModel);

                return args;
            }
            catch(Exception )
            {
                return null;
            }
        }

        private EventBusArgs ParseDataToCommCallDeviceModeCompletedArgs(byte[] buffer)
        {
            try
            {
                RCCallDeviceModeCompletedArgs commArgs = RCCallDeviceModeCompletedArgs.ParseFrom(buffer);
                Comm.CommProcess commProcess = (Comm.CommProcess)commArgs.CommProcess;
                byte[] decompressed = CompressHelper.Decompress(commArgs.Data.ToByteArray());

                EffectorModelBase effectorModel = base.effectorSeralizer.DeseralizeToCurrEffectorFromBuffer(decompressed, false);
                effectorModel.system.deviceModeCallIndex.initV = (short)commArgs.DeviceModeCallIndex;

                CallDeviceModeCompletedArgs args = new CallDeviceModeCompletedArgs(commProcess, effectorModel);

                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private EventBusArgs ParseDataToCommUploadDataCompletedArgs(byte[] buffer)
        {
            try
            {
                RCDeviceModeDataArgs commArgs = RCDeviceModeDataArgs.ParseFrom(buffer);
                //Comm.CommProcess commProcess = (Comm.CommProcess)commArgs.CommProcess;
                byte[] decompressed = CompressHelper.Decompress(commArgs.Data.ToByteArray());
                
                EffectorModelBase effectorModel = base.effectorSeralizer.DeseralizeToCurrEffectorFromBuffer(decompressed, false);

                if(effectorModel.isSupportDataEncryption && commArgs.Encrypted)
                {
                    effectorModel.dataIsPrivate = true;
                }

                UploadDataCompletedArgs args = new UploadDataCompletedArgs(effectorModel);

                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SendEffectorEvent(EventBusArgs e)
        {
            if (!rcConnected)
            {
                return;
            }

            byte[] buffer = EventBusArgsSerialize(e);
            if (buffer != null)
            {
                this.RemoteControlRequest(MessageType.RCCommEventRequest, buffer);
            }
            else
            {
                AEC.Tools.LogHelper.WriteInfo("EventBusArgs type: {0} Serialize Failed!", e.type);
            }
        }

        private static byte[] EventBusArgsSerialize(EventBusArgs e)
        {
            RCCommEvent.Builder builder = new RCCommEvent.Builder();

            switch (e.type)
            {
                case EventType.DataParamChanged:
                    {
                        DataParamChangedArgs args = e as DataParamChangedArgs;
                        RCDataParamChangedArgs.Builder argsBuilder = new RCDataParamChangedArgs.Builder()
                            .SetModuleIndex(args.param.moduleIndex)
                            .SetAddr(args.param.addr)
                            .SetMax(args.param.max)
                            .SetValue(args.param.v);
                        builder.EventName = RCCommEventName.CommDataParamChanged;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.EQChanged:
                    {
                        EQChangedArgs args = e as EQChangedArgs;
                        EQ eq = args.eq;
                        RCEQChangedArgs.Builder argsBuilder = new RCEQChangedArgs.Builder()
                            .SetModuleIndex(args.moduleIndex)
                            .SetEqIndex(eq.index)
                            .SetEqType(eq.typeIndex)
                            .SetEqFreq(eq.freqIndex)
                            .SetEqGain(eq.gainIndex)
                            .SetEqQ(eq.qIndex_)
                            .SetEqS(eq.sIndex_);

                        builder.EventName = RCCommEventName.CommEQChanged;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.PasswordChanged:
                    {
                        PasswordChangedArgs args = e as PasswordChangedArgs;
                        RCPasswordChangedArgs.Builder argsBuilder = new RCPasswordChangedArgs.Builder()
                            .SetAddr(args.addr)
                            .SetPassword(args.passowrd);

                        builder.EventName = RCCommEventName.CommPasswordChanged;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.SaveDeviceMode:
                    {
                        SaveDeviceModeArgs args = e as SaveDeviceModeArgs;
                        RCSaveDeviceModeArgs.Builder argsBuilder = new RCSaveDeviceModeArgs.Builder()
                            .SetModeIndex(args.modeIndex)
                            .SetModeName(args.modeName);

                        builder.EventName = RCCommEventName.CommSaveDeviceMode;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.UpdateModuleAllData:
                    {
                        UpdateModuleAllDataArgs args = e as UpdateModuleAllDataArgs;
                        RCUpdateModuleAllDataArgs.Builder argsBuilder = new RCUpdateModuleAllDataArgs.Builder()
                            .SetModuleIndex(args.moduleIndex);

                        List<int> datas = new List<int>(args.dataList.Count);
                        for(int i = 0; i < datas.Count; ++i)
                        {
                            datas[i] = args.dataList[i];
                        }

                        argsBuilder.AddRangeDataList(datas);

                        builder.EventName = RCCommEventName.CommUpdateModuleAllData;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                default:
                    return null;
            }
        }
    }
}
