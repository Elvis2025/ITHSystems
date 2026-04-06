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
    private readonly IRepository<User> userRepository;

    public LoginService(IApiManagerService apiManagerService,
                        IPreferenceService preferenceService,
                        IRepository<User> userRepository)
    {
        this.apiManagerService = apiManagerService;
        this.preferenceService = preferenceService;
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

        var content = new 
        {
            tenancyName = "ibs",
            usernameOrEmailAddress = userDto.UserName,
            password = userDto.Password,
            pinSequenceRequest = IBS.Authentication.PinRequest,
            isMobile = IBS.Authentication.IsMobile,
            accessToken = mobileId,
            accessPin = userDto.Pin,
        };
        var json = JsonConvert.SerializeObject(content);
        var request = IBS.HttpMethod.PostJson(IBS.Authentication.Endpoint.CreateOAuthToken, json);

        var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);
        var userAuthentication = await IBS.HttpResponse.DeserealizeObject<UserAuthenticationDto>(response);
        if (userAuthentication is null)
        {
            return userDto;
        }
        userDto.JWT = userAuthentication.Result.Token;
        userDto.Issued = userAuthentication.Result.Issued;
        userDto.Expires = userAuthentication.Result.Expires;
        var user = userDto.Map<User>();
        await userRepository.InsertOrReplaceAsync(user);
      
        IBS.Authentication.CurrentUser = userDto;
        await SetCurrentLogin();
        return userDto;
    }


    public async Task SetCurrentLogin()
    {
        try
        {

            if (IBS.Authentication.CurrentUser is null || IBS.Authentication.CurrentUser.JWT == string.Empty)
                return;
            var content = new Dictionary<string, string>() 
            { 
                { "jwt", $"{IBS.Authentication.CurrentUser.JWT}" }       
        
            };

            var request = IBS.HttpMethod.Get(IBS.Authentication.Endpoint.GetCurrentLogin, content);

            var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return;

            var userAuthentication = await IBS.HttpResponse.DeserealizeObject<CurrentLoginDto>(response);

            IBS.Authentication.CurrentLogin = userAuthentication.Result;
        }
        catch (Exception e)
        {
            Debug.Write(e);
        }
    }

    public async Task EnsureValidTokenAsync()
    {
        var user = IBS.Authentication.CurrentUser;

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
