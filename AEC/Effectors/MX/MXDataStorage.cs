using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using AEC.Core;
using AEC.Common;
using AEC.Effectors.MX;
using AEC.Effectors.MX.Modules;
using AEC.Misc;

namespace AEC.Effectors.MX
{
    public class MXDataStorage
    {
        private const string rootNodeName = "AEC";
        private const string modelAttr = "model";
        private const string versionAttr = "version";
        private const string version = "2.0";

        private const string musicModule = "music";
        private const string micModule = "mic";
        private const string musicDeqModule = "musicDeq";
        private const string micDeqModule = "micDeq";
        private const string echoModule = "echo";
        private const string reverbModule = "reverb";
        private const string mainOutputModule = "mainOutput";
        private const string aux1OutputModule = "aux1Output";
        private const string aux2OutputModule = "aux2Output";
        private const string surOutputModule = "surOutput";
        private const string systemModule = "system";

        private delegate void ParseModuleDataCallback(XmlElement ele, MXEffectorModel effector);

        public static Localization localization = Config.GetInstance().localization;

        public static void SaveToFile(MXEffectorModel effector, string model, string filePath, bool isContainAllSystemData)
        {
            Debug.Assert(effector != null);

            XmlDocument xmldoc = CreateEffectorXmlDoc(effector, model, isContainAllSystemData);

            xmldoc.Save(filePath);
        }

        public static MXEffectorModel LoadFromFile(string filePath, string model)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            MXEffectorModel effector = LoadFromStream(fs, model, false);
            fs.Close();
            return effector;

            //if (!File.Exists(filePath))
            //{
            //    return null;
            //}

            //try
            //{
            //    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //    MXEffectorModel effector = LoadFromStream(fs, model, false);
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

        public static MXEffectorModel Buffer2Effector(byte[] buffer, string model, bool isContainAllSystemData = false)
        {
            MemoryStream ms = new MemoryStream(buffer);
            return LoadFromStream(ms, model, isContainAllSystemData);
        }

        public static byte[] Effector2Buffer(MXEffectorModel effector, string model, bool isContainAllSystemData = false)
        {
            XmlDocument xmldoc = CreateEffectorXmlDoc(effector, model, isContainAllSystemData);

            MemoryStream ms = new MemoryStream();
            xmldoc.Save(ms);
            return ms.ToArray();
        }

        private static MXEffectorModel LoadFromStream(Stream stream, string model, bool isContainAllSystemData)
        {
            Debug.Assert(stream != null);

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(stream);

            Dictionary<string, ParseModuleDataCallback> moduleParseMap = new Dictionary<string, ParseModuleDataCallback>()
                {
                    {musicModule, ParseMusicModule},
                    {micModule, ParseMicModule},
                    {musicDeqModule, ParseMusicDeqModule},
                    {micDeqModule, ParseMicDeqModule},
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
                    rootNode.GetAttribute(modelAttr) == model &&
                    rootNode.HasAttribute(versionAttr) &&
                    rootNode.GetAttribute(versionAttr) == version)
                {

                    MXEffectorModel effector = MXEffectorModel.GetTempModel();

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

        private static XmlDocument CreateEffectorXmlDoc(MXEffectorModel effector, string model, bool isContainAllSystemData)
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
            rootEle.AppendChild(CreateMusicDeqNode(xmldoc, effector.musicDeq));
            rootEle.AppendChild(CreateMicDeqNode(xmldoc, effector.micDeq));
            rootEle.AppendChild(CreateEchoNode(xmldoc, effector.echo));
            rootEle.AppendChild(CreateReverbNode(xmldoc, effector.reverb));
            rootEle.AppendChild(CreateMainOutputNode(xmldoc, effector.mainOutput));
            rootEle.AppendChild(CreateAux1OutputNode(xmldoc, effector.aux1Output));
            rootEle.AppendChild(CreateAux2OutputNode(xmldoc, effector.aux2Output));
            rootEle.AppendChild(CreateSurOutputNode(xmldoc, effector.surOutput));
            rootEle.AppendChild(CreateSystemNode(xmldoc, effector.system, isContainAllSystemData));
            
            return xmldoc;
        }

        private const string workModeNode = "workMode";
        private const string workSelectNode = "workSelect";
        private const string singHPFFreqNode = "singHPFFreq";
        private const string discoHPFFreqNode = "discoHPFFreq";

        private static void ParseMusicModule(XmlElement moduleNode, MXEffectorModel effector)
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
                    music.wm.initV = value;
                    break;

                case workSelectNode:
                    music.ws.initV = value;
                    break;
            }
        }

