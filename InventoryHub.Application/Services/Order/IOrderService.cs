using InventoryHub.Application.Dto.Order;

namespace InventoryHub.Application.Services;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllAsync();
    Task<OrderDto> GetById(Guid id);

    Task<OrderDto> AddAsync(CreateOrderDto dto);
}
