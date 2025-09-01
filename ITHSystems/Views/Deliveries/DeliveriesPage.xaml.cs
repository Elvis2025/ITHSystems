using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries;
[RegisterAsRoute]
public partial class DeliveriesPage : ContentPage
{
	public DeliveriesPage(DeliveriesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}