// <copyright file="WindowRecordSurface.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

namespace Microsoft.VisualStudio.ScreenCapture.Win
{
    /// <summary>
    /// Windows Recording Window.
    /// </summary>
    public class WindowRecordSurface : ISurface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowRecordSurface"/> class.
        /// </summary>
        /// <param name="windowHandle">Window pointer Handle.</param>
        public WindowRecordSurface(IntPtr windowHandle)
        {
            var item = CaptureHelper.CreateItemForWindow(windowHandle);
            this.Title = item.DisplayName;
            this.CaptureSurface = item;
        }

        /// <inheritdoc/>
        public string Title { get; }

        /// <inheritdoc/>
        public object CaptureSurface { get; }
    }
}
