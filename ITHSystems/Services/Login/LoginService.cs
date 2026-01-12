using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Extensions;
using ITHSystems.Model;
using ITHSystems.Repositories.SQLite;
using ITHSystems.Services.ApiManager;
using ITHSystems.Services.General;

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

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("TenancyName", IBS.Global.TenancyName ),
            new KeyValuePair<string, string>("UsernameOrEmailAddress", userDto.UserName ),
            new KeyValuePair<string, string>("Password", userDto.Password),
            new KeyValuePair<string, string>("PinSequenceRequest", IBS.Authentication.PinRequest),
            new KeyValuePair<string, string>("IsMobile", IBS.Authentication.IsMobile ),
            new KeyValuePair<string, string>("AccessToken", mobileId),
            new KeyValuePair<string, string>("AccessPin", userDto.Pin),
        });

        var request = IBS.HttpMethod.Post(IBS.Authentication.CreateOAuthToken, content);

        var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);
        var userAuthentication = await IBS.HttpResponse.DeserealizeObject<UserAuthenticationDto>(response);
        if(userAuthentication is null)
        {
            return userDto;
        }
        userDto.JWT = userAuthentication.Result.Token;
        userDto.Issued = userAuthentication.Result.Issued;
        userDto.Expires = userAuthentication.Result.Expires;
        var user = userDto.Map<User>();
        await userRepository.InsertOrReplaceAsync(user);
        IBS.Authentication.CurrentUser = userDto;
        return userDto;
    }

    public async Task EnsureValidTokenAsync()
    {
        var user = IBS.Authentication.CurrentUser;

        if (user.TokenExpired)
            await Login(user);
    }


}
