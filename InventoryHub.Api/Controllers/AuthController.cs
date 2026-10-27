using InventoryHub.Application.Dto;
using InventoryHub.Application.Dto.Auth;
using InventoryHub.Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) :ControllerBase
{
    [AllowAnonymous]
    [HttpPost("signUp")]
    public async Task<UserDto> SignUp(CreateUserDto dto)
    {
        return await authService.SignUp(dto);
    }

    [AllowAnonymous]
    [HttpPost("signIn")]
    public async Task<string> SignIn(SignInDto dto)
    {
        return await authService.SignIn(dto);
    }
}
