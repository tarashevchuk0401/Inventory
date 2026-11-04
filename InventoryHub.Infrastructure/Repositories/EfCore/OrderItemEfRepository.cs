using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


class OrderItemEfRepository(ApplicationDbContext context) : IOrderItemRepository
{
    public async Task<List<OrderItem>> GetAllAsync()
    {
        return await context.OrderItems.ToListAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(Guid id)
    {
        return await context.OrderItems.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(OrderItem orderItem)
    {
        context.OrderItems.Add(orderItem);
        await context.SaveChangesAsync();
    }
}