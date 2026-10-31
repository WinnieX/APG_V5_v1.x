using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;
using AEC.Effectors.VX;
using AEC.UI.Skin;
using AEC.UI.Utility;

namespace AEC.UI
{
    public partial class FormModeOperation : Form
    {
        public FormModeOperation()
        {
            InitializeComponent();
            InitFont();
            this.title.BindingForm(this);
            this.deviceModePanel.cmbDeviceMode.DropDownBtnColor = SystemPanel.themeColor;
        }

        private void InitFont()
        {
            this.title.Font = FontHelper.CreateFont10();
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

        public void Binding(MController mController_, SystemModuleBase system_, EventBus eventBus_)
        {
            this.volMgtPanel.BindingSystemModule(system_, eventBus_);
            this.deviceModePanel.Binding(mController_, system_, eventBus_);
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            const int WM_KEYDOWN = 256;
            const int WM_SYSKEYDOWN = 260;
            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        this.Close();//esc关闭窗体
                        break;
                }
            }

            return false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            GDIHelper.FillRectangle(e.Graphics, this.ClientRectangle, SkinManager.CurrentSkin.proModeBackgroundColor);
            //Log.OutputLine("FromProMode OnPaint:{0}", ++count);
        }

        private void FormModeOperation_Load(object sender, EventArgs e)
        {
            UpdateDisplayLanguage();
        }

        private void FormModeOperation_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.volMgtPanel.Close();
            this.deviceModePanel.Close();
        }

        private Localization localization = Config.GetInstance().localization;
        private void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formModeQuickOperation];
            this.volMgtPanel.UpdateDisplayLanguage();
            this.deviceModePanel.UpdateDisplayLanguage();
        }
    }
}
