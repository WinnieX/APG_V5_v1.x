using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AEC.Core;
using AEC.Effectors.VX;
using AEC.Tools;
using AEC.Misc;
using AEC.UI.RemoteControl;
using AEC.UI.Skin;
using AEC.UI.Utility;

namespace AEC.UI
{
    public partial class FormMain : Form
    {
        private SynchronizationContext syncContext;

        private readonly MController mController;
        private FormProMode formProMode = null;

        private QRCodePanel qrCodePanel = null;

        private FormControlTheOtherParty formControlTheOtherParty = null;

        private Localization localization = Config.GetInstance().localization;

        private const int toolStripItemPaddingTop = 4;
        private const int toolStripItemPaddingLeft = 8;

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

        public FormMain(MController mController_)
        {
            InitializeComponent();
            this.InitFont();
            this.syncContext = WindowsFormsSynchronizationContext.Current;

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, false);

            this.title.BindingForm(this);
            this.mController = mController_;
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont14();
            this.title.Font = FontHelper.CreateFont10();
            this.btnProMode.Font = FontHelper.CreateFont14();
            this.btnRemoteControl.Font = FontHelper.CreateFont14();
            this.btnVideoTutorial.Font = FontHelper.CreateFont14();
            //AEC.Log.OutputLine("FormMain title font:{0}", this.title.Font.FontFamily.Name);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //this.RegisterCommEventHandler();
            //this.AsyncConnectDevice();
            //Application.Idle += Application_Idle;
            this.Text = ProductsConfig.AppName + " " + VersionHelper.GetVersion();
            this.UpdateDisplayLanguage();
            this.localization.langEventBus.RegisterHandler(LangEventBusHandler);
            this.mController.rcEvent.RegisterHandler(RCEventBusHandler);
        }

        //private void Application_Idle(object sender, EventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    //Log.OutputLine("Application_Idle");
        //    //System.Threading.Thread.Sleep();
        //    Log.OutputLine("Application_Idle 0");
        //    if (this.formProMode == null || this.formProMode.IsDisposed)
        //    {
        //        this.formProMode = new FormProMode(this.v5Controller);
        //        this.formProMode.VisibleChanged += form_VisibleChanged;
        //    }
        //    Application.Idle -= Application_Idle;
        //    Log.OutputLine("Application_Idle 1");
        //}

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Tools.LogHelper.WriteInfo("FormMain_FormClosing");
            if (this.mController.commState == Comm.CommState.Connecting)
            {
                gtMessageBox.ShowMessage(this, localization[Localization.systemIsBusyAndTryAgain]);
                e.Cancel = true;
            }

            //this.RemoveCommEventHandler();
            if (formProMode != null)
            {
                formProMode.Close();
            }            
            if(formControlTheOtherParty != null)
            {
                formControlTheOtherParty.Close();
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.localization.langEventBus.RemoveHandler(LangEventBusHandler);
        }

        //private void AsyncConnectDevice()
        //{
        //    syncContext.Post(StartConnectDevice, null);
        //}

        //public void StartConnectDevice(object state)
        //{
        //    if (this.v5Controller.commSvc.AsyncConnectAndLoadAllData())
        //    {
        //        this.panelBack.Enabled = false;
        //    }
        //}

        //private void RegisterCommEventHandler()
        //{
        //    this.v5Controller.commEvent.RegisterHandler(CommEventHandler);
        //}

        //private void RemoveCommEventHandler()
        //{
        //    this.v5Controller.commEvent.RemoveHandler(CommEventHandler);
        //}

        //private void CommEventHandler(object sender, EventBusArgs e)
        //{
        //    syncContext.Post(ProcessCommEvent, e);
        //}

        //private void ProcessCommEvent(object state)
        //{
        //    EventBusArgs e = state as EventBusArgs;
        //    if (e.type == EventType.CommStateChanged)
        //    {
        //        //AEC.Log.OutputLine("Main Form CommEventHandler state:{0}", this.v5Controller.commSvc.commState);
        //        this.panelBack.Enabled = this.v5Controller.commSvc.commState != Comm.CommState.Connecting;
        //    }
        //}

