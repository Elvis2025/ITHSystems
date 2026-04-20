using ITHSystems.Attributes;

namespace ITHSystems.Views.Login;
[RegisterAsRoute]
public partial class SettingOfTenantPage : BaseContentPage<LoginPageViewModel>
{
    private readonly LoginPageViewModel viewModel;

    public SettingOfTenantPage(LoginPageViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
        LoginPageViewModel.isSettingTenantOpened = true;
        this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoginPageViewModel.isSettingTenantOpened = true;
       await viewModel.SetUserTenantInfo();
    }
	
}