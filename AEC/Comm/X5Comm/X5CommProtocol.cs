using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Effectors.X5.Modules;
using AEC.Effectors.X5;
using AEC.Misc;
using AEC.Common;

namespace AEC.Comm.X5Comm
{
    //X5通讯协议
    class X5CommProtocol : CommProtocolBase
    {
#region 协议中的常量
        public const byte musicModuleIndex = 0x01;
        public const byte micModuleIndex = 0x02;

        public const byte echoModuleIndex = 0x03;
        public const byte reverbModuleIndex = 0x04;
        public const byte mainOutputModuleIndex = 0x05;
        public const byte centerModuleIndex = 0x06;
        public const byte subModuleIndex = 0x07;
        public const byte surOutputModuleIndex = 0x08;

        public const int musicModuleDataLength = 41;
        public const int micModuleDataLength = 83;
        public const int echoModuleDataLength = 28;
        public const int reverbModuleDataLength = 8;
        public const int mainOutputModuleDataLength = 78 + 2;
        public const int centerOutputModuleDataLength = 62 + 1;
        public const int subOutputModuleDataLength = 60 + 1;
        public const int surOutputModuleDataLength = 64 + 2;
        public const int outputModuleDataLength = 80;

        private const byte SystemStatusDataLength = 22 * 4;

        public const UInt32 LimiterReference_0dB = 0x7fffffff;
        public const UInt32 CompressorReference_0dB = 0x02000000;
        public const UInt32 LevelReference_0dB = 0x7fffffff;

        public enum MusicParamAddr
        {
            workMode = 0x00,
            workSelect = 0x01,
            hpfFreq = 0x02,
            hpfType = 0x03,
            eq = 0x04,
            eqPass = 0x05,
        }

        public enum MicParamAddr
        {
            noiseGate = 0x00,
            fbe = 0x01,
            hpfFreq = 0x02,
            attack = 0x03,
            release = 0x04,
            threshold = 0x05,
            ratio = 0x06,
            eq = 0x07,
            eqPass = 0x08,
        }
        
        public enum EchoParamAddr
        {
            echoMixerLevel = 0x00,
            echoMixerPhase = 0x01,
            directMixerLevel = 0x02,
            directMixerPhase = 0x03,
            echoHpfFreq = 0x04,
            echoLpfFreq = 0x05,
            echoPreDelay = 0x06,
            echoDelay = 0x07,
            echoRepeat = 0x08,
            echoRChDelay = 0x09,
            echoRChPreDelay = 0x0a,
            eq = 0x0b,
            eqPass = 0x0c,
        }

        public enum ReverbParamAddr
        {
            reverbMixerLevel = 0x00,
            reverbMixerPhase = 0x01,
            directMixerLevel = 0x02,
            directMixerPhase = 0x03,
            reverbHpfFreq = 0x04,
            reverbLpfFreq = 0x05,
            reverbTime = 0x06,  
            reverbPreDelay = 0x07,
        }

        public enum OutputBaseParamAddr
        {
            musicMixerLevel = 0x00,
            musicMixerPhase = 0x01,
            micMixerLevel = 0x02,
            micMixerPhase = 0x03,
            echoMixerLevel = 0x04,
            echoMixerPhase = 0x05,
            reverbMixerLevel = 0x06,
            reverbMixerPhase = 0x07,
            hpfType = 0x08,
            hpfFreq = 0x09,
            hpfQ    = 0x0A,
            lpfType = 0x0B,
            lpfFreq = 0x0C,
            lpfQ    = 0x0D,
            attack = 0x15,
            release = 0x16,
            threshold = 0x17,
            ratio = 0x18,
            eq = 0x20,
            eqPass = 0x21,
        }

        public enum StereoOutputParamAddr
        {
            leftChDelay = 0x19,
            leftChMute = 0x1A,
            rightChDelay = 0x1C,
            rightChMute = 0x1D,
        }

        public enum MonoOutputParamAddr
        {
            delay = 0x19,
            mute = 0x1A,
        }

