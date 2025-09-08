using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverBeneficiary;

[RegisterAsRoute]
public partial class DeliverBenefiaryPage : ContentPage
{
	public DeliverBenefiaryPage(DeliverBeneficiaryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        //await MainThread.InvokeOnMainThreadAsync(async () =>
        //{
        //    // await MediaPicker.CapturePhotoAsync();
        //    var cameraPermissionStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

        //    if (!cameraPermissionStatus.Equals(PermissionStatus.Granted))
        //    {
        //        // var cameraPermission  = await Permissions.RequestAsync<Permissions.Camera>();

        //        await Permissions.RequestAsync<Permissions.Camera>();
        //        await Shell.Current.DisplayAlert("Error", $"Abusador", "OK");
        //        return;
        //    }
        //    //  var cameraPermission = await Permissions.CheckStatusAsync<Permissions.Camera>();

        //});
    }

    private void ShowImageId(object sender, TappedEventArgs e)
    {

    }
}