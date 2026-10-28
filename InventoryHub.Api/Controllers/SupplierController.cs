using InventoryHub.Application.Dto.Supplier;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController(ISupplierService supplierService) : ControllerBase
{
    [HttpGet("getAll")]
    public Task<List<SupplierDto>> GetAll() => supplierService.GetAllAsync();

    [HttpGet("{id}")]
    public Task<SupplierDto> GetUser(Guid id)
    {
        return supplierService.GetById(id);
    }

    [HttpPost("create")]
    public Task<SupplierDto> AddSupplier(CreateSupplierDto dto)
    {
        return supplierService.AddAsync(dto);
    }
}
