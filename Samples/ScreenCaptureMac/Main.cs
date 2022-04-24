// <copyright file="Main.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using ScreenCaptureMac;

// This is the main entry point of the application.
NSApplication.Init();

NSApplication.SharedApplication.Delegate = new AppDelegate();

NSApplication.Main(args);