using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using ITHSystems.Model;
using ITHSystems.Repositories.SQLite;
using ITHSystems.Services.ApiManager;
using ITHSystems.Services.General;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ITHSystems.Services.Login;
[RegisterService]
public class LoginService : ILoginService
{
    private readonly IApiManagerService apiManagerService;
    private readonly IPreferenceService preferenceService;
    private readonly IRepository<Tenant> tenantRepository;
    private readonly IRepository<User> userRepository;

    public LoginService(IApiManagerService apiManagerService,
                        IPreferenceService preferenceService,
                        IRepository<Tenant> tenantRepository,
                        IRepository<User> userRepository)
    {
        this.apiManagerService = apiManagerService;
        this.preferenceService = preferenceService;
        this.tenantRepository = tenantRepository;
        this.userRepository = userRepository;
    }

    public async Task<UserDto> Login(UserDto userDto)
    {
        var mobileId = preferenceService.Get(IBS.Global.Key, string.Empty);
        if (string.IsNullOrEmpty(mobileId))
        {
            mobileId = Guid.NewGuid().ToString();
            preferenceService.Set(IBS.Global.Key, mobileId);
        }

        var content = new FormUrlEncodedContent(new[]
       {
            new KeyValuePair<string, string>("TenancyName", "ibs" ),
            new KeyValuePair<string, string>("UsernameOrEmailAddress", userDto.UserName ),
            new KeyValuePair<string, string>("Password", userDto.Password),
            new KeyValuePair<string, string>("PinSequenceRequest", IBS.Authentication.PinRequest),
            new KeyValuePair<string, string>("IsMobile", IBS.Authentication.IsMobile ),
            new KeyValuePair<string, string>("AccessToken", mobileId),
            new KeyValuePair<string, string>("AccessPin", userDto.Pin),
        });

        var request = IBS.HttpMethod.Post(IBS.Authentication.Endpoint.CreateOrAuthToken, content);

        var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);
        var userAuthentication = await IBS.HttpResponse.DeserealizeObject<UserAuthenticationDto>(response);
        if (userAuthentication is null)
        {
            return userDto;
        }
        userDto.JWT = userAuthentication.Result.Token;
        userDto.Issued = userAuthentication.Result.Issued;
        userDto.Expires = userAuthentication.Result.Expires;
   
      
        IBS.Authentication.CurrentLogin.User = userDto;
        await SetCurrentLogin();
        return userDto;
    }


    public async Task SetCurrentLogin()
    {
        try
        {

            if (IBS.Authentication.CurrentLogin.User is null || IBS.Authentication.CurrentLogin.User.JWT == string.Empty)
                return;
   
            var userDto = IBS.Authentication.CurrentLogin.User;
            var request = IBS.HttpMethod.Post(IBS.Authentication.Endpoint.GetCurrentLogin, IBS.Authentication.CurrentLogin.User.JWT);

            var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return;

            var userAuthentication = await IBS.HttpResponse.DeserealizeObject<CurrentLoginDto>(response);

            var user = userDto.Map<User>();
            var tenant = userAuthentication.Result.Tenant.Map<Tenant>();
            tenant.TenantId = userAuthentication.Result.Tenant.Id;
            tenant.Id = 0;
            await tenantRepository.DeleteAllAsync();
            await userRepository.DeleteAllAsync();

            await userRepository.InsertAsync(user);
            await tenantRepository.InsertAsync(tenant);
            IBS.Authentication.CurrentLogin.Tenant = tenant.Map<TenantDto>();
            IBS.Authentication.CurrentLogin.Tenant.Id = tenant.TenantId;
        }
        catch (Exception e)
        {
            Debug.Write(e);
        }
    }

    public async Task EnsureValidTokenAsync()
    {
        var user = IBS.Authentication.CurrentLogin.User;

        if (user.TokenExpired)
            await Login(user);
        await SetCurrentLogin();
    }

    public Task GetMessengers(UserDto userDTO)
    {
        try
        {
            return Task.CompletedTask;
        }
        catch (Exception e)
        {

            throw;
        }
    }
}
