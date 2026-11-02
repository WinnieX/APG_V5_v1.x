using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Common;
using AEC.Effectors.VX.Modules;
using AEC.Effectors.VX;
using AEC.Misc;

namespace AEC.Comm.VXComm
{
    //V系列协议
    class VXCommProtocol : CommProtocolBase
    {
#region 协议中的常量
        public const byte musicModuleIndex = 0x00;
        public const byte micModuleIndex = 0x01;
        public const byte dEffectModuleIndex = 0x02;
        public const byte echoModuleIndex = 0x03;
        public const byte reverbModuleIndex = 0x04;
        public const byte mainOutputModuleIndex = 0x05;
        public const byte aux1ModuleIndex = 0x06;
        public const byte aux2ModuleIndex = 0x07;
        public const byte surOutputModuleIndex = 0x08;
        //public const byte systemModuleIndex = 0x0a;

        public const int musicModuleDataLength = 89;
        public const int micModuleDataLength = 83;
        public const int dEffectModuleDataLength = 5;
        public const int echoModuleDataLength = 41;
        public const int reverbModuleDataLength = 15;
        public const int mainOutputModuleDataLength = 70;
        public const int auxModuleDataLength = 57;
        public const int surOutputModuleDataLength = 60;

        private const byte OutputLimiterStatusDataLength = 0x20;

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
            attack = 0x00,
            release = 0x01,
            threshold = 0x02,
            ratio = 0x03,
            hpfFreq = 0x04,
            fbe = 0x05,
            eq = 0x06,
            eqPass = 0x07,
        }

        public enum DEffectParamAddr
        {
            attack = 0x00,
            release = 0x01,
            threshold = 0x02,
            ratio = 0x03,
            dGain = 0x04,
        }

        public enum EchoParamAddr
        {
            echoLevelPhase = 0x00,
            echoLevel = 0x01,
            directLevelPhase = 0x02,
            directLevel = 0x03,
            echoHpfFreq = 0x04,
            echoLpfFreq = 0x05,
            echoPreDelay = 0x06,
            echoDelay = 0x07,
            echoRepeat = 0x08,
            echoRChDelay = 0x09,
            echoRChPreDelay = 0x0A,
            echoLpfQ = 0x0B,
            eq = 0x0e,
            eqPass = 0x0f,
        }

        public enum ReverbParamAddr
        {
            reverbMicLevelPhase = 0x00,
            reverbMicLevel = 0x01,
            directMicLevelPhase = 0x02,
            directMicLevel = 0x03,
            reverbTime = 0x04,  
            reverbPreDelay = 0x05,
            reverbMicHPF = 0x06,
            reverbMicLPF = 0x07,
            reverbMusicLevelPhase = 0x08,
            reverbMusicLevel = 0x09,
            reverbMusicHPF = 0x0a,
            reverbMusicLPF = 0x0b,
            reverbBassDamp = 0x0c,
            reverbTrebleDamp = 0x0d,
        }

        public enum OutputBaseParamAddr
        {
            musicSingLevel = 0x00,
            musicDiscoLevel = 0x01,
            musicSingLevelPhase = 0x02,
            musicDiscoLevelPhase = 0x03,
            micSingLevel = 0x04,
            micDiscoLevel = 0x05,
            micSingLevelPhase = 0x06,
            micDiscoLevelPhase = 0x07,
            echoSingLevel = 0x08,
            echoDiscoLevel = 0x09,
            echoSingLevelPhase = 0x0a,
            echoDiscoLevelPhase = 0x0b,
            reverbSingLevel = 0x0c,
            reverbDiscoLevel = 0x0d,
            reverbSingLevelPhase = 0x0e,
            reverbDiscoLevelPhase = 0x0f,
            hpfMode = 0x10,  
            hpfFreq = 0x11,
            hpfQ = 0x12,
            lpfMode = 0x13,
            lpfFreq = 0x14,
            lpfQ = 0x15,
            attack = 0x16,
            release = 0x17,
            threshold = 0x18,
            ratio = 0x19,
            mode = 0x1a,
        }

        public enum OutputParamAddr
        {
            leftChDelay = 0x1b,
            rightChDelay = 0x1c,
            leftChMute = 0x1d,
            rightChMute = 0x1e,
            eqControlMode = 0x1f,
            eq = 0x20,
            eqPass = 0x21,
        }

        public enum AuxParamAddr
        {
            delay = 0x1b,
            mute = 0x1c,
            eq = 0x1d,
            eqPass = 0x1e,
        }

