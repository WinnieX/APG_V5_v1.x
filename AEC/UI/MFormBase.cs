using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using AEC.Misc;

namespace AEC.UI
{
    public partial class MFormBase : Form
    {
        private Color _borderColor = Color.FromArgb(64, 64, 64);

        protected readonly SynchronizationContext syncContext;

        protected static Localization localization = Config.GetInstance().localization;

        public MFormBase()
        {
            InitializeComponent();
            this.title.BindingForm(this);
            this.InitStyle();
            this.InitFont();
            this.syncContext = WindowsFormsSynchronizationContext.Current;
        }

        private void InitFont()
        {
            this.title.Font = FontHelper.CreateFont10();
        }

        private void InitStyle()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserMouse, true);
            this.SetStyle(ControlStyles.CacheText, true);
            //this.SetStyle(ControlStyles.Selectable, true);

            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景

            this.DoubleBuffered = true;

            this.UpdateStyles();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    cp.ExStyle |= 0x02000000;//用双缓冲绘制窗口的所有子控件
                    return cp;
                }

                return cp;
            }
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.title.Text = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                this.OnLanguageChanged(false);
                //this.OnUpdateFontSizeWithDPIScaling();
                this.OnRegisterEventHandler();
                this.OnLoadHandler();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!DesignMode)
            {
                this.OnClosingHandler(e);
            }

            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (!DesignMode)
            {
                this.OnRemoveEventHandler();
                this.OnClosedHandler();
            }

            base.OnClosed(e);
        }

        protected virtual void OnLoadHandler()
        {
            this.UpdateDisplayLanguage();
        }

        protected virtual void OnClosingHandler(CancelEventArgs e)
        {

        }

        protected virtual void OnClosedHandler()
        {

        }

        protected virtual void OnLanguageChanged(bool isUpdateUI = true)
        {

        }

        protected virtual void OnRegisterEventHandler()
        {
            
        }

        protected virtual void OnRemoveEventHandler()
        {
            
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.title.Width != this.Width)
            {
                this.title.Width = this.Width;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            this.PaintBack(e.Graphics);

            //mControl.Log.OutputLine("MFormBase.OnPaint()...{0}", ++paintCount);
        }

        private void PaintBack(Graphics g)
        {
            this.PaintBorder(g);
            //this.PaintModel(g);
        }

        protected virtual void PaintBorder(Graphics g)
        {
            UI.Utility.GDIHelper.DrawBorder(g, this, this._borderColor, 1.6f);
        }

        protected virtual void UpdateDisplayLanguage()
        {
            
        }
    }
}
