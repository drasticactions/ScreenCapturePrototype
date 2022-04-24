// <copyright file="MainWindow.xaml.cs" company="Microsoft Corp">
// Copyright (c) Microsoft Corp. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Microsoft.VisualStudio.ScreenCapture.Win;
using Windows.Foundation.Metadata;
using Windows.Graphics.Capture;
using Windows.UI.Composition;

namespace ScreenCaptureWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntPtr hwnd;
        private Compositor? compositor;
        private CompositionTarget? target;
        private ContainerVisual? root;

        private CaptureLayer? sample;

        private ObservableCollection<ProcessRecordSurface> processes = new ObservableCollection<ProcessRecordSurface>();
        private ObservableCollection<MonitorRecordSurface> monitors = new ObservableCollection<MonitorRecordSurface>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

#if DEBUG
            // Force graphicscapture.dll to load.
            var picker = new GraphicsCapturePicker();
#endif
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var interopWindow = new WindowInteropHelper(this);
            this.hwnd = interopWindow.Handle;

            var presentationSource = PresentationSource.FromVisual(this);
            double dpiX = 1.0;
            double dpiY = 1.0;
            if (presentationSource != null)
            {
                dpiX = presentationSource.CompositionTarget.TransformToDevice.M11;
                dpiY = presentationSource.CompositionTarget.TransformToDevice.M22;
            }

            var controlsWidth = (float)(this.ControlsGrid.ActualWidth * dpiX);

            this.InitComposition(controlsWidth);
            this.InitProcessList();
            this.InitMonitorList();
        }

        private void InitProcessList()
        {
            if (ApiInformation.IsApiContractPresent(typeof(Windows.Foundation.UniversalApiContract).FullName, 8))
            {
                this.WindowComboBox.ItemsSource = this.processes = new ObservableCollection<ProcessRecordSurface>(WindowEnumerationHelper.GetMainCaptureSurfacesViaProcesses());
            }
            else
            {
                this.WindowComboBox.IsEnabled = false;
            }
        }

        private void InitMonitorList()
        {
            if (ApiInformation.IsApiContractPresent(typeof(Windows.Foundation.UniversalApiContract).FullName, 8))
            {
                this.monitors = new ObservableCollection<MonitorRecordSurface>(MonitorEnumerationHelper.GetCaptureSurfacesViaMonitors());
                this.MonitorComboBox.ItemsSource = monitors;
            }
            else
            {
                this.MonitorComboBox.IsEnabled = false;
                this.PrimaryMonitorButton.IsEnabled = false;
            }
        }

        private void MonitorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var monitor = (MonitorRecordSurface)comboBox.SelectedItem;

            if (monitor != null && monitor.MonitorInfo is MonitorInfo monitorInfo && monitor.CaptureSurface is GraphicsCaptureItem item)
            {
                this.StopCapture();
                this.WindowComboBox.SelectedIndex = -1;
                var hmon = monitorInfo.Hmon;
                try
                {
                    this.sample?.StartCaptureFromItem(item);
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Hmon 0x{hmon.ToInt32():X8} is not valid for capture!");
                    this.monitors.Remove(monitor);
                    comboBox.SelectedIndex = -1;
                }
            }
        }

        private void WindowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var process = (ProcessRecordSurface)comboBox.SelectedItem;

            if (process != null && process.CaptureSurface is GraphicsCaptureItem item)
            {
                this.StopCapture();
                this.MonitorComboBox.SelectedIndex = -1;
                try
                {
                    this.hwnd = process.Process.MainWindowHandle;
                    this.sample?.StartCaptureFromItem(item);
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Hwnd 0x{hwnd.ToInt32():X8} is not valid for capture!");
                    this.processes.Remove(process);
                    comboBox.SelectedIndex = -1;
                }
            }
        }

        private void PrimaryMonitorButton_Click(object sender, RoutedEventArgs e)
        {
            this.StopCapture();
            this.WindowComboBox.SelectedIndex = -1;
            this.MonitorComboBox.SelectedIndex = -1;
            this.StartPrimaryMonitorCapture();
        }

        private void InitComposition(float controlsWidth)
        {
            // Create the compositor.
            this.compositor = new Compositor();

            // Create a target for the window.
            this.target = this.compositor.CreateDesktopWindowTarget(hwnd, true);

            // Attach the root visual.
            this.root = this.compositor.CreateContainerVisual();
            this.root.RelativeSizeAdjustment = Vector2.One;
            this.root.Size = new Vector2(-controlsWidth, 0);
            this.root.Offset = new Vector3(controlsWidth, 0, 0);
            this.target.Root = this.root;

            // Setup the rest of the sample application.
            this.sample = new CaptureLayer(this.compositor);
            this.root.Children.InsertAtTop(this.sample.Visual);
        }

        private async void PickerButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = this.CreateCapturePicker(this);
            var item = await picker.PickSingleItemAsync();
            if (item != null)
            {
                this.sample?.StartCaptureFromItem(item);
            }
        }

        private GraphicsCapturePicker CreateCapturePicker(System.Windows.Window window)
        {
            var interopWindow = new System.Windows.Interop.WindowInteropHelper(window);
            var hwnd = interopWindow.Handle;
            var picker = new GraphicsCapturePicker();
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
            return picker;
        }

        private void StopCapture()
        {
            this.sample?.StopCapture();
        }

        private void StartPrimaryMonitorCapture()
        {
            var monitor = (from m in MonitorEnumerationHelper.GetMonitors()
                                   where m.IsPrimary
                                   select m).First();

            if (monitor is MonitorInfo info)
            {
                this.sample?.StartCaptureFromItem(CaptureHelper.CreateItemForMonitor(info.Hmon));
            }
        }
    }
}
