using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Common;

namespace AEC.UI
{
    [ToolboxItem(true)]
    public partial class EQGraphicPanel : Control
    {
        private delegate void DelVoidCallback();

        private const int qIndex2offsetX = 12;
        private ModuleBase module;
        private ModuleBase[] otherModulesInTheGroups;
        private EQGraphicPoint selectedPoint = null;
        private PointF offset;
        private Point lastMouseDown;
        private short qIndexMouseDown = 0;

        private DateTime lastKeydownTimestamp = DateTime.Now;
        private Keys lastKey = Keys.None;

        private Dictionary<Keys, DelVoidCallback> key2EventMap;

        private Timer activeTime;
        private DateTime lastUpdateActiveTimestamp = DateTime.Now;

        private bool isShowGlobalEQ {get;set;}

        //private EQGraphic eqGraphic
        //{
        //    get
        //    {
        //        if(this.module != null && this.module.eqGraphic != null)
        //        {
        //            return this.module.eqGraphic;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        private ModuleBase currModule
        {
            get
            {
                if (this.module != null)
                {
                    if (this.module.isSecondaryModuleActivated)
                    {
                        return this.module.secondaryModule;
                    }
                    else
                    {
                        return this.module;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private EQGraphic eqGraphic
        {
            get
            {
                if (this.module != null)
                {
                    if(this.module.isSecondaryModuleActivated)
                    {
                        if(this.module.secondaryModule != null)
                        {
                            return this.module.secondaryModule.eqGraphic;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return this.module.eqGraphic;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private EQGraphicHelper eqGraphicHelper
        {
            get
            {
                return EQGraphicHelper.GetCurrInstance();
            }
        }

        public EQGraphicPanel()
        {
            InitializeComponent();
            this.Init();
        }

        public void BindingModule(ModuleBase module_, ModuleBase[] otherModulesInTheGroups_)
        {
            if(this.module != null)
            {
                this.module.eventBus.RemoveHandler(EQGraphicNeedToBeUpdatedHandler);
            }

            this.module = module_;
            this.module.InitEQNormalGraphic();
            this.otherModulesInTheGroups = otherModulesInTheGroups_;
            if(this.otherModulesInTheGroups != null)
            {
                foreach(ModuleBase otherModule in this.otherModulesInTheGroups)
                {
                    otherModule.InitEQNormalGraphic();
                }
            }
            this.UpdateGraphic();
            this.module.eventBus.RegisterHandler(EQGraphicNeedToBeUpdatedHandler);
            //AEC.Test.PrintTest.PointNow("InitEQGraphic()完成");
        }

        public void UpdateUIFromData()
        {
            if(this.module == null)
            {
                return;
            }

            this.module.InitEQNormalGraphic();

            if (this.otherModulesInTheGroups != null)
            {
                foreach (ModuleBase otherModule in this.otherModulesInTheGroups)
                {
                    otherModule.UpdateEQNormalFreqRespGraphic();
                }
            }

            this.UpdateGraphic();
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;

            this.key2EventMap = new Dictionary<Keys, DelVoidCallback>()
            {
                {Keys.Up, UpOperation},
                {Keys.Down, DownOperation},
                {Keys.Left, LeftOperation},
                {Keys.Right, RightOperation},
                {Keys.Home, HomeOperation},
                {Keys.End, EndOperation},
                {Keys.Add, AddOperation},
                {Keys.Subtract, SubtractOperation},
            };

            this.activeTime = new Timer();
            this.activeTime.Interval = 64;
            this.activeTime.Tick += activeTime_Tick;

            this.VisibleChanged += EQGraphicPanel_VisibleChanged;
        }

        public void RegisterSizeChangedHandler()
        {
            this.Resize += EQGraphicPanel_Resize;
        }

        public void RemoveSizeChangedHandler()
        {
            this.Resize -= EQGraphicPanel_Resize;
        }

        public void InitPanelSize()
        {
            if (this.Size.IsEmpty)
            {
                return;
            }
            //AEC.Log.OutputLine("EQGraphicPanel Size:{0}", this.Size);
            EQGraphicHelper.GetInstance(this.Size);
        }

        private void EQGraphicPanel_Resize(object sender, EventArgs e)
        {
            PanelSizeChanged();
        }

        private void PanelSizeChanged()
        {
            if (this.Size.IsEmpty)
            {
                return;
            }

            Tools.LogHelper.WriteInfo("EQGraphicPanel Size:{0}", this.Size);
            EQGraphicHelper.GetInstance(this.Size);
            if (this.module != null)
            {
                this.module.InitEQNormalGraphic();
            }

            if (this.otherModulesInTheGroups != null)
            {
                foreach (ModuleBase otherModule in this.otherModulesInTheGroups)
                {
                    otherModule.InitEQNormalGraphic();
                }
            }
        }

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    //if(this.Size.IsEmpty)
        //    //{
        //    //    return;
        //    //}
        //    //AEC.Log.OutputLine("EQGraphicPanel OnResize:{0}", this.Size);
        //    ////AEC.Test.PrintTest.PointNow("OnResize()1");
        //    //EQGraphicHelper.GetInstance(this.Size);
        //    ////AEC.Test.PrintTest.PointNow("OnResize()2");
        //    //if (this.module != null)
        //    //{
        //    //    this.module.InitEQGraphic();
        //    //}
        //    //AEC.Test.PrintTest.PointNow("OnResize()3");
        //}

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.SelectPoint(e.Location);
            this.Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.MovePoint(e.Location, e.Button);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.StopOperation();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if(e.Delta > 0)
            {
                this.UpOperation();
            }
            else
            {
                this.DownOperation();
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            this.isShowGlobalEQ = !isShowGlobalEQ;
            if(this.otherModulesInTheGroups != null)
            {
                this.UpdateGraphic();
            }
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (msg.Msg == Win32.WM_SYSKEYDOWN || msg.Msg == Win32.WM_KEYDOWN)
        //    {
        //        Console.WriteLine("ProcessCmdKey");
        //        if (keyData == Keys.Up ||
        //            keyData == Keys.Down ||
        //            keyData == Keys.Left ||
        //            keyData == Keys.Right)
        //        {
        //            if (this.lastKey != keyData || (DateTime.Now - lastKeydownTimestamp).Milliseconds >= 250)
        //            {
        //                this.lastKey = keyData;
        //                lastKeydownTimestamp = DateTime.Now;
        //                switch (keyData)
        //                {
        //                    case Keys.Up:
        //                        this.UpOperation();
        //                        break;

        //                    case Keys.Down:
        //                        this.DownOperation();
        //                        break;

        //                    case Keys.Left:
        //                        this.LeftOperation();
        //                        break;

        //                    case Keys.Right:
        //                        this.RightOperation();
        //                        break;
        //                }
        //                //AEC.Log.OutputLine("OnPreviewKeyDown{0}", e.KeyCode);
        //            }
        //        }

        //        return true;
        //    }

        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        //protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        //{
        //    base.OnPreviewKeyDown(e);
        //    if (e.KeyCode == Keys.Up ||
        //        e.KeyCode == Keys.Down ||
        //        e.KeyCode == Keys.Left ||
        //        e.KeyCode == Keys.Right)
        //    {
        //        if (this.lastKey != e.KeyCode || (DateTime.Now - lastKeydownTimestamp).Milliseconds >= 250)
        //        {
        //            this.lastKey = e.KeyCode;
        //            lastKeydownTimestamp = DateTime.Now;
        //            switch (e.KeyCode)
        //            {
        //                case Keys.Up:
        //                    this.UpOperation();
        //                    break;

        //                case Keys.Down:
        //                    this.DownOperation();
        //                    break;

        //                case Keys.Left:
        //                    this.LeftOperation();
        //                    break;

        //                case Keys.Right:
        //                    this.RightOperation();
        //                    break;
        //            }
        //            //AEC.Log.OutputLine("OnPreviewKeyDown{0}", e.KeyCode);
        //        }
        //    }
        //}

        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    switch (keyData)
        //    {
        //        case Keys.Left: MessageBox.Show("左"); break;
        //        case Keys.Right: MessageBox.Show("右"); break;
        //        case Keys.Up: MessageBox.Show("上"); break;
        //        case Keys.Down: MessageBox.Show("下"); break;
        //    }
        //    return true;
        //}

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == lastKey)
            {
                lastKey = Keys.None;
            }
            //AEC.Log.OutputLine("OnKeyUp:{0}", e.KeyCode);
        }
        //int count = 0;
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    //AEC.Log.OutputLine("EQ Graphic Panel OnKeyDown()...{0}", ++count);
        //    if (e.KeyCode == Keys.Up ||
        //        e.KeyCode == Keys.Down ||
        //        e.KeyCode == Keys.Left ||
        //        e.KeyCode == Keys.Right)
        //    {
        //        if (this.lastKey != e.KeyCode || (DateTime.Now - lastKeydownTimestamp).Milliseconds >= 250)
        //        {
        //            this.lastKey = e.KeyCode;
        //            lastKeydownTimestamp = DateTime.Now;
        //            switch (e.KeyCode)
        //            {
        //                case Keys.Up:
        //                    this.UpOperation();
        //                    break;

        //                case Keys.Down:
        //                    this.DownOperation();
        //                    break;

        //                case Keys.Left:
        //                    this.LeftOperation();
        //                    break;

        //                case Keys.Right:
        //                    this.RightOperation();
        //                    break;
        //            }
        //            //AEC.Log.OutputLine("OnPreviewKeyDown{0}", e.KeyCode);
        //        }
        //    }

        //    base.OnKeyDown(e);
        //}

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.key2EventMap.ContainsKey(keyData))
            {
                if (this.lastKey != keyData || (DateTime.Now - lastKeydownTimestamp).Milliseconds >= 250)
                {
                    this.lastKey = keyData;
                    lastKeydownTimestamp = DateTime.Now;

                    this.key2EventMap[keyData]();
                }

                return false;
            }

            return base.ProcessDialogKey(keyData);
        }

        //protected override void OnKeyPress(KeyPressEventArgs e)
        //{
        //    base.OnKeyPress(e);
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.PaintGraphic(pe.Graphics);
        }

        private void PaintGraphic(Graphics g)
        {
            if (this.module != null && this.module.eqGraphic != null)
            {
                if (this.isShowGlobalEQ && this.otherModulesInTheGroups != null)
                {
                    this.module.eqGraphic.PaintNormalBackground(g);

                    foreach (ModuleBase otherModule in this.otherModulesInTheGroups)
                    {
                        if (otherModule != null && otherModule.eqGraphic != null)
                        {
                            otherModule.eqGraphic.PaintNormalFreqRespInactiveCurve(g);
                        }
                    }

                    this.module.eqGraphic.PaintForeground(g);
                }
                else
                {
                    this.module.eqGraphic.PaintNormal(g);
                }
            }
        }

        private void UpOperation()
        {
            if (currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            if (currModule.activeItem == ActiveItem.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged)
                {
                    if (currModule.eqGroup.currEQ.gainCanBeIncreased)
                    {
                        currModule.eqGroup.currEQ.GainIncrease();
                        currModule.UpdateEQFromGraphicPanel(currModule.eqGroup.currEQ);
                        this.UpdateGraphic();
                    }
                }
            }
            else if (currModule.activeItem == ActiveItem.HPF)
            {
                if (currModule.hpf != null && currModule.hpfFreqCanBeChanged && currModule.hpf.freqCanBeIncreased)
                {
                    currModule.hpf.FreqIncrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Freq);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.LPF)
            {
                if (currModule.lpf != null && currModule.lpfFreqCanBeChanged && currModule.lpf.freqCanBeIncreased)
                {
                    currModule.lpf.FreqIncrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Freq);
                    UpdateGraphic();
                }
            }
        }

        private void DownOperation()
        {
            if (currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            if (currModule.activeItem == ActiveItem.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged)
                {
                    if (currModule.eqGroup.currEQ.gainCanBeDecreased)
                    {
                        currModule.eqGroup.currEQ.GainDecrease();
                        currModule.UpdateEQFromGraphicPanel(currModule.eqGroup.currEQ);
                        UpdateGraphic();
                    }
                }
            }
            else if (currModule.activeItem == ActiveItem.HPF)
            {
                if (currModule.hpf != null && currModule.hpfFreqCanBeChanged && currModule.hpf.freqCanBeDecreased)
                {
                    currModule.hpf.FreqDecrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Freq);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.LPF)
            {
                if (currModule.lpf != null && currModule.lpfFreqCanBeChanged && currModule.lpf.freqCanBeDecreased)
                {
                    currModule.lpf.FreqDecrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Freq);
                    UpdateGraphic();
                }
            }
        }

        private void LeftOperation()
        {
            if (currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            if (currModule.activeItem == ActiveItem.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged)
                {
                    if (currModule.eqGroup.currEQ.freqCanBeDecreased)
                    {
                        currModule.eqGroup.currEQ.FreqDecrease();
                        currModule.UpdateEQFromGraphicPanel(currModule.eqGroup.currEQ);
                        UpdateGraphic();
                    }
                }
            }
            else if (currModule.activeItem == ActiveItem.HPF)
            {
                if (currModule.hpf != null && currModule.hpfFreqCanBeChanged && currModule.hpf.freqCanBeDecreased)
                {
                    currModule.hpf.FreqDecrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Freq);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.LPF)
            {
                if (currModule.lpf != null && currModule.lpfFreqCanBeChanged && currModule.lpf.freqCanBeDecreased)
                {
                    currModule.lpf.FreqDecrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Freq);
                    UpdateGraphic();
                }
            }
        }

        private void RightOperation()
        {
            if (currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            if (currModule.activeItem == ActiveItem.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged)
                {
                    if (currModule.eqGroup.currEQ.freqCanBeIncreased)
                    {
                        currModule.eqGroup.currEQ.FreqIncrease();
                        currModule.UpdateEQFromGraphicPanel(currModule.eqGroup.currEQ);
                        UpdateGraphic();
                    }
                }
            }
            else if (currModule.activeItem == ActiveItem.HPF)
            {
                if (currModule.hpf != null && currModule.hpfFreqCanBeChanged && currModule.hpf.freqCanBeIncreased)
                {
                    currModule.hpf.FreqIncrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Freq);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.LPF)
            {
                if (currModule.lpf != null && currModule.lpfFreqCanBeChanged && currModule.lpf.freqCanBeIncreased)
                {
                    currModule.lpf.FreqIncrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Freq);
                    UpdateGraphic();
                }
            }
        }

        private void HomeOperation()
        {
            if (currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            if (currModule.activeItem == ActiveItem.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged)
                {
                    currModule.eqGroup.currEQ.Change2PrevMode();
                    currModule.UpdateEQFromGraphicPanel(currModule.eqGroup.currEQ);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.HPF)
            {
                if (currModule.hpf != null && currModule.hpf.typeCanChanged)
                {
                    currModule.hpf.Change2PrevType();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Type);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.LPF)
            {
                if (currModule.lpf != null && currModule.lpf.typeCanChanged)
                {
                    currModule.lpf.Change2PrevType();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Type);
                    UpdateGraphic();
                }
            }
        }

        private void EndOperation()
        {
            if (currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            if (currModule.activeItem == ActiveItem.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged)
                {
                    currModule.eqGroup.currEQ.Change2NextMode();
                    currModule.UpdateEQFromGraphicPanel(currModule.eqGroup.currEQ);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.HPF)
            {
                if (currModule.hpf != null && currModule.hpf.typeCanChanged)
                {
                    currModule.hpf.Change2NextType();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Type);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.LPF)
            {
                if (currModule.lpf != null && currModule.lpf.typeCanChanged)
                {
                    currModule.lpf.Change2NextType();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Type);
                    UpdateGraphic();
                }
            }
        }

        private void AddOperation()
        {
            if (currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            if (currModule.activeItem == ActiveItem.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged && currModule.eqGroup.currEQ.qCanBeIncreased)
                {
                    currModule.eqGroup.currEQ.QIncrease();
                    currModule.UpdateEQFromGraphicPanel(currModule.eqGroup.currEQ);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.HPF)
            {
                if (currModule.hpf != null && currModule.hpf.qCanBeIncreased)
                {
                    currModule.hpf.QIncrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Q);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.LPF)
            {
                if (currModule.lpf != null && currModule.lpf.qCanBeIncreased)
                {
                    currModule.lpf.QIncrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Q);
                    UpdateGraphic();
                }
            }
        }

        private void SubtractOperation()
        {
            if (currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            if (currModule.activeItem == ActiveItem.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged && currModule.eqGroup.currEQ.qCanBeDecreased)
                {
                    currModule.eqGroup.currEQ.QDecrease();
                    currModule.UpdateEQFromGraphicPanel(currModule.eqGroup.currEQ);
                    UpdateGraphic();
                }
            }
            else if (currModule.activeItem == ActiveItem.HPF)
            {
                if (currModule.hpf != null && currModule.hpf.qCanBeDecreased)
                {
                    currModule.hpf.QDecrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Q);
                    UpdateGraphic(); 
                }
            }
            else if (currModule.activeItem == ActiveItem.LPF)
            {
                if (currModule.lpf != null && currModule.lpf.qCanBeDecreased)
                {
                    currModule.lpf.QDecrease();
                    currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Q);
                    UpdateGraphic();
                }
            }
        }

