using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repositories.InMemory;

public class ProductInMemoryRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        Product.Create("Widget", 9.99m, 100, Guid.NewGuid(), Guid.NewGuid()),
        Product.Create("Gadget", 19.99m, 50, Guid.NewGuid(), Guid.NewGuid())
    };

    public Task<List<Product>> GetAllAsync()
    {
        return Task.FromResult(_products);
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
    }

    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }
}
