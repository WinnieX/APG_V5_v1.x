using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Common;
using AEC.Effectors.MX;
using AEC.Effectors.MX.Modules;
using AEC.Misc;

namespace AEC.UI.MX
{
    public partial class MXDeqPanel : PanelBase
    {
        private DeqModule deqModule;
        private DEQAdjuster[] deqAdjusterList;
        private int lastActiveIndex = 0;

        public MXDeqPanel()
        {
            InitializeComponent();
            InitFont();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
        }

        protected void BindingDeqModule(DeqModule deqModule_)
        {
            this.deqModule = deqModule_;
            this.InitDeqAdjusterList();
            this.deqModule.eventBus.RegisterHandler(EQChangedHandler);
            //base.BindingOutputBaseModule(this.output);
            //this.sliderDelay.Binding(this.output.delay, OutputBaseModule.GetDelayLabel, OutputBaseModule.CalcDelayIndexFromText);
            //this.chkMute.isChecked = this.output.mute.value;

            //base.cmbHpfMode.DropDownBtnColor = this.output.themeColor;
            //base.cmbLpfMode.DropDownBtnColor = this.output.themeColor;

            //if (this._eventBus != null)
            //{
            //    this._eventBus.RegisterHandler(EventBusHandler);
            //}
        }

        public void InitDeqAdjusterList()
        {
            if (this.deqAdjusterList != null && this.deqAdjusterList.Length != this.deqModule.eqGroup.eqBandCount)
            {
                this.panelDEQGroup.Controls.Clear();
                foreach (DEQAdjuster deqAdjuster in this.deqAdjusterList)
                {
                    if (deqAdjuster != null)
                    {
                        deqAdjuster.EQChanged -= DEQGroupPanel_EQChanged;
                        deqAdjuster.DEQParamUpdated -= DEQGroupPanel_DEQParamUpdated;
                        deqAdjuster.Activated -= DEQGroupPanel_Activated;
                        deqAdjuster.Dispose();
                    }
                }
            }

            if (this.deqAdjusterList == null)
            {
                this.deqAdjusterList = new DEQAdjuster[this.deqModule.eqGroup.eqBandCount];
            }

            for (int i = 0; i < this.deqAdjusterList.Length; ++i)
            //    for (int i = 0; i < 1; ++i)
            {
                if (this.deqAdjusterList[i] == null)
                {
                    this.deqAdjusterList[i] = new DEQAdjuster();
                    this.deqAdjusterList[i].EQChanged += DEQGroupPanel_EQChanged;
                    this.deqAdjusterList[i].DEQParamUpdated += DEQGroupPanel_DEQParamUpdated;
                    this.deqAdjusterList[i].Activated += DEQGroupPanel_Activated;
                }

                this.deqAdjusterList[i].activeIndexLabelBackground = GetActiveIndexLabelBackground(this.deqModule);
                this.deqAdjusterList[i].activeIndexLabelColor = this.deqModule.themeColor;
                this.deqAdjusterList[i].eqTypeImage = GetEQTypeImage(this.deqModule);
                this.deqAdjusterList[i].sliderBackgroundNormal = GetSliderBackgroundNormal(this.deqModule);

                this.deqAdjusterList[i].BindingDEQ(this.deqModule.deqGroup.deqList[i], base.mController.mEffectorThreshold);

                this.deqAdjusterList[i].isActiveInit = i == this.deqModule.eqGroup.currEQIndex;

                if (!this.panelDEQGroup.Controls.Contains(this.deqAdjusterList[i]))
                {
                    this.panelDEQGroup.Controls.Add(this.deqAdjusterList[i]);
                    if(i < this.deqAdjusterList.Length - 1)
                    {
                        SplitLine sl = new SplitLine();
                        sl.Height = this.deqAdjusterList[i].Height;
                        sl.Width = 1;
                        sl.BackColor = Color.FromArgb(96, 96, 96);
                        this.panelDEQGroup.Controls.Add(sl);
                    }
                }
            }

            if (this.lastActiveIndex != this.deqModule.eqGroup.currEQIndex)
            {
                this.deqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                this.lastActiveIndex = this.deqModule.eqGroup.currEQIndex;
                this.deqAdjusterList[this.deqModule.eqGroup.currEQIndex].Focus();
            }
        }

