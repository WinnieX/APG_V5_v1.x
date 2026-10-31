using System;
using System.Collections.Generic;
using AEC.RemoteControl.Message;
using AEC.RemoteControl.Utility;
using AEC.RemoteControl.Protocol;
using AEC.Core;
using AEC.Common;
using AEC.Effectors;
using AEC.Tools;

namespace AEC.RemoteControl
{
    public class RemoteClient : RCClient
    {
        private static readonly Dictionary<string, DeserializeToEventBusArgsCallback> deserializeToEventBusArgsCallbackMap;

        private Dictionary<string, Action<byte[]>> MessageHandlerMap;
        private readonly EventBus remoteEventBus;

        private uint _verificationCode = VerificationCodeHelper.GeneratorCode();
        private bool _allowToBeControlled = true;

        public bool allowToBeControlled
        {
            get
            {
                return _allowToBeControlled;
            }
            set
            {
                _allowToBeControlled = value;
            }
        }

        public string VCode
        {
            get
            {
                return VerificationCodeHelper.Code2String(this._verificationCode);
            }
        }

        public uint sourceID
        {
            get;
            private set;
        }

        public string sourceIDCode
        {
            get
            {
                return ClientIdCode2String(sourceID);
            }
        }

        public string sourceIP
        {
            get;
            private set;
        }

        public void UpdateVerificationCode()
        {
            _verificationCode = VerificationCodeHelper.GeneratorCode();
        }

        static RemoteClient()
        {
            deserializeToEventBusArgsCallbackMap = new Dictionary<string, DeserializeToEventBusArgsCallback>()
            {
                {RCCommEventName.CommDataParamChanged, ParseDataToCommDataParamChangedArgs},
                {RCCommEventName.CommEQChanged, ParseDataToCommEQChangedArgs},
                {RCCommEventName.CommPasswordChanged, ParseDataToCommPasswordChangedArgs},
                {RCCommEventName.CommSaveDeviceMode, ParseDataToCommSaveDeviceModeArgs},
                {RCCommEventName.CommUpdateModuleAllData, ParseDataToCommUpdateModuleAllDataArgs},
            };
        }

        public RemoteClient(EventBus remoteEventBus_, MEffectorSeralizer effectorSeralizer_)
            : base(clientType_: HandshakeRequest.Types.ClientType.RemoteClient, effectorSeralizer_:effectorSeralizer_)
        {
            clientTag = "Remote Client";

            this.remoteEventBus = remoteEventBus_;

            this.MessageHandlerMap = new Dictionary<string, Action<byte[]>>()
            {
                {MessageType.LcConnectToRcRequest, LcConnectToRcRequestHandler},
                {MessageType.LcConnectToRcToRcResponse, LcConnectToRcToRcResponseHandler},
                {MessageType.RemoteControlRequest, RemoteControlResponseHandler},
                {MessageType.RCSwapData, RCSwapDataRequestHandler},
                {MessageType.RCProModeConnectAndLoadData, RCProModeConnectAndLoadDataHandler},
                {MessageType.RCEasyModeConnectAndLoadData, RCEasyModeConnectAndLoadDataHandler},
                {MessageType.RCProModeCallDeviceMode, RCProModeCallDeviceModeHandler},
                {MessageType.RCEasyModeCallDeviceMode, RCEasyModeCallDeviceModeHandler},
                {MessageType.RCProModeUpdateToBeUploadedData, RCProModeUpdateToBeUploadedDataHandler},
                {MessageType.RCProModeUploadData, RCProModeUploadDataHandler},
                {MessageType.RCEasyModeUploadMultiPresetData, RCEasyModeUploadMultiPresetDataHandler},
                {MessageType.RCForcedDisconnect, RCForcedDisconnectHandler},
                {MessageType.RCReadSystemSatatus, RCReadSystemSatatusHandler},
                {MessageType.RCCommEventRequest, RCCommEventRequestHandler},
            };
        }

