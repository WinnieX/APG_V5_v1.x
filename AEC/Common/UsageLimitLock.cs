using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Common
{
    public class UsageLimitLock
    {
        public const ushort MaxUsageCount = 3000;

        private const string DefUsageLimitPassword = "000000";
        public static readonly int PasswordLength = DefUsageLimitPassword.Length;
        public static readonly int DataLength = sizeof(short) * 2 + DefUsageLimitPassword.Length;

        public readonly short index;

        public ushort _usageCount;

        public bool enable{ get; set; }

        public ushort usageCount
        {
            get
            {
                return _usageCount;
            }
            set
            {
                this._usageCount = Core.Utils.ConstrainMax(value, MaxUsageCount);
            }
        }

        public bool isLocked
        {
            get
            {
                return this.enable && this._usageCount == 0;
            }
        }
        
        private readonly Char[] _systemPassword = DefUsageLimitPassword.ToCharArray();
        public bool passwordVisible { get; set; }

        public string systemPasswordStr
        {
            get
            {
                return new string(_systemPassword);
            }
            set
            {
                if (value.Length == PasswordLength && AEC.Tools.StringHelper.StringIsDigit(value))
                {
                    Array.Copy(value.ToCharArray(), _systemPassword, PasswordLength);                    
                }
                else
                {
                    Array.Copy(DefUsageLimitPassword.ToCharArray(), _systemPassword, PasswordLength);
                }
            }
        }

        public UsageLimitLock(short index_)
        {
            this.index = index_;
            this.Reset();
        }

        public void Reset()
        {
            this.enable = false;
            this.usageCount = 0;
            Array.Copy(DefUsageLimitPassword.ToCharArray(), _systemPassword, PasswordLength);
        }

        public void CopyFrom(UsageLimitLock other)
        {
            this.enable = other.enable;
            this.usageCount = other.usageCount;
            Array.Copy(other._systemPassword, this._systemPassword, PasswordLength);
        }

        public override string ToString()
        {
            return string.Format("enable:{0}, count = {1}, password = {2}", AEC.Core.DataConverter.Bool2Short(this.enable), this.usageCount, this.systemPasswordStr);
        }
    }
}
