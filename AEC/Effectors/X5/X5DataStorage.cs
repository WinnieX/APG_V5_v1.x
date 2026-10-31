using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using AEC.Core;
using AEC.Common;
using AEC.Effectors;
using AEC.Effectors.X5;
using AEC.Effectors.X5.Modules;
using AEC.Misc;

namespace AEC.Effectors.X5
{
    public class X5DataStorage
    {
        private const string rootNodeName = "AEC";
        private const string modelAttr = "model";
        private const string versionAttr = "version";
        private const string version = "1.0";

        private const string musicModule = "music";
        private const string micModule = "mic";
        private const string echoModule = "echo";
        private const string reverbModule = "reverb";
        private const string mainOutputModule = "mainOutput";
        private const string centerOutputModule = "centerOutput";
        private const string subOutputModule = "subOutput";
        private const string surOutputModule = "surOutput";
        private const string systemModule = "system";

        private delegate void ParseModuleDataCallback(XmlElement ele, X5EffectorModel effector);

        public static Localization localization = Config.GetInstance().localization;

        public static void SaveToFile(X5EffectorModel effector, string model, string filePath, bool isContainAllSystemData)
        {
            Debug.Assert(effector != null);

            XmlDocument xmldoc = CreateEffectorXmlDoc(effector, model, isContainAllSystemData);

            xmldoc.Save(filePath);
        }

        //public static X5EffectorModel LoadFromX5PresetFile(string filePath)
        //{
        //    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //    try
        //    {
        //        if (DataBackup.CheckBackupFileIsX5(filePath))
        //        {
        //            return LoadFromX5PresetFileStream(fs);
        //        }
        //    }
        //    finally
        //    {
        //        fs.Close();
        //    }

        //    return null;
        //}

        public static X5EffectorModel LoadFromFile(string filePath, CheckPresetModel checkPresetModelCallback)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            try
            {
                if (DataBackup.CheckBackupFileIsX5(filePath))
                {
                    return LoadFromX5PresetFileStream(fs);
                }
                else
                {
                    return LoadFromStream(fs, checkPresetModelCallback, false);
                }
            }
            finally
            {
                fs.Close();
            }
            

            

            //if (!File.Exists(filePath))
            //{
            //    return null;
            //}

            //try
            //{
            //    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //    VXEffectorModel effector = LoadFromStream(fs, false);
            //    effector.system.ConstraintParamFromFile();
            //    fs.Close();
            //    return effector;
            //}
            //catch
            //{
            //    //MessageBox.Show("加载文件发生异常!", "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //return null;
            //}

            //return null;
        }

        public static X5EffectorModel Buffer2Effector(byte[] buffer, CheckPresetModel checkPresetModelCallback, bool isContainAllSystemData = false)
        {
            MemoryStream ms = new MemoryStream(buffer);
            return LoadFromStream(ms, checkPresetModelCallback, isContainAllSystemData);
        }

        public static byte[] Effector2Buffer(X5EffectorModel effector, string model, bool isContainAllSystemData = false)
        {
            XmlDocument xmldoc = CreateEffectorXmlDoc(effector, model, isContainAllSystemData);

            MemoryStream ms = new MemoryStream();
            xmldoc.Save(ms);
            return ms.ToArray();
        }

        
        private static X5EffectorModel LoadFromX5PresetFileStream(FileStream fileStream)
        {
            Debug.Assert(fileStream != null);

            short[] buffer = GetX5FileBuffer(fileStream);

            if (buffer == null)
            {
                new Exception("X5 Preset File Error!");
            }

            X5EffectorModel effector = X5EffectorModel.GetTempModel();

            X5PresetParseMusicModule(buffer, effector.music);
            X5PresetParseMicModule(buffer, effector.mic);
            X5PresetParseEchoModule(buffer, effector.echo);
            X5PresetParseReverbModule(buffer, effector.reverb);
            X5PresetParseMainOutputModule(buffer, effector.mainOutput);
            X5PresetParseSubOutputModule(buffer, effector.subOutput);
            X5PresetParseCenterOutputModule(buffer, effector.centerOutput);
            X5PresetParseSurOutputModule(buffer, effector.surOutput);
            X5PresetParseSystemModule(buffer, effector.system);

            return effector;
        }

        private const int X5PresetAutoWorkModeIndex = 0x38 / 2;
        private const int X5PresetMusicEQIndex = 0x0204 / 2;

