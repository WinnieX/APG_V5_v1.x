using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Misc;
using AEC.Effectors;
using AEC.Effectors.VKxS;
using AEC.Effectors.VKxS.Modules;
using AEC.UI.Controls;
using AEC.UI.Common;

namespace AEC.UI.VKxS
{
    //Compressor/Limiter 压限控制面板
    public partial class VKxSCLPanel : AEC.UI.Controls.MContainerControl
    {
        private OutputBaseModule output;
        private Limiter limiter;
        private EventBus eventBus;
        private OutputChannelType outputChannel;
        private MEffectorThreshold effectorThreshold;

        private PopMenuThresholdUint popMenuThresholdUint;

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        public VKxSCLPanel()
        {
            InitializeComponent();
            InitFont();
            InitStyle();

            this.components = new System.ComponentModel.Container();
            this.popMenuThresholdUint = new PopMenuThresholdUint(this.components);
        }

        private void InitStyle()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void InitFont()
        {
            this.lblTtile.Font = FontHelper.CreateFont10();
            this.lblThreshold.Font = FontHelper.CreateFont10();
            this.lblAttack.Font = FontHelper.CreateFont10();
            this.lblRelease.Font = FontHelper.CreateFont10();
            this.lblClipThreshold.Font = FontHelper.CreateFont10();
            this.lblClipRelease.Font = FontHelper.CreateFont10();
        }

        public void Binding(OutputBaseModule output_, EventBus eventBus_, OutputChannelType channel_, MEffectorThreshold effectorThreshold_)
        {
            this.output = output_;
            this.limiter = output_.limiter;
            this.eventBus = eventBus_;
            this.outputChannel = channel_;
            this.effectorThreshold = effectorThreshold_;

            this.popMenuThresholdUint.BindingEventBus(this.eventBus);

            this.paramThreshold.Init(Limiter.MaxThreshold, limiter.threshold.v, this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            this.paramAttack.Init(Limiter.MaxAttack, limiter.attack.v, Limiter.GetAttackLabel, Limiter.CalcAttackIndexFromText);
            this.paramRelease.Init(Limiter.MaxRelease, limiter.release.v, Limiter.GetReleaseLabel, Limiter.CalcReleaseIndexFromText);
            this.paramClipThreshold.Init(Limiter.MaxClipThreshold, limiter.clipThreshold.v, Limiter.GetClipThresholdLabel, Limiter.CalcClipThresholdIndexFromText);
            this.paramClipRelease.Init(Limiter.MaxRelease, limiter.clipRelease.v, Limiter.GetReleaseLabel, Limiter.CalcReleaseIndexFromText);

            this.paramThreshold.ValueChanged += param_ValueChanged;
            this.paramAttack.ValueChanged += param_ValueChanged;
            this.paramRelease.ValueChanged += param_ValueChanged;
            this.paramClipThreshold.ValueChanged += param_ValueChanged;
            this.paramClipRelease.ValueChanged += param_ValueChanged;

            this.UpdateAutoStatus();

            this.eventBus.RegisterHandler(EventBusHandler);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.ReLayout();
        }

        private void ReLayout()
        {
            this.lblTtile.Location = new Point((this.Width - this.lblTtile.Width) / 2, this.lblTtile.Location.Y);
            this.chkAuto.Location = new Point((this.Width - this.chkAuto.Width) / 2, this.chkAuto.Location.Y);
            this.lblThreshold.Location = new Point((this.Width - this.lblThreshold.Width) / 2, this.lblThreshold.Location.Y);
            this.paramThreshold.Location = new Point((this.Width - this.paramThreshold.Width) / 2, this.paramThreshold.Location.Y);
            this.lblAttack.Location = new Point((this.Width - this.lblAttack.Width) / 2, this.lblAttack.Location.Y);
            this.paramAttack.Location = new Point((this.Width - this.paramAttack.Width) / 2, this.paramAttack.Location.Y);
            this.lblRelease.Location = new Point((this.Width - this.lblRelease.Width) / 2, this.lblRelease.Location.Y);
            this.paramRelease.Location = new Point((this.Width - this.paramRelease.Width) / 2, this.paramRelease.Location.Y);
            this.lblClipThreshold.Location = new Point((this.Width - this.lblClipThreshold.Width) / 2, this.lblClipThreshold.Location.Y);
            this.paramClipThreshold.Location = new Point((this.Width - this.paramClipThreshold.Width) / 2, this.paramClipThreshold.Location.Y);
            this.lblClipRelease.Location = new Point((this.Width - this.lblClipRelease.Width) / 2, this.lblClipRelease.Location.Y);
            this.paramClipRelease.Location = new Point((this.Width - this.paramClipRelease.Width) / 2, this.paramClipRelease.Location.Y);
        }

        private void UpdateAutoStatus()
        {
            this.chkAuto.isChecked = limiter.auto;
            this.paramAttack.Enabled = !limiter.auto;
            this.paramRelease.Enabled = !limiter.auto;
            this.paramClipThreshold.Enabled = !limiter.auto;
            this.paramClipRelease.Enabled = !limiter.auto;
        }

        private void param_ValueChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(this.limiter != null && this.eventBus != null);

            LimiterParam limiterParam = LimiterParam.All;

            ParamLabel param = sender as ParamLabel;
            if(object.Equals(param, this.paramThreshold))
            {
                this.limiter.threshold.v = param.v;
                limiterParam = LimiterParam.Threshold;
            }
            else if(object.Equals(param, this.paramAttack))
            {
                this.limiter.attack.v = param.v;
                limiterParam = LimiterParam.Attack;
            }
            else if (object.Equals(param, this.paramRelease))
            {
                this.limiter.release.v = param.v;
                limiterParam = LimiterParam.Release;
            }
            else if (object.Equals(param, this.paramClipThreshold))
            {
                this.limiter.clipThreshold.v = param.v;
                limiterParam = LimiterParam.ClipThreshold;
            }
            else if (object.Equals(param, this.paramClipRelease))
            {
                this.limiter.clipRelease.v = param.v;
                limiterParam = LimiterParam.ClipRelease;
            }

            this.eventBus.NotifyObeserver(new OutputLimiterParamChangedArgs(this.outputChannel, OutputParamChangedSource.MonitorPanel, limiterParam));
        }

