using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using AEC.Core;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;
using AEC.UI.Skin;

namespace AEC.UI
{
    public partial class DeviceModePanel : gtUserControl
    {
        private MController mController;
        private SystemModuleBase system;
        private EventBus mainEventBus;
        private EventBus controllerEvent;

        private SynchronizationContext syncContext;

        private short deviceModeIndex
        {
            get
            {
                return (short)this.cmbDeviceMode.SelectedIndex;
            }
        }

        private bool _isLockSaveLastPreset = false;
        public bool isLockSaveLastPreset 
        { 
            get
            {
                return _isLockSaveLastPreset;
            }
            set
            {
                if(_isLockSaveLastPreset != value)
                {
                    _isLockSaveLastPreset = value;
                    if (_isLockSaveLastPreset)
                    {
                        this.txtModeName.Enabled = deviceModeIndex < system.deviceModeCount - 1;
                    }
                    else
                    {
                        this.txtModeName.Enabled = true;
                    }
                }
            }
        }

        public DeviceModePanel()
        {
            InitializeComponent();
            InitFont();
            this.syncContext = WindowsFormsSynchronizationContext.Current;
            RegisterCommboxSelectedIndexChanged();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.lblDeviceMode.Font = FontHelper.CreateFont10();
            this.lblCurrDeviceMode.Font = FontHelper.CreateFont10();
            this.lblDeviceName.Font = FontHelper.CreateFont10();
            //this.cmbDeviceMode.Font = FontHelper.CreateFont10();
            this.btnCallMode.Font = FontHelper.CreateFont10();
            this.btnSaveMode.Font = FontHelper.CreateFont10();
            //this.txtModeName.Font = FontHelper.CreateFont10();
        }

        public void InitTheme()
        {
            this.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.ForeColor = SkinManager.CurrentSkin.PanelForeColor;

            this.lblDeviceMode.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblDeviceMode.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblCurrDeviceMode.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            
            this.txtModeName.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtModeName.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            this.lblDeviceName.ForeColor = SkinManager.CurrentSkin.TextForeColor;
        }

        public void Binding(MController mController_, SystemModuleBase system_, EventBus eventBus_)
        {
            Debug.Assert(mController_ != null && system_ != null && eventBus_ != null);

            this.mController = mController_;
            this.system = system_;

            this.RemoveEventBus();
            this.mainEventBus = eventBus_;
            this.controllerEvent = this.mController.controllerEvent;
            this.RegisterEventBus();

            this.InitUI();
        }

        public void UpdateUIFromData()
        {
            this.InitUI();
        }

        private void InitUI()
        {
            RemoveCommboxSelectedIndexChanged();

            this.cmbDeviceMode.Items.Clear();
            for (int i = 0; i < this.system.deviceModeCount; ++i)
            {
                string deviceMode = this.system.deviceModeList[i].Trim();
                string item = (i + 1).ToString() + " - " + deviceMode;
                this.cmbDeviceMode.Items.Add(item);
            }
            this.cmbDeviceMode.SelectedIndex = this.system.deviceModeCallIndex.v;
            //this.txtModeName.Enabled = this.system.deviceModeCallIndex.v != this.system.deviceModeCount - 1;
            RegisterCommboxSelectedIndexChanged();

            this.txtModeName.Text = this.system.deviceModeList[this.system.deviceModeCallIndex.v].Trim();

            lblCurrDeviceMode.Text = localization[Localization.panelSystem_DeviceModeMgt_CurrDeviceMode] + (this.system.deviceModeCallIndex.v + 1).ToString();
            lblCurrDeviceMode.UpdateDisplay();

            this.btnLock.Visible = this.system.isSupportDataEncryption;
        }

        public void EventBusHandler(object sender, EventBusArgs e)
        {
            switch (e.type)
            {
                case EventType.ModeNameUpdated:
                    this.InitUI();
                    break;
            }
        }
        

        private void RegisterCommboxSelectedIndexChanged()
        {
            this.cmbDeviceMode.SelectedIndexChanged += comboBox_SelectedIndexChanged;
        }

        private void RemoveCommboxSelectedIndexChanged()
        {
            this.cmbDeviceMode.SelectedIndexChanged -= comboBox_SelectedIndexChanged;
        }

        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.system == null)
            {
                return;
            }

