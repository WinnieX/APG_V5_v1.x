using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;  
using AEC.Core;
using AEC.Common;
using AEC.Comm;
using AEC.Effectors;
using AEC.Misc;
using AEC.Tools;
using AEC.UI.Skin;

namespace AEC.UI
{
    public partial class DataBackupMgtPanel : gtUserControl
    {
        //private static readonly Color factoryModeForeColor = Color.FromArgb(46, 125, 50);//Color.FromArgb(156, 204, 74);
        //private static readonly Color externalModeForeColor = Color.FromArgb(230, 81, 0);//Color.FromArgb(255, 168, 38);
        //private static readonly Color defaultModeForeColor = Color.FromArgb(224, 224, 224);

        private MController mController;
        private EventBus mainEventBus = null;
        private EventBus controllerEvent = null;

        private SynchronizationContext syncContext;

        private string currEffectorPresetMainFolder = string.Empty;        //效果器型号

        private List<MEffectorModeDataProvider> mdpList = new List<MEffectorModeDataProvider>();

        private bool readyToDragDrop = false;
        private Point mouseDownPosition;

        public EffectorModelBase effectorModel
        {
            get
            {
                if (this.mController != null)
                {
                    return this.mController.currEffector.effectorModel;
                }

                return null;
            }
        }

        public CustomModeDataFileManager customModeDataFileManager
        {
            get
            {
                if (this.mController != null)
                {
                    return this.mController.customModeDataFileManager;
                }

                return null;
            }
        }

        private string currModelPresetFolder
        {
            get
            {
                return this.mController.mEffectorModel.currModelPresetMainFolder[0];
            }
        }

        public string currModeDataFileDir
        {
            get
            {
                return this.customModeDataFileManager.GetPresetDataFileDir(currModelPresetFolder);
            }
        }

        public DataBackupMgtPanel()
        {
            InitializeComponent();
            InitFont();
            this.syncContext = WindowsFormsSynchronizationContext.Current;

            //this.dgvFile.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);

            this.dgvFile.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dgvFile.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvFile.Scroll += dgvFile_Scroll;
            this.vScrollbar.Scroll += vScrollbar_Scroll;

            this.VisibleChanged += DataBackupMgtPanel_VisibleChanged;
        }

