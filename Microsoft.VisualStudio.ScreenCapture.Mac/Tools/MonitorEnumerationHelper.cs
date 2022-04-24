// <copyright file="MonitorEnumerationHelper.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using ScreenCaptureKit;

namespace Microsoft.VisualStudio.ScreenCapture.Mac
{
    /// <summary>
    /// Monitor Enumeration Helper.
    /// </summary>
    public static class MonitorEnumerationHelper
    {
        public static async Task<IReadOnlyList<MonitorInfo>> GetMonitorsAsync()
        {
            var list = new List<MonitorInfo>();
            var result = await SCShareableContent.GetShareableContentAsync(true, false);
            foreach (var item in result.Displays)
            {
                list.Add(new MonitorInfo(item));
            }

            return list;
        }
    }
}