            if (deviceModeIndex >= 0 && deviceModeIndex < this.system.deviceModeCount)
            {
                this.txtModeName.Text = system.deviceModeList[deviceModeIndex].Trim();
            }

            if(isLockSaveLastPreset)
            {
                this.txtModeName.Enabled = deviceModeIndex != this.system.deviceModeCount - 1;
            }
        }

        public void Close()
        {
            this.RemoveEventBus();
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

        private void CallMode()
        {
            if(this.system == null)
            {
                return;
            }

            short modeIndex = (short)this.cmbDeviceMode.SelectedIndex;
            string modeName = this.system.deviceModeList[modeIndex];

#if !COMM_TEST
            if (DialogResult.OK != gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DeviceModeMgt_AskForCallModeData], modeIndex + 1, modeName.Trim()), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }
#endif

            //调用设备模式
            this.mainEventBus.NotifyObeserver(new CallDeviceModeArgs((short)(this.cmbDeviceMode.SelectedIndex)));
        }

        private void SaveMode()
        {
            if (this.system == null)
            {
                return;
            }

            if (isLockSaveLastPreset && (this.cmbDeviceMode.SelectedIndex == this.system.deviceModeCount - 1))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DeviceModeMgt_FactoryPresetCannotBeSaved], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

#if !COMM_TEST
            if (system.isLockUserOperation)
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DeviceModeMgt_CannotSave], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
#endif

            string newModeName = this.txtModeName.Text;
            //string showModeName = (deviceModeIndex + 1).ToString() + "-" + newModeName;
#if !COMM_TEST
            if (DialogResult.OK != gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DeviceModeMgt_AskForSaveModeData], deviceModeIndex + 1, newModeName), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }
#endif
            int modeIndex = this.cmbDeviceMode.SelectedIndex;
            this.cmbDeviceMode.Items[modeIndex] = (modeIndex + 1) + " - " + newModeName;
            this.txtModeName.Text = newModeName;

#if COMM_TEST
            this.system.deviceModeSaveIndex.initV = (short)modeIndex;

            AEC.Log.OutputLine("保存模式:{0}", modeIndex);
            AEC.Tools.LogHelper.WriteDebug("保存模式:{0}", modeIndex);
