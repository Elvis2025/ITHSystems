using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverBeneficiary;

[RegisterAsRoute]
public partial class DeliverBenefiaryPage : BaseContentPage<DeliverBeneficiaryViewModel>
{
    public DeliverBenefiaryPage(DeliverBeneficiaryViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
    }

    
}