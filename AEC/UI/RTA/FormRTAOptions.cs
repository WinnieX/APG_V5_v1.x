using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AEC.Core;
using AEC.RTA;
using AEC.RTA.Core;
using AEC.Misc;
using AEC.UI.Skin;
using AEC.UI.Utility;

namespace AEC.UI.RTA
{
    public partial class FormRTAOptions : Form
    {
        private delegate void VoidCallback();
        private AudioAnalysis audioAnalysis = AudioAnalysis.GetInstance();
        
        private SynchronizationContext syncContext;
        private Delay currDelay = null;

        private static readonly Dictionary<string, object> srMap = new Dictionary<string, object>()
        {
            {"48 kHz", RTASampleRate.sr48k},
            {"96 kHz", RTASampleRate.sr96k},
        };

        private static readonly Dictionary<string, object> spFftSizeMap = new Dictionary<string, object>()
        {
            {"32 k", FFTSize.fftSize32k},
            {"16 k", FFTSize.fftSize16k},
            {"8 k", FFTSize.fftSize8k},
        };

        private static readonly Dictionary<string, object> tfFftSizeMap = new Dictionary<string, object>()
        {
            {"FPPO", FFTSize.FPPO},
            {"32 k", FFTSize.fftSize32k},
            {"16 k", FFTSize.fftSize16k},
            {"8 k", FFTSize.fftSize8k},
        };

        public static readonly AudioRTAParam rtaParam = AudioAnalysis.rtaParam;

        private Dictionary<ComboBox, VoidCallback> cmbSelectedValueChangedMap;


        private bool needResumeAudioBuffer = false;
        private bool needRestartRTA = false;
        private bool needRestartAudioOutput = false;
        private bool isActuallyClose = false;

        private void SuspendAudioBuffer()
        {
            needResumeAudioBuffer = audioAnalysis.rtaIsON && !audioAnalysis.isSuspend;

            if(needResumeAudioBuffer)
            {
                this.audioAnalysis.SuspendAudioBuffer();
                //Console.WriteLine("中断处理");
            }
        }

        private void ResumeAudioBuffer()
        {
            if (needResumeAudioBuffer)
            {
                this.audioAnalysis.ResumeAudioBuffer();
            }
        }

        private void StopRTA()
        {
            needRestartRTA = audioAnalysis.rtaIsON;

            if (needRestartRTA)
            {
                audioAnalysis.TurnOffRTA();
            }
        }

        private void RestartRTA()
        {
            if (needRestartRTA)
            {
                audioAnalysis.TurnOnRTA();
            }
        }

        private void StopAudioOutput()
        {
            needRestartAudioOutput = audioAnalysis.audioOutIsON;

            if (needRestartAudioOutput)
            {
                audioAnalysis.TurnOffAudioOut();
            }
        }

        private void RestartAudioOutput()
        {
            if (needRestartAudioOutput)
            {
                audioAnalysis.TurnOnAudioOut();
            }
        }

        public FormRTAOptions()
        {
            InitializeComponent();
            this.title.BindingForm(this);
            this.InitFont();

            this.syncContext = WindowsFormsSynchronizationContext.Current;

            this.cmbSelectedValueChangedMap = new Dictionary<ComboBox, VoidCallback>()
            {
                {cmbAudioInDevice, AudioInDeviceChanged},
                {cmbSampleRate, SampleRateChanged},
                {cmbAudioOutDevice, AudioOutDeviceChanged},
                {cmbFFTSize, FFTSizeChanged},
                {cmbWindowFunction, WindowFunctionChanged},
                {cmbAverage, AverageChanged},
                {cmbSmooth, SmoothChanged},
                {cmbScale, ScaleChanged},
                {cmbOutputSignal, OutputSignalChanged},
            };
        }

        private void InitFont()
        {
            this.title.Font = FontHelper.CreateFont10();
            this.panelAudioIO.Font = FontHelper.CreateFont10();
            this.panelRTAParams.Font = FontHelper.CreateFont10();
            this.panelSignalLevel.Font = FontHelper.CreateFont10();
            this.panelDelay.Font = FontHelper.CreateFont10();
            this.panelSignalGenerator.Font = FontHelper.CreateFont10();
            this.panelRefTrace.Font = FontHelper.CreateFont10();
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.title.Text = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GDIHelper.FillRectangle(e.Graphics, this.ClientRectangle, SkinManager.CurrentSkin.proModeBackgroundColor);

            int borderWidth = 1;
            using (Pen border = new Pen(Color.FromArgb(96, 96, 96), borderWidth))
            {
                Point[] points = new Point[]
                {
                    new Point(borderWidth - 1, borderWidth - 1),
                    new Point(borderWidth - 1, this.Height - (borderWidth)),
                    new Point(this.Width - (borderWidth), this.Height - (borderWidth)),
                    new Point(this.Width - (borderWidth), borderWidth - 1),
                };
                e.Graphics.DrawLines(border, points);
            }
        }

        private ReferenceTrace refTrace
        {
            get
            {
                return audioAnalysis._display.refTrace;
            }
        }

        private bool isShowRefTrace
        {
            get
            {
                return audioAnalysis._display.refTrace.isShowRefTrace;
            }
            set
            {
                audioAnalysis._display.refTrace.isShowRefTrace = value;
            }
        }

        public bool traceGroupEnabled
        {
            set
            {
                this.traceGroupA.Enabled = value;
                this.traceGroupB.Enabled = value;
                this.traceGroupC.Enabled = value;
                this.traceGroupD.Enabled = value;
                this.traceGroupE.Enabled = value;
            }
        }

        private void UpdateTraceComment()
        {
            //this.txtTraceComment.TextChanged -= txtTraceComment_TextChanged;
            //this.txtTraceComment.Text = comment;
            //this.txtTraceComment.TextChanged += txtTraceComment_TextChanged;

            if (refTrace.GetCurrGroup().CurrTraceIsValid)
            {
                //UpdateTraceComment(refTrace.GetCurrGroup().GetCurrTrace().comment);
            }
            else
            {
                //UpdateTraceComment(string.Empty);
            }
        }

        private TraceGroup GetGroup(RefTraceGroupNum gn)
        {
            switch (gn)
            {
                case RefTraceGroupNum.GroupA:
                    return traceGroupA;

                case RefTraceGroupNum.GroupB:
                    return traceGroupB;

                case RefTraceGroupNum.GroupC:
                    return traceGroupC;

                case RefTraceGroupNum.GroupD:
                    return traceGroupD;

                case RefTraceGroupNum.GroupE:
                    return traceGroupE;
            }

            return traceGroupA;
        }

        private bool outputIsReleated2SampleRate
        {
            get
            {
                return rtaParam.outputSignalType == OutputSignalType.PinkSweep ||
                                rtaParam.outputSignalType == OutputSignalType.Sine ||
                                rtaParam.outputSignalType == OutputSignalType.DualSine;
            }
        }

        private void FormRTAOption_Load(object sender, EventArgs e)
        {
            this.InitUI();
            this.RegisterEventHandler();
            this.UpdateDisplayLanguage();
        }

        private void FormRTAOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = !isActuallyClose;
        }

