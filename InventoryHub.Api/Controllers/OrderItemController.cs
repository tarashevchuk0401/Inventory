using InventoryHub.Application.Dto.OrderItem;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController(IOrderItemService orderItemService) : ControllerBase
{
    [HttpGet("getAll")]
    public Task<List<OrderItemDto>> GetAll() => orderItemService.GetAllAsync();

    [HttpGet("{id}")]
    public Task<OrderItemDto> GetOrderItem(Guid id)
    {
        return orderItemService.GetById(id);
    }

    [HttpPost("create")]
    public Task<OrderItemDto> AddOrderItem(CreateOrderItemDto dto)
    {
        return orderItemService.AddAsync(dto);
    }
}
