using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using AEC.RTA.Utils;
using AEC.RTA.Core;
using AEC.Core;

namespace AEC.RTA
{
    public delegate float CalcCoorHandler(double val);

    public class showCross
    {
        public static Object crossLock = new Object();
        public static int crossLength = 25;
        public bool _isShow = false;
        public int _x = 0;
        public int _y = 0;

        public void Update(bool isShow = false, int x = -1, int y = -1)
        {
            lock (crossLock)
            {
                this._isShow = isShow;
                this._x = x;
                this._y = y;
            }
        }
    }

    public class Display : ThreadBase
    {
        private readonly static float MinFreq = 10;
        private readonly static float MaxFreq = 21500;
        private readonly static float MinPointCalcOctave = 32;

        private static object paramLock = new object();

        //public RTAMode _rtaMode = RTAMode.Spectrum;

        public StereoBuffer signalAmp = null;        //信号的归一化幅度值
        public StereoBuffer signalMag = null;        //信号的dB值

        public StereoOctaveLevel signalOctaveLevel = null;   //信号的倍频程电平值

        public FrequencyResponse freqResp { get; private set; } //系统频率响应

        private bool _needToUpdateKeyFreqIndex = false;
        private Size targetSize = Size.Empty;

        private float curr_FR = 1.0f;

        private int _octave_bottom = -999;

        private List<int> _keyFreqIndex = null;

        public Point[] _spectrumLeftChPoint = null;
        public Point[] _spectrumRightChPoint = null;
        public Point[] _transfer_mag_Point = null;
        public Point[] _transfer_phase_Point = null;

        public Rectangle[] leftOctaveBands = null;
        public Rectangle[] rightOctaveBands = null;

        private EventHandler DisplayHandler = null;

        private Mutex dataLock = new Mutex();
        //private Mutex spectrumLock = new Mutex();
        //public Mutex graphicLock = new Mutex();
        //public BufferedGraphics bufferedGraphics = null;

        public CalcCoorHandler CalcYdB2Panle = null;    //分贝值到PanelY坐标
        public CalcCoorHandler CalcXFreq2Panel = null;  //频率值到PanelX坐标
        public CalcCoorHandler CalcPanelX2Freq = null;  //panel x坐标计算频率
        public CalcCoorHandler CalcYAngle2Panle = null; //角度值到PanelY坐标

        public showCross currCross = new showCross();

        private SpectrumMap _spectrumMap = SpectrumMap.DoubleCh;
        private FreqRespMap _freqRespMap = FreqRespMap.AmpRespAndPhaseResp;

        public readonly RTAContext rtaContext = new RTAContext();

        private int fftSize = 0;
        private int sampleRate = 0;

        private StringFormat sf = new StringFormat();

        public ReferenceTrace refTrace = null;

        private bool _rta_is_on = false;
        private object rta_lock = new object();
        public bool rta_is_on
        {
            get
            {
                lock (rta_lock)
                {
                    return _rta_is_on;
                }
            }
            set
            {
                lock (rta_lock)
                {
                    _rta_is_on = value;
                }
            }
        }

        private bool _sp_input_data_is_valid = false;
        private bool _tf_input_data_is_valid = false;

        public Display()
        {
            signalOctaveLevel = new StereoOctaveLevel();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            LoadRefTrace();
        }

        public void RegisterDisplayHandler(EventHandler handler)
        {
            DisplayHandler += handler;
        }

        public void RemoveDisplayHandler(EventHandler handler)
        {
            DisplayHandler -= handler;
        }

        public bool isFPPO
        {
            get
            {
                return this.rtaContext.rtaMode == RTAMode.Transfer && this.rtaContext.rtaFftSize == FFTSize.FPPO;
            }
        }

        public SpectrumMap spectrumMap
        {
            get
            {
                return this._spectrumMap;
            }
            set
            {
                this._spectrumMap = value;
            }
        }

        public FreqRespMap freqRespMap
        {
            get
            {
                return this._freqRespMap;
            }
            set
            {
                this._freqRespMap = value;
            }
        }

        //private bool paramIsChanged
        //{
        //    get
        //    {
        //        lock (paramLock)
        //        {
        //            return this._paramIsChanged;
        //        }
        //    }
        //    set
        //    {
        //        lock (paramLock)
        //        {
        //            this._paramIsChanged = value;
        //        }
        //    }
        //}

        public int octave_bottom
        {
            set
            {
                this._octave_bottom = value;
            }
        }

        public void RunOnce()
        {
            base.ExecuteOnce();
        }

        private void UpdateCalcCoorHandler()
        {
            EQGraphicHelper currEQGraphicHelper = EQGraphicHelper.GetCurrInstance();

            if (rtaContext.rtaMode == RTAMode.Spectrum)
            {
                CalcYdB2Panle = currEQGraphicHelper.SpAmp2PanelY;
            }
            else
            {
                CalcYdB2Panle = currEQGraphicHelper.TfAmpResp2PanelY;
            }

            CalcXFreq2Panel = currEQGraphicHelper.Freq2PanelX;
            CalcPanelX2Freq = currEQGraphicHelper.PanelX2Freq;

            CalcYAngle2Panle = currEQGraphicHelper.TfPhaseResp2PanelY;

            if(targetSize != currEQGraphicHelper.clientSize)
            {
                targetSize = currEQGraphicHelper.clientSize;
                _needToUpdateKeyFreqIndex = true;
            }
        }

        protected override void Run()
        {
            if (dataLock.WaitOne(0))
            {
                UpdateParams();
                UpdateCalcCoorHandler();

                if (rtaContext.rtaMode == RTAMode.Spectrum)
                {
                    octave.UpdateScale(AudioAnalysis.rtaParam.spScale);

                    GenRTASpectrumMapData();
                    GenRefTraceSpectrumMapData();
                }
                else
                {
                    GenRTATransferFunctionData();
                    GenRefTraceTransferFunctionData();
                }

                dataLock.ReleaseMutex();

                if (DisplayHandler != null)
                {
                    //Console.WriteLine("Display.DisplayHandler()...");
                    DisplayHandler(this, EventArgs.Empty);
                }
            }
        }

        private void GenRTASpectrumMapData()
        {
            if (rta_is_on && _sp_input_data_is_valid)
            {
                if (octave._scale == SpectrumScale.Log)
                {
                    InitKeyFreqPoint();
                    GenKeySpectrumPoints();
                }
                else
                {
                    CalcOctaveBandsLevel();
                    GenOctaveBandsRect();
                }
            }
        }

        private void GenRefTraceSpectrumMapData()
        {
            if (refTrace._isShowSpectrum)
            {
                if (octave._scale == SpectrumScale.Log)
                {
                    InitRefTraceKeyFreqPoint(refTrace.spGroups);
                    GenRefTraceKeySpectrumPoints();
                }
                else
                {
                    CalcRefTraceOctaveBandsLevel();
                    GenRefTraceOctaveBandsRect();
                }
            }
        }

        private void GenRTATransferFunctionData()
        {
            if (rta_is_on && _tf_input_data_is_valid)
            {
                if (rtaContext.rtaFftSize != FFTSize.FPPO)
                {
                    InitKeyFreqPoint();
                    GenKeyTransferPoints();
                }
                else
                {
                    GenFPPOTransferFunctionData();
                }
            }
        }

