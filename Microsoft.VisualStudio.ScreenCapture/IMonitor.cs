﻿// <copyright file="IMonitor.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System.Drawing;
using System.Numerics;

namespace Microsoft.VisualStudio.ScreenCapture
{
    /// <summary>
    /// Monitor Information.
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// Gets a value indicating whether the monitor is the primary monitor on the computer.
        /// </summary>
        bool IsPrimary { get; }

        /// <summary>
        /// Gets the screen size.
        /// </summary>
        SizeF ScreenSize { get; }

        /// <summary>
        /// Gets the total monitor area.
        /// </summary>
        Rectangle MonitorArea { get; }

        /// <summary>
        /// Gets the total work area.
        /// </summary>
        Rectangle WorkArea { get; }

        /// <summary>
        /// Gets the monitor name.
        /// </summary>
        string DeviceName { get; }
    }
}
