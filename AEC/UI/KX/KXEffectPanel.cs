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
using AEC.Comm;
using AEC.Effectors.KX;
using AEC.Effectors.KX.Modules;
using AEC.Misc;

namespace AEC.UI.KX
{
    public partial class KXEffectPanel : PanelBase
    {
        private EchoModule echo;
        private ReverbModule reverb;

        public KXEffectPanel()
        {
            InitializeComponent();
            InitFont();
            this.InitControl(this.eqGroupPanel, this.sliderEchoHpf, this.sliderEchoLpf);

            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;

            this.sliderEchoHpfType.Actived += XPFilter_Actived;
            this.sliderEchoHpfType.ValueChanged += FilterType_ValueChanged;
            this.sliderEchoLpfType.Actived += XPFilter_Actived;
            this.sliderEchoLpfType.ValueChanged += FilterType_ValueChanged;

            this.sliderReverbMicHpf.Actived += XPFilter_Actived;
            this.sliderReverbMicHpf.ValueChanged += FilterType_ValueChanged;
            this.sliderReverbMicHpfType.Actived += XPFilter_Actived;
            this.sliderReverbMicHpfType.ValueChanged += FilterType_ValueChanged;
            this.sliderReverbMicLpf.Actived += XPFilter_Actived;
            this.sliderReverbMicLpf.ValueChanged += FilterType_ValueChanged;
            this.sliderReverbMicLpfType.Actived += XPFilter_Actived;
            this.sliderReverbMicLpfType.ValueChanged += FilterType_ValueChanged;
            this.sliderReverbBassDamp.Actived += XPFilter_Actived;
            this.sliderReverbBassDamp.ValueChanged += FilterType_ValueChanged;
            this.sliderReverbTrebleDamp.Actived += XPFilter_Actived;
            this.sliderReverbTrebleDamp.ValueChanged += FilterType_ValueChanged;

            this.lblEchoHpf.MouseDown += lbl_MouseDown;
            this.lblEchoHpfSlope.MouseDown += lbl_MouseDown;
            this.lblEchoLpf.MouseDown += lbl_MouseDown;
            this.lblEchoLpfSlope.MouseDown += lbl_MouseDown;
            this.lblReverbMicHpf.MouseDown += lbl_MouseDown;
            this.lblReverbMicHpfSlope.MouseDown += lbl_MouseDown;
            this.lblReverbMicLpf.MouseDown += lbl_MouseDown;
            this.lblReverbMicLpfSlope.MouseDown += lbl_MouseDown;
            this.lblReverbBassDamp.MouseDown += lbl_MouseDown;
            this.lblReverbTrebleDamp.MouseDown += lbl_MouseDown;
        }

        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.echo == null || this.reverb == null)
            {
                return;
            }

