using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Common
{
    public class ExtendedFunctionState
    {
        public bool isEnableCustomModel;

        public ExtendedFunctionState()
        {
            this.isEnableCustomModel = false;
        }

        public void CopyFrom(ExtendedFunctionState other)
        {
            this.isEnableCustomModel = other.isEnableCustomModel;
        }
    }
}
