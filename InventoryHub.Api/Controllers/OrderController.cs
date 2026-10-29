using InventoryHub.Application.Dto.Order;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet("getAll")]
    public Task<List<OrderDto>> GetAll() => orderService.GetAllAsync();

    [HttpGet("{id}")]
    public Task<OrderDto> GetOrder(Guid id)
    {
        return orderService.GetById(id);
    }

    [HttpPost("create")]
    public Task<OrderDto> AddOrder(CreateOrderDto dto)
    {
        return orderService.AddAsync(dto);
    }
}
