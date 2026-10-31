using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using AEC.AudioProducts;
using AEC.Misc;

namespace AEC.UI
{
    [ToolboxItem(true), DefaultEvent("SelectedChanged")]
    public partial class SelectAudioSystemProduct : UserControl
    {
        //public const string all = "所有";

        public delegate void VoidCallback();

        private List<IGetAudioSystem> getAudioSystemList = new List<IGetAudioSystem>();
        //private List<AudioSystem> filteredAudioSystemList = new List<AudioSystem>();
        private Dictionary<gtComboBox, VoidCallback> cmbMap2Callback = null;
        private Dictionary<LinkLabel, VoidCallback> linkLabelMap2Callback = null;

        public IGetAudioSystem selectedGetAudioSystem;
        public event EventHandler SelectedChanged;

        private SynchronizationContext syncContext;

        private AudioProductManagement apm = AudioProductManagement.GetInstance();

        public SelectAudioSystemProduct()
        {
            InitializeComponent();
            InitFont();

            this.syncContext = WindowsFormsSynchronizationContext.Current;

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.cmbMap2Callback = new Dictionary<gtComboBox, VoidCallback>()
            {
                {cmbLSBrand, LSBrandChanged},
                {cmbLSModel, LSModelChanged},
                {cmbLSPower, LSPowerChagned},
                {cmbPABrand, PABrandChanged},
                {cmbPAModel, PAModelChanged},
                {cmbPAPower, PAPowerChagned},
                {cmbMicBrand, MicBrandChanged},
                {cmbMicModel, MicModelChanged},
            };

            this.linkLabelMap2Callback = new Dictionary<LinkLabel, VoidCallback>()
            {
                {linkLabelClearLS, linkLabelLSClicked},
                {linkLabelClearPA, linkLabelPAClicked},
                {linkLabelClearMic, linkLabelMicClicked},
            };

            this.Init();

            this.audioSystemListBox.selectedItemChanged += audioSystemListBox_selectedItemChanged;
            
            this.audioSystemListBox.MouseWheel += audioSystemListBox_MouseWheel;
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.panelWithTitle.Font = FontHelper.CreateFont14();

            this.lblLS.Font = FontHelper.CreateFont10();
            this.linkLabelClearLS.Font = FontHelper.CreateFont10();
            this.lblPA.Font = FontHelper.CreateFont10();
            this.linkLabelClearPA.Font = FontHelper.CreateFont10();
            this.lblMic.Font = FontHelper.CreateFont10();
            this.linkLabelClearMic.Font = FontHelper.CreateFont10();

            //this.cmbLSBrand.Font = FontHelper.CreateFont10();
            //this.cmbLSModel.Font = FontHelper.CreateFont10();
            //this.cmbLSPower.Font = FontHelper.CreateFont10();
            //this.cmbPABrand.Font = FontHelper.CreateFont10();
            //this.cmbPAModel.Font = FontHelper.CreateFont10();
            //this.cmbPAPower.Font = FontHelper.CreateFont10();
            //this.cmbMicBrand.Font = FontHelper.CreateFont10();
            //this.cmbMicModel.Font = FontHelper.CreateFont10();

            this.audioSystemListBox.Font = FontHelper.CreateFont10();
            this.lblSelectedItem.Font = FontHelper.CreateFont10();
        }

        private void Init()
        {
            this.InitCmbLS();
            this.InitCmbPA();
            this.InitCmbMic();

            
            //cmbPABrand.SelectedIndex = 0;
            //cmbMicBrand.SelectedIndex = 0;
        }

