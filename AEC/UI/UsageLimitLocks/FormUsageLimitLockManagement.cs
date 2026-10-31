using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using AEC.Common;
using AEC.Core;
using AEC.UI.Skin;
using AEC.UI.Utility;
using AEC.Misc;

namespace AEC.UI.UsageLimitLocks
{
    public partial class FormUsageLimitLockManagement : AEC.UI.MFormBase
    {
        private const string HidePasswordStr = "●●●●●●";
        private MController mController;
        private SystemModuleBase system;
        private EventBus mainEventBus;
        private EventBus controllerEvent;

        private readonly UsageLimitLockPanel[] usageLimitLockPanels = new UsageLimitLockPanel[SystemModuleBase.DefUsageLimitLockCount];

        private uint randKey = 0;
        
        private bool deviceNameVisible
        {
            set
            {
                this.lblDeviceName.Visible = value;
                this.txtDeviceName.Visible = value;
            }
        }

        private void InitTheme()
        {
            this.lblDeviceID.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblDeviceName.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblRandKey.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblTaskFile.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.txtDeviceID.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtDeviceID.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            this.txtDeviceName.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtDeviceName.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            this.txtRandKey.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtRandKey.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            this.txtTaskFilePath.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtTaskFilePath.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            this.txtOutput.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtOutput.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            foreach(var panel in usageLimitLockPanels)
            {
                panel.InitTheme();
            }
        }

        public FormUsageLimitLockManagement()
        {
            InitializeComponent();

            usageLimitLockPanels = new UsageLimitLockPanel[SystemModuleBase.DefUsageLimitLockCount]
            {
                this.panelUsageLimitLock1,
                this.panelUsageLimitLock2,
                this.panelUsageLimitLock3,
                this.panelUsageLimitLock4,
                this.panelUsageLimitLock5,
            };

            this.InitTheme();
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
            this.InitRandKeyUI();
            this.mainEventBus.NotifyObeserver(new UsageLimitLockFormShowArgs(true));
        }

