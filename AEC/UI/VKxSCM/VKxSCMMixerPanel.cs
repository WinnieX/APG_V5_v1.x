using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Common;
using AEC.Effectors.VKxSCM.Modules;
using AEC.Misc;
using AEC.UI.Common;

namespace AEC.UI.VKxSCM
{
    public partial class VKxSCMMixerPanel : gtUserControl
    {
        private OutputBaseModule output;
        private EventBus eventBus;

        private PopMenuMixerLevelUint popMenuMixerLevelUint;

        public VKxSCMMixerPanel()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitFont();
            //disabledBack = AEC.Properties.Resources.output_mixer_level_disabled;

            this.components = new System.ComponentModel.Container();
            this.popMenuMixerLevelUint = new PopMenuMixerLevelUint(this.components);
        }

        private void InitFont()
        {
            this.lblMusicLevel.Font = FontHelper.CreateFont10();
            this.lblMicLevel.Font = FontHelper.CreateFont10();
            this.lblEchoLevel.Font = FontHelper.CreateFont10();
            this.lblReverbLevel.Font = FontHelper.CreateFont10();
        }

        public Image backgroundNormal
        {
            get
            {
                return this.mixerMusic.backgroundNormal;
            }
            set
            {
                this.mixerMusic.backgroundNormal = value;
                this.mixerMic.backgroundNormal = value;
                this.mixerEcho.backgroundNormal = value;
                this.mixerReverb.backgroundNormal = value;
            }
        }

        public Image backgroundDisabled
        {
            get
            {
                return this.mixerMusic.backgroundDisabled;
            }
            set
            {
                this.mixerMusic.backgroundDisabled = value;
                this.mixerMic.backgroundDisabled = value;
                this.mixerEcho.backgroundDisabled = value;
                this.mixerReverb.backgroundDisabled = value;
            }
        }

        public void Binding(OutputBaseModule output_, EventBus eventBus_)
        {
            Debug.Assert(output_ != null && eventBus_ != null);
            this.output = output_;
            this.eventBus = eventBus_;

            this.popMenuMixerLevelUint.BindingEventBus(eventBus_);

            this.mixerMusic.Binding(output.musicSignalMixer);
            this.mixerMic.Binding(output.micSignalMixer);
            this.mixerEcho.Binding(output.echoSignalMixer);
            this.mixerReverb.Binding(output.reverbSignalMixer);

            this.eventBus.RegisterHandler(EventHandler);
        }

        private void EventHandler(object sender, EventBusArgs e)
        {
            if (e.type == EventType.MixerLevelUnitChanged)
            {
                this.MixerLevelUnitChangedHandler();
            }
        }

        private void MixerLevelUnitChangedHandler()
        {
            this.mixerMusic.UpdateLabel();
            this.mixerMic.UpdateLabel();
            this.mixerEcho.UpdateLabel();
            this.mixerReverb.UpdateLabel();
        }

        public void UpdateUIFromData(SystemModuleBase system)
        {
            //System.Diagnostics.Debug.Assert(system != null);

            this.mixerMusic.Binding(output.musicSignalMixer);
            this.mixerMic.Binding(output.micSignalMixer);
            this.mixerEcho.Binding(output.echoSignalMixer);
            this.mixerReverb.Binding(output.reverbSignalMixer);
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblMusicLevel.Text = localization[Localization.mixerPanel_MusicLevel];
            this.lblMicLevel.Text = localization[Localization.mixerPanel_MicLevel];
            this.lblEchoLevel.Text = localization[Localization.mixerPanel_EchoLevel];
            this.lblReverbLevel.Text = localization[Localization.mixerPanel_ReverbLevel];
        }

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            config.SwitchMixerLevelUnit();
            this.eventBus.NotifyObeserver(new MixerLevelUnitChangedArgs());
        }

        private void KXMixerPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenuMixerLevelUint.Show();
            }
        }
    }
}
