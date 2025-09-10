using System.Text.RegularExpressions;
using ZXing.Net.Maui;

namespace ITHSystems.Controls;

public partial class CameraCodeBarControlPage : ContentPage
{
    public CameraCodeBarControlPage()
    {
        InitializeComponent();

        // Configurar formatos 1D típicos (sin PDF417)
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

    private void OnBarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        // Evita múltiples lecturas seguidas
        BarcodeReader.IsDetecting = false;

        var result = e.Results?.FirstOrDefault();
        if (result is null)
        {
            BarcodeReader.IsDetecting = true;
            return;
        }

        var raw = result.Value?.Trim() ?? string.Empty;

        // Busca 11 dígitos (la cédula)
        var m = Regex.Match(raw, @"\b(\d{11})\b");
        if (!m.Success)
        {
            // Reinicia si no encontró 11 dígitos
            MainThread.BeginInvokeOnMainThread(() => BarcodeReader.IsDetecting = true);
            return;
        }

        var cedula = m.Groups[1].Value;

        if (!EsCedulaValida(cedula))
        {
            // Si el contenido del PDF417 trae más campos, puedes parsear/limpiar aquí
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("Cédula inválida", "El código leído no pasó la validación.", "OK");
                BarcodeReader.IsDetecting = true;
            });
            return;
        }

        var formatted = $"{cedula.Substring(0, 3)}-{cedula.Substring(3, 7)}-{cedula.Substring(10, 1)}";

        MainThread.BeginInvokeOnMainThread(() =>
        {
            CedulaEntry.Text = formatted;
            // Si quieres seguir escaneando automáticamente, vuelve a activar:
            // BarcodeReader.IsDetecting = true;
        });
    }

    private bool EsCedulaValida(string c)  // Validador típico RD (módulo 10 tipo Luhn)
    {
        if (string.IsNullOrWhiteSpace(c) || c.Length != 11) return false;

        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            int d = c[i] - '0';
            int factor = (i % 2 == 0) ? 1 : 2;  // alterna 1,2 desde la izquierda
            int prod = d * factor;
            sum += (prod > 9) ? (prod - 9) : prod;
        }
        int check = (10 - (sum % 10)) % 10;
        return check == (c[10] - '0');
    }
}