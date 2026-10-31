using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm
{
    internal class CommArge
    {
        public readonly CommProcess process;
        public readonly object state;

        public CommArge(CommProcess process_)
        {
            this.process = process_;
        }

        public CommArge(CommProcess process_, object state_)
        {
            this.process = process_;
            this.state = state_;
        }
    }
}
