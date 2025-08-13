using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using ITHSystems.Attributes;
using ITHSystems.Resx;
using System.Diagnostics;

namespace ITHSystems.Views.Login;


public partial class LoginPage : ContentPage
{
    private readonly LoginPageViewModel viewModel;

    public LoginPage(LoginPageViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
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
			await viewModel.ErrorAlert("Error", $"Error creando tablas\n{e.Message}");
		}
    }
}