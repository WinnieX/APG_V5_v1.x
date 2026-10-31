using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.Misc;

namespace AEC.UI
{
    public partial class EQResetAndByPassPanel : UserControl
    {
        //private bool _isPass = false;
        private Color eqPassColor = Color.FromArgb(32, 32, 64);

        public event EventHandler<EventArgs> ResetEQ;
        public event EventHandler<EventArgs> EQPassChanged;

        public EQResetAndByPassPanel()
        {
            InitializeComponent();
            InitFont();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.UpdateStyles();
            this.btnEQReset.Click += btnEQReset_Click;
            this.chkEQPass.CheckedChanged += chkEQPass_CheckedChanged;
        }

        private void InitFont()
        {
            this.btnEQReset.Font = FontHelper.CreateFont10();
            this.chkEQPass.Font = FontHelper.CreateFont10();
        }

        public void Init(bool isPass_, Color eqPassColor_)
        {
            this.chkEQPass.isChecked = isPass_;
            this.eqPassColor = eqPassColor_;
            this.UpdateEQPassForeColor();
        }

        public bool isPass
        {
            get
            {
                return this.chkEQPass.isChecked;
            }
            set
            {
                if(this.chkEQPass.isChecked != value)
                {
                    this.chkEQPass.isChecked = value;
                    UpdateEQPassForeColor();
                }
            }
        }

        private void UpdateEQPassForeColor()
        {
            if (this.chkEQPass.isChecked)
            {
                this.chkEQPass.ForeColor = eqPassColor;
            }
            else
            {
                this.chkEQPass.ForeColor = Color.FromArgb(32, 32, 64);
            }
        }

        private void chkEQPass_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateEQPassForeColor();
            if(EQPassChanged != null)
            {
                EQPassChanged(this, EventArgs.Empty);
            }
        }

        private delegate void DelVoidCallback();
        private void btnEQReset_Click(object sender, EventArgs e)
        {
            //this.AskWhetherResetEQ();
            DelVoidCallback callback = new DelVoidCallback(AskWhetherResetEQ);
            this.BeginInvoke(callback);
        }

        private void AskWhetherResetEQ()
        {
            if (DialogResult.OK == gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.eqReset_Tips], "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                if (this.ResetEQ != null)
                {
                    this.ResetEQ(this, EventArgs.Empty);
                }
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.btnEQReset.Text = localization[Localization.eqReset];
        }
    }
}
