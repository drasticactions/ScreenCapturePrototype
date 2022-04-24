// <copyright file="MainWindow.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.ScreenCapture.Mac;

namespace ScreenCaptureMac
{
    public class MainWindow : NSWindow
    {
        private int NumberOfTimesClicked = 0;

        public NSButton ClickMeButton { get; set; }

        public NSTextField ClickMeLabel { get; set; }

        public MainWindow(CGRect contentRect, NSWindowStyle aStyle, NSBackingStore bufferingType, bool deferCreation) : base(contentRect, aStyle, bufferingType, deferCreation)
        {
            this.Title = "ScreenCaptureMac";

            // Create the content view for the window and make it fill the window
            this.ContentView = new NSView(this.Frame);

            // Add UI Elements to window
            ClickMeButton = new NSButton(new CGRect(10, Frame.Height - 70, 100, 30))
            {
                AutoresizingMask = NSViewResizingMask.MinYMargin
            };
            ContentView.AddSubview(ClickMeButton);

            //ClickMeLabel = new NSTextField(new CGRect(120, Frame.Height - 65, Frame.Width - 130, 20))
            //{
            //    BackgroundColor = NSColor.Clear,
            //    TextColor = NSColor.Black,
            //    Editable = false,
            //    Bezeled = false,
            //    AutoresizingMask = NSViewResizingMask.WidthSizable | NSViewResizingMask.MinYMargin,
            //    StringValue = "Button has not been clicked yet."
            //};
            //ContentView.AddSubview(ClickMeLabel);
        }

        /// <inheritdoc/>
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            //// Wireup events
            ClickMeButton.Activated += async (sender, e) =>
            {
                await MonitorEnumerationHelper.GetMonitorsAsync();
            };
        }
    }
}