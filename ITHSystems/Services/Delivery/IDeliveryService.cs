using ITHSystems.DTOs;

namespace ITHSystems.Services.Delivery;

public interface IDeliveryService
{
    Task<ResponseDto<OrdersDto>> DeliverOrder(OrdersDto orderDto);
    Task<ResponseListDto<OrdersDto>> GetOrder();
}
