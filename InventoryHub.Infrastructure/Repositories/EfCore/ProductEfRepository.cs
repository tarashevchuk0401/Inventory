using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace InventoryHub.Infrastructure.Repositories.EfCore;

public class ProductEfRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<List<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> DecrementStocks(Guid id, int quantity = 1)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id && p.StockQuantity >= quantity);
        if (product == null) return null;
        product.StockQuantity -= quantity;
        return product;
    }


    public async Task AddAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetLowStockAsync()
    {
        return await context.Products
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();
    }

    public async Task<bool> IsExistAsync(Guid id)
    {
        return await context.Customers.AnyAsync(c => c.Id == id);
    }
}
