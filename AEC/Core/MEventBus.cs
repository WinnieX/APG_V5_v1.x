using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class MEventBus
    {
        private event EventHandler<MEventBusArgs> _handler;
        private bool _isNotify = true;

        public void RegisterHandler(EventHandler<MEventBusArgs> handler)
        {
            this._handler += handler;
        }

        public void RemoveHandler(EventHandler<MEventBusArgs> handler)
        {
            this._handler -= handler;
        }

        public void RegisterHandler(IEventHanler<MEventBusArgs> eventHandlerInterface)
        {
            this._handler += eventHandlerInterface.EventBusHandler;
        }

        public void RemoveHandler(IEventHanler<MEventBusArgs> eventHandlerInterface)
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

        public void NotifyObeserver(MEventBusArgs e)
        {
            if (this._handler != null && _isNotify)
            {
                this._handler(this, e);
            }
        }
    }

    public class MEventBusArgs : EventArgs
    {
        public readonly string type;
        public MEventBusArgs(string type_)
        {
            this.type = type_;
        }
    }
}
