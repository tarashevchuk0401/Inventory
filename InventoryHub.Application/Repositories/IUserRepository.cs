using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetUser(Guid id);

    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ChackPassword(string email, string password);
    Task AddAsync(User user);
}
