using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AEC.Core;
using AEC.Misc;

namespace AEC.UI.Forms
{
    public partial class FormRS232ToWifiConverter : AEC.UI.MFormBase
    {
        private const int BaudRate_115200 = 115200;
        private const int BaudRate_57600 = 57600;

        private DialogResult dr = DialogResult.Cancel;

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private System.Windows.Forms.Timer scanTimer;
        private int timerCount { get; set; }
        //private List<IPAddress> ipList = new List<IPAddress>();
        private readonly Dictionary<IPAddress, RS232ToWifiConverter> ipToScannerMap = new Dictionary<IPAddress, RS232ToWifiConverter>();     //{扫描器ip, 扫描器}
        private readonly Dictionary<IPAddress, IPAddress> onlineIpMap = new Dictionary<IPAddress, IPAddress>();     //{扫描器ip, 设备ip}
        private readonly Dictionary<IPAddress, ConverterDeviceInfo> ipToDeviceInfoMap = new Dictionary<IPAddress, ConverterDeviceInfo>(); //{设备ip, 设备信息}

        private readonly Dictionary<string, IPAddress> ipMap = new Dictionary<string, IPAddress>(); //用于comboBox的{设备ip, 设备ip}
        private string _lastIp = string.Empty;

        private ConverterDeviceInfo currDeviceBasicInfo{get;set;}

        private readonly MEventBus eventBus = new MEventBus();

        private string lastIp
        {
            get
            {
                return this._lastIp;
            }
            set
            {
                if(this._lastIp != value)
                {
                    this._lastIp = value;

                    if (this.ipMap.ContainsKey(this._lastIp) && this.ipToDeviceInfoMap.ContainsKey(this.ipMap[this._lastIp]))
                    {
                        this.UpdateDeviceBasieInfo(this.ipToDeviceInfoMap[this.ipMap[this._lastIp]]);
                    }
                    else
                    {
                        this.UpdateDeviceBasieInfo(null);
                    }
                }
            }
        }

        public FormRS232ToWifiConverter()
        {
            InitializeComponent();
        }

        protected override void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formRS232ToWifiConverter];
            this.lblDeviceIP.Text = localization[Localization.formRS232ToWifiConverter_DeviceIp];
            this.linkRefreshPort.Text = localization[Localization.formRS232ToWifiConverter_RefreshDevice];
            this.lblDeviceInfo.Text = localization[Localization.formRS232ToWifiConverter_DeviceInfo];
            this.lblBautRate.Text = localization[Localization.formRS232ToWifiConverter_BaudRate];
            this.btnUpdateConfig.Text = localization[Localization.formRS232ToWifiConverter_Update];
            //this.lblCommRetransmission.Text = localization[Localization.formCommSettings_CommRetransmission];

            //this.lblDefaultSerialPort.Text = localization[Localization.formCommSettings_DefaultSerialPort];

            //this.mSwitchSerialPortManualMode.Text = localization[Localization.formCommSettings_SerialPortManualMode];

            //this.lblIPAddress.Text = localization[Localization.formCommSettings_IPAddress];
            //this.lblPort.Text = localization[Localization.formCommSettings_Port];

            //this.linkHelp.Text = localization[Localization.formCommSettings_Help];

