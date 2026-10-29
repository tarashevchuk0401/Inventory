using InventoryHub.Application.Dto.OrderItem;

namespace InventoryHub.Application.Services;

public interface IOrderItemService
{
    Task<List<OrderItemDto>> GetAllAsync();
    Task<OrderItemDto> GetById(Guid id);

    Task<OrderItemDto> AddAsync(CreateOrderItemDto dto);
}
