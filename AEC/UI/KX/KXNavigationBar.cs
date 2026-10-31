using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Controls;
using AEC.Misc;

namespace AEC.UI.KX
{
    public partial class KXNavigationBar : AEC.UI.NavigationBarBase
    {
        public KXNavigationBar()
        {
            InitializeComponent();
            this.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Location = new System.Drawing.Point(2, 2);
            this.Margin = new System.Windows.Forms.Padding(2);
        }

        protected override void InitFont()
        {
            this.Font = FontHelper.CreateFont10();

            this.rdoMusic.Font = FontHelper.CreateFont10();
            this.rdoMic.Font = FontHelper.CreateFont10();
            this.rdoEffect.Font = FontHelper.CreateFont10();
            this.rdoMainOutput.Font = FontHelper.CreateFont10();
            this.rdoAux1Output.Font = FontHelper.CreateFont10();
            this.rdoAux2Output.Font = FontHelper.CreateFont10();
            this.rdoSurOutput.Font = FontHelper.CreateFont10();
            this.rdoSystem.Font = FontHelper.CreateFont10();
            this.rdoMonitor.Font = FontHelper.CreateFont10();
        }

        public override void Init()
        {
            base.Init();

            this.item2RdoMap = new Dictionary<PageItem, MRadioButton>()
            {
                {PageItem.Music, this.rdoMusic},
                {PageItem.Mic, this.rdoMic},
                {PageItem.Effect,  this.rdoEffect},
                {PageItem.MainOutput, this.rdoMainOutput},
                {PageItem.Aux1Output, this.rdoAux1Output},
                {PageItem.Aux2Output, this.rdoAux2Output},
                {PageItem.SurOutput, this.rdoSurOutput},
                {PageItem.System, this.rdoSystem},
                {PageItem.Monitor, this.rdoMonitor},
            };

            this.rdo2ItemMap = new Dictionary<MRadioButton, PageItem>()
            {
                {this.rdoMusic, PageItem.Music},
                {this.rdoMic, PageItem.Mic},
                {this.rdoEffect, PageItem.Effect},
                {this.rdoMainOutput, PageItem.MainOutput},
                {this.rdoAux1Output, PageItem.Aux1Output},
                {this.rdoAux2Output, PageItem.Aux2Output},
                {this.rdoSurOutput, PageItem.SurOutput},
                {this.rdoSystem, PageItem.System},
                {this.rdoMonitor, PageItem.Monitor},
            };

            if (this.item2RdoMap.ContainsKey(_pageItem))
            {
                this.item2RdoMap[_pageItem].IsCheckedInit = true;
            }

            foreach (MRadioButton rdo in this.rdo2ItemMap.Keys)
            {
                rdo.CheckedChanged += base.rdo_CheckedChanged;
            }

            this.item2RdoMap[PageItem.MainOutput].MouseClick += NavigationBar_MouseClick;
            this.item2RdoMap[PageItem.Aux1Output].MouseClick += NavigationBar_MouseClick;
            this.item2RdoMap[PageItem.Aux2Output].MouseClick += NavigationBar_MouseClick;
            this.item2RdoMap[PageItem.SurOutput].MouseClick += NavigationBar_MouseClick;
        }

        public override void UpdateDisplayLanguage()
        {
            this.rdoMusic.Text = localization[Localization.formProMode_Music];
            this.rdoMic.Text = localization[Localization.formProMode_Mic];
            this.rdoEffect.Text = localization[Localization.formProMode_Effect];
            this.rdoMainOutput.Text = localization[Localization.formProMode_MainOutput];
            this.rdoAux1Output.Text = localization[Localization.formProMode_Aux1Output];
            this.rdoAux2Output.Text = localization[Localization.formProMode_Aux2Output];
            this.rdoSurOutput.Text = localization[Localization.formProMode_SurOutput];
            this.rdoSystem.Text = localization[Localization.formProMode_System];
            this.rdoMonitor.Text = localization[Localization.formProMode_Monitor];
            this.Invalidate();
            this.Update();
        }
    }
}
