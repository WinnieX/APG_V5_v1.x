using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Common;

namespace AEC.UI
{
    public partial class TimbrePnael : AEC.UI.Controls.MContainerControl
    {
        public TimbrePnael()
        {
            InitializeComponent();

            this.key2EventMap = new Dictionary<Keys, VoidCallback>()
            {
                {Keys.Up, Increase},
                {Keys.Down, Decrease},
                {Keys.Left, Decrease},
                {Keys.Right, Increase},
                {Keys.Add, Increase},
                {Keys.Subtract, Decrease},
            };
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.paramLabel.Font = FontHelper.CreateFont10();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitFont();

            this.paramLabel.Padding = new Padding(0);
            this.paramLabel.isShowUpdownButton = false;

            this.hSlider.ValueChanged += Timbre_ValueChanged;
            this.paramLabel.ValueChanged += Timbre_ValueChanged;
        }

        private delegate void VoidCallback();

        private ShortParam timbreParam;

        private Dictionary<Keys, VoidCallback> key2EventMap;
        private DateTime lastKeydownTimestamp = DateTime.Now;
        private Keys lastKey = Keys.None;

        public short upperLimit
        {
            get
            {
                return this.hSlider.upperLimit;
            }
            set
            {
                //Log.OutputLine("volume panel upperLimit:{0}", value);
                this.hSlider.upperLimit = value;
                this.paramLabel.upperLimit = value;
                //Log.OutputLine("slider value:{0}, label value:{0}", this.hSlider.v, this.volumeLabel.v);
            }
        }

        public bool canBeChanged
        {
            get
            {
                return this.hSlider.canBeChanged;
            }
            set
            {
                this.hSlider.canBeChanged = value;
                this.paramLabel.canBeChanged = value;
            }
        }

        public void Binding(ShortParam timbreParam_)
        {
            Debug.Assert(timbreParam_ != null);
            this.timbreParam = timbreParam_;
            this.hSlider.Init(timbreParam_);
            this.paramLabel.Init(this.timbreParam.max, this.timbreParam.v, ParamGetLabelHelper.GetTimbreLabel);
            
            this.hSlider.UpdateDisplay();

            //Log.OutputLine("当前音色值：{0}, 对应的 波特率为：{1}.", this.timbreParam.v, dsp_level_meter_uart_baud_rate_table[this.timbreParam.v]);
        }

        public void UpdateUIFromData()
        {
            //Log.OutputLine("Volume Panel UpdateUIFromData(), Level:{0} mute:{1}", volumeParam.v, muteParam.value);
            this.hSlider.initV = this.timbreParam.v;
            this.paramLabel.initV = this.timbreParam.v;            
            this.hSlider.UpdateDisplay();

            //Log.OutputLine("当前音色值：{0}, 对应的 波特率为：{1}.", this.timbreParam.v, dsp_level_meter_uart_baud_rate_table[this.timbreParam.v]);
        }

        private void Timbre_ValueChanged(object sender, EventArgs e)
        {
            if (this.timbreParam == null)
            {
                return;
            }

            this.Focus();

            if(sender.Equals(this.hSlider))
            {
                this.paramLabel.initV = this.hSlider.v;
            }
            else if (sender.Equals(this.paramLabel))
            {
                this.hSlider.initV = this.paramLabel.v;
            }

            this.timbreParam.v = this.hSlider.v;

            //Log.OutputLine("音色值：{0}, 对应的 波特率为：{1}.", this.timbreParam.v, dsp_level_meter_uart_baud_rate_table[this.timbreParam.v]);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //AEC.Log.OutputLine("HSliderParam ProcessDialogKey{0}", keyData);
            if (key2EventMap.ContainsKey(keyData))
            {
                if (this.lastKey != keyData || (DateTime.Now - lastKeydownTimestamp).Milliseconds >= 250)
                {
                    this.lastKey = keyData;
                    lastKeydownTimestamp = DateTime.Now;

                    this.key2EventMap[keyData]();
                }

                return false;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void Increase()
        {
            if(!this.canBeChanged)
            {
                return;
            }

            if (this.timbreParam.v < this.timbreParam.max)
            {
                this.timbreParam.v += 1;
                this.paramLabel.initV = this.timbreParam.v;
                this.hSlider.initV = this.timbreParam.v;
            }
        }

        private void Decrease()
        {
            if (!this.canBeChanged)
            {
                return;
            }

            if (this.timbreParam.v > 0)
            {
                this.timbreParam.v -= 1;
                this.paramLabel.initV = this.timbreParam.v;
                this.hSlider.initV = this.timbreParam.v;
            }
        }
    }
}
