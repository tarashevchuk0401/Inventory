using InventoryHub.Application.Dto;
using InventoryHub.Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

public class AuthController(IAuthService authService) :ControllerBase
{
    [HttpPost]
    public Task<UserDto> SignUp(CreateUserDto dto)
    {
        return authService.SignUp(dto);
    }
}
