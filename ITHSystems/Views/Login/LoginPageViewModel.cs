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
using ITHSystems.Services.Login;
using ITHSystems.Views.Home;
using System.Diagnostics;

namespace ITHSystems.Views.Login;

[RegisterViewModsel]
public partial class LoginPageViewModel : BaseViewModel
{
    private IEnumerable<Language> Languages = Language.List();
    private IPreferenceService preference;
    public static bool isSettingTenantOpened = false;
    [ObservableProperty]
    public string? baseUrl;
    [ObservableProperty]
    private string languageName;
    private Language? CurrentLanguage;
    private readonly ISQLiteManager managerSQLite;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Tenant> tenantRepository;
    private readonly IRepository<BaseUrl> baseUrlRepository;
    private readonly IITHNavigationService iTHNavigation;
    private readonly ILoginService loginService;

    public LoginPageViewModel(ISQLiteManager managerSQLite,
                              IRepository<User> userRepository,
                              IRepository<Tenant> tenantRepository,
                              IRepository<BaseUrl> baseUrlRepository,
                              IPreferenceService preference,
                              IITHNavigationService iTHNavigation,
                              ILoginService loginService)
    {
        this.managerSQLite = managerSQLite;
        this.userRepository = userRepository;
        this.tenantRepository = tenantRepository;
        this.baseUrlRepository = baseUrlRepository;
        this.iTHNavigation = iTHNavigation;
        this.loginService = loginService;
        this.preference = ITHPreference.Instance;
        LanguageName = preference.Get(nameof(Language), Language.Spanish.Code);
    }


    [RelayCommand]
    public async Task Login(UserDto userDto)
    {
        try
        {
            if (userDto is null || userIsNotValid2(userDto)) return;
            UserDTO = new ()
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
            };
            if (IsBusy) return;

            string jwt = string.Empty;
            var currentUser = (await userRepository.GetAllAsync()).FirstOrDefault();
            var currentTenant = (await tenantRepository.GetAllAsync()).FirstOrDefault();

            if (isSettingTenantOpened)
            {
                await SaveTenantConfiguration();
            }
            else
            {
                IBS.Authentication.BaseUrl = (await baseUrlRepository.GetAllAsync()).FirstOrDefault()?.Url ?? string.Empty;
            }
            IsBusy = true;
            if (string.IsNullOrEmpty(IBS.Authentication.BaseUrl))
            {
                IsBusy = false;
                await iTHNavigation.ErrorAlert(IBSResources.Error, "No se ha configurado la URL del tenant. Por favor, configúrala antes de iniciar sesión.");
                return;
            }

            if ((currentUser is null || currentTenant is null))
            {
                if (await NoInternetConnection())
                {
                    IsBusy = false;
                    await iTHNavigation.ErrorAlert(IBSResources.Error, "Necesitas conexion a internet al menos una vez, para autenticarte correctamente.");
                    return;
                }

                var user = await loginService.Login(UserDTO!) ?? new();
                await iTHNavigation.PushRelativePageAsync<HomePage>();
                return;
            }

            var currentUserDto = currentUser.Map<UserDto>();
            var currentTenantDto = currentTenant.Map<TenantDto>();

            if (UserDTO.UserName != currentUserDto.UserName ||
                  UserDTO.Password != currentUserDto.Password)
            {
                IsBusy = false;
                await iTHNavigation.ErrorAlert(IBSResources.Error, "Credenciales incorrectas.");
                return;
            }



            if (currentUserDto.TokenExpired && !await NoInternetConnection())
            {
                var user = await loginService.Login(UserDTO) ?? new();
                await iTHNavigation.PushRelativePageAsync<HomePage>();
                return;
            }

            IBS.Authentication.CurrentLogin = new()
            {
                User = currentUserDto,
                Tenant = currentTenantDto!
            };
            IBS.Authentication.CurrentLogin.Tenant.Id = currentTenant.TenantId;
          
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

    [RelayCommand]
    public async Task SaveTenantConfiguration()
    {
        if(IsBusy) return;
        if (string.IsNullOrEmpty(BaseUrl))
        {
            await ErrorAlert(IBSResources.Error, "el Campo de la URL no puede estar vacío");
            return;
        }
        IBS.Authentication.BaseUrl = BaseUrl;
        await baseUrlRepository.DeleteAllAsync();
        await baseUrlRepository.InsertAsync(new BaseUrl { Url = BaseUrl });
        await GoBack();
    }
    
    private bool CredentialsAreValid(UserDto userDto, UserDto currentUserDto)
    {
        if (userDto is null) return false;
        if (string.IsNullOrEmpty(userDto.UserName)) return false;
        if (string.IsNullOrEmpty(userDto.Password)) return false;
        if (string.IsNullOrEmpty(userDto.Pin)) return false;
        return true;
    }

    private bool userIsNotValid(UserDto userDto)
    {
        if (userDto is null) return true;
        if (string.IsNullOrEmpty(userDto.UserName)) return true;
        if (string.IsNullOrEmpty(userDto.Password)) return true;
        if (string.IsNullOrEmpty(userDto.Pin)) return true;
        return false;

    }
    private bool userIsNotValid2(UserDto userDto)
    {
        if (string.IsNullOrEmpty(userDto.UserName)) return true;
        if (string.IsNullOrEmpty(userDto.Password)) return true;
        //if (string.IsNullOrEmpty(userDto.Pin)) return true;
        return false;

    }

    [RelayCommand]
    public async Task RegisterUser(UserDto userDto)
    {
        try
        {
            if (userDto == null) return;
            if (userIsNotValid(userDto)) return;
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

    [RelayCommand]
    public async Task GoToSetting()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            var baseUrl = await baseUrlRepository.GetAllAsync() ?? new List<BaseUrl>();
            BaseUrl = baseUrl.FirstOrDefault()?.Url ?? string.Empty;
            await PushRelativePageAsync<SettingOfTenantPage>(true);
        }
        catch (Exception e)
        {
            Debug.Write(e);
            await ErrorAlert("iThot System", "Error de navegación.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    public async Task SetUserTenantInfo()
    {
        var currentUser = (await userRepository.GetAllAsync()).FirstOrDefault();
        if (currentUser is null) return;
        UserDTO = new()
        {
            UserName = currentUser?.UserName ?? string.Empty,
            Password = currentUser?.Password ?? string.Empty,
        };
 
        if (!isSettingTenantOpened) return;
        var baseUrl = await baseUrlRepository.GetAllAsync() ?? new List<BaseUrl>();
        BaseUrl = baseUrl.FirstOrDefault()?.Url ?? string.Empty;
    }

}