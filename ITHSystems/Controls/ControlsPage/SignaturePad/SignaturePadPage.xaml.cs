using ITHSystems.Attributes;
using System.Diagnostics;

namespace ITHSystems.Controls.ControlsPage.SignaturePad;

[RegisterAsRoute]
public partial class SignaturePadPage : ContentPage
{
    public static int DeliveryId { get; set; }
    public Action<ImageSource?, bool>? OnSaveSignatrue { get ; set; }

    public SignaturePadPage()
	{
		InitializeComponent();
    }

    private async void OnSaveTapped(object sender, TappedEventArgs e)
    {
        try
        {
            if (!Sig.IsLoaded)
            {
                await DisplayAlert("Firma", "No hay firma dibujada", "OK");
                return;
            }
             var imageSource = Sig.ToImageSource();

            if (imageSource is StreamImageSource sis)
            {
                using var stream = await sis.Stream(CancellationToken.None);

                var path = Path.Combine(FileSystem.AppDataDirectory,
                                        $"firma_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                using var file = File.OpenWrite(path);
                await stream.CopyToAsync(file);

                await DisplayAlert("Firma", $"Firma guardada en:\n{path}", "OK");

                OnSaveSignatrue?.Invoke(imageSource,true);
            }
            else
            {
                await DisplayAlert("Firma", "No se pudo exportar la firma.", "OK");
                OnSaveSignatrue?.Invoke(imageSource, false);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error al guardar la firma", ex.Message);
        }
    }

    private void OnClearTapped(object sender, TappedEventArgs e)
    {
        Sig.Clear();
    }
    
    async void OnCancelTapped(object sender, TappedEventArgs e)
    {
        var ok = await DisplayAlert("Cancelar", "¿Desea salir sin guardar?", "Sí", "No");
        if (ok) OnSaveSignatrue?.Invoke(null, false);
    }
}