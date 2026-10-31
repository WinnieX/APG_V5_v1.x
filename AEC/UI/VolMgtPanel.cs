using System;
using System.Collections.Generic;
using AEC.Core;
using AEC.Common;
using AEC.Misc;
using AEC.UI.Skin;

namespace AEC.UI
{
    public partial class VolMgtPanel : gtUserControl
    {
        public const short DefStartToMaxDelta = 6;

        private SystemModuleBase systemModule;
        private EventBus mainEventBus;

        public VolMgtPanel()
        {
            InitializeComponent();
            InitFont();
            this.title = "音量控制";
            this.sliderMusicStartVol.ValueChanged += Vol_ValueChanged;
            this.sliderMicStartVol.ValueChanged += Vol_ValueChanged;
            this.sliderEffectStartVol.ValueChanged += Vol_ValueChanged;
            this.sliderMusicLimitVol.ValueChanged += Vol_ValueChanged;
            this.sliderMicLimitVol.ValueChanged += Vol_ValueChanged;
            this.sliderEffectLimitVol.ValueChanged += Vol_ValueChanged;

            //this.sliderMusicLimitVol.LabelDoubleClick += LimitVol_LabelDoubleClick;
            //this.sliderMicLimitVol.LabelDoubleClick += LimitVol_LabelDoubleClick;
            //this.sliderEffectLimitVol.LabelDoubleClick += LimitVol_LabelDoubleClick;
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.lblMusicStartVol.Font = FontHelper.CreateFont10();
            this.lblMusicLimitVol.Font = FontHelper.CreateFont10();
            this.lblMicStartVol.Font = FontHelper.CreateFont10();
            this.lblMicLimitVol.Font = FontHelper.CreateFont10();
            this.lblEffectStartVol.Font = FontHelper.CreateFont10();
            this.lblEffectLimitVol.Font = FontHelper.CreateFont10();
        }

        public void InitTheme()
        {
            this.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.ForeColor = SkinManager.CurrentSkin.PanelForeColor;

            this.lblMusicStartVol.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblMusicLimitVol.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblMicStartVol.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblMicLimitVol.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblEffectStartVol.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblEffectLimitVol.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.sliderMusicStartVol.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.sliderMusicLimitVol.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.sliderMicStartVol.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.sliderMicLimitVol.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.sliderEffectStartVol.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.sliderEffectLimitVol.BackColor = SkinManager.CurrentSkin.PanelBackColor;
        }

        public void BindingSystemModule(SystemModuleBase systemModule_, EventBus eventBus_)
        {
            this.systemModule = systemModule_;

            this.RemoveEventBus();
            this.mainEventBus = eventBus_;
            this.RegisterEventBus();

            this.sliderMusicStartVol.Binding(this.systemModule.musicStartVolume, ParamGetLabelHelper.GetVolumeLabel);
            this.sliderMusicLimitVol.Binding(this.systemModule.musicMaxVolume, ParamGetLabelHelper.GetVolumeLabel);
            this.sliderMicStartVol.Binding(this.systemModule.micStartVolume, ParamGetLabelHelper.GetVolumeLabel);
            this.sliderMicLimitVol.Binding(this.systemModule.micMaxVolume, ParamGetLabelHelper.GetVolumeLabel);
            this.sliderEffectStartVol.Binding(this.systemModule.effectStartVolume, ParamGetLabelHelper.GetVolumeLabel);
            this.sliderEffectLimitVol.Binding(this.systemModule.effectMaxVolume, ParamGetLabelHelper.GetVolumeLabel);

            this.sliderMusicStartVol.upperLimit = this.systemModule.musicMaxVolume.v;
            this.sliderMicStartVol.upperLimit = this.systemModule.micMaxVolume.v;
            this.sliderEffectStartVol.upperLimit = this.systemModule.effectMaxVolume.v;
        }