        public static readonly Dictionary<byte, byte> eqAddrDict = new Dictionary<byte, byte>()
        {
            {musicModuleIndex, (byte)MusicParamAddr.eq},
            {micModuleIndex,  (byte)MicParamAddr.eq},            
            {echoModuleIndex, (byte)EchoParamAddr.eq},
            {mainOutputModuleIndex, (byte)OutputBaseParamAddr.eq},
            {centerModuleIndex, (byte)OutputBaseParamAddr.eq},
            {subModuleIndex, (byte)OutputBaseParamAddr.eq},
            {surOutputModuleIndex, (byte)OutputBaseParamAddr.eq},
        };

        

#endregion

        public X5CommProtocol() :
            base()
        {

        }

        public static DeviceStatus ParseSystemStatusData(byte[] buffer)
        {
            if (buffer[BeforCmdLength] != ReadCmd || buffer[(uint)DataFrame.ModuleIndex] != DefaultSystemModuleIndex)
            {
                return null;
            }

            if (!CommProtocolBase.CheckReadResponseIsOk(buffer))
            {
                return null;
            }

            byte addr = buffer[ReadSystemResponseDataIndex];
            if (addr != (byte)SystemCommProtocol.SystemParamAddr.systemStatus)
            {
                return null;
            }

            byte[] statusData = GetSystemModuleData(buffer);

            if (statusData == null || statusData.Length != SystemStatusDataLength)
            {
                //AEC.Log.OutputLine("解析系统状态数据，长度出错");
                return null;
            }

            //Log.OutputLine("statusData : {0}", AEC.Core.Utils.ByteArrayToHexStr(statusData));

            DeviceStatus deviceStatus = new DeviceStatus();
            
            int pointer = 0;

            for (int i = 0; i < DeviceStatus.DeviceStatusOutputLimiterChannels; ++i)
            {
                deviceStatus.ouputLimiter[i] = ParseNormlizeLinearLevelFromBuffer(statusData, pointer++, LimiterReference_0dB);
            }

            deviceStatus.micCompressor = ParseNormlizeLinearLevelFromBuffer(statusData, pointer++, CompressorReference_0dB);

            for (int i = 0; i < DeviceStatus.DeviceStatusOutputChannels; ++i)
            {
                //double level_indB = ParseDecibelsFromBuffer(statusData, pointer++) + 12;
                //if(level_indB > 0)
                //{
                //    level_indB = 0;
                //}
                deviceStatus.outputLevel[i] = ParseNormlizeLinearLevelFromBuffer(statusData, pointer++) * 4;    //进行乘 4 处理，使弥补DSP计算过程中将信号右移了2位
            }
            for (int i = 0; i < DeviceStatus.DeviceStatuMusicInputChannels; ++i)
            {
                deviceStatus.musicInputLevel[i] = ParseNormlizeLinearLevelFromBuffer(statusData, pointer++);
            }
            for (int i = 0; i < DeviceStatus.DeviceStatuMicInputChannels; ++i)
            {
                //deviceStatus.micInputLevel[i] = ParseDecibelsFromBuffer(statusData, pointer++);
                deviceStatus.micInputLevel[i] = ParseNormlizeLinearLevelFromBuffer(statusData, pointer++);
            }

            return deviceStatus;
        }

        //private static double ParseDecibelsFromBuffer(byte[] buffer, int index, UInt32 reference_0dB = LevelReference_0dB)
        //{
        //    UInt32 linear = ParseUint32DataFromBuffer(buffer, index);
        //    if(linear > 0)
        //    {
        //        return AEC.Core.DSP.DSPHelper.LinearToDecibels((double)linear / (double)reference_0dB);
        //    }
        //    else
        //    {
        //        return ZerodBuLevel;
        //    }
        //}

        private static double ParseNormlizeLinearLevelFromBuffer(byte[] buffer, int index, UInt32 reference_0dB = LevelReference_0dB)
        {
            UInt32 linear = ParseUint32DataFromBuffer(buffer, index);
            return (double)linear / (double)reference_0dB;
        }

