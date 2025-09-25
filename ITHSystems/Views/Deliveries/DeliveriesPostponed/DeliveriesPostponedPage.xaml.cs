using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries.DeliveriesPostponed;
[RegisterAsRoute]
public partial class DeliveriesPostponedPage : BaseContentPage<DeliveriesPostponedViewModel>
{
	public DeliveriesPostponedPage(DeliveriesPostponedViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}