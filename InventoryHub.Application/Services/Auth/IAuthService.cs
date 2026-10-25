using InventoryHub.Application.Dto;

namespace InventoryHub.Application.Services.Auth;

public interface IAuthService
{
    Task<UserDto> SignUp(CreateUserDto dto);
}
