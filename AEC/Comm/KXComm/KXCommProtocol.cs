using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Effectors.KX.Modules;
using AEC.Effectors.KX;
using AEC.Misc;
using AEC.Common;

namespace AEC.Comm.KXComm
{
    //V系列协议
    class KXCommProtocol : CommProtocolBase
    {
#region 协议中的常量
        public const byte musicModuleIndex = 0x01;
        public const byte micModuleIndex = 0x02;
        public const byte echoModuleIndex = 0x03;
        public const byte reverbModuleIndex = 0x04;
        public const byte mainOutputModuleIndex = 0x05;
        public const byte aux1ModuleIndex = 0x06;
        public const byte aux2ModuleIndex = 0x07;
        public const byte surOutputModuleIndex = 0x08;

        public const int musicModuleDataLength = 79;
        public const int micModuleDataLength = 87;
        public const int echoModuleDataLength = 41;
        public const int reverbModuleDataLength = 15;
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
            eq = 0x03,
            eqPass = 0x04,
        }

        public enum MicParamAddr
        {
            micInput = 0x00,
            fbe = 0x01,
            attack = 0x02,
            release = 0x03,
            threshold = 0x04,
            ratio = 0x05,
            hpfFreq = 0x06,
            lpfFreq = 0x07,
            lpfType = 0x08,
            noiseGate = 0x09,
            eq = 0x0a,
            eqPass = 0x0b,
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
            echoRChDelay = 0x0b,
            echoRChPreDelay = 0x0c,
            eq = 0x0e,
            eqPass = 0x0f,
        }

        public enum ReverbParamAddr
        {
            reverbMicLevel = 0x00,
            reverbMicLevelPhase = 0x01,
            directMicLevel = 0x02,
            directMicLevelPhase = 0x03,
            reverbTime = 0x04,  
            reverbPreDelay = 0x05,
            reverbMicHPF = 0x06,
            reverbMicHPFType = 0x07,
            reverbMicLPF = 0x08,
            reverbMicLPFType = 0x09,
                     
            reverbBassDamp = 0x0c,
            reverbTrebleDamp = 0x0d,
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
            echoDingLevelPhase = 0x09,
            echoDiscoLevel = 0x0a,
            echoDiscoLevelPhase = 0x0b,
            reverbSingLevel = 0x0c,
            reverbSingLevelPhase = 0x0d,
            reverbDiscoLevel = 0x0e,
            reverbDiscoLevelPhase = 0x0f,
            hpfMode = 0x10,  
            hpfFreq = 0x11,
            lpfMode = 0x12,
            lpfFreq = 0x13,
            attack = 0x14,
            release = 0x15,
            threshold = 0x16,
            clipThreshold = 0x17,
            clipRelease = 0x18,
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
            {micModuleIndex,  (byte)MicParamAddr.eq},            
            {echoModuleIndex, (byte)EchoParamAddr.eq},
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

        public KXCommProtocol() :
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

                case aux1ModuleIndex:
                    return localization[Localization.comm_Aux1OutputModule];

                case aux2ModuleIndex:
                    return localization[Localization.comm_Aux2OutputModule];

                case surOutputModuleIndex:
                    return localization[Localization.comm_SurOutputModule];
            }

