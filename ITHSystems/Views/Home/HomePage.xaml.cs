using ITHSystems.Attributes;

namespace ITHSystems.Views.Home;

[RegisterAsRoute]
public partial class HomePage : BaseContentPage<HomePageViewModel>
{
	public HomePage(HomePageViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
    }
}