#endif

            this.system.SaveDeviceMode((short)modeIndex, newModeName);

            RemoveEventBus();
            this.mainEventBus.NotifyObeserver(new ModeNameUpdatedArgs());
            RegisterEventBus();
        }

        private delegate void DelVoidCallback();

        private void button_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnCallMode))
            {
                DelVoidCallback callback = new DelVoidCallback(CallMode);
                this.BeginInvoke(callback);
            }
            else if (sender.Equals(this.btnSaveMode))
            {
                DelVoidCallback callback = new DelVoidCallback(SaveMode);
                this.BeginInvoke(callback);
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
                this.btnCallMode.Enabled = this.mController.commState != CommState.Connecting;
                this.btnSaveMode.Enabled = this.mController.commState != CommState.Connecting;

                if (this.mController.commState == CommState.Connected)
                {
                    this.InitUI();
                    this.InitLockButton();
                }
                else
                {
                    this.btnLock.Visible = false;
                }

                if (mController.commState == CommState.Disconnected)
                {
                    this.CommIdleHandler();
                }
            }
            //else if(e.type == EventType.CommCompleted)
            //{
            //    CommCompletedArgs commCompleted = e as CommCompletedArgs;
            //    AEC.Tools.LogHelper.WriteDebug("DeviceModePanel.CommEventHandler :{0}", commCompleted.commResult.success);
            //    if(commCompleted.commResult.success)
            //    {
            //        InitLockButton();
            //    }
            //}
            else if(e.type == EventType.StartSaveMode)
            {
                this.StartSaveModeHandler();
            }
            else if (e.type == EventType.SaveModeCompleted)
            {
                this.SaveModeCompleteHandler();
            }
        }

        private void InitLockButton()
        {
            if (!this.system.isSupportDataEncryption)
            {
                return;
            }

            this.btnLock.Enabled = !this.system.dataIsPrivate;
            if(this.system.dataIsPrivate)
            {
                //AEC.Tools.LogHelper.WriteDebug("DeviceModePanel.InitLockButton : 数据已加密");
                this.btnLock.backNormal = global::AEC.Properties.Resources.mode_locked_normal;
                this.btnLock.backDisabled = global::AEC.Properties.Resources.mode_locked_disabled;
            }
            else
            {
                //AEC.Tools.LogHelper.WriteDebug("DeviceModePanel.InitLockButton :数据未加密");
                this.btnLock.backNormal = global::AEC.Properties.Resources.mode_unlock_normal;
                this.btnLock.backDisabled = global::AEC.Properties.Resources.mode_unlock_disabled;
            }
            this.btnLock.UpdateDisplay();
        }

        private void StartSaveModeHandler()
        {
            this.btnCallMode.Enabled = false;
            this.btnSaveMode.Enabled = false;
        }

        private void SaveModeCompleteHandler()
        {
            this.CommIdleHandler();
        }

        private void CommIdleHandler()
        {
            this.btnCallMode.Enabled = true;
            this.btnSaveMode.Enabled = true;
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.txtModeName.Focused)
            {
                return false;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        public const char ascii_char_upper_limiter = '\u007f';

        private void txtModeName_TextChanged(object sender, EventArgs e)
        {
            string source_text = this.txtModeName.Text;
            //List<char> list = new List<char>();
            //string dest_text = string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char ch in source_text)
            {
                if (ch <= ascii_char_upper_limiter && !char.IsControl(ch)) 
                {
                    sb.Append(ch);
                    //dest_text += source_text[i];
                }
            }
            //for (int i = 0; i < source_text.Length; ++i)
            //{
            //    if (source_text[i] <= ascii_char_upper_limiter && !char.IsControl(source_text[i])) 
            //    {
            //        sb.Append(source_text[i]);
            //        //dest_text += source_text[i];
            //    }
            //}
            //dest_text = dest_text.Trim();

            this.txtModeName.TextChanged -= txtModeName_TextChanged;
            this.txtModeName.Text = sb.ToString();
            this.txtModeName.TextChanged += txtModeName_TextChanged;
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if(!this.system.dataIsPrivate)
            {
                if (DialogResult.OK == gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DeviceModeMgt_AskForEncrpyData], localization[Localization.panelSystem_DeviceModeMgt_EncrpyCurrModeData], MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    this.btnLock.Enabled = false;
                    mainEventBus.NotifyObeserver(new EncryptModeDataArgs());
                }
            }
        }

        private void txtModeName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SaveMode();
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.title = localization[Localization.panelSystem_DeviceModeMgt];
            this.lblDeviceMode.Text = localization[Localization.panelSystem_DeviceModeMgt_DeviceMode];
            this.lblCurrDeviceMode.Text = localization[Localization.panelSystem_DeviceModeMgt_CurrDeviceMode] + (this.system.deviceModeCallIndex.v + 1).ToString();
            this.lblDeviceName.Text = localization[Localization.panelSystem_DeviceModeMgt_ModeName];
            this.btnSaveMode.Text = localization[Localization.panelSystem_DeviceModeMgt_SaveMode];
            this.btnCallMode.Text = localization[Localization.panelSystem_DeviceModeMgt_CallMode];
            
            this.Invalidate();
        }

#if COMM_TEST
        public readonly static Random random = new Random();

        public void RandSaveMode()
        {
            string str = "0123456789" + "abcdefghijklmnopqrstuvwxyz" + " ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            char[] randCharList = new char[20];
            for (int i = 0; i < randCharList.Length; ++i)
            {

                randCharList[i] = str[random.Next(str.Length)];
            }

            this.cmbDeviceMode.SelectedIndex = random.Next(this.cmbDeviceMode.Items.Count);

            this.txtModeName.Text = new string(randCharList);

            AEC.Log.OutputLine("保存随机模式, mode name[{0}]:{1}", this.cmbDeviceMode.SelectedIndex, this.txtModeName.Text);

            this.SaveMode();
        }

        public void RandCallMode()
        {
            //this.cmbDeviceMode.SelectedIndex = random.Next(this.cmbDeviceMode.Items.Count);
            AEC.Log.OutputLine("调用随机模式:{0}", this.system.deviceModeSaveIndex.v);
            this.cmbDeviceMode.SelectedIndex = this.system.deviceModeSaveIndex.v;
            this.CallMode();
        }
#endif
    }
}
