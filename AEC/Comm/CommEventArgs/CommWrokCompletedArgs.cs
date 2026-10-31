using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Comm.CommEventArgs
{
    public class CommWrokCompletedArgs : MEventBusArgs
    {
        public const string CommWrokCompleted = "CommWrokCompleted";
        public readonly bool result;
        public CommWrokCompletedArgs(bool result_ = true)
            : base(CommWrokCompleted)
        {
            this.result = result_;
        }
    }
}
