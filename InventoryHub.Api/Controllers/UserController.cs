using InventoryHub.Application.Dto;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [Authorize]
    [HttpGet("getAll")]
    public Task<List<UserDto>> GetAll()
    {
        return userService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public Task<UserDto> GetUser(Guid id)
    {
        return userService.GetUser(id);
    }

    [HttpPost("create")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userDto)
    {
        var created = await userService.CreateUser(userDto);
        return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
    }
}
