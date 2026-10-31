using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Core.DSP;
using AEC.Misc;

namespace AEC.Common
{
    public abstract class ModuleBase : MAudioSystem
    {
        public readonly byte moduleIndex;

        public ActiveItem activeItem = ActiveItem.EQ;

        public readonly EventBus eventBus = new EventBus();
        protected readonly EventBus effectorEvent;

        private EQGraphic _eqGraphic = null;
        protected Color _themeColor = Color.White;

        public bool _dataIsPrivate = false;

        protected static Localization localization = Config.GetInstance().localization;

        public ModuleBase(byte index, EventBus effectorEvent_)
        {
            this.moduleIndex = index;
            this.effectorEvent = effectorEvent_;

            this.isShowExciter = true;
        }

        public void InitEQGraphic()
        {
            if (this._eqGraphic == null)
            {
                this._eqGraphic = new EQGraphic(this);
            }
            this._eqGraphic.InitNormal();
            this._eqGraphic.InitThumbnail();
        }

        public virtual void InitEQNormalGraphic()
        {
            if (this._eqGraphic == null)
            {
                this._eqGraphic = new EQGraphic(this);
            }

            this._eqGraphic.InitNormal();
        }

        public virtual void InitSecondaryNormalGraphic()
        {
            if (this._eqGraphic == null)
            {
                this._eqGraphic = new EQGraphic(this);
            }

            this._eqGraphic.InitSecondaryNormal();
        }

        public void InitEQThumbnailGraphic()
        {
            if (this._eqGraphic == null)
            {
                this._eqGraphic = new EQGraphic(this);
            }

            this._eqGraphic.InitThumbnail();
        }

        public void UpdateEQNormalFreqRespGraphic()
        {
            if (this._eqGraphic == null)
            {
                return;
            }

            this._eqGraphic.UpdateNormalRespFreqCurve();
        }

        public bool dataIsPrivate
        {
            get
            {
                return this._dataIsPrivate;
            }
        }

        public short dataIsPrivateParam
        {
            get
            {
                //if(this._dataIsPrivate)
                //{
                //    Random random = new Random();
                //    short rand = (short)random.Next(1, 0x7fff);
                //    return rand > 0 ? rand : (short)0x7fff;
                //}
                //else
                //{
                //    return 0;
                //}

                return 0;
            }
            set
            {
                //this._dataIsPrivate = value != 0;
            }
        }

        public ModuleBase secondaryModule
        {
            protected set;
            get;
        }

        public bool isSecondaryModuleActivated
        {
            get;
            set;
        }

        public bool isShowExciter
        {
            get;
            set;
        }

        public virtual BassExciter getBassExciter
        {
            get
            {
                return null;
            }
        }

        public virtual Exciter getExciter1
        {
            get
            {
                return null;
            }
        }

        public virtual Exciter getExciter2
        {
            get
            {
                return null;
            }
        }

        public virtual EQGroup eqGroup
        {
            get
            {
                return null;
            }
        }
        
        public EQGraphic eqGraphic
        {
            get
            {
                return this._eqGraphic;
            }
        }

        public virtual Color themeColor
        {
            get
            {
                return this._themeColor;
            }
        }

        public virtual void ResetData()
        {

        }

        public virtual bool hasFreqRespCurve
        {
            get
            {
                return true;
            }
        }