        private void InitCmbLS()
        {
            this.cmbLSBrand.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbLSModel.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbLSPower.SelectedIndexChanged -= cmb_SelectedIndexChanged;

            cmbLSBrand.Items.Clear();
            cmbLSModel.Items.Clear();
            cmbLSPower.Items.Clear();

            cmbLSBrand.Items.Add(localization[Localization.panelSystemSelection_All]);
            cmbLSBrand.Items.AddRange(apm.LSBrands);

            cmbLSModel.Items.Add(localization[Localization.panelSystemSelection_All]);

            cmbLSPower.Items.Add(localization[Localization.panelSystemSelection_All]);
            cmbLSPower.Items.AddRange(apm.LSPowers);
            
            cmbLSBrand.SelectedIndex = 0;
            cmbLSModel.SelectedIndex = 0;
            cmbLSPower.SelectedIndex = 0;

            this.cmbLSBrand.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbLSModel.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbLSPower.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        private void InitCmbPA()
        {
            this.cmbPABrand.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbPAModel.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbPAPower.SelectedIndexChanged -= cmb_SelectedIndexChanged;

            cmbPABrand.Items.Clear();
            cmbPAModel.Items.Clear();
            cmbPAPower.Items.Clear();

            cmbPABrand.Items.Add(localization[Localization.panelSystemSelection_All]);
            cmbPABrand.Items.AddRange(apm.PABrands);

            cmbPAModel.Items.Add(localization[Localization.panelSystemSelection_All]);

            cmbPAPower.Items.Add(localization[Localization.panelSystemSelection_All]);
            cmbPAPower.Items.AddRange(apm.PAPowers);

            cmbPABrand.SelectedIndex = 0;
            cmbPAModel.SelectedIndex = 0;
            cmbPAPower.SelectedIndex = 0;

            this.cmbPABrand.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbPAModel.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbPAPower.SelectedIndexChanged += cmb_SelectedIndexChanged;

            this.vScrollbar.Scroll += vScrollbar_Scroll;
        }

        private void InitCmbMic()
        {
            this.cmbMicBrand.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbMicModel.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            
            cmbMicBrand.Items.Clear();
            cmbMicModel.Items.Clear();

            cmbMicBrand.Items.Add(localization[Localization.panelSystemSelection_All]);
            cmbMicBrand.Items.AddRange(apm.MicBrands);

            cmbMicModel.Items.Add(localization[Localization.panelSystemSelection_All]);

            cmbMicBrand.SelectedIndex = 0;
            cmbMicModel.SelectedIndex = 0;

            this.cmbMicBrand.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbMicModel.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        public void InitListBox(IEnumerable<IGetAudioSystem> getAudioSystemCollection)
        {
            getAudioSystemList = new List<IGetAudioSystem>(getAudioSystemCollection);
            this.InitListBox(getAudioSystemList);
            UpdateSelectedItemLabel();
        }

        public void UpdateListBox(IEnumerable<IGetAudioSystem> getAudioSystemCollection)
        {
            getAudioSystemList = new List<IGetAudioSystem>(getAudioSystemCollection);
            this.InitListBox(getAudioSystemList);
            UpdateSelectedItemLabel();
            Init();
            FilterByAll();
        }

        public void Clear()
        {
            this.InitListBox(getAudioSystemList);
            audioSystemListBox.Clear();
            this.Init();
        }

        private void InitListBox(List<IGetAudioSystem> filteredAudioSystemList)
        {
            System.Diagnostics.Debug.Assert(filteredAudioSystemList != null);

            List<AudioSystemItem> audioSystemItemList = new List<AudioSystemItem>();
            foreach (IGetAudioSystem getAudioSystem in filteredAudioSystemList)
            {
                audioSystemItemList.Add(new AudioSystemItem(getAudioSystem));
            }

            audioSystemListBox.Init(audioSystemItemList.ToArray());

            lblNoMatchFound.Visible = audioSystemListBox.Items.Count == 0;

            AsyncUpdateVScrollbar();
        }

        private void audioSystemListBox_selectedItemChanged(object sender, EventArgs e)
        {
            //AEC.Log.OutputLine("audioSystemListBox_selectedItemChanged");
            selectedGetAudioSystem = this.audioSystemListBox.selectedItem != null ? this.audioSystemListBox.selectedItem.getAudioSystem : null;
            UpdateSelectedItemLabel();
            if (SelectedChanged != null)
            {
                SelectedChanged(this, EventArgs.Empty);
            }
        }

        private void UpdateSelectedItemLabel()
        {
            if (selectedGetAudioSystem == null)
            {
                lblSelectedItem.Text = string.Format("{0} : ...", localization[Localization.panelSystemSelection_SelectedItem]);
            }
            else
            {
                AudioSystem audiosystem = selectedGetAudioSystem.GetAudioSystem();
                string baseInfo = string.Format("{0} : {1}   {2}   {3}", localization[Localization.panelSystemSelection_SelectedItem], audiosystem.ls.basicInfo, audiosystem.pa.basicInfo, audiosystem.mic.basicInfo);
                lblSelectedItem.Text = baseInfo;
            }
        }

        //private void UpdateSelectedItem(AudioSystemItem audioSystemItem)
        //{
        //    if(audioSystemItem == null)
        //    {
        //        lblSelectedItem.Text = "...";
        //    }
        //    else
        //    {
        //        AudioSystem audioSystem = audioSystemItem.audioSystem;
        //        string baseInfo = string.Format("{0}   {1}   {2}", audioSystem.ls.basicInfo, audioSystem.pa.basicInfo, audioSystem.mic.basicInfo);
        //        lblSelectedItem.Text = baseInfo;
        //    }
        //}

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if (this.cmbMap2Callback.ContainsKey(cmb))
            {
                this.cmbMap2Callback[cmb]();
            }
        }

        public void audioSystemListBox_MouseWheel(object sender, MouseEventArgs e)
        {
            //AEC.Log.OutputLine("MouseWheel");
            AsyncUpdateVScrollbar();
        }

        public void AsyncUpdateVScrollbar()
        {
            //AEC.Log.OutputLine("AsyncUpdateHScrollbar");
            syncContext.Post(UpdateNewVScrollbar, null);
        }

        private void vScrollbar_Scroll(object sender, EventArgs e)
        {
            UpdateOwnVScrollbar();
        }

        //private int count = 0;
        //private void Test()
        //{
        //    //bool vScrollbarvisible = Win32API.IsVerticalScrollBarVisible(this.audioSystemListBox);

        //    Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
        //    si.cbSize = (uint)Marshal.SizeOf(si);
        //    si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
        //    Win32API.GetScrollInfo(this.audioSystemListBox.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref si);
        //    AEC.Log.OutputLine("SCROLLINFO: min = {0}, max = {1}, page = {2}, pos = {3}, trackPos = {4}, [{5}]", si.nMin, si.nMax, si.nPage, si.nPos, si.nTrackPos, ++count);
        //    AEC.Log.OutputLine("VScroll: min = {0}, max = {1}, largeChange = {2}, value = {3}", this.vScrollbar.Minimum, this.vScrollbar.Maximum, this.vScrollbar.LargeChange, this.vScrollbar.Value);
        //}

        private void UpdateNewVScrollbar(object state)
        {
            this.vScrollbar.Visible = Win32API.IsVerticalScrollBarVisible(this.audioSystemListBox);
            if (this.vScrollbar.Visible)
            {
                Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
                si.cbSize = (uint)Marshal.SizeOf(si);
                si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
                Win32API.GetScrollInfo(this.audioSystemListBox.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref si);
                //AEC.Log.OutputLine("SCROLLINFO: min = {0}, max = {1}, page = {2}, pos = {3}, trackPos = {4}, [{5}]", si.nMin, si.nMax, si.nPage, si.nPos, si.nTrackPos, ++count);
                this.vScrollbar.Maximum = si.nMax + 1;
                this.vScrollbar.LargeChange = (int)si.nPage;
                this.vScrollbar.Value = si.nPos;
            }
        }

        private void UpdateOwnVScrollbar()
        {
            if (!this.vScrollbar.Visible)
            {
                return;
            }

            Win32API.SCROLLINFO si = new Win32API.SCROLLINFO();
            si.cbSize = (uint)Marshal.SizeOf(si);
            si.fMask = (int)(Win32API.ScrollInfoMask.SIF_DISABLENOSCROLL | Win32API.ScrollInfoMask.SIF_ALL);
            Win32API.GetScrollInfo(this.audioSystemListBox.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref si);
            si.nPos = this.vScrollbar.Value;
            si.nTrackPos = this.vScrollbar.Value;
            Win32API.SetScrollInfo(this.audioSystemListBox.Handle, (int)Win32API.ScrollBarDirection.SB_VERT, ref si, true);
            Win32API.PostMessage(audioSystemListBox.Handle, 0x115, 4 + 0x10000 * this.vScrollbar.Value, 0);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Test();
            //return;
            LinkLabel linkLabel = sender as LinkLabel;
            if (this.linkLabelMap2Callback.ContainsKey(linkLabel))
            {
                this.linkLabelMap2Callback[linkLabel]();
            }
        }

        private void LSBrandChanged()
        {
            this.cmbLSModel.SelectedIndexChanged -= cmb_SelectedIndexChanged;

            this.cmbLSModel.Items.Clear();
            this.cmbLSModel.Items.Add(localization[Localization.panelSystemSelection_All]);
            if (this.cmbLSBrand.SelectedIndex > 0)
            {
                this.cmbLSModel.Items.AddRange(apm.GetLSModels(cmbLSBrand.Text));
            }
            this.cmbLSModel.SelectedIndex = 0;
                
            this.cmbLSModel.SelectedIndexChanged += cmb_SelectedIndexChanged;

            FilterByAll();
        }

        private void LSModelChanged()
        {
            if (this.cmbLSModel.SelectedIndex > 0)
            {
                Loudspeaker ls = apm.GetLoudSpeaker(this.cmbLSBrand.Text, this.cmbLSModel.Text);
                if(ls == null)
                {
                    return;
                }

                string power = AudioProductManagement.GetPowerLabel(ls.power);

                if (this.cmbLSPower.SelectedIndex > 0)
                {
                    this.cmbLSPower.SelectedIndexChanged -= cmb_SelectedIndexChanged;
                    this.cmbLSPower.SelectedIndex = 0;
                    this.cmbLSPower.SelectedIndexChanged += cmb_SelectedIndexChanged;
                }

                //if (this.cmbLSPower.Items.Contains(power))
                //{
                //    this.cmbLSPower.SelectedIndexChanged -= cmb_SelectedIndexChanged;
                //    this.cmbLSPower.Text = power;
                //    this.cmbLSPower.SelectedIndexChanged += cmb_SelectedIndexChanged;
                //}
            }

            FilterByAll();
        }

        private void LSPowerChagned()
        {
            FilterByAll();
        }

        private void PABrandChanged()
        {
            this.cmbPAModel.SelectedIndexChanged -= cmb_SelectedIndexChanged;

            this.cmbPAModel.Items.Clear();
            this.cmbPAModel.Items.Add(localization[Localization.panelSystemSelection_All]);
            if (this.cmbPABrand.SelectedIndex > 0)
            {
                this.cmbPAModel.Items.AddRange(apm.GetPAModels(cmbPABrand.Text));
            }
            this.cmbPAModel.SelectedIndex = 0;

            this.cmbPAModel.SelectedIndexChanged += cmb_SelectedIndexChanged;

            FilterByAll();
        }

        private void PAModelChanged()
        {
            if (this.cmbPAModel.SelectedIndex > 0)
            {
                PowerAmplifier pa = apm.GetPowerAmplifier(this.cmbPABrand.Text, this.cmbPAModel.Text);
                if (pa == null)
                {
                    return;
                }

                string power = AudioProductManagement.GetPowerLabel(pa.power);

                if(this.cmbPAPower.SelectedIndex > 0)
                {
                    this.cmbPAPower.SelectedIndexChanged -= cmb_SelectedIndexChanged;
                    this.cmbPAPower.SelectedIndex = 0;
                    this.cmbPAPower.SelectedIndexChanged += cmb_SelectedIndexChanged;
                }
                
                //if (this.cmbPAPower.Items.Contains(power))
                //{
                //    this.cmbPAPower.SelectedIndexChanged -= cmb_SelectedIndexChanged;
                //    this.cmbPAPower.Text = power;
                //    this.cmbPAPower.SelectedIndexChanged += cmb_SelectedIndexChanged;
                //}
            }

            FilterByAll();
        }

        private void PAPowerChagned()
        {
            FilterByAll();
        }

        private void MicBrandChanged()
        {
            this.cmbMicModel.SelectedIndexChanged -= cmb_SelectedIndexChanged;

            this.cmbMicModel.Items.Clear();
            this.cmbMicModel.Items.Add(localization[Localization.panelSystemSelection_All]);

            if (this.cmbMicBrand.SelectedIndex > 0)
            {
                this.cmbMicModel.Items.AddRange(apm.GetMicModels(cmbMicBrand.Text));
            }
            this.cmbMicModel.SelectedIndex = 0;

            this.cmbMicModel.SelectedIndexChanged += cmb_SelectedIndexChanged;

            FilterByAll();
        }

        private void MicModelChanged()
        {
            FilterByAll();
        }

        private void linkLabelLSClicked()
        {
            InitCmbLS();
            FilterByAll();
        }

        private void linkLabelPAClicked()
        {
            InitCmbPA();
            FilterByAll();
        }

        private void linkLabelMicClicked()
        {
            InitCmbMic();
            FilterByAll();
        }

        private void FilterByAll()
        {
            List<IGetAudioSystem> filteredAudioSystemList = new List<IGetAudioSystem>(getAudioSystemList);

            FilterByLS(filteredAudioSystemList);
            FilterByPA(filteredAudioSystemList);
            FilterByMic(filteredAudioSystemList);

            InitListBox(filteredAudioSystemList);
        }

        private void FilterByLS(List<IGetAudioSystem> srcAudioSystemList)
        {
            srcAudioSystemList.RemoveAll(LSBrandIsNotMatch);
            srcAudioSystemList.RemoveAll(LSModelIsNotMatch);
            srcAudioSystemList.RemoveAll(LSPowerIsNotMatch);
        }

        private void FilterByPA(List<IGetAudioSystem> srcAudioSystemList)
        {
            srcAudioSystemList.RemoveAll(PABrandIsNotMatch);
            srcAudioSystemList.RemoveAll(PAModelIsNotMatch);
            srcAudioSystemList.RemoveAll(PAPowerIsNotMatch);
        }

        private void FilterByMic(List<IGetAudioSystem> srcAudioSystemList)
        {
            srcAudioSystemList.RemoveAll(MicBrandIsNotMatch);
            srcAudioSystemList.RemoveAll(MicModelIsNotMatch);
        }

        private bool LSBrandIsNotMatch(IGetAudioSystem getaudioSystem)
        {
            return IsNotMatch(this.cmbLSBrand, getaudioSystem.GetAudioSystem().ls.brand);
        }

        private bool LSModelIsNotMatch(IGetAudioSystem getaudioSystem)
        {
            return IsNotMatch(this.cmbLSModel, getaudioSystem.GetAudioSystem().ls.model);
        }

        private bool LSPowerIsNotMatch(IGetAudioSystem getaudioSystem)
        {
            return IsNotMatch(this.cmbLSPower, AudioProductManagement.GetPowerLabel(getaudioSystem.GetAudioSystem().ls.power));
        }

        private bool PABrandIsNotMatch(IGetAudioSystem getaudioSystem)
        {
            return IsNotMatch(this.cmbPABrand, getaudioSystem.GetAudioSystem().pa.brand);
        }

        private bool PAModelIsNotMatch(IGetAudioSystem getaudioSystem)
        {
            return IsNotMatch(this.cmbPAModel, getaudioSystem.GetAudioSystem().pa.model);
        }

        private bool PAPowerIsNotMatch(IGetAudioSystem getaudioSystem)
        {
            return IsNotMatch(this.cmbPAPower, AudioProductManagement.GetPowerLabel(getaudioSystem.GetAudioSystem().pa.power));
        }

        private bool MicBrandIsNotMatch(IGetAudioSystem getaudioSystem)
        {
            return IsNotMatch(this.cmbMicBrand, getaudioSystem.GetAudioSystem().mic.brand);
        }

        private bool MicModelIsNotMatch(IGetAudioSystem getaudioSystem)
        {
            return IsNotMatch(this.cmbMicModel, getaudioSystem.GetAudioSystem().mic.model);
        }

        private static bool IsNotMatch(gtComboBox cmb, string t)
        {
            System.Diagnostics.Debug.Assert(cmb != null);

            if(cmb.SelectedIndex > 0)
            {
                return t != cmb.Text;
            }
            else
            {
                return false;
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {

            this.panelWithTitle.title = localization[Localization.panelSystemSelection];
            this.lblLS.Text = localization[Localization.panelSystemSelection_Loudspeaker];
            this.lblPA.Text = localization[Localization.panelSystemSelection_PowerAmplifier];
            this.lblMic.Text = localization[Localization.panelSystemSelection_Microphone];
            this.linkLabelClearLS.Text = localization[Localization.panelSystemSelection_Clear];
            this.linkLabelClearPA.Text = localization[Localization.panelSystemSelection_Clear];
            this.linkLabelClearMic.Text = localization[Localization.panelSystemSelection_Clear];
            this.lblLSBrand.Text = localization[Localization.panelSystemSelection_Brand];
            this.lblLSModel.Text = localization[Localization.panelSystemSelection_Model];
            this.lblLSPower.Text = localization[Localization.panelSystemSelection_Power];
            this.lblPABrand.Text = localization[Localization.panelSystemSelection_Brand];
            this.lblPAModel.Text = localization[Localization.panelSystemSelection_Model];
            this.lblPAPower.Text = localization[Localization.panelSystemSelection_Power];
            this.lblMicBrand.Text = localization[Localization.panelSystemSelection_Brand];
            this.lblMicModel.Text = localization[Localization.panelSystemSelection_Model];

            this.cmbLSBrand.Items[0] = localization[Localization.panelSystemSelection_All];
            this.cmbLSModel.Items[0] = localization[Localization.panelSystemSelection_All];
            this.cmbLSPower.Items[0] = localization[Localization.panelSystemSelection_All];
            this.cmbPABrand.Items[0] = localization[Localization.panelSystemSelection_All];
            this.cmbPAModel.Items[0] = localization[Localization.panelSystemSelection_All];
            this.cmbPAPower.Items[0] = localization[Localization.panelSystemSelection_All];
            this.cmbMicBrand.Items[0] = localization[Localization.panelSystemSelection_All];
            this.cmbMicModel.Items[0] = localization[Localization.panelSystemSelection_All];

            this.lblNoMatchFound.Text = localization[Localization.panelSystemSelection_TheExactMatchCouldNotBeFound];

            this.UpdateSelectedItemLabel();
        }
    }
}
