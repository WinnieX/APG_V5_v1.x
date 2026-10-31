using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;

namespace AEC.Common
{
    public class SystemModuleBase
    {
        public const int DefModuleIndex = 0x0a;
        public const int DefDeviceModeCount = 9;
        public const int DefDeviceNameSize = 0x20;
        public const int DefUsageLimitLockCount = 5;
        public const int DeviceModeNameLength = 20;
        public const int KeyLockPasswordLength = 4;
        public const int SystemPasswordLength = 6;
        public const int DeviceIdLength = 4;

        public const short MusicVolumeMax = 82;
        public const short MicVolumeMax = 82;
        public const short EffectVolumeMax = 70;

        public const short TimbreMax = 100;
        public const short DeviceWorkModeMax = 1;
        public const short DeviceClarityMax = 5;

        public const short DefMusicInputGainMax = 2;
        public const short DefMusicInputPortMax = 1;

        private const string DefSystemPassword = "000000";
        private const string DefKeyLockPassword = "0000";

        public readonly byte moduleIndex;
        
        public readonly int deviceModeCount;

        public readonly short musicInputGainMax;
        public readonly short musicInputPortMax;

        private string[] _deviceModeList = null;    //模式名

        //private short _deviceModeSave = 0x00;        //设备模式保存
        //private short _deviceModeCall = 0x00;        //设备模式调用
        public readonly ShortParam deviceModeSaveIndex;
        public readonly ShortParam deviceModeCallIndex;

        public readonly ShortParam musicInputGain;

        private readonly Char[] _systemPassword = DefSystemPassword.ToCharArray();
        private readonly Char[] _keyboardPassword = DefKeyLockPassword.ToCharArray();

        public readonly ShortParam musicStartVolume;
        public readonly ShortParam musicMaxVolume;
        public readonly ShortParam micStartVolume;
        public readonly ShortParam micMaxVolume;
        public readonly ShortParam effectStartVolume;
        public readonly ShortParam effectMaxVolume;

        public readonly ShortParam user;
        public readonly BoolParam isLockKeyboard;

        public readonly ShortParam musicInputPort;

        public readonly BoolParam isEnableRemote;

        public readonly ShortParam musicVolume;
        public readonly ShortParam micVolume;
        public readonly ShortParam effectVolume;

        public readonly ShortParam timbreControl;

        public readonly ShortParam deviceWorkMode;

        public readonly ShortParam clarity;
        private bool _isSupportClarity = true;

        public readonly byte[] deviceId = new byte[DeviceIdLength];
        public bool deviceIsActivated = false;
        protected short _deviceModelIdentify = 0;

        private const uint DefaultFirmwareVersion = 0xffffffff;
        private const uint DefaultFirmwareBuildNumber = 0xffffffff;

        public string devicePrototypeModel;
        public uint deviceFirmwareVersion = DefaultFirmwareVersion;
        public uint deviceFirmwareBuildNumber = DefaultFirmwareBuildNumber;
        //public readonly BoolParam isEnableExtendedFunction;
        public readonly ExtendedFunctionState extendedFunctionState;
        public readonly ExtendedFunctionData extendedFunctionData;
        
        //public bool deviceIsActivated = false;
        private uint _deviceHardwareState = 0;
        private bool _isSupportDeviceName = false;
        private string _deviceName = string.Empty;
        private bool _isSupportUsageLimitLock = false;
        public readonly UsageLimitLock[] usageLimitLocks;

        private readonly EventBus effectorEvent;

        public string firmwareVersion
        {
            get
            {
                if(this.deviceFirmwareVersion != DefaultFirmwareVersion)
                {
                    byte[] data = BitConverter.GetBytes(this.deviceFirmwareVersion);
                    byte version_major = data[3];
                    byte version_minor = data[2];
                    UInt16 version_patch = BitConverter.ToUInt16(data, 0);

                    return string.Format("{0}.{1}.{2}", version_major, version_minor, version_patch);
                }

                return string.Empty;
            }
        }

        public string firmwareBuildNumber
        {
            get
            {
                if (this.deviceFirmwareBuildNumber != DefaultFirmwareBuildNumber)
                {
                    return deviceFirmwareBuildNumber.ToString();
                }

                return string.Empty;
            }
        }

        public bool dataIsPrivate = false;

        public BoolParam musicMute
        {
            get;
            protected set;
        }

