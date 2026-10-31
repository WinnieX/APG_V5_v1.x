using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Common;
using AEC.UI.Controls;
using AEC.UI.Common;
using AEC.Misc;

namespace AEC.UI
{
    [ToolboxItem(true), DefaultEvent("CheckedChanged")]
    public partial class NavigationBarBase : UserControl
    {
        public static readonly Color muteColor = Color.FromArgb(239, 83, 80);
        protected PageItem _pageItem = PageItem.Music;

        private MController mController;
        private EffectorModelBase currEffectorModel = null;

        protected Dictionary<MRadioButton, PageItem> rdo2ItemMap;
        protected Dictionary<PageItem, MRadioButton> item2RdoMap;

        public event EventHandler<EventArgs> CheckedChanged;
        
        private EventBus mainEventBus = null;
        protected Localization localization = Config.GetInstance().localization;

        public PageItem pageItem
        {
            get
            {
                return _pageItem;
            }
            set
            {
                if (_pageItem != value)
                {
                    _pageItem = value;
                    item2RdoMap[_pageItem].IsCheckedInit = true;

                    if (CheckedChanged != null)
                    {
                        CheckedChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public PageItem initPageItem
        {
            get
            {
                return _pageItem;
            }
            set
            {
                if (_pageItem != value)
                {
                    _pageItem = value;
                    item2RdoMap[_pageItem].IsCheckedInit = true;
                }
            }
        }

        public NavigationBarBase()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
        }

        protected virtual void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
        }

        public virtual void Init()
        {
            this.InitFont();
        }

        public void Binding(MController mController_, EventBus eventBus_)
        {
            this.mController = mController_;
            this.mainEventBus = eventBus_;

            this.mainEventBus.RegisterHandler(MuteChangedHandler);

            this.currEffectorModel = this.mController.currEffectorModel;

            this.UpdateUIFromData();

            this.UpdateDisplayLanguage();
        }

        private void MuteChangedHandler(object sender, EventBusArgs e)
        {
            if (e.type == EventType.OutputMuteChangedFromPanel)
            {
                switch ((e as OutputMuteChangedFromPanelArgs).channel)
                {
                    case OutputChannelType.MainOutput:
                        this.UpdateMainOutputBtn();
                        break;

                    case OutputChannelType.Aux1Output:
                        this.UpdateAux1OutputBtn();
                        break;

                    case OutputChannelType.Aux2Output:
                        this.UpdateAux2OutputBtn();
                        break;

                    case OutputChannelType.SurOutput:
                        this.UpdateSurOutputBtn();
                        break;
                }
            }
        }

        public void UpdateUIFromData()
        {
            if (this.currEffectorModel == null)
            {
                return;
            }

            this.UpdateMainOutputBtn();
            this.UpdateAux1OutputBtn();
            this.UpdateAux2OutputBtn();
            this.UpdateSurOutputBtn();
        }

        protected void NavigationBar_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.currEffectorModel == null || this.mainEventBus == null || this.item2RdoMap == null)
            {
                return;
            }

            if(!this.item2RdoMap.ContainsKey(PageItem.MainOutput) ||
               !this.item2RdoMap.ContainsKey(PageItem.Aux1Output) ||
               !this.item2RdoMap.ContainsKey(PageItem.Aux2Output) ||
               !this.item2RdoMap.ContainsKey(PageItem.SurOutput))
            {
                return;
            }

            gtRadioButton rdo = sender as gtRadioButton;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (sender.Equals(item2RdoMap[PageItem.MainOutput]))
                {
                    this.currEffectorModel.mainOutputIsMute = !this.currEffectorModel.mainOutputIsMute;
                    this.mainEventBus.NotifyObeserver(new OutputMuteChangedFromNavigatorArgs(OutputChannelType.MainOutput));
                    this.UpdateMainOutputBtn();
                }
                else if (sender.Equals(item2RdoMap[PageItem.Aux1Output]))
                {
                    this.currEffectorModel.aux1OutputIsMute = !this.currEffectorModel.aux1OutputIsMute;
                    this.mainEventBus.NotifyObeserver(new OutputMuteChangedFromNavigatorArgs(OutputChannelType.Aux1Output));
                    this.UpdateAux1OutputBtn();
                }
                else if (sender.Equals(item2RdoMap[PageItem.Aux2Output]))
                {
                    this.currEffectorModel.aux2OutputIsMute = !this.currEffectorModel.aux2OutputIsMute;
                    this.mainEventBus.NotifyObeserver(new OutputMuteChangedFromNavigatorArgs(OutputChannelType.Aux2Output));
                    this.UpdateAux2OutputBtn();
                }
                else if (sender.Equals(item2RdoMap[PageItem.SurOutput]))
                {
                    this.currEffectorModel.surOutputIsMute = !this.currEffectorModel.surOutputIsMute;
                    this.mainEventBus.NotifyObeserver(new OutputMuteChangedFromNavigatorArgs(OutputChannelType.SurOutput));
                    this.UpdateSurOutputBtn();
                }
            }
        }

        private void UpdateMainOutputBtn()
        {
            if(this.item2RdoMap != null && this.item2RdoMap.ContainsKey(PageItem.MainOutput))
            {
                this.item2RdoMap[PageItem.MainOutput].ForeColor = this.currEffectorModel.mainOutputIsMute ? muteColor : Color.FromArgb(32,32,32);
            }
        }

        private void UpdateAux1OutputBtn()
        {
            if (this.item2RdoMap != null && this.item2RdoMap.ContainsKey(PageItem.Aux1Output))
            {
                this.item2RdoMap[PageItem.Aux1Output].ForeColor = this.currEffectorModel.aux1OutputIsMute ? muteColor : Color.FromArgb(32, 32, 32);
            }
        }

        private void UpdateAux2OutputBtn()
        {
            if (this.item2RdoMap != null && this.item2RdoMap.ContainsKey(PageItem.Aux2Output))
            {
                //Log.OutputLine("Navigation: sub mute：{0}", this.currEffectorModel.aux2OutputIsMute);
                this.item2RdoMap[PageItem.Aux2Output].ForeColor = this.currEffectorModel.aux2OutputIsMute ? muteColor : Color.FromArgb(32, 32, 32);
            }
        }

        private void UpdateSurOutputBtn()
        {
            if (this.item2RdoMap != null && this.item2RdoMap.ContainsKey(PageItem.SurOutput))
            {
                this.item2RdoMap[PageItem.SurOutput].ForeColor = this.currEffectorModel.surOutputIsMute ? muteColor : Color.FromArgb(32, 32, 32);
            }
        }

        protected void rdo_CheckedChanged(object sender, EventArgs e)
        {
            MRadioButton rdo = sender as MRadioButton;
            if (rdo.IsChecked && rdo2ItemMap.ContainsKey(rdo))
            {
                _pageItem = rdo2ItemMap[rdo];
                if (CheckedChanged != null)
                {
                    CheckedChanged(this, EventArgs.Empty);
                }
            }
        }

        public virtual void UpdateDisplayLanguage()
        {

        }
    }
}
