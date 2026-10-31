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
using AEC.Effectors.VKxDM.Modules;
using AEC.Misc;
using AEC.UI.Common;

namespace AEC.UI.VKxDM
{
    public partial class VKxDMMixerPanel : gtUserControl
    {
        private OutputBaseModule output;
        private EventBus eventBus;

        private PopMenuMixerLevelUint popMenuMixerLevelUint;

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        public VKxDMMixerPanel()
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
                return this.levelMusic.backgroundNormal;
            }
            set
            {
                this.levelMusic.backgroundNormal = value;
                this.levelMic.backgroundNormal = value;
                this.levelEcho.backgroundNormal = value;
                this.levelReverb.backgroundNormal = value;
            }
        }

        public Image backgroundDisabled
        {
            get
            {
                return this.levelMusic.backgroundDisabled;
            }
            set
            {
                this.levelMusic.backgroundDisabled = value;
                this.levelMic.backgroundDisabled = value;
                this.levelEcho.backgroundDisabled = value;
                this.levelReverb.backgroundDisabled = value;
            }
        }

        public void Binding(OutputBaseModule output_, EventBus eventBus_)
        {
            Debug.Assert(output_ != null);
            this.output = output_;
            this.eventBus = eventBus_;

            this.levelMusic.Binding(output.musicSignalMixer);
            this.levelMic.Binding(output.micSignalMixer);
            this.levelEcho.Binding(output.echoSignalMixer);
            this.levelReverb.Binding(output.reverbSignalMixer);

            this.popMenuMixerLevelUint.BindingEventBus(eventBus_);
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
            this.levelMusic.UpdateLabel();
            this.levelMic.UpdateLabel();
            this.levelEcho.UpdateLabel();
            this.levelReverb.UpdateLabel();
        }

        public void UpdateUIFromData()
        {
            //System.Diagnostics.Debug.Assert(system != null);

            this.levelMusic.Binding(output.musicSignalMixer);
            this.levelMic.Binding(output.micSignalMixer);
            this.levelEcho.Binding(output.echoSignalMixer);
            this.levelReverb.Binding(output.reverbSignalMixer);
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblMusicLevel.Text = localization[Localization.mixerPanel_MusicLevel];
            this.lblMicLevel.Text = localization[Localization.mixerPanel_MicLevel];
            this.lblEchoLevel.Text = localization[Localization.mixerPanel_EchoLevel];
            this.lblReverbLevel.Text = localization[Localization.mixerPanel_ReverbLevel];
        }

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            config.SwitchMixerLevelUnit();
            this.eventBus.NotifyObeserver(new MixerLevelUnitChangedArgs());
        }

        private void VKxSMixerPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenuMixerLevelUint.Show();
            }
        }
    }
}