        private void btn_click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnProMode))
            {
                this.OpenProMode();
            }
            else if (sender.Equals(this.btnRemoteControl))
            {
                this.OpenRemoteControl();
            }
            else if(sender.Equals(this.btnVideoTutorial))
            {
                this.OpenVideoTutorial();
            }
        }

        private void OpenEasyMode()
        {
            //return;

            //if (this.formEasyMode == null || this.formEasyMode.IsDisposed)
            //{
            //    this.formEasyMode = new FormEasyMode(this.v5Controller, this.mController);
            //    this.formEasyMode.VisibleChanged += form_VisibleChanged;
            //}

            //this.formEasyMode.Show();
        }

        private void OpenProMode()
        {
            if(this.formProMode == null || this.formProMode.IsDisposed)
            {
                this.formProMode = new FormProMode(this.mController);
                this.formProMode.VisibleChanged += form_VisibleChanged;
            }
            
            this.formProMode.Show();
        }

        private void form_VisibleChanged(object sender, EventArgs e)
        {
            Form form = sender as Form;
            this.Visible = !form.Visible;
        }

        private void OpenRemoteControl()
        {
            gtMessageBox.ShowMessage(this, "此功能正在开发过程中，敬请期待！\r\n This function is in the process of development, please look forward to it !", ProductsConfig.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            return;

            //if (ActiveControlTheOtherPartyFrom())
            //{
            //    return;
            //}

            //int toolStripItemWidth = this.Width - toolStripItemPaddingLeft * 2;
            //Point position = new Point(toolStripItemPaddingLeft, this.btnRemoteControl.Location.Y + this.btnRemoteControl.Height + toolStripItemPaddingTop);
            //ToolStripDropDown toolStripDropDown = new ToolStripDropDown();
            //toolStripDropDown.Items.Add(CreateToolStripItem(localization[Localization.formMain_ControlTheOtherParty], toolStripItemWidth, ShowRemoteControlTheOtherPartyForm));
            //toolStripDropDown.Items.Add(CreateToolStripItem(localization[Localization.formMain_ControlledRemotelyByTheOtherParty], toolStripItemWidth, ShowControlledRemotelyByTheOtherParty));
            //toolStripDropDown.Show(this, position);
        }

        private void OpenVideoTutorial()
        {
            ToolStripDropDown toolStripDropDown = new ToolStripDropDown();
            toolStripDropDown.Items.Add(CreateToolStripItem(localization[Localization.formMain_OpenVideoTutorial], this.btnVideoTutorial.Width, OpenLink));
            toolStripDropDown.Items.Add(CreateToolStripItem(localization[Localization.formMain_ShowTutorialQRCode], this.btnVideoTutorial.Width, ShowQRCode));
            toolStripDropDown.Show(this.btnVideoTutorial, new Point(0, this.btnVideoTutorial.Height + toolStripItemPaddingTop));
        }

        private ToolStripItem CreateToolStripItem(string text, int width, EventHandler click)
        {
            ToolStripItem item = new ToolStripMenuItem();
            item.AutoSize = false;
            item.Height = 48;
            item.Width = width;
            item.Text = text;
            item.BackColor = Color.White;
            item.ForeColor = Color.Black;
            item.Font = this.Font;
            item.Click += click;
            return item;
        }

        private bool ActiveControlTheOtherPartyFrom()
        {
            //如果远程控制窗口未关闭，则激活窗口
            if (formControlTheOtherParty != null && !formControlTheOtherParty.IsDisposed)
            {
                formControlTheOtherParty.Activate();

                return true;
            }

            return false;
        }

        private void ShowRemoteControlTheOtherPartyForm(object sender, EventArgs e)
        {
            if (ActiveControlTheOtherPartyFrom())
            {
                return;
            }

            formControlTheOtherParty = new FormControlTheOtherParty(this.mController.rcClientEngine.localClient);
            formControlTheOtherParty.Show();
        }

        private void ShowControlledRemotelyByTheOtherParty(object sender, EventArgs e)
        {
            FormControlledRemotelyByTheOtherParty formControlledRemotelyByTheOtherParty = new FormControlledRemotelyByTheOtherParty(this.mController.rcClientEngine.remoteClient);
            formControlledRemotelyByTheOtherParty.ShowDialog();
        }

        private const string url = "https://www.meng-audio.com/video.html";
        private void OpenLink(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(url);
        }
        
        private void ShowQRCode(object sender, EventArgs e)
        {
            if (this.qrCodePanel == null)
            {
                this.qrCodePanel = new QRCodePanel();
                this.qrCodePanel.Text = localization[Localization.formMain_ScanQRCode];
                this.qrCodePanel.QRCodeImage = global::AEC.Properties.Resources.QRCodeVideoTutorial;
                this.qrCodePanel.Location = new Point(
                    (this.Width - this.qrCodePanel.Width) / 2 ,
                    this.title.Height
                    );
                this.qrCodePanel.PanelClick += qrCodePanel_PanelClick;
                this.Controls.Add(this.qrCodePanel);
                this.qrCodePanel.BringToFront();
            }
            else
            {
                this.qrCodePanel.Text = localization[Localization.formMain_ScanQRCode];
                this.qrCodePanel.Visible = true;
            }
        }

        private void qrCodePanel_PanelClick(object sender, EventArgs e)
        {
            if (this.qrCodePanel != null)
            {
                this.qrCodePanel.Visible = false;
            }
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            Win32.ReleaseCapture();
            Win32API.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MOVE + Win32.HTCAPTION, 0);

            if (this.qrCodePanel != null)
            {
                this.qrCodePanel.Visible = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            GDIHelper.FillRectangle(e.Graphics, this.ClientRectangle, SkinManager.CurrentSkin.proModeBackgroundColor);
            //GDIHelper.QuickDrawImage(e.Graphics, Point.Empty, global::AEC.Properties.Resources.main_form_back);
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
            //using (Pen border = new Pen(Color.FromArgb(128, 128, 128), borderWidth))
            //{
            //    Point[] points = new Point[]
            //    {
            //        new Point(1, 1),
            //        new Point(1, this.Height - 2),
            //        new Point(this.Width - 2, this.Height - 2),
            //        new Point(this.Width - 2, 1),
            //    };
            //    e.Graphics.DrawLines(border, points);
            //}
        }

        private void LangEventBusHandler(object sender, EventBusArgs e)
        {
            UpdateDisplayLanguage();
        }

        private void RCEventBusHandler(object sender, EventBusArgs e)
        {
            if(e.type == EventType.LCNotConnectedToRC)
            {
                if(this.formControlTheOtherParty != null && !this.formControlTheOtherParty.IsDisposed)
                {
                    this.formControlTheOtherParty.Activate();
                }
            }
        }

        private void UpdateDisplayLanguage()
        {
            this.btnProMode.Text = localization[Localization.formProMode];
            this.btnRemoteControl.Text = localization[Localization.formMain_RemoteControl];
            this.btnVideoTutorial.Text = localization[Localization.formMain_VideoTutorial];
            //this.Text = V5Controller.model + " AEC " + localization[Localization.formProMode_ProMode] + " " + VersionHelper.GetVersion();
            //Console.WriteLine(this.Text);
        }
    }
}
