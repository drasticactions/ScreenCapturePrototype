// <copyright file="MainWindow.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.ScreenCapture.Mac;

namespace ScreenCaptureMac
{
    public class MainWindow : NSWindow
    {
        private int NumberOfTimesClicked = 0;

        public FrameView FrameView { get; private set; }

        public NSButton ClickMeButton { get; set; }

        public NSTextField ClickMeLabel { get; set; }

        private MonitorRecordSurface Monitor { get; set; }

        public MainWindow(CGRect contentRect, NSWindowStyle aStyle, NSBackingStore bufferingType, bool deferCreation) : base(contentRect, aStyle, bufferingType, deferCreation)
        {
            this.FrameView = new FrameView();
            this.Title = "ScreenCaptureMac";

            // Create the content view for the window and make it fill the window
            this.ContentView = new NSView(this.Frame);

            // Add UI Elements to window
            ClickMeButton = new NSButton(new CGRect(10, Frame.Height - 70, 100, 30))
            {
                AutoresizingMask = NSViewResizingMask.MinYMargin
            };
            ContentView.AddSubview(ClickMeButton);
            ClickMeLabel = new NSTextField(new CGRect(120, Frame.Height - 65, Frame.Width - 130, 20))
            {
                BackgroundColor = NSColor.Clear,
                TextColor = NSColor.Black,
                Editable = false,
                Bezeled = false,
                AutoresizingMask = NSViewResizingMask.WidthSizable | NSViewResizingMask.MinYMargin,
                StringValue = "Button has not been clicked yet."
            };
            ContentView.AddSubview(ClickMeLabel);
        }

        /// <inheritdoc/>
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            //// Wireup events
            ClickMeButton.Activated += async (sender, e) =>
            {
                Task.Run(() =>
                {
                    var result = MonitorEnumerationHelper.GetMonitorsAsync().Result;
                    var test = result.First();
                    this.Monitor = new MonitorRecordSurface(test);
                    this.Monitor.ScreenRecorder.StartCapture();
                });
                //if (this.FrameView is not null)
                //{
                //    ContentView.WillRemoveSubview(this.FrameView);
                //}
                //this.ClickMeLabel.StringValue = "1";
                //var result = await MonitorEnumerationHelper.GetMonitorsAsync();
                //this.ClickMeLabel.StringValue = "2";
                //var test = result.First();
                //this.ClickMeLabel.StringValue = "3";
                //this.Monitor = new MonitorRecordSurface(test);
                //this.ClickMeLabel.StringValue = "4";
                //this.Monitor.ScreenRecorder.StartCapture();
                //this.ClickMeLabel.StringValue = "5";
                //this.FrameView = this.Monitor.ScreenRecorder.FrameView;
                //this.ClickMeLabel.StringValue = "6";
                //ContentView.AddSubview(this.FrameView);
            };
        }
    }
}