using ITHSystems.DTOs;
using Newtonsoft.Json;

namespace ITHSystems.Constants;

public static class IBS
{
    public const string BaseUrl = "https://demo.ibsystems.com.do";
    public const string TenancyName = "ats";

    public const string Key = "iThotSystemMobileId";
    public const string JWT = "UserJWT";

    public static readonly Lazy<Task<string>> MobileId
        = new(async () => await GetOrCreateAsync());

    private static async Task<string> GetOrCreateAsync()
    {
        var id = await SecureStorage.GetAsync(Key);
        if (!string.IsNullOrWhiteSpace(id))
            return id;

        id = Guid.NewGuid().ToString();
        await SecureStorage.SetAsync(Key, id);

        return id;

    }
    public static class Authentication
    {
        public const string CreateToken = BaseUrl + "/Home/CreateToken";
        public const string CreateOAuthToken = BaseUrl + "/api/Account/Authenticate";
        public const string CreateOAuthTokenExt = BaseUrl + "/api/Account/AuthenticateExt";
        public const string Login = BaseUrl + "/Home/Pin_Login";
        public const string Messengers = BaseUrl + "/api/services/itcCoreSystem/messengers/MyInfo";
        public const string DeleteApplication = "/api/applications/{0}";
        public const string GetOrders = BaseUrl + "/api/services/itcEntregas/orders/GetOrders";
        public const string PinRequest = "10";
        public const string IsMobile = "false";
    }
    public static class Delivery
    {
        public const string SendOrder = BaseUrl + "/api/services/itcEntregas/orders/DeliverOrder";

    }

    public static HttpRequestMessage Post(string url, string jwt = "")
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        if (!string.IsNullOrEmpty(jwt))
        {
            request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
        }
        request.Content = new StringContent("");

        return request;
    }
    public static HttpRequestMessage Post(string url, FormUrlEncodedContent httpContent, string jwt = "")
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        if (!string.IsNullOrEmpty(jwt))
        {
            request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
        }
        request.Content = httpContent;

        return request;
    }

    public static async Task<ResponseDto<T>> DeserealizeDto<T>(HttpResponseMessage httpResponse)
    {
        var json = await httpResponse.Content.ReadAsStringAsync();
       
        var settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        // Aquí deserializamos: wrapper ABP -> result -> paged result
        var dto = JsonConvert.DeserializeObject<ResponseDto<T>>(json, settings);
        return dto;
    }
}
