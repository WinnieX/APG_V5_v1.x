using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Common;
using AEC.UI.Skin;

namespace AEC.UI.UsageLimitLocks
{
    public partial class ULLUsageCountGroupPanel : AEC.UI.gtUserControl
    {
        private readonly TextBox[] txtUsageCountGroup;
        public ULLUsageCountGroupPanel()
        {
            InitializeComponent();

            this.txtUsageCountGroup = new TextBox[]
            {
                this.txtUsageCountLock1,
                this.txtUsageCountLock2,
                this.txtUsageCountLock3,
                this.txtUsageCountLock4,
                this.txtUsageCountLock5,
            };

            this.InitTheme();
        }

        private void InitTheme()
        {
            foreach (var txt in this.txtUsageCountGroup)
            {
                txt.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
                txt.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            }

            this.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.BackColor = SkinManager.CurrentSkin.PanelBackColor;
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Log.OutputLine("key press:{0}", e.KeyChar.ToString());
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            //Log.OutputLine("key down:{0}", e.KeyCode.ToString());

            TextBox txt = sender as TextBox;

            if(e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    txt.Copy();
                }
                else if(e.KeyCode == Keys.V)
                {
                    txt.Paste();
                }
                else if (e.KeyCode == Keys.X)
                {
                    txt.Cut();
                }
            }

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            for (int i = 0; i < this.txtUsageCountGroup.Length; ++i)
            {
                if(sender.Equals(this.txtUsageCountGroup[i]))
                {
                    int nextIndex = i < this.txtUsageCountGroup.Length - 1 ? i + 1 : 0;
                    this.txtUsageCountGroup[nextIndex].Focus();
                }
            }  
        }

        public void Clear()
        {
            foreach(var txt in this.txtUsageCountGroup)
            {
                txt.Clear();
            }
        }

        public ushort[] usageCounts
        {
            get
            {
                ushort[] usageCounts = new ushort[this.txtUsageCountGroup.Length];

                for (int i = 0; i < usageCounts.Length; ++i)
                {
                    if (!ushort.TryParse(this.txtUsageCountGroup[i].Text, out usageCounts[i]))
                    {
                        this.txtUsageCountGroup[i].Clear();
                    }
                }

                return usageCounts;
            }
            set
            {
                Debug.Assert(value != null && value.Length == this.txtUsageCountGroup.Length);
                for (int i = 0; i < value.Length; ++i)
                {
                    this.txtUsageCountGroup[i].Text = value[i].ToString();
                }
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string digitStr = UsageLimitLockHelper.FilterDigitChar(txt.Text);
            if (digitStr != txt.Text)
            {
                txt.Text = digitStr;
            }
        }
    }
}
