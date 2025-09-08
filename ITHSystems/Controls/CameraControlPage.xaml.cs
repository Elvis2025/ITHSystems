using ITHSystems.Attributes;
using ITHSystems.Views;

namespace ITHSystems.Controls;
[RegisterAsRoute]
public partial class CameraControlPage : ContentPage
{
    public static int DeliveryId { get; set; }
    public Action<ImageSource?, bool>? OnSavePhoto { get; set; }
    public CameraControlPage()
	{
		InitializeComponent();
	}
    private async void OnSnap(object sender, EventArgs e)
    {
        try
        {
            await Cam.CaptureImage(CancellationToken.None);

        }
        catch (Exception ex)
        {
            await BaseViewModel.ErrorAlert("Camara", $"Error tomando la foto.{ex.Message}");
        }

    }

    private async void Cam_MediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    {
        try
        {
            byte[] imageBytes;
            using (var ms = new MemoryStream())
            {
                await e.Media.CopyToAsync(ms);
                imageBytes = ms.ToArray();
            }
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Foto.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            });

        }
        catch (Exception ex)
        {

            await BaseViewModel.ErrorAlert("Camara", $"Error tomando la foto.{ex.Message}");
        }
    }

    private async void ShowImage(object sender, TappedEventArgs e)
    {
     if (Foto.Source.IsEmpty) return;
        var imageView = new ImageViewControl(Foto.Source);
        await Navigation.PushAsync(imageView);

    }

    private void SavePhoto(object sender, EventArgs e)
    {
        if (Foto.Source.IsEmpty) return;
        OnSavePhoto?.Invoke(Foto.Source,true);
    }

    private void CancelPhoto(object sender, EventArgs e)
    {
        OnSavePhoto?.Invoke(null,false);
    }


}