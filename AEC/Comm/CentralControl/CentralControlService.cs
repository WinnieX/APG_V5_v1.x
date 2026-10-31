using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AEC.Comm.CentralControl
{
    public class CentralControlService
    {
        private System.ComponentModel.BackgroundWorker bgwSendCmd;     //用于联机,加载，上传，调用模式数据等通讯过程中

        private readonly List<byte> _recvBuffer = new List<byte>();

        private readonly Queue<CentralControlCmd> cmdQueue = new Queue<CentralControlCmd>();

        public bool isRunning
        {
            get
            {
                if(this.ccSerialPort != null)
                {
                    return this.ccSerialPort.IsOpen;
                }

                return false;
            }
        }

        private CentralControlSerialPortDevice ccSerialPort;

        private EventWaitHandle[] events = new EventWaitHandle[]
        {
            new AutoResetEvent(false),
            new AutoResetEvent(false),
        };

        private EventWaitHandle stopEvent
        {
            get
            {
                return events[0];
            }
        }

        private EventWaitHandle sendCmdEvent
        {
            get
            {
                return events[1];
            }
        }
    
        public CentralControlService()
        {
            this.bgwSendCmd = new System.ComponentModel.BackgroundWorker();
            this.bgwSendCmd.DoWork += bgwSendCmd_DoWork;

            this.ccSerialPort = new CentralControlSerialPortDevice(CentralControlProtocol.CheckResponseIsCompleted, this._recvBuffer);
        }

        private void bgwSendCmd_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool exitLoop = false;


            
            //Console.WriteLine("进入循环!");
            while (true)
            {
                //Console.WriteLine("wait event-0!");
                int result = WaitHandle.WaitAny(events);
                //Console.WriteLine("wait event-1!");
                switch (result)
                {
                    case 0:
                        exitLoop = true;
                        break;

                    case 1:
                        try
                        {
                            SendCmdFromQueue();
                        }
                        catch (Exception ex)
                        {
                            Tools.LogHelper.WriteDebug("SendCmdFromQueue.... Exception: ", ex);
                        }
                        break;

                    default:
                        exitLoop = true;
                        break;
                }

                if (exitLoop)
                {
                    break;
                }

                //AEC.Log.OutputLine("bgwSendCmd_DoWork 循环:{0}", ++bgwSendCmd_count);
            }
        }

        private void SendCmdFromQueue()
        {
            //Console.WriteLine("队列中命令数量:{0}", cmdQueue.count);
            if(cmdQueue.Count == 0)
            {
                return;
            }

            CentralControlCmd cmd = cmdQueue.Dequeue();
            if (cmd == null)
            {
                return;
            }

            byte[] cmdData = CentralControlProtocol.GetCommand(cmd);

            if(cmdData == null)
            {
                return;
            }

            if(!this.ccSerialPort.IsOpen)
            {
                return;
            }

            this.ccSerialPort.SyncSend(cmdData);

            if(CentralControlProtocol.CheckResponseIsOK(cmdData, this._recvBuffer.ToArray()))
            {
                Log.OutputLine("发送成功");
            }
            else
            {
                Log.OutputLine("发送失败！");
            }

            if(cmdQueue.Count > 0)
            {
                this.sendCmdEvent.Set();
            }
        }

        public bool StartService(string portName)
        {
            if (!this.bgwSendCmd.IsBusy)
            {
                this.bgwSendCmd.RunWorkerAsync();
            }

            this.ccSerialPort.portName = portName;

            return this.ccSerialPort.Open();
        }

        public void StopService()
        {
            this.cmdQueue.Clear();

            this.ccSerialPort.CancelWait();

            this.stopEvent.Set();

            Thread.Sleep(32);

            this.ccSerialPort.Close();
        }

        public void SetMusicVolume(byte data)
        {
            CentralControlCmd cmd = new CentralControlCmd(CentralControlProtocol.Commmand.SetMusicVolume, data);
            AddCmdToQueue(cmd);
        }

        public void SetMicVolume(byte data)
        {
            CentralControlCmd cmd = new CentralControlCmd(CentralControlProtocol.Commmand.SetMicVolume, data);
            AddCmdToQueue(cmd);
        }

        public void SetEffectVolume(byte data)
        {
            CentralControlCmd cmd = new CentralControlCmd(CentralControlProtocol.Commmand.SetEffectVolume, data);
            AddCmdToQueue(cmd);
        }

        public void RecallPresetMode(byte data)
        {
            CentralControlCmd cmd = new CentralControlCmd(CentralControlProtocol.Commmand.RecallPreset, data);
            AddCmdToQueue(cmd);
        }

        public void SetMusicInputPort(byte data)
        {
            CentralControlCmd cmd = new CentralControlCmd(CentralControlProtocol.Commmand.SetMusicInputPort, data);
            AddCmdToQueue(cmd);
        }

        private void AddCmdToQueue(CentralControlCmd cmd)
        {
            if (!isRunning)
            {
                return;
            }

            this.cmdQueue.Enqueue(cmd);
            this.sendCmdEvent.Set();
        }
    }
}
