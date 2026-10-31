using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using AEC.Core;
using AEC.Comm;
using AEC.Common;
using AEC.Misc;

namespace AEC.UI
{
    public partial class FormComm : Form
    {
        private SynchronizationContext syncContext;
        private MController mController;
        //private CommService commSvc;
        //private EventBus commEvent;
        private EventBus mainEvent;
        private bool isShow = false;
        private bool evnetIsRegisted = false;
        private bool isNewLine = false;
        private CommProcess commProcess = CommProcess.ProModeConnectLoadData;
        private OperationMode operationMode = OperationMode.Professional;

        private Localization localization = Config.GetInstance().localization;

        private EffectorModelBase currEffectorModel
        {
            get
            {
                return this.mController.currEffector.effectorModel;
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

        public FormComm()
        {
            InitializeComponent();
            InitFont();
            this.DoubleBuffered = true;
            this.syncContext = WindowsFormsSynchronizationContext.Current;
            this.title.BindingForm(this);
            this.vScrollbar.Scroll += vScrollbar_Scroll;
            this.richTextBoxLog.VScroll += richTextBoxLog_VScroll;
        }

        private void InitFont()
        {
            this.title.Font = FontHelper.CreateFont10();
            this.circleProgress.Font = FontHelper.CreatePixelFont(36);
            this.richTextBoxLog.Font = FontHelper.CreateFont12();
            //this.lblDeviceIsLocked.Font = FontHelper.CreateFont10();

            //btnDisconnect
            this.lblDeviceNotActivated.Font = FontHelper.CreateFont10();

            this.lblDeviceIsLocked.Font = FontHelper.CreateFont10();
            this.lblPassword.Font = FontHelper.CreateFont10();
            this.btnConfirm.Font = FontHelper.CreateFont10();

            this.btnDisconnect.Font = FontHelper.CreateFont10();
            this.btnClose.Font = FontHelper.CreateFont10();
            //AEC.Log.OutputLine("FormComm title font:{0}", this.title.Font.FontFamily.Name);
            //this.title.FontChanged += title_FontChanged;
        }

        //private void title_FontChanged(object sender, EventArgs e)
        //{
        //    AEC.Log.OutputLine("FormComm title font:{0}", this.title.Font.FontFamily.Name);
        //}

        public void Binding(MController mController_, EventBus mainEvent)
        {
            System.Diagnostics.Debug.Assert(mController_ != null);
            this.mController = mController_;
            this.mainEvent = mainEvent;
            this.RegisterCommEventHandler();
        }

        private void UpdateTitle()
        {
            switch (commProcess)
            {
                case CommProcess.ProModeConnectLoadData:
                case CommProcess.EasyModeConnectLoadData:
                    this.Text = localization[Localization.formComm_ConnectAndLoadData];
                    break;

                case CommProcess.ProModeUploadData:
                case CommProcess.EasyModeUploadMultiModeData:
                    this.Text = localization[Localization.formComm_UploadData];
                    break;

                case CommProcess.ProModeCallDeviceMode:
                case CommProcess.ProModeSwitchDeviceWorkMode:
                case CommProcess.EasyModeCallDeviceMode:
                    this.Text = localization[Localization.formComm_CallModeData];
                    break;
            }
        }

        public void ShowCommInfo(IWin32Window owner, CommProcess commProcess_, OperationMode operationMode_)
        {
            this.Init();

            this.commProcess = commProcess_;

            this.UpdateTitle();

            this.operationMode = operationMode_;

            if (!isShow)
            {
                this.Show(owner);
                isShow = true;
            }
            else
            {
                this.Visible = true;
            }
        }

        private void SwitchToDeviceNotActivated()
        {
            //this.Text = localization[Localization.formComm_DeviceIsNotActivated];
            this.btnClose.Visible = false;
            this.btnDisconnect.Visible = true;
            this.panelCommProgress.Visible = false;
            this.panelDeviceIsLocked.Visible = false;
            this.panelDeviceNotActivated.Visible = true;
            this.btnDisconnect.Focus();
        }

        public void ShowDeviceNotActivated()
        {
            SwitchToDeviceNotActivated();
            this.Visible = true;
        }

        private void SwitchToDeviceLocked(OperationMode operationMode_)
        {
            //this.Text = localization[Localization.formComm_DeviceIsLocked];
            this.operationMode = operationMode_;
            this.btnClose.Visible = false;
            this.btnDisconnect.Visible = true;
            this.panelCommProgress.Visible = false;
            this.panelDeviceNotActivated.Visible = false;
            this.panelDeviceIsLocked.Visible = true;
            this.txtPassword.Focus();
            this.txtPassword.Clear();
            if (operationMode == OperationMode.Professional)
            {
                this.lblDeviceIsLocked.Text = string.Format(localization[Localization.formComm_PleaseInputPasswordToUnlockSystem], SystemModuleBase.KeyLockPasswordLength);
                this.txtPassword.MaxLength = SystemModuleBase.KeyLockPasswordLength;
                //设备已被锁定，请输入4位密码解锁!
            }
            else if (operationMode == OperationMode.Easy)
            {
                this.lblDeviceIsLocked.Text = string.Format(localization[Localization.formComm_InputAdminPassword], SystemModuleBase.SystemPasswordLength);
                this.txtPassword.MaxLength = SystemModuleBase.SystemPasswordLength;
            }
        }

        public void ShowDeviceKeyboradIsLocked(OperationMode operationMode_)
        {
            SwitchToDeviceLocked(operationMode_);
            this.Visible = true;
        }

        private void Init()
        {
            this.circleProgress.Init();
            //this.btnClose.Enabled = true;
            this.btnClose.Enabled = false;
            this.btnClose.Visible = true;
            this.btnDisconnect.Visible = false;
            this.panelCommProgress.Visible = true;
            this.panelDeviceNotActivated.Visible = false;
            this.panelDeviceIsLocked.Visible = false;
            this.richTextBoxLog.Clear();
            this.txtPassword.Clear();
            this.isNewLine = false;
            this.UpdateNewVScrollbar();
            //AEC.Log.OutputLine("this.btnClose.Enabled:{0}", this.btnClose.Enabled);
        }

        private void FormComm_Load(object sender, EventArgs e)
        {
            UpdateDisplayLanguage();
            this.localization.langEventBus.RegisterHandler(LangEventBusHandler);
        }

        private void FormComm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveCommEventHandler();
            this.localization.langEventBus.RemoveHandler(LangEventBusHandler);
        }

        private void btn_click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnClose))
            {
                this.HideForm();
            }
            else if(sender.Equals(this.btnDisconnect))
            {
                //this.commSvc.Disconnect();
                this.HideForm();
                this.mainEvent.NotifyObeserver(new DisconnectArgs());
            }
            else if(sender.Equals(this.btnConfirm))
            {
                this.VerifySystemPassword();
            }
        }

        private void CommEventHandler(object sender, EventBusArgs e)
        {
            //AEC.Log.OutputLine("通讯事件{0}", e.type);
            syncContext.Post(ProcessCommEvent, e);
        }

        private void ProcessCommEvent(object state)
        {
            if (!this.Visible)
            {
                return;
            }

            EventBusArgs e = state as EventBusArgs;
            if (e.type == EventType.CommProgressUpdated)
            {
                CommProgressUpdatedArgs args = e as CommProgressUpdatedArgs;
                //AEC.Log.OutputLine(args.msg);
                if (args.progressIncrement > 0)
                {
                    this.circleProgress.Append(args.progressIncrement);
                }
                this.AppendOutputInfoList(args);
            }
            else if (e.type == EventType.CommCompleted)
            {
                CommResult commResult = (e as CommCompletedArgs).commResult;
                if (this.mController.isConnected)
                {
                    if (commResult.process == CommProcess.ProModeConnectLoadData || commProcess == CommProcess.ProModeSwitchDeviceWorkMode)
                    {
                        if (!this.currEffectorModel.deviceIsActivated)
                        {
                            this.SwitchToDeviceNotActivated();
                            return;
                        }
                        else if(this.operationMode == OperationMode.Professional)
                        {
                            if (this.currEffectorModel.deviceKeyIsLocked)
                            {
                                this.SwitchToDeviceLocked(OperationMode.Professional);
                                return;
                            }
                            else
                            {
                                this.HideForm();
                            }
                        }
                        else if(this.operationMode == OperationMode.Easy)
                        {
                            if (this.currEffectorModel.isLockUserOperation)
                            {
                                this.SwitchToDeviceLocked(OperationMode.Easy);
                                return;
                            }
                            else
                            {
                                this.HideForm();
                            }
                        }
                        else
                        {
                            this.HideForm();
                        }

                        //else if (this.currEffector.deviceKeyIsLocked)
                        //{
                        //    this.ShowDeviceKeyboradIsLocked();
                        //    return;
                        //}
                        //else
                        //{
                        //    this.HideForm();
                        //}
                    }
                    else
                    {
                        this.HideForm();
                    }
                }
                else
                {
                    this.btnClose.Enabled = true;
                }
            }
            else if(e.type == EventType.CommInterrupted)
            {
                this.HideForm();
            }
            else if (e.type == EventType.ConnectionIsDisconnected)
            {
                this.HideForm();
            }
        }

        public void HideForm()
        {
            this.Visible = false;
            this.richTextBoxLog.Clear();
            GC.Collect();
        }

        public void CloseForm()
        {

        }

        
        private void AppendOutputInfoList(CommProgressUpdatedArgs args)
        {
            //AEC.Log.OutputLine("Msg:{0}, isFailed:{1}, isEnd:{2}", args.msg, args.isFailed, args.isEnd);
            if(args.msg.Length > 0)
            {
                AppendTextColorful(this.richTextBoxLog, args.msg, args.isFailed ? Color.Red : Color.FromArgb(32,32,32), isNewLine);
                this.UpdateNewVScrollbar();
                isNewLine = args.isEnd;
                //this.richTextBoxLog.
            }
            //this.richTextBoxLog.ScrollToCaret();
            //this.Activate();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (this.operationMode == OperationMode.Professional)
            {
                this.btnConfirm.Enabled = this.txtPassword.TextLength == SystemModuleBase.KeyLockPasswordLength;
            }
            else
            {
                this.btnConfirm.Enabled = this.txtPassword.TextLength == SystemModuleBase.SystemPasswordLength;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (this.operationMode == OperationMode.Professional)
            {
                if (this.txtPassword.TextLength != SystemModuleBase.KeyLockPasswordLength)
                {
                    return;
                }
            }
            else if (this.operationMode == OperationMode.Easy)
            {
                if (this.txtPassword.TextLength != SystemModuleBase.SystemPasswordLength)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            this.VerifySystemPassword();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == System.Convert.ToChar((int)Keys.Enter))
            {
                e.Handled = true;
            }
        }

        private void VerifySystemPassword()
        {
            if (this.operationMode == OperationMode.Professional)
            {
                if (this.txtPassword.Text == this.currEffectorModel.system.keyboardLockPassword)
                {
                    this.txtPassword.Clear();
                    //this.currEffector.deviceKeyIsLocked = false;
                    this.mainEvent.NotifyObeserver(new UnlockKeyLockArgs());
                    this.HideForm();
                    return;
                }
            }
            else if(this.operationMode == OperationMode.Easy)
            {
                if (this.txtPassword.Text == this.currEffectorModel.system.systemPassword)
                {
                    this.txtPassword.Clear();
                    //this.currEffector.isLockUserOperation = false;
                    this.currEffectorModel.system.currUser = User.Admin;
                    this.mainEvent.NotifyObeserver(new UnlockUserPermissionsArgs());
                    this.HideForm();
                    return;
                }
            }

            this.txtPassword.Clear();
            gtMessageBox.ShowMessage(this, localization[Localization.formComm_InputPasswordError], localization[Localization.formComm_Error], MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        public void RegisterCommEventHandler()
        {
            if (this.mController != null)
            {
                this.mController.controllerEvent.RegisterHandler(CommEventHandler);
                this.evnetIsRegisted = true;
            }
        }

        public void RemoveCommEventHandler()
        {
            if (this.mController != null && this.evnetIsRegisted)
            {
                this.mController.controllerEvent.RemoveHandler(CommEventHandler);
            }
        }

        public static void AppendTextColorful(RichTextBox rtBox, string text, Color color, bool isNewLine = true)
        {
            //Log.OutputLine("AppendTextColorful, thread id = {0}", Thread.CurrentThread.ManagedThreadId);

            if (isNewLine)
            {
                text = Environment.NewLine + text;
            }
            rtBox.SelectionStart = rtBox.TextLength;
            rtBox.SelectionLength = 0;
            rtBox.SelectionColor = color;
            rtBox.AppendText(text);
            rtBox.SelectionColor = rtBox.ForeColor;
        }

        private void richTextBoxLog_VScroll(object sender, EventArgs e)
        {
            UpdateNewVScrollbar();
        }

        private void vScrollbar_Scroll(object sender, EventArgs e)
        {
            UpdateOwnHScrollbar();
        }

        //private int count = 0;
        private void UpdateNewVScrollbar()
        {
            this.vScrollbar.Visible = Win32API.IsVerticalScrollBarVisible(this.richTextBoxLog);
            if(this.vScrollbar.Visible)
            {
                Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
                si.cbSize = (uint)Marshal.SizeOf(si);
                si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
                Win32API.GetScrollInfo(this.richTextBoxLog.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref si);
                //AEC.Log.OutputLine("SCROLLINFO: min = {0}, max = {1}, page = {2}, pos = {3}, trackPos = {4}, [{5}]", si.nMin, si.nMax, si.nPage, si.nPos, si.nTrackPos, ++count);
                this.vScrollbar.Maximum = si.nMax + 1;
                this.vScrollbar.LargeChange = (int)si.nPage;
                this.vScrollbar.Value = si.nPos;
            }
        }

        private void UpdateOwnHScrollbar()
        {
            if (!this.vScrollbar.Visible)
            {
                return;
            }

            Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
            Win32API.GetScrollInfo(this.richTextBoxLog.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref si);
            si.nPos = this.vScrollbar.Value;
            si.nTrackPos = this.vScrollbar.Value;
            Win32API.SetScrollInfo(this.richTextBoxLog.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref si, true);
            Win32API.PostMessage(richTextBoxLog.Handle, 0x115, 4 + 0x10000 * this.vScrollbar.Value, 0);
        }

        private void LangEventBusHandler(object sender, EventBusArgs e)
        {
            UpdateDisplayLanguage();
        }

        private void UpdateDisplayLanguage()
        {
            this.UpdateTitle();
            this.btnClose.Text = localization[Localization.formComm_Close];
            this.btnDisconnect.Text = localization[Localization.formComm_Disconnect];
            this.lblDeviceNotActivated.Text = localization[Localization.formComm_PleaseActivateTheDeviceFirst];
            this.lblPassword.Text = localization[Localization.formComm_Password];
            this.btnConfirm.Text = localization[Localization.formComm_Ok];
            this.panelDeviceNotActivated.Invalidate();
            this.panelDeviceIsLocked.Invalidate();
            this.panelBottom.Invalidate();
        }
    }

    public enum OperationMode
    {
        Professional = 0,
        Easy,
    }
}
