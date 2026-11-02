using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Common;

namespace AEC.Comm
{
    public enum CommCmdType
    {
        Read = 0,
        Write,
        Unknown,
    }

    public class CommProtocolBase
    {
        protected enum DataFrame : uint
        {
            HeaderIndex             = 0x00,
            IdentityIndex           = 0x01,
            LengthLowIndex          = 0x02,
            //LengthHighIndex         = 0x03,
            CmdTypeIndex            = 0x03,
            ModuleIndex             = 0x04,
            DataAddressIndex        = 0x05,
            ValidDataStartIndex     = 0x06,
        };

        protected const byte Header = 0xFC;
        protected const byte ReadCmd = 0xA0;                 //读取命令
        protected const byte WriteCmd = 0xA7;                //写入命令
        //protected const byte DataLength = 0x09;              //数据长度
        protected const byte ReadModuleCmdLength = 0x09;        //读取模块数据长度
        protected const byte ReadSystemParamCmdLength = 0x07;
        protected const byte ReadSystemModeNameParamCmdLength = 0x09;
        protected const byte ReadSystemUsageLimitLockCmdLength = 0x09;
        protected const byte ReadActivationCodeCmdLength = 0x09;
        protected const byte ConnectCmdLength = 0x09; 
        protected const byte ReadCmdMinLength = 0x07;         //读取命令最短长度
        protected const byte WriteCmdMinLength = 0x09;        //写入命令最短长度
        protected const byte ReadResponseMinLength = 0x09;    //读取数据响应最短长度
        protected const byte DefReadDataCmdLength = 0x09;              //数据长度
        protected const byte DefWriteDataCmdLength = 0x09;              //数据长度
        protected const byte BeforCmdLength = 0x03;           //控制命令之前的4个字节:起始标志\身份标识\数据包长度
        protected const byte ReadSysCmdLength = 0x07;
        protected const byte WriteSysCmdLength = 0x09;
        protected const byte WriteSysKeyCmdLength = 0x0B;
        protected const byte WriteSystemWorkModeCmdLength = 0x0D;
        
        protected const byte SystemFrontLevelDataTotalLength = 0x0C;
        protected const byte SystemEndLevelDataTotalLength = 0x10;
        
        protected const byte SystemLevelDataLength = 0x04;
        protected const byte ModuleValidDataStartIndex = 0x06;
        protected const byte ValidDataStartIndex = 0x06;
        protected const byte ReadSystemResponseDataIndex = 0x05;
        protected const byte ModuleIndexLength = 0x02;
        protected const byte WriteModuleParamCmdLength = 0x09;
        protected const byte WriteEQCmdLength = 0x013;
        protected const byte WriteModuleDEQParamCmdLength = 0x0b;

        protected const float ZerodBuLevel = -512;

        protected const byte ConnectionStateAddr = (byte)SystemCommProtocol.SystemParamAddr.connectionState;

        protected const int SingleDataSize = 2;
        protected const int DualDataSize = SingleDataSize + SingleDataSize;
         
        public const int HeartbeatTimerInterval = 2048;
        public const int TimerError = 64;
        public const int HeartbeatInterval = HeartbeatTimerInterval + TimerError;
        public const int WriteDeviceModeWaitTime = 2048;

        private const byte DefaultIdentity = 0x31;
        public const byte DefaultSystemModuleIndex = 0x0A;

        private readonly byte identity = DefaultIdentity;
        private readonly byte systemModuleIndex = DefaultSystemModuleIndex;

        public CommProtocolBase()
        {
            
        }

