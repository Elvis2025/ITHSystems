using ITHSystems.DTOs;

namespace ITHSystems.Services.Login;

public interface ILoginService
{
    Task<bool> GetMessengers(UserDto userDto);
    Task<ResponseDto<OrdersDto>> GetOrder(UserDto userDto);
    Task<string> Login(UserDto userDto);
}