        private static UInt32 ParseUint32DataFromBuffer(byte[] buffer, int index)
        {
            return BitConverter.ToUInt32(buffer, index * 4);
        }

        protected static Localization localization = Config.GetInstance().localization;

        public static string GetModuleName(byte moduleIndex)
        {
            switch (moduleIndex)
            {
                case musicModuleIndex:
                    return localization[Localization.comm_MusicModule];

                case micModuleIndex:
                    return localization[Localization.comm_MicModule];

                case echoModuleIndex:
                    return localization[Localization.comm_EchoModule];

                case reverbModuleIndex:
                    return localization[Localization.comm_ReverbModule];

                case mainOutputModuleIndex:
                    return localization[Localization.comm_MainOutputModule];

                case centerModuleIndex:
                    return localization[Localization.comm_Aux1OutputModule];

                case subModuleIndex:
                    return localization[Localization.comm_Aux2OutputModule];

                case surOutputModuleIndex:
                    return localization[Localization.comm_SurOutputModule];
            }

            return string.Empty;
        }
        
        public static bool ParseNonSystemModuleData(short index, byte[] decryptedReceivedData, X5EffectorModel effector)
        {
            if (decryptedReceivedData.Length < CommProtocolBase.ReadResponseMinLength || 
                decryptedReceivedData[CommProtocolBase.BeforCmdLength] != CommProtocolBase.ReadCmd)
            {
                return false;
            }

            if (!CommProtocolBase.CheckReadResponseIsOk(decryptedReceivedData))
            {
                Tools.LogHelper.WriteInfo("Data verification failed!");
                return false;
            }

            int moduleIndex = CommProtocolBase.GetShortFromDualBytes(decryptedReceivedData, (int)DataFrame.ModuleIndex);
            if(moduleIndex != index)
            {
                return false;
            }

            short[] dataList = CommProtocolBase.GetModuleData2ShortList(decryptedReceivedData);

            switch((byte)moduleIndex)
            {
                case musicModuleIndex:
                    return ParseMusicModuleData(dataList, effector.music);
                    
                case micModuleIndex:
                    return ParseMicModuleData(dataList, effector.mic);
                   
                case echoModuleIndex:
                    return ParseEchoModuleData(dataList, effector.echo);
                    
                case reverbModuleIndex:
                    return ParseReverbModuleData(dataList, effector.reverb);
                    
                case mainOutputModuleIndex:
                    return ParseMainOutputModuleData(dataList, effector.mainOutput);
                    
                case centerModuleIndex:
                    return ParseCenterOutputModuleData(dataList, effector.centerOutput);
                    
                case subModuleIndex:
                    return ParseSubOutputModuleData(dataList, effector.subOutput);
                    
                case surOutputModuleIndex:
                    return ParseSurOutputModuleData(dataList, effector.surOutput);
            }

            Tools.LogHelper.WriteInfo("ParseNonSystemModuleData()...Failed!");
            
            return false;
        }

        public static List<short> GetNonSystemModuleData(short moduleIndex, X5EffectorModel effector)
        {
            switch ((byte)moduleIndex)
            {
                case musicModuleIndex:
                    return GetMusicModuleData(effector.music);

                case micModuleIndex:
                    return GetMicModuleData(effector.mic);

                case echoModuleIndex:
                    return GetEchoModuleData(effector.echo);

                case reverbModuleIndex:
                    return GetReverbModuleData(effector.reverb);

                case mainOutputModuleIndex:
                    return GetMainOutputData(effector.mainOutput);

                case centerModuleIndex:
                    return GetCenterOutputData(effector.centerOutput);

                case subModuleIndex:
                    return GetSubOutputData(effector.subOutput);

                case surOutputModuleIndex:
                    return GetSurOutputData(effector.surOutput);

                default:
                    throw (new Exception("数据错误"));
            }
        }