        private void DataBackupMgtPanel_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible)
            {
                this.UpdateNewVScrollbar();
            }
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.dgvFile.RowsDefaultCellStyle.Font = FontHelper.CreateFont10();
            this.lblBackupFileName.Font = FontHelper.CreateFont10();
            this.btnSave.Font = FontHelper.CreateFont10();
            this.btnLoad.Font = FontHelper.CreateFont10();
        }

        public void InitTheme()
        {
            this.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.ForeColor = SkinManager.CurrentSkin.PanelForeColor;

            this.lblBackupFileName.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.textBoxFileName.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.textBoxFileName.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            this.dgvFile.BackgroundColor = SkinManager.CurrentSkin.SystemDgvBackColor;
            this.dgvFile.AlternatingRowsDefaultCellStyle.BackColor = SkinManager.CurrentSkin.SystemDgvAlternatingRowsBackColor;

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = this.dgvFile.ColumnHeadersDefaultCellStyle;
            dataGridViewCellStyle1.ForeColor = SkinManager.CurrentSkin.SystemDgvHeaderBackColor;

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = this.dgvFile.DefaultCellStyle;
            dataGridViewCellStyle3.BackColor = SkinManager.CurrentSkin.SystemDgvCellDefaultBackColor;
            dataGridViewCellStyle3.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            this.vScrollbar.BackColor = SkinManager.CurrentSkin.ScrollbarBackColor;
            this.vScrollbar.FillColor = SkinManager.CurrentSkin.ScrollbarBackColor;
            this.vScrollbar.ThumbColor = SkinManager.CurrentSkin.ScrollbarForeColor;
        }

        public void Binding(MController mController_, EventBus eventBus_)
        {
            this.mController = mController_;
            this.mainEventBus = eventBus_;

            this.RemoveEventBus();
            this.controllerEvent = this.mController.controllerEvent;
            this.RegisterEventBus();

            if (this.currEffectorPresetMainFolder != this.mController.mEffectorModel.currModelPresetMainFolder[0])
            {
                this.currEffectorPresetMainFolder = this.mController.mEffectorModel.currModelPresetMainFolder[0];
                this.InitModeDataProviderList(false);
            }

            this.InitUI();
        }

        public void UpdateUIFromData()
        {
            if (this.currEffectorPresetMainFolder != this.mController.mEffectorModel.currModelPresetMainFolder[0])
            {
                this.currEffectorPresetMainFolder = this.mController.mEffectorModel.currModelPresetMainFolder[0];

                ReleadModeListToCache(false);
            }
            else
            {
                UpdateCustomModeModelTipText();
            }
        }

        public void Close()
        {
            this.RemoveEventBus();
        }

        public string GetCurrModeFileFullPath(string name)
        {
            return this.customModeDataFileManager.GetPresetFileFullPath(currModelPresetFolder, name);
        }

        public void UpdateModeFile(string name)
        {
            this.customModeDataFileManager.UpdateModeFile(currModelPresetFolder, name);
        }

        private void InitModeDataProviderList(bool updateCache)
        {
            mdpList.Clear();

            MCustomModeProvider[] customModes = this.customModeDataFileManager.GetCustomModeProviderList(this.mController.mEffectorModel.currModelPresetMainFolder, updateCache);
            if (customModes != null)
            {
                mdpList.AddRange(customModes);
            }

            MFactoryModeProvider[] factoryModes = GetFactoryModeProviderList(this.mController.mEffectorModel.currModel);
            if(factoryModes != null)
            {
                mdpList.AddRange(factoryModes);
            }

            this.btnLoad.Enabled = mdpList.Count > 0;
        }

        private void UpdateCustomModeModelTipText()
        {
            for (int i = 0; i < mdpList.Count; ++i)
            {
                if(mdpList[i].isCustomData)
                {
                    MCustomModeProvider cmp = mdpList[i] as MCustomModeProvider;
                    if(cmp != null && !cmp.isExternal)
                    {
                        this.dgvFile.Rows[i].Cells[1].ToolTipText = GetModeDataProviderToolTipText(mdpList[i]);
                    }
                }
            }
        }

        private void InitUI()
        {
            this.dgvFile.Rows.Clear();

            foreach (var mdp in mdpList)
            {
                int index = this.dgvFile.Rows.Add();
                this.dgvFile.Rows[index].Cells[0].Value = (index + 1).ToString();
                this.dgvFile.Rows[index].Cells[1].Value = mdp.name;
                this.dgvFile.Rows[index].Cells[1].Style.ForeColor = GetModeDataProviderForeColor(mdp);
                this.dgvFile.Rows[index].Cells[1].ToolTipText = GetModeDataProviderToolTipText(mdp);
            }

            if (mdpList.Count > 0)
            {
                int rowIndex = 0;
                this.dgvFile.Rows[rowIndex].Selected = true;
                this.textBoxFileName.Text = this.dgvFile.Rows[rowIndex].Cells[1].Value.ToString();
                this.dgvFile.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            else
            {
                this.textBoxFileName.Clear();
            }

            UpdateNewVScrollbar();
        }

        public static Color GetModeDataProviderForeColor(MEffectorModeDataProvider mdp)
        {
            if (!mdp.isCustomData)
            {
                return SkinManager.CurrentSkin.SystemFactoryModeForeColor;
            }
            else
            {
                MCustomModeProvider customMode = mdp as MCustomModeProvider;
                if (customMode != null)
                {
                    if (customMode.mainModel != customMode.folder)
                    {
                        return SkinManager.CurrentSkin.SystemExternalModeForeColor;
                    }
                }
            }

            return SkinManager.CurrentSkin.TextBoxForeColor;
        }

        public string GetModeDataProviderToolTipText(MEffectorModeDataProvider mdp)
        {
            if (!mdp.isCustomData)
            {
                return localization[Localization.panelSystem_DataBackupMgt_FactoryMode];
            }
            else
            {
                MCustomModeProvider customMode = mdp as MCustomModeProvider;
                if (customMode != null)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.AppendLine(localization[Localization.panelSystem_DataBackupMgt_UserData]);

                    sb.Append(GetCustomDataInfo(customMode));

                    return sb.ToString();
                }
            }

            return string.Empty;
        }

        private string GetCustomDataInfo(MCustomModeProvider customMode)
        {
            string model = customMode.isExternal ? customMode.folder : this.mController.mEffectorModel.modelLabel;

            return string.Format(
                    localization[Localization.panelSystem_DataBackupMgt_UserDataInfo],
                    customMode.name + System.Environment.NewLine,
                    model + System.Environment.NewLine,
                    customMode.path + System.Environment.NewLine,
                    GetFileModifiedTime(customMode.path));
        }

        private static string GetFileModifiedTime(string path)
        {
            if(System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);

                    return fileInfo.LastWriteTime.ToString(("yyyy-MM-dd HH:mm:ss:fff"));
                }
                catch (Exception)
                {

                }
            }

            return string.Empty;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnSave))
            {
                this.SaveDataToLocalFile();
            }
            else if (sender.Equals(this.btnLoad))
            {
                this.LoadDataToDevice();
            }
        }

        private void RegisterEventBus()
        {
            if (this.controllerEvent != null)
            {
                this.controllerEvent.RegisterHandler(CommStateChanged);
            }
        }

        private void RemoveEventBus()
        {
            if (this.controllerEvent != null)
            {
                this.controllerEvent.RemoveHandler(CommStateChanged);
            }
        }

        private void SaveDataToLocalFile()
        {
            if (!this.mController.isSuperUserMode && this.effectorModel.dataIsPrivate)
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DataBackupMgt_DataIsEncryptedAndCannotBeExported], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }

            this.textBoxFileName.Text = this.textBoxFileName.Text.Trim();
            if (this.textBoxFileName.TextLength > 0)
            {
                this.SeralizeToFileAndUpdateUI(mController.currEffectorModel, this.textBoxFileName.Text);
            }
        }

        private void SeralizeToFileAndUpdateUI(EffectorModelBase effectorModel, string name)
        {
            System.Diagnostics.Debug.Assert(effectorModel != null && !string.IsNullOrEmpty(name));

            //string name = this.textBoxFileName.Text;
            string path = this.GetCurrModeFileFullPath(name);

            if (System.IO.File.Exists(path))
            {
                if (System.Windows.Forms.DialogResult.OK != gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_AskForSaveAndOverwriteFile], name), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    return;
                }
            }

            int index = FindModeDataFormCustomMode(name);

            bool isContain = index >= 0;

            try
            {
                mController.SeralizeEffectorModelToFile(effectorModel, path);
            }
            catch (Exception ex)
            {
                gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FailedToSaveFile], ex.Message));
                return;
            }

            if (isContain)
            {
                this.mdpList.RemoveAt(index);

                for (int i = 0; i < this.dgvFile.Rows.Count; i++)
                {
                    if (dgvFile.Rows[i].Cells[1].Value.ToString() == name)
                    {
                        this.dgvFile.Rows.RemoveAt(i);
                        break;
                    }
                }
            }

            this.InsertCustomData2DGVFirstRow(name);

            this.UpdateModeFile(name);
        }

        private void LoadDataToDevice()
        {
            LogHelper.WriteDebug("LoadDataToDevice()...");
            if (this.dgvFile.CurrentRow == null)
            {
                return;
            }

            if (this.dgvFile.CurrentRow.Index >= 0 && this.dgvFile.CurrentRow.Index < this.mdpList.Count)
            {
                MEffectorModeDataProvider mdp = this.mdpList[this.dgvFile.CurrentRow.Index];
                string modeName = string.Empty;

                if(mdp.isCustomData)
                {
                    MCustomModeProvider cm = mdp as MCustomModeProvider;

                    if(cm == null)
                    {
                        return;
                    }

                    if (!System.IO.File.Exists(cm.path))
                    {
                        gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DataBackupMgt_FailedToLoadDataAndFileNotExist], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    modeName = string.Format("{0}（{1}）", cm.name, localization[Localization.panelSystem_DataBackupMgt_UserData]);
                }
                else
                {
                    MFactoryModeProvider fm = mdp as MFactoryModeProvider;
                    if (fm == null)
                    {
                        return;
                    }

                    modeName = string.Format("{0}（{1}）", fm.name, localization[Localization.panelSystem_DataBackupMgt_FactoryMode]);
                }

                EffectorModelBase tempEffectorModel = null;

                try
                {
                    this.effectorModel.InitTempModel();

                    tempEffectorModel = mdp.Deseralize(this.mController.mEffectorSeralizer);                    
                }
                catch (Exception)
                {
                    gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DataBackupMgt_FailedToLoadDataAndFileDataError], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LogHelper.WriteDebug("Load The Data To Be Updated Successfully.");

                if (tempEffectorModel != null)
                {
                    if (this.mController.isConnected)
                    {
                        if (DialogResult.OK == gtMessageBox.ShowMessage(this.FindForm(), string.Format(string.Format(localization[Localization.panelSystem_DataBackupMgt_AskForLoadAndOverwriteData], modeName)), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                        {
                            LogHelper.WriteDebug("Fire Update Data Event()...");
                            this.mainEventBus.NotifyObeserver(new UploadDataToDeviceArgs(tempEffectorModel));
                        }
                    }
                    else
                    {
                        if (DialogResult.OK == gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_AskForContinueLoadingData], modeName), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                        {
                            this.effectorModel.CopyFrom(tempEffectorModel);
                            this.effectorModel.InitFreqRespAndEQGraphic();
                            this.mainEventBus.NotifyObeserver(new UpdateUIFromDataArgs());
                        }
                    }
                }
                //else
                //{
                //    gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DataBackupMgt_FailedToLoadDataAndFileDataError], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
            }
        }

        private void dgvFile_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < this.dgvFile.Rows.Count)
            {
                try
                {
                    string file = this.dgvFile.Rows[e.RowIndex].Cells[1].Value.ToString();
                    this.textBoxFileName.Text = file;
                }
                catch(Exception ex)
                {
                    AEC.Tools.LogHelper.WriteInfo("DataBackupMgtPanel.dgvFile_CellMouseDown().... Exception: ", ex);
                }

                if(e.Button == MouseButtons.Right)
                {
                    this.dgvFile.CurrentCell = this.dgvFile[1, e.RowIndex];
                    if(e.RowIndex < this.mdpList.Count)
                    {
                        this.tsmiDelete.Enabled = this.mdpList[e.RowIndex].isCustomData;
                        
                        contextMenuStrip.Show(Control.MousePosition);
                    }
                }
                else if(e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (e.RowIndex >= 0 && e.RowIndex < this.mdpList.Count)
                    {
                        if (this.mdpList[e.RowIndex].isCustomData)
                        {
                            this.readyToDragDrop = true;
                            mouseDownPosition = e.Location;
                            //Log.OutputLine("dgvFile_CellMouseDown()...{0}", this.readyToDragDrop);
                        }
                    }
                }
            }
        }

        private void dgvFile_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.readyToDragDrop)
            {
                Point leftTop = new Point(mouseDownPosition.X - SystemInformation.DragSize.Width / 2, mouseDownPosition.Y - SystemInformation.DragSize.Height / 2);
                Rectangle rect = new Rectangle(leftTop, SystemInformation.DragSize);
                if(!rect.Contains(e.Location))
                {
                    BeginDoDropFile();
                }
            }
        }

        private void dgvFile_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.readyToDragDrop = false;
            //Log.OutputLine("dgvFile_CellMouseUp()...{0}", this.readyToDragDrop);
        }

        private void BeginDoDropFile()
        {
            if(this.dgvFile.CurrentRow == null)
            {
                return;
            }

            //Log.OutputLine("BeginDoDropFile()...");

            if (this.dgvFile.CurrentRow.Index >= 0 && this.dgvFile.CurrentRow.Index < this.mdpList.Count)
            {
                //Log.OutputLine("BeginDoDropFile()...1");

                MEffectorModeDataProvider mdp = this.mdpList[this.dgvFile.CurrentRow.Index];

                //Log.OutputLine("BeginDoDropFile()...2");

                if (mdp.isCustomData)
                {
                    //Log.OutputLine("BeginDoDropFile()...3");

                    MCustomModeProvider cmp = mdp as MCustomModeProvider;

                    if (cmp == null)
                    {
                        return;
                    }

                    //Log.OutputLine("BeginDoDropFile()...4");

                    if (System.IO.File.Exists(cmp.path))
                    {
                        //Log.OutputLine("DoDragDrop()...");
                        this.dgvFile.DoDragDrop(new DataObject(DataFormats.FileDrop, new string[] { cmp.path }), DragDropEffects.Copy);
                    }
                    else
                    {
                        this.readyToDragDrop = false;
                        if (DialogResult.OK == gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FailedToLoadDataAndFileDataError], cmp.name), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
                        {
                            DeleteHandler(cmp);
                        }
                    }
                }
            }

            //String name = this.dgvFile.CurrentRow.Cells[1].Value.ToString();
            //String path = this.GetCurrModeFileFullPath(name);

            
        }

        private void dgvFile_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvFile.Rows.Count == 0 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                AEC.Tools.FileHelper.OpenDirectory(currModeDataFileDir);
            }
        }

        private void dgvFile_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left && e.RowIndex == -1)
            {
                this.ReleadModeListToCache(true);
            }
        }

        private void ReleadModeListToCache(bool updateCache)
        {
            this.InitModeDataProviderList(updateCache);
            this.InitUI();
        }

        private void CommStateChanged(object sender, EventBusArgs e)
        {
            syncContext.Post(ProcessCommEvent, e);
        }

        private void ProcessCommEvent(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if (e.type == EventType.CommStateChanged)
            {
                //AEC.Log.OutputLine("联机状态发生变化:{0}", this.commService.commState);
                this.btnSave.Enabled = this.mController.commState != CommState.Connecting;
                this.btnLoad.Enabled = this.mController.commState != CommState.Connecting;
                this.tsmiLoadData.Enabled = this.mController.commState != CommState.Connecting;
                //AEC.Log.OutputLine("tsmiLoadData enabled:{0}", this.tsmiLoadData.Enabled);

                if (mController.commState == CommState.Disconnected)
                {
                    this.CommIdleHandler();
                }
            }
            else if (e.type == EventType.StartSaveMode)
            {
                this.StartSaveModeHandler();
            }
            else if (e.type == EventType.SaveModeCompleted)
            {
                this.SaveModeCompleteHandler();
            }
        }

        private void StartSaveModeHandler()
        {
            this.btnSave.Enabled = false;
            this.btnLoad.Enabled = false;
        }

        private void SaveModeCompleteHandler()
        {
            this.CommIdleHandler();
        }

        private void CommIdleHandler()
        {
            this.btnSave.Enabled = true;
            this.btnLoad.Enabled = true;
        }

        private void tsmi_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.tsmiLoadData))
            {
                LoadDataToDevice();
            }
            else if(sender.Equals(this.tsmiOpenFileLocation))
            {
                OpenFileLocation();
            }
            else if (sender.Equals(this.tsmiDelete))
            {
                DeleteFile();
            }
        }

        private void OpenFileLocation()
        {
            if(this.dgvFile.CurrentRow == null)
            {
                return;
            }

            if(this.dgvFile.CurrentRow.Index >= 0 && this.dgvFile.CurrentRow.Index < this.mdpList.Count)
            {
                MEffectorModeDataProvider mdp = this.mdpList[this.dgvFile.CurrentRow.Index];
                if(mdp.isCustomData)
                {
                    MCustomModeProvider cmp = mdp as MCustomModeProvider;
                    if(cmp == null)
                    {
                        return;
                    }
                    //String name = this.dgvFile.CurrentRow.Cells[1].Value.ToString();
                    string path = this.customModeDataFileManager.GetPresetFileFullPath(cmp.folder, cmp.name);
                    if (!AEC.Tools.FileHelper.OpenFileLocation(path))
                    {
                        if (DialogResult.OK == gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FileNotExist], cmp.name), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
                        {
                            DeleteHandler(cmp);
                        }
                    }
                }
                else
                {
                    AEC.Tools.FileHelper.OpenDirectory(currModeDataFileDir);
                }
            }
        }

        private void DeleteFile()
        {
            if(this.dgvFile.CurrentRow == null)
            {
                return;
            }

            if (this.dgvFile.CurrentRow.Index >= 0 && this.dgvFile.CurrentRow.Index < this.mdpList.Count)
            {
                MEffectorModeDataProvider mdp = this.mdpList[this.dgvFile.CurrentRow.Index];

                if (mdp.isCustomData)
                {
                    MCustomModeProvider cmp = mdp as MCustomModeProvider;

                    if (cmp == null)
                    {
                        return;
                    }

                    string fileInfo = Environment.NewLine + GetCustomDataInfo(cmp);

                    if (DialogResult.OK == gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_AskForDeleteDataFile], fileInfo), localization[Localization.panelSystem_DataBackupMgt_DeleteFile], MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {
                        this.DeleteHandler(cmp);
                    }
                }
            }
        }

        private void DeleteHandler(MCustomModeProvider cmp)
        {
            System.Diagnostics.Debug.Assert(cmp != null);
            //从管理器中删除

            this.customModeDataFileManager.DeleteModeFile(cmp.folder, cmp.name);

            int index = this.mdpList.IndexOf(cmp); //FindModeDataFormCustomMode(cmp);

            if (index >= 0)
            {
                this.mdpList.RemoveAt(index);
                this.btnLoad.Enabled = mdpList.Count > 0;

                if (index < this.dgvFile.Rows.Count)
                {
                    this.dgvFile.Rows.RemoveAt(index);

                    for (int i = index; i < this.dgvFile.Rows.Count; i++)
                    {
                        dgvFile.Rows[i].Cells[0].Value = (i + 1).ToString();
                    }

                    if (this.dgvFile.CurrentRow != null)
                    {
                        this.textBoxFileName.Text = this.dgvFile.CurrentRow.Cells[1].Value.ToString();
                    }
                    else
                    {
                        this.textBoxFileName.Clear();
                    }
                }
                
                UpdateNewVScrollbar();
            }
        }

        //private int count = 0;
        private void dgvFile_Scroll(object sender, ScrollEventArgs e)
        {
            if(e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                UpdateNewVScrollbar();
            }
        }

        private void vScrollbar_Scroll(object sender, EventArgs e)
        {
            UpdateOwnVScrollbar();
        }

        private void UpdateNewVScrollbar()
        {
            this.vScrollbar.Visible = this.dgvFile.VScrollbar.Visible;
            if (this.dgvFile.VScrollbar.Visible)
            {
                ScrollBar VScrollbar = this.dgvFile.VScrollbar;
                this.vScrollbar.Maximum = VScrollbar.Maximum;
                this.vScrollbar.LargeChange = VScrollbar.LargeChange;
                //this.vScrollbar.SmallChange = VScrollbar.SmallChange;
                this.vScrollbar.Value = VScrollbar.Value;
            }
        }

        private void UpdateOwnVScrollbar()
        {
            if (this.dgvFile.VScrollbar.Visible)
            {
                ScrollBar VScrollbar = this.dgvFile.VScrollbar;
                //this.vScrollbar.Maximum = VScrollbar.Maximum;
                //this.vScrollbar.LargeChange = VScrollbar.LargeChange;
                //this.vScrollbar.SmallChange = VScrollbar.SmallChange;
                VScrollbar.Value = this.vScrollbar.Value;
                int rowIndex = this.vScrollbar.Value / 23;
                this.dgvFile.FirstDisplayedScrollingRowIndex = rowIndex;
                //AEC.Log.OutputLine("UpdateOwnVScrollbar()...{0},rowIndex = {1}", this.vScrollbar.Value, rowIndex + 1);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.textBoxFileName.Focused)
            {
                return false;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        private void dgvFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))      //判断该文件是否可以转换到文件放置格式
            {
                e.Effect = DragDropEffects.Copy;       //放置效果为链接放置
            }
            else
            {
                e.Effect = DragDropEffects.None;      //不接受该数据,无法放置，后续事件也无法触发
            }
        }

        private void dgvFile_DragDrop(object sender, DragEventArgs e)
        {
            //Log.OutputLine("dgvFile_DragDrop()...readyToDragDrop = {0}", readyToDragDrop);
            if (readyToDragDrop)
            {
                return;
            }

            string srcFilePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            DropFile(srcFilePath);
        }

        private void DropFile(string path)
        {
            //Log.OutputLine("DropFile()...path = {0}, readyToDragDrop = {1}", path, this.readyToDragDrop);
            if(!System.IO.File.Exists(path))
            {
                return;
            }

            if (!DataBackup.CheckFileExtensionIsOk(path))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DataBackupMgt_FileTypeError], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EffectorModelBase effectorModel = null;
            try
            {
                effectorModel = this.mController.mEffectorSeralizer.DeseralizeToCurrEffectorFromFile(path, false);
                if(effectorModel == null)
                {
                    return;
                }
            }
            catch(Exception)
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.panelSystem_DataBackupMgt_FileDataError], "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string originalName = System.IO.Path.GetFileNameWithoutExtension(path).Trim();

            FormImportExternalFile form = new FormImportExternalFile(originalName);
            string importName = form.ShowDialog(this);

            if (string.IsNullOrEmpty(importName))
            {
                //Log.OutputLine("文件名不能为空");
                return;
            }

            //string dir = System.IO.Path.GetDirectoryName(path) + @"\";

            //if (dir != currModeDataFileDir || DataBackup.CheckBackupFileIsX5(path))  //如果源目录与目标目录不同或者是X5
            //{
            //    string destPath = this.GetCurrModeFileFullPath(importName);
            //    int index = FindModeDataFormCustomMode(importName);
            //    if (index >= 0 && System.IO.File.Exists(destPath))
            //    {
            //        if (System.Windows.Forms.DialogResult.OK != gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_AskForSaveAndOverwriteFile], importName), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            //        {
            //            return;
            //        }
            //    }

            //    //if (DataBackup.CheckBackupFileIsX5(path))
            //    //{
            //    //    if(!this.mController.SeralizeEffectorModelToFile(effectorModel, destPath))
            //    //    {
            //    //        gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FailedToSaveFile], "Error"));
            //    //        return;
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    try
            //    //    {
            //    //        System.IO.File.Copy(path, destPath, true);
            //    //    }
            //    //    catch (Exception ex)
            //    //    {
            //    //        gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FailedToCopyFile], ex.Message), "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //        return;
            //    //    }
            //    //}

            //    if (!this.mController.SeralizeEffectorModelToFile(effectorModel, destPath))
            //    {
            //        gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FailedToSaveFile], "Error"));
            //        return;
            //    }

            //    //如果列表中存在name
            //    this.textBoxFileName.Text = importName;
            //    if (!SelectCustomModeRow(importName))
            //    {
            //        InsertCustomData2DGVFirstRow(importName);
            //    }

            //    this.UpdateModeFile(importName);
            //}
            //else
            //{
            //    this.textBoxFileName.Text = name;

            //    if (!SelectCustomModeRow(name))
            //    {
            //        InsertCustomData2DGVFirstRow(name);
            //    }

            //    this.UpdateModeFile(name);
            //}

            //string destPath = this.GetCurrModeFileFullPath(importName);
            //int index = FindModeDataFormCustomMode(importName);
            //if (index >= 0 && System.IO.File.Exists(destPath))
            //{
            //    if (System.Windows.Forms.DialogResult.OK != gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_AskForSaveAndOverwriteFile], importName), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            //    {
            //        return;
            //    }
            //}

            SeralizeToFileAndUpdateUI(effectorModel, importName);

            //if (DataBackup.CheckBackupFileIsX5(path))
            //{
            //    if(!this.mController.SeralizeEffectorModelToFile(effectorModel, destPath))
            //    {
            //        gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FailedToSaveFile], "Error"));
            //        return;
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        System.IO.File.Copy(path, destPath, true);
            //    }
            //    catch (Exception ex)
            //    {
            //        gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FailedToCopyFile], ex.Message), "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}
            //bool isContain = index >= 0;

            //if (!this.mController.SeralizeEffectorModelToFile(effectorModel, destPath))
            //{
            //    gtMessageBox.ShowMessage(this.FindForm(), string.Format(localization[Localization.panelSystem_DataBackupMgt_FailedToSaveFile], "Error"));
            //    return;
            //}

            ////如果列表中存在name
            //this.textBoxFileName.Text = importName;
            //if (!SelectCustomModeRow(importName))
            //{
            //    InsertCustomData2DGVFirstRow(importName);
            //}

            //this.UpdateModeFile(importName);
        }

        private bool SelectCustomModeRow(string name)
        {
            int rowIndex = FindModeDataFormCustomMode(name);
            if (rowIndex >= 0 && rowIndex < this.dgvFile.Rows.Count)
            {
                this.dgvFile.Rows[rowIndex].Selected = true;
                this.dgvFile.FirstDisplayedScrollingRowIndex = rowIndex;

                return true;
            }

            return false;
        }

        private void InsertCustomData2DGVFirstRow(string name)
        {
            this.mdpList.Insert(0, new MCustomModeProvider(name, this.GetCurrModeFileFullPath(name), currModelPresetFolder, currModelPresetFolder));

            this.btnLoad.Enabled = mdpList.Count > 0;

            //Log.OutputLine("{0}", this.dgvFile.Rows[0].Cells[0].GetType());
            DataGridViewRow dgvr = new DataGridViewRow();
            DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
            cell0.Value = (0 + 1).ToString();
            DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
            cell1.Value = name;
            dgvr.Cells.Add(cell0);
            dgvr.Cells.Add(cell1);
            this.dgvFile.Rows.Insert(0, dgvr);
            this.dgvFile.Rows[0].Selected = true;
            this.dgvFile.CurrentCell = this.dgvFile.Rows[0].Cells[1];
            this.dgvFile.Rows[0].Cells[1].ToolTipText = GetModeDataProviderToolTipText(this.mdpList[0]);
            for (int i = 1; i < this.dgvFile.Rows.Count; ++i)
            {
                this.dgvFile.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
            this.dgvFile.FirstDisplayedScrollingRowIndex = 0;
            UpdateNewVScrollbar();
        }

        private void dgvFile_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if(e.Action == DragAction.Cancel || e.Action == DragAction.Drop)
            {
                readyToDragDrop = false;
                //Log.OutputLine("dgvFile_QueryContinueDrag()...{0}, {1}", this.readyToDragDrop, e.Action);
            }
        }

        private int FindModeDataFormCustomMode(string name)
        {
            return this.mdpList.FindIndex(delegate(MEffectorModeDataProvider md)
                {
                    if (md.isCustomData)
                    {
                        MCustomModeProvider cmp = md as MCustomModeProvider;

                        if(cmp != null)
                        {
                            return cmp.name == name && cmp.mainModel == currModelPresetFolder;
                        }
                    }

                    return false;
                }
            );
        }

        private void textBoxFileName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SaveDataToLocalFile();
            }
        }

        private static Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.title = localization[Localization.panelSystem_DataBackupMgt];
            this.dgvFile.Columns[0].HeaderCell.Value = localization[Localization.panelSystem_DataBackupMgt_Number];
            this.dgvFile.Columns[1].HeaderCell.Value = localization[Localization.panelSystem_DataBackupMgt_DataName];
            this.lblBackupFileName.Text = localization[Localization.panelSystem_DataBackupMgt_BackupFileName];
            this.btnSave.Text = localization[Localization.panelSystem_DataBackupMgt_Save];
            this.btnLoad.Text = localization[Localization.panelSystem_DataBackupMgt_Load];
            this.tsmiLoadData.Text = localization[Localization.panelSystem_DataBackupMgt_LoadData];
            this.tsmiOpenFileLocation.Text = localization[Localization.panelSystem_DataBackupMgt_OpenFileLocation];
            this.tsmiDelete.Text = localization[Localization.panelSystem_DataBackupMgt_Delete];

            for (int i = 0; i < this.dgvFile.Rows.Count; ++i )
            {
                this.dgvFile.Rows[i].Cells[1].ToolTipText = GetModeDataProviderToolTipText(mdpList[i]);
            }

            this.Invalidate();
        }

        //public MCustomModeProvider[] GetCustomModeProviderList(string model)
        //{
        //    List<string> fileNamelist = this.customModeDataFileManager.GetModeFileList(model);
        //    List<MCustomModeProvider> customModeList = new List<MCustomModeProvider>();

        //    foreach (string name in fileNamelist)
        //    {
        //        string filePath = this.GetCurrModeFileFullPath(name);
        //        customModeList.Add(new MCustomModeProvider(name, filePath));
        //    }

        //    return customModeList.ToArray();
        //}

        public static MFactoryModeProvider[] GetFactoryModeProviderList(DeviceModel model)
        {
            Dictionary<string, byte[]> currFactoryModeList = null;
            if(model == DeviceModel.M_Audio)
            {
                model = ProductsConfig.MAudioCorrespondingModel;
            }

            if (!ProductsConfig.ProductIsMengBrand(model))
            {
                return null;
            }

            switch (model)
            {
                case DeviceModel.X5:
                case DeviceModel.X5W:
                case DeviceModel.X5Pro:
                case DeviceModel.X5OPBT:
                    currFactoryModeList = FactoryPresetsModeData.x5FactoryModeList;
                    break;

                case DeviceModel.VHi:
                    currFactoryModeList = FactoryPresetsModeData.vhiFactoryModeList;
                    break;

                case DeviceModel.V0:
                    currFactoryModeList = FactoryPresetsModeData.v0FactoryModeList;
                    break;

                case DeviceModel.V5:
                    currFactoryModeList = FactoryPresetsModeData.v5FactoryModeList;
                    break;

                case DeviceModel.E300:
                    currFactoryModeList = FactoryPresetsModeData.e300FactoryModeList;
                    break;

                case DeviceModel.M5Pro:
                    currFactoryModeList = FactoryPresetsModeData.m5ProFactoryModeList;
                    break;

                case DeviceModel.K320:
                case DeviceModel.K220:
                case DeviceModel.A300:
                    currFactoryModeList = FactoryPresetsModeData.vkxFactoryModeList;
                    break;

                case DeviceModel.VK3D:
                case DeviceModel.VK3BT:
                case DeviceModel.VK8:
                case DeviceModel.VK5D:
                    break;

                case DeviceModel.VK6S:
                case DeviceModel.VK6WS:
                case DeviceModel.VK6GS:
                case DeviceModel.VK8S:
                case DeviceModel.Q2:
                case DeviceModel.Q3:
                    break;

                case DeviceModel.VK8Pro:
                case DeviceModel.VK6Pro:
                    break;

                case DeviceModel.VK8SPro:
                case DeviceModel.VK6SPro:
                    break;

                case DeviceModel.VK5S:
                
                case DeviceModel.VK3S:
                case DeviceModel.VK3I:
                case DeviceModel.VK3W:
                case DeviceModel.VK3W_WithoutBT:
                    currFactoryModeList = FactoryPresetsModeData.vkxSFactoryModeList;
                    break;

                case DeviceModel.VK3WDM:
                    currFactoryModeList = FactoryPresetsModeData.vkxdmFactoryModeList;
                    break;

                default:
                    throw new Exception("未知效果器型号!");
            }

            if (currFactoryModeList == null)
            {
                return null;
            }

            List<MFactoryModeProvider> list = new List<MFactoryModeProvider>();
            foreach (string name in currFactoryModeList.Keys)
            {
                list.Add(new MFactoryModeProvider(name, currFactoryModeList[name]));
            }

            return list.ToArray();
        }

        private void textBoxFileName_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = !string.IsNullOrEmpty(this.textBoxFileName.Text.Trim());
        }

#if COMM_TEST
        public readonly static Random random = new Random();

        public void RandLoadDataToDevice()
        {
            int rowIndex = random.Next(this.dgvFile.Rows.Count);
            this.dgvFile.Rows[rowIndex].Selected = true;
            this.LoadDataToDevice();
            //AEC.Log.OutputLine("随机上传模式:{0}", rowIndex);
        }
#endif
    }
}
