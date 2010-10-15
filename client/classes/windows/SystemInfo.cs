using System;
using com.jds.AWLauncher.classes.windows.dll;

namespace com.jds.AWLauncher.classes.windows
{
    public class SystemInfo
    {
        private static SystemInfo _instance;
        private readonly OperatingSystem _osInfo = Environment.OSVersion;

        public static SystemInfo Instance
        {
            get
            {
                return _instance ?? (_instance = new SystemInfo());
            }
        }

        public bool WindowsVistaOrGreater
        {
            get
            {
                return _osInfo.Version.Major >= 6;
            }
        }

        public bool Windows7OrGreater
        {
            get
            {
                return (_osInfo.Version.Major == 6 && _osInfo.Version.Minor >= 1)  || (_osInfo.Version.Major > 6);
            }
        }

        public bool isGlassEnable
        {
            get
            {
                return WindowsVistaOrGreater && DWMApi.DwmIsCompositionEnabled();
            }
        }
    }
}