        private void GenFPPOTransferFunctionData()
        {
            if (rta_is_on && _tf_input_data_is_valid)
            {
                dataLock.WaitOne();

                //List<Point> mag_pts = new List<Point>();
                //List<Point> phase_pts = new List<Point>();
                //bool startIndex = false;
                //for (int i = 0; i < fppoFreqResp.freqs.Length; ++i)
                //{
                //    if (fppoFreqResp.freqs[i] > MinFreq)
                //    {
                //        int x = (int)CalcXFreq2Panel(fppoFreqResp.freqs[i]);
                //        int mag_y = (int)CalcYdB2Panle((float)fppoFreqResp.magnitudes[i]);
                //        int phase_y = (int)CalcYAngle2Panle((float)fppoFreqResp.phases[i]);
                //        mag_pts.Add(new Point(x, mag_y));
                //        phase_pts.Add(new Point(x, phase_y));

                //        if(!startIndex)
                //        {
                //            fppoFreqResp.startIndex = i;
                //            startIndex = true;
                //        }
                //    }
                //}

                //_transfer_mag_Point = mag_pts.ToArray();
                //_transfer_phase_Point = phase_pts.ToArray();

                FPPOFreqRespToPoints(fppoFreqResp, out _transfer_mag_Point, out _transfer_phase_Point);

                dataLock.ReleaseMutex();
            }
        }

        public void FPPOFreqRespToPoints(FPPO.FreqResp fppo_freqResp, out Point[] mag_points, out Point[] phase_points)
        {
            List<Point> mag_pts = new List<Point>();
            List<Point> phase_pts = new List<Point>();
            bool startIndex = false;
            for (int i = 0; i < fppo_freqResp.freqs.Length; ++i)
            {
                if (fppo_freqResp.freqs[i] > MinFreq)
                {
                    int x = (int)CalcXFreq2Panel(fppo_freqResp.freqs[i]);
                    int mag_y = (int)CalcYdB2Panle((float)fppo_freqResp.magnitudes[i]);
                    int phase_y = (int)CalcYAngle2Panle((float)fppo_freqResp.phases[i]);
                    mag_pts.Add(new Point(x, mag_y));
                    phase_pts.Add(new Point(x, phase_y));

                    if (!startIndex)
                    {
                        fppo_freqResp.startIndex = i;
                        startIndex = true;
                    }
                }
            }

            mag_points = mag_pts.ToArray();
            phase_points = phase_pts.ToArray();
        }

        private void GenRefTraceTransferFunctionData()
        {
            if (refTrace._isShowTransfer)
            {
                //InitRefTraceKeyFreqPoint(refTrace.tfGroups);
                //GenKeyTransferTracePoints();
                GenRefTracePoints();
            }
        }

        private void GenRefTracePoints()
        {
            for (int i = 0; i < refTrace.tfGroups.Length; ++i)
            {
                if (refTrace.tfGroups[i].isNeedtoUpdate)
                {
                    TransferInfo trace = (TransferInfo)refTrace.tfGroups[i].GetCurrTrace();
                    if (trace != null && trace.isValid)
                    {
                        if (trace.isFPPO)
                        {
                            FPPOFreqRespToPoints(trace.fppoFreqResp, out refTrace.tfGroups[i].magPointList, out refTrace.tfGroups[i].phasePointList);
                        }
                        else
                        {
                            if (_keyFreqIndex != null && trace.sample_freq == sampleRate && trace.fft_size == fftSize)
                            {
                                refTrace.tfGroups[i]._keyFreqIndex = Utility.CopyList(_keyFreqIndex);
                            }
                            else
                            {
                                refTrace.tfGroups[i]._keyFreqIndex = GenKeyFreqsIndex(trace.fr, trace.fft_size);
                            }

                            lock (refTrace.tfGroups[i].data_lock)
                            {
                                refTrace.tfGroups[i].magPointList = CalcKeyPoints(refTrace.tfGroups[i]._keyFreqIndex, trace.transfer.amp, trace.fr);
                                refTrace.tfGroups[i].phasePointList = CalcKeyPoints(refTrace.tfGroups[i]._keyFreqIndex, trace.transfer.phase, trace.fr, true);
                            }
                        }
                    }

                    refTrace.tfGroups[i].isNeedtoUpdate = false;
                }
            }
        }

        //只是显示，不需要计算
        public void DisplayOutput()
        {
            if (DisplayHandler != null)
            {
                DisplayHandler(this, EventArgs.Empty);
            }
        }

        private void InitSignalAmp()
        {
            int fftLength = rtaContext.fftSize;
            if (signalAmp == null || signalAmp.length != fftLength / 2)
            {
                signalAmp = new StereoBuffer(fftLength / 2);
                _needToUpdateKeyFreqIndex = true;
            }
        }

        private void InitSignalMag()
        {
            int fftLength = rtaContext.fftSize;
            if (signalMag == null || signalMag.length != fftLength / 2)
            {
                signalMag = new StereoBuffer(fftLength / 2);
                _needToUpdateKeyFreqIndex = true;

                for (int i = 0; i < signalMag.length; ++i)
                {
                    signalMag.left[i] = AudioRTA.ZeroMagIndB;
                    signalMag.right[i] = AudioRTA.ZeroMagIndB;
                }
            }
        }

        private void InitFreqResp()
        {
            int fftLength = rtaContext.fftSize;
            if (freqResp == null || freqResp.length != fftLength / 2)
            {
                freqResp = new FrequencyResponse(fftLength / 2);
                _needToUpdateKeyFreqIndex = true;
            }
        }

        private void UpdateCurrFr()
        {
            if (fftSize != rtaContext.fftSize || sampleRate != rtaContext.sampleRate)
            {
                _needToUpdateKeyFreqIndex = true;
            }

            fftSize = rtaContext.fftSize;
            sampleRate = rtaContext.sampleRate;
            curr_FR = (float)sampleRate / (float)fftSize;
        }

        private void UpdateParams()
        {
            InitSignalAmp();
            InitSignalMag();
            InitFreqResp();
            UpdateCurrFr();
        }

        public void UpdateDisplaySize()
        {
            if (rtaContext.rtaMode == RTAMode.Spectrum)
            {
                if (refTrace._isShowSpectrum)
                {
                    for (int i = 0; i < refTrace.spGroups.Length; ++i)
                    {
                        if (refTrace.spGroups[i].isShow)
                        {
                            refTrace.spGroups[i].isNeedtoUpdate = true;
                        }
                    }
                }
            }
            else
            {
                if (refTrace._isShowTransfer)
                {
                    for (int i = 0; i < refTrace.tfGroups.Length; ++i)
                    {
                        if (refTrace.tfGroups[i].isShow)
                        {
                            refTrace.tfGroups[i].isNeedtoUpdate = true;
                        }
                    }
                }
            }
        }

        public void LoadRefTrace()
        {
            refTrace = ReferenceTrace.LoadTraceFromFile();
            if (refTrace != null)
            {
                foreach (RefTraceGroupNum gn in ReferenceTrace.all_group)
                {
                    if (refTrace.GetSpectrumGroup(gn).isShow)
                    {
                        refTrace.GetSpectrumGroup(gn).isNeedtoUpdate = true;
                    }

                    if (refTrace.GetTransferGroup(gn).isShow)
                    {
                        refTrace.GetTransferGroup(gn).isNeedtoUpdate = true;
                    }
                }

                /*
                foreach (RefTraceGroupNum gn in ReferenceTrace.all_group)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        Console.WriteLine("spectrum " + gn.ToString() + " comment:" + refTrace.GetSpectrumGroup(gn).GetTrace(i).comment);
                    }
                }

                foreach (RefTraceGroupNum gn in ReferenceTrace.all_group)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        Console.WriteLine("transfer " + gn.ToString() + " comment:" + refTrace.GetTransferGroup(gn).GetTrace(i).comment);
                    }                   
                }
                */

                if (refTrace.spGroups.Length != RefTraceBase.TraceLength ||
                    refTrace.tfGroups.Length != RefTraceBase.TraceLength)
                {
                    refTrace = new ReferenceTrace();
                }
            }
            else
            {
                refTrace = new ReferenceTrace();
            }
        }

