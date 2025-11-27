using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Services.ApiManager;
using ITHSystems.Services.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;
using System.Text.Json;
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

        var request = IBS.Post(IBS.Authentication.CreateOAuthToken, content);

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

        var request = IBS.Post(IBS.Authentication.Messengers, userDto.JWT);

        var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);

        var returnedJsonStr = await response.Content.ReadAsStringAsync();

        
        return response.IsSuccessStatusCode;
    }
    public async Task<ResponseDto<OrdersDto>> GetOrder(UserDto userDto)
    {

        var content = new FormUrlEncodedContent(new[]
       {
            new KeyValuePair<string, string>("filter", ""),
        });

        var request = IBS.Post(IBS.Authentication.GetOrders, content, userDto.JWT);

        var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);

      

        // Aquí deserializamos: wrapper ABP -> result -> paged result
        var dto = await IBS.DeserealizeDto<OrdersDto>(response);
        return dto;
    }

    public async Task<ResponseDto<OrdersDto>> DeliverOrder(UserDto userDto)
    {

        var content = new FormUrlEncodedContent(new[]
       {
            new KeyValuePair<string, string>("WorkingForOfficeId", ""),
            new KeyValuePair<string, string>("ProductBatchAssignmentId", ""),
            new KeyValuePair<string, string>("Comment", ""),
            new KeyValuePair<string, string>("IsSecondPerson", ""),
            new KeyValuePair<string, string>("SecondPersonRelationshipId", ""),
            new KeyValuePair<string, string>("IdentificationDocumentPhoto", ""),
            new KeyValuePair<string, string>("SignatureImage", ""),
            new KeyValuePair<string, string>("IdentificationDocumentTypeId", ""),
            new KeyValuePair<string, string>("IdentificationValue", ""),
            new KeyValuePair<string, string>("Latitude", ""),
            new KeyValuePair<string, string>("Longitude", ""),
            new KeyValuePair<string, string>("EventOcurredOn", ""),
            new KeyValuePair<string, string>("IsSelected", ""),
            new KeyValuePair<string, string>("CauseSelected", ""),
        });

        var request = IBS.Post(IBS.Delivery.SendOrder, content, userDto.JWT);

        var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);

      

        // Aquí deserializamos: wrapper ABP -> result -> paged result
        var dto = await IBS.DeserealizeDto<OrdersDto>(response);
        return dto;
    }



}
