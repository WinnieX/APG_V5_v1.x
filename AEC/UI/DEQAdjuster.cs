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
using AEC.Effectors;
using AEC.Misc;

namespace AEC.UI
{
    public partial class DEQAdjuster : UserControl
    {
        private const short threshold0775V = 6; //0x06
        private const short threshold1V = 9;   //0x09
        private const short threshold14V = 12;  //0x0C

        private DEQ deq;
        private MEffectorThreshold effectorThreshold;

        private readonly EQIndexLabel indexLabel = new EQIndexLabel();

        public event EventHandler<DEQParamUpdatedArgs> DEQParamUpdated;
        public event EventHandler<EventArgs> EQChanged;
        public event EventHandler<EventArgs> Activated;


        public bool isActiveInit
        {
            get
            {
                return this.indexLabel.isActive;
            }
            set
            {
                if (this.indexLabel.isActive != value)
                {
                    this.indexLabel.isActive = value;
                    this.eqAdjuster.isActiveInit = value;
                    this.Invalidate(this.indexLabel.labelRect);
                    this.Update();
                }
            }
        }

        public Image activeIndexLabelBackground 
        { 
            get
            {
                return this.indexLabel.activeBackground;
            }
            set
            {
                this.indexLabel.activeBackground = value;
            }
        }

        public Color activeIndexLabelColor
        {
            get
            {
                return this.indexLabel.activeColor;
            }
            set
            {
                this.indexLabel.activeColor = value;
            }
        }

        public EQTypeImage eqTypeImage
        {
            set
            {
                this.eqAdjuster.eqTypeImage = value;
            }
        }

        public Image sliderBackgroundNormal
        {
            set
            {
                this.sliderThreshold.backgroundNormal = value;
                this.sliderAttack.backgroundNormal = value;
                this.sliderRelease.backgroundNormal = value;
                this.sliderRatio.backgroundNormal = value;
            }
        }

        public DEQAdjuster()
        {
            InitializeComponent();
            this.Init();

            this.sliderThreshold.ValueChanged += slider_ValueChanged;
            this.sliderAttack.ValueChanged += slider_ValueChanged;
            this.sliderRelease.ValueChanged += slider_ValueChanged;
            this.sliderRatio.ValueChanged += slider_ValueChanged;

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.lblCM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.labelFilterType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.labelGain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.labelFreq.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.labelQ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.lblAmpSensitivity.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.lblThreshold.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.lblAttack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.lblRelease.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.lblRatio.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.lblDM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            
            this.rdoOff.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.rdoBelow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.rdoAbove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);