        public void SaveRefTrace()
        {
            ReferenceTrace.SaveTraceToFile(refTrace);
        }

        public void EraseSpectrumTrace(RefTraceGroupNum gn)
        {
            SpectrumGroupTrace sgt = refTrace.GetSpectrumGroup(gn);

            lock (sgt.data_lock)
            {
                sgt.GetCurrTrace().Reset();
                sgt.pointList = null;
                sgt.octaveBands = null;
            }
        }



        public void EraseTransferTrace(RefTraceGroupNum gn)
        {
            TransferGroupTrace sgt = refTrace.GetTransferGroup(gn);

            lock (sgt.data_lock)
            {
                sgt.GetCurrTrace().Reset();
                sgt.magPointList = null;
                sgt.phasePointList = null;
            }
        }

        private void GenRefTraceOctaveBandsRect()
        {
            OctaveEntity entity = this.octave._entity;
            if (entity == null)
            {
                return;
            }

            for (int i = 0; i < refTrace.spGroups.Length; ++i)
            {
                if (refTrace.spGroups[i].isNeedtoUpdate)
                {
                    SpectrumGroupTrace sgt = refTrace.spGroups[i];
                    SpectrumInfo trace = (SpectrumInfo)refTrace.spGroups[i].GetCurrTrace();
                    if (trace.isValid && sgt.bandLevels != null)
                    {
                        lock (refTrace.spGroups[i].data_lock)
                        {
                            if (sgt.bandLevels != null)
                            {
                                sgt.octaveBands = GenBandsRect(entity, sgt.bandLevels);
                            }
                            else
                            {
                                sgt.octaveBands = null;
                            }
                        }
                    }

                    //Console.WriteLine("更新");
                    refTrace.spGroups[i].isNeedtoUpdate = false;
                }
            }
        }

        private Rectangle[] GenBandsRect(OctaveEntity entity, double[] octaveLevel)
        {
            Debug.Assert(entity != null && entity.bandCount == octaveLevel.Length);

            Rectangle[] rects = new Rectangle[entity.bandCount];

            for (int i = 0; i < entity.bandCount; ++i)
            {
                int left = (int)CalcXFreq2Panel(entity.bands[i].lower);
                int right = (int)CalcXFreq2Panel(entity.bands[i].upper);
                int bottom = (int)CalcYdB2Panle(_octave_bottom);
                int leftTop = (int)CalcYdB2Panle((float)octaveLevel[i]);

                rects[i].X = left;
                rects[i].Width = right - left - 1;
                rects[i].Y = leftTop;
                rects[i].Height = bottom - leftTop;
            }

            return rects;
        }

        private void GenOctaveBandsRect()
        {
            OctaveEntity entity = this.octave._entity;
            if (entity == null || signalOctaveLevel.left == null || signalOctaveLevel.right == null)
            {
                return;
            }

            leftOctaveBands = GenBandsRect(entity, signalOctaveLevel.left);
            rightOctaveBands = GenBandsRect(entity, signalOctaveLevel.right);
        }

        private void CalcRefTraceOctaveBandsLevel()
        {
            OctaveEntity entity = this.octave._entity;
            if (entity == null)
            {
                return;
            }

            for (int i = 0; i < refTrace.spGroups.Length; ++i)
            {
                if (refTrace.spGroups[i].isNeedtoUpdate)
                {
                    SpectrumGroupTrace sgt = refTrace.spGroups[i];
                    SpectrumInfo trace = (SpectrumInfo)refTrace.spGroups[i].GetCurrTrace();
                    if (trace.isValid)
                    {
                        sgt.bandLevels = CalcBandLevel(entity, trace.amp, trace.fr);
                    }
                }
            }
        }

        private static double[] CalcBandLevel(OctaveEntity entity, double[] amp, double fr)
        {
            Debug.Assert(entity != null);
            double[] bands_level = new double[entity.bandCount];
            OctaveBand first_band = entity.bands[0];
            float first_width = first_band.upper - first_band.lower;
            int low_index = (int)(first_band.lower / fr) + 1;
            int high_index = (int)(first_band.upper / fr);
            int firstband_pt_number = high_index - low_index + 1;
            if (firstband_pt_number < MinPointCalcOctave)
            {
                double t = first_width / MinPointCalcOctave;
                int scale = (int)(fr / t) + 1;
                double t_fr = fr / scale;
                int critical_index = 0;
                for (int i = 1; i < entity.bandCount; ++i)
                {
                    float bandWidth = entity.bands[i].bandWidth;
                    if (bandWidth / fr > MinPointCalcOctave)
                    {
                        critical_index = i;
                        break;
                    }
                }

                int min_index = (int)(entity.bands[0].lower / fr);
                int max_index = (int)(entity.bands[critical_index].lower / fr) + 1;

                double[] amp_scale = new double[max_index * scale];
                for (int i = min_index; i <= max_index - 1; ++i)
                {
                    double t1 = amp[i];
                    double t2 = amp[i + 1];
                    for (int j = 0; j < scale; ++j)
                    {
                        amp_scale[scale * i + j] = t1 * (scale - j) / (double)scale + t2 * j / (double)scale;
                    }
                }

                double sum = 0;
                for (int i = 0; i < critical_index; ++i)
                {
                    sum = 0;
                    int st = (int)(entity.bands[i].lower / t_fr) + 1;
                    int ed = (int)(entity.bands[i].upper / t_fr);
                    for (int j = st; j <= ed; ++j)
                    {
                        sum += amp_scale[j] * amp_scale[j];
                    }
                    bands_level[i] = sum > 0 ? 10 * Math.Log10(sum / scale) : AudioRTA.ZeroMagIndB;
                }
                for (int i = critical_index; i < entity.bandCount; ++i)
                {
                    sum = 0;
                    int st = (int)(entity.bands[i].lower / fr) + 1;
                    int ed = (int)(entity.bands[i].upper / fr);
                    for (int j = st; j <= ed; ++j)
                    {
                        sum += amp[j] * amp[j];
                    }
                    bands_level[i] = sum > 0 ? 10 * Math.Log10(sum) : AudioRTA.ZeroMagIndB;
                }
            }
            else
            {
                for (int i = 0; i < entity.bandCount; ++i)
                {
                    double sum = 0;
                    int st = (int)(entity.bands[i].lower / fr) + 1;
                    int ed = (int)(entity.bands[i].upper / fr);
                    for (int j = st; j <= ed; ++j)
                    {
                        sum += amp[j] * amp[j];
                    }
                    bands_level[i] = sum > 0 ? 10 * Math.Log10(sum) : AudioRTA.ZeroMagIndB;
                }
            }

            return bands_level;
        }

        private void CalcOctaveBandsLevel()
        {
            OctaveEntity entity = this.octave._entity;
            if (entity == null)
            {
                return;
            }

            signalOctaveLevel.left = CalcBandLevel(entity, signalAmp.left, curr_FR);
            signalOctaveLevel.right = CalcBandLevel(entity, signalAmp.right, curr_FR);
        }

        //private void GenSpectrumMapData()
        //{
        //    InitKeyFreqPoint();
        //    GenKeySpectrumPoints();

        //    if (refTrace._isShowSpectrum)
        //    {
        //        InitRefTraceKeyFreqPoint(refTrace.spGroups);
        //        GenRefTraceKeySpectrumPoints();
        //    }
        //}

        //private void GenTransferFunctionData()
        //{
        //    InitKeyFreqPoint();
        //    GenKeyTransferPoints();