        private void LcConnectToRcRequestHandler(byte[] buffer)
        {
            LcConnectToRcRequest request = LcConnectToRcRequest.ParseFrom(buffer);

            LcConnectToRcToLcResponse.Builder builder = new LcConnectToRcToLcResponse.Builder()
                .SetStatus(LcConnectToRcToLcResponse.Types.StatusCode.Unknown)
                .SetSourceClientId(request.SourceClientId)
                .SetTargetIp(base.ipAddress);

            if (!this._allowToBeControlled)
            {
                builder.Status = LcConnectToRcToLcResponse.Types.StatusCode.NotAllowedToBeControlled;
            }
            else if (this.rcConnected)
            {
                builder.Status = LcConnectToRcToLcResponse.Types.StatusCode.RemoteClientBusy;
            }
            else if(request.VerificationCode != this._verificationCode)
            {
                builder.Status = LcConnectToRcToLcResponse.Types.StatusCode.VerificationCodeError;
            }
            else
            {
                builder.Status = LcConnectToRcToLcResponse.Types.StatusCode.Accept;
            }

            CommMessage response = new CommMessage(MessageType.LcConnectToRcToLcResponse, builder.Build().ToByteArray());
            this.tcpClient.Send(FrameEncoder.Encode(response));
        }

        //被动断开远程控制连接
        private void DisconnectedFromLcHandler(string message)
        {
            if (!tcpCliented || !rcConnected)
            {
                return;
            }

            this.rcConnected = false;
            //this.eventBus.NotifyObeserver(new RCDisconnectedFromRcArgs(message));
        }

        private void LcConnectToRcToRcResponseHandler(byte[] buffer)
        {
            LcConnectToRcToRcResponse resp = LcConnectToRcToRcResponse.ParseFrom(buffer);
            this.sourceID = resp.SourceClientId;
            this.sourceIP = resp.SourceIp;
            this.eventBus.NotifyObeserver(new RCLcConnectToRcToRcRespArgs());
            base.rcConnected = true;
        }

        protected override void RCMessageHandler(CommMessage message)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCMessageHandler:{0}", message.name);
            if (this.MessageHandlerMap.ContainsKey(message.name))
            {
                try
                {
                    MessageHandlerMap[message.name](message.data);
                }
                catch (Exception e)
                {
                    AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCMessageHandler().... Exception: {0}", e.Message);
                    this.CommExceptionHandler("RemoteClient.MessageHandler().... Exception:" + e.Message);
                }
            }
            else
            {
                this.CommExceptionHandler("RemoteClient.MessageHandler()...Unprocessable Message: " + message.name);
            }
        }

        private void RemoteControlResponseHandler(byte[] buffer)
        {
            RCRequest request = RCRequest.ParseFrom(buffer);

            //Log.OutputLine(request.ToString());

            if (MessageHandlerMap.ContainsKey(request.CmdName))
            {
                try
                {
                    byte[] data = null;
                    if (request.HasData)
                    {
                        data = request.Data.ToByteArray();
                    }

                    MessageHandlerMap[request.CmdName](data);
                }
                catch (Exception e)
                {
                    AEC.Tools.LogHelper.WriteDebug("RemoteClient.RemoteControlResponseHandler.... Exception: ", e.Message);
                    this.CommExceptionHandler("RemoteClient.RemoteControlResponseHandler.... Exception: " + e.Message);
                }
            }
            else
            {
                CommExceptionHandler("RemoteClient.RemoteControlResponseHandler出现异常,关闭连接");
            }
        }

        private void RemoteControlResponse(string cmdName, byte[] bytes)
        {
            if (Connected && rcConnected)
            {
                RCResponse.Builder builder = new RCResponse.Builder()
                    .SetCmdName(cmdName);

                if (bytes != null)
                {
                    builder.SetData(Google.ProtocolBuffers.ByteString.CopyFrom(bytes));
                }

                CommMessage message = new CommMessage(MessageType.RemoteControlResponse, builder.Build().ToByteArray());
                //byte[] request = ;
                //Log.OutputLine(clientTag + ":远程联机请求: {0}", AEC.Core.Utils.ByteArrayToHexStr(request));
                if(this.tcpClient != null)
                {
                    this.tcpClient.Send(FrameEncoder.Encode(message));
                }
            }
        }

