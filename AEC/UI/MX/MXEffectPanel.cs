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
using AEC.Common;
using AEC.Effectors.MX;
using AEC.Effectors.MX.Modules;
using AEC.Misc;

namespace AEC.UI.MX
{
    public partial class MXEffectPanel : PanelBase
    {
        private EchoModule echo;
        private ReverbModule reverb;

        public MXEffectPanel()
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

            this.lblMicReverb.Font = FontHelper.CreateFont10();
            this.lblMicReverbDirect.Font = FontHelper.CreateFont10();
            this.lblReverbMicHpf.Font = FontHelper.CreateFont10();
            this.lblReverbMicLpf.Font = FontHelper.CreateFont10();
            this.lblReverbPreDelay.Font = FontHelper.CreateFont10();
            this.lblReverbDelay.Font = FontHelper.CreateFont10();
            this.lblReverbBassDamp.Font = FontHelper.CreateFont10();
            this.lblReverbTrebleDamp.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            this.echo = base.mController.mxEffectorModel.echo;
            this.reverb = base.mController.mxEffectorModel.reverb;

            base.BindingModule(this.echo, MEffectorFamily.MX);

            this.slideMicEcho.Binding(this.echo.echoLevel);
            this.sliderMicEchoDirect.Binding(this.echo.directLevel);
            this.sliderEchoPreDelay.Binding(this.echo.preDelay, PreDelayGetLabel, null);
            this.sliderEchoDelay.Binding(this.echo.delayTime, ParamGetLabelHelper.EchoDelayGetLabel, null);
            this.sliderEchoRepeat.Binding(this.echo.repeat, ParamGetLabelHelper.EchoRepeatGetLabel, null);
            this.sliderEchoRChPreDelay.Binding(this.echo.rChPreDelay, ParamGetLabelHelper.EchoRChPreDelayGetLabel, null);
            this.sliderEchoRChDelay.Binding(this.echo.rChDelayTime, ParamGetLabelHelper.EchoRChDelayGetLabel, null);

            this.sliderMicReverb.Binding(this.reverb.reverbLevel);
            this.sliderMicReverbDirect.Binding(this.reverb.directLevel);            
            this.sliderReverbMicHpf.Binding(this.reverb.micHpfFreq, GetHpfFreqLabel, null);
            this.sliderReverbMicLpf.Binding(this.reverb.micLpfFreq, GetLpfFreqLabel, null);
            this.sliderReverbMusicHpf.Binding(this.reverb.musicHpfFreq, GetHpfFreqLabel, null);
            this.sliderReverbMusicLpf.Binding(this.reverb.musicLpfFreq, GetLpfFreqLabel, null);
            this.sliderMusicReverb.Binding(this.reverb.reverbMusicLevel);
            this.sliderReverbPreDelay.Binding(this.reverb.preDelay, PreDelayGetLabel, null);
            this.sliderReverbDelay.Binding(this.reverb.delayTime, ParamGetLabelHelper.ReverbDelayGetLabel, null);
            this.sliderReverbBassDamp.Binding(this.reverb.bassDamp, ParamGetLabelHelper.ReverbGetDampLabel, null);
            this.sliderReverbTrebleDamp.Binding(this.reverb.trebleDamp, ParamGetLabelHelper.ReverbGetDampLabel, null);

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

            this.sliderMicReverb.UpdateUIFromData();
            this.sliderMicReverbDirect.UpdateUIFromData();
            this.sliderReverbMicHpf.UpdateUIFromData();
            this.sliderReverbMicLpf.UpdateUIFromData();
            this.sliderReverbMusicHpf.UpdateUIFromData();
            this.sliderReverbMusicLpf.UpdateUIFromData();
            this.sliderMusicReverb.UpdateUIFromData();
            this.sliderReverbPreDelay.UpdateUIFromData();
            this.sliderReverbDelay.UpdateUIFromData();
            this.sliderReverbBassDamp.UpdateUIFromData();
            this.sliderReverbTrebleDamp.UpdateUIFromData();

            this.eqResetAndByPassPanel.isPass = this.echo.eqGroup.isBypass;
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

            this.lblMicReverb.Text = localization[Localization.panelEffect_Reverb_ReverbLevel];
            this.lblMicReverbDirect.Text = localization[Localization.panelEffect_Reverb_ReverbDirectLevel];
            this.lblReverbMicHpf.Text = localization[Localization.panelEffect_Reverb_MicHpf];
            this.lblReverbMicLpf.Text = localization[Localization.panelEffect_Reverb_MicLpf];
            this.lblReverbMusicHpf.Text = localization[Localization.panelEffect_Reverb_MusicHpf];
            this.lblReverbMusicLpf.Text = localization[Localization.panelEffect_Reverb_MusicLpf];
            this.lblReverbPreDelay.Text = localization[Localization.panelEffect_Reverb_PreDelay];
            this.lblReverbDelay.Text = localization[Localization.panelEffect_Reverb_Delay];
            this.lblReverbBassDamp.Text = localization[Localization.panelEffect_Reverb_BassDamp];
            this.lblReverbTrebleDamp.Text = localization[Localization.panelEffect_Reverb_TrebleDamp];
            this.lblMusicReverb.Text = localization[Localization.panelEffect_Reverb_MusicReverbLevel];

            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        public static string PreDelayGetLabel(short index)
        {
            return ParamGetLabelHelper.EchoPreDelayGetLabel((short)(index * 2));
        }
    }
}
