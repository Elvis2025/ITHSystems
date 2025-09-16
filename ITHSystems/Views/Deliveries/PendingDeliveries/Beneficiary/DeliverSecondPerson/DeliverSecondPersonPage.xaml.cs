using ITHSystems.Attributes;
using ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverBeneficiary;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverSecondPerson;
[RegisterAsRoute]
public partial class DeliverSecondPersonPage : ContentPage
{
	public DeliverSecondPersonPage(DeliverBeneficiaryViewModel viewModel)
	{
		InitializeComponent();
        viewModel.IsSecondPerson = true;
        BindingContext = viewModel;

    }

    
}