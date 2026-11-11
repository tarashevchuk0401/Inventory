using InventoryHub.Application.Dto.Product;
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

    // Tracks the order item only — caller must call IUnitOfWork.SaveChangesAsync() to persist.
    public Task AddAsync(OrderItem orderItem)
    {
        context.OrderItems.Add(orderItem);
        return Task.CompletedTask;
    }
}
