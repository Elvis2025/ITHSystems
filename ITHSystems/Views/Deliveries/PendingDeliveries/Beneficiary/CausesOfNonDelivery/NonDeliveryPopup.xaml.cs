using CommunityToolkit.Maui.Views;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.CausesOfNonDelivery;

public partial class NonDeliveryPopup : Popup
{
	public NonDeliveryPopup(CausesOfNonDeliveryViewModel viewModel)
	{
		InitializeComponent();
		viewModel.IsBusy = false;
		BindingContext = viewModel;
    }
    
}