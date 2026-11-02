using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repositories.EfCore;

public class UserEFRepository: IUserRepository
{
    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException("Not impl");
    }

    Task<User?> GetUser(Guid id);
    Task<User?> GetUserByEmail(string id);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(User user);
}
