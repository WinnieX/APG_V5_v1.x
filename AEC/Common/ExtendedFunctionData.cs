using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Common
{
    public class ExtendedFunctionData
    {
        public string custom_manufacturer;
        public string custom_device_model;
        public string custom_device_version;
        public string custom_device_description;

        public ExtendedFunctionData()
        {
            this.custom_manufacturer = string.Empty;
            this.custom_device_model = string.Empty;
            this.custom_device_version = string.Empty;
            this.custom_device_description = string.Empty;
        }

        public void CopyFrom(ExtendedFunctionData other)
        {
            this.custom_manufacturer = other.custom_manufacturer;
            this.custom_device_model = other.custom_device_model;
            this.custom_device_version = other.custom_device_version;
            this.custom_device_description = other.custom_device_description;
        }
    }
}