        public static readonly Dictionary<byte, byte> eqAddrDict = new Dictionary<byte, byte>()
        {
            {musicModuleIndex, (byte)MusicParamAddr.eq},
            {micModuleIndex,  (byte)MicParamAddr.eq},            
            {echoModuleIndex, (byte)EchoParamAddr.eq},
            {mainOutputModuleIndex, (byte)OutputParamAddr.eq},
            {aux1ModuleIndex, (byte)AuxParamAddr.eq},
            {aux2ModuleIndex, (byte)AuxParamAddr.eq},
            {surOutputModuleIndex, (byte)OutputParamAddr.eq},
            //{musicModuleIndex, 0x03},
            //{micModuleIndex, 0x06},            
            //{echoModuleIndex, 0x0e},            
            //{mainOutputModuleIndex, 0x20},
            //{aux1ModuleIndex, 0x1d},
            //{aux2ModuleIndex, 0x1d},
            //{surOutputModuleIndex, 0x20},
        };

#endregion

        public VXCommProtocol():
            base()
        {

        }

        public static QuadChannelLimiterStatus ParseOutputLimiterStatusData(byte[] buffer)
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

            if (statusData == null || statusData.Length != OutputLimiterStatusDataLength)
            {
                //AEC.Log.OutputLine("解析电平数据，长度出错");
                return null;
            }

            //AEC.Tools.LogHelper.WriteDebug("limiter data:{0}", Utils.ByteToHexStr(statusData));
            //Console.WriteLine("limiter data:{0}", Utils.ByteToHexStr(statusData));

            int index = 0;
            //int seed = statusData[0] + statusData[1] + statusData[2] + statusData[3];
            //Random random = new Random(seed);
            QuadChannelLimiterStatus limiterStatus = new QuadChannelLimiterStatus();
            for (int i = 0; i < QuadChannelLimiterStatus.Quad; ++i)
            {
                //random.Next(36) - 36; //
                float level = Math.Abs(Get32Float(statusData, index++));
                float ratio = Get32Float(statusData, index++);
                limiterStatus[i].linearValue = level;
                limiterStatus[i].ratio = ratio;
                //Log.OutputLine("output[{0}], level:{1}({2}), ratio:{3}", i, limiterStatus[i].level, level, ratio);
            }

            return limiterStatus;
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

                case dEffectModuleIndex:
                    return localization[Localization.comm_DEffectModule];

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
        
        public static bool ParseNonSystemModuleData(short index, byte[] decryptedReceivedData, VXEffectorModel effector)
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
                    
                case dEffectModuleIndex:
                    return ParseDEffectModuleData(dataList, effector.dEffect);
                    
                case echoModuleIndex:
                    return ParseEchoModuleData(dataList, effector.echo);
                    
                case reverbModuleIndex:
                    return ParseReverbModuleData(dataList, effector.reverb);
                    
                case mainOutputModuleIndex:
                    return ParseMainOutputModuleData(dataList, effector.mainOutput);
                    
                case aux1ModuleIndex:
                    return ParseAuxOutputModuleData(dataList, effector.aux1Output);
                    
                case aux2ModuleIndex:
                    return ParseAuxOutputModuleData(dataList, effector.aux2Output);
                    
