using ITHSystems.Attributes;
using ITHSystems.DTOs;

namespace ITHSystems.Views.Deliveries.PendingDeliveries.Beneficiary;
[RegisterAsRoute]
public partial class Beneficiary : ContentPage
{
    private readonly BeneficiaryViewModel viewModel;

    public Beneficiary(BeneficiaryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Person", out var value) && value is PersonDto p)
        {
            viewModel.Person = p;
        }
    }
}