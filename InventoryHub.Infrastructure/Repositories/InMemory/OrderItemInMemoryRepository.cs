using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repositories.InMemory;

public class OrderItemInMemoryRepository : IOrderItemRepository
{
    private readonly List<OrderItem> _orderItems = new()
    {
        OrderItem.Create(Guid.NewGuid(), Guid.NewGuid(), 2, 9.99m),
        OrderItem.Create(Guid.NewGuid(), Guid.NewGuid(), 1, 19.99m)
    };

    public Task<List<OrderItem>> GetAllAsync()
    {
        return Task.FromResult(_orderItems);
    }

    public Task<OrderItem?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_orderItems.FirstOrDefault(oi => oi.Id == id));
    }

    public Task AddAsync(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
        return Task.CompletedTask;
    }
}