        public void UpdateUIFromData()
        {
            //AEC.Log.OutputLine("music start vol:{0}", this.systemModule.musicStartVolume.v);
            this.sliderMusicStartVol.upperLimit = this.systemModule.musicMaxVolume.v;
            this.sliderMicStartVol.upperLimit = this.systemModule.micMaxVolume.v;
            this.sliderEffectStartVol.upperLimit = this.systemModule.effectMaxVolume.v;
            
            this.sliderMusicStartVol.UpdateUIFromData();
            this.sliderMusicLimitVol.UpdateUIFromData();
            this.sliderMicStartVol.UpdateUIFromData();
            this.sliderMicLimitVol.UpdateUIFromData();
            this.sliderEffectStartVol.UpdateUIFromData();
            this.sliderEffectLimitVol.UpdateUIFromData();
        }

        public void Close()
        {
            this.RemoveEventBus();
        }

        private void RegisterEventBus()
        {
            if (this.mainEventBus != null)
            {
                this.mainEventBus.RegisterHandler(EventBusHandler);
            }
        }

        private void RemoveEventBus()
        {
            if (this.mainEventBus != null)
            {
                this.mainEventBus.RemoveHandler(EventBusHandler);
            }
        }

        public void EventBusHandler(object sender, EventBusArgs e)
        {
            Dictionary<EventType, object> eventTypeSet = new Dictionary<EventType, object>()
            {
                {EventType.MusicStartVolumeChanged, null},
                {EventType.MicStartVolumeChanged, null},
                {EventType.EffectStartVolumeChanged, null},
                {EventType.MusicMaxVolumeChanged, null},
                {EventType.MicMaxVolumeChanged, null},
                {EventType.EffectMaxVolumeChanged, null},
            };

            if (!eventTypeSet.ContainsKey(e.type))
            {
                return;
            }

            VolumeChangedArgs vca = e as VolumeChangedArgs;
            if(vca.sender == null)
            {
                return;
            }

            switch (e.type)
            {
                case EventType.MusicStartVolumeChanged:
                    if(vca.sender != this.sliderMusicStartVol)
                    {
                        this.sliderMusicStartVol.UpdateUIFromData();
                    }
                    break;

                case EventType.MicStartVolumeChanged:
                    if (vca.sender != this.sliderMicStartVol)
                    {
                        this.sliderMicStartVol.UpdateUIFromData();
                    }
                    break;

                case EventType.EffectStartVolumeChanged:
                    if (vca.sender != this.sliderEffectStartVol)
                    {
                        this.sliderEffectStartVol.UpdateUIFromData();
                    }
                    break;

                case EventType.MusicMaxVolumeChanged:
                    if (vca.sender != this.sliderMusicLimitVol)
                    {
                        this.sliderMusicLimitVol.UpdateUIFromData();
                        this.sliderMusicStartVol.upperLimit = this.systemModule.musicMaxVolume.v;
                        if (this.systemModule.musicStartVolume.v > this.systemModule.musicMaxVolume.v)
                        {
                            this.systemModule.musicStartVolume.v = this.systemModule.musicMaxVolume.v;
                        }
                    }
                    break;

                case EventType.MicMaxVolumeChanged:
                    if (vca.sender != this.sliderMicLimitVol)
                    {
                        this.sliderMicLimitVol.UpdateUIFromData();
                        this.sliderMicStartVol.upperLimit = this.systemModule.micMaxVolume.v;
                        if (this.systemModule.micStartVolume.v > this.systemModule.micMaxVolume.v)
                        {
                            this.systemModule.micStartVolume.v = this.systemModule.micMaxVolume.v;
                        }
                    }
                    break;

                case EventType.EffectMaxVolumeChanged:
                    if (vca.sender != this.sliderEffectLimitVol)
                    {
                        this.sliderEffectLimitVol.UpdateUIFromData();
                        this.sliderEffectStartVol.upperLimit = this.systemModule.effectMaxVolume.v;
                        if (this.systemModule.effectStartVolume.v > this.systemModule.effectMaxVolume.v)
                        {
                            this.systemModule.effectStartVolume.v = this.systemModule.effectMaxVolume.v;
                        }
                    }
                    break;
            }
        }

