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
using AEC.Effectors.VKxSCM;
using AEC.Effectors.VKxSCM.Modules;
using AEC.Misc;

namespace AEC.UI.VKxSCM
{
    public partial class VKxSCMOutputBasePanel : PanelBase
    {
        //public const string DelayUnit = "ms";
        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private OutputBaseModule outputBase;

        private Image _sliderNormalBack;

        protected virtual Image mixerSliderBack
        {
            set
            {
                this.mixerPanel.backgroundNormal = value;
            }
        }

        protected virtual Image sliderNormalBack
        { 
            get
            {
                return _sliderNormalBack;
            }
            set
            {
                this._sliderNormalBack = value;
                this.limiterPanel.sliderNormalBack = value;
                this.sliderHpfFreq.backgroundNormal = value;
                this.sliderLpfFreq.backgroundNormal = value;
            }
        }
        
        public VKxSCMOutputBasePanel()
            : base()
        {
            InitializeComponent();
            this.InitControl(this.eqGroupPanel, this.sliderHpfFreq, this.sliderLpfFreq);
            //this.cmbHpfMode.DrawMode = DrawMode.OwnerDrawFixed;

            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
        }

        protected virtual void InitFont()
        {
            this.labelHpfFreq.Font = FontHelper.CreateFont10();
            this.labelHpfMode.Font = FontHelper.CreateFont10();
            //this.cmbHpfMode.Font = FontHelper.CreateLabelFont10();
            this.labelLpfFreq.Font = FontHelper.CreateFont10();
            this.labelLpfMode.Font = FontHelper.CreateFont10();
            //this.cmbLpfMode.Font = FontHelper.CreateLabelFont10();
        }

        //public virtual void BindingEffector(Effector effector_)
        //{
        //    //Debug.Assert()
        //    //if (this.baseModule != null)
        //    //{
        //    //    this.baseModule.eventBus.RemoveHandler(HPFChangedFromGraphicHandler);
        //    //}
        //    //this.music = effector_.music;
        //    //this.baseModule = this.music;
        //    //base.BindingEffector(effector_);
        //    //this.
        //}

        protected void BindingOutputBaseModule(OutputBaseModule outputBase_)
        {
            this.outputBase = outputBase_;
            base.BindingModule(this.outputBase, MEffectorFamily.VKxSCM);
            this.mixerPanel.Binding(this.outputBase, base._eventBus);
            this.limiterPanel.Binding(this.outputBase.limiter, base._eventBus, this.outputBase.outputChannelType, base.mController.mEffectorThreshold);
            
            this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
            this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;

            this.WrokModeChanged();

            this.eqResetAndByPassPanel.Init(this.outputBase.eqGroup.isBypass, this.outputBase.themeColor);
            this.eqResetAndByPassPanel.Enabled = this.outputBase.autoWorkMode.isManual;

            this.workModePanel.Init(this.outputBase.autoWorkMode, this._eventBus, this.outputBase.themeColor);

            this.RegisterCmbSelectedIndexChangedHandler();

            if (this._eventBus != null)
            {
                this._eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
            }
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.mixerPanel.UpdateUIFromData(base.mController.currEffector.effectorModel.system);
            this.limiterPanel.UpdateUIFromData();
            
            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
            this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.eqResetAndByPassPanel.isPass = this.outputBase.eqGroup.isBypass;
            this.mixerPanel.Enabled = this.outputBase.autoWorkMode.isManual;
            this.workModePanel.UpdateUIFromData();
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            this.cmbHpfType.SelectedIndexChanged += cmbFilterType_SelectedIndexChanged;
            this.cmbLpfType.SelectedIndexChanged += cmbFilterType_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbHpfType.SelectedIndexChanged -= cmbFilterType_SelectedIndexChanged;
            this.cmbLpfType.SelectedIndexChanged -= cmbFilterType_SelectedIndexChanged;
        }

