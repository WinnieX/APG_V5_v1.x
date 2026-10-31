using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Effectors.VKxPIIDCM.Modules;
using AEC.Effectors.VKxPIIDCM;
using AEC.Misc;
using AEC.Common;

namespace AEC.Comm.VKxPIIDCMComm
{
    //V系列协议
    class VKxPIIDCMCommProtocol : CommProtocolBase
    {
#region 协议中的常量
        public const byte musicModuleIndex = 0x01;
        public const byte micAModuleIndex = 0x02;
        public const byte echoModuleIndex = 0x03;
        public const byte reverbModuleIndex = 0x04;
        public const byte mainOutputModuleIndex = 0x05;
        public const byte aux1ModuleIndex = 0x06;
        public const byte aux2ModuleIndex = 0x07;
        public const byte surOutputModuleIndex = 0x08;
        public const byte micBModuleIndex = 0x09;

        public const int musicModuleDataLength = 45;
        public const int micModuleDataLength = 104;
        public const int echoModuleDataLength = 38;
        public const int reverbModuleDataLength = 36;
        public const int stereooutputModuleDataLength = 113;
        public const int monoOutputModuleDataLength = 110;

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
            bassExciterOn = 0x06,
            bassExciterXoverFreq = 0x07,
            bassExciterFBGain = 0x08,
            bassExciterBoostLevel = 0x09,
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

            micLevel = 0x09,

            hpfType = 0x0a,
            lpfFreq = 0x0b,
            lpfType = 0x0c,

            exciter1Select = 0x0d,

            bassExciterOn = 0x0e,
            bassExciterXoverFreq = 0x0f,
            bassExciterFBGain = 0x10,
            bassExciterBoostLevel = 0x11,

            exciter1On = 0x12,
            exciter1XoverFreq = 0x13,
            exciter1BandWidth = 0x14,
            exciter1FBGain = 0x15,
            exciter1BoostLevel = 0x16,
            exciter1OffsetLevel = 0x17,

            exciter2On = 0x18,
            exciter2XoverFreq = 0x19,
            exciter2BandWidth = 0x1a,
            exciter2FBGain = 0x1b,
            exciter2BoostLevel = 0x1c,
            exciter2OffsetLevel = 0x1d,
        }
        
        public enum EchoParamAddr
        {
            echoLevel = 0x00,
            echoLevelPhase = 0x01,
            directLevel = 0x02,
            directLevelPhase = 0x03,
            echoHpfFreq = 0x04,
            echoLpfFreq = 0x05,
            echoPreDelay = 0x06,
            echoDelay = 0x07,
            echoRepeat = 0x08,
            echoRChDelay = 0x09,
            echoRChPreDelay = 0x0a,
            eq = 0x0b,
            eqPass = 0x0c,

            //echoLevel = 0x00,
            //echoLevelPhase = 0x01,
            //directLevel = 0x02,
            //directLevelPhase = 0x03,
            //echoHpfFreq = 0x04,
            //echoHpfType = 0x05,
            //echoLpfFreq = 0x06,
            //echoLpfType = 0x07,
            //echoPreDelay = 0x08,
            //echoDelay = 0x09,
            //echoRepeat = 0x0a,
            //echoRChPreDelay = 0x0b,
            //echoRChDelay = 0x0c,
            //reserved = 0x0d,
            //eq = 0x0e,
            //eqPass = 0x0f,
        }

        public enum ReverbParamAddr
        {
            reverbMicLevel = 0x00,
            reverbMicLevelPhase = 0x01,
            directMicLevel = 0x02,
            directMicLevelPhase = 0x03,            
            reverbMicHPFFreq = 0x04,
            reverbMicLPFFreq = 0x05,
            reverbTime = 0x06,
            reverbPreDelay = 0x07,
            reserved = 0x08,
            eq = 0x09,
            eqPass = 0x0a,

            //reverbMicLevel = 0x00,
            //reverbMicLevelPhase = 0x01,
            //directMicLevel = 0x02,
            //directMicLevelPhase = 0x03,
            //reverbMicHPFFreq = 0x04,
            //reverbMicHPFType = 0x05,
            //reverbMicLPFFreq = 0x06,
            //reverbMicLPFType = 0x07,
            //reverbPreDelay = 0x08,
            //reverbTime = 0x09,
            //reserve1 = 0x0a,
            //reserve2 = 0x0b,
            //reserve3 = 0x0c,
            //reserve4 = 0x0d,
            //eq = 0x0e,
            //eqPass = 0x0f,
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
            hpfQ = 0x0A,
            lpfType = 0x0B,
            lpfFreq = 0x0C,
            lpfQ = 0x0D,
            attack = 0x10,
            release = 0x11,
            threshold = 0x12,
            ratio = 0x13,
            reserved = 0x14,
            eq = 0x20,
            eqPass = 0x21,

