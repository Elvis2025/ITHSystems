using ITHSystems.Attributes;
using ITHSystems.DTOs;

namespace ITHSystems.Views.Deliveries.PendingDeliveries;

[RegisterAsRoute]
public partial class PendingDeliveries : BaseContentPage<PendingDeliveriesViewModel>
{
    private readonly PendingDeliveriesViewModel viewModel;

    public PendingDeliveries(PendingDeliveriesViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
        this.viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.init();
    }
   
}