        private void SelectPoint(Point cursor)
        {
            if (currModule == null || !currModule.hasFreqRespCurve || eqGraphic == null || eqGraphic.allPointList == null || eqGraphic.allPointList.Length == 0)
            {
                return;
            }

            float[] distanceList = new float[eqGraphic.allPointList.Length];
            float minDistance = float.MaxValue;
            short minIndex = -1;
            for (short i = 0; i < eqGraphic.allPointList.Length; ++i)
            {
                if(!eqGraphic.allPointList[i].visible)
                {
                    continue;
                }

                PointF center = eqGraphic.allPointList[i].center;
                float distance = EQGraphic.CalcDistance(center, cursor);
                if(distance < minDistance)
                {
                    minIndex = i;
                    minDistance = distance;
                }
            }

            if (minIndex < 0 || minDistance > EQGraphic.normalFilterRadius)
            {
                return;
            }

            lastMouseDown = cursor;
            offset = new PointF(eqGraphic.allPointList[minIndex].center.X - cursor.X, eqGraphic.allPointList[minIndex].center.Y - cursor.Y);
            EQPointType currType = eqGraphic.allPointList[minIndex].type;
            if (currType == EQPointType.EQ && eqGraphic.eqPoints != null && minIndex < eqGraphic.eqPoints.Length)
            {
                selectedPoint = eqGraphic.allPointList[minIndex];

                if (currModule.activeItem != ActiveItem.EQ || currModule.eqGroup.currEQIndex != minIndex)
                {
                    currModule.activeItem = ActiveItem.EQ;
                    currModule.eqGroup.currEQIndex = minIndex;
                    CurrEQIndexChanged();
                    UpdateGraphic();
                }

                qIndexMouseDown = currModule.eqGroup.currEQ.qIndex;
            }
            else if (currType == EQPointType.HPF)
            {
                if (eqGraphic.hpfPoint == null || !eqGraphic.hpfPoint.visible)
                {
                    return;
                }

                selectedPoint = eqGraphic.hpfPoint;
                if (currModule.activeItem != ActiveItem.HPF)
                {
                    currModule.activeItem = ActiveItem.HPF;
                    UpdateGraphic();
                }

                qIndexMouseDown = currModule.hpf.filterQIndex;
            }
            else if (currType == EQPointType.LPF)
            {
                if (eqGraphic.lpfPoint == null || !eqGraphic.lpfPoint.visible)
                {
                    return;
                }

                selectedPoint = eqGraphic.lpfPoint;
                if (currModule.activeItem != ActiveItem.LPF)
                {
                    currModule.activeItem = ActiveItem.LPF;
                    UpdateGraphic();
                }

                qIndexMouseDown = currModule.lpf.filterQIndex;
            }

            //if (EQGraphic.CheckPointIsSelected(center, cursor))
            //{
            //    lastMouseDown = cursor;
            //    offset = new PointF(center.X - cursor.X, center.Y - cursor.Y);
            //    EQPointType currType = eqGraphic.allPointList[i].type;
            //    if (currType == EQPointType.EQ && i < eqGraphic.eqPoints.Length)
            //    {
            //        selectedPoint = eqGraphic.allPointList[i];

            //        if (module.activeItem != ActiveItem.EQ || module.currEQIndex != i)
            //        {
            //            module.activeItem = ActiveItem.EQ;
            //            module.currEQIndex = i;
            //            UpdateGraphic();
            //        }

            //        qIndexMouseDown = module.currEQ.qIndex;
            //    }
            //    else if (currType == EQPointType.HPF)
            //    {
            //        selectedPoint = eqGraphic.hpfPoint;
            //        if (module.activeItem != ActiveItem.HPF)
            //        {
            //            module.activeItem = ActiveItem.HPF;
            //            UpdateGraphic();
            //        }

            //        qIndexMouseDown = module.hpf.qIndex;
            //    }
            //    else if (currType == EQPointType.LPF)
            //    {
            //        selectedPoint = eqGraphic.lpfPoint;
            //        if (module.activeItem != ActiveItem.LPF)
            //        {
            //            module.activeItem = ActiveItem.LPF;
            //            UpdateGraphic();
            //        }

            //        qIndexMouseDown = module.lpf.qIndex;
            //    }
            //    break;
            //}
        }