            if (
                sender.Equals(this.lblEchoHpf) ||
                sender.Equals(this.lblEchoHpfSlope))
            {
                this.echo.activeItem = ActiveItem.HPF;
                this.echo.isSecondaryModuleActivated = false;
                this.eqGroupPanel.ActivateCurrEQ(true);
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if(
                sender.Equals(this.lblEchoLpf) ||
                sender.Equals(this.lblEchoLpfSlope))
            {
                this.echo.activeItem = ActiveItem.LPF;
                this.echo.isSecondaryModuleActivated = false;
                this.eqGroupPanel.ActivateCurrEQ(true);
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }            
            else if (
                sender.Equals(this.lblReverbMicHpf) ||
                sender.Equals(this.lblReverbMicHpfSlope))
            {
                this.reverb.activeItem = ActiveItem.HPF;
                this.echo.isSecondaryModuleActivated = true;
                this.eqGroupPanel.ActivateCurrEQ(false);
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if (
                sender.Equals(this.lblReverbMicLpf) ||
                sender.Equals(this.lblReverbMicLpfSlope))
            {
                this.reverb.activeItem = ActiveItem.LPF;
                this.echo.isSecondaryModuleActivated = true;
                this.eqGroupPanel.ActivateCurrEQ(false);
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if (
                sender.Equals(this.lblReverbBassDamp) ||
                sender.Equals(this.lblReverbTrebleDamp))
            {
                this.echo.isSecondaryModuleActivated = true;
                this.eqGroupPanel.ActivateCurrEQ(false);
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
        }

        protected void XPFilter_Actived(object sender, EventArgs e)
        {
            if (this.echo == null || this.reverb == null)
            {
                return;
            }

            if (sender.Equals(this.sliderEchoHpfType))
            {
                if (this.echo.activeItem != ActiveItem.HPF || this.echo.isSecondaryModuleActivated)
                {
                    this.echo.activeItem = ActiveItem.HPF;
                    this.echo.isSecondaryModuleActivated = false;
                    this.eqGroupPanel.ActivateCurrEQ(true);
                    this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if (sender.Equals(this.sliderEchoLpfType))
            {
                if (this.echo.activeItem != ActiveItem.LPF || this.echo.isSecondaryModuleActivated)
                {
                    this.echo.activeItem = ActiveItem.LPF;
                    this.echo.isSecondaryModuleActivated = false;
                    this.eqGroupPanel.ActivateCurrEQ(true);
                    this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if(
                sender.Equals(this.sliderReverbMicHpf) ||
                sender.Equals(this.sliderReverbMicHpfType))
            {
                if (!this.echo.isSecondaryModuleActivated)
                {
                    this.reverb.activeItem = ActiveItem.HPF;
                    this.echo.isSecondaryModuleActivated = true;
                    this.eqGroupPanel.ActivateCurrEQ(false);
                    this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if (
                sender.Equals(this.sliderReverbMicLpf) ||
                sender.Equals(this.sliderReverbMicLpfType))
            {
                if (!this.echo.isSecondaryModuleActivated)
                {
                    this.reverb.activeItem = ActiveItem.LPF;
                    this.echo.isSecondaryModuleActivated = true;
                    this.eqGroupPanel.ActivateCurrEQ(false);
                    this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if (
                sender.Equals(this.sliderReverbBassDamp) ||
                sender.Equals(this.sliderReverbTrebleDamp))
            {
                if (!this.echo.isSecondaryModuleActivated)
                {
                    this.echo.isSecondaryModuleActivated = true;
                    this.eqGroupPanel.ActivateCurrEQ(false);
                    this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
        }

        private void FilterType_ValueChanged(object sender, EventArgs e)
        {
            if (this.echo == null)
            {
                return;
            }

            HSliderParam hSliderParam = sender as HSliderParam;
            if (sender.Equals(this.sliderEchoHpfType))
            {
                this.echo.hpf.UpdateTypeIndexFromParam();
                this.echo.isSecondaryModuleActivated = false;                
                this.echo.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Type);
            }
            else if (sender.Equals(this.sliderEchoLpfType))
            {
                this.echo.lpf.UpdateTypeIndexFromParam();
                this.echo.isSecondaryModuleActivated = false;
                this.echo.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Type);
            }
            else if (sender.Equals(this.sliderReverbMicHpf))
            {
                this.reverb.hpf.UpdateFreqIndexFormParam();
                this.reverb.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Freq);
                this.echo.isSecondaryModuleActivated = true;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if (sender.Equals(this.sliderReverbMicHpfType))
            {
                this.reverb.hpf.UpdateTypeIndexFromParam();
                this.reverb.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Type);
                this.echo.isSecondaryModuleActivated = true;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if (sender.Equals(this.sliderReverbMicLpf))
            {
                this.reverb.lpf.UpdateFreqIndexFormParam();
                this.reverb.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Freq);
                this.echo.isSecondaryModuleActivated = true;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if (sender.Equals(this.sliderReverbMicLpfType))
            {
                this.reverb.lpf.UpdateTypeIndexFromParam();
                this.reverb.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Type);
                this.echo.isSecondaryModuleActivated = true;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if (sender.Equals(this.sliderReverbBassDamp))
            {
                this.reverb.UpdateToneControl();
                this.echo.isSecondaryModuleActivated = true;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if (sender.Equals(this.sliderReverbTrebleDamp))
            {
                this.reverb.UpdateToneControl();
                this.echo.isSecondaryModuleActivated = true;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }

            this.eqGroupPanel.ActivateCurrEQ(!this.echo.isSecondaryModuleActivated);
        }

        private void InitFont()
        {
            this.lblMicEcho.Font = FontHelper.CreateFont10();
            this.lblMicEchoDirect.Font = FontHelper.CreateFont10();
            this.lblEchoHpf.Font = FontHelper.CreateFont10();
            this.lblEchoHpfSlope.Font = FontHelper.CreateFont10();
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
            this.lblReverbMicHpfSlope.Font = FontHelper.CreateFont10();
            this.lblReverbMicLpf.Font = FontHelper.CreateFont10();
            this.lblReverbMicLpfSlope.Font = FontHelper.CreateFont10();
            this.lblReverbPreDelay.Font = FontHelper.CreateFont10();
            this.lblReverbDelay.Font = FontHelper.CreateFont10();
            this.lblReverbBassDamp.Font = FontHelper.CreateFont10();
            this.lblReverbTrebleDamp.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.kxEffectorModel != null);

            this.echo = base.mController.kxEffectorModel.echo;
            this.reverb = base.mController.kxEffectorModel.reverb;

            base.BindingModule(this.echo, MEffectorFamily.KX);

            this.reverb.eventBus.RegisterHandler(ReverbXPFChangedFromGraphicHandler);

            this.slideMicEcho.Binding(this.echo.echoLevel);
            this.sliderMicEchoDirect.Binding(this.echo.directLevel);
            this.sliderEchoPreDelay.Binding(this.echo.preDelay, getEchoPreDelayLabel, null);
            this.sliderEchoHpfType.Binding(this.echo.hpf.typeParam, getFilterTypeLabel, null);
            this.sliderEchoLpfType.Binding(this.echo.lpf.typeParam, getFilterTypeLabel, null);
            this.sliderEchoDelay.Binding(this.echo.delayTime, getEchoDelayLabel, null);
            this.sliderEchoRepeat.Binding(this.echo.repeat, getEchoRepeatLabel, null);
            this.sliderEchoRChPreDelay.Binding(this.echo.rChPreDelay, getEchoRChPreDelayLabel, null);
            this.sliderEchoRChDelay.Binding(this.echo.rChDelayTime, getEchoRChDelayLabel, null);

            this.sliderMicReverb.Binding(this.reverb.reverbLevel);
            this.sliderMicReverbDirect.Binding(this.reverb.directLevel);
            this.sliderReverbMicHpf.Binding(this.reverb.hpf.freqParam, GetHpfFreqLabel, null);
            this.sliderReverbMicLpf.Binding(this.reverb.lpf.freqParam, GetLpfFreqLabel, null);
            this.sliderReverbMicHpfType.Binding(this.reverb.hpf.typeParam, getFilterTypeLabel, null);
            this.sliderReverbMicLpfType.Binding(this.reverb.lpf.typeParam, getFilterTypeLabel, null);
            this.sliderReverbPreDelay.Binding(this.reverb.preDelay, GetReverbPreDelayLabel, null);
            this.sliderReverbDelay.Binding(this.reverb.delayTime, GetReverbDelayLabel, null);
            this.sliderReverbBassDamp.Binding(this.reverb.bassDamp, GetReverbDampLabel, null);
            this.sliderReverbTrebleDamp.Binding(this.reverb.trebleDamp, GetReverbDampLabel, null);

            this.eqResetAndByPassPanel.Init(this.echo.eqGroup.isBypass, this.echo.themeColor);
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.slideMicEcho.UpdateUIFromData();
            this.sliderMicEchoDirect.UpdateUIFromData();
            this.sliderEchoHpfType.UpdateUIFromData();
            this.sliderEchoLpfType.UpdateUIFromData();
            this.sliderEchoPreDelay.UpdateUIFromData();
            this.sliderEchoDelay.UpdateUIFromData();
            this.sliderEchoRepeat.UpdateUIFromData();
            this.sliderEchoRChPreDelay.UpdateUIFromData();
            this.sliderEchoRChDelay.UpdateUIFromData();

            this.sliderMicReverb.UpdateUIFromData();
            this.sliderMicReverbDirect.UpdateUIFromData();
            this.sliderReverbMicHpf.UpdateUIFromData();
            this.sliderReverbMicHpfType.UpdateUIFromData();
            this.sliderReverbMicLpf.UpdateUIFromData();
            this.sliderReverbMicLpfType.UpdateUIFromData();
            this.sliderReverbPreDelay.UpdateUIFromData();
            this.sliderReverbDelay.UpdateUIFromData();
            this.sliderReverbBassDamp.UpdateUIFromData();
            this.sliderReverbTrebleDamp.UpdateUIFromData();

            this.eqResetAndByPassPanel.isPass = this.echo.eqGroup.isBypass;
        }

        protected void ReverbXPFChangedFromGraphicHandler(object sender, EventBusArgs args)
        {
            if (this.reverb != null && args.type == EventType.XPFChangedFromGraphic)
            {
                XPFChangedFromGraphicArgs filterChangedFromGraphicArgs = args as XPFChangedFromGraphicArgs;
                ReverbXPFChangedHandler(filterChangedFromGraphicArgs.filterType, filterChangedFromGraphicArgs.filterElement);
            }
        }

        private void ReverbXPFChangedHandler(XPFilterType filterType, FilterElement filterElement)
        {
            if (filterElement == FilterElement.Freq)
            {
                if (filterType == XPFilterType.HighPass)
                {
                    if (this.sliderReverbMicHpf != null)
                    {
                        this.sliderReverbMicHpf.value = this.reverb.hpf.freqIndex;
                    }
                }
                else if (filterType == XPFilterType.LowPass)
                {
                    if (this.sliderReverbMicLpf != null)
                    {
                        this.sliderReverbMicLpf.value = this.reverb.lpf.freqIndex;
                    }
                }
            }
            else if (filterElement == FilterElement.Type)
            {
                if (filterType == XPFilterType.HighPass)
                {
                    if (this.sliderReverbMicHpfType != null)
                    {
                        this.sliderReverbMicHpfType.value = this.reverb.hpf.typeIndex;
                    }
                }
                else if (filterType == XPFilterType.LowPass)
                {
                    if (this.sliderReverbMicLpfType != null)
                    {
                        this.sliderReverbMicLpfType.value = this.reverb.lpf.typeIndex;
                    }
                }
            }
        }

        protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {
            if (filterType == XPFilterType.HighPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.sliderEchoHpfType.value = this.echo.hpf.typeIndex;
                }
            }
            else if (filterType == XPFilterType.LowPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.sliderEchoLpfType.value = this.echo.lpf.typeIndex;
                }
            }
        }

        protected override void MainModuleActivatedHandler()
        {
            this.eqGroupPanel.ActivateCurrEQ(true);
        }

        public static string getFilterTypeLabel(short index)
        {
            return (index + 1).ToString();
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
                return string.Format("{0} dB", -index);
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
            this.lblEchoHpfSlope.Text = localization[Localization.panelEffect_Echo_HpfType];
            this.lblEchoLpf.Text = localization[Localization.panelEffect_Echo_Lpf];
            this.lblEchoLpfSlope.Text = localization[Localization.panelEffect_Echo_LpfType];
            this.lblEchoPreDelay.Text = localization[Localization.panelEffect_Echo_PreDelay];
            this.lblEchoDelay.Text = localization[Localization.panelEffect_Echo_Delay];
            this.lblEchoRepeat.Text = localization[Localization.panelEffect_Echo_Repeat];
            this.lblEchoRChPreDelay.Text = localization[Localization.panelEffect_Echo_RChPreDelay];
            this.lblEchoRChDelay.Text = localization[Localization.panelEffect_Echo_RChDelay];

            this.lblMicReverb.Text = localization[Localization.panelEffect_Reverb_ReverbLevel];
            this.lblMicReverbDirect.Text = localization[Localization.panelEffect_Reverb_ReverbDirectLevel];
            this.lblReverbMicHpf.Text = localization[Localization.panelEffect_Reverb_Hpf];
            this.lblReverbMicHpfSlope.Text = localization[Localization.panelEffect_Reverb_HpfType];
            this.lblReverbMicLpf.Text = localization[Localization.panelEffect_Reverb_Lpf];
            this.lblReverbMicLpfSlope.Text = localization[Localization.panelEffect_Reverb_LpfType];
            this.lblReverbPreDelay.Text = localization[Localization.panelEffect_Reverb_PreDelay];
            this.lblReverbDelay.Text = localization[Localization.panelEffect_Reverb_Delay];
            this.lblReverbBassDamp.Text = localization[Localization.panelEffect_Reverb_BassDamp];
            this.lblReverbTrebleDamp.Text = localization[Localization.panelEffect_Reverb_TrebleDamp];

            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }
    }
}
