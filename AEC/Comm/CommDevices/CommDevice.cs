using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AEC.Comm.CommDevices
{
    public abstract class CommDevice
    {
        private const int DefWaitTime = 1024;
        public readonly CommProtocolDetector detector;
        private readonly List<byte> recvBuffer;
        private AutoResetEvent responseEvent = new AutoResetEvent(false);
        private CommCmdType currCmdType = CommCmdType.Read;

        protected static readonly int writeTimeout = CommServiceBase.MCommWaitTimeUnit * 1;

        public virtual string connectionInfo
        {
            get
            {
                return string.Empty;
            }
        }
        
        public CommDevice(CommProtocolDetector detector_, List<byte> recvBuffer_)
        {
            System.Diagnostics.Debug.Assert(detector_ != null);

            this.detector = detector_;
            this.recvBuffer = recvBuffer_;
        }

        #region 方法
        public abstract bool Open();
        public abstract void Close();
        protected abstract void Send(byte[] data);
        #endregion

        private void ClearRecvBuffer()
        {
            lock(this.recvBuffer)
            {
                recvBuffer.Clear();
            }
        }

        public void AppendRecvData(byte[] data)
        {
            List<byte> buffer = null;

            lock (this.recvBuffer)
            {
                recvBuffer.AddRange(data);
                buffer = new List<byte>(recvBuffer);
            }

            System.Diagnostics.Debug.Assert(buffer != null);

            if (currCmdType == CommCmdType.Read)       //如果是读取命令
            {
                if (this.detector.checkReadResponseIsCompleted(buffer))
                {
                    responseEvent.Set();
                }
            }
            else if (currCmdType == CommCmdType.Write)       //如果是写入命令
            {
                if (this.detector.checkWriteResponseIsCompleted(buffer))
                {
                    responseEvent.Set();
                }
            }
        }

        public void SyncSend(byte[] data, int timeout = DefWaitTime)
        {
            responseEvent.Reset();

            currCmdType = this.detector.checkCommCmdType(data);

            ClearRecvBuffer();

            Send(data);

            if (timeout > 0)
            {
                responseEvent.WaitOne(timeout);
            }
        }
    }
}
