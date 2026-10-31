using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Effectors.MX.Modules;
using AEC.Effectors.MX;
using AEC.Misc;

namespace AEC.Comm.MXComm
{
    //V系列协议
    class MXCommProtocol : CommProtocolBase
    {
#region 协议中的常量
        public const byte musicModuleIndex = 0x00;
        public const byte micModuleIndex = 0x01;
        public const byte musicDeqModuleIndex = 0x02;
        public const byte micDeqModuleIndex = 0x03;
        public const byte echoModuleIndex = 0x04;
        public const byte reverbModuleIndex = 0x05;
        public const byte mainOutputModuleIndex = 0x06;
        public const byte aux1ModuleIndex = 0x07;
        public const byte aux2ModuleIndex = 0x08;
        public const byte surOutputModuleIndex = 0x09;
        public const byte systemModuleIndex = 0x0a;

        public const int musicModuleDataLength = 96;
        public const int micModuleDataLength = 81;
        public const int deqModuleDataLength = 37;
        public const int echoModuleDataLength = 41;
        public const int reverbModuleDataLength = 14;
        public const int dualChOutputModuleDataLength = 116;
        public const int auxModuleDataLength = 66;

        public enum MusicParamAddr
        {
            workMode = 0x00,
            workSelect = 0x01,
            eq = 0x02,
            eqPass = 0x03,
        }

        public enum MicParamAddr
        {
            hpfFreq = 0x00,
            fbe = 0x02,
            noiseGate = 0x03,
            eq = 0x04,
            eqPass = 0x05,
        }

        public enum DeqParamAddr
        {
            eq = 0x00,
            deqIsPass = 0x01,
            dcm = 0x07,
            attack = 0x08,
            release = 0x09,
            threshold = 0x0a,
            ratio = 0x0b,
            dm = 0x0c,
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
            echoDingLevelPhase = 0x0a,
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
            {micModuleIndex, (byte)MicParamAddr.eq},
            {musicDeqModuleIndex, (byte)DeqParamAddr.eq},
            {micDeqModuleIndex, (byte)DeqParamAddr.eq},            
            {echoModuleIndex, (byte)EchoParamAddr.eq},            
            {mainOutputModuleIndex, (byte)OutputParamAddr.eq},
            {aux1ModuleIndex, (byte)AuxParamAddr.eq},
            {aux2ModuleIndex, (byte)AuxParamAddr.eq},
            {surOutputModuleIndex, (byte)OutputParamAddr.eq},
        };

        public static readonly Dictionary<DeqParamType, byte> deqParamAddrDict = new Dictionary<DeqParamType, byte>()
        {
            {DeqParamType.DeqIsPass, (byte)DeqParamAddr.deqIsPass},
            {DeqParamType.DCM, (byte)DeqParamAddr.dcm},
            {DeqParamType.Attack, (byte)DeqParamAddr.attack},
            {DeqParamType.Release, (byte)DeqParamAddr.release},
            {DeqParamType.Threshold, (byte)DeqParamAddr.threshold},
            {DeqParamType.Ratio, (byte)DeqParamAddr.ratio},
            {DeqParamType.DM, (byte)DeqParamAddr.dm},
        };

#endregion

        public MXCommProtocol() :
            base()
        {

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

                case musicDeqModuleIndex:
                    return localization[Localization.comm_MusicDeqModule];

                case micDeqModuleIndex:
                    return localization[Localization.comm_MicDeqModule];

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
        
        public static bool ParseNonSystemModuleData(short index, byte[] decryptedReceivedData, MXEffectorModel effector)
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
                    
                case musicDeqModuleIndex:
                    return ParseDeqModuleData(dataList, effector.musicDeq);

                case micDeqModuleIndex:
                    return ParseDeqModuleData(dataList, effector.micDeq);
                    
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

        public static List<short> GetNonSystemModuleData(short moduleIndex, MXEffectorModel effector)
        {
            switch ((byte)moduleIndex)
            {
                case musicModuleIndex:
                    return GetMusicModuleData(effector.music);

                case micModuleIndex:
                    return GetMicModuleData(effector.mic);

                case musicDeqModuleIndex:
                    return GetDeqModuleData(effector.musicDeq);

                case micDeqModuleIndex:
                    return GetDeqModuleData(effector.micDeq);

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

            music.wm.initV = dataList[pointer++];
            music.ws.initV = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music.singEQGroup);
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, music.discoEQGroup);

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
            mic.hpf.freqIndex = dataList[pointer++];
            pointer += 1;
            mic.fbe.initV = dataList[pointer++];
            mic.noiseGate.initV = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, mic.eqGroup);
            //Log.OutputLine("解析麦克风模块成功");

            return true;
        }

