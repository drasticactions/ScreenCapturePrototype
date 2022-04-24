// <copyright file="AppDelegate.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

namespace ScreenCaptureMac;

/// <summary>
/// App Delegate.
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : NSApplicationDelegate
{
    private MainWindowController? mainWindowController;

    /// <inheritdoc/>
    public override void DidFinishLaunching(NSNotification notification)
    {
        this.mainWindowController = new MainWindowController();
        this.mainWindowController.Window.MakeKeyAndOrderFront(this);
    }
}
