using InventoryHub.Application.Dto;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet(Name = "getAll")]
    public Task<List<UserDto>> Get()
    {
        return userService.GetAllAsync();
    }
}