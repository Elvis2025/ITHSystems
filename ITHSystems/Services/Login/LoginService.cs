using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Services.ApiManager;
using ITHSystems.Services.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;
using ZXing.Aztec.Internal;

namespace ITHSystems.Services.Login;
[RegisterService]
public class LoginService : ILoginService
{
    private readonly IApiManagerService apiManagerService;
    private readonly IPreferenceService preferenceService;

    public LoginService(IApiManagerService apiManagerService,IPreferenceService preferenceService)
    {
        this.apiManagerService = apiManagerService;
        this.preferenceService = preferenceService;
    }

    public async Task<string> Login(UserDto userDto)
    {
        var mobileId = preferenceService.Get(IBS.Key, string.Empty);

        if (string.IsNullOrEmpty(mobileId)) 
        {
            mobileId = Guid.NewGuid().ToString();
            preferenceService.Set(IBS.Key, mobileId);
        }

        var request = new HttpRequestMessage(HttpMethod.Post, IBS.Authentication.CreateOAuthToken);
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("TenancyName", IBS.TenancyName ),
            new KeyValuePair<string, string>("UsernameOrEmailAddress", userDto.UserName ),
            new KeyValuePair<string, string>("Password", userDto.Password),
            new KeyValuePair<string, string>("PinSequenceRequest", IBS.Authentication.PinRequest),
            new KeyValuePair<string, string>("IsMobile", IBS.Authentication.IsMobile ),
            new KeyValuePair<string, string>("AccessToken", mobileId),
            new KeyValuePair<string, string>("AccessPin", userDto.Pin),
        });
        request.Content = content;

        var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);

        var returnedJsonStr = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var jObject = JObject.Parse(returnedJsonStr);
            string result = (string)jObject["result"];
            return result;
        }
        return string.Empty;
    }
    public async Task<bool> GetMessengers(UserDto userDto)
    {
       

        var request = new HttpRequestMessage(HttpMethod.Post, IBS.Authentication.Messengers);

        request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userDto.JWT);
        request.Content = new StringContent("");

        var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);

        var returnedJsonStr = await response.Content.ReadAsStringAsync();

        
        return response.IsSuccessStatusCode;
    }


}
