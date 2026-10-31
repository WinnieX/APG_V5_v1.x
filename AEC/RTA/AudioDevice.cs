using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA
{
    public class AudioDevice
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Clsid
        /// </summary>
        public Guid Clsid { get; private set; }

        public AudioDevice(string name, Guid clsid)
        {
            this.Name = name;
            this.Clsid = clsid;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
