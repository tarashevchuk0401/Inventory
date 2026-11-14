using InventoryHub.Application.Dto.Customer;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpGet("getAll")]
    public Task<PaginationResponseDto<CustomerDto>> GetAll([FromQuery] PaginationRequestDto request)
    {
        return customerService.GetAll(request);
    }

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

    [HttpGet("revenueByuCustomer/{id}")]
    public Task<decimal> GetRevenueByCustomer(Guid id)
    {
        return customerService.GetRevenueByCustomer(id);
    }
}
