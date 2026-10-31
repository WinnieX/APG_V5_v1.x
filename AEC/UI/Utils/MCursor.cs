using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace AEC.UI.Utils
{
    public class MCursor
    {
        private IntPtr hIcon = IntPtr.Zero;
        private IntPtr hCursor = IntPtr.Zero;
        private IntPtr hbmMask = IntPtr.Zero;
        private IntPtr hbmColor = IntPtr.Zero;

        public Cursor cursor
        {
            get;
            private set;
        }

        public void InitCursor(Bitmap bitmap, Point hotSpot)
        {
            Debug.Assert(bitmap != null && hotSpot.X >= 0 && hotSpot.Y >= 0);
            int xHotSpot = Math.Min(bitmap.Width - 1, hotSpot.X);
            int yHotSpot = Math.Min(bitmap.Height - 1, hotSpot.Y);
            
            this.hIcon = bitmap.GetHicon();

            Win32.IconInfo iconInfo = new Win32.IconInfo();
            if(Win32.GetIconInfo(hIcon, ref iconInfo))
            {
                iconInfo.xHotspot = xHotSpot;
                iconInfo.yHotspot = yHotSpot;

                iconInfo.fIcon = false;

                this.hbmMask = iconInfo.hbmMask;
                this.hbmColor = iconInfo.hbmColor;

                this.hCursor = Win32.CreateIconIndirect(ref iconInfo);

                this.cursor = new Cursor(this.hCursor);
            }
        }

        public void Release()
        {
            if (this.cursor != null)
            {
                this.cursor.Dispose();
                this.cursor = null;
            }

            if (this.hbmColor != IntPtr.Zero)
            {
                Win32.DeleteObject(this.hbmColor);
                this.hbmColor = IntPtr.Zero;
            }

            if (this.hbmMask != IntPtr.Zero)
            {
                Win32.DeleteObject(this.hbmMask);
                this.hbmMask = IntPtr.Zero;
            }

            if (this.hCursor != IntPtr.Zero)
            {
                Utils.Win32.DestroyIcon(this.hCursor);
                this.hCursor = IntPtr.Zero;
            }

            if (this.hIcon != IntPtr.Zero)
            {
                Utils.Win32.DestroyIcon(this.hIcon);
                this.hIcon = IntPtr.Zero;
            }
        }
    }
}
