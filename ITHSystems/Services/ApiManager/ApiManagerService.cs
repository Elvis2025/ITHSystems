using System.Net.Http;

namespace ITHSystems.Services.ApiManager;

public class ApiManagerService : IApiManagerService
{
    private readonly IHttpClientFactory httpClientFactory;
    public HttpClient ApiManagerHttpClient { get; private set; }

    public ApiManagerService(IHttpClientFactory httpClientFactory)
    {
        ApiManagerHttpClient = httpClientFactory.CreateClient();
        this.httpClientFactory = httpClientFactory;
    }


    public void CreateClient()
    {
        ApiManagerHttpClient = httpClientFactory.CreateClient();
    }
}