        private static void X5PresetParseMusicModule(short[] buffer, MusicModule music)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && music != null);

            int pointer = X5PresetAutoWorkModeIndex;
            music.autoWorkMode.workModeInit = buffer[pointer++];
            music.autoWorkMode.workSelectInit = buffer[pointer++];
            music.highPassFilter.freqIndex = buffer[pointer++];
            pointer++;
            music.highPassFilter.typeIndex = buffer[pointer++];
            pointer++;

            pointer = X5PresetMusicEQIndex;
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, music._eqGroup);
        }

        private const int X5PresetMicIndex = 0x0004 / 2;
        private const int X5PresetMicEQIndex = 0x0304 / 2;

        private static void X5PresetParseMicModule(short[] buffer, MicModule mic)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && mic != null);

            int pointer = X5PresetMicIndex;
            mic.fbe.initV = buffer[pointer++];
            mic.highPassFilter.freqIndex = buffer[pointer++];
            AEC.Comm.X5Comm.X5CommProtocol.ParseCompAndLimterData(buffer, ref pointer, mic.compressor);
            
            pointer = X5PresetMicEQIndex;
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, mic._eqGroup);
        }

        private const int X5PresetEchoIndex = 0x0010 / 2;
        private const int X5PresetEchoEQIndex = 0x0404 / 2;

        private static void X5PresetParseEchoModule(short[] buffer, EchoModule echo)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && echo != null);

            int pointer = X5PresetEchoIndex;

            echo.echoMixer.phase.initV = buffer[pointer++];
            echo.echoMixer.level.initV = buffer[pointer++];
            echo.directMixer.phase.initV = buffer[pointer++];
            echo.directMixer.level.initV = buffer[pointer++];
            echo.preDelay.initV = buffer[pointer++];
            echo.highPassFilter.freqIndex = buffer[pointer++];
            echo.lowPassFilter.freqIndex = buffer[pointer++];
            echo.delayTime.initV = buffer[pointer++];
            echo.repeat.initV = buffer[pointer++];
            pointer++;
            echo.rChDelayTime.initV = buffer[pointer++];
            echo.rChPreDelay.initV = buffer[pointer++];

            pointer = X5PresetEchoEQIndex;
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, echo.eqGroup);
        }

        private const int X5PresetReverbIndex = 0x0028 / 2;

        private static void X5PresetParseReverbModule(short[] buffer, ReverbModule reverb)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && reverb != null);

            int pointer = X5PresetReverbIndex;

            reverb.reverbMixer.phase.initV = buffer[pointer++];
            reverb.reverbMixer.level.initV = buffer[pointer++];
            reverb.directMixer.phase.initV = buffer[pointer++];
            reverb.directMixer.level.initV = buffer[pointer++];
            reverb.reverbTime.initV = buffer[pointer++];
            reverb.preDelay.initV = buffer[pointer++];

            reverb.highPassFilter.freqIndex = buffer[pointer++];
            reverb.highPassFilter.freqIndex++;

            reverb.lowPassFilter.freqIndex = buffer[pointer++];
        }

        private const int X5PresetMainOutputIndex = 0x0044 / 2;
        private const int X5PresetMainOutputEQIndex = 0x0504 / 2;

        private static void X5PresetParseMainOutputModule(short[] buffer, MainOutputModule output)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && output != null);

            int pointer = X5PresetMainOutputIndex;

            ParseOutputMixerData(buffer, ref pointer, output.musicMixer);
            ParseOutputMixerData(buffer, ref pointer, output.micMixer);
            ParseOutputMixerData(buffer, ref pointer, output.echoMixer);
            ParseOutputMixerData(buffer, ref pointer, output.reverbMixer);
            AEC.Comm.X5Comm.X5CommProtocol.ParseCompAndLimterData(buffer, ref pointer, output.limiter);
            output.leftChannel.delay.initV = buffer[pointer++];
            output.rightChannel.delay.initV = buffer[pointer++];
            output.leftChannel.mute.initV = buffer[pointer++];
            output.rightChannel.mute.initV = buffer[pointer++];

            pointer = X5PresetMainOutputEQIndex;
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, output.singEQGroup);
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, output.discoEQGroup);
        }

        private const int X5PresetSubOutputIndex = 0x0074 / 2;
        private const int X5PresetSubOutputEQIndex = 0x0704 / 2;

        private static void X5PresetParseSubOutputModule(short[] buffer, SubOutputModule output)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && output != null);

            int pointer = X5PresetSubOutputIndex;

            ParseOutputMixerData(buffer, ref pointer, output.musicMixer);
            ParseOutputMixerData(buffer, ref pointer, output.micMixer);

            output.singHPF.typeIndex = buffer[pointer++];
            output.discoHPF.typeIndex = buffer[pointer++];
            output.singHPF.freqIndex = buffer[pointer++];
            output.discoHPF.freqIndex = buffer[pointer++];
            output.singHPF.qIndex = buffer[pointer++];
            output.discoHPF.qIndex = buffer[pointer++];

            output.singLPF.typeIndex = buffer[pointer++];
            output.discoLPF.typeIndex = buffer[pointer++];
            output.singLPF.freqIndex = buffer[pointer++];
            output.discoLPF.freqIndex = buffer[pointer++];
            output.singLPF.qIndex = buffer[pointer++];
            output.discoLPF.qIndex = buffer[pointer++];

            if (output.singLPF.freqIndex > 0)
            {
                output.singLPF.freqIndex--;
            }
            if (output.discoLPF.freqIndex > 0)
            {
                output.discoLPF.freqIndex--;
            }

            AEC.Comm.X5Comm.X5CommProtocol.ParseCompAndLimterData(buffer, ref pointer, output.limiter);
            output.monoChannel.delay.initV = buffer[pointer++];
            output.monoChannel.mute.initV = buffer[pointer++];

            pointer = X5PresetSubOutputEQIndex;
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, output.singEQGroup);
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, output.discoEQGroup);
        }

        private const int X5PresetCenterOutputIndex = 0x0104 / 2;
        private const int X5PresetCenterOutputEQIndex = 0x0604 / 2;

        private static void X5PresetParseCenterOutputModule(short[] buffer, CenterOutputModule output)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && output != null);

            int pointer = X5PresetCenterOutputIndex;

            ParseOutputMixerData(buffer, ref pointer, output.musicMixer);
            ParseOutputMixerData(buffer, ref pointer, output.micMixer);
            ParseOutputMixerData(buffer, ref pointer, output.echoMixer);
            ParseOutputMixerData(buffer, ref pointer, output.reverbMixer);
            
            output.singHPF.freqIndex = buffer[pointer++];
            output.discoHPF.freqIndex = buffer[pointer++];
            output.singHPF.typeIndex = buffer[pointer++];
            output.discoHPF.typeIndex = buffer[pointer++];
            AEC.Comm.X5Comm.X5CommProtocol.ParseCompAndLimterData(buffer, ref pointer, output.limiter);
            output.monoChannel.delay.initV = buffer[pointer++];
            output.monoChannel.mute.initV = buffer[pointer++];

            pointer = X5PresetCenterOutputEQIndex;
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, output.singEQGroup);
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, output.discoEQGroup);
        }

        private const int X5PresetSurOutputIndex = 0x0138 / 2;
        private const int X5PresetSurOutputEQIndex = 0x0804 / 2;

        private static void X5PresetParseSurOutputModule(short[] buffer, SurOutputModule output)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && output != null);

            int pointer = X5PresetSurOutputIndex;

            ParseOutputMixerData(buffer, ref pointer, output.musicMixer);
            ParseOutputMixerData(buffer, ref pointer, output.micMixer);
            ParseOutputMixerData(buffer, ref pointer, output.echoMixer);
            ParseOutputMixerData(buffer, ref pointer, output.reverbMixer);

            output.singHPF.freqIndex = buffer[pointer++];
            output.discoHPF.freqIndex = buffer[pointer++];

            AEC.Comm.X5Comm.X5CommProtocol.ParseCompAndLimterData(buffer, ref pointer, output.limiter);

            output.leftChannel.delay.initV = buffer[pointer++];
            output.rightChannel.delay.initV = buffer[pointer++];
            output.leftChannel.mute.initV = buffer[pointer++];
            output.rightChannel.mute.initV = buffer[pointer++];

            pointer = X5PresetSurOutputEQIndex;
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, output.singEQGroup);
            AEC.Comm.CommProtocolBase.ParseDataList2EQGroup(buffer, ref pointer, output.discoEQGroup);
        }

        private const int X5PresetSystemMusicInputGainIndex = 0x0192 / 2;

        private static void X5PresetParseSystemModule(short[] buffer, SystemModuleBase system)
        {
            Debug.Assert(buffer != null && buffer.Length == X5PresetFileLength / sizeof(short) && system != null);

            int pointer = X5PresetSystemMusicInputGainIndex;

            system.musicInputGain.initV = buffer[pointer++];

            pointer += 0x05;

            system.musicStartVolume.initV = buffer[pointer++];
            system.musicMaxVolume.initV = buffer[pointer++];

            system.micStartVolume.initV = buffer[pointer++];
            system.micMaxVolume.initV = buffer[pointer++];

            system.effectStartVolume.initV = buffer[pointer++];
            system.effectMaxVolume.initV = buffer[pointer++];

            pointer += 2;

            system.musicInputPort.initV = buffer[pointer++];
        }

        protected static void ParseOutputMixerData(short[] buffer, ref int pointer, OutputMixer outputMixer)
        {
            Debug.Assert(buffer != null && outputMixer != null);

            outputMixer.singMixer.level.initV = buffer[pointer++];
            outputMixer.discoMixer.level.initV = buffer[pointer++];
            outputMixer.singMixer.phase.initV = buffer[pointer++];
            outputMixer.discoMixer.phase.initV = buffer[pointer++];
        }

        private const long X5PresetFileLength = 0x924;
        private const short X5PresetFileSignatures = 0x0CA7;
        private static short[] GetX5FileBuffer(FileStream fileStream)
        {
            if (fileStream.Length != X5PresetFileLength)
            {
                return null;
            }

            byte[] byteBuffer = new byte[fileStream.Length];
            fileStream.Read(byteBuffer, 0, (int)fileStream.Length);

            short[] buffer = new short[byteBuffer.Length / sizeof(short)];
            for (int i = 0; i < buffer.Length; ++i)
            {
                buffer[i] = BitConverter.ToInt16(byteBuffer, i * 2);
            }

            if(buffer[0] != X5PresetFileSignatures)
            {
                return null;
            }

            return buffer;
        }

        private static X5EffectorModel LoadFromStream(Stream stream, CheckPresetModel checkPresetModelCallback, bool isContainAllSystemData)
        {
            Debug.Assert(stream != null && checkPresetModelCallback != null);
            
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(stream);

            if (xmldoc.DocumentElement != null &&
                xmldoc.DocumentElement.LocalName == rootNodeName)
            {
                XmlElement rootNode = xmldoc.DocumentElement;

                return LoadFromXmlDoc(rootNode, checkPresetModelCallback, isContainAllSystemData);
            }

            throw new Exception();
        }

        public static X5EffectorModel LoadFromXmlDoc(XmlElement rootNode, CheckPresetModel checkPresetModelCallback, bool isContainAllSystemData)
        {
            if (rootNode.HasAttribute(modelAttr) &&
                    checkPresetModelCallback(rootNode.GetAttribute(modelAttr).Trim()) &&
                    rootNode.HasAttribute(versionAttr) &&
                    rootNode.GetAttribute(versionAttr) == version)
            {

                X5EffectorModel effector = X5EffectorModel.GetTempModel();

                Dictionary<string, ParseModuleDataCallback> moduleParseMap = new Dictionary<string, ParseModuleDataCallback>()
                    {
                        {musicModule, ParseMusicModule},
                        {micModule, ParseMicModule},
                        {echoModule, ParseEchoModule},
                        {reverbModule, ParseReverbModule},
                        {mainOutputModule, ParseMainOutputModule},
                        {centerOutputModule, ParseCenterOutputModule},
                        {subOutputModule, ParseSubOutputModule},
                        {surOutputModule, ParseSurOutputModule},
                        //{systemModule, ParseSystemParam},
                    };

                foreach (XmlElement ele in rootNode.ChildNodes)
                {
                    if (ele.Name == moduleNode)
                    {
                        string moduleName = ele.GetAttribute(nameAttr);
                        if (moduleParseMap.ContainsKey(moduleName))
                        {
                            moduleParseMap[moduleName](ele, effector);
                        }
                        else if (moduleName == systemModule)
                        {
                            ParseSystemParam(ele, effector, isContainAllSystemData);
                        }
                    }
                }

                return effector;
            }

            throw new Exception();
        }

        private static XmlDocument CreateEffectorXmlDoc(X5EffectorModel effector, string model, bool isContainAllSystemData)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);

            //加入根节点 lmAEC
            XmlElement rootEle = xmldoc.CreateElement(rootNodeName);
            xmldoc.AppendChild(rootEle);
            rootEle.SetAttribute(modelAttr, model);
            rootEle.SetAttribute(versionAttr, version);

            rootEle.AppendChild(CreateMusicNode(xmldoc, effector.music));
            rootEle.AppendChild(CreateMicNode(xmldoc, effector.mic));
            rootEle.AppendChild(CreateEchoNode(xmldoc, effector.echo));
            rootEle.AppendChild(CreateReverbNode(xmldoc, effector.reverb));
            rootEle.AppendChild(CreateMainOutputNode(xmldoc, effector.mainOutput));
            rootEle.AppendChild(CreateCenterOutputNode(xmldoc, effector.centerOutput));
            rootEle.AppendChild(CreateSubOutputNode(xmldoc, effector.subOutput));
            rootEle.AppendChild(CreateSurOutputNode(xmldoc, effector.surOutput));
            rootEle.AppendChild(CreateSystemNode(xmldoc, effector.system, isContainAllSystemData));
            
            return xmldoc;
        }

        private const string workModeNode = "workMode";
        private const string workSelectNode = "workSelect";
        private const string singHPFFreqNode = "singHPFFreq";
        private const string discoHPFFreqNode = "discoHPFFreq";

        private static void ParseMusicModule(XmlElement moduleNode, X5EffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            MusicModule music = effector.music;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == autoWrokModeNode)
                {
                    ParseAutoWorkModeElement(ele, music.autoWorkMode);
                }
                else if(ele.Name == filterNode)
                {
                    string filterName = ele.GetAttribute(nameAttr);
                    if (filterName == hpfName)
                    {
                        ParseFilterNode(ele, music.highPassFilter);
                    }
                }
                else if (ele.Name == eqGroupNode)
                {
                    ParseEQGroup(ele, music._eqGroup);
                }
            }
        }

        private static XmlElement CreateMusicNode(XmlDocument xmldoc, MusicModule music)
        {
            XmlElement ele = CreateModuleNode(xmldoc, musicModule);

            ele.AppendChild(CreateAutoWorkModeNode(xmldoc, music.autoWorkMode));
            ele.AppendChild(CreateFilterNode(xmldoc, music.highPassFilter, hpfName));
            ele.AppendChild(CreateEQGroupElement(xmldoc, music._eqGroup));
            return ele;
        }

        private static XmlElement CreateMicNode(XmlDocument xmldoc, MicModule mic)
        {
            XmlElement ele = CreateModuleNode(xmldoc, micModule);

            ele.AppendChild(CreateParamNode(xmldoc, fbeName, mic.fbe));
            ele.AppendChild(CreateParamNode(xmldoc, hPFFreqName, mic.highPassFilter.freqIndex));
            ele.AppendChild(CreateX5CompAndLimNode(xmldoc, mic.compressor));
            ele.AppendChild(CreateEQGroupElement(xmldoc, mic.eqGroup));
            return ele;
        }

        private static void ParseMicModule(XmlElement moduleNode, X5EffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            MicModule mic = effector.mic;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseMicParam(ele, mic);
                }
                else if (ele.Name == compressorAndLimiterNode)
                {
                    ParseX5CompAndLimElement(ele, mic.compressor);
                }
                else if (ele.Name == eqGroupNode)
                {
                    ParseEQGroup(ele, mic.eqGroup);
                }
            }
        }

        private static void ParseMicParam(XmlElement dataEle, MicModule mic)
        {
            string name = dataEle.GetAttribute(nameAttr);
            short value = ParseText2Short(dataEle.InnerText);
            if (value < 0)
            {
                return;
            }

            switch (name)
            {
                case fbeName:
                    mic.fbe.initV = value;
                    break;

                case hPFFreqName:
                    mic.highPassFilter.freqIndex = value;
                    break;
            }
        }

        private static void ParseEchoModule(XmlElement moduleNode, X5EffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            EchoModule echo = effector.echo;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseEchoParam(ele, echo);
                }
                else if (ele.Name == mixerNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == echoMixerName)
                    {
                        ParseSignalMixerElement(ele, echo.echoMixer);
                    }
                    else if (mixerName == directMixerName)
                    {
                        ParseSignalMixerElement(ele, echo.directMixer);
                    }
                }
                else if (ele.Name == eqGroupNode)
                {
                    ParseEQGroup(ele, echo.eqGroup);
                }
            }
        }

        private static void ParseEchoParam(XmlElement dataEle, EchoModule echo)
        {
            string name = dataEle.GetAttribute(nameAttr);
            short value = ParseText2Short(dataEle.InnerText);
            if (value < 0)
            {
                return;
            }

            switch (name)
            {
                case hPFFreqName:
                    echo.highPassFilter.freqIndex = value;
                    break;

                case lPFFreqName:
                    echo.lowPassFilter.freqIndex = value;
                    break;

                case preDelayName:
                    echo.preDelay.initV = value;
                    break;

                case delayTimeName:
                    echo.delayTime.initV = value;
                    break;

                case repeatName:
                    echo.repeat.initV = value;
                    break;

                case rChDelayTimeName:
                    echo.rChDelayTime.initV = value;
                    break;

                case rChPreDelayName:
                    echo.rChPreDelay.initV = value;
                    break;
            }
        }

        private static XmlElement CreateEchoNode(XmlDocument xmldoc, EchoModule echo)
        {
            XmlElement ele = CreateModuleNode(xmldoc, echoModule);

            ele.AppendChild(CreateSignalMixerNode(xmldoc, echo.echoMixer, echoMixerName));
            ele.AppendChild(CreateSignalMixerNode(xmldoc, echo.directMixer, directMixerName));
            ele.AppendChild(CreateParamNode(xmldoc, hPFFreqName, echo.highPassFilter.freqIndex));         
            ele.AppendChild(CreateParamNode(xmldoc, lPFFreqName, echo.lowPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, preDelayName, echo.preDelay));
            ele.AppendChild(CreateParamNode(xmldoc, delayTimeName, echo.delayTime));
            ele.AppendChild(CreateParamNode(xmldoc, repeatName, echo.repeat));
            ele.AppendChild(CreateParamNode(xmldoc, rChDelayTimeName, echo.rChDelayTime));
            ele.AppendChild(CreateParamNode(xmldoc, rChPreDelayName, echo.rChPreDelay));

            ele.AppendChild(CreateEQGroupElement(xmldoc, echo.eqGroup));

            return ele;
        }

        private static void ParseReverbModule(XmlElement moduleNode, X5EffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ReverbModule reverb = effector.reverb;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseReverbParam(ele, reverb);
                }
                else if (ele.Name == mixerNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == reverbMixerName)
                    {
                        ParseSignalMixerElement(ele, reverb.reverbMixer);
                    }
                    else if (mixerName == directMixerName)
                    {
                        ParseSignalMixerElement(ele, reverb.directMixer);
                    }
                }
            }
        }

        private static void ParseReverbParam(XmlElement dataEle, ReverbModule reverb)
        {
            string name = dataEle.GetAttribute(nameAttr);
            short value = ParseText2Short(dataEle.InnerText);
            if (value < 0)
            {
                return;
            }

            switch (name)
            {
                case hPFFreqName:
                    reverb.highPassFilter.freqIndex = value;
                    break;

                case lPFFreqName:
                    reverb.lowPassFilter.freqIndex = value;
                    break;

                case preDelayName:
                    reverb.preDelay.initV = value;
                    break;

                case reverbTimeName:
                    reverb.reverbTime.initV = value;
                    break;
            }
        }

        private static XmlElement CreateReverbNode(XmlDocument xmldoc, ReverbModule reverb)
        {
            XmlElement ele = CreateModuleNode(xmldoc, reverbModule);

            ele.AppendChild(CreateSignalMixerNode(xmldoc, reverb.reverbMixer, reverbMixerName));
            ele.AppendChild(CreateSignalMixerNode(xmldoc, reverb.directMixer, directMixerName));
            ele.AppendChild(CreateParamNode(xmldoc, hPFFreqName, reverb.highPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lPFFreqName, reverb.lowPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, preDelayName, reverb.preDelay));
            ele.AppendChild(CreateParamNode(xmldoc, reverbTimeName, reverb.reverbTime));

            return ele;
        }

        private static void ParseMainOutputModule(XmlElement moduleNode, X5EffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);

            MainOutputModule mainOutput = effector.mainOutput;

            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == outputMixerNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == musicMixerName)
                    {
                        ParseOutputMixerNode(ele, mainOutput.musicMixer);
                    }
                    else if (mixerName == micMixerName)
                    {
                        ParseOutputMixerNode(ele, mainOutput.micMixer);
                    }
                    else if(mixerName == echoMixerName)
                    {
                        ParseOutputMixerNode(ele, mainOutput.echoMixer);
                    }
                    else if (mixerName == reverbMixerName)
                    {
                        ParseOutputMixerNode(ele, mainOutput.reverbMixer);
                    }
                }
                else if(ele.Name == outputChannelNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == leftChannel)
                    {
                        ParseX5OutputChannelElement(ele, mainOutput.leftChannel);
                    }
                    else if (mixerName == rightChannel)
                    {
                        ParseX5OutputChannelElement(ele, mainOutput.rightChannel);
                    }
                }
                else if(ele.Name == eqGroupNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == singEQGroupName)
                    {
                        ParseEQGroup(ele, mainOutput.singEQGroup);
                    }
                    else if (groupName == discoEQGroupName)
                    {
                        ParseEQGroup(ele, mainOutput.discoEQGroup);
                    }
                }
                else
                {
                    ParseOutputBaseModule(ele, mainOutput);
                }
            }
        }

        private static XmlElement CreateMainOutputNode(XmlDocument xmldoc, MainOutputModule mainOutput)
        {
            XmlElement ele = CreateOutputBaseNode(xmldoc, mainOutput, mainOutputModule);

            ele.AppendChild(CreateOutputMixerNode(xmldoc, mainOutput.musicMixer, musicMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, mainOutput.micMixer, micMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, mainOutput.echoMixer, echoMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, mainOutput.reverbMixer, reverbMixerName));

            ele.AppendChild(CreateX5OutputChannelNode(xmldoc, leftChannel, mainOutput.leftChannel));
            ele.AppendChild(CreateX5OutputChannelNode(xmldoc, rightChannel, mainOutput.rightChannel));

            ele.AppendChild(CreateEQGroupElement(xmldoc, mainOutput.singEQGroup, singEQGroupName));
            ele.AppendChild(CreateEQGroupElement(xmldoc, mainOutput.discoEQGroup, discoEQGroupName));

            return ele;
        }

        private static void ParseCenterOutputModule(XmlElement moduleNode, X5EffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);

            CenterOutputModule centerOutput = effector.centerOutput;

            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == outputMixerNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == musicMixerName)
                    {
                        ParseOutputMixerNode(ele, centerOutput.musicMixer);
                    }
                    else if (mixerName == micMixerName)
                    {
                        ParseOutputMixerNode(ele, centerOutput.micMixer);
                    }
                    else if (mixerName == echoMixerName)
                    {
                        ParseOutputMixerNode(ele, centerOutput.echoMixer);
                    }
                    else if (mixerName == reverbMixerName)
                    {
                        ParseOutputMixerNode(ele, centerOutput.reverbMixer);
                    }
                }
                else if(ele.Name == filterNode)
                {
                    string filterName = ele.GetAttribute(nameAttr);
                    if (filterName == singHpfName)
                    {
                        ParseFilterNode(ele, centerOutput.singHPF);
                    }
                    else if (filterName == discoHpfName)
                    {
                        ParseFilterNode(ele, centerOutput.discoHPF);
                    }
                }
                else if (ele.Name == outputChannelNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == monoChannel)
                    {
                        ParseX5OutputChannelElement(ele, centerOutput.monoChannel);
                    }
                }
                else if (ele.Name == eqGroupNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == singEQGroupName)
                    {
                        ParseEQGroup(ele, centerOutput.singEQGroup);
                    }
                    else if (groupName == discoEQGroupName)
                    {
                        ParseEQGroup(ele, centerOutput.discoEQGroup);
                    }
                }
                else
                {
                    ParseOutputBaseModule(ele, centerOutput);
                }
            }
        }

        private static XmlElement CreateCenterOutputNode(XmlDocument xmldoc, CenterOutputModule centerOutput)
        {
            XmlElement ele = CreateOutputBaseNode(xmldoc, centerOutput, centerOutputModule);

            ele.AppendChild(CreateOutputMixerNode(xmldoc, centerOutput.musicMixer, musicMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, centerOutput.micMixer, micMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, centerOutput.echoMixer, echoMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, centerOutput.reverbMixer, reverbMixerName));

            ele.AppendChild(CreateFilterNode(xmldoc, centerOutput.singHPF, singHpfName));
            ele.AppendChild(CreateFilterNode(xmldoc, centerOutput.discoHPF, discoHpfName));

            ele.AppendChild(CreateX5OutputChannelNode(xmldoc, monoChannel, centerOutput.monoChannel));

            ele.AppendChild(CreateEQGroupElement(xmldoc, centerOutput.singEQGroup, singEQGroupName));
            ele.AppendChild(CreateEQGroupElement(xmldoc, centerOutput.discoEQGroup, discoEQGroupName));

            return ele;
        }

        private static void ParseSubOutputModule(XmlElement moduleNode, X5EffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);

            SubOutputModule subOutput = effector.subOutput;

            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == outputMixerNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == musicMixerName)
                    {
                        ParseOutputMixerNode(ele, subOutput.musicMixer);
                    }
                    else if (mixerName == micMixerName)
                    {
                        ParseOutputMixerNode(ele, subOutput.micMixer);
                    }
                }
                else if (ele.Name == filterNode)
                {
                    string filterName = ele.GetAttribute(nameAttr);
                    if (filterName == singHpfName)
                    {
                        ParseFilterNode(ele, subOutput.singHPF);
                    }
                    else if (filterName == discoHpfName)
                    {
                        ParseFilterNode(ele, subOutput.discoHPF);
                    }
                    if (filterName == singLpfName)
                    {
                        ParseFilterNode(ele, subOutput.singLPF);
                    }
                    else if (filterName == discoLpfName)
                    {
                        ParseFilterNode(ele, subOutput.discoLPF);
                    }
                }
                else if (ele.Name == outputChannelNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == monoChannel)
                    {
                        ParseX5OutputChannelElement(ele, subOutput.monoChannel);
                    }
                }
                else if (ele.Name == eqGroupNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == singEQGroupName)
                    {
                        ParseEQGroup(ele, subOutput.singEQGroup);
                    }
                    else if (groupName == discoEQGroupName)
                    {
                        ParseEQGroup(ele, subOutput.discoEQGroup);
                    }
                }
                else
                {
                    ParseOutputBaseModule(ele, subOutput);
                }
            }
        }

        private static XmlElement CreateSubOutputNode(XmlDocument xmldoc, SubOutputModule subOutput)
        {
            XmlElement ele = CreateOutputBaseNode(xmldoc, subOutput, subOutputModule);

            ele.AppendChild(CreateOutputMixerNode(xmldoc, subOutput.musicMixer, musicMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, subOutput.micMixer, micMixerName));

            ele.AppendChild(CreateFilterNode(xmldoc, subOutput.singHPF, singHpfName));
            ele.AppendChild(CreateFilterNode(xmldoc, subOutput.discoHPF, discoHpfName));
            ele.AppendChild(CreateFilterNode(xmldoc, subOutput.singLPF, singLpfName));
            ele.AppendChild(CreateFilterNode(xmldoc, subOutput.discoLPF, discoLpfName));

            ele.AppendChild(CreateX5OutputChannelNode(xmldoc, monoChannel, subOutput.monoChannel));

            ele.AppendChild(CreateEQGroupElement(xmldoc, subOutput.singEQGroup, singEQGroupName));
            ele.AppendChild(CreateEQGroupElement(xmldoc, subOutput.discoEQGroup, discoEQGroupName));

            return ele;
        }

        private static void ParseSurOutputModule(XmlElement moduleNode, X5EffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);

            SurOutputModule surOutput = effector.surOutput;

            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == outputMixerNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == musicMixerName)
                    {
                        ParseOutputMixerNode(ele, surOutput.musicMixer);
                    }
                    else if (mixerName == micMixerName)
                    {
                        ParseOutputMixerNode(ele, surOutput.micMixer);
                    }
                    else if (mixerName == echoMixerName)
                    {
                        ParseOutputMixerNode(ele, surOutput.echoMixer);
                    }
                    else if (mixerName == reverbMixerName)
                    {
                        ParseOutputMixerNode(ele, surOutput.reverbMixer);
                    }
                }
                else if(ele.Name == paramNode)
                {
                    ParseSubOutputParam(ele, surOutput);
                }
                else if (ele.Name == outputChannelNode)
                {
                    string mixerName = ele.GetAttribute(nameAttr);
                    if (mixerName == leftChannel)
                    {
                        ParseX5OutputChannelElement(ele, surOutput.leftChannel);
                    }
                    else if (mixerName == rightChannel)
                    {
                        ParseX5OutputChannelElement(ele, surOutput.rightChannel);
                    }
                }
                else if (ele.Name == eqGroupNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == singEQGroupName)
                    {
                        ParseEQGroup(ele, surOutput.singEQGroup);
                    }
                    else if (groupName == discoEQGroupName)
                    {
                        ParseEQGroup(ele, surOutput.discoEQGroup);
                    }
                }
                else
                {
                    ParseOutputBaseModule(ele, surOutput);
                }
            }
        }

        private static void ParseSubOutputParam(XmlElement dataEle, SurOutputModule surOutput)
        {
            string name = dataEle.GetAttribute(nameAttr);
            short value = ParseText2Short(dataEle.InnerText);
            if (value < 0)
            {
                return;
            }

            switch (name)
            {
                case singHpfFreqName:
                    surOutput.singHPF.freqIndex = value;
                    break;

                case discoHpfFreqName:
                    surOutput.discoHPF.freqIndex = value;
                    break;
            }
        }

        private static XmlElement CreateSurOutputNode(XmlDocument xmldoc, SurOutputModule surOutput)
        {
            XmlElement ele = CreateOutputBaseNode(xmldoc, surOutput, surOutputModule);

            ele.AppendChild(CreateOutputMixerNode(xmldoc, surOutput.musicMixer, musicMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, surOutput.micMixer, micMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, surOutput.echoMixer, echoMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, surOutput.reverbMixer, reverbMixerName));

            ele.AppendChild(CreateParamNode(xmldoc, singHpfFreqName, surOutput.singHPF.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, discoHpfFreqName, surOutput.discoHPF.freqIndex));

            ele.AppendChild(CreateX5OutputChannelNode(xmldoc, leftChannel, surOutput.leftChannel));
            ele.AppendChild(CreateX5OutputChannelNode(xmldoc, rightChannel, surOutput.rightChannel));

            ele.AppendChild(CreateEQGroupElement(xmldoc, surOutput.singEQGroup, singEQGroupName));
            ele.AppendChild(CreateEQGroupElement(xmldoc, surOutput.discoEQGroup, discoEQGroupName));

            return ele;
        }

        private static void ParseOutputBaseModule(XmlElement ele, OutputBaseModule output)
        {
            if (ele.Name == compressorAndLimiterNode)
            {
                ParseX5CompAndLimElement(ele, output.limiter);
            }
        }
        
        private static XmlElement CreateOutputBaseNode(XmlDocument xmldoc, OutputBaseModule output, string name)
        {
            XmlElement ele = CreateModuleNode(xmldoc, name);

            ele.AppendChild(CreateX5CompAndLimNode(xmldoc, output.limiter));

            return ele;
        }

        private static void ParseSystemParam(XmlElement dataEle, X5EffectorModel effector, bool isContainAllSystemData)
        {
            SystemModuleBase system = effector.system;
            
            if(isContainAllSystemData)
            {
                Dictionary<string, ShortParam> paramsMap = new Dictionary<string, ShortParam>()
                {
                    {musicStartVolumeName, system.musicStartVolume},
                    {musicMaxVolumeName, system.musicMaxVolume},
                    {micStartVolumeName, system.micStartVolume},
                    {micMaxVolumeName, system.micMaxVolume},
                    {effectStartVolumeName, system.effectStartVolume},
                    {effectMaxVolumeName, system.effectMaxVolume},
                    {musicVolumeName, system.musicVolume},
                    {micVolumeName, system.micVolume},
                    {effectVolumeName, system.effectVolume},
                    {musicMuteName, system.musicMute},
                    {micMuteName, system.micMute},
                    {effectMuteName, system.effectMute},
                    {deviceModeSaveIndexName, system.deviceModeSaveIndex},
                    {deviceModeCallIndexName, system.deviceModeCallIndex},
                    {musicInputGainName, system.musicInputGain},
                    {systemUserName, system.user},
                    {isLockKeyboardName, system.isLockKeyboard},
                    {musicInputPortName, system.musicInputPort},
                    {isEnableRemoteControlName, system.isEnableRemote},
                };

                foreach (XmlElement ele in dataEle.ChildNodes)
                {
                    if (ele.Name == paramNode)
                    {
                        string name = ele.GetAttribute(nameAttr);

                        if (paramsMap.ContainsKey(name))
                        {
                            short value = ParseText2Short(ele.InnerText);
                            if (value > 0)
                            {
                                paramsMap[name].initV = value;
                            }
                        }
                        else
                        {
                            switch(name)
                            {
                                case systemPasswordName:
                                    system.systemPasswordInit = ele.InnerText;
                                    break;

                                case keyboardLockPasswordName:
                                    system.keyboardLockPasswordInit = ele.InnerText;
                                    break;

                                case deviceIdName:
                                    byte[] idData = Utils.HexStringToByteArray(ele.InnerText);
                                    if (idData != null && idData.Length == AEC.Common.SystemModuleBase.DeviceIdLength)
                                    {
                                        Array.Copy(idData, system.deviceId, AEC.Common.SystemModuleBase.DeviceIdLength);
                                    }
                                    break;

                                case deviceIsActivatedName:
                                    {
                                        short value = ParseText2Short(ele.InnerText);
                                        system.deviceIsActivated = DataConverter.Short2Bool(value);
                                    }
                                    break;

                                //case deviceModelAttr:
                                //    {
                                //        short index = ParseText2Short(ele.InnerText);
                                //        system.deviceModelIdentify = index;
                                //    }
                                //    break;
                                    break;
                            }
                        }
                    }
                    else if(ele.Name == modeNameListNode)
                    {
                        foreach (XmlElement modeNameEle in ele.ChildNodes)
                        {
                            if(modeNameEle.Name == modeNameNode)
                            {
                                short index = ParseText2Short(modeNameEle.GetAttribute(indexName));
                                if (index < 0 || index >= system.deviceModeCount)
                                {
                                    break;
                                }

                                String modeName = modeNameEle.InnerText;
                                if (modeName.Length > SystemModule.DeviceModeNameLength)
                                {
                                    modeName = modeName.Substring(0, SystemModule.DeviceModeNameLength);
                                }

                                system.deviceModeList[index] = modeName;
                            }
                        }
                    }
                }
            }
            else
            {
                Dictionary<string, ShortParam> paramsMap = new Dictionary<string,ShortParam>()
                {
                    {musicStartVolumeName, system.musicStartVolume},
                    {musicMaxVolumeName, system.musicMaxVolume},
                    {micStartVolumeName, system.micStartVolume},
                    {micMaxVolumeName, system.micMaxVolume},
                    {effectStartVolumeName, system.effectStartVolume},
                    {effectMaxVolumeName, system.effectMaxVolume},
                    {timbreName, system.timbreControl},
                    {musicInputPortName, system.musicInputPort},
                };

                foreach (XmlElement ele in dataEle.ChildNodes)
                {
                    if(ele.Name != paramNode)
                    {
                        break;
                    }

                    string name = ele.GetAttribute(nameAttr);
                    short value = ParseText2Short(ele.InnerText);
                    if (value < 0)
                    {
                        break;
                    }

                    if (paramsMap.ContainsKey(name))
                    {
                        paramsMap[name].initV = value;
                    }
                }
            }

            system.UpdateCurrVolume();
        }

        private static XmlElement CreateSystemNode(XmlDocument xmldoc, SystemModuleBase system, bool isContainAllSystemData)
        {
            XmlElement ele = CreateModuleNode(xmldoc, systemModule);
            ele.AppendChild(CreateParamNode(xmldoc, musicStartVolumeName, system.musicStartVolume));
            ele.AppendChild(CreateParamNode(xmldoc, musicMaxVolumeName, system.musicMaxVolume));
            ele.AppendChild(CreateParamNode(xmldoc, micStartVolumeName, system.micStartVolume));
            ele.AppendChild(CreateParamNode(xmldoc, micMaxVolumeName, system.micMaxVolume));
            ele.AppendChild(CreateParamNode(xmldoc, effectStartVolumeName, system.effectStartVolume));
            ele.AppendChild(CreateParamNode(xmldoc, effectMaxVolumeName, system.effectMaxVolume));
            ele.AppendChild(CreateParamNode(xmldoc, timbreName, system.timbreControl));
            ele.AppendChild(CreateParamNode(xmldoc, musicInputPortName, system.musicInputPort));

            if (isContainAllSystemData)
            {
                ele.AppendChild(CreateModeNameListElement(xmldoc, system.deviceModeList));
                ele.AppendChild(CreateParamNode(xmldoc, deviceModeSaveIndexName, system.deviceModeSaveIndex));
                ele.AppendChild(CreateParamNode(xmldoc, deviceModeCallIndexName, system.deviceModeCallIndex));
                ele.AppendChild(CreateParamNode(xmldoc, musicInputGainName, system.musicInputGain));
                ele.AppendChild(CreateParamNode(xmldoc, systemPasswordName, system.systemPassword));
                ele.AppendChild(CreateParamNode(xmldoc, keyboardLockPasswordName, system.keyboardLockPassword));
                ele.AppendChild(CreateParamNode(xmldoc, systemUserName, system.user));
                ele.AppendChild(CreateParamNode(xmldoc, isLockKeyboardName, system.isLockKeyboard));
                ele.AppendChild(CreateParamNode(xmldoc, isEnableRemoteControlName, system.isEnableRemote));
                ele.AppendChild(CreateParamNode(xmldoc, musicVolumeName, system.musicVolume));
                ele.AppendChild(CreateParamNode(xmldoc, micVolumeName, system.micVolume));
                ele.AppendChild(CreateParamNode(xmldoc, effectVolumeName, system.effectVolume));
                ele.AppendChild(CreateParamNode(xmldoc, musicMuteName, system.musicMute));
                ele.AppendChild(CreateParamNode(xmldoc, micMuteName, system.micMute));
                ele.AppendChild(CreateParamNode(xmldoc, effectMuteName, system.effectMute));
                ele.AppendChild(CreateParamNode(xmldoc, deviceIdName, system.deviceIdCode));
                ele.AppendChild(CreateParamNode(xmldoc, deviceIsActivatedName, DataConverter.Bool2Short(system.deviceIsActivated)));       
            }
            
            return ele;
        }

        private static XmlElement CreateModeNameListElement(XmlDocument xmldoc, string[] deviceModeList)
        {
            XmlElement modeNameListEle = xmldoc.CreateElement(modeNameListNode);

            for (int i = 0; i < deviceModeList.Length; ++i)
            {
                XmlElement modeNameEle = xmldoc.CreateElement(modeNameNode);

                modeNameEle.SetAttribute(indexName, i.ToString());
                modeNameEle.InnerText = deviceModeList[i];

                modeNameListEle.AppendChild(modeNameEle);
            }

            return modeNameListEle;
        }

        //通用
        private static void ParseX5OutputChannelElement(XmlElement channelEle, AEC.Common.X5OutputChannel outputChannel)
        {
            foreach (XmlElement ele in channelEle.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    string name = ele.GetAttribute(nameAttr);
                    short value = ParseText2Short(ele.InnerText);
                    if (value >= 0)
                    {
                        switch (name)
                        {
                            case delayName:
                                outputChannel.delay.initV = value;
                                break;

                            case muteName:
                                outputChannel.mute.initV = value;
                                break;
                        }
                    }
                }
            }
        }

        private static XmlElement CreateX5OutputChannelNode(XmlDocument xmldoc, string name, AEC.Common.X5OutputChannel outputChannel)
        {
            XmlElement ele = xmldoc.CreateElement(outputChannelNode);
            ele.SetAttribute(nameAttr, name);

            ele.AppendChild(CreateParamNode(xmldoc, delayName, outputChannel.delay));
            ele.AppendChild(CreateParamNode(xmldoc, muteName, outputChannel.mute));

            return ele;
        }


        private static XmlElement CreateOutputFilterNode(XmlDocument xmldoc, XPFilterBase filter, string name)
        {
            XmlElement ele = xmldoc.CreateElement(name);
            ele.AppendChild(CreateParamNode(xmldoc, filterTypeName, filter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, freqName, filter.freqIndex));

            return ele;
        }

        private static void ParseOutputMixerNode(XmlElement outputMixerEle, OutputMixer outputMixer)
        {
            foreach (XmlElement ele in outputMixerEle.ChildNodes)
            {
                string mixerName = ele.GetAttribute(nameAttr);
                if (mixerName == singName)
                {
                    ParseSignalMixerElement(ele, outputMixer.singMixer);
                }
                else if (mixerName == discoName)
                {
                    ParseSignalMixerElement(ele, outputMixer.discoMixer);
                }
            }
        }


        private static XmlElement CreateOutputMixerNode(XmlDocument xmldoc, OutputMixer outputMixer, string name)
        {
            XmlElement ele = xmldoc.CreateElement(outputMixerNode);
            ele.SetAttribute(nameAttr, name);

            ele.AppendChild(CreateSignalMixerNode(xmldoc, outputMixer.singMixer, singName));
            ele.AppendChild(CreateSignalMixerNode(xmldoc, outputMixer.discoMixer, discoName));

            return ele;
        }


        private static void ParseSignalMixerElement(XmlElement levelEle, SignalMixer mixer)
        {
            foreach (XmlElement ele in levelEle.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    string name = ele.GetAttribute(nameAttr);
                    short value = ParseText2Short(ele.InnerText);
                    if (value >= 0)
                    {
                        switch (name)
                        {
                            case levelName:
                                mixer.level.initV = value;
                                break;

                            case phaseName:
                                mixer.phase.initV = value;
                                //AEC.Log.OutputLine("AEC phase = {0}", level.phase.v);
                                break;
                        }
                    }
                }
            }
        }

        private static XmlElement CreateSignalMixerNode(XmlDocument xmldoc, SignalMixer mixer, string name)
        {
            XmlElement ele = xmldoc.CreateElement(mixerNode);
            ele.SetAttribute(nameAttr, name);

            ele.AppendChild(CreateParamNode(xmldoc, levelName, mixer.level));
            ele.AppendChild(CreateParamNode(xmldoc, phaseName, mixer.phase));
            
            return ele;
        }

        private static void ParseX5CompAndLimElement(XmlElement clEle, X5_CompressorLimiter cl)
        {
            if (clEle.Name == compressorAndLimiterNode)
            {
                foreach (XmlElement ele in clEle.ChildNodes)
                {
                    if (ele.Name == paramNode)
                    {
                        string name = ele.GetAttribute(nameAttr);
                        short value = ParseText2Short(ele.InnerText);
                        if (value >= 0)
                        {
                            switch (name)
                            {
                                case thresholdName:
                                    cl.threshold.initV = value;
                                    break;

                                case attackName:
                                    cl.attack.initV = value;
                                    break;

                                case releaseName:
                                    cl.release.initV = value;
                                    break;

                                case ratioName:
                                    cl.ratio.initV = value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static XmlElement CreateX5CompAndLimNode(XmlDocument xmldoc, X5_CompressorLimiter cl)
        {
            XmlElement ele = xmldoc.CreateElement(compressorAndLimiterNode);

            ele.AppendChild(CreateParamNode(xmldoc, thresholdName, cl.threshold));
            ele.AppendChild(CreateParamNode(xmldoc, attackName, cl.attack));
            ele.AppendChild(CreateParamNode(xmldoc, releaseName, cl.release));
            ele.AppendChild(CreateParamNode(xmldoc, ratioName, cl.ratio));

            return ele;
        }

        private static XmlElement CreateFilterNode(XmlDocument xmldoc, XPFilterBase filter, string name = "")
        {
            XmlElement ele = xmldoc.CreateElement(filterNode);

            if (!string.IsNullOrEmpty(name))
            {
                ele.SetAttribute(nameAttr, name);
            }

            ele.AppendChild(CreateParamNode(xmldoc, filterTypeName, filter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, freqName, filter.freqIndex));

            if(filter.hasQ)
            {
                ele.AppendChild(CreateParamNode(xmldoc, qName, filter.qIndex));
            }

            return ele;
        }

        private static void ParseFilterNode(XmlElement dataEle, XPFilterBase filter)
        {
            foreach (XmlElement ele in dataEle.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    string name = ele.GetAttribute(nameAttr);
                    short value = ParseText2Short(ele.InnerText);
                    if (value < 0)
                    {
                        break;
                    }

                    if (name == filterTypeName)
                    {
                        filter.typeIndex = value;
                    }
                    else if (name == freqName)
                    {
                        filter.freqIndex = value;
                    }
                    else if(name == qName)
                    {
                        if(filter.hasQ)
                        {
                            filter.qIndex = value;
                        }
                    }
                }
            }
        }

        private static XmlElement CreateEQGroupElement(XmlDocument xmldoc, EQGroup eqGroup, string name = "")
        {
            XmlElement ele = xmldoc.CreateElement(eqGroupNode);
            if (!string.IsNullOrEmpty(name))
            {
                ele.SetAttribute(nameAttr, name);
            }

            ele.AppendChild(CreateParamNode(xmldoc, currEQIndexAttr, eqGroup.currEQIndex));
            ele.AppendChild(CreateParamNode(xmldoc, eqIsPassAttr, eqGroup.eqPassIndex));

            XmlElement eqListEle = xmldoc.CreateElement(eqListNode);
            foreach(EQ eq in eqGroup.eqList)
            {
                eqListEle.AppendChild(CreateEQElement(xmldoc, eq));
            }

            ele.AppendChild(eqListEle);

            return ele;
        }

        private static void ParseEQGroup(XmlElement eqGroupEle, EQGroup eqGroup)
        {
            foreach (XmlElement ele in eqGroupEle.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    string name = ele.GetAttribute(nameAttr);
                    short value = ParseText2Short(ele.InnerText);
                    if (value >= 0)
                    {
                        switch (name)
                        {
                            case currEQIndexAttr:
                                eqGroup.currEQIndex = value;
                                break;

                            case eqIsPassAttr:
                                eqGroup.eqPassIndex = value;
                                break;
                        }
                    }
                }
                else if (ele.Name == eqListNode)
                {
                    foreach (XmlElement eqEle in ele.ChildNodes)
                    {
                        ParseEQElement(eqEle, eqGroup);
                    }
                }
            }
        }

        private static void ParseEQElement(XmlElement eqEle, EQGroup eqGroup)
        {
            if (eqEle.Name == eqNode)
            {
                short index = ParseText2Short(eqEle.GetAttribute(indexName));
                if (index < 0 || index >= eqGroup.eqBandCount)
                {
                    return;
                }

                foreach (XmlElement ele in eqEle.ChildNodes)
                {
                    if (ele.Name == paramNode)
                    {
                        string name = ele.GetAttribute(nameAttr);
                        short value = ParseText2Short(ele.InnerText);
                        if (value >= 0)
                        {
                            switch (name)
                            {
                                case typeName:
                                    eqGroup.eqList[index].typeIndex = value;
                                    //AEC.Log.OutputLine("eq type:{0}", eqGroup.eqList[index].type);
                                    break;

                                case freqName:
                                    eqGroup.eqList[index].freqIndex = value;
                                    break;

                                case gainName:
                                    eqGroup.eqList[index].gainIndex = value;
                                    break;

                                case qName:
                                    eqGroup.eqList[index].qIndex_ = value;
                                    break;

                                case sName:
                                    eqGroup.eqList[index].sIndex_ = value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static XmlElement CreateEQElement(XmlDocument xmldoc, EQ eq)
        {
            XmlElement ele = xmldoc.CreateElement(eqNode);
            ele.SetAttribute(indexName, eq.index.ToString());
            //AEC.Log.OutputLine("eq type:{0}", eq.type);
            ele.AppendChild(CreateParamNode(xmldoc, typeName, eq.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, freqName, eq.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, gainName, eq.gainIndex));
            ele.AppendChild(CreateParamNode(xmldoc, qName, eq.qIndex_));
            ele.AppendChild(CreateParamNode(xmldoc, sName, eq.sIndex_));

            return ele;
        }



        private static void ParseAutoWorkModeElement(XmlElement autoWrokModeEle, AutoWorkMode autoWorkMode)
        {
            if (autoWrokModeEle.Name == autoWrokModeNode)
            {
                foreach (XmlElement ele in autoWrokModeEle.ChildNodes)
                {
                    if (ele.Name == paramNode)
                    {
                        string name = ele.GetAttribute(nameAttr);
                        short value = ParseText2Short(ele.InnerText);
                        if (value >= 0)
                        {
                            switch (name)
                            {
                                case workModeNode:
                                    autoWorkMode.workModeInit = value;
                                    break;

                                case workSelectNode:
                                    autoWorkMode.workSelectInit = value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static XmlElement CreateAutoWorkModeNode(XmlDocument xmldoc, AutoWorkMode autoWorkMode)
        {
            XmlElement ele = xmldoc.CreateElement(autoWrokModeNode);

            ele.AppendChild(CreateParamNode(xmldoc, workModeNode, autoWorkMode.wm));
            ele.AppendChild(CreateParamNode(xmldoc, workSelectNode, autoWorkMode.ws));

            return ele;
        }

        private static XmlElement CreateParamNode(XmlDocument xmldoc, string name, ShortParam param)
        {
            return CreateParamNode(xmldoc, name, param.v);
        }

        private static XmlElement CreateParamNode(XmlDocument xmldoc, string name, short param)
        {
            return CreateParamNode(xmldoc, name, param.ToString());
        }

        private static XmlElement CreateParamNode(XmlDocument xmldoc, string name, string value)
        {
            XmlElement ele = xmldoc.CreateElement(paramNode);
            ele.SetAttribute(nameAttr, name);
            ele.InnerText = value;
            return ele;
        }

        private static XmlElement CreateModuleNode(XmlDocument xmldoc, string moduleName)
        {
            XmlElement ele = xmldoc.CreateElement(moduleNode);
            ele.SetAttribute(nameAttr, moduleName);
            return ele;
        }

        private static short ParseText2Short(string txt)
        {
            short value;
            if(short.TryParse(txt, out value))
            {
                return value;
            }

            return -1;
        }

        private const string moduleNode = "module";
        private const string paramNode = "param";
        private const string nameAttr = "name";
        private const string eqGroupNode = "eqGroup";
        private const string eqListNode = "eqList";
        private const string eqNode = "eq";
        private const string indexName = "index";

        private const string typeName = "type";
        private const string freqName = "freq";
        private const string gainName = "gain";
        private const string qName = "q";
        private const string sName = "s";

        private const string currEQIndexAttr = "currEQIndex";
        private const string eqIsPassAttr = "eqIsPass";

        private const string autoWrokModeNode = "autoWrokMode";
        private const string filterNode = "filter";
        private const string compressorAndLimiterNode = "compAndLim";

        private const string compressorNode = "compressor";
        private const string thresholdName = "threshold";
        private const string attackName = "attack";
        private const string releaseName = "release";
        private const string ratioName = "ratio";

        private const string limiterNode = "limiter";
        private const string clipThresholdName = "clipThreshold";
        private const string clipReleaseName = "clipRelease";

        private const string mixerNode = "mixer";

        private const string levelName = "level";
        private const string phaseName = "phase";
        private const string outputMixerNode = "outputMixer";
        private const string filterTypeName = "type";
        private const string singEQGroupName = "singEQGroup";
        private const string discoEQGroupName = "discoEQGroup";
        private const string micNoiseGateName = "noiseGate";
        private const string hPFFreqName = "hPFFreq";
        private const string fbeName = "fbe";
        private const string echoMixerName = "echoMixer";
        private const string directMixerName = "directMixer";
        private const string lPFFreqName = "lPFFreq";
        private const string preDelayName = "preDelay";
        private const string delayTimeName = "delayTime";
        private const string repeatName = "repeat";
        private const string rChDelayTimeName = "rChDelayTime";
        private const string rChPreDelayName = "rChPreDelay";
        private const string reverbMixerName = "reverbMixer";
        private const string reverbTimeName = "reverbTime";

        private const string singName = "sing";
        private const string discoName = "disco";

        private const string musicMixerName = "musicMixer";
        private const string micMixerName = "micMixer";

        private const string hpfName = "hpf";

        private const string singHpfName = "singHpf";
        private const string discoHpfName = "discoHpf";
        private const string singLpfName = "singLpf";
        private const string discoLpfName = "discoLpf";

        private const string singHpfFreqName = "singHpfFreq";
        private const string discoHpfFreqName = "discoHpfFreq";

        private const string outputChannelNode = "outputChannel";
        private const string leftChannel = "leftChannel";
        private const string rightChannel = "rightChannel";
        private const string monoChannel = "monoChannel";

        private const string delayName = "delay";
        private const string muteName = "mute";
        
        private const string musicStartVolumeName = "musicStartVolume";
        private const string musicMaxVolumeName = "musicMaxVolume";
        private const string micStartVolumeName = "micStartVolume";
        private const string micMaxVolumeName = "micMaxVolume";
        private const string effectStartVolumeName = "effectStartVolume";
        private const string effectMaxVolumeName = "effectMaxVolume";
        private const string timbreName = "timbre";
        private const string modeNameListNode = "modeNameList";
        private const string modeNameNode = "modeName";
        private const string deviceModeSaveIndexName = "deviceModeSaveIndex";
        private const string deviceModeCallIndexName = "deviceModeCallIndex";
        private const string musicInputGainName = "musicInputGain";
        private const string systemPasswordName = "systemPassword";
        private const string keyboardLockPasswordName = "keyboardLockPassword";
        private const string systemUserName = "systemUser";
        private const string isLockKeyboardName = "isLockKeyboard";
        private const string musicInputPortName = "musicInputPort";
        private const string isEnableRemoteControlName = "isEnableRemoteControl";
        private const string musicVolumeName = "musicVolume";
        private const string micVolumeName = "micVolume";
        private const string effectVolumeName = "effectVolume";
        private const string musicMuteName = "musicMute";
        private const string micMuteName = "micMute";
        private const string effectMuteName = "effectMute";
        private const string deviceIdName = "deviceId";
        private const string deviceIsActivatedName = "deviceIsActivated";
        private const string deviceModelName = "deviceModel";
    }
}