        private void EventBusHandler(object sender, EventBusArgs e)
        {
            if(e.type == EventType.OutputLimiterParamChanged)
            {
                LimiterParamChangedHandler(e as OutputLimiterParamChangedArgs);
            }
            else if(e.type == EventType.LimiterThresholdUnitChanged)
            {
                LimiterThresholdUnitChangedHandler();
            }
        }

        private void LimiterThresholdUnitChangedHandler()
        {
            this.UpdateThresholdUI();
        }

        private void LimiterParamChangedHandler(OutputLimiterParamChangedArgs olp)
        {
            if(olp.source == OutputParamChangedSource.MonitorPanel)
            {
                return;
            }

            switch(olp.limiterParam)
            {
                case LimiterParam.Threshold:
                    this.paramThreshold.initV = this.limiter.threshold.v;
                    break;

                case LimiterParam.Attack:
                    this.paramAttack.initV = this.limiter.attack.v;
                    break;

                case LimiterParam.Release:
                    this.paramRelease.initV = this.limiter.release.v;
                    break;

                case LimiterParam.ClipThreshold:
                    this.paramClipThreshold.initV = this.limiter.clipThreshold.v;
                    break;

                case LimiterParam.ClipRelease:
                    this.paramClipRelease.initV = this.limiter.clipRelease.v;
                    break;
            }
        }

        public void UpdateUIFromData()
        {
            System.Diagnostics.Debug.Assert(limiter != null);

            this.paramThreshold.Init(Limiter.MaxThreshold, limiter.threshold.v, this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);

            if (this.paramThreshold.v != this.limiter.threshold.v)
            {
                this.paramThreshold.initV = this.limiter.threshold.v;
            }
            if (this.paramAttack.v != this.limiter.attack.v)
            {
                this.paramAttack.initV = this.limiter.attack.v;
            }
            if (this.paramRelease.v != this.limiter.release.v)
            {
                this.paramRelease.initV = this.limiter.release.v;
            }
            if (this.paramClipThreshold.v != this.limiter.clipThreshold.v)
            {
                this.paramClipThreshold.initV = this.limiter.clipThreshold.v;
            }
            if (this.paramClipRelease.v != this.limiter.clipRelease.v)
            {
                this.paramClipRelease.initV = this.limiter.clipRelease.v;
            }

            this.UpdateAutoStatus();
        }

        public void UpdateThresholdUI()
        {
            this.paramThreshold.Init(Limiter.MaxThreshold, limiter.threshold.v, this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            this.paramThreshold.initV = this.limiter.threshold.v;
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.chkAuto.Text = localization[Localization.autoMode];
            this.lblTtile.Text = localization[Localization.limiterPanel];
            this.lblThreshold.Text = localization[Localization.limiterPanel_Threshold];
            this.lblAttack.Text = localization[Localization.limiterPanel_Attack];
            this.lblRelease.Text = localization[Localization.limiterPanel_Release];
            this.lblClipThreshold.Text = localization[Localization.limiterPanel_ClipThreshold];
            this.lblClipRelease.Text = localization[Localization.limiterPanel_ClipRelease];

            this.Invalidate();
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            this.limiter.auto = this.chkAuto.isChecked;

            if(this.limiter.auto)
            {
                this.limiter.AutoModeChangedHandler(this.output.hpf.freq);
                this.UpdateUIFromData();
                this.eventBus.NotifyObeserver(new OutputLimiterParamChangedArgs(this.outputChannel, OutputParamChangedSource.MonitorPanel, LimiterParam.All));
            }
            else
            {
                this.UpdateAutoStatus();
                this.eventBus.NotifyObeserver(new OutputLimiterParamChangedArgs(this.outputChannel, OutputParamChangedSource.MonitorPanel, LimiterParam.Auto));
            }
        }

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            config.SwitchThresholdUnit();
            this.eventBus.NotifyObeserver(new LimiterThresholdUnitChangedArgs());
        }

        private void KXCLPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenuThresholdUint.Show();
            }
        }
    }
}
