using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary;
[RegisterAsRoute]
public partial class Beneficiary : ContentPage
{

    public Beneficiary(BeneficiaryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}