using InventoryHub.Application.Dto;
using InventoryHub.Application.Dto.Auth;

namespace InventoryHub.Application.Services.Auth;

public interface IAuthService
{
    Task<UserDto> SignUp(CreateUserDto dto);
    Task<string> SignIn(SignInDto dto);
}
