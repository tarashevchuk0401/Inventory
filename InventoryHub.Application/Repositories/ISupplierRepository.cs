using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync();
    Task<Supplier?> GetByIdAsync(Guid id);
    Task AddAsync(Supplier supplier);
}
