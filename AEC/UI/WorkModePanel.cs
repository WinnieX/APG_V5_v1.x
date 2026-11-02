using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Common;
using AEC.Misc;

namespace AEC.UI
{
    public partial class WorkModePanel : UserControl
    {
        //public WorkMode workMode_;
        //public WorkSelect workSelect_;
        //private ShortParam workMode;
        //private ShortParam workSelect;
        //private MusicModuleBase music;
        private AutoWorkMode autoWorkMode;
        private EventBus eventBus;
        public Color checkedColor = Color.FromArgb(32, 32, 64);

        public WorkModePanel()
        {
            InitializeComponent();
            InitFont();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.UpdateStyles();
            this.rdoManual.MouseClick += rdo_MouseClick;
            this.rdoAuto.MouseClick += rdo_MouseClick;
            this.rdoSing.MouseClick += rdo_MouseClick;
            this.rdoDisco.MouseClick += rdo_MouseClick;

            this.ForeColorChanged += WorkModePanel_ForeColorChanged;
        }

        void WorkModePanel_ForeColorChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void InitFont()
        {
            this.rdoManual.Font = FontHelper.CreateFont10();
            this.rdoAuto.Font = FontHelper.CreateFont10();
            this.rdoSing.Font = FontHelper.CreateFont10();
            this.rdoDisco.Font = FontHelper.CreateFont10();
        }

        public void Init(AutoWorkMode autoWorkMode_, EventBus eventBus_, Color checkedColor_)
        {
            this.autoWorkMode = autoWorkMode_;
            this.eventBus = eventBus_;
            this.checkedColor = checkedColor_;

            this.UpdateWrokModeRdoState();
            this.UpdateWrokSelectRdoState();

            if(this.eventBus != null)
            {
                this.eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
            }
        }

        public void UpdateUIFromData()
        {
            this.UpdateWrokModeRdoState();
            this.UpdateWrokSelectRdoState();
        }

        private void UpdateWrokModeRdoState()
        {
            if (this.autoWorkMode.workMode == WorkMode.Manual)
            {
                this.rdoManual.ForeColor = this.checkedColor;
                this.rdoAuto.ForeColor = this.ForeColor;
                this.rdoManual.isCheckInit = true;
            }
            else
            {
                this.rdoAuto.ForeColor = this.checkedColor;
                this.rdoManual.ForeColor = this.ForeColor;
                this.rdoAuto.isCheckInit = true;
            }
            this.panelWrokSelect.Enabled = this.autoWorkMode.workMode == WorkMode.Manual;
        }

        private void UpdateWrokSelectRdoState()
        {
            if (this.autoWorkMode.workSelect == WorkSelect.Sing)
            {
                this.rdoSing.ForeColor = this.checkedColor;
                this.rdoDisco.ForeColor = this.ForeColor;
                this.rdoSing.isCheckInit = true;
            }
            else
            {
                this.rdoDisco.ForeColor = this.checkedColor;
                this.rdoSing.ForeColor = this.ForeColor;
                this.rdoDisco.isCheckInit = true;
            }
        }

        private void rdo_MouseClick(object sender, EventArgs e)
        {
            gtRadioButton rdo = sender as gtRadioButton;
            if(rdo.isChecked)
            {
                return;
            }

            if (rdo.Equals(this.rdoManual))
            {
                this.autoWorkMode.workMode = WorkMode.Manual;
                this.autoWorkMode.ws.UpdateParam();     //当切换回手动模式时，需要强制发送一次work select数据
                this.FireWorkModeChanged();
            }
            else if (rdo.Equals(this.rdoAuto))
            {
                this.autoWorkMode.workMode = WorkMode.Auto;
                this.FireWorkModeChanged();
            }
            else if (rdo.Equals(this.rdoSing))
            {
                this.autoWorkMode.workSelect = WorkSelect.Sing;
                this.FireWorkSelectChanged();
            }
            else if (rdo.Equals(this.rdoDisco))
            {
                this.autoWorkMode.workSelect = WorkSelect.Disco;
                this.FireWorkSelectChanged();
            }
        }

        private void FireWorkModeChanged()
        {
            if (this.eventBus != null)
            {
                this.eventBus.NotifyObeserver(new WorkModeChangedArgs());
            }
        }

        private void FireWorkSelectChanged()
        {
            if (this.eventBus != null)
            {
                this.eventBus.NotifyObeserver(new WorkSelectChangedArgs());
            }
        }

        private void WorkModeOrWorkSelectChanged(object sender, EventBusArgs e)
        {
            if(e.type == EventType.WorkModeChanged)
            {
                UpdateWrokModeRdoState();
            }
            else if(e.type == EventType.WorkSelectChanged)
            {
                UpdateWrokSelectRdoState();
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.rdoManual.Text = localization[Localization.manualMode];
            this.rdoAuto.Text = localization[Localization.autoMode];
            this.rdoSing.Text = localization[Localization.singMode];
            this.rdoDisco.Text = localization[Localization.discoMode];
        }
    }
}