        private void cmbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if (sender.Equals(this.cmbHpfType))
            {
                //this.outputBase.hpf.UpdateTypeParam() = (short)cmb.SelectedIndex;
                this.outputBase.hpf.typeIndexParam = (short)cmb.SelectedIndex;
                this.outputBase.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Type);                
                //this.sliderHpfQ.Invalidate();
            }
            else if(sender.Equals(this.cmbLpfType))
            {
                this.outputBase.lpf.typeIndexParam = (short)cmb.SelectedIndex;
                this.outputBase.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Type);
                //this.sliderLpfQ.Invalidate();
            }
        }

        private static string GetFilterQLabel(short index)
        {
            return DataUtils.FilterQIndex2String(index);  
        }

        protected short CalcFilterQIndexFromText(string text)
        {
            if (text.ToLower().Replace(" ", "") == Off.ToLower())
            {
                return 0;
            }

            text = text.Trim();
            double value;
            if (double.TryParse(text, out value))
            {
                return DataUtils.FilterQ2FilterIndex(value);
            }

            return -1;
        }

        protected override void WrokModeChanged()
        {
            this.mixerPanel.Enabled = this.outputBase.autoWorkMode.isManual;
        }

        protected override void WrokSelectChanged()
        {
            this.mixerPanel.UpdateUIFromData(null);
        }

        protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {
            if (filterType == XPFilterType.HighPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.RemoveCmbSelectedIndexChangedHandler();
                    this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
                    this.RegisterCmbSelectedIndexChangedHandler();
                }
            }
            else if (filterType == XPFilterType.LowPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.RemoveCmbSelectedIndexChangedHandler();
                    this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;
                    this.RegisterCmbSelectedIndexChangedHandler();
                }
            }
        }

        //protected override void HPFChanged(FilterElement filterElement)
        //{
        //    if(filterElement == FilterElement.Type)
        //    {
        //        this.outputBase.hpfTypeParam.v = this.outputBase.hpfModeInit;
        //        this.RemoveCmbSelectedIndexChangedHandler();
        //        this.cmbHpfMode.SelectedIndex = this.outputBase.hpfTypeParam.v;
        //        this.RegisterCmbSelectedIndexChangedHandler();
        //        this.sliderHpfQ.Enabled = this.outputBase.hpf.qCanBeChanged;
        //    }
        //    else if (filterElement == FilterElement.Q)
        //    {
        //        this.outputBase.hpfQParam.v = this.outputBase.hpf.qIndex;
        //        this.sliderHpfQ.UpdateUIFromData();
        //    }
        //}

        //protected override void LPFChanged(FilterElement filterElement)
        //{
        //    if (filterElement == FilterElement.Type)
        //    {
        //        this.outputBase.lpfTypeParam.v = this.outputBase.lpfModeInit;
        //        this.RemoveCmbSelectedIndexChangedHandler();
        //        this.cmbLpfMode.SelectedIndex = this.outputBase.lpfTypeParam.v;
        //        this.RegisterCmbSelectedIndexChangedHandler();
        //        this.sliderLpfQ.Enabled = this.outputBase.lpf.qCanBeChanged;
        //    }
        //    else if (filterElement == FilterElement.Q)
        //    {
        //        this.outputBase.lpfQParam.v = this.outputBase.lpfQIndex;
        //        this.sliderLpfQ.UpdateUIFromData();
        //    }
        //}

        public override void UpdateDisplayLanguage()
        {
            this.labelHpfFreq.Text = localization[Localization.panelOutput_HpfFreq];
            this.labelHpfMode.Text = localization[Localization.panelOutput_HpfMode];
            this.labelLpfFreq.Text = localization[Localization.panelOutput_LpfFreq];
            this.labelLpfMode.Text = localization[Localization.panelOutput_LpfMode];
            
            this.mixerPanel.UpdateDisplayLanguage();
            this.limiterPanel.UpdateDisplayLanguage();
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();
            this.workModePanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        public override void UpdateEffectorModel()
        {
            this.limiterPanel.UpdateThresholdUI();
        }

        protected override void HpfFreqChangedCallback()
        {
            base._eventBus.NotifyObeserver(new OutputLimiterParamChangedArgs(this.outputBase.outputChannelType, OutputParamChangedSource.HpfFreqChanged, LimiterParam.Attack));
        }

        protected void SwitchChannelDelayUnit()
        {
            config.SwitchChannelDelayUnit();
            this._eventBus.NotifyObeserver(new ChannelDelayUnitChangedArgs());
        }
    }
}
