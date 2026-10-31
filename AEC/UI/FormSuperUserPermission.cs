using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Tools;

namespace AEC.UI
{
    public partial class FormSuperUserPermission : Form
    {
        private const string mengAudio = "M-Audio";

        private MController mController = null;
        public FormSuperUserPermission(MController mController_)
        {
            InitializeComponent();
            this.title.BindingForm(this);
            this.mController = mController_;
            this.Text = "超级用户权限";
            this.txtPermissionCode.Enabled = !this.mController.isSuperUserMode;
            if (this.mController.isSuperUserMode)
            {
                this.txtRequestCode.Enabled = false;
                //this.txtPermissionCode.Enabled = false;
                this.btnPermission.Text = "取消授权";
            }
            else
            {
                this.btnPermission.Text = "请求授权";
                this.btnPermission.Enabled = false;
                this.txtRequestCode.Text = GenMachineCode();
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

        public new bool ShowDialog()
        {
            base.ShowDialog();
            return this.mController.isSuperUserMode;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnPermission))
            {
                if (this.mController.isSuperUserMode)
                {
                    this.mController.isSuperUserMode = false;
                    this.Close();
                }
                else
                {
                    if(CheckPermissionCode())
                    {
                        this.mController.isSuperUserMode = true;
                        this.Close();
                    }
                    else
                    {
                        gtMessageBox.ShowMessage(this, "授权码验证错误!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if(sender.Equals(this.btnClose))
            {
                this.Close();
            }
        }

        public bool CheckPermissionCode()
        {
            string requestCode = this.txtRequestCode.Text;
            if(string.IsNullOrEmpty(requestCode))
            {
                return false;
            }

            string permissionCode = AEC.Core.Utils.ByteArrayToHexStr(HashHelper.MD5(Encoding.UTF8.GetBytes(mengAudio + requestCode)), string.Empty);

            return this.txtPermissionCode.Text == permissionCode;
        }

        public static string GenMachineCode()
        {
            List<byte> datas = new List<byte>(MachineIDHelper.MachineCode);
            long now = DateTime.Now.ToBinary();
            datas.AddRange(BitConverter.GetBytes(now));

            return AEC.Core.Utils.ByteArrayToHexStr(HashHelper.MD5(datas.ToArray()), string.Empty);
        }

        private void txtPermissionCode_TextChanged(object sender, EventArgs e)
        {
            if (!this.mController.isSuperUserMode)
            {
                this.btnPermission.Enabled = this.txtPermissionCode.TextLength == this.txtRequestCode.TextLength;
            }
        }
    }
}
