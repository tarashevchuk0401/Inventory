using InventoryHub.Application.Dto;

namespace InventoryHub.Application.Services.Auth;

public class AuthService : IAuthService
{
    public Task<UserDto> SignUp(CreateUserDto dto)
    {
        throw new NotImplementedException();
    }
}
