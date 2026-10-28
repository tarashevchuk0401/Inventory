using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repositories.InMemory;

public class OrderInMemoryRepository : IOrderRepository
{
    private readonly List<Order> _orders = new()
    {
        Order.Create(Guid.NewGuid()),
        Order.Create(Guid.NewGuid())
    };

    public Task<List<Order>> GetAllAsync()
    {
        return Task.FromResult(_orders);
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
    }

    public Task AddAsync(Order order)
    {
        _orders.Add(order);
        return Task.CompletedTask;
    }
}
