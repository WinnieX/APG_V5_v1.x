using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.AudioProducts
{
    public class AudioSystem
    {
        public readonly Loudspeaker ls;
        public readonly PowerAmplifier pa;
        public readonly Microphone mic;
        //public readonly bool isBuildIn;

        public AudioSystem(Loudspeaker ls_, PowerAmplifier pa_, Microphone mic_)
        {
            this.ls = ls_;
            this.pa = pa_;
            this.mic = mic_;
            //this.isBuildIn = isBuildIn_;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            AudioSystem other = obj as AudioSystem;

            return this.ls.Equals(other.ls) && pa.Equals(other.pa) && mic.Equals(other.mic);
        }

        public override int GetHashCode()
        {
            return ls.GetHashCode() + pa.GetHashCode() + mic.GetHashCode();
        }
    }

    public interface IGetAudioSystem
    {
        AudioSystem GetAudioSystem();
    }

    public class BuildInAudioSystem : IGetAudioSystem
    {
        public AudioSystem audioSystem
        {
            get;
            private set;
        }

        public BuildInAudioSystem(AudioSystem audioSystem_)
        {
            this.audioSystem = audioSystem_;
        }

        public AudioSystem GetAudioSystem()
        {
            return this.audioSystem;
        }
    }

    public class CustomAudioSystem : IGetAudioSystem
    {
        public AudioSystem audioSystem
        {
            get;
            private set;
        }

        public string filePath
        {
            get;
            private set;
        }

        public CustomAudioSystem(AudioSystem audioSystem_, string filePath_)
        {
            this.audioSystem = audioSystem_;
            this.filePath = filePath_;
        }

        public AudioSystem GetAudioSystem()
        {
            return this.audioSystem;
        }
    }
}
