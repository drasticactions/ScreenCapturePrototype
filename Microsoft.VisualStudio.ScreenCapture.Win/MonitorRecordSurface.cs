// <copyright file="MonitorRecordSurface.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

namespace Microsoft.VisualStudio.ScreenCapture.Win
{
    /// <summary>
    /// The Monitor Record Surface.
    /// </summary>
    public class MonitorRecordSurface : IMonitorSurface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorRecordSurface"/> class.
        /// </summary>
        /// <param name="info"><see cref="MonitorInfo"/>.</param>
        public MonitorRecordSurface(MonitorInfo info)
        {
            ArgumentNullException.ThrowIfNull(info, nameof(info));
            var item = CaptureHelper.CreateItemForMonitor(info.Hmon);
            this.CaptureSurface = item;
            this.Title = item.DisplayName;
            this.MonitorInfo = info;
        }

        /// <inheritdoc/>
        public string Title { get; }

        /// <inheritdoc/>
        public object CaptureSurface { get; }

        /// <inheritdoc/>
        public IMonitor MonitorInfo { get; }
    }
}