        //将通讯事件发送到LocalRemote
        public void SendCommEvent(EventBusArgs e)
        {
            if (!rcConnected)
            {
                return;
            }

            byte[] buffer = EventBusArgsSerialize(e);
            if(buffer != null)
            {
                this.RemoteControlResponse(MessageType.RCCommEventResponse, buffer);
            }
            else
            {
                AEC.Tools.LogHelper.WriteInfo("EventBusArgs type: {0} Serialization Error!", e.type);
            }

            //if (e.GetType().IsSerializable)
            //{
            //    try
            //    {
            //        /* 序列化
            //        MemoryStream stream = new MemoryStream();
            //        BinaryFormatter bf = new BinaryFormatter();
            //        bf.Serialize(stream, e);
            //        byte[] buffer = stream.ToArray();
            //        */
            //        byte[] buffer = EventBusArgsSerialize();
            //        this.RemoteControlResponse(MessageType.RCCommEventResponse, buffer);
            //    }
            //    catch (Exception ex)
            //    {
            //        AEC.Log.OutputLine("RemoteClient.SendCommEvent()... 出现异常:{0}", ex.Message);
            //    }
            //}
            //else
            //{
            //    AEC.Log.OutputLine("EventBusArgs type:{0} 是不可序列化的!", e.type);
            //}
        }

