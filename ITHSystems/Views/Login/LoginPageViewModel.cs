using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Constants;
using ITHSystems.Repositories;
using ITHSystems.Resx;
using ITHSystems.Views.Home;
using System.Diagnostics;

namespace ITHSystems.Views.Login;

public partial class LoginPageViewModel : BaseViewModel
{
    private IEnumerable<Language> Languages = Language.List();
    [ObservableProperty]
    private string languageName = Preferences.Get(nameof(Language), Language.Spanish.Code);
    private Language? CurrentLanguage;
    private readonly ISQLiteManager managerSQLite;

    public LoginPageViewModel(ISQLiteManager managerSQLite)
    {
        this.managerSQLite = managerSQLite;
    }

    [RelayCommand]
    public async Task Login()
    {
               try
        {
            if (IsBusy) return;
            IsBusy = true;
            await PushRelativePageAsync<HomePage>();
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error during login: {e.Message}");
            await ErrorAlert(IBSResources.Error, $"Error login\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }


   public async Task Init()
    {
        try
        {
            managerSQLite.CreateTablesUnAsync();
        }
        catch (Exception e )
        {
            Debug.WriteLine($"Error creating tables: {e.Message}");
            throw;
        }
    }

    [RelayCommand]
    public async Task ChangeLanguage()
    {
        if (IsBusy) return;
        try
        {

            Languages = Language.List();
            var language = await Shell.Current.DisplayActionSheet(
                                                 IBSResources.Languages,
                                                 IBSResources.Cancel,
                                                 null,
                                                 Languages.Select(x => x.Name.Split('-').LastOrDefault()).ToArray());
            if (string.IsNullOrEmpty(language) || language == CurrentLanguage?.Name) return;
            IsBusy = true;
            await Task.Delay(300);
            CurrentLanguage = Languages.FirstOrDefault(x => x.Name == language)!;
            LanguageName = CurrentLanguage.Code;
            AppShell.ChangeLanguage?.Invoke(LanguageName);
            await Task.Delay(4000);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error changing language {e.Message}");
            throw;
        }
        finally
        {
            IsBusy = false;
        }

    }
}