        //    //
        //    if (refTrace._isShowTransfer)
        //    {
        //        //InitTraceKeyFreqPoint();
        //        InitRefTraceKeyFreqPoint(refTrace.tfGroups);
        //        GenKeyTransferTracePoints();
        //    }
        //}

        public void InitKeyFreqPoint()
        {
            if (_needToUpdateKeyFreqIndex)
            {
                _keyFreqIndex = GenKeyFreqsIndex(curr_FR, fftSize); //重新生成关键频率
                _needToUpdateKeyFreqIndex = false;
            }
        }

        public void InitSpectrumTraceKeyFreqPoint()
        {
            for (int i = 0; i < refTrace.spGroups.Length; ++i)
            {
                if (refTrace.spGroups[i].isNeedtoUpdate)
                {
                    RefTraceInfoBase trace = (RefTraceInfoBase)refTrace.spGroups[i].GetCurrTrace();
                    if (trace.isValid)
                    {
                        if (_keyFreqIndex != null && trace.sample_freq == sampleRate && trace.fft_size == fftSize)
                        {
                            refTrace.spGroups[i]._keyFreqIndex = Utility.CopyList(_keyFreqIndex);
                        }
                        else
                        {
                            refTrace.spGroups[i]._keyFreqIndex = GenKeyFreqsIndex(trace.fr, trace.fft_size);
                        }
                    }
                }
            }
        }

        public void InitRefTraceKeyFreqPoint(TraceGroupBase[] groups)
        {
            for (int i = 0; i < groups.Length; ++i)
            {
                if (groups[i].isNeedtoUpdate)
                {
                    RefTraceInfoBase trace = groups[i].GetCurrTrace();
                    if (trace.isValid)
                    {
                        if (_keyFreqIndex != null && trace.sample_freq == sampleRate && trace.fft_size == fftSize)
                        {
                            groups[i]._keyFreqIndex = Utility.CopyList(_keyFreqIndex);
                        }
                        else
                        {
                            groups[i]._keyFreqIndex = GenKeyFreqsIndex(trace.fr, trace.fft_size);
                        }
                    }
                }
            }
        }

        private void GenRefTraceKeySpectrumPoints()
        {
            for (int i = 0; i < refTrace.spGroups.Length; ++i)
            {
                if (refTrace.spGroups[i].isNeedtoUpdate)
                {
                    SpectrumInfo trace = (SpectrumInfo)refTrace.spGroups[i].GetCurrTrace();
                    if (trace.isValid)
                    {
                        double[] mag_dB = new double[trace.fft_size / 2];

                        CalcSignaldBFromAmp(trace.amp, mag_dB);

                        double sum = 0;
                        for (int j = 9; j < 19; ++j)
                        {
                            float freq = j * trace.fr;
                            if (freq >= 14.12538 && freq < 17.78279)
                            {
                                double amp = trace.amp[j];
                                sum += amp * amp;
                                //Console.WriteLine("freq = {0}, Amp = {1} ,Mag = {2}dB", freq, amp, mag_dB[j]);
                            }
                        }
                        //Console.WriteLine("sum = {0}", 10 * Math.Log10(sum));
                        sum = 0;
                        for (int j = 10; j < 20; ++j)
                        {
                            float freq = j * trace.fr;
                            if (freq >= 17.78279 && freq < 22.38721)
                            {
                                double amp = trace.amp[j];
                                sum += amp * amp;
                                //Console.WriteLine("freq = {0}, Amp = {1} ,Mag = {2}dB", freq, amp, mag_dB[j]);
                            }
                        }
                        //Console.WriteLine("sum = {0}", 10 * Math.Log10(sum));

                        lock (refTrace.spGroups[i].data_lock)
                        {
                            refTrace.spGroups[i].pointList = CalcKeyPoints(refTrace.spGroups[i]._keyFreqIndex, mag_dB, trace.fr);
                        }
                    }

                    refTrace.spGroups[i].isNeedtoUpdate = false;
                }
            }
        }

        private void GenKeyTransferTracePoints()
        {
            for (int i = 0; i < refTrace.tfGroups.Length; ++i)
            {
                if (refTrace.tfGroups[i].isNeedtoUpdate)
                {
                    TransferInfo trace = (TransferInfo)refTrace.tfGroups[i].GetCurrTrace();
                    if (trace.isValid)
                    {
                        lock (refTrace.tfGroups[i].data_lock)
                        {
                            refTrace.tfGroups[i].magPointList = CalcKeyPoints(refTrace.tfGroups[i]._keyFreqIndex, trace.transfer.amp, trace.fr);
                            refTrace.tfGroups[i].phasePointList = CalcKeyPoints(refTrace.tfGroups[i]._keyFreqIndex, trace.transfer.phase, trace.fr, true);
                        }
                    }

                    refTrace.tfGroups[i].isNeedtoUpdate = false;
                }
            }
        }

        private void GenKeySpectrumPoints()
        {
            if (_keyFreqIndex != null)
            {
                CalcSignaldBFromAmp(signalAmp.left, signalMag.left);
                CalcSignaldBFromAmp(signalAmp.right, signalMag.right);

                _spectrumLeftChPoint = CalcKeyPoints(_keyFreqIndex, signalMag.left, curr_FR);
                _spectrumRightChPoint = CalcKeyPoints(_keyFreqIndex, signalMag.right, curr_FR);
            }
        }

        /// <summary>
        /// 通过信号原始幅度值计算信号db单位值
        /// </summary>
        /// <param name="amp">原始信号幅度值</param>
        /// <param name="mag">信号dB值</param>
        private void CalcSignaldBFromAmp(double[] amp, double[] mag)
        {
            System.Diagnostics.Debug.Assert(amp != null);
            System.Diagnostics.Debug.Assert(mag != null);
            System.Diagnostics.Debug.Assert(amp.Length == mag.Length);

            for (int i = 0; i < amp.Length; ++i)
            {
                if (amp[i] > 0.0)
                {
                    mag[i] = 20 * Math.Log10(amp[i]);
                }
                else
                {
                    mag[i] = AudioRTA.ZeroMagIndB;
                }
            }
        }

        private void GenKeyTransferPoints()
        {
            if (_keyFreqIndex != null)
            {
                dataLock.WaitOne();
                _transfer_mag_Point = CalcKeyPoints(_keyFreqIndex, freqResp.amp, curr_FR);
                _transfer_phase_Point = CalcKeyPoints(_keyFreqIndex, freqResp.phase, curr_FR, true);
                dataLock.ReleaseMutex();
            }
        }

