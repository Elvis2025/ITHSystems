using ITHSystems.Attributes;
using System.Diagnostics;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverBeneficiary;

[RegisterAsRoute]
public partial class DeliverBenefiaryPage : ContentPage
{
    private bool _isUpdating;

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

    private void CedulaEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not Entry entry) return;
        if (_isUpdating) return;

        try
        {
            var raw = e.NewTextValue ?? string.Empty;

            // Mantén solo dígitos y limita a 11
            var digits = new string(raw.Where(char.IsDigit).ToArray());
            if (digits.Length > 11) digits = digits[..11];

            // ###-########-#
            string formatted =
                digits.Length <= 3
                    ? digits
                    : $"{digits[..3]}-{digits[3..]}";

            if (digits.Length == 11)
                formatted = $"{digits[..3]}-{digits.Substring(3, 8)}-{digits[11..]}";

            if (formatted == raw) return;

            _isUpdating = true;

            // Evitar re-entrada durante la reasignación
            entry.TextChanged -= CedulaEntry_TextChanged;
            entry.Text = formatted;
            entry.TextChanged += CedulaEntry_TextChanged;

          
               
               
                  



        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        
    }

    private void Label_BindingContextChanged(object sender, EventArgs e)
    {

    }
}