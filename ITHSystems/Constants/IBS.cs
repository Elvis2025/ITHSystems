using ITHSystems.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace ITHSystems.Constants;

public static class IBS
{
    #region Authentication Endpoints
    public static class Authentication
    {
        public static UserDto CurrentUser { get; set; } = new();
        public static CurrentLoginDto CurrentLogin { get; set; } = new();

        public const string Messengers = Global.BaseUrl + "/api/services/itcCoreSystem/messengers/MyInfo";
        public const string GetOrders = Global.BaseUrl + "/api/services/itcEntregas/orders/GetOrders";
        public const string PinRequest = "10";
        public const string IsMobile = "false";
        public static class Endpoint
        {
            public const string CreateOAuthToken = "https://xvkhk6p9-7055.use.devtunnels.ms" + "/api/services/app/ItcAuthentication/Authenticate";
            public const string GetCurrentLogin = "https://xvkhk6p9-7055.use.devtunnels.ms" + "/api/services/app/ItcAuthentication/GetCurrentLoginInformations";
        }
    }
    #endregion

    public static class RawQuery
    {
        public static class Endpoint
        {
            public const string Execute = "https://xvkhk6p9-7055.use.devtunnels.ms" + "/api/services/app/ItcRawQuery/Execute";
        }
    }
    #region Delivery Endpoints
    public static class Delivery
    {
        public const string SendOrder = Global.BaseUrl + "/api/services/itcEntregas/orders/DeliverOrder";

    }
    #endregion

    #region Global Constants
    public static class Global
    {
        public const int WorkingForOfficeId = 1;
        public const string BaseUrl = "https://demo.ibsystems.com.do";
        public const string TenancyName = "ats";
        public static readonly Lazy<Task<string>> MobileId
        = new(async () => await GetOrCreateAsync());
        public const string Key = "iThotSystemMobileId";
        public const string KeyJWT = "UserJWT";

        private static async Task<string> GetOrCreateAsync()
        {
            var id = await SecureStorage.GetAsync(Key);
            if (!string.IsNullOrWhiteSpace(id))
                return id;

            id = Guid.NewGuid().ToString();
            await SecureStorage.SetAsync(Key, id);

            return id;

        }

    }
    #endregion

    #region Methods Http
    public static class HttpMethod
    {
        public static HttpRequestMessage Post(string url, string jwt = "")
        {
            var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);
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
            var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);

            if (!string.IsNullOrEmpty(jwt))
            {
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
            }
            request.Content = httpContent;

            return request;
        }
        public static HttpRequestMessage PostJson(string url, string json, string jwt = "")
        {
            var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);

            if (!string.IsNullOrWhiteSpace(jwt))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
            }

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return request;
        }
        public static HttpRequestMessage Get(string url, Dictionary<string, string>? parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                var query = string.Join("&",
                    parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));

                url = $"{url}?{query}";
            }

            var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);

            //if (!string.IsNullOrEmpty(jwt))
            //{
            //    request.Headers.Authorization =
            //        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
            //}

            return request;
        }

    }
    public static class HttpResponse
    {
        public static async Task<ResponseListDto<T>> DeserealizeList<T>(HttpResponseMessage httpResponse)
        {
            var json = await httpResponse.Content.ReadAsStringAsync();

            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            // Aquí deserializamos: wrapper ABP -> result -> paged result
            var dto = JsonConvert.DeserializeObject<ResponseListDto<T>>(json, settings);
            return dto;
        }
        public static async Task<ResponseDto<T>> DeserealizeObject<T>(HttpResponseMessage httpResponse)
        {
            var json = await httpResponse.Content.ReadAsStringAsync();

            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            var dto = JsonConvert.DeserializeObject<ResponseDto<T>>(json, settings);
            return dto;
        }

    }

    #endregion

    #region Exeptions
    public static class Exceptions
    {
        public static ResponseListDto<T> ResponseListDto<T>(Exception e)
        {
            return new ResponseListDto<T>
            {
                Success = false,
                Error = new ErrorDto
                {
                    Message = e.Message,
                    Code = e.HResult,
                    Details = e.StackTrace ?? string.Empty
                }
            };
        }

        public static ResponseDto<T> ResponseDto<T>(Exception e)
        {
            return new ResponseDto<T>
            {
                Success = false,
                Error = new ErrorDto
                {
                    Message = e.Message,
                    Code = e.HResult,
                    Details = e.StackTrace ?? string.Empty
                }
            };
        }


    }
    #endregion
}
