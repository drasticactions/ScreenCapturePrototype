// <copyright file="WindowRecordSurface.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using ScreenCaptureKit;

namespace Microsoft.VisualStudio.ScreenCapture.Mac
{
    /// <summary>
    /// Mac Recording Window.
    /// </summary>
    public class WindowRecordSurface : ISurface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowRecordSurface"/> class.
        /// </summary>
        /// <param name="window"><see cref="SCWindow"/>.</param>
        public WindowRecordSurface(SCWindow window)
        {
            ArgumentNullException.ThrowIfNull(window, nameof(window));

            this.Title = window.Title ?? string.Empty;
            this.Filter = new SCContentFilter(window);
            this.Config = new SCStreamConfiguration();
            this.Config.MinimumFrameInterval = new CoreMedia.CMTime(1, 60);
            this.Stream = new SCStream(this.Filter, this.Config, null);
        }

        /// <inheritdoc/>
        public string Title { get; }

        /// <inheritdoc/>
        public object CaptureSurface => this.Stream;

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