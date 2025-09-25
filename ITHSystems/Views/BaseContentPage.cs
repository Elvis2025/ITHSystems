using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Diagnostics;

namespace ITHSystems.Views;

public abstract partial class BaseContentPage<TViewModel> (TViewModel viewModel, bool shouldUseSafeArea = true) 
                            : BaseContentPage(viewModel, shouldUseSafeArea) where TViewModel 
                            : BaseViewModel
{
    public new TViewModel BindingContext => (TViewModel)base.BindingContext;
}

public abstract partial class BaseContentPage : ContentPage
{
    protected BaseContentPage(object? viewModel = null, bool shouldUseSafeArea = true)
    {
        BindingContext = viewModel;
        On<iOS>().SetUseSafeArea(shouldUseSafeArea);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Debug.WriteLine($"OnAppearing: {Title}");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        Debug.WriteLine($"OnDisappearing: {Title}");
    }
}