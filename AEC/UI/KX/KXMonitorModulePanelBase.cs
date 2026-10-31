using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Controls;
using AEC.Common;
using AEC.Effectors;

namespace AEC.UI.KX
{
    public partial class KXMonitorModulePanelBase : AEC.UI.Controls.MContainerControl
    {
        private const string eqLabel = "EQ";
        protected const string LimiterLabel = "Limiter";

        protected const int LayoutLevelTicksY = 96;
        protected const int LayoutLimiterY = 496;
        protected const int LayoutLimiterTicksY = 520;

        private DUILabel labelEQ;

        public event EventHandler<EventArgs> EQThumbnailMouseClick;

        public ModuleBase module
        {
            get;
            private set;
        }

        public MEffectorThreshold effectorThreshold
        {
            get;
            private set;
        }

        private int maxLevel { get; set; }

        public KXMonitorModulePanelBase()
        {
            InitializeComponent();
            this.Font = this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.BorderColor = Color.Black;
            this.BorderWidth = 2;
        }

        public virtual bool monitorIsON
        {
            set
            {

            }
        }

        protected override void OnInitDuiControls()
        {
            if (this.labelEQ == null)
            {
                this.labelEQ = new DUILabel();
                labelEQ.Location = new Point(0, 28);
                labelEQ.Size = new Size(this.Width, 16);
                labelEQ.Font = this.Font;
                labelEQ.Text = eqLabel;
                labelEQ.ForeColor = this.ForeColor;
                labelEQ.Alignment = ContentAlignment.MiddleCenter;
                labelEQ.ShowBackground = false;
                labelEQ.ShowBorder = false;
                labelEQ.BorderColor = Color.Transparent;
                labelEQ.FillColor = Color.Transparent;
                labelEQ.Visible = true;
            }

            base.AddDuiControl(this.labelEQ);
        }

        public virtual void DoLevelFreeFall()
        {

        }

        public virtual void UpdateUIFromData()
        {
            this.eqThumbnail.Invalidate();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.labelEQ != null)
            {
                this.labelEQ.Location = new Point((this.Width - this.labelEQ.Width) / 2, this.labelEQ.Location.Y);
            }

            this.eqThumbnail.Location = new Point((this.Width - this.eqThumbnail.Width) / 2, this.eqThumbnail.Location.Y);
        }

        public void BindingModule(ModuleBase module_, MEffectorThreshold effectorThreshold_)
        {
            this.module = module_;
            this.effectorThreshold = effectorThreshold_;

            this.maxLevel = this.effectorThreshold.maxLevel;

            this.eqThumbnail.BindingModule(module_);
        }

        private void eqThumbnail_MouseClick(object sender, MouseEventArgs e)
        {
            if(this.EQThumbnailMouseClick != null)
            {
                this.EQThumbnailMouseClick(this, EventArgs.Empty);
            }
        }

        public virtual void UpdateEffectorModel()
        {
            
        }
    }
}
