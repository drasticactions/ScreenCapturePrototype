// <copyright file="ProcessRecordSurface.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System.Diagnostics;

namespace Microsoft.VisualStudio.ScreenCapture.Win
{
    /// <summary>
    /// Process Windows Recording Window.
    /// </summary>
    public class ProcessRecordSurface : ISurface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessRecordSurface"/> class.
        /// </summary>
        /// <param name="process"><see cref="Process"/>.</param>
        public ProcessRecordSurface(Process process)
        {
            ArgumentNullException.ThrowIfNull(process, nameof(process));
            this.Process = process;
            var item = CaptureHelper.CreateItemForWindow(process.MainWindowHandle);
            this.Title = item.DisplayName;
            this.CaptureSurface = item;
        }

        /// <inheritdoc/>
        public string Title { get; }

        /// <inheritdoc/>
        public object CaptureSurface { get; }

        /// <summary>
        /// Gets the process.
        /// </summary>
        public Process Process { get; }
    }
}
