using System.Threading.Tasks;

namespace ITHSystems.Controls;

public partial class ImageViewControl : ContentPage
{
    private readonly ImageSource imageSource;
    public static bool IsGoBack { get; set; } = false;
    public Action? CameraOn { get;set; }

    public ImageViewControl(ImageSource imageSource)
	{
		InitializeComponent();
        this.imageSource = imageSource;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if(IsGoBack)
        {
            await Navigation.PopAsync();
            IsGoBack = false;
            return;
        }

        Img.Source = imageSource;
    }

    public async void OnClose(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    public async void OpenCamera(object sender, EventArgs e)
    {
        if(CameraOn is null)
        {
            await Navigation.PopAsync();
            return;
        }
        CameraOn?.Invoke();
        IsGoBack = true;
    }


}