        private void DEQGroupPanel_EQChanged(object sender, EventArgs e)
        {
            EQ eq = sender as EQ;
            this.deqModule.eqGroup.currEQIndex = eq.index;
            this.deqAdjusterList[this.deqModule.eqGroup.currEQIndex].isActiveInit = true;
            if (this.lastActiveIndex != eq.index)
            {
                this.deqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                this.lastActiveIndex = this.deqModule.eqGroup.currEQIndex;
                //AEC.Log.OutputLine("last index:{0}取消激活", this.module.lastEQIndex);
            }
            this.deqModule.UpdateEQFromControlPanel(eq);

            //AEC.Log.OutputLine("eq:{0}", eq.label);
        }

        private void DEQGroupPanel_DEQParamUpdated(object sender, DEQParamUpdatedArgs e)
        {
            bool eqGraphicNeedToBeUpdated = false;
            this.deqModule.eqGroup.currEQIndex = e.deq.index;
            this.deqAdjusterList[this.deqModule.eqGroup.currEQIndex].isActiveInit = true;
            if (this.lastActiveIndex != e.deq.index)
            {
                this.deqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                this.lastActiveIndex = this.deqModule.eqGroup.currEQIndex;
                eqGraphicNeedToBeUpdated = true;
            }

            if(e.paramType == DeqParamType.DCM)
            {
                e.deq.ResetEQ();
                this.deqAdjusterList[e.deq.index].UpdateEQ();
                this.deqModule.UpdateEQFromControlPanel(e.deq);
            }
            else if(e.paramType == DeqParamType.DeqIsPass)
            {
                this.deqModule.UpdateDEQFromControlPanel(e.deq);
                eqGraphicNeedToBeUpdated = false;
            }

            if(eqGraphicNeedToBeUpdated)
            {
                this.deqModule.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
            }

            this.deqModule.UpdateDEQParam(e.deq, e.paramType);
        }

        private void DEQGroupPanel_Activated(object sender, EventArgs e)
        {
            DEQ deq = sender as DEQ;

            this.deqModule.eqGroup.currEQIndex = deq.index;
            //AEC.Log.OutputLine("last index:{0}, curr Index = {1}", this.lastActiveIndex, this.module.currEQIndex);
            this.deqAdjusterList[this.deqModule.eqGroup.currEQIndex].isActiveInit = true;
            if (this.lastActiveIndex != deq.index)
            {
                this.deqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                this.lastActiveIndex = this.deqModule.eqGroup.currEQIndex;
                //AEC.Log.OutputLine("last index:{0}取消激活", this.module.lastEQIndex);
            }
            //AEC.Log.OutputLine("curr index:{0}, last index:{1}", this.module.currEQIndex, this.module.lastEQIndex);

            //this.module.UpdateEQ(eq);
            this.deqModule.activeItem = ActiveItem.EQ;
            this.deqModule.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
        }

        private void EQChangedHandler(object sender, EventBusArgs args)
        {
            if (args.type == EventType.CurrEQChangedFromGraphic)
            {
                this.deqAdjusterList[this.deqModule.eqGroup.currEQIndex].UpdateEQUIFromData();
            }
            else if (args.type == EventType.CurrEQIndexChangedFromGraphic)
            {
                //Log.OutputLine("EQAdjuster Focus()");
                this.deqAdjusterList[this.deqModule.eqGroup.currEQIndex].Focus();
                this.deqAdjusterList[this.deqModule.eqGroup.currEQIndex].isActiveInit = true;
                if (this.lastActiveIndex != this.deqModule.eqGroup.currEQIndex)
                {
                    this.deqAdjusterList[this.lastActiveIndex].isActiveInit = false;
                    this.lastActiveIndex = this.deqModule.eqGroup.currEQIndex;
                }
            }
        }