        public BoolParam micMute
        {
            get;
            protected set;
        }

        public BoolParam effectMute
        {
            get;
            protected set;
        }

        public virtual bool isSupportDataEncryption
        {
            get
            {
                return false;
            }
        }

        public bool isLockUserOperation
        {
            get
            {
                return this.currUser == User.Normal;
            }
        }

        public uint deviceHardwareState
        {
            get
            {
                return this._deviceHardwareState;
            }
            set
            {
                this._deviceHardwareState = value;
            }
        }

        public bool isSupportDeviceName
        {
            get
            {
                return this._isSupportDeviceName;
            }
            set
            {
                this._isSupportDeviceName = value;
                if (!this._isSupportUsageLimitLock)
                {
                    _deviceName = string.Empty;
                }
            }
        }

        public string deviceName
        {
            get
            {
                if(this._isSupportDeviceName)
                {
                    return this._deviceName;
                }

                return string.Empty;
            }
            set
            {
                this._deviceName = value;
            }
        }

        public bool isSupportUsageLimitLock
        {
            get
            {
                return this._isSupportUsageLimitLock;
            }
            set
            {
                this._isSupportUsageLimitLock = value;
                if (!this._isSupportUsageLimitLock)
                {
                    for (int i = 0; i < this.usageLimitLocks.Length; ++i)
                    {
                        this.usageLimitLocks[i].Reset();
                    }
                }
            }
        }

        public int usageLimitLocksCount
        {
            get
            {
                return this.usageLimitLocks.Length;
            }
        }

