using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Comm.CentralControl;
using AEC.Core;
using AEC.Common;

namespace AEC.UI
{
    public partial class FormCentralControl : AEC.UI.MFormBase
    {
        private CentralControlService ccService = new CentralControlService();

        public readonly ShortParam musicVolume = new ShortParam(SystemModuleBase.MusicVolumeMax);
        public readonly ShortParam micVolume = new ShortParam(SystemModuleBase.MicVolumeMax);
        public readonly ShortParam effectVolume = new ShortParam(SystemModuleBase.EffectVolumeMax);

        public FormCentralControl()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnStart))
            {
                this.StartWork();
            }
            else if(sender.Equals(this.btnStop))
            {
                this.StopWork();
            }
        }

        private void StartWork()
        {
            if(string.IsNullOrEmpty(this.cmbPorts.Text))
            {
                return;
            }

            if (this.ccService.StartService(this.cmbPorts.Text))
            {
                this.cmbPorts.Enabled = false;
                this.linkRefresh.Enabled = false;
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;
            }
        }

        private void StopWork()
        {
            this.ccService.StopService();
            this.cmbPorts.Enabled = true;
            this.linkRefresh.Enabled = true;
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }

        private void FormCentralControl_Load(object sender, EventArgs e)
        {
            this.InitUI();
        }

        private void InitUI()
        {
            this.InitCommPorts();

            this.sliderMusicVol.Binding(this.musicVolume, ParamGetLabelHelper.GetVolumeLabel);
            this.sliderMicVol.Binding(this.micVolume, ParamGetLabelHelper.GetVolumeLabel);
            this.sliderEffectVol.Binding(this.effectVolume, ParamGetLabelHelper.GetVolumeLabel);

            this.sliderMusicVol.ValueChanged += slider_ValueChanged;
            this.sliderMicVol.ValueChanged += slider_ValueChanged;
            this.sliderEffectVol.ValueChanged += slider_ValueChanged;

            this.InitCmbPresets();
            this.InitCmbMusicInputPorts();
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.sliderMusicVol))
            {
                this.MusicVolumeChanged();
            }
            else if(sender.Equals(this.sliderMicVol))
            {
                this.MicVolumeChanged();
            }
            else if(sender.Equals(this.sliderEffectVol))
            {
                this.EffectVolumeChanged();
            }
        }

        private void InitCmbPresets()
        {
            List<string> presets = new List<string>();
            for (int i = 0; i < AEC.Effectors.X5.Modules.SystemModule.X5PresetModeCount; ++i)
            {
                presets.Add(string.Format("Mode {0}", i + 1));
            }

            this.cmbPresets.Items.Clear();
            this.cmbPresets.Items.AddRange(presets.ToArray());
            this.cmbPresets.SelectedIndex = 0;
        }

        private void InitCmbMusicInputPorts()
        {
            string[] musicInputPorts = new string[]
            {
                "Port 1",
                "port 2",
            };

            this.cmbMusicInputPort.Items.Clear();
            this.cmbMusicInputPort.Items.AddRange(musicInputPorts);
            this.cmbMusicInputPort.SelectedIndex = 0;
        }

        private void InitCommPorts()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            this.cmbPorts.Items.Clear();
            this.cmbPorts.Items.AddRange(ports);

            if(ports.Length > 0)
            {
                this.cmbPorts.SelectedIndex = 0;
            }
        }

        private void FormCentralControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ccService.StopService();
        }

        private void linkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.InitCommPorts();
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.cmbPresets))
            {
                this.PresetModeChanged();
            }
            else if(sender.Equals(this.cmbMusicInputPort))
            {
                this.MusicInputPortChanged();
            }
        }

        private void MusicVolumeChanged()
        {
            int index = this.sliderMusicVol.value;
            this.ccService.SetMusicVolume((byte)index);
        }

        private void MicVolumeChanged()
        {
            int index = this.sliderMicVol.value;
            this.ccService.SetMicVolume((byte)index);
        }

        private void EffectVolumeChanged()
        {
            int index = this.sliderEffectVol.value;
            this.ccService.SetEffectVolume((byte)index);
        }

        private void PresetModeChanged()
        {
            int index = this.cmbPresets.SelectedIndex;
            this.ccService.RecallPresetMode((byte)index);
        }

        private void MusicInputPortChanged()
        {
            int index = this.cmbMusicInputPort.SelectedIndex;
            if(index == 0)
            {
                index = 1;
            }
            else if(index == 1)
            {
                index = 0;
            }
            this.ccService.SetMusicInputPort((byte)index);
        }
    }
}
