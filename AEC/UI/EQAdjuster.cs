using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.UI.Utility;
using AEC.UI.Skin;

namespace AEC.UI
{
    public partial class EQAdjuster : UserControl
    {
        private delegate void VoidCallback();

        public static readonly Size DefaultClientSize = new Size(80, 209);
        private const string GainUnit = "dB";
        private const string FreqUnit = "Hz";
        private const string Kilo = "k";
        private static readonly Image eqIndexBackgroundNormal = AEC.Properties.Resources.EQ_index_back_normal;

        private EQ eq;
        private string indexLabel = string.Empty;
        private Color labelIndexForeColor = Color.FromArgb(32, 32, 64);
        
        private Image indexBackground = eqIndexBackgroundNormal;
        private Point eqIndexLeftTop = new Point(3, 3);
        private Size eqLabelSize = new Size(28, 23);
        private Rectangle eqIndexRect;

        private DateTime lastKeydownTimestamp = DateTime.Now;
        private Keys lastKey = Keys.None;
        private bool _canBeChanged = true;

        public event EventHandler<EventArgs> EQChanged;
        public event EventHandler<EventArgs> Activated;

        private Dictionary<Keys, VoidCallback> key2EventMap;

        private bool _isShowIndex = true;
        public bool isShowIndex
        {
            get
            {
                return _isShowIndex;
            }
            set
            {
                _isShowIndex = value;
            }
        }
        //private Bitmap cachedBitmap = null;
        public void InitTheme()
        {
            this.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.gainSlider.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.buttonP.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.buttonLS.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.buttonHS.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.paramLabelGain.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.paramLabelFreq.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.paramLabelQ.BackColor = SkinManager.CurrentSkin.PanelBackColor;
        }

        public EQAdjuster()
        {
            InitializeComponent();
            this.Init();
        }

        //public EQAdjuster(EQ eq_)
        //{
        //    InitializeComponent();
        //    this.Init();
        //    this.BindingEQ(eq_);
        //}

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

            this.gainSlider.Init(DataUtils.maxGainIndex, DataUtils.defGainIndex);
            this.paramLabelGain.Init(DataUtils.maxGainIndex, DataUtils.defGainIndex, GetGainLabel, CalcGainIndexFromText);
            this.paramLabelFreq.Init(DataUtils.maxEQFreqIndex, DataUtils.defFreqIndex, GetFreqLabel, CalcFreqIndexFromText);
            //this.paramLabelQ.Init(DataUtils.maxGainIndex, DataUtils.defGainIndex, GetGainLabel, CalcGainIndexFromText);

            this.gainSlider.ValueChanged += EQ_ValueChanged;
            this.paramLabelGain.ValueChanged += EQ_ValueChanged;
            this.paramLabelFreq.ValueChanged += EQ_ValueChanged;
            this.paramLabelQ.ValueChanged += EQ_ValueChanged;

            //this.GotFocus += EQAdjuster_GotFocus;
            //this.LostFocus += EQAdjuster_LostFocus;