            return string.Empty;
        }
        
        public static bool ParseNonSystemModuleData(short index, byte[] decryptedReceivedData, KXEffectorModel effector)
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

        public static List<short> GetNonSystemModuleData(short moduleIndex, KXEffectorModel effector)
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
            music.singHPF.freqIndex = dataList[pointer++];
            music.discoHPF.freqIndex = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music.singEQGroup);
            //pointer += 10;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music.discoEQGroup);
            music.dataIsPrivateParam = dataList[pointer++];
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

            mic.micInput.initV = dataList[pointer++];
            mic.fbe.initV = dataList[pointer++];

            mic.compressor.attack.initV = dataList[pointer++];
            mic.compressor.release.initV = dataList[pointer++];
            mic.compressor.threshold.initV = dataList[pointer++];
            mic.compressor.ratio.initV = dataList[pointer++];

            mic.highPassFilter.freqIndex = dataList[pointer++];
            mic.lowPassFilter.freqIndex = dataList[pointer++];
            mic.lowPassFilter.typeIndex = dataList[pointer++];

            mic.micNoiseGate.initV = dataList[pointer++];
            
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

            echo.echoLevel.level.initV = dataList[pointer++];
            echo.echoLevel.phase.initV = dataList[pointer++];
            echo.directLevel.level.initV = dataList[pointer++];
            echo.directLevel.phase.initV = dataList[pointer++];
            echo.highPassFilter.freqIndex = dataList[pointer++];
            echo.highPassFilter.typeIndex = dataList[pointer++];
            echo.lowPassFilter.freqIndex = dataList[pointer++];
            echo.lowPassFilter.typeIndex = dataList[pointer++];
            echo.preDelay.initV = dataList[pointer++];
            echo.delayTime.initV = dataList[pointer++];
            echo.repeat.initV = dataList[pointer++];
            echo.rChDelayTime.initV = dataList[pointer++];
            echo.rChPreDelay.initV = dataList[pointer++];
            
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

            reverb.reverbLevel.level.initV = dataList[pointer++];
            reverb.reverbLevel.phase.initV = dataList[pointer++];
            reverb.directLevel.level.initV = dataList[pointer++];
            reverb.directLevel.phase.initV = dataList[pointer++];
            reverb.delayTime.initV = dataList[pointer++];
            reverb.preDelay.initV = dataList[pointer++];
            reverb.highPassFilter.freqIndex = dataList[pointer++];
            reverb.highPassFilter.typeIndex = dataList[pointer++];
            reverb.lowPassFilter.freqIndex = dataList[pointer++];
            reverb.lowPassFilter.typeIndex = dataList[pointer++];
            pointer += 2;
            reverb.bassDamp.initV = dataList[pointer++];
            reverb.trebleDamp.initV = dataList[pointer++];
            reverb.dataIsPrivateParam = dataList[pointer++];
            //Log.OutputLine("解析混响模块加密{0}", reverb.dataIsPrivate);
            //Log.OutputLine("解析混响模块成功");
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

            ParseOutputLevelData(dataList, ref pointer, output.musicLevel);
            ParseOutputLevelData(dataList, ref pointer, output.micLevel);
            ParseOutputLevelData(dataList, ref pointer, output.echoLevel);
            ParseOutputLevelData(dataList, ref pointer, output.reverbLevel);

            output.highPassFilter.typeIndex = dataList[pointer++];
            output.highPassFilter.freqIndex = dataList[pointer++];
            output.lowPassFilter.typeIndex = dataList[pointer++];
            output.lowPassFilter.freqIndex = dataList[pointer++];

            //AEC.Log.OutputLine("output hpf q: {0}", output.highPassFilter.qIndex);
            //AEC.Log.OutputLine("output lpf q: {0}", output.lowPassFilter.qIndex);

            output.limiter.attack.initV = dataList[pointer++];
            output.limiter.release.initV = dataList[pointer++];
            output.limiter.threshold.initV = dataList[pointer++];
            output.limiter.clipThreshold.initV = dataList[pointer++];
            output.limiter.clipRelease.initV = dataList[pointer++];

            return pointer;
        }

        protected static void ParseOutputLevelData(short[] dataList, ref int pointer, OutputLevel outputLevel)
        {
            Debug.Assert(dataList != null && outputLevel != null);

            outputLevel.singLevel.level.initV = dataList[pointer++];
            outputLevel.singLevel.phase.initV = dataList[pointer++];
            outputLevel.discoLevel.level.initV = dataList[pointer++];
            outputLevel.discoLevel.phase.initV = dataList[pointer++];
        }
        #endregion

        #region 获取模块数据
        private static List<short> GetMusicModuleData(MusicModule music)
        {
            Debug.Assert(music != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(music.autoWorkMode.wm.v);
            moduleData.Add(music.autoWorkMode.ws.v);
            moduleData.Add(music.singHPF.freqIndex);
            moduleData.Add(music.discoHPF.freqIndex);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music.discoEQGroup);
            moduleData.Add(music.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetMicModuleData(MicModule mic)
        {
            Debug.Assert(mic != null);
            List<short> moduleData = new List<short>();

            moduleData.Add(mic.micInput.v);
            moduleData.Add(mic.fbe.v);

            moduleData.Add(mic.compressor.attack.v);
            moduleData.Add(mic.compressor.release.v);
            moduleData.Add(mic.compressor.threshold.v);
            moduleData.Add(mic.compressor.ratio.v);

            moduleData.Add(mic.hpf.freqIndex);
            moduleData.Add(mic.lpf.freqIndex);
            moduleData.Add(mic.lpf.typeIndex);


            moduleData.Add(mic.micNoiseGate.v);
            
            //111
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, mic.eqGroup);

            return moduleData;
        }

        private static List<short> GetEchoModuleData(EchoModule echo)
        {
            Debug.Assert(echo != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(echo.echoLevel.level.v);
            moduleData.Add(echo.echoLevel.phase.v);
            moduleData.Add(echo.directLevel.level.v);
            moduleData.Add(echo.directLevel.phase.v);
            moduleData.Add(echo.highPassFilter.freqIndex);
            moduleData.Add(echo.highPassFilter.typeIndex);
            moduleData.Add(echo.lowPassFilter.freqIndex);
            moduleData.Add(echo.lowPassFilter.typeIndex);
            moduleData.Add(echo.preDelay.v);
            moduleData.Add(echo.delayTime.v);
            moduleData.Add(echo.repeat.v);
            moduleData.Add(echo.rChDelayTime.v);
            moduleData.Add(echo.rChPreDelay.v);
            
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, echo.eqGroup);

            return moduleData;
        }

        private static List<short> GetReverbModuleData(ReverbModule reverb)
        {
            Debug.Assert(reverb != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(reverb.reverbLevel.level.v);
            moduleData.Add(reverb.reverbLevel.phase.v);
            moduleData.Add(reverb.directLevel.level.v);
            moduleData.Add(reverb.directLevel.phase.v);
            moduleData.Add(reverb.delayTime.v);
            moduleData.Add(reverb.preDelay.v);
            moduleData.Add(reverb.highPassFilter.freqIndex);
            moduleData.Add(reverb.highPassFilter.typeIndex);
            moduleData.Add(reverb.lowPassFilter.freqIndex);
            moduleData.Add(reverb.lowPassFilter.typeIndex);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 2);
            moduleData.Add(reverb.bassDamp.v);
            moduleData.Add(reverb.trebleDamp.v);
            moduleData.Add(reverb.dataIsPrivateParam);

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
            moduleData.Add(output.dataIsPrivateParam);

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

            AppendOutputLevelData(moduleData, output.musicLevel);
            AppendOutputLevelData(moduleData, output.micLevel);
            AppendOutputLevelData(moduleData, output.echoLevel);
            AppendOutputLevelData(moduleData, output.reverbLevel);

            moduleData.Add(output.highPassFilter.typeIndex);
            moduleData.Add(output.highPassFilter.freqIndex);

            moduleData.Add(output.lowPassFilter.typeIndex);
            moduleData.Add(output.lowPassFilter.freqIndex);

            moduleData.Add(output.limiter.attack.v);
            moduleData.Add(output.limiter.release.v);
            moduleData.Add(output.limiter.threshold.v);
            moduleData.Add(output.limiter.clipThreshold.v);
            moduleData.Add(output.limiter.clipRelease.v);

            return moduleData;
        }

        protected static void AppendOutputLevelData(List<short> data, OutputLevel outputLevel)
        {
            Debug.Assert(data != null && outputLevel != null);

            data.Add(outputLevel.singLevel.level.v);
            data.Add(outputLevel.singLevel.phase.v);
            data.Add(outputLevel.discoLevel.level.v);
            data.Add(outputLevel.discoLevel.phase.v);
        }
        #endregion
    }
}