        protected override void OnClosedHandler()
        {
            if (mController != null && mController.isConnected)
            {
                if (system.isUsageLimitLock)
                {
                    this.mainEventBus.NotifyObeserver(new DisconnectArgs());
                }
                else
                {
                    this.mainEventBus.NotifyObeserver(new UnlockUsageLimitArgs());
                }
            }

            this.mainEventBus.NotifyObeserver(new UsageLimitLockFormShowArgs(false));

            this.RemoveEventBus();
            base.OnClosedHandler();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
 	        base.OnKeyDown(e);
            if (e.Control && e.Alt)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if(ActivationCode.CheckLicense() != ActivationCode.MLicenseType.Unauthorized)
                    {
                        this.ShowFormUsageLimitLockDynamicPassword();
                    }
                }
            }
        }

        private UsageLimitLocks.FormUsageLimitLockDynamicPassword formUsageLimitLockDynamicPassword = null;

        private void ShowFormUsageLimitLockDynamicPassword()
        {
            if (this.formUsageLimitLockDynamicPassword == null || this.formUsageLimitLockDynamicPassword.IsDisposed)
            {
                this.formUsageLimitLockDynamicPassword = new UsageLimitLocks.FormUsageLimitLockDynamicPassword();
                this.formUsageLimitLockDynamicPassword.Owner = this;
            }
            else
            {
                this.formUsageLimitLockDynamicPassword.Activate();
            }

            this.formUsageLimitLockDynamicPassword.Show();
        }
        
        private void InitRandKeyUI()
        {
            byte[] data = Tools.RandNumberHelper.GetRandNumbers(sizeof(uint));
            this.randKey = BitConverter.ToUInt32(data, 0);
            this.txtRandKey.Text = Core.Utils.BytesToHexStr(data);

            for (short i = 0; i < this.usageLimitLockPanels.Length; ++i)
            {
                this.usageLimitLockPanels[i].randKey = this.randKey;
            }
        }

        public void Binding(MController mController_, SystemModuleBase system_, EventBus eventBus_)
        {
            Debug.Assert(mController_ != null && system_ != null && eventBus_ != null);

            this.mController = mController_;
            this.system = system_;

            //this.RemoveEventBus();
            this.mainEventBus = eventBus_;
            this.controllerEvent = this.mController.controllerEvent;
            this.RegisterEventBus();

            for (short i = 0; i < this.usageLimitLockPanels.Length; ++i)
            {
                this.usageLimitLockPanels[i].Binding(this.system, i);
            }

            this.btnForgotPassword.Visible = mController.isConnected;
            this.btnSetting.Enabled = mController.isConnected && System.IO.File.Exists(this.txtTaskFilePath.Text.Trim());

            this.InitUI();
        }

        public void UpdateUIFromData(SystemModuleBase system_)
        {
            this.system = system_;

            for (short i = 0; i < this.usageLimitLockPanels.Length; ++i)
            {
                this.usageLimitLockPanels[i].UpdateUIFromData(this.system);
            }

            this.InitUI();
        }

        private void RegisterEventBus()
        {
            if (this.mainEventBus != null)
            {
                this.mainEventBus.RegisterHandler(EventBusHandler);
            }

            if (this.controllerEvent != null)
            {
                this.controllerEvent.RegisterHandler(CommStateChanged);
            }
        }

        private void RemoveEventBus()
        {
            if (this.mainEventBus != null)
            {
                this.mainEventBus.RemoveHandler(EventBusHandler);
            }

            if (this.controllerEvent != null)
            {
                this.controllerEvent.RemoveHandler(CommStateChanged);
            }
        }

        public void EventBusHandler(object sender, EventBusArgs e)
        {
            switch (e.type)
            {
                case EventType.SystemUserChanged:
                    this.UserChangedHandler();
                    break;
            }
        }

        private void UserChangedHandler()
        {
            if (system.isSupportDeviceName)
            {
                this.deviceName = system.deviceName;
                this.txtDeviceName.canEditable = !system.isLockUserOperation;
            }
        }

        private void CommStateChanged(object sender, EventBusArgs e)
        {
            CommEventHandler(e);
        }

        private void CommEventHandler(EventBusArgs e)
        {
            syncContext.Post(ProcessCommEvent, e);
        }

        private void ProcessCommEvent(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if (e.type == EventType.CommStateChanged)
            {
                //AEC.Log.OutputLine("联机状态发生变化:{0}", this.commService.commState);
                //this.btnCallMode.Enabled = this.mController.commState != CommState.Connecting;
                //this.btnSaveMode.Enabled = this.mController.commState != CommState.Connecting;

                //if (this.mController.commState == CommState.Connected)
                //{
                //    this.InitUI();
                //    this.InitLockButton();
                //}
                //else
                //{
                //    this.btnLock.Visible = false;
                //}

                this.btnForgotPassword.Visible = mController.isConnected;
                this.btnSetting.Enabled = mController.isConnected && System.IO.File.Exists(this.txtTaskFilePath.Text.Trim());

                //if (mController.commState == CommState.Disconnected)
                //{
                //    this.CommIdleHandler();
                //}
            }
            else if (e.type == EventType.UpdateUsageLimitLockCompleted)
            {
                UpdateUsageLimitLockCompletedArgs args = e as UpdateUsageLimitLockCompletedArgs;
                Debug.Assert(args != null);
                if (args.usageLimitLock.index < this.usageLimitLockPanels.Length)
                {
                    this.usageLimitLockPanels[args.usageLimitLock.index].UpdateCurrData(args.usageLimitLock);
                }

                this.OutputMsg(localization[Localization.usageLimitLock_LockXIsUpdated], args.usageLimitLock.index + 1, GetUsageLimitLockDescription(args.usageLimitLock));
            }
        }

        private void UpdateText()
        {
            if (this.system != null)
            {
                if (this.system.isSupportUsageLimitLock)
                {
                    this.Text = localization[Localization.usageLimitLock_Management];
                }
                else
                {
                    this.Text = localization[Localization.usageLimitLock_Management_NotSupport];
                }
            }
            else
            {
                this.Text = localization[Localization.usageLimitLock_Management];
            }
        }

        private void InitUI()
        {
            Debug.Assert(this.system != null);

            this.UpdateText();

            this.txtDeviceID.Text = this.system.deviceIdCode;
            this.deviceNameVisible = this.system.isSupportDeviceName;
            if(system.isSupportDeviceName)
            {
                this.deviceName = system.deviceName;
                this.txtDeviceName.canEditable = !system.isLockUserOperation;
            }

            for (int i = 0; i < this.usageLimitLockPanels.Length; ++i)
            {
                this.usageLimitLockPanels[i].InitUI();
            }
        }


        private string deviceName
        {
            get
            {
                return this.txtDeviceName.Text;
            }
            set
            {
                this.txtDeviceName.TextChanged -= txtDeviceName_TextChanged;
                this.txtDeviceName.Text = value;
                this.txtDeviceName.TextChanged += txtDeviceName_TextChanged;
            }
        }

        private void txtDeviceName_TextChanged(object sender, EventArgs e)
        {
            string name = Tools.StringHelper.GetMaxSizeUtf8String(this.txtDeviceName.Text.Trim(), SystemModuleBase.DefDeviceNameSize);

            if (name != this.txtDeviceName.Text)
            {
                this.deviceName = name;
            }

            //Log.OutputLine("设备名称:{0}", name);
            this.system.UpdateDeviceName(name);
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            string copyText = this.txtOutput.SelectedText;
            Clipboard.SetDataObject(copyText);
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            this.txtOutput.Clear();
        }

        private void OutputMsg(string format, params object[] arg)
        {
            string txt = string.Format(format, arg);
            if(this.txtOutput.TextLength != 0)
            {
                this.txtOutput.AppendText(System.Environment.NewLine);
            }
            this.txtOutput.AppendText(txt);
            this.txtOutput.SelectionStart = this.txtOutput.TextLength;
            this.txtOutput.SelectionLength = 0;
        }
        
        public string GetUsageLimitLockDescription(AEC.Common.UsageLimitLock usageLimitLock)
        {
            return string.Format(localization[Localization.usageLimitLock_LockToString], BoolToEnableString(usageLimitLock.enable), usageLimitLock.usageCount, usageLimitLock.passwordVisible ? usageLimitLock.systemPasswordStr : HidePasswordStr);
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            for (short i = 0; i < this.usageLimitLockPanels.Length; ++i)
            {
                this.usageLimitLockPanels[i].passwordVisible = this.chkShowPassword.IsChecked;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnPasswordManagement))
            {
                this.ShowPasswordManagement();
            }
            else if(sender.Equals(this.btnLockManagement))
            {
                this.ShowLockManagement();
            }
            else if (sender.Equals(this.btnForgotPassword))
            {
                this.ForgotPassword();
            }
            else if (sender.Equals(this.btnSetting))
            {
                this.Setting();
            }
        }

        private void ForgotPassword()
        {
            FormForgotPassword form = new FormForgotPassword();
            form.Init(this.system, this.txtRandKey.Text);
            form.ShowDialog();
        }

        private void Setting()
        {
            string errorMsg = string.Empty;
            ULLTask task = ULLTaskSeralizer.DeseralizeFromFile(this.txtTaskFilePath.Text.Trim(), out errorMsg);
            if (task == null)
            {
                this.OutputMsg(localization[Localization.usageLimitLock_TaskFileXNotExist], this.txtTaskFilePath.Text.Trim());
                return;
            }

            if (task.updateCount == 0)
            {
                this.OutputMsg(localization[Localization.usageLimitLock_NoLockWillToBeUpdated]);
                return;
            }
            
            for(int i = 0; i < task.ullOperations.Length; ++i)
            {
                if(task.ullOperations[i].isUpdate)
                {
                    List<string> passowrds = this.system.GetUsageLimitLockPasswords((uint)i, this.randKey);

                    if(!passowrds.Contains(task.ullOperations[i].oldPassword) && !passowrds.Contains(task.ullOperations[i].newPassword))
                    {
                        this.OutputMsg(localization[Localization.usageLimitLock_LockXPasswordNotMatch], i + 1);
                        return;
                    }
                }
            }

            for (int i = 0; i < task.ullOperations.Length; ++i)
            {
                if (task.ullOperations[i].isUpdate)
                {
                    task.ullOperations[i].ull.passwordVisible = this.chkShowPassword.IsChecked;
                    this.system.UpdateUsageLimitLock(task.ullOperations[i].ull);
                }
            }
        }

        private FormPasswordManagement formPasswordManagement;

        private void ShowPasswordManagement()
        {
            if(this.formPasswordManagement == null || this.formPasswordManagement.IsDisposed)
            {
                this.formPasswordManagement = new FormPasswordManagement();
                this.formPasswordManagement.Owner = this;
                this.formPasswordManagement.Show();
            }

            this.formPasswordManagement.Activate();
        }

        private FormLockManagement formLockManagement;

        private void ShowLockManagement()
        {
            if (this.formLockManagement == null || this.formLockManagement.IsDisposed)
            {
                this.formLockManagement = new FormLockManagement();
                this.formLockManagement.Owner = this;
                this.formLockManagement.Show();
            }

            this.formLockManagement.Activate();
        }

        private void txt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory || fi.Extension != ULLTaskSeralizer.MLT_FILE_EXTENSION)
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    e.Effect = DragDropEffects.All;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void txt_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径

            if (this.txtTaskFilePath.Text != path)
            {
                this.txtTaskFilePath.Text = path;
            }
            else
            {
                this.UpdateTaskMsg();
            }

            this.txtTaskFilePath.SelectionStart = this.txtTaskFilePath.TextLength;
        }

        private void txtTaskFilePath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ULLTaskSeralizer.DEF_MLT_FILE_FILTER;
            ofd.InitialDirectory = ULLTaskSeralizer.InitialDirectory;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(this.txtTaskFilePath.Text != ofd.FileName)
                {
                    this.txtTaskFilePath.Text = ofd.FileName;
                }
                else
                {
                    this.UpdateTaskMsg();
                }

                this.txtTaskFilePath.SelectionStart = this.txtTaskFilePath.TextLength;
            }
        }

        private void txtTaskFilePath_TextChanged(object sender, EventArgs e)
        {
            string taskFilePath = this.txtTaskFilePath.Text.Trim();
            this.btnSetting.Enabled = mController.isConnected && System.IO.File.Exists(taskFilePath);
            this.UpdateTaskMsg();
        }

        private void UpdateTaskMsg()
        {
            if (System.IO.File.Exists(this.txtTaskFilePath.Text.Trim()))
            {
                string errorMsg = string.Empty;
                ULLTask task = ULLTaskSeralizer.DeseralizeFromFile(this.txtTaskFilePath.Text.Trim(), out errorMsg);
                if (task != null)
                {
                    if (task.updateCount > 0)
                    {
                        this.OutputMsg(localization[Localization.usageLimitLock_TheFollowingLocksWillToBeUpdated]);
                        foreach (var t in task.ullOperations)
                        {
                            if (t.isUpdate)
                            {
                                this.OutputMsg(ULLOperationToString(t, this.chkShowPassword.IsChecked));
                            }
                        }
                    }
                    else
                    {
                        this.OutputMsg(localization[Localization.usageLimitLock_NoLockWillToBeUpdated]);
                    }
                }
            }
            else
            {
                this.OutputMsg(localization[Localization.usageLimitLock_TaskFileXNotExist], this.txtTaskFilePath.Text.Trim());
            }
        }

        public static string ULLOperationToString(ULLOperation ullOperation, bool passwordVisible)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(localization[Localization.usageLimitLock_LockTaskToString], 
                ullOperation.index + 1,
                passwordVisible ? ullOperation.oldPassword : HidePasswordStr,
                BoolToEnableString(ullOperation.enable),
                ullOperation.usageCount,
                passwordVisible ? ullOperation.newPassword : HidePasswordStr);

            return sb.ToString();
        }

        private static string BoolToEnableString(bool enable)
        {
            return enable ? localization[Localization.usageLimitLock_Enable] : localization[Localization.usageLimitLock_Disable];
        }

        protected override void UpdateDisplayLanguage()
        {
            this.UpdateText();
            this.lblDeviceID.UpdateText(localization[Localization.usageLimitLock_DeviceID], true);
            this.lblDeviceName.UpdateText(localization[Localization.usageLimitLock_DeviceName], true);
            this.lblRandKey.UpdateText(localization[Localization.usageLimitLock_RandKey], true);
            this.btnPasswordManagement.UpdateText(localization[Localization.usageLimitLock_PasswordManagement], true);
            this.btnLockManagement.UpdateText(localization[Localization.usageLimitLock_LockTaskManagement], true);
            this.btnForgotPassword.UpdateText(localization[Localization.usageLimitLock_ForgotPassword], true);
            this.lblTaskFile.UpdateText(localization[Localization.usageLimitLock_TaskFilePath], true);
            this.btnSetting.UpdateText(localization[Localization.usageLimitLock_Setting], true);

            this.tsmiCopy.Text = localization[Localization.usageLimitLock_Copy];
            this.tsmiClear.Text = localization[Localization.usageLimitLock_Clear];

            foreach(UsageLimitLockPanel panel in this.usageLimitLockPanels)
            {
                panel.UpdateDisplayLanguage();
            }
        }
    }
}
