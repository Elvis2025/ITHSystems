using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using System.Windows.Input;
using ZXing;
using ZXing.Net.Maui;

namespace ITHSystems.Controls;

public partial class BarcodeScannerView : ContentView
{
    private bool _isProcessing;
    private bool _isLoaded;
    private bool _isCameraPermissionGranted;
    private bool _isStarting;
    private Window? _window;

    public BarcodeScannerView()
    {
        InitializeComponent();

        BarcodeReader.Options = new BarcodeReaderOptions
        {
            AutoRotate = true,
            TryHarder = true,
            Multiple = false
        };

        Loaded += BarcodeScannerView_Loaded;
        Unloaded += BarcodeScannerView_Unloaded;

        StartScanningCommand = new AsyncRelayCommand(StartScanningAsync);
        StopScanningCommand = new RelayCommand(StopScanning);
    }

    public ICommand StartScanningCommand { get; }

    public ICommand StopScanningCommand { get; }

    public static readonly BindableProperty IsScanningProperty =
        BindableProperty.Create(
            nameof(IsScanning),
            typeof(bool),
            typeof(BarcodeScannerView),
            true,
            BindingMode.TwoWay,
            propertyChanged: OnIsScanningChanged);

    public bool IsScanning
    {
        get => (bool)GetValue(IsScanningProperty);
        set => SetValue(IsScanningProperty, value);
    }

    public static readonly BindableProperty BarcodeDetectedCommandProperty =
        BindableProperty.Create(
            nameof(BarcodeDetectedCommand),
            typeof(ICommand),
            typeof(BarcodeScannerView));

    public ICommand? BarcodeDetectedCommand
    {
        get => (ICommand?)GetValue(BarcodeDetectedCommandProperty);
        set => SetValue(BarcodeDetectedCommandProperty, value);
    }

    public static readonly BindableProperty BarcodeDetectedCommandParameterProperty =
        BindableProperty.Create(
            nameof(BarcodeDetectedCommandParameter),
            typeof(object),
            typeof(BarcodeScannerView),
            null);

    public object? BarcodeDetectedCommandParameter
    {
        get => GetValue(BarcodeDetectedCommandParameterProperty);
        set => SetValue(BarcodeDetectedCommandParameterProperty, value);
    }

    public static readonly BindableProperty LastDetectedBarcodeProperty =
        BindableProperty.Create(
            nameof(LastDetectedBarcode),
            typeof(string),
            typeof(BarcodeScannerView),
            string.Empty,
            BindingMode.TwoWay);

    public string LastDetectedBarcode
    {
        get => (string)GetValue(LastDetectedBarcodeProperty);
        set => SetValue(LastDetectedBarcodeProperty, value);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        UnsubscribeFromWindowEvents();

        if (Handler is not null)
        {
            _window = GetParentWindow();
            SubscribeToWindowEvents();
        }
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        UnsubscribeFromWindowEvents();
        base.OnHandlerChanging(args);
    }

    private static void OnIsScanningChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not BarcodeScannerView view || newValue is not bool isScanning)
            return;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (isScanning)
            {
                await view.StartScanningAsync();
            }
            else
            {
                view.StopScanning();
            }
        });
    }

    private async void BarcodeScannerView_Loaded(object? sender, EventArgs e)
    {
        _isLoaded = true;

        if (IsScanning)
        {
            await StartScanningAsync();
        }
    }

    private void BarcodeScannerView_Unloaded(object? sender, EventArgs e)
    {
        _isLoaded = false;
        StopScanning();
    }

    private void SubscribeToWindowEvents()
    {
        if (_window is null)
            return;

        _window.Resumed += Window_Resumed;
        _window.Stopped += Window_Stopped;
        _window.Destroying += Window_Destroying;
    }

    private void UnsubscribeFromWindowEvents()
    {
        if (_window is null)
            return;

        _window.Resumed -= Window_Resumed;
        _window.Stopped -= Window_Stopped;
        _window.Destroying -= Window_Destroying;
        _window = null;
    }

    private async void Window_Resumed(object? sender, EventArgs e)
    {
        if (!_isLoaded || !IsVisible || !IsScanning)
            return;

        await RestartScannerAsync();
    }

    private void Window_Stopped(object? sender, EventArgs e)
    {
        PauseScanner();
    }

    private void Window_Destroying(object? sender, EventArgs e)
    {
        PauseScanner();
    }

    public async Task StartScanningAsync()
    {
        if (_isStarting || !_isLoaded || !IsVisible)
            return;

        try
        {
            _isStarting = true;

            var permissionGranted = await EnsureCameraPermissionAsync();
            if (!permissionGranted)
            {
                StopScanning();
                return;
            }

            _isCameraPermissionGranted = true;

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                BarcodeReader.IsDetecting = false;
                BarcodeReader.CameraLocation = CameraLocation.Rear;

                // Pequeño reset para forzar reconstrucción del preview
                await Task.Delay(150);

                BarcodeReader.IsDetecting = true;
                if (!IsScanning)
                {
                    IsScanning = true;
                }
            });
        }
        finally
        {
            _isStarting = false;
        }
    }

    public void StopScanning()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            BarcodeReader.IsDetecting = false;

            if (IsScanning)
            {
                SetValue(IsScanningProperty, false);
            }
        });
    }

    private void PauseScanner()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            BarcodeReader.IsDetecting = false;
        });
    }

    private async Task RestartScannerAsync()
    {
        if (!_isCameraPermissionGranted)
        {
            var permissionGranted = await EnsureCameraPermissionAsync();
            if (!permissionGranted)
                return;

            _isCameraPermissionGranted = true;
        }

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            BarcodeReader.IsDetecting = false;
            await Task.Delay(200);

            BarcodeReader.CameraLocation = CameraLocation.Rear;
            BarcodeReader.IsDetecting = true;
        });
    }

    private static async Task<bool> EnsureCameraPermissionAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status == PermissionStatus.Granted)
            return true;

        status = await Permissions.RequestAsync<Permissions.Camera>();
        return status == PermissionStatus.Granted;
    }

    private async void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (_isProcessing)
            return;

        var result = e.Results?.FirstOrDefault();
        if (result is null)
            return;

        var barcodeValue = result.Value?.Trim();
        if (string.IsNullOrWhiteSpace(barcodeValue))
            return;

        _isProcessing = true;
        PauseScanner();

        try
        {
            LastDetectedBarcode = barcodeValue;

            if (BarcodeDetectedCommand?.CanExecute(barcodeValue) == true)
            {
                BarcodeDetectedCommand.Execute(barcodeValue);
            }
            else if (BarcodeDetectedCommand?.CanExecute(BarcodeDetectedCommandParameter) == true)
            {
                BarcodeDetectedCommand.Execute(BarcodeDetectedCommandParameter);
            }

            await Task.Delay(1200);

            if (IsScanning && _isLoaded && IsVisible)
            {
                await RestartScannerAsync();
            }
        }
        finally
        {
            _isProcessing = false;
        }
    }

    private Window? GetParentWindow()
    {
        Element? current = this;

        while (current is not null)
        {
            if (current is Page page && page.Window is not null)
                return page.Window;

            current = current.Parent;
        }

        return Application.Current?.Windows.FirstOrDefault();
    }
}