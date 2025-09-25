using ITHSystems.Attributes;

namespace ITHSystems.Views.PickupService;
[RegisterAsRoute]
public partial class PickupServicePage : BaseContentPage<PickupServiceViewModel>
{
	public PickupServicePage(PickupServiceViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
    }
}