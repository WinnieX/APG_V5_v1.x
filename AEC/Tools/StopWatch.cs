using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Tools
{
    class StopWatch
    {
        private Stopwatch stopWatch = new Stopwatch();
        private int _elapsed = 0;
        private string _process = string.Empty;

        public StopWatch(string process = "")
        {
            this._process = process;
        }

        public void Start()
        {
            stopWatch.Start();
        }

        public void Stop()
        {
            stopWatch.Stop();
            _elapsed = (int)stopWatch.ElapsedMilliseconds;
            Output(this._process, this._elapsed);
            stopWatch.Reset();
        }

        public void Output(string process, int elapsed)
        {
            Console.WriteLine(process + "耗时:" + elapsed.ToString() + "ms");
        }
    }
}
