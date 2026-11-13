using InventoryHub.Application.Dto.Order;
using InventoryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet("getAll")]
    public Task<PaginationResponseDto<OrderDto>> GetAll([FromQuery] PaginationRequestDto<OrderSortBy> request)
    {
        return orderService.GetAllAsync(request);
    }

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

    [HttpGet("getSummary/{id}")]
    public Task<OrderSummaryDto> GetOrderSummary(Guid id)
    {
        return orderService.GetOrderSummary(id);
    }
}
