using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AEC.RTA.Core
{
    public enum RefTraceGroupNum
    {
        GroupA = 0,
        GroupB,
        GroupC,
        GroupD,
        GroupE,
    }

    [Serializable()]
    public class RefTraceInfoBase
    {
        public int sample_freq = AudioRTAParam.SampleRate48k;
        public int sample_accuracy = AudioRTAParam.BitsPerSample16;
        public float delay_time = 0.0f;
        public int fft_size = AudioRTAParam.FFTSize32K;
        public FFTSize rtaFFTSize = FFTSize.fftSize32k;
        public WindowFunction window = WindowFunction.HannWindow;
        public string comment = string.Empty;

        public void UpdateFromAudioParam(AudioRTAParam param)
        {
            this.sample_freq = param.sampleRate;
            this.sample_accuracy = param.sampleBitDepth;
            this.fft_size = param.fftSize;
            this.rtaFFTSize = param.rtaFftSize;
            this.window = (param.inputSignal == InputSignal.Normal ? param.spWindow : WindowFunction.RectangleWindow);
        }

        public virtual void Reset()
        {
            comment = string.Empty;
        }

        public float fr
        {
            get
            {
                return (float)sample_freq / (float)fft_size;
            }
        }

        public virtual bool isValid
        {
            get
            {
                return false;
            }
        }

        public override string ToString()
        {
            //string info = resource._strDict[lmResource.SampleFreq] + this.sample_freq.ToString() + "Hz";
            //info += NewLine + resource._strDict[lmResource.SampleAccuracy] + this.sample_accuracy + " Bit";
            //info += NewLine + resource._strDict[lmResource.DelayTime] + this.delay_time.ToString("0.00") + " ms";
            //info += NewLine + resource._strDict[lmResource.FFTSize] + AudioParams.FFTSizeToString(rtaFFTSize);
            //info += NewLine + resource._strDict[lmResource.WindowFunction] + Utility.WindowFunctionToString(this.window);

            return string.Empty;
        }

        public string NewLine
        {
            get
            {
                return System.Environment.NewLine;
            }
        }
    }

    [Serializable()]
    public class SpectrumInfo : RefTraceInfoBase
    {
        public AudioChannel chan = AudioChannel.LeftCh;
        public double[] amp = null;

        //[NonSerialized]
        //public double[] mag_dB = null;

        public override bool isValid
        {
            get
            {
                return (amp != null && amp.Length == fft_size / 2);
            }
        }

        public override void Reset()
        {
            base.Reset();
            amp = null;
            //mag_dB = null;
        }

        public override string ToString()
        {
            //if (this.isValid)
            //{
            //    string info = base.ToString();

            //    info += NewLine + resource._strDict[lmResource.Channel] + (this.chan == AudioChannel.LeftCh ? resource._strDict[lmResource.LeftCh] : resource._strDict[lmResource.RightCh]);
            //    info += NewLine + resource._strDict[lmResource.Comment] + this.comment;

            //    return info;
            //}

            return string.Empty;
        }
    }

    [Serializable]
    public class TransferInfo : RefTraceInfoBase
    {
        public bool isFPPO = false;
        //public double[] transfer_mag = null;
        //public double[] transfer_phase = null;
        public FrequencyResponse transfer = null;
        public FPPO.FreqResp fppoFreqResp = null;

        public override bool isValid
        {
            get
            {
                if (!isFPPO)
                {
                    return (transfer != null && transfer.length == fft_size / 2 && transfer.amp != null && transfer.phase != null && transfer.amp.Length == fft_size / 2 && transfer.phase.Length == fft_size / 2);
                }
                else
                {
                    return fppoFreqResp != null && fppoFreqResp.length > 0 &&
                        fppoFreqResp.freqs != null && fppoFreqResp.freqs.Length == fppoFreqResp.length &&
                        fppoFreqResp.magnitudes != null && fppoFreqResp.magnitudes.Length == fppoFreqResp.length &&
                        fppoFreqResp.phases != null && fppoFreqResp.phases.Length == fppoFreqResp.length;
                }
            }
        }

        public override void Reset()
        {
            base.Reset();
            //transfer_mag = null;
            //transfer_phase = null;
            transfer = null;
            fppoFreqResp = null;
            isFPPO = false;
        }

        public override string ToString()
        {
            //if (this.isValid)
            //{
            //    string info = base.ToString();

            //    info += NewLine + resource._strDict[lmResource.Comment] + this.comment;

            //    return info;
            //}

            return string.Empty;
        }
    }

    [Serializable()]
    public class TraceGroupBase
    {
        public const int GroupSize = 4;
        public readonly RefTraceGroupNum groupNum;
        private int _currIndex = 0;
        public RefTraceInfoBase[] traces = null;
        protected bool _isShow = false;

        [NonSerialized]
        public List<int> _keyFreqIndex = null;

        [NonSerialized]
        public bool isNeedtoUpdate = false;

        [NonSerialized]
        public object data_lock = new object();

        public TraceGroupBase(RefTraceGroupNum gn)
        {
            groupNum = gn;
        }

        public RefTraceInfoBase GetCurrTrace()
        {
            Debug.Assert(currIndex >= 0 && currIndex < GroupSize);
            return traces[currIndex];
        }

        public RefTraceInfoBase GetTrace(int index)
        {
            Debug.Assert(index >= 0 && index < GroupSize);
            return traces[index];
        }

        public bool CurrTraceIsValid
        {
            get
            {
                return GetCurrTrace().isValid;
            }
        }

        public bool isShow
        {
            set
            {
                if (value)
                {
                    isNeedtoUpdate = true;
                }

                _isShow = value;
            }
            get
            {
                return _isShow;
            }
        }

        public int currIndex
        {
            get
            {
                return _currIndex;
            }
            set
            {
                Debug.Assert(value >= 0 && value < GroupSize);
                if (_currIndex != value)
                {
                    if (traces[value].isValid)
                    {
                        this.isNeedtoUpdate = true;
                    }
                    else
                    {

                    }

                    _currIndex = value;
                }
            }
        }

        public virtual void ClearGraphicData()
        {

        }
    }

    [Serializable()]
    public class SpectrumGroupTrace : TraceGroupBase
    {
        [NonSerialized]
        public System.Drawing.Point[] pointList = null;
        [NonSerialized]
        public double[] bandLevels = null;
        [NonSerialized]
        public System.Drawing.Rectangle[] octaveBands = null;

        public SpectrumGroupTrace(RefTraceGroupNum gn)
            : base(gn)
        {
            traces = new SpectrumInfo[GroupSize];
            for (int i = 0; i < GroupSize; ++i)
            {
                traces[i] = new SpectrumInfo();
            }
        }

        public void DrawSpectrum(System.Drawing.Graphics g, System.Drawing.Pen pen)
        {
            if (_isShow)
            {
                lock (data_lock)
                {
                    if (pointList != null && pointList.Length > 0)
                    {
                        g.DrawLines(pen, pointList);
                    }
                }
            }
        }

        public void DrawOctaves(System.Drawing.Graphics g, System.Drawing.Brush brush)
        {
            if (_isShow)
            {
                lock (data_lock)
                {
                    if (octaveBands != null && octaveBands.Length > 0)
                    {
                        g.FillRectangles(brush, octaveBands);
                    }
                }
            }
        }

        public override void ClearGraphicData()
        {
            lock (data_lock)
            {
                pointList = null;
                bandLevels = null;
                octaveBands = null;
            }
        }
    }

    [Serializable()]
    public class TransferGroupTrace : TraceGroupBase
    {
        [NonSerialized]
        public System.Drawing.Point[] magPointList = null;
        [NonSerialized]
        public System.Drawing.Point[] phasePointList = null;

        public TransferGroupTrace(RefTraceGroupNum gn)
            : base(gn)
        {
            traces = new TransferInfo[GroupSize];
            for (int i = 0; i < GroupSize; ++i)
            {
                traces[i] = new TransferInfo();
            }
        }

        public void DrawAmp(System.Drawing.Graphics g, System.Drawing.Pen pen)
        {
            if (_isShow)
            {
                lock (data_lock)
                {
                    if (magPointList != null && magPointList.Length > 0)
                    {
                        g.DrawLines(pen, magPointList);
                    }
                }
            }
        }

        public void DrawPhase(System.Drawing.Graphics g, System.Drawing.Pen pen)
        {
            if (_isShow)
            {
                lock (data_lock)
                {
                    if (phasePointList != null && phasePointList.Length > 0)
                    {
                        g.DrawLines(pen, phasePointList);
                    }
                }
            }
        }

        public override void ClearGraphicData()
        {
            lock (data_lock)
            {
                magPointList = null;
                phasePointList = null;
            }
        }
    }

    [Serializable()]
    public class RefTraceBase
    {
        public static readonly RefTraceGroupNum[] all_group = { RefTraceGroupNum.GroupA, RefTraceGroupNum.GroupB, RefTraceGroupNum.GroupC, RefTraceGroupNum.GroupD, RefTraceGroupNum.GroupE };
        public static System.Drawing.Color[] trace_color = 
        {
            System.Drawing.Color.FromArgb(128, 255, 128, 0),
            System.Drawing.Color.FromArgb(128, 0, 128, 255),
            System.Drawing.Color.FromArgb(128, 255, 0, 128),
            System.Drawing.Color.FromArgb(128, 0, 170, 0),
            System.Drawing.Color.FromArgb(128, 255, 0, 255),
        };

        public static System.Drawing.Pen[] trace_pen = 
        {
          new System.Drawing.Pen(trace_color[0]),
          new System.Drawing.Pen(trace_color[1]), 
          new System.Drawing.Pen(trace_color[2]),
          new System.Drawing.Pen(trace_color[3]),
          new System.Drawing.Pen(trace_color[4]),
        };

        public static System.Drawing.Brush[] trace_brush = 
        { 
          new System.Drawing.SolidBrush(trace_color[0]),
          new System.Drawing.SolidBrush(trace_color[1]), 
          new System.Drawing.SolidBrush(trace_color[2]),
          new System.Drawing.SolidBrush(trace_color[3]),
          new System.Drawing.SolidBrush(trace_color[4]),
        };

        public static int TraceLength
        {
            get
            {
                return all_group.Length;
            }
        }
    }

    [Serializable()]
    public class ReferenceTrace : RefTraceBase
    {
        private static readonly string TraceFileName = "ref_trace.dat";

        public SpectrumGroupTrace[] spGroups = null;
        public TransferGroupTrace[] tfGroups = null;

        [NonSerialized]
        public bool _isShowSpectrum = false;

        [NonSerialized]
        public bool _isShowTransfer = false;

        [NonSerialized]
        public RefTraceGroupNum currSpGroup = RefTraceGroupNum.GroupA;

        [NonSerialized]
        public RefTraceGroupNum currTfGroup = RefTraceGroupNum.GroupA;

        [NonSerialized]
        public RTAMode rtaMode = RTAMode.Spectrum;

        public ReferenceTrace()
        {
            spGroups = new SpectrumGroupTrace[RefTraceBase.all_group.Length];
            for (int i = 0; i < spGroups.Length; ++i)
            {
                spGroups[i] = new SpectrumGroupTrace(RefTraceBase.all_group[i]);
            }

            tfGroups = new TransferGroupTrace[RefTraceBase.all_group.Length];
            for (int i = 0; i < tfGroups.Length; ++i)
            {
                tfGroups[i] = new TransferGroupTrace(RefTraceBase.all_group[i]);
            }
        }

        public bool isShowRefTrace
        {
            get
            {
                if (rtaMode == RTAMode.Spectrum)
                {
                    return _isShowSpectrum;
                }
                else
                {
                    return _isShowTransfer;
                }
            }
            set
            {
                if (rtaMode == RTAMode.Spectrum)
                {
                    _isShowSpectrum = value;
                }
                else
                {
                    _isShowTransfer = value;
                }
            }
        }

        public TraceGroupBase GetGroup(RefTraceGroupNum gn)
        {
            if (rtaMode == RTAMode.Spectrum)
            {
                return GetSpectrumGroup(gn);
            }
            else
            {
                return GetTransferGroup(gn);
            }
        }

        public RefTraceGroupNum CurrGroup
        {
            get
            {
                if (rtaMode == RTAMode.Spectrum)
                {
                    return currSpGroup;
                }
                else
                {
                    return currTfGroup;
                }
            }
            set
            {
                if (rtaMode == RTAMode.Spectrum)
                {
                    currSpGroup = value;
                }
                else
                {
                    currTfGroup = value;
                }
            }
        }

        public TraceGroupBase GetCurrGroup()
        {
            if (rtaMode == RTAMode.Spectrum)
            {
                return GetCurrSpectrumGroup();
            }
            else
            {
                return GetCurrTransferGroup();
            }
        }

        public SpectrumGroupTrace GetSpectrumGroup(RefTraceGroupNum gn)
        {
            int index = (int)gn;
            return spGroups[index];
        }

        public SpectrumGroupTrace GetCurrSpectrumGroup()
        {
            return GetSpectrumGroup(currSpGroup);
        }

        public TransferGroupTrace GetTransferGroup(RefTraceGroupNum gn)
        {
            int index = (int)gn;
            return tfGroups[index];
        }

        public TransferGroupTrace GetCurrTransferGroup()
        {
            return GetTransferGroup(currTfGroup);
        }

        public static ReferenceTrace LoadTraceFromFile()
        {
            if (!File.Exists(RefTraceFileName))
            {
                return null;
            }

            try
            {
                using (FileStream fs = new FileStream(RefTraceFileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    ReferenceTrace rt = (ReferenceTrace)bf.Deserialize(fs);
                    fs.Close();
                    rt.Deserialize();
                    return rt;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        private void Deserialize()
        {
            foreach (RefTraceGroupNum gn in all_group)
            {
                GetSpectrumGroup(gn).data_lock = new object();
                GetTransferGroup(gn).data_lock = new object();
            }
        }

        public static void SaveTraceToFile(ReferenceTrace rt)
        {
            try
            {
                using (FileStream fs = new FileStream(RefTraceFileName, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, rt);
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string RefTraceFileName
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory + TraceFileName;
            }
        }

        /*
        //弃用代码，不使用xml方式存储，使用序列化存储
        
        private static readonly string RootNodeName = "TraceData";
        private static readonly string SpectrumNodeName = "spectrum";
        private static readonly string TransferNodeName = "transfer";
        private static readonly string TraceNodeName = "trace";

        private static readonly string SFAttr = "sample_freq";
        private static readonly string SAAttr = "sample_accuracy";
        private static readonly string DelayAttr = "delay";
        private static readonly string fftSizeAttr = "fft_size";
        private static readonly string WindowAttr = "window";
        private static readonly string ChanAttr = "channel";
        private static readonly string CommentAttr = "comment";
        
        public static string RefTraceDir
        {
            get
            {
                string dir = lmUtility.lmDirectory.configDir + folder ;
                try
                {
                    if (!System.IO.Directory.Exists(dir))
                    {
                        System.IO.Directory.CreateDirectory(dir);
                    }
                }
                catch (Exception)
                {

                }

                return dir;
            }
        }

        public void SaveData()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);

            //加入根节点 lmAEC
            XmlElement rootEle = xmldoc.CreateElement(RootNodeName);
            xmldoc.AppendChild(rootEle);

            XmlElement spectrumEle = xmldoc.CreateElement(SpectrumNodeName);
            rootEle.AppendChild(spectrumEle);
            AppendSpectrumNodes(xmldoc, spectrumEle);

            XmlElement transferEle = xmldoc.CreateElement(TransferNodeName);
            rootEle.AppendChild(transferEle);
            AppendTransferNodes(xmldoc, transferEle);

            xmldoc.Save(RefTraceXmlFileName);
        }

        public void LoadData()
        {
            
        }

        public void AppendSpectrumNodes(XmlDocument xmldoc, XmlElement spectrumEle)
        {
            foreach (SpectrumGroupTrace group in spectrumGroups)
            {
                string groupNo = GroupNum2String(group.groupNum);
                foreach(SpectrumInfo spectrumTrace in group.traces)
                {
                    if(spectrumTrace.isValid)
                    {
                        double[] data = new double[spectrumTrace.amp];
                        try
                        {
                            
                        }
                        catch
                        {

                        }
                        XmlElement traceEle = xmldoc.CreateElement(TraceNodeName);
                        traceEle.SetAttribute(SFAttr, spectrumTrace.sample_freq.ToString());
                    }
                }
            }
        }

        public void AppendTransferNodes(XmlDocument xmldoc, XmlElement spectrumEle)
        {

        }
        
        public static string GroupNum2String(RefTraceGroupNum gn)
        {
            switch(gn)
            {
                case RefTraceGroupNum.GroupA:
                    return "A";

                case RefTraceGroupNum.GroupB:
                    return "B";

                case RefTraceGroupNum.GroupC:
                    return "C";

                case RefTraceGroupNum.GroupD:
                    return "D";

                case RefTraceGroupNum.GroupE:
                    return "E";
            }

            return "X";
        }
        */
    }
}
