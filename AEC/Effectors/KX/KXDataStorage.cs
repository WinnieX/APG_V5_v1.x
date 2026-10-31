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
using AEC.Effectors.KX;
using AEC.Effectors.KX.Modules;
using AEC.Misc;

namespace AEC.Effectors.KX
{
    public class KXDataStorage
    {
        private const string rootNodeName = "AEC";
        private const string modelAttr = "model";
        private const string versionAttr = "version";
        private const string version = "1.0";

        private const string musicModule = "music";
        private const string micModule = "mic";
        private const string dEffectModule = "dEffect";
        private const string echoModule = "echo";
        private const string reverbModule = "reverb";
        private const string mainOutputModule = "mainOutput";
        private const string aux1OutputModule = "aux1Output";
        private const string aux2OutputModule = "aux2Output";
        private const string surOutputModule = "surOutput";
        private const string systemModule = "system";

        private delegate void ParseModuleDataCallback(XmlElement ele, KXEffectorModel effector);

        public static Localization localization = Config.GetInstance().localization;

        public static void SaveToFile(KXEffectorModel effector, string model, string filePath, bool isContainAllSystemData)
        {
            Debug.Assert(effector != null);

            XmlDocument xmldoc = CreateEffectorXmlDoc(effector, model, isContainAllSystemData);

            xmldoc.Save(filePath);
        }

        public static KXEffectorModel LoadFromFile(string filePath, CheckPresetModel checkPresetModelCallback)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            KXEffectorModel effector = LoadFromStream(fs, checkPresetModelCallback, false);
            fs.Close();
            return effector;

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

        public static KXEffectorModel Buffer2Effector(byte[] buffer, CheckPresetModel checkPresetModelCallback, bool isContainAllSystemData = false)
        {
            MemoryStream ms = new MemoryStream(buffer);
            return LoadFromStream(ms, checkPresetModelCallback, isContainAllSystemData);
        }

        public static byte[] Effector2Buffer(KXEffectorModel effector, string model, bool isContainAllSystemData = false)
        {
            XmlDocument xmldoc = CreateEffectorXmlDoc(effector, model, isContainAllSystemData);

            MemoryStream ms = new MemoryStream();
            xmldoc.Save(ms);
            return ms.ToArray();
        }

        private static KXEffectorModel LoadFromStream(Stream stream, CheckPresetModel checkPresetModelCallback, bool isContainAllSystemData)
        {
            Debug.Assert(stream != null && checkPresetModelCallback != null);
            
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(stream);

            Dictionary<string, ParseModuleDataCallback> moduleParseMap = new Dictionary<string, ParseModuleDataCallback>()
                {
                    {musicModule, ParseMusicModule},
                    {micModule, ParseMicModule},
                    {echoModule, ParseEchoModule},
                    {reverbModule, ParseReverbModule},
                    {mainOutputModule, ParseMainOutputModule},
                    {aux1OutputModule, ParseAux1OutputModule},
                    {aux2OutputModule, ParseAux2OutputModule},
                    {surOutputModule, ParseSurOutputModule},
                    //{systemModule, ParseSystemParam},
                };

            if (xmldoc.DocumentElement != null &&
                xmldoc.DocumentElement.LocalName == rootNodeName)
            {
                XmlElement rootNode = xmldoc.DocumentElement;

                if (rootNode.HasAttribute(modelAttr) &&
                    checkPresetModelCallback(rootNode.GetAttribute(modelAttr).Trim()) &&
                    rootNode.HasAttribute(versionAttr) &&
                    rootNode.GetAttribute(versionAttr) == version)
                {

                    KXEffectorModel effector = KXEffectorModel.GetTempModel();

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
            }

            throw new Exception();
        }

        private static XmlDocument CreateEffectorXmlDoc(KXEffectorModel effector, string model, bool isContainAllSystemData)
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
            rootEle.AppendChild(CreateStereoOutputNode(xmldoc, effector.mainOutput, mainOutputModule));
            rootEle.AppendChild(CreateMonoOutputNode(xmldoc, effector.aux1Output, aux1OutputModule));
            rootEle.AppendChild(CreateMonoOutputNode(xmldoc, effector.aux2Output, aux2OutputModule));
            rootEle.AppendChild(CreateStereoOutputNode(xmldoc, effector.surOutput, surOutputModule));
            rootEle.AppendChild(CreateSystemNode(xmldoc, effector.system, isContainAllSystemData));
            
            return xmldoc;
        }

        private const string workModeNode = "workMode";
        private const string workSelectNode = "workSelect";
        private const string singHPFFreqNode = "singHPFFreq";
        private const string discoHPFFreqNode = "discoHPFFreq";

