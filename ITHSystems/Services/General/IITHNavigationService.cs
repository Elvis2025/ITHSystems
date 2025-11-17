using CommunityToolkit.Maui.Views;

namespace ITHSystems.Services.General;

public interface IITHNavigationService
{
    Task PushRelativePageAsync<T>() where T : ContentPage;
    Task PushPageAsync<T>() where T : ContentPage;
    Task PushRelativePageAsync<T>(Dictionary<string, object> param) where T : ContentPage;
    Task PushPopupAsync<T>() where T : Popup;
    Task PopPopupAsync();
    Task PushAsync(Page page);
}
