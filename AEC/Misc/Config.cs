using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Net;

namespace AEC.Misc
{
    public class Config
    {
        private const string RootNode = "configuration";

        private const string LangNode = "lang";

        private const string DefaultDeviceModel = "default_device_model";
        private const string UseLastDeviceModelWhenStarts = "use_last_device_model_when_starts";
        private const string AutoConnectionNode = "auto_connection";

        private const string LimiterThresholdUnitNode = "threshold_unit";
        private const string MixerLevelUnitNode = "mixer_level_unit";
        private const string ChannelDelayUnitNode = "channel_delay_unit";
        private const string TemperatureNode = "temperature";

        private const string CommNode = "comm";
        
        private const string ConnectionMethodNode = "connection_method";
        private const string DefaultSerialPortNode = "default_serialPort";
        private const string SpManualModeNode = "sp_manual_mode";
        private const string UdpNode = "udp";
        private const string TcpNode = "tcp";
        private const string IpNode = "ip";
        private const string PortNode = "port";
        private const string CommRetriesNode = "comm_retries";

        public const string comPortPrefix = "COM";

        private const string RcNode = "remote_control";
        private const string RcServerIpNode = "rc_server_ip";
        private const string RcServerPortNode = "rc_server_port";

        private const string RtaNode = "rta";
        private const string RtaModeNode = "mode";
        private const string AudioIONode = "audio_io";
        private const string SpNode = "sp";
        private const string TfNode = "tf";
        private const string OutputSignalNode = "output_signal";
        private const string InputDeviceNode = "input_device";
        private const string OutputDeviceNode = "output_device";
        private const string SampleRateNode = "sample_rate";
        private const string SampleBitDepthNode = "sample_bit_depth";
        private const string SpFftSizeNode = "sp_fft_size";
        private const string SpWindowNode = "sp_window";
        private const string SpAveragerNode = "sp_averager";
        private const string SpScaleNode = "sp_scale";
        private const string SpGraphNode = "sp_graph";
        private const string TfFftSizeNode = "tf_fft_size";
        private const string TfWindowNode = "tf_window";
        private const string TfAveragerNode = "tf_averager";
        private const string TfSmootherNode = "tf_smoother";
        private const string TfAmpOffsetNode = "tf_amp_offset";
        private const string TfGraphNode = "tf_graph";
        private const string DelayNode = "delay";
        private const string OutputSignalTypeNode = "output_signal_type";
        private const string PinkNoiseLevelNode = "pink_noise_level";
        private const string PinkSweepLevelNode = "pink_sweep_level";
        private const string SineLevelNode = "sine_level";
        private const string SineFreqNode = "sine_freq";
        private const string DualSine1LevelNode = "dual_sine1_level";
        private const string DualSine1FreqNode = "dual_sine1_freq";
        private const string DualSine2LevelNode = "dual_sine2_level";
        private const string DualSine2FreqNode = "dual_sine2_freq";

        private static string configFileName = "AEC.xml";

        private static string configFilePath
        {
            get
            {
                string currDir = System.AppDomain.CurrentDomain.BaseDirectory;
                return currDir + configFileName;
            }
        }

        public static Config GetInstance()
        {
            return _instance;
        }

        private static Config _instance = new Config();

        private const string DefSerialPort = "COM1";
        public const string DefUdpIPAddress = "192.168.8.1";
        public const ushort DefUdpPort = 8889;
        public const string DefTCPIPAddress = "192.168.8.1";
        public const ushort DefTCPPort = 8899;
        public const int MaxCommRetries = 2;
        public const int DefaultCommRetries = MaxCommRetries;

        //public const ConnectionMethod DefConnectionMethodFlag = ConnectionMethod.USB | ConnectionMethod.RS232 | ConnectionMethod.UDP;
        public const ConnectionMethod AllConnectionMethodFlag = ConnectionMethod.RS232 | ConnectionMethod.UDP | ConnectionMethod.TCP;

        public const int MinTemperature = -30;
        public const int MaxTemperature = 70;
        public const int DefaultTemperature = 20;

        public readonly Localization localization = new Localization();

        private readonly AEC.RemoteControl.RCConfig rcConfig = AEC.RemoteControl.RCClient.config;

        private string _defaultDevcieModel = string.Empty;
        private bool _useLastConnectedModelWhenStarts = true;

        public string defaultDevcieModel
        {
            get
            {
                return this._defaultDevcieModel;
            }
            set
            {
                if (this._defaultDevcieModel != value)
                {
                    this._defaultDevcieModel = value;
                    this.SaveToConfigFile();
                }
            }
        }

