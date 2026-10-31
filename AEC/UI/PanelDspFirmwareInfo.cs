using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Comm;
using AEC.Comm.FirmwareUpdate;

namespace AEC.UI
{
    public partial class PanelDspFirmwareInfo : AEC.UI.Controls.MContainerControl
    {
        public const uint DefaultVersion = 0x00000000;

        public DSPFirmwareInfo _dspFirmwareInfo;
        public bool _isEditable = false;

        public PanelDspFirmwareInfo()
        {
            InitializeComponent();
        }

        public bool isEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                this._isEditable = value;
                this.switchTPM.Enabled = value;
                //this.switchEfuse.Enabled = value;
                this.txtVersion.ReadOnly = !value;
                this.txtComments.ReadOnly = !value;
            }
        }

        public DSPFirmwareInfo dspFirmwareInfo
        {
            get
            {
                return this._dspFirmwareInfo;
            }
            set
            {
                this._dspFirmwareInfo = value;
                if(this._dspFirmwareInfo != null)
                {
                    this.txtFlashSize.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.flashSize);
                    this.txtExeSize.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.exeSize);
                    this.txtExeCrc.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.exeCrc);

                    if (this._dspFirmwareInfo.wavAddress != DSPFirmwareInfo.DefaultAddress)
                    {
                        this.txtWavAddress.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.wavAddress);
                    }
                    else
                    {
                        this.txtWavAddress.Clear();
                    }

                    if (this._dspFirmwareInfo.wavSize > 0)
                    {
                        this.txtWavSize.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.wavSize);
                    }
                    else
                    {
                        this.txtWavSize.Clear();
                    }

                    if (this._dspFirmwareInfo.wavCrc != DSPFirmwareInfo.DefaultCRC)
                    {
                        this.txtWavCrc.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.wavCrc);
                    }
                    else
                    {
                        this.txtWavCrc.Clear();
                    }
                    
                    this.txtEnvAddress.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.envAddress);
                    this.txtEnvSize.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.envSize);
                    this.txtEnvCrc.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.envCrc);
                    this.txtTpmAddress.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.tpmAddress);
                    this.txtTimeStamp.Text = AEC.Tools.TimeStampHelper.Uint32TimeStampToDateTime(this._dspFirmwareInfo.lastModifiedTimeStamp).ToString("F");

                    this.switchTPM.IsCheckedInit = this._dspFirmwareInfo.isTpmEncrypted;
                    this.switchEfuse.IsChecked = this._dspFirmwareInfo.isEfuseEncrypted;

                    if (this.txtVersion.ReadOnly)
                    {
                        this.txtVersion.Text = Utils.Uint32ToHexString(this._dspFirmwareInfo.version);
                    }
                    
                    this.txtFileName.Text = this._dspFirmwareInfo.fileName;
                    this.txtFileName.SelectionStart = this.txtFileName.TextLength;

                    if (this.txtVersion.ReadOnly)
                    {
                        this.txtComments.Text = this._dspFirmwareInfo.comments;
                    }
                }
                else
                {
                    this.txtFlashSize.Clear();
                    this.txtExeSize.Clear();
                    this.txtExeCrc.Clear();
                    this.txtWavAddress.Clear();
                    this.txtWavSize.Clear();
                    this.txtWavCrc.Clear();
                    this.txtEnvAddress.Clear();
                    this.txtEnvSize.Clear();
                    this.txtEnvCrc.Clear();
                    this.txtTpmAddress.Clear();
                    this.txtTimeStamp.Clear();
                }
            }
        }

        protected override void OnLoadHandler()
        {
            this.InitUI();
        }

        private void InitUI()
        {
            this.version = DefaultVersion;
        }

        public DSPFirmwareInfo GetDSPFirmwareInfo(out string errorMsg)
        {
            errorMsg = string.Empty;

            if (this._dspFirmwareInfo == null)
            {
                return null;
            }

            this._dspFirmwareInfo.comments = this.txtComments.Text.Trim();

            uint version;
            if(Utils.ParseHexStringToUint32(this.txtVersion.Text.Trim(), out version, out errorMsg))
            {
                this._dspFirmwareInfo.version = version;

                return this._dspFirmwareInfo;
            }

            errorMsg = string.Format("解析版本号数据出错, {0}", errorMsg);

            return null;
        }

        public uint version
        {
            //get
            //{
            //    if(this._dspFirmwareInfo != null)
            //    {
            //        return this._dspFirmwareInfo.version;
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}
            set
            {
                this.txtVersion.Text = Utils.Uint32ToHexString(value);
                if(this._dspFirmwareInfo != null)
                {
                    this._dspFirmwareInfo.version = value;                    
                }                
            }
        }
    }
}
