using ITHSystems.Attributes;
using ITHSystems.DTOs;

namespace ITHSystems.Views.Deliveries.PendingDeliveries;

[RegisterAsRoute]
public partial class PendingDeliveries : BaseContentPage<PendingDeliveriesViewModel>
{

    public PendingDeliveries(PendingDeliveriesViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
    }

   
}