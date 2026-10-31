using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;

namespace AEC.UI
{
    [ToolboxItem(true), DefaultEvent("ValueChaged")]
    public partial class MicParam : UserControl
    {
        private ShortParam param;
        public event EventHandler ValueChaged;
        public MicParam()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.paramLabel.ValueChanged += MicParam_ValueChanged;
            this.btnLeft.MouseClick += btn_MouseClick;
            this.btnRight.MouseClick += btn_MouseClick;
            //this.btnLeft.MouseDoubleClick += btn_MouseDoubleClick;
            //this.btnRight.MouseDoubleClick += btn_MouseDoubleClick;
            this.MouseWheel += MicParam_MouseWheel;
        }

        private void MicParam_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta > 0)
            {
                this.Increase();
            }
            else
            {
                this.Decrease();
            }
        }

        private void btn_MouseClick(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnLeft))
            {
                this.Decrease();
            }
            else if (sender.Equals(this.btnRight))
            {
                this.Increase();
            }
        }

        //private void btn_MouseDoubleClick(object sender, EventArgs e)
        //{
        //    if (sender.Equals(this.btnLeft))
        //    {
        //        this.Decrease();
        //    }
        //    else if (sender.Equals(this.btnRight))
        //    {
        //        this.Increase();
        //    }
        //}

        private void OnValueChanged()
        {
            if(ValueChaged != null)
            {
                ValueChaged(this, EventArgs.Empty);
            }
        }

        private void MicParam_ValueChanged(object sender, EventArgs e)
        {
            this.param.v = this.paramLabel.v;
            this.btnLeft.Enabled = this.param.v > 0;
            this.btnRight.Enabled = this.param.v < this.param.max;
            this.OnValueChanged();
        }

        public void Binding(ShortParam param_, GetLabelDelegate getLabel_)
        {
            this.param = param_;
            this.paramLabel.Init(this.param.max, param.v, getLabel_, null);
            this.btnLeft.Enabled = this.param.v > 0;
            this.btnRight.Enabled = this.param.v < this.param.max;
        }

        public void UpdateUIFromData()
        {
            if(param == null)
            {
                return;
            }

            this.paramLabel.initV = param.v;
            this.btnLeft.Enabled = this.paramLabel.v > 0;
            this.btnRight.Enabled = this.paramLabel.v < this.param.max;
        }

        private void Increase()
        {
            if (this.param.v < this.param.max)
            {
                this.param.v += 1;
                this.paramLabel.initV = this.param.v;
                this.btnLeft.Enabled = this.param.v > 0;
                this.btnRight.Enabled = this.param.v < this.param.max;
                this.OnValueChanged();
            }
        }

        private void Decrease()
        {
            if (this.param.v > 0)
            {
                this.param.v -= 1;
                this.paramLabel.initV = this.param.v;
                this.btnLeft.Enabled = this.param.v > 0;
                this.btnRight.Enabled = this.param.v < this.param.max;
                this.OnValueChanged();
            }
        }
    }
}
