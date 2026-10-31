using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Effectors.VKxDCM.Modules;
using AEC.Effectors.VKxDCM;
using AEC.Misc;
using AEC.Common;

namespace AEC.Comm.VKxDCMComm
{
    //V系列协议
    class VKxDCMCommProtocol : CommProtocolBase
    {
#region 协议中的常量
        public const byte musicModuleIndex = 0x01;
        public const byte micAModuleIndex = 0x02;
        public const byte micBModuleIndex = 0x03;
        public const byte echoModuleIndex = 0x04;
        public const byte reverbModuleIndex = 0x05;
        public const byte mainOutputModuleIndex = 0x06;
        public const byte aux1ModuleIndex = 0x07;
        public const byte aux2ModuleIndex = 0x08;
        public const byte surOutputModuleIndex = 0x09;

        public const int musicModuleDataLength = 84;
        public const int micModuleDataLength = 104;
        public const int echoModuleDataLength = 41;
        public const int reverbModuleDataLength = 41;
        public const int outputModuleDataLength = 69;

        private const byte SystemStatusDataLength = 22 * 4;

        public const UInt32 LimiterReference_0dB = 0x7fffffff;
        public const UInt32 CompressorReference_0dB = 0x02000000;
        public const UInt32 LevelReference_0dB = 0x7fffffff;

        public enum MusicParamAddr
        {
            workMode = 0x00,
            workSelect = 0x01,
            bassExciterOn = 0x02,
            bassExciterXoverFreq = 0x03,
            bassExciterFBGain = 0x04,
            bassExciterBoostLevel = 0x05,
            hpfFreq = 0x06,
            hpfType = 0x07,
            eq = 0x08,
            eqPass = 0x09,
        }

        public enum MicParamAddr
        {
            micLevel = 0x00,
            noiseGate = 0x01,
            fbe = 0x02,
            attack = 0x03,
            release = 0x04,
            threshold = 0x05,
            ratio = 0x06,

            exciter1Select = 0x07,

            bassExciterOn = 0x08,
            bassExciterXoverFreq = 0x09,
            bassExciterFBGain = 0x0A,
            bassExciterBoostLevel = 0x0B,

            exciter1On = 0x0C,
            exciter1XoverFreq = 0x0D,
            exciter1BandWidth = 0x0E,
            exciter1FBGain = 0x0F,
            exciter1BoostLevel = 0x10,
            exciter1OffsetLevel = 0x11,

            exciter2On = 0x12,
            exciter2XoverFreq = 0x13,
            exciter2BandWidth = 0x14,
            exciter2FBGain = 0x15,
            exciter2BoostLevel = 0x16,
            exciter2OffsetLevel = 0x17,

            hpfFreq = 0x18,
            hpfType = 0x19,
            lpfFreq = 0x1A,
            lpfType = 0x1B,
            
            eq = 0x1C,
            eqPass = 0x1D,
        }
        
        public enum EchoParamAddr
        {
            echoLevel = 0x00,
            echoLevelPhase = 0x01,
            directLevel = 0x02,
            directLevelPhase = 0x03,
            echoHpfFreq = 0x04,
            echoHpfType = 0x05,
            echoLpfFreq = 0x06,
            echoLpfType = 0x07,
            echoPreDelay = 0x08,
            echoDelay = 0x09,
            echoRepeat = 0x0a,
            echoRChPreDelay = 0x0b,
            echoRChDelay = 0x0c,
            reserved = 0x0d,
            eq = 0x0e,
            eqPass = 0x0f,
        }

        public enum ReverbParamAddr
        {
            reverbMicLevel = 0x00,
            reverbMicLevelPhase = 0x01,
            directMicLevel = 0x02,
            directMicLevelPhase = 0x03,            
            reverbMicHPFFreq = 0x04,
            reverbMicHPFType = 0x05,
            reverbMicLPFFreq = 0x06,
            reverbMicLPFType = 0x07,
            reverbPreDelay = 0x08,
            reverbTime = 0x09,
            reserve1 = 0x0a,
            reserve2 = 0x0b,
            reserve3 = 0x0c,
            reserve4 = 0x0d,
            eq = 0x0e,
            eqPass = 0x0f,
        }

