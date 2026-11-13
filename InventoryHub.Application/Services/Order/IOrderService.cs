using InventoryHub.Application.Dto.Order;

namespace InventoryHub.Application.Services;

public interface IOrderService
{
    Task<PaginationResponseDto<OrderDto>> GetAllAsync(PaginationRequestDto<OrderSortBy>  request);
    Task<OrderDto> GetById(Guid id);

    Task<OrderDto> AddAsync(CreateOrderDto dto);

    Task<OrderSummaryDto> GetOrderSummary(Guid id);
}
