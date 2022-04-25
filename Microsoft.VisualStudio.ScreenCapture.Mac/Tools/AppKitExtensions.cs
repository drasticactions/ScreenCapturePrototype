// <copyright file="AppKitExtensions.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System;
using CoreImage;
using CoreMedia;
using CoreVideo;

namespace Microsoft.VisualStudio.ScreenCapture.Mac
{
    public static class AppKitExtensions
    {
        public static NSImage? MakeNSImage(this CMSampleBuffer sampleBuffer)
        {
            NSImage? image;
            using (CVPixelBuffer? pixelBuffer = sampleBuffer.GetImageBuffer() as CVPixelBuffer)
            {
                if (pixelBuffer is null)
                {
                    return null;
                }

                // Lock the base address
                pixelBuffer.Lock(CVPixelBufferLock.ReadOnly);
                using (CIImage cIImage = new CIImage(pixelBuffer))
                {
                    image = null;

                    AutoResetEvent e = new AutoResetEvent(false);
                    cIImage.InvokeOnMainThread(() =>
                    {
                        NSCIImageRep rep = new NSCIImageRep(cIImage);
                        image = new NSImage(rep.Size);
                        image.AddRepresentation(rep);
                        e.Set();
                    });
                    e.WaitOne();
                }

                pixelBuffer.Unlock(CVPixelBufferLock.ReadOnly);
            }

            return image;
        }

        public static NSData? AsEncodedBitmapData(NSImage image, NSBitmapImageFileType fileType)
        {
            if (image == null)
                return null;

            image.LockFocus();
            var rect = new CGRect(0, 0, image.Size.Width, image.Size.Height);
            var rep = new NSBitmapImageRep(rect);
            image.UnlockFocus();
            return rep.RepresentationUsingTypeProperties(fileType, null);
        }

        public static NSData? AsPNG(this NSImage image)
        {
            return AsEncodedBitmapData(image, NSBitmapImageFileType.Png);
        }

        public static NSData? AsJPEG(this NSImage image)
        {
            return AsEncodedBitmapData(image, NSBitmapImageFileType.Jpeg);
        }
    }
}