        public enum OutputBaseParamAddr
        {
            musicSingLevel = 0x00,
            musicSingLevelPhase = 0x01,
            musicDiscoLevel = 0x02,
            musicDiscoLevelPhase = 0x03,
            micSingLevel = 0x04,
            micSingLevelPhase = 0x05,
            micDiscoLevel = 0x06,
            micDiscoLevelPhase = 0x07,
            echoSingLevel = 0x08,
            echoSingLevelPhase = 0x09,
            echoDiscoLevel = 0x0a,
            echoDiscoLevelPhase = 0x0b,
            reverbSingLevel = 0x0c,
            reverbSingLevelPhase = 0x0d,
            reverbDiscoLevel = 0x0e,
            reverbDiscoLevelPhase = 0x0f,

            attack = 0x10,
            release = 0x11,
            threshold = 0x12,
            clipThreshold = 0x13,
            clipRelease = 0x14,

            hpfFreq = 0x15,
            hpfType = 0x16,

            lpfFreq = 0x17,
            lpfType = 0x18,
            
            eq = 0x20,
            eqPass = 0x21,
        }

        public enum OutputParamAddr
        {
            leftChDelay = 0x19,
            leftChMute = 0x1a,
            leftChPhase = 0x1b,
            rightChDelay = 0x1c,
            rightChMute = 0x1d,
            rightChPhase = 0x1e,
        }

        public enum AuxParamAddr
        {
            delay = 0x19,
            mute = 0x1a,
            phase = 0x1b,
        }

        public static readonly Dictionary<byte, byte> eqAddrDict = new Dictionary<byte, byte>()
        {
            {musicModuleIndex, (byte)MusicParamAddr.eq},
            {micAModuleIndex,  (byte)MicParamAddr.eq},            
            {micBModuleIndex,  (byte)MicParamAddr.eq},            
            {echoModuleIndex, (byte)EchoParamAddr.eq},
            {reverbModuleIndex, (byte)ReverbParamAddr.eq},
            {mainOutputModuleIndex, (byte)OutputBaseParamAddr.eq},
            {aux1ModuleIndex, (byte)OutputBaseParamAddr.eq},
            {aux2ModuleIndex, (byte)OutputBaseParamAddr.eq},
            {surOutputModuleIndex, (byte)OutputBaseParamAddr.eq},
            //{musicModuleIndex, 0x03},
            //{micModuleIndex, 0x06},            
            //{echoModuleIndex, 0x0e},            
            //{mainOutputModuleIndex, 0x20},
            //{aux1ModuleIndex, 0x1d},
            //{aux2ModuleIndex, 0x1d},
            //{surOutputModuleIndex, 0x20},
        };

        

#endregion

        public VKxDCMCommProtocol() :
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

                case micAModuleIndex:
                    return localization[Localization.comm_MicAModule];

                case micBModuleIndex:
                    return localization[Localization.comm_MicBModule];

                case echoModuleIndex:
                    return localization[Localization.comm_EchoModule];

                case reverbModuleIndex:
                    return localization[Localization.comm_ReverbModule];

                case mainOutputModuleIndex:
                    return localization[Localization.comm_MainOutputModule];

                case aux1ModuleIndex:
                    return localization[Localization.comm_Aux1OutputModule];

                case aux2ModuleIndex:
                    return localization[Localization.comm_Aux2OutputModule];

                case surOutputModuleIndex:
                    return localization[Localization.comm_SurOutputModule];
            }

