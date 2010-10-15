using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using com.jds.AWLauncher.classes.windows;
using com.jds.AWLauncher.classes.windows.dll;

namespace com.jds.AWLauncher.n_classes.windows.dll
{
    public class UxTheme
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
      
        private static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);
        [StructLayout(LayoutKind.Sequential)]
        private struct DTTOPTS
        {
            public int dwSize;
            public int dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public int iTextShadowType;
            public POINT ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public int pfnDrawTextCallback;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class BITMAPINFO
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }
        
        public enum TextStyle
        {
            Normal,
            Glowing
        }
        
        private const int DTT_COMPOSITED = 8192;
        private const int DTT_GLOWSIZE = 2048;
        private const int DTT_TEXTCOLOR = 1;

        public static void DrawText(Graphics graphics, string text, Font font, Rectangle bounds, Color color, TextFormatFlags flags)
        {
            DrawText(graphics, text, font, bounds, color, flags, TextStyle.Normal);
        }
        
        public static void DrawText(Graphics graphics, string text, Font font, Rectangle bounds, Color color, TextFormatFlags flags, TextStyle textStyle)
        {
            IntPtr primaryHdc = graphics.GetHdc();

            // Create a memory DC so we can work offscreen
            IntPtr memoryHdc = GDI32.CreateCompatibleDC(primaryHdc);

            // Create a device-independent bitmap and select it into our DC
            BITMAPINFO info = new BITMAPINFO();
            info.biSize = Marshal.SizeOf(info);
            info.biWidth = bounds.Width;
            info.biHeight = -bounds.Height;
            info.biPlanes = 1;
            info.biBitCount = 32;
            info.biCompression = 0; // BI_RGB
            IntPtr dib = GDI32.CreateDIBSection(primaryHdc, info, 0, 0, IntPtr.Zero, 0);
            GDI32.SelectObject(memoryHdc, dib);

            // Create and select font
            IntPtr fontHandle = font.ToHfont();
            GDI32.SelectObject(memoryHdc, fontHandle);

            // Draw glowing text
            VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
            DTTOPTS dttOpts = new DTTOPTS {dwSize = Marshal.SizeOf(typeof (DTTOPTS))};

            if (textStyle == TextStyle.Glowing)
            {
                dttOpts.dwFlags = DTT_COMPOSITED | DTT_GLOWSIZE | DTT_TEXTCOLOR;
            }
            else
            {
                dttOpts.dwFlags = DTT_COMPOSITED | DTT_TEXTCOLOR;
            }
            dttOpts.crText = ColorTranslator.ToWin32(color);
            dttOpts.iGlowSize = 8; // This is about the size Microsoft Word 2007 uses

            RECT textBounds = new RECT(0, 0, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
            DrawThemeTextEx(renderer.Handle, memoryHdc, 0, 0, text, -1, (int)flags, ref textBounds, ref dttOpts);

            // Copy to foreground
            const int SRCCOPY = 0x00CC0020;
            GDI32.BitBlt(primaryHdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, memoryHdc, 0, 0, SRCCOPY);

            // Clean up
            GDI32.DeleteObject(fontHandle);
            GDI32.DeleteObject(dib);
            GDI32.DeleteDC(memoryHdc);

            graphics.ReleaseHdc(primaryHdc);
        }
    }
}
