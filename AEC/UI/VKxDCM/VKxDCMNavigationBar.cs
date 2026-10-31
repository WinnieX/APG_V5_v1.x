using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Controls;
using AEC.Misc;

namespace AEC.UI.VKxDCM
{
    public partial class VKxDCMNavigationBar : AEC.UI.NavigationBarBase
    {
        public VKxDCMNavigationBar()
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
            this.rdoMicA.Font = FontHelper.CreateFont10();
            this.rdoMicB.Font = FontHelper.CreateFont10();
            this.rdoMicExciter.Font = FontHelper.CreateFont10();
            this.rdoEcho.Font = FontHelper.CreateFont10();
            this.rdoReverb.Font = FontHelper.CreateFont10();
            this.rdoMainOutput.Font = FontHelper.CreateFont10();
            this.rdoAux1Output.Font = FontHelper.CreateFont10();
            this.rdoAux2Output.Font = FontHelper.CreateFont10();
            this.rdoSurOutput.Font = FontHelper.CreateFont10();
            this.rdoSystem.Font = FontHelper.CreateFont10();
        }

        public override void Init()
        {
            base.Init();

            this.item2RdoMap = new Dictionary<PageItem, MRadioButton>()
            {
                {PageItem.Music, this.rdoMusic},
                {PageItem.MicA, this.rdoMicA},
                {PageItem.MicB, this.rdoMicB},
                {PageItem.MicExciter, this.rdoMicExciter},
                {PageItem.Echo,  this.rdoEcho},
                {PageItem.Reverb,  this.rdoReverb},
                {PageItem.MainOutput, this.rdoMainOutput},
                {PageItem.Aux1Output, this.rdoAux1Output},
                {PageItem.Aux2Output, this.rdoAux2Output},
                {PageItem.SurOutput, this.rdoSurOutput},
                {PageItem.System, this.rdoSystem},
            };

            this.rdo2ItemMap = new Dictionary<MRadioButton, PageItem>()
            {
                {this.rdoMusic, PageItem.Music},
                {this.rdoMicA, PageItem.MicA},
                {this.rdoMicB, PageItem.MicB},
                {this.rdoMicExciter, PageItem.MicExciter},
                {this.rdoEcho, PageItem.Echo},
                {this.rdoReverb, PageItem.Reverb},
                {this.rdoMainOutput, PageItem.MainOutput},
                {this.rdoAux1Output, PageItem.Aux1Output},
                {this.rdoAux2Output, PageItem.Aux2Output},
                {this.rdoSurOutput, PageItem.SurOutput},
                {this.rdoSystem, PageItem.System},
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
            if (isLinked)
            {
                this.rdoMicA.Text = localization[Localization.formProMode_MicAPlusB];
                this.rdoMicB.Text = localization[Localization.formProMode_MicAPlusB];
            }
            else
            {
                this.rdoMicA.Text = localization[Localization.formProMode_MicA];
                this.rdoMicB.Text = localization[Localization.formProMode_MicB];
            }
            
            this.rdoMicExciter.Text = localization[Localization.formProMode_MicExciter];
            this.rdoEcho.Text = localization[Localization.formProMode_Echo];
            this.rdoReverb.Text = localization[Localization.formProMode_Reverb];
            this.rdoMainOutput.Text = localization[Localization.formProMode_MainOutput];
            this.rdoAux1Output.Text = localization[Localization.formProMode_CenterOutput];
            this.rdoAux2Output.Text = localization[Localization.formProMode_SubOutput];
            this.rdoSurOutput.Text = localization[Localization.formProMode_SurOutput];
            this.rdoSystem.Text = localization[Localization.formProMode_System];
            this.Invalidate();
            this.Update();
        }

        private bool isLinked { get; set; }

        public void LInkMicAB(Core.MicChannel channel)
        {
            System.Diagnostics.Debug.Assert((channel == Core.MicChannel.MicA && rdoMicA.IsChecked) || (channel == Core.MicChannel.MicB && rdoMicB.IsChecked));

            isLinked = true;

            if (channel == Core.MicChannel.MicA)
            {
                this.rdoMicA.Location = new Point(47, 1);
                this.rdoMicA.Size = new Size(56 + 16, 29);
                this.rdoMicA.Visible = true;
                this.rdoMicB.Visible = false;
            }
            else
            {
                this.rdoMicA.Visible = false;
                this.rdoMicB.Visible = true;
                this.rdoMicB.Location = new Point(47, 1);
                this.rdoMicB.Size = new Size(56 + 16, 29);
            }

            this.rdoMicA.Text = localization[Localization.formProMode_MicAPlusB];
            this.rdoMicB.Text = localization[Localization.formProMode_MicAPlusB];

            this.rdoMicExciter.Location = new Point(166 - 60 + 16, 1);
            this.rdoEcho.Location = new Point(234 - 60 + 16, 1);
            this.rdoReverb.Location = new Point(278 - 60 + 16, 1);
            this.rdoMainOutput.Location = new Point(326 - 60 + 16, 1);
            this.rdoAux1Output.Location = new Point(394 - 60 + 16, 1);
            this.rdoAux2Output.Location = new Point(467 - 60 + 16, 1);
            this.rdoSurOutput.Location = new Point(540 - 60 + 16, 1);
            this.rdoSystem.Location = new Point(613 - 60 + 16, 1);

            this.Invalidate();
            this.Update();
        }

        public void UnlinkMicAB()
        {
            isLinked = false;

            this.rdoMicA.Visible = true;
            this.rdoMicB.Visible = true;
            this.rdoMicA.Text = localization[Localization.formProMode_MicA];
            this.rdoMicB.Text = localization[Localization.formProMode_MicB];
            this.rdoMicA.Size = new Size(56, 29);
            this.rdoMicB.Size = new Size(56, 29);
            this.rdoMicA.Location = new Point(47, 1);
            this.rdoMicB.Location = new Point(107, 1);
            this.rdoMicExciter.Location = new Point(166, 1);
            this.rdoEcho.Location = new Point(234, 1);
            this.rdoReverb.Location = new Point(278, 1);
            this.rdoMainOutput.Location = new Point(326, 1);
            this.rdoAux1Output.Location = new Point(394, 1);
            this.rdoAux2Output.Location = new Point(467, 1);
            this.rdoSurOutput.Location = new Point(540, 1);
            this.rdoSystem.Location = new Point(613, 1);

            this.Invalidate();
            this.Update();
        }
    }
}
