using ITHSystems.Attributes;

namespace ITHSystems.Views.Home;

[RegisterAsRoute]
public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}