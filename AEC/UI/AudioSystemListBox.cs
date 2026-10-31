using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using AEC.AudioProducts;
using AEC.Misc;

namespace AEC.UI
{
    public partial class AudioSystemListBox : ListBox
    {
        private AudioSystemItem mouseItem;
        public AudioSystemItem selectedItem = null;
        private ListBoxItemCollection m_items;
        private float _lsWeight = 0.36f;
        private float _paWeight = 0.36f;
        private Color _lsBack = Color.FromArgb(70, 193, 203);
        private Color _paBack = Color.FromArgb(113, 219, 174);
        private Color _micBack = Color.FromArgb(113, 179, 161);
        private int _itemHeight = 68;
        private int _textPaddingLeft = 64;
        private SynchronizationContext syncContext;

        public event EventHandler selectedItemChanged;

        private Localization localization = Config.GetInstance().localization;

        public float itemHeight
        {
            get
            {
                return _itemHeight;
            }
        }
        public float lsWeight
        {
            get
            {
                return _lsWeight;
            }
        }
        public float paWeight
        {
            get
            {
                return _paWeight;
            }
        }

        private Color lsBack
        {
            get
            {
                return _lsBack;
            }
            set
            {
                _lsBack = value;
            }
        }

        private Color paBack
        {
            get
            {
                return _paBack;
            }
            set
            {
                _paBack = value;
            }
        }

        private Color micBack
        {
            get
            {
                return _micBack;
            }
            set
            {
                _micBack = value;
            }
        }

        public new ListBoxItemCollection Items
        {
            get
            {
                return m_items;
            }
        }

        internal ListBox.ObjectCollection OldItemSource
        {
            get
            {
                return base.Items;
            }
        }

        public void Init(AudioSystemItem[] items)
        {
            System.Diagnostics.Debug.Assert(items != null);

            this.Items.Clear();

            this.mouseItem = null;
            this.BeginUpdate();
            this.Items.AddRange(items);
            this.EndUpdate();
            if(this.selectedItem != null)
            {
                bool isMatch = false;
                foreach(AudioSystemItem item in items)
                {
                    if (item.getAudioSystem.Equals(this.selectedItem.getAudioSystem))
                    {
                        this.selectedItem = item;
                        isMatch = true;
                        break;
                    }
                }

                if(!isMatch)
                {
                    this.selectedItem = null;
                    SelectedItemChangedHandler();
                    return;
                }
            }
        }

        public void Clear()
        {
            if(this.selectedItem != null)
            {
                this.selectedItem = null;
                SelectedItemChangedHandler();
            }
        }

        public AudioSystemListBox()
        {
            InitializeComponent();

            m_items = new ListBoxItemCollection(this);

            base.DrawMode = DrawMode.OwnerDrawVariable;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲   
            this.SetStyle(ControlStyles.ResizeRedraw, true); // 调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景. 
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true); // 开启控件透明

            this.syncContext = WindowsFormsSynchronizationContext.Current;
            //this.scr
            this.MouseWheel += AudioSystemListBox_MouseWheel;
            //this.MouseWheel
        }

        private void AudioSystemListBox_MouseWheel(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();

            //Win32API.SCROLLINFO scrollInfo_ = scrollInfo;
            //AEC.Log.OutputLine("AudioSystemListBox MouseWheel");
            syncContext.Post(CommEventHandlerCallback, null);
        }

        //private void MouseWheelEventHandler()
        //{
            
        //}

        private void CommEventHandlerCallback(object state)
        {
            //Control.MousePosition
            //AEC.Log.OutputLine("AEC Log:{0}", PointToClient(Control.MousePosition));
            //Win32API.SCROLLINFO scrollInfo_ = scrollInfo;
            //AEC.Log.OutputLine("SCROLLINFO: min = {0}, max = {1}, page = {2}, pos = {3}, trackPos = {4}, [{5}]", scrollInfo_.nMin, scrollInfo_.nMax, scrollInfo_.nPage, scrollInfo_.nPos, scrollInfo_.nTrackPos, ++count);
            UpdateMouseItem(PointToClient(Control.MousePosition));
        }

