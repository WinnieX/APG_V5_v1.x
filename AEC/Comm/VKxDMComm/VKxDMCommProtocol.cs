using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Effectors.VKxDM.Modules;
using AEC.Effectors.VKxDM;
using AEC.Misc;
using AEC.Common;

namespace AEC.Comm.VKxDMComm
{
    //V系列协议
    class VKxDMCommProtocol : CommProtocolBase
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

        public const int musicModuleDataLength = 41;
        public const int micModuleDataLength = 86;
        public const int echoModuleDataLength = 38;
        public const int reverbModuleDataLength = 8;
        public const int stereooutputModuleDataLength = 173;
        public const int monoOutputModuleDataLength = 170;

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
            micLevel = 0x03,
            effectLevel = 0x04,
            attack = 0x05,
            release = 0x06,
            threshold = 0x07,
            ratio = 0x08,
            eq = 0x09,
            eqPass = 0x0a,
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
            hpfQ = 0x0A,
            lpfType = 0x0B,
            lpfFreq = 0x0C,
            lpfQ = 0x0D,
            attack = 0x10,
            release = 0x11,
            threshold = 0x12,
            clipThreshold = 0x13,
            clipRelease = 0x14,
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

        public VKxDMCommProtocol() :
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
        
        public static bool ParseNonSystemModuleData(short index, byte[] decryptedReceivedData, VKxDMEffectorModel effector)
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
                    //Log.OutputLine("Parse VKS Center Output Data:*********");
                    return ParseMonoOutputModuleData(dataList, effector.aux1Output);
                    
                case aux2ModuleIndex:
                    //Log.OutputLine("Parse VKS Sub Output Data:*********");
                    return ParseMonoOutputModuleData(dataList, effector.aux2Output);
                    
                case surOutputModuleIndex:
                    return ParseStereoOutputModuleData(dataList, effector.surOutput);
            }

            Tools.LogHelper.WriteInfo("ParseNonSystemModuleData()...Failed!");
            
            return false;
        }

        public static List<short> GetNonSystemModuleData(short moduleIndex, VKxDMEffectorModel effector)
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

            mic.micNoiseGate.initV = dataList[pointer++];
            mic.fbe.initV = dataList[pointer++];
            mic.highPassFilter.freqIndex = dataList[pointer++];
            mic.micLevel.initV = dataList[pointer++];
            mic.effectLevel.initV = dataList[pointer++];
            mic.compressor.attack.initV = dataList[pointer++];
            mic.compressor.release.initV = dataList[pointer++];
            mic.compressor.threshold.initV = dataList[pointer++];
            mic.compressor.ratio.initV = dataList[pointer++];            
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

        private static bool ParseStereoOutputModuleData(short[] dataList, StereoOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("StereoOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, stereooutputModuleDataLength);

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
            //pointer++;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.discoEQGroup);
            output.dataIsPrivateParam = 0;
            //Log.OutputLine("解析Stereo输出模块加密{0}", output.dataIsPrivate);
            //Log.OutputLine("解析主输出模块成功");
            return true;
        }

        private static bool ParseMonoOutputModuleData(short[] dataList, MonoOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);

            //Log.OutputLine("MonoOutput 数据包长度是: {0}, 要求的最低长度是: {1}", dataList.Length, monoOutputModuleDataLength);

            if (dataList.Length < monoOutputModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.monoChannel.delay.initV = dataList[pointer++];
            output.monoChannel.mute.initV = dataList[pointer++];
            output.monoChannel.phase.initV = dataList[pointer++];
            //pointer += 4;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.discoEQGroup);
            output.dataIsPrivateParam = 0;
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

            //Log.OutputLine("VKS Mono Filter Data:*********");
            //for (int i = 0; i < 12; ++i)
            //{
            //    Log.OutputLine("data:{0}", dataList[pointer + i]);
            //}
            //Log.OutputLine("VKS Mono Filter Data End...........");

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

            //AEC.Log.OutputLine("output hpf q: {0}", output.highPassFilter.qIndex);
            //AEC.Log.OutputLine("output lpf q: {0}", output.lowPassFilter.qIndex);

            output.limiter.attack.initV = dataList[pointer++];
            output.limiter.release.initV = dataList[pointer++];
            output.limiter.threshold.initV = dataList[pointer++];
            output.limiter.clipThreshold.initV = dataList[pointer++];
            output.limiter.clipRelease.initV = dataList[pointer++];

            return pointer;
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
            moduleData.Add(0);

            return moduleData;
        }

        private static List<short> GetMicModuleData(MicModule mic)
        {
            Debug.Assert(mic != null);
            List<short> moduleData = new List<short>();

            moduleData.Add(mic.micNoiseGate.v);
            moduleData.Add(mic.fbe.v);
            moduleData.Add(mic.hpf.freqIndex);
            moduleData.Add(mic.micLevel.v);
            moduleData.Add(mic.effectLevel.v);
            moduleData.Add(mic.compressor.attack.v);
            moduleData.Add(mic.compressor.release.v);
            moduleData.Add(mic.compressor.threshold.v);
            moduleData.Add(mic.compressor.ratio.v);
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
            moduleData.Add(0);

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
            moduleData.Add(0);

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

            moduleData.Add(output.limiter.attack.v);
            moduleData.Add(output.limiter.release.v);
            moduleData.Add(output.limiter.threshold.v);
            moduleData.Add(output.limiter.clipThreshold.v);
            moduleData.Add(output.limiter.clipRelease.v);

            return moduleData;
        }

        protected static void AppendOutputMixerData(List<short> data, OutputMixer outputMixer)
        {
            Debug.Assert(data != null && outputMixer != null);

            data.Add(outputMixer.singMixer.level.v);
            data.Add(outputMixer.singMixer.phase.v);
            data.Add(outputMixer.discoMixer.level.v);
            data.Add(outputMixer.discoMixer.phase.v);
        }
        #endregion
    }
}
