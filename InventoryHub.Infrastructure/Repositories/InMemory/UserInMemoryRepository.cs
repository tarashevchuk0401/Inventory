using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repositories.InMemory;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        User.Create("11111111-1111-1111-1111-111111111111", "Ann", "Lee", "ann@example.com", "hashed-password",
            "Admin"),
        User.Create("22222222-2222-2222-2222-222222222222", "Bob", "Smith", "bob@example.com", "hashed-password",
            "Admin")
    };


    public Task<List<User>> GetAllAsync()
    {
        return Task.FromResult(_users);
    }

    public Task<User?> GetUser(Guid id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }
}
