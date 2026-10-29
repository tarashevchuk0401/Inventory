using InventoryHub.Application.Dto.Customer;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpGet("getAll")]
    public Task<List<CustomerDto>> GetAll() => customerService.GetAllAsync();

    [HttpGet("{id}")]
    public Task<CustomerDto> GetCustomer(Guid id)
    {
        return customerService.GetById(id);
    }

    [HttpPost("create")]
    public Task<CustomerDto> AddCustomer(CreateCustomerDto dto)
    {
        return customerService.AddAsync(dto);
    }
}
