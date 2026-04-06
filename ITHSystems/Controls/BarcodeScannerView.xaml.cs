using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZXing;
using ZXing.Net.Maui;

namespace ITHSystems.Controls;

public partial class BarcodeScannerView : ContentView
{
    private bool _isProcessing;

    public BarcodeScannerView()
    {
        InitializeComponent();

        BarcodeReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            TryHarder = true,
            Multiple = false
        };

        Loaded += BarcodeScannerView_Loaded;
        Unloaded += BarcodeScannerView_Unloaded;

        StartScanningCommand = new RelayCommand(StartScanning);
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

    private static void OnIsScanningChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not BarcodeScannerView view || newValue is not bool isScanning)
            return;

        if (isScanning)
            view.StartScanning();
        else
            view.StopScanning();
    }

    private void BarcodeScannerView_Loaded(object? sender, EventArgs e)
    {
        if (IsScanning)
        {
            StartScanning();
        }
    }

    private void BarcodeScannerView_Unloaded(object? sender, EventArgs e)
    {
        StopScanning();
    }

    public void StartScanning()
    {
        BarcodeReader.CameraLocation = CameraLocation.Rear;
        BarcodeReader.IsDetecting = true;
        IsScanning = true;
    }

    public void StopScanning()
    {
        BarcodeReader.IsDetecting = false;
        IsScanning = false;
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
        StopScanning();

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
        }
        finally
        {
            _isProcessing = false;

            if (IsScanning)
            {
                StartScanning();
            }
        }
    }
}