using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Maui.Views;

namespace ITHSystems.Views.Login;

public partial class SettingOfTenantPopup : Popup
{
	public SettingOfTenantPopup(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        LoginPageViewModel.isSettingTenantOpened = true;
    }

	
}