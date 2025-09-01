using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using ITHSystems.Resx;
using ITHSystems.UsesCases.IconFonts;
using ITHSystems.Views.Deliveries;
using ITHSystems.Views.Login;
using ITHSystems.Views.PickupService;
using System.Diagnostics;

namespace ITHSystems.Views;

public abstract partial class BaseViewModel : ObsevablePropertiesViewModel
{
    [RelayCommand]
    public void ShowPassword()
    {
        IsPassword = false;
        PasswordEye = IconsTwoTone.Visibility;
        _ = Task.Run(async () =>
        {
            await Task.Delay(500);
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                IsPassword = true;
                PasswordEye = IconsTwoTone.Visibility_off;
            });

        });
    }




    public static async Task PushPageAsync<T>() where T : ContentPage
    {
        try
        {
            var name = typeof(T).Name;
            await Shell.Current.GoToAsync($"//{typeof(T).Name}");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            await Shell.Current.DisplayAlert("Spend Flow Error", e.Message, IBSResources.Ok);
        }
    }
    public static async Task PushRelativePageAsync<T>() where T : ContentPage
    {
        try
        {
            var name = typeof(T).Name;
            await Shell.Current.GoToAsync($"{typeof(T).Name}");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            await Shell.Current.DisplayAlert("Spend Flow Error", e.Message, IBSResources.Ok);
        }
    }


    public static async Task PushPopup<T>() where T : Popup
    {
        try
        {
            var popup = CreateInstance<T>();
            await Shell.Current.ShowPopupAsync(popup);
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
        }
    }

    public static T CreateInstance<T>() where T : class
    {
        return ActivatorUtilities.GetServiceOrCreateInstance<T>(Application.Current!.Handler.MauiContext!.Services!);
    }

    public async Task SuccessAlert(string title, string message)
    {
        await Shell.Current.DisplayAlert(title, message, IBSResources.Ok);
    }

    public async Task ErrorAlert(string title, string message)
    {
        await Shell.Current.DisplayAlert(title, message, IBSResources.Ok);
    }
    public async Task WarningAlert(string title, string message)
    {
        await Shell.Current.DisplayAlert(title, message, IBSResources.Ok);
    }

    [RelayCommand]
    public async Task GoToModule(ModuleDTO moduleDTO)
    {
        if (IsBusy) return;
        IsBusy = true;
        CurrentModule = moduleDTO;
        try
        {
            if (CurrentModule is null || !CurrentModule.IsVisible || CurrentModule.IsDeleted) return;
            switch (CurrentModule.Modules)
            {
                case Enums.Modules.PICKUPSERVICE:
                    await PushRelativePageAsync<PickupServicePage>();
                    break;
                case Enums.Modules.DELIVERIES:
                    await PushRelativePageAsync<DeliveriesPage>();
                    break;
                default:
                    await WarningAlert("Warning", $"The module {CurrentModule.Modules.ToString()} is not implemented");
                    break;
            }

        }
        finally
        {
            IsBusy = false;
            CurrentModule = null;
        }
    }

    [RelayCommand]
    public async Task GoBack(bool goToLogin = false)
    {
        try
        {
            if(IsBusy) return;
            IsBusy = true;
            if(goToLogin) await PushPageAsync<LoginPage>();
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
            await Shell.Current.DisplayAlert("iThot system navigation Error", e.Message, IBSResources.Ok);
        }
        finally
        {
            IsBusy = false;
        }
       
    }
}
