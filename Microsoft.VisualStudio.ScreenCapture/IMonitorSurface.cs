// <copyright file="IMonitorSurface.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.VisualStudio.ScreenCapture
{
    /// <summary>
    /// Monitor Surface.
    /// </summary>
    public interface IMonitorSurface : ISurface
    {
        /// <summary>
        /// Gets the monitor info.
        /// </summary>
        IMonitor MonitorInfo { get; }
    }
}
