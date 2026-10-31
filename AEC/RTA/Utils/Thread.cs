using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AEC.RTA.Utils
{
    public abstract class ThreadBase
    {
        private bool run_flag_ = false;
        private Thread thread_ = null;
        private int wait_time_ = 0;

        protected AutoResetEvent run_event_ = null;
        private AutoResetEvent stop_event_ = null;
        private AutoResetEvent exit_event_ = null;

        public ManualResetEvent idleEvent = new ManualResetEvent(true);

        private readonly int Def_Wait_Time = 0;

        protected ThreadBase()
        {
            wait_time_ = Def_Wait_Time;
            run_event_ = new AutoResetEvent(false);
            stop_event_ = new AutoResetEvent(false);
            exit_event_ = new AutoResetEvent(false);
        }

        public void Start()
        {
            if (this.run_flag_)
            {
                this.Stop();
            }

            this.run_event_.Reset();
            this.stop_event_.Reset();
            this.exit_event_.Reset();

            this.thread_ = new Thread(new ThreadStart(ThreadProc));
            this.thread_.Start();
            this.run_flag_ = true;
        }

        public void Stop()
        {
            if (this.run_flag_)
            {
                this.run_event_.Reset();
                this.stop_event_.Set();
                //Console.WriteLine("exit thread (0)*********");
                this.exit_event_.WaitOne();
                //Console.WriteLine("exit thread (1)*********");
                this.stop_event_.Reset();
                this.run_flag_ = false;
                this.thread_ = null;
            }
        }

        public void SuspendRun()
        {
            if (this.run_flag_)
            {
                this.run_event_.Reset();
            }
        }

        public void ExecuteOnce()
        {
            if (this.run_flag_)
            {
                this.run_event_.Set();
            }
        }

        public bool isRunning
        {
            get
            {
                return this.run_flag_;
            }
        }

        private void Loop()
        {
            idleEvent.Reset();
            Run();
            idleEvent.Set();
        }

        protected virtual void Run() { }
        protected virtual void Entrance() { }
        protected virtual void Exit() { }

        public int wait_time
        {
            set
            {
                this.wait_time_ = value;
            }

            get
            {
                return this.wait_time_;
            }
        }

        private void ThreadProc()
        {
            this.Entrance();

            //Console.WriteLine("thread enter!");
            EventWaitHandle[] events = new EventWaitHandle[2];
            events[0] = this.stop_event_;
            events[1] = this.run_event_;

            bool exitLoop = false;

            while (true)
            {
                //Console.WriteLine("wait event-0!");
                int value = WaitHandle.WaitAny(events);
                //Console.WriteLine("wait event-1!");
                switch (value)
                {
                    case 0:
                        exitLoop = true;
                        break;

                    case 1:
                        //this.Run();
                        this.Loop();
                        break;
                }

                if (exitLoop)
                {
                    break;
                }

                Thread.Sleep(this.wait_time_);
            }

            this.Exit();
            this.exit_event_.Set();
        }
    }
}