        private void MovePoint(Point cursor, MouseButtons mb)
        {
            if(selectedPoint == null || eqGraphicHelper == null)
            {
                return;
            }

            PointF newLocation = new PointF(cursor.X + offset.X, cursor.Y + offset.Y);
            //AEC.Log.OutputLine();
            if(selectedPoint.type == EQPointType.EQ)
            {
                if (currModule.eqGroup != null && currModule.eqCanBeChanged && currModule.eqGroup.currEQ.canBeChanged)
                {
                    EQPoint point = selectedPoint as EQPoint;
                    if(mb == System.Windows.Forms.MouseButtons.Left)
                    {
                        short newFreqIndex = eqGraphicHelper.PanelX2NearestEQFreq(newLocation.X);
                        short newGainIndex = eqGraphicHelper.PanelY2NearestGain(newLocation.Y);
                        if (point.eq.freqIndex != newFreqIndex || point.eq.gainIndex != newGainIndex)
                        {
                            point.eq.freqIndex = newFreqIndex;
                            point.eq.gainIndex = newGainIndex;
                            currModule.UpdateEQFromGraphicPanel(point.eq);
                            UpdateGraphic();
                        }
                    }
                    else if(mb == System.Windows.Forms.MouseButtons.Right)
                    {
                        short diffX = (short)((cursor.X - lastMouseDown.X) / qIndex2offsetX);
                        short qIndex = point.eq.ConstrainQ((short)(qIndexMouseDown + diffX));
                        if(qIndex != point.eq.qIndex)
                        {
                            //lastMouseDown = cursor;
                            point.eq.qIndex = qIndex;
                            currModule.UpdateEQFromGraphicPanel(point.eq);
                            UpdateGraphic();
                            //this.CurrEQChanged();
                        }
                    }
                    //AEC.Log.OutputLine("freq = {0:0.0}Hz, gain = {1:0.0}dB", DataUtils.eqFreqList[newFreqIndex], DataUtils.eqGainList[newGainIndex]);
                }
            }
            else if(selectedPoint.type == EQPointType.HPF)
            {
                HPFPoint point = selectedPoint as HPFPoint;
                if (mb == System.Windows.Forms.MouseButtons.Left)
                {
                    if (currModule.hpfFreqCanBeChanged)
                    {
                        short newFreqIndex = eqGraphicHelper.PanelX2NearestFilterFreq(newLocation.X);
                        newFreqIndex = point.filter.ConstrainFreqIndex(newFreqIndex);
                        //AEC.Log.OutputLine("hpf freqIndex = {0}, {1}", newFreqIndex, point.filter.ConstrainFreqIndex(newFreqIndex));
                        if (point.filter.filterFreqIndex != newFreqIndex)
                        {
                            //point.filter.freqIndex = newFreqIndex;
                            point.filter.filterFreqIndex = newFreqIndex;
                            currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Freq);
                            UpdateGraphic();
                        }
                    }
                }
                else if (mb == System.Windows.Forms.MouseButtons.Right)
                {
                    if (point.filter.qCanBeChanged)
                    {
                        short diffX = (short)((cursor.X - lastMouseDown.X) / qIndex2offsetX);
                        short qIndex = Utils.ConstrainValue((short)(qIndexMouseDown + diffX), point.filter.qLowerLimitIndex, point.filter.qUpperLimitIndex);
                        if (qIndex != point.filter.filterQIndex)
                        {
                            point.filter.filterQIndex = qIndex;
                            currModule.UpdateXPFFromGraphicPanel(XPFilterType.HighPass, FilterElement.Q);
                            UpdateGraphic();
                        }
                    }
                }
            }
            else if (selectedPoint.type == EQPointType.LPF)
            {
                LPFPoint point = selectedPoint as LPFPoint;
                if (mb == System.Windows.Forms.MouseButtons.Left)
                {
                    if (currModule.lpfFreqCanBeChanged)
                    {
                        short newFreqIndex = eqGraphicHelper.PanelX2NearestFilterFreq(newLocation.X);
                        newFreqIndex = point.filter.ConstrainFreqIndex(newFreqIndex);
                        //AEC.Log.OutputLine("hpf freqIndex = {0}, {1}", newFreqIndex, point.filter.ConstrainFreqIndex(newFreqIndex));
                        if (point.filter.filterFreqIndex != newFreqIndex)
                        {
                            currModule.lpf.filterFreqIndex = newFreqIndex;
                            currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Freq);
                            UpdateGraphic();
                        }
                    }
                }
                else if (mb == System.Windows.Forms.MouseButtons.Right)
                {
                    if (point.filter.qCanBeChanged)
                    {
                        short diffX = (short)((cursor.X - lastMouseDown.X) / qIndex2offsetX);
                        short qIndex = Utils.ConstrainValue((short)(qIndexMouseDown + diffX), point.filter.qLowerLimitIndex, point.filter.qUpperLimitIndex);
                        if (qIndex != point.filter.filterQIndex)
                        {
                            point.filter.filterQIndex = qIndex;
                            currModule.UpdateXPFFromGraphicPanel(XPFilterType.LowPass, FilterElement.Q);
                            UpdateGraphic();
                        }
                    }
                }
            }
        }

        private void StopOperation()
        {
            selectedPoint = null;
        }

        public void UpdateGraphic()
        {
            #region 不用双缓冲
            this.Invalidate();
            this.Update();
            #endregion

            //Rectangle clientRect = this.ClientRectangle;
            //if (clientRect.Width == 0 || clientRect.Height == 0)
            //{
            //    return;
            //}

            //Graphics graphics = this.CreateGraphics();
            //BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            //BufferedGraphics buffer = currentContext.Allocate(graphics, clientRect);
            //Graphics g = buffer.Graphics;

            //this.RenderGraphic(g);

            //buffer.Render(graphics);

            //g.Dispose();
            //buffer.Dispose();
            //currentContext.Dispose();
            //graphics.Dispose();
        }

        private void EQGraphicNeedToBeUpdatedHandler(object sender, EventBusArgs args)
        {
            if (args.type == EventType.EQGraphicNeedToBeUpdated)
            {
                this.UpdateGraphic();
            }
        }

        private void CurrEQIndexChanged()
        {
            if (this.currModule == null || !currModule.hasFreqRespCurve)
            {
                return;
            }

            this.currModule.eventBus.NotifyObeserver(new CurrEQIndexChangedFromGraphicArgs());
        }

        private void EQGraphicPanel_VisibleChanged(object sender, EventArgs e)
        {
            this.activeTime.Enabled = this.Visible;
        }

        private void activeTime_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - lastUpdateActiveTimestamp).Milliseconds <= 50 || this.eqGraphic == null)
            {
                return;
            }

            lastUpdateActiveTimestamp = DateTime.Now;
            EQGraphic.TimePassing();

            PointF activeCenter = PointF.Empty;
            switch (this.module.activeItem)
            {
                case ActiveItem.EQ:
                    activeCenter = this.eqGraphic.eqPoints[this.module.eqGroup.currEQIndex].center;
                    break;

                case ActiveItem.HPF:
                    activeCenter = this.eqGraphic.hpfPoint.center;
                    break;

                case ActiveItem.LPF:
                    activeCenter = this.eqGraphic.lpfPoint.center;
                    break;
            }

            Rectangle invalidRect = new Rectangle((int)(activeCenter.X - EQGraphic.activeMaxRadius), (int)(activeCenter.Y - EQGraphic.activeMaxRadius), 2 * EQGraphic.activeMaxRadius, 2 * EQGraphic.activeMaxRadius);
            this.Invalidate(invalidRect);
            this.Update();
        }
    }
}
