using InventoryHub.Application.Dto.Product;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet("getAll")]
    public Task<List<ProductDto>> GetAll() => productService.GetAllAsync();

    [HttpGet("{id}")]
    public Task<ProductDto> GetProduct(Guid id)
    {
        return productService.GetById(id);
    }

    [HttpPost("create")]
    public Task<ProductDto> AddProduct(CreateProductDto dto)
    {
        return productService.AddAsync(dto);
    }
}