        private void FormRTAOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveEventHandler();
        }

        public new void Close()
        {
            this.isActuallyClose = true;
            base.Close();
        }

        private void InitUI()
        {
            InitRTASwitch();
            InitRTAPause();
            InitRdoRTAMode();
            RefreshInputDevice();
            RefreshOutputDevice();
            InitCmbSampleRate();
            InitRdoBitsPerSample();
            InitCmbFftSize();
            InitCmbWindowFunction();
            InitCmbAverage();
            InitCmbSmooth();
            InitCmbScale();
            InitRdoSpectrumGraph();
            InitParamLabelAmpOffset();
            InitRdoFreqRespGraph();
            UpdateRTAParamPanel();
            InitCmbOutputSignal();
            InitOutputSwitch();
            InitSignalInputLevel();
            InitParamLabelDelay();
            InitRefTracePanel();
        }

        private void RegisterEventHandler()
        {
            RegisterRdoEvents();
            RegisterCmbEvents();
            RegisterParamLabelChanged();
            //RegisterAudioOutGetNextSamplesHandler();
            RegisterTraceGroupHandler();
            RegisterEventBusHandler();
        }

        private void RemoveEventHandler()
        {
            RemoveRdoEvents();
            RemoveCmbEvents();
            RemoveParamLabelChanged();
            //RemoveAudioOutGetNextSamplesHandler();
            RemoveTraceGroupHandler();
            RemoveEventBusHandler();
        }

        private void RegisterTraceGroupHandler()
        {
            this.traceGroupA.GroupShowChangedHandler += GroupShowChanged;
            this.traceGroupA.GroupCurrIndexChangedHandler += GroupCurrIndexChanged;
            this.traceGroupB.GroupShowChangedHandler += GroupShowChanged;
            this.traceGroupB.GroupCurrIndexChangedHandler += GroupCurrIndexChanged;
            this.traceGroupC.GroupShowChangedHandler += GroupShowChanged;
            this.traceGroupC.GroupCurrIndexChangedHandler += GroupCurrIndexChanged;
            this.traceGroupD.GroupShowChangedHandler += GroupShowChanged;
            this.traceGroupD.GroupCurrIndexChangedHandler += GroupCurrIndexChanged;
            this.traceGroupE.GroupShowChangedHandler += GroupShowChanged;
            this.traceGroupE.GroupCurrIndexChangedHandler += GroupCurrIndexChanged;
        }

        private void RemoveTraceGroupHandler()
        {
            this.traceGroupA.GroupShowChangedHandler -= GroupShowChanged;
            this.traceGroupA.GroupCurrIndexChangedHandler -= GroupCurrIndexChanged;
            this.traceGroupB.GroupShowChangedHandler -= GroupShowChanged;
            this.traceGroupB.GroupCurrIndexChangedHandler -= GroupCurrIndexChanged;
            this.traceGroupC.GroupShowChangedHandler -= GroupShowChanged;
            this.traceGroupC.GroupCurrIndexChangedHandler -= GroupCurrIndexChanged;
            this.traceGroupD.GroupShowChangedHandler -= GroupShowChanged;
            this.traceGroupD.GroupCurrIndexChangedHandler -= GroupCurrIndexChanged;
            this.traceGroupE.GroupShowChangedHandler -= GroupShowChanged;
            this.traceGroupE.GroupCurrIndexChangedHandler -= GroupCurrIndexChanged;
        }

        private void RegisterEventBusHandler()
        {
            if(this.audioAnalysis.rtaEventBus != null)
            {
                this.audioAnalysis.rtaEventBus.RegisterHandler(RTAEventBusHandler);
            }
            this.localization.langEventBus.RegisterHandler(LangEventBusHandler);
        }

        private void RemoveEventBusHandler()
        {
            if (this.audioAnalysis != null)
            {
                this.audioAnalysis.rtaEventBus.RemoveHandler(RTAEventBusHandler);
            }
            this.localization.langEventBus.RemoveHandler(LangEventBusHandler);
        }

        private void LangEventBusHandler(object sender, EventBusArgs e)
        {
            UpdateDisplayLanguage();
        }

        public void RTAEventBusHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(RTAEventBusHandlerCallback, e);
        }

        private void RTAEventBusHandlerCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if(e == null)
            {
                return;
            }

            switch(e.type)
            {
                case EventType.RTATryOperate:
                    //Console.WriteLine("FormRTA Try Operate");
                    RTATryOperateCallback((e as RTATryOperateArgs).rtaOperate);
                    break;

                case EventType.RTAOperateCompleted:
                    //Console.WriteLine("FormRTA Operate Completed");
                    RTAOperateCompletedCallback(e as RTAOperateCompletedArgs);
                    break;

                case EventType.MeasDelayCompleted:
                    short delayOffset = (e as MeasDelayCompletedArgs).delayOffset;
                    currDelay = new Delay(rtaParam.sampleRate);
                    currDelay.delayOffset = delayOffset;
                    this.paramLabelMeasDelayResult.Init(Delay.maxOffset, currDelay.delayOffset, currDelay.GetDelayLabel);
                    this.MeasDelayCompletedCallback();
                    break;

                case EventType.AudioInputLevelUpdated:
                    AudioInputLevelUpdatedArgs ail = e as AudioInputLevelUpdatedArgs;
                    this.levelLeft.level = ail.left;
                    this.levelRight.level = ail.right;
                    break;

                case EventType.RecvMeasDelayDataProgressChanged:
                    progressBar.progress = (e as RecvMeasDelayDataProgressChangedArgs).progress;
                    break;
            }
        }

        private void RTATryOperateCallback(RTAOperate rtaOperate)
        {
            switch (rtaOperate)
            {
                case RTAOperate.TurnOnRTA:
                    this.cmbAudioInDevice.Enabled = false;
                    this.btnRefreshInputDevice.Enabled = false;
                    this.cmbSampleRate.Enabled = false;
                    this.rdoBitsPerSample16.Enabled = false;
                    this.rdoBitsPerSample24.Enabled = false;
                    this.rtaSwitch.Enabled = false;
                    this.rtaPause.Enabled = false;
                    //Console.WriteLine("RTA Try Start:false");
                    break;

                case RTAOperate.TurnOffRTA:
                    this.rtaSwitch.Enabled = false;
                    this.rtaPause.Enabled = false;
                    break;

                case RTAOperate.TurnOnOutput:
                    this.cmbAudioOutDevice.Enabled = false;
                    this.btnRefreshOutputDevice.Enabled = false;
                    this.audioAnalysis.TurnOnAudioOut();
                    break;

                case RTAOperate.TurnOffOutput:
                    this.audioAnalysis.TurnOffAudioOut();
                    break;
            }
        }

        private void RTAOperateCompletedCallback(RTAOperateCompletedArgs e)
        {
            switch (e.operate)
            {
                case RTAOperate.TurnOnRTA:
                case RTAOperate.TurnOffRTA:
                    RTAOperateCompletedHandler(e);
                    break;

                case RTAOperate.TurnOnOutput:
                case RTAOperate.TurnOffOutput:
                    AudioOutputOperateCompletedHandler(e);
                    break;
            }
        }

        private void RTAOperateCompletedHandler(RTAOperateCompletedArgs e)
        {
            if (e.operate == RTAOperate.TurnOnRTA)
            {
                this.cmbAudioInDevice.Enabled = true;
                this.btnRefreshInputDevice.Enabled = true;
                this.cmbSampleRate.Enabled = true;
                this.rdoBitsPerSample16.Enabled = true;
                this.rdoBitsPerSample24.Enabled = true;
                this.rtaSwitch.Enabled = true;
                this.rtaPause.Enabled = true;

                if (e.result.success)
                {
                    RTAStartCallback();
                }
            }            
            else if (e.operate == RTAOperate.TurnOffRTA)
            {
                if(e.result.success)
                {
                    RTAStopCallback();
                }
            }
        }

        private void AudioOutputOperateCompletedHandler(RTAOperateCompletedArgs e)
        {
            if(e.operate == RTAOperate.TurnOnOutput && !e.result.success)
            {
                gtMessageBox.ShowMessage(this, string.Format(localization[Localization.rta_FailedToStartCapturingAudio], e.result.error), "RTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.outputSwitch.Enabled = true;
            this.outputSwitch.isOn = audioAnalysis.audioOutIsON;
            this.btnRefreshOutputDevice.Enabled = !audioAnalysis.audioOutIsON;
            this.cmbAudioOutDevice.Enabled = true;
            if (audioAnalysis.audioOutIsON && rtaParam.outputSignalType == OutputSignalType.PinkSweep)
            {
                rtaParam.inputSignal = InputSignal.Sweep;
                cmbWindowFunction.Enabled = false;
            }
            else
            {
                rtaParam.inputSignal = InputSignal.Normal;
                cmbWindowFunction.Enabled = true;
            }
        }

        private void RegisterRdoEvents()
        {
            this.rdoSpectrum.CheckedChanged += rdo_CheckedChanged;
            this.rdoTransferFunction.CheckedChanged += rdo_CheckedChanged;
            this.rdoBitsPerSample16.CheckedChanged += rdo_CheckedChanged;
            this.rdoBitsPerSample24.CheckedChanged += rdo_CheckedChanged;
            this.rdoDoubleCh.CheckedChanged += rdo_CheckedChanged;
            this.rdoLeftCh.CheckedChanged += rdo_CheckedChanged;
            this.rdoRightCh.CheckedChanged += rdo_CheckedChanged;
            this.rdoAmpAndPhaseResp.CheckedChanged += rdo_CheckedChanged;
            this.rdoAmpResp.CheckedChanged += rdo_CheckedChanged;
            this.rdoPhaseResp.CheckedChanged += rdo_CheckedChanged;
        }

        private void RemoveRdoEvents()
        {
            this.rdoSpectrum.CheckedChanged -= rdo_CheckedChanged;
            this.rdoTransferFunction.CheckedChanged -= rdo_CheckedChanged;
            this.rdoBitsPerSample16.CheckedChanged -= rdo_CheckedChanged;
            this.rdoBitsPerSample24.CheckedChanged -= rdo_CheckedChanged;
            this.rdoDoubleCh.CheckedChanged -= rdo_CheckedChanged;
            this.rdoLeftCh.CheckedChanged -= rdo_CheckedChanged;
            this.rdoRightCh.CheckedChanged -= rdo_CheckedChanged;
            this.rdoAmpAndPhaseResp.CheckedChanged -= rdo_CheckedChanged;
            this.rdoAmpResp.CheckedChanged -= rdo_CheckedChanged;
            this.rdoPhaseResp.CheckedChanged -= rdo_CheckedChanged;
        }

        private void RegisterCmbEvents()
        {
            this.cmbAudioInDevice.SelectedValueChanged += cmb_SelectedValueChanged;
            this.cmbSampleRate.SelectedValueChanged += cmb_SelectedValueChanged;
            this.cmbAudioOutDevice.SelectedValueChanged += cmb_SelectedValueChanged;
            this.cmbFFTSize.SelectedValueChanged += cmb_SelectedValueChanged;
            this.cmbWindowFunction.SelectedValueChanged += cmb_SelectedValueChanged;
            this.cmbAverage.SelectedValueChanged += cmb_SelectedValueChanged;
            this.cmbSmooth.SelectedValueChanged += cmb_SelectedValueChanged;
            this.cmbScale.SelectedValueChanged += cmb_SelectedValueChanged;
            this.cmbOutputSignal.SelectedValueChanged += cmb_SelectedValueChanged;
        }

        private void RemoveCmbEvents()
        {
            this.cmbAudioInDevice.SelectedValueChanged -= cmb_SelectedValueChanged;
            this.cmbSampleRate.SelectedValueChanged -= cmb_SelectedValueChanged;
            this.cmbAudioOutDevice.SelectedValueChanged -= cmb_SelectedValueChanged;
            this.cmbFFTSize.SelectedValueChanged -= cmb_SelectedValueChanged;
            this.cmbWindowFunction.SelectedValueChanged -= cmb_SelectedValueChanged;
            this.cmbAverage.SelectedValueChanged -= cmb_SelectedValueChanged;
            this.cmbSmooth.SelectedValueChanged -= cmb_SelectedValueChanged;
            this.cmbScale.SelectedValueChanged -= cmb_SelectedValueChanged;
            this.cmbOutputSignal.SelectedValueChanged -= cmb_SelectedValueChanged;
        }

        private void RegisterParamLabelChanged()
        {
            this.paramLabelAmpOffset.ValueChanged += paramLabel_ValueChanged;
            this.paramLabelPinkLevel.ValueChanged += paramLabel_ValueChanged;
            this.paramLabelSine1Level.ValueChanged += paramLabel_ValueChanged;
            this.paramLabelSine1Freq.ValueChanged += paramLabel_ValueChanged;
            this.paramLabelSine2Level.ValueChanged += paramLabel_ValueChanged;
            this.paramLabelSine2Freq.ValueChanged += paramLabel_ValueChanged;
            this.paramLabelDelay.ValueChanged += paramLabel_ValueChanged;
        }

        private void RemoveParamLabelChanged()
        {
            this.paramLabelAmpOffset.ValueChanged -= paramLabel_ValueChanged;
            this.paramLabelPinkLevel.ValueChanged -= paramLabel_ValueChanged;
            this.paramLabelSine1Level.ValueChanged -= paramLabel_ValueChanged;
            this.paramLabelSine1Freq.ValueChanged -= paramLabel_ValueChanged;
            this.paramLabelSine2Level.ValueChanged -= paramLabel_ValueChanged;
            this.paramLabelSine2Freq.ValueChanged -= paramLabel_ValueChanged;
            this.paramLabelDelay.ValueChanged -= paramLabel_ValueChanged;
        }

        private void rdo_CheckedChanged(object sender, EventArgs e)
        {
            gtRadioButton rdo = sender as gtRadioButton;
            if(!rdo.isChecked)
            {
                return;
            }

            if(rdo == this.rdoSpectrum)
            {
                SuspendAudioBuffer();

                audioAnalysis.rtaMode = RTAMode.Spectrum;
                UpdateRTAModeUI();

                ResumeAudioBuffer();
            }
            else if(rdo == this.rdoTransferFunction)
            {
                SuspendAudioBuffer();

                audioAnalysis.rtaMode = RTAMode.Transfer;
                UpdateRTAModeUI();

                ResumeAudioBuffer();
            }
            else if(rdo == this.rdoBitsPerSample16)
            {
                StopRTA();

                rtaParam.sbd = RTASampleBitDepth.bd16Bit;

                RestartRTA();
            }
            else if (rdo == this.rdoBitsPerSample24)
            {
                StopRTA();

                rtaParam.sbd = RTASampleBitDepth.bd24Bit;

                RestartRTA();
            }
            else if (rdo == this.rdoDoubleCh)
            {
                audioAnalysis.spectrumMap = SpectrumMap.DoubleCh;
                this.DoUpdateDiplay();
            }
            else if (rdo == this.rdoLeftCh)
            {
                audioAnalysis.spectrumMap = SpectrumMap.LeftCh;
                this.DoUpdateDiplay();
            }
            else if (rdo == this.rdoRightCh)
            {
                audioAnalysis.spectrumMap = SpectrumMap.RightCh;
                this.DoUpdateDiplay();
            }
            else if (rdo == this.rdoAmpAndPhaseResp)
            {
                audioAnalysis.freqRespMap = FreqRespMap.AmpRespAndPhaseResp;
                this.DoUpdateDiplay();
            }
            else if (rdo == this.rdoAmpResp)
            {
                audioAnalysis.freqRespMap = FreqRespMap.AmpResp;
                this.DoUpdateDiplay();
            }
            else if (rdo == this.rdoPhaseResp)
            {
                audioAnalysis.freqRespMap = FreqRespMap.PhaseResp;
                this.DoUpdateDiplay();
            }
        }

        private void cmb_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if(cmb == null)
            {
                return;
            }

            if (this.cmbSelectedValueChangedMap.ContainsKey(cmb))
            {
                this.cmbSelectedValueChangedMap[cmb]();
            }

            //if(sender.Equals(this.cmbAudioInDevice))
            //{

            //}
            //else if (sender.Equals(this.cmbSampleRate))
            //{

            //}
            //else if (sender.Equals(this.cmbAudioOutDevice))
            //{

            //}
        }

        private void AudioInDeviceChanged()
        {
            StopRTA();

            AudioDevice device = this.cmbAudioInDevice.SelectedValue as AudioDevice;
            if(device != null)
            {
                rtaParam.inputDeviceDescription = device.Name;
            }

            RestartRTA();
        }

        private void SampleRateChanged()
        {
            StopRTA();

            if(outputIsReleated2SampleRate)
            {
                StopAudioOutput();
            }

            rtaParam.sr = (RTASampleRate)this.cmbSampleRate.SelectedValue;
            audioAnalysis.delay.UpdateSr(rtaParam.sampleRate);
            this.paramLabelDelay.Init(Delay.maxOffset, (short)audioAnalysis.delay.delayOffset, audioAnalysis.delay.GetDelayLabel, audioAnalysis.delay.CalcDelayIndex);

            if (outputIsReleated2SampleRate)
            {
                RestartAudioOutput();
            }

            RestartRTA();
        }

        private void AudioOutDeviceChanged()
        {
            AudioDevice device = this.cmbAudioOutDevice.SelectedValue as AudioDevice;
            if (device != null)
            {
                rtaParam.outputDeviceDescription = device.Name;
            }
        }

        private void FFTSizeChanged()
        {
            SuspendAudioBuffer();

            if(rtaParam.outputSignalType == OutputSignalType.PinkSweep)
            {
                StopAudioOutput();
            }
            

            FFTSize fftSize = (FFTSize)this.cmbFFTSize.SelectedValue;
            rtaParam.rtaFftSize = fftSize;

            if (rtaParam.outputSignalType == OutputSignalType.PinkSweep)
            {
                RestartAudioOutput();
            }
            
            ResumeAudioBuffer();
        }

        private void WindowFunctionChanged()
        {
            SuspendAudioBuffer();

            WindowFunction wf = (WindowFunction)this.cmbWindowFunction.SelectedValue;
            rtaParam.rtaWindow = wf;

            ResumeAudioBuffer();
        }

        private void AverageChanged()
        {
            Averager.AverageMode averageMode = (Averager.AverageMode)this.cmbAverage.SelectedValue;
            rtaParam.rtaAverageMode = averageMode;
        }

        private void SmoothChanged()
        {
            Smoother.SmoothPoint smoothPoint = (Smoother.SmoothPoint)this.cmbSmooth.SelectedValue;
            rtaParam.tfSmooth = smoothPoint;
        }

        private void ScaleChanged()
        {
            SpectrumScale scale = (SpectrumScale)this.cmbScale.SelectedValue;
            rtaParam.spScale = scale;

            if (audioAnalysis._display.refTrace._isShowSpectrum)
            {
                for (int i = 0; i < refTrace.spGroups.Length; ++i)
                {
                    if (refTrace.spGroups[i].isShow && refTrace.spGroups[i].CurrTraceIsValid)
                    {
                        refTrace.spGroups[i].isNeedtoUpdate = true;
                    }
                }

                audioAnalysis.RunOnceDisplay();
            }
        }

        private void OutputSignalChanged()
        {
            rtaParam.outputSignalType = (OutputSignalType)this.cmbOutputSignal.SelectedValue;
            
            this.UpdateOutputSignal();

            this.audioAnalysis.OutputSignalTypeChangedCallback();
        }

        private void InitRTASwitch()
        {
            this.rtaSwitch.isOn = audioAnalysis.rtaIsON;
        }

        private void InitRTAPause()
        {
            this.rtaPause.isOn = audioAnalysis.isPaused;
            this.rtaPause.Enabled = this.rtaSwitch.isOn;
        }
        
        //初始化RTA模式选择按钮
        private void InitRdoRTAMode()
        {
            if (audioAnalysis.rtaMode == AEC.RTA.Core.RTAMode.Spectrum)
            {
                this.rdoSpectrum.isCheckInit = true;
            }
            else
            {
                this.rdoTransferFunction.isCheckInit = true;
            }
        }

        //初始化采样频率Cmb
        private void InitCmbSampleRate()
        {
            InitComboDataSource(this.cmbSampleRate, srMap);
            if (!srMap.ContainsValue(rtaParam.sr))
            {
                rtaParam.sr = RTASampleRate.sr48k;
            }
            this.cmbSampleRate.SelectedValue = rtaParam.sr;
        }

        //初始化采样深度选择按钮
        private void InitRdoBitsPerSample()
        {
            if(rtaParam.sbd == RTASampleBitDepth.bd16Bit)
            {
                rdoBitsPerSample16.isCheckInit = true;
            }
            else if (rtaParam.sbd == RTASampleBitDepth.bd24Bit)
            {
                rdoBitsPerSample24.isCheckInit = true;
            }
        }

        //刷新输入设备
        private void RefreshInputDevice()
        {
            //初始化音频信号输入列表
            //this.cmbAudioInDevice.Items.Clear();
            List<AudioDevice> devices = new List<AudioDevice>(AudioIn.Devices);
            if (devices.Count > 0)
            {
                Dictionary<string, object> deviceMap = new Dictionary<string, object>();
                foreach (AudioDevice device in devices)
                {
                    deviceMap[device.Name] = device;
                }

                InitComboDataSource(this.cmbAudioInDevice, deviceMap);

                if (!string.IsNullOrEmpty(AudioAnalysis.rtaParam.inputDeviceDescription) &&
                   deviceMap.ContainsKey(AudioAnalysis.rtaParam.inputDeviceDescription))
                {
                    this.cmbAudioInDevice.Text = AudioAnalysis.rtaParam.inputDeviceDescription;
                }
                else
                {
                    this.cmbAudioInDevice.SelectedIndex = 0;
                    //rtaParam.inputDeviceDescription = this.cmbAudioInDevice.Text;
                }
            }
            else
            {
                this.cmbAudioInDevice.DataSource = null;
            }
        }

        private void RefreshOutputDevice()
        {
            //初始化音频信号输出列表
            //this.cmbAudioOutDevice.Items.Clear();
            List<AudioDevice> devices = new List<AudioDevice>(AudioOut.Devices);
            if (devices.Count > 0)
            {
                Dictionary<string, object> deviceMap = new Dictionary<string, object>();
                foreach (AudioDevice device in devices)
                {
                    deviceMap[device.Name] = device;
                }

                InitComboDataSource(this.cmbAudioOutDevice, deviceMap);

                if (!string.IsNullOrEmpty(AudioAnalysis.rtaParam.outputDeviceDescription) &&
                   deviceMap.ContainsKey(AudioAnalysis.rtaParam.outputDeviceDescription))
                {
                    this.cmbAudioOutDevice.Text = AudioAnalysis.rtaParam.outputDeviceDescription;
                }
                else
                {
                    this.cmbAudioOutDevice.SelectedIndex = 0;
                }
            }
            else
            {
                this.cmbAudioOutDevice.DataSource = null;
            }
        }

        //初始化FFT 长度 Cmb
        private void InitCmbFftSize()
        {
            if(audioAnalysis.rtaMode == RTAMode.Spectrum)
            {
                InitComboDataSource(this.cmbFFTSize, spFftSizeMap);
                this.cmbFFTSize.SelectedValue = rtaParam.spFftSize;
            }
            else
            {
                InitComboDataSource(this.cmbFFTSize, tfFftSizeMap);
                this.cmbFFTSize.SelectedValue = rtaParam.tfFftSize;
            }
        }

        private void InitCmbWindowFunction()
        {
            Dictionary<string, object> windowFunctionMap = new Dictionary<string, object>()
            {
                {localization[Localization.formRTAOptions_HannWindow], WindowFunction.HannWindow},
                {localization[Localization.formRTAOptions_HammingWindow], WindowFunction.HammingWindow},
                {localization[Localization.formRTAOptions_RectangleWindow], WindowFunction.RectangleWindow},
                {localization[Localization.formRTAOptions_GaussianWindow], WindowFunction.GaussianWindow},
                {localization[Localization.formRTAOptions_BlackmanWindow], WindowFunction.BlackmanWindow},
                {localization[Localization.formRTAOptions_BlackmanHarrisWindow], WindowFunction.BlackmanHarrisWindow},
                {localization[Localization.formRTAOptions_FlatTopWindow], WindowFunction.FlatTopWindow},
            };
            InitComboDataSource(this.cmbWindowFunction, windowFunctionMap);
            this.cmbWindowFunction.SelectedValue = rtaParam.rtaWindow;
        }

        private void InitCmbAverage()
        {
            Dictionary<string, object> averageMap = new Dictionary<string, object>()
            {
                {"1", Averager.AverageMode.FIFO1},
                {"2", Averager.AverageMode.FIFO2},
                {"4", Averager.AverageMode.FIFO4},
                {"8", Averager.AverageMode.FIFO8},
                {"16", Averager.AverageMode.FIFO16},
                {"32", Averager.AverageMode.FIFO32},
                {"64", Averager.AverageMode.FIFO64},
                {"128", Averager.AverageMode.FIFO128},
                {localization[Localization.formRTAOptions_AverageInf], Averager.AverageMode.INF},
                {localization[Localization.formRTAOptions_AverageFast], Averager.AverageMode.FAST},
                {localization[Localization.formRTAOptions_AverageSlow], Averager.AverageMode.SLOW},
            };
            InitComboDataSource(this.cmbAverage, averageMap);
            this.cmbAverage.SelectedValue = rtaParam.rtaAverageMode;
        }

        private void InitCmbSmooth()
        {
            Dictionary<string, object> smoothMap = new Dictionary<string, object>()
            {
                
                {localization[Localization.formRTAOptions_SmoothNone], Smoother.SmoothPoint.None},
                {localization[Localization.formRTAOptions_Smooth3Pt], Smoother.SmoothPoint.Point3},
                {localization[Localization.formRTAOptions_Smooth5Pt], Smoother.SmoothPoint.Point5},
                {localization[Localization.formRTAOptions_Smooth7Pt], Smoother.SmoothPoint.Point7},
                {localization[Localization.formRTAOptions_Smooth9Pt], Smoother.SmoothPoint.Point9},
            };
            InitComboDataSource(this.cmbSmooth, smoothMap);
            this.cmbSmooth.SelectedValue = rtaParam.tfSmooth;
        }

        private void InitCmbScale()
        {
            Dictionary<string, object> scaleMap = new Dictionary<string, object>()
            {
                {"Log", SpectrumScale.Log},
                {"Octave", SpectrumScale.Octave},
                {"1/3 Octave", SpectrumScale.Octave3},
                {"1/6 Octave", SpectrumScale.Octave6},
                {"1/12 Octave", SpectrumScale.Octave12},
                {"1/24 Octave", SpectrumScale.Octave24},
            };
            InitComboDataSource(this.cmbScale, scaleMap);
            this.cmbScale.SelectedValue = rtaParam.spScale;
        }

        private void InitRdoSpectrumGraph()
        {
            if (rtaParam.spMap == SpectrumMap.DoubleCh)
            {
                rdoDoubleCh.isCheckInit = true;
            }
            else if (rtaParam.spMap == SpectrumMap.LeftCh)
            {
                rdoLeftCh.isCheckInit = true;
            }
            else if (rtaParam.spMap == SpectrumMap.RightCh)
            {
                rdoRightCh.isCheckInit = true;
            }
        }

        private void InitParamLabelAmpOffset()
        {
            this.paramLabelAmpOffset.Init(AudioRTAParam.TransferAmpOffsetIndexMax, rtaParam.tfAmpOffset, rtaParam.GetTfAmpLabel, rtaParam.CalcAmpOffsetIndex);
        }

        private void InitRdoFreqRespGraph()
        {
            if (rtaParam.freqRespMap == FreqRespMap.AmpRespAndPhaseResp)
            {
                rdoAmpAndPhaseResp.isCheckInit = true;
            }
            else if (rtaParam.freqRespMap == FreqRespMap.AmpResp)
            {
                rdoAmpResp.isCheckInit = true;
            }
            else if (rtaParam.freqRespMap == FreqRespMap.PhaseResp)
            {
                rdoPhaseResp.isCheckInit = true;
            }
        }

        private void InitCmbOutputSignal()
        {
            Dictionary<string, object> outputSignalMap = new Dictionary<string, object>()
            {
                {localization[Localization.formRTAOptions_PinkNoise], OutputSignalType.PinkNoise},
                {localization[Localization.formRTAOptions_PinkSweep], OutputSignalType.PinkSweep},
                {localization[Localization.formRTAOptions_Sine], OutputSignalType.Sine},
                {localization[Localization.formRTAOptions_DualSine], OutputSignalType.DualSine},
            };
            InitComboDataSource(this.cmbOutputSignal, outputSignalMap);
            this.cmbOutputSignal.SelectedValue = rtaParam.outputSignalType;
            
            UpdateOutputSignal();
        }

        private void InitOutputSwitch()
        {
            this.outputSwitch.isOn = audioAnalysis.audioOutIsON;
        }

        private void InitSignalInputLevel()
        {
            this.levelLeft.isShowLevel = audioAnalysis.rtaIsON;
            this.levelRight.isShowLevel = audioAnalysis.rtaIsON;
        }

        private void InitParamLabelDelay()
        {
            this.paramLabelDelay.Init(Delay.maxOffset, (short)audioAnalysis.delay.delayOffset, audioAnalysis.delay.GetDelayLabel, audioAnalysis.delay.CalcDelayIndex);
            this.paramLabelMeasDelayResult.canBeChanged = false;
            this.btnMeasDelay.Enabled = audioAnalysis.rtaIsON;
            this.btnApplyMeasResult.Visible = false;
            this.btnDeclineMeasResult.Visible = false;
        }

        private void InitRefTracePanel()
        {
            refTraceSwitch.isOn = isShowRefTrace;
            //Console.WriteLine("状态:{0}", chkBoxRefTrace.isOn);
            traceGroupEnabled = isShowRefTrace;

            this.btnCapture.Enabled = false;
            this.btnDelete.Enabled = false;

            traceGroupA.UpdateGroupTraceUI(refTrace.GetGroup(RefTraceGroupNum.GroupA));
            traceGroupB.UpdateGroupTraceUI(refTrace.GetGroup(RefTraceGroupNum.GroupB));
            traceGroupC.UpdateGroupTraceUI(refTrace.GetGroup(RefTraceGroupNum.GroupC));
            traceGroupD.UpdateGroupTraceUI(refTrace.GetGroup(RefTraceGroupNum.GroupD));
            traceGroupE.UpdateGroupTraceUI(refTrace.GetGroup(RefTraceGroupNum.GroupE));
        
            if(isShowRefTrace)
            {
                if (refTrace.GetCurrGroup().isShow)
                {
                    GetGroup(refTrace.CurrGroup).isActive = true;
                    this.btnCapture.Enabled = true;
                    if (refTrace.GetCurrGroup().CurrTraceIsValid)
                    {
                        this.btnDelete.Enabled = true;
                        UpdateTraceComment();
                        //this.btnDelete.Enabled = true;
                        //this.txtTraceComment.Text = refTrace.GetCurrGroup().GetCurrTrace().comment;
                    }
                }
                else
                {
                    foreach (RefTraceGroupNum gn in ReferenceTrace.all_group)
                    {
                        if (refTrace.GetGroup(gn).isShow)
                        {
                            refTrace.CurrGroup = gn;
                            GetGroup(gn).isActive = true;
                            this.btnCapture.Enabled = true;
                            if (refTrace.GetGroup(gn).CurrTraceIsValid)
                            {
                                this.btnDelete.Enabled = true;
                                UpdateTraceComment();
                                //this.btnDelete.Enabled = true;
                                //this.txtTraceComment.Text = refTrace.GetCurrGroup().GetCurrTrace().comment;
                                //this.txtTraceComment.Enabled = true;
                            }

                            break;
                        }
                    }
                }
            }
        }

        private void UpdateOutputSignal()
        {
            bool isPinkSignal = rtaParam.outputSignalType == OutputSignalType.PinkNoise ||
               rtaParam.outputSignalType == OutputSignalType.PinkSweep;

            if(audioAnalysis.audioOutIsON)
            {
                if (rtaParam.outputSignalType == OutputSignalType.PinkSweep)
                {
                    rtaParam.inputSignal = InputSignal.Sweep;
                    cmbWindowFunction.Enabled = false;
                }
                else
                {
                    rtaParam.inputSignal = InputSignal.Normal;
                    cmbWindowFunction.Enabled = true;
                }
            }

            this.panelPink.Visible = isPinkSignal;
            this.panelSine.Visible = !isPinkSignal;

            bool isDualSine = rtaParam.outputSignalType == OutputSignalType.DualSine;
            lblSine2Level.Visible = isDualSine;
            paramLabelSine2Level.Visible = isDualSine;
            lblSine2Freq.Visible = isDualSine;
            paramLabelSine2Freq.Visible = isDualSine;

            if(rtaParam.outputSignalType == OutputSignalType.PinkNoise)
            {
                this.paramLabelPinkLevel.Init(OutputSignalLevel.MaxOutputLevel, rtaParam.pinkNoiseLevel.levelIndex, OutputSignalLevel.GetLabel, OutputSignalLevel.CalcLevelIndex);
            }
            else if(rtaParam.outputSignalType == OutputSignalType.PinkSweep)
            {
                this.paramLabelPinkLevel.Init(OutputSignalLevel.MaxOutputLevel, rtaParam.pinkSweepLevel.levelIndex, OutputSignalLevel.GetLabel, OutputSignalLevel.CalcLevelIndex);
            }
            else if(rtaParam.outputSignalType == OutputSignalType.Sine)
            {
                this.lblSine1Level.Text = localization[Localization.formRTAOptions_Level];
                this.lblSine1Freq.Text = localization[Localization.formRTAOptions_SineFreq];
                this.lblSine1Level.UpdateDisplay();
                this.lblSine1Freq.UpdateDisplay();

                this.paramLabelSine1Level.Init(OutputSignalLevel.MaxOutputLevel, rtaParam.sineLevel.levelIndex, OutputSignalLevel.GetLabel, OutputSignalLevel.CalcLevelIndex);
                this.paramLabelSine1Freq.Init(OutputSineSignalFreq.MaxSineFreq, rtaParam.sineFreq.freqIndex, OutputSineSignalFreq.GetLabel, OutputSineSignalFreq.CalcFreqIndex);
            }
            else if (rtaParam.outputSignalType == OutputSignalType.DualSine)
            {
                this.lblSine1Level.Text = localization[Localization.formRTAOptions_Sine1Level];
                this.lblSine1Freq.Text = localization[Localization.formRTAOptions_Sine1Freq];
                this.lblSine1Level.UpdateDisplay();
                this.lblSine1Freq.UpdateDisplay();

                this.paramLabelSine1Level.Init(OutputSignalLevel.MaxOutputLevel, rtaParam.dualSine1Level.levelIndex, OutputSignalLevel.GetLabel, OutputSignalLevel.CalcLevelIndex);
                this.paramLabelSine1Freq.Init(OutputSineSignalFreq.MaxSineFreq, rtaParam.dualSine1Freq.freqIndex, OutputSineSignalFreq.GetLabel, OutputSineSignalFreq.CalcFreqIndex);

                this.paramLabelSine2Level.Init(OutputSignalLevel.MaxOutputLevel, rtaParam.dualSine2Level.levelIndex, OutputSignalLevel.GetLabel, OutputSignalLevel.CalcLevelIndex);
                this.paramLabelSine2Freq.Init(OutputSineSignalFreq.MaxSineFreq, rtaParam.dualSine2Freq.freqIndex, OutputSineSignalFreq.GetLabel, OutputSineSignalFreq.CalcFreqIndex);
            }
        }

        private void UpdateRTAParamPanel()
        {
            this.lblSmooth.Visible = audioAnalysis.rtaMode == RTAMode.Transfer;
            this.cmbSmooth.Visible = audioAnalysis.rtaMode == RTAMode.Transfer;
            this.panelSPParam.Visible = audioAnalysis.rtaMode == RTAMode.Spectrum;
            this.panelTFParam.Visible = audioAnalysis.rtaMode == RTAMode.Transfer;
        }

        private void UpdateRTAModeUI()
        {
            cmbFFTSize.SelectedValueChanged -= cmb_SelectedValueChanged;
            cmbWindowFunction.SelectedValueChanged -= cmb_SelectedValueChanged;
            cmbAverage.SelectedValueChanged -= cmb_SelectedValueChanged;

            InitCmbFftSize();
            InitCmbWindowFunction();
            InitCmbAverage();

            UpdateRTAParamPanel();

            cmbFFTSize.SelectedValueChanged += cmb_SelectedValueChanged;
            cmbWindowFunction.SelectedValueChanged += cmb_SelectedValueChanged;
            cmbAverage.SelectedValueChanged += cmb_SelectedValueChanged;

            InitRefTracePanel();

            if(isShowRefTrace)
            {
                audioAnalysis.UpdateRefTraceDisplay();
            }
            else
            {
                audioAnalysis.UpdateDisplay();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnRefreshInputDevice))
            {
                this.cmbAudioInDevice.SelectedValueChanged -= cmb_SelectedValueChanged;
                this.RefreshInputDevice();
                this.cmbAudioInDevice.SelectedValueChanged += cmb_SelectedValueChanged;
            }
            else if (sender.Equals(this.btnRefreshOutputDevice))
            {
                this.cmbAudioOutDevice.SelectedValueChanged -= cmb_SelectedValueChanged;
                this.RefreshOutputDevice();
                this.cmbAudioOutDevice.SelectedValueChanged += cmb_SelectedValueChanged;
            }
            else if(sender.Equals(this.btnCapture))
            {
                this.CaptureTrace();
            }
            else if(sender.Equals(this.btnDelete))
            {
                this.DeleteTrace();
            }
        }


        private void paramLabel_ValueChanged(object sender, EventArgs e)
        {
            //gtButton btn = sender as gtButton;
            if (sender.Equals(this.paramLabelAmpOffset))
            {
                rtaParam.tfAmpOffset = this.paramLabelAmpOffset.v;
            }
            else if (sender.Equals(this.paramLabelPinkLevel))
            {
                if(rtaParam.outputSignalType == OutputSignalType.PinkNoise)
                {
                    rtaParam.pinkNoiseLevel.levelIndex = this.paramLabelPinkLevel.v;
                    audioAnalysis.OutputPinkNoiseLevelChangedCallback();
                }
                else if (rtaParam.outputSignalType == OutputSignalType.PinkSweep)
                {
                    rtaParam.pinkSweepLevel.levelIndex = this.paramLabelPinkLevel.v;
                    audioAnalysis.OutputPinkSweepLevelChangedCallback();
                }
            }
            else if (sender.Equals(this.paramLabelSine1Level))
            {
                if (rtaParam.outputSignalType == OutputSignalType.Sine)
                {
                    rtaParam.sineLevel.levelIndex = this.paramLabelSine1Level.v;
                    audioAnalysis.OutputSineLevelChangedCallback();
                }
                else if (rtaParam.outputSignalType == OutputSignalType.DualSine)
                {
                    rtaParam.dualSine1Level.levelIndex = this.paramLabelSine1Level.v;
                    audioAnalysis.OutputDualSine1LevelChangedCallback();
                }
            }
            else if (sender.Equals(this.paramLabelSine1Freq))
            {
                if (rtaParam.outputSignalType == OutputSignalType.Sine)
                {
                    rtaParam.sineFreq.freqIndex = this.paramLabelSine1Freq.v;
                    audioAnalysis.OutputSineFreqChangedCallback();
                }
                else if (rtaParam.outputSignalType == OutputSignalType.DualSine)
                {
                    rtaParam.dualSine1Freq.freqIndex = this.paramLabelSine1Freq.v;
                    audioAnalysis.OutputDualSine1FreqChangedCallback();
                }
            }
            else if (sender.Equals(this.paramLabelSine2Level))
            {
                rtaParam.dualSine2Level.levelIndex = this.paramLabelSine2Level.v;
                audioAnalysis.OutputDualSine2LevelChangedCallback();
            }
            else if (sender.Equals(this.paramLabelSine2Freq))
            {
                rtaParam.dualSine2Freq.freqIndex = this.paramLabelSine2Freq.v;
                audioAnalysis.OutputDualSine2FreqChangedCallback();
            }
            else if(sender.Equals(this.paramLabelDelay))
            {
                audioAnalysis.delay.delayOffset = this.paramLabelDelay.v;
                rtaParam.delay = audioAnalysis.delay.delayTime;
            }
        }

        private static void InitComboDataSource(ComboBox cmb, IDictionary<string, object> dict)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dict;
            cmb.DataSource = bs;
            cmb.DisplayMember = "key";
            cmb.ValueMember = "value";
        }

        private void outputSwitch_Click(object sender, EventArgs e)
        {
            if(!audioAnalysis.audioOutIsON)
            {
                this.outputSwitch.Enabled = false;
                this.audioAnalysis.rtaEventBus.NotifyObeserver(new RTATryOperateArgs(RTAOperate.TurnOnOutput));
            }
            else
            {
                this.outputSwitch.Enabled = false;
                this.audioAnalysis.rtaEventBus.NotifyObeserver(new RTATryOperateArgs(RTAOperate.TurnOffOutput));
            }
        }

        private void btnMeasDelay_Click(object sender, EventArgs e)
        {
            StartMeasDelay();
        }

        private void StartMeasDelay()
        {
            if (audioAnalysis.rtaIsON)
            {
                audioAnalysis.StartRecvDataToMeasDelay();
                this.paramLabelMeasDelayResult.Reset();
                this.btnMeasDelay.Enabled = false;
                this.progressBar.Visible = true;
                this.btnApplyMeasResult.Enabled = false;
                this.btnDeclineMeasResult.Enabled = false;
                this.cmbSampleRate.Enabled = false;
            }
        }

        private void RTAStartCallback()
        {
            this.levelLeft.isShowLevel = true;
            this.levelRight.isShowLevel = true;

            this.btnMeasDelay.Enabled = true;

            this.panelCapture.Enabled = true;

            this.rtaSwitch.isOn = true;

            this.btnRefreshInputDevice.Enabled = false;
        }

        private void RTAStopCallback()
        {
            this.levelLeft.Reset();
            this.levelRight.Reset();

            this.btnMeasDelay.Enabled = false;
            this.progressBar.Visible = false;

            this.btnApplyMeasResult.Enabled = false;
            this.btnDeclineMeasResult.Enabled = false;

            this.btnApplyMeasResult.Visible = false;
            this.btnDeclineMeasResult.Visible = false;

            this.panelCapture.Enabled = false;

            this.btnRefreshInputDevice.Enabled = true;

            this.cmbSampleRate.Enabled = true;

            this.rtaSwitch.Enabled = true;
            this.rtaSwitch.isOn = false;
            this.rtaPause.isOn = false;

            this.progressBar.Reset();
            this.paramLabelMeasDelayResult.Reset();
        }

        private void MeasDelayCompletedCallback()
        {
            this.btnMeasDelay.Enabled = true;
            this.btnApplyMeasResult.Visible = true;
            this.btnDeclineMeasResult.Visible = true;
            this.btnApplyMeasResult.Enabled = true;
            this.btnDeclineMeasResult.Enabled = true;
            this.cmbSampleRate.Enabled = true;
            this.progressBar.Reset();
            this.progressBar.Visible = false;
        }

        private void btnApplyMeasResult_Click(object sender, EventArgs e)
        {
            if(currDelay != null)
            {
                audioAnalysis.delay.delayTime = currDelay.delayTime;
                rtaParam.delay = audioAnalysis.delay.delayTime;
                this.paramLabelDelay.Init(Delay.maxOffset, (short)audioAnalysis.delay.delayOffset, audioAnalysis.delay.GetDelayLabel, audioAnalysis.delay.CalcDelayIndex);
                this.paramLabelMeasDelayResult.Reset();
                this.progressBar.Reset();
                this.btnApplyMeasResult.Enabled = false;
                this.btnDeclineMeasResult.Enabled = false;
                this.btnApplyMeasResult.Visible = false;
                this.btnDeclineMeasResult.Visible = false;
            }
        }

        private void btnDeclineMeasResult_Click(object sender, EventArgs e)
        {
            if (currDelay != null)
            {
                this.paramLabelMeasDelayResult.Reset();
                this.progressBar.Reset();
                this.btnApplyMeasResult.Enabled = false;
                this.btnDeclineMeasResult.Enabled = false;
                this.btnApplyMeasResult.Visible = false;
                this.btnDeclineMeasResult.Visible = false;
            }
        }

        private void refTraceSwitch_Click(object sender, EventArgs e)
        {
            this.refTraceSwitch.isOn = !this.refTraceSwitch.isOn;

            this.isShowRefTrace = this.refTraceSwitch.isOn;

            this.InitRefTracePanel();

            if(this.isShowRefTrace)
            {
                DoShowRefTrace();
            }
            else
            {
                audioAnalysis.UpdateDisplay();
            }
        }

        private void DoUpdateDiplay()
        {
            if (this.isShowRefTrace || (!audioAnalysis.rtaIsON || audioAnalysis.isPaused))
            {
                audioAnalysis.UpdateDisplay();
            }
        }

        private void DoShowRefTrace()
        {
            if (audioAnalysis.rtaIsON && !audioAnalysis.isPaused)
            {
                //当RTA打开并且RTA没有暂停时显示RefTrace
                audioAnalysis.SetUpdateRefTrace();
            }
            else
            {
                //当RTA关闭或者RTA暂停时显示RefTrace
                audioAnalysis.SetUpdateRefTrace();
                audioAnalysis.UpdateRTAContext();
                audioAnalysis.RunOnceDisplay();
            }
        }

        public void GroupShowChanged(object sender, EventArgs e)
        {
            ShowChangedEventArgs arg = e as ShowChangedEventArgs;
            refTrace.GetGroup(arg._gn).isShow = arg._isShow;
            this.GetGroup(arg._gn).isActive = arg._isShow;

            if (arg._isShow)
            {
                refTrace.CurrGroup = arg._gn;
                this.btnCapture.Enabled = true;

                this.btnDelete.Enabled = refTrace.GetCurrGroup().CurrTraceIsValid;
                
                audioAnalysis.RunOnceDisplay();
            }
            else
            {
                this.btnCapture.Enabled = false;
                this.btnDelete.Enabled = false;

                foreach (RefTraceGroupNum gn in ReferenceTrace.all_group)
                {
                    if (refTrace.GetGroup(gn).isShow)
                    {
                        refTrace.CurrGroup = gn;
                        GetGroup(gn).isActive = true;
                        this.btnCapture.Enabled = true;
                        this.btnDelete.Enabled = refTrace.GetGroup(gn).CurrTraceIsValid;
                        //this.txtTraceComment.Enabled = refTrace.GetCurrGroup().CurrTraceIsValid;

                        break;
                    }
                }

                audioAnalysis.UpdateDisplay();
            }
        }

        public void GroupCurrIndexChanged(object sender, EventArgs e)
        {
            GroupCurrIndexChangedEventArgs arg = e as GroupCurrIndexChangedEventArgs;

            GetGroup(arg._gn).isActive = true;
            GetGroup(arg._gn).isShowGroup = true;
            refTrace.CurrGroup = arg._gn;
            refTrace.GetGroup(arg._gn).isShow = true;
            refTrace.GetGroup(arg._gn).currIndex = arg._currIndex;

            this.btnCapture.Enabled = true;
            this.btnDelete.Enabled = refTrace.GetGroup(arg._gn).CurrTraceIsValid;

            if (refTrace.GetGroup(arg._gn).CurrTraceIsValid)
            {
                refTrace.GetGroup(arg._gn).isNeedtoUpdate = true;
                audioAnalysis.RunOnceDisplay();
            }
            else
            {
                refTrace.GetGroup(arg._gn).ClearGraphicData();
                audioAnalysis.UpdateDisplay();
            }
        }

        private void CaptureTrace()
        {
            if (audioAnalysis.rtaMode == RTAMode.Spectrum)
            {
                if (refTrace.GetCurrGroup().isShow)
                {
                    AudioChannel curr_ch = AudioChannel.LeftCh;
                    if (AudioAnalysis.rtaParam.spMap == SpectrumMap.DoubleCh)
                    {
                        SelectChannel();
                        return;
                    }
                    else if (AudioAnalysis.rtaParam.spMap == SpectrumMap.RightCh)
                    {
                        curr_ch = AudioChannel.RightCh;
                    }

                    audioAnalysis.CaptureSpectrum(refTrace.CurrGroup, curr_ch);
                    this.GetGroup(refTrace.CurrGroup).UpdateCurrTrace(true);
                    this.btnDelete.Enabled = true;
                    //this.txtTraceComment.Enabled = true;
                }
            }
            else
            {
                if (refTrace.GetCurrGroup().isShow)
                {
                    audioAnalysis.CaptureTransfer(refTrace.CurrGroup);
                    this.GetGroup(refTrace.CurrGroup).UpdateCurrTrace(true);
                    this.btnDelete.Enabled = true;
                    //this.txtTraceComment.Enabled = true;
                }
            }
        }

        private void DeleteTrace()
        {
            audioAnalysis.EraseTrace(refTrace.CurrGroup);
            this.GetGroup(refTrace.CurrGroup).UpdateCurrTrace(false);
            this.btnDelete.Enabled = false;
            audioAnalysis.UpdateDisplay(); 
        }

        private void SelectChannel()
        {
            ToolStripDropDown toolStripDropDown = new ToolStripDropDown();
            toolStripDropDown.Items.Add(CreateToolStripItem("左声道", delegate(object sender, EventArgs e) { CaptureCh(AudioChannel.LeftCh);}));
            toolStripDropDown.Items.Add(CreateToolStripItem("右声道", delegate(object sender, EventArgs e) { CaptureCh(AudioChannel.RightCh);}));
            toolStripDropDown.Show(this.btnCapture, new Point(0, this.btnCapture.Height + 4));
        }

        private ToolStripItem CreateToolStripItem(string text, EventHandler click)
        {
            ToolStripItem item = new ToolStripMenuItem();
            item.AutoSize = false;
            item.Height = 36;
            item.Width = this.btnCapture.Width;
            item.Text = text;
            item.BackColor = Color.White;
            item.ForeColor = Color.Black;
            item.Font = this.Font;
            item.Click += click;
            return item;
        }

        private void CaptureCh(AudioChannel ch)
        {
            audioAnalysis.CaptureSpectrum(refTrace.CurrGroup, ch);
            this.GetGroup(refTrace.CurrGroup).UpdateCurrTrace(true);
            this.btnDelete.Enabled = true;
        }

        private void rtaSwitch_Click(object sender, EventArgs e)
        {
            if (!this.audioAnalysis.rtaIsON)
            {
                this.rtaSwitch.Enabled = false;
                this.audioAnalysis.rtaEventBus.NotifyObeserver(new RTATryOperateArgs(RTAOperate.TurnOnRTA));
            }
            else
            {
                this.rtaSwitch.Enabled = false;
                this.audioAnalysis.rtaEventBus.NotifyObeserver(new RTATryOperateArgs(RTAOperate.TurnOffRTA));
            }
        }

        private void rtaPause_Click(object sender, EventArgs e)
        {
            this.audioAnalysis.isPaused = !this.audioAnalysis.isPaused;
            this.rtaPause.isOn = this.audioAnalysis.isPaused;
        }

        private Localization localization = Config.GetInstance().localization;
        private void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formRTAOptions];
            this.rdoSpectrum.Text = localization[Localization.formRTAOptions_Spectrum];
            this.rdoTransferFunction.Text = localization[Localization.formRTAOptions_TransferFunction];
            this.panelAudioIO.title = localization[Localization.formRTAOptions_AudioIO];
            this.lblAudioInDevice.Text = localization[Localization.formRTAOptions_AudioInDevice];
            this.lblSampleRate.Text = localization[Localization.formRTAOptions_SampleRate];
            this.lblSampleBitDepth.Text = localization[Localization.formRTAOptions_SampleBitDepth];
            this.lblAudioOutDevice.Text = localization[Localization.formRTAOptions_AudioOutDevice];
            this.panelRTAParams.title = localization[Localization.formRTAOptions_RTAParams];
            this.lblFftSize.Text = localization[Localization.formRTAOptions_FftSize];
            this.lblWindowFunction.Text = localization[Localization.formRTAOptions_WindowFunction];
            this.lblAverage.Text = localization[Localization.formRTAOptions_Average];
            this.lblSmooth.Text = localization[Localization.formRTAOptions_Smooth];
            this.lblScale.Text = localization[Localization.formRTAOptions_Scale];
            this.lblSpectrumGraph.Text = localization[Localization.formRTAOptions_SpectrumGraph];
            this.rdoDoubleCh.Text = localization[Localization.formRTAOptions_DoubleCh];
            this.rdoLeftCh.Text = localization[Localization.formRTAOptions_LeftCh];
            this.rdoRightCh.Text = localization[Localization.formRTAOptions_RightCh];
            this.lblAmpOffset.Text = localization[Localization.formRTAOptions_AmpOffset];
            this.lblFreqRespGraph.Text = localization[Localization.formRTAOptions_FreqRespGraph];
            this.rdoAmpAndPhaseResp.Text = localization[Localization.formRTAOptions_AmpAndPhaseResp];
            this.rdoAmpResp.Text = localization[Localization.formRTAOptions_AmpResp];
            this.rdoPhaseResp.Text = localization[Localization.formRTAOptions_PhaseResp];

            this.panelSignalLevel.title = localization[Localization.formRTAOptions_SignalLevel];
            this.lblLeftCh.Text = localization[Localization.formRTAOptions_SignalLeftCh];
            this.lblRightCh.Text = localization[Localization.formRTAOptions_SignalRightCh];

            this.panelDelay.title = localization[Localization.formRTAOptions_Delay];
            this.lblDelay.Text = localization[Localization.formRTAOptions_Delay];
            this.btnMeasDelay.Text = localization[Localization.formRTAOptions_MeasDelay];
            this.lblMeasDelayResult.Text = localization[Localization.formRTAOptions_MeasDelayResult];
            this.btnApplyMeasResult.Text = localization[Localization.formRTAOptions_ApplyMeasResult];
            this.btnDeclineMeasResult.Text = localization[Localization.formRTAOptions_DeclineMeasResult];

            this.panelSignalGenerator.title = localization[Localization.formRTAOptions_SignalGenerator];
            this.lblSignal.Text = localization[Localization.formRTAOptions_Signal];
            this.lblLevel.Text = localization[Localization.formRTAOptions_Level];
            if (rtaParam.outputSignalType == OutputSignalType.Sine)
            {
                this.lblSine1Level.Text = localization[Localization.formRTAOptions_Level];
                this.lblSine1Freq.Text = localization[Localization.formRTAOptions_SineFreq];
            }
            else
            {
                this.lblSine1Level.Text = localization[Localization.formRTAOptions_Sine1Level];
                this.lblSine1Freq.Text = localization[Localization.formRTAOptions_Sine1Freq];
            }
            this.lblSine2Level.Text = localization[Localization.formRTAOptions_Sine2Level];
            this.lblSine2Freq.Text = localization[Localization.formRTAOptions_Sine2Freq];

            this.panelRefTrace.title = localization[Localization.formRTAOptions_RefTrace];
            this.btnCapture.Text = localization[Localization.formRTAOptions_Capture];
            this.btnDelete.Text = localization[Localization.formRTAOptions_Delete];

            this.RemoveCmbEvents();

            InitCmbWindowFunction();
            InitCmbAverage();
            InitCmbSmooth();
            InitCmbOutputSignal();

            this.RegisterCmbEvents();

            this.panelRTAModePanel.Invalidate();
            this.panelAudioIO.Invalidate();
            this.panelRTAParams.Invalidate();
            this.panelSPParam.Invalidate();
            this.panelTFParam.Invalidate();
            this.panelSignalLevel.Invalidate();
            this.panelDelay.Invalidate();
            this.panelSignalGenerator.Invalidate();
            this.panelSine.Invalidate();
            this.panelRefTrace.Invalidate();
            this.btnCapture.Invalidate();
        }
    }
}
