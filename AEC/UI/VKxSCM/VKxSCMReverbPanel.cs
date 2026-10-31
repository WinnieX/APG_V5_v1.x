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
using AEC.Effectors.VKxSCM;
using AEC.Effectors.VKxSCM.Modules;
using AEC.Misc;

namespace AEC.UI.VKxSCM
{
    public partial class VKxSCMReverbPanel : PanelBase
    {
        private ReverbModule reverb;

        public VKxSCMReverbPanel()
        {
            InitializeComponent();
            InitFont();
            this.InitControl(this.eqGroupPanel, this.sliderReverbMicHpf, this.sliderReverbMicLpf);

            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;

            this.sliderReverbMicHpf.Actived += XPFilter_Actived;
            this.sliderReverbMicHpf.ValueChanged += FilterType_ValueChanged;            
            this.sliderReverbMicLpf.Actived += XPFilter_Actived;
            this.sliderReverbMicLpf.ValueChanged += FilterType_ValueChanged;            
        }

        protected void XPFilter_Actived(object sender, EventArgs e)
        {
            if (this.reverb == null)
            {
                return;
            }

            if(
                sender.Equals(this.sliderReverbMicHpf))
            {
                if (this.reverb.activeItem != ActiveItem.HPF)
                {
                    this.reverb.activeItem = ActiveItem.HPF;
                    this.reverb.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if (sender.Equals(this.sliderReverbMicLpf))
            {
                if (this.reverb.activeItem != ActiveItem.LPF)
                {
                    this.reverb.activeItem = ActiveItem.LPF;
                    this.reverb.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
        }

        private void FilterType_ValueChanged(object sender, EventArgs e)
        {
            if (this.reverb == null)
            {
                return;
            }

            HSliderParam hSliderParam = sender as HSliderParam;
            if (sender.Equals(this.sliderReverbMicHpf))
            {
                this.reverb.hpf.UpdateFreqIndexFormParam();
                this.reverb.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Freq);
                this.reverb.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }            
            else if (sender.Equals(this.sliderReverbMicLpf))
            {
                this.reverb.lpf.UpdateFreqIndexFormParam();
                this.reverb.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Freq);
                this.reverb.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
        }

        private void InitFont()
        {
            this.lblMicReverb.Font = FontHelper.CreateFont10();
            this.lblMicReverbDirect.Font = FontHelper.CreateFont10();
            this.lblReverbMicHpf.Font = FontHelper.CreateFont10();
            this.lblReverbMicLpf.Font = FontHelper.CreateFont10();
            this.lblReverbPreDelay.Font = FontHelper.CreateFont10();
            this.lblReverbDelay.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vkxscmEffectorModel != null);

            this.reverb = base.mController.vkxscmEffectorModel.reverb;

            base.BindingModule(this.reverb, MEffectorFamily.VKxSCM);

            this.reverb.eventBus.RegisterHandler(ReverbXPFChangedFromGraphicHandler);

            this.sliderMicReverb.Binding(this.reverb.reverbMixer);
            this.sliderMicReverbDirect.Binding(this.reverb.directMixer);
            this.sliderReverbMicHpf.Binding(this.reverb.hpf.freqParam, GetHpfFreqLabel, null);
            this.sliderReverbMicLpf.Binding(this.reverb.lpf.freqParam, GetLpfFreqLabel, null);
            this.sliderReverbPreDelay.Binding(this.reverb.preDelay, GetReverbPreDelayLabel, null);
            this.sliderReverbTime.Binding(this.reverb.reverbTime, GetReverbTimeLabel, null);

            this.eqResetAndByPassPanel.Init(this.reverb.eqGroup.isBypass, this.reverb.themeColor);
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();           

            this.sliderMicReverb.UpdateUIFromData();
            this.sliderMicReverbDirect.UpdateUIFromData();
            this.sliderReverbMicHpf.UpdateUIFromData();
            this.sliderReverbMicLpf.UpdateUIFromData();
            this.sliderReverbPreDelay.UpdateUIFromData();
            this.sliderReverbTime.UpdateUIFromData();

            this.eqResetAndByPassPanel.isPass = this.reverb.eqGroup.isBypass;
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
        }

        //protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        //{
        //    if (filterType == XPFilterType.LowPass)
        //    {
        //        if (filterElement == FilterElement.Type)
        //        {
        //            this.sliderReverbMicLpfType.value = this.reverb.lpf.typeIndex;
        //        }
        //    }
        //}

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
        public string GetReverbTimeLabel(short index)
        {
            return string.Format("{0:0} ms", Math.Round(reverbDelayStep * index, 1, MidpointRounding.AwayFromZero));
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
            this.lblMicReverb.Text = localization[Localization.panelEffect_Reverb_ReverbLevel];
            this.lblMicReverbDirect.Text = localization[Localization.panelEffect_Reverb_ReverbDirectLevel];
            this.lblReverbMicHpf.Text = localization[Localization.panelEffect_Reverb_Hpf];
            this.lblReverbMicLpf.Text = localization[Localization.panelEffect_Reverb_Lpf];
            this.lblReverbPreDelay.Text = localization[Localization.panelEffect_Reverb_PreDelay];
            this.lblReverbDelay.Text = localization[Localization.panelEffect_Reverb_Delay];
            
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }
    }
}