            //musicSingLevel = 0x00,
            //musicSingLevelPhase = 0x01,
            //musicDiscoLevel = 0x02,
            //musicDiscoLevelPhase = 0x03,
            //micSingLevel = 0x04,
            //micSingLevelPhase = 0x05,
            //micDiscoLevel = 0x06,
            //micDiscoLevelPhase = 0x07,
            //echoSingLevel = 0x08,
            //echoDingLevelPhase = 0x09,
            //echoDiscoLevel = 0x0a,
            //echoDiscoLevelPhase = 0x0b,
            //reverbSingLevel = 0x0c,
            //reverbSingLevelPhase = 0x0d,
            //reverbDiscoLevel = 0x0e,
            //reverbDiscoLevelPhase = 0x0f,

            //attack = 0x10,
            //release = 0x11,
            //threshold = 0x12,
            //ratio = 0x13,
            //reserved = 0x14,

            //hpfFreq = 0x15,
            //hpfType = 0x16,

            //lpfFreq = 0x17,
            //lpfType = 0x18,
            
            //eq = 0x20,
            //eqPass = 0x21,
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

        public VKxPIIDCMCommProtocol() :
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
        
        public static bool ParseNonSystemModuleData(short index, byte[] decryptedReceivedData, VKxPIIDCMEffectorModel effector)
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

        public static List<short> GetNonSystemModuleData(short moduleIndex, VKxPIIDCMEffectorModel effector)
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

            music.highPassFilter.typeIndex = dataList[pointer++];
            music.highPassFilter.freqIndex = dataList[pointer++];

            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music._eqGroup);

            ParseBassExciterData(dataList, ref pointer, music.bassExciter);

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

            //Log.OutputLine("KVXPII Mic 模块****************");

            int pointer = 0;

            mic.micNoiseGate.initV = dataList[pointer++];
            mic.fbe.initV = dataList[pointer++];

            mic.highPassFilter.freqIndex = dataList[pointer++];

            ParseCompressorData(dataList, ref pointer, mic.compressor);

            //Log.OutputLine("Compressor attack = {0}", mic.compressor.attack.v);
            //Log.OutputLine("Compressor release = {0}", mic.compressor.release.v);
            //Log.OutputLine("Compressor threshold = {0}", mic.compressor.threshold.v);
            //Log.OutputLine("Compressor ratio = {0}", mic.compressor.ratio.v);

            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, mic.eqGroup);

            mic.micLevel.initV = dataList[pointer++];

            pointer += 1;

            mic.lowPassFilter.freqIndex = dataList[pointer++];
            mic.lowPassFilter.typeIndex = dataList[pointer++];

            mic.exciter1Select.initV = dataList[pointer++];
            
            ParseBassExciterData(dataList, ref pointer, mic.bassExciter);
            ParseExciterData(dataList, ref pointer, mic.exciter1);
            ParseExciterData(dataList, ref pointer, mic.exciter2);

            //Log.OutputLine("KVXPII Mic 模块###################");

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
            
            pointer += 1;
            
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

            if (dataList.Length < stereooutputModuleDataLength)
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
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.discoEQGroup);
            //Log.OutputLine("解析Stereo输出模块加密{0}", output.dataIsPrivate);
            //Log.OutputLine("解析主输出模块成功");
            return true;
        }

        private static bool ParseMonoOutputModuleData(short[] dataList, MonoOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("MonoOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, outputModuleDataLength);

            if (dataList.Length < monoOutputModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.monoChannel.delay.initV = dataList[pointer++];
            output.monoChannel.mute.initV = dataList[pointer++];
            output.monoChannel.phase.initV = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.discoEQGroup);
            //output.dataIsPrivateParam = dataList[pointer++];
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

            ParseLimiterData(dataList, ref pointer, output.limiter);

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

            moduleData.Add(music.highPassFilter.typeIndex);
            moduleData.Add(music.highPassFilter.freqIndex);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music._eqGroup);

            AppendBassExciterData(moduleData, music.bassExciter);
            //moduleData.Add(music.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetMicModuleData(MicModule mic)
        {
            Debug.Assert(mic != null);
            List<short> moduleData = new List<short>();

            moduleData.Add(mic.micNoiseGate.v);
            moduleData.Add(mic.fbe.v);

            moduleData.Add(mic.hpf.freqIndex);

            AppendCompressorData(moduleData, mic.compressor);

            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, mic.eqGroup);
            
            moduleData.Add(mic.micLevel.v);

            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);

            moduleData.Add(mic.lpf.freqIndex);
            moduleData.Add(mic.lpf.typeIndex);

            moduleData.Add(mic.exciter1Select.v);
            AppendBassExciterData(moduleData, mic.bassExciter);
            AppendExciterData(moduleData, mic.exciter1);
            AppendExciterData(moduleData, mic.exciter2);

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

            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);

            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, reverb.eqGroup);

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
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.discoEQGroup);
            //moduleData.Add(output.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetMonoOutputData(MonoOutputModule output)
        {
            List<short> moduleData = GetOutputBaseData(output);
            moduleData.Add(output.monoChannel.delay.v);
            moduleData.Add(output.monoChannel.mute.v);
            moduleData.Add(output.monoChannel.phase.v);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.discoEQGroup);
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

            AppendLimiterData(moduleData, output.limiter);

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
