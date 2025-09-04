using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries.PendingDeliveries;

[RegisterAsRoute]
public partial class PendingDeliveries : ContentPage
{
	public PendingDeliveries(PendingDeliveriesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}