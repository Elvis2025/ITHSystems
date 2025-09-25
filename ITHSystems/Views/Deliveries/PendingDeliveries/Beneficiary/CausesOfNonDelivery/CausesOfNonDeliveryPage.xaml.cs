using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.CausesOfNonDelivery;
[RegisterAsRoute]
public partial class CausesOfNonDeliveryPage : BaseContentPage<CausesOfNonDeliveryViewModel>
{
	public CausesOfNonDeliveryPage(CausesOfNonDeliveryViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}