        private Point[] CalcKeyPoints(List<int> keyFreqIndex, double[] data, float fr, bool isPhase = false)
        {
            System.Diagnostics.Debug.Assert(keyFreqIndex != null);
            System.Diagnostics.Debug.Assert(data != null);

            List<Point> pointList = new List<Point>();
            Point tempPoint = new Point();

            for (int i = 0; i < keyFreqIndex.Count; ++i)
            {
                int start = keyFreqIndex[i];
                int end = 0;

                if (i < keyFreqIndex.Count - 1)
                {
                    end = keyFreqIndex[i + 1];
                }
                else
                {
                    end = keyFreqIndex.Count;
                }

                float freqMax = start * fr;
                float freqMin = freqMax;
                float dBMax = (float)data[start];
                float dBMin = dBMax;

                if (end - start <= 1)
                {
                    tempPoint.X = (int)CalcXFreq2Panel(freqMax);
                    if (!isPhase)
                    {
                        tempPoint.Y = (int)CalcYdB2Panle(dBMax);
                    }
                    else
                    {
                        tempPoint.Y = (int)CalcYAngle2Panle(dBMax);
                    }
                    pointList.Add(tempPoint);
                }
                else
                {
                    for (int j = start + 1; j < end; ++j)
                    {
                        float curr = (float)data[j];
                        if (curr > dBMax)
                        {
                            dBMax = curr;
                            freqMax = j * fr;
                        }

                        if (curr < dBMin)
                        {
                            dBMin = curr;
                            freqMin = j * fr;
                        }
                    }

                    if (freqMax < freqMin)
                    {
                        tempPoint.X = (int)CalcXFreq2Panel(freqMax);
                        if (!isPhase)
                        {
                            tempPoint.Y = (int)CalcYdB2Panle(dBMax);
                        }
                        else
                        {
                            tempPoint.Y = (int)CalcYAngle2Panle(dBMax);
                        }
                        pointList.Add(tempPoint);
                        tempPoint.X = (int)CalcXFreq2Panel(freqMin);
                        if (!isPhase)
                        {
                            tempPoint.Y = (int)CalcYdB2Panle(dBMin);
                        }
                        else
                        {
                            tempPoint.Y = (int)CalcYAngle2Panle(dBMin);
                        }
                        pointList.Add(tempPoint);
                    }
                    else
                    {
                        tempPoint.X = (int)CalcXFreq2Panel(freqMin);
                        if (!isPhase)
                        {
                            tempPoint.Y = (int)CalcYdB2Panle(dBMax);
                        }
                        else
                        {
                            tempPoint.Y = (int)CalcYAngle2Panle(dBMax);
                        }
                        pointList.Add(tempPoint);
                        tempPoint.X = (int)CalcXFreq2Panel(freqMax);
                        if (!isPhase)
                        {
                            tempPoint.Y = (int)CalcYdB2Panle(dBMin);
                        }
                        else
                        {
                            tempPoint.Y = (int)CalcYAngle2Panle(dBMin);
                        }
                        pointList.Add(tempPoint);
                    }
                }
            }

            return pointList.ToArray();
        }

        private Octave octave = new Octave();

        public void RTADataHandler(object sender, EventArgs e)
        {
            AudioRTA audioRTA = sender as AudioRTA;

            if (audioRTA == null)
            {
                return;
            }

            if (dataLock.WaitOne(0))
            {
                this.rtaContext.CopyFrom(audioRTA.rtaContext);
                //Console.WriteLine("Display rta mode:{0}", this.rtaContext.rtaMode);

                if (this.rtaContext.rtaMode == RTAMode.Spectrum)
                {
                    InputDoubleChAmpData(audioRTA.stereoAmp);
                    _sp_input_data_is_valid = true;
                }
                else
                {
                    if (this.rtaContext.rtaFftSize != FFTSize.FPPO)
                    {
                        InputTransferData(audioRTA.freqResp);
                    }
                    else
                    {
                        InputFPPOTransferData(audioRTA.fppoFreqResp);
                    }

                    _tf_input_data_is_valid = true;
                }

                dataLock.ReleaseMutex();
            }

            this.ExecuteOnce();
        }

        private void InputDoubleChAmpData(StereoBuffer stereoAmp)
        {
            System.Diagnostics.Debug.Assert(stereoAmp != null);

            InitSignalAmp();

            signalAmp.CopyFrom(stereoAmp);
        }

        public void InputTransferData(FrequencyResponse freq_resp)
        {
            System.Diagnostics.Debug.Assert(freq_resp != null);

            InitFreqResp();

            freqResp.CopyFrom(freq_resp);
        }

        public FPPO.FreqResp fppoFreqResp { get; private set; }
        private void InputFPPOTransferData(FPPO.FreqResp freqResp)
        {
            System.Diagnostics.Debug.Assert(freqResp != null);

            if (fppoFreqResp == null || fppoFreqResp.length != freqResp.length)
            {
                fppoFreqResp = new FPPO.FreqResp(freqResp.freqs);
            }

            Array.Copy(freqResp.magnitudes, fppoFreqResp.magnitudes, freqResp.length);
            Array.Copy(freqResp.phases, fppoFreqResp.phases, freqResp.length);
            //this.ExecuteOnce();
        }

        #region GDI
        public static Color backColor = Color.Black;
        public static Color leftChColor = Color.FromArgb(128, 0, 192, 0);
        public static Color rightChColor = Color.FromArgb(128, 0, 192, 192);
        public static Color transferMagColor = Color.FromArgb(168, 192, 192, 0);
        public static Color transferPhaseColor = Color.FromArgb(168, 192, 0, 192);
        public static Pen grayPen = new Pen(Color.Gray, 1);
        public static Brush labelbrush = new SolidBrush(Color.White);
        public static Font labelFont = new Font("微软雅黑", 13f, FontStyle.Regular);
        public static Pen LChSignalLinePen = new Pen(leftChColor, 1);
        public static Pen RChSignalLinePen = new Pen(rightChColor, 1);
        public static Pen TransferMagPen = new Pen(transferMagColor, 1);
        public static Pen TransferPhasePen = new Pen(transferPhaseColor, 1);
        public static Brush backgroundBrush = new SolidBrush(backColor);
        public static Brush leftChBrush = new SolidBrush(leftChColor);
        public static Brush rightChBrush = new SolidBrush(rightChColor);
        public static Brush transferMagBrush = new SolidBrush(transferMagColor);
        public static Brush transferPhaseBrush = new SolidBrush(transferPhaseColor);
        public static Font showInfoFont = new Font("微软雅黑", 16, FontStyle.Regular);
        public static Pen crossPen = new Pen(Color.White, 1);
        #endregion

        #region 绘图部分

        public void DrawData(Graphics g)
        {
            DrawRefTraceData(g);
            DrawRTAData(g);
        }

        private void DrawRTAData(Graphics g)
        {
            if (this.rta_is_on)
            {
                if (rtaContext.rtaMode == RTAMode.Spectrum)
                {
                    DrawRTASpectrum(g);
                }
                else
                {
                    DrawRTATransfer(g);
                }
            }
        }

        private void DrawRefTraceData(Graphics g)
        {
            if (rtaContext.rtaMode == RTAMode.Spectrum)
            {
                DrawRefTraceSpectrum(g);
            }
            else
            {
                DrawRefTraceTransfer(g);
            }
        }

        private void DrawRTASpectrum(Graphics g)
        {
            if (this.octave == null)
            {
                return;
            }

            if (this.octave._scale == SpectrumScale.Log)
            {
                DrawSpectrumMap(g);
            }
            else
            {
                DrawSpectrumOctave(g);
            }
        }

        private void DrawRTATransfer(Graphics g)
        {
            dataLock.WaitOne();
            switch (_freqRespMap)
            {
                case FreqRespMap.AmpRespAndPhaseResp:
                    if (_transfer_mag_Point != null && _transfer_mag_Point.Length > 0)
                    {
                        g.DrawLines(TransferMagPen, _transfer_mag_Point);
                    }
                    if (_transfer_phase_Point != null && _transfer_phase_Point.Length > 0)
                    {
                        g.DrawLines(TransferPhasePen, _transfer_phase_Point);
                    }
                    break;

                case FreqRespMap.AmpResp:
                    if (_transfer_mag_Point != null && _transfer_mag_Point.Length > 0)
                    {
                        g.DrawLines(TransferMagPen, _transfer_mag_Point);
                    }
                    break;

                case FreqRespMap.PhaseResp:
                    if (_transfer_phase_Point != null && _transfer_phase_Point.Length > 0)
                    {
                        g.DrawLines(TransferPhasePen, _transfer_phase_Point);
                    }
                    break;
            }
            dataLock.ReleaseMutex();
        }