        private byte[] EventBusArgsSerialize(EventBusArgs e)
        {
            RCCommEvent.Builder builder = new RCCommEvent.Builder();

            switch(e.type)
            {
                case EventType.CommInterrupted:
                    builder.EventName = RCCommEventName.CommInterrupted;
                    return builder.Build().ToByteArray();

                case EventType.CommStateChanged:
                    {
                        CommStateChangedArgs args = e as CommStateChangedArgs;
                        RCCommStateChangedArgs.Builder argsBuilder = new RCCommStateChangedArgs.Builder();
                        argsBuilder.CommStatus = (uint)(args.commState);

                        builder.EventName = RCCommEventName.CommStateChanged;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.CommProgressUpdated:
                    {
                        CommProgressUpdatedArgs args = e as CommProgressUpdatedArgs;
                        RCCommProgressUpdatedArgs.Builder argsBuilder = new RCCommProgressUpdatedArgs.Builder()
                            .SetMsg(args.msg)
                            .SetEnd(args.isEnd)
                            .SetProgressIncrement(args.progressIncrement)
                            .SetFailed(args.isFailed);

                        builder.EventName = RCCommEventName.CommProgressUpdated;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.StartSaveMode:
                    builder.EventName = RCCommEventName.CommStartSaveMode;
                    return builder.Build().ToByteArray();

                case EventType.SaveModeCompleted:
                    builder.EventName = RCCommEventName.CommSaveModeCompleted;
                    return builder.Build().ToByteArray();

                case EventType.ConnectionIsDisconnected:
                    builder.EventName = RCCommEventName.CommConnectionIsDisconnected;
                    return builder.Build().ToByteArray();

                case EventType.CommCompleted:
                    {
                        CommCompletedArgs args = e as CommCompletedArgs;
                        RCCommCompletedArgs.Builder argsBuilder = new RCCommCompletedArgs.Builder()
                            .SetProcess((uint)args.commResult.process)
                            .SetSuccess(args.commResult.success);

                        builder.EventName = RCCommEventName.CommCompleted;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.OutputLimiterStatusLoaded:
                    {
                        OutputLimiterStatusLoadedArgs args = e as OutputLimiterStatusLoadedArgs;

                        RCCommOutputLimiterStatusLoadedArgs.Builder argsBuilder = new RCCommOutputLimiterStatusLoadedArgs.Builder();

                        for(int i = 0; i < QuadChannelLimiterStatus.Quad; ++i)
                        {
                            RCCommOutputLimiterStatusLoadedArgs.Types.LimiterStatus.Builder limiterStatusBuilder = new RCCommOutputLimiterStatusLoadedArgs.Types.LimiterStatus.Builder()
                                .SetLevel(args.limiterStatus[i].level)
                                .SetRatio(args.limiterStatus[i].ratio);

                            argsBuilder.AddStatus(limiterStatusBuilder.Build());
                        }

                        builder.EventName = RCCommEventName.CommOutputLimiterStatusLoaded;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.LoadEffectorModelCompleted:
                    {
                        LoadEffectorModelCompletedArgs args = e as LoadEffectorModelCompletedArgs;
                        RCCommLoadEffectorModelCompletedArgs.Builder argsBuilder = new RCCommLoadEffectorModelCompletedArgs.Builder()
                            .SetEffectorModel((uint)args.model);

                        builder.EventName = RCCommEventName.CommLoadEffectorModelCompleted;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.LoadDataCompleted:
                    {
                        LoadDataCompletedArgs args = e as LoadDataCompletedArgs;

                        RCLoadDataCompletedArgs.Builder argsBuilder = new RCLoadDataCompletedArgs.Builder()
                            .SetCommProcess((int)args.commProcess);
                        
                        EffectorModelBase effectorModel = args.effectorModel;
                        if (effectorModel.isSupportDataEncryption && effectorModel.dataIsPrivate)
                        {
                            effectorModel.dataIsPrivate = true;
                        }

                        byte[] buffer = null;

                        try
                        {
                            buffer = base.effectorSeralizer.NextEffectorSeralizeToBinary(effectorModel, true);
                        }
                        catch(Exception ex)
                        {
                            AEC.Tools.LogHelper.WriteDebug("RemoteClient.EventBusArgsSerialize()...base.effectorSeralizer.NextEffectorSeralizeToBinary(effectorModel, true).... Exception: ", ex);
                            return null;
                        }
                       
                        byte[] compressed = CompressHelper.Compress(buffer);

                        argsBuilder.SetData(Google.ProtocolBuffers.ByteString.CopyFrom(compressed));

                        builder.EventName = RCCommEventName.CommLoadDataCompleted;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.CallDeviceModeCompleted:
                    {
                        CallDeviceModeCompletedArgs args = e as CallDeviceModeCompletedArgs;
                        RCCallDeviceModeCompletedArgs.Builder argsBuilder = new RCCallDeviceModeCompletedArgs.Builder()
                            .SetCommProcess((int)args.commProcess);

                        EffectorModelBase effectorModel = args.effectorModel;

                        byte[] buffer = base.effectorSeralizer.CurrEffectorSeralizeToBinary(effectorModel, false);
                        byte[] compressed = CompressHelper.Compress(buffer);

                        argsBuilder.SetData(Google.ProtocolBuffers.ByteString.CopyFrom(compressed));
                        argsBuilder.SetDeviceModeCallIndex(effectorModel.system.deviceModeCallIndex.v);

                        builder.EventName = RCCommEventName.CommCallDeviceModeCompleted;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                case EventType.UploadDataCompleted:
                    {
                        UploadDataCompletedArgs args = e as UploadDataCompletedArgs;
                        RCDeviceModeDataArgs.Builder argsBuilder = new RCDeviceModeDataArgs.Builder();

                        EffectorModelBase effectorModel = args.effectorModel;

                        byte[] buffer = base.effectorSeralizer.CurrEffectorSeralizeToBinary(effectorModel, false);
                        byte[] compressed = CompressHelper.Compress(buffer);

                        argsBuilder.SetData(Google.ProtocolBuffers.ByteString.CopyFrom(compressed));
                        argsBuilder.SetEncrypted(effectorModel.dataIsPrivate);

                        builder.EventName = RCCommEventName.CommUploadDataCompleted;
                        builder.SetArgs(Google.ProtocolBuffers.ByteString.CopyFrom(argsBuilder.Build().ToByteArray()));

                        return builder.Build().ToByteArray();
                    }

                default:
                    return null;
            }
        }

        private void RCSwapDataRequestHandler(byte[] buffer)
        {
            Configuration configuration = Configuration.ParseFrom(buffer);
            //Console.WriteLine(configuration.ToString());

            List<string> audioInputDevices = new List<string>();
            foreach(AEC.RTA.AudioDevice device in AEC.RTA.AudioIn.Devices)
            {
                audioInputDevices.Add(device.Name);
            }
            List<string> audioOutputDevices = new List<string>();
            foreach(AEC.RTA.AudioDevice device in AEC.RTA.AudioOut.Devices)
            {
                audioOutputDevices.Add(device.Name);
            }

            AudioDevice.Builder builder = new AudioDevice.Builder()
                .AddRangeInputDevices(audioInputDevices)
                .AddRangeOutputDevices(audioOutputDevices);

            this.RemoteControlResponse(MessageType.RCSwapData, builder.Build().ToByteArray());
        }

        private void RCProModeConnectAndLoadDataHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCProModeConnectAndLoadDataHandler()...");
            this.remoteEventBus.NotifyObeserver(new RCProModeConnectAndLoadDataArgs());
        }

        private void RCEasyModeConnectAndLoadDataHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCEasyModeConnectAndLoadDataHandler()...");
            this.remoteEventBus.NotifyObeserver(new RCEasyModeConnectAndLoadDataArgs());
        }

        private void RCProModeCallDeviceModeHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCProModeCallDeviceModeHandler()...");
            if(buffer == null)
            {
                return;
            }

            RCDeviceModeIndexArgs deviceModeIndex = RCDeviceModeIndexArgs.ParseFrom(buffer);
            this.remoteEventBus.NotifyObeserver(new RCProModeCallDeviceModeArgs((short)deviceModeIndex.Index));
        }

        private void RCEasyModeCallDeviceModeHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCEasyModeCallDeviceModeHandler()...");
            if (buffer == null)
            {
                return;
            }

            RCDeviceModeIndexArgs deviceModeIndex = RCDeviceModeIndexArgs.ParseFrom(buffer);
            this.remoteEventBus.NotifyObeserver(new RCEasyModeCallDeviceModeArgs((short)deviceModeIndex.Index));
        }

        private void RCProModeUpdateToBeUploadedDataHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCProModeUpdateToBeUploadedDataHandler()...");
            if (buffer == null)
            {
                return;
            }

            RCDeviceModeDataArgs deviceModeData = RCDeviceModeDataArgs.ParseFrom(buffer);

            byte[] decompressed = CompressHelper.Decompress(deviceModeData.Data.ToByteArray());

            try
            {
                EffectorModelBase effectorModel = base.effectorSeralizer.DeseralizeToCurrEffectorFromBuffer(decompressed, true);
                if(effectorModel.isSupportDataEncryption && deviceModeData.Encrypted)
                {
                    effectorModel.dataIsPrivate = deviceModeData.Encrypted;
                }
                this.remoteEventBus.NotifyObeserver(new RCProModeUpdateToBeUploadedDataArgs(effectorModel));
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("RemoteClient.RCProModeUpdateToBeUploadedDataHandler().... Exception: ", ex);
            }
        }

        private void RCProModeUploadDataHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCProModeUploadDataHandler()...");
            this.remoteEventBus.NotifyObeserver(new RCProModeUploadDataArgs());
        }

        private void RCEasyModeUploadMultiPresetDataHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCEasyModeUploadMultiPresetDataHandler()...");
            if (buffer == null)
            {
                return;
            }

            List<MDeviceMode> deviceModeList = new List<MDeviceMode>();

            RCMultiDeviceModesArgs multiDeviceModes = RCMultiDeviceModesArgs.ParseFrom(buffer);

            foreach (Message.RCDeviceModeArgs deviceMode in multiDeviceModes.DeviceModeList)
            {
                byte[] decompressed = CompressHelper.Decompress(deviceMode.Data.ToByteArray());

                try
                {
                    EffectorModelBase effectorModel = base.effectorSeralizer.DeseralizeToCurrEffectorFromBuffer(decompressed, false);
                    if (effectorModel.isSupportDataEncryption && deviceMode.Encrypted)
                    {
                        effectorModel.dataIsPrivate = true;
                    }

                    deviceModeList.Add(new MDeviceMode(effectorModel, deviceMode.Name));
                }
                catch(Exception ex)
                {
                    AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCEasyModeUploadMultiPresetDataHandler().... Exception: ", ex);
                }
            }

            this.remoteEventBus.NotifyObeserver(new RCEasyUploadMultiPresetDataArgs(deviceModeList));
        }