            this.eqIndexRect = new Rectangle(eqIndexLeftTop, indexBackground.Size);

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EQAdjuster_MouseDown);
            this.buttonHS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EQAdjuster_MouseDown);
            this.buttonLS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EQAdjuster_MouseDown);
            this.buttonP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EQAdjuster_MouseDown);
            this.paramLabelQ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EQAdjuster_MouseDown);
            this.paramLabelFreq.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EQAdjuster_MouseDown);
            this.paramLabelGain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EQAdjuster_MouseDown);
            this.gainSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EQAdjuster_MouseDown);

            this.key2EventMap = new Dictionary<Keys, VoidCallback>()
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

            //this.buttonP.Visible = false;
            //this.buttonLS.Visible = false;
            //this.buttonHS.Visible = false;
            this.InitTheme();
        }

        //private void EQAdjuster_GotFocus(object sender, EventArgs e)
        //{
        //    //this.indexBackground = AEC.Properties.Resources.music_EQ_index_back_actived;
        //    Log.OutputLine("EQAdjuster_GotFocus");
        //}

        //private void EQAdjuster_LostFocus(object sender, EventArgs e)
        //{
        //    //this.indexBackground = AEC.Properties.Resources.EQ_index_back_normal;
        //    Log.OutputLine("EQAdjuster_LostFocus");
        //}

        private void EQ_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.gainSlider))
            {
                this.paramLabelGain.initV = this.gainSlider.v;
            }
            else if(sender.Equals(this.paramLabelGain))
            {
                this.gainSlider.initV = this.paramLabelGain.v;
            }
            //else if (sender.Equals(this.paramLabelFreq))
            //{
                
            //}
            //else if (sender.Equals(this.paramLabelQ))
            //{

            //}

            if(this.eq != null)
            {
                this.eq.freqIndex = this.paramLabelFreq.v;
                this.eq.gainIndex = this.paramLabelGain.v;
                this.eq.qIndex = this.paramLabelQ.v;

                this.OnEQChanged();
            }
        }

        public void BindingEQ(EQ eq_)
        {
            this.eq = eq_;
            this.indexLabel = (eq.index + 1).ToString();

            this.gainSlider.initV = this.eq.gainIndex;
            this.paramLabelGain.initV = this.eq.gainIndex;
            this.paramLabelFreq.initV = this.eq.freqIndex;
            this.InitParamLabelQ();
            this.InitEQTypeButtonState();
            this.UpdateDisplay();
        }

        public void UpdateUIFromData()
        {
            if(this.eq == null)
            {
                return;
            }

            this.gainSlider.initV = this.eq.gainIndex;
            this.paramLabelGain.initV = this.eq.gainIndex;
            this.paramLabelFreq.initV = this.eq.freqIndex;
            this.paramLabelQ.initV = this.eq.qIndex;
            this.InitEQTypeButtonState();
            this.UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            this.Invalidate();
            this.Update();
        }

        private void InitParamLabelQ()
        {
            Debug.Assert(this.eq != null);

            this.paramLabelQ.Init(this.eq.maxQIndex, this.eq.qIndex, GetQLabel, CalcQIndexFromText);
        }

        private string GetGainLabel(short index)
        {
            return DataUtils.EQGain2String(index);
        }
        
        private short CalcGainIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, GainUnit);
            double value;
            if (double.TryParse(str, out value))
            {
                short index = DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.eqGainList);
                return index;
            }

            return -1;
        }

        private string GetFreqLabel(short index)
        {
            return DataUtils.EQFreq2String(index);
        }

        private short CalcFreqIndexFromText(string text)
        {
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
                short index = DataUtils.Freq2EQIndex(value * multiple);
                return index;
            }

            return -1;
        }

        private string GetQLabel(short index)
        {
            if(this.eq == null)
            {
                return string.Empty;
            }
            //AEC.Log.OutputLine("qindex = {0}, {1}", index, this.eq.QIndex2String(index));
            return this.eq.QIndex2String(index);
        }

        private short CalcQIndexFromText(string text)
        {
            if (this.eq == null)
            {
                return -1;
            }

            double value;
            if (double.TryParse(text, out value))
            {
                if(this.eq.type == EQType.Peaking)
                {
                    return DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.eqQList);
                }
                else
                {
                    return DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.eqSList);
                }
            }

            return -1;
        }

        #region 重写基类方法
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = DefaultClientSize;
        }

        //private int count = 0;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_isShowIndex)
            {
                this.PaintIndexLabel(e.Graphics);
            }
            //if (this.eq != null && this.eq.index == 0)
            //{
            //    AEC.Log.OutputLine("EQAdjuster OnPaint:{0},  {1}Hz", ++count, this.eq.freq);
            //}
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //AEC.Log.OutputLine("EQAdjuster OnMouseWheel");
            base.OnMouseWheel(e);

            if(!this._canBeChanged)
            {
                return;
            }

            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }

            if (e.Delta > 0)
            {
                this.UpOperation();
            }
            else
            {
                this.DownOperation();
            }
        }

        //protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        //{
        //    Console.WriteLine("EQAdjuster OnPreviewKeyDown:{0}", e.KeyCode);
        //    base.OnPreviewKeyDown(e);
        //}

        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    this.isActive = true;
        //    //this.gainSlider.Focus();
        //    //AEC.Log.OutputLine("EQAdjuster OnMouseDown()...{0}", ++count);
        //}

        //int count = 0;
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    //AEC.Log.OutputLine("EQAdjuster OnKeyDown()...{0}", ++count);
        //}

        

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (msg.Msg == Win32.WM_SYSKEYDOWN || msg.Msg == Win32.WM_KEYDOWN)
        //    {
        //        Keys key = (Keys)msg.WParam.ToInt32();
        //        Console.WriteLine("ProcessCmdKey:{0}", key);
        //        if (keyData == Keys.Up ||
        //            keyData == Keys.Down ||
        //            keyData == Keys.Left ||
        //            keyData == Keys.Right)
        //        {
        //            //Console.WriteLine("ProcessCmdKey");
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
        //            return true;
        //        }                
        //    }

        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        //protected override bool IsInputKey(Keys keyData)
        //{
        //    AEC.Log.OutputLine("EQAdjuster IsInputKey:{0}", keyData);
        //    return base.IsInputKey(keyData);
        //}

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //AEC.Log.OutputLine("EQAdjuster ProcessDialogKey:{0}", keyData);
            if (_canBeChanged && this.key2EventMap.ContainsKey(keyData))
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
        #endregion

        private void PaintIndexLabel(Graphics g)
        {
            if(this.indexBackground == null)
            {
                return;
            }

            Rectangle srcRect = new Rectangle(0, 0, this.indexBackground.Width, this.indexBackground.Height);
            Rectangle destRect = new Rectangle(eqIndexLeftTop, srcRect.Size);
            GDIHelper.QuickDrawImage(g, eqIndexLeftTop, this.indexBackground);

            //Rectangle client = this.ClientRectangle;
            //client.Width -= 1;
            //client.Height -= 1;
            //g.DrawRectangle(new Pen(Color.Green, 1), client);
            //g.FillRectangle(new SolidBrush(Color.Green), destRect);
            //Rectangle labelRect = new Rectangle(destRect)
            
            TextRenderer.DrawText(g, this.indexLabel, this.Font, destRect, this.labelIndexForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //StringFormat sf = new StringFormat();
            //sf.Alignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;
            //using (Brush brush = new SolidBrush(this.labelIndexForeColor))
            //{
            //    g.DrawString(this.indexLabel, this.Font, brush, destRect, sf);
            //}
            //sf.Dispose();
        }

        private bool _isActive = false;

        public EQTypeImage eqTypeImage
        {
            set
            {
                this.buttonP.backHover = value.peakingHover;
                this.buttonP.backChecked = value.peakingChecked;
                this.buttonLS.backHover = value.lowShelfHover;
                this.buttonLS.backChecked = value.lowShelfChecked;
                this.buttonHS.backHover = value.highShelfHover;
                this.buttonHS.backChecked = value.highShelfChecked;
            }
        }
        public Image eqIndexBackgroundActive { get; set; }
        public Color activeColor { get; set; }

        public bool isActiveInit
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if(this._isActive != value)
                {
                    this._isActive = value;
                    if (this._isActive)
                    {
                        //this.labelIndex.ForeColor = activeColor;
                        this.labelIndexForeColor = activeColor;
                        this.indexBackground = eqIndexBackgroundActive;
                    }
                    else
                    {
                        //this.labelIndex.ForeColor = Color.White;
                        this.labelIndexForeColor = Color.FromArgb(32, 32, 64);
                        this.indexBackground = eqIndexBackgroundNormal;
                    }
                    this.Invalidate(eqIndexRect);
                    this.Update();
                }
            }
        }

        public bool isActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if (this._isActive != value)
                {
                    this._isActive = value;
                    if (this._isActive)
                    {
                        this.labelIndexForeColor = activeColor;
                        this.indexBackground = eqIndexBackgroundActive;
                        OnActivated();
                    }
                    else
                    {
                        this.labelIndexForeColor = Color.White;

                        this.indexBackground = eqIndexBackgroundNormal;
                    }
                    this.Invalidate(eqIndexRect);
                }
            }
        }

        public bool canBeChanged
        {
            get
            {
                return _canBeChanged;
            }
            set
            {
                this._canBeChanged = value;
                this.paramLabelFreq.canBeChanged = this._canBeChanged;
                this.paramLabelGain.canBeChanged = this._canBeChanged;
                this.paramLabelQ.canBeChanged = this._canBeChanged;
                this.gainSlider.canBeChanged = this._canBeChanged;
                this.buttonP.canBeChanged = this._canBeChanged;
                this.buttonLS.canBeChanged = this._canBeChanged;
                this.buttonHS.canBeChanged = this._canBeChanged;
            }
        }

        private void InitEQTypeButtonState()
        {
            if(this.eq != null)
            {
                switch (this.eq.type)
                {
                    case EQType.Peaking:
                        buttonP.isCheckInit = true;
                        break;

                    case EQType.LowShelf:
                        buttonLS.isCheckInit = true;
                        break;

                    case EQType.HighShelf:
                        buttonHS.isCheckInit = true;
                        break;
                }
            }
        }

        private void UpOperation()
        {
            if(this.eq == null)
            {
                return;
            }

            if(this.eq.gainIndex < DataUtils.maxGainIndex)
            {
                this.eq.gainIndex += 1;
                this.gainSlider.initV = this.eq.gainIndex;
                this.paramLabelGain.initV = this.eq.gainIndex;
                this.OnEQChanged();
            }
        }

        private void DownOperation()
        {
            if (this.eq == null)
            {
                return;
            }

            if (this.eq.gainIndex > 0)
            {
                this.eq.gainIndex -= 1;
                this.gainSlider.initV = this.eq.gainIndex;
                this.paramLabelGain.initV = this.eq.gainIndex;
                this.OnEQChanged();
            }
        }

        private void LeftOperation()
        {
            if (this.eq == null)
            {
                return;
            }

            if (this.eq.freqIndex > 0)
            {
                this.eq.freqIndex -= 1;
                this.paramLabelFreq.initV = this.eq.freqIndex;
                this.OnEQChanged();
            }
        }

        private void RightOperation()
        {
            if (this.eq == null)
            {
                return;
            }

            if (this.eq.freqIndex < DataUtils.maxEQFreqIndex)
            {
                this.eq.freqIndex += 1;
                this.paramLabelFreq.initV = this.eq.freqIndex;
                this.OnEQChanged();
            }
        }

        private void HomeOperation()
        {
            if (this.eq == null)
            {
                return;
            }

            this.eq.Change2PrevMode();
            this.paramLabelQ.Init(this.eq.maxQIndex, this.eq.qIndex, GetQLabel, CalcQIndexFromText);
            this.InitEQTypeButtonState();
            this.OnEQChanged();
        }

        private void EndOperation()
        {
            if (this.eq == null)
            {
                return;
            }

            this.eq.Change2NextMode();
            this.paramLabelQ.Init(this.eq.maxQIndex, this.eq.qIndex, GetQLabel, CalcQIndexFromText);
            this.InitEQTypeButtonState();
            this.OnEQChanged();
        }

        private void AddOperation()
        {
            if (this.eq == null)
            {
                return;
            }

            if (this.eq.qIndex < this.eq.maxQIndex)
            {
                this.eq.qIndex += 1;
                this.paramLabelQ.initV = this.eq.qIndex;
                this.OnEQChanged();
            }
        }

        private void SubtractOperation()
        {
            if (this.eq == null)
            {
                return;
            }

            if (this.eq.qIndex > 0)
            {
                this.eq.qIndex -= 1;
                this.paramLabelQ.initV = this.eq.qIndex;
                this.OnEQChanged();
            }
        }


        private void OnEQChanged()
        {
            if (this.eq != null && EQChanged != null)
            {
                EQChanged(this.eq, EventArgs.Empty);
            }
        }

        private void OnActivated()
        {
            if (this.eq != null && Activated != null)
            {
                Activated(this.eq, EventArgs.Empty);
            }
        }

        private void EQAdjuster_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            this.isActive = true;
        }

        private void btn_CheckedChanged(object sender, EventArgs e)
        {
            if(this.eq == null || !this.canBeChanged)
            {
                return;
            }

            //AEC.Log.OutputLine("CheckedChanged");
            gtRadioButton rdo = sender as gtRadioButton;
            if(rdo.isChecked)
            {
                if(rdo.Equals(this.buttonP))
                {
                    this.eq.type = EQType.Peaking;
                }
                else if(rdo.Equals(this.buttonLS))
                {
                    this.eq.type = EQType.LowShelf;
                }
                else if (rdo.Equals(this.buttonHS))
                {
                    this.eq.type = EQType.HighShelf;
                }

                this.InitParamLabelQ();

                this.OnEQChanged();
            }
        }
    }

    public class EQTypeImage
    {
        public Image peakingHover;
        public Image peakingChecked;
        public Image lowShelfHover;
        public Image lowShelfChecked;
        public Image highShelfHover;
        public Image highShelfChecked;

        public EQTypeImage(
            Image peakingHover_,
            Image peakingChecked_,
            Image lowShelfHover_,
            Image lowShelfChecked_,
            Image highShelfHover_,
            Image highShelfChecked_)
        {
            this.peakingHover = peakingHover_;
            this.peakingChecked = peakingChecked_;
            this.lowShelfHover = lowShelfHover_;
            this.lowShelfChecked = lowShelfChecked_;
            this.highShelfHover = highShelfHover_;
            this.highShelfChecked = highShelfChecked_;
        }
    }
}
