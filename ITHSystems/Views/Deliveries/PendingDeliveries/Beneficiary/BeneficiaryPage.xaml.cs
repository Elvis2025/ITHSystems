using ITHSystems.Attributes;
using System.Threading.Tasks;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary;
[RegisterAsRoute]
public partial class BeneficiaryPage : ContentPage
{
    public BeneficiaryPage(BeneficiaryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    
}