            //this.btnRestoreToDefault.Text = localization[Localization.formCommSettings_RestoreToDefault];
            //this.btnClose.Text = localization[Localization.formCommSettings_Close];
        }

        public new DialogResult ShowDialog()
        {
            base.ShowDialog();

            return dr;
        }

        protected override void OnLoadHandler()
        {
            base.OnLoadHandler();
            this.InitUI();
            this.InitEventBus();
            this.ScanDevices();
            this.StartTimer();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if(this.currDeviceBasicInfo != null)
            {
                IPAddress deviceIp = this.currDeviceBasicInfo.deviceIp;
                bool udpIsDifferent = false;
                bool tcpIsDifferent = false;
                int udpPort = 0;
                int tcpPort = 0;
                bool supportUdp = false;
                bool supportTcp = false;
                
                foreach (string name in this.currDeviceBasicInfo.socketMap.Keys)
                {
                    ConverterDeviceInfo.Socket socket = this.currDeviceBasicInfo.socketMap[name];
                    if (socket.protocol == ConverterDeviceInfo.Socket.PROTOCOL_UDP_SERVER)
                    {
                        udpIsDifferent = deviceIp.ToString() != config.udp_ip || socket.localPort != (int)config.udp_port;
                        udpPort = socket.localPort;
                        supportUdp = true;
                    }
                    else if (socket.protocol == ConverterDeviceInfo.Socket.PROTOCOL_TCP_SERVER)
                    {
                        tcpIsDifferent = deviceIp.ToString() != config.tcp_ip || socket.localPort != (int)config.tcp_port;
                        tcpPort = socket.localPort;
                        supportTcp = true;
                    }
                }

                if(udpIsDifferent || tcpIsDifferent)
                {
                    if (DialogResult.OK == gtMessageBox.ShowMessage(this, localization[Localization.formRS232ToWifiConverter_SocketNotMatch], buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Question))
                    {
                        if(supportUdp)
                        {
                            config.udp_ip = deviceIp.ToString();
                            config.udp_port = (ushort)udpPort;
                        }

                        if(supportTcp)
                        {
                            config.tcp_ip = deviceIp.ToString();
                            config.tcp_port = (ushort)tcpPort;
                        }

                        if(!config.connectionViaUDP && !config.connectionViaTCP)
                        {
                            if(supportUdp)
                            {
                                config.connectionViaUDP = true;
                            }
                            else if(supportTcp)
                            {
                                config.connectionViaTCP = true;
                            }
                        }
                        else if (config.connectionViaUDP && !config.connectionViaTCP)
                        {
                            if (!supportUdp && supportTcp)
                            {
                                config.connectionViaUDP = false;
                                config.connectionViaTCP = true;
                            }
                        }
                        else if (!config.connectionViaUDP && config.connectionViaTCP)
                        {
                            if (supportUdp && !supportTcp)
                            {
                                config.connectionViaUDP = true;
                                config.connectionViaTCP = false;
                            }
                        }

                        dr = System.Windows.Forms.DialogResult.OK;
                    }
                }                
            }
        }

        private void InitUI()
        {
            int firstBaudRate = AEC.Comm.CommDevices.SerialPort.defaultBaudRate;
            int secondaryBaudRate = firstBaudRate == BaudRate_115200 ? BaudRate_57600 : BaudRate_115200;

            string[] baudRates = new string[] { firstBaudRate.ToString(), secondaryBaudRate.ToString()};

            this.cmbBaudRate.Items.AddRange(baudRates);

            this.cmbBaudRate.SelectedIndex = 0;
        }

        protected override void OnClosedHandler()
        {
            base.OnClosedHandler();
            this.DeinitEventBus();
            this.StopTimer();
            this.CloseAllDevices();
        }

        private void CloseAllDevices()
        {
            foreach(var ip in this.ipToScannerMap.Keys)
            {
                this.ipToScannerMap[ip].Close();
            }
        }

        private void InitEventBus()
        {
            this.eventBus.RegisterHandler(EventBusHandler);
        }

        private void DeinitEventBus()
        {
            this.eventBus.RemoveHandler(EventBusHandler);
        }


        public void EventBusHandler(object sender, MEventBusArgs e)
        {
            base.syncContext.Post(EventBusUIHandler, e);
        }

        private void EventBusUIHandler(object state)
        {
            MEventBusArgs e = state as MEventBusArgs;
            if(e == null)
            {
                return;
            }

            switch(e.type)
            {
                case DeviceConfigLoadedArgs.DeviceConfigLoaded:
                    DeviceConfigLoadedArgs dcla = e as DeviceConfigLoadedArgs;
                    if(dcla != null)
                    {
                        this.DeviceConfigLoadedHandler(dcla.deviceInfo);
                    }
                    break;

                case DeviceCheckOnlineArgs.DeviceCheckOnline:
                    DeviceCheckOnlineArgs dcoa = e as DeviceCheckOnlineArgs;
                    if (dcoa != null)
                    {
                        this.DeviceCheckOnlineHandler(dcoa.deviceIp, dcoa.isOnline);
                    }
                    break;

                case DeviceConfigUpdatedArgs.DeviceConfigUpdated:
                    DeviceConfigUpdatedArgs dcua = e as DeviceConfigUpdatedArgs;
                    if (dcua != null)
                    {
                        this.DeviceConfigUpdatedHandler(dcua.deviceIp, dcua.success);
                    }
                    break;

                case DeviceBusyArgs.DeviceBusy:
                    DeviceBusyArgs dba = e as DeviceBusyArgs;
                    if(dba != null)
                    {
                        this.DeviceBusyHandler(dba.deviceIp, dba.busy);
                    }
                    break;
            }
        }

        private void DeviceConfigLoadedHandler(ConverterDeviceInfo deviceInfo)
        {
            Debug.Assert(deviceInfo != null);

            AEC.Tools.LogHelper.WriteInfo("设备 {0} 配置加载完成", deviceInfo.deviceIp);

            onlineIpMap[deviceInfo.localIp] = deviceInfo.deviceIp;

            ipToDeviceInfoMap[deviceInfo.deviceIp] = deviceInfo;

            ipMap[deviceInfo.deviceIp.ToString()] = deviceInfo.deviceIp;

            this.UpdateCmbDeviceIP();
        }

        private void DeviceCheckOnlineHandler(IPAddress deviceIp, bool isOnline)
        {
            Debug.Assert(deviceIp != null);

            AEC.Tools.LogHelper.WriteInfo("设备IP:{0} 是否在线?{1}", deviceIp, isOnline ? "是" : "否");

            if (!isOnline)
            {
                //this.scanTimer.Stop();
                if (ipToDeviceInfoMap.ContainsKey(deviceIp))
                {
                    IPAddress scanerIp = ipToDeviceInfoMap[deviceIp].localIp;

                    if (onlineIpMap.ContainsKey(scanerIp))
                    {
                        onlineIpMap.Remove(scanerIp);
                    }

                    ipToDeviceInfoMap.Remove(deviceIp);

                    if (this.ipMap.ContainsKey(deviceIp.ToString()))
                    {
                        this.ipMap.Remove(deviceIp.ToString());
                    }
                }

                this.UpdateCmbDeviceIP();
            }
            else
            {
                if (this.currDeviceBasicInfo != null && this.currDeviceBasicInfo.deviceIp.Equals(deviceIp))
                {
                    this.btnUpdateConfig.Enabled = true;
                }
            }
        }

        private void DeviceConfigUpdatedHandler(IPAddress deviceIp, bool success)
        {
            Debug.Assert(deviceIp != null);

            AEC.Tools.LogHelper.WriteInfo("设备IP:{0} 配置已经更新，结果 ？{1}", deviceIp, success ? "Success" : "Failed");

            this.RefreshDeviceInfo();
            //if (ipToDeviceInfoMap.ContainsKey(deviceIp))
            //{
            //    IPAddress scanerIp = ipToDeviceInfoMap[deviceIp].localIp;

            //    if (onlineIpMap.ContainsKey(scanerIp))
            //    {
            //        onlineIpMap.Remove(scanerIp);
            //    }

            //    ipToDeviceInfoMap.Remove(deviceIp);

            //    if (this.ipMap.ContainsKey(deviceIp.ToString()))
            //    {
            //        this.ipMap.Remove(deviceIp.ToString());
            //    }
            //}

            //this.UpdateCmbDeviceIP();
        }

        private void DeviceBusyHandler(IPAddress deviceIp, bool busy)
        {
            AEC.Tools.LogHelper.WriteInfo("设备IP:{0} 是否忙碌:{1}", deviceIp, busy ? "Yes" : "No");
            if(busy && this.currDeviceBasicInfo != null && this.currDeviceBasicInfo.deviceIp == deviceIp)
            {
                this.btnUpdateConfig.Enabled = false;
            }
        }

        private void linkRefreshPort_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.RefreshDeviceInfo();
        }

        private void RefreshDeviceInfo()
        {
            this.onlineIpMap.Clear();
            this.ipToDeviceInfoMap.Clear();
            this.ipMap.Clear();
            this.UpdateCmbDeviceIP();
            this.ScanDevices();
            this.timerCount = 0;
        }

        private void UpdateCmbDeviceIP()
        {
            //lock (this.tempIpToDeviceInfoMap)
            //{
            //    this.ipToDeviceInfoMap.Clear();

            //    foreach(var ip in this.tempIpToDeviceInfoMap.Keys)
            //    {
            //        this.ipToDeviceInfoMap[ip] = this.tempIpToDeviceInfoMap[ip];
            //    }

            //    this.tempIpToDeviceInfoMap.Clear();
            //}

            //List<string> ipToRemove = new List<string>();

            //foreach(var key in this.ipMap.Keys)
            //{
            //    IPAddress ipAddr = this.ipMap[key];

            //    if (!this.ipToDeviceInfoMap.ContainsKey(ipAddr))
            //    {
            //        ipToRemove.Add(key);
            //    }
            //}

            //foreach(var ip in ipToRemove)
            //{
            //    if(ipMap.ContainsKey(ip))
            //    {
            //        ipMap.Remove(ip);
            //    }
            //}

            //foreach (var ip in this.ipToDeviceInfoMap.Keys)
            //{
            //    ipMap[ip.ToString()] = ip;
            //}


            this.cmbDeviceIP.Items.Clear();

            //this.ipBindingSource.Clear();
            //this.ipBindingSource.DataSource = this.ipMap;
            //this.cmbDeviceIP.DataSource = this.ipBindingSource;
 
            if (ipMap.Count > 0)
            {
                //this.cmbDeviceIP.DisplayMember = "Key";
                //this.cmbDeviceIP.ValueMember = "Value";
                foreach(var ip in this.ipMap.Keys)
                {
                    this.cmbDeviceIP.Items.Add(ip);
                }

                if(string.IsNullOrEmpty(this.lastIp) || !this.ipMap.ContainsKey(this.lastIp))
                {
                    this.cmbDeviceIP.SelectedIndex = 0;
                    this.lastIp = this.cmbDeviceIP.Items[0].ToString();
                }
                else if(this.ipMap.ContainsKey(this.lastIp))
                {
                    this.cmbDeviceIP.SelectedItem = this.lastIp;
                }
            }
            else
            {
                this.lastIp = string.Empty;
            }
        }

        private void ScanDevices()
        {
            //this.UpdateCmbDevice();

            List<IPAddress> ipAddressList = GetLocalIPAddress();

            List<IPAddress> ipToRemove = new List<IPAddress>();
            foreach (var ip in ipToScannerMap.Keys)
            {
                if (!ipAddressList.Contains(ip)) //认为此ip网卡已经移除
                {
                    ipToRemove.Add(ip);
                }
            }

            bool isUpdateDeviceComboBox = false;

            foreach (var ip in ipToRemove)
            {
                if(this.onlineIpMap.ContainsKey(ip))
                {
                    if (this.ipToDeviceInfoMap.ContainsKey(this.onlineIpMap[ip]))
                    {
                        this.ipToDeviceInfoMap.Remove(this.onlineIpMap[ip]);
                    }

                    if (this.ipMap.ContainsKey(this.onlineIpMap[ip].ToString()))
                    {
                        this.ipMap.Remove(this.onlineIpMap[ip].ToString());
                        AEC.Tools.LogHelper.WriteInfo("设备:{0}已经被移除", this.onlineIpMap[ip].ToString());
                        isUpdateDeviceComboBox = true;
                    }

                    this.onlineIpMap.Remove(ip);
                }
                
                if(ipToScannerMap.ContainsKey(ip))
                {
                    ipToScannerMap[ip].Close();
                    ipToScannerMap.Remove(ip);
                }
            }

            if (isUpdateDeviceComboBox)
            {
                AEC.Tools.LogHelper.WriteInfo("comboBox需要更新");
                this.UpdateCmbDeviceIP();
            }

            foreach (var ip in ipAddressList)
            {
                if (!ipToScannerMap.ContainsKey(ip))
                {
                    RS232ToWifiConverter scanner = new RS232ToWifiConverter(ip, this.eventBus);
                    if(scanner.InitClient())
                    {
                        ipToScannerMap[ip] = scanner;
                    }
                }
            }

            foreach (var ip in ipToScannerMap.Keys)
            {
                if(this.onlineIpMap.ContainsKey(ip) && this.ipToDeviceInfoMap.ContainsKey(this.onlineIpMap[ip]))
                {
                    ipToScannerMap[ip].AsyncCheckOnline(this.onlineIpMap[ip]);

                    AEC.Tools.LogHelper.WriteInfo("扫描器ip {0} 检测是否在线", ip.ToString());
                }
                else
                {
                    ipToScannerMap[ip].AsyncLoadConfigData();

                    AEC.Tools.LogHelper.WriteInfo("扫描器ip {0} 异步加载配置数据", ip.ToString());
                }
            }
        }

        private void scanTimer_Tick(object sender, EventArgs e)
        {
            //return;
            //this.scanTimer.Stop();
            if (++this.timerCount >= 0x08)
            {
                this.timerCount = 0;

                this.ScanDevices();
            }
            
        }

        private void StartTimer()
        {
            this.scanTimer = new System.Windows.Forms.Timer();
            this.scanTimer.Interval = 256;
            this.scanTimer.Tick += scanTimer_Tick;
            this.scanTimer.Start();
        }

        private void StopTimer()
        {
            if(this.scanTimer != null)
            {
                this.scanTimer.Stop();
            }
        }

        private void UpdateDeviceBasieInfo(ConverterDeviceInfo deviceBasicInfo)
        {
            if(deviceBasicInfo != null)
            {
                this.txtDeviceBasicInfo.Text = deviceBasicInfo.ToString();
                this.txtDeviceBasicInfo.SelectionStart = this.txtDeviceBasicInfo.TextLength;

                if(deviceBasicInfo.baudRate == AEC.Comm.CommDevices.SerialPort.defaultBaudRate)
                {
                    this.linkLabelTips.LinkColor = Color.FromArgb(7, 193, 96);
                    this.linkLabelTips.ActiveLinkColor = Color.FromArgb(6, 175, 87);
                    this.linkLabelTips.Text = localization[Localization.formRS232ToWifiConverter_RestoreConfigToDefault];
                }
                else
                {
                    this.linkLabelTips.LinkColor = Color.FromArgb(255, 0, 0);
                    this.linkLabelTips.ActiveLinkColor = Color.FromArgb(224, 36, 36);
                    this.linkLabelTips.Text = localization[Localization.formRS232ToWifiConverter_BaudRateNotMatch];
                }
            }
            else
            {
                this.txtDeviceBasicInfo.Clear();
            }

            this.btnUpdateConfig.Enabled = deviceBasicInfo != null;

            this.linkLabelTips.Visible = deviceBasicInfo != null;

            this.currDeviceBasicInfo = deviceBasicInfo;
        }

        public static List<IPAddress> GetLocalIPAddress()
        {
            //return new List<IPAddress>()
            //{
            //    new System.Net.IPAddress(new byte[] { 10, 10, 100, 150 })
            //};
            List<IPAddress> localIPAddressList = new List<IPAddress>();

            // 获取本地计算机上的所有网络接口
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                // 过滤掉非活动的接口和回环接口
                if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    // 获取网络接口上的IP属性
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                    // 获取该接口上的所有IP地址
                    foreach (UnicastIPAddressInformation ipAddressInfo in ipProperties.UnicastAddresses)
                    {
                        // 过滤掉IPv6地址和非本地链接地址
                        if (ipAddressInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !IPAddress.IsLoopback(ipAddressInfo.Address))
                        {
                            //Console.WriteLine("IP地址: " + ipAddressInfo.Address.ToString());
                            localIPAddressList.Add(ipAddressInfo.Address);
                        }
                    }
                }
            }

            return localIPAddressList;
        }

        private void btnUpdateConfig_Click(object sender, EventArgs e)
        {
            this.UpdateSelectedConfig();
        }

        private int GetSelectedBuadRate()
        {
            string text = this.cmbBaudRate.Text.Trim();
            int baudRate = 0;
            int.TryParse(text, out baudRate);

            return baudRate;
        }

        

        private void UpdateSelectedConfig()
        {
            if(this.currDeviceBasicInfo == null || !ipToScannerMap.ContainsKey(this.currDeviceBasicInfo.localIp))
            {
                return;
            }

            int baudRate = GetSelectedBuadRate();

            this.UpdateConfig(baudRate);
            //if(baudRate != BaudRate_115200 && baudRate != BaudRate_57600)
            //{
            //    return;
            //}

            //byte[] configData = GetConfigData(baudRate);
            
            //RS232ToWifiConverter rs232ToWifiConverter = ipToScannerMap[this.currDeviceBasicInfo.localIp];

            //Debug.Assert(rs232ToWifiConverter != null);

            //rs232ToWifiConverter.AsyncUpdateConfig(this.currDeviceBasicInfo.deviceIp, configData);
        }

        private void UpdateConfig(int baudRate)
        {
            //if (this.currDeviceBasicInfo == null || !ipToScannerMap.ContainsKey(this.currDeviceBasicInfo.localIp))
            //{
            //    return;
            //}

            //int baudRate = GetSelectedBuadRate();
            if (baudRate != BaudRate_115200 && baudRate != BaudRate_57600)
            {
                return;
            }

            byte[] configData = GetConfigData(baudRate);

            RS232ToWifiConverter rs232ToWifiConverter = ipToScannerMap[this.currDeviceBasicInfo.localIp];

            Debug.Assert(rs232ToWifiConverter != null);

            rs232ToWifiConverter.AsyncUpdateConfig(this.currDeviceBasicInfo.deviceIp, configData);

            this.timerCount = 0;
        }

        private static byte[] GetConfigData(int baudRate)
        {
            switch(baudRate)
            {
                case BaudRate_115200:
                    return AEC.Properties.Resources.M_DSP_115200;

                case BaudRate_57600:
                    return AEC.Properties.Resources.M_DSP_57600;

                default:
                    return null;
            }            
        }

        private void lblBautRate_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.cmbBaudRate.Enabled = !this.cmbBaudRate.Enabled;
        }

        private void linkLabelTips_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.UpdateConfig(AEC.Comm.CommDevices.SerialPort.defaultBaudRate);
        }

        //private void cmbDeviceIP_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if(this.cmbDeviceIP.SelectedValue != null)
        //    {
        //        Log.OutputLine(this.cmbDeviceIP.SelectedValue.ToString());
        //    }
        //    else
        //    {
        //        Log.OutputLine("selectedvalue为空");
        //    }
        //}
    }

    //public class IPAddressComboxItem
    //{
    //    public string ip { get; }
    //    public IPAddress ipAddress { get; }
 
    //    public IPAddressComboxItem(string ip, IPAddress ipAddress)
    //    {
    //        this.ip = ip;
    //        this.ipAddress = ipAddress;
    //    }
    //}
    

    public class ConverterDeviceInfo
    {
        private static Localization localization = Config.GetInstance().localization;

        public class Socket
        {
            public const string PROTOCOL_TCP_SERVER = "TCP-SERVER";
            public const string PROTOCOL_UDP_SERVER = "UDP-SERVER";

            public readonly string protocol;
            public readonly int localPort;

            public string protocolType
            {
                get
                {
                    if (protocol != string.Empty)
                    {
                        return protocol;
                    }
                    else
                    {
                        return localization[Localization.formRS232ToWifiConverter_DeviceSocketProtocolUnknown];
                    }
                }
            }

            public Socket(string protocol_, int port_)
            {
                this.protocol = protocol_;
                this.localPort = port_;
            }
        }

        public readonly IPAddress localIp;
        public IPAddress deviceIp { get; set; }
        public string type { get; set; }
        public string version { get; set; }
        public string mac { get; set; }

        public readonly Dictionary<string, Socket> socketMap = new Dictionary<string, Socket>();
        //public string sockName { get; set; }
        //public string protocol { get; set; }
        //public int localPort { get; set; }

        public int baudRate { get; set; }
        public int dataBits { get; set; }
        public int stopBits { get; set; }
        public System.IO.Ports.Parity parity { get; set; }

        public string userName { get; set; }
        public string password { get; set; }
        public string WiFi_AP_SSID { get; set; }
        public string lanIpAddress { get; set; }

        public ConverterDeviceInfo(IPAddress localIp_)
        {
            this.localIp = localIp_;
        }

        public void Reset()
        {
            this.deviceIp = null;
            this.type = string.Empty;
            this.version = string.Empty;
            this.mac = string.Empty;

            this.socketMap.Clear();
            //this.protocol = string.Empty;
            //this.localPort = 0;

            this.baudRate = 0;
            this.dataBits = 0;
            this.stopBits = 0;
            this.parity = System.IO.Ports.Parity.None;

            this.userName = string.Empty;
            this.password = string.Empty;
            this.WiFi_AP_SSID = string.Empty;
            this.lanIpAddress = string.Empty;
        }

        public string parityType
        {
            get
            {
                switch(this.parity)
                {
                    case System.IO.Ports.Parity.None:
                        return localization[Localization.formRS232ToWifiConverter_DeviceUartParityNone];

                    case System.IO.Ports.Parity.Odd:
                        return localization[Localization.formRS232ToWifiConverter_DeviceUartParityOdd];

                    case System.IO.Ports.Parity.Even:
                        return localization[Localization.formRS232ToWifiConverter_DeviceUartParityEven];

                    default:
                        return localization[Localization.formRS232ToWifiConverter_DeviceUartParityOther];
                }                
            }
            set
            {
                switch (value)
                {
                    case "NONE":
                        this.parity = System.IO.Ports.Parity.None;
                        break;

                    case "ODD":
                        this.parity = System.IO.Ports.Parity.Odd;
                        break;

                    case "EVEN":
                        this.parity = System.IO.Ports.Parity.Even;
                        break;

                    default:
                        this.parity = System.IO.Ports.Parity.None;
                        break;
                } 
            }
        }

        public override string ToString()
        {
            //return base.ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} : {1}", localization[Localization.formRS232ToWifiConverter_DeviceModel], this.type);
            sb.AppendLine();
            sb.AppendFormat("{0} : {1}", localization[Localization.formRS232ToWifiConverter_DeviceFirmwareVersion], this.version);
            sb.AppendLine();
            sb.AppendFormat("{0} : {1}", localization[Localization.formRS232ToWifiConverter_DeviceIp], this.lanIpAddress);
            sb.AppendLine();
            sb.AppendFormat("{0} : {1}", localization[Localization.formRS232ToWifiConverter_DeviceMacAddress], this.mac);            
            foreach(var socketName in this.socketMap.Keys)
            {
                sb.AppendLine();
                //sb.AppendFormat("连接名称：{0}，协议类型：{1}，端口号：{2}", socketName, this.socketMap[socketName].protocolType, this.socketMap[socketName].localPort);
                sb.AppendFormat("{0} : {1}, {2} : {3}, {4} : {5}",
                    localization[Localization.formRS232ToWifiConverter_DeviceSocketName],
                    socketName,
                    localization[Localization.formRS232ToWifiConverter_DeviceSocketProtocol],
                    this.socketMap[socketName].protocolType,
                    localization[Localization.formRS232ToWifiConverter_DeviceSocketPort],
                    this.socketMap[socketName].localPort);
            }            
            sb.AppendLine();
            //sb.AppendFormat("波特率：{0}，数据位：{1}，停止位：{2}，校验位：{3}", this.baudRate, this.dataBits, this.stopBits, this.parityType);
            sb.AppendFormat("{0} : {1}, {2} : {3}, {4} : {5}, {6} : {7}",
                localization[Localization.formRS232ToWifiConverter_BaudRate],
                this.baudRate,
                localization[Localization.formRS232ToWifiConverter_DeviceUartDataBits],
                this.dataBits,
                localization[Localization.formRS232ToWifiConverter_DeviceUartStopBits],
                this.stopBits,
                localization[Localization.formRS232ToWifiConverter_DeviceUartParity],
                this.parityType);
            sb.AppendLine();
            //sb.AppendFormat("用户名：{0}，密码：{1}", this.userName, this.password);
            sb.AppendFormat("{0} : {1}, {2} : {3}",
                localization[Localization.formRS232ToWifiConverter_DeviceUserName],
                this.userName,
                localization[Localization.formRS232ToWifiConverter_DevicePassword],
                this.password);
            sb.AppendLine();
            sb.AppendFormat("WiFi AP SSID：{0}", this.WiFi_AP_SSID);
            

            return sb.ToString();
        }
    }

    public class DeviceConfigLoadedArgs : MEventBusArgs
    {
        public const string DeviceConfigLoaded = "DeviceConfigLoaded";

        public readonly ConverterDeviceInfo deviceInfo;
        public DeviceConfigLoadedArgs(ConverterDeviceInfo deviceInfo_) :
            base(DeviceConfigLoaded)
        {
            this.deviceInfo = deviceInfo_;
        }
    }

    public class DeviceCheckOnlineArgs : MEventBusArgs
    {
        public const string DeviceCheckOnline = "DeviceCheckOnline";

        public readonly IPAddress deviceIp;
        public readonly bool isOnline;
        public DeviceCheckOnlineArgs(IPAddress deviceIp_, bool isOnline_) :
            base(DeviceCheckOnline)
        {
            this.deviceIp = deviceIp_;
            this.isOnline = isOnline_;
        }
    }

    public class DeviceConfigUpdatedArgs : MEventBusArgs
    {
        public const string DeviceConfigUpdated = "DeviceConfigUpdated";

        public readonly IPAddress deviceIp;
        public readonly bool success;

        public DeviceConfigUpdatedArgs(IPAddress deviceIp_, bool success_) :
            base(DeviceConfigUpdated)
        {
            this.deviceIp = deviceIp_;
            this.success = success_;
        }
    }

    public class DeviceBusyArgs: MEventBusArgs
    {
        public const string DeviceBusy = "DeviceBusy";

        public readonly IPAddress deviceIp;
        public readonly bool busy;
        public DeviceBusyArgs(IPAddress deviceIp_, bool busy_) :
            base(DeviceBusy)
        {
            this.deviceIp = deviceIp_;
            this.busy = busy_;
        }
    }

    public class RS232ToWifiConverter
    {
        private AEC.Comm.Net.UDPClient udpClient = new Comm.Net.UDPClient();

        private const int TargetPort = 48899;

        //{"Agent":0,"Type":"EP_Tool","PasswordEn":0,"ServPort":0,"Version":"3.1.3f","ConnectMode":"Forbidden","Zone":"EPort","ServEn":1,"ServAddr":"0.0.0.0","ClientEn":0}
        private static readonly byte[] searchDeviceRequest = Convert.FromBase64String("eyJBZ2VudCI6MCwiVHlwZSI6IkVQX1Rvb2wiLCJQYXNzd29yZEVuIjowLCJTZXJ2UG9ydCI6MCwiVmVyc2lvbiI6IjMuMS4zZiIsIkNvbm5lY3RNb2RlIjoiRm9yYmlkZGVuIiwiWm9uZSI6IkVQb3J0IiwiU2VydkVuIjoxLCJTZXJ2QWRkciI6IjAuMC4wLjAiLCJDbGllbnRFbiI6MH0=");

        //{"JCMD":1,"PL":["SYS","UART","SOCK"],"CID":10001}
        private static readonly byte[] cid_10001_Request = Convert.FromBase64String("eyJKQ01EIjoxLCJQTCI6WyJTWVMiLCJVQVJUIiwiU09DSyJdLCJDSUQiOjEwMDAxfQ==");

        //{"JCMD":1,"Remain":1,"CID":10002}
        private static readonly byte[] cid_10002_Request = Convert.FromBase64String("eyJKQ01EIjoxLCJSZW1haW4iOjEsIkNJRCI6MTAwMDJ9");

        //{"JCMD":1,"PL":["SYS","UART","SOCK"],"CID":10003}
        private static readonly byte[] cid_10003_Request = Convert.FromBase64String("eyJKQ01EIjoxLCJQTCI6WyJTWVMiLCJVQVJUIiwiU09DSyJdLCJDSUQiOjEwMDAzfQ==");

        //{"JCMD":1,"Remain":1,"CID":10004}
        private static readonly byte[] cid_10004_Request = Convert.FromBase64String("eyJKQ01EIjoxLCJSZW1haW4iOjEsIkNJRCI6MTAwMDR9");

        private static readonly byte[] xml_start_Request = Encoding.ASCII.GetBytes("xml start");
        private static readonly byte[] xml_end_Request = Encoding.ASCII.GetBytes("xml end");

        private const int DatagramPayloadMaxSize = 512;

        private readonly ConverterDeviceInfo targetDeviceInfo;
        private bool responseDataIsInvalid = false;
        private bool dataLoadedCompletely = false;

        private readonly System.Net.IPAddress localIP;
        private readonly MEventBus eventBus;

        private readonly AutoResetEvent responseEvent = new AutoResetEvent(false);

        private const int SearchDeviceWaitTime = 512;
        private const int CidRequestWaitTime = 512;
        //private EventWaitHandle[] events = new EventWaitHandle[]
        //{
        //    new AutoResetEvent(false),
        //    new AutoResetEvent(false),
        //};

        //private EventWaitHandle exitEvent
        //{
        //    get
        //    {
        //        return events[0];
        //    }
        //}

        //private EventWaitHandle commandEvent
        //{
        //    get
        //    {
        //        return events[1];
        //    }
        //}

        public enum CommCommand
        {
            LoadConfigData,
            CheckOnline,
            UpdateConfig,
        }

        public class ControlArgs
        {
            public readonly CommCommand commCommand;
            public readonly object state;

            public ControlArgs(CommCommand commCommand_, object state_)
            {
                this.commCommand = commCommand_;
                this.state = state_;
            }
        }

        //public class CheckOnlineArgs : ControlArgs
        //{
        //    public readonly IPAddress ipAddress;

        //    public CheckOnlineArgs()
        //}

        public class UpdateConfigParams
        {
            public readonly IPAddress ipAddress;
            public readonly byte[] configData;

            public UpdateConfigParams(IPAddress ipAddress_, byte[] configData_)
            {
                Debug.Assert(ipAddress_ != null && configData_ != null);

                this.ipAddress = ipAddress_;
                this.configData = configData_;
            }
        }

        public enum Request
        {
            SearchDevice,
            LoadConfig,
            PreUpdateConfig,
            CompleteUpdateConfig,
            //Cid_10001,
            //Cid_10002_1,
            //Cid_10002_2,
            //Cid_10003,
            //Cid_10004_1,
            //Cid_10004_2,
        }

        private BackgroundWorker bgWorker = new BackgroundWorker();

        private Request request = Request.SearchDevice;
        
        public RS232ToWifiConverter(System.Net.IPAddress localIP_, MEventBus eventBus_)
        {
            Debug.Assert(localIP_ != null);

            this.udpClient.DatagramReceived += udpClient_DatagramReceived;

            this.localIP = localIP_;
            this.eventBus = eventBus_;

            this.targetDeviceInfo = new ConverterDeviceInfo(localIP_);

            this.bgWorker.DoWork += bkWorker_DoWork;
        }

        private void bkWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ControlArgs controlArgs = e.Argument as ControlArgs;
            if(controlArgs == null)
            {
                return;
            }

            switch (controlArgs.commCommand)
            {
                case CommCommand.LoadConfigData:
                    if(this.LoadConfigData())
                    {
                        this.eventBus.NotifyObeserver(new DeviceConfigLoadedArgs(this.targetDeviceInfo));
                    }
                    break;

                case CommCommand.CheckOnline:
                    {
                        if (controlArgs.state == null || (controlArgs.state as IPAddress) == null)
                        {
                            return;
                        }

                        IPAddress ipAddress = controlArgs.state as IPAddress;

                        this.eventBus.NotifyObeserver(new DeviceBusyArgs(ipAddress, true));

                        bool isOnline = this.CheckOnline(ipAddress);

                        this.eventBus.NotifyObeserver(new DeviceCheckOnlineArgs(ipAddress, isOnline));
                    }
                    
                    break;

                case CommCommand.UpdateConfig:
                    {
                        if (controlArgs.state == null || (controlArgs.state as UpdateConfigParams) == null)
                        {
                            return;
                        }

                        UpdateConfigParams ucp = controlArgs.state as UpdateConfigParams;

                        this.eventBus.NotifyObeserver(new DeviceBusyArgs(ucp.ipAddress, true));

                        bool isSuccess = this.UpdateConfig(ucp);

                        this.eventBus.NotifyObeserver(new DeviceConfigUpdatedArgs(ucp.ipAddress, isSuccess));
                    }
                    
                    break;
            }
            //bool exitLoop = false;
            ////Console.WriteLine("进入循环!");
            //while (true)
            //{
            //    //Console.WriteLine("wait event-0!");
            //    int result = WaitHandle.WaitAny(events);
            //    //Console.WriteLine("wait event-1!");
            //    switch (result)
            //    {
            //        case 0:
            //            exitLoop = true;
            //            break;

            //        case 1:
            //            try
            //            {
            //                CommandHandler();
            //            }
            //            catch (Exception ex)
            //            {
            //                Tools.LogHelper.WriteDebug("SendCmdFromQueue.... Exception: ", ex);
            //            }
            //            break;

            //        default:
            //            exitLoop = true;
            //            break;
            //    }

            //    if (exitLoop)
            //    {
            //        break;
            //    }

            //    //AEC.Log.OutputLine("bgwSendCmd_DoWork 循环:{0}", ++bgwSendCmd_count);
            //}
        }

        //private void CommandHandler()
        //{
            
        //}

        private readonly Dictionary<IPAddress, string> responseMap = new Dictionary<IPAddress, string>();

        private void udpClient_DatagramReceived(object sender, Comm.Net.UdpClientDatagramReceivedEventArgs<byte[]> e)
        {
            //string remoteEP = e.remoteEndPoint.ToString();
            string json = Encoding.ASCII.GetString(e.Datagram);
            AEC.Tools.LogHelper.WriteDebug(string.Format("{0} >> {1}", e.remoteEndPoint.ToString(), json));
            if(e.remoteEndPoint.Port == TargetPort)
            {
                if (!responseMap.ContainsKey(e.remoteEndPoint.Address))
                {
                    responseMap[e.remoteEndPoint.Address] = json;
                }
                else
                {
                    responseMap[e.remoteEndPoint.Address] += json;
                }

                if (!CheckResponseIsComplete(responseMap[e.remoteEndPoint.Address]))
                {
                    return;
                }

                if (responseMap[e.remoteEndPoint.Address].Length > 0x1000)
                {
                    responseMap[e.remoteEndPoint.Address] = string.Empty;
                    return;
                }

                try
                {
                    switch(request)
                    {
                        case Request.SearchDevice:
                            SearchDeviceResponseHandler(e.remoteEndPoint.Address, responseMap[e.remoteEndPoint.Address]);
                            break;

                        case Request.LoadConfig:
                            LoadConfigResponseHandler(responseMap[e.remoteEndPoint.Address]);
                            break;

                        case Request.PreUpdateConfig:
                            PreUpdateConfigResponseHandler(responseMap[e.remoteEndPoint.Address]);
                            break;

                        case Request.CompleteUpdateConfig:
                            CompleteUpdateConfigResponseHandler(responseMap[e.remoteEndPoint.Address]);
                            break;

                        //case Request.Cid_10001:
                        //    Cid_10001_ResponseHandler(responseMap[e.remoteEndPoint.Address]);
                        //    break;

                        //case Request.Cid_10002_1:
                        //    Cid_10002_ResponseHandler(responseMap[e.remoteEndPoint.Address]);
                        //    break;

                        //case Request.Cid_10002_2:
                        //    Cid_10002_ResponseHandler(responseMap[e.remoteEndPoint.Address]);
                        //    break;

                        //case Request.Cid_10003:
                        //    Cid_10003_ResponseHandler(responseMap[e.remoteEndPoint.Address]);
                        //    break;

                        //case Request.Cid_10004_1:
                        //    Cid_10004_1_ResponseHandler(responseMap[e.remoteEndPoint.Address]);
                        //    break;

                        //case Request.Cid_10004_2:
                        //    Cid_10004_2_ResponseHandler(responseMap[e.remoteEndPoint.Address]);
                        //    break;

                    }
                    //JObject objs = JObject.Parse(json);

                    //ConverterDeviceBasicInfo basicInfo = new ConverterDeviceBasicInfo()
                    //{
                    //    type = objs["Type"].ToString(),
                    //    version = objs["Version"].ToString(),
                    //    mac = objs["MAC"].ToString(),
                    //};

                    //lock(this.deviceMap)
                    //{
                    //    this.deviceMap[e.remoteEndPoint.Address] = basicInfo;
                    //}
                }
                catch(Exception ex)
                {
                    AEC.Tools.LogHelper.WriteDebug("udpClient_DatagramReceived,异常:{0}", ex.Message);
                }

                responseMap[e.remoteEndPoint.Address] = string.Empty;
            }
        }

        private void SearchDeviceResponseHandler(System.Net.IPAddress deviceIP, string json)
        {
            try
            {
                JObject objs = JObject.Parse(json);

                targetDeviceInfo.deviceIp = deviceIP;
                targetDeviceInfo.type = objs["Type"].ToString();
                targetDeviceInfo.version = objs["Version"].ToString();
                targetDeviceInfo.mac = objs["MAC"].ToString();

                this.responseDataIsInvalid = false;
            }
            catch(Exception)
            {
                this.responseDataIsInvalid = true;
            }
            finally
            {
                this.responseEvent.Set();
            }
        }

        private void LoadConfigResponseHandler(string json)
        {
            try
            {
                JObject objs = JObject.Parse(json);

                int cid = (int)objs["CID"];

                if(cid != 10004)
                {
                    this.responseDataIsInvalid = true;
                    return;
                }

                int remain = (int)objs["Remain"];
                this.dataLoadedCompletely = remain == 0;    //当 "Remain":0 时，数据加载完毕

                JToken firstToken = objs["PL"].First;
                if (firstToken == null || (firstToken as JProperty) == null)
                {
                    this.responseDataIsInvalid = true;
                    return;
                }

                JProperty firstProperty = firstToken as JProperty;
                if(firstProperty.Name == "SOCK")
                {
                    JArray socks = firstProperty.Value as JArray;
                    if(socks == null)
                    {
                        this.responseDataIsInvalid = true;
                        return;
                    }

                    foreach(var sock in socks)
                    {
                        string name = sock["Name"].ToString();
                        string protocol = sock["Protocol"].ToString();
                        int localPort = (int)sock["LocalPort"];

                        this.targetDeviceInfo.socketMap[name] = new ConverterDeviceInfo.Socket(protocol, localPort);
                    }                    
                    //socks[0]["Protocol"].ToString();
                }
                else if (firstProperty.Name == "UART")
                {
                    //targetDeviceInfo.baudRate = (int)objs["PL"]["UART"]["Baudrate"];
                    //targetDeviceInfo.dataBits = (int)objs["PL"]["UART"]["Databits"];
                    //targetDeviceInfo.stopBits = (int)objs["PL"]["UART"]["Stopbits"];
                    //targetDeviceInfo.parityType = objs["PL"]["UART"]["Parity"].ToString();
                    targetDeviceInfo.baudRate = (int)firstProperty.Value["Baudrate"];
                    targetDeviceInfo.dataBits = (int)firstProperty.Value["Databits"];
                    targetDeviceInfo.stopBits = (int)firstProperty.Value["Stopbits"];
                    targetDeviceInfo.parityType = firstProperty.Value["Parity"].ToString();
                }
                else if (firstProperty.Name == "SYS")
                {
                    //        targetDeviceInfo.userName = objs["PL"]["SYS"]["User"].ToString();
                    //        targetDeviceInfo.password = objs["PL"]["SYS"]["Password"].ToString();
                    //        targetDeviceInfo.WiFi_AP_SSID = objs["PL"]["SYS"]["WiFiAPSSID"].ToString();
                    //        targetDeviceInfo.lanIpAddress = objs["PL"]["SYS"]["LanIpAddress"].ToString();
                    targetDeviceInfo.userName = firstProperty.Value["User"].ToString();
                    targetDeviceInfo.password = firstProperty.Value["Password"].ToString();
                    targetDeviceInfo.WiFi_AP_SSID = firstProperty.Value["WiFiAPSSID"].ToString();
                    targetDeviceInfo.lanIpAddress = firstProperty.Value["LanIpAddress"].ToString();
                }

                this.responseDataIsInvalid = false;
            }
            catch (Exception)
            {
                responseDataIsInvalid = true;
            }
            finally
            {
                this.responseEvent.Set();
            }
        }

        private void PreUpdateConfigResponseHandler(string json)
        {
            try
            {
                JObject objs = JObject.Parse(json);

                int cid = (int)objs["CID"];
                int rc = (int)objs["RC"];

                this.responseDataIsInvalid = cid != 10010 || rc != 0;
            }
            catch (Exception)
            {
                responseDataIsInvalid = true;
            }

            this.responseEvent.Set();
        }

        private void CompleteUpdateConfigResponseHandler(string json)
        {
            try
            {
                JObject objs = JObject.Parse(json);

                string type = objs["Type"].ToString();
                int cid = (int)objs["CID"];
                int rc = (int)objs["RC"];

                this.responseDataIsInvalid = type != "Cfg" || cid != 20008 || rc != 0;
            }
            catch (Exception)
            {
                responseDataIsInvalid = true;
            }

            this.responseEvent.Set();
        }

        //private void Cid_10001_ResponseHandler(string json)
        //{
        //    try
        //    {
        //        JObject objs = JObject.Parse(json);

        //        int cid = (int)objs["CID"];

        //        JArray sock = (JArray)objs["PL"]["SOCK"];
        //        if(sock.Count > 0)
        //        {
        //            sock[0]["Protocol"].ToString();
        //        }
        //        else
        //        {

        //        }

        //        this.responseDataIsInvalid = cid != 10002;
        //    }
        //    catch (Exception)
        //    {
        //        responseDataIsInvalid = true;
        //    }

        //    this.responseEvent.Set();
        //}

        //private void Cid_10002_ResponseHandler(string json)
        //{
        //    try
        //    {
        //        JObject objs = JObject.Parse(json);

        //        int cid = (int)objs["CID"];

        //        this.responseDataIsInvalid = cid != 10002;
        //    }
        //    catch (Exception)
        //    {
        //        responseDataIsInvalid = true;
        //    }

        //    this.responseEvent.Set();
        //}

        //private void Cid_10003_ResponseHandler(string json)
        //{
        //    try
        //    {
        //        JObject objs = JObject.Parse(json);

        //        int cid = (int)objs["CID"];

        //        JArray sock = (JArray)objs["PL"]["SOCK"];
        //        if(sock.Count > 0)
        //        {
        //            targetDeviceInfo.protocol = sock[0]["Protocol"].ToString();
        //            targetDeviceInfo.localPort = (int)sock[0]["LocalPort"];

        //            this.responseDataIsInvalid = cid != 10004;
        //        }
        //        else
        //        {
        //            this.responseDataIsInvalid = true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        responseDataIsInvalid = true;
        //    }

        //    this.responseEvent.Set();
        //}

        //private void Cid_10004_1_ResponseHandler(string json)
        //{
        //    try
        //    {
        //        JObject objs = JObject.Parse(json);

        //        int cid = (int)objs["CID"];

        //        targetDeviceInfo.baudRate = (int)objs["PL"]["UART"]["Baudrate"];
        //        targetDeviceInfo.dataBits = (int)objs["PL"]["UART"]["Databits"];
        //        targetDeviceInfo.stopBits = (int)objs["PL"]["UART"]["Stopbits"];
        //        targetDeviceInfo.parityType = objs["PL"]["UART"]["Parity"].ToString();

        //        this.responseDataIsInvalid = cid == 10004;
        //    }
        //    catch (Exception)
        //    {
        //        responseDataIsInvalid = true;
        //    }

        //    this.responseEvent.Set();
        //}

        //private void Cid_10004_2_ResponseHandler(string json)
        //{
        //    try
        //    {
        //        JObject objs = JObject.Parse(json);

        //        int cid = (int)objs["CID"];

        //        targetDeviceInfo.userName = objs["PL"]["SYS"]["User"].ToString();
        //        targetDeviceInfo.password = objs["PL"]["SYS"]["Password"].ToString();
        //        targetDeviceInfo.WiFi_AP_SSID = objs["PL"]["SYS"]["WiFiAPSSID"].ToString();
        //        targetDeviceInfo.lanIpAddress = objs["PL"]["SYS"]["LanIpAddress"].ToString();

        //        this.responseDataIsInvalid = cid != 10004;
        //    }
        //    catch (Exception)
        //    {
        //        responseDataIsInvalid = true;
        //    }

        //    this.responseEvent.Set();
        //}

        public bool InitClient()
        {
            try
            {
                this.udpClient.Init(localIP);
                
                return true;
            }
            catch (Exception)
            {

            }

            return false;
        }

        //public void LoadDeviceInfo()
        //{
        //    try
        //    {
        //        this.udpClient.Broadcast(TargetPort, BroadcastData);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        public void AsyncWork(ControlArgs controlArgs)
        {
            if(this.bgWorker.IsBusy)
            {
                return;
            }

            this.bgWorker.RunWorkerAsync(controlArgs);
        }

        //private bool WaitResponse(int time)
        //{
            
        //}

        private bool SearchDevice()
        {
            request = Request.SearchDevice;
            this.responseEvent.Reset();            
            this.udpClient.Broadcast(TargetPort, searchDeviceRequest);
            AEC.Tools.LogHelper.WriteDebug("{0}发送广播请求：{0}", this.localIP.ToString(), Encoding.ASCII.GetString(searchDeviceRequest));

            if (!responseEvent.WaitOne(SearchDeviceWaitTime) || this.responseDataIsInvalid)
            {
                return false;
            }

            return true;    
        }

        //private bool Request_Cid_10001()
        //{
        //    Debug.Assert(this.targetDeviceInfo.ipAddress != null);

        //    request = Request.Cid_10001;
        //    this.udpClient.SendData(new IPEndPoint(this.targetDeviceInfo.ipAddress, TargetPort), cid_10001_Request);
        //    AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10001请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.ipAddress, Encoding.ASCII.GetString(cid_10001_Request));

        //    if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //private bool Request_Cid_10002()
        //{
        //    Debug.Assert(this.targetDeviceInfo.ipAddress != null);

        //    IPEndPoint targetEP = new IPEndPoint(this.targetDeviceInfo.ipAddress, TargetPort);

        //    request = Request.Cid_10002_1;
        //    this.udpClient.SendData(targetEP, cid_10002_Request);
        //    AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10002请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.ipAddress, Encoding.ASCII.GetString(cid_10002_Request));

        //    if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
        //    {
        //        return false;
        //    }

        //    request = Request.Cid_10002_2;
        //    this.udpClient.SendData(targetEP, cid_10002_Request);
        //    AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10002请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.ipAddress, Encoding.ASCII.GetString(cid_10002_Request));

        //    if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //private bool Request_Cid_10003()
        //{
        //    Debug.Assert(this.targetDeviceInfo.ipAddress != null);

        //    IPEndPoint targetEP = new IPEndPoint(this.targetDeviceInfo.ipAddress, TargetPort);

        //    request = Request.Cid_10003;
        //    this.udpClient.SendData(targetEP, cid_10003_Request);
        //    AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10003请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.ipAddress, Encoding.ASCII.GetString(cid_10003_Request));

        //    if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //private bool Request_Cid_10004()
        //{
        //    Debug.Assert(this.targetDeviceInfo.ipAddress != null);

        //    IPEndPoint targetEP = new IPEndPoint(this.targetDeviceInfo.ipAddress, TargetPort);

        //    request = Request.Cid_10004_1;
        //    this.udpClient.SendData(targetEP, cid_10004_Request);
        //    AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10004请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.ipAddress, Encoding.ASCII.GetString(cid_10004_Request));

        //    if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
        //    {
        //        return false;
        //    }

        //    request = Request.Cid_10004_2;
        //    this.udpClient.SendData(targetEP, cid_10004_Request);
        //    AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10004请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.ipAddress, Encoding.ASCII.GetString(cid_10004_Request));

        //    if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        private bool RequestLoadConfig()
        {
            Debug.Assert(this.targetDeviceInfo.deviceIp != null);

            IPEndPoint targetEP = new IPEndPoint(this.targetDeviceInfo.deviceIp, TargetPort);

            request = Request.LoadConfig;
            this.udpClient.SendData(targetEP, cid_10003_Request);
            AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10003请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.deviceIp, Encoding.ASCII.GetString(cid_10003_Request));

            if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
            {
                return false;
            }

            int count = 0;

            while(!dataLoadedCompletely)
            {
                if (count++ > 0x10)
                {
                    return false;
                }

                this.udpClient.SendData(targetEP, cid_10004_Request);
                AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10004请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.deviceIp, Encoding.ASCII.GetString(cid_10004_Request));

                if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
                {
                    return false;
                }
            }

            return true;
        }

        public bool AsyncLoadConfigData()
        {
            if(this.bgWorker.IsBusy)
            {
                return false;
            }

            ControlArgs controlArgs = new ControlArgs(CommCommand.LoadConfigData, null);

            this.AsyncWork(controlArgs);

            return true;
        }

        public bool AsyncCheckOnline(IPAddress ip)
        {
            Debug.Assert(ip != null);

            if (this.bgWorker.IsBusy)
            {
                return false;
            }

            ControlArgs controlArgs = new ControlArgs(CommCommand.CheckOnline, ip);

            this.AsyncWork(controlArgs);

            return true;
        }

        public bool AsyncUpdateConfig(IPAddress ip, byte[] configData)
        {
            Debug.Assert(ip != null);

            if (this.bgWorker.IsBusy)
            {
                return false;
            }

            UpdateConfigParams configParams = new UpdateConfigParams(ip, configData);

            ControlArgs controlArgs = new ControlArgs(CommCommand.UpdateConfig, configParams);

            this.AsyncWork(controlArgs);

            return true;
        }

        private bool LoadConfigData()
        {
            try
            {
                this.responseDataIsInvalid = false;
                this.targetDeviceInfo.Reset();

                if (!this.SearchDevice())
                {
                    //AEC.Tools.LogHelper.WriteDebug("搜索设备失败");
                    return false;
                }

                if(!RequestLoadConfig())
                {
                    //AEC.Tools.LogHelper.WriteDebug("加载配置数据失败");
                    return false;
                }

                return true;

                //if (!this.Request_Cid_10001())
                //{
                //    //AEC.Tools.LogHelper.WriteDebug("CID_10001请求失败");
                //    return;
                //}

                //if (!this.Request_Cid_10002())
                //{
                //    //AEC.Tools.LogHelper.WriteDebug("CID_10002请求失败");
                //    return;
                //}

                //if (!this.Request_Cid_10003())
                //{
                //    //AEC.Tools.LogHelper.WriteDebug("CID_10003请求失败");
                //    return;
                //}

                //if (!this.Request_Cid_10004())
                //{
                //    //AEC.Tools.LogHelper.WriteDebug("CID_10004请求失败");
                //    return;
                //}

            }
            catch(Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("LoadConfigData()...异常:", ex);
            }

            return false;
        }

        private bool CheckOnline(IPAddress ipAddress)
        {
            request = Request.SearchDevice;
            this.responseEvent.Reset();
            this.udpClient.SendData(new IPEndPoint(ipAddress, TargetPort), searchDeviceRequest);
            AEC.Tools.LogHelper.WriteDebug("{0}发送心跳请求：{0}", this.localIP.ToString(), Encoding.ASCII.GetString(searchDeviceRequest));

            if (!responseEvent.WaitOne(SearchDeviceWaitTime) || this.responseDataIsInvalid)
            {
                return false;
            }

            return true;
        }

        private bool UpdateConfig(UpdateConfigParams ucp)
        {
            request = Request.PreUpdateConfig;

            IPEndPoint targetEP = new IPEndPoint(ucp.ipAddress, TargetPort);
            byte[] requestCID_10009 = GetRequestCID_10009(ucp.configData.Length);
            this.udpClient.SendData(targetEP, requestCID_10009);

            AEC.Tools.LogHelper.WriteDebug("{0}发送CID_10009请求到目标：{1}, 数据：{2}", this.localIP.ToString(), this.targetDeviceInfo.deviceIp, Encoding.ASCII.GetString(requestCID_10009));

            if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
            {
                AEC.Tools.LogHelper.WriteDebug("等待CID_10010响应失败");
                return false;
            }

            request = Request.CompleteUpdateConfig;

            this.udpClient.SendData(targetEP, xml_start_Request);
            AEC.Tools.LogHelper.WriteDebug("{0}发送 xml start 到目标", this.localIP.ToString());

            System.Threading.Thread.Sleep(1);

            using(MemoryStream ms = new MemoryStream(ucp.configData))
            {
                while(true)
                {
                    int remain = (int)(ms.Length - ms.Position);
                    if(remain <= 0)
                    {
                        break;
                    }

                    int length = remain > DatagramPayloadMaxSize ? DatagramPayloadMaxSize : remain;
                    byte[] data = new byte[length];
                    ms.Read(data, 0, data.Length);
                    this.udpClient.SendData(targetEP, data);

                    AEC.Tools.LogHelper.WriteDebug("{0}发送数据到目标：{1}, 数据：", this.localIP.ToString(), this.targetDeviceInfo.deviceIp);
                    AEC.Tools.LogHelper.WriteDebug("{0}", Encoding.ASCII.GetString(data));

                    System.Threading.Thread.Sleep(1);
                }
            }

            this.udpClient.SendData(targetEP, xml_end_Request);

            AEC.Tools.LogHelper.WriteDebug("{0}发送 xml end 到目标", this.localIP.ToString());

            if (!responseEvent.WaitOne(CidRequestWaitTime) || this.responseDataIsInvalid)
            {
                AEC.Tools.LogHelper.WriteDebug("等待CID_10010响应失败");
                return false;
            }

            AEC.Tools.LogHelper.WriteDebug("更新配置成功！");

            return true;
        }

        public void Close()
        {
            this.udpClient.DatagramReceived -= udpClient_DatagramReceived;
            this.udpClient.Close();
        }

        public static byte[] GetRequestCID_10009(int length)
        {
            JObject jObj = new JObject
            {
                {"JCMD", 1},
                {"PL", new JObject{{"Length", 2954}}},
                {"CID", 10009},
            };

            return Encoding.ASCII.GetBytes(jObj.ToString(Formatting.None));
        }

        private static bool CheckResponseIsComplete(string json)
        {
            int openBrace = 0;
            int closeBrace = 0;
            foreach (var c in json)
            {
                if (c == '{')
                {
                    openBrace++;
                }
                else if (c == '}')
                {
                    closeBrace++;
                }
            }

            return openBrace > 0 && openBrace == closeBrace;
        }
    }
}
