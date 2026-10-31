using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RemoteControl.Message
{
    class MessageType
    {
        public const string HandshakeRequest = "handshake.request";
        public const string HandshakeResponse = "handshake.response";
        public const string HeartbeatRequest = "heartbeat.request";
        public const string HeartbeatResponse = "heartbeat.response";
        public const string RemoteClientUpdateRequest = "remoteClientUpdate.request";

        public const string LcConnectToRcRequest = "LcConnectToRc.request";
        public const string LcConnectToRcToLcResponse = "LcConnectToRcToLc.response";
        public const string LcConnectToRcToRcResponse = "LcConnectToRcToRc.response";

        public const string LcDisconnectFromRcRequest = "LcDisconnectFromRc.request";
        public const string LcDisconnectFromRcResponse = "LcDisconnectFromRc.response";

        public const string RemoteControlRequest = "remoteControl.request";
        public const string RemoteControlResponse = "remoteControl.response";

        public const string RCSwapData = "rcSwapData";

        public const string RCForcedDisconnect = "rcForcedDisconnect";
        public const string RCReadSystemSatatus = "rcReadSystemSatatus";
        public const string RCProModeConnectAndLoadData = "rcProModeConnectAndLoadData";
        public const string RCEasyModeConnectAndLoadData = "rcEasyModeConnectAndLoadData";
        public const string RCProModeCallDeviceMode = "rcProModeCallDeviceMode";
        public const string RCEasyModeCallDeviceMode = "rcEasyModeCallDeviceMode";
        public const string RCProModeUpdateToBeUploadedData = "rcProModeUpdateToBeUploadedData";
        public const string RCProModeUploadData = "rcProModeUploadData";
        public const string RCEasyModeUploadMultiPresetData = "rcEasyModeUploadMultiPresetData";
        
        //public const string RCProModeConnectAndLoadData = "rcProModeConnectAndLoadData";
        //public const string RCProModeConnectAndLoadData = "rcProModeConnectAndLoadData";

        public const string RCCommEventRequest = "rcCommEventRequest";
        public const string RCCommEventResponse = "rcCommEventResponse";

        public const string QueryServerStatusRequest = "queryServerStatus.request";
    }
}
