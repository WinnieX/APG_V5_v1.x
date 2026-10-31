using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;

namespace AEC.Common
{
    public class X5Mixer
    {
        public readonly AutoWorkMode autoWorkModule;
        public readonly OutputMixer music;
        public readonly OutputMixer mic;
        public readonly OutputMixer echo;
        public readonly OutputMixer reverb;

        public X5Mixer(AutoWorkMode autoWorkModule_, OutputMixer music_, OutputMixer mic_, OutputMixer echo_ = null, OutputMixer reverb_ = null)
        {
            Debug.Assert(autoWorkModule_ != null && music_ != null && mic_ != null);

            this.autoWorkModule = autoWorkModule_;
            this.music = music_;
            this.mic = mic_;
            this.echo = echo_;
            this.reverb = reverb_;
        }

        public SignalMixer musicMixer
        {
            get
            {
                if(this.autoWorkModule.isSing)
                {
                    return this.music.singMixer;
                }
                else
                {
                    return this.music.discoMixer;
                }
            }
        }

        public SignalMixer micMixer
        {
            get
            {
                if (this.autoWorkModule.isSing)
                {
                    return this.mic.singMixer;
                }
                else
                {
                    return this.mic.discoMixer;
                }
            }
        }

        public SignalMixer echoMixer
        {
            get
            {
                if(this.echo == null)
                {
                    return null;
                }

                if (this.autoWorkModule.isSing)
                {
                    return this.echo.singMixer;
                }
                else
                {
                    return this.echo.discoMixer;
                }
            }
        }

        public SignalMixer reverbMixer
        {
            get
            {
                if (this.reverb == null)
                {
                    return null;
                }

                if (this.autoWorkModule.isSing)
                {
                    return this.reverb.singMixer;
                }
                else
                {
                    return this.reverb.discoMixer;
                }
            }
        }
    }
}
