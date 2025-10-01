
namespace ITHSystems.Services.ApiManager;

public interface IApiManagerService
{
    HttpClient ApiManagerHttpClient { get; }

    void CreateClient();
}
