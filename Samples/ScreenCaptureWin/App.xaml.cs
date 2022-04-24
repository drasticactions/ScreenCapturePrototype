// <copyright file="App.xaml.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.ScreenCapture.Win;
using System.Windows;
using Windows.System;

namespace ScreenCaptureWin
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            _controller = CoreMessagingHelper.CreateDispatcherQueueControllerForCurrentThread() ?? throw new NullReferenceException();
        }

        private DispatcherQueueController _controller;
    }
}