        public override void UpdateUIFromData()
        {
            if(this.deqAdjusterList != null)
            {
                foreach(DEQAdjuster deqAdjuster in this.deqAdjusterList)
                {
                    deqAdjuster.UpdateUIFromData();
                }
            }
        }

        //public override void UpdateEffectorModel()
        //{
        //    if (this.deqAdjusterList != null)
        //    {
        //        foreach (DEQAdjuster deqAdjuster in this.deqAdjusterList)
        //        {
        //            deqAdjuster.UpdateEffectorModel();
        //        }
        //    }
        //}

        public override void UpdateDisplayLanguage()
        {
            if (this.deqAdjusterList != null)
            {
                foreach (DEQAdjuster deqAdjuster in this.deqAdjusterList)
                {
                    deqAdjuster.UpdateDisplayLanguage();
                }
            }

            this.btnReset.Text = localization[Localization.deqReset];
            this.panelDEQGroup.Invalidate();
        }

        private static Image GetActiveIndexLabelBackground(DeqModule deqModule)
        {
            switch(deqModule.moduleIndex)
            {
                case AEC.Comm.MXComm.MXCommProtocol.musicDeqModuleIndex:
                    return AEC.Properties.Resources.music_EQ_index_back_actived;

                case AEC.Comm.MXComm.MXCommProtocol.micDeqModuleIndex:
                    return AEC.Properties.Resources.mic_EQ_index_back_actived;

                default:
                    throw new Exception("索引对应模块没有DEQ!");
            }
        }

        private static Image GetSliderBackgroundNormal(DeqModule deqModule)
        {
            switch (deqModule.moduleIndex)
            {
                case AEC.Comm.MXComm.MXCommProtocol.musicDeqModuleIndex:
                    return AEC.Properties.Resources.music_deq_slider_back_normal;

                case AEC.Comm.MXComm.MXCommProtocol.micDeqModuleIndex:
                    return AEC.Properties.Resources.mic_deq_slider_back_normal;;

                default:
                    throw new Exception("索引对应模块没有DEQ!");
            }
        }

        private static EQTypeImage GetEQTypeImage(DeqModule deqModule)
        {
            switch (deqModule.moduleIndex)
            {
                case AEC.Comm.MXComm.MXCommProtocol.musicDeqModuleIndex:
                    return new EQTypeImage(
                            AEC.Properties.Resources.music_EQ_peak_hover,
                            AEC.Properties.Resources.music_EQ_peak_checked,
                            AEC.Properties.Resources.music_EQ_ls_hover,
                            AEC.Properties.Resources.music_EQ_ls_checked,
                            AEC.Properties.Resources.music_EQ_hs_hover,
                            AEC.Properties.Resources.music_EQ_hs_checked
                            );

                case AEC.Comm.MXComm.MXCommProtocol.micDeqModuleIndex:
                    return new EQTypeImage(
                            AEC.Properties.Resources.mic_EQ_peak_hover,
                            AEC.Properties.Resources.mic_EQ_peak_checked,
                            AEC.Properties.Resources.mic_EQ_ls_hover,
                            AEC.Properties.Resources.mic_EQ_ls_checked,
                            AEC.Properties.Resources.mic_EQ_hs_hover,
                            AEC.Properties.Resources.mic_EQ_hs_checked
                            );

                default:
                    throw new Exception("索引对应模块没有DEQ!");
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnM1))
            {
                this.UpdateToM1();
            }
            else if(sender.Equals(this.btnReset))
            {
                this.Reset();
            }
        }

