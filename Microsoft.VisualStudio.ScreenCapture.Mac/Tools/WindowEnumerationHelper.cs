// <copyright file="WindowEnumerationHelper.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using ScreenCaptureKit;

namespace Microsoft.VisualStudio.ScreenCapture.Mac
{
    /// <summary>
    /// Window Enumeration Helper.
    /// </summary>
    public static class WindowEnumerationHelper
    {
        public static async Task<IReadOnlyList<WindowRecordSurface>> GetMainCaptureSurfacesViaWindowsAsync()
        {
            var list = new List<WindowRecordSurface>();

            var result = await SCShareableContent.GetShareableContentAsync(false, true);
            foreach (var item in result.Windows)
            {
                list.Add(new WindowRecordSurface(item));
            }

            return list;
        }
    }
}