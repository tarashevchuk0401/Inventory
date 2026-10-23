using InventoryHub.Application.Dto;
using InventoryHub.Application.Services;
using InventoryHub.Infrastructure.Repository;

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
}