using ITHSystems.Attributes;
using System.Threading.Tasks;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary;
[RegisterAsRoute]
public partial class BeneficiaryPage : BaseContentPage<BeneficiaryViewModel>
{
    public BeneficiaryPage(BeneficiaryViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
    }

    
}