using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.RTA.Core;
using AEC.RTA.Utils;

namespace AEC.RTA
{
    public class AudioAnalysis
    {
        private static AudioAnalysis _instance = null;
        public static AudioAnalysis GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AudioAnalysis();
            }

            return _instance;
        }

        static AudioAnalysis()
        {
            rtaParam = new AudioRTAParam();
            //sampleFreqDict = new Dictionary<string, int>()
            //{
            //    {sr48kHz, sr48k},
            //    {sr96kHz, sr96k},
            //};
        }

        public static AudioRTAParam rtaParam = null;

        public AudioIn _audioIn = null; //音频输入
        public AudioBuffer _audioBuffer = null;
        public AudioRTA _audioRTA = null;
        public MeasDelay _measDelay = null;
        public Display _display = null;
        public AudioOut _audioOut = null;

        private EventBus _rtaEventBus = null;


        private object signalOutputLock = new object();
        private SignalGenerator signalGenerator = null;

        //private RTAMode _rtaMode = RTAMode.Spectrum;
        //private SpectrumMap _spectrumMap = SpectrumMap.DoubleCh;
        //private FreqRespMap _freqRespMap = FreqRespMap.AmpRespAndPhaseResp;

        //public static Dictionary<string, int> sampleFreqDict = null;
        //public const string sr48kHz = "48kHz";
        //public const int sr48k = 48000;
        //public const string sr96kHz = "96kHz";
        //public const int sr96k = 96000;
        //public const string sr192kHz = "192kHz";
        //public const int sr192k = 192000;

        public void Init(EventBus rtaEventBus)
        {
            this._rtaEventBus = rtaEventBus;

            this._audioIn = new AudioIn();
            this._audioBuffer = new AudioBuffer();
            this._audioRTA = new AudioRTA();
            this._measDelay = new MeasDelay();
            this._display = new Display();
            this._audioOut = new AudioOut();

            this._audioBuffer.delay.UpdateSr(rtaParam.sampleRate);
            this._audioBuffer.delay.Init(rtaParam.sampleRate, rtaParam.delay);

            this._display.spectrumMap = rtaParam.spMap;
            this._display.freqRespMap = rtaParam.freqRespMap;
            this._display.refTrace.rtaMode = this.rtaMode;

            this.StartWrok();
            this.RegisterHandler();
        }

        private void RegisterHandler()
        {
            this._audioIn.RegisterAudioDataHandler(this._audioBuffer.RecvAudioDataHandler);

            this._audioBuffer.RegisterAudioBufferHandler(this._audioRTA.AudioDataHandler);
            this._audioBuffer.RegisterMeasDelayHandler(this._measDelay.MeasDelayHandler);
            this._audioBuffer.RegisterUpdateSignalLevelUIHandler(this.UpdateSignalLevelUIHandler);
            this._audioBuffer.RegisterRecvMeasDelayDataProgressHandler(this.RecvMeasDelayDataProgressHandler);

            this._audioRTA.RegisterOutputHandler(this._display.RTADataHandler);

            this._measDelay.RegisterMeasDelayCompletedHandler(this.MeasDelayCompletedHandler);

            this._display.RegisterDisplayHandler(this.UpdateRTADisplayHandler);

            this._audioOut.RegisterGetNextSamplesHandler(this.GetOutNextSamplesCallback);
        }

        private void RemoveHandler()
        {
            this._audioIn.RemoveAudioDataHandler(this._audioBuffer.RecvAudioDataHandler);

            this._audioBuffer.RemoveAudioBufferHandler(this._audioRTA.AudioDataHandler);
            this._audioBuffer.RemoveMeasDelayHandler(this._measDelay.MeasDelayHandler);
            this._audioBuffer.RemoveUpdateSignalLevelUIHandler(this.UpdateSignalLevelUIHandler);
            this._audioBuffer.RemoveRecvMeasDelayDataProgressHandler(this.RecvMeasDelayDataProgressHandler);

            this._audioRTA.RemoveOutputHandler(this._display.RTADataHandler);

            this._measDelay.RemoveMeasDelayCompletedHandler(this.MeasDelayCompletedHandler);

            this._display.RemoveDisplayHandler(this.UpdateRTADisplayHandler);

            this._audioOut.RemoveGetNextSamplesHandler(this.GetOutNextSamplesCallback);
        }

        private void UpdateSignalLevelUIHandler(object sender, EventArgs e)
        {
            SteroLevel steroLevel = sender as SteroLevel;
            if(steroLevel != null && rtaEventBus != null)
            {
                rtaEventBus.NotifyObeserver(new AudioInputLevelUpdatedArgs(steroLevel.leftCh, steroLevel.rightCh));
                //Console.WriteLine("left:{0}, right:{1}", steroLevel.leftCh, steroLevel.rightCh);
            }
        }

        private void RecvMeasDelayDataProgressHandler(object sender, EventArgs e)
        {
            MeasDelayAudioDataBuffer measDelayAudioDataBuffer = sender as MeasDelayAudioDataBuffer;
            if (measDelayAudioDataBuffer != null && rtaEventBus != null)
            {
                rtaEventBus.NotifyObeserver(new RecvMeasDelayDataProgressChangedArgs(measDelayAudioDataBuffer.progress));                
            }
        }

        public void StartRecvDataToMeasDelay()
        {
            _audioBuffer.StartRecvDataToMeasDelay();
        }

        private void UpdateRTADisplayHandler(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void MeasDelayCompletedHandler(object sender, EventArgs e)
        {
            short delayOffset = (short)sender;
            if (rtaEventBus != null)
            {
                rtaEventBus.NotifyObeserver(new MeasDelayCompletedArgs(delayOffset));
            }
        }

        public void UpdateDisplay()
        {
            if (this._rtaEventBus != null)
            {
                this._rtaEventBus.NotifyObeserver(new UpdateRTADisplayArgs());
            }
        }

        public void PaintRTAMap(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            AEC.Core.EQGraphicHelper graphicHelper = AEC.Core.EQGraphicHelper.GetCurrInstance();

            g.SetClip(graphicHelper.eqRect);

            this._display.DrawData(g);

            g.ResetClip();

            this._display.DrawCursorData(g, graphicHelper.topRect);
        }

        public void StartWrok()
        {
            this._display.Start();
            this._measDelay.Start();
            this._audioRTA.Start();
            this._audioBuffer.Start();
            this._audioIn.Start();
            this._audioOut.Start();
        }

        public void StopWrok()
        {
            if (this._audioIn != null)
            {
                this._audioIn.StopCapture();
            }

            if (this._audioOut != null)
            {
                this._audioOut.StopPlayback();
            }

            if (this._audioIn != null)
            {
                this._audioIn.SuspendRun();
            }

            if (this._audioBuffer != null)
            {
                this._audioBuffer.SuspendRun();
            }

            if (this._measDelay != null)
            {
                this._measDelay.SuspendRun();
            }

            if (this._audioRTA != null)
            {
                this._audioRTA.SuspendRun();
            }

            if (this._display != null)
            {
                this._display.SuspendRun();
            }

            if (this._audioOut != null)
            {
                this._audioOut.SuspendRun();
            }


            if (this._display != null)
            {
                this._display.Stop();
            }

            if (this._audioRTA != null)
            {
                this._audioRTA.Stop();
            }

            if (this._measDelay != null)
            {
                this._measDelay.Stop();
            }

            if (this._audioBuffer != null)
            {
                this._audioBuffer.Stop();
            }

            if (this._audioOut != null)
            {
                this._audioOut.Stop();
            }

            if (this._audioIn != null)
            {
                this._audioIn.Stop();
            }

            if (this._display != null)
            {
                this._display.SaveRefTrace();
            }
        }

        public void TurnOnRTA()
        {
            if (!this.rtaIsON)
            {
                List<AudioDevice> devices = new List<AudioDevice>(AudioIn.Devices);

                if (devices.Count > 0)
                {
                    string description = rtaParam.inputDeviceDescription;

                    int deviceIndex = 0;
                    //AudioDevice currDevice = null;

                    if (!string.IsNullOrEmpty(description))
                    {
                        deviceIndex = devices.FindIndex(delegate(AudioDevice device)
                        {
                            if (device.Name == description)
                            {
                                return true;
                            }

                            return false;
                        });

                        if (deviceIndex < 0)
                        {
                            deviceIndex = 0;
                        }
                    }

                    CaptureFormat format = new CaptureFormat(sampleRate_: rtaParam.sampleRate, bitsPerSample_: rtaParam.sampleBitDepth);

                    this.UpdateRTAContext();

                    ResumeAudioIn(devices[deviceIndex], format);
                }
            }
        }

        public void TurnOffRTA()
        {
            SuspendRTA();
            UpdateDisplay(); //更新显示
            this._rtaEventBus.NotifyObeserver(new RTAOperateCompletedArgs(RTAOperate.TurnOffRTA, new OperateResult(true)));
        }

        public void TurnOnAudioOut()
        {
            if(!audioOutIsON)
            {
                List<AudioDevice> devices = new List<AudioDevice>(AudioOut.Devices);

                if (devices.Count > 0)
                {
                    string description = rtaParam.outputDeviceDescription;

                    int deviceIndex = 0;
                    //AudioDevice currDevice = null;

                    if (!string.IsNullOrEmpty(description))
                    {
                        deviceIndex = devices.FindIndex(delegate(AudioDevice device)
                        {
                            if (device.Name == description)
                            {
                                return true;
                            }

                            return false;
                        });

                        if (deviceIndex < 0)
                        {
                            deviceIndex = 0;
                        }
                    }

                    InitSignalGenerator();
                    ResumeAudioOut(devices[deviceIndex], rtaParam.sampleRate);
                }
            }
        }

        public void TurnOffAudioOut()
        {
            SuspendAudioOut();
            ResetSignalGenerator();
            this._rtaEventBus.NotifyObeserver(new RTAOperateCompletedArgs(RTAOperate.TurnOffOutput, new OperateResult(true)));
        }

        private void ResumeAudioIn(AudioDevice device, CaptureFormat format)
        {
            if (!_display.rta_is_on)
            {
                try
                {
                    this._audioIn.StartCapture(device, format);
                    _display.rta_is_on = true;
                    //Console.WriteLine("audioAnalysis NotifyObeserver RTAOperateCompletedArgs RTAOperate.StartRTA");
                    this._rtaEventBus.NotifyObeserver(new RTAOperateCompletedArgs(RTAOperate.TurnOnRTA, new OperateResult(true)));
                    
                }
                catch (Exception e)
                {
                    //Console.WriteLine("audioAnalysis NotifyObeserver RTAOperateCompletedArgs RTAOperate.StartRTA failed");
                    this._rtaEventBus.NotifyObeserver(new RTAOperateCompletedArgs(RTAOperate.TurnOnRTA, new OperateResult(false, e.Message)));
                }
            }
        }

        private void SuspendRTA()
        {
            this._display.rta_is_on = false;
            this._audioIn.StopCapture();  //暂停音频信号输入
            this._audioBuffer.Reset(); //重置音频缓冲
        }

        private void ResumeAudioOut(AudioDevice device, int sr)
        {
            if (!audioOutIsON)
            {
                try
                {
                    this._audioOut.StartPlay(device, sr);
                    this._rtaEventBus.NotifyObeserver(new RTAOperateCompletedArgs(RTAOperate.TurnOnOutput, new OperateResult(true)));
                }
                catch (Exception e)
                {
                    this._rtaEventBus.NotifyObeserver(new RTAOperateCompletedArgs(RTAOperate.TurnOnOutput, new OperateResult(false, e.Message)));
                }
            }
        }

        private void SuspendAudioOut()
        {
            this._audioOut.StopPlayback();
        }

        public void SuspendAudioBuffer()
        {
            this.isSuspend = true;

            this._display.idleEvent.WaitOne(128);
            this._audioRTA.idleEvent.WaitOne(128);
            this._display.idleEvent.WaitOne(128);

        }

        public void ResumeAudioBuffer()
        {
            this.isSuspend = false;
        }

        public void UpdateRTAContext()
        {
            this._audioBuffer.rtaContext.Update(rtaParam.rtaMode, rtaParam.sr, rtaParam.rtaFftSize);
            this._audioRTA.rtaContext.CopyFrom(this._audioBuffer.rtaContext);
            this._display.rtaContext.CopyFrom(this._audioBuffer.rtaContext);
        }

        public void WaitIdle()
        {
            //System.Threading.EventWaitHandle[] events = new System.Threading.EventWaitHandle[3];
            this._display.idleEvent.WaitOne(100);
            this._audioRTA.idleEvent.WaitOne(100);
            this._audioIn.idleEvent.WaitOne(100);
        }

        public Delay delay
        {
            get
            {
                return this._audioBuffer.delay;
            }
        }

        public bool isSuspend
        {
            get
            {
                return _audioBuffer.isSuspend;
            }
            set
            {
                //_isPause = value;
                //_display.isPause = _isPause;
                _audioBuffer.isSuspend = value;
            }
        }

        public bool isPaused
        {
            get
            {
                return _audioBuffer.isPaused;
            }
            set
            {
                //_isPause = value;
                //_display.isPause = _isPause;
                _audioBuffer.isPaused = value;
            }
        }

        public bool rtaIsON
        {
            get
            {
                if (_display != null)
                {
                    return _display.rta_is_on;
                }

                return false;
            }
        }

        public bool audioOutIsON
        {
            get
            {
                if (_audioOut != null)
                {
                    return _audioOut.PlaybackState != PlaybackState.Stopped;
                }

                return false;
            }
        }

        private bool isShowRefTrace
        {
            get
            {
                if(_display != null && _display.refTrace != null)
                {
                    return _display.refTrace.isShowRefTrace;
                }

                return false;
            }
        }

        public bool displayIsOn
        {
            get
            {
                return rtaIsON || isShowRefTrace;
            }
        }

        public RTAMode rtaMode
        {
            get
            {
                return rtaParam.rtaMode;
            }
            set
            {
                rtaParam.rtaMode = value;
                //_audioRTA._rtaMode = value;
                //_display._rtaMode = value;
                _display.refTrace.rtaMode = value;
            }
        }

        public SpectrumMap spectrumMap
        {
            get
            {
                return rtaParam.spMap;
            }
            set
            {
                rtaParam.spMap = value;
                _display.spectrumMap = value;
            }
        }

        public FreqRespMap freqRespMap
        {
            get
            {
                return rtaParam.freqRespMap;
            }
            set
            {
                rtaParam.freqRespMap = value;
                _display.freqRespMap = value;
            }
        }

        public ReferenceTrace refTrace
        {
            get
            {
                return _display.refTrace;
            }
        }

        public void UpdateDisplaySize()
        {
            ///_display.UpdateDisplaySize();
            if (refTrace.isShowRefTrace)
            {
                UpdateRefTraceDisplay();
            }
            else if (rtaIsON)
            {
                this.RunOnceDisplay();
            }
        }

        //public void UpdateDisplay()
        //{
        //    this._display.DisplayOutput();
        //}

        public void UpdateRefTraceDisplay()
        {
            this.SetUpdateRefTrace();
            this.RunOnceDisplay();
        }

        public void MouseMove(Point location)
        {
            AEC.Core.EQGraphicHelper graphicHelper = AEC.Core.EQGraphicHelper.GetCurrInstance();

            if(location.X > graphicHelper.eqRect.Right)
            {
                location.X = graphicHelper.eqRect.Right;
            }

            this._display.currCross.Update(true, location.X);

            if(this.isPaused)
            {
                this._display.DisplayOutput();
            }
        }

        public void MouseLeave()
        {
            this._display.currCross.Update();
        }

        //触发运行一次并且更新显示
        public void RunOnceDisplay()
        {
            _display.RunOnce();
        }

        //将所有组参考曲线都设置为需要更新
        public void SetUpdateRefTrace()
        {
            foreach (RefTraceGroupNum gn in ReferenceTrace.all_group)
            {
                if (refTrace.GetGroup(gn).isShow && refTrace.GetGroup(gn).GetCurrTrace().isValid)
                {
                    refTrace.GetGroup(gn).isNeedtoUpdate = true;
                }
            }
        }

        //public void UpdateRTAParams()
        //{

        //}

        public void CaptureSpectrum(RefTraceGroupNum gn, AudioChannel chan)
        {
            SpectrumGroupTrace sgt = refTrace.GetSpectrumGroup(gn);

            lock (sgt.data_lock)
            {
                SpectrumInfo st = (SpectrumInfo)sgt.GetCurrTrace();
                int ch = (int)chan;
                if (chan == AudioChannel.LeftCh)
                {
                    st.amp = Utility.CopyArray(_display.signalAmp.left);//ArrayCopy
                }
                else
                {
                    st.amp = Utility.CopyArray(_display.signalAmp.right);//ArrayCopy
                }
                st.chan = chan;
                st.UpdateFromAudioParam(AudioAnalysis.rtaParam);
                st.delay_time = _audioBuffer.delay.delayTime;
                if (chan == AudioChannel.LeftCh)
                {
                    if (rtaParam.spScale == SpectrumScale.Log)
                    {
                        sgt.pointList = _display._spectrumLeftChPoint;
                    }
                    else
                    {
                        sgt.octaveBands = Utility.CopyRectArray(_display.leftOctaveBands);
                    }
                }
                else
                {
                    if (rtaParam.spScale == SpectrumScale.Log)
                    {
                        sgt.pointList = _display._spectrumRightChPoint;
                    }
                    else
                    {
                        sgt.octaveBands = Utility.CopyRectArray(_display.rightOctaveBands);
                    }
                }
            }
        }

        public void CaptureTransfer(RefTraceGroupNum gn)
        {
            TransferGroupTrace sgt = refTrace.GetTransferGroup(gn);
            lock (sgt.data_lock)
            {
                TransferInfo tf = (TransferInfo)sgt.GetCurrTrace();
                tf.isFPPO = _display.isFPPO;
                if (tf.isFPPO)
                {
                    tf.fppoFreqResp = new FPPO.FreqResp(_display.fppoFreqResp.freqs);
                    Array.Copy(_display.fppoFreqResp.magnitudes, tf.fppoFreqResp.magnitudes, _display.fppoFreqResp.length);
                    Array.Copy(_display.fppoFreqResp.phases, tf.fppoFreqResp.phases, _display.fppoFreqResp.length);
                }
                else
                {
                    tf.transfer = new FrequencyResponse(_display.freqResp.length);
                    Array.Copy(_display.freqResp.amp, tf.transfer.amp, _display.freqResp.length);
                    Array.Copy(_display.freqResp.phase, tf.transfer.phase, _display.freqResp.length);
                    //tf.transfer_mag = Utility.CopyArray(_display.freqResp.amp);
                    //tf.transfer_phase = Utility.CopyArray(_display.freqResp.phase);
                }

                tf.UpdateFromAudioParam(AudioAnalysis.rtaParam);
                tf.delay_time = _audioBuffer.delay.delayTime;
                sgt.magPointList = _display._transfer_mag_Point;
                sgt.phasePointList = _display._transfer_phase_Point;
            }
        }

        public void EraseTrace(RefTraceGroupNum gn)
        {
            if (rtaMode == RTAMode.Spectrum)
            {
                this._display.EraseSpectrumTrace(gn);
            }
            else
            {
                this._display.EraseTransferTrace(gn);
            }
        }

        public EventBus rtaEventBus
        {
            get
            {
                return _rtaEventBus;
            }
        }

        private void InitSignalGenerator()
        {
            lock (signalOutputLock)
            {
                signalGenerator = rtaParam.CreateSignalGenerator();
            }
        }

        private void ResetSignalGenerator()
        {
            lock (signalOutputLock)
            {
                signalGenerator = null;
            }
        }

        public void OutputSignalTypeChangedCallback()
        {
            if (this.audioOutIsON)
            {
                InitSignalGenerator();
            }
        }

        public void SampleRateChangedCallback()
        {
            if (rtaParam.outputSignalType == OutputSignalType.Sine ||
                rtaParam.outputSignalType == OutputSignalType.DualSine)
            {
                lock (signalOutputLock)
                {
                    signalGenerator = rtaParam.CreateSignalGenerator();
                }
            }
        }

        public byte[] GetOutNextSamplesCallback(int length)
        {
            byte[] samples = null;

            lock (signalOutputLock)
            {
                if (signalGenerator != null)
                {
                    samples = signalGenerator.GetStereoSignal(length);
                }
            }

            return samples;
        }

        public void OutputPinkNoiseLevelChangedCallback()
        {
            lock (signalOutputLock)
            {
                if (this.audioOutIsON && signalGenerator != null && signalGenerator.type == OutputSignalType.PinkNoise)
                {
                    (signalGenerator as PinkNoiseGenerator).levelIndB = rtaParam.pinkNoiseLevel.level;
                }
            }
        }

        public void OutputPinkSweepLevelChangedCallback()
        {
            lock (signalOutputLock)
            {
                if (this.audioOutIsON && signalGenerator != null && signalGenerator.type == OutputSignalType.PinkSweep)
                {
                    (signalGenerator as PinkSweepGenerator).levelIndB = rtaParam.pinkSweepLevel.level;
                }
            }
        }

        public void OutputSineLevelChangedCallback()
        {
            lock (signalOutputLock)
            {
                if (this.audioOutIsON && signalGenerator != null && signalGenerator.type == OutputSignalType.Sine)
                {
                    (signalGenerator as SineGenerator).levelIndB = rtaParam.sineLevel.level;
                }
            }
        }

        public void OutputSineFreqChangedCallback()
        {
            lock (signalOutputLock)
            {
                if (this.audioOutIsON && signalGenerator != null && signalGenerator.type == OutputSignalType.Sine)
                {
                    (signalGenerator as SineGenerator).freq = rtaParam.sineFreq.freq;
                }
            }
        }

        public void OutputDualSine1LevelChangedCallback()
        {
            lock (signalOutputLock)
            {
                if (this.audioOutIsON && signalGenerator != null && signalGenerator.type == OutputSignalType.DualSine)
                {
                    (signalGenerator as DualSineGenerator).sine1LevelIndB = rtaParam.dualSine1Level.level;
                }
            }
        }

        public void OutputDualSine1FreqChangedCallback()
        {
            lock (signalOutputLock)
            {
                if (this.audioOutIsON && signalGenerator.type == OutputSignalType.DualSine)
                {
                    (signalGenerator as DualSineGenerator).sine1freq = rtaParam.dualSine1Freq.freq;
                }
            }
        }

        public void OutputDualSine2LevelChangedCallback()
        {
            lock (signalOutputLock)
            {
                if (this.audioOutIsON && signalGenerator != null && signalGenerator.type == OutputSignalType.DualSine)
                {
                    (signalGenerator as DualSineGenerator).sine2LevelIndB = rtaParam.dualSine2Level.level;
                }
            }
        }

        public void OutputDualSine2FreqChangedCallback()
        {
            lock (signalOutputLock)
            {
                if (this.audioOutIsON && signalGenerator.type == OutputSignalType.DualSine)
                {
                    (signalGenerator as DualSineGenerator).sine2freq = rtaParam.dualSine2Freq.freq;
                }
            }
        }
    }
}
