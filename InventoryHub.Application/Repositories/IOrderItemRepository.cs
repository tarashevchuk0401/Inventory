using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetAllAsync();
    Task<List<OrderItem>> GetAllWithProductsAsync();
    Task<OrderItem?> GetByIdAsync(Guid id);
    Task AddAsync(OrderItem orderItem);
}
