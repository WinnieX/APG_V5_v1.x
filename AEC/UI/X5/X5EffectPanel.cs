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
using AEC.Common;
using AEC.Comm;
using AEC.Effectors.X5;
using AEC.Effectors.X5.Modules;
using AEC.Misc;

namespace AEC.UI.X5
{
    public partial class X5EffectPanel : PanelBase
    {
        private EchoModule echo;
        private ReverbModule reverb;

        public X5EffectPanel()
        {
            InitializeComponent();
            InitFont();
            this.InitControl(this.eqGroupPanel, this.sliderEchoHpf, this.sliderEchoLpf);

            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;

            this.sliderReverbHpf.Actived += XPFilter_Actived;
            this.sliderReverbHpf.ValueChanged += FilterType_ValueChanged;
            this.sliderReverbLpf.Actived += XPFilter_Actived;
            this.sliderReverbLpf.ValueChanged += FilterType_ValueChanged;

            this.lblEchoHpf.MouseDown += lbl_MouseDown;
            this.lblEchoLpf.MouseDown += lbl_MouseDown;
            this.lblReverbHpf.MouseDown += lbl_MouseDown;
            this.lblReverbLpf.MouseDown += lbl_MouseDown;
        }

        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.echo == null || this.reverb == null)
            {
                return;
            }

