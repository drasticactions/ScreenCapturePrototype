// <copyright file="CapturedFrame.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System;
using CoreMedia;
using CoreVideo;

namespace Microsoft.VisualStudio.ScreenCapture.Mac
{
    /// <summary>
    /// Captured Frame.
    /// </summary>
    public class CapturedFrame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CapturedFrame"/> class.
        /// </summary>
        /// <param name="sample"><see cref="CMSampleBuffer"/>.</param>
        public CapturedFrame(CMSampleBuffer sample)
        {
            this.SampleBuffer = sample;
            var attachments = sample.GetAttachments(CMAttachmentMode.ShouldPropagate);

            using var imageBuffer = this.SampleBuffer.GetImageBuffer() as CVPixelBuffer;
            if (imageBuffer is not null)
            {
                this.Surface = imageBuffer.GetIOSurface();
            }
        }

        public CMSampleBuffer SampleBuffer { get; }

        public IOSurface.IOSurface? Surface { get; }

        public CGRect? ContentRect { get; }

        public double ContentScale { get; }

        public double ScaleFactor { get; }
    }
}