        private void RCForcedDisconnectHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCForcedDisconnectHandler()...");
            this.remoteEventBus.NotifyObeserver(new RCForcedDisconnectArgs());
        }

        private void RCReadSystemSatatusHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCReadSystemSatatusHandler()...");
            this.remoteEventBus.NotifyObeserver(new RCReadSystemSatatusArgs());
        }

        private void RCCommEventRequestHandler(byte[] buffer)
        {
            AEC.Tools.LogHelper.WriteInfo("RemoteClient.RCCommEventRequestHandler()...");
            RCCommEvent rcCommEvent = RCCommEvent.ParseFrom(buffer);

            if (deserializeToEventBusArgsCallbackMap.ContainsKey(rcCommEvent.EventName))
            {
                EventBusArgs args = deserializeToEventBusArgsCallbackMap[rcCommEvent.EventName](rcCommEvent.Args.ToByteArray());
                if (args != null)
                {
                    remoteEventBus.NotifyObeserver(args);
                }
                else
                {
                    AEC.Tools.LogHelper.WriteInfo("LocalClient.DeserializeToEventBusArgs()...Deserialize Failed!", rcCommEvent.EventName);
                }
            }
            else
            {
                AEC.Tools.LogHelper.WriteInfo("LocalClient.DeserializeToEventBusArgs()...{0} does not support deserialize！", rcCommEvent.EventName);
            }
        }

        private static EventBusArgs ParseDataToCommDataParamChangedArgs(byte[] buffer)
        {
            try
            {
                RCDataParamChangedArgs commArgs = RCDataParamChangedArgs.ParseFrom(buffer);
                ShortParam param = new ShortParam((byte)commArgs.ModuleIndex, (byte)commArgs.Addr, (short)commArgs.Max, (short)commArgs.Value);
                DataParamChangedArgs args = new DataParamChangedArgs(param);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static EventBusArgs ParseDataToCommEQChangedArgs(byte[] buffer)
        {
            try
            {
                RCEQChangedArgs commArgs = RCEQChangedArgs.ParseFrom(buffer);
                EQ eq = EQ.Create((short)commArgs.EqIndex);
                eq.typeIndex = (short)commArgs.EqType;
                eq.freqIndex = (short)commArgs.EqFreq;
                eq.gainIndex = (short)commArgs.EqGain;
                eq.qIndex_ = (short)commArgs.EqQ;
                eq.sIndex_ = (short)commArgs.EqS;
                EQChangedArgs args = new EQChangedArgs((byte)commArgs.ModuleIndex, eq);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static EventBusArgs ParseDataToCommPasswordChangedArgs(byte[] buffer)
        {
            try
            {
                RCPasswordChangedArgs commArgs = RCPasswordChangedArgs.ParseFrom(buffer);
                PasswordChangedArgs args = new PasswordChangedArgs((byte)commArgs.Addr, commArgs.Password);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static EventBusArgs ParseDataToCommSaveDeviceModeArgs(byte[] buffer)
        {
            try
            {
                RCSaveDeviceModeArgs commArgs = RCSaveDeviceModeArgs.ParseFrom(buffer);
                SaveDeviceModeArgs args = new SaveDeviceModeArgs((short)commArgs.ModeIndex, commArgs.ModeName);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static EventBusArgs ParseDataToCommUpdateModuleAllDataArgs(byte[] buffer)
        {
            try
            {
                RCUpdateModuleAllDataArgs commArgs = RCUpdateModuleAllDataArgs.ParseFrom(buffer);
                List<short> datas = new List<short>(commArgs.DataListCount);
                for(int i = 0; i < datas.Count; ++i)
                {
                    datas[i] = (short)commArgs.GetDataList(i);
                }
                UpdateModuleAllDataArgs args = new UpdateModuleAllDataArgs((byte)commArgs.ModuleIndex, datas);
                return args;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
