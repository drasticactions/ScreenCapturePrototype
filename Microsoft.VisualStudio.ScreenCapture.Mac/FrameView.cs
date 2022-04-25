// <copyright file="FrameView.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System;

namespace Microsoft.VisualStudio.ScreenCapture.Mac
{
    public class FrameView : NSView
    {
        public FrameView()
        {
            this.Frame = new CGRect(0, 0, 250, 250);
        }

        public void UpdateFrame(IOSurface.IOSurface surface)
        {
            this.Layer?.SetContents(surface);
        }
    }
}