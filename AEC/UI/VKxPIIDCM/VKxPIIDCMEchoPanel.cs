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
using AEC.Effectors.VKxPIIDCM;
using AEC.Effectors.VKxPIIDCM.Modules;
using AEC.Misc;

namespace AEC.UI.VKxPIIDCM
{
    public partial class VKxPIIDCMEchoPanel : PanelBase
    {
        private EchoModule echo;

        public VKxPIIDCMEchoPanel()
        {
            InitializeComponent();
            InitFont();
            this.InitControl(this.eqGroupPanel, this.sliderEchoHpf, this.sliderEchoLpf);

            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
        }

        private void InitFont()
        {
            this.lblMicEcho.Font = FontHelper.CreateFont10();
            this.lblMicEchoDirect.Font = FontHelper.CreateFont10();
            this.lblEchoHpf.Font = FontHelper.CreateFont10();
            this.lblEchoLpf.Font = FontHelper.CreateFont10();
            this.lblEchoPreDelay.Font = FontHelper.CreateFont10();
            this.lblEchoDelay.Font = FontHelper.CreateFont10();
            this.lblEchoRepeat.Font = FontHelper.CreateFont10();
            this.lblEchoRChPreDelay.Font = FontHelper.CreateFont10();
            this.lblEchoRChDelay.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vkxPIIdcmEffectorModel != null);

            this.echo = base.mController.vkxPIIdcmEffectorModel.echo;

            base.BindingModule(this.echo, MEffectorFamily.VKxPIIDCM);

            this.slideMicEcho.Binding(this.echo.echoMixer);
            this.sliderMicEchoDirect.Binding(this.echo.directMixer);
            this.sliderEchoPreDelay.Binding(this.echo.preDelay, getEchoPreDelayLabel, null);
            this.sliderEchoDelay.Binding(this.echo.delayTime, getEchoDelayLabel, null);
            this.sliderEchoRepeat.Binding(this.echo.repeat, getEchoRepeatLabel, null);
            this.sliderEchoRChPreDelay.Binding(this.echo.rChPreDelay, getEchoRChPreDelayLabel, null);
            this.sliderEchoRChDelay.Binding(this.echo.rChDelayTime, getEchoRChDelayLabel, null);

            this.eqResetAndByPassPanel.Init(this.echo.eqGroup.isBypass, this.echo.themeColor);
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.slideMicEcho.UpdateUIFromData();
            this.sliderMicEchoDirect.UpdateUIFromData();
            this.sliderEchoPreDelay.UpdateUIFromData();
            this.sliderEchoDelay.UpdateUIFromData();
            this.sliderEchoRepeat.UpdateUIFromData();
            this.sliderEchoRChPreDelay.UpdateUIFromData();
            this.sliderEchoRChDelay.UpdateUIFromData();

            this.eqResetAndByPassPanel.isPass = this.echo.eqGroup.isBypass;
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
            this.lblEchoLpf.Text = localization[Localization.panelEffect_Echo_Lpf];
            this.lblEchoPreDelay.Text = localization[Localization.panelEffect_Echo_PreDelay];
            this.lblEchoDelay.Text = localization[Localization.panelEffect_Echo_Delay];
            this.lblEchoRepeat.Text = localization[Localization.panelEffect_Echo_Repeat];
            this.lblEchoRChPreDelay.Text = localization[Localization.panelEffect_Echo_RChPreDelay];
            this.lblEchoRChDelay.Text = localization[Localization.panelEffect_Echo_RChDelay];

            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }
    }
}
