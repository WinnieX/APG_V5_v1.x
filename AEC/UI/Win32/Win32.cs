using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using AEC.UI.Utility;

namespace AEC.UI
{
    internal class Win32
    {
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_KEYDOWN = 256;

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        #region fileds

        public static readonly IntPtr TRUE = new IntPtr(1);

        public static readonly IntPtr FALSE = IntPtr.Zero;

        #endregion

        #region extern methods

        /// <summary>
        /// windows窗口的动画效果
        /// </summary>
        /// <param name="whnd">The WHND.</param>
        /// <param name="dwtime">The dwtime.</param>
        /// <param name="dwflag">动画标识，多个以|分隔）</param>
        /// <returns></returns>
        /// User:K.Anding  CreateTime:2011-7-19 23:55.
        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

        /// <summary>
        /// 获取指定窗口控件的系统信息
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-21 13:44.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);

        /// <summary>
        /// 释放设备上下文环境
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-21 13:45.
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

        /// <summary>
        /// 从当前线程的窗体中释放对鼠标的扑捉
        /// </summary>
        /// <returns>Return a data(or instance) of Boolean.</returns>
        /// User:Ryan  CreateTime:2011-07-26 15:27.
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        //[DllImport("user32.dll", EntryPoint = "SendMessage")]
        //public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, int lParam);

        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hWnd, int Msg, int wParam, ref RECT lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hwnd, int msg, int wParam, ref HDITEM lParam);

        /// <summary>
        /// 获取ComboBox的控件信息
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);

        /// <summary>
        /// 设置圆角窗体
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        /// <summary>
        /// 获取窗体的工作区域
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        /// <summary>
        /// 创建圆角的区域
        /// </summary>
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            IntPtr hwndChildAfter,
            string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        public extern static int OffsetRect(ref RECT lpRect, int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PtInRect([In] ref RECT lprc, Point pt);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, ref RECT r);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr handle);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        public static extern int ShowScrollBar(IntPtr hWnd, int iBar, int bShow);


        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern bool HideCaret(IntPtr hWnd);
        #endregion

        #region methods

        /// <summary>
        /// 取地两位值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// User:Ryan  CreateTime:2011-07-26 15:29.
        public static int LOWORD(int value)
        {
            return value & 0xFFFF;
        }

        /// <summary>
        /// 取高两位值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// User:Ryan  CreateTime:2011-07-26 15:30.
        public static int HIWORD(int value)
        {
            return value >> 16;
        }

        public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, nIndex);
            }
            return GetWindowLongPtr64(hWnd, nIndex);
        }

        [DllImport("user32.dll ", CharSet = CharSet.Auto, ExactSpelling = true)]
        static public extern IntPtr GetForegroundWindow();
        #endregion
    }

    public static class Win32API
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg,
            int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll")]
        private static extern bool GetComboBoxInfo(IntPtr hwnd, ref COMBOBOXINFO pcbi);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 MessageBeep(UInt32 uType);

        [StructLayout(LayoutKind.Sequential)]
        private struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public IntPtr stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndItem;
            public IntPtr hwndList;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private const int EM_SETCUEBANNER = 0x1501;
        private const int EM_GETCUEBANNER = 0x1502;

        public static void SetCueText(Control control, string text)
        {
            if (control is ComboBox)
            {
                COMBOBOXINFO info = GetComboBoxInfo(control);
                SendMessage(info.hwndItem, EM_SETCUEBANNER, 0, text);
            }
            else
            {
                SendMessage(control.Handle, EM_SETCUEBANNER, 0, text);
            }
        }

        private static COMBOBOXINFO GetComboBoxInfo(Control control)
        {
            COMBOBOXINFO info = new COMBOBOXINFO();
            //a combobox is made up of three controls, a button, a list and textbox; 
            //we want the textbox 
            info.cbSize = Marshal.SizeOf(info);
            GetComboBoxInfo(control.Handle, ref info);
            return info;
        }

        public static string GetCueText(Control control)
        {
            StringBuilder builder = new StringBuilder();
            if (control is ComboBox)
            {
                COMBOBOXINFO info = new COMBOBOXINFO();
                //a combobox is made up of two controls, a list and textbox; 
                //we want the textbox 
                info.cbSize = Marshal.SizeOf(info);
                GetComboBoxInfo(control.Handle, ref info);
                SendMessage(info.hwndItem, EM_GETCUEBANNER, 0, builder);
            }
            else
            {
                SendMessage(control.Handle, EM_GETCUEBANNER, 0, builder);
            }
            return builder.ToString();
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct tagSCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }
        public enum fnBar
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2
        }
        public enum fMask
        {
            SIF_ALL,
            SIF_DISABLENOSCROLL = 0X0010,
            SIF_PAGE = 0X0002,
            SIF_POS = 0X0004,
            SIF_RANGE = 0X0001,
            SIF_TRACKPOS = 0X0008
        }

        public struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        } 
        public enum ScrollInfoMask
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = SIF_RANGE + SIF_PAGE + SIF_POS + SIF_TRACKPOS
        }
        public enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        public static int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }
        public const int SB_THUMBTRACK = 5;
        public const int WM_HSCROLL = 0x114;
        public const int WM_VSCROLL = 0x115;
        [DllImport("user32.dll", EntryPoint = "GetScrollInfo")]
        public static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        [DllImport("user32.dll", EntryPoint = "SetScrollInfo")]
        public static extern int SetScrollInfo(IntPtr hwnd, int fnBar, [In] ref SCROLLINFO lpsi, bool fRedraw);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, long wParam,int lParam);

        [DllImport("user32.dll", EntryPoint = "GetScrollPos")]
        public static extern int GetScrollPos(
         int hwnd,
         int nBar
        );

        private const int WS_HSCROLL = 0x100000;
        private const int WS_VSCROLL = 0x200000;
        private const int GWL_STYLE = (-16);

        [System.Runtime.InteropServices.DllImport("user32",CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        /// <summary>
        /// 判断是否出现垂直滚动条
        /// </summary>
        /// <param name="ctrl">待测控件</param>
        /// <returns>出现垂直滚动条返回true，否则为false</returns>
        internal static bool IsVerticalScrollBarVisible(Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
                return false;

            return (GetWindowLong(ctrl.Handle, GWL_STYLE) & WS_VSCROLL) != 0;
        }
        /// <summary>
        /// 判断是否出现水平滚动条
        /// </summary>
        /// <param name="ctrl">待测控件</param>
        /// <returns>出现水平滚动条返回true，否则为false</returns>
        internal static bool IsHorizontalScrollBarVisible(Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
                return false;
            return (GetWindowLong(ctrl.Handle, GWL_STYLE) & WS_HSCROLL) != 0;
        }
    }

    //public struct SCROLLINFO
    //{
    //    public uint cbSize;
    //    public uint fMask;
    //    public int nMin;
    //    public int nMax;
    //    public uint nPage;
    //    public int nPos;
    //    public int nTrackPos;
    //}
    //enum ScrollInfoMask
    //{
    //    SIF_RANGE = 0x1,
    //    SIF_PAGE = 0x2,
    //    SIF_POS = 0x4,
    //    SIF_DISABLENOSCROLL = 0x8,
    //    SIF_TRACKPOS = 0x10,
    //    SIF_ALL = SIF_RANGE + SIF_PAGE + SIF_POS + SIF_TRACKPOS
    //}
    //enum ScrollBarDirection
    //{
    //    SB_HORZ = 0,
    //    SB_VERT = 1,
    //    SB_CTL = 2,
    //    SB_BOTH = 3
    //}
}
