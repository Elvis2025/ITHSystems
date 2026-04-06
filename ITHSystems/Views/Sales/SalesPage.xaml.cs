using ITHSystems.Attributes;
using ITHSystems.Views.PickupService;

namespace ITHSystems.Views.Sales;

[RegisterAsRoute]
public partial class SalesPage : BaseContentPage<SalesViewModel>
{
	public SalesPage(SalesViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}