        private static void ParseMusicModule(XmlElement moduleNode, KXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            MusicModule music = effector.music;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseMusicParam(ele, music);
                }
                else if (ele.Name == eqGroupNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if(groupName == singEQGroupAttr)
                    {
                        ParseEQGroup(ele, music.singEQGroup);
                    }
                    else if (groupName == discoEQGroupAttr)
                    {
                        ParseEQGroup(ele, music.discoEQGroup);
                    }
                }
            }
        }

        private static void ParseMusicParam(XmlElement dataEle, MusicModule music)
        {
            string name = dataEle.GetAttribute(nameAttr);
            short value = ParseText2Short(dataEle.InnerText);
            if(value < 0)
            {
                return;
            }

            switch(name)
            {
                case workModeNode:
                    music.autoWorkMode.workModeInit = value;
                    break;

                case workSelectNode:
                    music.autoWorkMode.workSelectInit = value;
                    break;

                case singHPFFreqNode:
                    music.singHPF.freqIndex = value;
                    break;

                case discoHPFFreqNode:
                    music.discoHPF.freqIndex = value;
                    break;
            }
        }

        private static XmlElement CreateMusicNode(XmlDocument xmldoc, MusicModule music)
        {
            XmlElement ele = CreateModuleNode(xmldoc, musicModule);

            ele.AppendChild(CreateParamNode(xmldoc, workModeNode, music.autoWorkMode.wm.v));
            ele.AppendChild(CreateParamNode(xmldoc, workSelectNode, music.autoWorkMode.ws.v));
            ele.AppendChild(CreateParamNode(xmldoc, singHPFFreqNode, music.singHPF.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, discoHPFFreqNode, music.discoHPF.freqIndex));
            ele.AppendChild(CreateEQGroupElement(xmldoc, music.singEQGroup, singEQGroupAttr));
            ele.AppendChild(CreateEQGroupElement(xmldoc, music.discoEQGroup, discoEQGroupAttr));
            return ele;
        }

        private static XmlElement CreateMicNode(XmlDocument xmldoc, MicModule mic)
        {
            XmlElement ele = CreateModuleNode(xmldoc, micModule);
            //ele.AppendChild(CreateParamNode(xmldoc, micInputAttr, mic.micInput));
            ele.AppendChild(CreateParamNode(xmldoc, micNoiseGateAttr, mic.micNoiseGate)); 
            ele.AppendChild(CreateParamNode(xmldoc, fbeAttr, mic.fbe));
            ele.AppendChild(CreateCompressorNode(xmldoc, mic.compressor));
            ele.AppendChild(CreateParamNode(xmldoc, hPFFreqAttr, mic.hpf.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lPFFreqAttr, mic.lpf.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lpfTypeAttr, mic.lpf.typeParam));
            ele.AppendChild(CreateEQGroupElement(xmldoc, mic.eqGroup));
            return ele;
        }

        private static void ParseMicModule(XmlElement moduleNode, KXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            MicModule mic = effector.mic;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseMicParam(ele, mic);
                }
                else if (ele.Name == compressorNode)
                {
                    ParseCompressorElement(ele, mic.compressor);
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
                //case micInputAttr:
                //    mic.micInput.initV = value;
                //    break;

                case micNoiseGateAttr:
                    mic.micNoiseGate.initV = value;
                    break;

                case fbeAttr:
                    mic.fbe.initV = value;
                    break;

                case hPFFreqAttr:
                    mic.hpf.freqIndex = value;
                    break;

                case lPFFreqAttr:
                    mic.lpf.freqIndex = value;
                    break;

                case lpfTypeAttr:
                    mic.lpf.typeIndex = value;
                    break;
            }
        }

        private static void ParseEchoModule(XmlElement moduleNode, KXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            EchoModule echo = effector.echo;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseEchoParam(ele, echo);
                }
                else if (ele.Name == levelNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == echoLevelAttr)
                    {
                        ParseLevelElement(ele, echo.echoLevel);
                    }
                    else if (groupName == directLevelAttr)
                    {
                        ParseLevelElement(ele, echo.directLevel);
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
                case hPFFreqAttr:
                    echo.highPassFilter.freqIndex = value;
                    break;

                case hpfTypeAttr:
                    echo.highPassFilter.typeIndex = value;
                    break;

                case lPFFreqAttr:
                    echo.lowPassFilter.freqIndex = value;
                    break;

                case lpfTypeAttr:
                    echo.lowPassFilter.typeIndex = value;
                    break;

                case preDelayAttr:
                    echo.preDelay.initV = value;
                    break;

                case delayTimeAttr:
                    echo.delayTime.initV = value;
                    break;

                case repeatAttr:
                    echo.repeat.initV = value;
                    break;

                case rChDelayTimeAttr:
                    echo.rChDelayTime.initV = value;
                    break;

                case rChPreDelayAttr:
                    echo.rChPreDelay.initV = value;
                    break;
            }
        }

        private static XmlElement CreateEchoNode(XmlDocument xmldoc, EchoModule echo)
        {
            XmlElement ele = CreateModuleNode(xmldoc, echoModule);

            ele.AppendChild(CreateLevelNode(xmldoc, echoLevelAttr, echo.echoLevel));
            ele.AppendChild(CreateLevelNode(xmldoc, directLevelAttr, echo.directLevel));
            ele.AppendChild(CreateParamNode(xmldoc, hPFFreqAttr, echo.highPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, hpfTypeAttr, echo.highPassFilter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lPFFreqAttr, echo.lowPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lpfTypeAttr, echo.lowPassFilter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, preDelayAttr, echo.preDelay));
            ele.AppendChild(CreateParamNode(xmldoc, delayTimeAttr, echo.delayTime));
            ele.AppendChild(CreateParamNode(xmldoc, repeatAttr, echo.repeat));
            ele.AppendChild(CreateParamNode(xmldoc, rChDelayTimeAttr, echo.rChDelayTime));
            ele.AppendChild(CreateParamNode(xmldoc, rChPreDelayAttr, echo.rChPreDelay));

            ele.AppendChild(CreateEQGroupElement(xmldoc, echo.eqGroup));

            return ele;
        }

        private static void ParseReverbModule(XmlElement moduleNode, KXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ReverbModule reverb = effector.reverb;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseReverbParam(ele, reverb);
                }
                else if (ele.Name == levelNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == reverbLevelAttr)
                    {
                        ParseLevelElement(ele, reverb.reverbLevel);
                    }
                    else if (groupName == directLevelAttr)
                    {
                        ParseLevelElement(ele, reverb.directLevel);
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
                case delayTimeAttr:
                    reverb.delayTime.initV = value;
                    break;

                case preDelayAttr:
                    reverb.preDelay.initV = value;
                    break;

                case hPFFreqAttr:
                    reverb.highPassFilter.freqIndex = value;
                    break;

                case hpfTypeAttr:
                    reverb.highPassFilter.typeIndex = value;
                    break;

                case lPFFreqAttr:
                    reverb.lowPassFilter.freqIndex = value;
                    break;

                case lpfTypeAttr:
                    reverb.lowPassFilter.typeIndex = value;
                    break;

                case bassDampAttr:
                    reverb.bassDamp.initV = value;
                    break;

                case trebleDampAttr:
                    reverb.trebleDamp.initV = value;
                    break;
            }
        }

        private static XmlElement CreateReverbNode(XmlDocument xmldoc, ReverbModule reverb)
        {
            XmlElement ele = CreateModuleNode(xmldoc, reverbModule);

            ele.AppendChild(CreateLevelNode(xmldoc, reverbLevelAttr, reverb.reverbLevel));
            ele.AppendChild(CreateLevelNode(xmldoc, directLevelAttr, reverb.directLevel));
            ele.AppendChild(CreateParamNode(xmldoc, delayTimeAttr, reverb.delayTime));
            ele.AppendChild(CreateParamNode(xmldoc, preDelayAttr, reverb.preDelay));
            ele.AppendChild(CreateParamNode(xmldoc, hPFFreqAttr, reverb.highPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, hpfTypeAttr, reverb.highPassFilter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lPFFreqAttr, reverb.lowPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lpfTypeAttr, reverb.lowPassFilter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, bassDampAttr, reverb.bassDamp));
            ele.AppendChild(CreateParamNode(xmldoc, trebleDampAttr, reverb.trebleDamp));

            return ele;
        }

        private static void ParseMainOutputModule(XmlElement moduleNode, KXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);

            ParseStereoOutputModule(moduleNode, effector.mainOutput);
        }

        private static void ParseSurOutputModule(XmlElement moduleNode, KXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);

            ParseStereoOutputModule(moduleNode, effector.surOutput);
        }

        private static void ParseStereoOutputModule(XmlElement moduleNode, StereoOutputModule output)
        {
            Debug.Assert(moduleNode != null && output != null);

            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == outputChannelNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == leftChannel)
                    {
                        ParseOutputChannelElement(ele, output.leftChannel);
                    }
                    else if (groupName == rightChannel)
                    {
                        ParseOutputChannelElement(ele, output.rightChannel);
                    }
                }
                else
                {
                    ParseOutputBaseModule(ele, output);
                }
            }
        }

        private static void ParseAux1OutputModule(XmlElement moduleNode, KXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseMonoOutputModule(moduleNode, effector.aux1Output);
        }

        private static void ParseAux2OutputModule(XmlElement moduleNode, KXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseMonoOutputModule(moduleNode, effector.aux2Output);
        }

        private static void ParseMonoOutputModule(XmlElement moduleNode, MonoOutputModule output)
        {
            Debug.Assert(moduleNode != null && output != null);

            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == outputChannelNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == monoChannel)
                    {
                        ParseOutputChannelElement(ele, output.monoChannel);
                    }
                }
                else
                {
                    ParseOutputBaseModule(ele, output);
                }
            }
        }

        private static void ParseOutputBaseModule(XmlElement ele, OutputBaseModule output)
        {
            if (ele.Name == outputLevelNode)
            {
                string groupName = ele.GetAttribute(nameAttr);
                if (groupName == musicLevelAttr)
                {
                    ParseOutputLevelParam(ele, output.musicLevel);
                }
                else if (groupName == micLevelAttr)
                {
                    ParseOutputLevelParam(ele, output.micLevel);
                }
                else if (groupName == echoLevelAttr)
                {
                    ParseOutputLevelParam(ele, output.echoLevel);
                }
                else if (groupName == reverbLevelAttr)
                {
                    ParseOutputLevelParam(ele, output.reverbLevel);
                }
            }
            else if (ele.Name == hpfNode)
            {
                ParseOutputFilterNode(ele, output.hpf);
            }
            else if (ele.Name == lpfNode)
            {
                ParseOutputFilterNode(ele, output.lpf);
            }
            else if (ele.Name == limiterNode)
            {
                ParseLimiterElement(ele, output.limiter);
            }            
            else if (ele.Name == eqGroupNode)
            {
                ParseEQGroup(ele, output.eqGroup);
            }
        }

        private static void ParseOutputFilterNode(XmlElement dataEle, XPFilterBase filter)
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

                    if (name == filterTypeAttr)
                    {
                        filter.typeIndex = value;
                    }
                    else if (name == freqAttr)
                    {
                        filter.freqIndex = value;
                    }
                }
            }
        }

        private static void ParseOutputLevelParam(XmlElement dataEle, OutputLevel outputLevel)
        {
            foreach (XmlElement ele in dataEle.ChildNodes)
            {
                if (ele.Name == levelNode)
                {
                    string name = ele.GetAttribute(nameAttr);
                    if (name == singAttr)
                    {
                        ParseLevelElement(ele, outputLevel.singLevel);
                    }
                    else if (name == discoAttr)
                    {
                        ParseLevelElement(ele, outputLevel.discoLevel);
                    }
                }
            }
        }

        private static XmlElement CreateStereoOutputNode(XmlDocument xmldoc, StereoOutputModule output, string name)
        {
            XmlElement ele = CreateOutputBaseNode(xmldoc, output, name);
            AppendOutput(xmldoc, ele, output);
            ele.AppendChild(CreateEQGroupElement(xmldoc, output.eqGroup));

            return ele;
        }

        private static XmlElement CreateMonoOutputNode(XmlDocument xmldoc, MonoOutputModule output, string name)
        {
            XmlElement ele = CreateOutputBaseNode(xmldoc, output, name);
            AppendAux(xmldoc, ele, output);
            ele.AppendChild(CreateEQGroupElement(xmldoc, output.eqGroup));

            return ele;
        }

        private static void AppendOutput(XmlDocument xmldoc, XmlElement outputEle, StereoOutputModule output)
        {
            Debug.Assert(outputEle != null);

            outputEle.AppendChild(CreateOutputChannelNode(xmldoc, leftChannel, output.leftChannel));
            outputEle.AppendChild(CreateOutputChannelNode(xmldoc, rightChannel, output.rightChannel));
        }

        private static void AppendAux(XmlDocument xmldoc, XmlElement outputEle, MonoOutputModule output)
        {
            Debug.Assert(outputEle != null);

            outputEle.AppendChild(CreateOutputChannelNode(xmldoc, monoChannel, output.monoChannel));
        }
        
        private static XmlElement CreateOutputBaseNode(XmlDocument xmldoc, OutputBaseModule output, string name)
        {
            XmlElement ele = CreateModuleNode(xmldoc, name);
            ele.AppendChild(CreateOutputLevelNode(xmldoc, output.musicLevel, musicLevelAttr));
            ele.AppendChild(CreateOutputLevelNode(xmldoc, output.micLevel, micLevelAttr));
            ele.AppendChild(CreateOutputLevelNode(xmldoc, output.echoLevel, echoLevelAttr));
            ele.AppendChild(CreateOutputLevelNode(xmldoc, output.reverbLevel, reverbLevelAttr));

            ele.AppendChild(CreateOutputFilterNode(xmldoc, output.hpf, hpfNode));
            ele.AppendChild(CreateOutputFilterNode(xmldoc, output.lpf, lpfNode));

            ele.AppendChild(CreateLimiterNode(xmldoc, output.limiter));

            return ele;
        }

        private static void ParseSystemParam(XmlElement dataEle, KXEffectorModel effector, bool isContainAllSystemData)
        {
            SystemModuleBase system = effector.system;
            
            if(isContainAllSystemData)
            {
                Dictionary<string, ShortParam> paramsMap = new Dictionary<string, ShortParam>()
                {
                    {musicStartVolumeAttr, system.musicStartVolume},
                    {musicMaxVolumeAttr, system.musicMaxVolume},
                    {micStartVolumeAttr, system.micStartVolume},
                    {micMaxVolumeAttr, system.micMaxVolume},
                    {effectStartVolumeAttr, system.effectStartVolume},
                    {effectMaxVolumeAttr, system.effectMaxVolume},
                    {musicVolumeAttr, system.musicVolume},
                    {micVolumeAttr, system.micVolume},
                    {effectVolumeAttr, system.effectVolume},
                    {musicMuteAttr, system.musicMute},
                    {micMuteAttr, system.micMute},
                    {effectMuteAttr, system.effectMute},
                    {deviceModeSaveIndexAttr, system.deviceModeSaveIndex},
                    {deviceModeCallIndexAttr, system.deviceModeCallIndex},
                    {musicInputGainAttr, system.musicInputGain},
                    {systemUserAttr, system.user},
                    {isLockKeyboardAttr, system.isLockKeyboard},
                    {musicInputPortAttr, system.musicInputPort},
                    {isEnableRemoteControlAttr, system.isEnableRemote},
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
                                case systemPasswordAttr:
                                    system.systemPasswordInit = ele.InnerText;
                                    break;

                                case keyboardLockPasswordAttr:
                                    system.keyboardLockPasswordInit = ele.InnerText;
                                    break;

                                case deviceIdAttr:
                                    byte[] idData = Utils.HexStringToByteArray(ele.InnerText);
                                    if (idData != null && idData.Length == AEC.Common.SystemModuleBase.DeviceIdLength)
                                    {
                                        Array.Copy(idData, system.deviceId, AEC.Common.SystemModuleBase.DeviceIdLength);
                                    }
                                    break;

                                case deviceIsActivatedAttr:
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

                                case dataIsPrivateAttr:
                                    {
                                        short value = ParseText2Short(ele.InnerText);
                                        system.dataIsPrivate = DataConverter.Short2Bool(value);
                                        //AEC.Log.OutputLine("system dataIsPrivate:{0}", system.dataIsPrivate);
                                    }
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
                                short index = ParseText2Short(modeNameEle.GetAttribute(indexAttr));
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
                    {musicStartVolumeAttr, system.musicStartVolume},
                    {musicMaxVolumeAttr, system.musicMaxVolume},
                    {micStartVolumeAttr, system.micStartVolume},
                    {micMaxVolumeAttr, system.micMaxVolume},
                    {effectStartVolumeAttr, system.effectStartVolume},
                    {effectMaxVolumeAttr, system.effectMaxVolume},
                    {timbreAttr, system.timbreControl},
                    {musicInputPortAttr, system.musicInputPort},
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
            ele.AppendChild(CreateParamNode(xmldoc, musicStartVolumeAttr, system.musicStartVolume));
            ele.AppendChild(CreateParamNode(xmldoc, musicMaxVolumeAttr, system.musicMaxVolume));
            ele.AppendChild(CreateParamNode(xmldoc, micStartVolumeAttr, system.micStartVolume));
            ele.AppendChild(CreateParamNode(xmldoc, micMaxVolumeAttr, system.micMaxVolume));
            ele.AppendChild(CreateParamNode(xmldoc, effectStartVolumeAttr, system.effectStartVolume));
            ele.AppendChild(CreateParamNode(xmldoc, effectMaxVolumeAttr, system.effectMaxVolume));
            ele.AppendChild(CreateParamNode(xmldoc, timbreAttr, system.timbreControl));
            ele.AppendChild(CreateParamNode(xmldoc, musicInputPortAttr, system.musicInputPort));

            if (isContainAllSystemData)
            {
                ele.AppendChild(CreateModeNameListElement(xmldoc, system.deviceModeList));
                ele.AppendChild(CreateParamNode(xmldoc, deviceModeSaveIndexAttr, system.deviceModeSaveIndex));
                ele.AppendChild(CreateParamNode(xmldoc, deviceModeCallIndexAttr, system.deviceModeCallIndex));
                ele.AppendChild(CreateParamNode(xmldoc, musicInputGainAttr, system.musicInputGain));
                ele.AppendChild(CreateParamNode(xmldoc, systemPasswordAttr, system.systemPassword));
                ele.AppendChild(CreateParamNode(xmldoc, keyboardLockPasswordAttr, system.keyboardLockPassword));
                ele.AppendChild(CreateParamNode(xmldoc, systemUserAttr, system.user));
                ele.AppendChild(CreateParamNode(xmldoc, isLockKeyboardAttr, system.isLockKeyboard));
                ele.AppendChild(CreateParamNode(xmldoc, isEnableRemoteControlAttr, system.isEnableRemote));
                ele.AppendChild(CreateParamNode(xmldoc, musicVolumeAttr, system.musicVolume));
                ele.AppendChild(CreateParamNode(xmldoc, micVolumeAttr, system.micVolume));
                ele.AppendChild(CreateParamNode(xmldoc, effectVolumeAttr, system.effectVolume));
                ele.AppendChild(CreateParamNode(xmldoc, musicMuteAttr, system.musicMute));
                ele.AppendChild(CreateParamNode(xmldoc, micMuteAttr, system.micMute));
                ele.AppendChild(CreateParamNode(xmldoc, effectMuteAttr, system.effectMute));
                ele.AppendChild(CreateParamNode(xmldoc, deviceIdAttr, system.deviceIdCode));
                ele.AppendChild(CreateParamNode(xmldoc, deviceIsActivatedAttr, DataConverter.Bool2Short(system.deviceIsActivated)));
                ele.AppendChild(CreateParamNode(xmldoc, dataIsPrivateAttr, DataConverter.Bool2Short(system.dataIsPrivate)));                
            }
            
            return ele;
        }

        private static XmlElement CreateModeNameListElement(XmlDocument xmldoc, string[] deviceModeList)
        {
            XmlElement modeNameListEle = xmldoc.CreateElement(modeNameListNode);

            for (int i = 0; i < deviceModeList.Length; ++i)
            {
                XmlElement modeNameEle = xmldoc.CreateElement(modeNameNode);

                modeNameEle.SetAttribute(indexAttr, i.ToString());
                modeNameEle.InnerText = deviceModeList[i];

                modeNameListEle.AppendChild(modeNameEle);
            }

            return modeNameListEle;
        }

        //通用
        private static void ParseOutputChannelElement(XmlElement channelEle, AEC.Common.OutputChannel outputChannel)
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
                            case delayAttr:
                                outputChannel.delay.initV = value;
                                break;

                            case muteAttr:
                                outputChannel.mute.initV = value;
                                break;

                            case phaseAttr:
                                outputChannel.phase.initV = value;
                                break;
                        }
                    }
                }
            }
        }

        private static XmlElement CreateOutputChannelNode(XmlDocument xmldoc, string name, AEC.Common.OutputChannel outputChannel)
        {
            XmlElement ele = xmldoc.CreateElement(outputChannelNode);
            ele.SetAttribute(nameAttr, name);

            ele.AppendChild(CreateParamNode(xmldoc, delayAttr, outputChannel.delay));
            ele.AppendChild(CreateParamNode(xmldoc, muteAttr, outputChannel.mute));
            ele.AppendChild(CreateParamNode(xmldoc, phaseAttr, outputChannel.phase));

            return ele;
        }

        private static XmlElement CreateOutputFilterNode(XmlDocument xmldoc, XPFilterBase filter, string name)
        {
            XmlElement ele = xmldoc.CreateElement(name);
            ele.AppendChild(CreateParamNode(xmldoc, filterTypeAttr, filter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, freqAttr, filter.freqIndex));

            return ele;
        }

        private static XmlElement CreateOutputLevelNode(XmlDocument xmldoc, OutputLevel outputLevel, string name)
        {
            XmlElement ele = xmldoc.CreateElement(outputLevelNode);
            ele.SetAttribute(nameAttr, name);

            ele.AppendChild(CreateLevelNode(xmldoc, singAttr, outputLevel.singLevel));
            ele.AppendChild(CreateLevelNode(xmldoc, discoAttr, outputLevel.discoLevel));

            return ele;
        }


        private static void ParseLevelElement(XmlElement levelEle, Level level)
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
                            case levelAttr:
                                level.level.initV = value;
                                break;

                            case phaseAttr:
                                level.phase.initV = value;
                                //AEC.Log.OutputLine("AEC phase = {0}", level.phase.v);
                                break;
                        }
                    }
                }
            }
        }

        private static XmlElement CreateLevelNode(XmlDocument xmldoc, string name, Level level)
        {
            XmlElement ele = xmldoc.CreateElement(levelNode);
            ele.SetAttribute(nameAttr, name);

            ele.AppendChild(CreateParamNode(xmldoc, levelAttr, level.level));
            ele.AppendChild(CreateParamNode(xmldoc, phaseAttr, level.phase));
            
            return ele;
        }

        private static void ParseLimiterElement(XmlElement limiterEle, Limiter limiter)
        {
            if (limiterEle.Name == limiterNode)
            {
                foreach (XmlElement ele in limiterEle.ChildNodes)
                {
                    if (ele.Name == paramNode)
                    {
                        string name = ele.GetAttribute(nameAttr);
                        short value = ParseText2Short(ele.InnerText);
                        if (value >= 0)
                        {
                            switch (name)
                            {
                                case thresholdAttr:
                                    limiter.threshold.initV = value;
                                    break;

                                case attackAttr:
                                    limiter.attack.initV = value;
                                    break;

                                case releaseAttr:
                                    limiter.release.initV = value;
                                    break;

                                case clipThresholdNode:
                                    limiter.clipThreshold.initV = value;
                                    break;

                                case clipReleaseNode:
                                    limiter.clipRelease.initV = value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static XmlElement CreateLimiterNode(XmlDocument xmldoc, Limiter limiter)
        {
            XmlElement ele = xmldoc.CreateElement(limiterNode);

            ele.AppendChild(CreateParamNode(xmldoc, thresholdAttr, limiter.threshold));
            ele.AppendChild(CreateParamNode(xmldoc, attackAttr, limiter.attack));
            ele.AppendChild(CreateParamNode(xmldoc, releaseAttr, limiter.release));
            ele.AppendChild(CreateParamNode(xmldoc, clipThresholdNode, limiter.clipThreshold));
            ele.AppendChild(CreateParamNode(xmldoc, clipReleaseNode, limiter.clipRelease));

            return ele;
        }

        private static void ParseCompressorElement(XmlElement compressorEle, Compressor compressor)
        {
            if (compressorEle.Name == compressorNode)
            {
                foreach (XmlElement ele in compressorEle.ChildNodes)
                {
                    if (ele.Name == paramNode)
                    {
                        string name = ele.GetAttribute(nameAttr);
                        short value = ParseText2Short(ele.InnerText);
                        if (value >= 0)
                        {
                            switch (name)
                            {
                                case thresholdAttr:
                                    compressor.threshold.initV = value;
                                    break;

                                case attackAttr:
                                    compressor.attack.initV = value;
                                    break;

                                case releaseAttr:
                                    compressor.release.initV = value;
                                    break;

                                case ratioAttr:
                                    compressor.ratio.initV = value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static XmlElement CreateCompressorNode(XmlDocument xmldoc, Compressor compressor)
        {
            XmlElement ele = xmldoc.CreateElement(compressorNode);

            ele.AppendChild(CreateParamNode(xmldoc, thresholdAttr, compressor.threshold));
            ele.AppendChild(CreateParamNode(xmldoc, attackAttr, compressor.attack));
            ele.AppendChild(CreateParamNode(xmldoc, releaseAttr, compressor.release));
            ele.AppendChild(CreateParamNode(xmldoc, ratioAttr, compressor.ratio));

            //ele.SetAttribute("threshold", compressor.threshold.v.ToString());
            //ele.SetAttribute("attack", compressor.attack.v.ToString());
            //ele.SetAttribute("release", compressor.release.v.ToString());
            //ele.SetAttribute("ratio", compressor.ratio.v.ToString());

            return ele;
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
                short index = ParseText2Short(eqEle.GetAttribute(indexAttr));
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
                                case typeAttr:
                                    eqGroup.eqList[index].typeIndex = value;
                                    //AEC.Log.OutputLine("eq type:{0}", eqGroup.eqList[index].type);
                                    break;

                                case freqAttr:
                                    eqGroup.eqList[index].freqIndex = value;
                                    break;

                                case gainAttr:
                                    eqGroup.eqList[index].gainIndex = value;
                                    break;

                                case qAttr:
                                    eqGroup.eqList[index].qIndex_ = value;
                                    break;

                                case sAttr:
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
            ele.SetAttribute(indexAttr, eq.index.ToString());
            //AEC.Log.OutputLine("eq type:{0}", eq.type);
            ele.AppendChild(CreateParamNode(xmldoc, typeAttr, eq.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, freqAttr, eq.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, gainAttr, eq.gainIndex));
            ele.AppendChild(CreateParamNode(xmldoc, qAttr, eq.qIndex_));
            ele.AppendChild(CreateParamNode(xmldoc, sAttr, eq.sIndex_));

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
        private const string indexAttr = "index";

        private const string typeAttr = "type";
        private const string freqAttr = "freq";
        private const string gainAttr = "gain";
        private const string qAttr = "q";
        private const string sAttr = "s";

        private const string currEQIndexAttr = "currEQIndex";
        private const string eqIsPassAttr = "eqIsPass";

        private const string compressorNode = "compressor";
        private const string thresholdAttr = "threshold";
        private const string attackAttr = "attack";
        private const string releaseAttr = "release";
        private const string ratioAttr = "ratio";

        private const string limiterNode = "limiter";
        private const string clipThresholdNode = "clipThreshold";
        private const string clipReleaseNode = "clipRelease";

        private const string levelNode = "level";
        private const string levelAttr = "level";
        private const string phaseAttr = "phase";
        private const string outputLevelNode = "outputLevel";
        private const string filterModeAttr = "mode";
        private const string filterTypeAttr = "type";
        private const string hpfTypeAttr = "hpfType";
        private const string lpfTypeAttr = "lpfType";
        private const string singEQGroupAttr = "singEQGroup";
        private const string discoEQGroupAttr = "discoEQGroup";
        private const string micInputAttr = "micInput";
        private const string micNoiseGateAttr = "noiseGate";
        private const string hPFFreqAttr = "hPFFreq";
        private const string fbeAttr = "fbe";
        private const string echoLevelAttr = "echoLevel";
        private const string directLevelAttr = "directLevel";
        private const string lPFFreqAttr = "lPFFreq";
        private const string lPFQAttr = "lPFQ";
        private const string preDelayAttr = "preDelay";
        private const string delayTimeAttr = "delayTime";
        private const string repeatAttr = "repeat";
        private const string rChDelayTimeAttr = "rChDelayTime";
        private const string rChPreDelayAttr = "rChPreDelay";
        private const string reverbLevelAttr = "reverbLevel";
        private const string bassDampAttr = "bassDamp";
        private const string trebleDampAttr = "trebleDamp";

        private const string singAttr = "sing";
        private const string discoAttr = "disco";

        private const string musicLevelAttr = "musicLevel";
        private const string micLevelAttr = "micLevel";
        //private const string echoLevelAttr = "echoLevel";
        //private const string reverbLevelAttr = "reverbLevel";

        private const string hpfNode = "hpf";
        private const string lpfNode = "lpf";

        private const string outputChannelNode = "outputChannel";
        private const string leftChannel = "leftChannel";
        private const string rightChannel = "rightChannel";
        private const string monoChannel = "monoChannel";

        private const string leftChDelayAttr = "leftChDelay";
        private const string rightChDelayAttr = "rightChDelay";
        private const string leftChMuteAttr = "leftChMute";
        private const string rightChMuteAttr = "rightChMute";
        private const string delayAttr = "delay";
        private const string muteAttr = "mute";

        
        private const string musicStartVolumeAttr = "musicStartVolume";
        private const string musicMaxVolumeAttr = "musicMaxVolume";
        private const string micStartVolumeAttr = "micStartVolume";
        private const string micMaxVolumeAttr = "micMaxVolume";
        private const string effectStartVolumeAttr = "effectStartVolume";
        private const string effectMaxVolumeAttr = "effectMaxVolume";
        private const string timbreAttr = "timbre";
        private const string modeNameListNode = "modeNameList";
        private const string modeNameNode = "modeName";
        private const string deviceModeSaveIndexAttr = "deviceModeSaveIndex";
        private const string deviceModeCallIndexAttr = "deviceModeCallIndex";
        private const string musicInputGainAttr = "musicInputGain";
        private const string systemPasswordAttr = "systemPassword";
        private const string keyboardLockPasswordAttr = "keyboardLockPassword";
        private const string systemUserAttr = "systemUser";
        private const string isLockKeyboardAttr = "isLockKeyboard";
        private const string musicInputPortAttr = "musicInputPort";
        private const string isEnableRemoteControlAttr = "isEnableRemoteControl";
        private const string musicVolumeAttr = "musicVolume";
        private const string micVolumeAttr = "micVolume";
        private const string effectVolumeAttr = "effectVolume";
        private const string musicMuteAttr = "musicMute";
        private const string micMuteAttr = "micMute";
        private const string effectMuteAttr = "effectMute";
        private const string deviceIdAttr = "deviceId";
        private const string deviceIsActivatedAttr = "deviceIsActivated";
        private const string deviceModelAttr = "deviceModel";
        private const string dataIsPrivateAttr = "dataIsPrivate";
    }
}
