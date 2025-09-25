using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries;
[RegisterAsRoute]
public partial class DeliveriesPage : BaseContentPage<DeliveriesViewModel>
{
	public DeliveriesPage(DeliveriesViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
    }
}