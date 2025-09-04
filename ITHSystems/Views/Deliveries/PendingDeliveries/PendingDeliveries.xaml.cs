using ITHSystems.Attributes;
using ITHSystems.DTOs;

namespace ITHSystems.Views.Deliveries.PendingDeliveries;

[RegisterAsRoute]
public partial class PendingDeliveries : ContentPage
{
    private readonly PendingDeliveriesViewModel viewModel;

    public PendingDeliveries(PendingDeliveriesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }

   
}