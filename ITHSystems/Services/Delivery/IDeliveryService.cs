using ITHSystems.DTOs;

namespace ITHSystems.Services.Delivery;

public interface IDeliveryService
{
    Task<ResponseListDto<OrdersDto>> DeliverOrder(OrdersDto orderDto);
    Task<ResponseListDto<OrdersDto>> GetOrder();
}