        //protected override void OnMouseWheel(MouseEventArgs e)
        //{
        //    //base.OnMouseWheel(e);
        //    AEC.Log.OutputLine("AudioSystemListBox OnMouseWheel");
        //    Win32API.SCROLLINFO scrollInfo_ = scrollInfo;
        //    AEC.Log.OutputLine("SCROLLINFO: min = {0}, max = {1}, page = {2}, pos = {3}, trackPos = {4}, [{5}]", scrollInfo_.nMin, scrollInfo_.nMax, scrollInfo_.nPage, scrollInfo_.nPos, scrollInfo_.nTrackPos, ++count);
        //    //UpdateMouseItem(Control.MousePosition);
        //}

        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_HSCROLL = 0x0114;
        public const int WM_VSCROLL = 0x0115;
        //private int count = 0;

        //protected override void WndProc(ref Message m)
        //{
        //    //AEC.Log.OutputLine("Wnd Proc:{0}, [{1}]", m.Msg.ToString("X"), ++count);
        //    //if (m.Msg == WM_MOUSEWHEEL)
        //    //{
        //    //    IntPtr wParam = m.WParam;
        //    //    Int16 low = (Int16)(wParam.ToInt64() & 0xffff);
        //    //    Int16 high = (Int16)((wParam.ToInt64() >> 16) & 0xffff);
        //    //    high = (Int16)(high / 3);
        //    //    long newParam = high << 16 + low;
        //    //    m.WParam = new IntPtr(newParam);
        //    //    //long param = wParam.ToInt64() >> 16;
        //    //    //Int16 t = (Int16)param;
        //    //    Int16 delta = (Int16)((high) & 0xffff);
        //    //    AEC.Log.OutputLine("WM_MOUSEWHEEL, wParam:{0}, lParam:{1}, delta = {2}", m.WParam, m.LParam, delta);
        //    //    //return;
        //    //}
        //    //else if (m.Msg == WM_VSCROLL)
        //    //{
        //    //    AEC.Log.OutputLine("WM_MOUSEWHEEL");
        //    //    return;
        //    //}

        //    if (m.Msg == WM_VSCROLL || m.Msg == WM_HSCROLL)
        //    {
        //        AEC.Log.OutputLine("WM_MOUSEWHEEL");
        //        return;
        //    }

        //    base.WndProc(ref m);
        //}

        //protected override void OnMouseDoubleClick(MouseEventArgs e)
        //{
        //    base.OnMouseDoubleClick(e);
        //    Win32API.SCROLLINFO scrollInfo_ = scrollInfo;
        //    if(scrollInfo_.nPos < scrollInfo_.nMax)
        //    {
        //        scrollInfo_.nPos += 1;
        //    }

        //    Win32API.SetScrollInfo(this.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref scrollInfo_, true);
        //    Win32API.PostMessage(this.Handle, Win32API.WM_VSCROLL, Win32API.MakeLong((short)Win32API.SB_THUMBTRACK, (short)(scrollInfo_.nPos)), 0);
        //    //AEC.Log.OutputLine("SCROLLINFO: min = {0}, max = {1}, page = {2}, pos = {3}, trackPos = {4}", scrollInfo_.nMin, scrollInfo_.nMax, scrollInfo_.nPage, scrollInfo_.nPos, scrollInfo_.nTrackPos);
        //}

        //public Win32API.SCROLLINFO scrollInfo
        //{
        //    get
        //    {

        //        Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
        //        si.cbSize = (uint)Marshal.SizeOf(si);
        //        si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
        //        Win32API.GetScrollInfo(this.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref si);
        //        return si;
        //    }
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.PaintListBox(pe.Graphics);
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (Items.Count > 0)
            {
                AudioSystemItem item = Items[e.Index];
                e.ItemHeight = _itemHeight;
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.UpdateMouseItem(e.Location);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.mouseItem = null;
            this.Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if(mouseItem != null)
            {
                selectedItem = mouseItem;
                this.Invalidate();
                this.SelectedItemChangedHandler();
            }
        }

        private void SelectedItemChangedHandler()
        {
            if (selectedItemChanged != null)
            {
                selectedItemChanged(this, EventArgs.Empty);
            }
        }

