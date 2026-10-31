using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI
{
    public partial class gtFlowLayoutPanel : FlowLayoutPanel
    {
        public gtFlowLayoutPanel()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.UpdateStyles();

            this.BackColor = Color.Transparent;

            //this.AutoScrollPosition
            //this.Scroll += gtFlowLayoutPanel_Scroll;
        }

        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    AEC.Log.OutputLine("{0}", e.Clicks);
        //}

        //private int count = 0;
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014)
        //    {
        //        AEC.Log.OutputLine("擦除背景");
        //        return;
        //    }
        //    AEC.Log.OutputLine("gtFlowLayoutPanel WndProc Msg:{0:X} [{1}]", m.Msg, ++count);
        //    base.WndProc(ref m);
        //}

        //private const int WS_VSCROLL = 0x00200000;
        //private const int WS_HSCROLL = 0x00100000;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        if (HScroll || HorizontalScroll.Visible)
        //        {
        //            cp.Style |= WS_HSCROLL;
        //        }
        //        else
        //        {
        //            cp.Style &= (~WS_HSCROLL);
        //        }
        //        if (VScroll || VerticalScroll.Visible)
        //        {
        //            cp.Style |= WS_VSCROLL;
        //        }
        //        else
        //        {
        //            cp.Style &= (~WS_VSCROLL);
        //        }
        //        return cp;
        //    }
        //}

        //private void gtFlowLayoutPanel_Scroll(object sender, ScrollEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    AEC.Log.OutputLine("{0}", this.AutoScrollPosition); 
        //}

        //protected override void OnScroll(ScrollEventArgs se)
        //{
        //    base.OnScroll(se);
        //    AEC.Log.OutputLine("{0}", se.NewValue); 
        //}
    }
}
