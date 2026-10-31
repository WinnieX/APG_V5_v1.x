using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI
{
    public partial class FormImportExternalFile : AEC.UI.MFormBase
    {
        public string fileName = string.Empty;
        public FormImportExternalFile(string fileName_)
        {
            InitializeComponent();

            this.txtName.Text = fileName_;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            bool nameIsValid = !string.IsNullOrEmpty(this.txtName.Text.Trim());
            this.btnOK.Enabled = nameIsValid;
            this.btnCancel.Enabled = nameIsValid;
        }

        public new string ShowDialog(IWin32Window owner)
        {
            base.ShowDialog(owner);

            return fileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.OKHandler();
        }

        private void OKHandler()
        {
            fileName = this.txtName.Text.Trim();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormImportExternalFile_Activated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                this.txtName.Focus();
                //Log.OutputLine("txtName focus()...");
            }
            else
            {
                this.btnOK.Focus();
                //Log.OutputLine("btnOK focus()...");
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                this.OKHandler();
            }
        }
    }
}
