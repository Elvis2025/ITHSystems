using System.Windows.Input;
using ZXing.Net.Maui;

namespace ITHSystems.Controls;

public partial class BarcodeScannerPage : ContentPage
{
    public Action<string>? OnBarcodeScanned { get; set; }
    public Action? OnCancel { get; set; }
    public BarcodeScannerPage()
    {
        InitializeComponent();

        BarcodeReader.Options = new BarcodeReaderOptions
        {
            // Habilita los lineales más comunes
            Formats =
               BarcodeFormats.OneDimensional,

            AutoRotate = true,
            TryHarder = true,
            Multiple = false
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BarcodeReader.IsDetecting = true;
        BarcodeReader.CameraLocation = CameraLocation.Rear;
    }

    protected override void OnDisappearing()
    {
        BarcodeReader.IsDetecting = false;          // libera la cámara al salir
        base.OnDisappearing();
    }
  

   

    private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        BarcodeReader.IsDetecting = false;

        var result = e.Results?.FirstOrDefault();
        if (result is null)
        {
            BarcodeReader.IsDetecting = true;
            return;
        }

        var raw = result.Value?.Trim() ?? string.Empty;

        // Busca 11 dígitos (la cédula)
        if (string.IsNullOrEmpty(raw))
        {
            // Reinicia si no encontró 11 dígitos
            MainThread.BeginInvokeOnMainThread(() => BarcodeReader.IsDetecting = true);
            return;
        }
        OnBarcodeScanned?.Invoke(raw);
    }

    public ICommand CancelCommand => new Command(() =>
    {
        OnCancel?.Invoke();
    });
}