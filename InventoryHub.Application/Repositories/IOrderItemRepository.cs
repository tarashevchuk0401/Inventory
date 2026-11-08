using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(Guid id);

    // Tracks the entity only — caller must call IUnitOfWork.SaveChangesAsync() to persist.
    Task AddAsync(OrderItem orderItem);
}
