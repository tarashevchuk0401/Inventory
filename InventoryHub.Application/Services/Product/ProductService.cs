using InventoryHub.Application.Dto.Product;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ProductDto> GetById(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException("There no product with such id.");
        }

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            SupplierId = product.SupplierId,
            Category = product.Category
        };
    }

    public async Task<ProductDto> AddAsync(CreateProductDto dto)
    {
        var newProduct = Product.Create(dto.Name, dto.Price, dto.StockQuantity, dto.SupplierId, dto.Category);
        await productRepository.AddAsync(newProduct);

        return new ProductDto
        {
            Id = newProduct.Id,
            Name = newProduct.Name,
            Price = newProduct.Price,
            StockQuantity = newProduct.StockQuantity,
            SupplierId = newProduct.SupplierId,
            Category = newProduct.Category
        };
    }

    public async Task<List<ProductDto>> GetLowStocks()
    {
        var lowStock = await productRepository.GetLowStockAsync();

        if (lowStock.Count == 0) throw new NotFoundException("There are no products at all!");

        return lowStock
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                SupplierId = p.SupplierId,
                Category = p.Category
            })
            .ToList();
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var all = await productRepository.GetAllAsync();

        return all
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                SupplierId = p.SupplierId,
                Category = p.Category
            })
            .ToList();
    }
}
