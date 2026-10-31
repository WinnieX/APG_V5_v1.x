using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AEC.Common;
using AEC.UI.Skin;
using AEC.UI.Utility;

namespace AEC.UI.UsageLimitLocks
{
    public partial class FormUsageLimitLockDynamicPassword : AEC.UI.MFormBase
    {
        public FormUsageLimitLockDynamicPassword()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GDIHelper.FillRectangle(e.Graphics, this.ClientRectangle, SkinManager.CurrentSkin.proModeBackgroundColor);

            int borderWidth = 1;
            using (Pen border = new Pen(Color.FromArgb(96, 96, 96), borderWidth))
            {
                Point[] points = new Point[]
                {
                    new Point(borderWidth - 1, borderWidth - 1),
                    new Point(borderWidth - 1, this.Height - (borderWidth)),
                    new Point(this.Width - (borderWidth), this.Height - (borderWidth)),
                    new Point(this.Width - (borderWidth), borderWidth - 1),
                };
                e.Graphics.DrawLines(border, points);
            }
        }

        protected override void OnLoadHandler()
        {
            base.OnLoadHandler();
        }

        protected override void OnClosedHandler()
        {
            base.OnClosedHandler();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.txtDeviceID))
            {
                this.DeviceIdChangedHandler();
            }
            else if(sender.Equals(this.txtRandomKey))
            {
                this.RandomKeyChangedHandler();
            }
        }

        private void DeviceIdChangedHandler()
        {
            string errorMsg = string.Empty;

            byte[] deviceId = GetDeviceIdFromUI();
            if(deviceId != null)
            {
                uint timeStamp = Tools.TimeStampHelper.GetTimeStampWithoutSecond(DateTime.UtcNow);

                UpdateTxtGroup(this.panelPasswordGroup1, deviceId, timeStamp);

                this.linkRefresh.Enabled = true;
                this.linkCopyPassword1.Enabled = true;
                this.linkExportPassword1.Enabled = true;

                uint key = 0;
                if (GetRandomKey(out key))
                {
                    UpdateTxtGroup(this.panelPasswordGroup2, deviceId, key);
                    this.linkCopyPassword2.Enabled = true;
                    this.linkExportPassword2.Enabled = true;
                }
                else
                {
                    this.panelPasswordGroup2.Clear();
                    this.linkCopyPassword2.Enabled = false;
                    this.linkExportPassword2.Enabled = false;
                }

                return;
            }

            this.panelPasswordGroup1.Clear();
            this.panelPasswordGroup2.Clear();

            this.linkRefresh.Enabled = false;
            this.linkCopyPassword1.Enabled = false;
            this.linkExportPassword1.Enabled = false;
            this.linkCopyPassword2.Enabled = false;
            this.linkExportPassword2.Enabled = false;

        }

        private void UpdateTxtGroup(ULLPasswordGroupPanel panelPassword, byte[] deviceId, uint key)
        {
            Debug.Assert(panelPassword != null);

            for (int i = 0; i < panelPassword.passwordCount; ++i)
            {
                panelPassword.UpdatePassword(i, AEC.Common.UsageLimitLockHelper.GetSuperPassword(deviceId, (uint)i, key));
            }
        }


        private void RandomKeyChangedHandler()
        {
            uint key = 0;
            if (GetRandomKey(out key))
            {
                byte[] deviceId = GetDeviceIdFromUI();
                if (deviceId != null)
                {
                    UpdateTxtGroup(this.panelPasswordGroup2, deviceId, key);
                }

                this.linkCopyPassword2.Enabled = true;
                this.linkExportPassword2.Enabled = true;

                return;
            }

            this.panelPasswordGroup2.Clear();
            this.linkCopyPassword2.Enabled = false;
            this.linkExportPassword2.Enabled = false;
        }

        private bool GetRandomKey(out uint key)
        {
            string errorMsg = string.Empty;

            key = 0;

            if (this.txtRandomKey.TextLength == sizeof(uint) * 2)
            {
                byte[] randomKey = Comm.Utils.HexStringToBytes(this.txtRandomKey.Text, out errorMsg);
                if (randomKey != null && randomKey.Length == sizeof(uint))
                {
                    key = BitConverter.ToUInt32(randomKey, 0);

                    return true;
                }
            }

            return false;
        }
        
        private byte[] GetDeviceIdFromUI()
        {
            string errorMsg = string.Empty;

            if (this.txtDeviceID.TextLength == AEC.Common.SystemModuleBase.DeviceIdLength * 2)
            {
                byte[] deviceId = Comm.Utils.HexStringToBytes(this.txtDeviceID.Text, out errorMsg);
                if (deviceId != null && deviceId.Length == AEC.Common.SystemModuleBase.DeviceIdLength)
                {
                    return deviceId;
                }
            }

            return null;
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(sender.Equals(this.linkRefresh))
            {
                this.DeviceIdChangedHandler();
            }
            else if (sender.Equals(this.linkCopyPassword1))
            {
                this.CopyPasswordGroup1();
            }
            else if (sender.Equals(this.linkExportPassword1))
            {
                this.ExportPassword(this.panelPasswordGroup1, Tools.StringHelper.DateTime2String(DateTime.Now));
            }
            else if (sender.Equals(this.linkCopyPassword2))
            {
                this.CopyPasswordGroup2();
            }
            else if (sender.Equals(this.linkExportPassword2))
            {
                this.ExportPassword(this.panelPasswordGroup2, this.txtRandomKey.Text);
            }
        }

        private void ExportPassword(ULLPasswordGroupPanel panelPasswordGroup, string key)
        {
            Debug.Assert(panelPasswordGroup != null);

            ULLPasswordGroup pg = new ULLPasswordGroup(panelPasswordGroup.passwords);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ULLPasswordGroupSeralizer.DEF_MLP_FILE_FILTER;
            sfd.RestoreDirectory = true;
            sfd.FileName = string.Format("{0}_{1}", this.txtDeviceID.Text, key);

            try
            {
                sfd.InitialDirectory = ULLPasswordGroupSeralizer.InitialDirectory;
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    ULLPasswordGroupSeralizer.SeralizeToFile(pg, sfd.FileName);                    
                    Tools.FileHelper.OpenFileLocation(sfd.FileName);
                }
            }
            catch (Exception e)
            {
                gtMessageBox.ShowMessage(this, e.Message, "Error");
            }
        }

        private void CopyPasswordGroup1()
        {
            this.panelPasswordGroup1.CopyToClipboard();
        }

        private void CopyPasswordGroup2()
        {
            this.panelPasswordGroup2.CopyToClipboard();
        }

        private void FormUsageLimitLockDynamicPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) != 0)
            {
                this.Paste();
            }
        }

        private const string GetDeviceIdRegexPattern = "(?<=Device\\s+ID\\s+:\\s+)(.+)(?=\\r\\n)";
        private const string GetRandKeyRegexPattern = "(?<=Rand\\s+Key\\s+:\\s+)(.+)(?=\\r\\n)";

        private void Paste()
        {
            string text = Clipboard.GetText();
            if(string.IsNullOrEmpty(text))
            {
                return;
            }

            string deviceId = GetFilterText(text, GetDeviceIdRegexPattern);
            if(!string.IsNullOrEmpty(deviceId) && deviceId.Length == this.txtDeviceID.MaxLength)
            {
                this.txtDeviceID.Text = deviceId;
            }

            string randKey = GetFilterText(text, GetRandKeyRegexPattern);
            if (!string.IsNullOrEmpty(randKey) && randKey.Length == this.txtRandomKey.MaxLength)
            {
                this.txtRandomKey.Text = randKey;
            }
        }

        public static string GetFilterText(string text, string pattern)
        {
            try
            {
                Regex regex = new Regex(pattern);
                Match match = regex.Match(text);

                if (match.Success && match.Groups.Count >= 1)
                {
                    return match.Groups[0].Value;
                }
            }
            catch(Exception)
            {

            }

            return string.Empty;
        }
    }
}
