using InventoryHub.Application.Dto;

namespace InventoryHub.Application.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetUser(Guid id);
}