        private void UpdateToM1()
        {
            System.Diagnostics.Debug.Assert(this.deqModule.deqGroup.eqBandCount == 3 && this.deqAdjusterList.Length == this.deqModule.deqGroup.eqBandCount);
            DEQ deq1 = this.deqModule.deqGroup.deqList[0];
            deq1.type = EQType.Peaking;
            deq1.gain = 3.6;
            deq1.freq = 40.1;
            deq1.q = 0.4;
            deq1.isPass = false;
            deq1.dcm = DEQControlMode.Below;

            DEQ deq2 = this.deqModule.deqGroup.deqList[1];
            deq2.type = EQType.Peaking;
            deq2.gain = 1.4;
            deq2.freq = 86.7;
            deq2.q = 0.4;
            deq2.isPass = false;
            deq2.dcm = DEQControlMode.Below;

            DEQ deq3 = this.deqModule.deqGroup.deqList[2];
            deq3.type = EQType.HighShelf;
            deq3.gain = -7.4;
            deq3.freq = 1390.0;
            deq3.q = 1;
            deq3.isPass = false;
            deq3.dcm = DEQControlMode.Above;

            foreach (DEQAdjuster deqAdjuster in this.deqAdjusterList)
            {
                deqAdjuster.InitDeqCM();
                deqAdjuster.UpdateEQ();
                deqAdjuster.UpdateEQIsPass();
            }

            foreach (DEQ deq in this.deqModule.deqGroup.deqList)
            {
                this.deqModule.UpdateDEQParam(deq, DeqParamType.DCM);
                this.deqModule.UpdateDEQParam(deq, DeqParamType.DeqIsPass);
            }

            this.deqModule.UpdateAllEQFromControlPanel();
        }

        private void Reset()
        {
            if (DialogResult.OK != gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.deqReset_Tips], "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                return;
            }

            System.Diagnostics.Debug.Assert(this.deqModule.deqGroup.eqBandCount == 3 && this.deqAdjusterList.Length == this.deqModule.deqGroup.eqBandCount);
            DEQ deq1 = this.deqModule.deqGroup.deqList[0];
            deq1.ResetEQ();
            deq1.isPass = false;
            deq1.dcm = DEQControlMode.Off;

            DEQ deq2 = this.deqModule.deqGroup.deqList[1];
            deq2.ResetEQ();
            deq2.isPass = false;
            deq2.dcm = DEQControlMode.Off;

            DEQ deq3 = this.deqModule.deqGroup.deqList[2];
            deq3.ResetEQ();
            deq3.isPass = false;
            deq3.dcm = DEQControlMode.Off;

            foreach (DEQAdjuster deqAdjuster in this.deqAdjusterList)
            {
                deqAdjuster.InitDeqCM();
                deqAdjuster.UpdateEQ();
                deqAdjuster.UpdateEQIsPass();
            }

            foreach (DEQ deq in this.deqModule.deqGroup.deqList)
            {
                this.deqModule.UpdateDEQParam(deq, DeqParamType.DCM);
                this.deqModule.UpdateDEQParam(deq, DeqParamType.DeqIsPass);
            }

            this.deqModule.UpdateAllEQFromControlPanel();
        }
    }

    public class MXMusicDeqPanel : MXDeqPanel
    {
        public MXMusicDeqPanel()
            : base()
        {
            
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            base.BindingDeqModule(base.mController.mxEffectorModel.musicDeq);
            //this.btnM1.ForeColor = base.mController.mxEffectorModel.musicDeq.themeColor;
            //this.btnReset.ForeColor = base.mController.mxEffectorModel.musicDeq.themeColor;
        }
    }

    public class MXMicDeqPanel : MXDeqPanel
    {
        public MXMicDeqPanel()
            : base()
        {
            this.btnM1.Visible = false;
            this.btnReset.Visible = false;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            base.BindingDeqModule(base.mController.mxEffectorModel.micDeq);
        }
    }
}
