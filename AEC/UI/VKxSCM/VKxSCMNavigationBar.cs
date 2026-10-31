using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Controls;
using AEC.Misc;

namespace AEC.UI.VKxSCM
{
    public partial class VKxSCMNavigationBar : AEC.UI.NavigationBarBase
    {
        //解锁位置
        private static readonly Point btnEchoUnlockLocation = new Point(170, 1);
        private static readonly Point btnReverbUnlockLocation = new Point(214, 1);
        private static readonly Point btnMainOutputUnlockLocation = new Point(262, 1);
        private static readonly Point btnAux1OutputUnlockLocation = new Point(330, 1);
        private static readonly Point btnAux2OutputUnlockLocation = new Point(403, 1);
        private static readonly Point btnSurOutputUnlockLocation = new Point(476, 1);
        private static readonly Point btnSystemUnlockLocation = new Point(549, 1);

        //锁定位置
        private static readonly Point btnEchoLockLocation = new Point(102, 1);
        private static readonly Point btnReverbLockLocation = new Point(146, 1);
        private static readonly Point btnMainOutputLockLocation = new Point(194, 1);
        private static readonly Point btnAux1OutputLockLocation = new Point(262, 1);
        private static readonly Point btnAux2OutputLockLocation = new Point(335, 1);
        private static readonly Point btnSurOutputLockLocation = new Point(408, 1);
        private static readonly Point btnSystemLockLocation = new Point(481, 1);


        public bool isLockMicVT
        {
            get
            {
                return !this.rdoMicExciter.Visible;
            }
            set
            {
                if (value)
                {
                    if (this.pageItem == PageItem.MicExciter)
                    {
                        this.pageItem = PageItem.Mic;
                    }

                    this.rdoMicExciter.Visible = !value;

                    this.rdoEcho.Location = btnEchoLockLocation;
                    this.rdoReverb.Location = btnReverbLockLocation;
                    this.rdoMainOutput.Location = btnMainOutputLockLocation;
                    this.rdoAux1Output.Location = btnAux1OutputLockLocation;
                    this.rdoAux2Output.Location = btnAux2OutputLockLocation;
                    this.rdoSurOutput.Location = btnSurOutputLockLocation;
                    this.rdoSystem.Location = btnSystemLockLocation;

                }
                else
                {
                    this.rdoMicExciter.Visible = !value;

                    this.rdoEcho.Location = btnEchoUnlockLocation;
                    this.rdoReverb.Location = btnReverbUnlockLocation;
                    this.rdoMainOutput.Location = btnMainOutputUnlockLocation;
                    this.rdoAux1Output.Location = btnAux1OutputUnlockLocation;
                    this.rdoAux2Output.Location = btnAux2OutputUnlockLocation;
                    this.rdoSurOutput.Location = btnSurOutputUnlockLocation;
                    this.rdoSystem.Location = btnSystemUnlockLocation;
                }
            }
        }

        public VKxSCMNavigationBar()
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
                {PageItem.Mic, this.rdoMic},
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
                {this.rdoMic, PageItem.Mic},
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
            this.rdoMic.Text = localization[Localization.formProMode_Mic];
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
    }
}