        public bool useLastConnectedModelWhenStarts
        {
            get
            {
                return _useLastConnectedModelWhenStarts;
            }
            set
            {
                if(_useLastConnectedModelWhenStarts != value)
                {
                    _useLastConnectedModelWhenStarts = value;
                    this.SaveToConfigFile();
                }
            }
        }

        public bool isAutoConnection = false;

        private short _thresholdUnit = 0;
        private short _mixerLevelUnit = 0;
        private short _channelDelayUnit = 0;

        private int _temperature = DefaultTemperature;

        private ConnectionMethod _connectionMethod = ConnectionMethod.Default;

        public ConnectionMethod connectionMethod
        {
            get
            {
                return _connectionMethod;
            }
            private set
            {
                this._connectionMethod = value & AllConnectionMethodFlag;
            }
        }

        public int connectionMethodIndex
        {
            get
            {
                return (int)this._connectionMethod;
            }
            set
            {
                this._connectionMethod = (ConnectionMethod)(value & (int)AllConnectionMethodFlag);
            }
        }

        public bool connectionViaUSB
        {
            get
            {
                return (this._connectionMethod & ConnectionMethod.USB) == ConnectionMethod.USB;
            }
            set
            {
                if(value)
                {
                    this._connectionMethod |= ConnectionMethod.USB;
                }
                else
                {
                    this._connectionMethod &= ~ConnectionMethod.USB;
                }
            }
        }

        public bool connectionViaRS232
        {
            get
            {
                return (this._connectionMethod & ConnectionMethod.RS232) == ConnectionMethod.RS232;
            }
            set
            {
                if (value)
                {
                    this._connectionMethod |= ConnectionMethod.RS232;
                }
                else
                {
                    this._connectionMethod &= ~ConnectionMethod.RS232;
                }
            }
        }

        public bool connectionViaUDP
        {
            get
            {
                return (this._connectionMethod & ConnectionMethod.UDP) == ConnectionMethod.UDP;
            }
            set
            {
                if (value)
                {
                    this._connectionMethod |= ConnectionMethod.UDP;
                }
                else
                {
                    this._connectionMethod &= ~ConnectionMethod.UDP;
                }
            }
        }

        public bool connectionViaTCP
        {
            get
            {
                return (this._connectionMethod & ConnectionMethod.TCP) == ConnectionMethod.TCP;
            }
            set
            {
                if (value)
                {
                    this._connectionMethod |= ConnectionMethod.TCP;
                }
                else
                {
                    this._connectionMethod &= ~ConnectionMethod.TCP;
                }
            }
        }

        public string default_SerialPort = DefSerialPort;
        public bool isSpManualMode = false;
        public string udp_ip = DefUdpIPAddress;
        public ushort udp_port = DefUdpPort;
        public string tcp_ip = DefUdpIPAddress;
        public ushort tcp_port = DefUdpPort;
        private int _commRetries = DefaultCommRetries;

        private AEC.RTA.Core.AudioRTAParam rtaParam = AEC.RTA.AudioAnalysis.rtaParam;

        public ThresholdUnit thresholdUnit
        {
            get
            {
                return this._thresholdUnit == 0 ? ThresholdUnit.IndB : ThresholdUnit.InV;
            }
            private set
            {
                this._thresholdUnit = (short)(value == ThresholdUnit.IndB ? 0 : 1);
            }
        }

        public MixerLevelUnit mixerLevelUnit
        {
            get
            {
                return this._mixerLevelUnit == 0 ? MixerLevelUnit.IndB : MixerLevelUnit.InPercentage;
            }
            private set
            {
                this._mixerLevelUnit = (short)(value == MixerLevelUnit.IndB ? 0 : 1);
            }
        }

        public ChannelDelayUnit channelDelayUnit
        {
            get
            {
                return this._channelDelayUnit == 0 ? ChannelDelayUnit.InMilliseconds : ChannelDelayUnit.InMeter;
            }
            private set
            {
                this._channelDelayUnit = (short)(value == ChannelDelayUnit.InMilliseconds ? 0 : 1);
            }
        }

        public int commRetries
        {
            get
            {
                return this._commRetries;
            }
            set
            {
                this._commRetries = AEC.Core.Utils.ConstrainMax(value, MaxCommRetries);
            }
        }

        public bool SetThresholdUnit(ThresholdUnit thresholdUnit)
        {
            if (this.thresholdUnit != thresholdUnit)
            {
                this.thresholdUnit = thresholdUnit;

                return true;
            }

            return false;            
        }

        public void SwitchThresholdUnit()
        {
            if (this.thresholdUnit == ThresholdUnit.IndB)
            {
                this.thresholdUnit = ThresholdUnit.InV;
            }
            else
            {
                this.thresholdUnit = ThresholdUnit.IndB;
            }
        }