        private void Vol_ValueChanged(object sender, EventArgs e)
        {
            if (this.mainEventBus == null)
            {
                return;
            }

            if (sender.Equals(this.sliderMusicStartVol))
            {
                this.mainEventBus.NotifyObeserver(new MusciStartVolumeChangedArgs(this.sliderMusicStartVol));
            }
            else if (sender.Equals(this.sliderMicStartVol))
            {
                this.mainEventBus.NotifyObeserver(new MicStartVolumeChangedArgs(this.sliderMicStartVol));
            }
            else if (sender.Equals(this.sliderEffectStartVol))
            {
                this.mainEventBus.NotifyObeserver(new EffectStartVolumeChangedArgs(this.sliderEffectStartVol));
            }
            else if (sender.Equals(this.sliderMusicLimitVol))
            {
                this.sliderMusicStartVol.upperLimit = this.systemModule.musicMaxVolume.v;
                if (this.systemModule.musicStartVolume.v > this.systemModule.musicMaxVolume.v)
                {
                    this.systemModule.musicStartVolume.v = this.systemModule.musicMaxVolume.v;
                }
                this.mainEventBus.NotifyObeserver(new MuiscMaxVolumeChangedArgs(this.sliderMusicLimitVol));
            }
            else if (sender.Equals(this.sliderMicLimitVol))
            {
                this.sliderMicStartVol.upperLimit = this.systemModule.micMaxVolume.v;
                if (this.systemModule.micStartVolume.v > this.systemModule.micMaxVolume.v)
                {
                    this.systemModule.micStartVolume.v = this.systemModule.micMaxVolume.v;
                }
                this.mainEventBus.NotifyObeserver(new MicMaxVolumeChangedArgs(this.sliderMicLimitVol));
            }
            else if (sender.Equals(this.sliderEffectLimitVol))
            {
                this.sliderEffectStartVol.upperLimit = this.systemModule.effectMaxVolume.v;
                if (this.systemModule.effectStartVolume.v > this.systemModule.effectMaxVolume.v)
                {
                    this.systemModule.effectStartVolume.v = this.systemModule.effectMaxVolume.v;
                }
                this.mainEventBus.NotifyObeserver(new EffectMaxVolumeChangedArgs(this.sliderEffectLimitVol));
            }
        }

        //private void LimitVol_LabelDoubleClick(object sender, EventArgs e)
        //{
        //    if (sender.Equals(this.sliderMusicLimitVol))
        //    {
        //        short oldVol = this.systemModule.musicStartVolume.v;
        //        short newVol = Utils.ConstrainMax((short)(oldVol + DefStartToMaxDelta), SystemModuleBase.MusicVolumeMax);
        //        if(newVol != oldVol)
        //        {
        //            this.systemModule.musicMaxVolume.v = newVol;
        //            this.sliderMusicLimitVol.UpdateUIFromData();
        //            this.sliderMusicStartVol.upperLimit = this.systemModule.musicMaxVolume.v;
        //            if (this.eventBus != null)
        //            {
        //                this.eventBus.NotifyObeserver(new MuiscMaxVolumeChangedArgs());
        //            }
        //        }
        //    }
        //    else if (sender.Equals(this.sliderMicLimitVol))
        //    {
        //        short oldVol = this.systemModule.micStartVolume.v;
        //        short newVol = Utils.ConstrainMax((short)(oldVol + DefStartToMaxDelta), SystemModuleBase.MicVolumeMax);
        //        if (newVol != oldVol)
        //        {
        //            this.systemModule.micMaxVolume.v = newVol;
        //            this.sliderMicLimitVol.UpdateUIFromData();
        //            this.sliderMicStartVol.upperLimit = this.systemModule.micMaxVolume.v;
        //            if (this.eventBus != null)
        //            {
        //                this.eventBus.NotifyObeserver(new MicMaxVolumeChangedArgs());
        //            }
        //        }
        //    }
        //    else if (sender.Equals(this.sliderEffectLimitVol))
        //    {
        //        short oldVol = this.systemModule.effectStartVolume.v;
        //        short newVol = Utils.ConstrainMax((short)(oldVol + DefStartToMaxDelta), SystemModuleBase.EffectVolumeMax);
        //        if (newVol != oldVol)
        //        {
        //            this.systemModule.effectMaxVolume.v = newVol;
        //            this.sliderEffectLimitVol.UpdateUIFromData();
        //            this.sliderEffectStartVol.upperLimit = this.systemModule.effectMaxVolume.v;
        //            if (this.eventBus != null)
        //            {
        //                this.eventBus.NotifyObeserver(new EffectMaxVolumeChangedArgs());
        //            }
        //        }
        //    }
        //}

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (this.systemModule == null)
            {
                return;
            }

