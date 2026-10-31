using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Effectors.VX;
using AEC.Effectors.VX.Modules;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.VX
{
    public partial class VXEffectPanel : PanelBase
    {
        private DEffectModule dEffect;
        private EchoModule echo;
        private ReverbModule reverb;

        public VXEffectPanel()
        {
            InitializeComponent();
            InitFont();
            this.InitControl(this.eqGroupPanel, this.sliderEchoHpf, this.sliderEchoLpf);

            this.sliderEchoLpfSlope.Actived += FilterQ_Actived;
            this.sliderEchoLpfSlope.ValueChanged += FilterQ_ValueChanged;
            
            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
        }

        private void InitFont()
        {
            this.lblMicEcho.Font = FontHelper.CreateFont10();
            this.lblMicEchoDirect.Font = FontHelper.CreateFont10();
            this.lblEchoHpf.Font = FontHelper.CreateFont10();
            this.lblEchoLpf.Font = FontHelper.CreateFont10();

            this.lblEchoLpfSlope.Font = FontHelper.CreateFont10();
            this.lblEchoPreDelay.Font = FontHelper.CreateFont10();
            this.lblEchoDelay.Font = FontHelper.CreateFont10();
            this.lblEchoRepeat.Font = FontHelper.CreateFont10();
            this.lblEchoRChPreDelay.Font = FontHelper.CreateFont10();
            this.lblEchoRChDelay.Font = FontHelper.CreateFont10();

            this.lblMicReverb.Font = FontHelper.CreateFont10();
            this.lblMicReverbDirect.Font = FontHelper.CreateFont10();
            this.lblReverbMicHpf.Font = FontHelper.CreateFont10();
            this.lblReverbMicLpf.Font = FontHelper.CreateFont10();
            this.lblReverbPreDelay.Font = FontHelper.CreateFont10();
            this.lblReverbDelay.Font = FontHelper.CreateFont10();
            this.lblReverbBassDamp.Font = FontHelper.CreateFont10();
            this.lblReverbTrebleDamp.Font = FontHelper.CreateFont10();

            this.lblDEffectThreshold.Font = FontHelper.CreateFont10();
            this.lblDEffectGain.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vxEffectorModel != null);

            this.dEffect = base.mController.vxEffectorModel.dEffect;
            this.echo = base.mController.vxEffectorModel.echo;
            this.reverb = base.mController.vxEffectorModel.reverb;

            base.BindingModule(this.echo, MEffectorFamily.VX);

            this.slideMicEcho.Binding(this.echo.echoLevel);
            this.sliderMicEchoDirect.Binding(this.echo.directLevel);
            this.sliderEchoLpfSlope.Binding(this.echo.lpf.qParam, delegate(short index) { return (index + 1).ToString(); }, null);
            this.sliderEchoPreDelay.Binding(this.echo.preDelay, getEchoPreDelayLabel, null);
            this.sliderEchoDelay.Binding(this.echo.delayTime, getEchoDelayLabel, null);
            this.sliderEchoRepeat.Binding(this.echo.repeat, getEchoRepeatLabel, null);
            this.sliderEchoRChPreDelay.Binding(this.echo.rChPreDelay, getEchoRChPreDelayLabel, null);
            this.sliderEchoRChDelay.Binding(this.echo.rChDelayTime, getEchoRChDelayLabel, null);

            this.sliderMicReverb.Binding(this.reverb.reverbLevel);
            this.sliderMicReverbDirect.Binding(this.reverb.directLevel);
            this.sliderReverbMicHpf.Binding(this.reverb.micHpfFreq, GetHpfFreqLabel, null);
            this.sliderReverbMicLpf.Binding(this.reverb.micLpfFreq, GetLpfFreqLabel, null);
            this.sliderReverbPreDelay.Binding(this.reverb.preDelay, GetReverbPreDelayLabel, null);
            this.sliderReverbDelay.Binding(this.reverb.delayTime, GetReverbDelayLabel, null);
            this.sliderReverbBassDamp.Binding(this.reverb.bassDamp, GetReverbDampLabel, null);
            this.sliderReverbTrebleDamp.Binding(this.reverb.trebleDamp, GetReverbDampLabel, null);

            this.sliderDEffectThreshold.Binding(this.dEffect.threshold, base.mController.mEffectorThreshold.GetThresholdLabelIndB, base.mController.mEffectorThreshold.CalcThresholdIndexFromTextIndB);
            
            this.sliderDEffectGain.Binding(this.dEffect.dGain, GetDEffectGainLabel, null);

            this.eqResetAndByPassPanel.Init(this.echo.eqGroup.isBypass, this.echo.themeColor);
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.slideMicEcho.UpdateUIFromData();
            this.sliderMicEchoDirect.UpdateUIFromData();
            this.sliderEchoLpfSlope.UpdateUIFromData();
            this.sliderEchoPreDelay.UpdateUIFromData();
            this.sliderEchoDelay.UpdateUIFromData();
            this.sliderEchoRepeat.UpdateUIFromData();
            this.sliderEchoRChPreDelay.UpdateUIFromData();
            this.sliderEchoRChDelay.UpdateUIFromData();

            this.sliderMicReverb.UpdateUIFromData();
            this.sliderMicReverbDirect.UpdateUIFromData();
            this.sliderReverbMicHpf.UpdateUIFromData();
            this.sliderReverbMicLpf.UpdateUIFromData();
            this.sliderReverbPreDelay.UpdateUIFromData();
            this.sliderReverbDelay.UpdateUIFromData();
            this.sliderReverbBassDamp.UpdateUIFromData();
            this.sliderReverbTrebleDamp.UpdateUIFromData();

            this.sliderDEffectThreshold.ReInitParamLabel(base.mController.mEffectorThreshold.GetThresholdLabelIndB, base.mController.mEffectorThreshold.CalcThresholdIndexFromTextIndB);
            this.sliderDEffectThreshold.UpdateUIFromData();
            this.sliderDEffectGain.UpdateUIFromData();

            this.eqResetAndByPassPanel.isPass = this.echo.eqGroup.isBypass;
        }

        public override void UpdateEffectorModel()
        {
            this.sliderDEffectThreshold.ReInitParamLabel(base.mController.mEffectorThreshold.GetThresholdLabelIndB, base.mController.mEffectorThreshold.CalcThresholdIndexFromTextIndB);
            this.sliderDEffectThreshold.UpdateUIFromData();
        }

        private void FilterQ_Actived(object sender, EventArgs e)
        {
            if (this.echo == null)
            {
                return;
            }

            if (this.echo.activeItem != ActiveItem.LPF)
            {
                this.echo.activeItem = ActiveItem.LPF;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
        }

        private void FilterQ_ValueChanged(object sender, EventArgs e)
        {
            if (this.echo == null)
            {
                return;
            }

            echo.lpf.UpdateQIndexFromParam();
            echo.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Q);
        }

        protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {
            if (filterType == XPFilterType.LowPass && filterElement == FilterElement.Q)
            {
                this.sliderEchoLpfSlope.UpdateUIFromData();
            }
        }

        public string getEchoPreDelayLabel(short index)
        {
            return string.Format("{0} ms", index);
        }

        public string getEchoDelayLabel(short index)
        {
            return string.Format("{0} ms", index * 2);
        }

        public string getEchoRepeatLabel(short index)
        {
            return string.Format("{0} %", index);
        }

        public string getEchoRChPreDelayLabel(short index)
        {
            return string.Format("{0} %", index);
        }

        public string getEchoRChDelayLabel(short index)
        {
            return string.Format("{0} %", index - 50);
        }

        public static readonly double reverbDelayStep = 5000.0 / 80.0;
        public string GetReverbDelayLabel(short index)
        {
            return string.Format("{0:0} ms", Math.Round(reverbDelayStep * index, 0, MidpointRounding.AwayFromZero));
        }

        public string GetReverbPreDelayLabel(short index)
        {
            return string.Format("{0} ms", index);
        }

        public string GetReverbDampLabel(short index)
        {
            if(index == 0)
            {
                return Off;
            }
            else
            {
                return (-index).ToString();
            }
        }

        //public string GetDEffectThresholdLabel(short index)
        //{
        //    return string.Format("{0} dBu", index - 22);
        //}

        public string GetDEffectGainLabel(short index)
        {
            return string.Format("{0} dB", index);
        }

        public override void UpdateDisplayLanguage()
        {
            this.lblMicEcho.Text = localization[Localization.panelEffect_Echo_EchoLevel];
            this.lblMicEchoDirect.Text = localization[Localization.panelEffect_Echo_EchoDirectLevel];
            this.lblEchoHpf.Text = localization[Localization.panelEffect_Echo_Hpf];
            this.lblEchoLpf.Text = localization[Localization.panelEffect_Echo_Lpf];
            this.lblEchoLpfSlope.Text = localization[Localization.panelEffect_Echo_LpfQ];
            this.lblEchoPreDelay.Text = localization[Localization.panelEffect_Echo_PreDelay];
            this.lblEchoDelay.Text = localization[Localization.panelEffect_Echo_Delay];
            this.lblEchoRepeat.Text = localization[Localization.panelEffect_Echo_Repeat];
            this.lblEchoRChPreDelay.Text = localization[Localization.panelEffect_Echo_RChPreDelay];
            this.lblEchoRChDelay.Text = localization[Localization.panelEffect_Echo_RChDelay];

            this.lblMicReverb.Text = localization[Localization.panelEffect_Reverb_ReverbLevel];
            this.lblMicReverbDirect.Text = localization[Localization.panelEffect_Reverb_ReverbDirectLevel];
            this.lblReverbMicHpf.Text = localization[Localization.panelEffect_Reverb_Hpf];
            this.lblReverbMicLpf.Text = localization[Localization.panelEffect_Reverb_Lpf];
            this.lblReverbPreDelay.Text = localization[Localization.panelEffect_Reverb_PreDelay];
            this.lblReverbDelay.Text = localization[Localization.panelEffect_Reverb_Delay];
            this.lblReverbBassDamp.Text = localization[Localization.panelEffect_Reverb_BassDamp];
            this.lblReverbTrebleDamp.Text = localization[Localization.panelEffect_Reverb_TrebleDamp];

            this.lblDEffectThreshold.Text = localization[Localization.panelEffect_DEffect_Threshold];
            this.lblDEffectGain.Text = localization[Localization.panelEffect_DEffect_Gain];

            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }
    }
}
