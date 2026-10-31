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
using AEC.Effectors.VKxSCM;
using AEC.Effectors.VKxSCM.Modules;
using AEC.Misc;

namespace AEC.Effectors.VKxSCM
{
    public class VKxSCMDataStorage
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
        private const string aux1OutputModule = "aux1Output";
        private const string aux2OutputModule = "aux2Output";
        private const string surOutputModule = "surOutput";
        private const string systemModule = "system";

        private delegate void ParseModuleDataCallback(XmlElement ele, VKxSCMEffectorModel effector);

        public static Localization localization = Config.GetInstance().localization;

        public static void SaveToFile(VKxSCMEffectorModel effector, string model, string filePath, bool isContainAllSystemData)
        {
            Debug.Assert(effector != null);

            XmlDocument xmldoc = CreateEffectorXmlDoc(effector, model, isContainAllSystemData);

            xmldoc.Save(filePath);
        }

        public static VKxSCMEffectorModel LoadFromFile(string filePath, CheckPresetModel checkPresetModelCallback)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            VKxSCMEffectorModel effector = LoadFromStream(fs, checkPresetModelCallback, false);
            fs.Close();
            return effector;
        }

        public static VKxSCMEffectorModel Buffer2Effector(byte[] buffer, CheckPresetModel checkPresetModelCallback, bool isContainAllSystemData = false)
        {
            MemoryStream ms = new MemoryStream(buffer);
            return LoadFromStream(ms, checkPresetModelCallback, isContainAllSystemData);
        }

        public static byte[] Effector2Buffer(VKxSCMEffectorModel effector, string model, bool isContainAllSystemData = false)
        {
            XmlDocument xmldoc = CreateEffectorXmlDoc(effector, model, isContainAllSystemData);

            MemoryStream ms = new MemoryStream();
            xmldoc.Save(ms);
            return ms.ToArray();
        }

        private static VKxSCMEffectorModel LoadFromStream(Stream stream, CheckPresetModel checkPresetModelCallback, bool isContainAllSystemData)
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

                    VKxSCMEffectorModel effector = VKxSCMEffectorModel.GetTempModel();

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

        private static XmlDocument CreateEffectorXmlDoc(VKxSCMEffectorModel effector, string model, bool isContainAllSystemData)
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

        private static void ParseMusicModule(XmlElement moduleNode, VKxSCMEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            MusicModule music = effector.music;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == autoWrokModeNode)
                {
                    ParseAutoWorkModeElement(ele, music.autoWorkMode);
                }
                else if (ele.Name == bassExciterNode)
                {
                    ParseBassExciterNode(ele, music.bassExciter);
                }
                else if (ele.Name == filterNode)
                {
                    string filterName = ele.GetAttribute(nameAttr);
                    if (filterName == singHpfName)
                    {
                        ParseFilterNode(ele, music.singHPF);
                    }
                    else if(filterName == discoHpfName)
                    {
                        ParseFilterNode(ele, music.discoHPF);
                    }
                }
                else if (ele.Name == eqGroupNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if(groupName == singEQGroupName)
                    {
                        ParseEQGroup(ele, music.singEQGroup);
                    }
                    else if (groupName == discoEQGroupName)
                    {
                        ParseEQGroup(ele, music.discoEQGroup);
                    }
                }
            }
        }

        private static XmlElement CreateMusicNode(XmlDocument xmldoc, MusicModule music)
        {
            XmlElement ele = CreateModuleNode(xmldoc, musicModule);

            ele.AppendChild(CreateAutoWorkModeNode(xmldoc, music.autoWorkMode));

            ele.AppendChild(CreateBassExciterNode(xmldoc, music.bassExciter));

            ele.AppendChild(CreateFilterNode(xmldoc, music.singHPF, singHpfName));
            ele.AppendChild(CreateFilterNode(xmldoc, music.discoHPF, discoHpfName));
            
            ele.AppendChild(CreateEQGroupElement(xmldoc, music.singEQGroup, singEQGroupName));
            ele.AppendChild(CreateEQGroupElement(xmldoc, music.discoEQGroup, discoEQGroupName));

            return ele;
        }

        private static XmlElement CreateMicNode(XmlDocument xmldoc, MicModule mic)
        {
            XmlElement ele = CreateModuleNode(xmldoc, micModule);
            ele.AppendChild(CreateParamNode(xmldoc, fbeName, mic.fbe));
            ele.AppendChild(CreateParamNode(xmldoc, micNoiseGateName, mic.micNoiseGate));

            ele.AppendChild(CreateCompressorNode(xmldoc, mic.compressor));

            ele.AppendChild(CreateParamNode(xmldoc, micExciter1SelectName, mic.exciter1Select));

            ele.AppendChild(CreateBassExciterNode(xmldoc, mic.bassExciter));
            ele.AppendChild(CreateExciterNode(xmldoc, mic.exciter1, micExciter1Name));
            ele.AppendChild(CreateExciterNode(xmldoc, mic.exciter2, micExciter2Name));

            ele.AppendChild(CreateParamNode(xmldoc, micHpfFreqName, mic.hpf.freqIndex));

            ele.AppendChild(CreateFilterNode(xmldoc, mic.lpf, micLpfName));

            ele.AppendChild(CreateEQGroupElement(xmldoc, mic.eqGroup));

            return ele;
        }

        private static void ParseMicModule(XmlElement moduleNode, VKxSCMEffectorModel effector)
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
                else if (ele.Name == bassExciterNode)
                {
                    ParseBassExciterNode(ele, mic.bassExciter);
                }
                else if (ele.Name == exciterNode)
                {
                    string exciterName = ele.GetAttribute(nameAttr);
                    if (exciterName == micExciter1Name)
                    {
                        ParseExciterNode(ele, mic.exciter1);
                    }
                    else if (exciterName == micExciter2Name)
                    {
                        ParseExciterNode(ele, mic.exciter2);
                    }
                }
                else if (ele.Name == filterNode)
                {
                    string filterName = ele.GetAttribute(nameAttr);
                    if (filterName == micLpfName)
                    {
                        ParseFilterNode(ele, mic.lpf);
                    }
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
                case micNoiseGateName:
                    mic.micNoiseGate.initV = value;
                    break;

                case fbeName:
                    mic.fbe.initV = value;
                    break;

                case micExciter1SelectName:
                    mic.exciter1Select.initV = value;
                    break;

                case micHpfFreqName:
                    mic.hpf.freqIndex = value;
                    break;
            }
        }

        private static void ParseEchoModule(XmlElement moduleNode, VKxSCMEffectorModel effector)
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
                case hpfFreqName:
                case hPFFreqName:
                    echo.highPassFilter.freqIndex = value;
                    break;

                case lpfFreqName:
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

                case rChPreDelayName:
                    echo.rChPreDelay.initV = value;
                    break;

                case rChDelayTimeName:
                    echo.rChDelayTime.initV = value;
                    break;
            }
        }

        private static XmlElement CreateEchoNode(XmlDocument xmldoc, EchoModule echo)
        {
            XmlElement ele = CreateModuleNode(xmldoc, echoModule);

            ele.AppendChild(CreateSignalMixerNode(xmldoc, echo.echoMixer, echoMixerName));
            ele.AppendChild(CreateSignalMixerNode(xmldoc, echo.directMixer, directMixerName));
            ele.AppendChild(CreateParamNode(xmldoc, hpfFreqName, echo.highPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lpfFreqName, echo.lowPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, preDelayName, echo.preDelay));
            ele.AppendChild(CreateParamNode(xmldoc, delayTimeName, echo.delayTime));
            ele.AppendChild(CreateParamNode(xmldoc, repeatName, echo.repeat));
            ele.AppendChild(CreateParamNode(xmldoc, rChPreDelayName, echo.rChPreDelay));
            ele.AppendChild(CreateParamNode(xmldoc, rChDelayTimeName, echo.rChDelayTime));
            ele.AppendChild(CreateEQGroupElement(xmldoc, echo.eqGroup));

            return ele;
        }

        private const string reverbLpfName = "reverbLpf";

        private static void ParseReverbModule(XmlElement moduleNode, VKxSCMEffectorModel effector)
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
                //else if (ele.Name == filterNode)
                //{
                //    string filterName = ele.GetAttribute(nameAttr);
                //    if (filterName == reverbLpfName)
                //    {
                //        ParseFilterNode(ele, reverb.lpf);
                //    }
                //}
                else if (ele.Name == eqGroupNode)
                {
                    ParseEQGroup(ele, reverb.eqGroup);
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
                case hpfFreqName:
                case hPFFreqName:
                    reverb.highPassFilter.freqIndex = value;
                    break;

                case lpfFreqName:
                case lPFFreqName:
                    reverb.lowPassFilter.freqIndex = value;
                    break;

                case preDelayName:
                    reverb.preDelay.initV = value;
                    break;

                case delayTimeName:
                    reverb.reverbTime.initV = value;
                    break;
            }
        }

        private static XmlElement CreateReverbNode(XmlDocument xmldoc, ReverbModule reverb)
        {
            XmlElement ele = CreateModuleNode(xmldoc, reverbModule);

            ele.AppendChild(CreateSignalMixerNode(xmldoc, reverb.reverbMixer, reverbMixerName));
            ele.AppendChild(CreateSignalMixerNode(xmldoc, reverb.directMixer, directMixerName));

            ele.AppendChild(CreateParamNode(xmldoc, hpfFreqName, reverb.highPassFilter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lpfFreqName, reverb.lowPassFilter.freqIndex));

            ele.AppendChild(CreateParamNode(xmldoc, preDelayName, reverb.preDelay));
            ele.AppendChild(CreateParamNode(xmldoc, delayTimeName, reverb.reverbTime));

            ele.AppendChild(CreateEQGroupElement(xmldoc, reverb.eqGroup));

            return ele;
        }

        private static void ParseMainOutputModule(XmlElement moduleNode, VKxSCMEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);

            ParseStereoOutputModule(moduleNode, effector.mainOutput);
        }

        private static void ParseSurOutputModule(XmlElement moduleNode, VKxSCMEffectorModel effector)
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

        private static void ParseAux1OutputModule(XmlElement moduleNode, VKxSCMEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseMonoOutputModule(moduleNode, effector.aux1Output);
        }

        private static void ParseAux2OutputModule(XmlElement moduleNode, VKxSCMEffectorModel effector)
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
            if (ele.Name == outputMixerNode)
            {
                string mixerName = ele.GetAttribute(nameAttr);
                if (mixerName == musicMixerName || mixerName == musicLevelName)
                {
                    ParseOutputMixerParam(ele, output.musicMixer);
                }
                else if (mixerName == micMixerName || mixerName == micLevelName)
                {
                    ParseOutputMixerParam(ele, output.micMixer);
                }                
                else if (mixerName == echoMixerName)
                {
                    ParseOutputMixerParam(ele, output.echoMixer);
                }
                else if (mixerName == reverbMixerName || mixerName == reverbLevelName)
                {
                    ParseOutputMixerParam(ele, output.reverbMixer);
                }
            }
            //else if (ele.Name == compressorNode)
            //{
            //    ParseCompressorElement(ele, output.compressor);
            //}
            else if (ele.Name == limiterNode)
            {
                ParseLimiterElement(ele, output.limiter);
            }  
            else if (ele.Name == hpfName)
            {
                ParseOutputFilterNode(ele, output.hpf);
            }
            else if (ele.Name == lpfName)
            {
                ParseOutputFilterNode(ele, output.lpf);
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

                    if (name == filterTypeName)
                    {
                        filter.typeIndex = value;
                    }
                    else if (name == freqName)
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
                    if (name == singName)
                    {
                        ParseLevelElement(ele, outputLevel.singLevel);
                    }
                    else if (name == discoName)
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
            ele.AppendChild(CreateOutputMixerNode(xmldoc, output.musicMixer, musicMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, output.micMixer, micMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, output.echoMixer, echoMixerName));
            ele.AppendChild(CreateOutputMixerNode(xmldoc, output.reverbMixer, reverbMixerName));

            //ele.AppendChild(CreateCompressorNode(xmldoc, output.compressor));

            ele.AppendChild(CreateLimiterNode(xmldoc, output.limiter));

            ele.AppendChild(CreateOutputFilterNode(xmldoc, output.hpf, hpfName));
            ele.AppendChild(CreateOutputFilterNode(xmldoc, output.lpf, lpfName));

            return ele;
        }

        private static void ParseSystemParam(XmlElement dataEle, VKxSCMEffectorModel effector, bool isContainAllSystemData)
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

                                case dataIsPrivateName:
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
                    {musicStartVolumeName, system.musicStartVolume},
                    {musicMaxVolumeName, system.musicMaxVolume},
                    {micStartVolumeName, system.micStartVolume},
                    {micMaxVolumeName, system.micMaxVolume},
                    {effectStartVolumeName, system.effectStartVolume},
                    {effectMaxVolumeName, system.effectMaxVolume},
                    {clarityName, system.clarity},
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
            ele.AppendChild(CreateParamNode(xmldoc, clarityName, system.clarity));
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
                ele.AppendChild(CreateParamNode(xmldoc, dataIsPrivateName, DataConverter.Bool2Short(system.dataIsPrivate)));                
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
                            case delayName:
                                outputChannel.delay.initV = value;
                                break;

                            case muteName:
                                outputChannel.mute.initV = value;
                                break;

                            case phaseName:
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

            ele.AppendChild(CreateParamNode(xmldoc, delayName, outputChannel.delay));
            ele.AppendChild(CreateParamNode(xmldoc, muteName, outputChannel.mute));
            ele.AppendChild(CreateParamNode(xmldoc, phaseName, outputChannel.phase));

            return ele;
        }

        private static XmlElement CreateOutputFilterNode(XmlDocument xmldoc, XPFilterBase filter, string name)
        {
            XmlElement ele = xmldoc.CreateElement(name);
            ele.AppendChild(CreateParamNode(xmldoc, filterTypeName, filter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, freqName, filter.freqIndex));

            return ele;
        }

        private static void ParseOutputMixerParam(XmlElement dataEle, OutputMixer outputLevel)
        {
            foreach (XmlElement ele in dataEle.ChildNodes)
            {
                if (ele.Name == mixerNode)
                {
                    string name = ele.GetAttribute(nameAttr);
                    if (name == singName)
                    {
                        ParseSignalMixerElement(ele, outputLevel.singMixer);
                    }
                    else if (name == discoName)
                    {
                        ParseSignalMixerElement(ele, outputLevel.discoMixer);
                    }
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
                            case levelName:
                                level.level.initV = value;
                                break;

                            case phaseName:
                                level.phase.initV = value;
                                //AEC.Log.OutputLine("AEC phase = {0}", level.phase.v);
                                break;
                        }
                    }
                }
            }
        }

        private static XmlElement CreateLevelNode(XmlDocument xmldoc, Level level, string name)
        {
            XmlElement ele = xmldoc.CreateElement(levelNode);
            ele.SetAttribute(nameAttr, name);

            ele.AppendChild(CreateParamNode(xmldoc, levelName, level.level));
            ele.AppendChild(CreateParamNode(xmldoc, phaseName, level.phase));
            
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
                                case thresholdName:
                                    limiter.threshold.initV = value;
                                    break;

                                case attackName:
                                    limiter.attack.initV = value;
                                    break;

                                case releaseName:
                                    limiter.release.initV = value;
                                    break;

                                case clipThresholdName:
                                    limiter.clipThreshold.initV = value;
                                    break;

                                case clipReleaseName:
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

            ele.AppendChild(CreateParamNode(xmldoc, thresholdName, limiter.threshold));
            ele.AppendChild(CreateParamNode(xmldoc, attackName, limiter.attack));
            ele.AppendChild(CreateParamNode(xmldoc, releaseName, limiter.release));
            ele.AppendChild(CreateParamNode(xmldoc, clipThresholdName, limiter.clipThreshold));
            ele.AppendChild(CreateParamNode(xmldoc, clipReleaseName, limiter.clipRelease));

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
                                case thresholdName:
                                    compressor.threshold.initV = value;
                                    break;

                                case attackName:
                                    compressor.attack.initV = value;
                                    break;

                                case releaseName:
                                    compressor.release.initV = value;
                                    break;

                                case ratioName:
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

            ele.AppendChild(CreateParamNode(xmldoc, thresholdName, compressor.threshold));
            ele.AppendChild(CreateParamNode(xmldoc, attackName, compressor.attack));
            ele.AppendChild(CreateParamNode(xmldoc, releaseName, compressor.release));
            ele.AppendChild(CreateParamNode(xmldoc, ratioName, compressor.ratio));

            //ele.SetAttribute("threshold", compressor.threshold.v.ToString());
            //ele.SetAttribute("attack", compressor.attack.v.ToString());
            //ele.SetAttribute("release", compressor.release.v.ToString());
            //ele.SetAttribute("ratio", compressor.ratio.v.ToString());

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
                    else if (name == qName)
                    {
                        if (filter.hasQ)
                        {
                            filter.qIndex = value;
                        }
                    }
                }
            }
        }

        private static XmlElement CreateFilterNode(XmlDocument xmldoc, XPFilterBase filter, string name = "")
        {
            XmlElement ele = xmldoc.CreateElement(filterNode);

            if (!string.IsNullOrEmpty(name))
            {
                ele.SetAttribute(nameAttr, name);
            }

            ele.AppendChild(CreateParamNode(xmldoc, freqName, filter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, filterTypeName, filter.typeIndex));

            if (filter.hasQ)
            {
                ele.AppendChild(CreateParamNode(xmldoc, qName, filter.qIndex));
            }

            return ele;
        }

        private static void ParseBassExciterNode(XmlElement dataEle, BassExciter bassExciter)
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

                    if (name == exceiterOnName)
                    {
                        bassExciter.on.initV = value;
                    }
                    else if (name == exceiterFreqName)
                    {
                        bassExciter.xoverFreq.initV = value;
                    }
                    else if (name == exceiterFBGainName)
                    {
                        bassExciter.fbGain.initV = value;
                    }
                    else if (name == exceiterBoostName)
                    {
                        bassExciter.boostLevel.initV = value;
                    }
                }
            }
        }

        private static XmlElement CreateBassExciterNode(XmlDocument xmldoc, BassExciter bassExciter, string name = "")
        {
            XmlElement ele = xmldoc.CreateElement(bassExciterNode);

            if (!string.IsNullOrEmpty(name))
            {
                ele.SetAttribute(nameAttr, name);
            }

            ele.AppendChild(CreateParamNode(xmldoc, exceiterOnName, bassExciter.on));
            ele.AppendChild(CreateParamNode(xmldoc, exceiterFreqName, bassExciter.xoverFreq));
            ele.AppendChild(CreateParamNode(xmldoc, exceiterFBGainName, bassExciter.fbGain));
            ele.AppendChild(CreateParamNode(xmldoc, exceiterBoostName, bassExciter.boostLevel));

            return ele;
        }

        private static void ParseExciterNode(XmlElement dataEle, Exciter exciter)
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

                    if (name == exceiterOnName)
                    {
                        exciter.on.initV = value;
                    }
                    else if (name == exceiterFreqName)
                    {
                        exciter.freq.initV = value;
                    }
                    else if(name == exceiterBandWidthName)
                    {
                        exciter.bandWidth.initV = value;
                    }
                    else if (name == exceiterFBGainName)
                    {
                        exciter.fbGain.initV = value;
                    }
                    else if (name == exceiterBoostName)
                    {
                        exciter.boostLevel.initV = value;
                    }
                }
            }
        }

        private static XmlElement CreateExciterNode(XmlDocument xmldoc, Exciter exciter, string name = "")
        {
            XmlElement ele = xmldoc.CreateElement(exciterNode);

            if (!string.IsNullOrEmpty(name))
            {
                ele.SetAttribute(nameAttr, name);
            }

            ele.AppendChild(CreateParamNode(xmldoc, exceiterOnName, exciter.on));
            ele.AppendChild(CreateParamNode(xmldoc, exceiterFreqName, exciter.freq));
            ele.AppendChild(CreateParamNode(xmldoc, exceiterBandWidthName, exciter.bandWidth));
            ele.AppendChild(CreateParamNode(xmldoc, exceiterFBGainName, exciter.fbGain));
            ele.AppendChild(CreateParamNode(xmldoc, exceiterBoostName, exciter.boostLevel));

            return ele;
        }

        private static XmlElement CreateEQGroupElement(XmlDocument xmldoc, EQGroup eqGroup, string name = "")
        {
            XmlElement ele = xmldoc.CreateElement(eqGroupNode);
            if (!string.IsNullOrEmpty(name))
            {
                ele.SetAttribute(nameAttr, name);
            }

            ele.AppendChild(CreateParamNode(xmldoc, currEQIndexName, eqGroup.currEQIndex));
            ele.AppendChild(CreateParamNode(xmldoc, eqBypassName, eqGroup.eqPassIndex));

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
                            case currEQIndexName:
                                eqGroup.currEQIndex = value;
                                break;

                            case eqBypassName:
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
                                case typeName:
                                    eqGroup.eqList[index].typeIndex = value;
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
            ele.SetAttribute(indexAttr, eq.index.ToString());
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
                                case workModeName:
                                    autoWorkMode.workModeInit = value;
                                    break;

                                case workSelectName:
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

            ele.AppendChild(CreateParamNode(xmldoc, workModeName, autoWorkMode.wm));
            ele.AppendChild(CreateParamNode(xmldoc, workSelectName, autoWorkMode.ws));

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

        private const string typeName = "type";
        private const string freqName = "freq";
        private const string gainName = "gain";
        private const string qName = "q";
        private const string sName = "s";

        private const string currEQIndexName = "currEQIndex";
        private const string eqBypassName = "eqBypass";

        private const string autoWrokModeNode = "autoWrokMode";
        private const string filterNode = "filter";

        private const string bassExciterNode = "bassExciter";
        private const string exciterNode = "exciter";
        private const string exceiterOnName = "on";
        private const string exceiterFreqName = "freq";
        private const string exceiterBandWidthName = "bandWidth";
        private const string exceiterFBGainName = "fbGain";
        private const string exceiterBoostName = "boost";

        private const string compressorNode = "compressor";
        private const string thresholdName = "threshold";
        private const string attackName = "attack";
        private const string releaseName = "release";
        private const string ratioName = "ratio";

        private const string limiterNode = "limiter";
        private const string clipThresholdName = "clipThreshold";
        private const string clipReleaseName = "clipRelease";

        private const string mixerNode = "mixer";
        private const string levelNode = "level";
        private const string levelName = "level";
        private const string phaseName = "phase";
        private const string outputMixerNode = "outputMixer";
        private const string filterTypeName = "type";
        private const string hpfTypeName = "hpfType";
        private const string lpfTypeName = "lpfType";
        private const string singEQGroupName = "singEQGroup";
        private const string discoEQGroupName = "discoEQGroup";
        private const string micNoiseGateName = "noiseGate";
        private const string hPFFreqName = "hPFFreq";
        private const string hpfFreqName = "hpfFreq";
        private const string fbeName = "fbe";
        private const string echoMixerName = "echoMixer";
        private const string directMixerName = "directMixer";
        private const string reverbMixerName = "reverbMixer";
        private const string lPFFreqName = "lPFFreq";
        private const string lpfFreqName = "lpfFreq";
        private const string lPFQAttr = "lPFQ";
        private const string preDelayName = "preDelay";
        private const string delayTimeName = "delayTime";
        private const string repeatName = "repeat";
        private const string rChDelayTimeName = "rChDelayTime";
        private const string rChPreDelayName = "rChPreDelay";
        //private const string reverbLevelAttr = "reverbLevel";
        private const string bassDampAttr = "bassDamp";
        private const string trebleDampAttr = "trebleDamp";

        private const string singName = "sing";
        private const string discoName = "disco";

        private const string musicLevelName = "musicLevel";
        private const string micLevelName = "micLevel";
        private const string reverbLevelName = "reverbLevel";

        private const string musicMixerName = "musicMixer";
        private const string micMixerName = "micMixer";

        private const string workModeName = "workMode";
        private const string workSelectName = "workSelect";
        private const string singHpfName = "singHpf";
        private const string discoHpfName = "discoHpf";

        private const string micExciter1SelectName = "micExciter1Select";
        private const string micExciter1Name = "micExciter1";
        private const string micExciter2Name = "micExciter2";
        private const string micHpfFreqName = "micHpfFreq";
        private const string micLpfName = "micLpf";

        private const string hpfName = "hpf";
        private const string lpfName = "lpf";

        private const string outputChannelNode = "outputChannel";
        private const string leftChannel = "leftChannel";
        private const string rightChannel = "rightChannel";
        private const string monoChannel = "monoChannel";

        private const string leftChDelayName = "leftChDelay";
        private const string rightChDelayName = "rightChDelay";
        private const string leftChMuteName = "leftChMute";
        private const string rightChMuteName = "rightChMute";
        private const string delayName = "delay";
        private const string muteName = "mute";

        
        private const string musicStartVolumeName = "musicStartVolume";
        private const string musicMaxVolumeName = "musicMaxVolume";
        private const string micStartVolumeName = "micStartVolume";
        private const string micMaxVolumeName = "micMaxVolume";
        private const string effectStartVolumeName = "effectStartVolume";
        private const string effectMaxVolumeName = "effectMaxVolume";
        private const string clarityName = "clarity";
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
        private const string dataIsPrivateName = "dataIsPrivate";
    }
}
