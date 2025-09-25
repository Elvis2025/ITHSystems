using ITHSystems.Attributes;
using ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverBeneficiary;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary.DeliverBeneficiary;
[RegisterAsRoute]
public partial class DeliverSecondPersonPage : BaseContentPage<DeliverBeneficiaryViewModel>
{
    private readonly DeliverBeneficiaryViewModel viewModel;

    public DeliverSecondPersonPage(DeliverBeneficiaryViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        viewModel.IsSecondPerson = true;
        this.viewModel = viewModel;
    }

    override protected void OnAppearing()
    {
        base.OnAppearing();
        viewModel.IsSecondPerson = true;

    }
}