        private void PaintListBox(Graphics g)
        {
            SolidBrush lsBrush = new SolidBrush(this._lsBack);
            SolidBrush paBrush = new SolidBrush(this._paBack);
            SolidBrush micBrush = new SolidBrush(this._micBack);
            for (int i = 0; i < Items.Count; ++i)
            {
                Rectangle bounds = this.GetItemRectangle(i);

                if(bounds.Bottom < 0 || bounds.Top > this.Height)
                {
                    continue;
                }

                //AEC.Log.OutputLine("rect {0}: {1}", i, bounds);
                int top = bounds.Top + 1;
                int bottom = bounds.Bottom - 1;
                int lsWidth = (int)(this.Width * _lsWeight);
                int paWidth = (int)(this.Width * _paWeight);
                int micWidth = bounds.Width - lsWidth - paWidth;
                Rectangle lsRect = new Rectangle(0, top, lsWidth, bottom - top);
                Rectangle paRect = new Rectangle(lsWidth, top, paWidth, bottom - top);
                Rectangle micRect = new Rectangle(lsWidth + paWidth, top, paWidth, bottom - top);

                g.FillRectangle(lsBrush, lsRect);
                g.FillRectangle(paBrush, paRect);
                g.FillRectangle(micBrush, micRect);

                if(selectedItem != null && Items[i] == selectedItem)
                {
                    Color selectedColor = Color.FromArgb(192, Color.LightSkyBlue);
                    using (Brush brush = new SolidBrush(selectedColor))
                    {
                        g.FillRectangle(brush, new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height));
                        //g.DrawRectangle(pen, new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height));
                    }
                }
                else
                {
                    if (mouseItem == Items[i])
                    {
                        Color leftColor = Color.FromArgb(192, 192, 224, 248);
                        using (SolidBrush brush = new SolidBrush(leftColor))
                        {
                            g.FillRectangle(brush, new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height));
                        }
                    }
                }

                AudioSystem audioSystem = Items[i].getAudioSystem.GetAudioSystem();

                Rectangle indexRect = new Rectangle(new Point(0, top), new Size(_textPaddingLeft, bounds.Height));
                
                string indexLabel = (i + 1).ToString();
                if(Items[i].getAudioSystem.GetType() == typeof(AEC.AudioProducts.BuildInAudioSystem))
                {
                    indexLabel += System.Environment.NewLine + localization[Localization.panelSystemSelection_BuildIn];//"内置";
                }
                else
                {
                    indexLabel += System.Environment.NewLine + localization[Localization.panelSystemSelection_Customer];//"外部";
                }

