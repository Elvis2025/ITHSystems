using ITHSystems.DTOs;

namespace ITHSystems.Services.Login;

public interface ILoginService
{
    Task EnsureValidTokenAsync();
    Task<UserDto> Login(UserDto userDto);
}
