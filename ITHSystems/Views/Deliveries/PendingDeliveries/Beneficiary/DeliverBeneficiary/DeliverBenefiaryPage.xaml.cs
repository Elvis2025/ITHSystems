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

    
}