        #region 解析数据
        private static bool ParseMusicModuleData(short[] dataList, MusicModule music)
        {
            Debug.Assert(dataList != null && music != null);

            //Log.OutputLine("Music 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, musicModuleDataLength);

            if(dataList.Length < musicModuleDataLength)
            {
                return false;
            }

            int pointer = 0;

            music.autoWorkMode.workModeInit = dataList[pointer++];
            music.autoWorkMode.workSelectInit = dataList[pointer++];
            music.highPassFilter.typeIndex = dataList[pointer++];
            music.highPassFilter.freqIndex = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music._eqGroup);
            //Log.OutputLine("解析音乐模块成功!");
            return true;
        }

        private static bool ParseMicModuleData(short[] dataList, MicModule mic)
        {
            //Log.OutputLine("解析麦克风模块...");
            Debug.Assert(dataList != null && mic != null);

            //Log.OutputLine("Mic 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, micModuleDataLength);

            if (dataList.Length < micModuleDataLength)
            {
                return false;
            }

            //Log.OutputLine("X5 Mic 模块****************");

            int pointer = 0;
            mic.fbe.initV = dataList[pointer++];
            mic.highPassFilter.freqIndex = dataList[pointer++];
            ParseCompAndLimterData(dataList, ref pointer, mic.compressor);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, mic.eqGroup);
            //Log.OutputLine("解析麦克风模块成功");

            //Log.OutputLine("Compressor attack = {0}", mic.compressor.attack.v);
            //Log.OutputLine("Compressor release = {0}", mic.compressor.release.v);
            //Log.OutputLine("Compressor threshold = {0}", mic.compressor.threshold.v);
            //Log.OutputLine("Compressor ratio = {0}", mic.compressor.ratio.v);

            //Log.OutputLine("X5 Mic 模块###################");

