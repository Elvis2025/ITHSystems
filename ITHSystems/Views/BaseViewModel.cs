using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Extensions;
using ITHSystems.Resx;
using ITHSystems.UsesCases.IconFonts;
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
        catch (Exception)
        {

            throw;
        }
    }
    public static async Task PushRelativePageAsync<T>() where T : ContentPage
    {
        var name = typeof(T).Name;
        await Shell.Current.GoToAsync($"{typeof(T).Name}");
    }

    public static async Task PushPopup<T>() where T : Popup
    {
        try
        {
            var popup = UtilExtensions.CreateInstance<T>();
            await Shell.Current.ShowPopupAsync(popup);
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
        }
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
}
