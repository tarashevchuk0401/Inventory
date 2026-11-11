using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Domain.Enums;

namespace InventoryHub.Infrastructure.Repositories.InMemory;

public class ProductInMemoryRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        Product.Create("Widget", 9.99m, 100, Guid.NewGuid(), ProductCategories.Console),
        Product.Create("Gadget", 19.99m, 50, Guid.NewGuid(), ProductCategories.Other)
    };

    public Task<List<Product>> GetAllAsync()
    {
        return Task.FromResult(_products);
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
    }

    public async Task<Product?> DecrementStocks(Guid id, int quantity = 1)
    {
        var product = await Task.FromResult(_products.FirstOrDefault(p => p.Id == id && p.StockQuantity >= quantity));
        if (product == null) return null;
        product.StockQuantity -= quantity;
        return product;
    }

    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task<List<Product>> GetLowStockAsync()
    {
        return Task.FromResult(_products.OrderBy(p => p.StockQuantity).ToList());
    }

    public Task<bool> IsExistAsync(Guid id)
    {
        return Task.FromResult(_products.Any(p => p.Id == id));
    }

    public Task<List<Product>>  GetNeverOrdered()
    {
        throw new NotImplementedException();
    }
}
