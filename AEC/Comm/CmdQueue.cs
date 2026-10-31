using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AEC.Comm
{
    class CmdQueue
    {
        private Queue<CommCmd> _cmds = new Queue<CommCmd>();

        private EventWaitHandle sendCmdEvent = null;

        public CmdQueue(EventWaitHandle eventWaitHandle)
        {
            this.sendCmdEvent = eventWaitHandle;
        }  

        public bool isEmpty
        {
            get
            {
                return count == 0;
            }
        }

        public bool isNotEmpty
        {
            get
            {
                return count > 0;
            }
        }

        public bool IsContainNormalCmd()
        {
            lock(_cmds)
            {
                if(isEmpty)
                {
                    return false;
                }
                foreach(CommCmd cmd in _cmds)
                {
                    //Console.WriteLine("cmd type:{0}", cmd.type);
                    if(cmd.type == CmdType.Normal)
                    {
                        Tools.LogHelper.WriteInfo("The queue contains general commands.");
                        return true;
                    }
                }

                return false;
            }
        }

        public void Clear()
        {
            lock (_cmds)
            {
                _cmds.Clear();
            }
        }

        public void Push(CommCmd cmd)
        {
            lock (_cmds)
            {
                bool notFindId = true;
                CommCmd[] commCmds = this._cmds.ToArray();
                for (int i = 0; i < commCmds.Length; ++i)
                {
                    if (commCmds[i].identity == cmd.identity)
                    {
                        commCmds[i].cmd = cmd.cmd;
                        notFindId = false;
                    }
                }

                if (notFindId)
                {
                    //Log.OutputLine("添加到队列");
                    _cmds.Enqueue(cmd);
                }


                //if (this._cmds.Count <= 1)
                //{
                //    _cmds.Enqueue(cmd);
                //}
                //else
                //{
                //    bool notFindId = false;
                //    CommCmd[] commCmds = this._cmds.ToArray();
                //    for (int i = 1; i < commCmds.Length; ++i)
                //    {
                //        if (commCmds[i].identity == cmd.identity)
                //        {
                //            commCmds[i].cmd = cmd.cmd;
                //            notFindId = false;
                //        }
                //    }

                //    if (notFindId)
                //    {
                //        _cmds.Enqueue(cmd);
                //    }
                //}
            }
            //Console.WriteLine("queue count = {0}, {1}", this._cmds.Count, count);
            this.sendCmdEvent.Set();
        }

        public CommCmd Pop()
        {
            CommCmd first = null;

            lock (_cmds)
            {
                if (isNotEmpty)
                {
                    first = _cmds.Dequeue();
                }
            }

            return first;
        }

        public int count
        {
            get
            {
                int count = 0;
                lock (_cmds)
                {
                    count = this._cmds.Count;
                }

                return count;
            }
        }
    }
}
