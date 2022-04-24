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
            this.Filter = new SCContentFilter(info.Display, new SCWindow[0], SCContentFilterOption.Exclude);

            // Set the capture size to twice the display size to support retina displays.
            this.Config = new SCStreamConfiguration();
            this.Config.Width = (nuint)info.Display.Width * 2;
            this.Config.Height = (nuint)info.Display.Height * 2;
            this.Config.MinimumFrameInterval = new CoreMedia.CMTime(1, 60);
            this.Stream = new SCStream(this.Filter, this.Config, null);
            this.MonitorInfo = info;
            this.Title = this.MonitorInfo.DeviceName;
        }

        /// <inheritdoc/>
        public string Title { get; }

        /// <inheritdoc/>
        public object CaptureSurface => this.Stream;

        /// <inheritdoc/>
        public IMonitor MonitorInfo { get; }

        /// <summary>
        /// Gets the <see cref="SCStream"/>.
        /// </summary>
        public SCStream Stream { get; }

        /// <summary>
        /// Gets the <see cref="SCStreamConfiguration"/>.
        /// </summary>
        public SCStreamConfiguration Config { get; }

        /// <summary>
        /// Gets the <see cref="SCStreamConfiguration"/>.
        /// </summary>
        public SCContentFilter Filter { get; }
    }
}