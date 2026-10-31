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
using AEC.Effectors.MX;
using AEC.Effectors.MX.Modules;
using AEC.Misc;

namespace AEC.UI.MX
{
    public partial class MXOutputBasePanel : PanelBase
    {
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
                this.compressorPanel.sliderNormalBack = value;
                this.sliderHpfFreq.backgroundNormal = value;
                this.sliderHpfQ.backgroundNormal = value;
                this.sliderLpfFreq.backgroundNormal = value;
                this.sliderLpfQ.backgroundNormal = value;
            }
        }

        public MXOutputBasePanel()
            : base()
        {
            InitializeComponent();
            this.InitControl(this.eqGroupPanel, this.sliderHpfFreq, this.sliderLpfFreq);
            this.sliderHpfQ.Actived += FilterQ_Actived;
            this.sliderHpfQ.ValueChanged += FilterQ_ValueChanged;
            this.sliderLpfQ.Actived += FilterQ_Actived;
            this.sliderLpfQ.ValueChanged += FilterQ_ValueChanged;
            //this.cmbHpfMode.DrawMode = DrawMode.OwnerDrawFixed;

            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
        }

        protected virtual void InitFont()
        {
            this.labelHpfFreq.Font = FontHelper.CreateFont10();
            this.labelHpfQ.Font = FontHelper.CreateFont10();
            this.labelHpfMode.Font = FontHelper.CreateFont10();
            //this.cmbHpfMode.Font = FontHelper.CreateLabelFont10();
            this.labelLpfFreq.Font = FontHelper.CreateFont10();
            this.labelLpfQ.Font = FontHelper.CreateFont10();
            this.labelLpfMode.Font = FontHelper.CreateFont10();
            //this.cmbLpfMode.Font = FontHelper.CreateLabelFont10();
        }

        private void FilterQ_Actived(object sender, EventArgs e)
        {
            if (this.outputBase == null)
            {
                return;
            }

            if(sender.Equals(this.sliderHpfQ))
            {
                if (this.outputBase.activeItem != ActiveItem.HPF)
                {
                    this.outputBase.activeItem = ActiveItem.HPF;
                    this.outputBase.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if(sender.Equals(this.sliderLpfQ))
            {
                if (this.outputBase.activeItem != ActiveItem.LPF)
                {
                    this.outputBase.activeItem = ActiveItem.LPF;
                    this.outputBase.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
        }

        private void FilterQ_ValueChanged(object sender, EventArgs e)
        {
            if (this.outputBase == null)
            {
                return;
            }

            if (sender.Equals(this.sliderHpfQ))
            {
                outputBase.hpf.UpdateQIndexFromParam();
                outputBase.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Q);
            }
            else if (sender.Equals(this.sliderLpfQ))
            {
                outputBase.lpf.UpdateQIndexFromParam();
                outputBase.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Q);
            }

            //this.outputBase.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
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
            base.BindingModule(this.outputBase, MEffectorFamily.MX);
            this.mixerPanel.Binding(this.outputBase);
            this.compressorPanel.Binding(this.outputBase.compressor, base._eventBus, this.mController.mEffectorThreshold);
            this.sliderHpfQ.Binding(this.outputBase.hpf.qParam, GetFilterQLabel, CalcFilterQIndexFromText);
            this.sliderLpfQ.Binding(this.outputBase.lpf.qParam, GetFilterQLabel, CalcFilterQIndexFromText);
            this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
            this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;

            this.sliderHpfQ.Enabled = this.outputBase.hpf.qCanBeChanged;
            this.sliderLpfQ.Enabled = this.outputBase.lpf.qCanBeChanged;

            this.WrokModeChanged();

            this.eqResetAndByPassPanel.Init(this.outputBase.eqGroup.isBypass, this.outputBase.themeColor);
            this.eqResetAndByPassPanel.Enabled = this.outputBase.music.isManual;

            //this.workModePanel.Init(this.outputBase.music, this._eventBus, this.outputBase.themeColor);

            this.RegisterCmbSelectedIndexChangedHandler();

            if (this._eventBus != null)
            {
                this._eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
            }
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.mixerPanel.UpdateUIFromData();
            this.compressorPanel.UpdateUIFromData();
            this.sliderHpfQ.UpdateUIFromData();
            this.sliderLpfQ.UpdateUIFromData();

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
            this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.sliderHpfQ.Enabled = this.outputBase.hpf.qCanBeChanged;
            this.sliderLpfQ.Enabled = this.outputBase.lpf.qCanBeChanged;

            this.mixerPanel.Enabled = this.outputBase.music.isManual;

            this.eqResetAndByPassPanel.isPass = this.outputBase.eqGroup.isBypass;
            this.workModePanel.UpdateUIFromData();
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            this.cmbHpfType.SelectedIndexChanged += cmbFilterMode_SelectedIndexChanged;
            this.cmbLpfType.SelectedIndexChanged += cmbFilterMode_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbHpfType.SelectedIndexChanged -= cmbFilterMode_SelectedIndexChanged;
            this.cmbLpfType.SelectedIndexChanged -= cmbFilterMode_SelectedIndexChanged;
        }

        private void cmbFilterMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if (sender.Equals(this.cmbHpfType))
            {
                this.outputBase.hpf.typeIndexParam = (short)cmb.SelectedIndex;
                this.outputBase.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Type);
                this.sliderHpfQ.Enabled = this.outputBase.hpf.qCanBeChanged;
                //this.sliderHpfQ.Invalidate();
            }
            else if(sender.Equals(this.cmbLpfType))
            {
                this.outputBase.lpf.typeIndexParam = (short)cmb.SelectedIndex;
                this.outputBase.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Type);
                this.sliderLpfQ.Enabled = this.outputBase.lpf.qCanBeChanged;
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
            this.mixerPanel.Enabled = outputBase.music.workMode == WorkMode.Manual;
        }

        protected override void WrokSelectChanged()
        {
            this.mixerPanel.UpdateUIFromData();
        }

        protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {
            if(filterType == XPFilterType.HighPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.RemoveCmbSelectedIndexChangedHandler();
                    this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
                    this.RegisterCmbSelectedIndexChangedHandler();
                    this.sliderHpfQ.Enabled = this.outputBase.hpf.qCanBeChanged;
                }
                else if (filterElement == FilterElement.Q)
                {
                    this.sliderHpfQ.UpdateUIFromData();
                }
            }
            else if(filterType == XPFilterType.LowPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.RemoveCmbSelectedIndexChangedHandler();
                    this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;
                    this.RegisterCmbSelectedIndexChangedHandler();
                    this.sliderLpfQ.Enabled = this.outputBase.lpf.qCanBeChanged;
                }
                else if (filterElement == FilterElement.Q)
                {
                    this.sliderLpfQ.UpdateUIFromData();
                }
            }
        }

        //protected override void LPFChanged(FilterElement filterElement)
        //{
            
        //}

        public override void UpdateDisplayLanguage()
        {
            this.labelHpfFreq.Text = localization[Localization.panelOutput_HpfFreq];
            this.labelHpfQ.Text = localization[Localization.panelOutput_HpfQ];
            this.labelHpfMode.Text = localization[Localization.panelOutput_HpfMode];
            this.labelLpfFreq.Text = localization[Localization.panelOutput_LpfFreq];
            this.labelLpfQ.Text = localization[Localization.panelOutput_LpfQ];
            this.labelLpfMode.Text = localization[Localization.panelOutput_LpfMode];
            
            this.mixerPanel.UpdateDisplayLanguage();
            this.compressorPanel.UpdateDisplayLanguage();
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();
            this.workModePanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        //public override void UpdateEffectorModel()
        //{
        //    this.compressorPanel.UpdateThresholdUI();
        //}

        //protected override void HpfFreqChangedCallback()
        //{
        //    base._eventBus.NotifyObeserver(new OutputLimiterParamChangedArgs(this.outputBase.channel, OutputParamChangedSource.HpfFreqChanged, LimiterParam.Attack));
        //}
    }
}
