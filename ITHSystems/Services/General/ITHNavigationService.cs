using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using ITHSystems.Extensions;
using ITHSystems.Resx;
using System.Diagnostics;

namespace ITHSystems.Services.General;

public class ITHNavigationService : IITHNavigationService
{

    private static INavigation Nav =>
        Application.Current?.Windows.FirstOrDefault()?.Page?.Navigation
        ?? throw new InvalidOperationException(
    "No hay NavigationPage en la raíz. Envuelve tu MainPage en un NavigationPage.");

    public async Task PopPopupAsync()
    {
        try
        {
            await Shell.Current.ClosePopupAsync();
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
        }
    }

    public Task PushAsync(Page page) =>
      page is null
          ? Task.FromException(new ArgumentNullException(nameof(page)))
          : MainThread.InvokeOnMainThreadAsync(() => Nav.PushAsync(page));

    public async Task PushPageAsync<T>() where T : ContentPage
    {
        try
        {
            await Shell.Current.GoToAsync($"//{typeof(T).Name}");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            await Shell.Current.DisplayAlert("Spend Flow Error", e.Message, IBSResources.Ok);
        }
    }

    public async Task PushPopupAsync<T>() where T : Popup
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

    public async Task PushRelativePageAsync<T>() where T : ContentPage
    {
        try
        {
            await Shell.Current.GoToAsync($"{typeof(T).Name}");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            await Shell.Current.DisplayAlert("Spend Flow Error", e.Message, IBSResources.Ok);
        }
    }

    public async Task PushRelativePageAsync<T>(Dictionary<string, object> param) where T : ContentPage
    {
        try
        {
            await Shell.Current.GoToAsync($"{typeof(T).Name}", param);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            await Shell.Current.DisplayAlert("Spend Flow Error", e.Message, IBSResources.Ok);
        }
    }

    public T CreateInstance<T>() where T : class
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
}
