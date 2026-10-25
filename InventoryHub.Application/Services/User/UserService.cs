using InventoryHub.Application.Dto;
using InventoryHub.Application.Repositories;

namespace InventoryHub.Application.Services;

public class UserService(IUserRepository usersRepository) : IUserService
{
    public async Task<List<UserDto>> GetAllAsync()
    {
        var all = await usersRepository.GetAllAsync();

        return all
            .Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role.ToString()
            })
            .ToList();
    }

    public async Task<UserDto> GetUser(Guid id)
    {
        var user = await usersRepository.GetUser(id) ?? throw new KeyNotFoundException("No User found");

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };
    }
}
