using ITHSystems.Attributes;

namespace ITHSystems.Views.PickupService;
[RegisterAsRoute]
public partial class PickupServicePage : ContentPage
{
	public PickupServicePage(PickupServiceViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}