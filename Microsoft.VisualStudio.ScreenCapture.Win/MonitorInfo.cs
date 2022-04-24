// <copyright file="MonitorInfo.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System.Drawing;
using System.Runtime.InteropServices;
using Windows.Graphics.Capture;
using static Microsoft.VisualStudio.ScreenCapture.Win.MonitorEnumerationHelper;

namespace Microsoft.VisualStudio.ScreenCapture.Win
{
    /// <summary>
    /// Windows Monitor Info.
    /// </summary>
    public class MonitorInfo : IMonitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorInfo"/> class.
        /// </summary>
        /// <param name="hMonitor">The IntPtr pointing to the monitor.</param>
        /// <param name="mi">The MonitorInfoEx. Must be already set.</param>
        internal MonitorInfo(IntPtr hMonitor, MonitorInfoEx mi)
        {
            this.ScreenSize = new SizeF(mi.Monitor.right - mi.Monitor.left, mi.Monitor.bottom - mi.Monitor.top);
            this.MonitorArea = new Rectangle(mi.Monitor.left, mi.Monitor.top, mi.Monitor.right - mi.Monitor.left, mi.Monitor.bottom - mi.Monitor.top);
            this.WorkArea = new Rectangle(mi.WorkArea.left, mi.WorkArea.top, mi.WorkArea.right - mi.WorkArea.left, mi.WorkArea.bottom - mi.WorkArea.top);
            this.IsPrimary = mi.Flags > 0;
            this.Hmon = hMonitor;
            this.DeviceName = mi.DeviceName;
        }

        /// <inheritdoc/>
        public bool IsPrimary { get; }

        /// <inheritdoc/>
        public SizeF ScreenSize { get; }

        /// <inheritdoc/>
        public Rectangle MonitorArea { get; }

        /// <inheritdoc/>
        public Rectangle WorkArea { get; }

        /// <inheritdoc/>
        public string DeviceName { get; }

        /// <summary>
        /// Gets the pointer to the monitors Hardware Monitor.
        /// </summary>
        public IntPtr Hmon { get; }
    }
}
