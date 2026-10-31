using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using AEC.Core;
using AEC.Effectors;
using AEC.Common;
using AEC.Comm;
using AEC.Effectors.MX;
using AEC.Effectors.MX.Modules;
using AEC.Misc;

namespace AEC.UI.MX
{
    public partial class MXMicPanel : PanelBase
    {
        private MicModule mic;

        public MXMicPanel()
            : base()
        {
            InitializeComponent();
            InitFont();

            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            //SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.

            //this.syncContext = WindowsFormsSynchronizationContext.Current;
            this.InitControl(this.eqGroupPanel, this.hpfFreqSlier, null);
            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
            //this.micLevel.Init(micLevelPeak, thresholdLevel);
            //this.micLevel.isShowLevel = true;
            //this.micLevel.level = 10;
        }

        private void InitFont()
        {
            //this.panelLevel.Font = FontHelper.CreateFont10();
            this.panelHPF.Font = FontHelper.CreateFont10();
            this.panelFbe.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            this.mic = base.mController.mxEffectorModel.mic;
            base.BindingModule(this.mic, MEffectorFamily.MX);
            this.micFbe.Binding(mic.fbe, ParamGetLabelHelper.ClassicalMicFbeGetLabel);
            this.eqResetAndByPassPanel.Init(this.mic.eqGroup.isBypass, this.mic.themeColor);
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.micFbe.UpdateUIFromData();
            this.eqResetAndByPassPanel.isPass = this.mic.eqGroup.isBypass;
        }

        public override void UpdateDisplayLanguage()
        {
            this.panelFbe.title = localization[Localization.panelMic_MicFBE];
            this.panelHPF.title = localization[Localization.panelMic_MicHpf];
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        //private void EventHandlerChanged(object sender, EventBusArgs e)
        //{
        //   if(e.type == EventType.ShowSignalLevel)
        //   {
        //       this.micLevel.isShowLevel = (e as ShowSignalLevelArgs).isShow;
        //   }
        //}

        //private void CommStateChanged(object sender, EventBusArgs e)
        //{
        //    CommEventHandler(e);
        //}

        //private void CommEventHandler(EventBusArgs e)
        //{
        //    syncContext.Post(ProcessCommEvent, e);
        //}

        //private void ProcessCommEvent(object state)
        //{
        //    EventBusArgs e = state as EventBusArgs;
        //    //if (e.type == EventType.CommStateChanged || e.type == EventType.CommInterrupted)
        //    if (e.type == EventType.CommStateChanged)
        //    {
        //        //AEC.Log.OutputLine("SystemThread:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
        //        if (!this._commSvc.isConnected)
        //        {
        //            this.micLevel.isShowLevel = false;
        //        }
        //    }
        //    //else if (e.type == EventType.SystemLevelLoaded)
        //    //{
        //    //    if(!this.Visible)
        //    //    {
        //    //        return;
        //    //    }

        //    //    //SystemLevel systemLevel = (e as SystemLevelLoadedArgs).systemLevel;
        //    //    //AEC.Log.OutputLine("level:{0}", systemLevel.rawMicLevel);
        //    //    //systemLevel.Test();

        //    //    //AEC.Log.OutputLine("Music Level:{0}dB, Mic Level:{1}dB, Effect Level:{2}dB, Mic Raw Level:{3}dBu",
        //    //    //    systemLevel.musicLevel,
        //    //    //    systemLevel.micLevel,
        //    //    //    systemLevel.effectLevel,
        //    //    //    systemLevel.rawMicLevel);

        //    //    //systemLevel.musicLevel = 0;

        //    //    if (micLevelPeak.Update(systemLevel.rawMicLevel) || micLevelPeak.CheckPeakFall())
        //    //    {
        //    //        this.micLevel.level = micLevelPeak.peak;
        //    //    }
        //    //}
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Log.OutputLine("Mic Panel Paint");
        //}

        //private int count = 0;
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Log.OutputLine("Mic Panel Paint:{0}", ++count);
        //}
    }
}