        private void DrawRefTraceSpectrum(Graphics g)
        {
            if (this.octave == null)
            {
                return;
            }

            if (this.octave._scale == SpectrumScale.Log)
            {
                DrawSpectrumTraceMap(g);
            }
            else
            {
                DrawRefTraceSpectrumOctave(g);
            }
        }

        private void DrawRefTraceTransfer(Graphics g)
        {
            if (refTrace._isShowTransfer)
            {
                switch (_freqRespMap)
                {
                    case FreqRespMap.AmpRespAndPhaseResp:
                        DrawTransferTraceAmpAndPhase(g);
                        break;

                    case FreqRespMap.AmpResp:
                        DrawTransferTraceAmp(g);
                        break;

                    case FreqRespMap.PhaseResp:
                        DrawTransferTracePhase(g);
                        break;
                }
            }
        }

        #endregion

        public void DrawRefTraceSpectrumOctave(Graphics g)
        {
            if (refTrace._isShowSpectrum)
            {
                for (int i = 0; i < refTrace.spGroups.Length; ++i)
                {
                    refTrace.spGroups[i].DrawOctaves(g, ReferenceTrace.trace_brush[i]);
                    /*
                    if (refTrace.spGroups[i].isShow && refTrace.spGroups[i].octaveBands != null && refTrace.spGroups[i].octaveBands.Length > 0)
                    {
                        g.FillRectangles(ReferenceTrace.trace_brush[i], refTrace.spGroups[i].octaveBands);
                    }
                    */
                }
            }
        }

        public void DrawSpectrumOctave(Graphics g)
        {
            dataLock.WaitOne();

            switch (_spectrumMap)
            {
                case SpectrumMap.DoubleCh:
                    DrawLeftAndRightOctave(g);
                    break;

                case SpectrumMap.LeftCh:
                    DrawLeftOctave(g);
                    break;

                case SpectrumMap.RightCh:
                    DrawRightOctave(g);
                    break;
            }
            dataLock.ReleaseMutex();
        }

        private void DrawLeftOctave(Graphics g)
        {
            if (leftOctaveBands != null)
            {
                g.FillRectangles(leftChBrush, leftOctaveBands);
            }
        }

        private void DrawRightOctave(Graphics g)
        {
            if (rightOctaveBands != null)
            {
                g.FillRectangles(rightChBrush, rightOctaveBands);
            }
        }

        private void DrawLeftAndRightOctave(Graphics g)
        {
            this.DrawLeftOctave(g);
            this.DrawRightOctave(g);
        }

        public void DrawSpectrumTraceMap(Graphics g)
        {
            if (refTrace._isShowSpectrum)
            {
                for (int i = 0; i < refTrace.spGroups.Length; ++i)
                {
                    refTrace.spGroups[i].DrawSpectrum(g, ReferenceTrace.trace_pen[i]);
                }
            }
        }

        public void DrawSpectrumMap(Graphics g)
        {
            dataLock.WaitOne();

            switch (_spectrumMap)
            {
                case SpectrumMap.DoubleCh:
                    DrawLeftAndRightSpectrum(g);
                    break;

                case SpectrumMap.LeftCh:
                    DrawLeftSpectrum(g);
                    break;

                case SpectrumMap.RightCh:
                    DrawRightSpectrum(g);
                    break;
            }
            dataLock.ReleaseMutex();
        }

        private void DrawLeftSpectrum(Graphics g)
        {
            if (_spectrumLeftChPoint != null && _spectrumLeftChPoint.Length > 0)
            {
                g.DrawLines(LChSignalLinePen, _spectrumLeftChPoint);
            }
        }

        private void DrawRightSpectrum(Graphics g)
        {
            if (_spectrumRightChPoint != null && _spectrumRightChPoint.Length > 0)
            {
                g.DrawLines(RChSignalLinePen, _spectrumRightChPoint);
            }
        }

        private void DrawLeftAndRightSpectrum(Graphics g)
        {
            DrawRightSpectrum(g);
            DrawLeftSpectrum(g);
        }

        private void DrawTransferTraceAmp(Graphics g)
        {
            if (refTrace._isShowTransfer)
            {
                for (int i = 0; i < refTrace.tfGroups.Length; ++i)
                {
                    refTrace.tfGroups[i].DrawAmp(g, ReferenceTrace.trace_pen[i]);
                }
            }
        }

        private void DrawTransferTracePhase(Graphics g)
        {
            if (refTrace._isShowTransfer)
            {
                for (int i = 0; i < refTrace.tfGroups.Length; ++i)
                {
                    refTrace.tfGroups[i].DrawPhase(g, ReferenceTrace.trace_pen[i]);
                }
            }
        }

        private void DrawTransferTraceAmpAndPhase(Graphics g)
        {
            DrawTransferTraceAmp(g);
            DrawTransferTracePhase(g);
        }

        public void DrawSpectrumCursorData(Graphics g, Rectangle top)
        {
            if (octave == null)
            {
                return;
            }

            if (octave._scale == SpectrumScale.Log)
            {
                DrawSpectrumMapCursorData(g, top);
            }
            else
            {
                DrawSpectrumOctaveCursorData(g, top);
            }
        }

        public void DrawSpectrumMapCursorData(Graphics g, Rectangle top)
        {
            if (_keyFreqIndex == null)
            {
                return;
            }

            int index = 0;
            float freq = GetFreqFromCursorX(out index);
            Rectangle rect = top;
            string info = string.Empty;

            switch (this.spectrumMap)
            {
                case SpectrumMap.LeftCh:
                    {
                        float mag = (float)signalMag.left[index];
                        if (mag <= AudioRTA.ZeroMagIndB)
                        {
                            return;
                        }
                        DrawDBInfo(g, top, freq, mag, leftChBrush);
                        DrawCross(g, freq, mag);
                    }

                    break;

                case SpectrumMap.RightCh:
                    {
                        float mag = (float)signalMag.right[index];
                        if (mag <= AudioRTA.ZeroMagIndB)
                        {
                            return;
                        }
                        DrawDBInfo(g, top, freq, mag, leftChBrush);
                        DrawCross(g, freq, mag);
                    }
                    break;

                case SpectrumMap.DoubleCh:
                    {
                        float left_mag = (float)signalMag.left[index];
                        float right_mag = (float)signalMag.right[index];

                        if (left_mag <= AudioRTA.ZeroMagIndB || right_mag <= AudioRTA.ZeroMagIndB)
                        {
                            return;
                        }

                        DrawDoubleDBInfo(g, top, freq, left_mag, right_mag);
                        DrawCross(g, freq, left_mag);
                        DrawCross(g, freq, right_mag);
                    }

                    break;
            }
        }

        private void DrawSpectrumOctaveCursorData(Graphics g, Rectangle top)
        {
            int index = CalcOctaveIndexFromCursorX();
            if (index < 0 || index >= octave._entity.bandCount ||
                signalOctaveLevel.left == null ||
                signalOctaveLevel.right == null)
            {
                return;
            }

            float freq = octave._entity.bands[index].nominal;
            float left_db = (float)signalOctaveLevel.left[index];
            float right_db = (float)signalOctaveLevel.right[index];
            Rectangle rect = top;

            switch (this.spectrumMap)
            {
                case SpectrumMap.LeftCh:
                    if (left_db <= AudioRTA.ZeroMagIndB)
                    {
                        return;
                    }
                    DrawDBInfo(g, top, freq, left_db, leftChBrush);
                    DrawCross(g, freq, left_db);
                    break;

                case SpectrumMap.RightCh:
                    if (right_db <= AudioRTA.ZeroMagIndB)
                    {
                        return;
                    }
                    DrawDBInfo(g, top, freq, right_db, rightChBrush);
                    DrawCross(g, freq, right_db);
                    break;

                case SpectrumMap.DoubleCh:
                    if (left_db <= AudioRTA.ZeroMagIndB || right_db <= AudioRTA.ZeroMagIndB)
                    {
                        return;
                    }
                    DrawDoubleDBInfo(g, top, freq, left_db, right_db);
                    DrawCross(g, freq, left_db);
                    DrawCross(g, freq, right_db);
                    break;
            }
        }

