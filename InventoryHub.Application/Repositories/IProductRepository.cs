using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);

    Task<Product?> DecrementStocks(Guid id, int quantity = 1);

    Task AddAsync(Product product);
    Task<List<Product>> GetLowStockAsync();

    Task<bool> IsExistAsync(Guid id);
}