            return string.Empty;
        }
        
        public static bool ParseNonSystemModuleData(short index, byte[] decryptedReceivedData, VKxDCMEffectorModel effector)
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
                    
                case micAModuleIndex:
                    return ParseMicModuleData(dataList, effector.micA);

                case micBModuleIndex:
                    return ParseMicModuleData(dataList, effector.micB);
                   
                case echoModuleIndex:
                    return ParseEchoModuleData(dataList, effector.echo);
                    
                case reverbModuleIndex:
                    return ParseReverbModuleData(dataList, effector.reverb);
                    
                case mainOutputModuleIndex:
                    return ParseStereoOutputModuleData(dataList, effector.mainOutput);
                    
                case aux1ModuleIndex:
                    return ParseMonoOutputModuleData(dataList, effector.aux1Output);
                    
                case aux2ModuleIndex:
                    return ParseMonoOutputModuleData(dataList, effector.aux2Output);
                    
                case surOutputModuleIndex:
                    return ParseStereoOutputModuleData(dataList, effector.surOutput);
            }

            Tools.LogHelper.WriteInfo("ParseNonSystemModuleData()...Failed!");
            
            return false;
        }

        public static List<short> GetNonSystemModuleData(short moduleIndex, VKxDCMEffectorModel effector)
        {
            switch ((byte)moduleIndex)
            {
                case musicModuleIndex:
                    return GetMusicModuleData(effector.music);

                case micAModuleIndex:
                    return GetMicModuleData(effector.micA);

                case micBModuleIndex:
                    return GetMicModuleData(effector.micB);

                case echoModuleIndex:
                    return GetEchoModuleData(effector.echo);

                case reverbModuleIndex:
                    return GetReverbModuleData(effector.reverb);

                case mainOutputModuleIndex:
                    return GetStereoOutputData(effector.mainOutput);

                case aux1ModuleIndex:
                    return GetMonoOutputData(effector.aux1Output);

                case aux2ModuleIndex:
                    return GetMonoOutputData(effector.aux2Output);

                case surOutputModuleIndex:
                    return GetStereoOutputData(effector.surOutput);

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

            ParseBassExciterData(dataList, ref pointer, music.bassExciter);

            music.singHPF.freqIndex = dataList[pointer++];
            music.singHPF.typeIndex = dataList[pointer++];
            music.discoHPF.freqIndex = dataList[pointer++];
            music.discoHPF.typeIndex = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music.discoEQGroup);
            //Log.OutputLine("解析音乐模块加密{0}", music.dataIsPrivate);
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

            int pointer = 0;

            mic.micLevel.initV = dataList[pointer++];

            mic.micNoiseGate.initV = dataList[pointer++];
            mic.fbe.initV = dataList[pointer++];

            ParseCompressorData(dataList, ref pointer, mic.compressor);

            mic.exciter1Select.initV = dataList[pointer++];
            
            ParseBassExciterData(dataList, ref pointer, mic.bassExciter);
            ParseExciterData(dataList, ref pointer, mic.exciter1);
            ParseExciterData(dataList, ref pointer, mic.exciter2);

            mic.highPassFilter.freqIndex = dataList[pointer++];

            pointer++;

            mic.lowPassFilter.freqIndex = dataList[pointer++];
            mic.lowPassFilter.typeIndex = dataList[pointer++];

            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, mic.eqGroup);
            //Log.OutputLine("解析麦克风模块成功");
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
            //echo.highPassFilter.typeIndex = dataList[pointer++];
            pointer++;
            echo.lowPassFilter.freqIndex = dataList[pointer++];
            //echo.lowPassFilter.typeIndex = dataList[pointer++];
            pointer++;
            echo.preDelay.initV = dataList[pointer++];
            echo.delayTime.initV = dataList[pointer++];
            echo.repeat.initV = dataList[pointer++];
            echo.rChPreDelay.initV = dataList[pointer++];
            echo.rChDelayTime.initV = dataList[pointer++];
            
            pointer += 1;

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
            //reverb.highPassFilter.typeIndex = dataList[pointer++];
            pointer++;
            reverb.lowPassFilter.freqIndex = dataList[pointer++];
            reverb.lowPassFilter.typeIndex = dataList[pointer++];
            reverb.preDelay.initV = dataList[pointer++];
            reverb.reverbTime.initV = dataList[pointer++];
            
            pointer += 4;
            
            //reverb.dataIsPrivateParam = dataList[pointer++];
            //Log.OutputLine("解析混响模块加密{0}", reverb.dataIsPrivate);
            //Log.OutputLine("解析混响模块成功");
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, reverb.eqGroup);
            return true;
        }

        private static bool ParseStereoOutputModuleData(short[] dataList, StereoOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("StereoOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, outputModuleDataLength);

            if (dataList.Length < outputModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.leftChannel.delay.initV = dataList[pointer++];
            output.leftChannel.mute.initV = dataList[pointer++];
            output.leftChannel.phase.initV = dataList[pointer++];
            output.rightChannel.delay.initV = dataList[pointer++];
            output.rightChannel.mute.initV = dataList[pointer++];
            output.rightChannel.phase.initV = dataList[pointer++];
            pointer++;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.eqGroup);
            output.dataIsPrivateParam = dataList[pointer++];
            //Log.OutputLine("解析Stereo输出模块加密{0}", output.dataIsPrivate);
            //Log.OutputLine("解析主输出模块成功");
            return true;
        }

        private static bool ParseMonoOutputModuleData(short[] dataList, MonoOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("MonoOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, outputModuleDataLength);

            if (dataList.Length < outputModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.monoChannel.delay.initV = dataList[pointer++];
            output.monoChannel.mute.initV = dataList[pointer++];
            output.monoChannel.phase.initV = dataList[pointer++];
            pointer += 4;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.eqGroup);
            output.dataIsPrivateParam = dataList[pointer++];
            //Log.OutputLine("解析Momo输出模块加密{0}", output.dataIsPrivate);
            //Log.OutputLine("解析辅助输出模块成功");
            return true;
        }

        private static int ParseOutputBaseData(short[] dataList, OutputBaseModule output)
        {
            Debug.Assert(dataList != null && output != null);
            
            int pointer = 0;

            ParseOutputMixerData(dataList, ref pointer, output.musicMixer);
            ParseOutputMixerData(dataList, ref pointer, output.micMixer);
            ParseOutputMixerData(dataList, ref pointer, output.echoMixer);
            ParseOutputMixerData(dataList, ref pointer, output.reverbMixer);

            ParseLimiterData(dataList, ref pointer, output.limiter);

            output.highPassFilter.freqIndex = dataList[pointer++];
            output.highPassFilter.typeIndex = dataList[pointer++];
            output.lowPassFilter.freqIndex = dataList[pointer++];
            output.lowPassFilter.typeIndex = dataList[pointer++];

            return pointer;
        }

        protected static void ParseCompressorData(short[] dataList, ref int pointer, Compressor compressor)
        {
            Debug.Assert(dataList != null && compressor != null);

            compressor.attack.initV = dataList[pointer++];
            compressor.release.initV = dataList[pointer++];
            compressor.threshold.initV = dataList[pointer++];
            compressor.ratio.initV = dataList[pointer++];
        }

        protected static void ParseBassExciterData(short[] dataList, ref int pointer, BassExciter bassExciter)
        {
            Debug.Assert(dataList != null && bassExciter != null);

            bassExciter.on.initV = dataList[pointer++];
            bassExciter.xoverFreq.initV = dataList[pointer++];
            bassExciter.fbGain.initV = dataList[pointer++];
            bassExciter.boostLevel.initV = dataList[pointer++];
        }

        protected static void ParseExciterData(short[] dataList, ref int pointer, Exciter exciter)
        {
            Debug.Assert(dataList != null && exciter != null);

            exciter.on.initV = dataList[pointer++];
            exciter.freq.initV = dataList[pointer++];
            exciter.bandWidth.initV = dataList[pointer++];
            exciter.fbGain.initV = dataList[pointer++];
            exciter.boostLevel.initV = dataList[pointer++];
            exciter.offsetLevel.initV = dataList[pointer++];
        }

        protected static void ParseOutputMixerData(short[] dataList, ref int pointer, OutputMixer outputMixer)
        {
            Debug.Assert(dataList != null && outputMixer != null);

            outputMixer.singMixer.level.initV = dataList[pointer++];
            outputMixer.singMixer.phase.initV = dataList[pointer++];
            outputMixer.discoMixer.level.initV = dataList[pointer++];
            outputMixer.discoMixer.phase.initV = dataList[pointer++];
        }

        protected static void ParseLimiterData(short[] dataList, ref int pointer, Limiter limiter)
        {
            Debug.Assert(dataList != null && limiter != null);

            limiter.attack.initV = dataList[pointer++];
            limiter.release.initV = dataList[pointer++];
            limiter.threshold.initV = dataList[pointer++];
            limiter.clipThreshold.initV = dataList[pointer++];
            limiter.clipRelease.initV = dataList[pointer++];
        }
        #endregion

        #region 获取模块数据
        private static List<short> GetMusicModuleData(MusicModule music)
        {
            Debug.Assert(music != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(music.autoWorkMode.wm.v);
            moduleData.Add(music.autoWorkMode.ws.v);

            AppendBassExciterData(moduleData, music.bassExciter);

            moduleData.Add(music.singHPF.freqIndex);
            moduleData.Add(music.singHPF.typeIndex);
            moduleData.Add(music.discoHPF.freqIndex);
            moduleData.Add(music.discoHPF.typeIndex);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music.discoEQGroup);
            //moduleData.Add(music.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetMicModuleData(MicModule mic)
        {
            Debug.Assert(mic != null);
            List<short> moduleData = new List<short>();

            moduleData.Add(mic.micLevel.v);

            moduleData.Add(mic.micNoiseGate.v);
            moduleData.Add(mic.fbe.v);

            AppendCompressorData(moduleData, mic.compressor);

            moduleData.Add(mic.exciter1Select.v);
            AppendBassExciterData(moduleData, mic.bassExciter);
            AppendExciterData(moduleData, mic.exciter1);
            AppendExciterData(moduleData, mic.exciter2);

            moduleData.Add(mic.hpf.freqIndex);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);
            moduleData.Add(mic.lpf.freqIndex);
            moduleData.Add(mic.lpf.typeIndex);

            //111
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
            moduleData.Add(echo.highPassFilter.typeIndex);
            moduleData.Add(echo.lowPassFilter.freqIndex);
            moduleData.Add(echo.lowPassFilter.typeIndex);
            moduleData.Add(echo.preDelay.v);
            moduleData.Add(echo.delayTime.v);
            moduleData.Add(echo.repeat.v);
            moduleData.Add(echo.rChPreDelay.v);
            moduleData.Add(echo.rChDelayTime.v);
            
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);

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
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);
            //moduleData.Add(reverb.highPassFilter.typeIndex);

            moduleData.Add(reverb.lowPassFilter.freqIndex);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);
            //moduleData.Add(reverb.lowPassFilter.typeIndex);

            moduleData.Add(reverb.preDelay.v);
            moduleData.Add(reverb.reverbTime.v);

            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 4);

            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, reverb.eqGroup);

            //moduleData.Add(reverb.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetStereoOutputData(StereoOutputModule output)
        {
            List<short> moduleData = GetOutputBaseData(output);
            moduleData.Add(output.leftChannel.delay.v);
            moduleData.Add(output.leftChannel.mute.v);
            moduleData.Add(output.leftChannel.phase.v);
            moduleData.Add(output.rightChannel.delay.v);
            moduleData.Add(output.rightChannel.mute.v);
            moduleData.Add(output.rightChannel.phase.v);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.eqGroup);
            //moduleData.Add(output.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetMonoOutputData(MonoOutputModule output)
        {
            List<short> moduleData = GetOutputBaseData(output);
            moduleData.Add(output.monoChannel.delay.v);
            moduleData.Add(output.monoChannel.mute.v);
            moduleData.Add(output.monoChannel.phase.v);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 4);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.eqGroup);
            moduleData.Add(output.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetOutputBaseData(OutputBaseModule output)
        {
            List<short> moduleData = new List<short>();

            AppendOutputMixerData(moduleData, output.musicMixer);
            AppendOutputMixerData(moduleData, output.micMixer);
            AppendOutputMixerData(moduleData, output.echoMixer);
            AppendOutputMixerData(moduleData, output.reverbMixer);

            AppendLimiterData(moduleData, output.limiter);

            moduleData.Add(output.highPassFilter.freqIndex);
            moduleData.Add(output.highPassFilter.typeIndex);

            moduleData.Add(output.lowPassFilter.freqIndex);
            moduleData.Add(output.lowPassFilter.typeIndex);

            return moduleData;
        }

        private static void AppendCompressorData(List<short> data, Compressor compressor)
        {
            Debug.Assert(data != null && compressor != null);

            data.Add(compressor.attack.v);
            data.Add(compressor.release.v);
            data.Add(compressor.threshold.v);
            data.Add(compressor.ratio.v);
        }

        private static void AppendBassExciterData(List<short> data, BassExciter bassExciter)
        {
            Debug.Assert(data != null && bassExciter != null);

            data.Add(bassExciter.on.v);
            data.Add(bassExciter.xoverFreq.v);
            data.Add(bassExciter.fbGain.v);
            data.Add(bassExciter.boostLevel.v);
        }

        private static void AppendExciterData(List<short> data, Exciter exciter)
        {
            Debug.Assert(data != null && exciter != null);

            data.Add(exciter.on.v);
            data.Add(exciter.freq.v);
            data.Add(exciter.bandWidth.v);
            data.Add(exciter.fbGain.v);
            data.Add(exciter.boostLevel.v);
            data.Add(exciter.offsetLevel.v);
        }

        protected static void AppendOutputMixerData(List<short> data, OutputMixer outputMixer)
        {
            Debug.Assert(data != null && outputMixer != null);

            data.Add(outputMixer.singMixer.level.v);
            data.Add(outputMixer.singMixer.phase.v);
            data.Add(outputMixer.discoMixer.level.v);
            data.Add(outputMixer.discoMixer.phase.v);
        }

        private static void AppendLimiterData(List<short> data, Limiter limiter)
        {
            Debug.Assert(data != null && limiter != null);

            data.Add(limiter.attack.v);
            data.Add(limiter.release.v);
            data.Add(limiter.threshold.v);
            data.Add(limiter.clipThreshold.v);
            data.Add(limiter.clipRelease.v);
        }
        #endregion
    }
}
