using System.Diagnostics;

namespace ITHSystems.Views.Login;


public partial class LoginPage : BaseContentPage<LoginPageViewModel>
{
    private readonly LoginPageViewModel viewModel;

    public LoginPage(LoginPageViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		try
		{
			await viewModel.Init();
        }
        catch (Exception e)
		{
			Debug.WriteLine($"Error initializing LoginPage: {e.Message}");
			await BaseViewModel.ErrorAlert("Error", $"Error creando tablas\n{e.Message}");
		}
    }
}