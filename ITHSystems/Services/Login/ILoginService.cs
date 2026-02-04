using ITHSystems.DTOs;

namespace ITHSystems.Services.Login;

public interface ILoginService
{
    Task EnsureValidTokenAsync();
    Task GetMessengers(UserDto userDTO);
    Task<UserDto> Login(UserDto userDto);
}
