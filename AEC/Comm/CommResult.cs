using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm
{
    public class CommResult
    {
        public readonly CommProcess process;
        public readonly bool success;

        public CommResult(CommProcess process_, bool success_)
        {
            this.process = process_;
            this.success = success_;
        }
    }
}