        private void DrawDBInfo(Graphics g, Rectangle top, float freq, float db, Brush brush)
        {
            string info = string.Format("{0}Hz, {1:0.0}dB", Freq2String(freq), db);
            g.DrawString(info, showInfoFont, brush, top, sf);
        }

        private void DrawAngleInfo(Graphics g, Rectangle top, float freq, float angle, Brush brush)
        {
            string info = string.Format("{0}Hz, {1:0.0}°", Freq2String(freq), angle);
            g.DrawString(info, showInfoFont, brush, top, sf);
            DrawAngleCross(g, freq, angle);
        }

        private const int gap = 10;

        private void DrawDoubleDBInfo(Graphics g, Rectangle top, float freq, float left_db, float right_db)
        {
            string leftCh = string.Format("{0}Hz, {1:0.0}dB", Freq2String(freq), left_db);
            string rightCh = string.Format("{0}Hz, {1:0.0}dB", Freq2String(freq), right_db);
            Size leftSize = Size.Ceiling(g.MeasureString(leftCh, showInfoFont));
            Size rightSize = Size.Ceiling(g.MeasureString(rightCh, showInfoFont));
            int leftX = (top.Width - leftSize.Width - rightSize.Width - gap) / 2;
            int rgihtX = leftX + leftSize.Width + gap;
            Rectangle leftRect = new Rectangle(leftX, top.Y, leftSize.Width, top.Height);
            Rectangle rightRect = new Rectangle(rgihtX, top.Y, rightSize.Width, top.Height);
            //Font font = new Font("微软雅黑", 16, FontStyle.Regular);
            //Brush brush = new SolidBrush(Color.White);
            //g.DrawString("adflkajsgljaslgjldasgj", font, brush, Point.Empty);
            //Console.WriteLine("Paint...");
            g.DrawString(leftCh, showInfoFont, leftChBrush, leftRect, sf);
            g.DrawString(rightCh, showInfoFont, rightChBrush, rightRect, sf);
        }

        private void DrawAmpAndPhaseInfo(Graphics g, Rectangle top, float freq, float amp, float phase)
        {
            string mag_info = string.Format("{0}Hz, {1:0.0}dB", Freq2String(freq), amp);
            Size magSize = Size.Ceiling(g.MeasureString(mag_info, showInfoFont));
            Rectangle magRect = new Rectangle((top.Width - gap) / 2 - magSize.Width, 0, magSize.Width, top.Height);
            g.DrawString(mag_info, showInfoFont, transferMagBrush, magRect, sf);
            string phase_info = string.Format("{0}Hz, {1:0.0}°", Freq2String(freq), phase);
            Size phaseSize = Size.Ceiling(g.MeasureString(phase_info, showInfoFont));
            Rectangle phaseRect = new Rectangle((top.Width + gap) / 2, 0, phaseSize.Width, top.Height);
            g.DrawString(phase_info, showInfoFont, transferPhaseBrush, phaseRect, sf);
        }

        public void DrawCursorData(Graphics g, Rectangle top)
        {
            if (currCross._isShow)
            {
                if (rtaContext.rtaMode == RTAMode.Spectrum)
                {
                    this.DrawSpectrumCursorData(g, top);
                }
                else
                {
                    if (rtaContext.rtaFftSize != FFTSize.FPPO)
                    {
                        this.DrawTransferCursorData(g, top);
                    }
                    else
                    {
                        this.DrawFPPOCursorData(g, top);
                    }
                }
            }
        }

        private void DrawFPPOCursorData(Graphics g, Rectangle top)
        {
            if (currCross._isShow)
            {
                int index = GetFPPOFreqFromCursorX();
                float freq = fppoFreqResp.freqs[index];

                switch (freqRespMap)
                {
                    case FreqRespMap.AmpRespAndPhaseResp:
                        {
                            float amp = (float)this.fppoFreqResp.magnitudes[index];
                            float phase = (float)this.fppoFreqResp.phases[index];
                            DrawAmpAndPhaseInfo(g, top, freq, amp, phase);
                            DrawCross(g, freq, amp);
                            DrawAngleCross(g, freq, phase);
                        }
                        break;

                    case FreqRespMap.AmpResp:
                        {
                            float amp = (float)this.fppoFreqResp.magnitudes[index];
                            DrawDBInfo(g, top, freq, amp, transferMagBrush);
                            DrawCross(g, freq, amp);
                        }
                        break;

                    case FreqRespMap.PhaseResp:
                        {
                            float phase = (float)this.fppoFreqResp.phases[index];
                            DrawAngleInfo(g, top, freq, phase, transferPhaseBrush);
                            DrawAngleCross(g, freq, phase);
                        }
                        break;
                }
            }
        }

        private void DrawTransferCursorData(Graphics g, Rectangle top)
        {
            if (currCross._isShow && _keyFreqIndex != null)
            {
                int index = 0;
                float freq = GetFreqFromCursorX(out index);

                switch (freqRespMap)
                {
                    case FreqRespMap.AmpRespAndPhaseResp:
                        {
                            float amp = (float)this.freqResp.amp[index];
                            float phase = (float)this.freqResp.phase[index];
                            DrawAmpAndPhaseInfo(g, top, freq, amp, phase);
                            DrawCross(g, freq, amp);
                            DrawAngleCross(g, freq, phase);
                        }
                        break;

                    case FreqRespMap.AmpResp:
                        {
                            float mag = (float)this.freqResp.amp[index];
                            DrawDBInfo(g, top, freq, mag, transferMagBrush);
                            DrawCross(g, freq, mag);
                        }
                        break;

                    case FreqRespMap.PhaseResp:
                        {
                            float phase = (float)this.freqResp.phase[index];
                            DrawAngleInfo(g, top, freq, phase, transferPhaseBrush);
                            DrawAngleCross(g, freq, phase);
                        }
                        break;
                }
            }
        }

        private int CalcOctaveIndexFromCursorX()
        {
            if (this.octave == null)
            {
                return -1;
            }

            OctaveEntity entity = this.octave._entity;
            if (entity == null)
            {
                return -1;
            }

            float freq = CalcPanelX2Freq(currCross._x);
            if (freq < entity.bands[0].upper)
            {
                return 0;
            }
            else if (freq >= entity.bands[entity.bandCount - 1].lower)
            {
                return entity.bandCount - 1;
            }
            else
            {
                int start = 0;
                int end = entity.bandCount - 1;
                while (end - start > 1)
                {
                    int mid = (start + end) / 2;
                    if (mid == end)
                    {
                        return mid;
                    }

                    if (freq < entity.bands[mid].lower)
                    {
                        end = mid;
                    }
                    else if (freq >= entity.bands[mid].upper)
                    {
                        start = mid;
                    }
                    else
                    {
                        return mid;
                    }
                }

                return start;
            }
        }

