using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repository.InMemory;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        User.Create("Ann", "Lee", "ann@example.com", "hashed-password", "Admin"),
        User.Create("Bob", "Smith", "bob@example.com", "hashed-password", "Admin")
    };


    public Task<List<User>> GetAllAsync()
    {
        return Task.FromResult(_users);
    }
}