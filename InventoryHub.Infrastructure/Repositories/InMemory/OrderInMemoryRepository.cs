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

    public Task<PaginationResponseDto<Order>> GetAllAsync(PaginationRequestDto<OrderSortBy> request)
    {
        throw new NotImplementedException();
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

    public Task<List<Order>> GetOrderItemsByCustomer(Guid id)
    {
        throw new NotImplementedException();
    }
}
