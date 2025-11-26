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
        public const string Login = BaseUrl + "/Home/Pin_Login";
        public const string Messengers = BaseUrl + "/api/services/itcCoreSystem/messengers/MyInfo";
        public const string DeleteApplication = "/api/applications/{0}";
        public const string PinRequest = "10";
        public const string IsMobile = "false";
    }


}
