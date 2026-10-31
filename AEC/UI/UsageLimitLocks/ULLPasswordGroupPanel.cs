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
    public partial class ULLPasswordGroupPanel : AEC.UI.gtUserControl
    {
        private readonly TextBox[] txtPasswords;

        public int passwordCount
        {
            get
            {
                return this.txtPasswords.Length;
            }
        }

        public ULLPasswordGroupPanel()
        {
            InitializeComponent();

            this.txtPasswords = new TextBox[]
            {
                this.txtPasswordLock1,
                this.txtPasswordLock2,
                this.txtPasswordLock3,
                this.txtPasswordLock4,
                this.txtPasswordLock5,
            };

            this.InitTheme();
        }

        private void InitTheme()
        {
            foreach (var txt in this.txtPasswords)
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

            for (int i = 0; i < this.txtPasswords.Length; ++i)
            {
                if(sender.Equals(this.txtPasswords[i]))
                {
                    if(this.txtPasswords[i].TextLength == this.txtPasswords[i].MaxLength)
                    {
                        int nextIndex = i < this.txtPasswords.Length - 1 ? i + 1 : 0;
                        this.txtPasswords[nextIndex].Focus();
                    }
                }
            }  
        }

        public void Clear()
        {
            foreach(var txt in this.txtPasswords)
            {
                txt.Clear();
            }
        }

        public void Random()
        {
            foreach (var txt in this.txtPasswords)
            {
                txt.Text = UsageLimitLockHelper.GetRandomPassword();
            }
        }

        public void CopyToClipboard()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < txtPasswords.Length; ++i)
            {
                sb.Append(txtPasswords[i].Text);
                if (i < txtPasswords.Length - 1)
                {
                    sb.AppendLine();
                }
            }

            Clipboard.SetDataObject(sb.ToString());
        }

        public string[] passwords
        {
            get
            {
                string[] passwords = new string[this.txtPasswords.Length];

                for (int i = 0; i < passwords.Length; ++i)
                {
                    if (this.txtPasswords[i].TextLength == UsageLimitLock.PasswordLength)
                    {
                        passwords[i] = this.txtPasswords[i].Text;
                    }
                    else
                    {
                        passwords[i] = string.Empty;
                        this.txtPasswords[i].Clear();
                    }
                }

                return passwords;
            }
            set
            {
                Debug.Assert(value != null && value.Length == this.txtPasswords.Length);
                for (int i = 0; i < value.Length; ++i)
                {
                    if (value[i].Length == UsageLimitLock.PasswordLength)
                    {
                        this.txtPasswords[i].Text = value[i];
                    }
                    else
                    {
                        this.txtPasswords[i].Clear();
                    }
                }
            }
        }

        public void UpdatePassword(int index, string password)
        {
            Debug.Assert(index >= 0 && index < this.txtPasswords.Length && !string.IsNullOrEmpty(password));
            if (password.Length == UsageLimitLock.PasswordLength)
            {
                this.txtPasswords[index].Text = password;
            }
            else
            {
                this.txtPasswords[index].Clear();
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
