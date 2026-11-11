using InventoryHub.Application.Dto.Product;

namespace InventoryHub.Application.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();

    Task<ProductDto> GetById(Guid id);

    Task<ProductDto> AddAsync(CreateProductDto dto);

    Task<List<ProductDto>> GetLowStocks();
    // Task<List<ProductDto>> GetNeverOrdered();
    Task<List<ProductDto>>  GetNeverOrdered();
    Task<List<TopSellingDto>>  GetTopSelling();
}