            this.systemModule.musicStartVolume.v = this.systemModule.musicVolume.v;
            this.systemModule.micStartVolume.v = this.systemModule.micVolume.v;
            this.systemModule.effectStartVolume.v = this.systemModule.effectVolume.v;
            
            this.sliderMusicStartVol.UpdateUIFromData();
            this.sliderMicStartVol.UpdateUIFromData();
            this.sliderEffectStartVol.UpdateUIFromData();

            this.mainEventBus.NotifyObeserver(new MusciStartVolumeChangedArgs(this.sliderMusicStartVol));
            this.mainEventBus.NotifyObeserver(new MicStartVolumeChangedArgs(this.sliderMicStartVol));
            this.mainEventBus.NotifyObeserver(new EffectStartVolumeChangedArgs(this.sliderEffectStartVol));

            short maxMusicVolume = Utils.ConstrainMax((short)(this.systemModule.musicVolume.v + DefStartToMaxDelta), SystemModuleBase.MusicVolumeMax);
            short maxMicVolume = Utils.ConstrainMax((short)(this.systemModule.micVolume.v + DefStartToMaxDelta), SystemModuleBase.MicVolumeMax);
            short maxEffectVolume = Utils.ConstrainMax((short)(this.systemModule.effectVolume.v + DefStartToMaxDelta), SystemModuleBase.EffectVolumeMax);

            if (maxMusicVolume != this.systemModule.musicMaxVolume.v)
            {
                this.systemModule.musicMaxVolume.v = maxMusicVolume;
                this.sliderMusicLimitVol.UpdateUIFromData();
                this.sliderMusicStartVol.upperLimit = this.systemModule.musicMaxVolume.v;
                if (this.mainEventBus != null)
                {
                    this.mainEventBus.NotifyObeserver(new MuiscMaxVolumeChangedArgs(this.sliderMusicLimitVol));
                }
            }

            if (maxMicVolume != this.systemModule.micMaxVolume.v)
            {
                this.systemModule.micMaxVolume.v = maxMicVolume;
                this.sliderMicLimitVol.UpdateUIFromData();
                this.sliderMicStartVol.upperLimit = this.systemModule.micMaxVolume.v;
                if (this.mainEventBus != null)
                {
                    this.mainEventBus.NotifyObeserver(new MicMaxVolumeChangedArgs(this.sliderMicLimitVol));
                }
            }

            if (maxEffectVolume != this.systemModule.effectMaxVolume.v)
            {
                this.systemModule.effectMaxVolume.v = maxEffectVolume;
                this.sliderEffectLimitVol.UpdateUIFromData();
                this.sliderEffectStartVol.upperLimit = this.systemModule.effectMaxVolume.v;
                if (this.mainEventBus != null)
                {
                    this.mainEventBus.NotifyObeserver(new EffectMaxVolumeChangedArgs(this.sliderEffectLimitVol));
                }
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.title = localization[Localization.panelSystem_VolMgt];

            this.lblMusicStartVol.Text = localization[Localization.panelSystem_VolMgt_MusicStartVolume];
            this.lblMusicLimitVol.Text = localization[Localization.panelSystem_VolMgt_MusicMaxVolume];
            this.lblMicStartVol.Text = localization[Localization.panelSystem_VolMgt_MicStartVolume];
            this.lblMicLimitVol.Text = localization[Localization.panelSystem_VolMgt_MicMaxVolume];
            this.lblEffectStartVol.Text = localization[Localization.panelSystem_VolMgt_EffectStartVolume];
            this.lblEffectLimitVol.Text = localization[Localization.panelSystem_VolMgt_EffectMaxVolume];

            this.btnQuickSettings.Text = localization[Localization.panelSystem_VolMgt_QuickSettings];

            this.Invalidate();
        }
    }
}