                case surOutputModuleIndex:
                    return ParseSurOutputModuleData(dataList, effector.surOutput);
            }

            Tools.LogHelper.WriteInfo("ParseNonSystemModuleData()...Failed!");
            
            return false;
        }

        public static List<short> GetNonSystemModuleData(short moduleIndex, VXEffectorModel effector)
        {
            switch ((byte)moduleIndex)
            {
                case musicModuleIndex:
                    return GetMusicModuleData(effector.music);

                case micModuleIndex:
                    return GetMicModuleData(effector.mic);

                case dEffectModuleIndex:
                    return GetDEffectModuleData(effector.dEffect);

                case echoModuleIndex:
                    return GetEchoModuleData(effector.echo);

                case reverbModuleIndex:
                    return GetReverbModuleData(effector.reverb);

                case mainOutputModuleIndex:
                    return GetOutputData(effector.mainOutput);

                case aux1ModuleIndex:
                    return GetAuxOutputData(effector.aux1Output);

                case aux2ModuleIndex:
                    return GetAuxOutputData(effector.aux2Output);

                case surOutputModuleIndex:
                    return GetOutputData(effector.surOutput);

                default:
                    throw (new Exception("数据错误"));
            }
        }

        #region 解析数据
        private static bool ParseMusicModuleData(short[] dataList, MusicModule music)
        {
            Debug.Assert(dataList != null && music != null);
            if(dataList.Length < musicModuleDataLength)
            {
                return false;
            }

            int pointer = 0;

            music.autoWorkMode.workModeInit = dataList[pointer++];
            music.autoWorkMode.workSelectInit = dataList[pointer++];
            //music.wm.initV = dataList[pointer++];
            //music.ws.initV = dataList[pointer++];

            music.singHPF.freqIndex = dataList[pointer++];
            music.discoHPF.freqIndex = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music.singEQGroup);
            pointer += 10;
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
            if (dataList.Length < micModuleDataLength)
            {
                return false;
            }

            int pointer = 0;

            mic.compressor.attack.initV = dataList[pointer++];
            mic.compressor.release.initV = dataList[pointer++];
            mic.compressor.threshold.initV = dataList[pointer++];
            mic.compressor.ratio.initV = dataList[pointer++];

            mic.hpf.freqIndex = dataList[pointer++];
            mic.fbe.initV = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, mic.eqGroup);
            //Log.OutputLine("解析麦克风模块成功");
            return true;
        }

        private static bool ParseDEffectModuleData(short[] dataList, DEffectModule dEffect)
        {
            Debug.Assert(dataList != null && dEffect != null);
            if (dataList.Length < dEffectModuleDataLength)
            {
                return false;
            }

            int pointer = 0;
            
            dEffect.attack.initV = dataList[pointer++];
            dEffect.release.initV = dataList[pointer++];
            dEffect.threshold.initV = dataList[pointer++];
            dEffect.ratio.initV = dataList[pointer++];
            dEffect.dGain.initV = dataList[pointer++];
            //Log.OutputLine("解析动态效果模块成功");
            return true;
        }

        private static bool ParseEchoModuleData(short[] dataList, EchoModule echo)
        {
            Debug.Assert(dataList != null && echo != null);
            if (dataList.Length < echoModuleDataLength)
            {
                return false;
            }

            int pointer = 0;

            echo.echoLevel.phase.initV = dataList[pointer++];
            echo.echoLevel.level.initV = dataList[pointer++];
            echo.directLevel.phase.initV = dataList[pointer++];
            echo.directLevel.level.initV = dataList[pointer++];
            echo.hpf.freqIndex = dataList[pointer++];
            echo.lpf.freqIndex = dataList[pointer++];
            echo.preDelay.initV = dataList[pointer++];
            echo.delayTime.initV = dataList[pointer++];
            echo.repeat.initV = dataList[pointer++];
            echo.rChDelayTime.initV = dataList[pointer++];
            echo.rChPreDelay.initV = dataList[pointer++];
            echo.lpf.qIndex = dataList[pointer++];
            pointer += 2;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, echo.eqGroup);
            //Log.OutputLine("解析回声模块成功");
            return true;
        }

        private static bool ParseReverbModuleData(short[] dataList, ReverbModule reverb)
        {
            Debug.Assert(dataList != null && reverb != null);
            if (dataList.Length < reverbModuleDataLength)
            {
                return false;
            }

            int pointer = 0;

            reverb.reverbLevel.phase.initV = dataList[pointer++];
            reverb.reverbLevel.level.initV = dataList[pointer++];
            reverb.directLevel.phase.initV = dataList[pointer++];
            reverb.directLevel.level.initV = dataList[pointer++];
            reverb.delayTime.initV = dataList[pointer++];
            reverb.preDelay.initV = dataList[pointer++];
            reverb.micHpfFreq.initV = dataList[pointer++];
            reverb.micLpfFreq.initV = dataList[pointer++];
            pointer += 4;
            reverb.bassDamp.initV = dataList[pointer++];
            reverb.trebleDamp.initV = dataList[pointer++];
            reverb.dataIsPrivateParam = dataList[pointer++];
            //Log.OutputLine("解析混响模块加密{0}", reverb.dataIsPrivate);
            //Log.OutputLine("解析混响模块成功");
            return true;
        }

        private static bool ParseMainOutputModuleData(short[] dataList, OutputModule output)
        {
            Debug.Assert(dataList != null && output != null);
            if (dataList.Length < mainOutputModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.leftChDelay.initV = dataList[pointer++];
            output.rightChDelay.initV = dataList[pointer++];
            output.leftChMute.initV = dataList[pointer++];
            output.rightChMute.initV = dataList[pointer++];
            pointer++;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.eqGroup);
            output.dataIsPrivateParam = dataList[pointer++];
            //Log.OutputLine("解析主输出模块加密{0}", output.dataIsPrivate);
            //Log.OutputLine("解析主输出模块成功");
            return true;
        }

        private static bool ParseAuxOutputModuleData(short[] dataList, AuxOutputModule output)
        {
            Debug.Assert(dataList != null && output != null);
            if (dataList.Length < auxModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.delay.initV = dataList[pointer++];
            output.mute.initV = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.eqGroup);
            output.dataIsPrivateParam = dataList[pointer++];
            //Log.OutputLine("解析辅助输出模块加密{0}", output.dataIsPrivate);
            //Log.OutputLine("解析辅助输出模块成功");
            return true;
        }

        private static bool ParseSurOutputModuleData(short[] dataList, OutputModule output)
        {
            Debug.Assert(dataList != null && output != null);
            if (dataList.Length < surOutputModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.leftChDelay.initV = dataList[pointer++];
            output.rightChDelay.initV = dataList[pointer++];
            output.leftChMute.initV = dataList[pointer++];
            output.rightChMute.initV = dataList[pointer++];
            pointer++;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.eqGroup);
            output.dataIsPrivateParam = dataList[pointer++];
            //Log.OutputLine("解析后置输出模块加密{0}", output.dataIsPrivate);
            //Log.OutputLine("解析后置输出模块成功");
            return true;
        }

        private static int ParseOutputBaseData(short[] dataList, OutputBaseModule output)
        {
            Debug.Assert(dataList != null && output != null);
            
            int pointer = 0;
            output.musicLevel.singLevel.level.initV = dataList[pointer++];
            output.musicLevel.discoLevel.level.initV = dataList[pointer++];
            output.musicLevel.singLevel.phase.initV = dataList[pointer++];
            output.musicLevel.discoLevel.phase.initV = dataList[pointer++];

            output.micLevel.singLevel.level.initV = dataList[pointer++];
            output.micLevel.discoLevel.level.initV = dataList[pointer++];
            output.micLevel.singLevel.phase.initV = dataList[pointer++];
            output.micLevel.discoLevel.phase.initV = dataList[pointer++];

            output.echoLevel.singLevel.level.initV = dataList[pointer++];
            output.echoLevel.discoLevel.level.initV = dataList[pointer++];
            output.echoLevel.singLevel.phase.initV = dataList[pointer++];
            output.echoLevel.discoLevel.phase.initV = dataList[pointer++];

            output.reverbLevel.singLevel.level.initV = dataList[pointer++];
            output.reverbLevel.discoLevel.level.initV = dataList[pointer++];
            output.reverbLevel.singLevel.phase.initV = dataList[pointer++];
            output.reverbLevel.discoLevel.phase.initV = dataList[pointer++];
            
            output.hpf.typeIndex = dataList[pointer++];
            output.hpf.freqIndex = dataList[pointer++];
            output.hpf.qIndex = dataList[pointer++];
            output.lpf.typeIndex = dataList[pointer++];
            output.lpf.freqIndex = dataList[pointer++];
            output.lpf.qIndex = dataList[pointer++];

            //AEC.Log.OutputLine("output hpf q: {0}", output.highPassFilter.qIndex);
            //AEC.Log.OutputLine("output lpf q: {0}", output.lowPassFilter.qIndex);

            output.limiter.attack.initV = dataList[pointer++];
            output.limiter.release.initV = dataList[pointer++];
            output.limiter.threshold.initV = dataList[pointer++];
            output.limiter.clipThreshold.initV = dataList[pointer++];
            output.limiter.clipRelease.initV = dataList[pointer++];

            return pointer;
        }
        #endregion

        #region 获取模块数据
        private static List<short> GetMusicModuleData(MusicModule music)
        {
            Debug.Assert(music != null);
            List<short> moduleData = new List<short>();
            //moduleData.Add(music.wm.v);
            //moduleData.Add(music.ws.v);
            moduleData.Add(music.autoWorkMode.wm.v);
            moduleData.Add(music.autoWorkMode.ws.v);

            moduleData.Add(music.singHPF.freqIndex);
            moduleData.Add(music.discoHPF.freqIndex);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music.singEQGroup);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 10);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music.discoEQGroup);
            moduleData.Add(music.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetMicModuleData(MicModule mic)
        {
            Debug.Assert(mic != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(mic.compressor.attack.v);
            moduleData.Add(mic.compressor.release.v);
            moduleData.Add(mic.compressor.threshold.v);
            moduleData.Add(mic.compressor.ratio.v);
            moduleData.Add(mic.hpf.freqIndex);
            moduleData.Add(mic.fbe.v);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, mic.eqGroup);

            return moduleData;
        }

        public static List<short> GetDEffectModuleData(DEffectModule dEffect)
        {
            Debug.Assert(dEffect != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(dEffect.attack.v);
            moduleData.Add(dEffect.release.v);
            moduleData.Add(dEffect.threshold.v);
            moduleData.Add(dEffect.ratio.v);
            moduleData.Add(dEffect.dGain.v);

            return moduleData;
        }

        private static List<short> GetEchoModuleData(EchoModule echo)
        {
            Debug.Assert(echo != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(echo.echoLevel.phase.v);
            moduleData.Add(echo.echoLevel.level.v);
            moduleData.Add(echo.directLevel.phase.v);
            moduleData.Add(echo.directLevel.level.v);
            moduleData.Add(echo.hpf.freqIndex);
            moduleData.Add(echo.lpf.freqIndex);
            moduleData.Add(echo.preDelay.v);
            moduleData.Add(echo.delayTime.v);
            moduleData.Add(echo.repeat.v);
            moduleData.Add(echo.rChDelayTime.v);
            moduleData.Add(echo.rChPreDelay.v);
            moduleData.Add(echo.lpf.qIndex);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 2);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, echo.eqGroup);

            return moduleData;
        }

        private static List<short> GetReverbModuleData(ReverbModule reverb)
        {
            Debug.Assert(reverb != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(reverb.reverbLevel.phase.v);
            moduleData.Add(reverb.reverbLevel.level.v);
            moduleData.Add(reverb.directLevel.phase.v);
            moduleData.Add(reverb.directLevel.level.v);
            moduleData.Add(reverb.delayTime.v);
            moduleData.Add(reverb.preDelay.v);
            moduleData.Add(reverb.micHpfFreq.v);
            moduleData.Add(reverb.micLpfFreq.v);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 4);
            moduleData.Add(reverb.bassDamp.v);
            moduleData.Add(reverb.trebleDamp.v);
            moduleData.Add(reverb.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetOutputData(OutputModule output)
        {
            List<short> moduleData = GetOutputBaseData(output);
            moduleData.Add(output.leftChDelay.v);
            moduleData.Add(output.rightChDelay.v);
            moduleData.Add(output.leftChMute.v);
            moduleData.Add(output.rightChMute.v);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.eqGroup);
            moduleData.Add(output.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetAuxOutputData(AuxOutputModule output)
        {
            List<short> moduleData = GetOutputBaseData(output);
            moduleData.Add(output.delay.v);
            moduleData.Add(output.mute.v);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.eqGroup);
            moduleData.Add(output.dataIsPrivateParam);

            return moduleData;
        }

        private static List<short> GetOutputBaseData(OutputBaseModule output)
        {
            List<short> moduleData = new List<short>();

            moduleData.Add(output.musicLevel.singLevel.level.v);
            moduleData.Add(output.musicLevel.discoLevel.level.v);
            moduleData.Add(output.musicLevel.singLevel.phase.v);
            moduleData.Add(output.musicLevel.discoLevel.phase.v);

            moduleData.Add(output.micLevel.singLevel.level.v);
            moduleData.Add(output.micLevel.discoLevel.level.v);
            moduleData.Add(output.micLevel.singLevel.phase.v);
            moduleData.Add(output.micLevel.discoLevel.phase.v);

            moduleData.Add(output.echoLevel.singLevel.level.v);
            moduleData.Add(output.echoLevel.discoLevel.level.v);
            moduleData.Add(output.echoLevel.singLevel.phase.v);
            moduleData.Add(output.echoLevel.discoLevel.phase.v);

            moduleData.Add(output.reverbLevel.singLevel.level.v);
            moduleData.Add(output.reverbLevel.discoLevel.level.v);
            moduleData.Add(output.reverbLevel.singLevel.phase.v);
            moduleData.Add(output.reverbLevel.discoLevel.phase.v);

            moduleData.Add(output.hpf.typeIndex);
            moduleData.Add(output.hpf.freqIndex);
            moduleData.Add(output.hpf.qIndex);

            moduleData.Add(output.lpf.typeIndex);
            moduleData.Add(output.lpf.freqIndex);
            moduleData.Add(output.lpf.qIndex);

            moduleData.Add(output.limiter.attack.v);
            moduleData.Add(output.limiter.release.v);
            moduleData.Add(output.limiter.threshold.v);
            moduleData.Add(output.limiter.clipThreshold.v);
            moduleData.Add(output.limiter.clipRelease.v);

            return moduleData;
        }
        #endregion
    }
}