        private int GetFPPOFreqFromCursorX()
        {
            float freq = CalcPanelX2Freq(currCross._x);
            int index = fppoFreqResp.startIndex;
            if (freq <= fppoFreqResp.minFreq)
            {
                index = fppoFreqResp.startIndex;
                return index;
            }
            else if (freq >= fppoFreqResp.maxFreq)
            {
                index = fppoFreqResp.freqs.Length - 1;
                return index;
            }
            else
            {
                int start = fppoFreqResp.startIndex;
                int end = fppoFreqResp.freqs.Length - 1;
                while (end - start > 1)
                {
                    int mid = (start + end) / 2;
                    if (mid == end)
                    {
                        index = mid;
                        return index;
                        //return fppoFreqResp.freqs[index];
                    }

                    if (freq < fppoFreqResp.freqs[mid])
                    {
                        end = mid;
                    }
                    else if (freq >= fppoFreqResp.freqs[mid])
                    {
                        start = mid;
                    }
                    else
                    {
                        index = mid;
                        return index;
                    }
                }

                float freq1 = fppoFreqResp.freqs[start];
                float freq2 = fppoFreqResp.freqs[end];
                if (freq - freq1 <= freq2 - freq)
                {
                    return start;
                }
                else
                {
                    return end;
                }
            }
        }

        private float GetFreqFromCursorX(out int index)
        {
            float freq = CalcPanelX2Freq(currCross._x);

            if (freq <= _keyFreqIndex[0] * curr_FR)
            {
                index = _keyFreqIndex[0];
                freq = _keyFreqIndex[0] * curr_FR;
            }
            else if (freq >= _keyFreqIndex[_keyFreqIndex.Count - 1] * curr_FR)
            {
                index = _keyFreqIndex[_keyFreqIndex.Count - 1];
                freq = _keyFreqIndex[_keyFreqIndex.Count - 1] * curr_FR;
            }
            else
            {
                index = (int)Math.Round(freq / curr_FR);
                freq = index * curr_FR;
            }

            return freq;
        }

        private void DrawCross(Graphics g, float freq, float dB)
        {
            Point cross = new Point();
            cross.X = (int)CalcXFreq2Panel(freq);
            cross.Y = (int)CalcYdB2Panle(dB);
            Point left = new Point(cross.X - showCross.crossLength, cross.Y);
            Point right = new Point(cross.X + showCross.crossLength, cross.Y);
            Point top = new Point(cross.X, cross.Y - showCross.crossLength);
            Point bottom = new Point(cross.X, cross.Y + showCross.crossLength);
            g.DrawLine(crossPen, left, right);
            g.DrawLine(crossPen, top, bottom);
        }

        private void DrawAngleCross(Graphics g, float freq, float angle)
        {
            Point cross = new Point();
            cross.X = (int)CalcXFreq2Panel(freq);
            cross.Y = (int)CalcYAngle2Panle(angle);
            Point left = new Point(cross.X - showCross.crossLength, cross.Y);
            Point right = new Point(cross.X + showCross.crossLength, cross.Y);
            Point top = new Point(cross.X, cross.Y - showCross.crossLength);
            Point bottom = new Point(cross.X, cross.Y + showCross.crossLength);
            g.DrawLine(crossPen, left, right);
            g.DrawLine(crossPen, top, bottom);
        }

        private void DrawCross(Graphics g, Point cross)
        {
            Point left = new Point(cross.X - showCross.crossLength, cross.Y);
            Point right = new Point(cross.X + showCross.crossLength, cross.Y);
            Point top = new Point(cross.X, cross.Y - showCross.crossLength);
            Point bottom = new Point(cross.X, cross.Y + showCross.crossLength);
            g.DrawLine(crossPen, left, right);
            g.DrawLine(crossPen, top, bottom);
        }

        private const float MinKeyFreqGap = 0.5f;

        //计算关键频率点
        /*
        private List<float> GenKeyFreqs()
        {
            List<float> keyFreqs = new List<float>();
            keyFreqs.Add(minFreq);

            int startX = (int)minFreq;
            int step = 1;
            int stepDelta = 10;

            int freqUpperLimit = fftSize / 2;

            bool endFlag = false;
            //float f = CalcXFreq2Panel(1000);
            //Console.WriteLine("f = {0}", f);
            for (int freq = startX; freq < freqUpperLimit; )
            {
                float x0 = CalcXFreq2Panel(freq);
                //计算合适的步长 初始值为1，2，3，4，5，6，7，8，9然后变成10，之后20，30，40，50……
                while (true)
                {
                    float tempF = freq + step;
                    if (tempF > freqUpperLimit)
                    {
                        endFlag = true;
                        break;
                    }
                    float x1 = CalcXFreq2Panel(tempF);
                    if (x1 - x0 >= MinKeyFreqGap)
                    {
                        //Console.WriteLine("步长:{0}", step);
                        //Trace.WriteLine("步长:" + step);
                        break;
                    }

                    if (step < stepDelta)
                    {
                        step += 1;
                    }
                    else
                    {
                        step += stepDelta;
                    }
                }

                for (int fTemp = freq; fTemp < freqUpperLimit; )
                {
                    int freq1 = fTemp + step;
                    if (freq1 >= freqUpperLimit)
                    {
                        endFlag = true;
                    }

                    float x1 = CalcXFreq2Panel(freq1);
                    float diff = x1 - x0;

                    if (diff >= MinKeyFreqGap)
                    {
                        if (fTemp > minFreq)
                        {
                            keyFreqs.Add(fTemp);
                        }
                    }
                    else
                    {
                        freq = fTemp;
                        break;
                    }

                    x0 = x1;
                    fTemp += step;
                }

                if (endFlag)
                {
                    break;
                }
            }

            return keyFreqs;
        }
        */

        private List<int> GenKeyFreqsIndex(float fr, int sample_size)
        {
            List<int> keyFreqIndex = new List<int>();

            int startX = (int)Math.Ceiling(MinFreq / fr);
            int maxX = (int)Math.Ceiling(MaxFreq / fr);
            int step = 1;

            bool endFlag = false;

            int maxIndex = maxX <= sample_size / 2 ? maxX : sample_size / 2;


            for (int index = startX; index <= maxIndex; )
            {
                float freq = index * fr;
                float x0 = CalcXFreq2Panel(freq);
                //计算合适的步长 初始值为1，2，3，4，5，6，7，8，9然后变成10，之后20，30，40，50……
                while (true)
                {
                    int tempIndex = index + step;
                    float tempF = tempIndex * fr;
                    if (tempIndex >= maxIndex)
                    {
                        endFlag = true;
                        break;
                    }
                    float x1 = CalcXFreq2Panel(tempF);
                    if (x1 - x0 >= MinKeyFreqGap)
                    {
                        //Console.WriteLine("步长:{0}", step);
                        //Trace.WriteLine("步长:" + step);
                        break;
                    }

                    step += 1;
                }

                if (endFlag)
                {
                    break;
                }

                for (int i0 = index; i0 < maxIndex; )
                {
                    int i1 = i0 + step;
                    if (i1 >= maxIndex)
                    {
                        endFlag = true;
                    }

                    float freq1 = i1 * fr;
                    float x1 = CalcXFreq2Panel(freq1);
                    float diff = x1 - x0;

                    if (diff >= MinKeyFreqGap)
                    {
                        keyFreqIndex.Add(i0);
                        //Console.WriteLine("key index = {0},  freq = {1}", i0, i0 * FR);
                    }
                    else
                    {
                        index = i0;
                        break;
                    }

                    x0 = x1;
                    i0 = i1;
                }

                if (endFlag)
                {
                    break;
                }
            }

            return keyFreqIndex;
        }

        public static string Freq2String(float freq)
        {
            if (freq < 1000)
            {
                return freq.ToString("0.0");
            }

            return (freq / 1000).ToString("0.00") + "K";
        }
    }
}
