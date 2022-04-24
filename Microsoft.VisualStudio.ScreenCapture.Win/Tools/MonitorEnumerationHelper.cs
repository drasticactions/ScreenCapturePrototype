// <copyright file="MonitorEnumerationHelper.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System.Runtime.InteropServices;

namespace Microsoft.VisualStudio.ScreenCapture.Win
{
    public class MonitorEnumerationHelper
    {
        private const int CCHDEVICENAME = 32;

        private delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

        /// <summary>
        /// Gets the list of monitors connected to this computer.
        /// </summary>
        /// <returns>A IReadOnlyList of <see cref="IMonitor"/>.</returns>
        public static IReadOnlyList<MonitorInfo> GetMonitors()
        {
            var result = new List<MonitorInfo>();

            EnumDisplayMonitors(
                IntPtr.Zero,
                IntPtr.Zero,
                delegate (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
                {
                    MonitorInfoEx mi = default(MonitorInfoEx);
                    mi.Size = Marshal.SizeOf(mi);
                    bool success = GetMonitorInfo(hMonitor, ref mi);
                    if (success)
                    {
                        result.Add(new MonitorInfo(hMonitor, mi));
                    }

                    return true;
                },
                IntPtr.Zero);
            return result;
        }

        public static IReadOnlyList<MonitorRecordSurface> GetCaptureSurfacesViaMonitors()
        {
            var windows = new List<MonitorRecordSurface>();
            var monitors = GetMonitors();

            foreach (var monitor in monitors)
            {
                windows.Add(new MonitorRecordSurface(monitor));
            }

            return windows;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfoEx lpmi);

        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

        /// <summary>
        /// Creates a common struct for a monitor Rectangle.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        /// <summary>
        /// The internal monitor information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct MonitorInfoEx
        {
            public int Size;
            public RECT Monitor;
            public RECT WorkArea;
            public uint Flags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string DeviceName;
        }
    }
}
