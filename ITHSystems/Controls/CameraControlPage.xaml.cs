using ITHSystems.Attributes;
using System.Diagnostics;

namespace ITHSystems.Controls;
public partial class CameraControlPage : ContentPage
{
    public static int DeliveryId { get; set; }
    public Action<ImageSource?, bool>? OnSaveSignatrue { get; set; }
    public CameraControlPage()
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

    //private void Cam_MediaCaptured(object sender, CommunityToolkit.Maui.Core.MediaCapturedEventArgs e)
    //{


    //    if (Dispatcher.IsDispatchRequired)
    //    {
    //        Dispatcher.Dispatch(() => Foto.Source = ImageSource.FromStream(() => e.Media));
    //    }

    //    Foto.Source = ImageSource.FromStream(() => e.Media);



    //}
}