            this.sliderThreshold.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.sliderAttack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.sliderRelease.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.sliderRatio.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.rdoFast.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.rdoMedium.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);
            this.rdoSlow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DEQAdjuster_MouseDown);

            this.eqAdjuster.EQChanged += DEQAdjuster_EQChanged;
            this.eqAdjuster.Activated += DEQAdjuster_Activated;
        }
        

        private void Init()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.indexLabel.PaintIndexLabel(e.Graphics);
        }

        public void BindingDEQ(DEQ deq_, MEffectorThreshold effectorThreshold_)
        {
            Debug.Assert(deq_ != null && effectorThreshold_ != null);

            this.deq = deq_;
            this.effectorThreshold = effectorThreshold_;
            this.eqAdjuster.BindingEQ(this.deq);
            this.indexLabel.index = this.deq.index;
            this.chkEQPass.isChecked = this.deq.isPass;
            this.UpdateEQAdjusterCanBeChanged();
            this.InitDeqCM();
            this.sliderThreshold.Binding(this.deq.threshold, this.effectorThreshold.GetThresholdLabelIndBu, this.effectorThreshold.CalcThresholdIndexFromTextIndBu);
            this.sliderAttack.Binding(this.deq.attack, ParamGetLabelHelper.GetAttackLabel, ParamGetIndexHelper.CalcAttackIndexFromText);
            this.sliderRelease.Binding(this.deq.release, ParamGetLabelHelper.GetReleaseLabel, ParamGetIndexHelper.CalcReleaseIndexFromText);
            this.sliderRatio.Binding(this.deq.ratio, ParamGetLabelHelper.GetRatioLabel, ParamGetIndexHelper.CalcRatioIndexFromText);
            this.InitDM();
        }

        private void DEQAdjuster_EQChanged(object sender, EventArgs e)
        {
            if(this.EQChanged != null)
            {
                this.EQChanged(this.deq, EventArgs.Empty);
            }
        }

        private void DEQAdjuster_Activated(object sender, EventArgs e)
        {
            if(this.Activated != null && this.deq != null)
            {
                this.Activated(this.deq, EventArgs.Empty);
            }
        }

        private void DEQAdjuster_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();

            if (this.Activated != null)
            {
                this.Activated(this.deq, EventArgs.Empty);
            }
        }

        public void UpdateEQUIFromData()
        {
            this.eqAdjuster.UpdateUIFromData();
        }

        public void UpdateUIFromData()
        {
            this.eqAdjuster.UpdateUIFromData();
            this.chkEQPass.isChecked = this.deq.isPass;
            this.chkEQPass.ForeColor = this.deq.isPass ? this.indexLabel.activeColor : Color.LightGray;
            this.UpdateEQAdjusterCanBeChanged();
            this.InitDeqCM();
            this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabelIndBu, this.effectorThreshold.CalcThresholdIndexFromTextIndBu);
            this.sliderThreshold.UpdateUIFromData();
            this.sliderAttack.UpdateUIFromData();
            this.sliderRelease.UpdateUIFromData();
            this.sliderRatio.UpdateUIFromData();
            this.InitDM();
        }

        public void UpdateEffectorModel()
        {
            this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabelIndB, this.effectorThreshold.CalcThresholdIndexFromTextIndB);
            this.sliderThreshold.UpdateUIFromData();
        }

        public void UpdateEQ()
        {
            this.eqAdjuster.UpdateUIFromData();
        }

        public void UpdateEQIsPass()
        {
            this.chkEQPass.isChecked = this.deq.isPass;
            this.chkEQPass.ForeColor = this.deq.isPass ? this.indexLabel.activeColor : Color.LightGray;
        }

        public void InitDeqCM()
        {
            Debug.Assert(this.deq != null);

            this.rdoOff.isCheckInit = this.deq.dcm == DEQControlMode.Off;
            this.rdoBelow.isCheckInit = this.deq.dcm == DEQControlMode.Below;
            this.rdoAbove.isCheckInit = this.deq.dcm == DEQControlMode.Above;
            this.panelMode.Enabled = this.deq.dcm != DEQControlMode.Off;

            this.rdoOff.Invalidate();
            this.rdoOff.Update();
            this.rdoBelow.Invalidate();
            this.rdoBelow.Update();
            this.rdoAbove.Invalidate();
            this.rdoAbove.Update();
        }

        private void InitDM()
        {
            Debug.Assert(this.deq != null);

            this.rdoFast.isCheckInit = this.deq.dm == CompressorMode.Fast;
            this.rdoMedium.isCheckInit = this.deq.dm == CompressorMode.Medium;
            this.rdoSlow.isCheckInit = this.deq.dm == CompressorMode.Slow;

            this.rdoFast.Invalidate();
            this.rdoFast.Update();
            this.rdoMedium.Invalidate();
            this.rdoMedium.Update();
            this.rdoSlow.Invalidate();
            this.rdoSlow.Update();
        }

        private void rdo_CheckedChanged(object sender, EventArgs e)
        {
            gtRadioButton rdo = sender as gtRadioButton;
            if(!rdo.isChecked)
            {
                return;
            }

            if (rdo.Equals(this.rdoOff))
            {
                SwitchDeqCM(DEQControlMode.Off);
            }
            else if (rdo.Equals(this.rdoBelow))
            {
                SwitchDeqCM(DEQControlMode.Below);
            }
            else if (rdo.Equals(this.rdoAbove))
            {
                SwitchDeqCM(DEQControlMode.Above);
            }
            else if (rdo.Equals(this.rdoFast))
            {
                SwitchDeqDM(CompressorMode.Fast);
            }
            else if(rdo.Equals(this.rdoMedium))
            {
                SwitchDeqDM(CompressorMode.Medium);
            }
            else if(rdo.Equals(this.rdoSlow))
            {
                SwitchDeqDM(CompressorMode.Slow);
            }
        }

        private void SwitchDeqCM(DEQControlMode mode)
        {
            this.deq.dcm = mode;
            this.DEQParamUpdatedHandler(DeqParamType.DCM);
            this.panelMode.Enabled = this.deq.dcm != DEQControlMode.Off;
        }

        private void SwitchDeqDM(CompressorMode mode)
        {
            this.deq.dm = mode;
            this.DEQParamUpdatedHandler(DeqParamType.DM);
        }

        private void chkEQPass_CheckedChanged(object sender, EventArgs e)
        {
            this.deq.isPass = this.chkEQPass.isChecked;
            this.UpdateEQAdjusterCanBeChanged();            
            this.DEQParamUpdatedHandler(DeqParamType.DeqIsPass);
        }

        public void UpdateEQAdjusterCanBeChanged()
        {
            this.eqAdjuster.canBeChanged = !this.deq.isPass;
            this.chkEQPass.ForeColor = this.deq.isPass ? this.indexLabel.activeColor : Color.LightGray;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btn0775V))
            {
                this.deq.threshold.v = threshold0775V;
            }
            else if(sender.Equals(this.btn1V))
            {
                this.deq.threshold.v = threshold1V;
            }
            else if(sender.Equals(this.btn14V))
            {
                this.deq.threshold.v = threshold14V;
            }
            this.sliderThreshold.UpdateUIFromData();

            this.DEQParamUpdatedHandler(DeqParamType.Threshold);
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.sliderThreshold))
            {
                this.DEQParamUpdatedHandler(DeqParamType.Threshold);
            }
            else if(sender.Equals(this.sliderAttack))
            {
                this.DEQParamUpdatedHandler(DeqParamType.Attack);
            }
            else if(sender.Equals(this.sliderRelease))
            {
                this.DEQParamUpdatedHandler(DeqParamType.Release);
            }
            else if(sender.Equals(this.sliderRatio))
            {
                this.DEQParamUpdatedHandler(DeqParamType.Ratio);
            }
        }

        public void DEQParamUpdatedHandler(DeqParamType type_)
        {
            Debug.Assert(this.deq != null);

            if(this.DEQParamUpdated != null)
            {
                this.DEQParamUpdated(this.deq, new DEQParamUpdatedArgs(this.deq, type_));
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblCM.Text = localization[Localization.deq_CM];
            this.labelFilterType.Text = localization[Localization.eq_Type];
            this.labelGain.Text = localization[Localization.eq_Gain];
            this.labelFreq.Text = localization[Localization.eq_Freq];
            this.labelQ.Text = localization[Localization.eq_Q];
            this.lblAmpSensitivity.Text = localization[Localization.deq_AmpSen];
            this.lblThreshold.Text = localization[Localization.deq_Threshold];
            this.lblAttack.Text = localization[Localization.deq_Attack];
            this.lblRelease.Text = localization[Localization.deq_Release];
            this.lblRatio.Text = localization[Localization.deq_Ratio];
            this.lblDM.Text = localization[Localization.deq_DM];
            this.rdoFast.Text = localization[Localization.fastMode];
            this.rdoMedium.Text = localization[Localization.mediumMode];
            this.rdoSlow.Text = localization[Localization.slowMode];
        }
    }
}