            if (
                sender.Equals(this.lblEchoHpf))
            {
                this.echo.activeItem = ActiveItem.HPF;
                this.echo.isSecondaryModuleActivated = false;
                this.eqGroupPanel.ActivateCurrEQ(true);
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if(
                sender.Equals(this.lblEchoLpf))
            {
                this.echo.activeItem = ActiveItem.LPF;
                this.echo.isSecondaryModuleActivated = false;
                this.eqGroupPanel.ActivateCurrEQ(true);
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }            
            else if (
                sender.Equals(this.lblReverbHpf))
            {
                this.reverb.activeItem = ActiveItem.HPF;
                this.echo.isSecondaryModuleActivated = true;
                this.eqGroupPanel.ActivateCurrEQ(false);
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
            else if (
                sender.Equals(this.lblReverbLpf))
            {
                this.reverb.activeItem = ActiveItem.LPF;
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

            if(sender.Equals(this.sliderReverbHpf))
            {
                if (!this.echo.isSecondaryModuleActivated)
                {
                    this.reverb.activeItem = ActiveItem.HPF;
                    this.echo.isSecondaryModuleActivated = true;
                    this.eqGroupPanel.ActivateCurrEQ(false);
                    this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if (sender.Equals(this.sliderReverbLpf))
            {
                if (!this.echo.isSecondaryModuleActivated)
                {
                    this.reverb.activeItem = ActiveItem.LPF;
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
            if (sender.Equals(this.sliderReverbHpf))
            {
                this.reverb.hpf.UpdateFreqIndexFormParam();
                this.reverb.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Freq);
                this.echo.isSecondaryModuleActivated = true;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }           
            else if (sender.Equals(this.sliderReverbLpf))
            {
                this.reverb.lpf.UpdateFreqIndexFormParam();
                this.reverb.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Freq);
                this.echo.isSecondaryModuleActivated = true;
                this.echo.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }

            this.eqGroupPanel.ActivateCurrEQ(!this.echo.isSecondaryModuleActivated);
        }

        private void InitFont()
        {
            this.lblEchoLevel.Font = FontHelper.CreateFont10();
            this.lblEchoDirect.Font = FontHelper.CreateFont10();
            this.lblEchoHpf.Font = FontHelper.CreateFont10();
            this.lblEchoLpf.Font = FontHelper.CreateFont10();
            this.lblEchoPreDelay.Font = FontHelper.CreateFont10();
            this.lblEchoDelay.Font = FontHelper.CreateFont10();
            this.lblEchoRepeat.Font = FontHelper.CreateFont10();
            this.lblEchoRChPreDelay.Font = FontHelper.CreateFont10();
            this.lblEchoRChDelay.Font = FontHelper.CreateFont10();

            this.lblReverbLevel.Font = FontHelper.CreateFont10();
            this.lblReverbDirect.Font = FontHelper.CreateFont10();
            this.lblReverbHpf.Font = FontHelper.CreateFont10();
            this.lblReverbLpf.Font = FontHelper.CreateFont10();
            this.lblReverbPreDelay.Font = FontHelper.CreateFont10();
            this.lblReverbTime.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.x5EffectorModel != null);

            this.echo = base.mController.x5EffectorModel.echo;
            this.reverb = base.mController.x5EffectorModel.reverb;

            base.BindingModule(this.echo, MEffectorFamily.VKxS);

            this.reverb.eventBus.RegisterHandler(ReverbXPFChangedFromGraphicHandler);

            this.slideMicEcho.Binding(this.echo.echoMixer);
            this.sliderEchoDirect.Binding(this.echo.directMixer);
            this.sliderEchoPreDelay.Binding(this.echo.preDelay, getEchoPreDelayLabel, null);
            this.sliderEchoDelay.Binding(this.echo.delayTime, getEchoDelayLabel, null);
            this.sliderEchoRepeat.Binding(this.echo.repeat, getEchoRepeatLabel, null);
            this.sliderEchoRChPreDelay.Binding(this.echo.rChPreDelay, getEchoRChPreDelayLabel, null);
            this.sliderEchoRChDelay.Binding(this.echo.rChDelayTime, getEchoRChDelayLabel, null);

            this.sliderReverbLevel.Binding(this.reverb.reverbMixer);
            this.sliderMicReverbDirect.Binding(this.reverb.directMixer);
            this.sliderReverbHpf.Binding(this.reverb.hpf.freqParam, GetReverbHpfFreqLabel, null);
            this.sliderReverbLpf.Binding(this.reverb.lpf.freqParam, GetLpfFreqLabel, null);            
            this.sliderReverbPreDelay.Binding(this.reverb.preDelay, GetReverbPreDelayLabel, null);
            this.sliderReverbTime.Binding(this.reverb.reverbTime, GetReverbTimeLabel, null);

            this.eqResetAndByPassPanel.Init(this.echo.eqGroup.isBypass, this.echo.themeColor);
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.slideMicEcho.UpdateUIFromData();
            this.sliderEchoDirect.UpdateUIFromData();
            this.sliderEchoPreDelay.UpdateUIFromData();
            this.sliderEchoDelay.UpdateUIFromData();
            this.sliderEchoRepeat.UpdateUIFromData();
            this.sliderEchoRChPreDelay.UpdateUIFromData();
            this.sliderEchoRChDelay.UpdateUIFromData();

            this.sliderReverbLevel.UpdateUIFromData();
            this.sliderMicReverbDirect.UpdateUIFromData();
            this.sliderReverbHpf.UpdateUIFromData();
            this.sliderReverbLpf.UpdateUIFromData();
            this.sliderReverbPreDelay.UpdateUIFromData();
            this.sliderReverbTime.UpdateUIFromData();

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
                    if (this.sliderReverbHpf != null)
                    {
                        this.sliderReverbHpf.value = this.reverb.hpf.freqIndex;
                    }
                }
                else if (filterType == XPFilterType.LowPass)
                {
                    if (this.sliderReverbLpf != null)
                    {
                        this.sliderReverbLpf.value = this.reverb.lpf.freqIndex;
                    }
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
            return string.Format("{0} ms", index * 2);
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
        public string GetReverbTimeLabel(short index)
        {
            return string.Format("{0:0.#} ms", Math.Round(reverbDelayStep * index, 1, MidpointRounding.AwayFromZero));
        }

        public string GetReverbPreDelayLabel(short index)
        {
            return string.Format("{0} ms", index * 2);
        }

        protected string GetReverbHpfFreqLabel(short index)
        {
            return DataUtils.FilterIndex2String(index + this.reverb.hpf.freqLowerLimitIndex);
        }

        protected string GetReverbLpfFreqLabel(short index)
        {
            return DataUtils.FilterIndex2String(index + this.reverb.lpf.freqLowerLimitIndex);
        }

        public override void UpdateDisplayLanguage()
        {
            this.lblEchoLevel.Text = localization[Localization.panelEffect_Echo_EchoLevel];
            this.lblEchoDirect.Text = localization[Localization.panelEffect_Echo_EchoDirectLevel];
            this.lblEchoHpf.Text = localization[Localization.panelEffect_Echo_Hpf];
            this.lblEchoLpf.Text = localization[Localization.panelEffect_Echo_Lpf];
            this.lblEchoPreDelay.Text = localization[Localization.panelEffect_Echo_PreDelay];
            this.lblEchoDelay.Text = localization[Localization.panelEffect_Echo_Delay];
            this.lblEchoRepeat.Text = localization[Localization.panelEffect_Echo_Repeat];
            this.lblEchoRChPreDelay.Text = localization[Localization.panelEffect_Echo_RChPreDelay];
            this.lblEchoRChDelay.Text = localization[Localization.panelEffect_Echo_RChDelay];

            this.lblReverbLevel.Text = localization[Localization.panelEffect_Reverb_ReverbLevel];
            this.lblReverbDirect.Text = localization[Localization.panelEffect_Reverb_ReverbDirectLevel];
            this.lblReverbHpf.Text = localization[Localization.panelEffect_Reverb_Hpf];
            this.lblReverbLpf.Text = localization[Localization.panelEffect_Reverb_Lpf];
            this.lblReverbPreDelay.Text = localization[Localization.panelEffect_Reverb_PreDelay];
            this.lblReverbTime.Text = localization[Localization.panelEffect_Reverb_Delay];

            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }
    }
}
