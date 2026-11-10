using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


class OrderEfRepository(ApplicationDbContext context) : IOrderRepository
{
    public async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    // Tracks the order only — caller must call IUnitOfWork.SaveChangesAsync() to persist.
    public Task AddAsync(Order order)
    {
        context.Orders.Add(order);
        return Task.CompletedTask;
    }
}
