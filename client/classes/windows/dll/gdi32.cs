using System;
using System.Runtime.InteropServices;

namespace com.jds.AWLauncher.n_classes.windows.dll
{
    public class GDI32
    {
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        internal static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateDIBSection(IntPtr hdc, UxTheme.BITMAPINFO pbmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);
    }
}
