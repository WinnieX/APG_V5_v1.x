using System;
using System.Collections.Generic;
using System.Text;
using AEC.Comm;

namespace AEC.Common
{
    public class MEffector
    {
        public readonly EffectorModelBase effectorModel;
        public readonly AbstractCommService commService;
        //public readonly DataBackup dataBack;

        public MEffector(EffectorModelBase effectorModel_, AbstractCommService commService_)
        {
            this.effectorModel = effectorModel_;
            this.commService = commService_;
        }
    }
}
