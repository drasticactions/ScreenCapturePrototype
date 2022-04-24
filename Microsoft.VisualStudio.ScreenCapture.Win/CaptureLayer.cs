// <copyright file="CaptureLayer.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System.Numerics;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.UI.Composition;

namespace Microsoft.VisualStudio.ScreenCapture.Win
{
    public class CaptureLayer : IDisposable
    {
        private Compositor? compositor;
        private ContainerVisual root;

        private SpriteVisual content;
        private CompositionSurfaceBrush brush;

        private IDirect3DDevice? device;
        private CaptureFrame? capture;

        public CaptureLayer(Compositor c)
        {
            ArgumentNullException.ThrowIfNull(c, nameof(c));
            this.compositor = c;

            this.device = Direct3D11Helper.CreateDevice();

            // Setup the root.
            this.root = this.compositor.CreateContainerVisual();
            this.root.RelativeSizeAdjustment = Vector2.One;

            // Setup the content.
            this.brush = this.compositor.CreateSurfaceBrush();
            this.brush.HorizontalAlignmentRatio = 0.5f;
            this.brush.VerticalAlignmentRatio = 0.5f;
            this.brush.Stretch = CompositionStretch.Uniform;

            this.content = this.compositor.CreateSpriteVisual();
            this.content.AnchorPoint = new Vector2(0.5f);
            this.content.RelativeOffsetAdjustment = new Vector3(0.5f, 0.5f, 0);
            this.content.RelativeSizeAdjustment = Vector2.One;
            this.content.Size = new Vector2(-80, -80);
            this.content.Brush = this.brush;
            this.root.Children.InsertAtTop(this.content);
        }

        public Visual Visual => this.root;

        public void Dispose()
        {
            this.StopCapture();
            this.compositor = null;
            this.root.Dispose();
            this.content.Dispose();
            this.brush.Dispose();
            this.device?.Dispose();
        }

        public void StartCaptureFromItem(GraphicsCaptureItem item)
        {
            this.StopCapture();
            if (this.device is not null && this.compositor is not null)
            {
                this.capture = new CaptureFrame(this.device, item);

                var surface = capture.CreateSurface(this.compositor);
                this.brush.Surface = surface;

                this.capture.StartCapture();
            }
        }

        public void StopCapture()
        {
            this.capture?.Dispose();
            this.brush.Surface = null;
        }
    }
}
