// <copyright file="ISurface.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

namespace Microsoft.VisualStudio.ScreenCapture
{
    /// <summary>
    /// Record Surface.
    /// The base interface for all recording services.
    /// </summary>
    public interface ISurface
    {
        /// <summary>
        /// Gets the Window title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the capture surface.
        /// </summary>
        object CaptureSurface { get; }
    }
}
