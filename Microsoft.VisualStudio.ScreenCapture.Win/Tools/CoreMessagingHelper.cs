﻿// <copyright file="CoreMessagingHelper.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System;
using System.Runtime.InteropServices;
using Windows.System;

namespace Microsoft.VisualStudio.ScreenCapture.Win
{
    public static class CoreMessagingHelper
    {
        enum DISPATCHERQUEUE_THREAD_APARTMENTTYPE
        {
            DQTAT_COM_NONE = 0,
            DQTAT_COM_ASTA = 1,
            DQTAT_COM_STA = 2
        }

        enum DISPATCHERQUEUE_THREAD_TYPE
        {
            DQTYPE_THREAD_DEDICATED = 1,
            DQTYPE_THREAD_CURRENT = 2
        }

        struct DispatcherQueueOptions
        {
            public int dwSize;
            public DISPATCHERQUEUE_THREAD_TYPE threadType;
            public DISPATCHERQUEUE_THREAD_APARTMENTTYPE apartmentType;
        }

        [DllImport(
            "CoreMessaging.dll",
            EntryPoint = "CreateDispatcherQueueController",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall
            )]

        static extern UInt32 CreateDispatcherQueueController(DispatcherQueueOptions options, out IntPtr dispatcherQueueController);

        public static DispatcherQueueController? CreateDispatcherQueueControllerForCurrentThread()
        {
            var options = new DispatcherQueueOptions
            {
                dwSize = Marshal.SizeOf<DispatcherQueueOptions>(),
                threadType = DISPATCHERQUEUE_THREAD_TYPE.DQTYPE_THREAD_CURRENT,
                apartmentType = DISPATCHERQUEUE_THREAD_APARTMENTTYPE.DQTAT_COM_NONE,
            };

            DispatcherQueueController? controller = null;
            uint hr = CreateDispatcherQueueController(options, out IntPtr controllerPointer);
            if (hr == 0)
            {
                controller = DispatcherQueueController.FromAbi(controllerPointer);
                Marshal.Release(controllerPointer);
            }

            return controller;
        }
    }
}