        private static bool ParseDeqModuleData(short[] dataList, DeqModule deq)
        {
            //Log.OutputLine("解析麦克风模块...");
            Debug.Assert(dataList != null && deq != null);
            if (dataList.Length < deqModuleDataLength)
            {
                return false;
            }

            int pointer = 0;
            CommProtocolBase.ParseDataList2DEQGroup(dataList, ref pointer, deq.deqGroup);

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
            pointer += 3;
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
            reverb.reverbMusicLevel.phase.initV = dataList[pointer++];
            reverb.reverbMusicLevel.level.initV = dataList[pointer++];
            reverb.musicHpfFreq.initV = dataList[pointer++];
            reverb.musicLpfFreq.initV = dataList[pointer++];
            reverb.bassDamp.initV = dataList[pointer++];
            reverb.trebleDamp.initV = dataList[pointer++];
            //Log.OutputLine("解析混响模块成功");

            return true;
        }

        private static bool ParseMainOutputModuleData(short[] dataList, OutputModule output)
        {
            Debug.Assert(dataList != null && output != null);
            if (dataList.Length < dualChOutputModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.leftChDelay.initV = dataList[pointer++];
            output.rightChDelay.initV = dataList[pointer++];
            output.leftChMute.initV = dataList[pointer++];
            output.rightChMute.initV = dataList[pointer++];
            output.eqControlMode.initV = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.leftEQGroup);
            pointer += 5;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.rightEQGroup);
            //Log.OutputLine("解析主输出模块成功");

            output.ConstraintParam();

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
            //Log.OutputLine("解析辅助输出模块成功");

            return true;
        }

        private static bool ParseSurOutputModuleData(short[] dataList, OutputModule output)
        {
            Debug.Assert(dataList != null && output != null);
            if (dataList.Length < dualChOutputModuleDataLength)
            {
                return false;
            }

            int pointer = ParseOutputBaseData(dataList, output);
            output.leftChDelay.initV = dataList[pointer++];
            output.rightChDelay.initV = dataList[pointer++];
            output.leftChMute.initV = dataList[pointer++];
            output.rightChMute.initV = dataList[pointer++];
            output.eqControlMode.initV = dataList[pointer++];
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.leftEQGroup);
            pointer += 5;
            CommProtocolBase.ParseDataList2EQGroup(dataList, ref pointer, output.rightEQGroup);
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

            output.compressor.attack.initV = dataList[pointer++];
            output.compressor.release.initV = dataList[pointer++];
            output.compressor.threshold.initV = dataList[pointer++];
            output.compressor.ratio.initV = dataList[pointer++];
            output.compressor.mode.initV = dataList[pointer++];

            return pointer;
        }
        #endregion

        #region 获取模块数据
        private static List<short> GetMusicModuleData(MusicModule music)
        {
            Debug.Assert(music != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(music.wm.v);
            moduleData.Add(music.ws.v);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music.singEQGroup);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, music.discoEQGroup);

            return moduleData;
        }

        private static List<short> GetMicModuleData(MicModule mic)
        {
            Debug.Assert(mic != null);
            List<short> moduleData = new List<short>();
            moduleData.Add(mic.hpf.freqIndex);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 1);
            moduleData.Add(mic.fbe.v);
            moduleData.Add(mic.noiseGate.v);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, mic.eqGroup);

            return moduleData;
        }

        private static List<short> GetDeqModuleData(DeqModule deq)
        {
            Debug.Assert(deq != null);
            List<short> moduleData = new List<short>();
            CommProtocolBase.AppendDEQGroup2DataBuffer(moduleData, deq.deqGroup);

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
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 3);
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
            moduleData.Add(reverb.reverbMusicLevel.phase.v);
            moduleData.Add(reverb.reverbMusicLevel.level.v);
            moduleData.Add(reverb.musicHpfFreq.v);
            moduleData.Add(reverb.musicLpfFreq.v);
            moduleData.Add(reverb.bassDamp.v);
            moduleData.Add(reverb.trebleDamp.v);

            return moduleData;
        }

        private static List<short> GetOutputData(OutputModule output)
        {
            List<short> moduleData = GetOutputBaseData(output);
            moduleData.Add(output.leftChDelay.v);
            moduleData.Add(output.rightChDelay.v);
            moduleData.Add(output.leftChMute.v);
            moduleData.Add(output.rightChMute.v);
            moduleData.Add(output.eqControlMode.v);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.leftEQGroup);
            CommProtocolBase.AppendZeros2DataBuffer(moduleData, 5);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.rightEQGroup);

            return moduleData;
        }

        private static List<short> GetAuxOutputData(AuxOutputModule output)
        {
            List<short> moduleData = GetOutputBaseData(output);
            moduleData.Add(output.delay.v);
            moduleData.Add(output.mute.v);
            CommProtocolBase.AppendEQGroup2DataBuffer(moduleData, output.eqGroup);

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

            moduleData.Add(output.compressor.attack.v);
            moduleData.Add(output.compressor.release.v);
            moduleData.Add(output.compressor.threshold.v);
            moduleData.Add(output.compressor.ratio.v);
            moduleData.Add(output.compressor.mode.v);

            return moduleData;
        }
        #endregion
    }
}
