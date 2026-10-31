using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Common;
using AEC.Misc;

namespace AEC.UI
{
    public partial class PanelBase : UserControl
    {
        public const string FreqUnit = "Hz";
        public const string Kilo = "k";
        public const string Off = "Off";

        protected MController mController;
        private ModuleBase baseModule;

        protected EQGroupPanel eqPanel;
        protected HSliderParam hpfFreq;
        protected HSliderParam lpfFreq;
        protected EventBus _eventBus;

        public PanelBase()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.UpdateStyles();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Visible = false;
        }

        private const int WM_SETFOCUS = 0x0007;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SETFOCUS)
            {
                return;
            }
            //AEC.Log.OutputLine("MicPanel WndProc Msg:{0:X} [{1}]", m.Msg, ++count);
            base.WndProc(ref m);
        }

        protected override void OnLoad(EventArgs e)
        {
 	        base.OnLoad(e);
            this.InitTheme();
        }

        protected virtual void InitTheme()
        {

        }

        protected void InitControl(EQGroupPanel uiEQGroupPanel_, HSliderParam uiHpfFreq_, HSliderParam uiLpfFreq_)
        {
            //Debug.Assert(uiEQGroupPanel_ != null);
            this.eqPanel = uiEQGroupPanel_;
            this.hpfFreq = uiHpfFreq_;
            this.lpfFreq = uiLpfFreq_;

            if (this.hpfFreq != null)
            {
                this.hpfFreq.Actived += Filter_Actived;
                this.hpfFreq.ValueChanged += FilterFreq_ValueChanged;
            }

            if (this.lpfFreq != null)
            {
                this.lpfFreq.Actived += Filter_Actived;
                this.lpfFreq.ValueChanged += FilterFreq_ValueChanged;
            }
        }

        protected void Filter_Actived(object sender, EventArgs e)
        {
            if (baseModule == null)
            {
                return;
            }

            if (sender.Equals(this.hpfFreq))
            {
                if (this.baseModule.activeItem != ActiveItem.HPF || this.baseModule.isSecondaryModuleActivated)
                {
                    this.baseModule.activeItem = ActiveItem.HPF;
                    this.baseModule.isSecondaryModuleActivated = false;
                    this.baseModule.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if (sender.Equals(this.lpfFreq))
            {
                if (this.baseModule.activeItem != ActiveItem.LPF || this.baseModule.isSecondaryModuleActivated)
                {
                    this.baseModule.activeItem = ActiveItem.LPF;
                    this.baseModule.isSecondaryModuleActivated = false;
                    this.baseModule.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }

            this.MainModuleActivatedHandler();
        }

        private void FilterFreq_ValueChanged(object sender, EventArgs e)
        {
            if (baseModule == null)
            {
                return;
            }

            HSliderParam hSliderParam = sender as HSliderParam;
            if (sender.Equals(this.hpfFreq))
            {
                this.baseModule.hpf.UpdateFreqIndexFormParam();
                this.baseModule.isSecondaryModuleActivated = false;
                baseModule.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Freq);
                this.HpfFreqChangedCallback();
            }
            else if (sender.Equals(this.lpfFreq))
            {
                this.baseModule.lpf.UpdateFreqIndexFormParam();
                this.baseModule.isSecondaryModuleActivated = false;
                baseModule.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Freq);
            }

            this.MainModuleActivatedHandler();
        }

        public virtual void BindingController(MController mController_, EventBus eventBus_)
        {
            Debug.Assert(mController_ != null && eventBus_ != null);

            this.mController = mController_;
            this._eventBus = eventBus_;
        }

        public virtual void UpdateUIFromData()
        {
            if(this.eqPanel != null)
            {
                this.eqPanel.InitEqAdjusterList();
            }
            if(this.hpfFreq != null)
            {
                this.hpfFreq.UpdateUIFromData();
            }
            if(this.lpfFreq != null)
            {
                this.lpfFreq.UpdateUIFromData();
            }
        }

        protected void BindingModule(ModuleBase baseModule_, MEffectorFamily effectorFamily)
        {
            Debug.Assert(baseModule_ != null);

            this.baseModule = baseModule_;

            if (this.baseModule.eqGroup != null && this.eqPanel != null)
            {
                this.eqPanel.BindingModule(this.baseModule, effectorFamily);
            }

            this.baseModule.eventBus.RegisterHandler(XPFChangedFromGraphicHandler);
            
            if(this.baseModule.hpf != null)
            {
                if(this.hpfFreq != null && this.baseModule.hpf.freqParam != null)
                {
                    this.hpfFreq.Binding(this.baseModule.hpf.freqParam, GetHpfFreqLabel, CalcHPFFreqIndexFromText);
                }
            }
            if(this.baseModule.lpf != null)
            {
                if (this.lpfFreq != null && this.baseModule.lpf.freqParam != null)
                {
                    this.lpfFreq.Binding(this.baseModule.lpf.freqParam, GetLpfFreqLabel, CalcLPFFreqIndexFromText);
                }
            }            
        }

        protected string GetHpfFreqLabel(short index)
        {
            return DataUtils.FilterIndex2String(index + this.baseModule.hpf.freqLowerLimitIndex);
        }

        protected string GetLpfFreqLabel(short index)
        {
            return DataUtils.FilterIndex2String(index + this.baseModule.lpf.freqLowerLimitIndex);
        }

        protected short CalcHPFFreqIndexFromText(string text)
        {
            if (text.ToLower().Replace(" ", "") == Off.ToLower())
            {
                return 0;
            }

            string str = Utils.RemoveStringSuffix(text, FreqUnit);
            double value;
            double multiple = 1.0;
            if (str.EndsWith(Kilo))
            {
                int index = text.IndexOf(Kilo);
                if (index > 0)
                {
                    str = text.Remove(index);
                    multiple = 1000.0;
                }
            }
            if (double.TryParse(str, out value))
            {
                return DataUtils.Freq2FilterIndex(value * multiple);
            }

            return -1;
        }

        protected short CalcLPFFreqIndexFromText(string text)
        {
            if (text.ToLower().Replace(" ", "") == Off.ToLower())
            {
                return this.baseModule.hpf.freqMaxIndex;
            }
            string str = Utils.RemoveStringSuffix(text, FreqUnit);
            double value;
            double multiple = 1.0;
            if (str.EndsWith(Kilo))
            {
                int index = text.IndexOf(Kilo);
                if (index > 0)
                {
                    str = text.Remove(index);
                    multiple = 1000.0;
                }
            }
            if (double.TryParse(str, out value))
            {
                short index = DataUtils.Freq2FilterIndex(value * multiple);
                return Utils.ConstrainMax((short)(index - this.baseModule.lpf.freqLowerLimitIndex), this.baseModule.lpf.freqMaxIndex);
            }

            return -1;
        }

        //XPF已经通过EQ Graphic 面板修改
        protected void XPFChangedFromGraphicHandler(object sender, EventBusArgs args)
        {
            if (this.baseModule != null && args.type == EventType.XPFChangedFromGraphic)
            {
                XPFChangedFromGraphicArgs filterChangedFromGraphicArgs = args as XPFChangedFromGraphicArgs;
                XPFChangedHandler(filterChangedFromGraphicArgs.filterType, filterChangedFromGraphicArgs.filterElement);
            }
        }

        private void XPFChangedHandler(XPFilterType filterType, FilterElement filterElement)
        {
            if (filterElement == FilterElement.Freq)
            {
                if(filterType == XPFilterType.HighPass)
                {
                    if(this.hpfFreq != null)
                    {
                        this.hpfFreq.value = this.baseModule.hpf.freqIndex;
                        this.HpfFreqChangedCallback();
                    }
                }
                else if(filterType == XPFilterType.LowPass)
                {
                    if (this.lpfFreq != null)
                    {
                        this.lpfFreq.value = this.baseModule.lpf.freqIndex;
                        this.LpfFreqChangedCallback();
                    }
                }
            }
            else
            {
                XPFChanged(filterType, filterElement);
            }
        }

        protected virtual void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {

        }

        protected void ResetEQGroup(object sender, EventArgs args)
        {
            this.baseModule.ResetEQGroup();
            this.eqPanel.UpdateUIFromData();
        }

        protected void SwitchEQByPass(object sender, EventArgs args)
        {
            EQResetAndByPassPanel byPassPanel = sender as EQResetAndByPassPanel;
            this.baseModule.UpdateEQByPass(byPassPanel.isPass);
            if(this.eqPanel != null)
            {
                this.eqPanel.UpdateEqCanbeChanged();
            }
        }

        protected void WorkModeOrWorkSelectChanged(object sender, EventBusArgs e)
        {
            if (e.type == EventType.WorkModeChanged)
            {
                WrokModeChanged();
            }
            else if (e.type == EventType.WorkSelectChanged)
            {
                WrokSelectChanged();
            }
        }

        protected virtual void WrokModeChanged()
        {

        }

        protected virtual void WrokSelectChanged()
        {

        }

        public virtual void Close()
        {

        }

        public void ActiveEQ(List<short> indexList)
        {
            //AEC.Log.OutputLine("ActiveEQ");

            if (baseModule == null || eqPanel ==null)
            {
                return;
            }

            foreach (short index in indexList)
            {
                if (index > 0 && index <= this.baseModule.eqGroup.eqBandCount)
                {
                    this.baseModule.eqGroup.currEQIndex = (short)(index - 1);
                    //AEC.Log.OutputLine("curr eq index = {0}", this.baseModule.currEQIndex);
                    eqPanel.ActiveEQIndexUpdated();
                    break;
                }
            }
        }

        public void ActiveNextEQ()
        {
            if (baseModule == null || eqPanel == null)
            {
                return;
            }

            if (this.baseModule.eqGroup.currEQIndex < this.baseModule.eqGroup.eqBandCount - 1)
            {
                this.baseModule.eqGroup.currEQIndex += 1;
            }
            else
            {
                this.baseModule.eqGroup.currEQIndex = 0;
            }
            eqPanel.ActiveEQIndexUpdated();
        }

        public void ActivePrevEQ()
        {
            if (baseModule == null || eqPanel == null)
            {
                return;
            }

            if (this.baseModule.eqGroup.currEQIndex > 0)
            {
                this.baseModule.eqGroup.currEQIndex -= 1;
            }
            else
            {
                this.baseModule.eqGroup.currEQIndex = (short)(this.baseModule.eqGroup.eqBandCount - 1);
            }
            eqPanel.ActiveEQIndexUpdated();
        }

        public virtual void ShowPanel()
        {
            this.Visible = true;
            if(this.baseModule != null && this.eqPanel != null)
            {
                if(this.baseModule.activeItem == ActiveItem.EQ)
                {
                    this.eqPanel.FocusCurrEQ();
                }
            }
        }

        public void HidePanel()
        {
            this.Visible = false;
        }

        protected Localization localization = Config.GetInstance().localization;
        public virtual void UpdateDisplayLanguage()
        {
            
        }

        public virtual void UpdateEqGroupPanelLanguage()
        {
            if (baseModule != null || baseModule.eqGroup.eqBandCount == 0 || eqPanel == null)
            {
                return;
            }
        }

        protected virtual void HpfFreqChangedCallback()
        {
        }

        protected virtual void LpfFreqChangedCallback()
        {
        }

        public virtual void UpdateEffectorModel()
        {

        }

        protected virtual void MainModuleActivatedHandler()
        {

        }
    }
}