        public bool isUsageLimitLock
        {
            get
            {
                if(this.isSupportUsageLimitLock)
                {
                    foreach(UsageLimitLock usageLimitLock in this.usageLimitLocks)
                    {
                        if(usageLimitLock.enable && usageLimitLock.usageCount == 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public bool isSupportClarity
        {
            get
            {
                return this._isSupportClarity;
            }
            set
            {
                this._isSupportClarity = value;
                if (!this._isSupportClarity)
                {
                    this.clarity.initV = 0;
                }
            }
        }

        public List<string> GetUsageLimitLockPasswords(uint index, uint randKey)
        {
            Debug.Assert(index >= 0 && index < this.usageLimitLocks.Length);

            DateTime now = DateTime.UtcNow;
            uint timeStamp_0 = Tools.TimeStampHelper.GetTimeStampWithoutSecond(now);
            uint timeStamp_n1 = Tools.TimeStampHelper.GetTimeStampWithoutSecond(now - new TimeSpan(0, 1, 0));
            uint timeStamp_p1 = Tools.TimeStampHelper.GetTimeStampWithoutSecond(now + new TimeSpan(0, 1, 0));

            List<string> passwords = new List<string>();
            passwords.Add(this.usageLimitLocks[index].systemPasswordStr);
            passwords.Add(UsageLimitLockHelper.GetSuperPassword(this.deviceId, index, timeStamp_0));
            passwords.Add(UsageLimitLockHelper.GetSuperPassword(this.deviceId, index, timeStamp_n1));
            passwords.Add(UsageLimitLockHelper.GetSuperPassword(this.deviceId, index, timeStamp_p1));
            passwords.Add(UsageLimitLockHelper.GetSuperPassword(this.deviceId, index, randKey));

            return passwords;
        }

        public SystemModuleBase(byte index, EventBus effectorEvent_, int modeCount = DefDeviceModeCount, short musicInputGainMax_ = DefMusicInputGainMax, short musicInputPortMax_ = DefMusicInputPortMax)
        {
            Debug.Assert(modeCount > 0 && musicInputGainMax >= 0 && musicInputPortMax >= 0);

            this.moduleIndex = index;
            this.effectorEvent = effectorEvent_;

            this.deviceModeCount = modeCount;

            this.musicInputGainMax = musicInputGainMax_;
            this.musicInputPortMax = musicInputPortMax_;

            this._deviceModeList = new string[modeCount];
            for (int i = 0; i < this._deviceModeList.Length; ++i)
            {
                this._deviceModeList[i] = string.Empty;
            }

            this.usageLimitLocks = new UsageLimitLock[DefUsageLimitLockCount];
            for (short i = 0; i < this.usageLimitLocks.Length; ++i )
            {
                this.usageLimitLocks[i] = new UsageLimitLock(i);
            }

            this.deviceModeSaveIndex = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.deviceModeSave, (short)(this.deviceModeCount - 1));
            this.deviceModeCallIndex = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.deviceModeCall, (short)(this.deviceModeCount - 1));

            this.musicInputGain = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicInputGain, musicInputGainMax, this.OnParamChanged);

            this.musicStartVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicStartVolume, MusicVolumeMax, this.OnParamChanged);
            this.musicMaxVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicMaxVolume, MusicVolumeMax, this.OnParamChanged);
            this.micStartVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.micStartVolume, MicVolumeMax, this.OnParamChanged);
            this.micMaxVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.micMaxVolume, MicVolumeMax, this.OnParamChanged);
            this.effectStartVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.effectStartVolume, EffectVolumeMax, this.OnParamChanged);
            this.effectMaxVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.effectMaxVolume, EffectVolumeMax, this.OnParamChanged);

            this.user = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.user, (short)User.Admin, this.OnParamChanged);
            this.user.initV = (short)User.Admin;

            this.isLockKeyboard = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.isLockKeyboard, this.OnParamChanged);

            this.musicInputPort = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicInputPort, musicInputPortMax, this.OnParamChanged);

            this.isEnableRemote = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.isEnableRomoteControl, this.OnParamChanged);

            this.musicVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.musicVolume, MusicVolumeMax, this.OnParamChanged);
            this.micVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.micVolume, MicVolumeMax, this.OnParamChanged);
            this.effectVolume = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.effectVolume, EffectVolumeMax, this.OnParamChanged);

            this.timbreControl = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.timbre, TimbreMax, this.OnParamChanged);

            this.deviceWorkMode = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.deviceWorkMode, DeviceWorkModeMax, this.OnParamChanged);

            this.clarity = new ShortParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.clarity, DeviceClarityMax, this.OnParamChanged);

            this.musicMaxVolume.initV = MusicVolumeMax;
            this.micMaxVolume.initV = MicVolumeMax;
            this.effectMaxVolume.initV = EffectVolumeMax;

            this.devicePrototypeModel = string.Empty;

            //this.isEnableExtendedFunction = new BoolParam(this.moduleIndex, (byte)SystemCommProtocol.SystemParamAddr.massProductionDownloadPresetMode, this.OnParamChanged);

            this.extendedFunctionState = new ExtendedFunctionState();
            this.extendedFunctionData = new ExtendedFunctionData();
        }

        //public short deviceModeSave
        //{
        //    get
        //    {
        //        return this._deviceModeSave;
        //    }
        //    set
        //    {
        //        this._deviceModeSave = Utils.ConstrainMax(value, (short)(this.deviceModeCount - 1));
        //    }
        //}

        //public short deviceModeCall
        //{
        //    get
        //    {
        //        return this._deviceModeCall;
        //    }
        //    set
        //    {
        //        this._deviceModeCall = Utils.ConstrainMax(value, (short)(this.deviceModeCount - 1));
        //    }
        //}

        public string[] deviceModeList
        {
            get
            {
                return this._deviceModeList;
            }
        }

        public virtual void CopyFrom(SystemModuleBase other, bool isCopyAllSystem)
        {
            CopyFromWithoutWorkMode(other, isCopyAllSystem);
            this.deviceWorkMode.initV = other.deviceWorkMode.v;
            //this.musicStartVolume.initV = other.musicStartVolume.v;
            //this.musicMaxVolume.initV = other.musicMaxVolume.v;
            //this.micStartVolume.initV = other.micStartVolume.v;
            //this.micMaxVolume.initV = other.micMaxVolume.v;
            //this.effectStartVolume.initV = other.effectStartVolume.v;
            //this.effectMaxVolume.initV = other.effectMaxVolume.v;

            //this.musicVolume.initV = other.musicVolume.v;
            //this.micVolume.initV = other.micVolume.v;
            //this.effectVolume.initV = other.effectVolume.v;

            //this.timbreControl.initV = other.timbreControl.v;
            //this.deviceWorkMode.initV = other.deviceWorkMode.v;

            //this.musicInputPort.initV = other.musicInputPort.v;

            //if (isCopyAllSystem)
            //{
            //    for (int i = 0; i < this.deviceModeCount; ++i)
            //    {
            //        this._deviceModeList[i] = other._deviceModeList[i];
            //    }

            //    if (this.musicMute != null && other.musicMute != null)
            //    {
            //        this.musicMute.initV = other.musicMute.v;
            //    }
            //    if (this.micMute != null && other.micMute != null)
            //    {
            //        this.micMute.initV = other.micMute.v;
            //    }
            //    if (this.effectMute != null && other.effectMute != null)
            //    {
            //        this.effectMute.initV = other.effectMute.v;
            //    }

            //    this.deviceModeSaveIndex.initV = other.deviceModeSaveIndex.v;
            //    this.deviceModeCallIndex.initV = other.deviceModeCallIndex.v;

            //    Array.Copy(other._systemPassword, this._systemPassword, this._systemPassword.Length);
            //    Array.Copy(other._keyboardPassword, this._keyboardPassword, this._keyboardPassword.Length);

            //    this.user.initV = other.user.v;
            //    this.isLockKeyboard.initV = other.isLockKeyboard.v;
            //    this.musicInputGain.initV = other.musicInputGain.v;
            //    this.isEnableRemote.initV = other.isEnableRemote.v;

            //    this.deviceIsActivated = other.deviceIsActivated;

            //    Array.Copy(other.deviceId, this.deviceId, DeviceIdLength);
            //}

            //this.devicePrototypeModel = other.devicePrototypeModel;
            //this.deviceFirmwareVersion = other.deviceFirmwareVersion;
            //this.deviceFirmwareBuildNumber = other.deviceFirmwareBuildNumber;

            //this.extendedFunctionState.CopyFrom(other.extendedFunctionState);
            //this.extendedFunctionData.CopyFrom(other.extendedFunctionData);
        }

        public void CopyFromWithoutWorkMode(SystemModuleBase other, bool isCopyAllSystem)
        {
            this.musicStartVolume.initV = other.musicStartVolume.v;
            this.musicMaxVolume.initV = other.musicMaxVolume.v;
            this.micStartVolume.initV = other.micStartVolume.v;
            this.micMaxVolume.initV = other.micMaxVolume.v;
            this.effectStartVolume.initV = other.effectStartVolume.v;
            this.effectMaxVolume.initV = other.effectMaxVolume.v;

            this.musicVolume.initV = other.musicVolume.v;
            this.micVolume.initV = other.micVolume.v;
            this.effectVolume.initV = other.effectVolume.v;

            this.timbreControl.initV = other.timbreControl.v;
            this.clarity.initV = other.clarity.v;
            this.isSupportClarity = other.isSupportClarity;
            //this.deviceWorkMode.initV = other.deviceWorkMode.v;

            this.musicInputPort.max = other.musicInputPort.max;
            this.musicInputPort.initV = other.musicInputPort.v;

            if (isCopyAllSystem)
            {
                for (int i = 0; i < this.deviceModeCount; ++i)
                {
                    this._deviceModeList[i] = other._deviceModeList[i];
                }

                this._deviceHardwareState = other._deviceHardwareState;
                this._isSupportDeviceName = other._isSupportDeviceName;
                this._deviceName = other._deviceName;
                this._isSupportUsageLimitLock = other._isSupportUsageLimitLock;
                for (int i = 0; i < this.usageLimitLocks.Length; ++i )
                {
                    this.usageLimitLocks[i].CopyFrom(other.usageLimitLocks[i]);
                }

                if (this.musicMute != null && other.musicMute != null)
                {
                    this.musicMute.initV = other.musicMute.v;
                }
                if (this.micMute != null && other.micMute != null)
                {
                    this.micMute.initV = other.micMute.v;
                }
                if (this.effectMute != null && other.effectMute != null)
                {
                    this.effectMute.initV = other.effectMute.v;
                }

                this.deviceModeSaveIndex.initV = other.deviceModeSaveIndex.v;
                this.deviceModeCallIndex.initV = other.deviceModeCallIndex.v;

                Array.Copy(other._systemPassword, this._systemPassword, this._systemPassword.Length);
                Array.Copy(other._keyboardPassword, this._keyboardPassword, this._keyboardPassword.Length);

                this.user.initV = other.user.v;
                this.isLockKeyboard.initV = other.isLockKeyboard.v;
                this.musicInputGain.initV = other.musicInputGain.v;
                this.isEnableRemote.initV = other.isEnableRemote.v;

                this.deviceIsActivated = other.deviceIsActivated;

                Array.Copy(other.deviceId, this.deviceId, DeviceIdLength);
            }

            this.devicePrototypeModel = other.devicePrototypeModel;
            this.deviceFirmwareVersion = other.deviceFirmwareVersion;
            this.deviceFirmwareBuildNumber = other.deviceFirmwareBuildNumber;

            this.extendedFunctionState.CopyFrom(other.extendedFunctionState);
            this.extendedFunctionData.CopyFrom(other.extendedFunctionData);
        }

        protected virtual void OnParamChanged(object sender, DataParamChangedArgs e)
        {
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(e);
            }
        }

        protected virtual void OnKeyLockPasswordChanged(object sender, EventArgs e)
        {
            if (this.effectorEvent != null)
            {

            }
        }

        protected virtual void OnSystemPasswordChanged(object sender, EventArgs e)
        {
            if (this.effectorEvent != null)
            {

            }
        }

        public void ConstraintParam()
        {
            this.musicStartVolume.initV = AEC.Core.Utils.ConstrainMax(this.musicStartVolume.v, this.musicMaxVolume.v);
            this.micStartVolume.initV = AEC.Core.Utils.ConstrainMax(this.micStartVolume.v, this.micMaxVolume.v);
            this.effectStartVolume.initV = AEC.Core.Utils.ConstrainMax(this.effectStartVolume.v, this.effectMaxVolume.v);

            this.musicVolume.initV = AEC.Core.Utils.ConstrainMax(this.musicVolume.v, this.musicMaxVolume.v);
            this.micVolume.initV = AEC.Core.Utils.ConstrainMax(this.micVolume.v, this.micMaxVolume.v);
            this.effectVolume.initV = AEC.Core.Utils.ConstrainMax(this.effectVolume.v, this.effectMaxVolume.v);

            //this.ConstraintMusicInputParam();
        }

        public void UpdateCurrVolume()
        {
            this.musicStartVolume.initV = AEC.Core.Utils.ConstrainMax(this.musicStartVolume.v, this.musicMaxVolume.v);
            this.micStartVolume.initV = AEC.Core.Utils.ConstrainMax(this.micStartVolume.v, this.micMaxVolume.v);
            this.effectStartVolume.initV = AEC.Core.Utils.ConstrainMax(this.effectStartVolume.v, this.effectMaxVolume.v);

            this.musicVolume.initV = this.musicStartVolume.v;
            this.micVolume.initV = this.micStartVolume.v;
            this.effectVolume.initV = this.effectStartVolume.v;

            //this.ConstraintMusicInputParam();
        }

        //protected virtual void ConstraintMusicInputParam()
        //{

        //}

        //public static string GetVolumeLabel(short index)
        //{
        //    return index.ToString();
        //}

        public string keyboardLockPassword
        {
            get
            {
                return new string(_keyboardPassword);
            }
            set
            {
                keyboardLockPasswordInit = value;
                if (this.effectorEvent != null)
                {
                    this.effectorEvent.NotifyObeserver(new PasswordChangedArgs((byte)SystemCommProtocol.SystemParamAddr.keyboardLockPassword, keyboardLockPassword));
                }
            }
        }

        public string keyboardLockPasswordInit
        {
            set
            {
                if (value.Length == KeyLockPasswordLength)
                {
                    if (AEC.Tools.StringHelper.StringIsDigit(value))
                    {
                        Array.Copy(value.ToCharArray(), _keyboardPassword, KeyLockPasswordLength);
                    }
                    else
                    {
                        Array.Copy(DefSystemPassword.ToCharArray(), _keyboardPassword, KeyLockPasswordLength);
                    }
                }
                else
                {
                    throw (new Exception("键盘密码长度应该为 6 位"));
                }

                //AEC.Log.OutputLine("AEC键盘锁密码{0}", keyboardLockPassword);
            }
        }

        public string systemPassword
        {
            get
            {
                return new string(_systemPassword);
            }
            set
            {
                systemPasswordInit = value;
                if (this.effectorEvent != null)
                {
                    this.effectorEvent.NotifyObeserver(new PasswordChangedArgs((byte)SystemCommProtocol.SystemParamAddr.systemPassword, systemPassword));
                }
            }
        }

        public string systemPasswordInit
        {
            get
            {
                return new string(_systemPassword);
            }
            set
            {
                if(value.Length == SystemPasswordLength)
                {
                    if (AEC.Tools.StringHelper.StringIsDigit(value))
                    {
                        Array.Copy(value.ToCharArray(), _systemPassword, SystemPasswordLength);
                    }
                    else
                    {
                        Array.Copy(DefSystemPassword.ToCharArray(), _systemPassword, SystemPasswordLength);
                    }
                }
                else
                {
                    throw(new Exception("系统密码长度应该为 6 位"));
                }
            }
        }

        public User currUser
        {
            get
            {
                if(this.user.v != 0)
                {
                    return User.Admin;
                }
                else
                {
                    return User.Normal;
                }
            }
            set
            {
                if(value == User.Normal)
                {
                    this.user.v = 0;
                }
                else
                {
                    this.user.v = 1;
                }
            }
        }

        public string deviceIdCode
        {
            get
            {
                return AEC.Core.Utils.BytesToHexStr(deviceId);
            }
        }

        public void UpdateDeviceName(string name)
        {
            this.deviceName = name;
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(new DeviceNameChangedArgs(this.deviceName));
            }
        }

        public void UpdateUsageLimitLock(UsageLimitLock usageLimitLock)
        {
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(new UsageLimitLockUpdatedArgs(usageLimitLock));
            }
        }

        public void SaveDeviceMode(short modeIndex, string name)
        {
            if (modeIndex >= 0 && modeIndex < DeviceModeNameLength)
            {
                if (name.Length > DeviceModeNameLength)
                {
                    name = name.Substring(0, DeviceModeNameLength);
                }

                this._deviceModeList[modeIndex] = name;

                if (this.effectorEvent != null)
                {
                    this.effectorEvent.NotifyObeserver(new SaveDeviceModeArgs(modeIndex, this._deviceModeList[modeIndex]));
                }
            }
        }

        public void UpdateCustomDeviceInfoEnable(bool enable)
        {
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(new UpdateCustomDeviceInfoEnableArgs(enable));
            }
        }

        public void UpdateDeviceInfoCustomManufacturer(string manufacturer)
        {
            UpdateCustomDeviceInfoData(SystemCommProtocol.SystemCustomDeviceInfoData.custom_manufacturer_data, manufacturer);
        }

        public void UpdateDeviceInfoCustomModel(string model)
        {
            UpdateCustomDeviceInfoData(SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_model_data, model);
        }

        public void UpdateDeviceInfoCustomVersion(string version)
        {
            UpdateCustomDeviceInfoData(SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_version_data, version);
        }

        public void UpdateDeviceInfoCustomDescription(string description)
        {
            UpdateCustomDeviceInfoData(SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_description_data, description);
        }

        private void UpdateCustomDeviceInfoData(SystemCommProtocol.SystemCustomDeviceInfoData efd, Object data)
        {
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(new UpdateCustomDeviceInfoDataArgs(efd, data));
            }
        }

