using ITHSystems.Attributes;

namespace ITHSystems.Views.Deliveries;
[RegisterAsRoute]
public partial class DeliveriesPage : BaseContentPage<DeliveriesViewModel>
{
    private readonly DeliveriesViewModel viewModel;

    public DeliveriesPage(DeliveriesViewModel viewModel) : base(viewModel)
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