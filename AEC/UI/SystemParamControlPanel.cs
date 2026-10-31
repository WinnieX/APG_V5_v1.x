using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Common;
using AEC.Effectors.VX;
using AEC.Effectors.VX.Modules;
using AEC.Misc;
using AEC.UI.Controls;

namespace AEC.UI
{
    public partial class SystemParamControlPanel : AEC.UI.Controls.MContainerControl
    {
        private MController mController;
        private EventBus mainEventBus;
        private SystemModuleBase systemModule;

        public bool showTimbre
        {
            set
            {
                this.timbrePnael.Visible = value;
            }
        }

        public SystemParamControlPanel()
        {
            InitializeComponent();
            InitFont();

            this.volumePanelMusic.MuteChanged += volumePanel_MuteChanged;
            this.volumePanelMic.MuteChanged += volumePanel_MuteChanged;
        }

        private void InitFont()
        {
            //chkCue.Font = FontHelper.CreateFont10();
        }

        public void Binding(MController mController_, EventBus mainEventBus_)
        {
            Debug.Assert(mController_ != null && mainEventBus_ != null);

            this.mController = mController_;
            this.mainEventBus = mainEventBus_;

            this.mainEventBus.RegisterHandler(EventHandler);

            this.ReBindingSystem();
        }

        public void ReBindingSystem()
        {
            this.systemModule = this.mController.currEffectorModel.system;

            this.volumePanelMusic.Binding(systemModule.musicVolume, systemModule.musicMute);
            this.volumePanelMic.Binding(systemModule.micVolume, systemModule.micMute);
            this.volumePanelEffect.Binding(systemModule.effectVolume, systemModule.effectMute);
            this.volumePanelMusic.upperLimit = systemModule.musicMaxVolume.v;
            this.volumePanelMic.upperLimit = systemModule.micMaxVolume.v;
            this.volumePanelEffect.upperLimit = systemModule.effectMaxVolume.v;

            this.timbrePnael.Binding(systemModule.timbreControl);
        }

        public void UpdateUIFromData()
        {
            this.volumePanelMusic.upperLimit = systemModule.musicMaxVolume.v;
            this.volumePanelMic.upperLimit = systemModule.micMaxVolume.v;
            this.volumePanelEffect.upperLimit = systemModule.effectMaxVolume.v;
            
            this.volumePanelMusic.UpdateUIFromData();
            this.volumePanelMic.UpdateUIFromData();
            this.volumePanelEffect.UpdateUIFromData();

            if (this.mController.currEffectorFamily == Effectors.MEffectorFamily.KX)
            {
                this.timbrePnael.UpdateUIFromData();
            }
        }

        private void EventHandler(object sender, EventBusArgs e)
        {
            if (e.type == EventType.MusicMaxVolumeChanged)
            {
                this.volumePanelMusic.upperLimit = this.systemModule.musicMaxVolume.v;
                if (this.systemModule.musicVolume.v > this.systemModule.musicMaxVolume.v)
                {
                    this.systemModule.musicVolume.v = this.systemModule.musicMaxVolume.v;
                }
            }
            else if (e.type == EventType.MicMaxVolumeChanged)
            {
                this.volumePanelMic.upperLimit = this.systemModule.micMaxVolume.v;
                if (this.systemModule.micVolume.v > this.systemModule.micMaxVolume.v)
                {
                    this.systemModule.micVolume.v = this.systemModule.micMaxVolume.v;
                }
            }
            else if (e.type == EventType.EffectMaxVolumeChanged)
            {
                this.volumePanelEffect.upperLimit = this.systemModule.effectMaxVolume.v;
                if (this.systemModule.effectVolume.v > this.systemModule.effectMaxVolume.v)
                {
                    this.systemModule.effectVolume.v = this.systemModule.effectMaxVolume.v;
                }
            }
            else if(e.type == EventType.MusicVolumeChanged)
            {
                this.volumePanelMusic.UpdateUIFromData();
            }
            else if (e.type == EventType.MicVolumeChanged)
            {
                this.volumePanelMic.UpdateUIFromData();
            }
            else if (e.type == EventType.EffectVolumeChanged)
            {
                this.volumePanelEffect.UpdateUIFromData();
            }
            //else if (e.type == EventType.InputMuteChanged)
            //{
            //    InputMuteChangedHandler(e as InputMuteChangedArgs);
            //}
        }

        //private void InputMuteChangedHandler(InputMuteChangedArgs e)
        //{
        //    if (e.operateSource == InputMuteOperateSource.MonitorPanel)
        //    {
        //        switch(e.muteSource)
        //        {
        //            case MuteSource.Music:
        //                this.volumePanelMusic.UpdateUIFromData();
        //                break;

        //            case MuteSource.Mic:
        //                this.volumePanelMic.UpdateUIFromData();
        //                break;
        //        }
        //    }
        //}

        private void volumePanel_MuteChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.volumePanelMusic))
            {
                OutputMuteChanged(MuteSource.Music);
            }
            else if (sender.Equals(this.volumePanelMic))
            {
                OutputMuteChanged(MuteSource.Mic);
            }
        }

        private void OutputMuteChanged(MuteSource src)
        {
            if(this.mainEventBus == null)
            {
                return;
            }

            if (src == MuteSource.Music)
            {
                if(systemModule.musicMute != null)
                {
                    this.mainEventBus.NotifyObeserver(new OutputMuteChangedArgs(src, systemModule.musicMute.value));
                }
                //this.mainEventBus.NotifyObeserver(new InputMuteChangedArgs(InputMuteOperateSource.VolumePanel, MuteSource.Music));
            }
            else if (src == MuteSource.Mic)
            {
                if (systemModule.micMute != null)
                {
                    this.mainEventBus.NotifyObeserver(new OutputMuteChangedArgs(src, systemModule.micMute.value));
                }
                //this.mainEventBus.NotifyObeserver(new InputMuteChangedArgs(InputMuteOperateSource.VolumePanel, MuteSource.Mic));
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            volumePanelMusic.tag = localization[Localization.musicVol];
            volumePanelMic.tag = localization[Localization.micVol];
            volumePanelEffect.tag = localization[Localization.effectVol];
            timbrePnael.Text = localization[Localization.timbre];
            this.Invalidate();
            this.Update();
        }

        public override Color FillColor
        {
            get
            {
                return base.FillColor;
            }
            set
            {
                base.FillColor = value;
                this.volumePanelMusic.FillColor = value;
                this.volumePanelMic.FillColor = value;
                this.volumePanelEffect.FillColor = value;
                this.timbrePnael.FillColor = value;
                base.BackColor = value;
            }
        } 
    }
}