        public bool SetMixerLevelUnit(MixerLevelUnit mixerLevelUnit)
        {
            if (this.mixerLevelUnit != mixerLevelUnit)
            {
                this.mixerLevelUnit = mixerLevelUnit;

                return true;
            }

            return false;
        }

        public void SwitchMixerLevelUnit()
        {
            if (this.mixerLevelUnit == MixerLevelUnit.IndB)
            {
                this.mixerLevelUnit = MixerLevelUnit.InPercentage;
            }
            else
            {
                this.mixerLevelUnit = MixerLevelUnit.IndB;
            }
        }

        public void SwitchChannelDelayUnit()
        {
            if (this.channelDelayUnit == ChannelDelayUnit.InMilliseconds)
            {
                this.channelDelayUnit = ChannelDelayUnit.InMeter;
            }
            else
            {
                this.channelDelayUnit = ChannelDelayUnit.InMilliseconds;
            }
        }

        public bool SetChannelDelayUnit(ChannelDelayUnit channelDelayUnit)
        {
            if (this.channelDelayUnit != channelDelayUnit)
            {
                this.channelDelayUnit = channelDelayUnit;

                return true;
            }

            return false;
        }

        public int temperature
        {
            get
            {
                return this._temperature;
            }
            set
            {
                int temp = AEC.Core.Utils.ConstrainValue(value, MinTemperature, MaxTemperature);
                this._temperature = temp;
            }
        }

        public int temperatureIndex
        {
            get
            {
                return this._temperature - MinTemperature;
            }
            set
            {
                this.temperature = value + MinTemperature;
            }
        }

        /// <summary>
        /// 当前温度下，声速 m/s, c=331.6+0.6T(m/s)(其中T为摄氏温标,331.6 m/s是声波在空气中温度为0摄氏度时的传播速度)
        /// </summary>
        public double speedOfSound_In_m
        {
            get
            {
                return (331.3 + 0.606 * this._temperature) / 1000;
            }
        }

        private Config()
        {
            if(!this.LoadFromConfigFile())
            {
                SaveToConfigFile();
            }
        }

        private bool LoadFromConfigFile()
        {
            string filePath = configFilePath;
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }

            XmlDocument xmldoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = null;

