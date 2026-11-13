using InventoryHub.Application.Dto.Order;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface IOrderRepository
{
    Task<PaginationResponseDto<Order>> GetAllAsync(PaginationRequestDto request);
    Task<Order?> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
}
