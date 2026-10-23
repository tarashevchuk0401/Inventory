using InventoryHub.Application.Dto;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MainUserController(IUserService userService) : ControllerBase
{
    [HttpGet(Name = "getAll")]
    public Task<List<UserDto>> GetAll()
    {
        return userService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public Task<UserDto> GetUser(Guid id)
    {
        return userService.GetUser(id);
    }
}
