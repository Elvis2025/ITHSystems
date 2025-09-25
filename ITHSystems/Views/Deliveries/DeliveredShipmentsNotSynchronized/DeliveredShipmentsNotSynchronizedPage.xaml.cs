using ITHSystems.Attributes;
using ITHSystems.Views;

namespace ITHSystems.Views.Deliveries.DeliveredShipmentsNotSynchronized;
[RegisterAsRoute]
public partial class DeliveredShipmentsNotSynchronizedPage : BaseContentPage<DeliveredShipmentsNotSynchronizedViewModel>
{
	public DeliveredShipmentsNotSynchronizedPage(DeliveredShipmentsNotSynchronizedViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}