using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using ITHSystems.Model;
using ITHSystems.Repositories.SQLite;
using ITHSystems.Resx;
using ITHSystems.Services.General;
using ITHSystems.Views.Home;
using System.Diagnostics;

namespace ITHSystems.Views.Login;
[RegisterViewModsel]
public partial class LoginPageViewModel : BaseViewModel
{
    private IEnumerable<Language> Languages = Language.List();
    private IPreferencesService preference;

    [ObservableProperty]
    private string languageName;
    private Language? CurrentLanguage;
    private readonly ISQLiteManager managerSQLite;
    private readonly IRepository<User> userRepository;
    private readonly IITHNavigationService iTHNavigation;

    public LoginPageViewModel(ISQLiteManager managerSQLite, IRepository<User> userRepository, IPreferencesService preference, IITHNavigationService iTHNavigation)
    {
        this.managerSQLite = managerSQLite;
        this.userRepository = userRepository;
        this.iTHNavigation = iTHNavigation;
        this.preference = ITHPreference.Instance;
        LanguageName = preference.Get(nameof(Language), Language.Spanish.Code);
    }

    [RelayCommand]
    public async Task Login()
    {
        try
        {
            if (IsBusy) return;
            IsBusy = true;
            await userRepository.GetAllAsync();
            await iTHNavigation.PushRelativePageAsync<HomePage>();
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

    public async Task Login(UserDto userDto)
    {
        try
        {
            if (userDto == null) return;

            if (IsBusy) return;
            IsBusy = true;
            var users = await userRepository.GetAllAsync();
            var user = users.FirstOrDefault();

            if(users.Any(x => x.UserName == userDto.UserName && x.Password == userDto.Password))
            {
                await iTHNavigation.PushRelativePageAsync<HomePage>();
            }
            await iTHNavigation.SuccessAlert("Alerta", $"Usuario no encontrado.");
            return;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error during login: {e.Message}");
            await iTHNavigation.ErrorAlert(IBSResources.Error, $"Error login\n{e.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
    private bool userIsNotValid(UserDto userDto)
    {
        if (string.IsNullOrEmpty(userDto.UserName)) return true;
        if (string.IsNullOrEmpty(userDto.Name)) return true;
        if (string.IsNullOrEmpty(userDto.Password)) return true;
        if (string.IsNullOrEmpty(userDto.Email)) return true;
        return false;

    }


    public async Task RegisterUser(UserDto userDto)
    {
        try
        {
            if (userDto == null) return;
            if(userIsNotValid(userDto)) return;
            if (IsBusy) return;

            IsBusy = true;
            await userRepository.InsertAsync(userDto.Map<User>());
            await iTHNavigation.PushRelativePageAsync<HomePage>();
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error during login: {e.Message}");
            await iTHNavigation.ErrorAlert(IBSResources.Error, $"Error login\n{e.Message}");
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
        catch (Exception e)
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
            if (CurrentLanguage is null) return;
            LanguageName = CurrentLanguage.Code;
            AppShell.ChangeLanguage?.Invoke(LanguageName);
            await Task.Delay(4000);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error changing language {e.Message}");
            await ErrorAlert("Error", "Error seleccionando un idioma");
        }
        finally
        {
            IsBusy = false;
        }

    }
}