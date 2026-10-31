using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Common;
using AEC.Misc;
using AEC.UI.Skin;

namespace AEC.UI
{
    public partial class EQGroupPanel : UserControl
    {
        private ModuleBase module;
        private MEffectorFamily effectorFamily;
        private EQAdjuster[] eqAdjusterList;
        private int lastActiveIndex = 0;
        private bool _eqCanbeChanged = true;

        private SynchronizationContext syncContext;

        public EQGroupPanel()
        {
            InitializeComponent();
            InitFont();
            this.syncContext = WindowsFormsSynchronizationContext.Current;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.UpdateStyles();
        }

        private void InitFont()
        {
            this.labelTitle.Font = FontHelper.CreateFont10();
            this.labelFilterType.Font = FontHelper.CreateFont10();
            this.labelGain.Font = FontHelper.CreateFont10();
            this.labelFreq.Font = FontHelper.CreateFont10();
            this.labelQ.Font = FontHelper.CreateFont10();
        }

        private const int WM_SETFOCUS = 0x0007;
        private const int WM_MOUSEACTIVATE = 0x21;
        private const int MA_NOACTIVATE = 3;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SETFOCUS)
            {
                return;
            }
            //AEC.Log.OutputLine("MicPanel WndProc Msg:{0:X} [{1}]", m.Msg, ++count);
            //if (m.Msg == WM_MOUSEACTIVATE)
            //{
            //    m.Result = new IntPtr(MA_NOACTIVATE);
            //    return;
            //}
            base.WndProc(ref m);
        }
        

        public void UpdateEqCanbeChanged()
        {
            if (this._eqCanbeChanged != this.module.eqCanBeChanged)
            {
                this._eqCanbeChanged = this.module.eqCanBeChanged;
                for (int i = 0; i < this.eqAdjusterList.Length; ++i)
                {
                    this.eqAdjusterList[i].canBeChanged = this._eqCanbeChanged;
                }
            }
        }

        public void InitTheme()
        {
            this.panelEQ.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelEQ.FillColor = SkinManager.CurrentSkin.PanelBackColor;

            this.labelTitle.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.hScrollbar.BackColor = SkinManager.CurrentSkin.ScrollbarBackColor;
            this.hScrollbar.FillColor = SkinManager.CurrentSkin.ScrollbarBackColor;
            this.hScrollbar.ThumbColor = SkinManager.CurrentSkin.ScrollbarForeColor;
        }

        //private int count = 0;
        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    AsyncInitHScrollbar();
        //    //Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
        //    //si.cbSize = (uint)Marshal.SizeOf(si);
        //    //si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
        //    //Win32API.GetScrollInfo(this.panelEQGroup.Handle, (int)Win32API.ScrollBarDirection.SB_HORZ, ref si);
        //    ////return si;
        //    //AEC.Log.OutputLine("SCROLLINFO: min = {0}, max = {1}, page = {2}, pos = {3}, trackPos = {4}, [{5}]", si.nMin, si.nMax, si.nPage, si.nPos, si.nTrackPos, ++count);
        //    //AEC.Log.OutputLine("{0}", e.Clicks);
        //}

        //void panelEQGroup_Scroll(object sender, ScrollEventArgs e)
        //{
        //    panelEQGroup.Invalidate();
        //    panelEQGroup.Update();
        //}

        //private void Init()
        //{
        //    this.DoubleBuffered = true;
        //}
        //protected override void OnMouseDown(MouseEventArgs e)
        //{
            
        //    AEC.Log.OutputLine("OnMouseDown");
        //    base.OnMouseDown(e);
        //}

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //}
        public void BindingModule(ModuleBase module_, MEffectorFamily effectorFamily_)
        {
            Debug.Assert(module_ != null && module_.eqGroup.eqBandCount > 0);
            this.module = module_;
            this.effectorFamily = effectorFamily_;
            this.labelTitle.Text = this.module.eqGroupLabel;
            //this.labelTitle.Text = "15段麦克风EQ";
            this.InitEqAdjusterList();
            this.module.eventBus.RegisterHandler(EQChangedHandler);
            //this.Enabled = this.module.eqCanBeChanged;
            //this.Enabled = false;
        }

        public void UpdateUIFromData()
        {
            for (int i = 0; i < this.eqAdjusterList.Length; ++i)
            {
                this.eqAdjusterList[i].BindingEQ(this.module.eqGroup.eqList[i]);
                //this.eqAdjusterList[i].canBeChanged = this._eqCanbeChanged;
            }

            //this._eqCanbeChanged = this.module.eqCanBeChanged;
            //this.eqAdjusterList[this.module.currEQIndex].isActiveInit = true;

            //if (this.lastActiveIndex != this.module.currEQIndex)
            //{
            //    this.eqAdjusterList[this.lastActiveIndex].isActiveInit = false;
            //    this.lastActiveIndex = this.module.currEQIndex;
            //}
        }

        public void InitEqAdjusterList()
        {
            if (this.eqAdjusterList != null && this.eqAdjusterList.Length != this.module.eqGroup.eqBandCount)
            {
                this.panelEQGroup.Controls.Clear();
                foreach (EQAdjuster eqAdjuster in this.eqAdjusterList)
                {
                    if (eqAdjuster != null)
                    {
                        eqAdjuster.EQChanged -= EQGroupPanel_EQChanged;
                        eqAdjuster.Activated -= EQGroupPanel_Activated;
                        eqAdjuster.Dispose();
                    }
                }
            }

            if (this.eqAdjusterList == null)
            {
                this.eqAdjusterList = new EQAdjuster[this.module.eqGroup.eqBandCount];
            }

            this._eqCanbeChanged = this.module.eqCanBeChanged;

            for (int i = 0; i < this.eqAdjusterList.Length; ++i)
            //    for (int i = 0; i < 1; ++i)
            {
                if (this.eqAdjusterList[i] == null)
                {
                    this.eqAdjusterList[i] = new EQAdjuster();
                    this.eqAdjusterList[i].EQChanged += EQGroupPanel_EQChanged;
                    this.eqAdjusterList[i].Activated += EQGroupPanel_Activated;                    
                }

                this.eqAdjusterList[i].eqIndexBackgroundActive = GetActiveIndexLabelBackground(this.module, this.effectorFamily);
                this.eqAdjusterList[i].eqTypeImage = GetEQTypeImage(this.module, this.effectorFamily);
                this.eqAdjusterList[i].activeColor = this.module.themeColor;
                this.eqAdjusterList[i].BindingEQ(this.module.eqGroup.eqList[i]);

                this.eqAdjusterList[i].isActiveInit = !this.module.isSecondaryModuleActivated && i == this.module.eqGroup.currEQIndex;
                this.eqAdjusterList[i].canBeChanged = this._eqCanbeChanged;
                
                if(!this.panelEQGroup.Controls.Contains(this.eqAdjusterList[i]))
                {
                    this.panelEQGroup.Controls.Add(this.eqAdjusterList[i]);
                }
            }

            if (this.lastActiveIndex != this.module.eqGroup.currEQIndex)
            {
                this.eqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                this.lastActiveIndex = this.module.eqGroup.currEQIndex;
                this.eqAdjusterList[this.module.eqGroup.currEQIndex].Focus();
                this.AsyncUpdateHScrollbar();
            }
        }

        public void FocusCurrEQ()
        {
            this.eqAdjusterList[this.module.eqGroup.currEQIndex].Focus();
        }

        public void ActiveEQIndexUpdated()
        {
            if (this.module == null)
            {
                return;
            }

            //AEC.Log.OutputLine("ActiveEQIndexUpdated() lastActiveIndex {0}, currEQIndex{1}", this.lastActiveIndex, this.module.currEQIndex);

            if (this.lastActiveIndex != this.module.eqGroup.currEQIndex)
            {
                this.eqAdjusterList[this.module.eqGroup.currEQIndex].Focus();
                this.eqAdjusterList[this.module.eqGroup.currEQIndex].isActiveInit = true;

                this.eqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                this.lastActiveIndex = this.module.eqGroup.currEQIndex;
            }

            this.module.activeItem = ActiveItem.EQ;
            //this.module.isSecondaryModuleFreqRespCurveActivated = false;
            this.module.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());

            this.AsyncUpdateHScrollbar();
        }

        private void EQGroupPanel_EQChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            EQ eq = sender as EQ;
            //AEC.Log.OutputLine("EQGroupPanel_EQChanged : group:{0}, index = {1}", eq.groupTag, eq.index);
            this.module.eqGroup.currEQIndex = eq.index;
            this.eqAdjusterList[this.module.eqGroup.currEQIndex].isActiveInit = true;
            if (this.lastActiveIndex != eq.index)
            {
                this.eqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                this.lastActiveIndex = this.module.eqGroup.currEQIndex;
                //AEC.Log.OutputLine("last index:{0}取消激活", this.module.lastEQIndex);
            }
            this.module.UpdateEQFromControlPanel(eq);
            
            //AEC.Log.OutputLine("eq:{0}", eq.label);
        }

        private void EQGroupPanel_Activated(object sender, EventArgs e)
        {
            EQ eq = sender as EQ;

            this.module.eqGroup.currEQIndex = eq.index;
            //AEC.Log.OutputLine("last index:{0}, curr Index = {1}", this.lastActiveIndex, this.module.currEQIndex);
            //this.eqAdjusterList[this.module.currEQIndex].isActiveInit = true;
            if (this.lastActiveIndex != eq.index)
            {
                this.eqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                this.lastActiveIndex = this.module.eqGroup.currEQIndex;
                //AEC.Log.OutputLine("last index:{0}取消激活", this.module.lastEQIndex);
            }
            //AEC.Log.OutputLine("curr index:{0}, last index:{1}", this.module.currEQIndex, this.module.lastEQIndex);
            
            //this.module.UpdateEQ(eq);
            this.module.activeItem = ActiveItem.EQ;
            this.module.isSecondaryModuleActivated = false;
            this.module.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            //AEC.Log.OutputLine("eq:{0}", eq.label);
            this.AsyncUpdateHScrollbar();
        }

        private void EQChangedHandler(object sender, EventBusArgs args)
        {
            if(args.type == EventType.CurrEQChangedFromGraphic)
            {
                this.eqAdjusterList[this.module.eqGroup.currEQIndex].UpdateUIFromData();
            }
            else if(args.type == EventType.CurrEQIndexChangedFromGraphic)
            {
                //Log.OutputLine("EQAdjuster Focus()");
                this.eqAdjusterList[this.module.eqGroup.currEQIndex].Focus();
                this.eqAdjusterList[this.module.eqGroup.currEQIndex].isActiveInit = true;
                if (this.lastActiveIndex != this.module.eqGroup.currEQIndex)
                {
                    this.eqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                    this.lastActiveIndex = this.module.eqGroup.currEQIndex;
                }

                //AEC.Log.OutputLine("EQChangedHandler() lastActiveIndex {0}, currEQIndex{1}", this.lastActiveIndex, this.module.currEQIndex);

                this.AsyncUpdateHScrollbar();
            }
        }

        public void ActivateCurrEQ(bool isActivate)
        {
            this.eqAdjusterList[this.module.eqGroup.currEQIndex].isActiveInit = isActivate;
        }

        private static Image GetActiveIndexLabelBackground(ModuleBase module, MEffectorFamily effectorFamily)
        {
            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //if (effectorFamily == MEffectorFamily.VKxS || effectorFamily == MEffectorFamily.X5)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.micModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.KX)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.KXComm.KXCommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.KXComm.KXCommProtocol.micModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.KXComm.KXCommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.KXComm.KXCommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.KXComm.KXCommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.KXComm.KXCommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.KXComm.KXCommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxSCM)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.micModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.reverbModuleIndex:
            //            return AEC.Properties.Resources.reverb_EQ_index_back_actived;

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxDCM)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.micAModuleIndex:
            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.micBModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.reverbModuleIndex:
            //            return AEC.Properties.Resources.reverb_EQ_index_back_actived;

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxPII)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.micModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.reverbModuleIndex:
            //            return AEC.Properties.Resources.reverb_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxPIIDCM)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.micAModuleIndex:
            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.micBModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.reverbModuleIndex:
            //            return AEC.Properties.Resources.reverb_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxDM)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.micAModuleIndex:
            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.micBModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if(effectorFamily == MEffectorFamily.VX)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VXComm.VXCommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.VXComm.VXCommProtocol.micModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.VXComm.VXCommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.VXComm.VXCommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.VXComm.VXCommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.VXComm.VXCommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.VXComm.VXCommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if(effectorFamily == MEffectorFamily.MX)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.MXComm.MXCommProtocol.musicModuleIndex:
            //            return AEC.Properties.Resources.music_EQ_index_back_actived;

            //        case AEC.Comm.MXComm.MXCommProtocol.micModuleIndex:
            //            return AEC.Properties.Resources.mic_EQ_index_back_actived;

            //        case AEC.Comm.MXComm.MXCommProtocol.echoModuleIndex:
            //            return AEC.Properties.Resources.effect_EQ_index_back_actived;

            //        case AEC.Comm.MXComm.MXCommProtocol.mainOutputModuleIndex:
            //            return AEC.Properties.Resources.main_output_EQ_index_back_actived;

            //        case AEC.Comm.MXComm.MXCommProtocol.aux1ModuleIndex:
            //            return AEC.Properties.Resources.aux1_output_EQ_index_back_actived;

            //        case AEC.Comm.MXComm.MXCommProtocol.aux2ModuleIndex:
            //            return AEC.Properties.Resources.aux2_output_EQ_index_back_actived;

            //        case AEC.Comm.MXComm.MXCommProtocol.surOutputModuleIndex:
            //            return AEC.Properties.Resources.sur_output_EQ_index_back_actived;

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else
            //{
            //    throw new Exception("EQGroupPanel.GetCurrImage()...没有此家族的效果器产品!");
            //}
        }

        private static EQTypeImage GetEQTypeImage(ModuleBase module, MEffectorFamily effectorFamily)
        {
            return new EQTypeImage(
                            AEC.Properties.Resources.main_output_EQ_peak_hover,
                            AEC.Properties.Resources.main_output_EQ_peak_checked,
                            AEC.Properties.Resources.main_output_EQ_ls_hover,
                            AEC.Properties.Resources.main_output_EQ_ls_checked,
                            AEC.Properties.Resources.main_output_EQ_hs_hover,
                            AEC.Properties.Resources.main_output_EQ_hs_checked
                            );

            //if (effectorFamily == MEffectorFamily.VKxS || effectorFamily == MEffectorFamily.X5)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.micModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSComm.VKxSCommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxDM)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.micAModuleIndex:
            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.micBModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDMComm.VKxDMCommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.KX)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.KXComm.KXCommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.KXComm.KXCommProtocol.micModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.KXComm.KXCommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.KXComm.KXCommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.KXComm.KXCommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.KXComm.KXCommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.KXComm.KXCommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxSCM)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.micModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.reverbModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.reverb_EQ_peak_hover,
            //                AEC.Properties.Resources.reverb_EQ_peak_checked,
            //                AEC.Properties.Resources.reverb_EQ_ls_hover,
            //                AEC.Properties.Resources.reverb_EQ_ls_checked,
            //                AEC.Properties.Resources.reverb_EQ_hs_hover,
            //                AEC.Properties.Resources.reverb_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxSCMComm.VKxSCMCommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxDCM)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.micAModuleIndex:
            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.micBModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.reverbModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.reverb_EQ_peak_hover,
            //                AEC.Properties.Resources.reverb_EQ_peak_checked,
            //                AEC.Properties.Resources.reverb_EQ_ls_hover,
            //                AEC.Properties.Resources.reverb_EQ_ls_checked,
            //                AEC.Properties.Resources.reverb_EQ_hs_hover,
            //                AEC.Properties.Resources.reverb_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxDCMComm.VKxDCMCommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxPII)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.micModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.reverbModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.reverb_EQ_peak_hover,
            //                AEC.Properties.Resources.reverb_EQ_peak_checked,
            //                AEC.Properties.Resources.reverb_EQ_ls_hover,
            //                AEC.Properties.Resources.reverb_EQ_ls_checked,
            //                AEC.Properties.Resources.reverb_EQ_hs_hover,
            //                AEC.Properties.Resources.reverb_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIComm.VKxPIICommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VKxPIIDCM)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.micAModuleIndex:
            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.micBModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.reverbModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.reverb_EQ_peak_hover,
            //                AEC.Properties.Resources.reverb_EQ_peak_checked,
            //                AEC.Properties.Resources.reverb_EQ_ls_hover,
            //                AEC.Properties.Resources.reverb_EQ_ls_checked,
            //                AEC.Properties.Resources.reverb_EQ_hs_hover,
            //                AEC.Properties.Resources.reverb_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VKxPIIDCMComm.VKxPIIDCMCommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.VX)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.VXComm.VXCommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.VXComm.VXCommProtocol.micModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.VXComm.VXCommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.VXComm.VXCommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VXComm.VXCommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VXComm.VXCommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.VXComm.VXCommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else if (effectorFamily == MEffectorFamily.MX)
            //{
            //    switch (module.moduleIndex)
            //    {
            //        case AEC.Comm.MXComm.MXCommProtocol.musicModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.music_EQ_peak_hover,
            //                AEC.Properties.Resources.music_EQ_peak_checked,
            //                AEC.Properties.Resources.music_EQ_ls_hover,
            //                AEC.Properties.Resources.music_EQ_ls_checked,
            //                AEC.Properties.Resources.music_EQ_hs_hover,
            //                AEC.Properties.Resources.music_EQ_hs_checked
            //                );

            //        case AEC.Comm.MXComm.MXCommProtocol.micModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.mic_EQ_peak_hover,
            //                AEC.Properties.Resources.mic_EQ_peak_checked,
            //                AEC.Properties.Resources.mic_EQ_ls_hover,
            //                AEC.Properties.Resources.mic_EQ_ls_checked,
            //                AEC.Properties.Resources.mic_EQ_hs_hover,
            //                AEC.Properties.Resources.mic_EQ_hs_checked
            //                );

            //        case AEC.Comm.MXComm.MXCommProtocol.echoModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.effect_EQ_peak_hover,
            //                AEC.Properties.Resources.effect_EQ_peak_checked,
            //                AEC.Properties.Resources.effect_EQ_ls_hover,
            //                AEC.Properties.Resources.effect_EQ_ls_checked,
            //                AEC.Properties.Resources.effect_EQ_hs_hover,
            //                AEC.Properties.Resources.effect_EQ_hs_checked
            //                );

            //        case AEC.Comm.MXComm.MXCommProtocol.mainOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.main_output_EQ_peak_hover,
            //                AEC.Properties.Resources.main_output_EQ_peak_checked,
            //                AEC.Properties.Resources.main_output_EQ_ls_hover,
            //                AEC.Properties.Resources.main_output_EQ_ls_checked,
            //                AEC.Properties.Resources.main_output_EQ_hs_hover,
            //                AEC.Properties.Resources.main_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.MXComm.MXCommProtocol.aux1ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux1_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux1_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.MXComm.MXCommProtocol.aux2ModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.aux2_output_EQ_peak_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_peak_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_ls_checked,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_hover,
            //                AEC.Properties.Resources.aux2_output_EQ_hs_checked
            //                );

            //        case AEC.Comm.MXComm.MXCommProtocol.surOutputModuleIndex:
            //            return new EQTypeImage(
            //                AEC.Properties.Resources.sur_output_EQ_peak_hover,
            //                AEC.Properties.Resources.sur_output_EQ_peak_checked,
            //                AEC.Properties.Resources.sur_output_EQ_ls_hover,
            //                AEC.Properties.Resources.sur_output_EQ_ls_checked,
            //                AEC.Properties.Resources.sur_output_EQ_hs_hover,
            //                AEC.Properties.Resources.sur_output_EQ_hs_checked
            //                );

            //        default:
            //            throw new Exception("索引对应模块没有EQ!");
            //    }
            //}
            //else
            //{
            //    throw new Exception("EQGroupPanel.GetEQTypeImage()...没有此家族的效果器产品!");
            //}
        }

        private void EQGroupPanel_Load(object sender, EventArgs e)
        {
            AsyncInitHScrollbar();
        }

        public void AsyncInitHScrollbar()
        {
            syncContext.Post(InitHScrollbarHandler, null);
        }

        public void AsyncUpdateHScrollbar()
        {
            //AEC.Log.OutputLine("AsyncUpdateHScrollbar");
            syncContext.Post(UpdateNewHScrollbar, null);
        }

        private bool hScrollIsVisible
        {
            get
            {
                return this.hScrollbar.Visible;
            }
        }

        public void InitHScrollbarHandler(object state)
        {
            bool isVisible = Win32API.IsHorizontalScrollBarVisible(this.panelEQGroup);
            //AEC.Log.OutputLine("水平滚动条存在:{0}", isVisible);

            if(!isVisible)
            {
                this.hScrollbar.Visible = false;
                return;
            }

            this.hScrollbar.Visible = true;
            Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
            Win32API.GetScrollInfo(this.panelEQGroup.Handle, (int)Win32API.ScrollBarDirection.SB_HORZ, ref si);

            this.hScrollbar.Maximum = (si.nMax + 1);
            this.hScrollbar.LargeChange = (int)si.nPage;
            this.hScrollbar.Value = si.nPos;
        }

        private int GetHScrollBarPos()
        {
            Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
            Win32API.GetScrollInfo(this.panelEQGroup.Handle, (int)Win32API.ScrollBarDirection.SB_HORZ, ref si);

            return si.nPos;
        }


        private List<int> scrollbarLocation = new List<int>();

        private void hScrollbar_Scroll(object sender, EventArgs e)
        {
            int eqAdjusterWidth = EQAdjuster.DefaultClientSize.Width;
            int position = this.hScrollbar.Value / eqAdjusterWidth * eqAdjusterWidth;

            if (position != this.panelEQGroup.HorizontalScroll.Value)
            {
                this.panelEQGroup.HorizontalScroll.Value = position;

                UpdateOwnHScrollbar();
            }
        }

        private void UpdateViewFromScrollbar(object state)
        {
            if (scrollbarLocation.Count > 0)
            {
                int position = scrollbarLocation[scrollbarLocation.Count - 1];
                this.panelEQGroup.HorizontalScroll.Value = position;

                UpdateOwnHScrollbar();
                this.scrollbarLocation.Clear();
            }
        }

        private void UpdateOwnHScrollbar()
        {
            if(!hScrollIsVisible)
            {
                return;
            }

            Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
            Win32API.GetScrollInfo(this.panelEQGroup.Handle, (int)Win32API.ScrollBarDirection.SB_HORZ, ref si);
            si.nPos = this.hScrollbar.Value ;
            si.nTrackPos = this.hScrollbar.Value;
            Win32API.SetScrollInfo(this.panelEQGroup.Handle, (int)Win32API.ScrollBarDirection.SB_HORZ, ref si, true);
        }

        private void UpdateNewHScrollbar(object state)
        {
            if (!hScrollIsVisible)
            {
                return;
            }

            Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
            Win32API.GetScrollInfo(this.panelEQGroup.Handle, (int)Win32API.ScrollBarDirection.SB_HORZ, ref si);
            this.hScrollbar.Value = si.nTrackPos;
            //si.nPos = this.hScrollbar.Value;
            //si.nTrackPos = this.hScrollbar.Value;
            //Win32API.SetScrollInfo(this.panelEQGroup.Handle, (int)Win32API.ScrollBarDirection.SB_HORZ, ref si, true);
        }

        protected Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.labelFilterType.Text = localization[Localization.eq_Type];
            this.labelGain.Text = localization[Localization.eq_Gain];
            this.labelFreq.Text = localization[Localization.eq_Freq];
            this.labelQ.Text = localization[Localization.eq_Q];
            if(module != null)
            {
                this.labelTitle.Text = this.module.eqGroupLabel;
            }
        }
    }
}
