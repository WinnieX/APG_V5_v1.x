using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Effectors;
using AEC.UI.RemoteControl;

namespace AEC.RemoteControl
{
    public class RCClientEngine
    {
        private readonly EventBus localEvent;
        private readonly EventBus remoteEvent;

        public readonly LocalClient localClient;
        public readonly RemoteClient remoteClient;

        private bool _localRCModeIsOn = false;
        private bool _remoteRCModeIsOn = false;

        public bool localRCModeIsOn
        {
            get
            {
                return _localRCModeIsOn;
            }
            set
            {
                if(_localRCModeIsOn != value)
                {
                    _localRCModeIsOn = value;
                    this.localEvent.NotifyObeserver(new RCOperationStatusChangedArgs(_localRCModeIsOn));
                }
            }
        }

        public bool remoteRCModeIsOn
        {
            get
            {
                return _remoteRCModeIsOn;
            }
            set
            {
                if(_remoteRCModeIsOn != value)
                {
                    _remoteRCModeIsOn = value;
                }
            }
        }

        public RCClientEngine(EventBus localEvent_, EventBus remoteEvent_, MEffectorSeralizer effectorSeralizer)
        {
            this.localEvent = localEvent_;
            this.remoteEvent = remoteEvent_;

            this.localClient = new LocalClient(this.localEvent, effectorSeralizer);
            this.remoteClient = new RemoteClient(this.remoteEvent, effectorSeralizer);

            this.localClient.eventBus.RegisterHandler(LocalClientEventBusHandler);
            this.remoteClient.eventBus.RegisterHandler(RemoteClientEventBusHandler);
        }

        private void LocalClientEventBusHandler(object sender, EventBusArgs e)
        {
            switch(e.type)
            {
                case EventType.RCOperationStatusChanged:
                    RCOperationStatusChangedArgs osca = e as RCOperationStatusChangedArgs;
                    localRCModeIsOn = osca.isOn;
                    AEC.Tools.LogHelper.WriteInfo("RC local mode:{0}", localRCModeIsOn);
                    break;

                case EventType.RCConnectionStatusChanged:
                case EventType.RCDisconnectFromServer:
                case EventType.RCDisconnectedFromServer:                
                case EventType.RCDisconnectedFromRc:
                case EventType.RCDisconnectFromRc:
                    this.localEvent.NotifyObeserver(e);
                    break;
            }
        }

        private void RemoteClientEventBusHandler(object sender, EventBusArgs e)
        {
            switch (e.type)
            {
                case EventType.RCOperationStatusChanged:
                    RCOperationStatusChangedArgs osca = e as RCOperationStatusChangedArgs;
                    remoteRCModeIsOn = osca.isOn;
                    AEC.Tools.LogHelper.WriteInfo("RC remote mode:{0}", remoteRCModeIsOn);
                    break;

                case EventType.RCDisconnectFromServer:
                case EventType.RCDisconnectedFromServer:
                case EventType.RCDisconnectedFromRc:
                case EventType.RCDisconnectFromRc:
                    this.remoteEvent.NotifyObeserver(e);
                    break;
            }
        }
    }
}
