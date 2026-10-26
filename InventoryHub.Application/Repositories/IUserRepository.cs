using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetUser(Guid id);
    Task<User?> GetUserByEmail(string id);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(User user);
}
