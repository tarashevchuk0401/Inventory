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
        return await context.Orders.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
    }
}