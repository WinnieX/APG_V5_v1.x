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
using AEC.Effectors.MX.Modules;
using AEC.Misc;

namespace AEC.UI.MX
{
    public partial class MXMixerPanel : gtUserControl
    {
        private OutputBaseModule output;

        public MXMixerPanel()
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

        public void Binding(OutputBaseModule output_)
        {
            Debug.Assert(output_ != null);
            this.output = output_;

            this.levelMusic.Binding(output.musicLevelParam);
            this.levelMic.Binding(output.micLevelParam);
            this.levelEcho.Binding(output.echoLevelParam);
            this.levelReverb.Binding(output.reverbLevelParam);
        }

        public void UpdateUIFromData()
        {
            this.levelMusic.Binding(output.musicLevelParam);
            this.levelMic.Binding(output.micLevelParam);
            this.levelEcho.Binding(output.echoLevelParam);
            this.levelReverb.Binding(output.reverbLevelParam);
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblMusicLevel.Text = localization[Localization.mixerPanel_MusicLevel];
            this.lblMicLevel.Text = localization[Localization.mixerPanel_MicLevel];
            this.lblEchoLevel.Text = localization[Localization.mixerPanel_EchoLevel];
            this.lblReverbLevel.Text = localization[Localization.mixerPanel_ReverbLevel];
        }
    }
}