                TextRenderer.DrawText(g, indexLabel, this.Font, indexRect, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                Rectangle rect = Rectangle.FromLTRB(lsRect.Left + _textPaddingLeft, lsRect.Top, lsRect.Right, lsRect.Bottom);
                TextRenderer.DrawText(g, audioSystem.ls.label, this.Font, rect, this.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                rect = Rectangle.FromLTRB(paRect.Left + _textPaddingLeft, paRect.Top, paRect.Right, paRect.Bottom);
                TextRenderer.DrawText(g, audioSystem.pa.label, this.Font, rect, this.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                rect = Rectangle.FromLTRB(micRect.Left + _textPaddingLeft, micRect.Top, micRect.Right, micRect.Bottom);
                TextRenderer.DrawText(g, audioSystem.mic.label, this.Font, rect, this.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
            lsBrush.Dispose();
            paBrush.Dispose();
            micBrush.Dispose();
        }

        private void UpdateMouseItem(Point mousePosition)
        {
            bool isContains = false;
            for (int i = 0; i < Items.Count; i++)
            {
                Rectangle bounds = this.GetItemRectangle(i);
                //Rectangle deleteBounds = new Rectangle(bounds.Width - 28, (bounds.Height - 16) / 2 + bounds.Top, 16, 16);

                if (bounds.Contains(mousePosition))
                {
                    //AEC.Log.OutputLine("Contains...");
                    isContains = true;
                    if (mouseItem != Items[i])
                    {
                        mouseItem = Items[i];
                        this.Invalidate();
                    }
                    break;
                }
                //else
                //{
                //    Items[i].isFocus = false;
                //}
            }

            if(!isContains)
            {
                if(mouseItem != null)
                {
                    mouseItem = null;
                    this.Invalidate();
                }
            }
        }

        //private int count = 0;
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014)
        //    {
        //        //AEC.Log.OutputLine("擦除背景");
        //        return;
        //    }
        //    AEC.Log.OutputLine("AudioSystemListBox WndProc Msg:{0:X} [{1}]", m.Msg, ++count);
        //    base.WndProc(ref m);
        //}
    }

    public class AudioSystemItem
    {
        public IGetAudioSystem getAudioSystem;

        //public bool isFocus {get;set; }

        public AudioSystemItem(IGetAudioSystem getAudioSystem_)
        {
            this.getAudioSystem = getAudioSystem_;
        }
    }

    
    [System.ComponentModel.ListBindable(false)]
    public class ListBoxItemCollection:IList, ICollection, IEnumerable
    {
        private AudioSystemListBox owner;

        public ListBoxItemCollection(AudioSystemListBox owner_)
        {
            this.owner = owner_;
        }

        internal AudioSystemListBox Owner
        {
            get
            {
                return this.owner;
            }
        }

        #region override
        public AudioSystemItem this[int index]
        {
            get
            {
                return owner.OldItemSource[index] as AudioSystemItem;
            }
            set
            {
                owner.OldItemSource[index] = value;
            }
        }
        public int Count
        {
            get { return Owner.OldItemSource.Count; }
        }

        public bool IsReadOnly
        {
            get { return Owner.OldItemSource.IsReadOnly; }
        }

        public int Add(AudioSystemItem item)
        {
            if (item == null)
            {
                throw new ArgumentException("item is null");
            }
            return Owner.OldItemSource.Add(item);
        }

        public void AddRange(AudioSystemItem[] items)
        {
            owner.OldItemSource.AddRange(items);
        }

        public void Clear()
        {
            if (Owner.OldItemSource.Count > 0)
            {
                owner.OldItemSource.Clear();
            }
        }

        public bool Contains(AudioSystemItem item)
        {
            bool rst = false;
            foreach (AudioSystemItem oldItem in owner.OldItemSource)
            {
                if (oldItem.getAudioSystem.Equals(item.getAudioSystem))
                {
                    rst = true;
                    break;
                }
            }
            return rst;
        }

        public void CopyTo(AudioSystemItem[] destination, int arrayIndex)
        {
            owner.OldItemSource.CopyTo(destination, arrayIndex);
        }

        public int IndexOf(AudioSystemItem item)
        {
            return owner.OldItemSource.IndexOf(item);
        }

        public void Insert(int index, AudioSystemItem item)
        {
            if (item == null)
            {
                throw new ArgumentException("item is null");
            }
            owner.OldItemSource.Insert(index, item);
        }

        public void Remove(AudioSystemItem item)
        {
            owner.OldItemSource.Remove(item);
        }

        public void RemoveAt(int index)
        {
            owner.OldItemSource.RemoveAt(index);
        }

        public IEnumerator GetEnumerator()
        {
            return owner.OldItemSource.GetEnumerator();
        }

        int IList.Add(object value)
        {
            if (!(value is AudioSystemItem))
            {
                throw new ArgumentException();
            }
            return Add(value as AudioSystemItem);
        }

        void IList.Clear()
        {
            Clear();
        }


        bool IList.Contains(object value)
        {
            return Contains(value as AudioSystemItem);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf(value as AudioSystemItem);
        }

        void IList.Insert(int index, object value)
        {
            if (!(value is AudioSystemItem))
            {
                throw new ArgumentException();
            }
            Insert(index, value as AudioSystemItem);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return IsReadOnly; }
        }

        void IList.Remove(object value)
        {
            Remove(value as AudioSystemItem);
        }

        void IList.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set
            {
                if (!(value is AudioSystemItem))
                {
                    throw new ArgumentException();
                }
                this[index] = value as AudioSystemItem;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo((AudioSystemItem[])array, index);
        }

        int ICollection.Count
        {
            get { return Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return false; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
