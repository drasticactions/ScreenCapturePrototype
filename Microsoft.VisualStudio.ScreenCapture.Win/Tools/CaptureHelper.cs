// <copyright file="CaptureHelper.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System.Runtime.InteropServices;
using Windows.Graphics.Capture;

namespace Microsoft.VisualStudio.ScreenCapture.Win
{
    public static class CaptureHelper
    {
        static readonly Guid GraphicsCaptureItemGuid = new Guid("79C3F95B-31F7-4EC2-A464-632EF5D30760");

        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComVisible(true)]
        private interface IInitializeWithWindow
        {
            void Initialize(
                IntPtr hwnd);
        }

        [ComImport]
        [Guid("3628E81B-3CAC-4C60-B7F4-23CE0E0C3356")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComVisible(true)]
        private interface IGraphicsCaptureItemInterop
        {
            void CreateForWindow(
                [In] IntPtr window,
                [In] ref Guid iid,
                out IntPtr result);

            void CreateForMonitor(
                [In] IntPtr monitor,
                [In] ref Guid iid,
                out IntPtr result);
        }

        public static GraphicsCaptureItem CreateItemForWindow(IntPtr hwnd)
        {
            var interop = GraphicsCaptureItem.As<IGraphicsCaptureItemInterop>();

            var temp = typeof(GraphicsCaptureItem);

            // For some reason typeof(GraphicsCaptureItem).GUID returns the wrong guid?
            interop.CreateForWindow(hwnd, GraphicsCaptureItemGuid, out var raw);
            var item = GraphicsCaptureItem.FromAbi(raw);
            Marshal.Release(raw);

            return item;
        }

        public static GraphicsCaptureItem CreateItemForMonitor(IntPtr hmon)
        {
            var interop = GraphicsCaptureItem.As<IGraphicsCaptureItemInterop>();

            var temp = typeof(GraphicsCaptureItem);

            // For some reason typeof(GraphicsCaptureItem).GUID returns the wrong guid?
            interop.CreateForMonitor(hmon, GraphicsCaptureItemGuid, out var raw);
            var item = GraphicsCaptureItem.FromAbi(raw);
            Marshal.Release(raw);

            return item;
        }
    }
}
