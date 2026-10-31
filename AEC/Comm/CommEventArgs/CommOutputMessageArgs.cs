using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Comm.CommEventArgs
{
    public class CommOutputMessageArgs : MEventBusArgs
    {
        public const string CommOutputMessage = "CommOutputMessage";
        public readonly string message;

        public CommOutputMessageArgs(string message_)
            : base(CommOutputMessage)
        {
            this.message = message_;
        }

        public static CommOutputMessageArgs Create(string message, bool lineFeed = true)
        {
            if (lineFeed)
            {
                message = System.Environment.NewLine + message;
            }

            return new CommOutputMessageArgs(message);
        }
    }
}