        public virtual string eqGroupLabel
        {
            get
            {
                if(this.eqGroup != null)
                {
                    return string.Format(localization[Localization.eqGroupPanel_XBandEq], this.eqGroup.eqBandCount);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public virtual HighPassFilter hpf
        {
            get
            {
                return null;
            }
        }

        public virtual LowPassFilter lpf
        {
            get
            {
                return null;
            }
        }

        public virtual bool eqCanBeChanged
        {
            get
            {
                return !this.eqGroup.isBypass;
            }
        }

        public virtual bool hpfFreqCanBeChanged
        {
            get
            {
                return this.hpf != null;
            }
        }

        public virtual bool lpfFreqCanBeChanged
        {
            get
            {
                return this.lpf != null;
            }
        }

        public virtual void InitFreqResp()
        {
            base.CheckFreqResp();
        }

        public virtual void UpdateFreqResp()
        {
            System.Diagnostics.Debug.Assert(this.freqResp != null);
        }

        public void UpdateAllEQFromControlPanel()
        {
            this.activeItem = ActiveItem.EQ;
            this.isSecondaryModuleActivated = false;
            this.InitFreqResp();
            this.UpdateAllEQParam();

            if (this.eqGraphic != null)
            {
                this.eqGraphic.InitNormal();
                this.eqGraphic.InitThumbnail();

                this.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
        }

        public void UpdateEQFromControlPanel(EQ eq)
        {
            this.activeItem = ActiveItem.EQ;
            this.isSecondaryModuleActivated = false;
            this.UpdateEQ(eq);
            this.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
        }

        public void UpdateDEQFromControlPanel(DEQ deq)
        {
            this.activeItem = ActiveItem.EQ;
            this.isSecondaryModuleActivated = false;
            this.eqGroup.UpdateEQFreqResp(deq);
            this.UpdateFreqResp();
            this.eqGraphic.UpdateEQ(deq);
            this.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
        }

        public void UpdateEQFromGraphicPanel(EQ eq)
        {
            this.UpdateEQ(eq);
            this.eventBus.NotifyObeserver(new CurrEQChangedFromGraphicArgs());
        }

        protected virtual void UpdateEQ(EQ eq)
        {
            this.eqGroup.UpdateEQFreqResp(eq);
            this.UpdateFreqResp();
            this.eqGraphic.UpdateEQ(eq);
            this.UpdateEQParam(eq);
        }

        public void UpdateEQParam(EQ eq)
        {
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(new EQChangedArgs(this.moduleIndex, eq));
            }
        }

        public void UpdateAllEQParam()
        {
            if (this.effectorEvent != null && this.eqGroup != null)
            {
                for (int i = this.eqGroup.eqBandCount - 1; i >= 0; --i)
                {
                    this.effectorEvent.NotifyObeserver(new EQChangedArgs(this.moduleIndex, this.eqGroup.eqList[i]));
                }
            }
        }

        public void UpdateDEQParam(DEQ deq, DeqParamType type)
        {
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(new DEQParamChangedArgs(this.moduleIndex, deq, type));
            }
        }

        public void ExciterChanged(ActiveItem item)
        {
            this.activeItem = item;
            this.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
        }


        public virtual void UpdateXPFromControlPanel(XPFilterType xpFilterType, FilterElement filterElement)
        {
            this.UpdateXPFilter(xpFilterType, filterElement);
            this.activeItem = xpFilterType == XPFilterType.HighPass ? ActiveItem.HPF : ActiveItem.LPF;
            this.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
        }

        public void UpdateXPFFromGraphicPanel(XPFilterType xpFilterType, FilterElement filterElement)
        {
            this.UpdateXPFilter(xpFilterType, filterElement);
            this.eventBus.NotifyObeserver(new XPFChangedFromGraphicArgs(xpFilterType, filterElement));
        }

        public virtual void UpdateXPFilter(XPFilterType xpFilterType, FilterElement filterElement)
        {
            if(xpFilterType == XPFilterType.HighPass)
            {
                System.Diagnostics.Debug.Assert(hpf != null);
                hpf.UpdateFreqResp();
            }
            else
            {
                System.Diagnostics.Debug.Assert(lpf != null);
                lpf.UpdateFreqResp();
            }

            UpdateFreqResp();
            eqGraphic.UpdateXPF(xpFilterType);
        }

        public virtual void ResetEQGroup()
        {
            this.isSecondaryModuleActivated = false;
            this.eqGroup.Reset();
            this.UpdateFreqResp();
            this.eqGraphic.UpdateEQGroup();
            this.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            if (this.effectorEvent != null)
            {
                for(int i = this.eqGroup.eqBandCount - 1; i >= 0; --i)
                {
                    this.effectorEvent.NotifyObeserver(new EQChangedArgs(this.moduleIndex, this.eqGroup.eqList[i]));
                }
            }
        }

        public void UpdateEQGroup()
        {
            if (this.eqGroup != null && effectorEvent != null)
            {
                for (int i = this.eqGroup.eqBandCount - 1; i >= 0; --i)
                {
                    this.effectorEvent.NotifyObeserver(new EQChangedArgs(this.moduleIndex, this.eqGroup.eqList[i]));
                }

                this.eqGroup.currEQIndex = 0;
            }
        }

        public void UpdateFreqRespAndInitGraphic()
        {
            this.UpdateFreqResp();
            if (this.eqGraphic != null)
            {
                this.eqGraphic.InitNormal();
                this.eqGraphic.InitThumbnail();                
                this.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }
        }

        public void UpdateEQByPass(bool isPass_)
        {
            this.isSecondaryModuleActivated = false;
            this.eqGroup.isBypassParam = isPass_;
            this.UpdateFreqResp();
            this.eqGraphic.UpdateFreqRespCurve();
            this.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
        }

        protected virtual void OnParamChanged(object sender, DataParamChangedArgs e)
        {
            if (this.effectorEvent != null)
            {
                this.effectorEvent.NotifyObeserver(e);
            }
        }

        //protected virtual void OnEQChanged(object sender, EQChangedArgs e)
        //{
        //    if (commEvent != null)
        //    {
        //        this.commEvent.NotifyObeserver(e);
        //    }
        //}

        //protected virtual void OnEQByPassChanged(object sender, EventArgs e)
        //{
        //    if (commEvent != null)
        //    {

        //    }
        //}

        protected static void EQGroupFreqResp(FreqResp result, EQGroup eqGroup)
        {
            if (eqGroup.isBypass)
            {
                result.Reset();
            }
            else
            {
                result.CopyFrom(eqGroup.freqResp);
            }
        }

        protected static void EQGroupAddFilterFreqResp(FreqResp result, EQGroup eqGroup, XPFilterBase filter)
        {
            if (filter.freqRespIsZero)
            {
                if (eqGroup.isBypass)
                {
                    result.Reset();
                }
                else
                {
                    result.CopyFrom(eqGroup.freqResp);
                }
            }
            else
            {
                if(eqGroup.isBypass)
                {
                    result.CopyFrom(filter.freqResp);
                }
                else
                {
                    result.Sum(eqGroup.freqResp, filter.freqResp);
                }
            }
        }

        protected static void EQGroupAddHLPFilterFreqResp(FreqResp result, EQGroup eqGroup, XPFilterBase lowPass, XPFilterBase highPass)
        {
            if (lowPass.freqRespIsZero)
            {
                if (highPass.freqRespIsZero)
                {
                    if (eqGroup.isBypass)
                    {
                        result.Reset();
                    }
                    else
                    {
                        result.CopyFrom(eqGroup.freqResp);
                    }
                }
                else
                {
                    if(eqGroup.isBypass)
                    {
                        result.CopyFrom(highPass.freqResp);
                    }
                    else
                    {
                        result.Sum(eqGroup.freqResp, highPass.freqResp);
                    }
                }
            }
            else
            {
                if (highPass.freqRespIsZero)
                {
                    if (eqGroup.isBypass)
                    {
                        result.CopyFrom(lowPass.freqResp);
                    }
                    else
                    {
                        result.Sum(eqGroup.freqResp, lowPass.freqResp);
                    }
                }
                else
                {
                    if (eqGroup.isBypass)
                    {
                        result.Sum(highPass.freqResp, lowPass.freqResp);
                    }
                    else
                    {
                        result.Sum(eqGroup.freqResp, highPass.freqResp, lowPass.freqResp);
                    }
                }
            }
        }

        protected static void HLPFilterFreqResp(FreqResp result, XPFilterBase lowPass, XPFilterBase highPass)
        {
            if (lowPass.freqRespIsZero)
            {
                if (highPass.freqRespIsZero)
                {
                    result.Reset();
                }
                else
                {
                    result.CopyFrom(highPass.freqResp);
                }
            }
            else
            {
                if (highPass.freqRespIsZero)
                {
                    result.CopyFrom(lowPass.freqResp);
                }
                else
                {
                    result.Sum(highPass.freqResp, lowPass.freqResp);
                }
            }
        }

        protected static void ToneControlAddXPFilterFreqResp(FreqResp result, ToneControlFilter toneControlFilter, XPFilterBase lowPass, XPFilterBase highPass)
        {
            if (lowPass.freqRespIsZero)
            {
                if (highPass.freqRespIsZero)
                {
                    if (toneControlFilter.freqRespIsZero)
                    {
                        result.Reset();
                    }
                    else
                    {
                        result.CopyFrom(toneControlFilter.freqResp);
                    }
                }
                else
                {
                    if (toneControlFilter.freqRespIsZero)
                    {
                        result.CopyFrom(highPass.freqResp);
                    }
                    else
                    {
                        result.Sum(toneControlFilter.freqResp, highPass.freqResp);
                    }
                }
            }
            else
            {
                if (highPass.freqRespIsZero)
                {
                    if (toneControlFilter.freqRespIsZero)
                    {
                        result.CopyFrom(lowPass.freqResp);
                    }
                    else
                    {
                        result.Sum(toneControlFilter.freqResp, lowPass.freqResp);
                    }
                }
                else
                {
                    if (toneControlFilter.freqRespIsZero)
                    {
                        result.Sum(highPass.freqResp, lowPass.freqResp);
                    }
                    else
                    {
                        result.Sum(toneControlFilter.freqResp, highPass.freqResp, lowPass.freqResp);
                    }
                }
            }
        }

#if COMM_TEST
        public readonly static Random random = new Random();
        public virtual void RandomTest()
        {
           
        }
#endif
    }
}
