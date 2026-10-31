using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace AEC.RemoteControl
{
    public class RCConfig
    {
        public const int Kilo = 1000;
        public const string defServerIP = "119.23.126.107";
        //public const string defServerIP = "127.0.0.1";
        public const int defServerPort = 7677;

        public string serverIP
        {
            get;
            set;
        }

        public int serverPort
        {
            get;
            set;
        }
        //public IPAddress serverIP = IPAddress.Parse("14.215.158.102");
        //public int serverPort = 80;
        public int heartbeatInterval = 5;

        public RCConfig()
        {
            this.serverIP = defServerIP;
            this.serverPort = defServerPort;
        }

        public string ServerInfo
        {
            get
            {
                return string.Format(" {0} : {1} ", this.serverIP, this.serverPort);
            }
        }

        public void RestoreToDefault()
        {
            this.serverIP = defServerIP;
            this.serverPort = defServerPort;
        }
    }
}
