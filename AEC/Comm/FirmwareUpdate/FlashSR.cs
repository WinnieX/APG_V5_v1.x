using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class FlashSR
    {
        public readonly uint mid;       //flash mid
        public readonly uint szMem;     //flash 容量大小
        public readonly sbyte szSR;     //SR寄存器数,不大于2
        public readonly uint cwUnp;     //解保护控制字
        public readonly uint cwEnp;     //加保护控制字
        public readonly uint cwMsk;     //保护控制字的屏蔽位
        public readonly byte sb;       //保护控制字起始位
        public readonly byte lb;       //保护控制字位长度
        public readonly byte[] cwdRd;  //各SR控制字读命令字
        public readonly byte[] cwdWr;  //各SR控制字写命令字,00:写命令字；01:ff=无，其他=写命令字2；02：保留；03：[01]!=0xff时写命令字2的内容
        public readonly string icName;  //ic name
        public readonly string manNam;  //制造商名

        public FlashSR(
            uint mid,
            uint szMem,
            sbyte szSR,
            uint cwUnp,
            uint cwEnp,
            uint cwMsk,
            byte sb,
            byte lb,
            byte[] cwdRd,
            byte[] cwdWr,
            string icName,
            string manNam)
        {
            this.mid = mid;
            this.szMem = szMem;
            this.szSR = szSR;
            this.cwUnp = cwUnp;
            this.cwEnp = cwEnp;
            this.cwMsk = cwMsk;
            this.sb = sb;
            this.lb = lb;
            this.cwdRd = cwdRd;
            this.cwdWr = cwdWr;
            this.icName = icName;
            this.manNam = manNam;
        }

        private const uint MB = 1 << 20;

        protected enum FlushID : uint
        {
            FLASH_ID_Puya_25Q80 = 0x146085,//puya 8M,w+v:10.4s,e+w+v:11.3s,新版e+w+v：8.3s
            FLASH_ID_Puya_25Q16 = 0x156085,//puya 16M,e+w+v:6s，新版w+v=4s，e+w+v=4.3s
        };

        public static int BIT(int n)
        {
            return (0x01 << n);
        }

        public static int BFD(int v, int bs, int bl)
        {
            return ((v & ((1 << (bl)) - 1)) << (bs));
        }

        private static FlashSR[] FlashSRList = new FlashSR[]
        {
            new FlashSR(
                mid : (uint)FlushID.FLASH_ID_Puya_25Q80, 
                szMem : (uint)(8 * MB),
                szSR : 2,
                cwUnp : (uint)0x00,
                cwEnp : (uint)0x07,
                cwMsk : (uint)(BIT(14) | BFD(0x1f, 2, 5)),
                sb : (byte)2,
                lb : (byte)5,
                cwdRd : new byte[]{0x05,0x35,0xff,0xff},
                cwdWr : new byte[]{0x01,0xff,0xff,0xff},
                icName : "P25Q80",
                manNam : "Puya"
                ),

            new FlashSR(
                mid : (uint)FlushID.FLASH_ID_Puya_25Q16, 
                szMem : (uint)(16 * MB),
                szSR : 2,
                cwUnp : (uint)0x00,
                cwEnp : (uint)0x07,
                cwMsk : (uint)(BIT(14) | BFD(0x1f, 2, 5)),
                sb : (byte)2,
                lb : (byte)5,
                cwdRd : new byte[]{0x05,0x35,0xff,0xff},
                cwdWr : new byte[]{0x01,0x31,0xff,0x42},
                icName : "P25Q16H",
                manNam : "Puya"
                ),
        };

        public static FlashSR GetFlashSR(uint mid)
        {
            for (int i = 0; i < FlashSRList.Length; ++i)
            {
                if (FlashSRList[i].mid == mid)
                {
                    return FlashSRList[i];
                }
            }

            return null;
        }
    }
}
