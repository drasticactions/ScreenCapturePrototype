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
            this.ScreenRecorder = new ScreenRecorder(window);
        }

        /// <inheritdoc/>
        public string Title { get; }

        /// <inheritdoc/>
        public object CaptureSurface => this.ScreenRecorder;

        /// <summary>
        /// Gets the <see cref="ScreenRecorder"/>.
        /// </summary>
        public ScreenRecorder ScreenRecorder { get; }
    }
}