using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace InventoryHub.Infrastructure.Repositories.EfCore;

public class UserEfRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<List<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUser(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}