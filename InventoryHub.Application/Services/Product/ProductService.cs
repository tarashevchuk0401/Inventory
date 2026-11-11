using InventoryHub.Application.Dto.Product;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class ProductService(
    IProductRepository productRepository,
    IOrderItemRepository orderItemRepository
) : IProductService
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
        var newProduct = Product.Create(dto.Name, dto.Price, dto.StockQuantity, dto.SupplierId!.Value,
            dto.Category!.Value);
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

    public async Task<List<ProductDto>> GetNeverOrdered()
    {
        var products = await productRepository.GetNeverOrdered();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Category = p.Category,
            Name = p.Name,
            StockQuantity = p.StockQuantity,
            SupplierId = p.SupplierId
        }).ToList();
    }

    public async Task<List<TopSellingDto>> GetTopSelling()
    {
        var orderItems = await orderItemRepository.GetAllWithProductsAsync();
        var groupedOrderItems = orderItems.ToList().GroupBy(i => i.Product);

        return groupedOrderItems.Select(groupItem => new TopSellingDto
        {
            QuantityOfSoldItems = groupItem.Sum(orderItem => orderItem.Quantity),
            Product = new ProductDto
            {
                Id = groupItem.Key.Id,
                StockQuantity = groupItem.Key.StockQuantity,
                Price = groupItem.Key.Price,
                Name = groupItem.Key.Name,
                SupplierId = groupItem.Key.SupplierId,
                Category = groupItem.Key.Category
            }
        }).ToList();
    }
}
