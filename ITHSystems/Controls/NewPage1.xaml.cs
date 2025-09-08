using ITHSystems.Attributes;

namespace ITHSystems.Controls;
[RegisterAsRoute]
public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}
    private async void OnSnap(object sender, EventArgs e)
    {
        await Cam.CaptureImage(CancellationToken.None);

    }

    private void Cam_MediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    {
        if (Dispatcher.IsDispatchRequired)
        {
            Dispatcher.Dispatch(() => Foto.Source = ImageSource.FromStream(() => e.Media));
        }

        Foto.Source = ImageSource.FromStream(() => e.Media);
    }
}