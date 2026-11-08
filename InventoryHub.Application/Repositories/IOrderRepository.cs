using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Guid id);

    // Tracks the entity only — caller must call IUnitOfWork.SaveChangesAsync() to persist.
    Task AddAsync(Order order);
}