            return true;
        }

        private static bool ParseEchoModuleData(short[] dataList, EchoModule echo)
        {
            Debug.Assert(dataList != null && echo != null);

            //Log.OutputLine("Echo 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, echoModuleDataLength);

            if (dataList.Length < echoModuleDataLength)
            {
                return false;
            }

            int pointer = 0;

            echo.echoMixer.level.initV = dataList[pointer++];
            echo.echoMixer.phase.initV = dataList[pointer++];
            echo.directMixer.level.initV = dataList[pointer++];
            echo.directMixer.phase.initV = dataList[pointer++];
            echo.highPassFilter.freqIndex = dataList[pointer++];
            echo.lowPassFilter.freqIndex = dataList[pointer++];
            echo.preDelay.initV = dataList[pointer++];
            echo.delayTime.initV = dataList[pointer++];
            echo.repeat.initV = dataList[pointer++];
            echo.rChDelayTime.initV = dataList[pointer++];
            echo.rChPreDelay.initV = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, echo.eqGroup);
            //Log.OutputLine("解析回声模块成功");
            return true;
        }

        private static bool ParseReverbModuleData(short[] dataList, ReverbModule reverb)
        {
            Debug.Assert(dataList != null && reverb != null);

            //Log.OutputLine("Reverb 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, reverbModuleDataLength);

            if (dataList.Length < reverbModuleDataLength)
            {
                return false;
            }

            int pointer = 0;

            reverb.reverbMixer.level.initV = dataList[pointer++];
            reverb.reverbMixer.phase.initV = dataList[pointer++];
            reverb.directMixer.level.initV = dataList[pointer++];
            reverb.directMixer.phase.initV = dataList[pointer++];
            reverb.highPassFilter.freqIndex = dataList[pointer++];
            reverb.lowPassFilter.freqIndex = dataList[pointer++];
            reverb.reverbTime.initV = dataList[pointer++];
            reverb.preDelay.initV = dataList[pointer++];
            //Log.OutputLine("解析混响模块成功");
            return true;
        }

        private static bool ParseMainOutputModuleData(short[] dataList, MainOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("MainOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, mainOutputModuleDataLength);

            if (dataList.Length < mainOutputModuleDataLength)
            {
                return false;
            }

            int pointer = 0;
            ParseOutputMixerData(dataList, ref pointer, output.musicMixer);
            ParseOutputMixerData(dataList, ref pointer, output.micMixer);
            ParseOutputMixerData(dataList, ref pointer, output.echoMixer);
            ParseOutputMixerData(dataList, ref pointer, output.reverbMixer);

            ParseCompAndLimterData(dataList, ref pointer, output.limiter);

            ParseOutputChannelData(dataList, ref pointer, output.leftChannel);
            ParseOutputChannelData(dataList, ref pointer, output.rightChannel);

            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.discoEQGroup);
            
            //Log.OutputLine("解析主输出模块成功");
            return true;
        }

        private static bool ParseCenterOutputModuleData(short[] dataList, CenterOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("CenterOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, centerOutputModuleDataLength);

            if (dataList.Length < centerOutputModuleDataLength)
            {
                return false;
            }

            int pointer = 0;
            ParseOutputMixerData(dataList, ref pointer, output.musicMixer);
            ParseOutputMixerData(dataList, ref pointer, output.micMixer);
            ParseOutputMixerData(dataList, ref pointer, output.echoMixer);
            ParseOutputMixerData(dataList, ref pointer, output.reverbMixer);


            output.singHPF.typeIndex = dataList[pointer++];
            output.singHPF.freqIndex = dataList[pointer++];
            pointer++;
            output.discoHPF.typeIndex = dataList[pointer++];
            output.discoHPF.freqIndex = dataList[pointer++];
            pointer++;

            ParseCompAndLimterData(dataList, ref pointer, output.limiter);

            ParseOutputChannelData(dataList, ref pointer, output.monoChannel);

            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.discoEQGroup);

            //Log.OutputLine("解析主输出模块成功");
            return true;
        }

        private static bool ParseSubOutputModuleData(short[] dataList,  SubOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("SubOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, subOutputModuleDataLength);

            if (dataList.Length < subOutputModuleDataLength)
            {
                return false;
            }

            int pointer = 0;
            ParseOutputMixerData(dataList, ref pointer, output.musicMixer);
            ParseOutputMixerData(dataList, ref pointer, output.micMixer);

            output.singHPF.typeIndex = dataList[pointer++];
            output.singHPF.freqIndex = dataList[pointer++];
            output.singHPF.qIndex = dataList[pointer++];
            output.discoHPF.typeIndex = dataList[pointer++];
            output.discoHPF.freqIndex = dataList[pointer++];
            output.discoHPF.qIndex = dataList[pointer++];
            output.singLPF.typeIndex = dataList[pointer++];
            output.singLPF.freqIndex = dataList[pointer++];
            output.singLPF.qIndex = dataList[pointer++];
            output.discoLPF.typeIndex = dataList[pointer++];
            output.discoLPF.freqIndex = dataList[pointer++];
            output.discoLPF.qIndex = dataList[pointer++];

            ParseCompAndLimterData(dataList, ref pointer, output.limiter);

            ParseOutputChannelData(dataList, ref pointer, output.monoChannel);

            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.discoEQGroup);

            //Log.OutputLine("解析主输出模块成功");
            return true;
        }

        private static bool ParseSurOutputModuleData(short[] dataList, SurOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("SurOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, surOutputModuleDataLength);

            if (dataList.Length < surOutputModuleDataLength)
            {
                return false;
            }

            int pointer = 0;
            ParseOutputMixerData(dataList, ref pointer, output.musicMixer);
            ParseOutputMixerData(dataList, ref pointer, output.micMixer);
            ParseOutputMixerData(dataList, ref pointer, output.echoMixer);
            ParseOutputMixerData(dataList, ref pointer, output.reverbMixer);

            pointer += 1;
            output.singHPF.freqIndex = dataList[pointer++];
            pointer += 2;
            output.discoHPF.freqIndex = dataList[pointer++];
            pointer += 1;

            ParseCompAndLimterData(dataList, ref pointer, output.limiter);

            ParseOutputChannelData(dataList, ref pointer, output.leftChannel);
            ParseOutputChannelData(dataList, ref pointer, output.rightChannel);

            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.discoEQGroup);

            //Log.OutputLine("解析主输出模块成功");
            return true;
        }

        protected static void ParseOutputChannelData(short[] dataList, ref int pointer, X5OutputChannel outputChannel)
        {
            Debug.Assert(dataList != null && outputChannel != null);

            outputChannel.delay.initV = dataList[pointer++];
            outputChannel.mute.initV = dataList[pointer++];
            pointer++;
        }

        public static void ParseCompAndLimterData(short[] dataList, ref int pointer, X5_CompressorLimiter compAndLimiter)
        {
            Debug.Assert(dataList != null && compAndLimiter != null);

            compAndLimiter.attack.initV = dataList[pointer++];
            compAndLimiter.release.initV = dataList[pointer++];
            compAndLimiter.threshold.initV = dataList[pointer++];
            compAndLimiter.ratio.initV = dataList[pointer++];
        }

        protected static void ParseOutputMixerData(short[] dataList, ref int pointer, OutputMixer outputMixer)
        {
            Debug.Assert(dataList != null && outputMixer != null);

            outputMixer.singMixer.level.initV = dataList[pointer++];
            outputMixer.singMixer.phase.initV = dataList[pointer++];
            outputMixer.discoMixer.level.initV = dataList[pointer++];
            outputMixer.discoMixer.phase.initV = dataList[pointer++];
        }
        #endregion

        #region 获取模块数据
        private static List<short> GetMusicModuleData(MusicModule music)
        {
            Debug.Assert(music != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(music.autoWorkMode.wm.v);
            moduleData.Add(music.autoWorkMode.ws.v);
            moduleData.Add(music.highPassFilter.typeIndex);
            moduleData.Add(music.highPassFilter.freqIndex);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music._eqGroup);

            return moduleData;
        }

        private static List<short> GetMicModuleData(MicModule mic)
        {
            Debug.Assert(mic != null);
            List<short> moduleData = new List<short>();

            moduleData.Add(mic.fbe.v);
            moduleData.Add(mic.hpf.freqIndex);

            AppendCompAndLimter2DataBuffer(moduleData, mic.compressor);

            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, mic.eqGroup);

            return moduleData;
        }

        

        private static List<short> GetEchoModuleData(EchoModule echo)
        {
            Debug.Assert(echo != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(echo.echoMixer.level.v);
            moduleData.Add(echo.echoMixer.phase.v);
            moduleData.Add(echo.directMixer.level.v);
            moduleData.Add(echo.directMixer.phase.v);
            moduleData.Add(echo.highPassFilter.freqIndex);
            moduleData.Add(echo.lowPassFilter.freqIndex);
            moduleData.Add(echo.preDelay.v);
            moduleData.Add(echo.delayTime.v);
            moduleData.Add(echo.repeat.v);
            moduleData.Add(echo.rChDelayTime.v);
            moduleData.Add(echo.rChPreDelay.v);
            
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, echo.eqGroup);

            return moduleData;
        }

        private static List<short> GetReverbModuleData(ReverbModule reverb)
        {
            Debug.Assert(reverb != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(reverb.reverbMixer.level.v);
            moduleData.Add(reverb.reverbMixer.phase.v);
            moduleData.Add(reverb.directMixer.level.v);
            moduleData.Add(reverb.directMixer.phase.v);
            moduleData.Add(reverb.highPassFilter.freqIndex);
            moduleData.Add(reverb.lowPassFilter.freqIndex);
            moduleData.Add(reverb.reverbTime.v);
            moduleData.Add(reverb.preDelay.v);

            return moduleData;
        }

        private static List<short> GetMainOutputData(MainOutputModule output)
        {
            List<short> moduleData = new List<short>();

            AppendOutputMixerData(moduleData, output.musicMixer);
            AppendOutputMixerData(moduleData, output.micMixer);
            AppendOutputMixerData(moduleData, output.echoMixer);
            AppendOutputMixerData(moduleData, output.reverbMixer);

            AppendCompAndLimter2DataBuffer(moduleData, output.limiter);

            AppendOutputChannelData(moduleData, output.leftChannel);
            AppendOutputChannelData(moduleData, output.rightChannel);

            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.discoEQGroup);

            return moduleData;
        }

        private static List<short> GetCenterOutputData(CenterOutputModule output)
        {
            List<short> moduleData = new List<short>();

            AppendOutputMixerData(moduleData, output.musicMixer);
            AppendOutputMixerData(moduleData, output.micMixer);
            AppendOutputMixerData(moduleData, output.echoMixer);
            AppendOutputMixerData(moduleData, output.reverbMixer);

            moduleData.Add(output.singHPF.typeIndex);
            moduleData.Add(output.singHPF.freqIndex);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);
            moduleData.Add(output.discoHPF.typeIndex);
            moduleData.Add(output.discoHPF.freqIndex); 
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);

            AppendCompAndLimter2DataBuffer(moduleData, output.limiter);

            AppendOutputChannelData(moduleData, output.monoChannel);

            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.discoEQGroup);

            return moduleData;
        }

        private static List<short> GetSubOutputData(SubOutputModule output)
        {
            List<short> moduleData = new List<short>();

            AppendOutputMixerData(moduleData, output.musicMixer);
            AppendOutputMixerData(moduleData, output.micMixer);

            moduleData.Add(output.singHPF.typeIndex);
            moduleData.Add(output.singHPF.freqIndex);
            moduleData.Add(output.singHPF.qIndex);
            moduleData.Add(output.discoHPF.typeIndex);
            moduleData.Add(output.discoHPF.freqIndex);
            moduleData.Add(output.discoHPF.qIndex);
            moduleData.Add(output.singLPF.typeIndex);
            moduleData.Add(output.singLPF.freqIndex);
            moduleData.Add(output.singLPF.qIndex);
            moduleData.Add(output.discoLPF.typeIndex);
            moduleData.Add(output.discoLPF.freqIndex);
            moduleData.Add(output.discoLPF.qIndex);

            AppendCompAndLimter2DataBuffer(moduleData, output.limiter);

            AppendOutputChannelData(moduleData, output.monoChannel);

            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.discoEQGroup);

            return moduleData;
        }

        private static List<short> GetSurOutputData(SurOutputModule output)
        {
            List<short> moduleData = new List<short>();

            AppendOutputMixerData(moduleData, output.musicMixer);
            AppendOutputMixerData(moduleData, output.micMixer);
            AppendOutputMixerData(moduleData, output.echoMixer);
            AppendOutputMixerData(moduleData, output.reverbMixer);

            moduleData.Add(0);
            moduleData.Add(output.singHPF.freqIndex);
            moduleData.Add(0);
            moduleData.Add(0);
            moduleData.Add(output.discoHPF.freqIndex);
            moduleData.Add(0);

            AppendCompAndLimter2DataBuffer(moduleData, output.limiter);

            AppendOutputChannelData(moduleData, output.leftChannel);
            AppendOutputChannelData(moduleData, output.rightChannel);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.discoEQGroup);

            return moduleData;
        }

        protected static void AppendOutputChannelData(List<short> data, X5OutputChannel outputChannel)
        {
            Debug.Assert(data != null && outputChannel != null);

            data.Add(outputChannel.delay.v);
            data.Add(outputChannel.mute.v);
            data.Add(0);
        }

        protected static void AppendOutputMixerData(List<short> data, OutputMixer outputMixer)
        {
            Debug.Assert(data != null && outputMixer != null);

            data.Add(outputMixer.singMixer.level.v);
            data.Add(outputMixer.singMixer.phase.v);
            data.Add(outputMixer.discoMixer.level.v);
            data.Add(outputMixer.discoMixer.phase.v);
        }

        protected static void AppendCompAndLimter2DataBuffer(List<short> data, X5_CompressorLimiter compAndLimiter)
        {
            Debug.Assert(data != null && compAndLimiter != null);

            data.Add(compAndLimiter.attack.v);
            data.Add(compAndLimiter.release.v);
            data.Add(compAndLimiter.threshold.v);
            data.Add(compAndLimiter.ratio.v);
        }
        #endregion
    }
}