            try
            {
                reader = XmlReader.Create(filePath, settings);
                xmldoc.Load(reader);

                if (xmldoc.DocumentElement != null &&
                    xmldoc.DocumentElement.LocalName == RootNode)
                {
                    XmlElement rootNode = xmldoc.DocumentElement;
                    if (rootNode.HasChildNodes)
                    {
                        XmlNode langNode = rootNode.SelectSingleNode(LangNode);
                        if (langNode != null)
                        {
                            ParseLangNode(langNode);
                        }

                        XmlNode defaultDeviceModelNode = rootNode.SelectSingleNode(DefaultDeviceModel);
                        if (defaultDeviceModelNode != null)
                        {
                            ParseDefaultDeviceModelNode(defaultDeviceModelNode);
                        }

                        XmlNode useLastDeviceModelNode = rootNode.SelectSingleNode(UseLastDeviceModelWhenStarts);
                        if (useLastDeviceModelNode != null)
                        {
                            ParseUseLastDeviceModelWhenStartsNode(useLastDeviceModelNode);
                        }

                        XmlNode autoConnectionNode = rootNode.SelectSingleNode(AutoConnectionNode);
                        if (autoConnectionNode != null)
                        {
                            ParseAutoConnectionNode(autoConnectionNode);
                        }

                        XmlNode thresholdUnitNode = rootNode.SelectSingleNode(LimiterThresholdUnitNode);
                        if (thresholdUnitNode != null)
                        {
                            ParseThresholdUnitNode(thresholdUnitNode);
                        }

                        XmlNode mixerLevelUnitNode = rootNode.SelectSingleNode(MixerLevelUnitNode);
                        if (mixerLevelUnitNode != null)
                        {
                            ParseMixerLevelUnitNode(mixerLevelUnitNode);
                        }

                        XmlNode channelDelayUnitNode = rootNode.SelectSingleNode(ChannelDelayUnitNode);
                        if (channelDelayUnitNode != null)
                        {
                            ParseChannelDelayUnitNode(channelDelayUnitNode);
                        }

                        XmlNode temperatureNode = rootNode.SelectSingleNode(TemperatureNode);
                        if (temperatureNode != null)
                        {
                            ParseTemperatureNode(temperatureNode);
                        }

                        XmlNode commNode = rootNode.SelectSingleNode(CommNode);
                        if(commNode != null)
                        {
                            ParseCommNode(commNode);
                        }

                        XmlNode rcNode = rootNode.SelectSingleNode(RcNode);
                        if (rcNode != null)
                        {
                            ParseRcNode(rcNode);
                        }

                        XmlNode rtaNode = rootNode.SelectSingleNode(RtaNode);
                        if (rtaNode != null)
                        {
                            ParseRtaNode(rtaNode);
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return true;
        }

        private void ParseLangNode(XmlNode langNode)
        {
            if (langNode == null)
            {
                return;
            }

            localization.langCode = langNode.InnerText;
        }

        private void ParseDefaultDeviceModelNode(XmlNode deviceModelNode)
        {
            if (deviceModelNode == null)
            {
                return;
            }

            this._defaultDevcieModel = deviceModelNode.InnerText.Trim();
        }

        private void ParseUseLastDeviceModelWhenStartsNode(XmlNode useLastDeviceModelNode)
        {
            if (useLastDeviceModelNode == null)
            {
                return;
            }

            int value = 0;
            if (int.TryParse(useLastDeviceModelNode.InnerText, out value))
            {
                this._useLastConnectedModelWhenStarts = value != 0;
            }
        }

        private void ParseAutoConnectionNode(XmlNode autoConnection)
        {
            if (autoConnection == null)
            {
                return;
            }

            int value = 0;
            if (int.TryParse(autoConnection.InnerText, out value))
            {
                this.isAutoConnection = value != 0;
            }
        }

        private void ParseThresholdUnitNode(XmlNode unitNode)
        {
            if (unitNode == null)
            {
                return;
            }

            short.TryParse(unitNode.InnerText, out this._thresholdUnit);
        }

        private void ParseMixerLevelUnitNode(XmlNode unitNode)
        {
            if (unitNode == null)
            {
                return;
            }

            short.TryParse(unitNode.InnerText, out this._mixerLevelUnit);
        }

        private void ParseChannelDelayUnitNode(XmlNode unitNode)
        {
            if (unitNode == null)
            {
                return;
            }

            short.TryParse(unitNode.InnerText, out this._channelDelayUnit);
        }

        private void ParseTemperatureNode(XmlNode unitNode)
        {
            if (unitNode == null)
            {
                return;
            }

            short temperature_ = DefaultTemperature;
            if (short.TryParse(unitNode.InnerText, out temperature_))
            {
                this.temperature = temperature_;
            }
        }

        private void ParseCommNode(XmlNode commNode)
        {
            if(commNode == null)
            {
                return;
            }
            foreach (XmlElement ele in commNode.ChildNodes)
            {
                switch(ele.Name)
                {
                    case ConnectionMethodNode:
                        {
                            uint method = 0;
                            if (uint.TryParse(ele.InnerText, out method))
                            {
                                this.connectionMethod = UInt2ConnectionMethod(method);
                            }
                        }
                        break;

                    case DefaultSerialPortNode:
                        {
                            if (ele.InnerText.StartsWith(comPortPrefix) && ele.InnerText.Length > comPortPrefix.Length)
                            {
                                string port_num = ele.InnerText.Substring(comPortPrefix.Length);
                                byte port = 0;
                                if (byte.TryParse(port_num, out port))
                                {
                                    this.default_SerialPort = ele.InnerText;
                                }
                            }
                        }
                        break;

                    case SpManualModeNode:
                        {
                            int value = 0;
                            if (int.TryParse(ele.InnerText, out value))
                            {
                                this.isSpManualMode = value == 1;
                            }
                        }
                        break;

                    case UdpNode:
                        ParseUdpNode(ele);
                        break;

                    case TcpNode:
                        ParseTcpNode(ele);
                        break;

                    case CommRetriesNode:
                        {
                            int value = 0;
                            if (int.TryParse(ele.InnerText, out value))
                            {
                                this.commRetries = value;
                            }
                        }
                        break;
                }
            }
        }

        private void ParseUdpNode(XmlNode udpNode)
        {
            if (udpNode == null)
            {
                return;
            }
            foreach (XmlElement ele in udpNode.ChildNodes)
            {
                switch (ele.Name)
                {
                    case IpNode:
                        {
                            IPAddress ipAddress;
                            if (IPAddress.TryParse(ele.InnerText, out ipAddress))
                            {
                                this.udp_ip = ele.InnerText;
                            }
                        }
                        break;

                    case PortNode:
                        {
                            ushort port = 0;
                            if (ushort.TryParse(ele.InnerText, out port))
                            {
                                this.udp_port = port;
                            }
                        }
                        break;
                }
            }
        }

        private void ParseTcpNode(XmlNode udpNode)
        {
            if (udpNode == null)
            {
                return;
            }
            foreach (XmlElement ele in udpNode.ChildNodes)
            {
                switch (ele.Name)
                {
                    case IpNode:
                        {
                            IPAddress ipAddress;
                            if (IPAddress.TryParse(ele.InnerText, out ipAddress))
                            {
                                this.tcp_ip = ele.InnerText;
                            }
                        }
                        break;

                    case PortNode:
                        {
                            ushort port = 0;
                            if (ushort.TryParse(ele.InnerText, out port))
                            {
                                this.tcp_port = port;
                            }
                        }
                        break;
                }
            }
        }

        private void ParseRcNode(XmlNode rcNode)
        {
            if (rcNode == null)
            {
                return;
            }

            foreach (XmlElement ele in rcNode.ChildNodes)
            {
                switch (ele.Name)
                {
                    case RcServerIpNode:
                        {
                            IPAddress ipAddress;
                            if (IPAddress.TryParse(ele.InnerText, out ipAddress))
                            {
                                this.rcConfig.serverIP = ele.InnerText;
                            }
                        }
                        break;

                    case RcServerPortNode:
                        {
                            ushort port = 0;
                            if (ushort.TryParse(ele.InnerText, out port))
                            {
                                this.rcConfig.serverPort = port;
                            }
                        }
                        break;
                }
            }
        }

        private void ParseRtaNode(XmlNode rtaNode)
        {
            if (rtaNode == null)
            {
                return;
            }

            XmlNode modeNode = rtaNode.SelectSingleNode(RtaModeNode);
            if (modeNode != null)
            {
                modeNode.InnerText = modeNode.InnerText.Trim();
                int value = 0;
                if (int.TryParse(modeNode.InnerText, out value))
                {
                    rtaParam.rtaModeIndex = value;
                }
            }

            XmlNode audioIONode = rtaNode.SelectSingleNode(AudioIONode);
            if (audioIONode != null)
            {
                ParseAudioIONode(audioIONode);
            }

            XmlNode spNode = rtaNode.SelectSingleNode(SpNode);
            if (spNode != null)
            {
                ParseSpNode(spNode);
            }

            XmlNode tfNode = rtaNode.SelectSingleNode(TfNode);
            if (tfNode != null)
            {
                ParseTfNode(tfNode);
            }

            XmlNode delayNode = rtaNode.SelectSingleNode(DelayNode);
            if (delayNode != null)
            {
                float value = 0;
                if(float.TryParse(delayNode.InnerText, out value))
                {
                    rtaParam.delay = value;
                }
            }

            XmlNode outputSignalNode = rtaNode.SelectSingleNode(OutputSignalNode);
            if (outputSignalNode != null)
            {
                ParseOutputSignalNode(outputSignalNode);
            }
        }

        private void ParseAudioIONode(XmlNode audioIONode)
        {
            foreach (XmlElement ele in audioIONode.ChildNodes)
            {
                ele.InnerText = ele.InnerText.Trim();
                switch (ele.Name)
                {
                    case InputDeviceNode:
                        rtaParam.inputDeviceDescription = ele.InnerText;
                        break;

                    case OutputDeviceNode:
                        rtaParam.outputDeviceDescription = ele.InnerText;
                        break;

                    case SampleRateNode:
                        {
                            int value = 0;
                            if (int.TryParse(ele.InnerText, out value))
                            {
                                rtaParam.sampleRate = value;
                            }
                        }
                        break;

                    case SampleBitDepthNode:
                        {
                            short value = 0;
                            if (short.TryParse(ele.InnerText, out value))
                            {
                                rtaParam.sampleBitDepth = value;
                            }
                        }
                        break;
                }
            }
        }

        private void ParseSpNode(XmlNode spNode)
        {
            foreach (XmlElement ele in spNode.ChildNodes)
            {
                ele.InnerText = ele.InnerText.Trim();
                int value = 0;
                if (!int.TryParse(ele.InnerText, out value))
                {
                    break;
                }

                switch (ele.Name)
                {
                    case SpFftSizeNode:
                        rtaParam.spFftSizeIndex = value;
                        break;

                    case SpWindowNode:
                        rtaParam.spWindowIndex = value;
                        break;

                    case SpAveragerNode:
                        rtaParam.spAverageModeIndex = value;
                        break;

                    case SpScaleNode:
                        rtaParam.spScaleIndex = value;
                        break;

                    case SpGraphNode:
                        rtaParam.spMapIndex = value;
                        break;
                }
            }
        }

        private void ParseTfNode(XmlNode tfNode)
        {
            foreach (XmlElement ele in tfNode.ChildNodes)
            {
                ele.InnerText = ele.InnerText.Trim();
                int value = 0;
                if (!int.TryParse(ele.InnerText, out value))
                {
                    break;
                }

                switch (ele.Name)
                {
                    case TfFftSizeNode:
                        rtaParam.tfFftSizeIndex = value;
                        break;

                    case TfWindowNode:
                        rtaParam.tfWindowIndex = value;
                        break;

                    case TfAveragerNode:
                        rtaParam.tfAverageModeIndex = value;
                        break;

                    case TfSmootherNode:
                        rtaParam.tfSmoothIndex = value;
                        break;

                    case TfAmpOffsetNode:
                        rtaParam.tfMagOffsetIndex = (short)value;
                        break;

                    case TfGraphNode:
                        rtaParam.freqRespMapIndex = value;
                        break;
                }
            }
        }

        private void ParseOutputSignalNode(XmlNode outputSignalNode)
        {
            foreach (XmlElement ele in outputSignalNode.ChildNodes)
            {
                ele.InnerText = ele.InnerText.Trim();
                short value = 0;
                if (!short.TryParse(ele.InnerText, out value))
                {
                    break;
                }

                switch (ele.Name)
                {
                    case OutputSignalTypeNode:
                        rtaParam.outputSignalTypeIndex = value;
                        break;

                    case PinkNoiseLevelNode:
                        rtaParam.pinkNoiseLevel.level = value;
                        break;

                    case PinkSweepLevelNode:
                        rtaParam.pinkSweepLevel.level = value;
                        break;

                    case SineLevelNode:
                        rtaParam.sineLevel.level = value;
                        break;

                    case SineFreqNode:
                        rtaParam.sineFreq.freq = value;
                        break;

                    case DualSine1LevelNode:
                        rtaParam.dualSine1Level.level = value;
                        break;

                    case DualSine1FreqNode:
                        rtaParam.dualSine1Freq.freq = value;
                        break;

                    case DualSine2LevelNode:
                        rtaParam.dualSine2Level.level = value;
                        break;

                    case DualSine2FreqNode:
                        rtaParam.dualSine2Freq.freq = value;
                        break;
                }
            }
        }

        public void ResetConnectionMethod()
        {
            _connectionMethod = ConnectionMethod.Default;
        }

        public bool CanBeUncheckedConnectionMethod(ConnectionMethod method)
        {
            method &= ConnectionMethod.All;

            return (this._connectionMethod & ~method) != ConnectionMethod.None;
        }

        public void RestoreCommParamToDefault()
        {
            ResetConnectionMethod();
            default_SerialPort = DefSerialPort;
            isSpManualMode = false;
            commRetries = DefaultCommRetries;
            udp_ip = DefUdpIPAddress;
            udp_port = DefUdpPort;
            tcp_ip = DefTCPIPAddress;
            tcp_port = DefTCPPort;

        }

        public void SaveToConfigFile()
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(xmldecl);

                //加入根节点 lmAEC
                XmlElement rootEle = xmldoc.CreateElement(RootNode);
                xmldoc.AppendChild(rootEle);

                rootEle.AppendChild(CreateLangNode(xmldoc));
                rootEle.AppendChild(CreateDefaultDeviceModelNode(xmldoc));
                rootEle.AppendChild(CreateUseLastDeviceModelWhenStartsNode(xmldoc));
                rootEle.AppendChild(CreateAutoConnectionNode(xmldoc));
                rootEle.AppendChild(CreateThresholdUnitNode(xmldoc));
                rootEle.AppendChild(CreateMixerLevelUnittNode(xmldoc));
                rootEle.AppendChild(CreateChannelDelayUnittNode(xmldoc));
                rootEle.AppendChild(CreateTemperatureNodeNode(xmldoc));
                rootEle.AppendChild(CreateCommNode(xmldoc));
                rootEle.AppendChild(CreateRcNode(xmldoc));
                rootEle.AppendChild(CreateRtaNode(xmldoc));
                
                xmldoc.Save(configFilePath);
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("Config.SaveToConfigFile()...", ex);
            }
        }

        private XmlElement CreateRcNode(XmlDocument xmldoc)
        {
            XmlElement commEle = xmldoc.CreateElement(RcNode);

            commEle.AppendChild(CreateParamNode(xmldoc, RcServerIpNode, rcConfig.serverIP));
            commEle.AppendChild(CreateParamNode(xmldoc, RcServerPortNode, rcConfig.serverPort));

            return commEle;
        }

        private XmlElement CreateCommNode(XmlDocument xmldoc)
        {
            XmlElement commEle = xmldoc.CreateElement(CommNode);

            commEle.AppendChild(CreateParamNode(xmldoc, ConnectionMethodNode, ((int)connectionMethod).ToString()));
            commEle.AppendChild(CreateParamNode(xmldoc, DefaultSerialPortNode, default_SerialPort));
            commEle.AppendChild(CreateParamNode(xmldoc, SpManualModeNode, isSpManualMode ? "1" : "0"));

            XmlElement udpEle = xmldoc.CreateElement(UdpNode);
            udpEle.AppendChild(CreateParamNode(xmldoc, IpNode, udp_ip));
            udpEle.AppendChild(CreateParamNode(xmldoc, PortNode, udp_port.ToString()));
            commEle.AppendChild(udpEle);

            XmlElement tcpEle = xmldoc.CreateElement(TcpNode);
            tcpEle.AppendChild(CreateParamNode(xmldoc, IpNode, tcp_ip));
            tcpEle.AppendChild(CreateParamNode(xmldoc, PortNode, tcp_port.ToString()));
            commEle.AppendChild(tcpEle);

            commEle.AppendChild(CreateParamNode(xmldoc, CommRetriesNode, this.commRetries.ToString()));
            
            return commEle;
        }

        private XmlElement CreateLangNode(XmlDocument xmldoc)
        {
            XmlElement langEle = xmldoc.CreateElement(LangNode);
            langEle.InnerText = localization.langCode;
            return langEle;
        }

        private XmlElement CreateDefaultDeviceModelNode(XmlDocument xmldoc)
        {
            XmlElement defaultDeviceModelEle = xmldoc.CreateElement(DefaultDeviceModel);
            defaultDeviceModelEle.InnerText = this.defaultDevcieModel;
            return defaultDeviceModelEle;
        }

        private XmlElement CreateUseLastDeviceModelWhenStartsNode(XmlDocument xmldoc)
        {
            XmlElement useLastDeviceModelEle = xmldoc.CreateElement(UseLastDeviceModelWhenStarts);
            useLastDeviceModelEle.InnerText = useLastConnectedModelWhenStarts ? "1" : "0";
            return useLastDeviceModelEle;
        }

        private XmlElement CreateAutoConnectionNode(XmlDocument xmldoc)
        {
            XmlElement autoConnectionEle = xmldoc.CreateElement(AutoConnectionNode);
            autoConnectionEle.InnerText = isAutoConnection ? "1" : "0";
            return autoConnectionEle;
        }

        private XmlElement CreateThresholdUnitNode(XmlDocument xmldoc)
        {
            XmlElement unitEle = xmldoc.CreateElement(LimiterThresholdUnitNode);
            unitEle.InnerText = this._thresholdUnit.ToString();
            return unitEle;
        }

        private XmlElement CreateMixerLevelUnittNode(XmlDocument xmldoc)
        {
            XmlElement unitEle = xmldoc.CreateElement(MixerLevelUnitNode);
            unitEle.InnerText = this._mixerLevelUnit.ToString();
            return unitEle;
        }

        private XmlElement CreateChannelDelayUnittNode(XmlDocument xmldoc)
        {
            XmlElement unitEle = xmldoc.CreateElement(ChannelDelayUnitNode);
            unitEle.InnerText = this._channelDelayUnit.ToString();
            return unitEle;
        }

        private XmlElement CreateTemperatureNodeNode(XmlDocument xmldoc)
        {
            XmlElement unitEle = xmldoc.CreateElement(TemperatureNode);
            unitEle.InnerText = this.temperature.ToString();
            return unitEle;
        }

        private XmlElement CreateRtaNode(XmlDocument xmldoc)
        {
            XmlElement rtaEle = xmldoc.CreateElement(RtaNode);

            rtaEle.AppendChild(CreateParamNode(xmldoc, RtaModeNode, rtaParam.rtaModeIndex));

            XmlElement audioIOEle = xmldoc.CreateElement(AudioIONode);
            audioIOEle.AppendChild(CreateParamNode(xmldoc, InputDeviceNode, rtaParam.inputDeviceDescription));
            audioIOEle.AppendChild(CreateParamNode(xmldoc, OutputDeviceNode, rtaParam.outputDeviceDescription));
            audioIOEle.AppendChild(CreateParamNode(xmldoc, SampleRateNode, rtaParam.sampleRate));
            audioIOEle.AppendChild(CreateParamNode(xmldoc, SampleBitDepthNode, rtaParam.sampleBitDepth));
            rtaEle.AppendChild(audioIOEle);

            XmlElement spEle = xmldoc.CreateElement(SpNode);
            spEle.AppendChild(CreateParamNode(xmldoc, SpFftSizeNode, (int)rtaParam.spFftSize));
            spEle.AppendChild(CreateParamNode(xmldoc, SpWindowNode, (int)rtaParam.spWindow));
            spEle.AppendChild(CreateParamNode(xmldoc, SpAveragerNode, (int)rtaParam.spAverageMode));
            spEle.AppendChild(CreateParamNode(xmldoc, SpScaleNode, (int)rtaParam.spScale));
            spEle.AppendChild(CreateParamNode(xmldoc, SpGraphNode, (int)rtaParam.spMap));
            rtaEle.AppendChild(spEle);

            XmlElement tfEle = xmldoc.CreateElement(TfNode);
            tfEle.AppendChild(CreateParamNode(xmldoc, TfFftSizeNode, (int)rtaParam.tfFftSize));
            tfEle.AppendChild(CreateParamNode(xmldoc, TfWindowNode, (int)rtaParam.tfWindow));
            tfEle.AppendChild(CreateParamNode(xmldoc, TfAveragerNode, (int)rtaParam.tfAverageMode));
            tfEle.AppendChild(CreateParamNode(xmldoc, TfSmootherNode, (int)rtaParam.tfSmooth));
            tfEle.AppendChild(CreateParamNode(xmldoc, TfAmpOffsetNode, rtaParam.tfMagOffsetIndex));
            tfEle.AppendChild(CreateParamNode(xmldoc, TfGraphNode, (int)rtaParam.freqRespMap));
            rtaEle.AppendChild(tfEle);

            rtaEle.AppendChild(CreateParamNode(xmldoc, DelayNode, rtaParam.delay.ToString("0.00")));

            XmlElement outputSignalEle = xmldoc.CreateElement(OutputSignalNode);
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, OutputSignalTypeNode, (int)rtaParam.outputSignalType));
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, PinkNoiseLevelNode, rtaParam.pinkNoiseLevel.level));
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, PinkSweepLevelNode, rtaParam.pinkSweepLevel.level));
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, SineLevelNode, rtaParam.sineLevel.level));
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, SineFreqNode, rtaParam.sineFreq.freq));
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, DualSine1LevelNode, rtaParam.dualSine1Level.level));
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, DualSine1FreqNode, rtaParam.dualSine1Freq.freq));
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, DualSine2LevelNode, rtaParam.dualSine2Level.level));
            outputSignalEle.AppendChild(CreateParamNode(xmldoc, DualSine2FreqNode, rtaParam.dualSine2Freq.freq));
            rtaEle.AppendChild(outputSignalEle);

            return rtaEle;
        }

        private static ConnectionMethod UInt2ConnectionMethod(uint value)
        {
            value &= (uint)AllConnectionMethodFlag;
            if (value == (uint)ConnectionMethod.None)
            {
                return ConnectionMethod.Default;
            }

            return (ConnectionMethod)value;
        }

        private static XmlElement CreateParamNode(XmlDocument xmldoc, string name, short param)
        {
            XmlElement ele = xmldoc.CreateElement(name);
            ele.InnerText = param.ToString();
            return ele;
        }

        private static XmlElement CreateParamNode(XmlDocument xmldoc, string name, int param)
        {
            XmlElement ele = xmldoc.CreateElement(name);
            ele.InnerText = param.ToString();
            return ele;
        }

        private static XmlElement CreateParamNode(XmlDocument xmldoc, string name, string param)
        {
            XmlElement ele = xmldoc.CreateElement(name);
            ele.InnerText = param.ToString();
            return ele;
        }

        private static void TryParseText2Int(string text, ref int value)
        {
            if (int.TryParse(text, out value))
            {
               
            }
        }

        private static void TryParseText2Short()
        {
            
        }
    }

    //public enum ConnectionMethod
    //{
    //    SerialPortAndUdp = 0,
    //    OnlySerialPort,
    //    OnlyUdp,
    //    UdpAndSerialPort,
    //}
    
    [Flags]
    public enum ConnectionMethod : uint
    {
        None = 0x00000000,
        USB = 0x00000001,
        RS232 = 0x00000002,
        UDP = 0x00000004,
        TCP = 0x00000008,
        Default = RS232 | UDP,
        All = USB | RS232 | UDP | TCP,
    }

    public enum ThresholdUnit
    {
        IndB,
        InV,
    }

    public enum MixerLevelUnit
    {
        InPercentage,
        IndB,
    }

    public enum ChannelDelayUnit
    {
        InMilliseconds,   //毫秒
        InMeter,    //米
    }
}
