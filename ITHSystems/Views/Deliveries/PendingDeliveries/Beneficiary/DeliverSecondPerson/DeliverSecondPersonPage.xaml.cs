using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverSecondPerson;
[RegisterAsRoute]
public partial class DeliverSecondPersonPage : ContentPage
{
	public DeliverSecondPersonPage(DeliverSecondPersonViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}