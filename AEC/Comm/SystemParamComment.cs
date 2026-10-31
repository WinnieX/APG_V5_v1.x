using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm
{
    public class SystemParamComment
    {
        public SystemCommProtocol.SystemParamAddr param;
        public string comment;


        public SystemParamComment(SystemCommProtocol.SystemParamAddr param_, string comment_)
        {
            this.param = param_;
            this.comment = comment_;
        }
    }

    public class SystemParamCommentValue : SystemParamComment
    {
        public short value;

        public SystemParamCommentValue(SystemCommProtocol.SystemParamAddr param_, string comment_, short value_) :
            base(param_, comment_)
        {
            this.value = value_;
        }
    }
}