#if COMM_TEST
        public readonly static Random random = new Random();

        public virtual void RandomTest()
        {
            this.musicInputGain.RandomTest();
            this.musicMaxVolume.RandomTest();
            this.musicStartVolume.RandomTest(this.musicMaxVolume.v);
            this.micMaxVolume.RandomTest();
            this.micStartVolume.RandomTest(this.micMaxVolume.v);
            this.effectMaxVolume.RandomTest();
            this.effectStartVolume.RandomTest(this.effectMaxVolume.v);
            this.user.RandomTest();
            this.isLockKeyboard.RandomTest();
            this.musicInputPort.RandomTest();
            this.isEnableRemote.RandomTest();
            this.musicVolume.RandomTest();
            this.micVolume.RandomTest();
            this.effectVolume.RandomTest();
            this.timbreControl.RandomTest();

            if (this.musicMute != null)
            {
                this.musicMute.RandomTest();
            }
            if (this.micMute != null)
            {
                this.micMute.RandomTest();
            }
            if (this.effectMute != null)
            {
                this.effectMute.RandomTest();
            }

            this.keyboardLockPassword = random.Next(10000).ToString("D4");
            this.systemPassword = random.Next(1000000).ToString("D6");

            AEC.Log.OutputLine("更新键盘锁密码:{0}", this.keyboardLockPassword);
            AEC.Log.OutputLine("更新系统密码:{0}", this.systemPassword);

            if (this.isLockKeyboard.value)
            {
                this.isLockKeyboard.value = false;
            }
        }
#endif
    }
}
