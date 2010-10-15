using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace com.jds.AWLauncher.classes.windows.dll
{
	public class user32
	{
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32", EntryPoint = "FindWindowExA")]
        public static extern int FindWindowEx(IntPtr hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT rc);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, HOOKPROC lpfn, int hMod, int dwThreadId);

        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll")]
        public static extern int SetActiveWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hwndParent, EnumChildProc lpEnumFunc, ref RECT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hwnd, [MarshalAs(UnmanagedType.LPTStr)] string lpClassName, int capacity);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int DrawAnimatedRects(IntPtr hwnd, int idAni, ref RECT lprcFrom, ref RECT lprcTo);

        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, ref ANIMATIONINFO lpvParam, int fuWinIni);

        public const int SPI_GETANIMATION = 0x48;

        public const int IDANI_OPEN = 0x1;
        public const int IDANI_CLOSE = 0x2;
        public const int IDANI_CAPTION = 0x3;

        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_NORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_MAXIMIZE = 3;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_SHOWMINNOACTIVE = 7;
        private const int SW_SHOWNA = 8;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWDEFAULT = 10;
        private const int SW_FORCEMINIMIZE = 11;
        private const int SW_MAX = 11;

        public static void SwitchToProcess(Process process)
        {
            if (IsIconic(process.MainWindowHandle))
            {
                ShowWindowAsync(process.MainWindowHandle, SW_RESTORE);
            }

            SetForegroundWindow(process.MainWindowHandle);
        }

        //
        // Delegates
        //

        public delegate int HOOKPROC(int nCode, int wParam, int lParam);
        public delegate bool EnumChildProc(IntPtr hwnd, ref RECT lParam);

        //
        // Structs
        //
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ANIMATIONINFO
        {
            public int cbSize;
            public int iMinAnimate;
        }
	}
}
