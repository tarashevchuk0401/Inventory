using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(Guid id);
    Task AddAsync(OrderItem orderItem);
}
