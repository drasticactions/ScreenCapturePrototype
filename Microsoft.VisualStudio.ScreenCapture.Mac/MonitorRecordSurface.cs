// <copyright file="MonitorRecordSurface.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System;
using ScreenCaptureKit;

namespace Microsoft.VisualStudio.ScreenCapture.Mac
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
            this.ScreenRecorder = new ScreenRecorder(info.Display);
            this.MonitorInfo = info;
            this.Title = this.MonitorInfo.DeviceName;
        }

        /// <inheritdoc/>
        public string Title { get; }

        /// <inheritdoc/>
        public object CaptureSurface => this.ScreenRecorder;

        /// <inheritdoc/>
        public IMonitor MonitorInfo { get; }

        /// <summary>
        /// Gets the <see cref="ScreenRecorder"/>.
        /// </summary>
        public ScreenRecorder ScreenRecorder { get; }
    }
}