        #region 通讯命令
        public CommCmd GetHeartbeatCmd()
        {
            byte[] cmd = heartbeatCmd;

            int identity = (byte)SystemCommProtocol.SystemParamAddr.connectionState + (this.systemModuleIndex << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd GetReadSystemStatusCmd()
        {
            byte[] cmd = GetReadSystemDataCmd(SystemCommProtocol.SystemParamAddr.systemStatus);

            int identity = (byte)SystemCommProtocol.SystemParamAddr.systemStatus + (this.systemModuleIndex << 16);

            return new CommCmd(cmd, identity, CmdType.ReadSystemStatus);
        }

        public CommCmd GetWriteModuleParamCmd(ShortParam param)
        {
            byte[] cmd = GetWriteCmd(WriteModuleParamCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = param.moduleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = param.addr;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(param.v);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = GetHighByte(param.v);
            GenCheckSum(cmd);

            int identity = param.addr + (param.moduleIndex << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd GetWriteEQParamCmd(byte moduleIndex, byte addr, EQ eq)
        {
            byte[] cmd = GetWriteCmd(WriteEQCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = moduleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = addr;
            byte[] eqBuffer = EQ2ByteBuffer(eq);
            Array.Copy(eqBuffer, 0, cmd, CommProtocolBase.ValidDataStartIndex, eqBuffer.Length);
            GenCheckSum(cmd);

            int identity = addr + (eq.index << 8) + (moduleIndex << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd GetWriteDEQParamCmd(byte moduleIndex, byte addr, short index, ShortParam param)
        {
            byte[] cmd = GetWriteCmd(WriteModuleDEQParamCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = moduleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = addr;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(index);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x00;
            cmd[(uint)DataFrame.ValidDataStartIndex + 2] = GetLowByte(param.v);
            cmd[(uint)DataFrame.ValidDataStartIndex + 3] = GetHighByte(param.v);
            GenCheckSum(cmd);

            int identity = addr + (index << 8) + (moduleIndex << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd GetWritePasswordCmd(byte addr, string passowrd)
        {
            byte[] passowrdData = SystemCommProtocol.PassowrdEncode2PassowrdData(passowrd);
            int cmdLength = (int)DataFrame.ValidDataStartIndex + passowrdData.Length + 1;
            byte[] cmd = GetWriteCmd(cmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = addr;
            Array.Copy(passowrdData, 0, cmd, CommProtocolBase.ValidDataStartIndex, passowrdData.Length);
            GenCheckSum(cmd);

            int identity = addr + (systemModuleIndex << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd GetSaveDeviceModeCmd(short modeIndex, string modeName, CommCmd.VoidCallback StartSendCmd, CommCmd.VoidCallback SendCmdComplete)
        {
            byte[] cmd = GetSaveDeviceModeCmd(modeIndex, modeName);

            int identity = cmd[(uint)DataFrame.DataAddressIndex] + (systemModuleIndex << 16);

            return new CommCmd(cmd, identity, commWaitTime_: WriteDeviceModeWaitTime, StartSendCmd_: StartSendCmd, SendCmdComplete_: SendCmdComplete);
        }

        public CommCmd UpdateNonSysteModuleDataCmd(short moduleIndex, List<short> moduleData)
        {
            byte[] cmd = GetWriteNonSysteModuleCmd(moduleIndex, moduleData);
            int identity = cmd[(uint)DataFrame.DataAddressIndex] + (moduleIndex << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd UpdateCustomDeviceInfoEnableCmd(bool enable)
        {
            byte[] cmd = GetWriteCustomDeviceInfoEnableCmd(enable);
            int identity = cmd[(uint)DataFrame.DataAddressIndex] + ((short)cmd[4] << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd UpdateCustomDeviceInfoData(SystemCommProtocol.SystemCustomDeviceInfoData efd, Object data)
        {
            byte[] cmd = GetWriteCustomDeviceInfoDataCmd(efd, data);

            int identity = cmd[(uint)DataFrame.DataAddressIndex] + ((short)efd << 8) + ((short)cmd[4] << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd UpdateDeviceName(string name)
        {
            byte[] cmd = GetWriteDeviceNameCmd(name);

            int identity = cmd[(uint)DataFrame.DataAddressIndex] + (systemModuleIndex << 16);

            return new CommCmd(cmd, identity);
        }

        public CommCmd UpdateUsageLimitLockCmd(UsageLimitLock usageLimitLock, CommCmd.VoidCallback SendCmdCompleted)
        {
            byte[] cmd = GetUpdateUsageLimitLockCmd(usageLimitLock);

            int identity = cmd[(uint)DataFrame.DataAddressIndex] + (usageLimitLock.index << 8) + (systemModuleIndex << 16);

            return new CommCmd(cmd, identity, SendCmdComplete_: SendCmdCompleted, context_: usageLimitLock);
        }

        #endregion

        #region 解析数据
        protected virtual bool ParseSystemSingleData(byte addr, byte[] validData, SystemModuleBase system)
        {
            short value = GetShortFromDualBytes(validData, 0);

            switch (addr)
            {
                case (byte)SystemCommProtocol.SystemParamAddr.deviceModeSave:
                    system.deviceModeSaveIndex.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.deviceModeCall:
                    //AEC.Log.OutputLine("deviceModeCall:{0}", value);
                    system.deviceModeCallIndex.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.musicInputGain:
                    system.musicInputGain.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.systemPassword:
                    byte[] sysPasswordData = validData;
                    if (sysPasswordData.Length == SystemModuleBase.SystemPasswordLength)
                    {
                        string password = SystemCommProtocol.PasswordDataDecode2Password(sysPasswordData);
                        system.systemPasswordInit = password;
                        //Array.Copy(password, system._systemPassword, SystemModuleBase.SystemPasswordLength);

                        //return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.keyboardLockPassword:
                    byte[] keyPasswordData = validData;
                    if (keyPasswordData.Length == SystemModuleBase.KeyLockPasswordLength)
                    {
                        string password = SystemCommProtocol.PasswordDataDecode2Password(keyPasswordData);
                        system.keyboardLockPasswordInit = password;
                        //Array.Copy(password, system._keyboardPassword, SystemModuleBase.KeyLockPasswordLength);

                        //return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.musicStartVolume:
                    system.musicStartVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.musicMaxVolume:
                    system.musicMaxVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.micStartVolume:
                    system.micStartVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.micMaxVolume:
                    system.micMaxVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.effectStartVolume:
                    system.effectStartVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.effectMaxVolume:
                    system.effectMaxVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.user:
                    system.user.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.isLockKeyboard:
                    system.isLockKeyboard.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.musicInputPort:
                    //Log.OutputLine("music input port = {0}", value);
                    system.musicInputPort.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.isEnableRomoteControl:
                    system.isEnableRemote.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.musicVolume:
                    system.musicVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.micVolume:
                    system.micVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.effectVolume:
                    system.effectVolume.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.timbre:
                    system.timbreControl.initV = value;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.deviceWorkMode:
                    system.deviceWorkMode.initV = value;
                    //Log.OutputLine("system device work mode = {0}, system hash:{1}", value, AEC.Core.Utils.Uint32ToHexStr((uint)system.GetHashCode()));
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.clarity:
                    system.clarity.initV = value;
                    system.isSupportClarity = true;
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.musicMute:
                    if (system.musicMute != null)
                    {
                        value = (short)(value & 0x00ff);
                        system.musicMute.initV = value;
                    }
                    else
                    {
                        return false;
                    }
                    //AEC.Log.OutputLine("音乐静音:{0}", system.musicMute.value);
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.micMute:
                    if (system.micMute != null)
                    {
                        value = (short)(value & 0x00ff);
                        system.micMute.initV = value;
                    }
                    else
                    {
                        return false;
                    }
                    //AEC.Log.OutputLine("麦克风静音:{0}", system.micMute.value);
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.effectMute:
                    if (system.effectMute != null)
                    {
                        value = (short)(value & 0x00ff);
                        system.effectMute.initV = value;
                    }
                    else
                    {
                        return false;
                    }
                    //AEC.Log.OutputLine("效果静音:{0}", system.effectMute.value);
                    break;


                case (byte)SystemCommProtocol.SystemParamAddr.deviceId:
                    byte[] idData = validData;
                    if (idData.Length == SystemModuleBase.DeviceIdLength)
                    {
                        Array.Copy(idData, system.deviceId, SystemModuleBase.DeviceIdLength);
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.prototypeModel:
                    if (validData.Length == SystemCommProtocol.PrototypeModelLength)
                    {
                        system.devicePrototypeModel = Encoding.ASCII.GetString(validData).TrimEnd('\0');
                        //Log.OutputLine("设备原型:{0}", system.devicePrototypeModel);
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.firmwareVersion:
                    if (validData.Length == SystemCommProtocol.FirmwareVersionLength)
                    {
                        system.deviceFirmwareVersion = GetUintFromQuadBytes(validData);
                        //Log.OutputLine("固件版本号:{0}", system.deviceFirmwareVersion.ToString("X").PadLeft(8, '0'));
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.firmwareBuildNumbers:
                    if (validData.Length == SystemCommProtocol.FirmwareBuildVersionLength)
                    {
                        system.deviceFirmwareBuildNumber = GetUintFromQuadBytes(validData);
                        //Log.OutputLine("固件构建号:{0}", system.deviceFirmwareBuildNumber.ToString("X").PadLeft(8, '0'));
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.deviceHardwareState:
                    if (validData.Length == sizeof(short))
                    {
                        system.deviceHardwareState = (uint)value;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.deviceName:
                    if (validData.Length == sizeof(short))
                    {
                        system.isSupportDeviceName = false;
                    }
                    else if (validData.Length >= SystemModuleBase.DefDeviceNameSize)
                    {
                        system.isSupportDeviceName = true;
                        system.deviceName = Encoding.UTF8.GetString(validData).TrimEnd('\0');
                        //if (string.IsNullOrEmpty(system.deviceName))
                        //{
                        //    system.deviceName = "Device X";
                        //}
                        //Log.OutputLine("deviceName = {0}", system.deviceName);
                    }
                    else
                    {
                        system.isSupportDeviceName = false;
                        return false;
                    }
                    break;

                case (byte)SystemCommProtocol.SystemParamAddr.usageLimitLock:
                    if (validData.Length == sizeof(short))
                    {
                        system.isSupportUsageLimitLock = DataConverter.Short2Bool(value);
                    }
                    else if (validData.Length == UsageLimitLock.DataLength * SystemModuleBase.DefUsageLimitLockCount)
                    {
                        for (short i = 0; i < SystemModuleBase.DefUsageLimitLockCount; ++i)
                        {
                            int startIndex = UsageLimitLock.DataLength * i;
                            if (!SystemCommProtocol.ParseDataToUsageLimitLockData(validData, startIndex, system.usageLimitLocks[i]))
                            {
                                return false;
                            }
                            //Log.OutputLine("UsageLimitLock[{0}] : {1}", i, system.usageLimitLocks[i].ToString());
                        }
                        system.isSupportUsageLimitLock = true;
                    }
                    else
                    {
                        system.isSupportUsageLimitLock = false;
                        return false;
                    }
                    break;

                default:
                    return false;
            }
            
            return true;
        }

        public bool ParseSystemData(byte[] buffer, SystemModuleBase system)
        {
            //AEC.Log.OutputLine("ParseSystemData()...");
            if (buffer.Length < ReadResponseMinLength || buffer[BeforCmdLength] != ReadCmd || buffer[(uint)DataFrame.ModuleIndex] != system.moduleIndex)
            {
                return false;
            }
            
            //AEC.Log.OutputLine("CheckReadResponseIsOk()...");
            if (!CommProtocolBase.CheckReadResponseIsOk(buffer))
            {
                Tools.LogHelper.WriteInfo("Failed to detect response data!");
                return false;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...完成");

            byte addr = buffer[ReadSystemResponseDataIndex];            
            byte[] validData = GetSystemModuleData(buffer);

            if (validData == null || validData.Length < 2)
            {
                return false;
            }

            return ParseSystemSingleData(addr, validData, system);
        }

        public bool ParseEffectorModelData(byte[] buffer, ref DeviceModel effectorModel, ref string msg)
        {
            //AEC.Log.OutputLine("ParseEffectorModelData()...");
            Debug.Assert(buffer != null);

            //effectorModel = DeviceModel.M_Audio;

            if (buffer.Length < ReadResponseMinLength || buffer[BeforCmdLength] != ReadCmd || buffer[(uint)DataFrame.ModuleIndex] != this.systemModuleIndex)
            {
                return false;
            }

            //AEC.Log.OutputLine("DecryptData()...");
            //DecryptData(buffer, keyPair);
            //AEC.Log.OutputLine("CheckReadResponseIsOk()...");

            if (!CommProtocolBase.CheckReadResponseIsOk(buffer))
            {
                Tools.LogHelper.WriteInfo("Failed to detect response data!");
                return false;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...完成");

            byte addr = buffer[ReadSystemResponseDataIndex];
            if(addr != (byte)SystemCommProtocol.SystemParamAddr.deviceModel)
            {
                return false;
            }
            
            byte[] modelData = GetReadResponsePayload(buffer);
            uint model = 0xffffffff;
            if(modelData.Length == sizeof(ushort))
            {
                model = (uint)BitConverter.ToUInt16(modelData, 0);
            }
            else if(modelData.Length == sizeof(uint))
            {
                model = BitConverter.ToUInt32(modelData, 0);
            }
            else
            {
                Tools.LogHelper.WriteInfo("Response data length error!");
                return false;
            }

            if (ProductsConfig.effectorModelMap.ContainsKey(model))
            {
                effectorModel = ProductsConfig.effectorModelMap[model];

                //Log.OutputLine("device model = {0}", effectorModel);

                bool isSupported = AppConfig.DeviceModelIsSupported(effectorModel);

                if (!isSupported)
                {
                    msg = string.Format(AEC.Misc.Config.GetInstance().localization[AEC.Misc.Localization.comm_UnsupportedDevcieModelIdentity], Core.Utils.ShortToHexStr((short)model));
                    AEC.Tools.LogHelper.WriteInfo("Unsupported device model : {0} !", effectorModel.ToString());
                }
                else
                {
                    msg = string.Format(" {0} ", AEC.Core.Utils.Uint32ToShortHexStr(model));
                }

                return isSupported;
            }
            else
            {
                msg = string.Format(AEC.Misc.Config.GetInstance().localization[AEC.Misc.Localization.comm_UnknownDevcieModelIdentity], Core.Utils.ShortToHexStr((short)model));
                AEC.Tools.LogHelper.WriteInfo("The device model code data ( {0} ) is incorrect !", Core.Utils.Uint32ToHexStr(model));
                return false;
            }
        }

        private static float SignalAdd(float amp1, float amp2)
        {
            Debug.Assert(amp1 >= 0 && amp2 >= 0);

            if (amp1 > 0 && amp2 > 0)
            {
                return GetFloatLevelData(amp1 + amp2);
            }
            else if (amp1 > 0 && amp2 == 0)
            {
                return GetFloatLevelData(amp1);
            }
            else if (amp2 > 0 && amp1 == 0)
            {
                return GetFloatLevelData(amp2);
            }
            else
            {
                return ZerodBuLevel;
            }
        }

        private static float GetFloatAbsData(byte[] levelData, int startIndex)
        {
            Debug.Assert(levelData != null && startIndex + SystemLevelDataLength <= levelData.Length);
            return Math.Abs(BitConverter.ToSingle(levelData, startIndex));
        }

        private static float GetFloatLevelData(float rawData)
        {
            if (rawData == 0)
            {
                return ZerodBuLevel;
            }

            float normal_level = rawData * 0x7fffff00 / 0x80000000;
            float level_in_dB = 20 * (float)Math.Log10( Math.Abs(normal_level)) + 14;

            return level_in_dB;
        }

        //startIndex为4字节偏移量
        private static float Get24BitFixedPointAbs(byte[] levelData, int startIndex)
        {
            Debug.Assert(levelData != null && startIndex + SystemLevelDataLength <= levelData.Length);


            Int32 byte1 = levelData[SystemLevelDataLength * startIndex + 1];
            Int32 byte2 = levelData[SystemLevelDataLength * startIndex + 2] << 8;
            Int32 byte3 = (sbyte)levelData[SystemLevelDataLength * startIndex + 3] << 16;
            Int32 totle = byte1 + byte2 + byte3;
            //double level = byte1 + byte2 + byte3;
            //Console.WriteLine("total = {0}", totle);
            return (float)Math.Abs((double)totle) / 0x800000;
            //if (totle == 0)
            //{
            //    return ZerodBuLevel;
            //}
            ////totle = 0x7fffff;
            //float normalLevel = (float)Math.Abs((double)totle) / 0x800000;
            //float level_in_dBu = (float)(20 * Math.Log10(normalLevel) + 14);

            //Console.WriteLine("mic Level : {0} dBu", level);

            //Console.WriteLine("麦克风电平数据:{0}, FixedPoint = {1}, level = {2} dBu", lmUtility.lmUtility.ByteToHexStr(levelData.ToArray()), totle, level_in_dBu.ToString("F"));

            //return level_in_dBu;
        }

        protected static float Get32Float(byte[] levelData, int startIndex)
        {
            Debug.Assert(levelData != null && startIndex + SystemLevelDataLength <= levelData.Length);

            //byte[] temp = new byte[4] { 0xA3, 0x1F, 0x85, 0x3D };
            //temp[0] = levelData[startIndex + 3];
            //temp[1] = levelData[startIndex + 2];
            //temp[2] = levelData[startIndex + 1];
            //temp[3] = levelData[startIndex + 0];

            //byte[] temp = new byte[4];
            //Array.Copy(levelData, startIndex * SystemLevelDataLength, temp, 0, 4);
            //Console.WriteLine("temp data:{0}", Utils.ByteToHexStr(temp));

            //byte low_0 = levelData[startIndex + 3];
            //byte low_1 = levelData[startIndex + 2];
            //levelData[startIndex + 3] = levelData[startIndex + 0];
            //levelData[startIndex + 2] = levelData[startIndex + 1];
            //levelData[startIndex + 1] = low_1;
            //levelData[startIndex + 0] = low_0;

            return BitConverter.ToSingle(levelData, startIndex * SystemLevelDataLength);
        }

        private static float Get24BitFixedPointIndB(byte[] levelData, int startIndex)
        {
            float normalLevel = Get24BitFixedPointAbs(levelData, startIndex);
            if (normalLevel == 0)
            {
                return ZerodBuLevel;
            }

            float level_in_dBu = (float)(20 * Math.Log10(normalLevel));

            return level_in_dBu;
        }

        private static float GetFloatIndBu(byte[] levelData, int startIndex)
        {
            float normalLevel = Math.Abs(Get32Float(levelData, startIndex)) / 0x00800000;
            //float normalLevel = Math.Abs(Get32Float(levelData, startIndex));
            //Console.WriteLine("level:{0}", normalLevel / (0x80000000));
            if (normalLevel == 0)
            {
                return ZerodBuLevel;
            }

            float level_in_dBu = (float)(20 * Math.Log10(normalLevel));

            return level_in_dBu;
        }

        private static float Get24BitFixedPointIndBu(byte[] levelData, int startIndex)
        {
            float normalLevel = Get24BitFixedPointAbs(levelData, startIndex);
            //float normalLevel = Math.Abs(Get32Float(levelData, startIndex));
            //Console.WriteLine("level:{0}", normalLevel / (0x80000000));
            if (normalLevel == 0)
            {
                return ZerodBuLevel;
            }

            float level_in_dBu = (float)(20 * Math.Log10(normalLevel) + 14);

            return level_in_dBu;
        }

        public bool ParseDeviceActivationCodeData(byte[] buffer, SystemModuleBase system)
        {
            Debug.Assert(buffer != null);

            if (buffer.Length < ReadResponseMinLength || buffer[BeforCmdLength] != ReadCmd || buffer[(uint)DataFrame.ModuleIndex] != system.moduleIndex)
            {
                return false;
            }

            if (!CommProtocolBase.CheckReadResponseIsOk(buffer))
            {
                Tools.LogHelper.WriteInfo("Failed to detect response data!");
                return false;
            }

            byte addr = buffer[ReadSystemResponseDataIndex];
            if(addr != (byte)SystemCommProtocol.SystemParamAddr.activationCode)
            {
                return false;
            }

            byte[] code = GetSystemModuleData(buffer);
            if (code == null || code.Length != 8)
            {
                return false;
            }

            UInt16 t1 = (UInt16)(code[0] + (code[1] << 8));
            UInt16 t2 = (UInt16)(code[2] + (code[3] << 8));

            UInt16 rand1 = 0x1A56;
            UInt16 rand2 = 0x2157;

            UInt16 check1 = CalcCheckCode(t1, t2, rand1, rand2);
            UInt16 check2 = CalcCheckCode(t2, t1, rand2, rand1);

            UInt16 t3 = (UInt16)(code[4] + (code[5] << 8));
            UInt16 t4 = (UInt16)(code[6] + (code[7] << 8));

            system.deviceIsActivated = check1 == t3 && check2 == t4;

            //system.deviceIsActivated = true;
            //Console.WriteLine("设备激活状态:{0}",
            //    system.deviceIsActivated ? "已经激活" : "未激活");

            return true;
        }

        public bool ParseCustomDeviceInfoEnable(byte[] buffer, SystemModuleBase system)
        {
            byte[] validData = GetSystemModuleData(buffer);

            if (validData == null || validData.Length < 2)
            {
                return false;
            }

            system.extendedFunctionState.isEnableCustomModel = GetShortFromDualBytes(validData) != 0;

            //Log.OutputLine("设备自定义型号使能:{0}", system.extendedFunctionState.isEnableCustomModel);
            //if (index == (short)SystemCommProtocol.SystemExtendedFunctionState.enableCustomDeviceModel)
            //{
            //    return true;
            //}

            return true;
        }

        public bool ParseSystemExtendedFunctionData(short index, byte[] buffer, SystemModuleBase system)
        {
            byte[] validData = GetSystemModuleData(buffer);

            if (validData == null || validData.Length < 2)
            {
                return false;
            }

            switch(index)
            {
                case (short)SystemCommProtocol.SystemCustomDeviceInfoData.custom_manufacturer_data:
                    if (validData.Length == SystemCommProtocol.CustomManufacturerLength)
                    {
                        system.extendedFunctionData.custom_manufacturer = Encoding.ASCII.GetString(validData).TrimEnd('\0');
                        //Log.OutputLine("用户自定义厂商名:{0}", system.extendedFunctionData.custom_manufacturer);
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (short)SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_model_data:
                    if (validData.Length == SystemCommProtocol.CustomDeviceModelLength)
                    {
                        system.extendedFunctionData.custom_device_model = Encoding.ASCII.GetString(validData).TrimEnd('\0');
                        //Log.OutputLine("用户自定义型号:{0}", system.extendedFunctionData.custom_device_model);
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (short)SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_version_data:
                    if (validData.Length == SystemCommProtocol.CustomDeviceVersionLength)
                    {
                        system.extendedFunctionData.custom_device_version = Encoding.ASCII.GetString(validData).TrimEnd('\0');
                        //Log.OutputLine("用户自定义版本:{0}", system.extendedFunctionData.custom_device_version);
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case (short)SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_description_data:
                    if (validData.Length == SystemCommProtocol.CustomDeviceDescriptionLength)
                    {
                        system.extendedFunctionData.custom_device_description = Encoding.ASCII.GetString(validData).TrimEnd('\0');
                        //Log.OutputLine("用户自定义设备描述:{0}", system.extendedFunctionData.custom_device_description);
                    }
                    else
                    {
                        return false;
                    }
                    break;

            }            

            return true;
        }

        //public bool ParseSystemUsageLimitLockData(short index, byte[] buffer, int startIndex, SystemModuleBase system)
        //{
        //    Debug.Assert(index < system.usageLimitLocksCount && buffer != null && buffer.Length >= startIndex + UsageLimitLock.DataLength);

        //    if (!SystemCommProtocol.ParseDataToUsageLimitLockData(buffer, startIndex, system.usageLimitLocks[index]))
        //    {
        //        Tools.LogHelper.WriteInfo("Failed to detect response data!");
        //        return false;
        //    }

        //    return true;
        //}

        public bool ParseSystemModeNameData(short index, byte[] buffer, SystemModuleBase system)
        {
            Debug.Assert(index < system.deviceModeCount);

            if (buffer.Length < ReadResponseMinLength || buffer[BeforCmdLength] != ReadCmd || buffer[(uint)DataFrame.ModuleIndex] != system.moduleIndex)
            {
                return false;
            }
            
            if (!CommProtocolBase.CheckReadResponseIsOk(buffer))
            {
                Tools.LogHelper.WriteInfo("Failed to detect response data!");
                return false;
            }

            byte[] modeRawData = GetSystemModuleData(buffer);
            if (modeRawData.Length != SystemModuleBase.DeviceModeNameLength)
            {
                return false;
            }

            system.deviceModeList[index] = SystemCommProtocol.StringDataDecode2String(modeRawData).Trim();

            //Console.WriteLine(system.deviceModeList[index]);

            return true;
        }
        #endregion

        #region 联机通讯过程
        public byte[] GetReadModuleCmd(short moduleIndex)
        {
            byte[] cmd = GetReadCmd(ReadModuleCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.deviceDataUpload2Pc;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(moduleIndex);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x00;
            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetWriteNonSysteModuleCmd(short moduleIndex, List<short> moduleData)
        {
            byte[] byteList = DataList2ByteList(moduleData);
            int cmdLength = ValidDataStartIndex + ModuleIndexLength + byteList.Length + 1;
            byte[] cmd = GetWriteCmd(cmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.pcDataDownload2Device;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(moduleIndex);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x00;
            Array.Copy(byteList, 0, cmd, (int)DataFrame.ValidDataStartIndex + SingleDataSize, byteList.Length);
            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetWriteCustomDeviceInfoEnableCmd(bool enable)
        {
            short data = DataConverter.Bool2Short(enable);
            int cmdLength = ValidDataStartIndex + SingleDataSize + SingleDataSize + 1;
            byte[] cmd = GetWriteCmd(cmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.customDeviceInfoEnable;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte((short)data);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = GetHighByte((short)data);
            cmd[(uint)DataFrame.ValidDataStartIndex + 2] = GetLowByte(0);
            cmd[(uint)DataFrame.ValidDataStartIndex + 3] = GetHighByte(0);            
            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetWriteCustomDeviceInfoDataCmd(SystemCommProtocol.SystemCustomDeviceInfoData efd, Object data)
        {
            byte[] bytes = SystemCommProtocol.CustomDeviceInfoDataConvertToBytes(efd, data);
            if(bytes == null)
            {
                return null;
            }

            int cmdLength = ValidDataStartIndex + ModuleIndexLength + bytes.Length + 1;
            byte[] cmd = GetWriteCmd(cmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.customDeviceInfoData;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte((short)efd);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = GetHighByte((short)efd);

            Array.Copy(bytes, 0, cmd, (int)DataFrame.ValidDataStartIndex + SingleDataSize, bytes.Length);

            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetWriteDeviceNameCmd(string name)
        {
            int cmdLength = (int)DataFrame.ValidDataStartIndex + SystemModuleBase.DefDeviceNameSize + 1;
            byte[] cmd = GetWriteCmd(cmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.deviceName;

            name = Tools.StringHelper.GetMaxSizeUtf8String(name, SystemModuleBase.DefDeviceNameSize);

            byte[] data = new byte[SystemModuleBase.DefDeviceNameSize];
            byte[] encoded = Encoding.UTF8.GetBytes(name);
            int length = encoded.Length < SystemModuleBase.DefDeviceNameSize ? encoded.Length : SystemModuleBase.DefDeviceNameSize;
            Array.Copy(encoded, 0, cmd, (int)DataFrame.ValidDataStartIndex, length);

            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetUpdateUsageLimitLockCmd(UsageLimitLock usageLimitLock)
        {
            int cmdLength = (int)DataFrame.ValidDataStartIndex + sizeof(short) + UsageLimitLock.DataLength + 1;
            byte[] cmd = GetWriteCmd(cmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.usageLimitLock;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(usageLimitLock.index);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = GetHighByte(usageLimitLock.index);

            byte[] data = SystemCommProtocol.UsageLimitLockToByteArray(usageLimitLock);

            Array.Copy(data, 0, cmd, (int)DataFrame.ValidDataStartIndex + SingleDataSize, data.Length);

            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetReadSystemDataCmd(SystemCommProtocol.SystemParamAddr addr)
        {
            byte[] cmd = GetReadCmd(ReadSystemParamCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)addr;
            GenCheckSum(cmd);
            return cmd;
        }

        public byte[] GetSaveDeviceModeCmd(short modeIndex, string modeName)
        {
            int cmdLength = (int)DataFrame.ValidDataStartIndex + sizeof(short) + SystemModuleBase.DeviceModeNameLength + 1;
            byte[] cmd = GetWriteCmd(cmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.deviceModeSave;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(modeIndex);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = GetHighByte(modeIndex);

            byte[] data = SystemCommProtocol.StringEncode2BinaryData(modeName);
            Array.Copy(data, 0, cmd, (int)DataFrame.ValidDataStartIndex + SingleDataSize, data.Length);
            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetWriteSystemDataCmd(SystemCommProtocol.SystemParamAddr addr, short value)
        {
            byte[] cmd = GetWriteCmd(WriteSysCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)addr;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(value);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = GetHighByte(value);
            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetWriteSystemWrorkDataCmd(SystemCommProtocol.SystemParamAddr addr, SwitchWorkMode switchWorkMod)
        {
            byte[] cmd = GetWriteCmd(WriteSystemWorkModeCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)addr;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(switchWorkMod.workModeIndex);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = GetHighByte(switchWorkMod.workModeIndex);
            cmd[(uint)DataFrame.ValidDataStartIndex + 2] = GetLowByte(DataConverter.Bool2Short(switchWorkMod.retainsData));
            cmd[(uint)DataFrame.ValidDataStartIndex + 3] = GetHighByte(DataConverter.Bool2Short(switchWorkMod.retainsData));
            cmd[(uint)DataFrame.ValidDataStartIndex + 4] = GetLowByte(DataConverter.Bool2Short(switchWorkMod.outputMute));
            cmd[(uint)DataFrame.ValidDataStartIndex + 5] = GetHighByte(DataConverter.Bool2Short(switchWorkMod.outputMute));
            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetReadCustomDeviceInfoEnable()
        {
            byte[] cmd = GetReadCmd(ReadSystemModeNameParamCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.customDeviceInfoEnable;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = 0x00;
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x00;
            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetReadDeviceExtendedFunctionData(short data_index)
        {
            byte[] cmd = GetReadCmd(ReadSystemModeNameParamCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.customDeviceInfoData;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(data_index);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x00;
            GenCheckSum(cmd);

            return cmd;
        }

        //public byte[] GetReadUsageLimitLockDataCmd(short index)
        //{
        //    byte[] cmd = GetReadCmd(ReadSystemUsageLimitLockCmdLength);
        //    cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
        //    cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.usageLimitLock;
        //    cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(index);
        //    cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x00;
        //    GenCheckSum(cmd);

        //    return cmd;
        //}

        public byte[] GetReadDeviceModeName(short index)
        {
            byte[] cmd = GetReadCmd(ReadSystemModeNameParamCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.deviceModeName;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = GetLowByte(index);
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x00;
            GenCheckSum(cmd);

            return cmd;
        }

        public byte[] GetReadDeviceActivationCode()
        {
            byte[] cmd = GetReadCmd(ReadActivationCodeCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)SystemCommProtocol.SystemParamAddr.activationCode;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = 0x34;
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x12;
            GenCheckSum(cmd);

            return cmd;
        }
        
        #endregion

        #region 联机信号相关
        //握手命令
        public byte[] handshakeCmd
        {
            get
            {
                return GetControlConnectionCmd(SystemCommProtocol.ConnectionFlag.Connect);
            }
        }

        //心跳命令
        private byte[] heartbeatCmd
        {
            get
            {
                return GetControlConnectionCmd(SystemCommProtocol.ConnectionFlag.Online);
            }
        }

        //断线命令
        public byte[] OfflineCmd
        {
            get
            {
                return GetControlConnectionCmd(SystemCommProtocol.ConnectionFlag.Disconnect);
            }
        }

        private byte[] GetControlConnectionCmd(SystemCommProtocol.ConnectionFlag flag)
        {
            byte[] cmd = GetWriteCmd(ConnectCmdLength);
            cmd[(uint)DataFrame.ModuleIndex] = systemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = ConnectionStateAddr;
            cmd[(uint)DataFrame.ValidDataStartIndex + 0] = (byte)flag;
            cmd[(uint)DataFrame.ValidDataStartIndex + 1] = 0x00;
            GenCheckSum(cmd);
            return cmd;
        }
        #endregion

        private static byte[] GetWriteSystemDataCmd(SystemCommProtocol.SystemParamAddr addr, byte[] data)
        {
            System.Diagnostics.Debug.Assert(data != null);

            int cmdLength = (int)DataFrame.ValidDataStartIndex + data.Length + 1;

            byte[] cmd = new byte[cmdLength];
            cmd[(uint)DataFrame.HeaderIndex] = Header;
            cmd[(uint)DataFrame.IdentityIndex] = DefaultIdentity;
            ushort length = (ushort)(cmd.Length - BeforCmdLength);
            cmd[(uint)DataFrame.LengthLowIndex] = GetLowByte(length);
            cmd[(uint)DataFrame.CmdTypeIndex] = WriteCmd;
            cmd[(uint)DataFrame.ModuleIndex] = DefaultSystemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)addr;

            Array.Copy(data, 0, cmd, (int)DataFrame.ValidDataStartIndex, data.Length);
            
            GenCheckSum(cmd);

            return cmd;
        }

        public static byte[] GetDefaultConnectiontinCmd(SystemCommProtocol.ConnectionFlag flag)
        {
            return GetWriteSystemDataCmd(SystemCommProtocol.SystemParamAddr.connectionState, BitConverter.GetBytes((ushort)flag));
        }

        public static byte[] GetHandshakeCmd()
        {
            return GetDefaultConnectiontinCmd(SystemCommProtocol.ConnectionFlag.Connect);
        }

        public static byte[] GetOfflineCmd()
        {
            return GetDefaultConnectiontinCmd(SystemCommProtocol.ConnectionFlag.Disconnect);
        }

        public static byte[] GetLoadSystemDataCmd(SystemCommProtocol.SystemParamAddr addr)
        {
            byte[] cmd = new byte[ReadSystemParamCmdLength];
            cmd[(uint)DataFrame.HeaderIndex] = Header;
            cmd[(uint)DataFrame.IdentityIndex] = DefaultIdentity;
            ushort length = (ushort)(cmd.Length - BeforCmdLength);
            cmd[(uint)DataFrame.LengthLowIndex] = GetLowByte(length);
            cmd[(uint)DataFrame.CmdTypeIndex] = ReadCmd;
            cmd[(uint)DataFrame.ModuleIndex] = DefaultSystemModuleIndex;
            cmd[(uint)DataFrame.DataAddressIndex] = (byte)addr;

            GenCheckSum(cmd);

            return cmd;
        }

        public static byte[] GetLoadDeviceModelCodeCmd()
        {
            return GetLoadSystemDataCmd(SystemCommProtocol.SystemParamAddr.deviceModel);
        }

        public static byte[] GetLoadDeviceIdCmd()
        {
            return GetLoadSystemDataCmd(SystemCommProtocol.SystemParamAddr.deviceId);
        }

        public static byte[] GetUpdateActivateDataCmd(byte[] activateData)
        {
            return GetWriteSystemDataCmd(SystemCommProtocol.SystemParamAddr.activationCode, activateData);
        }

        public static bool ParseDeviceModelCode(byte[] buffer, out uint deviceModel)
        {
            deviceModel = 0xffffffff;

            if (buffer.Length < ReadResponseMinLength || buffer[BeforCmdLength] != ReadCmd || buffer[(uint)DataFrame.ModuleIndex] != DefaultSystemModuleIndex)
            {
                return false;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...");
            if (!CommProtocolBase.CheckReadResponseIsOk(buffer))
            {
                Tools.LogHelper.WriteInfo("Failed to detect response data!");
                return false;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...完成");

            byte addr = buffer[ReadSystemResponseDataIndex];
            byte[] payloadData = GetSystemModuleData(buffer);

            if (payloadData == null)
            {
                return false;
            }

            if (payloadData.Length == sizeof(uint))
            {
                deviceModel = BitConverter.ToUInt32(payloadData, 0);
            }
            else if (payloadData.Length == sizeof(ushort))
            {
                deviceModel = BitConverter.ToUInt16(payloadData, 0);
            }
            else
            {
                return false;
            }

            return true;
        }

        public static byte[] ParseDeviceIdCode(byte[] buffer)
        {
            if (buffer.Length < ReadResponseMinLength || 
                buffer[BeforCmdLength] != ReadCmd || 
                buffer[(uint)DataFrame.ModuleIndex] != DefaultSystemModuleIndex ||
                buffer[(uint)DataFrame.DataAddressIndex] != (byte)SystemCommProtocol.SystemParamAddr.deviceId)
            {
                return null;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...");
            if (!CommProtocolBase.CheckReadResponseIsOk(buffer))
            {
                Tools.LogHelper.WriteInfo("Failed to detect response data!");
                return null;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...完成");

            byte addr = buffer[ReadSystemResponseDataIndex];
            byte[] payloadData = GetSystemModuleData(buffer);

            return payloadData;
        }

        public static byte[] ParseSystemData(byte[] buffer, SystemCommProtocol.SystemParamAddr addr)
        {
            if (buffer.Length < ReadResponseMinLength || 
                buffer[BeforCmdLength] != ReadCmd || 
                buffer[(uint)DataFrame.ModuleIndex] != DefaultSystemModuleIndex ||
                buffer[(uint)DataFrame.DataAddressIndex] != (byte)addr)
            {
                return null;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...");
            if (!CommProtocolBase.CheckReadResponseIsOk(buffer))
            {
                Tools.LogHelper.WriteInfo("Failed to detect response data!");
                return null;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...完成");

            byte[] payloadData = GetSystemModuleData(buffer);

            return payloadData;
        }

        protected static byte[] GetSystemModuleData(byte[] buffer)
        {
            //Debug.Assert(
            //    buffer != null && 
            //    buffer.Length >= ReadResponseMinLength &&
            //    buffer[BeforCmdLength] == ReadCmd);
            if(buffer == null ||
                buffer.Length < ReadResponseMinLength ||
                buffer[BeforCmdLength] != ReadCmd)
            {
                return null;
            }

            //int cmdLength = GetShortFromDualBytes(buffer, (int)DataFrame.LengthLowIndex) + (int)DataFrame.CmdTypeIndex;
            int cmdLength = buffer[(int)DataFrame.LengthLowIndex] + (int)DataFrame.CmdTypeIndex;
            int length = cmdLength - 1 - (int)DataFrame.ValidDataStartIndex;
            if (length <= 0)
            {
                return null;
            }

            byte[] data = new byte[length];
            Array.Copy(buffer, (int)DataFrame.ValidDataStartIndex, data, 0, length);

            return data;
        }

        protected static byte[] GetReadResponsePayload(byte[] buffer)
        {
            if (buffer == null ||
                buffer.Length < ReadResponseMinLength ||
                buffer[BeforCmdLength] != ReadCmd)
            {
                return null;
            }

            //int cmdLength = GetShortFromDualBytes(buffer, (int)DataFrame.LengthLowIndex) + (int)DataFrame.CmdTypeIndex;
            int cmdLength = buffer[(int)DataFrame.LengthLowIndex] + (int)DataFrame.CmdTypeIndex;
            int length = cmdLength - 1 - (int)DataFrame.ValidDataStartIndex;
            if (length <= 0)
            {
                return null;
            }

            byte[] payload = new byte[length];
            Array.Copy(buffer, (int)DataFrame.ValidDataStartIndex, payload, 0, length);

            return payload;
        }

        private byte[] GetReadCmd(int cmdLength)
        {
            Debug.Assert(cmdLength >= ReadCmdMinLength);
            byte[] cmd = new byte[cmdLength];
            FillThe1st4ReadCmd(cmd);
            return cmd;
        }

        private byte[] GetWriteCmd(int cmdLength)
        {
            Debug.Assert(cmdLength >= WriteCmdMinLength);
            byte[] cmd = new byte[cmdLength];
            FillThe1st5WriteCmd(cmd);
            return cmd;
        }

        //填入读取命令前4个字节
        private void FillThe1st4ReadCmd(byte[] data)
        {
            FillThe1st4Byte(data);
            data[(uint)DataFrame.CmdTypeIndex] = ReadCmd;
        }

        //填入读取写入前4个字节
        private void FillThe1st5WriteCmd(byte[] data)
        {
            FillThe1st4Byte(data);
            data[(uint)DataFrame.CmdTypeIndex] = WriteCmd;
        }

        //填充前3个字节
        private void FillThe1st4Byte(byte[] data)
        {
            data[(uint)DataFrame.HeaderIndex] = Header;
            data[(uint)DataFrame.IdentityIndex] = identity;
            ushort length = (ushort)(data.Length - BeforCmdLength);
            data[(uint)DataFrame.LengthLowIndex] = GetLowByte(length);
        }

        public static void EncryptCmd(byte[] cmd, CommKeyPair keyPair, int startIndex = ValidDataStartIndex)
        {
            Debug.Assert(cmd != null && keyPair != null);
            byte key = (byte)((keyPair.key1) ^ (keyPair.key2));
            for (int i = startIndex; i < cmd.Length - 1; ++i)
            {
                cmd[i] = (byte)(cmd[i] ^ key);
            }
        }

        public static void DecryptData(byte[] data, CommKeyPair keyPair, int startIndex = ValidDataStartIndex)
        {
            Debug.Assert(data != null && keyPair != null);
            byte key = (byte)((keyPair.key1) ^ (keyPair.key2));
            for (int i = startIndex; i < data.Length - 1; ++i)
            {
                data[i] = (byte)(data[i] ^ key);
            }
        }

        

        public static void ParseDataList2EQGroup(short[] data, ref int pointer, EQGroup eqGroup)
        {
            Debug.Assert(data != null && eqGroup != null);
            eqGroup.currEQIndex = data[pointer++];
            eqGroup.eqPassIndex = data[pointer++];
            for (int i = 0; i < eqGroup.eqBandCount; ++i)
            {
                eqGroup.eqList[i].typeIndex = data[pointer++];
                eqGroup.eqList[i].freqIndex = data[pointer++];
                eqGroup.eqList[i].gainIndex = data[pointer++];
                eqGroup.eqList[i].qIndex_ = data[pointer++];
                eqGroup.eqList[i].sIndex_ = data[pointer++];
            }
        }

        protected static void ParseDataList2DEQGroup(short[] data, ref int pointer, DEQGroup deqGroup)
        {
            Debug.Assert(data != null && deqGroup != null);
            deqGroup.currEQIndex = data[pointer++];
            for (int i = 0; i < deqGroup.eqBandCount; ++i)
            {
                deqGroup.deqList[i].deqIsPass.initV = data[pointer++];
                deqGroup.deqList[i].typeIndex = data[pointer++];
                deqGroup.deqList[i].freqIndex = data[pointer++];
                deqGroup.deqList[i].gainIndex = data[pointer++];
                deqGroup.deqList[i].qIndex_ = data[pointer++];
                deqGroup.deqList[i].sIndex_ = data[pointer++];

                deqGroup.deqList[i].deqCM.initV = data[pointer++];
                deqGroup.deqList[i].attack.initV = data[pointer++];
                deqGroup.deqList[i].release.initV = data[pointer++];
                deqGroup.deqList[i].threshold.initV = data[pointer++];
                deqGroup.deqList[i].ratio.initV = data[pointer++];
                deqGroup.deqList[i].deqDM.initV = data[pointer++];
            }
        }

        private static byte[] DataList2ByteList(List<short> dataList)
        {
            List<byte> byteList = new List<byte>();
            foreach (short data in dataList)
            {
                byteList.Add(GetLowByte(data));
                byteList.Add(GetHighByte(data));
            }
            return byteList.ToArray();
        }

        protected static short[] GetModuleData2ShortList(byte[] buffer)
        {
            List<short> dataList = new List<short>();
            for (int i = ValidDataStartIndex; i < buffer.Length - 2; i += 2)
            {
                dataList.Add(GetShortFromDualBytes(buffer, i));
            }

            return dataList.ToArray();
        }

        protected static void AppendEQGroup2DataBuffer(List<short> data, EQGroup eqGroup)
        {
            Debug.Assert(data != null && eqGroup != null);
            data.Add(eqGroup.currEQIndex);
            data.Add(eqGroup.eqPassIndex);
            foreach (EQ eq in eqGroup.eqList)
            {
                data.Add(eq.typeIndex);
                data.Add(eq.freqIndex);
                data.Add(eq.gainIndex);
                data.Add(eq.qIndex_);
                data.Add(eq.sIndex_);
            }
        }

        protected static void AppendDEQGroup2DataBuffer(List<short> data, DEQGroup deqGroup)
        {
            Debug.Assert(data != null && deqGroup != null);
            data.Add(deqGroup.currEQIndex);
            foreach (DEQ deq in deqGroup.eqList)
            {
                data.Add(deq.deqIsPass.v);
                data.Add(deq.typeIndex);
                data.Add(deq.freqIndex);
                data.Add(deq.gainIndex);
                data.Add(deq.qIndex_);
                data.Add(deq.sIndex_);
                data.Add(deq.deqCM.v);
                data.Add(deq.attack.v);
                data.Add(deq.release.v);
                data.Add(deq.threshold.v);
                data.Add(deq.ratio.v);
                data.Add(deq.deqDM.v);
            }
        }

        private static byte[] EQ2ByteBuffer(EQ eq)
        {
            List<short> dataList = new List<short>();
            dataList.Add(eq.index);
            dataList.Add(eq.typeIndex);
            dataList.Add(eq.freqIndex);
            dataList.Add(eq.gainIndex);
            dataList.Add(eq.qIndex_);
            dataList.Add(eq.sIndex_);

            return ShortList2ByteList(dataList).ToArray();
        }

        protected static void AppendZeros2DataBuffer(List<short> data, int length)
        {
            short[] temp = new short[length];
            data.AddRange(temp);
        }

        public static CommCmdType CheckCommCmdType(byte[] data)
        {
            Debug.Assert(data != null);

            if (data.Length > BeforCmdLength + 1)
            {
                if(data[BeforCmdLength] == ReadCmd)
                {
                    return CommCmdType.Read;
                }
                else if (data[BeforCmdLength] == WriteCmd)
                {
                    return CommCmdType.Write;
                }                
            }

            return CommCmdType.Unknown;
        }

        //检测是否是读取命令
        //public static bool CheckCmdIsRead(byte[] data)
        //{
        //    Debug.Assert(data != null);

        //    if (data.Length > 4 && data[BeforCmdLength] == ReadCmd)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //检测写入响应是否正确
        public static bool CheckWriteResponseIsOk(List<byte> bytes)
        {
            byte[] byte_arr = bytes.ToArray();
            if (byte_arr.Length == 2)
            {
                if (byte_arr[0] == 0xfe && byte_arr[1] == 0xbb)
                {
                    return true;
                }
            }

            return false;
        }

        //检测读取响应是否正确
        protected static bool CheckReadResponseIsOk(byte[] byteArray)
        {
            System.Diagnostics.Debug.Assert(byteArray != null);

            List<byte> bytes = new List<byte>(byteArray);
            byte[] byte_arr = bytes.ToArray();
            int length = byte_arr.Length;

            if (length >= ReadResponseMinLength &&
                byte_arr[0] == Header)
            {
                //int cmdLength = BitConverter.ToUInt16(byte_arr, (int)DataFrame.LengthLowIndex);
                int cmdLength = byte_arr[(int)DataFrame.LengthLowIndex];

                int dataLength = cmdLength + BeforCmdLength;

                if(length < dataLength)
                {
                    return false;
                }

                int check_sum_index = dataLength - 1;
                byte curr_cheksum = byte_arr[check_sum_index];
                byte[] arr = new byte[dataLength];
                Array.Copy(byte_arr, 0, arr, 0, dataLength);
                byte checksum = CalcCheckSum(arr);
                if (checksum == curr_cheksum)
                {
                    return true;
                }
            }

            return false;
        }

        //检测读取响应是否完成
        public static bool CheckReadResponseIsCompleted(List<byte> bytes)
        {
            byte[] byte_arr = bytes.ToArray();
            int length = byte_arr.Length;

            if (length >= ReadResponseMinLength && 
                byte_arr[0] == Header)
            {
                //int cmdLength = BitConverter.ToUInt16(byte_arr, (int)DataFrame.LengthLowIndex);
                int cmdLength = byte_arr[(int)DataFrame.LengthLowIndex];
                //AEC.Log.OutputLine("检测读取数据响应是否完成..{0}, length = {1}, lengthFlag = {2}", DateTime.Now.ToString("hh:mm:ss fff"), length, byte_arr[2]);
                return cmdLength + BeforCmdLength <= length;
            }

            return false;
        }

        //将计算出校验码，保存到最后一位
        private static void GenCheckSum(byte[] data)
        {
            data[data.Length - 1] = CalcCheckSum(data);      //保存校验码
        }

        //数据除了最后一位校验码，都已经确定，计算出校验码
        private static byte CalcCheckSum(byte[] data)
        {
            int sum = data[0];  //起始标志
            for (int i = BeforCmdLength; i <= data.Length - 2; ++i)
            {
                sum += data[i];    //控制字+功能模块+寄存器地址
            }
            byte low = (byte)(sum & 0xff);      //取低8位
            byte ret = (byte)(low ^ data[1]);   //身份标识异或
            return ret;
        }

        //将数组转换为字节序列
        private static List<byte> ShortList2ByteList(List<short> datas)
        {
            List<byte> bytes = new List<byte>();
            foreach(short data in datas)
            {
                bytes.Add(GetLowByte(data));
                bytes.Add(GetHighByte(data));
            }
            return bytes;
        }

        //计算bytes数组第index位和index+1位组成的short数
        protected static short GetShortFromDualBytes(byte[] bytes, int index = 0)
        {
            if (index < bytes.Length - 1)
            {
                short low = bytes[index];
                short high = bytes[index + 1];

                return (short)(low + (high << 8));
            }

            return -1;
        }

        protected static uint GetUintFromQuadBytes(byte[] bytes, int index = 0)
        {
            if (index < bytes.Length - 3)
            {
                return ((uint)bytes[index] + (uint)(bytes[index + 1] << 8) + (uint)(bytes[index + 2] << 16) + (uint)(bytes[index + 3] << 24));
            }

            return 0xffffffff;
        }

        private static UInt16 CalcCheckCode(UInt16 c1, UInt16 c2, UInt16 c3, UInt16 c4)
        {
            UInt16 t1 = (UInt16)(c1 ^ c3);
            UInt16 t2 = (UInt16)(c1 + t1);
            UInt16 t3 = (UInt16)(t2 ^ c4);
            return (UInt16)(t3 + c2);
        }

        //计算低字节
        public static byte GetLowByte(short value)
        {
            return (byte)(value & 0xff);
        }
        //计算高字节
        public static byte GetHighByte(short value)
        {
            return (byte)((value >> 8) & 0xff);
        }

        //计算低字节
        public static byte GetLowByte(ushort value)
        {
            return (byte)(value & 0xff);
        }
        //计算高字节
        public static byte GetHighByte(ushort value)
        {
            return (byte)((value >> 8) & 0xff);
        }
    }
}