        private static XmlElement CreateMusicNode(XmlDocument xmldoc, MusicModule music)
        {
            XmlElement ele = CreateModuleNode(xmldoc, musicModule);

            ele.AppendChild(CreateParamNode(xmldoc, workModeNode, music.wm));
            ele.AppendChild(CreateParamNode(xmldoc, workSelectNode, music.ws));
            ele.AppendChild(CreateEQGroupElement(xmldoc, music.singEQGroup, singEQGroupAttr));
            ele.AppendChild(CreateEQGroupElement(xmldoc, music.discoEQGroup, discoEQGroupAttr));
            return ele;
        }

        private static XmlElement CreateMicNode(XmlDocument xmldoc, MicModule mic)
        {
            XmlElement ele = CreateModuleNode(xmldoc, micModule);
            ele.AppendChild(CreateParamNode(xmldoc, hPFFreqAttr, mic.hpf.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, fbeAttr, mic.fbe));
            //ele.AppendChild(CreateParamNode(xmldoc, noiseGateAttr, mic.noiseGate));
            ele.AppendChild(CreateEQGroupElement(xmldoc, mic.eqGroup));
            return ele;
        }

        private static void ParseMicModule(XmlElement moduleNode, MXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            MicModule mic = effector.mic;
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseMicParam(ele, mic);
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
                case hPFFreqAttr:
                    mic.hpf.freqIndex = value;
                    break;

                case fbeAttr:
                    mic.fbe.initV = value;
                    break;

                //case noiseGateAttr:
                //    mic.noiseGate.initV = value;
                //    break;
            }
        }

        private static void ParseMusicDeqModule(XmlElement moduleNode, MXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseDeqModule(moduleNode, effector.musicDeq);
        }

        private static void ParseMicDeqModule(XmlElement moduleNode, MXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseDeqModule(moduleNode, effector.micDeq);
        }

        private static void ParseDeqModule(XmlElement moduleNode, DeqModule deqModule)
        {
            Debug.Assert(moduleNode != null && deqModule != null);

            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if(ele.Name == deqGroupNode)
                {
                    ParseDEQGroup(ele, deqModule.deqGroup);
                }
            }
        }

        private static XmlElement CreateMusicDeqNode(XmlDocument xmldoc, DeqModule deq)
        {
            return CreateDeqNode(xmldoc, deq, musicDeqModule);
        }

        private static XmlElement CreateMicDeqNode(XmlDocument xmldoc, DeqModule deq)
        {
            return CreateDeqNode(xmldoc, deq, micDeqModule);
        }

        private static XmlElement CreateDeqNode(XmlDocument xmldoc, DeqModule deq, string deqModuleName)
        {
            XmlElement ele = CreateModuleNode(xmldoc, deqModuleName);
            ele.AppendChild(CreateDEQGroupElement(xmldoc, deq._deqGroup));
            return ele;
        }

        private static void ParseEchoModule(XmlElement moduleNode, MXEffectorModel effector)
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
                    echo.hpf.freqIndex = value;
                    break;

                case lPFFreqAttr:
                    echo.lpf.freqIndex = value;
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
            ele.AppendChild(CreateParamNode(xmldoc, hPFFreqAttr, echo.hpf.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, lPFFreqAttr, echo.lpf.freqIndex));       
            ele.AppendChild(CreateParamNode(xmldoc, preDelayAttr, echo.preDelay));
            ele.AppendChild(CreateParamNode(xmldoc, delayTimeAttr, echo.delayTime));
            ele.AppendChild(CreateParamNode(xmldoc, repeatAttr, echo.repeat));
            ele.AppendChild(CreateParamNode(xmldoc, rChDelayTimeAttr, echo.rChDelayTime));
            ele.AppendChild(CreateParamNode(xmldoc, rChPreDelayAttr, echo.rChPreDelay));

            ele.AppendChild(CreateEQGroupElement(xmldoc, echo.eqGroup));

            return ele;
        }

        private static void ParseReverbModule(XmlElement moduleNode, MXEffectorModel effector)
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
                    if (groupName == reverbMicLevelAttr)
                    {
                        ParseLevelElement(ele, reverb.reverbLevel);
                    }
                    else if (groupName == directLevelAttr)
                    {
                        ParseLevelElement(ele, reverb.directLevel);
                    }
                    else if (groupName == reverbMusicLevelAttr)
                    {
                        ParseLevelElement(ele, reverb.reverbMusicLevel);
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

                case micHpfFreqAttr:
                    reverb.micHpfFreq.initV = value;
                    break;

                case micLpfFreqAttr:
                    reverb.micLpfFreq.initV = value;
                    break;

                case musicHpfFreqAttr:
                    reverb.musicHpfFreq.initV = value;
                    break;

                case musicLpfFreqAttr:
                    reverb.musicLpfFreq.initV = value;
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

            ele.AppendChild(CreateLevelNode(xmldoc, reverbMicLevelAttr, reverb.reverbLevel));
            ele.AppendChild(CreateLevelNode(xmldoc, directLevelAttr, reverb.directLevel));
            ele.AppendChild(CreateParamNode(xmldoc, delayTimeAttr, reverb.delayTime));
            ele.AppendChild(CreateParamNode(xmldoc, preDelayAttr, reverb.preDelay));
            ele.AppendChild(CreateParamNode(xmldoc, micHpfFreqAttr, reverb.micHpfFreq));
            ele.AppendChild(CreateParamNode(xmldoc, micLpfFreqAttr, reverb.micLpfFreq));
            ele.AppendChild(CreateParamNode(xmldoc, musicHpfFreqAttr, reverb.musicHpfFreq));
            ele.AppendChild(CreateParamNode(xmldoc, musicLpfFreqAttr, reverb.musicLpfFreq));
            ele.AppendChild(CreateLevelNode(xmldoc, reverbMusicLevelAttr, reverb.reverbMusicLevel));
            ele.AppendChild(CreateParamNode(xmldoc, bassDampAttr, reverb.bassDamp));
            ele.AppendChild(CreateParamNode(xmldoc, trebleDampAttr, reverb.trebleDamp));

            return ele;
        }

        private static void ParseMainOutputModule(XmlElement moduleNode, MXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseOutputModule(moduleNode, effector.mainOutput);
        }

        private static void ParseAux1OutputModule(XmlElement moduleNode, MXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseAuxModule(moduleNode, effector.aux1Output);
        }

        private static void ParseAux2OutputModule(XmlElement moduleNode, MXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseAuxModule(moduleNode, effector.aux2Output);
        }

        private static void ParseSurOutputModule(XmlElement moduleNode, MXEffectorModel effector)
        {
            Debug.Assert(moduleNode != null && effector != null);
            ParseOutputModule(moduleNode, effector.surOutput);
        }

        private static void ParseOutputModule(XmlElement moduleNode, OutputModule output)
        {
            Debug.Assert(moduleNode != null && output != null);
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseOutputParam(ele, output);
                }
                else if (ele.Name == eqGroupNode)
                {
                    string groupName = ele.GetAttribute(nameAttr);
                    if (groupName == leftChEQGroupAttr)
                    {
                        ParseEQGroup(ele, output.leftEQGroup);
                    }
                    else if (groupName == rightChEQGroupAttr)
                    {
                        ParseEQGroup(ele, output.rightEQGroup);
                    }
                }
                else
                {
                    ParseOutputBaseModule(ele, output);
                }
            }

            output.ConstraintParam();
        }

        private static void ParseAuxModule(XmlElement moduleNode, AuxOutputModule output)
        {
            Debug.Assert(moduleNode != null && output != null);
            foreach (XmlElement ele in moduleNode.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    ParseAuxOutputParam(ele, output);
                }
                else if (ele.Name == eqGroupNode)
                {
                    ParseEQGroup(ele, output.eqGroup);
                }
                else
                {
                    ParseOutputBaseModule(ele, output);
                }
            }
        }

        private static void ParseOutputParam(XmlElement dataEle, OutputModule output)
        {
            string name = dataEle.GetAttribute(nameAttr);
            short value = ParseText2Short(dataEle.InnerText);
            if (value < 0)
            {
                return;
            }

            switch (name)
            {
                case leftChDelayAttr:
                    output.leftChDelay.initV = value;
                    break;

                case rightChDelayAttr:
                    output.rightChDelay.initV = value;
                    break;

                case leftChMuteAttr:
                    output.leftChMute.initV = value;
                    break;

                case rightChMuteAttr:
                    output.rightChMute.initV = value;
                    break;

                case eqControlModeAttr:
                    output.eqControlMode.initV = value;
                    break;
            }
        }

        private static void ParseAuxOutputParam(XmlElement dataEle, AuxOutputModule output)
        {
            string name = dataEle.GetAttribute(nameAttr);
            short value = ParseText2Short(dataEle.InnerText);
            if (value < 0)
            {
                return;
            }

            switch (name)
            {
                case delayAttr:
                    output.delay.initV = value;
                    break;

                case muteAttr:
                    output.mute.initV = value;
                    break;
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
                ParseOutputHpfParam(ele, output);
            }
            else if (ele.Name == lpfNode)
            {
                ParseOutputLpfParam(ele, output);
            }
            else if (ele.Name == compressorNode)
            {
                ParseCompressorElement(ele, output.compressor);
            }
        }

        private static void ParseOutputHpfParam(XmlElement dataEle, OutputBaseModule outputBase)
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

                    if (name == filterModeAttr)
                    {
                        outputBase.hpf.typeIndex = value;
                    }
                    else if (name == freqAttr)
                    {
                        outputBase.hpf.freqIndex = value;
                    }
                    else if (name == qAttr)
                    {
                        outputBase.hpf.qIndex = value;
                    }
                }
            }
        }

        private static void ParseOutputLpfParam(XmlElement dataEle, OutputBaseModule outputBase)
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

                    if (name == filterModeAttr)
                    {
                        outputBase.lpf.typeIndex = value;
                    }
                    else if (name == freqAttr)
                    {
                        outputBase.lpf.freqIndex = value;
                    }
                    else if (name == qAttr)
                    {
                        outputBase.lpf.qIndex = value;
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

        private static XmlElement CreateMainOutputNode(XmlDocument xmldoc, OutputModule output)
        {
            return CreateOutputNode(xmldoc, output, mainOutputModule);
        }

        private static XmlElement CreateAux1OutputNode(XmlDocument xmldoc, AuxOutputModule output)
        {
            return CreateAuxOutputNode(xmldoc, output, aux1OutputModule);
        }

        private static XmlElement CreateAux2OutputNode(XmlDocument xmldoc, AuxOutputModule output)
        {
            return CreateAuxOutputNode(xmldoc, output, aux2OutputModule);
        }

        private static XmlElement CreateSurOutputNode(XmlDocument xmldoc, OutputModule output)
        {
            return CreateOutputNode(xmldoc, output, surOutputModule);
        }

        private static XmlElement CreateOutputNode(XmlDocument xmldoc, OutputModule output, string name)
        {
            XmlElement ele = CreateOutputBaseNode(xmldoc, output, name);
            AppendOutput(xmldoc, ele, output);
            ele.AppendChild(CreateEQGroupElement(xmldoc, output.leftEQGroup, leftChEQGroupAttr));
            ele.AppendChild(CreateEQGroupElement(xmldoc, output.rightEQGroup, rightChEQGroupAttr));

            return ele;
        }

        private static XmlElement CreateAuxOutputNode(XmlDocument xmldoc, AuxOutputModule output, string name)
        {
            XmlElement ele = CreateOutputBaseNode(xmldoc, output, name);
            AppendAux(xmldoc, ele, output);
            ele.AppendChild(CreateEQGroupElement(xmldoc, output.eqGroup));

            return ele;
        }

        private static void AppendOutput(XmlDocument xmldoc, XmlElement outputEle, OutputModule output)
        {
            Debug.Assert(outputEle != null);
            outputEle.AppendChild(CreateParamNode(xmldoc, leftChDelayAttr, output.leftChDelay));
            outputEle.AppendChild(CreateParamNode(xmldoc, rightChDelayAttr, output.rightChDelay));
            outputEle.AppendChild(CreateParamNode(xmldoc, leftChMuteAttr, output.leftChMute));
            outputEle.AppendChild(CreateParamNode(xmldoc, rightChMuteAttr, output.rightChMute));
            outputEle.AppendChild(CreateParamNode(xmldoc, eqControlModeAttr, output.eqControlMode));
        }

        private static void AppendAux(XmlDocument xmldoc, XmlElement outputEle, AuxOutputModule output)
        {
            Debug.Assert(outputEle != null);
            outputEle.AppendChild(CreateParamNode(xmldoc, delayAttr, output.delay));
            outputEle.AppendChild(CreateParamNode(xmldoc, muteAttr, output.mute));
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

            ele.AppendChild(CreateCompressorNode(xmldoc, output.compressor));

            return ele;
        }

        private static void ParseSystemParam(XmlElement dataEle, MXEffectorModel effector, bool isContainAllSystemData)
        {
            SystemModule system = effector.system as SystemModule;
            
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
                    //{musicMuteAttr, system.musicMute},
                    //{micMuteAttr, system.micMute},
                    //{effectMuteAttr, system.effectMute},
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

                system.UpdateCurrVolume();
            }
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
                //ele.AppendChild(CreateParamNode(xmldoc, musicInputPortAttr, system.musicInputPort));
                ele.AppendChild(CreateParamNode(xmldoc, isEnableRemoteControlAttr, system.isEnableRemote));
                ele.AppendChild(CreateParamNode(xmldoc, musicVolumeAttr, system.musicVolume));
                ele.AppendChild(CreateParamNode(xmldoc, micVolumeAttr, system.micVolume));
                ele.AppendChild(CreateParamNode(xmldoc, effectVolumeAttr, system.effectVolume));
                //ele.AppendChild(CreateParamNode(xmldoc, musicMuteAttr, system.musicMute));
                //ele.AppendChild(CreateParamNode(xmldoc, micMuteAttr, system.micMute));
                //ele.AppendChild(CreateParamNode(xmldoc, effectMuteAttr, system.effectMute));
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
        private static XmlElement CreateOutputFilterNode(XmlDocument xmldoc, XPFilterBase filter, string name)
        {
            XmlElement ele = xmldoc.CreateElement(name);
            ele.AppendChild(CreateParamNode(xmldoc, filterModeAttr, filter.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, freqAttr, filter.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, qAttr, filter.qIndex));

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

        private static void ParseCompressorElement(XmlElement limiterEle, MXCompressor compressor)
        {
            if (limiterEle.Name == compressorNode)
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

                                case modeAttr:
                                    compressor.mode.initV = value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static XmlElement CreateCompressorNode(XmlDocument xmldoc, MXCompressor compressor)
        {
            XmlElement ele = xmldoc.CreateElement(compressorNode);

            ele.AppendChild(CreateParamNode(xmldoc, thresholdAttr, compressor.threshold));
            ele.AppendChild(CreateParamNode(xmldoc, attackAttr, compressor.attack));
            ele.AppendChild(CreateParamNode(xmldoc, releaseAttr, compressor.release));
            ele.AppendChild(CreateParamNode(xmldoc, ratioAttr, compressor.ratio));
            ele.AppendChild(CreateParamNode(xmldoc, modeAttr, compressor.mode));

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

        private static XmlElement CreateDEQGroupElement(XmlDocument xmldoc, DEQGroup deqGroup)
        {
            XmlElement ele = xmldoc.CreateElement(deqGroupNode);

            ele.AppendChild(CreateParamNode(xmldoc, currEQIndexAttr, deqGroup.currEQIndex));

            XmlElement deqListEle = xmldoc.CreateElement(deqListNode);
            foreach (DEQ deq in deqGroup.eqList)
            {
                deqListEle.AppendChild(CreateDEQElement(xmldoc, deq));
            }

            ele.AppendChild(deqListEle);

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

        private static void ParseDEQGroup(XmlElement deqGroupEle, DEQGroup deqGroup)
        {
            foreach (XmlElement ele in deqGroupEle.ChildNodes)
            {
                if (ele.Name == paramNode)
                {
                    string name = ele.GetAttribute(nameAttr);
                    short value = ParseText2Short(ele.InnerText);
                    if (value >= 0 && name == currEQIndexAttr)
                    {
                        deqGroup.currEQIndex = value;
                    }
                }
                else if (ele.Name == deqListNode)
                {
                    foreach (XmlElement deqEle in ele.ChildNodes)
                    {
                        ParseDEQElement(deqEle, deqGroup);
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

        private static void ParseDEQElement(XmlElement deqEle, DEQGroup deqGroup)
        {
            if (deqEle.Name == deqNode)
            {
                short index = ParseText2Short(deqEle.GetAttribute(indexAttr));
                if (index < 0 || index >= deqGroup.eqBandCount)
                {
                    return;
                }

                DEQ currDeq = deqGroup.deqList[index];

                foreach (XmlElement ele in deqEle.ChildNodes)
                {
                    if (ele.Name == paramNode)
                    {
                        string name = ele.GetAttribute(nameAttr);
                        short value = ParseText2Short(ele.InnerText);
                        if (value >= 0)
                        {
                            switch (name)
                            {
                                case eqIsPassAttr:
                                    currDeq.deqIsPass.initV = value;
                                    break;

                                case typeAttr:
                                    currDeq.typeIndex = value;
                                    break;

                                case freqAttr:
                                    currDeq.freqIndex = value;
                                    break;

                                case gainAttr:
                                    currDeq.gainIndex = value;
                                    break;

                                case qAttr:
                                    currDeq.qIndex_ = value;
                                    break;

                                case sAttr:
                                    currDeq.sIndex_ = value;
                                    break;

                                case deqControlModeAttr:
                                    currDeq.deqCM.initV = value;
                                    break;

                                case attackAttr:
                                    currDeq.attack.initV = value;
                                    break;

                                case releaseAttr:
                                    currDeq.release.initV = value;
                                    break;

                                case thresholdAttr:
                                    currDeq.threshold.initV = value;
                                    break;

                                case ratioAttr:
                                    currDeq.ratio.initV = value;
                                    break;

                                case dynamicModeAttr:
                                    currDeq.deqDM.initV = value;
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

        private static XmlElement CreateDEQElement(XmlDocument xmldoc, DEQ deq)
        {
            XmlElement ele = xmldoc.CreateElement(deqNode);
            ele.SetAttribute(indexAttr, deq.index.ToString());
            ele.AppendChild(CreateParamNode(xmldoc, eqIsPassAttr, deq.deqIsPass));
            ele.AppendChild(CreateParamNode(xmldoc, typeAttr, deq.typeIndex));
            ele.AppendChild(CreateParamNode(xmldoc, freqAttr, deq.freqIndex));
            ele.AppendChild(CreateParamNode(xmldoc, gainAttr, deq.gainIndex));
            ele.AppendChild(CreateParamNode(xmldoc, qAttr, deq.qIndex_));
            ele.AppendChild(CreateParamNode(xmldoc, sAttr, deq.sIndex_));
            ele.AppendChild(CreateParamNode(xmldoc, deqControlModeAttr, deq.deqCM));
            ele.AppendChild(CreateParamNode(xmldoc, attackAttr, deq.attack));
            ele.AppendChild(CreateParamNode(xmldoc, releaseAttr, deq.release));
            ele.AppendChild(CreateParamNode(xmldoc, thresholdAttr, deq.threshold));
            ele.AppendChild(CreateParamNode(xmldoc, ratioAttr, deq.ratio));
            ele.AppendChild(CreateParamNode(xmldoc, dynamicModeAttr, deq.deqDM));

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

        //private static XmlElement CreateParamNode(XmlDocument xmldoc, string name, bool param)
        //{
        //    XmlElement ele = xmldoc.CreateElement(paramNode);
        //    ele.SetAttribute(nameAttr, name);
        //    ele.InnerText = DataConvert.Bool2Short(param);
        //    return ele;
        //}

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
        private const string deqGroupNode = "deqGroup";
        private const string deqListNode = "deqList";
        private const string deqNode = "deq";

        private const string typeAttr = "type";
        private const string freqAttr = "freq";
        private const string gainAttr = "gain";
        private const string qAttr = "q";
        private const string sAttr = "s";

        private const string deqControlModeAttr = "cm";
        private const string dynamicModeAttr = "dm";

        private const string currEQIndexAttr = "currEQIndex";
        private const string eqIsPassAttr = "eqIsPass";

        private const string compressorNode = "compressor";
        private const string thresholdAttr = "threshold";
        private const string attackAttr = "attack";
        private const string releaseAttr = "release";
        private const string ratioAttr = "ratio";
        private const string modeAttr = "mode";

        //private const string limiterNode = "limiter";
        //private const string clipThresholdNode = "clipThreshold";
        //private const string clipReleaseNode = "clipRelease";

        private const string levelNode = "level";
        private const string levelAttr = "level";
        private const string phaseAttr = "phase";
        private const string outputLevelNode = "outputLevel";
        private const string filterModeAttr = "mode";
        private const string singEQGroupAttr = "singEQGroup";
        private const string discoEQGroupAttr = "discoEQGroup";
        private const string hPFFreqAttr = "hPFFreq";
        private const string fbeAttr = "fbe";
        private const string noiseGateAttr = "noiseGate";
        private const string echoLevelAttr = "echoLevel";
        private const string reverbLevelAttr = "reverbLevel";
        private const string directLevelAttr = "directLevel";
        private const string lPFFreqAttr = "lPFFreq";
        //private const string lPFQAttr = "lPFQ";
        private const string preDelayAttr = "preDelay";
        private const string delayTimeAttr = "delayTime";
        private const string repeatAttr = "repeat";
        private const string rChDelayTimeAttr = "rChDelayTime";
        private const string rChPreDelayAttr = "rChPreDelay";
        private const string reverbMicLevelAttr = "reverbMicLevel";
        private const string reverbMusicLevelAttr = "reverbMusicLevel";
        private const string micHpfFreqAttr = "micHpfFreq";
        private const string micLpfFreqAttr = "micLpfFreq";
        private const string musicHpfFreqAttr = "musicHpfFreq";
        private const string musicLpfFreqAttr = "musicLpfFreq";
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

        private const string leftChDelayAttr = "leftChDelay";
        private const string rightChDelayAttr = "rightChDelay";
        private const string leftChMuteAttr = "leftChMute";
        private const string rightChMuteAttr = "rightChMute";
        private const string eqControlModeAttr = "eqControlMode";
        private const string delayAttr = "delay";
        private const string muteAttr = "mute";
        private const string leftChEQGroupAttr = "leftChEQGroup";
        private const string rightChEQGroupAttr = "rightChEQGroup";

        private const string musicStartVolumeAttr = "musicStartVolume";
        private const string musicMaxVolumeAttr = "musicMaxVolume";
        private const string micStartVolumeAttr = "micStartVolume";
        private const string micMaxVolumeAttr = "micMaxVolume";
        private const string effectStartVolumeAttr = "effectStartVolume";
        private const string effectMaxVolumeAttr = "effectMaxVolume";       
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
        //private const string deviceModelAttr = "deviceModel";
        private const string dataIsPrivateAttr = "dataIsPrivate";
    }
}
