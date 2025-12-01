using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.DTOs;
using ITHSystems.Services.ApiManager;
using ITHSystems.Services.General;
using ITHSystems.Services.Login;

namespace ITHSystems.Services.Delivery;
[RegisterService]
public class DeliveryService : IDeliveryService
{
    private readonly ILoginService loginService;
    private readonly IApiManagerService apiManagerService;

    public DeliveryService(ILoginService loginService,
                           IPreferenceService preferences,
                           IApiManagerService apiManagerService)
    {
        this.loginService = loginService;
        this.apiManagerService = apiManagerService;
    }

    public async Task<ResponseListDto<OrdersDto>> GetOrder()
    {

        try
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("filter", ""),
            });

            await loginService.EnsureValidTokenAsync();

            var request = IBS.HttpMethod.Post(IBS.Authentication.GetOrders, content, IBS.Authentication.CurrentUser.JWT);

            var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);

            var dto = await IBS.HttpResponse.DeserealizeList<OrdersDto>(response);
            return dto;
        }
        catch (Exception e)
        {
            return IBS.Exceptions.ResponseListDto<OrdersDto>(e);
        }
    }

    public async Task<ResponseDto<OrdersDto>> DeliverOrder(OrdersDto orderDto)
    {
        try
        {


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("WorkingForOfficeId", $"{orderDto.WorkingForOfficeId}"),
                new KeyValuePair<string, string>("ProductBatchAssignmentId", $"{orderDto.ProductBatchAssignmentId}"),
                new KeyValuePair<string, string>("GenderId", $"{orderDto.GenderId}"),
                new KeyValuePair<string, string>("Comment", $"{orderDto.Comment}"),
                new KeyValuePair<string, string>("IsSecondPerson", $"{orderDto.IsSecondPerson}"),
                new KeyValuePair<string, string>("SecondPersonRelationshipId", $"{orderDto.SecondPersonRelationshipId}"),
                new KeyValuePair<string, string>("IdentificationDocumentPhoto", $"{orderDto.IdentificationDocumentPhoto}"),
                new KeyValuePair<string, string>("SignatureImage", $"{orderDto.SignatureImage}"),
                new KeyValuePair<string, string>("IdentificationDocumentTypeId", $"{orderDto.IdentificationDocumentTypeId}"),
                new KeyValuePair<string, string>("IdentificationValue", $"{orderDto.IdentificationValue}"),
                new KeyValuePair<string, string>("Latitude", $"{orderDto.Latitude}"),
                new KeyValuePair<string, string>("Longitude", $"{orderDto.Longitude}"),
                new KeyValuePair<string, string>("EventOcurredOn", $"{orderDto.EventOcurredOn}"),
                new KeyValuePair<string, string>("IsSelected", $"{orderDto.IsSelected}"),
                new KeyValuePair<string, string>("CauseSelected", $"{orderDto.CauseSelected}"),
            });


            await loginService.EnsureValidTokenAsync();

            var request = IBS.HttpMethod.Post(IBS.Delivery.SendOrder, content, IBS.Authentication.CurrentUser.JWT);
            var response = await apiManagerService.ApiManagerHttpClient.SendAsync(request);
            var dto = await IBS.HttpResponse.DeserealizeObject<OrdersDto>(response);

            return dto;
        }
        catch (Exception e)
        {
            return IBS.Exceptions.ResponseDto<OrdersDto>(e);
        }
    }
}
